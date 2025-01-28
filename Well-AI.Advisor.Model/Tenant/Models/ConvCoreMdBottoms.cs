using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ConvCoreMdBottoms
    {
        public ConvCoreMdBottoms()
        {
            ConvCoreChromatographs = new HashSet<ConvCoreChromatographs>();
            ConvCoreGeologyIntervals = new HashSet<ConvCoreGeologyIntervals>();
        }

        [Key]
        public int MdBottomId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MdBottom")]
        public virtual ICollection<ConvCoreChromatographs> ConvCoreChromatographs { get; set; }
        [InverseProperty("MdBottom")]
        public virtual ICollection<ConvCoreGeologyIntervals> ConvCoreGeologyIntervals { get; set; }
    }
}
