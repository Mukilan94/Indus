using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class WbGeometryCommonData
    {
        public WbGeometryCommonData()
        {
            WbGeometrys = new HashSet<WbGeometrys>();
        }

        [Key]
        public int WbGeometryCommonDataId { get; set; }
        public string ItemState { get; set; }
        public string Comments { get; set; }

        [InverseProperty("CommonDataWbGeometryCommonData")]
        public virtual ICollection<WbGeometrys> WbGeometrys { get; set; }
    }
}
