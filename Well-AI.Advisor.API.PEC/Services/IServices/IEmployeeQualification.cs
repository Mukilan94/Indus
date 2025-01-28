using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Well_AI.Advisor.API.PEC.Models.Dtos;

namespace Well_AI.Advisor.API.PEC.Services.IServices
{
   public interface IEmployeeQualification
    {
        Task<List<EmployeeQualificationDto>> GetEmployeeQualificationListAsync(string organizationId,string qualificationId);

        Task<List<EmployeeQualificationDto>> GetEmployeeQualificationListAsync(string employeeId, string select,string orderBy,string filter,string search,int page=1, int pageSize=10);
    }
}
