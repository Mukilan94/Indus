using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Well_AI.Advisor.API.PEC.Models.Dtos;
using Well_AI.Advisor.API.PEC.Services.IServices;

namespace Well_AI.Advisor.API.PEC.Services
{
    public class EmployeeQualificationService : IEmployeeQualification
    {
        public Task<List<EmployeeQualificationDto>> GetEmployeeQualificationListAsync(string organizationId, string qualificationId)
        {
            throw new NotImplementedException();
        }

        public Task<List<EmployeeQualificationDto>> GetEmployeeQualificationListAsync(string employeeId, string select, string orderBy, string filter, string search, int page = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }
    }
}
