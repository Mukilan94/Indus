using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.DLL.Repository;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.Model.ServiceCompany.Models;

namespace WellAI.Advisor.BLL.IBusiness
{
    public interface IAuctionProposalBusiness
    {
        public enum AuBidStatus
        {
            New = 1, Ongoing, Closed, Accepted, Rejected, Counter
        };
        List<AuctionProposalViewModel> GetAuctionsListByTenantid(WellIdentityUser user, string wellId);
        List<AuctionBidStatus> GetAuctionStatuses();
        List<AuctionBidStatus> GetAuctionsBidStatus();
        List<AuctionProposalAttachmentViewModel> GetAuctionProposalAttachmentsByBidId(string BidId);
        Task<bool> AddAuctionProposal(AuctionProposalViewModel auctionProposal);
        Task<bool> UpdateAuctionProposal(AuctionProposalViewModel auctionProposal);

        Task<AuctionProposalViewModel> GetAuctionProposalByProposalId(string proposalId);
        Task<AuctionProposalViewModel> GetNewAddedAuctionProposalByProposalId(string proposalId);

        Task<List<AuctionProposalAttachmentOpeViewModel>> GetAuctionProposalOperatorAttachmentByProposalId(string proposalId);
        List<WellViewModel> GetWellForAuctionProposal(WellIdentityUser user);

        List<RigViewModel> GetRigForAuctionProposal(WellIdentityUser user,string RigId);

        List<AuctionBidderDetailsViewModel> GetAuctionsBidsDataByProposalId(string ProposalId);
        AuctionBidderDetailsViewModel GetAuctionsBidderDetailsByBidId(string BidId);
        string AuctionsBidAcceptedRejectedCounter(AuctionBidderDetailsViewModel input);
        Model.OperatingCompany.Models.AuctionBidsModel AuctionDashboardOperatorStatus(WellIdentityUser user, string wellId);
        //Phase II Changes - 02/16/2021-Passing ServiceTenantRepository
        Task<Model.ServiceCompany.Models.AuctionBidsModel> AuctionDashboardServiceStatus(string TenantId, string operId,ServiceTenantRepository servrepo);
        Model.ServiceCompany.Models.AuctionBidsModel AuctionDashboardServiceStatus(string[] TenantId);
        Task<List<WellAI.Advisor.Model.OperatingCompany.Models.ActivityViewModel>> GetProjectActivityOperator(string tenantid, WellIdentityUser user, string wellId);
        List<AuctionBidViewModel> GetAuctionsProposalListForInDepath(string tenantid, string wellId);
        List<AuctionBidViewModel> GetAuctionsProposalListForInDepathSRV(string tenantid, string wellId);
        List<Model.ServiceCompany.Models.ActivityViewModel> GetProjectActivityServiceWithRigAndWellId(string tenantid, string operId, string rigId, string wellId);
        #region Service Company
        List<WellAI.Advisor.Model.ServiceCompany.Models.ActivityViewModel> GetProjectActivityService(string tenantid,string operId);
        List<AuctionBidAmountHistoryViewModel> GetAuctionBidAmountHistoryByAuctionBidId(string auctionBidId, string tenantid);
        //Phase II Changes - 02/16/2021 - Added ServiceTenantRepository
        List<AuctionBidViewModel> GetAuctionsProposalListForSRV(string tenantid, string operId, ServiceTenantRepository servRepo);
        AuctionProposalBidDeatilsViewModel GetAuctionsProposalForSRVByProposalId(string proposalId, string tenantid);
        Task<bool> AddAucuctionBidsBySRV(AuctionProposalBidDeatilsViewModel input);
        Task<List<DLL.ServiceEntity.Projects>> GetServiceCompanyAuctionProjects(string serviceTenantId, string operTenantId, bool isActive, string wellId);
        Task<List<DLL.ServiceEntity.Projects>> GetServiceCompanyAuctionProjects(string serviceTenantId, string operTenantId, bool isActive);
        Task<List<DLL.ServiceEntity.Projects>> GetOperatingCompanyAuctionProjects(string OperatingTenantId, string operTenantId, bool isActive);
        Task<List<Model.OperatingCompany.Models.ProjectAuctionModel>> GetServiceCompanyActualProposals(string serviceTenantId, string operTenantId, string wellId);
        Task<List<Model.ServiceCompany.Models.ProjectAuctionModel>> GetOperatingCompanyActualProposals(string serviceTenantId, string operTenantId);
        Task<List<Tasks>> GetTaskForJob();
        Task<List<ServiceCategory>> GetServiceCategorys();
       AuctionProposal GetUpcomingClosingBids();

        #endregion

        List<AuctionProposalViewModel> GetAuctionsListByTenantid_V1(string tenantId, string wellId);
    }
}
