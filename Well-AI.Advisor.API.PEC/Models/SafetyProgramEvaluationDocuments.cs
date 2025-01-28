using System;
using System.Collections.Generic;
using System.Text;

namespace Well_AI.Advisor.API.PEC.Models
{
    public class SafetyProgramEvaluationDocuments
    {
        public string id { get; set; }
        public string organizationId { get; set; }
        public string mimeType { get; set; }
        public string fileName { get; set; }
        public int fileSize { get; set; }
        public string createdByUserId { get; set; }
        public DateTime createdDateUtc { get; set; }
    }
}
