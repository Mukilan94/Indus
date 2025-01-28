using System;
using System.Collections.Generic;
using System.Text;

namespace Well_AI.Advisor.API.PEC.Models
{

    public class Organization
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class OverallRanking
    {
        public string organizationId { get; set; }
        public int organizationType { get; set; }
        public string label { get; set; }
        public string color { get; set; }
        public string ssqScore { get; set; }
    }

    public class ModuleRanking
    {
        public int organizationLegacyId { get; set; }
        public int organizationType { get; set; }
        public string name { get; set; }
        public string color { get; set; }
        public string value { get; set; }
    }

    public class OrganizationRankingModel
    {
        public Organization organization { get; set; }
        public OverallRanking overallRanking { get; set; }
        public IList<ModuleRanking> moduleRankings { get; set; }
    }

    
}
