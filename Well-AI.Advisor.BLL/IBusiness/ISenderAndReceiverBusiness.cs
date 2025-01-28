using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WellAI.Advisor.Model.Common;

namespace WellAI.Advisor.BLL.IBusiness
{
    public interface ISenderAndReceiverBusiness
    {
        Task<MessageToQueue> GetSendOnAuctionRequestforOperator(string senderAuthorId, string SRVTenantId, string receiverProposalId, decimal AMOUNT, string Summary);
        Task<MessageToQueue> GetSendOnAuctionsBidAcceptedRejectedForService(string senderAuthorId, string SRVTenantId, string receiverProposalId, string projectId, string auctionStatus,string BidId);
    }
}
