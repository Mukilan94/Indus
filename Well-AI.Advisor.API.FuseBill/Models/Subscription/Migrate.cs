using System;
using System.Collections.Generic;
using System.Text;

namespace Well_AI.Advisor.API.FuseBill.Models.Subscription
{
   

    public class DestinationPlan
    {
        public string code { get; set; }
        public string name { get; set; }
        public object reference { get; set; }
        public object description { get; set; }
        public object longdescription { get; set; }
        public string status { get; set; }
        public DateTime modificationTimestamp { get; set; }
        public IList<PlanFrequency> planFrequencies { get; set; }
        public bool autoApplyChanges { get; set; }
        public int id { get; set; }
        public string uri { get; set; }
    }

    public class DestinationPlanFrequency
    {
        public int numberOfIntervals { get; set; }
        public string interval { get; set; }
    }

    public class PlanFamilyRelationship
    {
        public int planFamilyId { get; set; }
        public string relationshipMigrationType { get; set; }
        public string sourceLabel { get; set; }
        public int sourcePlanId { get; set; }
        public int sourcePlanFrequencyId { get; set; }
        public object sourcePlan { get; set; }
        public object sourcePlanFrequency { get; set; }
        public string destinationLabel { get; set; }
        public int destinationPlanId { get; set; }
        public int destinationPlanFrequencyId { get; set; }
        public DestinationPlan destinationPlan { get; set; }
        public DestinationPlanFrequency destinationPlanFrequency { get; set; }
        public string earningOption { get; set; }
        public string nameOverrideOption { get; set; }
        public string descriptionOverrideOption { get; set; }
        public string referenceOption { get; set; }
        public string expiryOption { get; set; }
        public string contractStartOption { get; set; }
        public string contractEndOption { get; set; }
        public bool availableOnSsp { get; set; }
        public int id { get; set; }
        public string uri { get; set; }
    }

    public class PlanFamily
    {
        public string code { get; set; }
        public string name { get; set; }
        public object description { get; set; }
        public string earningOption { get; set; }
        public string nameOverrideOption { get; set; }
        public string descriptionOverrideOption { get; set; }
        public string referenceOption { get; set; }
        public string expiryOption { get; set; }
        public string contractStartOption { get; set; }
        public string contractEndOption { get; set; }
        public int id { get; set; }
        public string uri { get; set; }
    }

    public class Migrate
    {
        public int customerId { get; set; }
        public int planFamilyId { get; set; }
        public int planFamilyRelationshipId { get; set; }
        public int planFrequencyId { get; set; }
        public string earningOption { get; set; }
        public string nameOverrideOption { get; set; }
        public string descriptionOverrideOption { get; set; }
        public string referenceOption { get; set; }
        public string expiryOption { get; set; }
        public string contractStartOption { get; set; }
        public string contractEndOption { get; set; }
        public PlanFamilyRelationship planFamilyRelationship { get; set; }
        public PlanFamily planFamily { get; set; }
        public string migrationTimingOption { get; set; }
        public DateTime scheduledMigrationDate { get; set; }
        public int id { get; set; }
        public string uri { get; set; }
    }

}
