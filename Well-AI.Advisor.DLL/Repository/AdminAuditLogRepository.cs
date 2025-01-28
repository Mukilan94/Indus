using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.DLL.IRepository;

namespace WellAI.Advisor.DLL.Repository
{
    public class AdminAuditLogRepository : IAdminAuditLogRepository
    {

        private readonly WebAIAdvisorContext _dbContext;

        public AdminAuditLogRepository(WebAIAdvisorContext db)
        {
            _dbContext = db;
        }

        public bool WriteAuditLog(Audit auditLog)
        {
            var ActionName = auditLog.Activity.Split(':', System.StringSplitOptions.RemoveEmptyEntries).Last();

            if (ActionName != "TimeFunction" && ActionName != "RefreshUserSession")
            {
                try
                {
                    _dbContext.AuditLogs.Add(auditLog);
                    _dbContext.SaveChanges();
                }
                catch (Exception)
                {

                }
                
            }
            return true;
        }
    }
}
