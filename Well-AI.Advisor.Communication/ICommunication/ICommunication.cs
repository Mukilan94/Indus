using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Rest.Video.V1;
using Twilio.TwiML;
using Twilio.Types;

namespace Well_AI.Advisor.Communication
{
    public interface ICommunication
    {
        string GenerateCallToken(string role);
        //VoiceResponse ConnectCall(string phoneNumber);
        VoiceResponse ConnectCall(string to, string callingDeviceIdentity);

        Dictionary<string, string> GenerateVideoToken(string identity);
        Task<Dictionary<string, string>> GenerateChatToken(string identity);
        Dictionary<string, string> GetTwilioMessagForChannel(string identity, string channelId);
        RoomResource.RoomStatusEnum CloseRoom(string authToken, string roomId);
        string ConversantionParticipantAdd(string channelId, string user);
    }
}
