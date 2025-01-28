using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("Configuration")]
    public class ConfigurationSetting
    {
        [Key]
        public int Index { get; set; }
        public string FriendlyName { get; set; }
        public string ConstantName { get; set; }
        public string Value { get; set; }
    }
}
