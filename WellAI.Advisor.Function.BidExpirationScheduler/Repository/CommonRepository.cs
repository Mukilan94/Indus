using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellAI.Advisor.Function.BidExpirationScheduler.Data;
using WellAI.Advisor.Function.BidExpirationScheduler.Models;
using WellAI.Advisor.Function.BidExpirationScheduler.Repository.IRepository;

namespace WellAI.Advisor.Function.BidExpirationScheduler.Repository
{
    public class CommonRepository : ICommonRepository
    {
        private readonly WellAIDataContext _db;
        public CommonRepository(WellAIDataContext db)
        {
            _db = db;
        }
        public Task<int> AddNotificationAsync(MessageQueue message)
        {
            _db.MessageQueues.Add(message);
              _db.SaveChanges();
            return   Task<int>.FromResult(message.Messagequeue_id);
        }

        public Task<List<CrmCompanies>> GetCategorywiseCompanyDetail(string category)
        {

            var companyDetails = (from c in _db.CrmCompanies
                                  where c.Category.Contains(category)
                                  select c).ToList();

            return Task.FromResult(companyDetails);
        }

        public Task<List<AuctionProposalViewModel>> GetUpcomingClosingBidsAsync()
        {
            DbFunctions dfunc = null;
            DateTime dateTime = DateTime.Now;
            var result = _db.AuctionProposalView.Where(x => SqlServerDbFunctionsExtensions.DateDiffMinute(dfunc, x.AuctionEnd, dateTime) <= 60 && SqlServerDbFunctionsExtensions.DateDiffDay(dfunc, x.AuctionEnd, dateTime) == 0).ToList();

            return Task.FromResult(result);
        }
    }
}
