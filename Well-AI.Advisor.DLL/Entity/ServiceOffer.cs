﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("ServiceOffer")]
    public class ServiceOffer
    {
        [Key]
        [StringLength(40)]
        public string Id {get;set;}

        [StringLength(254)]
        public string Name { get; set; }

        [StringLength(1024)]
        public string Description { get; set; }
    }
}
