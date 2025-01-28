using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Models;
using WellAI.Advisor.Model.ServiceCompany.Models;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Identity;
using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Repository;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using WellAI.Advisor.BLL.IBusiness;
using WellAI.Advisor.Areas.Identity;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.Model.OperatingCompany.Models;
using Newtonsoft.Json;
namespace WellAI.Advisor.Areas.ServiceCompany.Controllers
{
    [Area("ServiceCompany")]
    public class CRMController : BaseController
    {
        private readonly ILogger<CRMController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly WebAIAdvisorContext db;
        private readonly TenantServiceDbContext _servicedb;
        public CRMController(WebAIAdvisorContext dbContext, SignInManager<WellIdentityUser> signInManager,
                             RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager, ILogger<CRMController> logger, TenantServiceDbContext servicedb)
        : base(userManager, dbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            db = dbContext;
            _servicedb = servicedb;
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

                // Count of Contacts
                //int Contactcount = _servicedb.CrmContacts.Where(x => x.ContactId != 0 && x.CompanyId != 0).Count();  // count: 2
                int Contactcount = 0;
                var Contactdetails = (from crm in _servicedb.CrmContacts
                                      join company in _servicedb.CrmCompanies on crm.CompanyId equals company.CompanyId
                                      select new Model.ServiceCompany.Models.CrmModelSRVContact
                                      {
                                          ContactID = crm.ContactId,
                                          InstanceID = crm.InstanceId,
                                          CompanyID = crm.CompanyId,
                                          FirstName = crm.FirstName,
                                          LastName = crm.LastName,
                                          Address1 = crm.Address1,
                                          Address2 = crm.Address2,
                                          City = crm.City,
                                          StateRegion = crm.StateRegion,
                                          Country = crm.Country,
                                          PostalCode = crm.PostalCode,
                                          Phone = crm.Phone,
                                          MobilePhone = crm.MobilePhone,
                                          Fax = crm.Fax,
                                          Email = crm.Email,
                                          Title = crm.Title,
                                          CompanyName = company.Name,
                                          TenantID = company.TenantId
                                      }
                                 ).ToList();

                Contactcount = Contactdetails.Count();
                ViewData["TotalContacts"] = Contactcount.ToString();
                // Count of Company
                //int Companycount = _servicedb.CrmCompanies.Where(x => x.CompanyId != 0).Count();  // count: 2
                //ViewData[" "] = Companycount.ToString();
                //GetCompanyList();
                //return View();

                //ViewData["TotalContacts"] = Contactcount.ToString(); 
                //// Count of Company
                int Companycount = _servicedb.CrmCompanies.Where(x => x.CompanyId != 0 ).Count();  // count: 2
                ViewData["TotalCompany"] = Companycount.ToString();
                GetCompanyList();
                return View();
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CRM Index", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public async Task<IActionResult> contactCounts()
        {
            // Count of Contacts
            int Contactcount = _servicedb.CrmContacts.Where(x => x.ContactId != 0 && x.CompanyId != 0).Count();  // count: 2
            ViewData["TotalContacts"] = Contactcount.ToString();
            return await Task.FromResult(Json(Contactcount));
        }
        public async Task<IActionResult> companyCounts()
        {
            // Count of Company
            int Companycount = _servicedb.CrmCompanies.Where(x => x.CompanyId != 0 ).Count();  // count: 2
            ViewData["TotalCompany"] = Companycount.ToString();
            return await Task.FromResult(Json(Companycount));
        }
            public IActionResult Error()
        {
            return View();
        }

        private bool GetComponentsBasedOnRole()
        {
            var TenantId = WellAIAppContext.Current.Session.GetString("TenantId");
            var userIdentity = (ClaimsIdentity)User.Identity;
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();

            List<string> rolesName = (from rl in roles
                                      select rl.Value
                                 ).ToList();

            IRolePermissionBusiness rolePermissionBusiness = new RolePermissionBusiness(db, _roleManager, _userManager);
            var roleResult = rolePermissionBusiness.GetRolesByNames(rolesName);
            var rolesResult = roleResult.Result;
            if (roleResult != null)
            {
                List<string> roleIds = (from rl in rolesResult
                                        select rl.Id
                                        ).ToList();
                return rolePermissionBusiness.GetSRVComponentBasedOnRolesList(roleIds, "CRM",TenantId);
            }
            else
            {
                return false;
            }
        }


        public async Task<IActionResult> Profile_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var CompanyDetails = (from crm in _servicedb.CrmCompanies
                                      select new Model.ServiceCompany.Models.CrmModelSRV
                                      {
                                          CompanyID = crm.CompanyId,
                                          InstanceID = crm.InstanceId,
                                          UserID = crm.UserId,
                                          Name = crm.Name,
                                          Address1 = crm.Address1,
                                          Address2 = crm.Address2,
                                          City = crm.City,
                                          StateRegion = crm.StateRegion,
                                          Country = crm.Country,
                                          PostalCode = crm.PostalCode,
                                          Phone = crm.Phone,
                                          MobilePhone = crm.MobilePhone,
                                          Fax = crm.Fax,
                                          Email = crm.Email,
                                          Website = crm.Website,
                                          TenantID = crm.TenantId
                                      }
                                   ).ToList();
               return await Task.FromResult(Json(CompanyDetails.ToDataSourceResult(request)));
           }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CRM Profile_Read", User.Identity.Name);
                string returnUrl = @"/ServiceDashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public async Task<IActionResult> ContactProfile_Read([DataSourceRequest] DataSourceRequest request)
        {
            try
            {   
                var ContactDetails = (from crm in _servicedb.CrmContacts
                                      join company in _servicedb.CrmCompanies on crm.CompanyId equals company.CompanyId
                                      select new Model.ServiceCompany.Models.CrmModelSRVContact
                                      {
                                          ContactID = crm.ContactId,
                                          InstanceID = crm.InstanceId,
                                          CompanyID = crm.CompanyId,
                                          FirstName = crm.FirstName,
                                          LastName = crm.LastName,
                                          Address1 = crm.Address1,
                                          Address2 = crm.Address2,
                                          City = crm.City,
                                          StateRegion = crm.StateRegion,
                                          Country = crm.Country,
                                          PostalCode = crm.PostalCode,
                                          Phone = crm.Phone,
                                          MobilePhone = crm.MobilePhone,
                                          Fax = crm.Fax,
                                          Email = crm.Email,
                                          Title = crm.Title,
                                          CompanyName = company.Name,
                                          TenantID = company.TenantId
                                      }
                                   ).ToList();
                return await Task.FromResult(Json(ContactDetails.ToDataSourceResult(request)));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CRM Profile_Read", User.Identity.Name);
                string returnUrl = @"/ServiceDashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public void GetCompanyList()
        {
            List<Model.ServiceCompany.Models.CrmModelSRV> result = new List<Model.ServiceCompany.Models.CrmModelSRV>();
            result = (from crm in _servicedb.CrmCompanies
                      select new Model.ServiceCompany.Models.CrmModelSRV
                      {
                          CompanyID = crm.CompanyId,
                          Name = crm.Name
                      }).ToList();

            ViewData["CRMCompanyList"] = result;
        }
        public async Task<IActionResult> ContactProfile_Create([DataSourceRequest] DataSourceRequest request, CrmModelSRVContact input)
        {
            try
            {
                    if (input != null)
                    {
                   var selectCompany = _servicedb.CrmCompanies.Where(x => x.Name == input.CompanyName).FirstOrDefault();
                    var contact = new WellAI.Advisor.Model.Tenant.Models.CrmContacts
                    {
                        InstanceId = 0,
                        CompanyId = selectCompany.CompanyId,
                        FirstName = input.FirstName,
                        LastName = input.LastName,
                        Title = input.Title,
                        Address1 = input.Address1,
                        Address2 = input.Address2,
                        City = input.City,
                        StateRegion = input.StateRegion,
                        PostalCode = input.PostalCode,
                        Phone = input.Phone,
                        MobilePhone = input.MobilePhone,
                        Email = input.Email
                    };
                    _servicedb.CrmContacts.Add(contact);
                    await _servicedb.SaveChangesAsync();
                }
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "AddContact ContactProfile_Create", User.Identity.Name);
                return LocalRedirect(@"/Dashboard/Error");
            }
        }
        public async Task<IActionResult> CompanyContactProfile_Create([DataSourceRequest] DataSourceRequest request, CrmModelSRVContact input)
        {
            try
            {
                if (ModelState.IsValid && input != null)
                {
                    var contact = new WellAI.Advisor.Model.Tenant.Models.CrmContacts
                    {
                        InstanceId = 0,
                        CompanyId = Convert.ToInt32(HttpContext.Session.GetInt32("companyId")),
                        FirstName = input.FirstName,
                        LastName = input.LastName,
                        Title = input.Title,
                        Address1 = input.Address1,
                        Address2 = input.Address2,
                        City = input.City,
                        StateRegion = input.StateRegion,
                        PostalCode = input.PostalCode,
                        Phone = input.Phone,
                        MobilePhone = input.MobilePhone,
                        Email = input.Email
                    };
                    _servicedb.CrmContacts.Add(contact);
                    await _servicedb.SaveChangesAsync();               
                }
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "AddContact CompanyContactProfile_Create", User.Identity.Name);
                return LocalRedirect(@"/Dashboard/Error");
            }
        }
        public async Task<IActionResult> ContactProfile_Update([DataSourceRequest] DataSourceRequest request, CrmModelSRVContact input)
        {
            try
            {
                if (input != null && ModelState.IsValid)
                {
                    var contact = new WellAI.Advisor.Model.Tenant.Models.CrmContacts
                    {
                        ContactId = input.ContactID,
                        InstanceId = 0,
                        CompanyId = input.CompanyID,
                        FirstName = input.FirstName,
                        LastName = input.LastName,
                        Title = input.Title,
                        Address1 = input.Address1,
                        Address2 = input.Address2,
                        City = input.City,
                        StateRegion = input.StateRegion,
                        PostalCode = input.PostalCode,
                        Phone = input.Phone,
                        MobilePhone = input.MobilePhone,
                        Email = input.Email
                    };
                    _servicedb.CrmContacts.Update(contact);
                    await _servicedb.SaveChangesAsync();
                    return Json(new[] { input }.ToDataSourceResult(request, ModelState));
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "ContactProfile ContactProfile_Update", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            return Json(new[] { input }.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> ContactProfile_Destroy(int contactId)
        {
            try
            {
                if (contactId > 0)
                {
                    var servicecontact = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                    var res = await servicecontact.DeleteCRMContact(contactId);
                    return Json(new[] { res });
                }
                return null;
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CRM ContactProfile_Destroy", User.Identity.Name);
                return null;
            }
        }
        public async Task<IActionResult> CompanyProfile_Create([DataSourceRequest] DataSourceRequest request, WellAI.Advisor.Model.Tenant.Models.CrmCompanies input)
        {
            try
            {
                if (ModelState.IsValid && input != null)
                {
                    var company = new WellAI.Advisor.Model.Tenant.Models.CrmCompanies
                    {
                        InstanceId = 0,
                        CompanyId = input.CompanyId,
                        Name = input.Name,
                        Address1 = input.Address1,
                        Address2 = input.Address2,
                        City = input.City,
                        StateRegion = input.StateRegion,
                        Country = input.Country,
                        PostalCode = input.PostalCode,
                        Phone = input.Phone,
                        MobilePhone = input.MobilePhone,
                        Fax = input.Fax,
                        Email = input.Email,
                        Website = input.Website,
                    };
                    _servicedb.CrmCompanies.Add(company);
                    await _servicedb.SaveChangesAsync();
                }
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CRM CompanyProfile_Create", User.Identity.Name);
                string returnUrl = @"/ServiceDashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public async Task<IActionResult> CompanyProfile_Update([DataSourceRequest] DataSourceRequest request, WellAI.Advisor.Model.Tenant.Models.CrmCompanies input)
        {
            try
            {
                var result = _servicedb.CrmCompanies.Where(x => x.CompanyId == input.CompanyId).FirstOrDefault();
                result.InstanceId = 0;
                result.CompanyId = input.CompanyId;
                result.Name = input.Name;
                result.Address1 = input.Address1;
                result.Address2 = input.Address2;
                result.City = input.City;
                result.StateRegion = input.StateRegion;
                result.Country = input.Country;
                result.PostalCode = input.PostalCode;
                result.Phone = input.Phone;
                result.MobilePhone = input.MobilePhone;
                result.Fax = input.Fax;
                result.Email = input.Email;
                result.Website = input.Website;
                _servicedb.CrmCompanies.Update(result);
                await _servicedb.SaveChangesAsync();
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CRM CompanyProfile_Update", User.Identity.Name);
                string returnUrl = @"/ServiceDashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> CompanyProfile_Destroy(int companyID)
        {
            try
            {
                if (companyID > 0)
                {
                    var servicecontact = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                    var res = await servicecontact.DeleteCRMCompany(companyID);
                    return Json(new[] { res });
                }
                return null;
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CRM CompanyProfile_Destroy", User.Identity.Name);
                return null;
            }
        }
        public IActionResult CompanyBook()
        {
            return View();
        }
        public IActionResult PhoneBook()
        {
            return View();
        }
        public IActionResult ContactDetail(int contactId)
        {
            Model.ServiceCompany.Models.CrmModelSRVContact ContactDetails = new Model.ServiceCompany.Models.CrmModelSRVContact();
            try
            {
                ContactDetails = (from crm in _servicedb.CrmContacts
                                  join company in _servicedb.CrmCompanies on crm.CompanyId equals company.CompanyId
                                  where crm.ContactId == contactId
                                  select new Model.ServiceCompany.Models.CrmModelSRVContact
                                  {
                                      ContactID = crm.ContactId,
                                      CompanyID = Convert.ToInt32(crm.CompanyId),
                                      InstanceID = crm.InstanceId,
                                      CompanyName = company.Name,
                                      FirstName = crm.FirstName,
                                      LastName = crm.LastName,
                                      Address1 = crm.Address1,
                                      Address2 = crm.Address2,
                                      City = crm.City,
                                      StateRegion = crm.StateRegion,
                                      Country = crm.Country,
                                      PostalCode = crm.PostalCode,
                                      Phone = crm.Phone,
                                      MobilePhone = crm.MobilePhone,
                                      Fax = crm.Fax,
                                      Email = crm.Email,
                                      Title = crm.Title,
                                      Location = string.Format("{0},{1},{2},{3},{4}", crm.Address1, string.IsNullOrEmpty(crm.Address2) ? "" : "," + company.Address2,
                                                                                    crm.City, crm.StateRegion, crm.PostalCode)
                                  }).FirstOrDefault();
                ViewBag.ContactId = contactId;
                HttpContext.Session.SetInt32("contactId", contactId);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CRM ContactDetail", User.Identity.Name);
            }
            return View("ContactDetail", ContactDetails);
        }
        public async Task<IActionResult> ContactNotes_Read([DataSourceRequest] DataSourceRequest request, int ContactId)
        {
            try
            {
                var crmContactNotes = (from crm in _servicedb.CrmComments
                                       where crm.ContactId == ContactId
                                       select new WellAI.Advisor.Model.Tenant.Models.CrmComments
                                       {
                                           CommentType = crm.CommentType,
                                           CommentId = crm.CommentId,
                                           CommentDate = crm.CommentDate,
                                           Body = crm.Body
                                       }
                                   ).ToList();
                return await Task.FromResult(Json(crmContactNotes.ToDataSourceResult(request)));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CRM ContactsNotes_Read", User.Identity.Name);
                string returnUrl = @"/ServiceDashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public IActionResult CompanyDetail(int companyId)
        {
            return View();
        }
        public async Task<IActionResult> CRMContactNotes_Create([DataSourceRequest] DataSourceRequest request, WellAI.Advisor.Model.Tenant.Models.CrmComments input)
        {
            try
            {
                int CompanyId = 0;// HttpContext.Session.GetString("companyId");
                if (input != null && input.CommentId==0)
                {
                    var comment = new WellAI.Advisor.Model.Tenant.Models.CrmComments
                    {
                        CommentId = 0,
                        ContactId = Convert.ToInt32(HttpContext.Session.GetInt32("contactId")) ,
                    CompanyId = CompanyId,
                        CommentDate = DateTime.Now,
                        Body = input.Body
                    };
                    _servicedb.CrmComments.Add(comment);
                    await _servicedb.SaveChangesAsync();
                }
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CRM ContactProfile_Create", User.Identity.Name);
                return LocalRedirect(@"/Dashboard/Error");
            }
        }
        public async Task<IActionResult> CRMContactNotes_Update([DataSourceRequest] DataSourceRequest request, WellAI.Advisor.Model.Tenant.Models.CrmComments input,IFormCollection form)
        {
            try
            {
                if (input != null)
                {
                    var comment = _servicedb.CrmComments.Where(x => x.CommentId == input.CommentId).FirstOrDefault();
                    {
                        comment.CommentId = input.CommentId;
                        comment.CommentDate = DateTime.Now;
                        comment.Body = input.Body;
                    };
                    _servicedb.CrmComments.Update(comment);
                    await _servicedb.SaveChangesAsync();
                }
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "AddContact ContactProfile_Create", User.Identity.Name);
                return LocalRedirect(@"/Dashboard/Error");
            }
        }
       public async Task<IActionResult> CRMContactNotes_Destroy(int CommentID)
        {
            try
            {
                if (CommentID != 0)
                {
                    var result = _servicedb.CrmComments.Where(x => x.CommentId == CommentID).FirstOrDefault();
                    _servicedb.CrmComments.Remove(result);
                    await _servicedb.SaveChangesAsync();
                    return Json(new[] { result });
                }
                return null;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CRMController CRMContactNotes_Destroy", User.Identity.Name);
                return null;
            }
        }
        public async Task<IActionResult> ContactDetails_Read([DataSourceRequest] DataSourceRequest request, int companyId)
        {
            try
            {
                var ContactDetails = (from crm in _servicedb.CrmContacts
                                      join company in _servicedb.CrmCompanies on crm.CompanyId equals company.CompanyId
                                      where crm.CompanyId == companyId
                                      select new Model.ServiceCompany.Models.CrmModelSRVContact
                                      {
                                          ContactID = crm.ContactId,
                                          InstanceID = crm.InstanceId,
                                          CompanyID = Convert.ToInt32(crm.CompanyId),
                                          FirstName = crm.FirstName,
                                          LastName = crm.LastName,
                                          Address1 = crm.Address1,
                                          Address2 = crm.Address2,
                                          City = crm.City,
                                          StateRegion = crm.StateRegion,
                                          Country = crm.Country,
                                          PostalCode = crm.PostalCode,
                                          Phone = crm.Phone,
                                          MobilePhone = crm.MobilePhone,
                                          Fax = crm.Fax,
                                          Email = crm.Email,
                                          Title = crm.Title,
                                          CompanyName = company.Name
                                      }
                                   ).ToList();

                return await Task.FromResult(Json(ContactDetails.ToDataSourceResult(request)));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CRM ContactDetails_Read", User.Identity.Name);
                string returnUrl = @"/ServiceDashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public JsonResult ContactList(int companyId, CrmModelSRVContact contact)
        {
            try
            {
                var companyid = HttpContext.Session.GetInt32("companyId");
                var ContactDetails = (from crm in _servicedb.CrmContacts
                                      join company in _servicedb.CrmCompanies on crm.CompanyId equals company.CompanyId
                                      where crm.CompanyId == companyid
                                      select new Model.ServiceCompany.Models.CRMContactModel
                                      {
                                          ContactId = crm.ContactId,
                                          ContactName = crm.FirstName + " " + crm.LastName
                                      }
                                     ).ToList();
                return Json(ContactDetails);
            }
            catch(Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CRMController ContactList", User.Identity.Name);
                return null;
            }
        }
        public async Task<IActionResult> CompanyDetails_Read([DataSourceRequest] DataSourceRequest request, int companyId)
        {
            try
            {
                var CompanyDetails = (from input in _servicedb.CrmCompanies
                                      where input.CompanyId == companyId
                                      select new Model.ServiceCompany.Models.CrmModelSRV
                                      {
                                          InstanceID = 0,
                                          CompanyID = input.CompanyId,
                                          Name = input.Name,
                                          Address1 = input.Address1,
                                          Address2 = input.Address2,
                                          City = input.City,
                                          StateRegion = input.StateRegion,
                                          Country = input.Country,
                                          PostalCode = input.PostalCode,
                                          Phone = input.Phone,
                                          MobilePhone = input.MobilePhone,
                                          Fax = input.Fax,
                                          Email = input.Email,
                                          Website = input.Website,
                                      }
                                   ).ToList();

                return await Task.FromResult(Json(CompanyDetails.ToDataSourceResult(request)));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CRM CompanyDetails_Read", User.Identity.Name);
                string returnUrl = @"/ServiceDashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public IActionResult CompanyDetails(int companyId)
        {
            ViewBag.companyId = companyId;
                   try {
                var CompanyDetails = (from input in _servicedb.CrmCompanies
                                      where input.CompanyId == companyId
                                      select new Model.ServiceCompany.Models.CrmModelSRV
                                      {
                                          InstanceID = 0,
                                          CompanyID = input.CompanyId,
                                          Name = input.Name,
                                          Address1 = input.Address1,
                                          Address2 = input.Address2,
                                          City = input.City,
                                          StateRegion = input.StateRegion,
                                          PostalCode = input.PostalCode,
                                          Country = input.Country,
                                          Phone = input.Phone,
                                          MobilePhone = input.MobilePhone,
                                          Fax = input.Fax,
                                          Email = input.Email,
                                          Website = input.Website,
                                          TenantID = input.TenantId,
                                          Location = string.Format("{0}{1},{2},{3},{4}", input.Address1, string.IsNullOrEmpty(input.Address2) ? "" : "," + input.Address2,
                                                                                     input.City, input.StateRegion, input.PostalCode)
                                      }
                                   ).FirstOrDefault();
                HttpContext.Session.SetInt32("companyId", companyId);
                return View(CompanyDetails);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CRM CompanyDetails", User.Identity.Name);
                string returnUrl = @"/ServiceDashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        // For the 6 Grid
        public async Task<IActionResult> CRMJobHistory_Read([DataSourceRequest] DataSourceRequest request, int companyId)
        {
            List<ProjectViewSRVModel> result = new List<ProjectViewSRVModel>();
            try
            {
                IProjectBusiness projectBusiness = new ProjectBusiness(db, _userManager);
                var crmCompanyObject = (from input in _servicedb.CrmCompanies
                                        where input.CompanyId == companyId
                                        select new Model.ServiceCompany.Models.CrmModelSRV
                                        {
                                            TenantID = input.TenantId,
                                        }
                                   ).FirstOrDefault();
                string TenantID = HttpContext.GetMultiTenantContext().TenantInfo.Id;
                string operId = crmCompanyObject.TenantID;
                ViewData["OperatorTenantID"] = operId;
                if (TenantID != null && operId != null)
                {
                    result = await projectBusiness.GetUpCommingProjectsSRV(TenantID, operId);
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CRM CRMCOntroller CRMJobHistory_Read", User.Identity.Name);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
            return Json(result.ToDataSourceResult(request));
        }
        public JsonResult GetCRMCompanyList(string text)
        {
            try
            {
                List<Model.ServiceCompany.Models.CRMCompany> result = new List<Model.ServiceCompany.Models.CRMCompany>();
                result = (from crm in _servicedb.CrmCompanies
                          select new Model.ServiceCompany.Models.CRMCompany
                          {
                              CRMCompanyID = crm.CompanyId,
                              Name = crm.Name
                          }).ToList();
                return Json(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CRMCOntroller GetCRMCompanyList", User.Identity.Name);
                return null;
            }
        }
        // Merging CRMCompanyNotes Functionalities
        public async Task<IActionResult> CompanyNotes_Read([DataSourceRequest] DataSourceRequest request, int companyId)
        {
            try
            {
                var crmCompanyNotes = (from crm in _servicedb.CrmComments
                                       join company in _servicedb.CrmContacts on crm.ContactId equals company.ContactId
                                       where crm.CompanyId == companyId
                                       select new WellAI.Advisor.Model.ServiceCompany.Models.CrmModelSRVComments
                                       {
                                           CommentType = crm.CommentType,
                                           CommentId = crm.CommentId,
                                           ContactId = crm.ContactId,
                                           CompanyId = (int)crm.CompanyId,
                                           CommentDate = crm.CommentDate,
                                           Body = crm.Body,
                                           ContactName = company.FirstName + "" + company.LastName,
                                           FirstName = company.FirstName,
                                           LastName = company.LastName
                                       }
                                   ).ToList();
                return await Task.FromResult(Json(crmCompanyNotes.ToDataSourceResult(request)));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CRM Profile_Read", User.Identity.Name);
                string returnUrl = @"/ServiceDashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        public async Task<IActionResult> CRMCompanyNotes_Create([DataSourceRequest] DataSourceRequest request, WellAI.Advisor.Model.ServiceCompany.Models.CrmModelSRVComments input)
        {
            try
            {
                //int ContactId = 0;// HttpContext.Session.GetString("contactId");
                if (input != null && input.CommentId==0)
                {
                    var comment = new WellAI.Advisor.Model.Tenant.Models.CrmComments
                    {
                        CommentId = 0,
                        ContactId = input.ContactId,
                        CompanyId = HttpContext.Session.GetInt32("companyId"),
                        CommentDate = DateTime.Now,
                        Body = input.Body
                    };
                    _servicedb.CrmComments.Add(comment);
                    await _servicedb.SaveChangesAsync();
                }
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "AddContact ContactProfile_Create", User.Identity.Name);
                return LocalRedirect(@"/Dashboard/Error");
            }
        }
        public async Task<IActionResult> CRMCompanyNotes_Update([DataSourceRequest] DataSourceRequest request, WellAI.Advisor.Model.ServiceCompany.Models.CrmModelSRVComments input)
        {
            try
            {
                if (input != null && input.CommentId!=0)
                {
                    var comment = _servicedb.CrmComments.Where(x => x.CommentId == input.CommentId).FirstOrDefault();
                    {
                        comment.CommentId = input.CommentId;
                        comment.ContactId = input.ContactId;
                        comment.CommentDate = DateTime.Now;
                        comment.Body = input.Body;
                    };
                    _servicedb.CrmComments.Update(comment);
                    await _servicedb.SaveChangesAsync();
                }
                return Json(new[] { input }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "AddContact ContactProfile_Create", User.Identity.Name);
                return LocalRedirect(@"/Dashboard/Error");
            }
        }
        [AcceptVerbs("Post")]
        public async Task<IActionResult> CRMCompanyNotes_Destroy(int CommentID)
        {
            try
            {
                if (CommentID != 0)
                {
                    var result = _servicedb.CrmComments.Where(x => x.CommentId == CommentID).FirstOrDefault();
                    _servicedb.CrmComments.Remove(result);
                    await _servicedb.SaveChangesAsync();
                    return Json(new[] { result });
                }
                return null;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CRMController CRMCompanyNotes_Destroy", User.Identity.Name);
                return null;
            }
        }
        [AcceptVerbs("Post")]
        public async Task<JsonResult> CreateCRMActivity([DataSourceRequest] DataSourceRequest request, Model.ServiceCompany.Models.ActivityViewModel task)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (HttpContext.Session.GetString("ServiceScheduleTenantId") != "null")
                    {
                        task.TenantId = HttpContext.Session.GetString("ServiceScheduleTenantId");
                    }

                    var serviceTenant = new ServiceTenantRepository(_servicedb, HttpContext, _userManager, db);
                    var id = await serviceTenant.UpdateCRMActivityTask(task);
                    if (string.IsNullOrEmpty(task.ProjectId))
                    {
                        task.ProjectId = id;
                        task.ActivityIsTask = true;
                    }
                }
                return Json(new[] { task }.ToDataSourceResult(request, ModelState));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CRMController CreateCRMActivity", User.Identity.Name);
                return null;
            }
        }
        [AcceptVerbs("Post")]
        public JsonResult SetTenantIdForScheduler(string tenantId)
        {
            try
            {
                HttpContext.Session.SetString("ServiceScheduleTenantId", tenantId);
                return Json(tenantId);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "CRMController SetTenantIdForScheduler", User.Identity.Name);
                return null;
            }
        }
    }
}
