using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Repository.IRepository
{
   public interface IRiskRepository
    {
        ICollection<Risk> GetRiskDetails();
        Risk GetRiskDetail(string uid);
        bool RiskExists(string Uid);
        bool CreateRisk(Risk risk);
        bool UpdateRisk(Risk risk);
        bool DeleteRisk(Risk risk);
        bool Save();
    }
}
