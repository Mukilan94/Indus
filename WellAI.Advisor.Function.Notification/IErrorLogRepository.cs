using WellAI.Advisor.DLL.Entity;

namespace WellAI.Advisor.Function.Notification
{
    public class ErrorLogRepository : IErrorLogRepository
    {
        private readonly WellAINotificationHandlerContext db;
        public ErrorLogRepository(WellAINotificationHandlerContext db)
        {
            this.db = db;
        }

        public bool CreateErrorLog(ErrorLog errorLog)
        {
            try
            {
                db.ErrorLog.Add(errorLog);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return true;
            }
        }
    }
}