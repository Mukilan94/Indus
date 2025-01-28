using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Well_AI.Advisor.API.PEC.Models;
using Well_AI.Advisor.API.PEC.Models.Dtos;

namespace Well_AI.Advisor.API.PEC.Services.IServices
{
    public interface IPecService
    {
        Task<List<CoveredTaskDto>> GetCoveredTaskListAsync();

        Task<List<CoveredTaskDto>> GetOrganizationCoveredTaskListAsync(string organizationId);

        Task<CoveredTaskDto> GetCoveredTaskAsync(string organizationId,string coveredTaskId);

        Task<UserModel> GetUserInfoAsyc(string tenantId);

        Task<OrganizationModel> GetOrganizationDetailsAsyc(string organizationId, string tenantId);
        Task<OrganizationRankingModel> GetOrganizationRankingAsyc(string organizationId, string tenantId);


    }
}
