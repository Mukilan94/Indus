using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class FormationMarkerCommonDatas
    {
        public FormationMarkerCommonDatas()
        {
            FormationMarkers = new HashSet<FormationMarkers>();
        }

        [Key]
        public int FormationMarkerCommonDataId { get; set; }
        public string ItemState { get; set; }
        public string Comments { get; set; }

        [InverseProperty("CommonDataFormationMarkerCommonData")]
        public virtual ICollection<FormationMarkers> FormationMarkers { get; set; }
    }
}
