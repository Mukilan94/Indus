using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Repository.IRepository
{
   public interface IWellSubscriptionRepository
    {
        bool IsUniqueWorkStation(string workStationId);
        WellSubscription Authenticate(string custId, string workStationId, string apiAccesskey,string workstationToken);
        WellSubscription Register(string custId, string workStationId);

        Dictionary<string,string> GenerateToken();
    }
}
