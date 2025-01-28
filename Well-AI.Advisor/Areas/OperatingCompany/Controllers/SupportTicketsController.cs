using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Data;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.Model.Identity;
using System;
using System.Security.Claims;
using WellAI.Advisor.DLL.ServiceEntity;
using System.Threading.Tasks;
using System.Data.SqlClient;
using WellAI.Advisor.DLL.Data;
using Finbuckle.MultiTenant;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.Areas.Identity;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.Helper;
using Microsoft.AspNetCore.Http;
using WellAI.Advisor.DLL.Entity;
using Microsoft.AspNetCore.SignalR;
using WellAI.Advisor.Hubs;
using WellAI.Advisor.Model.Common;

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("OperatingCompany")]
    [SessionTimeOut]
    public class SupportTicketsController : BaseController
    {
        private readonly ILogger<SupportTicketsController> _logger;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly WebAIAdvisorContext _context;
        private  IHubContext<NotificationHub> _hubContext { get; set; }
        public SupportTicketsController(WebAIAdvisorContext context, SignInManager<WellIdentityUser> signInManager, ILogger<SupportTicketsController> logger,
            UserManager<WellIdentityUser> userManager, RoleManager<IdentityRole> roleManager, IHubContext<NotificationHub> hubContext)
            : base(userManager, context)
        {
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _hubContext = hubContext;
        }
        public IActionResult Index()
        {
            try
            {
                if (_signInManager.IsSignedIn(User) == false)
                {
                    string returnUrl = @"/Identity/Account/Login";
                    return LocalRedirect(returnUrl);
                }
                var statuslist = _context.hdStatus.ToList();
                ViewData["hdstatus"] = statuslist;
                ViewData["defaultstatus"] = statuslist.First();
                return View(CountTickets());
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _context, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "SupportTickets Index", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public IActionResult TicketDetail(string id)
        {
            try
            {
                int ticketID = Convert.ToInt32(id);
                var tickets = Getticket(ticketID);
                return View(tickets);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _context, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "SupportTickets TicketDetail", User.Identity.Name);
              _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public IActionResult NewTicket()
        {
            try
            {
                var newsts = _context.hdStatus.Where(x => x.Name == "New").FirstOrDefault();
                List<hdCategories> categories = new List<hdCategories>();
                categories = (from product in _context.hdCategories select product).ToList();
                categories.Insert(0, new hdCategories { CategoryID = 0, Name = "--Select Department--" });
                ViewBag.category = categories;
                var ticket = new TicketHistoryItem
                {
                    CreateDate = System.DateTime.Now,
                    LastActivity = System.DateTime.Now,
                    Status = newsts.Name,
                    StatusID = newsts.StatusID
                };
                return View(ticket);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _context, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "SupportTickets NewTicket", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [HttpPost]
        public async Task<IActionResult> SaveNewTicket(TicketHistoryItem tc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
                    var userName = User.FindFirstValue(ClaimTypes.Name); // will give the user's userName                               
                    hdIssues issue = new hdIssues();
                    if (ModelState.IsValid)
                    {
                        issue.UserID = userId;
                        issue.CategoryID = Convert.ToInt32(tc.CategoryID);
                        issue.StatusID = Convert.ToInt32(tc.StatusID);
                        issue.Subject = tc.HelpTopic;
                        issue.Body = tc.ReplyMessage;
                        issue.IssueDate = DateTime.Now;
                        issue.LastUpdated = DateTime.Now;
                        var ti = HttpContext.GetMultiTenantContext().TenantInfo;
                        if (ti != null)
                        {
                            issue.TenantId = ti.Id;
                        }
                    };
                    _context.hdIssues.Add(issue);
                    await _context.SaveChangesAsync();
                   ViewBag.Save = "";
                }
               return RedirectToAction("Index");
            }
            catch (SqlException ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _context, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
               customErrorHandler.WriteError(ex, "SupportTickets SaveTicket", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [HttpPost]
        public async Task<IActionResult> SaveTicketDetail(TicketHistoryItem td, string submit, string closeticket)
        {
            try
            {
                if (submit == "Submit")
                {
                    var statusid = _context.hdStatus.Where(x => x.Name == "New").FirstOrDefault();
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var userName = User.FindFirstValue(ClaimTypes.Name);
                    hdComments cmt = new hdComments();
                    if (ModelState.IsValid)
                    {
                        cmt.IssueID = Convert.ToInt32(td.TicketId);
                        cmt.CommentDate = DateTime.Now;
                        cmt.UserID = userId;
                        cmt.Body = td.LeaveReplay;
                        cmt.ForTechsOnly = Convert.ToBoolean(1);
                        cmt.IsSystem = Convert.ToBoolean(0);
                        cmt.Recipients = userName;
                    };
                    _context.hdComments.Add(cmt);
                    await _context.SaveChangesAsync();
                    var updatedate = _context.hdIssues.Single(date => date.IssueID == cmt.IssueID);
                    updatedate.LastUpdated = cmt.CommentDate;
                    if (td.Status == "New")
                    {
                        updatedate.StatusID = statusid.StatusID + 1;
                    }
                    await _context.SaveChangesAsync();                  
                   await _hubContext.Clients.All.SendAsync("updateNotification").ConfigureAwait(true);
                    return RedirectToAction("TicketDetail", new { id = td.TicketId });
                }
                else if (closeticket == "Close Ticket")
                {
                    var statusOpen = _context.hdStatus.Where(x => x.Name == "Open").FirstOrDefault();
                    var statusNew = _context.hdStatus.Where(x => x.Name == "New").FirstOrDefault();
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var userName = User.FindFirstValue(ClaimTypes.Name);
                    hdComments cmt = new hdComments();
                    if (ModelState.IsValid)
                    {
                        cmt.IssueID = Convert.ToInt32(td.TicketId);
                        cmt.CommentDate = DateTime.Now;
                        cmt.UserID = userId;
                        cmt.Body = td.LeaveReplay;
                        cmt.ForTechsOnly = Convert.ToBoolean(1);
                        cmt.IsSystem = Convert.ToBoolean(0);
                        cmt.Recipients = userName;
                    };
                    _context.hdComments.Add(cmt);
                    await _context.SaveChangesAsync();
                    var updatedate = _context.hdIssues.Single(date => date.IssueID == cmt.IssueID);
                    updatedate.LastUpdated = cmt.CommentDate;
                    if (td.Status == "Open")
                    {
                        updatedate.StatusID = statusOpen.StatusID + 1;
                    }
                    if (td.Status == "New")
                    {
                        updatedate.StatusID = statusNew.StatusID + 2;
                    }
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _context, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "SupportTickets SaveTicketDetail", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public List<Comment> GetAllCommentsForTicket(TicketHistoryItem ticket)
        {
            List<Comment> cmtsrvlist = new List<Comment>();
            try
            {
                ICommonBusiness commonBusiness = new CommonBusiness(_context, _roleManager, _userManager);
                var user = commonBusiness.GetUser(ticket.UserID);
                int id = Convert.ToInt32(ticket.TicketId);
                var cmtlist = (from objcmt in _context.hdComments.Where(x => x.IssueID.Equals(id))
                               select new Comment()
                               {
                                   Id = objcmt.CommentID,
                                   Content = objcmt.Body,
                                   TicketId = Convert.ToString(objcmt.IssueID),
                                   Author = objcmt.Recipients,
                                   Date = objcmt.CommentDate
                               }).ToList();
                cmtsrvlist.Add(new Comment
                {
                    Author = user.Result.FirstName + " " + (user.Result.MiddleName == "" ? "" : " " + user.Result.LastName),
                    Content = ticket.ReplyMessage,
                    Date = ticket.CreateDate.Value,
                    TicketId = ticket.TicketId
                });
                if (cmtlist.Count > 0)
                {
                    Comment cmtsrv = new Comment();
                    foreach (var item in cmtlist)
                    {
                        cmtsrv = new Comment();
                        cmtsrv.Id = item.Id;
                        cmtsrv.Content = item.Content;
                        cmtsrv.TicketId = item.TicketId;
                        cmtsrv.Author = item.Author;
                        cmtsrv.Date = item.Date;
                        cmtsrvlist.Add(cmtsrv);
                    }
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _context, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "SupportTickets GetAllCommentsForTicket", User.Identity.Name);
            }
            return cmtsrvlist;
        }
        public TicketHistoryItem Getticket(int id)
        {
            TicketHistoryItem detail = new TicketHistoryItem();
            try
            {
                var comments = new List<Comment>();
                var userName = User.FindFirstValue(ClaimTypes.Name);
                detail = (from issue in _context.hdIssues.Where(x => x.IssueID.Equals(id))
                          join status in _context.hdStatus on
                          issue.StatusID equals status.StatusID
                          join category in _context.hdCategories on
                          issue.CategoryID equals category.CategoryID
                          select new TicketHistoryItem()
                          {
                              Subject = issue.Subject,
                              TicketId = Convert.ToString(issue.IssueID),
                              Status = status.Name,
                              Department = category.Name,
                              CreateDate = issue.IssueDate,
                              ReplyMessage = issue.Body,
                              LastActivity = issue.LastUpdated,
                              LastMessage = userName,
                              Comment = comments,
                              UserID = issue.UserID,
                              CategoryID = issue.CategoryID
                          }).FirstOrDefault();
                if (detail != null)
                {
                    TicketHistoryItem tc = new TicketHistoryItem();
                    tc = new TicketHistoryItem();
                    tc.Subject = detail.Subject;
                    tc.TicketId = detail.TicketId;
                    tc.Department = detail.Department;
                    tc.CreateDate = detail.CreateDate;
                    tc.ReplyMessage = detail.ReplyMessage;
                    tc.LastActivity = detail.LastActivity;
                    tc.LastMessage = detail.LastMessage;
                    detail.Comment = GetAllCommentsForTicket(detail);
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _context, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "SupportTickets Getticket", User.Identity.Name);
            }
            return detail;
        }
        public IEnumerable<TicketHistoryItem> GetAllhdIssues()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<TicketHistoryItem> tickethistory = new List<TicketHistoryItem>();
            var ticketlist =
               (from objhdissue in _context.hdIssues
                where objhdissue.UserID == userId
                select new TicketHistoryItem()
                {
                    TicketId = Convert.ToString(objhdissue.IssueID),
                    Subject = objhdissue.Subject,
                    StatusID = objhdissue.StatusID,
                    CreateDate = objhdissue.IssueDate,
                    LastActivity = objhdissue.LastUpdated,
                    UserID = objhdissue.UserID,
                    CategoryID = objhdissue.CategoryID
                }).ToList();
            if (ticketlist.Count > 0)
            {
                TicketHistoryItem objticket = new TicketHistoryItem();
                foreach (var item in ticketlist)
                {
                    objticket = new TicketHistoryItem();
                    objticket.TicketId = item.TicketId;
                    objticket.Subject = item.Subject;
                    objticket.StatusID = item.StatusID;
                    objticket.CreateDate = item.CreateDate;
                    objticket.LastActivity = item.LastActivity;
                    objticket.UserID = item.UserID;
                    tickethistory.Add(objticket);
                }
            }
            return tickethistory;
        }
        public ActionResult GetClosedTickets([DataSourceRequest] DataSourceRequest request, string name)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                List<TicketHistoryItem> tickethistory = new List<TicketHistoryItem>();
                var ticketlist = (from a in _context.hdStatus
                                  join objhdissue in _context.hdIssues on a.StatusID equals objhdissue.StatusID
                                  where objhdissue.UserID.Equals(userId) & a.Name.Equals(name)
                                  select new TicketHistoryItem()
                                  {
                                      TicketId = Convert.ToString(objhdissue.IssueID),
                                      Subject = objhdissue.Subject,
                                      StatusID = objhdissue.StatusID,
                                      CreateDate = objhdissue.IssueDate,
                                      LastActivity = objhdissue.LastUpdated,
                                      UserID = objhdissue.UserID,
                                      CategoryID = objhdissue.CategoryID
                                  }).ToList();
                if (ticketlist.Count > 0)
                {
                    TicketHistoryItem objticket = new TicketHistoryItem();
                    foreach (var item in ticketlist)
                    {
                        objticket = new TicketHistoryItem();
                        objticket.TicketId = item.TicketId;
                        objticket.Subject = item.Subject;
                        objticket.StatusID = item.StatusID;
                        objticket.CreateDate = item.CreateDate;
                        objticket.LastActivity = item.LastActivity;
                        objticket.UserID = item.UserID;
                        tickethistory.Add(objticket);
                    }
                }
                return Json(tickethistory.ToDataSourceResult(request));
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _context, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "SupportTickets GetClosedTickets", User.Identity.Name);
                return null;
            }
        }
        public ActionResult GetOpenTickets([DataSourceRequest] DataSourceRequest request, string name)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                List<TicketHistoryItem> tickethistory = new List<TicketHistoryItem>();
                var ticketlist = (from a in _context.hdStatus
                                  join objhdissue in _context.hdIssues on a.StatusID equals objhdissue.StatusID
                                  where objhdissue.UserID.Equals(userId) & a.Name.Equals(name)
                                  select new TicketHistoryItem()
                                  {
                                      TicketId = Convert.ToString(objhdissue.IssueID),
                                      Subject = objhdissue.Subject,
                                      StatusID = objhdissue.StatusID,
                                      CreateDate = objhdissue.IssueDate,
                                      LastActivity = objhdissue.LastUpdated,
                                      UserID = objhdissue.UserID,
                                      CategoryID = objhdissue.CategoryID
                                  }).ToList();
                if (ticketlist.Count > 0)
                {
                    TicketHistoryItem objticket = new TicketHistoryItem();
                    foreach (var item in ticketlist)
                    {
                        objticket = new TicketHistoryItem();
                        objticket.TicketId = item.TicketId;
                        objticket.Subject = item.Subject;
                        objticket.StatusID = item.StatusID;
                        objticket.CreateDate = item.CreateDate;
                        objticket.LastActivity = item.LastActivity;
                        objticket.UserID = item.UserID;
                        tickethistory.Add(objticket);
                    }
                }
                return Json(tickethistory.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _context, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "SupportTickets GetOpenTickets", User.Identity.Name);
                return null;
            }
        }
        public ActionResult GetNewTickets([DataSourceRequest] DataSourceRequest request,string name)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                List<TicketHistoryItem> tickethistory = new List<TicketHistoryItem>();
                var ticketlist = (from a in _context.hdStatus
                                  join objhdissue in _context.hdIssues on a.StatusID equals objhdissue.StatusID
                                  where objhdissue.UserID.Equals(userId) & a.Name.Equals(name)
                                  select new TicketHistoryItem()
                                  {
                                      TicketId = Convert.ToString(objhdissue.IssueID),
                                      Subject = objhdissue.Subject,
                                      StatusID = objhdissue.StatusID,
                                      CreateDate = objhdissue.IssueDate,
                                      LastActivity = objhdissue.LastUpdated,
                                      UserID = objhdissue.UserID,
                                      CategoryID = objhdissue.CategoryID
                                  }).ToList();
                if (ticketlist.Count > 0)
                {
                    TicketHistoryItem objticket = new TicketHistoryItem();
                    foreach (var item in ticketlist)
                    {
                        objticket = new TicketHistoryItem();
                        objticket.TicketId = item.TicketId;
                        objticket.Subject = item.Subject;
                        objticket.StatusID = item.StatusID;
                        objticket.CreateDate = item.CreateDate;
                        objticket.LastActivity = item.LastActivity;
                        objticket.UserID = item.UserID;
                        tickethistory.Add(objticket);
                    }
                }
                return Json(tickethistory.ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _context, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "SupportTickets GetNewTickets", User.Identity.Name);
                return null;
            }
        }
        public TicketHistory CountTickets()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int Newcount = _context.hdIssues.Count(c => c.UserID == userId && c.StatusID == 3);
            int Opencount = _context.hdIssues.Count(c => c.UserID == userId && c.StatusID == 4);
            int Closecount = _context.hdIssues.Count(c => c.UserID == userId && c.StatusID == 5);
            return new TicketHistory()
            {
                New = Newcount,
                Open = Opencount,
                Closed = Closecount
            };
        }
        public async Task<ActionResult> Counts()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int Newcount = _context.hdIssues.Count(c => c.UserID == userId && c.StatusID == 3);
                int Opencount = _context.hdIssues.Count(c => c.UserID == userId && c.StatusID == 4);
                int Closecount = _context.hdIssues.Count(c => c.UserID == userId && c.StatusID == 5);
                var TicketCounts = new TicketHistory()
                {
                    New = Newcount,
                    Open = Opencount,
                    Closed = Closecount
                };
                return await Task.FromResult(Json(TicketCounts));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _context, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "SupportTickets Counts", User.Identity.Name);
                return null;
            }
        }
        public IActionResult TicketHistory_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(GetAllhdIssues().ToDataSourceResult(request));
        }
        [AcceptVerbs("Post")]
        public IActionResult PaymentMethods_Create([DataSourceRequest] DataSourceRequest request, TicketHistory method)
        {
            try
            {
                if (method != null && ModelState.IsValid)
                {
                }
                return Json(new[] { method }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _context, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "SupportTickets PaymentMethods_Create", User.Identity.Name);
                return null;
            }
        }
        [AcceptVerbs("Post")]
       public IActionResult TicketHistory_Update([DataSourceRequest] DataSourceRequest request, TicketHistory method)
        {
            try
            {
                if (method != null && ModelState.IsValid)
                {
                }
                return Json(new[] { method }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _context, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "SupportTickets TicketHistory_Update", User.Identity.Name);
                return null;
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        /// <summary>
        /// Phase II Changes - 03/20/2021
        /// Close Ticket
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CloseTicket([FromBody] TicketStatusModel ticket)
        {
            Int16 result = 0;
            try
            {
                var statusOpen = _context.hdStatus.Where(x => x.Name == "Open").FirstOrDefault();
                var statusNew = _context.hdStatus.Where(x => x.Name == "New").FirstOrDefault();
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userName = User.FindFirstValue(ClaimTypes.Name);

                hdComments cmt = new hdComments();
                cmt.IssueID = Convert.ToInt32(ticket.ticketId);
                cmt.CommentDate = DateTime.Now;
                cmt.UserID = userId;
                cmt.Body = ticket.ticketMessage;
                cmt.ForTechsOnly = Convert.ToBoolean(1);
                cmt.IsSystem = Convert.ToBoolean(0);
                cmt.Recipients = userName;
                _context.hdComments.Add(cmt);
                _context.SaveChanges();
                var updatedate = _context.hdIssues.Single(date => date.IssueID == cmt.IssueID);
                updatedate.LastUpdated = cmt.CommentDate;
                if (ticket.ticketStatus == "Open")
                {
                    updatedate.StatusID = statusOpen.StatusID + 1;
                }
                if (ticket.ticketStatus == "New")
                {
                    updatedate.StatusID = statusNew.StatusID + 2;
                }
                _context.SaveChanges();
                result = 1;
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, _context, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "SupportTicket Close Ticket", User.Identity.Name);
                return new JsonResult(result);
            }
        }
    }
}