using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TrajectoryMagTran1Raws
    {
        public TrajectoryMagTran1Raws()
        {
            TrajectoryRawDatas = new HashSet<TrajectoryRawDatas>();
        }

        [Key]
        public int MagTran1RawId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("MagTran1Raw")]
        public virtual ICollection<TrajectoryRawDatas> TrajectoryRawDatas { get; set; }
    }
}
