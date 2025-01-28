using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class FormationMarkerTvdTopSamples
    {
        public FormationMarkerTvdTopSamples()
        {
            FormationMarkers = new HashSet<FormationMarkers>();
        }

        [Key]
        public int TvdTopSampleId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TvdTopSample")]
        public virtual ICollection<FormationMarkers> FormationMarkers { get; set; }
    }
}
