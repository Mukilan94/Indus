using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class Configuration
    {
        [Key]
        public int Index { get; set; }
        [Required]
        public string FriendlyName { get; set; }
        [Required]
        public string ConstantName { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
