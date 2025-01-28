using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellAI.Advisor.BLL.IBusiness;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.DLL.Repository;
using WellAI.Advisor.Model.Common;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.Model.ServiceCompany.Models;
using WellAI.Advisor.Model.Administration;
//using Well_AI.Advisor.API.Models;
using WellAI.Advisor.BLL.Administration;

namespace WellAI.Advisor.BLL.Business
{
    public class ProjectBusiness : IProjectBusiness
    {
        private readonly WebAIAdvisorContext db;
        private UserManager<WellIdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public ProjectBusiness(WebAIAdvisorContext db, RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager)
        {
            this.db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public ProjectBusiness(WebAIAdvisorContext db, UserManager<WellIdentityUser> userManager)
        {
            this.db = db;
            _userManager = userManager;
        }

        public async Task<ProjectDashboardOperViewModel> ProjectDashboardOperTenantId(string tenantid, WellIdentityUser user, string RigId)
        {
            try
            {
                DateTime LastMonthLastDate = DateTime.Today.AddDays(0 - DateTime.Today.Day);
                DateTime LastMonthFirstDate = LastMonthLastDate.AddDays(1 - LastMonthLastDate.Day);

                var checkwellFilter = RigId == DLL.Constants.NoSpecificWellFilterKey;
                //DWOP - Task data from DrillPlanDetails

                var result = (from prj in db.Projects
                              join well in db.WellRegister on prj.WellID equals well.well_id
                              join rig in db.rig_register on well.RigID equals rig.Rig_id
                              join crf in db.CorporateProfile on prj.ServiceCompID equals crf.TenantId
                              join au in db.AuctionProposals on prj.ProposalID equals au.ProposalId
                              join task in db.DrillPlanDetails on au.JobId equals task.TaskId into taskLj
                              from task in taskLj.DefaultIfEmpty()
                              where prj.OprTenantID.Equals(user.TenantId) && (rig.Rig_id == RigId && !checkwellFilter || checkwellFilter)
                              select new
                              {
                                  prj.ID,
                                  prj.ProjectStatus,
                                  ProjectStartDate = prj.ActualStart.Value,
                                  ProjectEndDate = prj.ActualEnd.Value,
                                  CreatedDate = prj.DateCreated,
                                  Months = Convert.ToDateTime(prj.ActualStart).Month,
                                  CreatedMonths = Convert.ToDateTime(prj.DateCreated).Month,
                                  prj.WellID,
                                  rig.Rig_id
                              }).ToList();

                if (user != null && user.WellUser.HasValue && user.WellUser.Value)
                {
                    var userRigs = db.UserRigs.Where(x => x.UserId == user.Id).Select(x => x.RigID).ToList();
                    result = result.Where(x => userRigs.FirstOrDefault(y => y == x.Rig_id) != null && !checkwellFilter || checkwellFilter).ToList();
                }

                //Count Duplication fixes
                var resultAgg = result.GroupBy(x => x.ID).Select(g => g.First()).ToList();

                var projectsawardedCount = (from r in resultAgg where r.ProjectStatus == (int)ProjectStatusList.CloseProject select r).Count();
                var projectsStartedCount = (from r in resultAgg where r.ProjectStatus == (int)ProjectStatusList.UpCommingProject select r).Count();

                var ProjectsActive = (from r in resultAgg where r.ProjectStatus == (int)ProjectStatusList.OnGoingProjects select r).Count();
                var ProjectsSuspended = (from r in resultAgg where r.ProjectStatus == (int)ProjectStatusList.SuspendProject select r).Count();

                ProjectDashboardOperViewModel projectDashboardOper = new ProjectDashboardOperViewModel()
                {
                    ProjectsStartedThisMonthCount = Convert.ToInt32(projectsStartedCount),
                    ProjectActiveCount = Convert.ToInt32(ProjectsActive),
                    ProjectSuspendedCount = Convert.ToInt32(ProjectsSuspended),
                    ProjectsAwardedThisMonthCount = Convert.ToInt32(projectsawardedCount),
                };

                return await Task.FromResult(projectDashboardOper);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ProjectBusiness GetRIGAIGroup", null);
                return null;
            }
        }

      
        public async Task<ProjectDashboardSerViewModel> ProjectDashboardSerTenantId(string tenantid, string operId)
        {
            try
            {
                var nospecificOperator = operId == DLL.Constants.NoSpecificWellFilterKey;

                DateTime LastMonthLastDate = DateTime.Today.AddDays(0 - DateTime.Today.Day);
                DateTime LastMonthFirstDate = LastMonthLastDate.AddDays(1 - LastMonthLastDate.Day);

                var result = await GetUpCommingProjectsSRV(tenantid, operId);

                var resultAggr = result.GroupBy(x => x.ProjectId).Select(g => g.First()).ToList();
                //result changed to resultAggr
                var projectsawardedCount = (from r in resultAggr where r.ProjectStatusName == "Completed" select r).Count();
                var projectsStartedCount = (from r in resultAggr where r.ProjectStatusName == "Upcoming" select r).Count();
                var ProjectsActive = (from r in resultAggr where r.ProjectStatusName == "Active" select r).Count();
                var ProjectsSuspended = (from r in resultAggr where r.ProjectStatusName == "Suspended" select r).Count();

                ProjectDashboardSerViewModel projectDashboardOper = new ProjectDashboardSerViewModel()
                {
                    ProjectsStartedThisMonthCount = Convert.ToInt32(projectsStartedCount),
                    ProjectActiveCount = Convert.ToInt32(ProjectsActive),
                    ProjectSuspendedCount = Convert.ToInt32(ProjectsSuspended),
                    ProjectsAwardedThisMonthCount = Convert.ToInt32(projectsawardedCount),
                };

                return await Task.FromResult(projectDashboardOper);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ProjectBusiness ProjectDashboardSerTenantId", null);
                ProjectDashboardSerViewModel projectDashboardSerViewModel = new ProjectDashboardSerViewModel();

                return await Task.FromResult(projectDashboardSerViewModel);
            }
        }

        public Task<List<AuctionProposalAttachmentViewModel>> GetProjectProposalAttachments(string tenantId, string proposalId, string projectId)
        {
            try
            {
                var projectAttachment = db.ProjectAttachments.Where(x => x.ProjectId.Equals(projectId) && x.TenantID == tenantId && x.ProposalID == proposalId)
                                          .Select(x => new AuctionProposalAttachmentViewModel
                                          {
                                              AttachmentId = x.AttachmentId,
                                              FileName = x.FileName,
                                              TableName = "PR",
                                              DateUploaded=x.DateUploaded
                                          }).ToList();
                var proposalAttachments = db.AuctionProposalAttachments.Where(x => x.ProposalID == proposalId && x.TenantID == tenantId)
                    .Select(x => new AuctionProposalAttachmentViewModel { AttachmentId = x.AttachmentId, FileName = x.FileName, TableName = "AU",DateUploaded=x.DateUploaded }).ToList();
                var allResult = projectAttachment.Union(proposalAttachments).Select(x => new AuctionProposalAttachmentViewModel { AttachmentId = x.AttachmentId, FileName = x.FileName, TableName = x.TableName, DateUploaded = x.DateUploaded}).ToList();
                
                return Task.FromResult(allResult);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ProjectBusiness GetProjectProposalAttachments", null);
                return null;
            }
        }

        public Task<List<TechnicianViewModel>> GetAssignedTechnicianByProjectId(string projectId)
        {
            try
            {
                var result = (from pt in db.ProjectTechnicians
                              join pro in db.Projects on pt.ProjectId equals pro.ID
                              join user in _userManager.Users on pt.TechnitionId equals user.Id
                              where pt.ProjectId == projectId && pro.ID == projectId
                              select new TechnicianViewModel
                              {
                                  Id = pt.TechnitionId,
                                  TechName = $"{user.FirstName} {user.LastName}",
                                  TechAssignEndDate = pt.EndDate,
                                  TechWorkingStatus = pt.EndDate < DateTime.Now || pt.ProjectTechStatus == 0 ? "N/A" : "Assigned",
                                  TechAssignStartDate = pt.AssignDate,
                                  TechMobile = user.Mobile,
                                  Notes = pt.Notes,
                                  ProjectId = pt.ProjectId
                              }).ToList();

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ProjectBusiness GetAssignedTechnicianByProjectId", null);
                return null;
            }
        }

        public Task<ProjectViewSRVModel> GetUpCommingProjectsDetailsByTenantIdForSRV(string tenantid, string projectId)
        {
            try
            {
                var pr= db.Projects.FirstOrDefault(x => x.ID.Equals(projectId) && x.ServiceCompID.Equals(tenantid));
                if (pr == null)
                {
                    pr = db.Projects.FirstOrDefault(x => x.ProposalID.Equals(projectId) && x.ServiceCompID.Equals(tenantid));
                }
                //DWOP - Task data from DrillPlanDetails

                var result = (from ap in db.AuctionProposals
                              join well in db.WELL_REGISTERs on ap.WellId equals well.well_id into welllj
                              from well in welllj.DefaultIfEmpty()
                              join rigreg in db.rig_register on well.RigID equals rigreg.Rig_id
                              join crf in db.CorporateProfile on pr.OprTenantID equals crf.TenantId
                              join user in _userManager.Users on pr.CreateById equals user.Id
                              join task in db.DrillPlanDetails on ap.JobId equals task.TaskId into Task1
                              from TaskResult in Task1.DefaultIfEmpty()
                              where rigreg.isActive  && ap.ProposalId == pr.ProposalID
                              select new ProjectViewSRVModel()
                              {
                                  ProjectId = pr.ID,
                                  ProjectCode = pr.ProjectID,
                                  OperatorCompanyName = crf.Name,
                                  Title = pr.ProjectTitle,
                                  ActualStartDate = pr.ActualStart,
                                  ActualEndDate = pr.ActualEnd,
                                  ExpectedStartDate = ap.ProjectStartDate,
                                  ExpectedEndDate = ap.ProjectStartDate.AddHours(ap.ProjectDuration),
                                  Description = pr.ProjectDescription,
                                  DateAwared = pr.DateCreated.Value,
                                  Job = TaskResult.TaskName,
                                  WellName = well == null ? "N/A" : well.wellname,
                                  RigName = rigreg.Rig_Name,
                                  OperatorMobile = user.Mobile,
                                  OperatorUserName = $"{user.FirstName} {user.LastName}",
                                  ProposalId = pr.ProposalID,
                                  ProjectStatus = pr.ProjectStatus,
                                  OperatorTenantId = pr.OprTenantID
                              }).FirstOrDefault();
                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ProjectBusiness GetUpCommingProjectsDetailsByTenantIdForSRV", null);
                return null;
            }
        }

        public Task<ProjectViewModel> GetUpCommingProjectsDetailsByTenantIdForOperator(string tenantid, string projectId)
        {
            
            try
            {
               
                 var project = db.Projects.FirstOrDefault(x => x.ID.Equals(projectId) && x.OprTenantID.Equals(tenantid));
                if (project == null)
                {
                    project = db.Projects.FirstOrDefault(x => x.ProposalID.Equals(projectId) && x.OprTenantID.Equals(tenantid));
                }
                //DWOP - Task data from DrillPlanDetails

                var proposal = (from au in db.AuctionProposals
                                    join task in db.DrillPlanDetails on au.JobId equals task.TaskId into taskLj
                                    from task in taskLj.DefaultIfEmpty()
                                    join wells in db.WELL_REGISTERs on au.WellId equals wells.well_id
                                    join rig in db.rig_register on au.RigId equals rig.Rig_id
                                    where au.ProposalId == project.ProposalID
                                    select new
                                    {
                                        au,
                                        task,
                                        wells,
                                        rig
                                    }
                                    ).FirstOrDefault();
                    var well = proposal.wells;
                    var Rig = proposal.rig;
                    var profile = db.CorporateProfile.Where(x => x.TenantId == project.ServiceCompID).OrderByDescending(x => x.CreatedDate).FirstOrDefault();
                    var user = _userManager.Users.FirstOrDefault(x => x.Id == project.CreateById);

                    var checkList = "";

                    var wellCheckLists = db.WellCheckList.Where(x => x.TenantID == tenantid && x.WellId == proposal.wells.well_id).ToList();

                    for (var i = 0; i < wellCheckLists.Count; i++)
                    {
                        var checkItem = wellCheckLists[i].CheckList;

                        var wellCheckListDetail = JsonConvert.DeserializeObject<List<WellCheckListDetailModel>>(checkItem);

                        for (var j = 0; j < wellCheckListDetail.Count; j++)
                        {
                            if (wellCheckListDetail[j].CheckListStatus == 1)
                            {
                                checkList += wellCheckListDetail[j].WellTaskId + ";";
                            }
                        }
                    }

                    var result = new ProjectViewModel
                    {
                        ProjectId = project.ID,
                        ProjectCode = project.ProjectID,
                        OperatorCompanyName = profile == null ? "" : profile.Name,
                        Title = project.ProjectTitle,
                        ActualStartDate = project.ActualStart,
                        ActualEndDate = project.ActualEnd,
                        ExpectedStartDate = proposal.au.ProjectStartDate,
                        ExpectedEndDate = proposal.au.AuctionEnd.AddHours(proposal.au.ProjectDuration),
                        Description = project.ProjectDescription,
                        DateAwared = project.DateCreated.Value,
                        Job = proposal.task == null ? proposal.au.JobId : proposal.task.TaskName,
                        WellName = well.wellname,
                        WellId = well == null ? "" : well.well_id,
                        OperatorMobile = user.Mobile,
                        OperatorUserName = $"{user.FirstName} {user.LastName}",
                        ProposalId = project.ProposalID,
                        ProjectStatus = project.ProjectStatus,
                        ProjectStatusName = Convert.ToInt16(project.ProjectStatus) == 0 ? "Upcoming" :
                                            Convert.ToInt16(project.ProjectStatus) == 1 ? "Active" :
                                            Convert.ToInt16(project.ProjectStatus) == 2 ? "Closed" :
                                            Convert.ToInt16(project.ProjectStatus) == 3 ? "Suspended" : string.Empty,
                        CheckIds = checkList,
                        RigId = Rig.Rig_id,
                        RigName = Rig.Rig_Name
                    };

                    
                        return Task.FromResult(result);
              }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ProjectBusiness GetUpCommingProjectsDetailsByTenantIdForOperator", null);
                return null;
            }
        }

        public Task<MessageToQueue> GetProposalCreatorOperatorAndUserEmail(string projectId, string senderUserId, string tId)
        {
            try
            {
                string creatorEmail = string.Empty;
                var project = (from proj in db.Projects
                               join corp in db.CorporateProfile on proj.OprTenantID equals corp.TenantId
                               join well in db.WELL_REGISTERs on proj.WellID equals well.well_id into welllj
                               from well in welllj.DefaultIfEmpty()
                               join rig in db.RigRegisters on well.RigID equals rig.Rig_Id into riglj
                               from rig in riglj.DefaultIfEmpty()
                               where proj.ID == projectId
                               select new
                               {
                                   OperatorName = corp.Name,
                                   ProjectName = proj.ProjectTitle,
                                   ProjectID = proj.ProjectID,
                                   RigName = rig == null ? "No Rig" : rig.Rig_Name,
                                   CreateById = proj.CreateById,
                                   ProjectTitle = proj.ProjectTitle
                               }).FirstOrDefault();

                if (project == null)
                {
                    return null;
                }
                else
                {
                    var emailTemplates = db.EmailTemplates.Where(x => x.EventName == "ProjectTracker").FirstOrDefault();

                    var user = _userManager.Users.FirstOrDefault(x => x.Id == project.CreateById);
                    var techName = _userManager.Users.FirstOrDefault(x => x.Id == tId);
                    var fromEmail = _userManager.Users.FirstOrDefault(x => x.Id == senderUserId).Email;
                    creatorEmail = user.Email;
                    MessageToQueue message;
                    if (emailTemplates != null)
                    {
                        message = new MessageToQueue
                        {
                            FromEmail = fromEmail,
                            FromName = null,
                            MsgBody = emailTemplates.Body.Replace("[FIRSTNAME]", user.FirstName).Replace("[ProjectID]", project.ProjectID).
                            Replace("[ProjectTitle]", project.ProjectTitle).Replace("[TechnicianName]", techName.FirstName + " " + techName.LastName),
                            MsgSubject = emailTemplates.Subject.Replace("[OperatorName]", project.OperatorName).Replace("[TechnicianName]", techName.FirstName + " " + techName.LastName)
                            .Replace("[RigName]", project.RigName),
                            ToEmail = creatorEmail,
                            ToName = null
                        };
                    }
                    else
                    {
                        message = new MessageToQueue
                        {
                            FromEmail = fromEmail,
                            FromName = "Well AI",
                            MsgBody = $"Dear Member, You can track project {project.ProjectID}({project.ProjectTitle}) by clicking on <a href='@url'>{project.ProjectID}</a>.",
                            MsgSubject = $"Project {project.ProjectID}({project.ProjectTitle}) Tracker",
                            ToEmail = $"{creatorEmail}",
                            ToName = null
                        };
                    }
                    return Task.FromResult(message);
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ProjectBusiness GetProposalCreatorOperatorAndUserEmail", null);
                return null;
            }
        }

        public async Task<List<ProjectNote>> GetProjectNotes(string projectId)
        {
            try
            {
                var notes = await db.ProjectNotes.Where(x => x.ProjectId == projectId).ToListAsync();

                for (var i = 0; i < notes.Count; i++)
                {
                    var user = _userManager.Users.Where(x => x.Id == notes[i].Author).FirstOrDefault();

                    notes[i].ProjectId = db.Projects.FirstOrDefault(x => x.ID == notes[i].ProjectId).ProjectID;
                    notes[i].Author = user.FirstName + " " + (string.IsNullOrEmpty(user.MiddleName) ? "" : user.MiddleName + " ") + user.LastName;
                }

                return notes;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ProjectBusiness GetProjectNotes", null);
                return null;
            }
        }

        public async Task<ProjectNote> CreateNewProjectNote(ProjectNote newNote)
        {
            try
            {
                await db.ProjectNotes.AddAsync(newNote);

                var result = await db.SaveChangesAsync();

                var user = _userManager.Users.Where(x => x.Id == newNote.Author).FirstOrDefault();

                newNote.ProjectId = db.Projects.FirstOrDefault(x => x.ID == newNote.ProjectId).ProjectID;
                newNote.Author = user.FirstName + " " + (string.IsNullOrEmpty(user.MiddleName) ? "" : user.MiddleName + " ") + user.LastName;

                return newNote;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ProjectBusiness CreateNewProjectNote", null);
                return null;
            }
        }

        public async Task<List<TechnicianViewModel>> GetTechnicianByTenantid(string tenantId)
        {
            try
            {
                var output = await GetUserList(tenantId);
                var result = (from o in output
                              select new TechnicianViewModel
                              {
                                  TechUserId = o.UserID,
                                  TechName = $"{o.FirstName} {o.LastName}",
                              }).ToList();
                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ProjectBusiness GetTechnicianByTenantid", null);
                return null;
            }
        }

        private Task<List<UserViewModel>> GetUserList(string tenantId)
        {
            try
            {
                List<UserViewModel> userViewModelList = new List<UserViewModel>();
                //getting list of users//
                var users = (from u in _userManager.Users
                             join tu in db.TenantUsers on u.Id equals tu.UserId
                             where tu.TenantId == tenantId
                             select u).ToList();
                //.ToList();
                foreach (var user in users)
                {
                    UserViewModel userViewModel = new UserViewModel();

                    userViewModel.UserID = user.Id;
                    userViewModel.PhoneNumber = user.PhoneNumber;
                    userViewModel.Email = user.Email;
                    userViewModel.FirstName = user.FirstName;
                    userViewModel.MiddleName = user.MiddleName;
                    userViewModel.LastName = user.LastName;
                    userViewModel.Mobile = user.Mobile;
                    userViewModel.JobTitle = user.JobTitle;
                    userViewModel.Address = user.Address;
                    userViewModel.City = user.City;
                    userViewModel.State = user.State;
                    userViewModel.Zip = user.Zip;
                    userViewModel.IsPrimary = user.Primary.HasValue ? user.Primary.Value : false;
                    userViewModel.AdditionalNotes = user.AdditionalNotes;
                    var userRoleNames = _userManager.GetRolesAsync(user).Result;
                    var tenantRoles = (from r in _roleManager.Roles
                                       join tr in db.TenantRoles on r.Id equals tr.RoleId
                                       where tr.TenantId == tenantId
                                       select r).ToList();

                    userViewModel.roles = new List<IdentityRole>();
                    userViewModel.SelectedRoles = "";

                    foreach (var tenantRole in tenantRoles)
                    {
                        if (userRoleNames.Contains(tenantRole.Name))
                        {
                            userViewModel.roles.Add(new IdentityRole { Id = tenantRole.Id, Name = tenantRole.Name });
                            userViewModel.SelectedRoles += tenantRole.Id + ";";
                        }
                    }

                    userViewModelList.Add(userViewModel);
                }
                return Task.FromResult(userViewModelList);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ProjectBusiness GetUserList", null);
                return null;
            }
        }

        public async Task<TechnicianViewModel> AddTechnicianOnProject(TechnicianViewModel input)
        {
            try
            {
                ProjectTechnician projectTechnician = new ProjectTechnician()
                {
                    Id = Guid.NewGuid().ToString(),
                    ProjectId = input.ProjectId,
                    AssignDate = input.TechAssignStartDate,
                    EndDate = input.TechAssignEndDate,
                    Notes = input.Notes,
                    ServiceVehicleId = input.ServiceVehicleId,
                    ProjectTechStatus = 1,
                    TechnitionId = input.TechUserId
                };

                db.ProjectTechnicians.Add(projectTechnician);
                db.SaveChanges();
                
                var user = _userManager.Users.Where(x => x.Id.Equals(projectTechnician.TechnitionId)).Select(s => new TechnicianViewModel
                {
                    Id = projectTechnician.Id,
                    TechName = $"{s.FirstName} {s.LastName}",
                    TechMobile = s.Mobile,
                    TechAssignStartDate = projectTechnician.AssignDate,
                    TechWorkingStatus = "Assigned",
                    TechAssignEndDate = projectTechnician.EndDate,
                    Notes = input.Notes
            }).FirstOrDefault();

                return await Task.FromResult(user);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ProjectBusiness AddTechnicianOnProject", null);
                return null;
            }
        }
        public async Task<DateTime> ConvertToCstTime(DateTime DateTime)
        {
            try
            {
                if (DateTime != null)
                {
                    DateTime timeUtc = (TimeZoneInfo.ConvertTimeToUtc(DateTime, TimeZoneInfo.Local));
                    TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                    DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
                    return await Task.FromResult(cstTime);
                }
                return await Task.FromResult(DateTime);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "AuctionProposal ConvertToCstTime", null);
                return DateTime;
            }
        }
        public async Task<int> UpdateUpCommingProjectsDetails(ProjectViewSRVModel input)
        {
            try
            {
                var updatedate = db.Projects.Where(x => x.ID.Equals(input.ProjectId)).FirstOrDefault();
                if (input.ProjectStatusName == "Start" && updatedate.ProjectStatus != 2)
                {
                    updatedate.ProjectStatus = (int)OngoingProjectStatusList.OnGoingProjects;
                    updatedate.ActualStart = DateTime.Now;
                }
                else if (input.ProjectStatusName == "Close")
                {
                    updatedate.ProjectStatus = (int)OngoingProjectStatusList.CloseProject;
                    updatedate.ActualEnd = DateTime.Now;
                }
                else if (input.ProjectStatusName == "Suspend")
                {
                    updatedate.ProjectStatus = (int)OngoingProjectStatusList.SuspendProject;
                }

                var result = await db.SaveChangesAsync();
                return result;

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ProjectBusiness UpdateUpCommingProjectsDetails", null);
                return 0;
            }
        }

        public async Task<int> UpdateUpCommingProjectsDetailsForOperator(ProjectViewModel input)
        {
            try
            {
                var updatedate = db.Projects.Where(x => x.ID.Equals(input.ProjectId)).FirstOrDefault();
                var serviceusers = db.TenantUsers.Where(x => x.TenantId == updatedate.ServiceCompID).Distinct().ToList();
                
                if (input.ProjectStatusName == "Start" && updatedate.ProjectStatus == 0)
                {
                    updatedate.ProjectStatus = (int)OngoingProjectStatusList.OnGoingProjects;
                    updatedate.ActualStart = DateTime.Now;
                    updatedate.ModifyDate = DateTime.Now;
                    foreach (var item in serviceusers)
                    {
                        MessageQueue messageQueue = new MessageQueue { From_id = updatedate.CreateById, To_id = item.UserId, Type = 6, IsActive = 1, EntityId = updatedate.ProposalID, JobName = "Service Started", TaskName = "Service Started" + " "+input.RigName + "-" + input.WellName + ":" + input.Job, CreatedDate = DateTime.Now };
                        db.MessageQueues.Add(messageQueue);
                    }
                   
                }
                else if (input.ProjectStatusName == "Close")
                {
                    updatedate.ProjectStatus = (int)OngoingProjectStatusList.CloseProject;
                    updatedate.ActualEnd = DateTime.Now;
                    updatedate.ModifyDate = DateTime.Now;
                    foreach (var item in serviceusers)
                    {
                        MessageQueue messageQueue = new MessageQueue { From_id = updatedate.CreateById, To_id = item.UserId, Type = 6, IsActive = 1, EntityId = updatedate.ProposalID, JobName = "Service Closed", TaskName = "Service Closed" +" " + input.RigName + "-" + input.WellName + ":" + input.Job, CreatedDate = DateTime.Now };
                        db.MessageQueues.Add(messageQueue);
                    }
                }
                else if (input.ProjectStatusName == "Suspend")
                {
                    updatedate.ProjectStatus = (int)OngoingProjectStatusList.SuspendProject;
                    updatedate.ModifyDate = DateTime.Now;
                    foreach (var item in serviceusers)
                    {
                        MessageQueue messageQueue = new MessageQueue { From_id = updatedate.CreateById, To_id = item.UserId, Type = 6, IsActive = 1, EntityId = updatedate.ProposalID, JobName = "Service Suspend", TaskName = "Service Suspend" +" " + input.RigName + "-" + input.WellName + ":" + input.Job, CreatedDate = DateTime.Now };
                        db.MessageQueues.Add(messageQueue);
                    }
                }
                else if (input.ProjectStatusName == "Start" && updatedate.ProjectStatus == (int)OngoingProjectStatusList.SuspendProject)
                {
                    updatedate.ProjectStatus = (int)OngoingProjectStatusList.OnGoingProjects;
                    updatedate.ActualStart = DateTime.Now;
                    updatedate.ModifyDate = DateTime.Now;
                    foreach (var item in serviceusers)
                    {
                        MessageQueue messageQueue = new MessageQueue { From_id = updatedate.CreateById, To_id = item.UserId, Type = 6, IsActive = 1, EntityId = updatedate.ProposalID, JobName = "Service Started", TaskName = "Service Started"+ " " + input.RigName + "-" + input.WellName + ":" + input.Job, CreatedDate = DateTime.Now };
                        db.MessageQueues.Add(messageQueue);
                    }
                }

                var result = await db.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ProjectBusiness UpdateUpCommingProjectsDetailsForOperator", null);
                return 0;
            }
        }

        public async Task<int> UpdateUpComingProjectsDetailsForOperator_V1(ProjectViewModel input , string rigId)
        {
            try
            {
                DateTime timeUtc = DateTime.UtcNow;
                TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);

                var updatedate = db.Projects.Where(x => x.ID.Equals(input.ProjectId)).FirstOrDefault();
                var serviceusers = db.TenantUsers.Where(x => x.TenantId == updatedate.ServiceCompID).Distinct().ToList();

                if (input.ProjectStatusName == "Start" && updatedate.ProjectStatus == 0)
                {
                    updatedate.ProjectStatus = (int)OngoingProjectStatusList.OnGoingProjects;
                    updatedate.ActualStart = cstTime;
                    updatedate.ModifyDate = cstTime;
                    foreach (var item in serviceusers)
                    {
                        MessageQueue messageQueue = new MessageQueue { From_id = updatedate.CreateById, To_id = item.UserId, Type = 6, IsActive = 1, EntityId = updatedate.ProposalID, JobName = "Service Started", RigId=rigId, TaskName = input.RigName + "-" + input.WellName + ":" + input.Job, CreatedDate = DateTime.Now };
                        db.MessageQueues.Add(messageQueue);
                    }

                }
                else if (input.ProjectStatusName == "Close")
                {
                    updatedate.ProjectStatus = (int)OngoingProjectStatusList.CloseProject;
                    updatedate.ActualEnd = cstTime;
                    updatedate.ModifyDate = cstTime;
                    foreach (var item in serviceusers)
                    {
                        MessageQueue messageQueue = new MessageQueue { From_id = updatedate.CreateById, To_id = item.UserId, Type = 6, IsActive = 1, EntityId = updatedate.ProposalID, JobName = "Service Closed", RigId = rigId, TaskName = input.RigName + "-" + input.WellName + ":" + input.Job, CreatedDate = DateTime.Now };
                        db.MessageQueues.Add(messageQueue);
                    }
                }
                else if (input.ProjectStatusName == "Suspend")
                {
                    updatedate.ProjectStatus = (int)OngoingProjectStatusList.SuspendProject;
                    updatedate.ModifyDate = cstTime;
                    foreach (var item in serviceusers)
                    {
                        MessageQueue messageQueue = new MessageQueue { From_id = updatedate.CreateById, To_id = item.UserId, Type = 6, IsActive = 1, EntityId = updatedate.ProposalID, JobName = "Service Suspend", RigId = rigId, TaskName = input.RigName + "-" + input.WellName + ":" + input.Job, CreatedDate = DateTime.Now };
                        db.MessageQueues.Add(messageQueue);
                    }
                }
                else if (input.ProjectStatusName == "Start" && updatedate.ProjectStatus == (int)OngoingProjectStatusList.SuspendProject)
                {
                    updatedate.ProjectStatus = (int)OngoingProjectStatusList.OnGoingProjects;
                    updatedate.ActualStart = cstTime;
                    updatedate.ModifyDate = cstTime;
                    foreach (var item in serviceusers)
                    {
                        MessageQueue messageQueue = new MessageQueue { From_id = updatedate.CreateById, To_id = item.UserId, Type = 6, IsActive = 1, EntityId = updatedate.ProposalID, JobName = "Service Started", RigId = rigId, TaskName = input.RigName + "-" + input.WellName + ":" + input.Job, CreatedDate = DateTime.Now };
                        db.MessageQueues.Add(messageQueue);
                    }
                }

                var result = await db.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ProjectBusiness UpdateUpComingProjectsDetailsForOperator_V1", null);
                return 0;
            }
        }

        public Task<bool> RemoveTechUserIdFromProject(string projectTechId)
        {
            try
            {
                var result = db.ProjectTechnicians.Find(projectTechId);
                if (result != null && result.ProjectTechStatus == 1)
                {
                    result.EndDate = DateTime.Now;
                    result.ProjectTechStatus = 0;
                    db.SaveChanges();
                }
                else if (result != null && result.ProjectTechStatus == 0)
                {
                    result.EndDate = DateTime.Now;
                    result.ProjectTechStatus = 1;
                    db.SaveChanges();
                }
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ProjectBusiness RemoveTechUserIdFromProject", null);
                return null;
            }
        }

        public Task<List<ProjectViewModel>> GetUpCommingProjectsForOperator(string tenantId)
        {
            try
            {
                var project = (from prj in db.Projects.Where(x => x.OprTenantID.Equals(tenantId))
                               join crf in db.CorporateProfile on prj.ServiceCompID equals crf.TenantId
                               join au in db.AuctionProposals on prj.ProposalID equals au.ProposalId
                               join well in db.WELL_REGISTERs on au.WellId equals well.well_id into welllj
                               from well in welllj.DefaultIfEmpty()
                               join rig in db.RigRegisters on well.RigID equals rig.Rig_Id into riglj
                               from rig in riglj.DefaultIfEmpty()

                               select new ProjectViewModel()
                               {
                                   ProjectId = prj.ID,
                                   ProjectCode = prj.ProjectID,
                                   OperatorCompanyName = crf.Name,
                                   ExpectedStartDate = prj.ProposedStartDate.Value,
                                   Title = prj.ProjectTitle,
                                   RigName = rig == null ? "N/A" : rig.Rig_Name,
                                   WellName = well == null ? "N/A" : well.wellname,
                                   Description = prj.ProjectDescription,
                                   ProjectStatusName = Convert.ToInt16(prj.ProjectStatus) == 0 ? "Upcoming" :
                                                        Convert.ToInt16(prj.ProjectStatus) == 1 ? "Active" :
                                                        Convert.ToInt16(prj.ProjectStatus) == 2 ? "Closed" :
                                                        Convert.ToInt16(prj.ProjectStatus) == 3 ? "Suspended" : string.Empty,
                                   Job = au.JobId ?? String.Empty
                               }).ToList();

                return Task.FromResult(project);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ProjectBusiness GetUpCommingProjectsForOperator", null);
                return null;
            }
        }

        public async Task<List<WellCheckListDetailModel>> EnsureAndGetWellCheckListForProject(string tenantId, string wellId)
        {
            try
            {
                List<WellCheckListDetailModel> wellCheckListDetail = new List<WellCheckListDetailModel>();
                if (wellId == "00000000-0000-0000-0000-000000000000")
                {
                    return wellCheckListDetail;
                }
                var wellCheckLists = db.WellCheckList.FirstOrDefault(x => x.TenantID == tenantId && x.WellId == wellId);

                //if (wellCheckLists != null)
                //{
                //    wellCheckListDetail = JsonConvert.DeserializeObject<List<WellCheckListDetailModel>>(wellCheckLists.CheckList);
                //}
                //else
                //{
                    //DWOP - Task data from DrillPlanDetails
                    var taskList = await db.DrillPlanDetails.ToListAsync();
                    wellCheckListDetail = await (from task in db.DrillPlanDetails
                                                 join drillwells in db.DrillPlanWells on new { task.DrillPlanId, task.DrillPlanWellsId } equals new { drillwells.DrillPlanId, drillwells.DrillPlanWellsId }
                                                 where drillwells.Wellid == wellId
                                                 select new WellCheckListDetailModel
                                                 {
                                                     WellTaskId = task.TaskId,
                                                     WellTaskName = task.TaskName,
                                                     Day = task.Day,
                                                     Depth = task.Depth,
                                                     Duration = 0,//task.Duration,
                                                     ServiceDuration = task.ServiceDuration,
                                                     Time = (System.TimeSpan?)task.ScheduleTime,
                                                     Type = Convert.ToInt32(task.IsSpecialServices),
                                                     IsBiddable = Convert.ToBoolean(task.IsBiddable),
                                                     CompletedDate = task.ActualStartDate
                                                 }).ToListAsync();
                    wellCheckListDetail = JsonConvert.DeserializeObject<List<WellCheckListDetailModel>>(JsonConvert.SerializeObject(wellCheckListDetail));
                    WellCheckList wellCheckList = new WellCheckList
                    {
                        CheckList = JsonConvert.SerializeObject(wellCheckListDetail),
                        TenantID = tenantId,
                        WellId = wellId,
                        WellChecklistId = Guid.NewGuid().ToString()
                    };
                    //if (wellId != null)
                    //{
                    //    db.WellCheckList.Add(wellCheckList);
                    //    db.SaveChanges();
                    //}
                //}

                return wellCheckListDetail;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ProjectBusiness EnsureAndGetWellCheckListForProject", null);
                return null;
            }
        }

        public Task<List<ProjectWellCheckListModel>> GetWellCheckListForProjects(string tenantId, string RigId)
        {
            try
            {
                var result = new List<ProjectWellCheckListModel>();

                var checkwellFilter = RigId == DLL.Constants.NoSpecificWellFilterKey;

                var wellCheckLists = (from wellcheck in db.WellCheckList
                                      join well in db.WellRegister on wellcheck.WellId equals well.well_id
                                      join rig in db.rig_register on wellcheck.RigId equals rig.Rig_id
                                      where wellcheck.TenantID.Equals(tenantId) && well.Prediction == true && rig.isActive == true && (rig.Rig_id == RigId && !checkwellFilter || checkwellFilter)
                                      select new { wellcheck, well, rig })
                                    .ToList();
                var corpProfiles = db.CorporateProfile.Select(x => new { Vendor = x.TenantId, VendorName = x.Name, Number = x.Phone }).ToList();
                foreach (var item in wellCheckLists)
                {
                    var checkListDetail = JsonConvert.DeserializeObject<List<ProjectWellCheckListModel>>(item.wellcheck.CheckList);
                    foreach (var item2 in checkListDetail)
                    {
                        item2.Well = item.well.wellname;
                        item2.WellId = item.well.well_id;
                        item2.WellCheckListId = $"{item.wellcheck.WellChecklistId}:{item2.WellTaskId}";
                        item2.TypeName = item2.Type == 1 ? "Task" : (item2.Type == 2 ? "Service" : (item2.Type == 3 ? "Special" : (item2.Type == 4 ? "Supply" : "Others")));
                        item2.VendorNumber = (item2.Vendor == null || item2.Vendor == "undefined") ? null : corpProfiles.FirstOrDefault(x => x.Vendor == item2.Vendor).Number;
                        item2.Vendor = item2.Vendor == null ? "null2" : item2.Vendor;
                        item2.StageType = item2.StageType == null ? "N/A" : item2.StageType.Trim();
                        if (item.rig != null)
                        {
                            item2.RigId = (item.rig.Rig_id == null) ? "N/A" : item.rig.Rig_id;
                            item2.RigName = item.rig.Rig_Name == null ? "N/A" : item.rig.Rig_Name;
                        }
                        else
                        {
                            item2.RigId = "N/A";
                            item2.RigName = "N/A";
                        }
                    }
                    result.AddRange(checkListDetail);
                }


                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ProjectBusiness GetWellCheckListForProjects", null);
                return null;
            }
        }

        public async Task CreateCheckListItem(string tenantId, CheckListTaskModel input)
        {
            try
            {
                if (string.IsNullOrEmpty(input.WellId))
                    return;

                var wellCheckList = db.WellCheckList.FirstOrDefault(x => x.TenantID == tenantId && x.WellId == input.WellId);

                if (wellCheckList != null)
                {
                    if (!string.IsNullOrEmpty(wellCheckList.CheckList))
                    {
                        var wellCheckListDetail = JsonConvert.DeserializeObject<List<WellCheckListDetailModel>>(wellCheckList.CheckList);

                        var checkItem = new WellCheckListDetailModel
                        {
                            WellTaskId = input.TaskId,
                            WellTaskName = input.Name,
                            Depth = input.Depth,
                            Day = input.Day,
                            Duration = input.Duration,
                            Time = (System.TimeSpan?)input.ScheduleTime,
                            IsBiddable = input.IsBiddable,
                            Type = Convert.ToInt32(input.IsSpecialServices),
                            StageType=input.StageName,
                            ServiceCategory = input.CategoryName
                                          
                        };

                        wellCheckListDetail.Add(checkItem);

                        var newCheckListString = JsonConvert.SerializeObject(wellCheckListDetail);

                        wellCheckList.CheckList = newCheckListString;
                        wellCheckList.RigId = input.RigId;
                      
                    }
                    else
                    {
                        var weltasklist = new List<WellCheckListDetailModel>
                        {
                            new WellCheckListDetailModel
                            {
                                WellTaskId = input.TaskId,
                                WellTaskName = input.Name,
                                Depth = input.Depth,
                                Day = input.Day,
                                Duration = input.Duration,
                                Time =(System.TimeSpan?)input.ScheduleTime,
                                IsBiddable = input.IsBiddable,
                                Type = Convert.ToInt32(input.IsSpecialServices),
                                StageType=input.StageName,
                                ServiceCategory=input.CategoryName                            
                            }
                        };

                        var newCheckListString = JsonConvert.SerializeObject(weltasklist);

                        wellCheckList.CheckList = newCheckListString;
                        wellCheckList.RigId = input.RigId;
                       
                    }
                }
                else
                {
                    var newwellchecklist = new WellCheckList
                    {
                        TenantID = tenantId,
                        WellId = input.WellId,
                        WellChecklistId = Guid.NewGuid().ToString(),
                        RigId = input.RigId
                      
                    };

                    var weltasklist = new List<WellCheckListDetailModel>
                        {
                            new WellCheckListDetailModel
                            {
                                WellTaskId = input.TaskId,
                                WellTaskName = input.Name,
                                Depth = input.Depth,
                                Day = input.Day,
                                Duration = input.Duration,
                                Time = (System.TimeSpan?)input.ScheduleTime,
                                IsBiddable = input.IsBiddable,
                                Type = Convert.ToInt32(input.IsSpecialServices),
                                StageType=input.StageName,
                                ServiceCategory=input.CategoryName
                            }
                        };

                    var newCheckListString = JsonConvert.SerializeObject(weltasklist);

                    newwellchecklist.CheckList = newCheckListString;

                    db.WellCheckList.Add(newwellchecklist);
                }

                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ProjectBusiness CreateCheckListItem", null);
            }
        }

        public async Task RemoveCheckListItem(string tenantId, string wellId, string wellCheckListId, string taskId)
        {
            try
            {
                var wellCheckList = db.WellCheckList.FirstOrDefault(x => x.TenantID == tenantId && x.WellId == wellId && x.WellChecklistId == wellCheckListId);

                if (wellCheckList != null && !string.IsNullOrEmpty(wellCheckList.CheckList))
                {
                    var wellCheckListDetail = JsonConvert.DeserializeObject<List<WellCheckListDetailModel>>(wellCheckList.CheckList);
                    var checkListItem = wellCheckListDetail.FirstOrDefault(x => x.WellTaskId == taskId);

                    if (checkListItem != null)
                    {
                        wellCheckListDetail.Remove(checkListItem);

                        var newCheckListString = JsonConvert.SerializeObject(wellCheckListDetail);

                        wellCheckList.CheckList = newCheckListString;

                        await db.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ProjectBusiness CreateCheckListItem", null);
            }
        }

        private Task<List<WellCheckListDetailModel>> EnsureWellCheckListDetail(string projectId, string tenantId)
        {
            try
            {
                var result = new List<WellCheckListDetailModel>();

                var project = db.Projects.FirstOrDefault(x => x.ID == projectId);

                if (project != null)
                {
                    var wellType = db.WellRegister.FirstOrDefault(x => x.well_id == project.WellID);

                    if (wellType != null && !string.IsNullOrEmpty(wellType.welltype_id))
                    {
                        //DWOP - Task data from DrillPlanDetails
                        result = (from task in db.DrillPlanDetails
                                  join welltask in db.WellTasks.Where(x => x.WellTypeId == wellType.welltype_id) on task.TaskId equals welltask.TaskId
                                  select new WellCheckListDetailModel
                                  {
                                      WellTaskId = welltask.WellTaskId,
                                      WellTaskName = task.TaskName,
                                      Day = task.Day,
                                      Depth = task.Depth,
                                      Duration = 0,//task.Duration,
                                      Time = (System.TimeSpan?)task.ScheduleTime

                                  }).ToList();
                    }
                }

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ProjectBusiness EnsureWellCheckListDetail", null);
                return null;
            }
        }

        public async Task<string> UpdateWellCheckStatusListForProject(string tenantId, string wellId, List<string> checkIds)
        {
            var result = "";

            var wellCheckLists = db.WellCheckList.Where(x => x.TenantID == tenantId && x.WellId == wellId).ToList();

            try
            {
                for (var i = 0; i < wellCheckLists.Count; i++)
                {
                    var checkItem = wellCheckLists[i].CheckList;

                    var wellCheckListDetail = JsonConvert.DeserializeObject<List<WellCheckListDetailModel>>(checkItem);

                    for (var j = 0; j < wellCheckListDetail.Count; j++)
                    {
                        if (checkIds.Contains(wellCheckListDetail[j].WellTaskId))
                        {
                            wellCheckListDetail[j].CheckListStatus = 1;
                            wellCheckListDetail[j].CompletedDate = DateTime.Now;
                        }
                        else
                        {
                            wellCheckListDetail[j].CheckListStatus = 0;
                        }
                    }

                    var newcheckList = JsonConvert.SerializeObject(wellCheckListDetail);
                    wellCheckLists[i].CheckList = newcheckList;
                }

                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ProjectBusiness UpdateWellCheckStatusListForProject", null);
                result = ex.Message + ex.StackTrace;
            }

            return result;
        }

        public async Task<string> UpdateWellCheckStatusListForProjects(string tenantId, List<string> checkIds)
        {
            try
            {
                var result = "";

                var wellCheckLists = db.WellCheckList.Where(x => x.TenantID == tenantId).ToList();

                foreach (var checkId in checkIds)
                {
                    try
                    {
                        var splitIds = checkId.Split(':', StringSplitOptions.RemoveEmptyEntries);

                        var wellCheckList = wellCheckLists.FirstOrDefault(x => x.WellChecklistId == splitIds[0]);

                        if (wellCheckList != null)
                        {
                            var wellCheckListDetail = JsonConvert.DeserializeObject<List<WellCheckListDetailModel>>(wellCheckList.CheckList);

                            for (var j = 0; j < wellCheckListDetail.Count; j++)
                            {
                                if (wellCheckListDetail[j].WellTaskId == splitIds[1])
                                {
                                    wellCheckListDetail[j].CheckListStatus = 1;
                                    wellCheckListDetail[j].CompletedDate = DateTime.Now;
                                    if (splitIds[2] != "null")
                                    {
                                        wellCheckListDetail[j].Vendor = splitIds[2];
                                    }

                                    break;
                                }
                            }

                            var newcheckList = JsonConvert.SerializeObject(wellCheckListDetail);
                            wellCheckList.CheckList = newcheckList;
                        }
                    }
                    catch (Exception ex)
                    {
                        result += ex.Message + ex.StackTrace;
                    }
                }

                await db.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ProjectBusiness UpdateWellCheckStatusListForProjects", null);
                return null;
            }
        }

        public async Task<List<ProjectWellCheckListModel>> UpdateWellCheckStatusListForProjects(string tenantId, IEnumerable<ProjectWellCheckListModel> checkListModels)
        {
            try
            {
                var result = "";
                var wellIds = checkListModels.Select(x => x.WellId).Distinct().ToArray();
                var wellCheckLists = db.WellCheckList.Where(x => x.TenantID == tenantId && wellIds.Contains(x.WellId)).ToList();
                var corpProfiles = db.CorporateProfile.Select(x => new { Vendor = x.TenantId, VendorName = x.Name, Number = x.Phone }).ToList();
                List<ProjectWellCheckListModel> newProjectWellCheckList = new List<ProjectWellCheckListModel>();
                foreach (var checkId in checkListModels)
                {
                    try
                    {
                        var splitIds = checkId.WellCheckListId.Split(':', StringSplitOptions.RemoveEmptyEntries);

                        var wellCheckList = wellCheckLists.FirstOrDefault(x => x.WellChecklistId == splitIds[0]);
                        checkId.VendorNumber = checkId.Vendor == "null2" ? null : corpProfiles.FirstOrDefault(x => x.Vendor == checkId.Vendor).Number;
                        newProjectWellCheckList.Add(checkId);
                        if (wellCheckList != null)
                        {
                            var wellCheckListDetail = JsonConvert.DeserializeObject<List<WellCheckListDetailModel>>(wellCheckList.CheckList);

                            for (var j = 0; j < wellCheckListDetail.Count; j++)
                            {
                                var a = checkId.WellTaskId;
                                if (wellCheckListDetail[j].WellTaskId == checkId.WellTaskId && wellCheckListDetail[j].ServiceCategory == checkId.ServiceCategory && wellCheckListDetail[j].Depth == checkId.Depth && /*wellCheckListDetail[j].StageType == checkId.StageType &&*/ wellCheckListDetail[j].WellTaskName == checkId.WellTaskName)
                                {
                                    wellCheckListDetail[j].CheckListStatus = checkId.CheckListStatus;
                                    wellCheckListDetail[j].CompletedDate = DateTime.Now;
                                    wellCheckListDetail[j].Vendor = checkId.Vendor == "null2" ? null : checkId.Vendor;

                                }

                                var newcheckList = JsonConvert.SerializeObject(wellCheckListDetail);
                                wellCheckList.CheckList = newcheckList;
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                        customErrorHandler.WriteError(ex, "ProjectBusiness UpdateWellCheckStatusListForProjects", null);
                        result += ex.Message + ex.StackTrace;
                    }
                }

                await db.SaveChangesAsync();

                return newProjectWellCheckList;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ProjectBusiness UpdateWellCheckStatusListForProjects", null);
                return null;
            }
        }

        public async Task<List<ProjectViewSRVModel>> GetProjectsByWellId(string tenantId, string wellId)
        {
            try
            {
                var checkwellFilter = wellId == DLL.Constants.NoSpecificWellFilterKey;
                var project =new List<ProjectViewSRVModel>();
                if (wellId != null)
                {
                    //DWOP - Task data from DrillPlanDetails
                    project = await (from prj in db.Projects
                                         join au in db.AuctionProposals on prj.ProposalID equals au.ProposalId
                                         join task in db.DrillPlanDetails on au.JobId equals task.TaskId //into taskLj
                                         //from task in taskLj.DefaultIfEmpty()
                                         where prj.OprTenantID.Equals(tenantId) && (prj.ProjectStatus == (int)ProjectStatusList.OnGoingProjects) && (au.WellId.Equals(wellId) /*&& !checkwellFilter || checkwellFilter*/)
                                         select new ProjectViewSRVModel()
                                         {
                                             Title = prj.ProjectTitle,
                                             ProjectId = prj.ID,
                                             JobName = task.TaskName
                                         }).ToListAsync();
                }
                return project;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ProjectBusiness GetProjectsByWellId", null);
                return null;
            }
        }

        public async Task<List<ProjectViewSRVModel>> GetProjectsByWellIdSRV(string tenantId, string wellId)
        {
            try
            {
                var checkwellFilter = wellId == DLL.Constants.NoSpecificWellFilterKey;
                //DWOP - Task data from DrillPlanDetails
                var project = await (from prj in db.Projects
                                     join au in db.AuctionProposals on prj.ProposalID equals au.ProposalId
                                     join task in db.DrillPlanDetails on au.JobId equals task.TaskId into taskLj
                                     from task in taskLj.DefaultIfEmpty()
                                     where prj.ServiceCompID.Equals(tenantId) && (prj.ProjectStatus == (int)ProjectStatusList.OnGoingProjects) && (au.WellId.Equals(wellId) && !checkwellFilter || checkwellFilter)
                                     select new ProjectViewSRVModel()
                                     {
                                         Title = prj.ProjectTitle,
                                         ProjectId = prj.ID,
                                         JobName = task.TaskName
                                     }).ToListAsync();
                return project;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ProjectBusiness GetProjectsByWellIdSRV", null);
                return null;
            }
        }

        public async Task<List<ProjectViewSRVModel>> GetUpCommingProjectsSRV(string tenantId, string operId)
        {
            try
            {
                //DWOP - Task data from DrillPlanDetails
                var nospecificOper = operId == DLL.Constants.NoSpecificWellFilterKey;
                var project = await (from prj in db.Projects.Where(x => x.ServiceCompID.Equals(tenantId))
                                     join crf in db.CorporateProfile on prj.OprTenantID equals crf.TenantId
                                     join au in db.AuctionProposals on prj.ProposalID equals au.ProposalId
                                     join well in db.WELL_REGISTERs on au.WellId equals well.well_id into welllj
                                     from well in welllj.DefaultIfEmpty()
                                     join rig in db.RigRegisters on well.RigID equals rig.Rig_Id into riglj
                                     from rig in riglj.DefaultIfEmpty()
                                     join task in db.DrillPlanDetails on au.JobId equals task.TaskId into T1
                                     from TaskResult in T1.DefaultIfEmpty()
                                     where crf.TenantId == operId && !nospecificOper || nospecificOper
                                     select new ProjectViewSRVModel()
                                     {
                                         ProjectId = prj.ID,
                                         ProjectCode = prj.ProjectID,
                                         OperatorCompanyName = crf.Name,
                                         OperatorTenantId = crf.TenantId,
                                         ExpectedStartDate = prj.ProposedStartDate.Value,
                                         Title = prj.ProjectTitle,
                                         WellName = well == null ? "N/A" : well.wellname,
                                         RigName = rig == null ? "N/A" : rig.Rig_Name,
                                         ProjectDuration = au.ProjectDuration,
                                         ProjectStatusName = Convert.ToInt16(prj.ProjectStatus) == 0 ? "Upcoming" :
                                                              Convert.ToInt16(prj.ProjectStatus) == 1 ? "Active" :
                                                              Convert.ToInt16(prj.ProjectStatus) == 2 ? "Completed" :
                                                              Convert.ToInt16(prj.ProjectStatus) == 3 ? "Suspended" : string.Empty,
                                         Job = TaskResult.TaskName,
                                         ModifyDate=prj.ModifyDate,
                                         Depth = TaskResult.Depth,
                                         ProjectStatus = prj.ProjectStatus
                                     }).OrderBy(o => o.ProjectStatus).ToListAsync();

                var resultAgg = project.GroupBy(x => x.ProjectId).Select(g => g.First()).ToList();

                return resultAgg;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ProjectBusiness GetUpCommingProjectsSRV", null);
                return null;
            }
        }

        public List<ProjectViewSRVModel> GetUpComingProjectsSRV_Chat(string tenantId, string operId)
        {
            try
            {
                //DWOP - Task data from DrillPlanDetails
                var nospecificOper = operId == DLL.Constants.NoSpecificWellFilterKey;
                var project = (from prj in db.Projects.Where(x => x.ServiceCompID.Equals(tenantId) /*&& x.ProjectStatus != 3*/)
                                     join crf in db.CorporateProfile on prj.OprTenantID equals crf.TenantId
                                     join au in db.AuctionProposals on prj.ProposalID equals au.ProposalId
                                     join well in db.WELL_REGISTERs on au.WellId equals well.well_id into welllj
                                     from well in welllj.DefaultIfEmpty()
                                     join rig in db.RigRegisters on well.RigID equals rig.Rig_Id into riglj
                                     from rig in riglj.DefaultIfEmpty()
                                     join task in db.DrillPlanDetails on au.JobId equals task.TaskId into T1
                                     from TaskResult in T1.DefaultIfEmpty()
                                     where prj.ProjectStatus == 0 && crf.TenantId == operId && (!nospecificOper || nospecificOper)
                                     select new ProjectViewSRVModel()
                                     {
                                         ProjectId = prj.ID,
                                         ProjectCode = prj.ProjectID,
                                         OperatorCompanyName = crf.Name,
                                         OperatorTenantId = crf.TenantId,
                                         ExpectedStartDate = prj.ProposedStartDate.Value,
                                         Title = prj.ProjectTitle,
                                         WellName = well == null ? "N/A" : well.wellname,
                                         RigName = rig == null ? "N/A" : rig.Rig_Name,                                        
                                         ProjectStatusName = Convert.ToInt16(prj.ProjectStatus) == 0 ? "Upcoming" :
                                                              Convert.ToInt16(prj.ProjectStatus) == 1 ? "Active" :
                                                              Convert.ToInt16(prj.ProjectStatus) == 2 ? "Completed" :
                                                              Convert.ToInt16(prj.ProjectStatus) == 3 ? "Suspended" : string.Empty,
                                         Job = TaskResult.TaskName,
                                         ModifyDate = prj.ModifyDate
                                     }).ToList();

                project = project.OrderBy(x => x.ExpectedStartDate).ToList();
                return project;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ProjectBusiness GetUpComingProjectsSRV_Chat", null);
                return null;
            }
        }

        public List<ProjectViewModel> GetUpComingProjects_Chat(string tenantId, string serviceTenantId)
        {
            try
            {
                var project = (from prj in db.Projects.Where(x => x.OprTenantID.Equals(tenantId))
                               join crf in db.CorporateProfile on prj.ServiceCompID equals crf.TenantId
                               join au in db.AuctionProposals on prj.ProposalID equals au.ProposalId
                               join well in db.WELL_REGISTERs on au.WellId equals well.well_id into welllj
                               from well in welllj.DefaultIfEmpty()
                               join rig in db.RigRegisters on well.RigID equals rig.Rig_Id into riglj
                               from rig in riglj.DefaultIfEmpty()
                               where prj.ServiceCompID == serviceTenantId && Convert.ToInt16(prj.ProjectStatus) == 0 
                               select new ProjectViewModel()
                               {
                                   ProjectId = prj.ID,
                                   ProjectCode = prj.ProjectID,
                                   OperatorCompanyName = crf.Name,
                                   ExpectedStartDate = prj.ProposedStartDate.Value,
                                   Title = prj.ProjectTitle,
                                   RigName = rig == null ? "N/A" : rig.Rig_Name,
                                   WellName = well == null ? "N/A" : well.wellname,
                                   Description = prj.ProjectDescription,
                                   ProjectStatusName = Convert.ToInt16(prj.ProjectStatus) == 0 ? "Upcoming" :
                                                        Convert.ToInt16(prj.ProjectStatus) == 1 ? "Active" :
                                                        Convert.ToInt16(prj.ProjectStatus) == 2 ? "Closed" :
                                                        Convert.ToInt16(prj.ProjectStatus) == 3 ? "Suspended" : string.Empty,
                                   Job = au.JobId ?? String.Empty
                               }).ToList();

                project = project.OrderBy(x => x.ExpectedStartDate).ToList();
                return project;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ProjectBusiness GetUpComingProjects_Chat", null);
                return null;
            }
        }

        /// <summary>
        /// Phase II Changes - 02/04/2021 - GetStagingData
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="wellId"></param>
        /// <returns></returns>
        public async Task<List<StagingChartModel>> GetStagingData(string tenantId, string wellId)
        {
            List<StagingTasksModel> stagingTasksList = new List<StagingTasksModel>();
            List<StagingChartModel> stagingChartDaysList = new List<StagingChartModel>();
            StagingTasksModel stagingTaskListItem = new StagingTasksModel();
            StagingChartModel stagingChartItem = new StagingChartModel();
            try
            {

                stagingTasksList = GetStagingTasks(tenantId, wellId);
                //with Min Start Date (Running Date) and Max(End Date) from each Stage
                var stagingDataSet1 = stagingTasksList.GroupBy(r => r.StageId)
                    .Select(grp => new
                    {
                        StageNo = grp.Key,
                        StartDate = grp.Min(t => t.RunningDate),
                        EndDate = grp.Max(t => t.EndDate)
                    }).ToList();

                //to create dataset with Day for each stage
                if (stagingDataSet1.Count > 0)
                {
                    int j = 1;
                    //loop through the stagingDataSet to find the running day and push into stagingChartDaysList
                    foreach (var item in stagingDataSet1)
                    {
                        int i = 0;
                        DateTime startDate = Convert.ToDateTime(item.StartDate);
                        DateTime endDate = Convert.ToDateTime(item.EndDate);
                        for(i=1;i<=(endDate.Date - startDate.Date).TotalDays+1; i++)
                        {
                            stagingChartItem = new StagingChartModel();
                            stagingChartItem.Stage = item.StageNo;
                            stagingChartItem.Day = j;
                            stagingChartDaysList.Add(stagingChartItem);
                            j = j + 1;
                        }
                    }
                }

                return await Task.FromResult(stagingChartDaysList);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ProjectBusiness GetStagingData", null);
                return null;
            }
        }

        public async Task<StagingTasksModel> GetCasingStringAndCurrentStage(string tenantId, string wellId)
        {
            List<StagingTasksModel> stagingTasksList = new List<StagingTasksModel>();
            List<StagingChartModel> stagingChartDaysList = new List<StagingChartModel>();
            StagingTasksModel stagingTaskListItem = new StagingTasksModel();
            StagingChartModel stagingChartItem = new StagingChartModel();
            try
            {

                stagingTasksList = GetStagingTasks(tenantId, wellId);
                if (stagingTasksList.Count > 0)
                {
                    var currentStage = stagingTasksList[stagingTasksList.Count - 1];
                    if(currentStage!=null)
                        stagingTaskListItem.Stage = currentStage.Stage;

                    var lastCasingString = stagingTasksList.Where(x => x.ServiceCategory == "CASING").OrderByDescending(x=>x.RunningDate).FirstOrDefault();
                    if (lastCasingString != null)
                    {
                        if (lastCasingString.RunningDate != null)
                        {
                            stagingTaskListItem.Task = lastCasingString.Task + " " + Convert.ToDateTime(lastCasingString.RunningDate).ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            stagingTaskListItem.Task = lastCasingString.Task;
                        }
                    }
                        
                }                
                return await Task.FromResult(stagingTaskListItem);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ProjectBusiness GetCasingStringAndCurrentStage", null);
                return null;
            }
        }

        private List<StagingTasksModel> GetStagingTasks(string tenantId, string wellId){
            List<StagingTasksModel> stagingTasksList = new List<StagingTasksModel>();
            StagingTasksModel stagingTaskListItem = new StagingTasksModel();
            try
            {
                
                //Data from Project (Biddable)
                var checkwellFilter = wellId == DLL.Constants.NoSpecificWellFilterKey;
                //DWOP - Task data from DrillPlanDetails
                var closedProject = (from prj in db.Projects
                                     join au in db.AuctionProposals on prj.ProposalID equals au.ProposalId
                                     join task in db.DrillPlanDetails on au.JobId equals task.TaskId
                                     join stg in db.Stages on task.StageId equals stg.Id
                                     join catg in db.serviceCategories on au.Category equals catg.ServiceCategoryId
                                     where prj.ActualEnd != null && prj.ServiceCompID.Equals(tenantId) && (prj.ProjectStatus == (int)ProjectStatusList.CloseProject) && (au.WellId.Equals(wellId))
                                     select new StagingTasksModel()
                                     {
                                         Stage = stg.Name,
                                         Day = task.Day,
                                         EndDate = prj.ActualEnd,
                                         RunningDate = prj.ActualStart,
                                         Task = task.TaskName,
                                         ServiceCategory = catg.Name
                                     }).OrderBy(x => x.RunningDate).ToList();

                //var closedProject = (from prj in db.DrillPlanDetails
                //                     join plnwells in db.DrillPlanWells on new { prj.DrillPlanId,prj.DrillPlanWellsId} equals  new { plnwells.DrillPlanId,plnwells.DrillPlanWellsId}
                //                     join stg in db.Stages on prj.StageId equals stg.Id
                //                     join catg in db.serviceCategories on prj.CategoryId equals catg.ServiceCategoryId
                //                     where prj.PlanStartDate != null && plnwells.Wellid.Equals(wellId)
                //                     select new StagingTasksModel()
                //                     {
                //                         Stage = stg.Name,
                //                         Day = prj.Day,
                //                         EndDate = prj.ActualFinishedDate,
                //                         RunningDate = prj.ActualStartDate,
                //                         Task = prj.TaskName,
                //                         ServiceCategory = catg.Name
                //                     }).OrderBy(x=>x.RunningDate).ToList();

                if (closedProject != null)
                {
                    if (closedProject.Count > 0)
                    {
                        foreach(var item in closedProject)
                        {
                            stagingTaskListItem = new StagingTasksModel();
                            stagingTaskListItem.Stage = item.Stage;
                            stagingTaskListItem.Day = item.Day;
                            stagingTaskListItem.EndDate = item.EndDate;
                            stagingTaskListItem.RunningDate = item.RunningDate;
                            stagingTaskListItem.Task = item.Task;
                            stagingTaskListItem.StageId = Convert.ToInt32(item.Stage.ToString().Substring(0, item.Stage.IndexOf(":") - 1));
                            stagingTaskListItem.ServiceCategory = item.ServiceCategory;
                            stagingTasksList.Add(stagingTaskListItem);
                        }
                        
                    }
                }

                ////DWOP - Task data from DrillPlanDetails
                var openProject = (from prj in db.Projects
                                   join au in db.AuctionProposals on prj.ProposalID equals au.ProposalId
                                   join task in db.DrillPlanDetails on au.JobId equals task.TaskId
                                   join stg in db.Stages on task.StageId equals stg.Id
                                   join catg in db.serviceCategories on au.Category equals catg.ServiceCategoryId
                                   where prj.ActualEnd == null && prj.ServiceCompID.Equals(tenantId) && (prj.ProjectStatus == (int)ProjectStatusList.OnGoingProjects) && (au.WellId.Equals(wellId))
                                   select new StagingTasksModel()
                                   {
                                       Stage = stg.Name,
                                       Day = task.Day,
                                       EndDate = prj.ActualEnd,
                                       RunningDate = prj.ActualStart,
                                       Task = task.TaskName,
                                       ServiceCategory = catg.Name
                                   }).OrderBy(x => x.RunningDate).ToList();

                if (openProject != null)
                {
                    if (openProject.Count > 0)
                    {
                        foreach (var item in openProject)
                        {
                            stagingTaskListItem = new StagingTasksModel();
                            stagingTaskListItem.Stage = item.Stage;
                            stagingTaskListItem.Day = item.Day;
                            stagingTaskListItem.EndDate = DateTime.Now;
                            stagingTaskListItem.RunningDate = item.RunningDate;
                            stagingTaskListItem.Task = item.Task;
                            stagingTaskListItem.StageId = Convert.ToInt32(item.Stage.ToString().Substring(0, item.Stage.IndexOf(":") - 1));
                            stagingTaskListItem.ServiceCategory = item.ServiceCategory;
                            stagingTasksList.Add(stagingTaskListItem);
                        }
                    }
                }
                return stagingTasksList;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ProjectBusiness GetStagingTasks", null);
                return stagingTasksList;
            }           
        }

        public enum ProjectStatusList
        {
            UpCommingProject = 0,
            OnGoingProjects = 1,
            CloseProject = 2,
            SuspendProject = 3,
        }

      
    }
}