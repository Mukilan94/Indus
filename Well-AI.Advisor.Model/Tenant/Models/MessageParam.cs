using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MessageParam
    {
        public MessageParam()
        {
            Messages = new HashSet<Messages>();
        }

        [Key]
        public string Index { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }

        [InverseProperty("ParamIndexNavigation")]
        public virtual ICollection<Messages> Messages { get; set; }
    }
}
