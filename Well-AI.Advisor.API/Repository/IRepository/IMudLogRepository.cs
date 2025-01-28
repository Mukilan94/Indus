using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Repository.IRepository
{
    public interface IMudLogRepository
    {
        ICollection<MudLog> GetMudLogDetails();
        MudLog GetMudLogDetail(string uid);
        bool MudLogExists(string Uid);
        bool CreateMudLog(MudLog mudLog);
        bool UpdateMudLog(MudLog mudLog);
        bool DeleteMudLog(MudLog mudLog);
        bool Save();
    }
}
