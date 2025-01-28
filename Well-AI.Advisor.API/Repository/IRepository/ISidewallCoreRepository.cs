using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Repository.IRepository
{
   public interface ISidewallCoreRepository
    {
        ICollection<SidewallCore> GetSidewallCoreDetails();
        SidewallCore GetSidewallCoreDetail(string uid);
        bool SidewallCoreExists(string Uid);
        bool CreateSidewallCore(SidewallCore sidewallCore);
        bool UpdateSidewallCore(SidewallCore sidewallCore);
        bool DeleteSidewallCore(SidewallCore sidewallCore);
        bool Save();
    }
}
