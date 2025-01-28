using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Model.ServiceCompany.Models
{
    [Table("SubscriptionOperatorRigs")]
    public class SubscriptionOperatorRig
    {
        [Key]
        public string ID { get; set; }
        public string CompanyId { get; set; }
        public string RigId { get; set; }
    }
}
