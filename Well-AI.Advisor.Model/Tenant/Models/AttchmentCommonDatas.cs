using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class AttchmentCommonDatas
    {
        public AttchmentCommonDatas()
        {
            Attachments = new HashSet<Attachments>();
        }

        [Key]
        public int CommonDataId { get; set; }
        [Column("dTimCreation")]
        public DateTime DTimCreation { get; set; }
        [Column("dTimLastChange")]
        public DateTime DTimLastChange { get; set; }
        [Column("itemState")]
        public string ItemState { get; set; }

        [InverseProperty("CommonData")]
        public virtual ICollection<Attachments> Attachments { get; set; }
    }
}
