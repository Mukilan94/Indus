using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Models;

namespace Well_AI.Advisor.API.Repository.IRepository
{
   public interface IOpsReportRepository
    {
        ICollection<OpsReport> GetOpsReportDetails();
        OpsReport GetOpsReportDetail(string uid);
        bool OpsReportExists(string Uid);
        bool CreateOpsReport(OpsReport opsReport);
        bool UpdateOpsReport(OpsReport opsReport);
        bool DeleteOpsReport(OpsReport opsReport);
        bool Save();
    }
}
