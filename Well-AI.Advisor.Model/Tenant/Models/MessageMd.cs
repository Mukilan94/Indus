using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MessageMd
    {
        public MessageMd()
        {
            Messages = new HashSet<Messages>();
        }

        [Key]
        public int MdId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("Md")]
        public virtual ICollection<Messages> Messages { get; set; }
    }
}
