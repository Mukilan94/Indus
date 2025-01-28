using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Repository.IRepository
{
   public interface ICementJobRepository
    {
        ICollection<CementJob> GetCementJobDetails();
        CementJob GetCementJobDetail(string uid);
        bool CementJobExists(string Uid);
        bool CreateCementJob(CementJob cementJob);
        bool UpdateCementJob(CementJob cementJob);
        bool DeleteCementJob(CementJob cementJob);
        bool Save();

    }
}
