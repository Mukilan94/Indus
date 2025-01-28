using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("UserActivityStatus")]
    public class UserActivityStatus
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime LoggedInTime { get; set; }
        public bool IsLoggedIn { get; set; }
    }
}
