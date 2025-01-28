using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellAI.Advisor.BLL.Administration;
using WellAI.Advisor.BLL.IBusiness;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Common;
using WellAI.Advisor.Model.Identity;

namespace WellAI.Advisor.BLL.Business
{
    public class SenderAndReceiverBusiness : ISenderAndReceiverBusiness
    {
        private readonly WebAIAdvisorContext db;
        UserManager<WellIdentityUser> _userManager;
        public SenderAndReceiverBusiness(WebAIAdvisorContext db, UserManager<WellIdentityUser> userManager) 
        {
            this.db = db;
            _userManager = userManager;
        }

        public Task<MessageToQueue> GetSendOnAuctionRequestforOperator(string senderAuthorId, string SRVTenantId,string receiverProposalId,decimal  AMOUNT,string Summary)
        {
            try
            {
                var sender = _userManager.FindByIdAsync(senderAuthorId).Result;
                var auctionProposals = db.AuctionProposals.Where(x => x.ProposalId.Equals(receiverProposalId)).FirstOrDefault();
                var corporate = db.CorporateProfile.Where(x => x.TenantId.Equals(SRVTenantId)).FirstOrDefault();
                var receiver = _userManager.FindByIdAsync(auctionProposals.AuthorId).Result;
                string BIDDERID = $"{sender.FirstName} {sender.LastName}";
                var emailTemplate = db.EmailTemplates.Where(x => x.EventName.Equals("AuctionRequested")).FirstOrDefault();
                emailTemplate.Subject = emailTemplate.Subject.Replace("[AUCTIONID]", auctionProposals.AuctionNumber);
                string Body = emailTemplate.Body.Replace("[COMPANY]", corporate.Name).Replace("[AUCTIONCODE]", auctionProposals.AuctionNumber)
                                                        .Replace("[SUMMARY]", Summary).Replace("[DATE]", DateTime.Now.ToShortDateString())
                                                        .Replace("[BIDDERID]", BIDDERID).Replace("[AMOUNT]", AMOUNT.ToString("C", CultureInfo.GetCultureInfo("en-US")))
                                                        .Replace("[FIRSTNAME]", receiver.FirstName);
                MessageToQueue messageToQueue = new MessageToQueue()
                {
                    FromEmail = sender.Email,
                    FromName = sender.FirstName,
                    ToEmail = receiver.Email,
                    ToName = receiver.FirstName,
                    MsgSubject = emailTemplate.Subject,
                    MsgBody = Body
                };
                return Task.FromResult(messageToQueue);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "SenderAndReceive GetSendOnAuctionRequestforOperator", null);
                return null;
            }
        }

        public Task<MessageToQueue> GetSendOnAuctionsBidAcceptedRejectedForService(string senderAuthorId, string OperTenantId, string ProposalId, string projectId, string auctionStatus,string BidId)
        {
            try
            {
                var sender = _userManager.FindByIdAsync(senderAuthorId).Result;
                var auctionProposals = db.AuctionProposals.Where(x => x.ProposalId.Equals(ProposalId)).FirstOrDefault();
                var auctionBids = db.AuctionBids.Where(x => x.BidID == BidId).FirstOrDefault();
                var corporate = db.CorporateProfile.Where(x => x.TenantId.Equals(OperTenantId)).FirstOrDefault();
                var receiver = _userManager.FindByIdAsync(auctionBids.AuthorId).Result;
                string SENDERNAME = $"{sender.FirstName} {sender.LastName}";
                MessageToQueue messageToQueue = new MessageToQueue();
                var emailTemplate = db.EmailTemplates.Where(x => x.EventName.Equals(auctionStatus + "ed")).FirstOrDefault();
                if (emailTemplate != null)
                {
                    if (auctionStatus == "Reject" || auctionStatus == null)
                    {
                        emailTemplate.Subject = emailTemplate.Subject.Replace("[AUCTIONID]", auctionProposals.AuctionNumber);

                    }
                    else
                    {
                        emailTemplate.Subject = emailTemplate.Subject.Replace("[PROJECTID]", auctionProposals.AuctionNumber);
                        emailTemplate.Body = emailTemplate.Body.Replace("[PROJECTID]", auctionProposals.AuctionNumber);

                    }
                    string Body = emailTemplate.Body.Replace("[COMPANY]", corporate.Name).Replace("[AUCTIONCODE]", auctionProposals.AuctionNumber)
                                                             .Replace("[DATE]", DateTime.Now.ToShortDateString())
                                                            .Replace("[SENDERNAME]", SENDERNAME)
                                                            .Replace("[FIRSTNAME]", receiver.FirstName);
                    messageToQueue = new MessageToQueue()
                    {
                        FromEmail = sender.Email,
                        FromName = sender.FirstName,
                        ToEmail = receiver.Email,
                        ToName = receiver.FirstName,
                        MsgSubject = emailTemplate.Subject,
                        MsgBody = Body
                    };
                }

                return Task.FromResult(messageToQueue);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "SenderAndReceive GetSendOnAuctionsBidAcceptedRejectedForService", null);
                return null;
            }
        }
    }
}
