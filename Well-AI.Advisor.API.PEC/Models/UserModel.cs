using System;
using System.Collections.Generic;
using System.Text;

namespace Well_AI.Advisor.API.PEC.Models
{
    public class Activities
    {
        public IList<string> Organizations { get; set; }
        public IList<string> TaskStatuses { get; set; }
        public IList<string> Tasks { get; set; }
        public IList<string> TaskGroups { get; set; }
        public IList<string> IncidentWorkGroups { get; set; }
        public IList<string> IncidentRootCauses { get; set; }
        public IList<string> IncidentTypes { get; set; }
        public IList<string> IncidentCategories { get; set; }
        public IList<string> IncidentReports { get; set; }
        public IList<string> IncidentRegions { get; set; }
        public IList<string> Messages { get; set; }
        public IList<string> OqProviderMappingValues { get; set; }
        public IList<string> Employees { get; set; }
        public IList<string> OrganizationAssets { get; set; }
        public IList<string> ThirdPartyCredentials { get; set; }
        public IList<string> ESignatureTemplates { get; set; }
        public IList<string> ESignatureEnvelopes { get; set; }
        public IList<string> OperationalMetricValues { get; set; }
        public IList<string> OperationalMetrics { get; set; }
        public IList<string> AssignedCoveredTasks { get; set; }
        public IList<string> AssignedContractors { get; set; }
        public IList<string> AssignedEmployees { get; set; }
        public IList<string> Projects { get; set; }
        public IList<string> Subscriptions { get; set; }
        public IList<string> Insurances { get; set; }
        public IList<string> TrainingTracker { get; set; }
        public IList<string> Rankings { get; set; }
        public IList<string> ReleasedOrganizations { get; set; }
        public IList<string> RegionMetricValues { get; set; }
        public IList<string> RegionMetrics { get; set; }
        public IList<string> Reports { get; set; }
        public IList<string> Verifications { get; set; }
        public IList<string> Releases { get; set; }
        public IList<string> QuestionnaireSections { get; set; }
        public IList<string> EmployeeDrugAndAlcoholData { get; set; }
        public IList<string> VeriforceOrganizationLinks { get; set; }
        public IList<string> TrainingReleasedContractors { get; set; }
        public IList<string> Qualifications { get; set; }
        public IList<string> ReleasedClients { get; set; }
        public IList<string> CustomerApiGateway { get; set; }
        public IList<string> UserOrganizations { get; set; }
    }

    public class Activity
    {
        public string id { get; set; }
        public string type { get; set; }
        public Activities activities { get; set; }
    }

    public class UserModel
    {
        public string userId { get; set; }
        public IList<Activity> activities { get; set; }
        public string userName { get; set; }
        public string fullName { get; set; }
        public string primaryOrganizationId { get; set; }
        public string emailAddress { get; set; }
    }
    
}
