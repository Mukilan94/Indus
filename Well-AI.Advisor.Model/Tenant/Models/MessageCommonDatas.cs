using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MessageCommonDatas
    {
        public MessageCommonDatas()
        {
            Messages = new HashSet<Messages>();
        }

        [Key]
        public int MessageCommonDataId { get; set; }
        public string ItemState { get; set; }
        public string Comments { get; set; }

        [InverseProperty("CommonDataMessageCommonData")]
        public virtual ICollection<Messages> Messages { get; set; }
    }
}
