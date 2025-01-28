using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("WELL_REGISTER")]
    public partial class WELL_REGISTER
    {
        [Key]
        [StringLength(40)]
        public string well_id { get; set; }

        [StringLength(100)]
        public string wellname { get; set; }

        [StringLength(40)]
        public string welltype_id { get; set; }

        [StringLength(100)]
        public string Country { get; set; }

        public byte Midland { get; set; }

        public byte Delaware { get; set; }

        public byte Conplete_well_drill { get; set; }

        public byte Batch_drill_casing { get; set; }

        public byte Batch_drill_horizontal { get; set; }

        public byte Casing_string { get; set; }

        public string NumAPI { get; set; }

        public string NumAFE { get; set; }

        [StringLength(255)]
        public string customer_id { get; set; }

        public string RigID { get; set; }

        public bool Prediction { get; set; }
        public string Router_WellId { get; set; }
    }
}