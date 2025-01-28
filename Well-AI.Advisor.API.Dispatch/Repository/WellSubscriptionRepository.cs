using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Dispatch.Repository.IRepository;
using WellAI.Advisor.DLL;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Repository;
using Well_AI.Advisor.API.Dispatch.Data;
using WellAI.Advisor.API.Dispatch.Helpers;
using Well_AI.Advisor.API.Dispatch.Models;

namespace Well_AI.Advisor.API.Dispatch.Repository
{
    public class WellSubscriptionRepository : IWellSubscriptionRepository
    {

        private readonly WellAIAdvisiorContext _db;
        private readonly AppSettings _appsettings;
        private readonly WebAIAdvisorContext _dbMaster;


        public WellSubscriptionRepository(WellAIAdvisiorContext context, WebAIAdvisorContext masterContext, IOptions<AppSettings> appsettings)
        {
            _db = context;
            _appsettings = appsettings.Value;
            _dbMaster = masterContext;
        }

        public WellSubscription Authenticate(string custId, string workStationId, string apiAccesskey, string workstationToken)
        {
            bool isvalidApiAccessKey = _db.Configuration.Where(x => x.Value == apiAccesskey).Any();
            if (isvalidApiAccessKey)
            {
                // Need to add another flag to check if account is active
                bool isvalidCustomerid = _dbMaster.TenantUsers.Where(x => x.TenantId == custId).Any();

                if (isvalidCustomerid)
                {
                    bool isValidWorkstationDetails = _db.WorkstationRegister.Where(x => x.WorkstationIdentifier == workStationId && x.WorkstationToken == workstationToken).Any();

                    var subscription = _db.WorkstationRegister.SingleOrDefault(x => x.CustomerAccountIdentifier == custId && x.WorkstationIdentifier == workStationId);
                    if (subscription == null)
                    {
                        return null;
                    }
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_appsettings.SecretKey);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, subscription.CustomerAccountIdentifier)
                }),
                        Expires = DateTime.UtcNow.AddMinutes(5),
                        SigningCredentials = new SigningCredentials
                                            (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    subscription.AuthorizationToken = tokenHandler.WriteToken(token);

                    WellAIApiApContext.Current.Session.Set(Constants.TenantIdKey, Encoding.UTF8.GetBytes(custId));
                    return subscription;
                }


            }
            return null;
        }

        public Dictionary<string, string> GenerateToken()
        {
            Dictionary<string, string> Token = new Dictionary<string, string>();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appsettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, "ServiceToken")
                }),
                Expires = DateTime.UtcNow.AddSeconds(120),
                SigningCredentials = new SigningCredentials
                                    (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            Token.Add("ApiToken", tokenHandler.WriteToken(token));

            return Token;
        }

        public bool IsUniqueWorkStation(string workStationId)
        {
            var customer = _db.WorkstationRegister.SingleOrDefault(x => x.WorkstationIdentifier == workStationId && x.IsActive == true);
            if (customer == null)
                return true;
            return false;
        }

        public WellSubscription Register(string custId, string workStationId)
        {

            string secreatKey = SecretKeyUtils.GenerateKey();

            WellSubscription subscription = new WellSubscription()
            {
                CustomerAccountIdentifier = custId,
                WorkstationIdentifier = workStationId,
                WorkstationToken = secreatKey,
                IsActive = true
            };
            _db.WorkstationRegister.Add(subscription);
            _db.SaveChanges();
            subscription.AuthorizationToken = "";
            return subscription;
        }
    }
}
