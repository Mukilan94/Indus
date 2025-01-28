using WellAI.Advisor.DLL.Entity;

namespace WellAI.Advisor.Function.Notification
{
    public interface IErrorLogRepository
    {
        bool CreateErrorLog(ErrorLog errorLog);
    }
}