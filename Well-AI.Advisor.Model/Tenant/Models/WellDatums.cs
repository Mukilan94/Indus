using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class WellDatums
    {
        [Key]
        public int DatumId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int? ElevationId { get; set; }
        public string Uid { get; set; }
        public int? DatumNameId { get; set; }
        public int? WellId { get; set; }

        [ForeignKey(nameof(DatumNameId))]
        [InverseProperty(nameof(WellDatumNames.WellDatums))]
        public virtual WellDatumNames DatumName { get; set; }
        [ForeignKey(nameof(ElevationId))]
        [InverseProperty(nameof(WellElevations.WellDatums))]
        public virtual WellElevations Elevation { get; set; }
        [ForeignKey(nameof(WellId))]
        [InverseProperty(nameof(Wells.WellDatums))]
        public virtual Wells Well { get; set; }
    }
}
