using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Well_AI.Advisor.API.PEC.Models;
using Well_AI.Advisor.API.PEC.Repository;
using Well_AI.Advisor.API.PEC.Services.IServices;
using Well_AI.Advisor.Log.Error;

namespace Well_AI.Advisor.API.PEC.Services
{
    public class AuthonticateService : IAuthenticationService
    {
        private readonly WellAI.Advisor.DLL.Data.WebAIAdvisorContext _wdb;
        private readonly HttpClient _client;
        private readonly IAuthenticationRepository _authenticationRepository;
        public AuthonticateService(HttpClient client, IAuthenticationRepository authenticationRepository, WellAI.Advisor.DLL.Data.WebAIAdvisorContext wdb)
        {
            _client = client;
            _authenticationRepository = authenticationRepository;
            _wdb = wdb;
        }
        public async Task<AuthonticationResponse> AuthenticateAsync(string tenantId)
        {
            AuthonticateJsonModel authRequest = new AuthonticateJsonModel();
            AuthonticationResponse response = new AuthonticationResponse();
            try {
                TenantConfiguration Tenantconfig = _authenticationRepository.GetAuthonticationDetail(tenantId);
                userDetailConfiguration Userconfig = _authenticationRepository.GetAuthonticationUserDetail(tenantId);
                Parameter param = new Parameter();

                

                param.Bodyparameter= Tenantconfig.value;
                if (Tenantconfig!=null)
                {
                    var userDetails = "";
                var bodyParameter = JsonConvert.DeserializeObject(param.Bodyparameter);
              

                var resultJson = JsonConvert.SerializeObject(new { UserDetails = userDetails, BodyParameter = bodyParameter });
                authRequest = JsonConvert.DeserializeObject<AuthonticateJsonModel>(resultJson);

                var content = new FormUrlEncodedContent(new[]
                 {
                new KeyValuePair<string, string>("grant_type", authRequest.BodyParameter.PecGrantyType),
                new KeyValuePair<string, string>("client_id", authRequest.BodyParameter.PecClientId),
                new KeyValuePair<string, string>("client_secret", authRequest.BodyParameter.PecClientSecret),
                
                    new KeyValuePair<string, string>("username", "petromechapi" ),
                new KeyValuePair<string, string>("password", "A23$c45^"),

            });


                var result = await _client.PostAsync("https://pecdata.com/identity/connect/token", content);

                if (result.IsSuccessStatusCode)
                {
                    var responseContent = await result.Content.ReadAsStringAsync();
                    response = JsonConvert.DeserializeObject<AuthonticationResponse>(responseContent);

                }
                else
                {
                    response.ErrorMessage = "Something went wrong.Kindly check the Requested parameters!";

                }
                
            }
            
            else
            {
                response.ErrorMessage = "Invalid TenantId";
            }
            return response;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(null, null, _wdb, Guid.Empty, Guid.Empty);
                customErrorHandler.WriteError(ex, "AuthenticationRepository AuthenticationAsync", null);
                response.ErrorMessage = "Something went wrong.Kindly check the Requested parameters!";
                return response;
            }
            }
    }
}

