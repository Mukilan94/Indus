using System;
using System.Collections.Generic;
using System.Text;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.IRepository;
using WellAI.Advisor.DLL.Repository;
using WellAI.Advisor.DLL.Entity;

namespace Well_AI.Advisor.Log.Audit
{
    public class AdminCustomAuditLogHandler
    {
        private readonly Guid _UserId;
        private readonly Guid _CompnayId;
        private WebAIAdvisorContext _dbContext;

        public AdminCustomAuditLogHandler(WebAIAdvisorContext dbContext, Guid? userId, Guid? companyId)
        {
            _dbContext = dbContext;
            _UserId = userId.HasValue ? userId.Value : Guid.Empty;
            _CompnayId = companyId.HasValue ? companyId.Value : Guid.Empty;
        }

        public void WriteAuditLog(string activity, string location, string createBy)
        {
            IAdminAuditLogRepository repostirory = new AdminAuditLogRepository(_dbContext);
            var auditLog = new WellAI.Advisor.DLL.Entity.Audit { UserId = _UserId, CompanyId = _CompnayId, CreatedBy = createBy, CreateDate = DateTime.Now, Location = location, Activity = activity };
            var result = repostirory.WriteAuditLog(auditLog);
        }

    }
}
