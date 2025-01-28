using System;
using System.Collections.Generic;
using System.Text;

namespace WellAI.Advisor.Model.OperatingCompany.Models
{
    public class CommunicationModel
    {
        public string UserName { get; set; }
        public string UserLogoPath { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CompanyName { get; set; }
        public string LogoPath { get; set; }
        public string CompanyWebsite { get; set; }
        public int ClientContactId { get; set; }

        public string TenantId { get; set; }
        public string ClientUserId { get; set; }
    }

    public class ChatRoomModel
    {
        public string UserId { get; set; }

        public string UserRoomId { get; set; }

        public string UserRoomName { get; set; }

        public string LastChatDate { get; set; }

        public string LastChatMessage { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string CompanyName { get; set; }

        public string UserName { get; set; }
    }

    public class CommunicationViewModel
    {
        public IEnumerable<CommunicationModel> CommunicationModel;
        public IEnumerable<ChatRoomModel> ChatRoomModel;
        public IEnumerable<CompanyServicesModel> CompanyServicesModel;
    }

    public class ManageCallModel
    {
        public string Phone { get; set; }
    }
}
