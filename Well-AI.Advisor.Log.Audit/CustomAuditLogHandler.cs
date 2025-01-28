namespace Well_AI.Advisor.Log.Audit
{
    using System;
    using WellAI.Advisor.DLL.Data;
    using WellAI.Advisor.DLL.Entity;
    using WellAI.Advisor.DLL.IRepository;
    using WellAI.Advisor.DLL.Repository;

    public class CustomAuditLogHandler
    {
        private readonly Guid _UserId;
        private readonly Guid _CompnayId;
        private WebAIAdvisorContext _dbContext;

        public CustomAuditLogHandler(WebAIAdvisorContext dbContext, Guid? userId, Guid? companyId)
        {
            _dbContext = dbContext;
            _UserId = userId.HasValue ? userId.Value : Guid.Empty;
            _CompnayId = companyId.HasValue ? companyId.Value : Guid.Empty;
        }

        public void WriteAuditLog(string activity, string location, string createBy)
        {
            IAuditLogRepository repostirory = new AuditLogRepository(_dbContext);
            var auditLog = new Audit { UserId = _UserId, CompanyId = _CompnayId, CreatedBy = createBy, CreateDate = DateTime.Now, Location = location, Activity = activity };
            var result = repostirory.WriteAuditLog(auditLog);
        }
    }
}
