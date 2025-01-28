namespace WellAI.Advisor.DLL.IRepository
{
    using WellAI.Advisor.DLL.Entity;

    public interface IAuditLogRepository
    {
        bool WriteAuditLog(Audit auditLog);
    }
}
