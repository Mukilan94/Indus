using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Function.MailQueue.Repository.IRepository;

namespace WellAI.Advisor.Function.MailQueue
{
    //Phase II Changes - Session Reset
    public class SessionReset
    {
        private readonly ICommonRepository _repo;

        public SessionReset(ICommonRepository repo)
        {
            _repo = repo;
        }


        [FunctionName("SessionReset")]
        public System.Threading.Tasks.Task RunAsync([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer, ILogger log)
        {
            int sessionTimeOut = Int32.Parse(System.Environment.GetEnvironmentVariable("SessionResetTimeValue"));
            log.LogInformation($"Session Reset Starts at : {DateTime.Now}");
            try
            {               
                _repo.ResetUserSessions(log);

                log.LogInformation($"Session Reset Ends at : {DateTime.Now}");

                return Task.FromResult("True");
            }
            catch (Exception ex)
            {
                log.LogInformation($"Session Reset Error : {ex.Message}");
                return Task.FromResult("False");
            }
        }
    }
}
