using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Well_AI.Advisor.API.Samsara.Models
{
    public partial class Configuration
    {
        [Key]
        public int Index { get; set; }
        public string FriendlyName { get; set; }
        public string ConstantName { get; set; }
        public string Value { get; set; }
    }
}
