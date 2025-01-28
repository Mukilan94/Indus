using System;
using System.Collections.Generic;
using System.Text;
using Well_AI.Advisor.API.Samsara.Data;
using Well_AI.Advisor.API.Samsara.IRepository;
using Well_AI.Advisor.API.Samsara.Models;

namespace Well_AI.Advisor.API.Samsara.Repository
{
    public class ErrorLogRepository : IErrorLogRepository
    {
        private readonly WellAIAdvisiorApiSamsaraContext db;

        public ErrorLogRepository(WellAIAdvisiorApiSamsaraContext _db)
        {
            db = _db;
        }

        public bool CreateErrorLog(ErrorLog errorLog)
        {
            try
            {
                db.errorLog.Add(errorLog);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return true;
            }      
        }
    }
}
