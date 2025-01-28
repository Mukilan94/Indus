using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("StaffUserSessions")]
    public class StaffUserSessions
    {
        [Key]
        [StringLength(40)]
        public string SessionId { get; set; }
        [StringLength(40)]
        public string UserId { get; set; }
        [StringLength(256)]
        public string UserName { get; set; }
        public DateTime SessionTimeStamp { get; set; }

    }
}
