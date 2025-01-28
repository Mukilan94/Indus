using System;
using System.Collections.Generic;
using System.Text;
using WellAI.Advisor.DLL.Entity;

namespace WellAI.Advisor.Function.Notification
{
    public class ErrorHandlerForNotification
    {
        private WellAINotificationHandlerContext db;


        public ErrorHandlerForNotification(WellAINotificationHandlerContext dbContext)
        {
            db = dbContext;
        }

        public void WriteError(Exception ex, string location, string createBy)
        {
            string message = ex.Message + Environment.NewLine + ex.StackTrace;

            if (ex.InnerException != null)
            {
                message = $"{message} ***************> {ex.InnerException.Message}";
            }

            IErrorLogRepository repostirory = new ErrorLogRepository(db);
            var errorLog = new ErrorLog { ErrorMessage = message, UserId = Guid.Empty, CompanyId = Guid.Empty, CreatedBy = createBy, CreateDate = DateTime.Now, Location = location, ErrorCode = ex.HResult.ToString() };
            var result = repostirory.CreateErrorLog(errorLog);
        }
    }
}
