using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WellAI.Advisor.Function.BidExpirationScheduler.Models;

namespace WellAI.Advisor.Function.BidExpirationScheduler.Repository.IRepository
{
    public interface ICommonRepository
    {
        Task<List<AuctionProposalViewModel>> GetUpcomingClosingBidsAsync();
        Task<int> AddNotificationAsync(MessageQueue message);
        Task<List<CrmCompanies>> GetCategorywiseCompanyDetail(string category);
    }
}
