using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WellAI.Advisor.Function.BidExpirationScheduler.Repository.IRepository;
using System.Collections;
using WellAI.Advisor.Function.BidExpirationScheduler.Models;
using System.Collections.Generic;
using System.Linq;

namespace WellAI.Advisor.Function.BidExpirationScheduler
{
    public class BidExpirationScheduler
    {
        private readonly ICommonRepository _repo;

        public BidExpirationScheduler(ICommonRepository repo)
        {
            _repo = repo;
        }

        [FunctionName("WellAIBidExpirationScheduler")]
        public async System.Threading.Tasks.Task RunAsync([TimerTrigger("0 */15 * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            IEnumerable<AuctionProposalViewModel> proposal = new List<AuctionProposalViewModel>();
            proposal = await _repo.GetUpcomingClosingBidsAsync();
            if (proposal.Count() > 0)
            {
                foreach (var item in proposal)
                {
                    var user = await _repo.GetCategorywiseCompanyDetail(item.Category);
                    foreach (var obj in user)
                    {
                        MessageQueue messageQueue = new MessageQueue { From_id = "Request Closing", To_id = obj.UserId, Type = 5, EntityId = item.ProposalId, RigId = item.RigId, JobName = "Rquest Closing", TaskName = item.Rig_Name + ":" + item.WellName + "-" + item.TaskName + " bidding will close at ", IsActive = 1, CreatedDate = DateTime.Now };
                        await _repo.AddNotificationAsync(messageQueue);
                    }
                }
            }
        }
    }
}