using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.Model.ServiceCompany.Models;
namespace WellAI.Advisor.DLL.IRepository
{
    public interface ITwilioChat
    {
       public Task<byte> AddTwilioUserChannel(string fromUser, string toUser,string channelSID, string userIdentity, string channelUniqueName);
        public string GetChannelForFromAndToUser(string fromUser, string toUser);

        public string GetUniqueIdentifier(string fromUser);

        public string GetAdminSupportUser();    
        public string GetAdminSupportPhone();
    }
}