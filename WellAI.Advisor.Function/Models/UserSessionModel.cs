using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WellAI.Advisor.Function.MailQueue.Models
{
    [Table("UserSessions")]
    public class UserSessionModel
    {
        [Key]
        [Required]
        public string SessionId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime SessionTimeStamp { get; set; }
    }

    [Table("StaffUserSessions")]
    public class StaffUserSessionsModel
    {
        [Key]
        [Required]
        public string SessionId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime SessionTimeStamp { get; set; }
    }
}
