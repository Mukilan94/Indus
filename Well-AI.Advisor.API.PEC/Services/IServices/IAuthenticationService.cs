using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Well_AI.Advisor.API.PEC.Models;

namespace Well_AI.Advisor.API.PEC.Services.IServices
{
    public interface IAuthenticationService
    {
        Task<AuthonticationResponse> AuthenticateAsync(string tenantId);
    }
}
