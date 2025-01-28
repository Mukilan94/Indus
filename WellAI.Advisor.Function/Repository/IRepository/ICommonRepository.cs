using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WellAI.Advisor.Function.MailQueue.Repository.IRepository
{
    public interface ICommonRepository
    {
        public void ResetUserSessions(ILogger log);
    }
}
