using Finbuckle.MultiTenant;
using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WellAI.Advisor.BLL.Administration;
using WellAI.Advisor.BLL.IBusiness;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.DLL.Repository;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.Model.ServiceCompany.Models;
using AuctionBidAmountHistory = WellAI.Advisor.DLL.Entity.AuctionBidAmountHistory;

namespace WellAI.Advisor.BLL.Business
{
    public class AuctionProposalBusiness : IAuctionProposalBusiness
    {
        private readonly WebAIAdvisorContext db;
        private UserManager<WellIdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public enum AuBidStatus
        {
            New = 1, Ongoing, Closed, Accepted, Rejected, Counter
        }

        public AuctionProposalBusiness(WebAIAdvisorContext db, UserManager<WellIdentityUser> userManager, IConfiguration configuration = null)
        {
            this.db = db;
            _userManager = userManager;
            _configuration = configuration;
        }

        public Model.OperatingCompany.Models.AuctionBidsModel AuctionDashboardOperatorStatus(WellIdentityUser user, string RigId)
        {
            try
            {
                DateTime LastMonthLastDate = DateTime.Today.AddDays(0 - DateTime.Today.Day);
                DateTime LastMonthFirstDate = LastMonthLastDate.AddDays(1 - LastMonthLastDate.Day);
                var userwellIds = new List<string>();
                var checkwellFilter = RigId == DLL.Constants.NoSpecificWellFilterKey;
                var BidsCount = GetAuctionsListByTenantid(user, RigId);

                var awardedBids = (from r in BidsCount where r.AuctionBidStatusId == 3 group r by r.AuctionBidStatusId into rg select new { MonthCount = rg.Count(), MonthValue = rg.Sum(x => x.BidsAmount) }).FirstOrDefault();
                var projectsStarted = (from r in BidsCount where r.AuctionBidStatusId == 1 group r by r.AuctionBidStatusId into rg select new { MonthCount = rg.Count() }).FirstOrDefault();
                var activeBids = (from r in BidsCount where r.AuctionBidStatusId == 2 group r by r.AuctionBidStatusId into rg select new { MonthCount = rg.Count(), MonthValue = rg.Sum(x => x.BidsAmount) }).FirstOrDefault();

                int AwardedBidsThisMonthCount = awardedBids == null ? 0 : awardedBids.MonthCount;
                int ProjectsStartedThisMonthCount = projectsStarted == null ? 0 : projectsStarted.MonthCount;
                double AwardedBidsThisMonthValue = awardedBids == null ? 0 : (double)awardedBids.MonthValue;
                double ProjectsStartedThisMonthValue = projectsStarted == null ? 0 : 0;

                Model.OperatingCompany.Models.AuctionBidsModel auctionBidsModel = new Model.OperatingCompany.Models.AuctionBidsModel()
                {
                    AwardedBidsThisMonthCount = AwardedBidsThisMonthCount,
                    AwardedBidsThisMonthValue = AwardedBidsThisMonthValue,
                    AwardedBidsLastMonthDate = LastMonthFirstDate,
                    AwardedBidsThisMonthDate = DateTime.Now,
                    ProjectsStartedThisMonthCount = ProjectsStartedThisMonthCount,
                    ProjectsStartedThisMonthValue = ProjectsStartedThisMonthValue,
                    ProjectsStartedLastMonthDate = LastMonthFirstDate,
                    ProjectsStartedThisMonthDate = DateTime.Now,
                    ActiveBidsCount = activeBids == null ? 0 : activeBids.MonthCount,
                    ActiveBidsValue = activeBids == null ? 0 : (double)activeBids.MonthValue,
                    ActiveBidsSinceDate = DateTime.Now
                };
                return auctionBidsModel;
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal AuctionDashboardOperatorStatus", null);
                return null;
            }
        }

        public Task<List<Model.OperatingCompany.Models.ActivityViewModel>> GetProjectActivityOperator(string tenantid, WellIdentityUser user, string wellId)
        {
            try
            {
                List<Model.OperatingCompany.Models.ActivityViewModel> result = null;

                var checkwellFilter = wellId == DLL.Constants.NoSpecificWellFilterKey;

                var temp = (from project in db.Projects
                            join ap in db.AuctionProposals on project.ProposalID equals ap.ProposalId
                            join task in db.Tasks on ap.JobId equals task.TaskId
                            where project.OprTenantID == tenantid && (ap.RigId == wellId && !checkwellFilter || checkwellFilter)
                            && task.IsCalendar == true
                            select new Model.OperatingCompany.Models.ActivityViewModel
                            {
                                ProjectId = project.ID,
                                Title = project.ProjectTitle,
                                Start = project.ActualStart == null ? ap.ProjectStartDate : project.ActualStart.Value,
                                End = project.ActualEnd == null ? Convert.ToDateTime(ap.AuctionEnd).AddHours((double)ap.ProjectDuration) : project.ActualEnd.Value,
                                IsAllDay = false,
                                Description = project.ProjectSummary,
                                ProjectStatus = project.ProjectStatus,
                                ProjectStatusName = project.ProjectStatus == 0 ? "Upcoming" : "Ongoing",
                                WellId = project.WellID,
                                ActivityIsTask = false
                            }).ToList();

                if (user != null && user.WellUser.HasValue && user.WellUser.Value)
                {
                    var userwellIds = db.UsersWells.Where(x => x.UserId == user.Id).Select(x => x.WellId).ToList();

                    result = temp.Where(x => userwellIds.FirstOrDefault(y => y == x.WellId) != null).ToList();
                }
                else
                    result = temp;

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal GetProjectActivityOperator", null);
                return null;
            } 
        }

        /// <summary>
        /// Activities with Service provider and Well Filter
        /// </summary>
        /// <param name="tenantid"></param>
        /// <param name="user"></param>
        /// <param name="wellId"></param>
        /// <returns></returns>
        public Task<List<Model.OperatingCompany.Models.ActivityViewModel>> GetProjectActivityOperatorForServiceAndAdmin(string tenantid, WellIdentityUser user, string rigId, string wellId,string serviceProviderId)
        {
            try
            {
                List<Model.OperatingCompany.Models.ActivityViewModel> result = null;

                var checkrigFilter = rigId == DLL.Constants.NoSpecificWellFilterKey;

                var checkwellFilter = wellId == DLL.Constants.NoSpecificWellFilterKey;
                var checkserviceProviderFilter = serviceProviderId == DLL.Constants.NoSpecificWellFilterKey;

                var temp = (from project in db.Projects
                            join ap in db.AuctionProposals on project.ProposalID equals ap.ProposalId
                            join task in db.Tasks on ap.JobId equals task.TaskId
                            where project.OprTenantID == tenantid && (ap.RigId == rigId && !checkrigFilter || checkrigFilter)
                            && (ap.WellId == wellId && !checkwellFilter || checkwellFilter)
                            && (project.ServiceCompID == serviceProviderId && !checkserviceProviderFilter || checkserviceProviderFilter)
                            && task.IsCalendar == true
                            select new Model.OperatingCompany.Models.ActivityViewModel
                            {
                                ProjectId = project.ID,
                                Title = project.ProjectTitle,
                                Start = project.ActualStart == null ? ap.ProjectStartDate : project.ActualStart.Value,
                                End = project.ActualEnd == null ? Convert.ToDateTime(ap.AuctionEnd).AddHours((double)ap.ProjectDuration) : project.ActualEnd.Value,
                                IsAllDay = false,
                                Description = project.ProjectSummary,
                                ProjectStatus = project.ProjectStatus,
                                ProjectStatusName = project.ProjectStatus == 0 ? "Upcoming" : "Ongoing",
                                WellId = project.WellID,
                                ActivityIsTask = false
                            }).ToList();

                if (user != null && user.WellUser.HasValue && user.WellUser.Value)
                {
                    var userwellIds = db.UsersWells.Where(x => x.UserId == user.Id).Select(x => x.WellId).ToList();

                    result = temp.Where(x => userwellIds.FirstOrDefault(y => y == x.WellId) != null).ToList();
                }
                else
                    result = temp;

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal GetProjectActivityOperator", null);
                return null;
            }
        }



        //Phase II Changes - 02/16/2021
        public Task<Model.ServiceCompany.Models.AuctionBidsModel> AuctionDashboardServiceStatus(string tenantid, string operId,ServiceTenantRepository servrepo)
        {
            try
            {
                var nospecificOperator = operId == DLL.Constants.NoSpecificWellFilterKey;

                DateTime LastMonthLastDate = DateTime.Today.AddDays(0 - DateTime.Today.Day);
                DateTime LastMonthFirstDate = LastMonthLastDate.AddDays(1 - LastMonthLastDate.Day);

                var BidsList = GetAuctionsProposalListForSRV(tenantid, operId, servrepo);

                var result = (from a in BidsList
                              select new
                              {
                                  ProjectDateTime = Convert.ToDateTime(a.ProjectStartDate),
                                  Months = Convert.ToDateTime(a.ProjectStartDate).Month,
                                  BidAmount = a.BidAmount,
                                  a.ProjectDuration,
                                  a.ProposalId,
                                  BidStatusId = a.BidStatusId,
                                  BiddingStatus = a.BidStatusName
                              });

                var awardedBids = (from r in result where r.BidStatusId == 3 group r by r.BidStatusId into rg select new { MonthCount = rg.Count(), MonthValue = rg.Sum(x => x.BidAmount) }).FirstOrDefault();
                var projectsStarted = (from r in result where r.BidStatusId == 1 group r by r.BidStatusId into rg select new { MonthCount = rg.Count(), MonthValue = rg.Sum(x => x.BidAmount) }).FirstOrDefault();
                var activeBids = (from r in result where r.BidStatusId == 2 group r by r.BidStatusId into rg select new { MonthCount = rg.Count(), MonthValue = rg.Sum(x => x.BidAmount) }).FirstOrDefault();

                int AwardedBidsThisMonthCount = awardedBids == null ? 0 : awardedBids.MonthCount;
                int ProjectsStartedThisMonthCount = projectsStarted == null ? 0 : projectsStarted.MonthCount;
                double AwardedBidsThisMonthValue = awardedBids == null ? 0 : (double)awardedBids.MonthValue;
                double ProjectsStartedThisMonthValue = projectsStarted == null ? 0 : (double)projectsStarted.MonthValue;

                Model.ServiceCompany.Models.AuctionBidsModel auctionBidsModel = new Model.ServiceCompany.Models.AuctionBidsModel()
                {
                    AwardedBidsThisMonthCount = AwardedBidsThisMonthCount,
                    AwardedBidsThisMonthValue = AwardedBidsThisMonthValue,
                    AwardedBidsLastMonthDate = LastMonthFirstDate,
                    AwardedBidsThisMonthDate = DateTime.Now,
                    ProjectsStartedThisMonthCount = ProjectsStartedThisMonthCount,
                    ProjectsStartedThisMonthValue = ProjectsStartedThisMonthValue,
                    ProjectsStartedLastMonthDate = LastMonthFirstDate,
                    ProjectsStartedThisMonthDate = DateTime.Now,
                    ActiveBidsCount = activeBids == null ? 0 : activeBids.MonthCount,
                    ActiveBidsValue = activeBids == null ? 0 : (double)activeBids.MonthValue,
                    ActiveBidsSinceDate = DateTime.Now
                };
                return Task.FromResult(auctionBidsModel);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposals AuctionDashboardServiceStatus", null);
                Model.ServiceCompany.Models.AuctionBidsModel auctionBidsModel = new Model.ServiceCompany.Models.AuctionBidsModel();
                return Task.FromResult(auctionBidsModel);
            }
        }

        public Model.ServiceCompany.Models.AuctionBidsModel AuctionDashboardServiceStatus(string[] tenantid)
        {
            try
            {
                var result = (from a in db.AuctionProposals
                              join ab in db.AuctionBids on a.ProposalId equals ab.ProposalId
                              where tenantid.Contains(a.TenantID)
                              select new
                              {
                                  a.ProposalId,
                                  ab.BidStatus,
                                  a.BidStatusId
                              }
                              ).ToList();

                Model.ServiceCompany.Models.AuctionBidsModel auctionBidsModel = new Model.ServiceCompany.Models.AuctionBidsModel()
                {
                    ActiveBidsCount = result.Where(x => x.BidStatusId == (int)AuBidStatus.Ongoing).Count(),
                    AwardedBidsLastMonthCount = result.Where(x => x.BidStatus == (int)AuBidStatus.Accepted).Count(),
                };
                return auctionBidsModel;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal AuctionDashboardServiceStatus", null);
                Model.ServiceCompany.Models.AuctionBidsModel auctionBidsModel = new Model.ServiceCompany.Models.AuctionBidsModel();
                return auctionBidsModel;
            }
        }

        public async Task<bool> AddAuctionProposal(AuctionProposalViewModel auctionProposal)
        {
            try
            {
                AuctionProposal auction = new AuctionProposal()
                {
                    ModifyDate = DateTime.Now,
                    Created = DateTime.Now,
                    ProposalId = auctionProposal.ProposalId,
                    AuthorId = auctionProposal.AuthorId,
                    WellId = auctionProposal.WellId,
                    JobId = auctionProposal.JobId,
                    BidStatusId = (int)AuBidStatus.New,
                    ProjectDuration = auctionProposal.ProjectDuration,
                    Subject = auctionProposal.Subject,
                    Summary = auctionProposal.Summary,
                    Body = auctionProposal.Body,
                    TenantID = auctionProposal.TenantID,
                    AuctionStart = auctionProposal.AuctionStart,
                    AuctionEnd = auctionProposal.AuctionEnd,
                    ProjectStartDate = auctionProposal.ProjectStartDate,
                    AuctionNumber = auctionProposal.AuctionNumber,
                    IsPrivate = auctionProposal.IsPrivate == null ? false : auctionProposal.IsPrivate.Value,
                    SRVTenantId = auctionProposal.SRVTenantId,
                    RigId = auctionProposal.RigId,
                    Category = auctionProposal.ServiceCategoryId
                };
                db.AuctionProposals.Add(auction);
                await db.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal AddAuctionProposal", null);
                return false;
            }
        }

        public async Task<DateTime> ConvertToCstTime(DateTime CstTime)
        {
            try
            {
                if(CstTime != null)
                {
                    DateTime timeUtc = (TimeZoneInfo.ConvertTimeToUtc(CstTime,TimeZoneInfo.Local));
                    TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                    DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
                    return await Task.FromResult(cstTime);
                }
                return await Task.FromResult(CstTime);
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal ConvertToCstTime", null);
                return CstTime;
            }
        }

        public async Task<bool> UpdateAuctionProposal(AuctionProposalViewModel auctionProposal)
        {
            try
            {
                var result = db.AuctionProposals.Where(x => x.ProposalId == auctionProposal.ProposalId).FirstOrDefault();
                if (result == null)
                {
                    return false;
                }
                if (result.BidStatusId > 1)
                {
                    return true;
                }
                result.ModifyDate = DateTime.Now;
                result.AuthorId = auctionProposal.AuthorId;
                result.WellId = auctionProposal.WellId;
                result.JobId = auctionProposal.JobId;
                result.BidStatusId = auctionProposal.AuctionBidStatusId;
                result.Subject = auctionProposal.Subject;
                result.Summary = auctionProposal.Summary;
                result.Body = auctionProposal.Body;
                result.TenantID = auctionProposal.TenantID;
                result.AuctionStart = auctionProposal.AuctionStart;
                result.AuctionEnd = auctionProposal.AuctionEnd;
                result.ProjectDuration = auctionProposal.ProjectDuration;
                result.ProjectStartDate = auctionProposal.ProjectStartDate;
                result.RigId = auctionProposal.RigId;
                result.Category = auctionProposal.ServiceCategoryId;
                result.SRVTenantId = auctionProposal.SRVTenantId;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal UpdateAuctionProposal", null);
                return false;
            }
        }

        //Phase II Changes - 02/16/2021 - Added ServiceTenantRepository for Subscribed Rigs filter
        public List<AuctionBidViewModel> GetAuctionsProposalListForSRV(string tenantid, string operId,ServiceTenantRepository servRepo)        
        {
            try
            {
                var nospecificOper = operId == DLL.Constants.NoSpecificWellFilterKey;

                var serviceTenantUsers = db.TenantUsers.Where(x => x.TenantId == tenantid).ToList();

                List<SubscriptionOperatorRigs> subscribedRigs = new List<SubscriptionOperatorRigs>();
                List<string> subscribedRigsArray = new List<string>();

                //if (operId != "00000000-0000-0000-0000-000000000000")
                //{
                try
                {
                    subscribedRigs = servRepo.Get_SubsciberProviderRigs(operId).Result;
                    subscribedRigsArray = subscribedRigs.Select(item => item.RigId).ToList();
                }
                catch
                {

                }
                    
                //}
                //else
                //{
                //    subscribedRigs = servRepo.Get_AllSubsciberProviderRigs().Result;
                //    subscribedRigsArray = subscribedRigs.Select(item => item.RigId).ToList();
                //}

                //Phase II Changes - 02/16/2021
                //Split the Bids query as two - one of Upcoming and Active, one is for Completed
                //Subscribed Rigs filter will implement only on Upcoming and Active, and not on Completed

                List<AuctionBidViewModel> result = new List<AuctionBidViewModel>();

                List<AuctionBidViewModel> activeStatusResult = new List<AuctionBidViewModel>();
                List<AuctionBidViewModel> closedStatusResult = new List<AuctionBidViewModel>();
                if (subscribedRigsArray.Count > 0)
                {
                    //DWOP - Task data from DrillPlanDetails
                    activeStatusResult = (from auction in db.AuctionProposals
                                          join bitstatus in db.AuctionBidStatuses on auction.BidStatusId equals bitstatus.Id
                                          join corporate in db.CorporateProfile on auction.TenantID equals corporate.TenantId
                                          join t in db.DrillPlanDetails on auction.JobId equals t.TaskId into taskLj
                                          from t in taskLj.DefaultIfEmpty()
                                          join task in db.Tasks on auction.JobId equals task.TaskId into tasks
                                          from task in tasks.DefaultIfEmpty()
                                          join rig in db.rig_register on auction.RigId equals rig.Rig_id
                                          join well in db.WellRegister on auction.WellId equals well.well_id
                                          join auc in db.AuctionBids on auction.ProposalId equals auc.ProposalId into au1
                                          from auc in au1.DefaultIfEmpty()
                                          where ((auction.TenantID == operId && !nospecificOper || nospecificOper) && well.Prediction == true && rig.isActive == true)
                                          && subscribedRigsArray.Contains(rig.Rig_id) && bitstatus.Name != "Completed" || auction.SRVTenantId == tenantid
                                          orderby auction.Created descending
                                          select new AuctionBidViewModel
                                          {
                                              ProposalId = auction.ProposalId,
                                              TenantID = corporate.Name,
                                              Subject = auction.Subject,
                                              AuctionEnd = auction.AuctionEnd,
                                              ProjectStartDate = auction.ProjectStartDate,
                                              ProjectDuration = auction.ProjectDuration,
                                              AuctionStart = auction.AuctionStart,
                                              AuctionBidStatusName = bitstatus.Name,
                                              JobId = t == null ? auction.JobId : t.TaskName,
                                              WellName = well == null ? "N/A" : well.wellname,
                                              RigName = rig == null ? "N/A" : rig.Rig_Name,
                                              AuctionNumber = auction.AuctionNumber,
                                              BidStatusId = auction.BidStatusId,
                                              BidAmount = auc.BidAmount,
                                              BidID = auc.BidID,
                                              ModifyDate = auction.ModifyDate,
                                              SRVTenantId = auction.SRVTenantId,
                                              Depth = t.Depth == null ? task.Depth : t.Depth,
                                              AuctionBidStatus = bitstatus.Name == "Upcoming" ? 1 : bitstatus.Name == "Active" ? 2 : 3
                                          }).OrderBy(o => o.AuctionBidStatus).Distinct().ToList();

                    var BidList = activeStatusResult.Where(x => x.SRVTenantId != null).ToList();
                    var AsignedVendorBids = BidList.Where(x => x.SRVTenantId == tenantid).ToList();
                    activeStatusResult.Except(BidList);
                    activeStatusResult.AddRange(AsignedVendorBids);
                    activeStatusResult = activeStatusResult.Distinct().ToList();
                }


                //DWOP - Task data from DrillPlanDetails
                if (subscribedRigsArray.Count > 0)
                {
                    closedStatusResult = (from auction in db.AuctionProposals
                                          join bitstatus in db.AuctionBidStatuses on auction.BidStatusId equals bitstatus.Id
                                          join corporate in db.CorporateProfile on auction.TenantID equals corporate.TenantId
                                          join t in db.DrillPlanDetails on auction.JobId equals t.TaskId into taskLj
                                          from t in taskLj.DefaultIfEmpty()
                                          join rig in db.rig_register on auction.RigId equals rig.Rig_id
                                          join well in db.WellRegister on auction.WellId equals well.well_id
                                          join auc in db.AuctionBids on auction.ProposalId equals auc.ProposalId into au1
                                          from auc in au1.DefaultIfEmpty()
                                          where (auction.TenantID == operId && !nospecificOper || nospecificOper) && well.Prediction == true && rig.isActive == true
                                          && bitstatus.Name == "Completed"
                                          orderby auction.Created descending
                                          select new AuctionBidViewModel
                                          {
                                              ProposalId = auction.ProposalId,
                                              TenantID = corporate.Name,
                                              Subject = auction.Subject,
                                              AuctionEnd = auction.AuctionEnd,
                                              ProjectStartDate = auction.ProjectStartDate,
                                              ProjectDuration = auction.ProjectDuration,
                                              AuctionStart = auction.AuctionStart,
                                              AuctionBidStatusName = bitstatus.Name,
                                              JobId = t == null ? auction.JobId : t.TaskName,
                                              WellName = well == null ? "N/A" : well.wellname,
                                              RigName = rig == null ? "N/A" : rig.Rig_Name,
                                              AuctionNumber = auction.AuctionNumber,
                                              BidStatusId = auction.BidStatusId,
                                              BidAmount = auc.BidAmount,
                                              BidID = auc.BidID,
                                              ModifyDate = auction.ModifyDate,
                                              Depth = t.Depth,
                                              AuctionBidStatus = bitstatus.Name == "Upcoming" ? 1 : bitstatus.Name == "Active" ? 2 : 3
                                          }).Distinct().ToList();
                }
                result = activeStatusResult.Union(closedStatusResult).ToList();

                //var resultAgg = result.GroupBy(x => x.BidID).Select(g => g.First()).ToList();
                var resultAgg = result.GroupBy(x => x.AuctionNumber).Select(g => g.First()).ToList();

                if (resultAgg.Count > 0)
                {
                    foreach (var resItem in resultAgg)
                    {
                        var bids = db.AuctionBidAmountHistories.Where(x => x.AuctionBidId == resItem.BidID).ToList();
                        var bidsCount = bids.Where(x => serviceTenantUsers.Any(y => y.UserId == x.AuthorId)).Count();

                        resItem.Bids = bidsCount;
                    }
                }
                
                return resultAgg;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal GetAuctionsProposalListForSRV", null);
                return null;
            }
        }

        public List<AuctionProposalViewModel> GetAuctionsListByTenantid(WellIdentityUser user, string RigId)
        {
            try
            {
                List<AuctionProposalViewModel> result = null;

                var checkwellFilter = RigId == DLL.Constants.NoSpecificWellFilterKey;
                //DWOP - Task data from DrillPlanDetails
                var temp = (from auction in db.AuctionProposals
                            join bitstatus in db.AuctionBidStatuses on auction.BidStatusId equals bitstatus.Id
                            join corporate in db.CorporateProfile on auction.TenantID equals corporate.TenantId
                            join t in db.DrillPlanDetails on auction.JobId equals t.TaskId into taskLj
                            from t in taskLj.DefaultIfEmpty()
                            join Task in db.Tasks on auction.JobId equals Task.TaskId into Task1
                            from Task in Task1.DefaultIfEmpty()
                            join rig in db.rig_register on auction.RigId equals rig.Rig_id
                            join well in db.WellRegister on auction.WellId equals well.well_id
                            join au in db.AuctionBids on auction.ProposalId equals au.ProposalId into au1
                            from au in au1.DefaultIfEmpty()
                            where auction.TenantID == user.TenantId && well.Prediction == true && rig.isActive == true && (auction.RigId == RigId && !checkwellFilter || checkwellFilter)
                            select new AuctionProposalViewModel
                            {
                                ProposalId = auction.ProposalId,
                                TenantID = corporate.TenantId,
                                Subject = auction.Subject,
                                AuctionEnd = auction.AuctionEnd,
                                ProjectStartDate = auction.ProjectStartDate,
                                ProjectDuration = auction.ProjectDuration,
                                AuctionStart = auction.AuctionStart,
                                AuthorId = auction.AuthorId,
                                AuctionBidStatusName = bitstatus.Name,
                                AuctionBidStatusId = bitstatus.Id,
                                Body = auction.Body,
                                Created = auction.Created,
                                JobId = t == null ? Task.Name : t.TaskName,
                                WellId = auction.WellId,
                                WellName = well == null ? "N/A" : well.wellname,
                                AuctionNumber = auction.AuctionNumber,
                                RigName = rig.Rig_Name,
                                BidsAmount = au.BidAmount,
                                ModifyDate = auction.ModifyDate,
                                Depth = t.Depth == null ? Task.Depth : t.Depth,
                                RigId = auction.RigId,
                                AuctionBidStatusOrder = bitstatus.Name == "Upcoming" ? 1 : bitstatus.Name == "Active" ? 2 : 3
                            }).OrderBy(o => o.AuctionBidStatusOrder).Distinct().ToList();

                if (user != null && user.WellUser.HasValue && user.WellUser.Value)
                {
                    var userwellIds = db.UsersWells.Where(x => x.UserId == user.Id).Select(x => x.WellId).ToList();
                    if (userwellIds.Count > 0)
                    {
                        result = temp.Where(x => userwellIds.FirstOrDefault(y => y == x.WellId) != null).ToList();
                    }
                    else
                    {
                        result = temp;
                    }
                }
                else
                    result = temp;

                foreach (var resItem in result)
                {
                    var bids = (from auh in db.AuctionBidAmountHistories
                                join aub in db.AuctionBids.Where(x => x.ProposalId == resItem.ProposalId) on auh.AuctionBidId equals aub.BidID
                                join tu in db.TenantUsers on auh.AuthorId equals tu.UserId
                                select new AuctionBidAmountHistoryViewModel
                                {
                                    AuctionBidId = auh.AuctionBidId,
                                    AuthorId = auh.AuthorId,
                                    BidAmount = auh.BidAmount,
                                    Id = tu.TenantId
                                }).ToList();

                    var maxBidsofTenants = new List<decimal>();

                    var tenantGroups = bids.GroupBy(x => x.Id);
                    if (bids.Count > 0)
                    {
                        foreach (var tenGroup in tenantGroups)
                        {
                            var curMax = tenGroup.Max(x => x.BidAmount);
                            maxBidsofTenants.Add(curMax);
                        }
                    }

                    resItem.Bids = tenantGroups.Count();

                    if (maxBidsofTenants.Count > 0)
                    {
                        resItem.MaxBidsValue = maxBidsofTenants.Max();
                        resItem.MinBidsValue = maxBidsofTenants.Min();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal GetAuctionsListByTenantid", null);
                return null;
            }
        }

        public List<AuctionProposalViewModel> GetAuctionsListByTenantid_V1(string tenantId, string rigId)
        {
            try
            {
                List<AuctionProposalViewModel> result = null;

                var checkwellFilter = rigId == DLL.Constants.NoSpecificWellFilterKey;
                //DWOP - Task data from DrillPlanDetails
                var temp = (from auction in db.AuctionProposals
                            join bitstatus in db.AuctionBidStatuses on auction.BidStatusId equals bitstatus.Id
                            join corporate in db.CorporateProfile on auction.TenantID equals corporate.TenantId
                            join t in db.DrillPlanDetails on auction.JobId equals t.TaskId into taskLj
                            from t in taskLj.DefaultIfEmpty()
                            join Task in db.Tasks on auction.JobId equals Task.TaskId into Task1
                            from Task in Task1.DefaultIfEmpty()
                            join rig in db.rig_register on auction.RigId equals rig.Rig_id
                            join well in db.WellRegister on auction.WellId equals well.well_id
                            join au in db.AuctionBids on auction.ProposalId equals au.ProposalId into au1
                            from au in au1.DefaultIfEmpty()
                            where auction.TenantID == tenantId && well.Prediction == true && rig.isActive == true && (auction.RigId == rigId && !checkwellFilter || checkwellFilter)
                            select new AuctionProposalViewModel
                            {
                                ProposalId = auction.ProposalId,
                                TenantID = corporate.TenantId,
                                Subject = auction.Subject,
                                AuctionEnd = auction.AuctionEnd,
                                ProjectStartDate = auction.ProjectStartDate,
                                ProjectDuration = auction.ProjectDuration,
                                AuctionStart = auction.AuctionStart,
                                AuthorId = auction.AuthorId,
                                AuctionBidStatusName = bitstatus.Name,
                                AuctionBidStatusId = bitstatus.Id,
                                Body = auction.Body,
                                Created = auction.Created,
                                JobId = t == null ? Task.Name : t.TaskName,
                                WellId = auction.WellId,
                                WellName = well == null ? "N/A" : well.wellname,
                                AuctionNumber = auction.AuctionNumber,
                                RigName = rig.Rig_Name,
                                BidsAmount = au.BidAmount,
                                ModifyDate = auction.ModifyDate,
                                Depth = t.Depth == null ? Task.Depth : t.Depth,
                                RigId = auction.RigId,
                                AuctionBidStatusOrder = bitstatus.Name == "Upcoming" ? 1 : bitstatus.Name == "Active" ? 2 : 3
                            }).OrderBy(o => o.AuctionBidStatusOrder).Distinct().ToList();

                //if (user != null && user.WellUser.HasValue && user.WellUser.Value)
                //{
                //    var userwellIds = db.UsersWells.Where(x => x.UserId == user.Id).Select(x => x.WellId).ToList();
                //    if (userwellIds.Count > 0)
                //    {
                //        result = temp.Where(x => userwellIds.FirstOrDefault(y => y == x.WellId) != null).ToList();
                //    }
                //    else
                //    {
                //        result = temp;
                //    }
                //}
                //else
                //    result = temp;
                result = temp;

                foreach (var resItem in result)
                {
                    var bids = (from auh in db.AuctionBidAmountHistories
                                join aub in db.AuctionBids.Where(x => x.ProposalId == resItem.ProposalId) on auh.AuctionBidId equals aub.BidID
                                join tu in db.TenantUsers on auh.AuthorId equals tu.UserId
                                select new AuctionBidAmountHistoryViewModel
                                {
                                    AuctionBidId = auh.AuctionBidId,
                                    AuthorId = auh.AuthorId,
                                    BidAmount = auh.BidAmount,
                                    Id = tu.TenantId
                                }).ToList();

                    var maxBidsofTenants = new List<decimal>();

                    var tenantGroups = bids.GroupBy(x => x.Id);
                    if (bids.Count > 0)
                    {
                        foreach (var tenGroup in tenantGroups)
                        {
                            var curMax = tenGroup.Max(x => x.BidAmount);
                            maxBidsofTenants.Add(curMax);
                        }
                    }

                    resItem.Bids = tenantGroups.Count();

                    if (maxBidsofTenants.Count > 0)
                    {
                        resItem.MaxBidsValue = maxBidsofTenants.Max();
                        resItem.MinBidsValue = maxBidsofTenants.Min();
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal GetAuctionsListByTenantid_V1", null);
                return null;
            }
        }

        public List<AuctionBidStatus> GetAuctionStatuses()
        {
            return db.AuctionBidStatuses.Take(3).ToList();
        }

        public async Task<AuctionProposalViewModel> GetAuctionProposalByProposalId(string proposalId)
        {
            try
            {
                var AuctionBidderDetailsViewModels = GetAuctionsBidsDataByProposalId(proposalId);
                //DWOP - Task data from DrillPlanDetails
                var result = await (from auction in db.AuctionProposals
                                    join ab in db.AuctionBidStatuses on auction.BidStatusId equals ab.Id
                                    join well in db.WELL_REGISTERs on auction.WellId equals well.well_id
                                    join rigs in db.rig_register on auction.RigId equals rigs.Rig_id
                                    join task in db.DrillPlanDetails on auction.JobId equals task.TaskId
                                    join catry in db.serviceCategories on auction.Category equals catry.ServiceCategoryId
                                    where auction.ProposalId == proposalId
                                    select new AuctionProposalViewModel
                                    {
                                        AuctionBidStatusName = ab.Name,
                                        AuctionBidStatusId = ab.Id,
                                        AuctionStart = auction.AuctionStart,
                                        AuctionEnd = auction.AuctionEnd,
                                        Body = auction.Body,
                                        JobId = auction.JobId,
                                        WellId = auction.WellId,
                                        ProposalId = auction.ProposalId,
                                        ProjectStartDate = auction.ProjectStartDate,
                                        ProjectDuration = auction.ProjectDuration,
                                        Subject = auction.Subject,
                                        AuctionNumber = auction.AuctionNumber,
                                        AuctionBidderDetailsViewModels = AuctionBidderDetailsViewModels,
                                        Summary = auction.Summary,
                                        WellName = well.wellname,
                                        RigName = rigs.Rig_Name,
                                        RigId = auction.RigId,
                                        JobName = task.TaskName,
                                        ServiceCategoryId = auction.Category,
                                        CategoryName = catry.Name,
                                        IsPrivate= auction.IsPrivate,
                                        AuthorId=auction.AuthorId,
                                        SRVTenantId=auction.SRVTenantId
                                    }
                          ).FirstOrDefaultAsync();
                if (result != null)
                {
                    return result;
                }
                else
                {
                    AuctionProposalViewModel modelnull = new AuctionProposalViewModel();
                    modelnull.ProposalId = proposalId;
                    return modelnull;
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal GetAuctionProposalByProposalId", null);
                return null;
            }
        }

        public async Task<AuctionProposalViewModel> GetNewAddedAuctionProposalByProposalId(string proposalId)
        {
            try
            {
                var result = await (from auction in db.AuctionProposals
                                    join ab in db.AuctionBidStatuses on auction.BidStatusId equals ab.Id
                                    join well in db.WELL_REGISTERs on auction.WellId equals well.well_id into welllj
                                    from well in welllj.DefaultIfEmpty()
                                    where auction.ProposalId == proposalId
                                    select new AuctionProposalViewModel
                                    {
                                        AuctionBidStatusName = ab.Name,
                                        AuctionBidStatusId = ab.Id,
                                        AuctionStart = auction.AuctionStart,
                                        AuctionEnd = auction.AuctionEnd,
                                        Body = auction.Body,
                                        JobId = auction.JobId,
                                        WellId = well == null ? "N/A" : well.wellname,
                                        WellName = well == null ? "N/A" : well.wellname,
                                        ProposalId = auction.ProposalId,
                                        ProjectStartDate = auction.ProjectStartDate,
                                        ProjectDuration = auction.ProjectDuration,
                                        Subject = auction.Subject,
                                        AuctionNumber = auction.AuctionNumber,
                                        SRVTenantId = auction.SRVTenantId
                                    }
                          ).FirstOrDefaultAsync();
                if (result != null)
                {
                    return result;
                }
                else
                {
                    AuctionProposalViewModel modelnull = new AuctionProposalViewModel();
                    modelnull.ProposalId = proposalId;
                    return modelnull;
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal GetNewAddedAuctionProposalByProposalId", null);
                return null;
            }
        }

        public List<WellViewModel> GetWellForAuctionProposal(WellIdentityUser user)
        {
            try
            {
                List<WellViewModel> result = null;

                result = db.WellRegister.Where(x => x.customer_id == user.TenantId && x.Prediction == true).Select(x =>
                                new WellViewModel()
                                {
                                    WellId = x.well_id,
                                    Name = x.wellname,
                                    RigId = x.RigID
                                }).ToList();
                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal GetWellForAuctionProposal", null);
                return null;
            }
        }

        public List<RigViewModel> GetRigForAuctionProposal(WellIdentityUser user,string RigId)
        {
            try
            {
                List<RigViewModel> Result = null;
                var checkwellFilter = RigId == DLL.Constants.NoSpecificWellFilterKey;
                Result = db.rig_register.Where(r => r.TenantID == user.TenantId && r.isActive == true && (r.Rig_id == RigId && !checkwellFilter || checkwellFilter)).Select(x =>
                    new RigViewModel()
                    {
                        RigId = x.Rig_id,
                        RigName = x.Rig_Name
                    }).ToList();

                return Result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal GetRigForAuctionProposal", null);
                return null;
            }
        }

        public List<AuctionBidderDetailsViewModel> GetAuctionsBidsDataByProposalId(string ProposalId)
        {
            try
            {
                var result = new List<AuctionBidderDetailsViewModel>();

                var history = (from auctionbid in db.AuctionBids
                               join auctionbidhi in db.AuctionBidAmountHistories on auctionbid.BidID equals auctionbidhi.AuctionBidId
                               join tu in db.TenantUsers on auctionbidhi.AuthorId equals tu.UserId
                               join corporate in db.CorporateProfile on tu.TenantId equals corporate.TenantId
                               where auctionbid.ProposalId == ProposalId
                               select new AuctionBidderDetailsViewModel
                               {
                                   ProposalId = auctionbid.ProposalId,
                                   BidId = auctionbidhi.Id,
                                   BidAmount = auctionbidhi.BidAmount,
                                   ServiceCompany = corporate.Name,
                                   BidDescription = auctionbidhi.BidSummary,
                                   BidDate = auctionbidhi.BidDate,
                                   ServiceTenantId = corporate.TenantId
                               }).ToList();

                var groups = history.GroupBy(x => x.ServiceTenantId).ToList();

                foreach (var group in groups)
                {
                    var lastBid = group.OrderByDescending(x => x.BidDate).FirstOrDefault();

                    result.Add(lastBid);
                }

                return result.OrderByDescending(x => x.BidDate).ToList();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal GetAuctionsBidsDataByProposalId", null);
                return null;
            }
        }

        public AuctionBidderDetailsViewModel GetAuctionsBidderDetailsByBidId(string BidId)
        {
            try
            {
                var result = (from auctionbid in db.AuctionBids
                              join auh in db.AuctionBidAmountHistories on auctionbid.BidID equals auh.AuctionBidId
                              join tu in db.TenantUsers on auh.AuthorId equals tu.UserId
                              join corporate in db.CorporateProfile on tu.TenantId equals corporate.TenantId
                              join user in _userManager.Users on auctionbid.AuthorId equals user.Id
                              join bs in db.AuctionBidStatuses on auctionbid.BidStatus equals bs.Id into bsn
                              from bs in bsn.DefaultIfEmpty()
                              where auh.Id == BidId
                              select new AuctionBidderDetailsViewModel
                              {
                                  ProposalId = auctionbid.ProposalId,
                                  BidId = auctionbid.BidID,
                                  BidAmount = auh.BidAmount,
                                  ServiceCompany = corporate.Name,
                                  BidDescription = auh.BidSummary,
                                  BidDate = auh.BidDate,
                                  BidderName = $"{user.FirstName} {user.LastName}",
                                  BidderEmail = user.Email,
                                  BidderMobile = user.Mobile,
                                  ServiceTenantId = auctionbid.TenantID,
                                  BidStatusId = auctionbid.BidStatus == null ? 0 : auctionbid.BidStatus,
                                  BidStatusName = bs.Name
                              }).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal GetAuctionsBidderDetailsByBidId", null);
                return null;
            }
        }

        public List<AuctionProposalAttachmentViewModel> GetAuctionProposalAttachmentsByBidId(string bidId)
        {
            try
            {
                var resultBid = db.AuctionBids.Find(bidId);
                if (resultBid != null)
                    return db.AuctionProposalAttachments.
                        Where(x => x.TenantID == resultBid.TenantID && x.ProposalID == resultBid.ProposalId)
                        .Select(x => new AuctionProposalAttachmentViewModel { AttachmentId = x.AttachmentId, FileName = x.FileName }).ToList();
                return null;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal GetAuctionProposalAttachmentsByBidId", null);
                return null;
            }
        }

        public string AuctionsBidAcceptedRejectedCounter(AuctionBidderDetailsViewModel input)
        {
            try
            {
                var result = "";

                var auctionProposalResult = db.AuctionProposals.Find(input.ProposalId);
                if (auctionProposalResult.BidStatusId == (int)AuBidStatus.Closed)
                {
                    return result;
                }
                var auctionBids = db.AuctionBids.Find(input.BidId);
                if (input.BidStatusName == "Accept")
                {
                    auctionBids.BidStatus = (int)AuBidStatus.Accepted;
                    auctionProposalResult.BidStatusId = (int)AuBidStatus.Closed;
                    auctionProposalResult.ModifyDate = DateTime.Now;
                    AuctionProject auctionProject = new AuctionProject()
                    {
                        Id = Guid.NewGuid().ToString(),
                        ProposalId = input.ProposalId,
                        BidID = input.BidId,
                        AuthorId = input.AuthorId,
                        OperTenantID = auctionProposalResult.TenantID,
                        ServiceTenantID = auctionBids.TenantID
                    };
                    int count = db.Projects.Count() + 1;

                    DateTime timeUtc = DateTime.UtcNow;
                    TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                    DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);

                    DLL.ServiceEntity.Projects projects = new DLL.ServiceEntity.Projects()
                    {
                        ID = Guid.NewGuid().ToString(),
                        ProposalID = input.ProposalId,
                        ProjectID = "PRO" + count.ToString("00000"),
                        BidID = input.BidId,
                        DateCreated = cstTime,
                        CreateById = input.AuthorId,
                        OprTenantID = auctionProposalResult.TenantID,
                        ServiceCompID = auctionBids.TenantID,
                        ProjectDescription = auctionProposalResult.Body,
                        ProjectSummary = auctionProposalResult.Summary,
                        ProjectTitle = input.Title,
                        ProposedStartDate = auctionProposalResult.ProjectStartDate,
                        WellID = auctionProposalResult.WellId,
                        ModifyDate = cstTime
                    };

                    result = projects.ProjectID;

                    db.Projects.Add(projects);
                    db.AuctionProjects.Add(auctionProject);
                }
                else if (input.BidStatusId == (int)AuBidStatus.Counter)
                {
                }
                else
                {
                    auctionBids.BidStatus = (int)AuBidStatus.Rejected;
                }
                db.SaveChanges();

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal AuctionsBidAcceptedRejectedCounter", null);
                return null;
            }
        }

        public List<AuctionBidViewModel> GetAuctionsProposalListForInDepath(string tenantid, string WellId)
        {
            try
            {
                var checkwellFilter = WellId == DLL.Constants.NoSpecificWellFilterKey;
                var result = new List<AuctionBidViewModel>();
                if (WellId != null)
                {
                    //DWOP - Task data from DrillPlanDetails
                    result = (from auction in db.AuctionProposals
                              join proj in db.AuctionProjects on auction.ProposalId equals proj.ProposalId
                              join bitstatus in db.AuctionBidStatuses on auction.BidStatusId equals bitstatus.Id
                              join task in db.DrillPlanDetails on auction.JobId equals task.TaskId
                              where auction.BidStatusId == (int)AuBidStatus.Closed && (auction.SRVTenantId == tenantid || auction.WellId == WellId /*&& !checkwellFilter || checkwellFilter*/)
                              select new AuctionBidViewModel
                              {
                                  Subject = auction.Subject,
                                  ProposalId = auction.ProposalId,
                                  JobName = task.TaskName,
                                  BidID = proj.BidID
                              }).Distinct().ToList();
                }
                var resultBidIds = result.Select(x => x.BidID).Distinct().ToArray();
                var result2 = db.AuctionBidAmountHistories.Where(x => resultBidIds.Contains(x.AuctionBidId)).ToList();
                foreach (var item in result)
                {
                    var lastBid = result2.Where(x => x.AuctionBidId == item.BidID).OrderByDescending(x => x.BidDate).FirstOrDefault();
                    item.BidID = lastBid.Id;
                }
                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal GetAuctionsProposalListForInDepath", null);
                return null;
            }
        }

        public List<AuctionBidViewModel> GetAuctionsProposalListForInDepathSRV(string tenantid, string WellId)
        {
            try
            {
                //DWOP - Task data from DrillPlanDetails
                var checkwellFilter = WellId == DLL.Constants.NoSpecificWellFilterKey;
                var result = (from auction in db.AuctionProposals
                              join aucbid in db.AuctionBids on auction.ProposalId equals aucbid.ProposalId
                              join bitstatus in db.AuctionBidStatuses on auction.BidStatusId equals bitstatus.Id
                              join task in db.DrillPlanDetails on auction.JobId equals task.TaskId
                              where auction.BidStatusId == (int)AuBidStatus.Ongoing && aucbid.TenantID == tenantid && (auction.WellId == WellId && !checkwellFilter || checkwellFilter)
                              select new AuctionBidViewModel
                              {
                                  Subject = auction.Subject,
                                  ProposalId = auction.ProposalId,
                                  JobName = task.TaskName,
                                  BidID = aucbid.BidID,
                                  BidStatusName = bitstatus.Name
                              }).Distinct().ToList();
                var resultBidIds = result.Select(x => x.BidID).Distinct().ToArray();
                var result2 = db.AuctionBidAmountHistories.Where(x => resultBidIds.Contains(x.AuctionBidId)).ToList();
                foreach (var item in result)
                {
                    var lastBid = result2.Where(x => x.AuctionBidId == item.BidID).OrderByDescending(x => x.BidDate).FirstOrDefault();
                    item.BidID = lastBid.Id;
                }
                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal GetAuctionsProposalListForInDepathSRV", null);
                return null;
            }
        }
        public async Task<bool> AddAucuctionBidsBySRV(AuctionProposalBidDeatilsViewModel input)
        {
            try
            {
                DateTime timeUtc = DateTime.UtcNow;
                TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);

                var result1 = db.AuctionBids.Find(input.BidID);
                if (result1 != null)
                {
                    result1.AuthorId = input.AuthorId;
                    result1.BidAmount = input.BidAmount;
                    result1.BidStatus = input.BidStatusId;
                    result1.BidSummary = input.BidSummary;
                    result1.ProposalId = input.ProposalId;
                    result1.TenantID = input.TenantID;
                    var bidamountresult = db.AuctionBidAmountHistories.Where(x => x.AuctionBidId == input.BidID && x.BidAmount == input.BidAmount).ToList();
                    if (bidamountresult == null || bidamountresult.Count() <= 0)
                    {
                        AuctionBidAmountHistory amountHistory1 = new AuctionBidAmountHistory
                        {
                            Id = Guid.NewGuid().ToString(),
                            AuctionBidId = input.BidID,
                            AuthorId = input.AuthorId,
                            BidAmount = input.BidAmount,
                            BidDate = cstTime,
                            BidSummary = input.BidSummary
                        };
                        db.AuctionBidAmountHistories.Add(amountHistory1);
                    }
                    db.SaveChanges();
                    return await Task.FromResult(true);
                }
                AuctionBids auctionBids = new AuctionBids
                {
                    AuthorId = input.AuthorId,
                    BidAmount = input.BidAmount,
                    BidDate = cstTime,
                    BidID = Guid.NewGuid().ToString(),
                    BidStatus = input.BidStatusId,
                    BidSummary = input.BidSummary,
                    BidTime = cstTime,
                    ProposalId = input.ProposalId,
                    TenantID = input.TenantID
                };
                AuctionBidAmountHistory amountHistory2 = new AuctionBidAmountHistory
                {
                    Id = Guid.NewGuid().ToString(),
                    AuctionBidId = auctionBids.BidID,
                    AuthorId = input.AuthorId,
                    BidAmount = input.BidAmount,
                    BidDate = cstTime,
                    BidSummary = input.BidSummary
                };
                db.AuctionBidAmountHistories.Add(amountHistory2);
                var result = db.AuctionProposals.Find(input.ProposalId);
                db.AuctionBids.Add(auctionBids);
                result.BidStatusId = (int)AuBidStatus.Ongoing;
                result.ModifyDate = cstTime;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal AddAucuctionBidsBySRV", null);
                return false;
            }
        }

        public List<AuctionBidStatus> GetAuctionsBidStatus()
        {
            try
            {
                var result = db.AuctionBidStatuses.ToList();
                return result.TakeLast(3).ToList();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal GetAuctionsBidStatus", null);
                return null;
            }
        }

        public AuctionProposalBidDeatilsViewModel GetAuctionsProposalForSRVByProposalId(string proposalId, string tenantId)
        {
            try
            {
                var result1 = db.AuctionBids.FirstOrDefault(x => x.ProposalId == proposalId);

                if (result1 != null)
                {
                    var tenUsers = db.TenantUsers.Where(x => x.TenantId == tenantId).ToList();
                    //DWOP - Task data from DrillPlanDetails
                    var result2 = (from auction in db.AuctionProposals
                                   join bitstatus in db.AuctionBidStatuses on auction.BidStatusId equals bitstatus.Id
                                   join corporate in db.CorporateProfile on auction.TenantID equals corporate.TenantId
                                   join t in db.DrillPlanDetails on auction.JobId equals t.TaskId into taskLj
                                   from t in taskLj.DefaultIfEmpty()
                                   join well in db.WELL_REGISTERs on auction.WellId equals well.well_id into welllj
                                   from well in welllj.DefaultIfEmpty()
                                   join rig in db.RigRegisters on well.RigID equals rig.Rig_Id into riglj
                                   from rig in riglj.DefaultIfEmpty()
                                   join aubid in db.AuctionBids on auction.ProposalId equals aubid.ProposalId
                                   join aubh in db.AuctionBidAmountHistories on aubid.BidID equals aubh.AuctionBidId
                                   where auction.ProposalId == proposalId
                                   select new AuctionProposalBidDeatilsViewModel
                                   {
                                       ProposalId = auction.ProposalId,
                                       TenantID = corporate.Name,
                                       Subject = auction.Subject,
                                       AuctionEnd = auction.AuctionEnd,
                                       ProjectStartDate = auction.ProjectStartDate,
                                       ProjectDuration = auction.ProjectDuration,
                                       AuctionStart = auction.AuctionStart,
                                       AuctionBidStatusName = bitstatus.Name,
                                       Body = auction.Body,
                                       Summary = auction.Summary,
                                       Created = auction.Created,
                                       JobId = t == null ? auction.JobId : t.TaskName,
                                       WellName = well == null ? "N/A" : well.wellname,
                                       RigName = rig == null ? "N/A" : rig.Rig_Name,
                                       BidAmount = aubh.BidAmount,
                                       BidID = aubid.BidID,
                                       BidStatusId = aubid.BidStatus,
                                       BidSummary = aubh.BidSummary,
                                       AuctionNumber = auction.AuctionNumber,
                                       AuthorId = aubh.AuthorId,
                                       JobName = t.TaskName,
                                       BidDate = aubh.BidDate
                                   }).OrderByDescending(x => x.BidDate).ToList();

                    var result = result2.FirstOrDefault(x => tenUsers.Any(y => y.UserId == x.AuthorId));

                    return result;
                }
                else
                {
                    var result = (from auction in db.AuctionProposals
                                  join bitstatus in db.AuctionBidStatuses on auction.BidStatusId equals bitstatus.Id
                                  join corporate in db.CorporateProfile on auction.TenantID equals corporate.TenantId
                                  join t in db.DrillPlanDetails on auction.JobId equals t.TaskId
                                  join well in db.WELL_REGISTERs on auction.WellId equals well.well_id
                                  join rig in db.RigRegisters on well.RigID equals rig.Rig_Id
                                  where auction.ProposalId == proposalId
                                  select new AuctionProposalBidDeatilsViewModel
                                  {
                                      ProposalId = auction.ProposalId,
                                      TenantID = corporate.Name,
                                      Subject = auction.Subject,
                                      Summary = auction.Summary,
                                      AuctionEnd = auction.AuctionEnd,
                                      ProjectStartDate = auction.ProjectStartDate,
                                      ProjectDuration = auction.ProjectDuration,
                                      AuctionStart = auction.AuctionStart,
                                      AuctionBidStatusName = bitstatus.Name,
                                      Body = auction.Body,
                                      Created = auction.Created,
                                      JobId = t == null ? auction.JobId : t.TaskName,
                                      WellName = well == null ? "N/A" : well.wellname,
                                      RigName = rig == null ? "N/A" : rig.Rig_Name,
                                      AuctionNumber = auction.AuctionNumber
                                  }).FirstOrDefault();
                    return result;
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal GetAuctionsProposalForSRVByProposalId", null);
                return null;
            }
        }

        public List<AuctionBidAmountHistoryViewModel> GetAuctionBidAmountHistoryByAuctionBidId(string auctionBidId, string tenantId)
        {
            try
            {
                var result = (from aba in db.AuctionBidAmountHistories
                              join user in _userManager.Users on aba.AuthorId equals user.Id
                              where aba.AuctionBidId == auctionBidId && user.TenantId == tenantId
                              select new AuctionBidAmountHistoryViewModel
                              {
                                  Id = aba.Id,
                                  AuthorId = user.FirstName,
                                  BidAmount = aba.BidAmount,
                                  BidDate = aba.BidDate,
                                  BidSummary = aba.BidSummary
                              }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal GetAuctionBidAmountHistoryByAuctionBidId", null);
                return null;
            }
        }

        public Task<List<DLL.ServiceEntity.Projects>> GetServiceCompanyAuctionProjects(string serviceTenantId, string operTenantId, bool isActive, string wellId)
        {
            try
            {
                var checkwellFilter = wellId == DLL.Constants.NoSpecificWellFilterKey;

                var result = db.Projects.Where(x => x.ServiceCompID == serviceTenantId && x.OprTenantID == operTenantId &&
                                                ((x.ProjectStatus == 1 && isActive) || (x.ProjectStatus == 0 && !isActive)) &&
                                                (x.WellID == wellId && !checkwellFilter || checkwellFilter)).ToList();
                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal GetServiceCompanyAuctionProjects", null);
                return null;
            }
        }

        public Task<List<DLL.ServiceEntity.Projects>> GetServiceCompanyAuctionProjects(string operTenantId, string serviceTenantId, bool isActive)
        {
            try
            {
                var result = db.Projects.Where(x => x.ServiceCompID == serviceTenantId && x.OprTenantID == operTenantId &&
                                            ((x.ProjectStatus == 1 && isActive) || (x.ProjectStatus == 0 && !isActive))).ToList();
                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal GetServiceCompanyAuctionProjects", null);
                return null;
            }
        }

        public Task<List<Model.OperatingCompany.Models.ActivityViewModel>> GetActivityViewAwardedProjects(string operTenantId)
        {
            try
            {
                var result = (from project in db.Projects
                              join ap in db.AuctionProposals on project.ProposalID equals ap.ProposalId
                              join ab in db.AuctionBids on project.BidID equals ab.BidID
                              where (int)ab.BidStatus == (int)AuBidStatus.Accepted && project.OprTenantID == operTenantId && project.ProposedStartDate.HasValue
                              select new Model.OperatingCompany.Models.ActivityViewModel
                              {
                                  ProjectId = project.ID,
                                  Start = project.ProposedStartDate.Value,
                                  Title = project.ProjectTitle,
                                  Description = project.ProjectSummary,
                                  End = project.ProposedStartDate.Value.AddHours(ap.ProjectDuration)
                              }).ToList();

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal GetActivityViewAwardedProjects", null);
                return null;
            }
        }
        public Task<List<Model.OperatingCompany.Models.ProjectAuctionModel>> GetServiceCompanyActualProposals(string serviceTenantId, string operTenantId, string wellId)
        {
            try
            {
                var result = new List<Model.OperatingCompany.Models.ProjectAuctionModel>();

                //DWOP - Task data from DrillPlanDetails
                var checkwellFilter = wellId == DLL.Constants.NoSpecificWellFilterKey;

                result = (from proposal in db.AuctionProposals
                          join bid in db.AuctionBids on proposal.ProposalId equals bid.ProposalId
                          join attach in db.AuctionProposalAttachments on proposal.ProposalId equals attach.ProposalID
                          join aucstatus in db.AuctionBidStatuses on bid.BidStatus equals aucstatus.Id
                          join rig in db.rig_register on proposal.RigId equals rig.Rig_id
                          join well in db.WellRegister on proposal.WellId equals well.well_id
                          join tas in db.DrillPlanDetails on proposal.JobId equals tas.TaskId
                          where proposal.TenantID == operTenantId && bid.TenantID == serviceTenantId && attach.TenantID == bid.TenantID
                              && (proposal.WellId == wellId && !checkwellFilter || checkwellFilter)
                          select new Model.OperatingCompany.Models.ProjectAuctionModel
                          {
                              AuctionID = bid.ProposalId,
                              Name = rig.Rig_Name + "-" + well.wellname + ":" + tas.TaskName,
                              Status = aucstatus.Name,
                              Attachment = attach.FileName,
                              Location = attach.AttachmentId,
                              Description = attach.TenantID,
                              OpenDate = proposal.Created
                          }).ToList();

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal GetServiceCompanyActualProposals", null);
                return null;
            }
        }

        public Task<AuctionProposalAttachments> GetServiceCompanyAttachment(string serviceTenantId, string fileId)
        {
            try
            {
                var result = db.AuctionProposalAttachments.FirstOrDefault(x => x.TenantID == serviceTenantId && x.AttachmentId == fileId);

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal GetServiceCompanyAttachment", null);
                return null;
            }
        }


        public List<Model.ServiceCompany.Models.ActivityViewModel> GetProjectActivityService(string tenantid,string operId)
        {
            try
            {
                //&& task.IsCalendar==true - IsCalendar handled to filter only Calendar/Scheduler specific item
                var opernocheck = operId == DLL.Constants.NoSpecificWellFilterKey;
                var result = (from project in db.Projects
                              join ap in db.AuctionProposals on project.ProposalID equals ap.ProposalId
                              join task in db.Tasks on ap.JobId equals task.TaskId
                              where project.ServiceCompID == tenantid && (ap.TenantID == operId && !opernocheck || opernocheck)
                              && task.IsCalendar==true
                              select new Model.ServiceCompany.Models.ActivityViewModel
                              {
                                  ProjectId = project.ID,
                                  Title = string.IsNullOrEmpty(project.ProjectTitle) ? "No title" : project.ProjectTitle,
                                  Start = project.ActualStart == null ? ap.ProjectStartDate : project.ActualStart.Value,
                                  End = project.ActualEnd == null ? Convert.ToDateTime(ap.AuctionEnd).AddHours((double)ap.ProjectDuration) : project.ActualEnd.Value,
                                  IsAllDay = false,
                                  Description = project.ProjectSummary,
                                  ProjectStatus = project.ProjectStatus,
                                  ProjectStatusName = project.ProjectStatus == 0 ? "Upcoming" : "Ongoing"
                              }).ToList();
                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal GetProjectActivityService", null);
                return null;
            }
        }

        public List<Model.ServiceCompany.Models.ActivityViewModel> GetProjectActivityServiceWithRigAndWellId(string tenantid, string operId, string rigId, string wellId)
        {
            try
            {
                //&& task.IsCalendar==true - IsCalendar handled to filter only Calendar/Scheduler specific item
                var opernocheck = operId == DLL.Constants.NoSpecificWellFilterKey;

                var checkrigFilter = rigId == DLL.Constants.NoSpecificWellFilterKey;
                var checkwellFilter = wellId == DLL.Constants.NoSpecificWellFilterKey;

                var result = (from project in db.Projects
                              join ap in db.AuctionProposals on project.ProposalID equals ap.ProposalId
                              join task in db.Tasks on ap.JobId equals task.TaskId
                              where project.ServiceCompID == tenantid && (ap.TenantID == operId && !opernocheck || opernocheck)
                              && (ap.RigId == rigId && !checkrigFilter || checkrigFilter)
                              && (ap.WellId == wellId && !checkwellFilter || checkwellFilter)
                              && task.IsCalendar == true
                              select new Model.ServiceCompany.Models.ActivityViewModel
                              {
                                  ProjectId = project.ID,
                                  Title = string.IsNullOrEmpty(project.ProjectTitle) ? "No title" : project.ProjectTitle,
                                  Start = project.ActualStart == null ? ap.ProjectStartDate : project.ActualStart.Value,
                                  End = project.ActualEnd == null ? Convert.ToDateTime(ap.AuctionEnd).AddHours((double)ap.ProjectDuration) : project.ActualEnd.Value,
                                  IsAllDay = false,
                                  Description = project.ProjectSummary,
                                  ProjectStatus = project.ProjectStatus,
                                  ProjectStatusName = project.ProjectStatus == 0 ? "Upcoming" : "Ongoing"
                              }).ToList();
                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal GetProjectActivityService", null);
                return null;
            }
        }

        public async Task<List<AuctionProposalAttachmentOpeViewModel>> GetAuctionProposalOperatorAttachmentByProposalId(string proposalId)
        {
            try
            {
                var result = await db.AuctionProposalOperatorAttachments.Where(x => x.ProposalID.Equals(proposalId))
                .Select(x => new AuctionProposalAttachmentOpeViewModel()
                {
                    AttachmentId = x.AttachmentId,
                    FileName = x.FileName,
                    ProposalId = x.ProposalID
                }).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal GetAuctionProposalOperatorAttachmentByProposalId", null);
                return null;
            }
        }

        public Task<List<Model.ServiceCompany.Models.ProjectAuctionModel>> GetOperatingCompanyActualProposals(string operTenantId, string serviceTenantId)
        {
            try
            {
                var result = new List<Model.ServiceCompany.Models.ProjectAuctionModel>();

                var proposals = db.AuctionProposals.Where(x => x.TenantID == operTenantId).ToList();
                var auctionStatuses = db.AuctionBidStatuses.ToList();

                foreach (var proposal in proposals)
                {
                    var bid = db.AuctionBids.FirstOrDefault(x => x.TenantID == serviceTenantId && x.ProposalId == proposal.ProposalId);
                    //DWOP - Task data from DrillPlanDetails
                    var ProposalName = (from p in proposals
                                        join rig in db.rig_register on p.RigId equals rig.Rig_id
                                        join well in db.WellRegister on p.WellId equals well.well_id
                                        join tas in db.DrillPlanDetails on p.JobId equals tas.TaskId
                                        where p.ProposalId == proposal.ProposalId
                                        select new { p, rig, well, tas }).FirstOrDefault();

                    if (bid != null && ProposalName != null)
                    {
                        var statusTitle = auctionStatuses.FirstOrDefault(x => x.Id == bid.BidStatus);

                        var attachment = db.AuctionProposalAttachments.FirstOrDefault(x => x.ProposalID == proposal.ProposalId && x.TenantID == bid.TenantID);
                        result.Add(new Model.ServiceCompany.Models.ProjectAuctionModel
                        {
                            AuctionID = bid.ProposalId,
                            Name = proposal == null ? "" : ProposalName.rig.Rig_Name + "-" + ProposalName.well.wellname + ":" + ProposalName.tas.TaskName,
                            Status = statusTitle == null ? "" : statusTitle.Name,
                            Attachment = attachment == null ? "" : attachment.FileName,
                            Location = attachment == null ? "" : attachment.AttachmentId,
                            Description = attachment == null ? "" : attachment.TenantID,
                            OpenDate = proposal.Created
                        });
                    }
                }
                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal GetOperatingCompanyActualProposals", null);
                return null;
            }
        }

        public Task<List<DLL.ServiceEntity.Projects>> GetOperatingCompanyAuctionProjects(string OperatingTenantId, string ServiceTenantId, bool isActive)
        {
            try
            {
                var result = db.Projects.Where(x => x.OprTenantID == OperatingTenantId && x.ServiceCompID == ServiceTenantId &&
                                            ((x.ProjectStatus == 1 && isActive) || (x.ProjectStatus == 0 && !isActive))).ToList();
                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal GetOperatingCompanyAuctionProjects", null);
                return null;
            }
        }

        public async Task<List<Tasks>> GetTaskForJob()
        {
            try
            {
                //DWOP - Task data from DrillPlanDetails//t.IsActive == true && 
                var result = await (from t in db.DrillPlanDetails
                                    where t.IsBiddable == true
                                    select new Tasks
                                    {
                                        TaskId = t.TaskId,
                                        Name = t.TaskName
                                    }).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal GetTaskForJob", null);
                return null;
            }
        }

        public async Task<List<ServiceCategory>> GetServiceCategorys()
        {
            try
            {
                var result = await (from Scategory in db.serviceCategories
                                    where Scategory.IsActive == true && Scategory.ServiceCategoryId == Scategory.ParentId
                                    select new ServiceCategory
                                    {
                                        ServiceCategoryId = Scategory.ServiceCategoryId,
                                        Name = Scategory.Name
                                    }).OrderBy(x => x.Name).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal GetServiceCategorys", null);
                return null;
            }
        }
        public AuctionProposal GetUpcomingClosingBids()
        {
            try
            {
                DbFunctions dfunc = null;
                DateTime dateTime = DateTime.Now;
                var result = db.AuctionProposals.Where(x => SqlServerDbFunctionsExtensions.DateDiffMinute(dfunc, x.AuctionEnd, dateTime) <= 60 && SqlServerDbFunctionsExtensions.DateDiffDay(dfunc, x.AuctionEnd, dateTime) == 0).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal GetUpcomingClosingBids", null);
                return null;
            }
        }
        
    }
}