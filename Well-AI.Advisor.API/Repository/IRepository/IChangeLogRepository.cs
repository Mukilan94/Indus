using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Repository.IRepository
{
   public interface IChangeLogRepository
    {
        ICollection<ChangeLog> GetChangeLogDetails();
        ChangeLog GetChangeLogDetail(string uid);
        bool ChangeLogExists(string Uid);
        bool CreateChangeLog(ChangeLog changeLog);
        bool UpdateChangeLog(ChangeLog changeLog);
        bool DeleteChangeLog(ChangeLog changeLog);
        bool Save();

    }
}
