using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WellAI.Advisor.Model.SubcriptionPackage
{
    public class SubcriptionPackageModel
    {
        [ScaffoldColumn(false)]
        public string SubscriptionId { get; set; }
        [Required]
        public string SubscriptionName { get; set; }
        //[Required]
        public string SubscriptionType { get; set; }
        [Required]
        public string PricePerMonth { get; set; }

        //  public string PricePerYear { get; set; }

        public string Description { get; set; }

        public string Features { get; set; }

        //     public string SubscriptionStatus { get; set; }
        //public string CreatedDate { get; set; }
        //public string ModifiedDate { get; set; }

        //     public DateTime CreatedDate { get; set; }
        //      public DateTime ModifiedDate { get; set; }


    }
}
