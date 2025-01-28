using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MessageMdBit
    {
        public MessageMdBit()
        {
            Messages = new HashSet<Messages>();
        }

        [Key]
        public int MdBitId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MdBit")]
        public virtual ICollection<Messages> Messages { get; set; }
    }
}
