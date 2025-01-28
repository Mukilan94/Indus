using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WellAI.Advisor.Model.OperatingCompany.Models
{
    public class PadModel
    {
        [StringLength(40)]
        public string Pad_id { get; set; }
        [StringLength(100)]
        [Required]
        public string Pad_Name { get; set; }
        [Required]
        public double? Latitude { get; set; }
        [Required]
        public double? Longitude { get; set; }
        [StringLength(80)]
        public string TenantID { get; set; }
        public bool isActive { get; set; }
    }
}
