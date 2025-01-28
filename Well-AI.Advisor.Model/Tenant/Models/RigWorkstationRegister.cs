using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.Tenant.Models
{
    public partial class RigWorkstationRegister
    {
        [Key]
        public int RegisterationId { get; set; }
        public string CustomerAccountIdentifier { get; set; }
        public string WorkstationIdentifier { get; set; }
        public string WorkstationToken { get; set; }
    }
}
