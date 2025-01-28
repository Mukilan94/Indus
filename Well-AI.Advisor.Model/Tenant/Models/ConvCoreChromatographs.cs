using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ConvCoreChromatographs
    {
        public ConvCoreChromatographs()
        {
            ConvCoreGeologyIntervals = new HashSet<ConvCoreGeologyIntervals>();
        }

        [Key]
        public int ChromatographId { get; set; }
        [Column("DTim")]
        public string Dtim { get; set; }
        public int? MdTopId { get; set; }
        public int? MdBottomId { get; set; }
        public int? WtMudInId { get; set; }
        public int? WtMudOutId { get; set; }
        public string ChromType { get; set; }
        [Column("ETimChromCycleId")]
        public int? EtimChromCycleId { get; set; }
        public string ChromIntRpt { get; set; }
        public int? MethAvId { get; set; }
        public int? MethMnId { get; set; }
        public int? MethMxId { get; set; }
        public int? EthAvId { get; set; }
        public int? EthMnId { get; set; }
        public int? EthMxId { get; set; }
        public int? PropAvId { get; set; }
        public int? PropMnId { get; set; }
        public int? PropMxId { get; set; }
        public int? IbutAvId { get; set; }
        public int? IbutMnId { get; set; }
        public int? IbutMxId { get; set; }
        public int? NbutAvId { get; set; }
        public int? NbutMnId { get; set; }
        public int? NbutMxId { get; set; }
        public int? IpentAvId { get; set; }
        public int? IpentMnId { get; set; }
        public int? IpentMxId { get; set; }
        public int? NpentAvId { get; set; }
        public int? NpentMnId { get; set; }
        public int? NpentMxId { get; set; }
        public int? EpentAvId { get; set; }
        public int? EpentMnId { get; set; }
        public int? EpentMxId { get; set; }
        public int? IhexAvId { get; set; }
        public int? IhexMnId { get; set; }
        public int? IhexMxId { get; set; }
        public int? NhexAvId { get; set; }
        public int? NhexMnId { get; set; }
        public int? NhexMxId { get; set; }
        public int? Co2AvId { get; set; }
        public int? Co2MnId { get; set; }
        public int? Co2MxId { get; set; }
        public int? H2sAvId { get; set; }
        public int? H2sMnId { get; set; }
        public int? H2sMxId { get; set; }
        public int? AcetyleneId { get; set; }

        [ForeignKey(nameof(AcetyleneId))]
        [InverseProperty(nameof(ConvCoreAcetylenes.ConvCoreChromatographs))]
        public virtual ConvCoreAcetylenes Acetylene { get; set; }
        [ForeignKey(nameof(Co2AvId))]
        [InverseProperty(nameof(ConvCoreCo2Avs.ConvCoreChromatographs))]
        public virtual ConvCoreCo2Avs Co2Av { get; set; }
        [ForeignKey(nameof(Co2MnId))]
        [InverseProperty(nameof(ConvCoreCo2Mns.ConvCoreChromatographs))]
        public virtual ConvCoreCo2Mns Co2Mn { get; set; }
        [ForeignKey(nameof(Co2MxId))]
        [InverseProperty(nameof(ConvCoreCo2Mxs.ConvCoreChromatographs))]
        public virtual ConvCoreCo2Mxs Co2Mx { get; set; }
        [ForeignKey(nameof(EpentAvId))]
        [InverseProperty(nameof(ConvCoreEpentAvs.ConvCoreChromatographs))]
        public virtual ConvCoreEpentAvs EpentAv { get; set; }
        [ForeignKey(nameof(EpentMnId))]
        [InverseProperty(nameof(ConvCoreEpentMns.ConvCoreChromatographs))]
        public virtual ConvCoreEpentMns EpentMn { get; set; }
        [ForeignKey(nameof(EpentMxId))]
        [InverseProperty(nameof(ConvCoreEpentMxs.ConvCoreChromatographs))]
        public virtual ConvCoreEpentMxs EpentMx { get; set; }
        [ForeignKey(nameof(EthAvId))]
        [InverseProperty(nameof(ConvCoreEthAvs.ConvCoreChromatographs))]
        public virtual ConvCoreEthAvs EthAv { get; set; }
        [ForeignKey(nameof(EthMnId))]
        [InverseProperty(nameof(ConvCoreEthMns.ConvCoreChromatographs))]
        public virtual ConvCoreEthMns EthMn { get; set; }
        [ForeignKey(nameof(EthMxId))]
        [InverseProperty(nameof(ConvCoreEthMxs.ConvCoreChromatographs))]
        public virtual ConvCoreEthMxs EthMx { get; set; }
        [ForeignKey(nameof(EtimChromCycleId))]
        [InverseProperty(nameof(ConvCoreEtimChromCycles.ConvCoreChromatographs))]
        public virtual ConvCoreEtimChromCycles EtimChromCycle { get; set; }
        [ForeignKey(nameof(H2sAvId))]
        [InverseProperty(nameof(ConvCoreH2sAvs.ConvCoreChromatographs))]
        public virtual ConvCoreH2sAvs H2sAv { get; set; }
        [ForeignKey(nameof(H2sMnId))]
        [InverseProperty(nameof(ConvCoreH2sMns.ConvCoreChromatographs))]
        public virtual ConvCoreH2sMns H2sMn { get; set; }
        [ForeignKey(nameof(H2sMxId))]
        [InverseProperty(nameof(ConvCoreH2sMxs.ConvCoreChromatographs))]
        public virtual ConvCoreH2sMxs H2sMx { get; set; }
        [ForeignKey(nameof(IbutAvId))]
        [InverseProperty(nameof(ConvCoreIbutAvs.ConvCoreChromatographs))]
        public virtual ConvCoreIbutAvs IbutAv { get; set; }
        [ForeignKey(nameof(IbutMnId))]
        [InverseProperty(nameof(ConvCoreIbutMns.ConvCoreChromatographs))]
        public virtual ConvCoreIbutMns IbutMn { get; set; }
        [ForeignKey(nameof(IbutMxId))]
        [InverseProperty(nameof(ConvCoreIbutMxs.ConvCoreChromatographs))]
        public virtual ConvCoreIbutMxs IbutMx { get; set; }
        [ForeignKey(nameof(IhexAvId))]
        [InverseProperty(nameof(ConvCoreIhexAvs.ConvCoreChromatographs))]
        public virtual ConvCoreIhexAvs IhexAv { get; set; }
        [ForeignKey(nameof(IhexMnId))]
        [InverseProperty(nameof(ConvCoreIhexMns.ConvCoreChromatographs))]
        public virtual ConvCoreIhexMns IhexMn { get; set; }
        [ForeignKey(nameof(IhexMxId))]
        [InverseProperty(nameof(ConvCoreIhexMxs.ConvCoreChromatographs))]
        public virtual ConvCoreIhexMxs IhexMx { get; set; }
        [ForeignKey(nameof(IpentAvId))]
        [InverseProperty(nameof(ConvCoreIpentAvs.ConvCoreChromatographs))]
        public virtual ConvCoreIpentAvs IpentAv { get; set; }
        [ForeignKey(nameof(IpentMnId))]
        [InverseProperty(nameof(ConvCoreIpentMns.ConvCoreChromatographs))]
        public virtual ConvCoreIpentMns IpentMn { get; set; }
        [ForeignKey(nameof(IpentMxId))]
        [InverseProperty(nameof(ConvCoreIpentMxs.ConvCoreChromatographs))]
        public virtual ConvCoreIpentMxs IpentMx { get; set; }
        [ForeignKey(nameof(MdBottomId))]
        [InverseProperty(nameof(ConvCoreMdBottoms.ConvCoreChromatographs))]
        public virtual ConvCoreMdBottoms MdBottom { get; set; }
        [ForeignKey(nameof(MdTopId))]
        [InverseProperty(nameof(ConvCoreMdTops.ConvCoreChromatographs))]
        public virtual ConvCoreMdTops MdTop { get; set; }
        [ForeignKey(nameof(MethAvId))]
        [InverseProperty(nameof(ConvCoreMethAvs.ConvCoreChromatographs))]
        public virtual ConvCoreMethAvs MethAv { get; set; }
        [ForeignKey(nameof(MethMnId))]
        [InverseProperty(nameof(ConvCoreMethMns.ConvCoreChromatographs))]
        public virtual ConvCoreMethMns MethMn { get; set; }
        [ForeignKey(nameof(MethMxId))]
        [InverseProperty(nameof(ConvCoreMethMxs.ConvCoreChromatographs))]
        public virtual ConvCoreMethMxs MethMx { get; set; }
        [ForeignKey(nameof(NbutAvId))]
        [InverseProperty(nameof(ConvCoreNbutAvs.ConvCoreChromatographs))]
        public virtual ConvCoreNbutAvs NbutAv { get; set; }
        [ForeignKey(nameof(NbutMnId))]
        [InverseProperty(nameof(ConvCoreNbutMns.ConvCoreChromatographs))]
        public virtual ConvCoreNbutMns NbutMn { get; set; }
        [ForeignKey(nameof(NbutMxId))]
        [InverseProperty(nameof(ConvCoreNbutMxs.ConvCoreChromatographs))]
        public virtual ConvCoreNbutMxs NbutMx { get; set; }
        [ForeignKey(nameof(NhexAvId))]
        [InverseProperty(nameof(ConvCoreNhexAvs.ConvCoreChromatographs))]
        public virtual ConvCoreNhexAvs NhexAv { get; set; }
        [ForeignKey(nameof(NhexMnId))]
        [InverseProperty(nameof(ConvCoreNhexMns.ConvCoreChromatographs))]
        public virtual ConvCoreNhexMns NhexMn { get; set; }
        [ForeignKey(nameof(NhexMxId))]
        [InverseProperty(nameof(ConvCoreNhexMxs.ConvCoreChromatographs))]
        public virtual ConvCoreNhexMxs NhexMx { get; set; }
        [ForeignKey(nameof(NpentAvId))]
        [InverseProperty(nameof(ConvCoreNpentAvs.ConvCoreChromatographs))]
        public virtual ConvCoreNpentAvs NpentAv { get; set; }
        [ForeignKey(nameof(NpentMnId))]
        [InverseProperty(nameof(ConvCoreNpentMns.ConvCoreChromatographs))]
        public virtual ConvCoreNpentMns NpentMn { get; set; }
        [ForeignKey(nameof(NpentMxId))]
        [InverseProperty(nameof(ConvCoreNpentMxs.ConvCoreChromatographs))]
        public virtual ConvCoreNpentMxs NpentMx { get; set; }
        [ForeignKey(nameof(PropAvId))]
        [InverseProperty(nameof(ConvCorePropAvs.ConvCoreChromatographs))]
        public virtual ConvCorePropAvs PropAv { get; set; }
        [ForeignKey(nameof(PropMnId))]
        [InverseProperty(nameof(ConvCorePropMns.ConvCoreChromatographs))]
        public virtual ConvCorePropMns PropMn { get; set; }
        [ForeignKey(nameof(PropMxId))]
        [InverseProperty(nameof(ConvCorePropMxs.ConvCoreChromatographs))]
        public virtual ConvCorePropMxs PropMx { get; set; }
        [ForeignKey(nameof(WtMudInId))]
        [InverseProperty(nameof(ConvCoreWtMudIns.ConvCoreChromatographs))]
        public virtual ConvCoreWtMudIns WtMudIn { get; set; }
        [ForeignKey(nameof(WtMudOutId))]
        [InverseProperty(nameof(ConvCoreWtMudOuts.ConvCoreChromatographs))]
        public virtual ConvCoreWtMudOuts WtMudOut { get; set; }
        [InverseProperty("Chromatograph")]
        public virtual ICollection<ConvCoreGeologyIntervals> ConvCoreGeologyIntervals { get; set; }
    }
}
