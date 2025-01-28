using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Repository.IRepository
{
    public interface ILogRepository
    {
        ICollection<Log> GetLogDetails();
        Log GetLogDetail(string uid);
        bool LogExists(string Uid);
        bool CreateLog(Log log);
        bool UpdateLog(Log log);
        bool DeleteLog(Log log);
        bool Save();
    }
}
