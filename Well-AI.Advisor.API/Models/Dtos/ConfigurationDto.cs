using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WellAI.Advisor.API.Models.Dtos
{
    public class ConfigurationDto
    {

        [Key]
        [Required]
        public int Index { get; set; }
        [Required]
        public string FriendlyName { get; set; }
        [Required]
        public string ConstantName { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
