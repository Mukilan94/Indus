using System;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.DLL.Repository;
namespace WellAI.Advisor.API.Dispatch.Controllers
{
    public class CustomErrorHandlerForAdvisorAPI
    {
        private WebAIAdvisorContext db;

        public CustomErrorHandlerForAdvisorAPI(WebAIAdvisorContext db)
        {
            this.db = db;
        }
        public void WriteError(Exception ex, string location, string createBy)
        {
            string message = ex.Message + Environment.NewLine + ex.StackTrace;

            if (ex.InnerException != null)
            {
                message = $"{message} ***************> {ex.InnerException.Message}";
            }

            IErrorLogRepository repostirory = new ErrorLogRepository(db, null, null);
            var errorLog = new ErrorLog { ErrorMessage = message, UserId = Guid.Empty, CompanyId = Guid.Empty, CreatedBy = createBy, CreateDate = DateTime.Now, Location = location, ErrorCode = ex.HResult.ToString() };
            var result = repostirory.CreateErrorLog(errorLog);
        }
        internal object CreateErrorLog(ErrorLog errorLog)
        {
            db.ErrorLog.Add(errorLog);
            db.SaveChanges();
            return true;
        }
    }
}
