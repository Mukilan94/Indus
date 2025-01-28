using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.DLL.IRepository;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.Model.ServiceCompany.Models;

namespace WellAI.Advisor.DLL.Repository
{
    public class TwilioChat : ITwilioChat
    {
        private readonly WebAIAdvisorContext db;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;

        public TwilioChat(WebAIAdvisorContext db, RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager)
        {
            this.db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<byte> AddTwilioUserChannel(string fromUser, string toUser, string channelSID,string userIdentity, string uniqueName)
        {
            
            try
            {
                byte invitationstatus;
                var twiliouserchannel = db.TwilioChatUserMappings.Where(x => x.channelsid == channelSID).FirstOrDefault();
                if (twiliouserchannel == null)
                {

                    db.TwilioChatUserMappings.Add(new TwilioChatUserMappings
                    {
                        channelsid = channelSID,
                        sendername = fromUser,
                        receivername = toUser,
                        channeluniquename = uniqueName,
                        useridentity = userIdentity,
                        invitationstatus = 0
                    }
                    );
                    await db.SaveChangesAsync();

                   


                    invitationstatus = 0;
                }
                else if (twiliouserchannel.invitationstatus == 1)
                {
                    invitationstatus = 1;
                }
                else
                {
                    invitationstatus = 2;
                }

                return invitationstatus;
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "TwilioChat AddTwilioUserChannel", null);
                return 0;
            }
        }

        public async Task<byte> UpdateTwilioUserChannel(string fromUser, string toUser, string channelSID)
        {
            try
            {
                byte invitationstatus;

                var twiliouserchannel = db.TwilioChatUserMappings.Where(x => x.channelsid == channelSID).FirstOrDefault();
                if (twiliouserchannel != null)
                {
                    if (twiliouserchannel.invitationstatus != 1)
                    {
                        twiliouserchannel.channelsid = channelSID;
                        twiliouserchannel.sendername = fromUser;
                        twiliouserchannel.receivername = toUser;
                        twiliouserchannel.invitationstatus = 1;
                        db.TwilioChatUserMappings.Update(twiliouserchannel);
                        await db.SaveChangesAsync();
                        invitationstatus = 1;
                    }
                    else
                    {
                        invitationstatus = 2;
                    }
                }
                else
                {
                    invitationstatus = 2;
                }
                return invitationstatus;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "TwilioChat UpdateTwilioUserChannel", null);
                return 0;
            }
        }


        public string GetChannelForFromAndToUser(string fromUser, string toUser)
        {
            try
            {
                string channelid = "";
                string uniquenameFirst = "c_" + fromUser + "_" + toUser;
                string uniquenameSecond = "c_" + toUser + "_" + fromUser;

                var twiliouserchannel = db.TwilioChatUserMappings.Where(x => x.channeluniquename == uniquenameFirst).FirstOrDefault();
                if (twiliouserchannel != null)
                {
                    channelid = twiliouserchannel.channelsid;
                }
                else
                {
                    twiliouserchannel = db.TwilioChatUserMappings.Where(x => x.channeluniquename == uniquenameSecond).FirstOrDefault();
                    if (twiliouserchannel != null)
                    {
                        channelid = twiliouserchannel.channelsid;
                    }
                    else
                    {
                        channelid = "";
                    }
                }
                return channelid;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "TwilioChat GetChannelForFromAndToUser", null);
                return null;
            }
        }

        public string GetUniqueIdentifier(string fromUser)
        {
            try
            {
                string uniqueIdentifier = "";
                var twiliouserchannel = db.TwilioChatUserMappings.Where(x => x.sendername == fromUser).FirstOrDefault();
                if (twiliouserchannel != null)
                {
                    uniqueIdentifier = twiliouserchannel.useridentity;
                }
                else
                {
                    uniqueIdentifier = "";
                }
                return uniqueIdentifier;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "TwilioChat GetUniqueIdentifier", null);
                return null;
            }
        }

        public string GetAdminSupportUser()
        {
            try
            {
                var adminsupportUser= db.ConfigurationSettings.Where(x => x.FriendlyName == "Support Admin User").FirstOrDefault();
                return adminsupportUser.Value.ToString();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "TwilioChat GetAdminSupportUser", null);
                return null;
            }
            
        }

        public string GetAdminSupportPhone()
        {
            try
            {
                var adminsupportPhone = db.ConfigurationSettings.Where(x => x.FriendlyName == "Support Admin Phone").FirstOrDefault();
                return adminsupportPhone.Value.ToString();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "TwilioChat GetAdminSupportPhone", null);
                return null;
            }

        }

        public async Task<byte> UpdateTwilioUserChannelLeaveStatus(string fromUser, string toUser, string channelSID)
        {
            try
            {
                byte invitationstatus;

                var twiliouserchannel = db.TwilioChatUserMappings.Where(x => x.channelsid == channelSID).FirstOrDefault();
                if (twiliouserchannel != null)
                {

                    twiliouserchannel.channelsid = channelSID;
                    twiliouserchannel.sendername = fromUser;
                    twiliouserchannel.receivername = toUser;
                    twiliouserchannel.channeluniquename = "";
                    twiliouserchannel.invitationstatus = 3;
                    db.TwilioChatUserMappings.Update(twiliouserchannel);
                    await db.SaveChangesAsync();
                    invitationstatus = 3;
                }
                else
                {
                    invitationstatus = 2;
                }
                return invitationstatus;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "TwilioChat UpdateTwilioUserChannelLeaveStatus", null);
                return 0;
            }
        }

    }
}