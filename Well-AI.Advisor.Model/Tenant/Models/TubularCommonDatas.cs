using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class TubularCommonDatas
    {
        public TubularCommonDatas()
        {
            Tubulars = new HashSet<Tubulars>();
        }

        [Key]
        public int TubularyCommonDataId { get; set; }
        public string ItemState { get; set; }
        public string Comments { get; set; }

        [InverseProperty("CommonDataTubularyCommonData")]
        public virtual ICollection<Tubulars> Tubulars { get; set; }
    }
}
