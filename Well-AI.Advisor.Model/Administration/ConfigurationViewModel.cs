using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WellAI.Advisor.Model.Administration
{
    public class ConfigurationViewModel
    {
        public int Index { get; set; }
        [Required]
        [Display(Name = "Friendly Name")]
        public string FriendlyName { get; set; }
        [Required]
        [Display(Name = "Constant Name")]
        public string ConstantName { get; set; }
        [Required]
        public string Value { get; set; }
        public string ErrorMessage { get; set; }

    }
}
