using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Repository.IRepository
{
    public interface IDrillReportsRepository
    {
        ICollection<DrillReport> GetDrillReportDetails();
        DrillReport GetDrillReportDetail(string uid);
        bool DrillReportExists(string Uid);
        bool CreateDrillReport(DrillReport convCore);
        bool UpdateDrillReport(DrillReport convCore);
        bool DeleteDrillReport(DrillReport convCore);
        bool Save();
    }
}
