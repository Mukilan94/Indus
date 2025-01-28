using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class ConvCoreQualifiers
    {
        [Key]
        public int QualifierId { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Uid { get; set; }
        public int? ConvCoreLithologyLithologyId { get; set; }

        [ForeignKey(nameof(ConvCoreLithologyLithologyId))]
        [InverseProperty(nameof(ConvCoreLithologys.ConvCoreQualifiers))]
        public virtual ConvCoreLithologys ConvCoreLithologyLithology { get; set; }
    }
}
