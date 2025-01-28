using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TrajectoryMagTran2Raws
    {
        public TrajectoryMagTran2Raws()
        {
            TrajectoryRawDatas = new HashSet<TrajectoryRawDatas>();
        }

        [Key]
        public int MagTran2RawId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MagTran2Raw")]
        public virtual ICollection<TrajectoryRawDatas> TrajectoryRawDatas { get; set; }
    }
}
