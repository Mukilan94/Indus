using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class FormationMarkerTvdPrognoseds
    {
        public FormationMarkerTvdPrognoseds()
        {
            FormationMarkers = new HashSet<FormationMarkers>();
        }

        [Key]
        public int TvdPrognosedId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("TvdPrognosed")]
        public virtual ICollection<FormationMarkers> FormationMarkers { get; set; }
    }
}
