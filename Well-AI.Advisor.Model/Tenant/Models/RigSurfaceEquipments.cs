using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RigSurfaceEquipments
    {
        public RigSurfaceEquipments()
        {
            Rigs = new HashSet<Rigs>();
        }

        [Key]
        public int SurfaceEquipmentId { get; set; }
        public string Description { get; set; }
        public int? PresRatingId { get; set; }
        public string TypeSurfEquip { get; set; }
        public string UsePumpDischarge { get; set; }
        public string UseStandpipe { get; set; }
        public string UseHose { get; set; }
        public string UseSwivel { get; set; }
        public string UseKelly { get; set; }
        public string UseTopStack { get; set; }
        public string UseInjStack { get; set; }
        public string UseSurfaceIron { get; set; }
        public int? IdStandpipeId { get; set; }
        public int? LenStandpipeId { get; set; }
        public int? IdHoseId { get; set; }
        public int? LenHoseId { get; set; }
        public int? IdSwivelId { get; set; }
        public int? LenSwivelId { get; set; }
        public int? IdKellyId { get; set; }
        public int? LenKellyId { get; set; }
        public int? IdDischargeLineId { get; set; }
        public int? LenDischargeLineId { get; set; }
        public string CtWrapType { get; set; }
        public int? OdReelId { get; set; }
        public int? OdCoreId { get; set; }
        public int? WidReelWrapId { get; set; }
        public int? LenReelId { get; set; }
        public string InjStkUp { get; set; }
        public int? HtInjStkId { get; set; }
        public string UmbInside { get; set; }
        public int? OdUmbilicalId { get; set; }
        public int? LenUmbilicalId { get; set; }
        public int? IdTopStkId { get; set; }
        public int? HtTopStkId { get; set; }
        public int? HtFlangeId { get; set; }

        [ForeignKey(nameof(HtFlangeId))]
        [InverseProperty(nameof(RigHtFlanges.RigSurfaceEquipments))]
        public virtual RigHtFlanges HtFlange { get; set; }
        [ForeignKey(nameof(HtInjStkId))]
        [InverseProperty(nameof(RigHtInjStks.RigSurfaceEquipments))]
        public virtual RigHtInjStks HtInjStk { get; set; }
        [ForeignKey(nameof(HtTopStkId))]
        [InverseProperty(nameof(RigHtTopStks.RigSurfaceEquipments))]
        public virtual RigHtTopStks HtTopStk { get; set; }
        [ForeignKey(nameof(IdDischargeLineId))]
        [InverseProperty(nameof(RigIdDischargeLines.RigSurfaceEquipments))]
        public virtual RigIdDischargeLines IdDischargeLine { get; set; }
        [ForeignKey(nameof(IdHoseId))]
        [InverseProperty(nameof(RigIdHoses.RigSurfaceEquipments))]
        public virtual RigIdHoses IdHose { get; set; }
        [ForeignKey(nameof(IdKellyId))]
        [InverseProperty(nameof(RigIdKellys.RigSurfaceEquipments))]
        public virtual RigIdKellys IdKelly { get; set; }
        [ForeignKey(nameof(IdStandpipeId))]
        [InverseProperty(nameof(RigIdStandpipes.RigSurfaceEquipments))]
        public virtual RigIdStandpipes IdStandpipe { get; set; }
        [ForeignKey(nameof(IdSwivelId))]
        [InverseProperty(nameof(RigIdSwivels.RigSurfaceEquipments))]
        public virtual RigIdSwivels IdSwivel { get; set; }
        [ForeignKey(nameof(IdTopStkId))]
        [InverseProperty(nameof(RigIdTopStks.RigSurfaceEquipments))]
        public virtual RigIdTopStks IdTopStk { get; set; }
        [ForeignKey(nameof(LenDischargeLineId))]
        [InverseProperty(nameof(RigLenDischargeLines.RigSurfaceEquipments))]
        public virtual RigLenDischargeLines LenDischargeLine { get; set; }
        [ForeignKey(nameof(LenHoseId))]
        [InverseProperty(nameof(RigLenHoses.RigSurfaceEquipments))]
        public virtual RigLenHoses LenHose { get; set; }
        [ForeignKey(nameof(LenKellyId))]
        [InverseProperty(nameof(RigLenKellys.RigSurfaceEquipments))]
        public virtual RigLenKellys LenKelly { get; set; }
        [ForeignKey(nameof(LenReelId))]
        [InverseProperty(nameof(RigLenReels.RigSurfaceEquipments))]
        public virtual RigLenReels LenReel { get; set; }
        [ForeignKey(nameof(LenStandpipeId))]
        [InverseProperty(nameof(RigLenStandpipes.RigSurfaceEquipments))]
        public virtual RigLenStandpipes LenStandpipe { get; set; }
        [ForeignKey(nameof(LenSwivelId))]
        [InverseProperty(nameof(RigLenSwivels.RigSurfaceEquipments))]
        public virtual RigLenSwivels LenSwivel { get; set; }
        [ForeignKey(nameof(LenUmbilicalId))]
        [InverseProperty(nameof(RigLenUmbilicals.RigSurfaceEquipments))]
        public virtual RigLenUmbilicals LenUmbilical { get; set; }
        [ForeignKey(nameof(OdCoreId))]
        [InverseProperty(nameof(RigOdCores.RigSurfaceEquipments))]
        public virtual RigOdCores OdCore { get; set; }
        [ForeignKey(nameof(OdReelId))]
        [InverseProperty(nameof(RigOdReels.RigSurfaceEquipments))]
        public virtual RigOdReels OdReel { get; set; }
        [ForeignKey(nameof(OdUmbilicalId))]
        [InverseProperty(nameof(RigOdUmbilicals.RigSurfaceEquipments))]
        public virtual RigOdUmbilicals OdUmbilical { get; set; }
        [ForeignKey(nameof(PresRatingId))]
        [InverseProperty(nameof(RigPresRatings.RigSurfaceEquipments))]
        public virtual RigPresRatings PresRating { get; set; }
        [ForeignKey(nameof(WidReelWrapId))]
        [InverseProperty(nameof(RigWidReelWraps.RigSurfaceEquipments))]
        public virtual RigWidReelWraps WidReelWrap { get; set; }
        [InverseProperty("SurfaceEquipment")]
        public virtual ICollection<Rigs> Rigs { get; set; }
    }
}
