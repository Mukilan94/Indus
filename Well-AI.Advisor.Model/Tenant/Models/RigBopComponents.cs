using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RigBopComponents
    {
        [Key]
        public int BopComponentId { get; set; }
        public string TypeBopComp { get; set; }
        public string DescComp { get; set; }
        public int? IdPassThruId { get; set; }
        public int? PresWorkId { get; set; }
        public int? DiaCloseMnId { get; set; }
        public int? DiaCloseMxId { get; set; }
        public string Nomenclature { get; set; }
        public string IsVariable { get; set; }
        public string Uid { get; set; }
        public int? RigBopBopId { get; set; }

        [ForeignKey(nameof(DiaCloseMnId))]
        [InverseProperty(nameof(RigDiaCloseMns.RigBopComponents))]
        public virtual RigDiaCloseMns DiaCloseMn { get; set; }
        [ForeignKey(nameof(DiaCloseMxId))]
        [InverseProperty(nameof(RigDiaCloseMxs.RigBopComponents))]
        public virtual RigDiaCloseMxs DiaCloseMx { get; set; }
        [ForeignKey(nameof(IdPassThruId))]
        [InverseProperty(nameof(RigIdPassThrus.RigBopComponents))]
        public virtual RigIdPassThrus IdPassThru { get; set; }
        [ForeignKey(nameof(PresWorkId))]
        [InverseProperty(nameof(RigPresWorks.RigBopComponents))]
        public virtual RigPresWorks PresWork { get; set; }
        [ForeignKey(nameof(RigBopBopId))]
        [InverseProperty(nameof(RigBops.RigBopComponents))]
        public virtual RigBops RigBopBop { get; set; }
    }
}
