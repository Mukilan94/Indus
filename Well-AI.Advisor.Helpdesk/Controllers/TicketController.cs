using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using src.Data;
using src.Models;
using Well_AI.Advisor.Helpdesk.Models;
using Well_AI.Helpdesk.Models;
using Well_AI.Helpdesk.Models.ManageViewModels;

namespace src.Controllers
{
    public class TicketController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TicketController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index(Guid org)
        {
            if (org == Guid.Empty)
            {
                return NotFound();
            }
            Organization organization = _context.Organization.Where(x => x.organizationId.Equals(org)).FirstOrDefault();
            ViewData["org"] = org;
            return View(organization);
        }

        public IActionResult Customer(Guid customerId)
        {
            if (customerId == Guid.Empty)
            {
                return NotFound();
            }
            Customer customer = _context.Customer.Where(x => x.customerId.Equals(customerId)).FirstOrDefault();
            CorporateProfile corporateProfile = _context.CorporateProfile.Where(X => X.customerId.Equals(customerId)).FirstOrDefault();
            ViewData["cust"] = customerId;
            var applicationUserId = _userManager.GetUserId(User);
            ApplicationUser users = _context.ApplicationUser.Where(x => x.Id.Equals(applicationUserId)).FirstOrDefault();
            Organization organization = _context.Organization.Where(x => x.organizationOwnerId.Equals(applicationUserId)).FirstOrDefault();
            ViewData["org"] = organization.organizationId;
            return View(corporateProfile);
        }

        public IActionResult AddEdit(Guid org, Guid id)
        {
            if (id == Guid.Empty)
            {
                Ticket ticket = new Ticket();
                ticket.organizationId = org;

                IList<Product> products = _context.Product.Where(x => x.organizationId.Equals(org)).ToList();
                ViewBag.productId = new SelectList(products, "productId", "productName");

                IList<SupportAgent> agents = _context.SupportAgent.Where(x => x.organizationId.Equals(org)).ToList();
                ViewBag.supportAgentId = new SelectList(agents, "supportAgentId", "supportAgentName");

                IList<SupportEngineer> engineers = _context.SupportEngineer.Where(x => x.organizationId.Equals(org)).ToList();
                ViewBag.supportEngineerId = new SelectList(engineers, "supportEngineerId", "supportEngineerName");

                IList<Contact> contacts = _context.Contact
                    .Where(x => x.customer.organizationId.Equals(org)).ToList();
                ViewBag.contactId = new SelectList(contacts, "contactId", "contactName");

                return View(ticket);
            }
            else
            {
                Ticket ticket = _context.Ticket.Where(x => x.ticketId.Equals(id)).FirstOrDefault();

                IList<Product> products = _context.Product.Where(x => x.organizationId.Equals(ticket.organizationId)).ToList();
                ViewBag.productId = new SelectList(products, "productId", "productName", ticket.productId);

                IList<SupportAgent> agents = _context.SupportAgent.Where(x => x.organizationId.Equals(ticket.organizationId)).ToList();
                ViewBag.supportAgentId = new SelectList(agents, "supportAgentId", "supportAgentName", ticket.supportAgentId);

                IList<SupportEngineer> engineers = _context.SupportEngineer.Where(x => x.organizationId.Equals(ticket.organizationId)).ToList();
                ViewBag.supportEngineerId = new SelectList(engineers, "supportEngineerId", "supportEngineerName", ticket.supportEngineerId);

                IList<Contact> contacts = _context.Contact
                    .Where(x => x.customer.organizationId.Equals(ticket.organizationId)).ToList();
                ViewBag.contactId = new SelectList(contacts, "contactId", "contactName", ticket.contactId);

                return View(ticket);
            }

        }

        public IActionResult AddEditCustomerTicket(Guid cust, Guid id)
        {
            if (id == Guid.Empty)
            {
                Customer customer = _context.Customer.Where(x => x.customerId.Equals(cust)).FirstOrDefault();
                var applicationUserId = _userManager.GetUserId(User);
                Contact contact = _context.Contact.Where(x => x.applicationUserId.Equals(applicationUserId)).FirstOrDefault();
                Ticket ticket = new Ticket();
                ticket.customerId = cust;
                ticket.organizationId = customer.organizationId;
                ticket.contactId = contact.contactId;

                IList<Product> products = _context.Product.Where(x => x.organizationId.Equals(customer.organizationId)).ToList();
                ViewBag.productId = new SelectList(products, "productId", "productName");

                return View(ticket);
            }
            else
            {
                Ticket ticket = _context.Ticket.Where(x => x.ticketId.Equals(id)).FirstOrDefault();

                IList<Product> products = _context.Product.Where(x => x.organizationId.Equals(ticket.organizationId)).ToList();
                ViewBag.productId = new SelectList(products, "productId", "productName", ticket.productId);

                return View(ticket);
            }

        }
        public IActionResult ViewTicket(Guid org, int id)
        {
            List<TicketHistoryViewModel> historyList = new List<TicketHistoryViewModel>();
            if (id != 0 )
            {
               
                try
                {
                    hdIssues ticket = _context.hdIssues.Where(x => x.issueID.Equals(id)).FirstOrDefault();
                    var applicationUserId = _userManager.GetUserId(User);
                    ApplicationUser users = _context.ApplicationUser.Where(x => x.Id.Equals(applicationUserId)).FirstOrDefault();
                    CorporateProfile cust = _context.CorporateProfile.Where(x => x.customerId.Equals(ticket.TenantId)).FirstOrDefault();
                    hdStaffs Recipients = _context.hdStaffs.Where(x => x.Id.Equals(cust.UserId)).FirstOrDefault();
                    IList<hdComments> history = _context.hdComments.Where(x => x.IssueID.Equals(id)).ToList().OrderByDescending(p => p.CommentDate).ToList();
                    CorporateProfile userList = _context.CorporateProfile.Where(x => x.customerId.Equals(ticket.TenantId)).FirstOrDefault();
                    IList<hdIssues> hdIssues = _context.hdIssues.ToList();
                    IList<hdStatus> hdStatus = _context.hdStatus.ToList();
                    IList<hdStaffs> RecipientsList = _context.hdStaffs.ToList();

                    var list = (from hist in history
                                join user in RecipientsList on hist.UserID equals user.Id 
                                join issue in hdIssues on hist.IssueID equals issue.issueID 
                                join sts in hdStatus on issue.StatusID equals sts.StatusID into dept
                                from d in dept.DefaultIfEmpty()
                                select new
                                {
                                    CommentID = hist.CommentID,
                                    IssueID = hist.IssueID,
                                    UserID = hist.UserID,
                                    subject = issue.subject,
                                    body = issue.body,
                                    CommentDate = hist.CommentDate,
                                    Username= user.UserName,
                                    comment =hist.Body,
                                    stauts = d.status
                                }).ToList();


                    if (history.Count > 0)
                    {
                        TicketHistoryViewModel tickethistory = new TicketHistoryViewModel();
                        foreach (var item in list)
                        {
                            tickethistory = new TicketHistoryViewModel();
                            tickethistory.replyBy = Recipients.Email;
                            tickethistory.replyDate = item.CommentDate;
                            tickethistory.ticketDescription = item.body;
                            tickethistory.issueId = item.IssueID;
                            tickethistory.ticketTitle = item.subject;
                            tickethistory.transactionId = item.UserID;
                            tickethistory.userId = applicationUserId;
                            tickethistory.userName = item.Username;
                            tickethistory.replyMessage = item.comment;
                            tickethistory.ticketStatus = item.stauts;
                            tickethistory.closedBy = applicationUserId;
                            tickethistory.custName = cust.name;
                            historyList.Add(tickethistory);
                        }

                        tickethistory = new TicketHistoryViewModel();
                        tickethistory.replyBy = Recipients.Email;
                        tickethistory.replyDate = ticket.IssueDate;
                        tickethistory.ticketDescription = ticket.body;
                        tickethistory.issueId = ticket.issueID;
                        tickethistory.ticketTitle = ticket.subject;
                        tickethistory.userId = applicationUserId;
                        tickethistory.userName = Recipients.UserName;
                        tickethistory.replyMessage = ticket.body;
                        tickethistory.ticketStatus ="open";
                        tickethistory.closedBy = applicationUserId;
                        tickethistory.custName = cust.name;
                        historyList.Add(tickethistory);

                    }
                    else
                    {
                        TicketHistoryViewModel tickethistory = new TicketHistoryViewModel();
                        tickethistory.replyBy = Recipients.Email;
                        tickethistory.replyDate = System.DateTime.Now;
                        tickethistory.ticketDescription = ticket.body;
                        tickethistory.issueId = ticket.issueID;
                        tickethistory.ticketTitle = ticket.subject;
                        tickethistory.transactionId = "";
                        tickethistory.userId = applicationUserId;
                        tickethistory.userName = "";
                        tickethistory.replyMessage = cust.name;
                        tickethistory.ticketStatus = "open";
                        historyList.Add(tickethistory);
                    }
                }
                catch (Exception ex)
                {
                    string message = ex.InnerException.ToString();
                    throw;
                }        
            }
            return View(historyList);
        }
    }
}