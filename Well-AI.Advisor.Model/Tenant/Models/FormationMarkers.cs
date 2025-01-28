using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class FormationMarkers
    {
        [Key]
        public int FormationMarkerId { get; set; }
        public string NameWell { get; set; }
        public string NameWellbore { get; set; }
        public string Name { get; set; }
        public int? MdPrognosedId { get; set; }
        public int? TvdPrognosedId { get; set; }
        public int? MdTopSampleId { get; set; }
        public int? TvdTopSampleId { get; set; }
        public int? ThicknessBedId { get; set; }
        public int? ThicknessApparentId { get; set; }
        public int? ThicknessPerpenId { get; set; }
        public int? MdLogSampleId { get; set; }
        public int? TvdLogSampleId { get; set; }
        public int? DipId { get; set; }
        public int? DipDirectionId { get; set; }
        public int? LithostratigraphicId { get; set; }
        public int? ChronostratigraphicId { get; set; }
        public string Description { get; set; }
        public int? CommonDataFormationMarkerCommonDataId { get; set; }
        public string Uid { get; set; }
        public string UidWellbore { get; set; }
        public string UidWell { get; set; }

        [ForeignKey(nameof(ChronostratigraphicId))]
        [InverseProperty(nameof(FormationMarkerChronostratigraphics.FormationMarkers))]
        public virtual FormationMarkerChronostratigraphics Chronostratigraphic { get; set; }
        [ForeignKey(nameof(CommonDataFormationMarkerCommonDataId))]
        [InverseProperty(nameof(FormationMarkerCommonDatas.FormationMarkers))]
        public virtual FormationMarkerCommonDatas CommonDataFormationMarkerCommonData { get; set; }
        [ForeignKey(nameof(DipId))]
        [InverseProperty(nameof(FormationMarkerDips.FormationMarkers))]
        public virtual FormationMarkerDips Dip { get; set; }
        [ForeignKey(nameof(DipDirectionId))]
        [InverseProperty(nameof(FormationMarkerDipDirections.FormationMarkers))]
        public virtual FormationMarkerDipDirections DipDirection { get; set; }
        [ForeignKey(nameof(LithostratigraphicId))]
        [InverseProperty(nameof(FormationMarkerLithostratigraphics.FormationMarkers))]
        public virtual FormationMarkerLithostratigraphics Lithostratigraphic { get; set; }
        [ForeignKey(nameof(MdLogSampleId))]
        [InverseProperty(nameof(FormationMarkerMdLogSamples.FormationMarkers))]
        public virtual FormationMarkerMdLogSamples MdLogSample { get; set; }
        [ForeignKey(nameof(MdPrognosedId))]
        [InverseProperty(nameof(FormationMarkerMdPrognoseds.FormationMarkers))]
        public virtual FormationMarkerMdPrognoseds MdPrognosed { get; set; }
        [ForeignKey(nameof(MdTopSampleId))]
        [InverseProperty(nameof(FormationMarkerMdTopSamples.FormationMarkers))]
        public virtual FormationMarkerMdTopSamples MdTopSample { get; set; }
        [ForeignKey(nameof(ThicknessApparentId))]
        [InverseProperty(nameof(FormationMarkerThicknessApparents.FormationMarkers))]
        public virtual FormationMarkerThicknessApparents ThicknessApparent { get; set; }
        [ForeignKey(nameof(ThicknessBedId))]
        [InverseProperty(nameof(FormationMarkerThicknessBeds.FormationMarkers))]
        public virtual FormationMarkerThicknessBeds ThicknessBed { get; set; }
        [ForeignKey(nameof(ThicknessPerpenId))]
        [InverseProperty(nameof(FormationMarkerThicknessPerpens.FormationMarkers))]
        public virtual FormationMarkerThicknessPerpens ThicknessPerpen { get; set; }
        [ForeignKey(nameof(TvdLogSampleId))]
        [InverseProperty(nameof(FormationMarkerTvdLogSamples.FormationMarkers))]
        public virtual FormationMarkerTvdLogSamples TvdLogSample { get; set; }
        [ForeignKey(nameof(TvdPrognosedId))]
        [InverseProperty(nameof(FormationMarkerTvdPrognoseds.FormationMarkers))]
        public virtual FormationMarkerTvdPrognoseds TvdPrognosed { get; set; }
        [ForeignKey(nameof(TvdTopSampleId))]
        [InverseProperty(nameof(FormationMarkerTvdTopSamples.FormationMarkers))]
        public virtual FormationMarkerTvdTopSamples TvdTopSample { get; set; }
    }
}
