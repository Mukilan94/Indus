namespace WellAI.Advisor.DLL.Repository
{
    using System.Linq;
    using WellAI.Advisor.DLL.Data;
    using WellAI.Advisor.DLL.Entity;
    using WellAI.Advisor.DLL.IRepository;

    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly WebAIAdvisorContext _dbContext;

        public AuditLogRepository(WebAIAdvisorContext db)
        {
            _dbContext = db;
        }

        public bool WriteAuditLog(Audit auditLog)
        {
            var ActionName = auditLog.Activity.Split(':', System.StringSplitOptions.RemoveEmptyEntries).Last();

            if (ActionName != "TimeFunction" && ActionName != "WellData_Create")
            {
                _dbContext.AuditLogs.Add(auditLog);
                _dbContext.SaveChanges();
            }
            return true;
        }
    }
}
