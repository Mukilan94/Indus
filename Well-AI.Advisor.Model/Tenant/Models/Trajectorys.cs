using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class Trajectorys
    {
        [Key]
        public int TrajectoryId { get; set; }
        public string NameWell { get; set; }
        public string NameWellbore { get; set; }
        public string Name { get; set; }
        [Column("DTimTrajStart")]
        public string DtimTrajStart { get; set; }
        [Column("DTimTrajEnd")]
        public string DtimTrajEnd { get; set; }
        public int? MdMnId { get; set; }
        public int? MdMxId { get; set; }
        public string ServiceCompany { get; set; }
        public int? MagDeclUsedId { get; set; }
        public int? GridCorUsedId { get; set; }
        public int? AziVertSectId { get; set; }
        public int? DispNsVertSectOrigId { get; set; }
        public int? DispEwVertSectOrigId { get; set; }
        public string Definitive { get; set; }
        public string Memory { get; set; }
        public string FinalTraj { get; set; }
        public string AziRef { get; set; }
        public int? TrajectoryStationId { get; set; }
        public int? CommonDataTrajectoryCommonDataId { get; set; }
        public string Uid { get; set; }
        public string UidWellbore { get; set; }
        public string UidWell { get; set; }

        [ForeignKey(nameof(AziVertSectId))]
        [InverseProperty(nameof(TrajectoryAziVertSects.Trajectorys))]
        public virtual TrajectoryAziVertSects AziVertSect { get; set; }
        [ForeignKey(nameof(CommonDataTrajectoryCommonDataId))]
        [InverseProperty(nameof(TrajectoryCommonDatas.Trajectorys))]
        public virtual TrajectoryCommonDatas CommonDataTrajectoryCommonData { get; set; }
        [ForeignKey(nameof(DispEwVertSectOrigId))]
        [InverseProperty(nameof(TrajectoryDispEwVertSectOrigs.Trajectorys))]
        public virtual TrajectoryDispEwVertSectOrigs DispEwVertSectOrig { get; set; }
        [ForeignKey(nameof(DispNsVertSectOrigId))]
        [InverseProperty(nameof(TrajectoryDispNsVertSectOrigs.Trajectorys))]
        public virtual TrajectoryDispNsVertSectOrigs DispNsVertSectOrig { get; set; }
        [ForeignKey(nameof(GridCorUsedId))]
        [InverseProperty(nameof(TrajectoryGridCorUseds.Trajectorys))]
        public virtual TrajectoryGridCorUseds GridCorUsed { get; set; }
        [ForeignKey(nameof(MagDeclUsedId))]
        [InverseProperty(nameof(TrajectoryMagDeclUseds.Trajectorys))]
        public virtual TrajectoryMagDeclUseds MagDeclUsed { get; set; }
        [ForeignKey(nameof(MdMnId))]
        [InverseProperty(nameof(TrajectoryMdMns.Trajectorys))]
        public virtual TrajectoryMdMns MdMn { get; set; }
        [ForeignKey(nameof(MdMxId))]
        [InverseProperty(nameof(TrajectoryMdMxs.Trajectorys))]
        public virtual TrajectoryMdMxs MdMx { get; set; }
        [ForeignKey(nameof(TrajectoryStationId))]
        [InverseProperty(nameof(TrajectoryStations.Trajectorys))]
        public virtual TrajectoryStations TrajectoryStation { get; set; }
    }
}
