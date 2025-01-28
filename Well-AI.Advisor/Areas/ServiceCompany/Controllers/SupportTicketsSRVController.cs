using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.Areas.ServiceCompany.Models;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.DLL.ServiceEntity;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Hubs;
using WellAI.Advisor.Model.Common;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.ServiceCompany.Models;

namespace WellAI.Advisor.Areas.ServiceCompany.Controllers
{
    [Area("ServiceCompany")]
    public class SupportTicketsSRVController : BaseController
    {
        private readonly ILogger<SupportTicketsSRVController> _logger;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly WebAIAdvisorContext db;
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private IHubContext<NotificationHub> _hubContext { get; set; }
        public SupportTicketsSRVController(WebAIAdvisorContext dbContext, SignInManager<WellIdentityUser> signInManager,
                                           ILogger<SupportTicketsSRVController> logger, UserManager<WellIdentityUser> userManager, RoleManager<IdentityRole> roleManager, IHubContext<NotificationHub> hubContext)
            : base(userManager, dbContext)
        {
            _signInManager = signInManager;
            _logger = logger;
            db = dbContext;
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
                //checking invalid user//
                if (!Convert.ToBoolean(WellAIAppContext.Current.Session.GetString("IsMaster")))
                {
                    if (GetComponentsBasedOnRole() == false)
                    {
                        string returnUrl = @"/ServiceDashboard";
                        return LocalRedirect(returnUrl);
                    }
                }
                var statuslist = db.hdStatus.ToList();
                ViewData["hdstatus"] = statuslist;
                ViewData["defaultstatus"] = statuslist.First();
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "SupportTicketSRV Index", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
           return View(CountTickets());
        }
        private bool GetComponentsBasedOnRole()
        {
            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();
            string roleName = roles.FirstOrDefault().Value;
            IRolePermissionBusiness rolePermissionBusiness = new RolePermissionBusiness(db, _roleManager, _userManager);
            var roleResult = rolePermissionBusiness.GetRoleByName(roleName);
            return rolePermissionBusiness.GetSRVComponentBasedOnRole(roleResult.Id, "ViewDashboard");
        }
        public IActionResult TicketDetail(string id)
        {
            TicketHistoryItem tickets = new TicketHistoryItem();
            try
            {
                int ticketID = Convert.ToInt32(id);
                tickets = Getticket(ticketID);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "SupportTicketSRV TicketDetail", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            return View(tickets);
        }
        public IActionResult NewTicket()
        {
            try
            {
                var newsts = db.hdStatus.Where(x => x.Name == "New").FirstOrDefault();
                List<hdCategories> categories = new List<hdCategories>();
                categories = (from product in db.hdCategories select product).ToList();
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
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "SupportTicketSRV NewTicket", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [HttpPost]
        public async Task<IActionResult> SaveNewTicket(TicketHistoryItem tc)
        {
            var commonbus = new CommonBusiness(db, _roleManager, _userManager);
            var userId = _userManager.GetUserId(User);
            var tenantId = await commonbus.GetTenantIdByUserId(userId);
            try
            {
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
                    issue.TenantId = tenantId;
                };
                db.hdIssues.Add(issue);
                await db.SaveChangesAsync();
                var item = db.hdIssues.Where(x => x.TenantId.Equals(tenantId))
                       .OrderByDescending(p => p.IssueDate)
                       .FirstOrDefault();
                int Lastid = item.IssueID;
                return RedirectToAction("TicketDetail", new RouteValueDictionary(
                        new { controller = "SupportTicketsSRV", action = "TicketDetail", Id = Lastid }));
            }
            catch (SqlException ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "SupportTicketSRV SaveNewTicket", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }

        /// <summary>
        /// Phase II Changes - 03/19/2021
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
                var statusOpen = db.hdStatus.Where(x => x.Name == "Open").FirstOrDefault();
                var statusNew = db.hdStatus.Where(x => x.Name == "New").FirstOrDefault();
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
                db.hdComments.Add(cmt);
                db.SaveChanges();
                var updatedate = db.hdIssues.Single(date => date.IssueID == cmt.IssueID);
                updatedate.LastUpdated = cmt.CommentDate;
                if (ticket.ticketStatus == "Open")
                {
                    updatedate.StatusID = statusOpen.StatusID + 1;
                }
                if (ticket.ticketStatus == "New")
                {
                    updatedate.StatusID = statusNew.StatusID + 2;
                }
                db.SaveChanges();
                result = 1;
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "SupportTicketSRV Close Ticket", User.Identity.Name);
                return new JsonResult(result);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveTicketDetail(TicketHistoryItem td, string submit, string closeticket)
        {
            if (submit == "Submit")
            {
                try
                {
                    var statusid = db.hdStatus.Where(x => x.Name == "New").FirstOrDefault();
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var userName = User.FindFirstValue(ClaimTypes.Name);
                    hdComments cmt = new hdComments();
                    if (!ModelState.IsValid)
                    {
                        cmt.IssueID = Convert.ToInt32(td.TicketId);
                        cmt.CommentDate = DateTime.Now;
                        cmt.UserID = userId;
                        cmt.Body = td.LeaveReplay;
                        cmt.ForTechsOnly = Convert.ToBoolean(1);
                        cmt.IsSystem = Convert.ToBoolean(0);
                        cmt.Recipients = userName;
                    };
                    db.hdComments.Add(cmt);
                    await db.SaveChangesAsync();
                    var updatedate = db.hdIssues.Single(date => date.IssueID == cmt.IssueID);
                    updatedate.LastUpdated = cmt.CommentDate;
                    if (td.Status == "New")
                    {
                        updatedate.StatusID = statusid.StatusID + 1;
                    }
                    await db.SaveChangesAsync();
                    ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                    MessageQueue messageQueue = new MessageQueue { From_id = "Well-AI Support", To_id = userId, Type = 2, IsActive = 1, EntityId = td.TicketId, JobName = td.Subject, CreatedDate = DateTime.Now };
                    await commonBusiness.AddNotifications(messageQueue);
                    await _hubContext.Clients.All.SendAsync("updateNotification").ConfigureAwait(true);
                }
                catch (Exception ex)
                {
                    CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                    customErrorHandler.WriteError(ex, "SupportTicketSRV SaveTicketDetail", User.Identity.Name);
                    string returnUrl = @"/Dashboard/Error";
                    return LocalRedirect(returnUrl);
                }
            }
            else if (closeticket == "Close Ticket")
            {
                try
                {
                    var statusOpen = db.hdStatus.Where(x => x.Name == "Open").FirstOrDefault();
                    var statusNew = db.hdStatus.Where(x => x.Name == "New").FirstOrDefault();
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var userName = User.FindFirstValue(ClaimTypes.Name);
                    hdComments cmt = new hdComments();
                    if (!ModelState.IsValid)
                    {
                        cmt.IssueID = Convert.ToInt32(td.TicketId);
                        cmt.CommentDate = DateTime.Now;
                        cmt.UserID = userId;
                        cmt.Body = td.LeaveReplay;
                        cmt.ForTechsOnly = Convert.ToBoolean(1);
                        cmt.IsSystem = Convert.ToBoolean(0);
                        cmt.Recipients = userName;
                    };
                    db.hdComments.Add(cmt);
                    await db.SaveChangesAsync();
                    var updatedate = db.hdIssues.Single(date => date.IssueID == cmt.IssueID);
                    updatedate.LastUpdated = cmt.CommentDate;
                    if (td.Status == "Open")
                    {
                        updatedate.StatusID = statusOpen.StatusID + 1;
                    }
                    if (td.Status == "New")
                    {
                        updatedate.StatusID = statusNew.StatusID + 2;
                    }
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                    customErrorHandler.WriteError(ex, "SupportTicketSRV SaveTicketDetail", User.Identity.Name);
                    string returnUrl = @"/Dashboard/Error";
                    return LocalRedirect(returnUrl);
                }
            }
            return RedirectToAction("Index");
        }
        public List<CommentSRV> GetAllCommentsForTicket(string TicketId)
        {
            List<CommentSRV> cmtsrvlist = new List<CommentSRV>();
            try
            {
                int id = Convert.ToInt32(TicketId);
                var cmtlist = (from objcmt in db.hdComments.Where(x => x.IssueID.Equals(id))
                               select new CommentSRV()
                               {
                                   Id = objcmt.CommentID,
                                   Content = objcmt.Body,
                                   TicketId = Convert.ToString(objcmt.IssueID),
                                   Author = objcmt.Recipients,
                                   Date = objcmt.CommentDate
                               }).ToList();
                if (cmtlist.Count > 0)
                {
                    CommentSRV cmtsrv = new CommentSRV();
                    foreach (var item in cmtlist)
                    {
                        cmtsrv = new CommentSRV();
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
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "SupportTicketSRV GetAllCommentsForTicket", User.Identity.Name);
            }
            return cmtsrvlist;
        }
        //Phase-II Changes
        public ActionResult GetClosedTickets([DataSourceRequest] DataSourceRequest request, string name)
        {
            try
            {
                var commonbus = new CommonBusiness(db, _roleManager, _userManager);
                var userId = _userManager.GetUserId(User);
                var tenantId = commonbus.GetTenantIdByUserId(userId);
                List<TicketHistoryItem> tickethistory = new List<TicketHistoryItem>();
                var ticketlist = (from a in db.hdStatus
                                  join objhdissue in db.hdIssues on a.StatusID equals objhdissue.StatusID
                                  where objhdissue.TenantId.Equals(tenantId.Result) & a.Name.Equals(name)
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
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "SupportTicketSRV GetClosedTickets", User.Identity.Name);
                return Json(ex);
            }
        }
        //Phase-II Changes
        public ActionResult GetOpenTickets([DataSourceRequest] DataSourceRequest request, string name)
        {
            try
            {
                var commonbus = new CommonBusiness(db, _roleManager, _userManager);
                var userId = _userManager.GetUserId(User);
                var tenantId = commonbus.GetTenantIdByUserId(userId);
                List<TicketHistoryItem> tickethistory = new List<TicketHistoryItem>();
                var ticketlist = (from a in db.hdStatus
                                  join objhdissue in db.hdIssues on a.StatusID equals objhdissue.StatusID
                                  where objhdissue.TenantId.Equals(tenantId.Result) && a.Name.Equals(name)
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
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "SupportTicketSRV GetOpenTickets", User.Identity.Name);
                return Json(ex);
            }
        }
        //Phase-II Changes
        public ActionResult GetNewTicket([DataSourceRequest] DataSourceRequest request,string name)
        {
            try
            {
                var commonbus = new CommonBusiness(db, _roleManager, _userManager);
                var userId = _userManager.GetUserId(User);
                var tenantId = commonbus.GetTenantIdByUserId(userId);
                List<TicketHistoryItem> tickethistory = new List<TicketHistoryItem>();
                var ticketlist = (from objhdstatus in db.hdStatus
                                  join objhdissue in db.hdIssues on objhdstatus.StatusID equals objhdissue.StatusID
                                  where objhdissue.TenantId.Equals(tenantId.Result) & objhdstatus.Name.Equals(name)
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
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "SupportTicketSRV GetNewTicket", User.Identity.Name);
                return Json(ex);
            }
        }
        public TicketHistoryItem Getticket(int id)
        {
            TicketHistoryItem detail = new TicketHistoryItem();
            try
            {
                var comments = new List<CommentSRV>();
                var userName = User.FindFirstValue(ClaimTypes.Name);
                detail = (from issue in db.hdIssues.Where(x => x.IssueID.Equals(id))
                          join status in db.hdStatus on
                          issue.StatusID equals status.StatusID
                          join category in db.hdCategories on
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
                              CommentSRV = comments
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
                    detail.CommentSRV = GetAllCommentsForTicket(detail.TicketId);
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "SupportTicketSRV GetTicket", User.Identity.Name);
            }
            return detail;
        }
        public IEnumerable<TicketHistoryItem> GetAllhdIssues()
        {
            List<TicketHistoryItem> tickethistory = new List<TicketHistoryItem>();
            try
            {
                var commonbus = new CommonBusiness(db, _roleManager, _userManager);
                var userId = _userManager.GetUserId(User);
                var tenantId = commonbus.GetTenantIdByUserId(userId);
                var ticketlist = (from objhdissue in db.hdIssues.Where(x => x.TenantId.Equals(tenantId.Result))
                                  select new TicketHistoryItem()
                                  {
                                      TicketId = Convert.ToString(objhdissue.IssueID),
                                      Subject = objhdissue.Subject,
                                      StatusID = objhdissue.StatusID,
                                      CreateDate = objhdissue.IssueDate,
                                      LastActivity = objhdissue.LastUpdated
                                  }).ToList();
                if (ticketlist != null)
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
                        tickethistory.Add(objticket);
                    }
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "SupportTicketSRV GetAllhdIssues", User.Identity.Name);
            }
            return tickethistory;
        }
        public TicketHistory CountTickets()
        {
            var commonbus = new CommonBusiness(db, _roleManager, _userManager);
            var userId = _userManager.GetUserId(User);
            var tenantId = commonbus.GetTenantIdByUserId(userId);
            int Newcount = db.hdIssues.Count(c => c.StatusID == 3 && c.TenantId.Equals(tenantId.Result));
            int Opencount = db.hdIssues.Count(c => c.StatusID == 4 && c.TenantId.Equals(tenantId.Result));
            int Closecount = db.hdIssues.Count(c => c.StatusID == 5 && c.TenantId.Equals(tenantId.Result));
            int Deletecount = db.hdIssues.Count(c => c.StatusID == 6 && c.TenantId.Equals(tenantId.Result));
            int Resolvecount = db.hdIssues.Count(c => c.StatusID == 7 && c.TenantId.Equals(tenantId.Result));
            int Unapprovedcount = db.hdIssues.Count(c => c.StatusID == 8 && c.TenantId.Equals(tenantId.Result));
            return new TicketHistory()
            {
                New = Newcount,
                Open = Opencount,
                Closed = Closecount,
                Unapproved = Unapprovedcount,
                Deleted = Deletecount
            };
        }
        //Phase-II Changes
        public async Task<IActionResult> Counts()
        {
            try
            {
                var commonbus = new CommonBusiness(db, _roleManager, _userManager);
                var userId = _userManager.GetUserId(User);
                var tenantId = commonbus.GetTenantIdByUserId(userId);
                int Newcount = db.hdIssues.Count(c => c.StatusID == 3 && c.TenantId.Equals(tenantId.Result));
                int Opencount = db.hdIssues.Count(c => c.StatusID == 4 && c.TenantId.Equals(tenantId.Result));
                int Closecount = db.hdIssues.Count(c => c.StatusID == 5 && c.TenantId.Equals(tenantId.Result));
                int Deletecount = db.hdIssues.Count(c => c.StatusID == 6 && c.TenantId.Equals(tenantId.Result));
                int Resolvecount = db.hdIssues.Count(c => c.StatusID == 7 && c.TenantId.Equals(tenantId.Result));
                int Unapprovedcount = db.hdIssues.Count(c => c.StatusID == 8 && c.TenantId.Equals(tenantId.Result));
                var CountTickets = new TicketHistory()
                {
                    New = Newcount,
                    Open = Opencount,
                    Closed = Closecount,
                    Unapproved = Unapprovedcount,
                    Deleted = Deletecount
                };
                return await Task.FromResult(Json(CountTickets));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "SupportTicketSRV Counts", User.Identity.Name);
                return Json(ex);
            }
        }
        //Phase-II Changes
        public IActionResult TicketHistory_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                return Json(GetAllhdIssues().ToDataSourceResult(request));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "SupportTicketSRV TicketHistory_Read", User.Identity.Name);
                return Json(ex);
            }
        }
        //Phase-II Changes
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
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "SupportTicketSRV PaymentMethods_Create", User.Identity.Name);
                return Json(ex);
            }
        }
        //Phase-II Changes
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
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "SupportTicketSRV TicketHistory_Update", User.Identity.Name);
                return Json(ex);
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}