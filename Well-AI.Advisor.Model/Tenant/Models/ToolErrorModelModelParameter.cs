using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ToolErrorModelModelParameter
    {
        public ToolErrorModelModelParameter()
        {
            ToolErrorModels = new HashSet<ToolErrorModels>();
        }

        [Key]
        public int ModelParametersId { get; set; }
        public string MisalignmentMode { get; set; }
        public int? GyroInitializationId { get; set; }
        public int? GyroReinitializationDistanceId { get; set; }
        public string NoiseReductionFactor { get; set; }
        public string Switching { get; set; }

        [ForeignKey(nameof(GyroInitializationId))]
        [InverseProperty(nameof(ToolErrorModelGyroInitializations.ToolErrorModelModelParameter))]
        public virtual ToolErrorModelGyroInitializations GyroInitialization { get; set; }
        [ForeignKey(nameof(GyroReinitializationDistanceId))]
        [InverseProperty(nameof(ToolErrorModelGyroReinitializationDistances.ToolErrorModelModelParameter))]
        public virtual ToolErrorModelGyroReinitializationDistances GyroReinitializationDistance { get; set; }
        [InverseProperty("ModelParameters")]
        public virtual ICollection<ToolErrorModels> ToolErrorModels { get; set; }
    }
}
