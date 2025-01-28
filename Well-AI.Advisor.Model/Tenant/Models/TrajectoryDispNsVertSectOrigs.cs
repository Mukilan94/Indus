using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TrajectoryDispNsVertSectOrigs
    {
        public TrajectoryDispNsVertSectOrigs()
        {
            Trajectorys = new HashSet<Trajectorys>();
        }

        [Key]
        public int DispNsVertSectOrigId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("DispNsVertSectOrig")]
        public virtual ICollection<Trajectorys> Trajectorys { get; set; }
    }
}
