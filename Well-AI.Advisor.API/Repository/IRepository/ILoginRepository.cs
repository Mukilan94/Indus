using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellAI.Advisor.API.Models;
namespace Well_AI.Advisor.API.Repository.IRepository
{
    public interface ILoginRepository
    {
        public Task<LoginReponse> Login(LoginRequest request);
    }
}
