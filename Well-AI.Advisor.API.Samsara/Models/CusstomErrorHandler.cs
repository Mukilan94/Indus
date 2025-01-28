using System;
using System.Collections.Generic;
using System.Text;
using Well_AI.Advisor.API.Samsara.Data;
using Well_AI.Advisor.API.Samsara.IRepository;
using Well_AI.Advisor.API.Samsara.Repository;

namespace Well_AI.Advisor.API.Samsara.Models
{
    public class CusstomErrorHandler
    {
        private readonly WellAIAdvisiorApiSamsaraContext db;

        public CusstomErrorHandler(WellAIAdvisiorApiSamsaraContext _db)
        {
            db = _db;
        }

        public void WriteError(Exception ex,string Location,string createBy)
        {
                string message = ex.Message + Environment.NewLine + ex.StackTrace;

                if (ex.InnerException != null)
                {
                    message = $"{message} ***************> {ex.InnerException.Message}";
                }

                IErrorLogRepository repostirory = new ErrorLogRepository(db);
                var errorLog = new ErrorLog { ErrorMessage = message, UserId = Guid.Empty, CompanyId = Guid.Empty, CreatedBy = createBy, CreateDate = DateTime.Now, Location = Location, ErrorCode = ex.HResult.ToString() };
                var result = repostirory.CreateErrorLog(errorLog);
        }
    }
}
