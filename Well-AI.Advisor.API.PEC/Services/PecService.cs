using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Well_AI.Advisor.API.PEC.Models;
using Well_AI.Advisor.API.PEC.Models.Dtos;
using Well_AI.Advisor.API.PEC.Services.IServices;
using Well_AI.Advisor.Log.Error;

namespace Well_AI.Advisor.API.PEC.Services
{
    public class PecService : IPecService
    {
        private readonly WellAI.Advisor.DLL.Data.WebAIAdvisorContext _wdb;
        private readonly HttpClient _client;
        private readonly IAuthenticationService _authService;
        public PecService(HttpClient client, IAuthenticationService authService, WellAI.Advisor.DLL.Data.WebAIAdvisorContext wdb)
        {
            _client = client;
            _authService = authService;
            _wdb = wdb;
        }
        public Task<CoveredTaskDto> GetCoveredTaskAsync(string organizationId, string coveredTaskId)
        {
            throw new NotImplementedException();
        }

        public Task<List<CoveredTaskDto>> GetCoveredTaskListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<CoveredTaskDto>> GetOrganizationCoveredTaskListAsync(string organizationId)
        {
            throw new NotImplementedException();
        }

        public async Task<OrganizationModel> GetOrganizationDetailsAsyc(string organizationId,string tenantId)
        {
            OrganizationModel _res = new OrganizationModel();
            try
            {
                AuthonticationResponse authonticationData = await _authService.AuthenticateAsync(tenantId);
                if (authonticationData.access_token != null)
                {

                    _client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", authonticationData.access_token);
                    var httpResponse = await _client.GetAsync("https://pecdata.com/api/v2/Organizations/" + organizationId);

                    if (!httpResponse.IsSuccessStatusCode)
                    {
                        throw new Exception("Cannot retrieve Data");
                    }

                    var content = await httpResponse.Content.ReadAsStringAsync();
                     _res = JsonConvert.DeserializeObject<OrganizationModel>(content);

                    return _res;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(null, null, _wdb, Guid.Empty, Guid.Empty);
                customErrorHandler.WriteError(ex, "AuthenticationRepository GetAuthonticationUserDetail", null);
                return null;
            }
        }

        public async Task<OrganizationRankingModel> GetOrganizationRankingAsyc(string organizationId, string tenantId)
        {
            OrganizationRankingModel _res = new OrganizationRankingModel();
            try
            {
                AuthonticationResponse authonticationData = await _authService.AuthenticateAsync(tenantId);
                if (authonticationData.access_token != null)
                {

                    _client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", authonticationData.access_token);
                    var httpResponse = await _client.GetAsync("https://pecdata.com/api/v2/Organizations/" + organizationId+ "/Rankings");

                    if (!httpResponse.IsSuccessStatusCode)
                    {
                        throw new Exception("Cannot retrieve Data");
                    }

                    var content = await httpResponse.Content.ReadAsStringAsync();
                    _res = JsonConvert.DeserializeObject<OrganizationRankingModel>(content);

                    return _res;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(null, null, _wdb, Guid.Empty, Guid.Empty);
                customErrorHandler.WriteError(ex, "PecService GetOrganizationRankingAsyc", null);
                return null;
            }
        }

        public async Task<UserModel> GetUserInfoAsyc(string tenantId)
        {
            UserModel userDetails = new UserModel();
            try
            {
                AuthonticationResponse authonticationData = await _authService.AuthenticateAsync( tenantId);
                if (authonticationData.access_token != null)
                {
                    _client.DefaultRequestHeaders.Authorization =
                   new AuthenticationHeaderValue("Bearer", authonticationData.access_token);
                    var httpResponse = await _client.GetAsync("https://pecdata.com/api/v2/UserInfo");

                    if (!httpResponse.IsSuccessStatusCode)
                    {
                        throw new Exception("Cannot retrieve Data");
                    }

                    var content = await httpResponse.Content.ReadAsStringAsync();

                    userDetails = JsonConvert.DeserializeObject<UserModel>(content);
                    return userDetails;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(null, null, _wdb, Guid.Empty, Guid.Empty);
                customErrorHandler.WriteError(ex, "PecServices GetUserInfoAsync", null);
                return null;
            }
        }
    }
}

