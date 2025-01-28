using System;
using System.Collections.Generic;
using System.Text;

namespace Well_AI.Advisor.API.PEC.Models
{
  
    public class EmployeeQualification
    {
        public string id { get; set; }
        public DateTime qualifiedDate { get; set; }
        public string qualificationId { get; set; }
        public string employeeId { get; set; }
        public string employerOrganizationId { get; set; }
        public DateTime qualifiedUntilDate { get; set; }
        public string coveredTaskName { get; set; }
        public string providerTaskId { get; set; }
    }
}
