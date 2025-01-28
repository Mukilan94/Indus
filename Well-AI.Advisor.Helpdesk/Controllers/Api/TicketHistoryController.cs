using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using src.Data;
using src.Models;
using src.Services;
using Well_AI.Helpdesk.Models;
using Well_AI.Helpdesk.Models.ManageViewModels;

namespace src.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/TicketHistory")]
    [Authorize]
    public class TicketHistoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly ILogger _logger;
        public TicketHistoryController(ApplicationDbContext context, IEmailSender emailSender, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _emailSender = emailSender;
            _userManager = userManager;
        }


        [HttpPost]
        public async Task<IActionResult> PostTicket(TicketHistoryViewModel tickethistoryView, string submit)
        {
            var applicationUserId = _userManager.GetUserId(User);
            ApplicationUser users = _context.ApplicationUser.Where(x => x.Id.Equals(applicationUserId)).FirstOrDefault();

            Organization organization = _context.Organization.Where(x => x.organizationOwnerId.Equals(applicationUserId)).FirstOrDefault();
            ViewData["org"] = organization.organizationId;
            hdIssues hdissue = _context.hdIssues.Where(x => x.issueID.Equals(tickethistoryView.issueId)).FirstOrDefault();
            hdComments ticketHistory = new hdComments();
            if (submit == "Cancel")
            {
                return RedirectToAction("Customer", "Ticket", new { customerId = hdissue.TenantId });
            }
            try
            {        
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                ticketHistory.IssueID = tickethistoryView.issueId;
                ticketHistory.Body = tickethistoryView.replyMessage;
                ticketHistory.Recipients = tickethistoryView.replyBy;
                ticketHistory.CommentDate = tickethistoryView.replyDate;
                ticketHistory.UserID = applicationUserId;
               _context.hdComments.Add(ticketHistory);
               await _context.SaveChangesAsync();
            }
                if (submit == "Close")
            {
                    hdissue.StatusID = 5;
            }
            else
            {
                    hdissue.StatusID = 4;
            }

                hdissue.AssignedToUserID = hdissue.AssignedToUserID;
                hdissue.LastUpdated= tickethistoryView.replyDate;
                hdissue.IssueDate = hdissue.IssueDate;
                hdissue.TenantId = hdissue.TenantId;
                hdissue.UserID = hdissue.UserID;
                _context.Update(hdissue);
                _context.SaveChanges();
                return RedirectToAction("Customer", "Ticket", new { customerId = hdissue.TenantId });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }


}