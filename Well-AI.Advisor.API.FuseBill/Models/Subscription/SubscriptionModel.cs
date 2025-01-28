using System;
using System.Collections.Generic;
using System.Text;

namespace Well_AI.Advisor.API.FuseBill.Models.Subscription
{

    public class SetupFee
    {
        public double amount { get; set; }
        public string currency { get; set; }
        public int id { get; set; }
        public object uri { get; set; }
    }

    public class Charge
    {
        public double amount { get; set; }
        public string currency { get; set; }
        public int id { get; set; }
        public object uri { get; set; }
    }

   
    public class Migrations
    {
        public object sourceSubscriptionId { get; set; }
        public object sourceRelationshipMigrationType { get; set; }
        public object sourceRelationshipId { get; set; }
        public int destinationSubscriptionId { get; set; }
        public string destinationRelationshipMigrationType { get; set; }
        public int destinationRelationshipId { get; set; }
    }


    public class SubscriptionOverride
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
   
}
