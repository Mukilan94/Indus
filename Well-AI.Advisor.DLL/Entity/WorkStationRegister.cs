using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("WorkstationRegister")]
    public class WorkstationRegister
    {
        [Key]
        public System.Guid RegisterationId { get; set; }
        public string CustomerAccountIdentifier { get; set; }
        public string DeviceName { get; set; }
        public string WorkstationIdentifier { get; set; }
        [Column("WorkstationToken")]
        public string WorkstationToken { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
