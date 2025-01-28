using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class WellCommonDatas
    {
        public WellCommonDatas()
        {
            Wells = new HashSet<Wells>();
        }

        [Key]
        public int CommonDataId { get; set; }
        [Column("DTimCreation")]
        public string DtimCreation { get; set; }
        [Column("DTimLastChange")]
        public string DtimLastChange { get; set; }
        public string ItemState { get; set; }
        public string Comments { get; set; }
        public int? DefaultDatumId { get; set; }

        [ForeignKey(nameof(DefaultDatumId))]
        [InverseProperty(nameof(WellDefaultDatums.WellCommonDatas))]
        public virtual WellDefaultDatums DefaultDatum { get; set; }
        [InverseProperty("CommonData")]
        public virtual ICollection<Wells> Wells { get; set; }
    }
}
