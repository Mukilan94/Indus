using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TrajectoryDispEwVertSectOrigs
    {
        public TrajectoryDispEwVertSectOrigs()
        {
            Trajectorys = new HashSet<Trajectorys>();
        }

        [Key]
        public int DispEwVertSectOrigId { get; set; }
        public string Uom { get; set; }
        public string Text { get; set; }

        [InverseProperty("DispEwVertSectOrig")]
        public virtual ICollection<Trajectorys> Trajectorys { get; set; }
    }
}
