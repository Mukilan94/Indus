using System;
using System.Collections.Generic;
using System.Text;
using WellAI.Advisor.DLL.Entity;

namespace WellAI.Advisor.DLL.IRepository
{
    public interface IAdminAuditLogRepository
    {
        bool WriteAuditLog(Audit auditLog);
    }
}
