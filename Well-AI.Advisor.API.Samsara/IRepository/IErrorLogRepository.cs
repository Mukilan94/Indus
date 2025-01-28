using System;
using System.Collections.Generic;
using System.Text;
using Well_AI.Advisor.API.Samsara.Models;

namespace Well_AI.Advisor.API.Samsara.IRepository
{
    public interface IErrorLogRepository
    {
        bool CreateErrorLog(ErrorLog errorLog);
    }
}
