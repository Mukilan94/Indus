using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class FormationMarkerChronostratigraphics
    {
        public FormationMarkerChronostratigraphics()
        {
            FormationMarkers = new HashSet<FormationMarkers>();
        }

        [Key]
        public int ChronostratigraphicId { get; set; }
        public string Kind { get; set; }
        public string Text { get; set; }

        [InverseProperty("Chronostratigraphic")]
        public virtual ICollection<FormationMarkers> FormationMarkers { get; set; }
    }
}
