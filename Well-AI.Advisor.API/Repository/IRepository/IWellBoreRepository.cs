using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Repository.IRepository
{
    public interface IWellBoreRepository
    {
        ICollection<WellBore> GetWellBoreDetails();
        WellBore GetWellBoreDetail(string uid);
        bool WellBoreExists(string Uid);
        bool CreateWellBore(WellBore wellBore);
        bool UpdateWellBore(WellBore wellBore);
        bool DeleteWellBore(WellBore wellBore);
        bool Save();
    }
}
