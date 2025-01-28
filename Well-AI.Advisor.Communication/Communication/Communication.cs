using System;
using Twilio;
//Conversations api
using Twilio.Rest.Api.V2010.Account;
using Twilio.Rest.Conversations;
using Twilio.TwiML;
using Twilio.Types;
using System.Threading;
using System.Threading.Tasks;
using Twilio.TwiML.Voice;
using System.Collections.Generic;
using Twilio.Jwt;
using Twilio.Jwt.Client;
using Twilio.Base;
using WellAI.Advisor.Model.TwilioCredentials;
using Microsoft.Extensions.Options;
using Twilio.Jwt.AccessToken;
using Twilio.Rest.Video.V1;
using Twilio.Rest.Chat.V1.Service;
using WellAI.Advisor.DLL.Data;
//
using Twilio.Rest.Conversations.V1;
//using Twilio.Rest.Conversations.V1.Conversation;
using Twilio.Rest.Conversations.V1.Service.Conversation;
using MessageResource = Twilio.Rest.Conversations.V1.Conversation.MessageResource;
using System.Text.RegularExpressions;
using Faker;
using Faker.Extensions;

namespace Well_AI.Advisor.Communication
{
    public class Communication : ICommunication
    {
        private readonly TwilioCredentials credentialsChat;
        private readonly TwilioAccountDetails credentialsVoice;
        private readonly WebAIAdvisorContext db;
        public Communication(Microsoft.Extensions.Options.IOptions<TwilioCredentials> credentials, Microsoft.Extensions.Options.IOptions<TwilioAccountDetails> voiceCredentials,WebAIAdvisorContext wdb)
        {
            this.credentialsChat = credentials.Value;
            this.credentialsVoice = voiceCredentials.Value;
            db = wdb;
        }
        public string GenerateCallToken(string role)
        {
            var identity = Internet.UserName().AlphanumericOnly();

            //var scopes = new HashSet<IScope>
            //{
            //    new IncomingClientScope(role),
            //    new OutgoingClientScope(credentials.TwiMLApplicationSID)
            //};
            //var capability = new ClientCapability(credentials.AccountSID,
            //                                      credentials.AuthToken,
            //                                      scopes: scopes);
            var grant = new VoiceGrant();
            grant.OutgoingApplicationSid = credentialsVoice.TwimlAppSid;
            grant.IncomingAllow = true;

            var grants = new HashSet<IGrant>
            {
                { grant }
            };

            var token = new Token(
                credentialsVoice.AccountSid,
                credentialsVoice.ApiSid,
                credentialsVoice.ApiSecret,
                role,
                grants: grants).ToJwt();
            return token;

        }
        public string GenerateCallNewToken(string role)
        {
            var scopes = new HashSet<IScope>
            {
                new IncomingClientScope(role),
                new OutgoingClientScope(credentialsVoice.TwimlAppSid)
            };
            var capability = new ClientCapability(credentialsVoice.AccountSid,
                                                  credentialsVoice.AuthToken,
                                                  scopes: scopes);

            return capability.ToJwt();
        }

        public VoiceResponse ConnectCall(string to, string callingDeviceIdentity)
        {
            //var response = new VoiceResponse();

            var twiml = new VoiceResponse();
            string role = "customer";

            //Console.WriteLine($"to: {to}, callingDeviceIdentity: {callingDeviceIdentity}, thisDevice.Identity: {Device.Identity}");

            // someone calls into my Twilio Number, there is no thisDeviceIdentity passed to the /voice endpoint 
            if (string.IsNullOrEmpty(callingDeviceIdentity))
            {
                var dial = new Dial();
                var client = new Twilio.TwiML.Voice.Client();
                client.Identity(role);
                dial.Append(client);
                twiml.Append(dial);
            }
            else if (callingDeviceIdentity != credentialsVoice.CallerId)
            {
                var dial = new Dial();
                var client = new Twilio.TwiML.Voice.Client();
                client.Identity(role);
                dial.Append(client);
                twiml.Append(dial);
            }
            // if the POST request contains your browser device's identity
            // make an outgoing call to either another client or a number
            else
            {
                var dial = new Dial(callerId: credentialsVoice.CallerId);

                // check if the 'To' property in the POST request is
                // a client name or a phone number
                // and dial appropriately using either Number or Client

                if (Regex.IsMatch(to, "^[\\d\\+\\-\\(\\) ]+$"))
                {
                    Console.WriteLine("Match is true");
                    dial.Number(to);
                }
                else
                {
                    var client = new Twilio.TwiML.Voice.Client();
                    client.Identity(to);
                    dial.Append(client);

                }

                twiml.Append(dial);
            }

            Console.WriteLine(twiml.ToString());

            return twiml;
        }

        //public VoiceResponse ConnectCall(string phoneNumber)
        //{
        //    var response = new VoiceResponse();

        //    var dial = new Dial(callerId: credentials.PhoneNumber);
        //    if (!string.IsNullOrEmpty(phoneNumber))
        //    {
        //        dial.Number(phoneNumber);
        //    }
        //    else
        //    {
        //        dial.Client("support_agent");
        //    }
        //    response.Append(dial);

        //    return response;
        //}

        private void AddOutgoingCallerID(string phoneNumber)
        {
            TwilioClient.Init(credentialsVoice.AccountSid, credentialsVoice.AuthToken);

            var outgoingCallerIds = OutgoingCallerIdResource.Read(
                phoneNumber: new Twilio.Types.PhoneNumber(phoneNumber),
                limit: 10
            );
            foreach (var record in outgoingCallerIds)
            {
                var kk = record;
            }
            var validationRequest = ValidationRequestResource.Create(
            friendlyName: "My Home Phone Number",
            phoneNumber: new Twilio.Types.PhoneNumber(phoneNumber)
            );
        }

        public Dictionary<string, string> GenerateVideoToken(string identity)
        {
            if (string.IsNullOrEmpty(identity))
            {
                identity = Guid.NewGuid().ToString();
            }

            var grant = new VideoGrant();
            grant.Room = "WellAI";
            var grants = new HashSet<IGrant> { grant };

            var token = new Token(credentialsVoice.AccountSid, credentialsVoice.ApiSid, credentialsVoice.ApiSecret, identity: identity, grants: grants);

            return new Dictionary<string, string>()
            {
                {"identity", identity},
                {"token", token.ToJwt()},
                {"room","Test" }
            };
        }
        //Phase II Changes - 03/12/2021
        public RoomResource.RoomStatusEnum CloseRoom(string authToken,string roomId)
        {
            TwilioClient.Init(credentialsVoice.AccountSid, authToken);

            var room = RoomResource.Update(
            status: RoomResource.RoomStatusEnum.Completed,
                pathSid: roomId
            );

            return room.Status;
        }

        //Conversation Token
        public async Task<Dictionary<string, string>> GenerateChatToken(string userIdentity)
        {
            // These values are necessary for any access token
            //const string twilioAccountSid = "ACXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
            //const string twilioApiKey = "SKXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
            //const string twilioApiSecret = "your_secret";

            // These are specific to Chat
            string serviceSid = credentialsChat.ChatServiceSid;
            string identity = userIdentity;

            // Create an Chat grant for this token

            var grant = new ChatGrant
            {
                ServiceSid = serviceSid
            };

            var grants = new HashSet<IGrant>
            {
                { 
                    grant 
                }
            };

            // Create an Access Token generator
            var token = new Token(
                credentialsChat.AccountSID,
                credentialsChat.ApiKey,
                credentialsChat.ApiSecret,
                identity,
                grants: grants);

            //return 
            return await System.Threading.Tasks.Task.FromResult(new Dictionary<string, string>()
                {
                    {"identity", identity},
                    {"token", token.ToJwt()}
                });
            //Console.WriteLine(token.ToJwt());
        }
        //Chat Token
        //public async Task<Dictionary<string, string>> GenerateChatToken(string identity)
        //{
        //    var grants = new HashSet<IGrant>();

        //    if (credentials.ChatServiceSid != String.Empty)
        //    {

        //        var chatGrant = new ChatGrant()
        //        {
        //            ServiceSid = credentials.ChatServiceSid
        //        };

        //        grants.Add(chatGrant);
        //    }

        //    if (string.IsNullOrEmpty(identity))
        //    {
        //        identity = Guid.NewGuid().ToString();
        //    }

        //    var token =  new Token(credentials.AccountSID, credentials.ApiKey, credentials.ApiSecret, identity: identity, grants: grants);



        //    return await System.Threading.Tasks.Task.FromResult(new Dictionary<string, string>()
        //    {
        //        {"identity", identity},
        //        {"token", token.ToJwt()}
        //    });
        //}

        public Dictionary<string, string> GetTwilioMessagForChannel(string identity,string channelId)
        {
            string chatDate="";
            string chatMsg="";

            var grants = new HashSet<IGrant>(); 

            if (credentialsChat.ChatServiceSid != String.Empty)
            {
                var chatGrant = new ChatGrant()
                {
                    ServiceSid = credentialsChat.ChatServiceSid
                };

                grants.Add(chatGrant);
            }

            if (string.IsNullOrEmpty(identity))
            {
                identity = Guid.NewGuid().ToString();
            }

            var token = new Token(credentialsChat.AccountSID, credentialsChat.ApiKey, credentialsChat.ApiSecret, identity: identity, grants: grants);

            string accountSid = credentialsChat.AccountSID;
            string authToken = token.Id;

          
            try
            {
                TwilioClient.Init(accountSid, credentialsChat.AuthToken);

                //Conversations API
                //var members = Twilio.Rest.Chat.V2.Service.Channel.MemberResource.Read(
                //                    pathServiceSid: credentials.ChatServiceSid,
                //                    pathChannelSid: channelId,
                //                    limit: 20
                //                );

                var members = ParticipantResource.Read(credentialsChat.ChatServiceSid,channelId,20);

                foreach (var memrecord in members)
                {

                    if (memrecord.Identity == identity)
                    {
                        //var messages = Twilio.Rest.Chat.V2.Service.Channel.MessageResource.Read(
                        //   pathServiceSid: credentials.ChatServiceSid,
                        //   pathChannelSid: channelId,
                        //   limit: 1
                        //   );

                        var messages = MessageResource.Read(
                                        pathConversationSid: channelId
                                        );

                        if (messages != null)
                        {
                            foreach (var record in messages)
                            {
                                chatDate = Convert.ToDateTime(record.DateCreated).ToString("yyyy-MM-dd");
                                chatMsg = record.Body.ToString();
                            }

                        }
                    }
                }

                           
                return new Dictionary<string, string>()
                {
                    {"chatDate", chatDate},
                    {"chatMessage", chatMsg}
                };
            }
           catch(Exception ex)
            {
                CustomErrorHandlerForCommunication customErrorHandler = new CustomErrorHandlerForCommunication(null, null, db, Guid.Empty, Guid.Empty);
                customErrorHandler.WriteError(ex, "Communication GetTwilioMessageForChannel", null);
                return new Dictionary<string, string>()
                {
                    {"chatDate", chatDate},
                    {"chatMessage", chatMsg}
                };

            }

        }
        public string ConversantionParticipantAdd(string channelId, string user)
        {
            try
            {
                TwilioClient.Init(credentialsChat.AccountSID, credentialsChat.AuthToken);

                var participant = ParticipantResource.Create(credentialsChat.ChatServiceSid,channelId, user);

                return participant.AccountSid;

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForCommunication customErrorHandler = new CustomErrorHandlerForCommunication(null, null, db, Guid.Empty, Guid.Empty);
                customErrorHandler.WriteError(ex, "Communication ConversantionParticipantAdd", null);
                return "Participant Not added";

            }
        }
    }
}
