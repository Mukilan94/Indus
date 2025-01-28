using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class MudLogChromatograph
    {
        public MudLogChromatograph()
        {
            MudLogGeologyInterval = new HashSet<MudLogGeologyInterval>();
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
        [InverseProperty(nameof(MudLogAcetylene.MudLogChromatograph))]
        public virtual MudLogAcetylene Acetylene { get; set; }
        [ForeignKey(nameof(Co2AvId))]
        [InverseProperty(nameof(MudLogCo2Av.MudLogChromatograph))]
        public virtual MudLogCo2Av Co2Av { get; set; }
        [ForeignKey(nameof(Co2MnId))]
        [InverseProperty(nameof(MudLogCo2Mn.MudLogChromatograph))]
        public virtual MudLogCo2Mn Co2Mn { get; set; }
        [ForeignKey(nameof(Co2MxId))]
        [InverseProperty(nameof(MudLogCo2Mx.MudLogChromatograph))]
        public virtual MudLogCo2Mx Co2Mx { get; set; }
        [ForeignKey(nameof(EpentAvId))]
        [InverseProperty(nameof(MudLogEpentAv.MudLogChromatograph))]
        public virtual MudLogEpentAv EpentAv { get; set; }
        [ForeignKey(nameof(EpentMnId))]
        [InverseProperty(nameof(MudLogEpentMn.MudLogChromatograph))]
        public virtual MudLogEpentMn EpentMn { get; set; }
        [ForeignKey(nameof(EpentMxId))]
        [InverseProperty(nameof(MudLogEpentMx.MudLogChromatograph))]
        public virtual MudLogEpentMx EpentMx { get; set; }
        [ForeignKey(nameof(EthAvId))]
        [InverseProperty(nameof(MudLogEthAv.MudLogChromatograph))]
        public virtual MudLogEthAv EthAv { get; set; }
        [ForeignKey(nameof(EthMnId))]
        [InverseProperty(nameof(MudLogEthMn.MudLogChromatograph))]
        public virtual MudLogEthMn EthMn { get; set; }
        [ForeignKey(nameof(EthMxId))]
        [InverseProperty(nameof(MudLogEthMx.MudLogChromatograph))]
        public virtual MudLogEthMx EthMx { get; set; }
        [ForeignKey(nameof(EtimChromCycleId))]
        [InverseProperty(nameof(MudLogEtimChromCycle.MudLogChromatograph))]
        public virtual MudLogEtimChromCycle EtimChromCycle { get; set; }
        [ForeignKey(nameof(H2sAvId))]
        [InverseProperty(nameof(MudLogH2sAv.MudLogChromatograph))]
        public virtual MudLogH2sAv H2sAv { get; set; }
        [ForeignKey(nameof(H2sMnId))]
        [InverseProperty(nameof(MudLogH2sMn.MudLogChromatograph))]
        public virtual MudLogH2sMn H2sMn { get; set; }
        [ForeignKey(nameof(H2sMxId))]
        [InverseProperty(nameof(MudLogH2sMx.MudLogChromatograph))]
        public virtual MudLogH2sMx H2sMx { get; set; }
        [ForeignKey(nameof(IbutAvId))]
        [InverseProperty(nameof(MudLogIbutAv.MudLogChromatograph))]
        public virtual MudLogIbutAv IbutAv { get; set; }
        [ForeignKey(nameof(IbutMnId))]
        [InverseProperty(nameof(MudLogIbutMn.MudLogChromatograph))]
        public virtual MudLogIbutMn IbutMn { get; set; }
        [ForeignKey(nameof(IbutMxId))]
        [InverseProperty(nameof(MudLogIbutMx.MudLogChromatograph))]
        public virtual MudLogIbutMx IbutMx { get; set; }
        [ForeignKey(nameof(IhexAvId))]
        [InverseProperty(nameof(MudLogIhexAv.MudLogChromatograph))]
        public virtual MudLogIhexAv IhexAv { get; set; }
        [ForeignKey(nameof(IhexMnId))]
        [InverseProperty(nameof(MudLogIhexMn.MudLogChromatograph))]
        public virtual MudLogIhexMn IhexMn { get; set; }
        [ForeignKey(nameof(IhexMxId))]
        [InverseProperty(nameof(MudLogIhexMx.MudLogChromatograph))]
        public virtual MudLogIhexMx IhexMx { get; set; }
        [ForeignKey(nameof(IpentAvId))]
        [InverseProperty(nameof(MudLogIpentAv.MudLogChromatograph))]
        public virtual MudLogIpentAv IpentAv { get; set; }
        [ForeignKey(nameof(IpentMnId))]
        [InverseProperty(nameof(MudLogIpentMn.MudLogChromatograph))]
        public virtual MudLogIpentMn IpentMn { get; set; }
        [ForeignKey(nameof(IpentMxId))]
        [InverseProperty(nameof(MudLogIpentMx.MudLogChromatograph))]
        public virtual MudLogIpentMx IpentMx { get; set; }
        [ForeignKey(nameof(MdBottomId))]
        [InverseProperty(nameof(MudLogMdBottom.MudLogChromatograph))]
        public virtual MudLogMdBottom MdBottom { get; set; }
        [ForeignKey(nameof(MdTopId))]
        [InverseProperty(nameof(MudLogMdTop.MudLogChromatograph))]
        public virtual MudLogMdTop MdTop { get; set; }
        [ForeignKey(nameof(MethAvId))]
        [InverseProperty(nameof(MudLogMethAv.MudLogChromatograph))]
        public virtual MudLogMethAv MethAv { get; set; }
        [ForeignKey(nameof(MethMnId))]
        [InverseProperty(nameof(MudLogMethMn.MudLogChromatograph))]
        public virtual MudLogMethMn MethMn { get; set; }
        [ForeignKey(nameof(MethMxId))]
        [InverseProperty(nameof(MudLogMethMx.MudLogChromatograph))]
        public virtual MudLogMethMx MethMx { get; set; }
        [ForeignKey(nameof(NbutAvId))]
        [InverseProperty(nameof(MudLogNbutAv.MudLogChromatograph))]
        public virtual MudLogNbutAv NbutAv { get; set; }
        [ForeignKey(nameof(NbutMnId))]
        [InverseProperty(nameof(MudLogNbutMn.MudLogChromatograph))]
        public virtual MudLogNbutMn NbutMn { get; set; }
        [ForeignKey(nameof(NbutMxId))]
        [InverseProperty(nameof(MudLogNbutMx.MudLogChromatograph))]
        public virtual MudLogNbutMx NbutMx { get; set; }
        [ForeignKey(nameof(NhexAvId))]
        [InverseProperty(nameof(MudLogNhexAv.MudLogChromatograph))]
        public virtual MudLogNhexAv NhexAv { get; set; }
        [ForeignKey(nameof(NhexMnId))]
        [InverseProperty(nameof(MudLogNhexMn.MudLogChromatograph))]
        public virtual MudLogNhexMn NhexMn { get; set; }
        [ForeignKey(nameof(NhexMxId))]
        [InverseProperty(nameof(MudLogNhexMx.MudLogChromatograph))]
        public virtual MudLogNhexMx NhexMx { get; set; }
        [ForeignKey(nameof(NpentAvId))]
        [InverseProperty(nameof(MudLogNpentAv.MudLogChromatograph))]
        public virtual MudLogNpentAv NpentAv { get; set; }
        [ForeignKey(nameof(NpentMnId))]
        [InverseProperty(nameof(MudLogNpentMn.MudLogChromatograph))]
        public virtual MudLogNpentMn NpentMn { get; set; }
        [ForeignKey(nameof(NpentMxId))]
        [InverseProperty(nameof(MudLogNpentMx.MudLogChromatograph))]
        public virtual MudLogNpentMx NpentMx { get; set; }
        [ForeignKey(nameof(PropAvId))]
        [InverseProperty(nameof(MudLogPropAv.MudLogChromatograph))]
        public virtual MudLogPropAv PropAv { get; set; }
        [ForeignKey(nameof(PropMnId))]
        [InverseProperty(nameof(MudLogPropMn.MudLogChromatograph))]
        public virtual MudLogPropMn PropMn { get; set; }
        [ForeignKey(nameof(PropMxId))]
        [InverseProperty(nameof(MudLogPropMx.MudLogChromatograph))]
        public virtual MudLogPropMx PropMx { get; set; }
        [ForeignKey(nameof(WtMudInId))]
        [InverseProperty(nameof(MudLogWtMudIn.MudLogChromatograph))]
        public virtual MudLogWtMudIn WtMudIn { get; set; }
        [ForeignKey(nameof(WtMudOutId))]
        [InverseProperty(nameof(MudLogWtMudOut.MudLogChromatograph))]
        public virtual MudLogWtMudOut WtMudOut { get; set; }
        [InverseProperty("Chromatograph")]
        public virtual ICollection<MudLogGeologyInterval> MudLogGeologyInterval { get; set; }
    }
}
