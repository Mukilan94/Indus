using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Repository.IRepository
{
   public interface IFluidsReportCoreRepository
    {
        ICollection<FluidsReport> GetFluidsReportDetails();
        FluidsReport GetFluidsReportDetail(string uid);
        bool FluidsReportExists(string Uid);
        bool CreateFluidsReport(FluidsReport fluidsReport);
        bool UpdateFluidsReport(FluidsReport fluidsReport);
        bool DeleteFluidsReport(FluidsReport fluidsReport);
        bool Save();
    }
}
