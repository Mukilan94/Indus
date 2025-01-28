using System;
using System.Collections.Generic;
using System.Text;

namespace Well_AI.Advisor.API.PEC.Models.Dtos
{
   public class CoveredTaskDto
    {
        public string id { get; set; }
        public string createdDate { get; set; }
        public bool isDeleted { get; set; }
        public string name { get; set; }
        public string providerTaskId { get; set; }
        public string updatedDate { get; set; }
        public string organizationId { get; set; }
        public bool isProvidedByOrganization { get; set; }
        public Int32 assignedContractorsCount { get; set; }
        public Int32 assignedEmployeesCount { get; set; }
        public string description { get; set; }
        public string providerId { get; set; }
        public string providedBy { get; set; }
    }
}
