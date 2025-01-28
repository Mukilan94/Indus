using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.ServiceCompany.Models;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.DLL.ServiceEntity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using WellAI.Advisor.Model.Common;
using Microsoft.Data.SqlClient.Server;
using Newtonsoft.Json;
using WellAI.Advisor.Model.Administration;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using Finbuckle.MultiTenant;

//using Newtonsoft.Json;

namespace WellAI.Advisor.DLL.Repository
{
    public class CommonRepository : ICommonRepository
    {
        private readonly WebAIAdvisorContext db;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<WellIdentityUser> _userManager;
        private TenantOperatingDbContext _tdbContext;

        public CommonRepository(WebAIAdvisorContext db, RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager)
        {
            this.db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IdentityResult> Create(IdentityRole role)
        {
            return await _roleManager.CreateAsync(role);
        }

        public List<RoleViewModel> GetRoleList(string tenantId)
        {
            try
            {
                IRolePermissionRepository repository = new RolePermissionRepository(db, _roleManager, _userManager);
                List<RoleViewModel> roleViewModelList = new List<RoleViewModel>();
                var roles = (from r in _roleManager.Roles
                             join tr in db.TenantRoles on r.Id equals tr.RoleId
                             where tr.TenantId == tenantId
                             select r).ToList();
                foreach (var item in roles)
                {
                    RoleViewModel roleViewModel = new RoleViewModel();
                }
                return roleViewModelList;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetRoleList", null);
                return null;
            }
        }

        public List<IdentityRole> GetRoles(string tenantId)
        {
            try
            {
                var roles = (from r in _roleManager.Roles
                             join tr in db.TenantRoles on r.Id equals tr.RoleId
                             where tr.TenantId == tenantId && r.Name != "Operator" && r.Name != "Service Provider"
                             select r).ToList();
                return roles;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetRoles", null);
                return null;
            }
        }

        public List<IdentityRole> GetRolesSet()
        {
            return _roleManager.Roles.ToList();
        }

        public async Task<int> GetUserSubscriptionUserLeft(string tenantId)
        {
            try
            {
                int result = 0;

                var subscription = db.Subscription.FirstOrDefault(x => x.TenantId == tenantId);

                if (subscription != null)
                {
                    result = subscription.SubscriptionCount - (subscription.CurrentCount.HasValue ? subscription.CurrentCount.Value : 0);
                }

                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetUserSubscriptionUserLeft", null);
                return 0;
            }
        }

        public async Task<int> AddUserCountSubscription(string tenantId)
        {
            try
            {
                int result = 0;

                var subscription = db.Subscription.FirstOrDefault(x => x.TenantId == tenantId);

                if (subscription != null)
                {
                    if (!subscription.CurrentCount.HasValue)
                    {
                        subscription.CurrentCount = 1;
                    }
                    else
                    {
                        subscription.CurrentCount += 1;
                    }

                    result = subscription.CurrentCount.Value;

                    await db.SaveChangesAsync();
                }

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository AddUserCountSubscription", null);
                return 0;
            }
        }

        public async Task<int> RemoveUserCountSubscription(string tenantId)
        {
            try
            {
                int result = 0;

                var subscription = db.Subscription.FirstOrDefault(x => x.TenantId == tenantId);

                if (subscription != null)
                {
                    if (subscription.CurrentCount.HasValue && subscription.CurrentCount > 0)
                    {
                        subscription.CurrentCount -= 1;
                    }

                    result = subscription.CurrentCount ?? 0;

                    await db.SaveChangesAsync();
                }

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository RemoveUserCountSubscription", null);
                return 0;
            }
        }

        //Getting user list
        public async Task<List<UserViewModel>> GetUserList(string tenantId)
        {
            try
            {
                List<UserViewModel> userViewModelList = new List<UserViewModel>();
                var users = (from u in _userManager.Users
                             join tu in db.TenantUsers on u.Id equals tu.UserId
                             where tu.TenantId == tenantId
                             select u).ToList();
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
                    userViewModel.WellOfficeUser = user.WellUser.HasValue ? user.WellUser.Value : false;
                    userViewModel.Field = user.Field.HasValue ? user.Field.Value : false;
                    userViewModel.IsUser = user.IsUser.HasValue ? user.IsUser.Value : false;

                    var details = GetUserBasicDetail(user.Id);
                    if (details != null)
                    {
                        userViewModel.IsActive = details.IsActive;
                        userViewModel.IsMaster = details.IsMaster;
                    }

                    IList<string> userRoleNames = null;
                    userRoleNames = await _userManager.GetRolesAsync(user);

                    var tenantRoles = (from r in _roleManager.Roles
                                       join tr in db.TenantRoles on r.Id equals tr.RoleId
                                       where tr.TenantId == tenantId
                                       select r).ToList();

                    userViewModel.roles = new List<IdentityRole>();
                    userViewModel.SelectedRoles = "";

                    foreach (var tenantRole in tenantRoles)
                    {
                        if (userRoleNames != null && userRoleNames.Contains(tenantRole.Name))
                        {
                            userViewModel.roles.Add(new IdentityRole { Id = tenantRole.Id, Name = tenantRole.Name });
                            userViewModel.SelectedRoles += tenantRole.Id + ";";
                        }
                    }
                    userViewModel.ProfileImageName = user.ProfileImageName;
                    userViewModel.UserTenantId = user.TenantId;
                    userViewModelList.Add(userViewModel);
                }
                return userViewModelList;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetUserList", null);
                return null;
            }
        }

        public async Task<WellIdentityUser> GetUserByEmail(string email)
        {
            try
            {
                //var result = await _userManager.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
                //return result;// await _userManager.Users.Where(x => x.Email == email).FirstOrDefault();
                return await _userManager.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetUserByEmail", null);
                return null;
            }
        }

        public async Task<Model.Administration.RegisterStaffViewModel> GetAdminUserByEmail(string email)
        {
            try
            {
                var staffs = await db.staffusers.Where(x => x.Email == email).Select(x => new Model.Administration.RegisterStaffViewModel
                {
                    Email = x.Email,
                    FullName = x.FullName,
                    PhoneNumber = x.PhoneNumber,
                    Id = x.id,
                    IsActive = x.IsActive
                }).FirstOrDefaultAsync();
                return staffs;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetAdminUserByEmail", null);
                return null;
            }
        }

        public async Task<UserViewModel> GetUser(string id)
        {
            try
            {
                var user = _userManager.Users.Where(x => x.Id == id).FirstOrDefault();
                if (user != null)
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
                    userViewModel.AdditionalNotes = user.AdditionalNotes;
                    userViewModel.WellOfficeUser = user.WellUser.HasValue ? user.WellUser.Value : false;
                    userViewModel.Field = user.Field.HasValue ? user.Field.Value : false;

                    var tenantId = await GetTenantUser(id);
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
                    userViewModel.ProfileImageName = user.ProfileImageName;
                    userViewModel.UserTenantId = user.TenantId;

                    return userViewModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetUser", null);
                return null;
            }
        }

        public Task<UserViewModel> GetPrimaryUser(string tenantId)
        {
            try
            {
                UserViewModel userViewModel = null;

                var user = _userManager.Users.Where(x => x.TenantId == tenantId && x.Primary.HasValue && x.Primary.Value).FirstOrDefault();
                if (user != null)
                {
                    userViewModel = new UserViewModel();

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
                    userViewModel.AdditionalNotes = user.AdditionalNotes;
                    userViewModel.WellOfficeUser = user.WellUser.HasValue ? user.WellUser.Value : false;
                    userViewModel.Field = user.Field.HasValue ? user.Field.Value : false;

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
                }

                return Task.FromResult(userViewModel);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetPrimaryUser", null);
                return null;
            }
        }

        public WellIdentityUser GetUserDetail(string id)
        {
            try
            {
                return _userManager.Users.Where(x => x.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetUserDetail", null);
                return null;
            }
        }

        public async Task<IdentityResult> UpdateUser(WellIdentityUser resultUser)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(resultUser.Id);
                if (user != null)
                {
                    user.PhoneNumber = resultUser.PhoneNumber;
                    user.Email = resultUser.Email;
                    user.FirstName = resultUser.FirstName;
                    user.MiddleName = resultUser.MiddleName;
                    user.LastName = resultUser.LastName;
                    user.Mobile = resultUser.Mobile;
                    user.JobTitle = resultUser.JobTitle;
                    user.Address = resultUser.Address;
                    user.City = resultUser.City;
                    user.State = resultUser.State;
                    user.Zip = resultUser.Zip;
                    user.AdditionalNotes = resultUser.AdditionalNotes;
                    user.Primary = resultUser.Primary;
                    user.ProfileImageName = resultUser.ProfileImageName == null ? user.ProfileImageName : resultUser.ProfileImageName;
                    user.WellUser = resultUser.WellUser;
                    user.Field = resultUser.Field.HasValue ? resultUser.Field.Value : false;
                    user.IsUser = resultUser.IsUser;
                    //user.EmailConfirmed = resultUser.EmailConfirmed;

                    return await _userManager.UpdateAsync(user);
                }

                return null;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository UpdateUser", null);
                return null;
            }
        }

        public async Task<string> GetRoleName(WellIdentityUser resultUser)
        {
            try
            {
                var resultRole = await _userManager.GetRolesAsync(resultUser);
                return resultRole.FirstOrDefault();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetRoleName", null);
                return null;
            }
        }

        public async Task<IList<string>> GetUserRoleNames(WellIdentityUser resultUser)
        {
            try
            {
                return await _userManager.GetRolesAsync(resultUser);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetUserRoleNames", null);
                return null;
            }
        }

        public async Task<IdentityResult> RemoveUserRole(WellIdentityUser resultUser, string resultRole)
        {
            try
            {
                return await _userManager.RemoveFromRoleAsync(resultUser, resultRole);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository RemoveUserRole", null);
                return null;
            }
        }

        public async Task<bool> RemoveAllUserRoles(WellIdentityUser resultUser, IList<string> resultRoles)
        {
            try
            {
                await _userManager.RemoveFromRolesAsync(resultUser, resultRoles.ToArray());
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository RemoveAllUserRoles", null);
                var t = ex;
            }
            return true;
        }

        public async Task<IdentityResult> AddUserRole(WellIdentityUser resultUser, string resultRole)
        {
            return await _userManager.AddToRoleAsync(resultUser, resultRole);
        }

        public async Task<IdentityResult> CreateUser(WellIdentityUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> RemoveUser(WellIdentityUser user)
        {
            return await _userManager.DeleteAsync(user);
        }

        public bool CreateUserBasicDetail(CrmUserBasicDetail user)
        {
            try
            {
                user.PaymentStatus = 0;
                db.CrmUserBasicDetail.Add(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository CreateUserBasicDetail", null);
                return false;
            }
        }
        public bool UpdateUserBasicDetail(CrmUserBasicDetail user)
        {
            try
            {
                if (user != null)
                {
                    var CrmUser = db.CrmUserBasicDetail.Where(x => x.UserId == user.UserId).FirstOrDefault();
                    if (CrmUser != null)
                    {
                        CrmUser.Name = user.Name;
                        CrmUser.ModifiedDate = user.ModifiedDate;
                        db.CrmUserBasicDetail.Update(CrmUser);
                        db.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository CreateUserBasicDetail", null);
                return false;
            }
        }

        public Task<List<CorporateProfile>> GetServiceCompanies()
        {
            try
            {
                var tenantIds = db.CrmCompanies.Where(x => x.TenantId != null).Select(x => x.TenantId);

                var corpProfiles = db.CorporateProfile.Where(x => tenantIds.Contains(x.TenantId)).Distinct().ToList();

                return Task.FromResult(corpProfiles);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetServiceCompanies", null);
                return null;
            }
        }

        public Task<List<CorporateProfile>> GetServiceCompaniesByCategories(string ServiceCategoryId)
        {
            try
            {
                var TenantIds = db.CrmCompanies.Where(x => x.TenantId != null && x.Category.Contains(ServiceCategoryId)).Select(x => x.TenantId);

                var CorporateProfile = db.CorporateProfile.Where(x => TenantIds.Contains(x.TenantId)).Distinct().ToList();

                return Task.FromResult(CorporateProfile);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetServiceCompaniesByCategories", null);
                return null;
            }
        }

        public CrmUserBasicDetail DisableUserBasicDetail(string userId)
        {
            try
            {
                var detail = db.CrmUserBasicDetail.FirstOrDefault(x => x.UserId == userId);
                detail.IsActive = false;
                db.SaveChanges();
                return detail;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository DisableUserBasicDetail", null);
                return null;
            }
        }

        public async Task<CrmUserBasicDetail> EnableUser(string userId)
        {
            try
            {
                var detail = db.CrmUserBasicDetail.FirstOrDefault(x => x.UserId == userId);
                detail.IsActive = true;
                db.SaveChanges();
                return await Task.FromResult(detail);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository DisableUserBasicDetail", null);
                return null;
            }
        }

        public bool RemoveTenantUser(string userId)
        {
            try
            {
                var tenuser = db.TenantUsers.FirstOrDefault(x => x.UserId == userId.ToString());
                db.TenantUsers.Remove(tenuser);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository RemoveTenantUser", null);
                return false;
            }
        }

        public CrmUserBasicDetail GetUserBasicDetail(string userId)
        {
            return db.CrmUserBasicDetail.Where(x => x.UserId == userId).FirstOrDefault();
        }

        public Task<CrmUserBasicDetail> GetMasterUserByTenantId(string tenantId)
        {
            try
            {
                var users = _userManager.Users.Where(x => x.TenantId == tenantId);

                var detail = db.CrmUserBasicDetail.FirstOrDefault(x => x.IsMaster.HasValue && x.IsMaster.Value && (users.FirstOrDefault(y => y.Id == x.UserId) != null));

                return Task.FromResult(detail);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetMasterUserByTenantId", null);
                return null;
            }
        }

        public string GetUserBasicDetailByEmail(string email)
        {
            try
            {
                return (from u in _userManager.Users
                        join tu in db.CrmUserBasicDetail on u.Id equals tu.UserId
                        where u.UserName == email
                        select tu.Name).FirstOrDefault();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetUserBasicDetailByEmail", null);
                return null;
            }
        }

        public bool CreateCompanyDetail(CrmCompanies company)
        {
            try
            {
                var result = db.CrmCompanies.Where(x => x.UserId == company.UserId).FirstOrDefault();
                if (result == null)
                {
                    db.CrmCompanies.Add(company);
                    db.SaveChanges();
                }
                else
                {
                    result.Address1 = company.Address1;
                    result.Address2 = company.Address2;
                    result.City = company.City;
                    result.Category = company.Category;
                    result.EIN = company.EIN;
                    result.Phone = company.Phone;
                    result.StateRegion = company.StateRegion;
                    result.PostalCode = company.PostalCode;
                    result.Fax = company.Fax;
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository CreateCompanyDetail", null);
                return false;
            }
        }

        public bool UpdateCompanyCategories(string category, string tenantId)
        {
            try
            {
                var result = db.CrmCompanies.Where(x => x.TenantId == tenantId).FirstOrDefault();
                if (result != null)
                {
                    result.Category = category;
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository CreateCompanyDetail", null);
                return false;
            }
        }

        public CrmCompanies GetCompanyDetail(string userId)
        {
            return db.CrmCompanies.Where(x => x.UserId == userId).FirstOrDefault();
        }

        public CrmCompanies GetCompanyDetailByTenant(string tenantId)
        {
            return db.CrmCompanies.Where(x => x.TenantId == tenantId).FirstOrDefault();
        }

        public string GetStateRegion(int stateId)
        {
            return db.USAStates.Where(x => x.StateId == stateId).FirstOrDefault().Name;
        }

        public async Task<bool> CreateTenantRole(TenantRoles role)
        {
            try
            {
                db.TenantRoles.Add(role);
                db.SaveChanges();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository CreateTenantRole", null);
                return false;
            }
        }

        public async Task<bool> CreateTenantUser(TenantUsers user)
        {
            try
            {
                db.TenantUsers.Add(user);
                db.SaveChanges();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository CreateTenantUser", null);
                return false;
            }
        }

        public Task<string> GetTenantUser(string userId)
        {
            try
            {
                var tenantuser = db.TenantUsers.FirstOrDefault(x => x.UserId == userId);
                if (tenantuser != null)
                    return Task.FromResult(tenantuser.TenantId);
                return Task.FromResult("");
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetTenantUser", null);
                return null;
            }
        }

        public Task<List<string>> GetTenantUserIds(List<string> userIds)
        {
            try
            {
                var tenantusers = db.TenantUsers.Where(x => userIds.Contains(x.UserId)).Select(x => x.TenantId).ToList();

                return Task.FromResult(tenantusers);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetTenantUserIds", null);
                return null;
            }
        }

        public async Task<bool> CreateWellFile(WellFile file)
        {
            try
            {
                db.WellFiles.Add(file);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetTenantUserIds", null);
                return false;
            }
        }

        public async Task<string> RemoveWellFile(string fileId)
        {
            try
            {
                var result = ""; ;

                var file = db.WellFiles.FirstOrDefault(x => x.FileId == fileId);

                if (file != null)
                {
                    result = file.FileName;

                    var e = db.WellFiles.Remove(file);
                    await db.SaveChangesAsync();
                }

                //Phase II changes 02/08/2021 - remove file from ProviderMSALinks table
                var providerMSAFile = db.ProviderMSALinks.FirstOrDefault(x => x.FileId == fileId);

                if (providerMSAFile != null)
                {
                    result = file.FileName;

                    var e = db.ProviderMSALinks.Remove(providerMSAFile);
                    await db.SaveChangesAsync();
                }

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository RemoveWellFile", null);
                return null;
            }
        }

        public async Task<string> RemoveWellFiles(List<string> fileIds)
        {
            try
            {
                var result = ""; ;

                var files = db.WellFiles.Where(x => fileIds.Contains(x.FileId)).ToList();

                for (int i = 0; i < files.Count; i++)
                {
                    result = files[i].FileName;

                    var e = db.WellFiles.Remove(files[i]);
                }

                await db.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository RemoveWellFiles", null);
                return null;
            }
        }

        public async Task<string> RemoveWellFileByName(string path, string fileName, string tenantId)
        {
            try
            {
                var result = "";

                var dir = path.Replace("/" + fileName, "");

                var file = db.WellFiles.FirstOrDefault(x => x.Category == dir && x.FileName == fileName && x.TenantId == tenantId);

                if (file != null)
                {
                    result = file.FileName;

                    var e = db.WellFiles.Remove(file);
                    await db.SaveChangesAsync();
                }

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository RemoveWellFileByName", null);
                return null;
            }
        }

        public async Task<List<string>> RemoveWellFilesByName(List<KeyValuePair<string, string>> fileNamePaths, string tenantId)
        {
            try
            {
                var result = new List<string>();

                foreach (var fileNamePath in fileNamePaths)
                {
                    var dir = fileNamePath.Value.Replace("/" + fileNamePath.Key, "");

                    var file = db.WellFiles.FirstOrDefault(x => x.Category == dir && x.FileName == fileNamePath.Key && x.TenantId == tenantId);

                    if (file != null)
                    {
                        result.Add(file.FileId);

                        var e = db.WellFiles.Remove(file);
                    }
                }

                await db.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository RemoveWellFilesByName", null);
                return null;
            }
        }

        public Task<List<Model.OperatingCompany.Models.MSA>> GetMSAWellFilesFromTenants(List<string> tenantIds, string operTenantId, string wellId)
        {
            try
            {

                var provfiles = db.ProviderMSALinks
                .Where(x => x.OperationTenantId == operTenantId && tenantIds.Contains(x.ServiceTenantId) && x.IsActive == true).ToList();
                tenantIds.Add(operTenantId);

                var checkwellFilter = wellId == DLL.Constants.NoSpecificWellFilterKey;
                List<WellFile> docs = new List<WellFile>();

                //Phase II Changes - 01 / 18 / 2021 - Get Uploaded Files from the Service at Operating Company
                if (wellId != "00000000-0000-0000-0000-000000000000")
                {
                    docs = db.WellFiles.Where(x => tenantIds.Contains(x.TenantId) && (x.WellId == wellId && !checkwellFilter || checkwellFilter)).ToList();
                }
                else
                {
                    docs = db.WellFiles.Where(x => tenantIds.Contains(x.TenantId)).ToList();
                }

                var result = new List<Model.OperatingCompany.Models.MSA>();

                foreach (var provFile in provfiles)
                {
                    var file = docs.FirstOrDefault(x => x.FileId == provFile.FileId);

                    if (file != null)
                    {
                        result.Add(new Model.OperatingCompany.Models.MSA
                        {
                            MsaId = file.FileId,
                            Attachment = file.FileName,
                            Expiration = provFile.Expire,
                            Status = provFile.IsActive.HasValue && provFile.IsActive.Value ? "Active" : "Inactive",
                            Value = provFile.OperationTenantId,
                            CompanyId = provFile.ServiceTenantId,
                            IsApproved = provFile.IsApproved,
                            FileUploadTime = provFile.FileUploadTime
                        });
                    }
                }

                var msaresult = result.OrderByDescending(i => i.Expiration);
                return Task.FromResult(msaresult.ToList());
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetMSAWellFilesFromTenants", null);
                return null;
            }
        }

        /// <summary>
        /// MSA Files with user permissions
        /// </summary>
        /// <param name="tenantIds"></param>
        /// <param name="operTenantId"></param>
        /// <param name="wellId"></param>
        /// /// <param name="userId"></param>
        /// <returns></returns>
        public Task<List<Model.OperatingCompany.Models.MSA>> GetMSAWellFilesFromTenantsForUser(List<string> tenantIds, string operTenantId, string wellId, string userId)
        {
            try
            {
                var provfiles = db.ProviderMSALinks
                .Where(x => x.OperationTenantId == operTenantId && tenantIds.Contains(x.ServiceTenantId) && x.IsActive == true).ToList();
                tenantIds.Add(operTenantId);
                var checkwellFilter = wellId == DLL.Constants.NoSpecificWellFilterKey;
                List<WellFile> docs = new List<WellFile>();

                //Phase II Changes - 01 / 18 / 2021 - Get Uploaded Files from the Service at Operating Company
                if (wellId != "00000000-0000-0000-0000-000000000000")
                {
                    docs = db.WellFiles.Where(x => tenantIds.Contains(x.TenantId) && (x.WellId == wellId && !checkwellFilter || checkwellFilter)).ToList();
                }
                else
                {
                    docs = db.WellFiles.Where(x => tenantIds.Contains(x.TenantId)).ToList();
                }

                var componentId = 0;
                componentId = db.Components.Where(c => c.ComponentName == "MSA Approval").Select(c => c.ComponentId).FirstOrDefault();

                if (componentId == null)
                {
                    componentId = 0;
                }

                IList<string> userRoleNames = null;

                WellIdentityUser user = new WellIdentityUser();
                user = GetUserDetail(userId);

                userRoleNames = _userManager.GetRolesAsync(user).Result;

                var tenantRoles = (from r in _roleManager.Roles
                                   join tr in db.TenantRoles on r.Id equals tr.RoleId
                                   where tr.TenantId == operTenantId
                                   select r).ToList();

                UserViewModel userViewModel = new UserViewModel();
                userViewModel.roles = new List<IdentityRole>();
                userViewModel.SelectedRoles = "";

                List<string> userRoles = new List<string>();
                foreach (var tenantRole in tenantRoles)
                {
                    if (userRoleNames != null && userRoleNames.Contains(tenantRole.Name))
                    {

                        userRoles.Add(tenantRole.Id);
                    }
                }

                var msaPermission = (from CL in db.RolePermissionComponentLinks
                                     join CM in db.Components on CL.ComponentId equals CM.ComponentId
                                     join RP in db.RolePermissionLinks on CL.RolePermissionId equals RP.RolePermissionId
                                     join UR in db.UserRoles on RP.RoleId equals UR.RoleId
                                     join US in db.Users on UR.UserId equals US.Id
                                     where CM.ComponentId == componentId && CL.IsPermitted == true
                                     && userRoles.Contains(RP.RoleId)
                                     select RP.IsPermitted
                                  ).FirstOrDefault();

                bool msaIsPermitted = Convert.ToBoolean(msaPermission);

                var result = new List<Model.OperatingCompany.Models.MSA>();

                foreach (var provFile in provfiles)
                {
                    var file = docs.FirstOrDefault(x => x.FileId == provFile.FileId);

                    if (file != null)
                    {
                        result.Add(new Model.OperatingCompany.Models.MSA
                        {
                            MsaId = file.FileId,
                            Attachment = file.FileName,
                            Expiration = provFile.Expire,
                            Status = provFile.IsActive.HasValue && provFile.IsActive.Value ? "Active" : "Inactive",
                            Value = provFile.OperationTenantId,
                            CompanyId = provFile.ServiceTenantId,
                            IsApproved = provFile.IsApproved,
                            FileUploadTime = provFile.FileUploadTime,
                            IsPermitted = msaIsPermitted
                        });
                    }
                }

                var msaresult = result.OrderByDescending(i => i.Expiration);
                //result = result.Take(10);

                return Task.FromResult(msaresult.ToList());
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetMSAWellFilesFromTenants", null);
                return null;
            }
        }

        /// <summary>
        ///  Phase II Changes - 01/18/2021 - Get Approved MSA Files of the Service at Operating Company
        /// </summary>
        /// <param name="tenantIds"></param>
        /// <param name="operTenantId"></param>
        /// <param name="wellId"></param>
        /// <returns></returns>
        public Task<List<Model.OperatingCompany.Models.MSA>> GetApprovedMSAWellFilesOfServiceTenant(List<string> tenantIds, string operTenantId, string wellId)
        {
            try
            {
                var provfiles = db.ProviderMSALinks
                .Where(x => x.OperationTenantId == operTenantId && tenantIds.Contains(x.ServiceTenantId) && x.IsActive.HasValue && x.IsActive.Value && x.IsApproved == true).ToList();

                var checkwellFilter = wellId == DLL.Constants.NoSpecificWellFilterKey;

                //var docs = db.WellFiles.Where(x => x.TenantId == operTenantId && (x.WellId == wellId && !checkwellFilter || checkwellFilter)).ToList();
                var docs = db.WellFiles.Where(x => tenantIds.Contains(x.TenantId)).ToList();

                var result = new List<Model.OperatingCompany.Models.MSA>();

                foreach (var provFile in provfiles)
                {
                    var file = docs.FirstOrDefault(x => x.FileId == provFile.FileId);

                    if (file != null)
                    {
                        result.Add(new Model.OperatingCompany.Models.MSA
                        {
                            MsaId = file.FileId,
                            Attachment = file.FileName,
                            Expiration = provFile.Expire,
                            Status = provFile.IsActive.HasValue && provFile.IsActive.Value ? "Active" : "Inactive",
                            Value = provFile.OperationTenantId,
                            CompanyId = provFile.ServiceTenantId,
                            IsApproved = (bool)provFile.IsApproved
                        });
                    }
                }

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetApprovedMSAWellFilesOfServiceTenant", null);
                return null;
            }
        }

        public async Task<bool> UpdateProviderMSALinkWellFile(string operTenantId, string fileId, string vendorId, DateTime expire, bool isActive)
        {
            try
            {
                var vendorTenant = db.CorporateProfile.FirstOrDefault(x => x.ID == vendorId);

                if (vendorTenant != null)
                {
                    var provfile = db.ProviderMSALinks.FirstOrDefault(x => x.OperationTenantId == operTenantId
                                                                        && x.FileId == fileId && x.ServiceTenantId == vendorTenant.TenantId);

                    if (provfile != null)
                    {
                        provfile.Expire = expire;
                        provfile.IsActive = true;
                    }
                    else
                    {
                        var newfile = new ProviderMSALink
                        {
                            Id = Guid.NewGuid().ToString("D"),
                            FileId = fileId,
                            Expire = expire,
                            OperationTenantId = operTenantId,
                            ServiceTenantId = vendorTenant.TenantId,
                            IsActive = true,
                            FileUploadTime = DateTime.Now
                        };

                        db.ProviderMSALinks.Add(newfile);
                    }

                    await db.SaveChangesAsync();

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository UpdateProviderMSALinkWellFile", null);
                return false;
            }
        }

        public async Task<bool> UpdateProviderMSALinkWellFileServiceEdit(string fileId, string tenantId, DateTime expire, bool isActive)
        {
            try
            {
                var provfile = db.ProviderMSALinks.FirstOrDefault(x => x.FileId == fileId && x.ServiceTenantId == tenantId);

                if (provfile != null)
                {
                    provfile.Expire = expire;
                    provfile.IsActive = isActive;

                    await db.SaveChangesAsync();

                    return true;
                }
                else
                {
                }

                return false; ;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository UpdateProviderMSALinkWellFileServiceEdit", null);
                return false;
            }
        }

        public async Task<bool> UpdateProviderMSALinkWellFileService(string operTenantId, string fileId, string vendorTenantId, DateTime expire, bool isActive)
        {
            try
            {
                var OperatorTenant = db.CorporateProfile.FirstOrDefault(x => x.TenantId == operTenantId);

                if (OperatorTenant != null)
                {
                    var provfile = db.ProviderMSALinks.FirstOrDefault(x => x.OperationTenantId == OperatorTenant.TenantId
                                                                        && x.FileId == fileId && x.ServiceTenantId == vendorTenantId);

                    if (provfile != null)
                    {
                        provfile.Expire = expire;
                        provfile.IsActive = true;
                    }
                    else
                    {
                        var newfile = new ProviderMSALink
                        {
                            Id = Guid.NewGuid().ToString("D"),
                            FileId = fileId,
                            Expire = expire,
                            OperationTenantId = OperatorTenant.TenantId,
                            ServiceTenantId = vendorTenantId,
                            IsActive = true,
                            FileUploadTime = DateTime.Now
                        };

                        db.ProviderMSALinks.Add(newfile);
                    }

                    await db.SaveChangesAsync();

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository UpdateProviderMSALinkWellFileService", null);
                return false;
            }
        }

        public Task<List<Model.OperatingCompany.Models.UploadsGridFileModel>> GetWellFilesFromTenant(string tenantId, string wellCategory)
        {
            try
            {
                var docs = db.WellFiles.Where(x => x.TenantId == tenantId && !string.IsNullOrEmpty(x.WellId) && x.Category.IndexOf(wellCategory) > 0).ToList();

                var result = new List<UploadsGridFileModel>();

                foreach (var doc in docs)
                {
                    var well = db.WELL_REGISTERs.FirstOrDefault(x => x.well_id == doc.WellId);
                    if (well != null)
                    {
                        if (doc.Category.TrimEnd('/') == "Well/" + well.wellname + "/" + wellCategory)
                        {
                            var newItem = new UploadsGridFileModel
                            {
                                FileId = doc.FileId,
                                FileName = doc.FileName,
                                Date = doc.Date.Value,
                                WellName = well.wellname
                            };

                            result.Add(newItem);
                        }
                    }
                }

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetWellFilesFromTenant", null);
                return null;
            }
        }

        public Task<List<Model.OperatingCompany.Models.UploadsGridFileModel>> GetWellFilesFromTenantAndWell(string tenantId, string wellId, string wellCategory)
        {
            try
            {
                var docs = db.WellFiles.Where(x => x.TenantId == tenantId && x.WellId == wellId && x.Category.IndexOf(wellCategory) > 0).ToList();

                var well = db.WELL_REGISTERs.FirstOrDefault(x => x.well_id == wellId);

                var result = new List<UploadsGridFileModel>();

                foreach (var doc in docs)
                {
                    if (doc.Category.TrimEnd('/') == "Well/" + well.wellname + "/" + wellCategory)
                    {
                        var newItem = new UploadsGridFileModel
                        {
                            FileId = doc.FileId,
                            FileName = doc.FileName,
                            Date = doc.Date.Value,
                            WellName = well.wellname
                        };

                        result.Add(newItem);
                    }
                }

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetWellFilesFromTenantAndWell", null);
                return null;
            }
        }

        public Task<List<Model.OperatingCompany.Models.UploadsGridFileModel>> GetVendorFilesFromTenant(string tenantId, string fileCategory)
        {
            try
            {
                var docs = db.WellFiles.Where(x => x.TenantId == tenantId && !string.IsNullOrEmpty(x.VendorId) && x.Category.IndexOf(fileCategory) > 0).ToList();
                var result = new List<UploadsGridFileModel>();
                foreach (var doc in docs)
                {
                    var vendor = db.CorporateProfile.FirstOrDefault(x => x.ID == doc.VendorId);
                    if (vendor != null)
                    {
                        if (doc.Category.TrimEnd('/') == "Vendor/" + vendor.Name + "/" + fileCategory)
                        {
                            var providerMSALink = db.ProviderMSALinks.FirstOrDefault(x => x.FileId == doc.FileId && x.ServiceTenantId == vendor.TenantId);
                            var newItem = new UploadsGridFileModel
                            {
                                FileId = doc.FileId,
                                FileName = doc.FileName,
                                Date = doc.Date.Value,
                                WellName = vendor.Name
                            };

                            if (providerMSALink != null)
                            {
                                newItem.Expire = providerMSALink.Expire.Value;
                                newItem.IsActive = providerMSALink.IsActive.HasValue ? providerMSALink.IsActive.Value : false;
                                newItem.VendorId = vendor.ID;
                                result.Add(newItem);
                            }
                        }
                    }
                }

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetVendorFilesFromTenant", null);
                return null;
            }
        }

        public Task<List<Model.OperatingCompany.Models.UploadsGridFileModel>> GetVendorFilesFromServiceTenant(string tenantId, string fileCategory)
        {
            try
            {
                var docs = db.WellFiles.Where(x => x.TenantId == tenantId && !string.IsNullOrEmpty(x.VendorId) && x.Category.IndexOf(fileCategory) > 0).OrderByDescending(d => d.Date).ToList();

                var result = new List<UploadsGridFileModel>();

                foreach (var doc in docs)
                {
                    var vendor = db.CorporateProfile.FirstOrDefault(x => x.TenantId == doc.VendorId);

                    if (vendor != null && doc.Category.TrimEnd('/') == "Vendor/" + vendor.Name + "/" + fileCategory)
                    {
                        var providerMSALink = db.ProviderMSALinks.FirstOrDefault(x => x.FileId == doc.FileId && x.ServiceTenantId == tenantId);
                        var newItem = new UploadsGridFileModel
                        {
                            FileId = doc.FileId,
                            FileName = doc.FileName,
                            Date = doc.Date.Value,
                            WellName = vendor.Name
                        };

                        if (providerMSALink != null)
                        {
                            newItem.Expire = providerMSALink.Expire.Value;
                            newItem.IsActive = providerMSALink.IsActive.HasValue ? providerMSALink.IsActive.Value : false;
                            newItem.VendorId = vendor.ID;

                            result.Add(newItem);
                        }
                    }
                }

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetVendorFilesFromServiceTenant", null);
                return null;
            }
        }


        public Task<List<Model.OperatingCompany.Models.Insurance>> GetInsuranceWellFilesFromServiceTenants(List<string> tenantIds, string operTenantId, string wellId)
        {
            try
            {
                var checkwellFilter = wellId == DLL.Constants.NoSpecificWellFilterKey;

                var docs = db.WellFiles.Where(x => tenantIds.Contains(x.TenantId) && (x.Category == "Insurance") && (x.WellId == wellId && !checkwellFilter || checkwellFilter)).ToList();

                var result = new List<Model.OperatingCompany.Models.Insurance>();

                foreach (var provFile in docs)
                {
                    var file = docs.FirstOrDefault(x => x.FileId == provFile.FileId);

                    if (file != null)
                    {
                        result.Add(new Model.OperatingCompany.Models.Insurance
                        {
                            InsId = provFile.FileId,
                            Attachment = file.FileName,
                            Expiration = provFile.Date,
                            Value = provFile.TenantId,
                            Directory = "Insurance/" + file.FileName
                        });
                    }
                }

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetInsuranceWellFilesFromTenants", null);
                return null;
            }
        }

        public Task<WellFile> GetWellFileById(string fileId)
        {
            try
            {
                var docfile = db.WellFiles.FirstOrDefault(x => x.FileId == fileId);
                return Task.FromResult(docfile);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetWellFileById", null);
                return null;
            }
        }

        public Task<List<WellFileFolder>> GetWellFileFolders()
        {
            return Task.FromResult(db.WellFileFolders.Where(x => x.Enable).ToList());
        }

        public bool UpdateUserSubscription(string subscriptionId, string userId, int noOfItems)
        {
            try
            {
                var result = db.CrmUserBasicDetail.Where(x => x.UserId == userId).FirstOrDefault();
                result.SubscriptionId = subscriptionId;
                result.NoOfItems = noOfItems;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository UpdateUserSubscription", null);
                return false;
            }
        }

        public bool UpdateUserPagesCompleteStatus(int status, string userId)
        {
            try
            {
                var result = db.CrmUserBasicDetail.Where(x => x.UserId == userId).FirstOrDefault();
                result.RegisterPagesCompleteStatus = status;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository UpdateUserPagesCompleteStatus", null);
                return false;
            }
        }

        public bool UpdateUserPaymentStatus(int status, string userId)
        {
            try
            {
                var result = db.CrmUserBasicDetail.Where(x => x.UserId == userId).FirstOrDefault();
                result.PaymentStatus = status;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository UpdateUserPaymentStatus", null);
                return false;
            }
        }

        public List<RoleViewSRVModel> GetRoleSRVList(string tenantId)
        {
            try
            {
                IRolePermissionRepository repository = new RolePermissionRepository(db, _roleManager, _userManager);
                List<RoleViewSRVModel> roleViewSRVModelList = new List<RoleViewSRVModel>();
                var roles = (from r in _roleManager.Roles
                             join tr in db.TenantRoles on r.Id equals tr.RoleId
                             where tr.TenantId == tenantId
                             select r).ToList();
                foreach (var item in roles)
                {
                    RoleViewSRVModel roleViewSRVModel = new RoleViewSRVModel();
                }
                return roleViewSRVModelList;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository UpdateUserPaymentStatus", null);
                return null;
            }
        }

        public Task<List<UserViewSRVModel>> GetUserSRVList(string tenantId)
        {
            try
            {
                List<UserViewSRVModel> userViewModelList = new List<UserViewSRVModel>();
                var users = (from u in _userManager.Users
                             join tu in db.TenantUsers on u.Id equals tu.UserId
                             where tu.TenantId == tenantId
                             select u).ToList();
                foreach (var user in users)
                {
                    UserViewSRVModel userViewModel = new UserViewSRVModel();

                    userViewModel.UserID = user.Id;
                    userViewModel.PhoneNumber = user.PhoneNumber;
                    userViewModel.Email = user.Email;
                    userViewModel.FirstName = user.FirstName ?? String.Empty;
                    userViewModel.MiddleName = user.MiddleName;
                    userViewModel.LastName = user.LastName;
                    userViewModel.IsPrimary = user.Primary.HasValue ? user.Primary.Value : false;
                    userViewModel.JobTitle = user.JobTitle;
                    userViewModel.Address = user.Address;
                    userViewModel.City = user.City;
                    userViewModel.AdditionalNotes = user.AdditionalNotes;
                    userViewModel.Zip = user.Zip;
                    userViewModel.Mobile = user.Mobile;
                    userViewModel.State = user.State;
                    userViewModel.UserName = String.Concat(user.FirstName ?? String.Empty, " ", user.LastName ?? String.Empty);

                    var details = GetUserBasicDetail(user.Id);
                    if (details != null)
                    {
                        userViewModel.IsActive = details.IsActive;
                        userViewModel.IsMaster = details.IsMaster;
                    }

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

                    userViewModel.ProfileImageName = user.ProfileImageName;
                    userViewModel.UserTenantId = user.TenantId;

                    userViewModelList.Add(userViewModel);
                }


                return Task.FromResult(userViewModelList);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetUserSRVList", null);
                throw ex;
            }
        }

        public async Task<UserViewSRVModel> GetUserSRV(string id)
        {
            try
            {
                var user = _userManager.Users.Where(x => x.Id == id).FirstOrDefault();
                if (user != null)
                {
                    UserViewSRVModel userViewModel = new UserViewSRVModel();

                    userViewModel.UserID = user.Id;
                    userViewModel.PhoneNumber = user.PhoneNumber;
                    userViewModel.Email = user.Email;
                    userViewModel.FirstName = user.FirstName;
                    userViewModel.MiddleName = user.MiddleName;
                    userViewModel.LastName = user.LastName;
                    userViewModel.JobTitle = user.JobTitle;
                    userViewModel.Address = user.Address;
                    userViewModel.Mobile = user.Mobile;
                    userViewModel.City = user.City;
                    userViewModel.AdditionalNotes = user.AdditionalNotes;

                    var tenantId = await GetTenantUser(id);
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
                    userViewModel.ProfileImageName = user.ProfileImageName;
                    userViewModel.UserTenantId = user.TenantId;
                    return userViewModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetUserSRV", null);
                return null;
            }
        }

        public Task<List<USAState>> GetUSAStates()
        {
            var states = db.USAStates.ToList();
            return Task.FromResult(states);
        }

        public Task<List<Category>> GetCategories()
        {
            return Task.FromResult(db.Categories.ToList());
        }

        public Task<CorporateProfile> GetCorporateProfile()
        {
            CorporateProfile result = null;

            result = db.CorporateProfile.OrderByDescending(x => x.ModifiedDate).FirstOrDefault();

            return Task.FromResult(result ?? new CorporateProfile());
        }

        public Task<CorporateProfile> GetCorporateProfileByTenant(string tenantId)
        {
            var result = db.CorporateProfile.FirstOrDefault(x => x.TenantId == tenantId);

            return Task.FromResult(result ?? new CorporateProfile());
        }

        public Task<TenantConfiguration> GetApiConfigurationByTenant(string tenantId)
        {
            var result = db.TenantConfigurations.FirstOrDefault(x => x.TenantId == tenantId);

            return Task.FromResult(result ?? new TenantConfiguration());
        }

        public async Task<int> UpdateCorporateProfile(CorporateProfile input, string userId, string tenantId)
        {
            try
            {
                input.UserId = userId;
                input.TenantId = tenantId;

                input.ModifiedDate = DateTime.Now;

                var profile = db.CorporateProfile.FirstOrDefault(x => x.TenantId == tenantId);

                if (profile == null)
                {
                    input.ID = Guid.NewGuid().ToString("D");
                    input.CreatedDate = DateTime.Now;
                    input.UserId = userId;
                    db.CorporateProfile.Add(input);
                }
                else
                {
                    profile.Address1 = input.Address1;
                    profile.Address2 = input.Address2;
                    profile.City = input.City;
                    profile.Country = input.Country;
                    profile.Name = input.Name;
                    profile.Phone = input.Phone;
                    profile.State = input.State;
                    profile.Website = input.Website;
                    profile.Zip = input.Zip;
                    profile.LogoPath = input.LogoPath;
                    profile.ModifiedDate = DateTime.Now;
                    db.SaveChanges();
                    WellAIAppContext.Current.Session.SetString("CompanyName", profile.Name);
                }


                db.CorporateProfileHistory.Add(new CorporateProfileHistory
                {
                    ID = Guid.NewGuid().ToString("D"),
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    UserId = userId,
                    TenantId = tenantId,
                    Address1 = input.Address1,
                    Address2 = input.Address2,
                    City = input.City,
                    Country = input.Country,
                    Name = input.Name,
                    Phone = input.Phone,
                    State = input.State,
                    Website = input.Website,
                    Zip = input.Zip,
                    LogoPath = input.LogoPath
                });

                var result = await db.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository UpdateCorporateProfile", null);
                return 0;
            }
        }

        public async Task<int> UpdateApiConfigurationByTenant(string value, string tenantId)
        {
            try
            {
                var config = db.TenantConfigurations.FirstOrDefault(x => x.TenantId == tenantId);

                if (config == null)
                {
                    config = new TenantConfiguration
                    {
                        TenantId = tenantId,
                        ConstantName = "ApiConfig",
                        Value = value
                    };
                    db.TenantConfigurations.Add(config);
                }
                else
                {
                    config.Value = value;
                }

                var result = await db.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository UpdateApiConfigurationByTenant", null);
                return 0;
            }
        }

        public List<FieldTicketSRV> GetProjectInvoice(string ProjectID)
        {
            int id = Convert.ToInt32(ProjectID);

            try
            {
                var InvoiceResult = db.ProjectInvoices.ToList();

                var result = (from invoice in db.ProjectInvoices.Where(p => p.ProjectID == id)
                              select new FieldTicketSRV()
                              {
                                  fdId = invoice.ID,
                                  Ticket = Convert.ToString(invoice.ProjectID),
                                  Invoice = invoice.InvoiceNum,
                                  Date = invoice.InvoiceDate,
                                  Amount = invoice.InvoiceAmount
                              }).ToList();
                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetProjectInvoice", null);
                return null;
            }
        }

        public Task<List<WellAI.Advisor.Model.OperatingCompany.Models.ServiceOffering>> GetServiceOfferings(string tenantId)
        {
            try
            {
                var offering = db.ServiceCompanyOfferings.FirstOrDefault(x => x.ServiceTenantId == tenantId);

                if (offering != null)
                {
                    var ids = System.Text.Json.JsonSerializer.Deserialize<List<string>>(offering.Offerings);

                    var offers = db.ServiceOffers.Where(x => ids.Contains(x.Id)).ToList();

                    var resultoffers = new List<WellAI.Advisor.Model.OperatingCompany.Models.ServiceOffering>();
                    foreach (var offer in offers)
                    {
                        resultoffers.Add(new Model.OperatingCompany.Models.ServiceOffering
                        {
                            ServiceOfferId = offer.Id,
                            Title = offer.Name
                        });
                    }
                    return Task.FromResult(resultoffers);
                }
                else
                {
                    var resultoffers = new List<WellAI.Advisor.Model.OperatingCompany.Models.ServiceOffering>();
                    return Task.FromResult(resultoffers);
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetServiceOfferings", null);
                return null;
            }
        }

        public Task<ServiceCorporateProfile> GetServiceCorporateProfile()
        {
            try
            {
                ServiceCorporateProfile result = null;

                result = (from obj in db.CorporateProfile.OrderByDescending(x => x.ModifiedDate)
                          select new ServiceCorporateProfile()
                          {
                              ID = obj.ID,
                              Name = obj.Name,
                              Website = obj.Website,
                              Phone = obj.Phone,
                              Address1 = obj.Address1,
                              Address2 = obj.Address2,
                              City = obj.City,
                              Country = obj.Country,
                              State = obj.State,
                              LogoPath = obj.LogoPath,
                              UserId = obj.UserId,
                              TenantId = obj.TenantId,
                              ModifiedDate = obj.ModifiedDate,
                              CreatedDate = obj.CreatedDate,
                              Zip = obj.Zip
                          }).FirstOrDefault();

                return Task.FromResult(result ?? new ServiceCorporateProfile());
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetServiceCorporateProfile", null);
                return null;
            }
        }

        public Task<ServiceCorporateProfile> GetServiceCorporateProfileByTenant(string tenantId)
        {
            ServiceCorporateProfile result = null;
            try
            {
                result = (from obj in db.CorporateProfile.OrderByDescending(x => x.ModifiedDate)
                          select new ServiceCorporateProfile()
                          {
                              ID = obj.ID,
                              Name = obj.Name,
                              Website = obj.Website,
                              Phone = obj.Phone,
                              Address1 = obj.Address1,
                              Address2 = obj.Address2,
                              City = obj.City,
                              Country = obj.Country,
                              State = obj.State,
                              LogoPath = obj.LogoPath,
                              UserId = obj.UserId,
                              TenantId = obj.TenantId,
                              ModifiedDate = obj.ModifiedDate,
                              CreatedDate = obj.CreatedDate,
                              Zip = obj.Zip
                          }).FirstOrDefault(x => x.TenantId == tenantId);

                return Task.FromResult(result ?? new ServiceCorporateProfile());
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetServiceCorporateProfile", null);
                return Task.FromResult(result);
            }
        }

        public async Task<int> UpdateServiceCorporateProfile(ServiceCorporateProfile input, string userId, string tenantId)
        {
            try
            {
                var crf = db.CorporateProfile.Where(x => x.TenantId == tenantId).FirstOrDefault();
                if (crf != null)
                {
                    crf.UserId = userId;
                    crf.TenantId = tenantId;
                    crf.ModifiedDate = DateTime.Now;
                    crf.LogoPath = input.LogoPath == null ? crf.LogoPath : input.LogoPath;
                    crf.Name = input.Name;
                    crf.Phone = input.Phone;
                    crf.State = input.State;
                    crf.Website = input.Website;
                    crf.Zip = input.Zip;
                    crf.Country = input.Country;
                    crf.Address1 = input.Address1;
                    crf.Address2 = input.Address2;
                    crf.City = input.City;
                    crf.CServices = input.CServices;
                    WellAIAppContext.Current.Session.SetString("CompanyName", crf.Name);
                }
                else
                {
                    CorporateProfile crf1 = new CorporateProfile();
                    crf1.UserId = userId;
                    crf1.TenantId = tenantId;
                    crf1.ID = Guid.NewGuid().ToString("D");
                    crf1.CreatedDate = DateTime.Now;
                    crf1.ModifiedDate = DateTime.Now;
                    crf1.LogoPath = input.LogoPath;
                    crf1.Name = input.Name;
                    crf1.Phone = input.Phone;
                    crf1.State = input.State;
                    crf1.Website = input.Website;
                    crf1.Zip = input.Zip;
                    crf1.Country = input.Country;
                    crf1.Address1 = input.Address1;
                    crf1.Address2 = input.Address2;
                    crf1.City = input.City;
                    crf.CServices = input.CServices;
                    db.CorporateProfile.Add(crf1);
                    WellAIAppContext.Current.Session.SetString("CompanyName", crf.Name);
                }

                var result = await db.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository UpdateServiceCorporateProfile", null);
                return 0;
            }
        }

        public async Task<List<ProjectViewSRVModel>> GetUpCommingProjects(string tenantId)
        {
            try
            {
                var project = (from prj in db.Projects.Where(x => x.ServiceCompID.Equals(tenantId))
                               join crf in db.CorporateProfile on
                               prj.ServiceCompID equals crf.TenantId
                               join au in db.AuctionProposals on
                               prj.ProposalID equals au.ProposalId into aj
                               from auctionresult in aj.DefaultIfEmpty()
                               select new ProjectViewSRVModel()
                               {
                                   ProjectId = prj.ID,
                                   ProjectCode = prj.ProjectID,
                                   OperatorCompanyName = crf.Name,
                                   ExpectedStartDate = prj.ProposedStartDate.Value,
                                   Title = prj.ProjectTitle,
                                   Description = prj.ProjectDescription,
                                   ProjectStatusName = Convert.ToInt16(prj.ProjectStatus) == 0 ? "Upcoming" :
                                                        Convert.ToInt16(prj.ProjectStatus) == 1 ? "Active" : Convert.ToInt16(prj.ProjectStatus) == 3 ? "Suspend" : "Closed",
                                   Job = auctionresult.JobId ?? String.Empty
                               }).ToList();
                return await Task.FromResult(project);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetUpCommingProjects", null);
                return null;
            }
        }

        public Task<List<ProjectViewModel>> GetUpCommingProjectsForOperator(WellIdentityUser user, string RigId)
        {
            var checkwellFilter = RigId == DLL.Constants.NoSpecificWellFilterKey;

            try
            {
                //DWOP - Task data from DrillPlanDetails
                List<ProjectViewModel> result = null;
                var temp = (from prj in db.Projects
                            join well in db.WellRegister on prj.WellID equals well.well_id
                            join rig in db.rig_register on well.RigID equals rig.Rig_id
                            join crf in db.CorporateProfile on prj.ServiceCompID equals crf.TenantId
                            join au in db.AuctionProposals on prj.ProposalID equals au.ProposalId
                            join task in db.DrillPlanDetails on au.JobId equals task.TaskId into taskLj
                            from task in taskLj.DefaultIfEmpty()
                            where prj.OprTenantID.Equals(user.TenantId) /*&& prj.ProjectStatus != 3*/ &&
                                                    (rig.Rig_id == RigId && !checkwellFilter || checkwellFilter)
                            select new ProjectViewModel()
                            {
                                ProjectId = prj.ID,
                                ProjectCode = prj.ProjectID,
                                OperatorCompanyName = crf.Name,
                                ExpectedStartDate = prj.ProposedStartDate.Value,
                                Title = prj.ProjectTitle,
                                Description = prj.ProjectDescription,
                                ProjectStatusName = Convert.ToInt16(prj.ProjectStatus) == 0 ? "Upcoming" :
                                                     Convert.ToInt16(prj.ProjectStatus) == 1 ? "Active" : Convert.ToInt16(prj.ProjectStatus) == 2 ? "Completed" : "Suspended",//"Suspend":"Completed"
                                Job = task == null ? au.JobId : task.TaskName,
                                WellId = prj.WellID,
                                WellName = well.wellname,
                                RigName = rig.Rig_Name,
                                ActualStartDate = prj.ActualStart.Value,
                                ActualEndDate = prj.ActualEnd.Value,
                                RigId = rig.Rig_id,
                                Depth = task.Depth,
                                ModifyDate = prj.ModifyDate,
                                ProjectStatus = prj.ProjectStatus
                            }).OrderBy(o => o.ProjectStatus).ToList();


                if (user != null && user.WellUser.HasValue && user.WellUser.Value)
                {
                    var userwellIds = db.UserRigs.Where(x => x.UserId == user.Id).Select(x => x.RigID).ToList();

                    result = temp.Where(x => userwellIds.FirstOrDefault(y => y == x.RigId) != null && !checkwellFilter || checkwellFilter).ToList();
                }
                else
                    result = temp;

                //Filter Duplication records for Service 
                var resultAgg = result.GroupBy(x => x.ProjectId).Select(g => g.First()).ToList();
                return Task.FromResult(resultAgg);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetUpCommingProjectsForOperator", null);
                return null;
            }
        }

        public async Task<int> UpdatedUpCommingProjectsDetails(ProjectViewSRVModel input, string tenantId)
        {
            try
            {
                var result = await db.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository UpdatedUpCommingProjectsDetails", null);
                return 0;
            }
        }

        public async Task<List<ProjectViewSRVModel>> GetTechnicianName(string tenantId)
        {
            try
            {
                List<WellIdentityUser> users = db.WellIdentityUser.Where(x => x.TenantId.Equals(tenantId)).ToList();
                var NameList = (from us in users.Where(x => x.TenantId.Equals(tenantId))

                                select new ProjectViewSRVModel
                                {
                                }).ToList();

                return await Task.FromResult(NameList);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetTechnicianName", null);
                return null;
            }
        }

        public Task<UserViewSRVModel> GetPrimaryUserSRV(string tenantId)
        {
            try
            {
                UserViewSRVModel userViewModel = null;

                var user = _userManager.Users.Where(x => x.TenantId == tenantId && x.Primary.HasValue && x.Primary.Value).FirstOrDefault();
                if (user != null)
                {
                    userViewModel = new UserViewSRVModel();

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
                    userViewModel.AdditionalNotes = user.AdditionalNotes;
                    userViewModel.WellOfficeUser = user.WellUser.HasValue ? user.WellUser.Value : false;
                    userViewModel.Field = user.Field.HasValue ? user.Field.Value : false;

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
                }

                return Task.FromResult(userViewModel);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetPrimaryUserSRV", null);
                return null;
            }
        }

        public Task<List<WellAI.Advisor.Model.OperatingCompany.Models.ServiceOffering>> GetOperatingCompanyServices(string TenantID)
        {
            try
            {
                var Offerigs = db.CrmCompanies.Where(x => x.TenantId == TenantID).FirstOrDefault();

                if (Offerigs != null)
                {
                    var Categories = Offerigs.Category.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    var result = new List<WellAI.Advisor.Model.OperatingCompany.Models.ServiceOffering>();
                    var OprOfferings = db.serviceCategories.Where(x => Categories.Contains(x.ServiceCategoryId)).ToList();
                    foreach (var offers in OprOfferings)
                    {
                        result.Add(new Model.OperatingCompany.Models.ServiceOffering
                        {
                            ServiceOfferId = offers.ServiceCategoryId,
                            Title = offers.Name
                        });
                    }
                    return Task.FromResult(result);
                }
                else
                {
                    var result = new List<WellAI.Advisor.Model.OperatingCompany.Models.ServiceOffering>();
                    return Task.FromResult(result);
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetOperatingCompanyServices", null);
                return null;
            }
        }

        public Task<List<Model.ServiceCompany.Models.ServiceOfferingSRV>> GetOperatingOfferings(string tenantId)
        {
            try
            {
                var offering = db.OperatingCompanyOfferings.FirstOrDefault(x => x.OperatingTenantId == tenantId);

                if (offering != null)
                {
                    var ids = System.Text.Json.JsonSerializer.Deserialize<List<string>>(offering.Offerings);

                    var offers = db.OperatingOffers.Where(x => ids.Contains(x.Id)).ToList();

                    var resultoffers = new List<WellAI.Advisor.Model.ServiceCompany.Models.ServiceOfferingSRV>();
                    foreach (var offer in offers)
                    {
                        resultoffers.Add(new Model.ServiceCompany.Models.ServiceOfferingSRV
                        {
                            ServiceOfferId = offer.Id,
                            Title = offer.Name
                        });
                    }
                    return Task.FromResult(resultoffers);
                }
                else
                {
                    var resultoffers = new List<WellAI.Advisor.Model.ServiceCompany.Models.ServiceOfferingSRV>();
                    return Task.FromResult(resultoffers);
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetOperatingOfferings", null);
                return null;
            }
        }

        public Task<List<Model.ServiceCompany.Models.ServiceMSA>> GetMSAWellFilesFromOperatingTenants(List<string> tenantIds, string ServiceTenantId)
        {
            try
            {
                var provfiles = db.ProviderMSALinks
               .Where(x => x.ServiceTenantId == ServiceTenantId && tenantIds.Contains(x.OperationTenantId) && x.IsActive == true).ToList();
                tenantIds.Add(ServiceTenantId);
                List<WellFile> docs = new List<WellFile>();
                docs = db.WellFiles.Where(x => tenantIds.Contains(x.TenantId)).ToList();

                var result = new List<Model.ServiceCompany.Models.ServiceMSA>();

                foreach (var provFile in provfiles)
                {
                    try
                    {
                        string id = provFile.FileId;

                        var file = docs.FirstOrDefault(x => x.FileId.Equals(id));

                        if (file != null)
                        {
                            if (file.TenantId != null)
                            {
                                result.Add(new Model.ServiceCompany.Models.ServiceMSA
                                {
                                    MsaId = file.FileId,
                                    Attachment = file.FileName,
                                    Expiration = provFile.Expire,
                                    Status = provFile.IsActive.HasValue && provFile.IsActive.Value ? "Active" : "Inactive",
                                    Value = provFile.OperationTenantId,
                                    FileUploadTime = provFile.FileUploadTime
                                });
                            }
                            else
                            {
                                if (provFile.IsApproved == true)
                                {
                                    result.Add(new Model.ServiceCompany.Models.ServiceMSA
                                    {
                                        MsaId = file.FileId,
                                        Attachment = file.FileName,
                                        Expiration = provFile.Expire,
                                        Status = provFile.IsActive.HasValue && provFile.IsActive.Value ? "Active" : "Inactive",
                                        Value = provFile.OperationTenantId,
                                        FileUploadTime = provFile.FileUploadTime
                                    });
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                        customErrorHandler.WriteError(ex, "CommonRepository GetMSAWellFilesFromOperatingTenants", null);
                        return null;
                    }
                }

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetMSAWellFilesFromOperatingTenants", null);
                return null;
            }
        }

        public Task<List<Model.ServiceCompany.Models.ServiceInsurance>> GetInsuranceWellFilesFromOperatingTenants(List<string> tenantIds, string ServiceTenantId)
        {
            try
            {
                var provfiles = db.ProviderInsuranceLinks
               .Where(x => x.ServiceTenantId == ServiceTenantId && tenantIds.Contains(x.OperationTenantId) && x.Status == "Active").ToList();

                var docs = db.WellFiles.Where(x => tenantIds.Contains(x.TenantId)).ToList();

                var result = new List<Model.ServiceCompany.Models.ServiceInsurance>();

                foreach (var provFile in provfiles)
                {
                    var file = docs.First(x => x.FileId == provFile.FileId);

                    result.Add(new Model.ServiceCompany.Models.ServiceInsurance
                    {
                        InsId = provFile.FileId,
                        Attachment = file.FileName,
                        Expiration = provFile.Expire,
                        Status = provFile.Status,
                        Value = provFile.OperationTenantId
                    });
                }

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetInsuranceWellFilesFromOperatingTenants", null);
                return null;
            }
        }

        ////Phase II Changes - 05/19/2021
        public Task<List<Model.ServiceCompany.Models.ServiceInsurance>> GetInsuranceFilesFromServiceTenants(List<string> tenantIds, string serviceTenantId, string operatingTenantId)
        {
            try
            {
                var docs = db.WellFiles.Where(x => x.TenantId == serviceTenantId && x.Category == "Insurance").ToList();

                var result = new List<Model.ServiceCompany.Models.ServiceInsurance>();

                foreach (var provFile in docs)
                {
                    var file = docs.FirstOrDefault(x => x.FileId == provFile.FileId);

                    result.Add(new Model.ServiceCompany.Models.ServiceInsurance
                    {
                        InsId = provFile.FileId,
                        Attachment = provFile.FileName,
                        Expiration = null,
                        Status = null,
                        Value = null
                    });
                    //}
                }

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetInsuranceWellFilesFromOperatingTenants", null);
                return null;
            }
        }

        public Task<List<CorporateProfile>> GetOperatingCompanies()
        {
            try
            {
                var SrvTenantIds = (from crp in db.CorporateProfile
                                    join crmp in db.CrmCompanies on crp.TenantId equals crmp.TenantId

                                    select new
                                    {
                                        TenantId = crp.TenantId
                                    }
                                        ).ToList();
                var corpProfiles = db.CorporateProfile.Where(x => !SrvTenantIds.Select(y => y.TenantId).Contains(x.TenantId)).Distinct().ToList();

                return Task.FromResult(corpProfiles);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetOperatingCompanies", null);
                return null;
            }
        }

        public Task<List<MessageQueue>> GetNotifications(string userId)
        {
            try
            {
                var result = db.MessageQueues.Where(x => x.To_id == userId && x.IsActive == 1).ToList();
                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetNotifications", null);
                return null;
            }
        }

        public int AddNotifications(MessageQueue messageQueue)
        {
            try
            {
                db.MessageQueues.Add(messageQueue);
                db.SaveChanges();
                return messageQueue.Messagequeue_id;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository AddNotifications", null);
                return 0;
            }
        }

        public bool UpdateNotifications(string toId)
        {
            try
            {
                var result = db.MessageQueues.Where(x => x.To_id == toId && x.IsActive == 1).ToList();
                if (result != null && result.Count() > 0)
                {
                    db.MessageQueues.RemoveRange(result);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository UpdateNotifications", null);
                return false;
            }
        }

        public bool GetUserAvailibilityStatus(string userId)
        {
            try
            {
                var result = db.UserActivityStatuses.FirstOrDefault(x => x.UserId == userId);
                if (result == null)
                    return false;
                else
                    return result.IsLoggedIn;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetUserAvailibilityStatus", null);
                return false;
            }
        }

        public void UpdateUserStatus(string userId, bool status)
        {
            try
            {
                var result = db.UserActivityStatuses.FirstOrDefault(x => x.UserId == userId);

                if (result == null)
                {
                    UserActivityStatus userActivityStatus = new UserActivityStatus
                    {
                        UserId = userId,
                        IsLoggedIn = status,
                        LoggedInTime = DateTime.Now
                    };
                    db.UserActivityStatuses.Add(userActivityStatus);
                    db.SaveChanges();
                }
                else
                {
                    result.IsLoggedIn = status;
                    result.LoggedInTime = DateTime.Now;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository UpdateUserStatus", null);
            }
        }

        //Call Count and MessageCount 
        public Task<int> GetUserNotificationCount(string userId, string notificationType)
        {
            try
            {
                var userType = (from usr in db.Users
                                where usr.Id == userId
                                select new
                                {
                                    usr.WellUser,
                                    usr.TenantId
                                }
                           ).ToList();

                if (userType != null && userType.Count > 0)
                {
                    if (userType[0].WellUser == false || userType[0].WellUser == null) //
                    {
                        int cnt = 0;

                        if (notificationType == "c")
                        {
                            cnt = (from usrmsg in db.MessageQueues
                                   join fromuser in _userManager.Users on usrmsg.From_id equals fromuser.Id
                                   where usrmsg.To_id == userId && usrmsg.IsActive == 1 && usrmsg.Type == 0 && fromuser.TenantId == userType[0].TenantId
                                   select usrmsg
                                           ).Count();
                        }
                        else
                        {
                            cnt = (from usrmsg in db.MessageQueues
                                   join fromuser in _userManager.Users on usrmsg.From_id equals fromuser.Id
                                   where usrmsg.To_id == userId && usrmsg.IsActive == 1 && usrmsg.Type != 0 && fromuser.TenantId == userType[0].TenantId
                                   select usrmsg
                                           ).Count();
                        }


                        return Task.FromResult(cnt);

                        //var messageDetails = (from message in db.MessageQueues
                        //                      join user in _userManager.Users on message.From_id equals user.Id into gj
                        //                      from x in gj.DefaultIfEmpty()
                        //                      where message.To_id == userId && message.IsActive == 1/* || (message.From_id== userId && message.IsActive == 1)*/
                        //                      select new MessageQueue
                        //                      {
                        //                          Messagequeue_id = message.Messagequeue_id,
                        //                          From_id = (message.From_id.Contains("Support") ? message.From_id : x.FirstName + " " + x.LastName),
                        //                          To_id = message.To_id,
                        //                          Type = message.Type,
                        //                          EntityId = message.EntityId,
                        //                          RigId = message.RigId,
                        //                          TaskName = message.TaskName,
                        //                          IsActive = message.IsActive,
                        //                          JobName = message.JobName,
                        //                          CreatedDate = message.CreatedDate
                        //                      }).Distinct().OrderByDescending(x => x.CreatedDate).ToList();
                        //foreach (var item in messageDetails)
                        //{
                        //    if (item.Type == 4)
                        //    {
                        //        item.From_id = db.RigRegisters.Where(r => r.Rig_Id == item.RigId).Select(r => r.Rig_Name).FirstOrDefault() + ":" + item.TaskName;
                        //    }
                        //    if (item.JobName == "Chatmessage")
                        //    {
                        //        item.From_id = item.From_id + " Says:" + item.TaskName;
                        //        item.TaskName = item.From_id;
                        //    }

                        //    if (item.Type == 5 || item.Type == 3)
                        //    {
                        //        var auctionproposalObj = db.AuctionProposals.Where(x => x.RigId == item.RigId && x.ProposalId == item.EntityId).FirstOrDefault();

                        //        if (item.JobName == "Request Closing")
                        //        {
                        //            item.CreatedDate = auctionproposalObj.AuctionEnd;
                        //        }
                        //        else if (item.JobName == "Bid Accepted" || item.JobName == "Bid Rejected" || item.JobName == "Bid")
                        //        {
                        //            item.TaskName = item.TaskName;
                        //        }
                        //    }
                        //    else if (item.Type == 6)
                        //    {
                        //        var auctionproposalObj = db.AuctionProposals.Where(x => x.RigId == item.RigId && x.ProposalId == item.EntityId).FirstOrDefault();

                        //        if (item.JobName == "Service Request")
                        //        {
                        //            item.CreatedDate = auctionproposalObj.AuctionEnd;
                        //        }
                        //        else if (item.JobName == "Service Accepted" || item.JobName == "Service Rejected" || item.JobName == "Service Closing")
                        //        {
                        //            //Phase II Changes
                        //            //item.TaskName = item.JobName + " " + item.TaskName;
                        //            item.TaskName = item.TaskName;
                        //        }
                        //    }
                        //}

                        //return Task.FromResult(0);
                    }
                    else
                    {
                        IEnumerable<string> usersListArray = from usr in db.Users
                                                             where usr.TenantId == userType[0].TenantId
                                                             select usr.Id;

                        //var messageTenant = (from usrmsg in db.MessageQueues
                        //                     join fromuser in _userManager.Users on usrmsg.From_id equals fromuser.Id
                        //                     where usrmsg.IsActive == 1 && fromuser.TenantId == userType[0].TenantId
                        //                     select new MessageQueue
                        //                     {
                        //                         From_id = Convert.ToString(usrmsg.From_id),
                        //                     }).Distinct().ToList();

                        //var messageDetails1 = (from usrmsg in db.MessageQueues
                        //                       where usrmsg.IsActive == 1
                        //                       select new MessageQueue
                        //                       {
                        //                           Messagequeue_id = usrmsg.Messagequeue_id,
                        //                           From_id = usrmsg.From_id,
                        //                           To_id = usrmsg.To_id,
                        //                           Type = usrmsg.Type,
                        //                           EntityId = usrmsg.EntityId,
                        //                           RigId = usrmsg.RigId,
                        //                           TaskName = usrmsg.TaskName,
                        //                           IsActive = usrmsg.IsActive,
                        //                           JobName = usrmsg.JobName,
                        //                           CreatedDate = usrmsg.CreatedDate,
                        //                           //TenantId = usrmsg.TenantId
                        //                       }).Distinct().OrderByDescending(x => x.CreatedDate).ToList();

                        int cnt = 0;

                        if (notificationType == "c")
                        {
                            cnt = (from usrmsg in db.MessageQueues
                                   join fromuser in _userManager.Users on usrmsg.From_id equals fromuser.Id
                                   where usrmsg.IsActive == 1 && usrmsg.Type == 0 && fromuser.TenantId == userType[0].TenantId
                                   select usrmsg
                                           ).Count();
                        }
                        else
                        {
                            cnt = (from usrmsg in db.MessageQueues
                                   join fromuser in _userManager.Users on usrmsg.From_id equals fromuser.Id
                                   where usrmsg.IsActive == 1 && usrmsg.Type != 0 && fromuser.TenantId == userType[0].TenantId
                                   select usrmsg
                                           ).Count();
                        }


                        return Task.FromResult(cnt);
                    }
                }
                else
                {
                    //List<MessageQueue> msgqueue = new List<MessageQueue>();
                    return Task.FromResult(0);
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetUserNotificationDetails", null);
                List<MessageQueue> msgqueue = new List<MessageQueue>();
                return Task.FromResult(0);
            }
        }

        public List<MessageQueue> GetUserNotificationDetails(string userId)
        {
            try
            {



                var userType = (from usr in db.Users
                                where usr.Id == userId
                                select new
                                {
                                    usr.WellUser,
                                    usr.TenantId
                                }
                           ).ToList();

                if (userType != null && userType.Count > 0)
                {
                    if (userType[0].WellUser == false || userType[0].WellUser == null) //
                    {
                        var messageDetails = (from message in db.MessageQueues
                                              join user in _userManager.Users on message.From_id equals user.Id into gj
                                              from x in gj.DefaultIfEmpty()
                                              where message.To_id == userId && message.IsActive == 1 /*|| (message.From_id== userId && message.IsActive == 1)*/
                                              select new MessageQueue
                                              {
                                                  Messagequeue_id = message.Messagequeue_id,
                                                  From_id = (message.From_id.Contains("Support") ? message.From_id : x.FirstName + " " + x.LastName),
                                                  To_id = message.To_id,
                                                  Type = message.Type,
                                                  EntityId = message.EntityId,
                                                  RigId = message.RigId,
                                                  TaskName = message.TaskName,
                                                  IsActive = message.IsActive,
                                                  JobName = message.JobName,
                                                  CreatedDate = message.CreatedDate
                                              }).Distinct().OrderByDescending(x => x.CreatedDate).Take(10).ToList();

                        //WellAIAppContext.Current.Session.SetString("CallCount", messageDetails.Where(x => x.Type == 0).Count().ToString());
                        //WellAIAppContext.Current.Session.SetString("MessageCount", messageDetails.Where(x => x.Type != 0).Count().ToString());

                        // messageDetails = messageDetails.Take(7).ToList();

                        foreach (var item in messageDetails)
                        {
                            if (item.Type == 4 || item.Type == 5 || item.Type == 6 || item.Type == 3 || item.JobName == "Chatmessage")
                            {
                                if (item.Type == 4)
                                {
                                    item.From_id = db.RigRegisters.Where(r => r.Rig_Id == item.RigId).Select(r => r.Rig_Name).FirstOrDefault() + ":" + item.TaskName;
                                }
                                if (item.JobName == "Chatmessage")
                                {
                                    item.From_id = item.From_id + " Says:" + item.TaskName;
                                    item.TaskName = item.From_id;
                                }

                                if (item.Type == 5 || item.Type == 3)
                                {
                                    var auctionproposalObj = db.AuctionProposals.Where(x => x.RigId == item.RigId && x.ProposalId == item.EntityId).FirstOrDefault();

                                    if (item.JobName == "Request Closing")
                                    {
                                        item.CreatedDate = auctionproposalObj.AuctionEnd;
                                    }
                                    //else if (item.JobName == "Bid Accepted" || item.JobName == "Bid Rejected" || item.JobName == "Bid")
                                    //{
                                    //    item.TaskName = item.TaskName;
                                    //}
                                }
                                else if (item.Type == 6)
                                {
                                    var auctionproposalObj = db.AuctionProposals.Where(x => x.RigId == item.RigId && x.ProposalId == item.EntityId).FirstOrDefault();

                                    if (item.JobName == "Service Request")
                                    {
                                        item.CreatedDate = auctionproposalObj.AuctionEnd;
                                    }
                                    //else if (item.JobName == "Service Accepted" || item.JobName == "Service Rejected" || item.JobName == "Service Closing")
                                    //{
                                    //    //Phase II Changes
                                    //    //item.TaskName = item.JobName + " " + item.TaskName;
                                    //    item.TaskName = item.TaskName;
                                    //}
                                }
                            }

                        }

                        return messageDetails;
                    }
                    else
                    {
                        IEnumerable<string> usersListArray = from usr in db.Users
                                                             where usr.TenantId == userType[0].TenantId
                                                             select usr.Id;

                        var messageTenant = (from usrmsg in db.MessageQueues
                                             join fromuser in _userManager.Users on usrmsg.From_id equals fromuser.Id
                                             where usrmsg.IsActive == 1 && fromuser.TenantId == userType[0].TenantId
                                             select new MessageQueue
                                             {
                                                 From_id = Convert.ToString(usrmsg.From_id),
                                             }).Distinct().ToList();

                        var messageDetails1 = (from usrmsg in db.MessageQueues
                                               where usrmsg.IsActive == 1
                                               select new MessageQueue
                                               {
                                                   Messagequeue_id = usrmsg.Messagequeue_id,
                                                   From_id = usrmsg.From_id,
                                                   To_id = usrmsg.To_id,
                                                   Type = usrmsg.Type,
                                                   EntityId = usrmsg.EntityId,
                                                   RigId = usrmsg.RigId,
                                                   TaskName = usrmsg.TaskName,
                                                   IsActive = usrmsg.IsActive,
                                                   JobName = usrmsg.JobName,
                                                   CreatedDate = usrmsg.CreatedDate,
                                                   //TenantId = usrmsg.TenantId
                                               }).Distinct().OrderByDescending(x => x.CreatedDate).ToList();

                        var messageDetails = (from usrmsg in messageDetails1
                                              join msg in messageTenant on usrmsg.From_id equals msg.From_id
                                              join user in _userManager.Users on usrmsg.From_id equals user.Id into gj
                                              from x in gj.DefaultIfEmpty()
                                              where usrmsg.IsActive == 1
                                              select new MessageQueue
                                              {
                                                  Messagequeue_id = usrmsg.Messagequeue_id,
                                                  From_id = (usrmsg.From_id.Contains("Support") ? usrmsg.From_id : x.FirstName + " " + x.LastName),
                                                  To_id = usrmsg.To_id,
                                                  Type = usrmsg.Type,
                                                  EntityId = usrmsg.EntityId,
                                                  RigId = usrmsg.RigId,
                                                  TaskName = usrmsg.TaskName,
                                                  IsActive = usrmsg.IsActive,
                                                  JobName = usrmsg.JobName,
                                                  CreatedDate = usrmsg.CreatedDate,
                                              }).Distinct().OrderByDescending(x => x.CreatedDate).ToList();

                        foreach (var item in messageDetails)
                        {
                            if (item.Type == 4)
                            {
                                item.From_id = db.RigRegisters.Where(r => r.Rig_Id == item.RigId).Select(r => r.Rig_Name).FirstOrDefault() + ":" + item.TaskName;
                            }
                            if (item.JobName == "Chatmessage")
                            {
                                item.From_id = item.From_id + " Says:" + item.TaskName;
                                item.TaskName = item.From_id;
                            }

                            if (item.Type == 5 || item.Type == 3)
                            {
                                var auctionproposalObj = db.AuctionProposals.Where(x => x.RigId == item.RigId && x.ProposalId == item.EntityId).FirstOrDefault();
                                if (item.JobName == "Rquest Closing")
                                {
                                    item.CreatedDate = auctionproposalObj.AuctionEnd;
                                }
                                else if (item.JobName == "Bid Accepted" || item.JobName == "Bid Rejected" || item.JobName == "Bid")
                                {
                                    //Phase II Changes
                                    //item.TaskName = item.JobName + " " + item.TaskName;
                                    item.TaskName = item.TaskName;
                                }
                            }
                            else if (item.Type == 6)
                            {
                                var auctionproposalObj = db.AuctionProposals.Where(x => x.RigId == item.RigId && x.ProposalId == item.EntityId).FirstOrDefault();
                                if (item.JobName == "Service Request")
                                {
                                    item.CreatedDate = auctionproposalObj.AuctionEnd;
                                }
                                else if (item.JobName == "Service Accepted" || item.JobName == "Service Rejected" || item.JobName == "Service Closing")
                                {
                                    //Phase II Changes
                                    //item.TaskName = item.JobName + " " + item.TaskName;
                                    item.TaskName = item.TaskName;
                                }
                            }
                        }
                        return messageDetails;
                    }
                }
                else
                {
                    List<MessageQueue> msgqueue = new List<MessageQueue>();
                    return msgqueue;
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetUserNotificationDetails", null);
                List<MessageQueue> msgqueue = new List<MessageQueue>();
                return msgqueue;
            }
        }

        //public List<MessageQueue> GetUserNotificationDetails(string userId)
        //{
        //    try
        //    {



        //        var userType = (from usr in db.Users
        //                        where usr.Id == userId
        //                        select new
        //                        {
        //                            usr.WellUser,
        //                            usr.TenantId
        //                        }
        //                   ).ToList();

        //        if (userType != null && userType.Count > 0)
        //        {
        //            if (userType[0].WellUser == false || userType[0].WellUser == null) //
        //            {
        //                var messageDetails = (from message in db.MessageQueues
        //                                      join user in _userManager.Users on message.From_id equals user.Id into gj
        //                                      from x in gj.DefaultIfEmpty()
        //                                      where message.To_id == userId && message.IsActive == 1/* || (message.From_id== userId && message.IsActive == 1)*/
        //                                      select new MessageQueue
        //                                      {
        //                                          Messagequeue_id = message.Messagequeue_id,
        //                                          From_id = (message.From_id.Contains("Support") ? message.From_id : x.FirstName + " " + x.LastName),
        //                                          To_id = message.To_id,
        //                                          Type = message.Type,
        //                                          EntityId = message.EntityId,
        //                                          RigId = message.RigId,
        //                                          TaskName = message.TaskName,
        //                                          IsActive = message.IsActive,
        //                                          JobName = message.JobName,
        //                                          CreatedDate = message.CreatedDate
        //                                      }).Distinct().OrderByDescending(x => x.CreatedDate).ToList();
        //                foreach (var item in messageDetails)
        //                {
        //                    if (item.Type == 4)
        //                    {
        //                        item.From_id = db.RigRegisters.Where(r => r.Rig_Id == item.RigId).Select(r => r.Rig_Name).FirstOrDefault() + ":" + item.TaskName;
        //                    }
        //                    if (item.JobName == "Chatmessage")
        //                    {
        //                        item.From_id = item.From_id + " Says:" + item.TaskName;
        //                        item.TaskName = item.From_id;
        //                    }

        //                    if (item.Type == 5 || item.Type == 3)
        //                    {
        //                        var auctionproposalObj = db.AuctionProposals.Where(x => x.RigId == item.RigId && x.ProposalId == item.EntityId).FirstOrDefault();

        //                        if (item.JobName == "Request Closing")
        //                        {
        //                            item.CreatedDate = auctionproposalObj.AuctionEnd;
        //                        }
        //                        else if (item.JobName == "Bid Accepted" || item.JobName == "Bid Rejected" || item.JobName == "Bid")
        //                        {
        //                            item.TaskName = item.TaskName;
        //                        }
        //                    }
        //                    else if (item.Type == 6)
        //                    {
        //                        var auctionproposalObj = db.AuctionProposals.Where(x => x.RigId == item.RigId && x.ProposalId == item.EntityId).FirstOrDefault();

        //                        if (item.JobName == "Service Request")
        //                        {
        //                            item.CreatedDate = auctionproposalObj.AuctionEnd;
        //                        }
        //                        else if (item.JobName == "Service Accepted" || item.JobName == "Service Rejected" || item.JobName == "Service Closing")
        //                        {
        //                            //Phase II Changes
        //                            //item.TaskName = item.JobName + " " + item.TaskName;
        //                            item.TaskName = item.TaskName;
        //                        }
        //                    }
        //                }

        //                return messageDetails;
        //            }
        //            else
        //            {
        //                IEnumerable<string> usersListArray = from usr in db.Users
        //                                                     where usr.TenantId == userType[0].TenantId
        //                                                     select usr.Id;

        //                var messageTenant = (from usrmsg in db.MessageQueues
        //                                     join fromuser in _userManager.Users on usrmsg.From_id equals fromuser.Id
        //                                     where usrmsg.IsActive == 1 && fromuser.TenantId == userType[0].TenantId
        //                                     select new MessageQueue
        //                                     {
        //                                         From_id = Convert.ToString(usrmsg.From_id),
        //                                     }).Distinct().ToList();

        //                var messageDetails1 = (from usrmsg in db.MessageQueues
        //                                       where usrmsg.IsActive == 1
        //                                       select new MessageQueue
        //                                       {
        //                                           Messagequeue_id = usrmsg.Messagequeue_id,
        //                                           From_id = usrmsg.From_id,
        //                                           To_id = usrmsg.To_id,
        //                                           Type = usrmsg.Type,
        //                                           EntityId = usrmsg.EntityId,
        //                                           RigId = usrmsg.RigId,
        //                                           TaskName = usrmsg.TaskName,
        //                                           IsActive = usrmsg.IsActive,
        //                                           JobName = usrmsg.JobName,
        //                                           CreatedDate = usrmsg.CreatedDate,
        //                                           //TenantId = usrmsg.TenantId
        //                                       }).Distinct().OrderByDescending(x => x.CreatedDate).ToList();

        //                var messageDetails = (from usrmsg in messageDetails1
        //                                      join msg in messageTenant on usrmsg.From_id equals msg.From_id
        //                                      join user in _userManager.Users on usrmsg.From_id equals user.Id into gj
        //                                      from x in gj.DefaultIfEmpty()
        //                                      where usrmsg.IsActive == 1
        //                                      select new MessageQueue
        //                                      {
        //                                          Messagequeue_id = usrmsg.Messagequeue_id,
        //                                          From_id = (usrmsg.From_id.Contains("Support") ? usrmsg.From_id : x.FirstName + " " + x.LastName),
        //                                          To_id = usrmsg.To_id,
        //                                          Type = usrmsg.Type,
        //                                          EntityId = usrmsg.EntityId,
        //                                          RigId = usrmsg.RigId,
        //                                          TaskName = usrmsg.TaskName,
        //                                          IsActive = usrmsg.IsActive,
        //                                          JobName = usrmsg.JobName,
        //                                          CreatedDate = usrmsg.CreatedDate,
        //                                      }).Distinct().OrderByDescending(x => x.CreatedDate).ToList();

        //                foreach (var item in messageDetails)
        //                {
        //                    if (item.Type == 4)
        //                    {
        //                        item.From_id = db.RigRegisters.Where(r => r.Rig_Id == item.RigId).Select(r => r.Rig_Name).FirstOrDefault() + ":" + item.TaskName;
        //                    }
        //                    if (item.JobName == "Chatmessage")
        //                    {
        //                        item.From_id = item.From_id + " Says:" + item.TaskName;
        //                        item.TaskName = item.From_id;
        //                    }

        //                    if (item.Type == 5 || item.Type == 3)
        //                    {
        //                        var auctionproposalObj = db.AuctionProposals.Where(x => x.RigId == item.RigId && x.ProposalId == item.EntityId).FirstOrDefault();
        //                        if (item.JobName == "Rquest Closing")
        //                        {
        //                            item.CreatedDate = auctionproposalObj.AuctionEnd;
        //                        }
        //                        else if (item.JobName == "Bid Accepted" || item.JobName == "Bid Rejected" || item.JobName == "Bid")
        //                        {
        //                            //Phase II Changes
        //                            //item.TaskName = item.JobName + " " + item.TaskName;
        //                            item.TaskName = item.TaskName;
        //                        }
        //                    }
        //                    else if (item.Type == 6)
        //                    {
        //                        var auctionproposalObj = db.AuctionProposals.Where(x => x.RigId == item.RigId && x.ProposalId == item.EntityId).FirstOrDefault();
        //                        if (item.JobName == "Service Request")
        //                        {
        //                            item.CreatedDate = auctionproposalObj.AuctionEnd;
        //                        }
        //                        else if (item.JobName == "Service Accepted" || item.JobName == "Service Rejected" || item.JobName == "Service Closing")
        //                        {
        //                            //Phase II Changes
        //                            //item.TaskName = item.JobName + " " + item.TaskName;
        //                            item.TaskName = item.TaskName;
        //                        }
        //                    }
        //                }
        //                return messageDetails;
        //            }
        //        }
        //        else
        //        {
        //            List<MessageQueue> msgqueue = new List<MessageQueue>();
        //            return msgqueue;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
        //        customErrorHandler.WriteError(ex, "CommonRepository GetUserNotificationDetails", null);
        //        List<MessageQueue> msgqueue = new List<MessageQueue>();
        //        return msgqueue;
        //    }
        //}

        public void UpdateNotificationStatus(int messageQueueId)
        {
            try
            {
                var result = db.MessageQueues.Where(x => x.Messagequeue_id == messageQueueId).FirstOrDefault();
                if (result != null)
                {
                    result.IsActive = 0;

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository UpdateNotificationStatus", null);
            }
        }

        public List<CrmCompanies> GetCategorywiseCompanyDetail(string categoryId)
        {
            try
            {
                var companyDetails = (from c in db.CrmCompanies
                                      where c.Category.Contains(categoryId)
                                      select c).ToList();

                return companyDetails;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetCategorywiseCompanyDetail", null);
                return null;
            }
        }

        public bool NotificationExists(string entityId, string toId)
        {
            try
            {
                bool value = db.MessageQueues.Any(x => x.To_id == toId && x.EntityId == entityId);
                return value;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository NotificationExists", null);
                return false;
            }
        }

        //Phase II Changes - 01/19/2021 - UpdateMSAApprovalStatus
        public async Task<int> UpdateMSAApprovalStatus(string fileId, bool status, string userid, ServiceTenantRepository servRepo)
        {
            try
            {
                int result = 0;
                string serviceUserid = "", operatingDirId = "", companyName = "", fileName = "";
                var file = db.ProviderMSALinks.Where(x => x.FileId == fileId).FirstOrDefault();
                var serviceFile = db.WellFiles.Where(x => x.FileId == fileId).FirstOrDefault();
                var corpProfile = db.CorporateProfile.Where(x => x.TenantId == file.OperationTenantId).FirstOrDefault();
                var operatingDir = await servRepo.GetOperatingDirectoryID(file.ServiceTenantId, corpProfile.TenantId);

                if (corpProfile != null)
                {
                    companyName = corpProfile.Name;
                }
                if (serviceFile != null)
                {
                    serviceUserid = serviceFile.UserId;
                    fileName = serviceFile.FileName;
                }
                if (operatingDir != null)
                {
                    operatingDirId = operatingDir.ProviderId;
                }

                if (file != null)
                {
                    file.IsApproved = status;
                    result = await db.SaveChangesAsync();
                }

                MessageQueue messageQueue = new MessageQueue { From_id = userid, To_id = serviceUserid, EntityId = Convert.ToString(operatingDirId), Type = Convert.ToInt32(7), IsActive = 1, TaskName = "MSA Document " + fileName + " " + (status == true ? "Approved" : "Rejected") + " by " + companyName + ". ", JobName = (status == true ? "MSA Approval" : "MSA Rejection"), CreatedDate = DateTime.Now };
                db.MessageQueues.Add(messageQueue);
                db.SaveChanges();

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository UpdateMSAApprovalStatus", null);
                return 0;
            }
        }

        /// <summary>
        /// Phase II changes 02/08/2021 -
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public async Task<int> DeactivateFile(string fileId)
        {
            try
            {
                int result = 0;

                var file = db.ProviderMSALinks.FirstOrDefault(x => x.FileId == fileId);

                if (file != null)
                {
                    file.IsActive = false;

                    result = await db.SaveChangesAsync();
                }

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository DeactivateFile", null);
                return 0;
            }
        }

        public async Task<int> CreateDepthPermission(RigsDepth_Permission DepthValue)
        {
            try
            {
                int result = 0;
                var exitsRigs = db.RigsDepth_Permissions.Where(x => x.RigId == DepthValue.RigId && x.WellId == DepthValue.WellId).FirstOrDefault();
                if (exitsRigs != null)
                {
                    exitsRigs.DepthPermission = DepthValue.DepthPermission;
                    await db.SaveChangesAsync();
                }
                else
                {
                    if (DepthValue != null)
                    {
                        db.RigsDepth_Permissions.Add(DepthValue);

                        result = await db.SaveChangesAsync();
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository CreateDepthPermission", null);
                return 0;
            }
        }

        /// <summary>
        /// Phase II Changes - 03/01/2021 - Update Vendor PreferredStatus (1-Welcome,2-Authorized,3-Preferred)
        /// Welcome - MSA - False, Well Depth - False
        /// Authorize - MSA - True, Well Depth - False
        /// Preferred - MSA - True, Well Depth - True
        /// </summary>
        /// <param name="operTenantId"></param>
        /// <param name="servTenantId"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int UpdateVendorPreferredStatus(string operTenantId, string servTenantId)
        {
            try
            {
                int result = 0;
                var depthPermissionsCount = db.RigsDepth_Permissions.Where(x => x.OprTenantId == operTenantId.ToString() && x.SerTenantId == servTenantId.ToString() && x.DepthPermission == true).Count();

                var msaApprovalsCount = db.ProviderMSALinks.Where(x => x.OperationTenantId == operTenantId.ToString() && x.ServiceTenantId == servTenantId.ToString() && x.IsApproved == true && x.IsActive == true).Count();

                if (Convert.ToInt32(msaApprovalsCount) == 0 && Convert.ToInt32(depthPermissionsCount) == 0)
                {
                    result = 1; //Welcome
                }
                else if (Convert.ToInt32(msaApprovalsCount) > 0 && Convert.ToInt32(depthPermissionsCount) == 0)
                {
                    result = 2; //Authorized
                }
                else if (Convert.ToInt32(msaApprovalsCount) > 0 && Convert.ToInt32(depthPermissionsCount) > 0)
                {
                    result = 3; //Preferred
                }

                return Convert.ToInt16(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository UpdateVendorPreferredStatus", null);
                return 0;
            }
        }

        /// <summary>
        /// Subscription
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public Task<ProductSubscriptionModel> GetProductSubscription(string tenantId)
        {
            try
            {
                var result = db.Subscription.FirstOrDefault(x => x.TenantId == tenantId);

                return Task.FromResult(result ?? new ProductSubscriptionModel());
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetProductSubscription", null);
                return null;
            }
        }

        //Phase II Changes - 03/16/2021
        public async Task<int> SaveUsersession(UserSessions user)
        {
            try
            {
                int Result = 0;

                if (user != null)
                {
                    db.UserSessions.Add(user);
                    Result = await db.SaveChangesAsync();
                }

                return await Task.FromResult(Result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository SaveUsersession", null);
                return 0;
            }
        }

        //Phase II Changes - 03/16/2021
        public async Task<int> DeleteUsersession(string Email)
        {
            try
            {
                int Result = 0;
                var UserSessionExits = db.UserSessions.Where(x => x.UserName == Email).ToList();
                foreach (var user in UserSessionExits)
                {
                    db.UserSessions.Remove(user);
                    Result = await db.SaveChangesAsync();
                }

                return Result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository DeleteUsersession", null);
                return 0;
            }
        }

        //Phase II Changes - 05/20/21
        public async Task<int> UpdateProviderInsuranceLink(string serviceTenantId, string operTenantId, string fileId, DateTime? expire)
        {
            try
            {
                var insuranceProfileLink = db.ProviderInsuranceLinks.FirstOrDefault(x => x.ServiceTenantId == serviceTenantId && x.OperationTenantId == operTenantId);

                if (insuranceProfileLink == null)
                {
                    var newInsuranceProfileLink = new ProviderInsuranceLink
                    {
                        Id = Guid.NewGuid().ToString("D"),
                        ServiceTenantId = serviceTenantId,
                        OperationTenantId = operTenantId,
                        FileId = fileId,
                        Expire = expire,
                        Status = "1"
                    };
                    db.ProviderInsuranceLinks.Add(newInsuranceProfileLink);
                }
                else
                {
                    insuranceProfileLink.ServiceTenantId = serviceTenantId;
                    insuranceProfileLink.OperationTenantId = operTenantId;
                    insuranceProfileLink.FileId = fileId;
                    insuranceProfileLink.Expire = expire;
                    insuranceProfileLink.Status = "1";
                    db.ProviderInsuranceLinks.Update(insuranceProfileLink);
                }
                var result = await db.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository UpdateProviderInsuranceLink", null);
                return 0;
            }
        }

        //phase II Changes - 05/22/2021
        public async Task<int> CreateWellChecklist(string TenantId, string WellId)
        {
            CustomErrorHandlerForRepository customErrorHandlerGen = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
            try
            {
                var wellName = db.WellRegister.Where(x => x.well_id == WellId).FirstOrDefault();

                List<WellCheckListDetailModel> wellCheckListDetail = new List<WellCheckListDetailModel>();

                var wellCheckLists = db.WellCheckList.FirstOrDefault(x => x.TenantID == TenantId && x.WellId == WellId);
                if (wellCheckLists != null)
                {
                    wellCheckListDetail = JsonConvert.DeserializeObject<List<WellCheckListDetailModel>>(wellCheckLists.CheckList);
                    return 1;
                }
                else
                {
                    try
                    {
                        var taskList = await db.Tasks.Where(x => x.IsActive == true).ToListAsync();
                    }
                    catch (Exception ex1)
                    {
                        customErrorHandlerGen.WriteError(ex1, "CommonRepository CreateWellChecklist_1", null);
                        return 0;
                    }

                    try
                    {
                        wellCheckListDetail = await (from task in db.Tasks
                                                     join st in db.Stages on task.StageType equals st.Id into st1
                                                     from st in st1.DefaultIfEmpty()
                                                     join ct in db.CategoryTasks on task.TaskId equals ct.TaskId
                                                     join Category in db.serviceCategories on ct.ServiceCategoryId equals Category.ServiceCategoryId
                                                     where task.IsActive == true
                                                     select new WellCheckListDetailModel
                                                     {
                                                         WellTaskId = task.TaskId,
                                                         WellTaskName = task.Name,
                                                         Day = task.Day,
                                                         Depth = task.Depth,
                                                         Duration = task.Duration,
                                                         Time = (System.TimeSpan?)task.ScheduleTime,
                                                         Type = task.IsSpecialServices,
                                                         IsBiddable = task.IsBiddable,
                                                         StageType = st.Name,
                                                         ServiceCategory = Category.Name
                                                     }).ToListAsync();

                        wellCheckListDetail = JsonConvert.DeserializeObject<List<WellCheckListDetailModel>>(JsonConvert.SerializeObject(wellCheckListDetail));

                        WellCheckList wellCheckList = new WellCheckList
                        {
                            CheckList = JsonConvert.SerializeObject(wellCheckListDetail),
                            TenantID = TenantId,
                            WellId = WellId,
                            WellChecklistId = Guid.NewGuid().ToString(),
                            RigId = wellName == null ? null : wellName.RigID
                        };
                        if (WellId != null)
                        {
                            db.WellCheckList.Add(wellCheckList);
                            var result = await db.SaveChangesAsync();
                            return result;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    catch (Exception ex2)
                    {
                        customErrorHandlerGen.WriteError(ex2, "CommonRepository CreateWellChecklist_2", null);
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository CreateWellChecklist", null);
                return 0;
            }
        }

        //DWOp
        public async Task<List<Model.OperatingCompany.Models.ChecklistTemplateModel>> ReadChecklistTemplateList(string operTenantId)
        {
            try
            {
                List<Model.OperatingCompany.Models.ChecklistTemplateModel> templateList = new List<Model.OperatingCompany.Models.ChecklistTemplateModel>();
                //var checklistTemplate = db.ChecklistTemplate.Where(x => x.TenantId == operTenantId).FirstOrDefault();

                templateList = await (from template in db.ChecklistTemplate
                                      join type in db.WellType on template.WellTypeId equals type.welltype_id
                                      join user in db.WellIdentityUser on template.CreatedBy equals user.Id
                                      where template.TenantId == operTenantId
                                      select new Model.OperatingCompany.Models.ChecklistTemplateModel
                                      {
                                          TemplateId = template.CheckListTemplateId,
                                          TemplateName = template.TemplateName,
                                          WellType = type.welltype_name,
                                          WellTypeId = type.welltype_id,
                                          TenantId = template.TenantId,
                                          CreatedDate = template.CreatedDate,
                                          CreatedUser = user.FirstName ?? String.Empty + " " + user.MiddleName ?? String.Empty + " " + user.LastName ?? String.Empty,
                                          IsDefault = template.IsDefault,
                                          Checklist = template.Checklist
                                      }).ToListAsync();


                templateList = templateList.Select(template => new Model.OperatingCompany.Models.ChecklistTemplateModel()
                {
                    TemplateId = template.TemplateId,
                    TemplateName = template.TemplateName,
                    WellType = template.WellType,
                    WellTypeId = template.WellTypeId,
                    TenantId = template.TenantId,
                    CreatedDate = template.CreatedDate,
                    CreatedUser = template.CreatedUser,
                    IsDefault = template.IsDefault,
                    TaskCount = Convert.ToInt32(template.Checklist.Split("Checklist")[0].Split(":")[1].Split(",")[0])
                }).ToList();

                return await Task.FromResult(templateList);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository ReadChecklistTemplateList", null);
                return null;
            }
        }


        public async Task<List<ChecklistTaskTemplateModel>> GetChecklistTemplate(string CheckListId, string TenantId)
        {
            try
            {
                List<ChecklistTaskTemplateModel> tasksList = new List<ChecklistTaskTemplateModel>();
                //ChecklistTemplateTaskListModel ChecklistTemplateList;
                // List< ChecklistTemplateListModel> ChecklistTemplateList = new List<ChecklistTemplateListModel>();
                var Checklist = db.ChecklistTemplate.Where(x => x.CheckListTemplateId == CheckListId && x.TenantId == TenantId).FirstOrDefault();
                if (Checklist != null)
                {
                    var ChecklistTemplateList = JsonConvert.DeserializeObject<ChecklistTemplateTaskListModel>(Checklist.Checklist);

                    tasksList = ChecklistTemplateList.checklist;

                    foreach (var item in tasksList)
                    {
                        if (item.ServiceDuration != null && item.ServiceDuration != "")
                        {
                            if (item.ServiceDuration.Length == 6)
                            {
                                item.ServiceDuration = "00" + item.ServiceDuration;
                            }
                            item.ServiceDurationDays = item.ServiceDuration != null ? Convert.ToString(item.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries)[0]) : "00";
                            item.ServiceDurationHours = item.ServiceDuration != null ? Convert.ToString(item.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries)[1]) : "00";
                            item.ServiceDurationMinutes = item.ServiceDuration != null ? Convert.ToString(item.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries)[2]) : "00";

                        }
                        else
                        {
                            item.ServiceDurationDays = "00";
                            item.ServiceDurationHours = "00";
                            item.ServiceDurationMinutes = "00";
                        }
                    }

                }
                //if (tasksList == null || tasksList.Count == 0)
                //{
                //    var tasks = GetTasksList().Result;

                //    tasksList = (from task in tasks                                   
                //                  select new Model.Administration.ChecklistTemplateModel
                //                   {
                //                        TaskOrder = 1,
                //                        Name = task.Name,
                //                        Description = task.Description,
                //                        TaskId = task.TaskId,
                //                        Day = task.Day,
                //                        Depth = task.Depth,
                //                        SeletedDependency = task.SeletedDependency == null ? "" : task.SeletedDependency.Replace(";", ","),
                //                        LeadTime = task.LeadTime,
                //                        ScheduleTime = (TimeSpan?)task.ScheduleTime,
                //                        IsActive = task.IsActive,
                //                        IsSpecialServices = task.IsSpecialServices == "0" ? "1" : task.IsSpecialServices.ToString(),
                //                        IsBiddable = task.IsBiddable,
                //                        StageType = task.StageType,
                //                        ServiceCategoryId = task.ServiceCategoryId,
                //                        StageTypeName = task.StageTypeName == null ? "N/A" : task.StageTypeName,
                //                        CategoryName = task.CategoryName,
                //                        ServiceDuration = task.ServiceDuration,
                //                        ServiceDurationDays = task.ServiceDuration != null ? Convert.ToString(task.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries)[0]) : "00",
                //                        ServiceDurationHours = task.ServiceDuration != null ? Convert.ToString(task.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries)[1]) : "00",
                //                        ServiceDurationMinutes = task.ServiceDuration != null ? Convert.ToString(task.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries)[2]) : "00",
                //                        IsActiveCategory = task.IsActiveCategory
                //                    }).ToList();

                //    tasksList = tasksList.OrderBy(x => x.StageTypeName).ThenBy(x => x.Name).ToList();
                //}
                return await Task.FromResult(tasksList);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository ReadChecklistTemplateList", null);
                return null;
            }
        }


        public async Task<List<TaskModel>> GetTasks()
        {
            try
            {
                var TaskList = (from task in db.Tasks
                                join category in db.CategoryTasks on task.TaskId equals category.TaskId
                                join service in db.serviceCategories on category.ServiceCategoryId equals service.ServiceCategoryId
                                join Stage in db.Stages on task.StageType equals Stage.Id into Stage1
                                from Stage in Stage1.DefaultIfEmpty()
                                where category.IsActive == true && task.IsActive == true
                                select new TaskModel
                                {
                                    Name = task.Name,
                                    Description = task.Description,
                                    TaskId = task.TaskId,
                                    Day = task.Day,
                                    Depth = task.Depth,
                                    SeletedDependency = task.Dependency != null ? task.Dependency.Replace(";", ",") : task.Dependency,
                                    Duration = task.Duration,
                                    LeadTime = task.LeadTime,
                                    ScheduleTime = (TimeSpan?)task.ScheduleTime,
                                    IsActive = task.IsActive,
                                    IsSpecialServices = task.IsSpecialServices == 0 ? "1" : task.IsSpecialServices.ToString(),
                                    IsBiddable = task.IsBiddable,
                                    StageType = task.StageType,
                                    ServiceCategoryId = category.ServiceCategoryId,
                                    StageTypeName = Stage.Name == null ? "N/A" : Stage.Name,
                                    CategoryName = service.Name,
                                    ServiceDuration = task.ServiceDuration,
                                    ServiceDurationDays = task.ServiceDuration != null ? Convert.ToString(task.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries).Length != 3 ? "00" : Convert.ToString(task.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries)[0])) : "00",
                                    ServiceDurationHours = task.ServiceDuration != null ? Convert.ToString(task.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries).Length != 3 ? "00" : Convert.ToString(task.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries)[1])) : "00",
                                    ServiceDurationMinutes = task.ServiceDuration != null ? Convert.ToString(task.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries).Length != 3 ? "00" : Convert.ToString(task.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries)[2])) : "00",
                                    IsActiveCategory = service.IsActive,
                                    IsBenchMark = task.IsBenchMark,
                                    IsPreSpud = task.IsPreSpud
                                }).ToList();

                return await Task.FromResult(TaskList);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository ReadChecklistTemplateList", null);
                return null;
            }

        }
        //DWOP

        public async Task<List<ChecklistTaskTemplateModel>> ReadChecklistTemplate(string welltype)
        {
            try
            {
                List<ChecklistTaskTemplateModel> Planlist = new List<ChecklistTaskTemplateModel>();
                var GetPlanList = db.WellType.Where(x => x.welltype_id == welltype).FirstOrDefault();
                if (GetPlanList.DrillPlanChecklist != null)
                {
                    Planlist = JsonConvert.DeserializeObject<List<ChecklistTaskTemplateModel>>(GetPlanList.DrillPlanChecklist);
                }


                return await Task.FromResult(Planlist);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository ReadChecklistTemplate", null);
                return null;
            }
        }

        public async Task<int> ChangeChecklistDefaultForTenant(string templatId, string tenantId, string wellTypeId, bool IsDefault)
        {
            try
            {
                //var nonDefaultChecklist = db.ChecklistTemplate.Where(x => x.TenantId == tenantId && x.CheckListTemplateId == templatId).FirstOrDefault();
                //nonDefaultChecklist.IsDefault = true;
                //db.ChecklistTemplate.Update(nonDefaultChecklist);
                int result = 0;
                if (IsDefault == true)
                {
                    var notDefaults = db.ChecklistTemplate.Where(x => x.TenantId == tenantId && x.CheckListTemplateId != templatId && x.WellTypeId == wellTypeId).ToList();
                    notDefaults.ForEach(a => a.IsDefault = false);
                    db.SaveChanges();
                }

                var defaultChecklist = db.ChecklistTemplate.Where(x => x.TenantId == tenantId && x.CheckListTemplateId == templatId && x.WellTypeId == wellTypeId).FirstOrDefault();
                if (defaultChecklist != null)
                {
                    defaultChecklist.IsDefault = IsDefault;
                    db.ChecklistTemplate.Update(defaultChecklist);
                }

                result = await db.SaveChangesAsync();
                var userId = db.CorporateProfile.Where(x => x.TenantId == tenantId).Select(y => y.UserId).FirstOrDefault();


                await SendChecklistTemplateNotifications(userId, tenantId, defaultChecklist, true, "TemplateDefault");

                return result;

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository ChangeChecklistDefaultForTenant", null);
                return 0;
            }
        }



        public async Task<string> SaveChecklistTemplate(ChecklistTemplate ChecklistTemplate, string TenantId, string userId)
        {
            var Result = "";
            var reOrderedChecklistTasks = await ReOderTasks(ChecklistTemplate.Checklist);
            var ChecklistData = new ChecklistTemplateTaskListModel
            {
                count = ChecklistTemplate.Checklist.Count(),
                checklist = reOrderedChecklistTasks
            };

            try
            {
                if (string.IsNullOrEmpty(ChecklistTemplate.ChecklistTemplateId))
                {
                    var Checklist = new CheckListTemplate
                    {
                        CheckListTemplateId = Guid.NewGuid().ToString("D"),
                        TemplateName = ChecklistTemplate.ChecklistTemplateName,
                        TenantId = TenantId,
                        WellTypeId = ChecklistTemplate.WellTypeId,
                        Checklist = JsonConvert.SerializeObject(ChecklistData),
                        IsDefault = ChecklistTemplate.IsDefault,
                        CreatedBy = userId,
                        CreatedDate = DateTime.Now,
                        ModifiedBy = userId,
                        ModifiedDate = DateTime.Now,
                        IsActive = true,
                        BopFrequency = ChecklistTemplate.BopFrequency
                    };
                    db.ChecklistTemplate.Add(Checklist);
                    await db.SaveChangesAsync();
                    await ChangeChecklistDefaultForTenant(Checklist.CheckListTemplateId, TenantId, Checklist.WellTypeId, Checklist.IsDefault);
                    Result = Checklist.CheckListTemplateId;
                    await SendChecklistTemplateNotifications(userId, TenantId, Checklist, false, "TemplateSaveUpdate");
                }
                else
                {
                    var ExitsTemplate = db.ChecklistTemplate.Where(x => x.CheckListTemplateId == ChecklistTemplate.ChecklistTemplateId && x.TenantId == TenantId).FirstOrDefault();
                    if (ExitsTemplate != null)
                    {
                        ExitsTemplate.TemplateName = ChecklistTemplate.ChecklistTemplateName;
                        ExitsTemplate.WellTypeId = ChecklistTemplate.WellTypeId;
                        ExitsTemplate.Checklist = JsonConvert.SerializeObject(ChecklistData);
                        ExitsTemplate.IsDefault = ChecklistTemplate.IsDefault;
                        ExitsTemplate.ModifiedBy = userId;
                        ExitsTemplate.ModifiedDate = DateTime.Now;
                        ExitsTemplate.BopFrequency = ChecklistTemplate.BopFrequency;
                        await ChangeChecklistDefaultForTenant(ExitsTemplate.CheckListTemplateId, TenantId, ExitsTemplate.WellTypeId, ChecklistTemplate.IsDefault);
                        db.ChecklistTemplate.Update(ExitsTemplate);
                        await db.SaveChangesAsync();
                        Result = ExitsTemplate.CheckListTemplateId;

                        if (ChecklistTemplate.DeletedTasks.Count > 0)
                        {
                            MessageQueue MessageQueue = new MessageQueue();
                            var Companies = db.CorporateProfile.ToList();
                            var ServiceCompanies = db.CrmCompanies.Select(x => x.TenantId).ToList();
                            var OperatingCompanies = from c in Companies
                                                     where !ServiceCompanies.Contains(c.TenantId)
                                                     select c;

                            foreach (var Operator in OperatingCompanies)
                            {
                                var MessageQueueTasks = new MessageQueue
                                {
                                    From_id = userId,
                                    To_id = Operator.UserId,
                                    Type = 9,
                                    IsActive = 1,
                                    EntityId = ExitsTemplate.CheckListTemplateId,
                                    JobName = "Checklist Template",
                                    TaskName = "Checklist Modified On " + DateTime.Now.ToString("MM/dd/yyyy h:mm tt") + " - " + ExitsTemplate.TemplateName + " - " + "Tasks Deleted  : " + string.Join(",", ChecklistTemplate.DeletedTasks),
                                    CreatedDate = DateTime.Now
                                };

                                await db.MessageQueues.AddAsync(MessageQueueTasks);
                                await db.SaveChangesAsync();
                            }
                        }

                        await SendChecklistTemplateNotifications(userId, TenantId, ExitsTemplate, true, "TemplateSaveUpdate");
                    }
                }

                foreach (var taskModel in ChecklistTemplate.Checklist)
                {
                    if (taskModel.ExportToMaster)
                    {
                        var TasksExits = db.Tasks.Where(x => x.TaskId == taskModel.TaskId).FirstOrDefault();

                        if (TasksExits != null)
                        {
                            continue;
                        }
                        taskModel.TaskId = taskModel.TaskId == "" ? Guid.NewGuid().ToString() : taskModel.TaskId;

                        Tasks tasks = new Tasks()
                        {
                            CreatedBy = userId,
                            CreatedDate = DateTime.UtcNow,
                            Description = taskModel.Description,
                            IsActive = true,
                            Day = taskModel.Day,
                            Dependency = taskModel.SeletedDependency,
                            Depth = taskModel.Depth,
                            LeadTime = taskModel.LeadTime,
                            ScheduleTime = (TimeSpan?)TimeSpan.Parse(taskModel.ScheduleTime),
                            Name = taskModel.Name,
                            IsSpecialServices = Convert.ToInt32(taskModel.IsSpecialServices),
                            TaskId = taskModel.TaskId,
                            IsBiddable = taskModel.IsBiddable,
                            StageType = taskModel.StageType,
                            ServiceDuration = taskModel.ServiceDuration
                        };

                        CategoryTask CategoryTask = new CategoryTask
                        {
                            CategoryTaskId = Guid.NewGuid().ToString(),
                            ServiceCategoryId = taskModel.ServiceCategoryId,
                            TaskId = taskModel.TaskId,
                            CreatedBy = userId,
                            CreatedDate = DateTime.UtcNow,
                            IsActive = true
                        };
                        db.CategoryTasks.Add(CategoryTask);
                        db.Tasks.Add(tasks);
                        db.SaveChanges();
                    }
                }

                return Result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository SaveChecklistTemplate", null);
                return null;
            }

        }

        public async Task<List<ChecklistTaskTemplateModel>> ReOderTasks(List<ChecklistTaskTemplateModel> ChecklistTaks)
        {
            List<ChecklistTaskTemplateModel> plannedTasksList = new List<ChecklistTaskTemplateModel>();
            try
            {
                var isPreSpudTasks = ChecklistTaks.Where(x => x.IsPreSpud == true).OrderBy(y => y.TaskOrder).ToList();
                var otherTasks = ChecklistTaks.Except(isPreSpudTasks).OrderBy(r => r.TaskOrder).ToList();

                if (isPreSpudTasks != null && otherTasks != null)
                {

                    int i = 0;
                    foreach (var item in isPreSpudTasks)
                    {
                        item.TaskOrder = i + 1;
                        plannedTasksList.Add(item);
                        i++;
                    }

                    if (otherTasks != null)
                    {
                        var benchMarkTasks = otherTasks.Where(x => x.IsBenchMark == true && x.IsPreSpud != true).OrderBy(o => o.TaskOrder).ToList();
                        if (benchMarkTasks.Count > 1)
                        {
                            for (var k = 0; benchMarkTasks.Count > k; k++)
                            {
                                if (k + 1 < benchMarkTasks.Count)
                                {
                                    var getBenchMarkTasks = new List<ChecklistTaskTemplateModel>();
                                    int nextBenchMarkTaskOrder = (int)benchMarkTasks[k + 1].TaskOrder;
                                    if (k > 0)
                                    {
                                        getBenchMarkTasks = otherTasks.Where(x => x.TaskOrder >= benchMarkTasks[k].TaskOrder && x.TaskOrder < nextBenchMarkTaskOrder && x.IsPreSpud != true).OrderByDescending(o => o.IsBenchMark).ToList();
                                    }
                                    else
                                    {
                                        getBenchMarkTasks = otherTasks.Where(x => x.TaskOrder < nextBenchMarkTaskOrder && x.IsPreSpud != true).OrderByDescending(o => o.IsBenchMark).ToList();
                                    }

                                    foreach (var item in getBenchMarkTasks)
                                    {
                                        item.TaskOrder = i + 1;
                                        plannedTasksList.Add(item);
                                        i++;
                                    }
                                }
                                else
                                {
                                    var getBenchMarkTasks = otherTasks.Where(x => x.TaskOrder >= benchMarkTasks[benchMarkTasks.Count - 1].TaskOrder && x.IsPreSpud != true).OrderByDescending(o => o.IsBenchMark).ToList();
                                    foreach (var item in getBenchMarkTasks)
                                    {
                                        item.TaskOrder = i + 1;
                                        plannedTasksList.Add(item);
                                        i++;
                                    }
                                }
                            }
                        }
                        else if (benchMarkTasks.Count == 1)
                        {
                            var getBenchMarkTasks = otherTasks.Where(x => x.IsPreSpud != true).OrderByDescending(o => o.IsBenchMark).ToList();
                            foreach (var item in getBenchMarkTasks)
                            {
                                item.TaskOrder = i + 1;
                                plannedTasksList.Add(item);
                                i++;
                            }
                        }
                        else
                        {
                            foreach (var item in otherTasks)
                            {
                                item.TaskOrder = i + 1;
                                plannedTasksList.Add(item);
                                i++;
                            }
                        }
                    }
                }

                return await Task.FromResult(plannedTasksList);
            }
            catch (Exception ex)
            {
                return ChecklistTaks;
            }
        }


        //DWOP
        public async Task<int> SendChecklistTemplateNotifications(string userId, string TenantId, CheckListTemplate Checklist, bool IsExist, string Status)
        {
            try
            {
                int result = 0;

                MessageQueue MessageQueue = new MessageQueue();
                var Companies = db.CorporateProfile.ToList();
                var ServiceCompanies = db.CrmCompanies.Select(x => x.TenantId).ToList();
                var OperatingCompanies = from c in Companies
                                         where !ServiceCompanies.Contains(c.TenantId)
                                         select c;
                if (Status == "TemplateSaveUpdate")
                {
                    foreach (var Operator in OperatingCompanies)
                    {
                        if (IsExist != true)
                        {
                            MessageQueue = new MessageQueue
                            {
                                From_id = userId,
                                To_id = Operator.UserId,
                                Type = 9,
                                IsActive = 1,
                                EntityId = Checklist.CheckListTemplateId,
                                JobName = "Checklist Template",
                                TaskName = "Checklist Created On " + DateTime.Now.ToString("MM/dd/yyyy h:mm tt") + " - " + Checklist.TemplateName,
                                CreatedDate = DateTime.Now
                            };
                            await db.MessageQueues.AddAsync(MessageQueue);
                            result = await db.SaveChangesAsync();

                            if (Checklist.Checklist != null)
                            {
                                var Tasks = JsonConvert.DeserializeObject<ChecklistTemplateTaskListModel>(Checklist.Checklist);

                                if (Tasks != null)
                                {
                                    var TasksName = Tasks.checklist.Select(x => x.Name).ToList();

                                    var MessageQueueTasks = new MessageQueue
                                    {
                                        From_id = userId,
                                        To_id = Operator.UserId,
                                        Type = 9,
                                        IsActive = 1,
                                        EntityId = Checklist.CheckListTemplateId,
                                        JobName = "Checklist Template",
                                        TaskName = "Checklist Tasks Added " + DateTime.Now.ToString("MM/dd/yyyy h:mm tt") + " - " + Checklist.TemplateName + " - " + "Tasks Added Count : " + Tasks.count,
                                        CreatedDate = DateTime.Now
                                    };

                                    await db.MessageQueues.AddAsync(MessageQueueTasks);
                                    result = await db.SaveChangesAsync();
                                }
                            }
                        }
                        else
                        {
                            if (Checklist.Checklist != null)
                            {
                                var Tasks = JsonConvert.DeserializeObject<ChecklistTemplateTaskListModel>(Checklist.Checklist);
                                if (Tasks != null)
                                {

                                    var TasksName = Tasks.checklist.Select(x => x.Name).ToList();

                                    var MessageQueueTasks = new MessageQueue
                                    {
                                        From_id = userId,
                                        To_id = Operator.UserId,
                                        Type = 9,
                                        IsActive = 1,
                                        EntityId = Checklist.CheckListTemplateId,
                                        JobName = "Checklist Template",
                                        TaskName = "Checklist Modified On " + DateTime.Now.ToString("MM/dd/yyyy h:mm tt") + " - " + Checklist.TemplateName + " - " + "Tasks Updated Count : " + Tasks.count,
                                        CreatedDate = DateTime.Now
                                    };

                                    await db.MessageQueues.AddAsync(MessageQueueTasks);
                                    result = await db.SaveChangesAsync();
                                }
                            }
                        }
                    }
                }
                else if (Status == "TemplateDelete")
                {
                    foreach (var Operator in OperatingCompanies)
                    {
                        var MessageQueueTasks = new MessageQueue
                        {
                            From_id = userId,
                            To_id = Operator.UserId,
                            Type = 9,
                            IsActive = 1,
                            EntityId = null,
                            JobName = "Checklist Template",
                            TaskName = "Checklist Deleted On " + DateTime.Now.ToString("MM/dd/yyyy h:mm tt") + " - " + Checklist.TemplateName,
                            CreatedDate = DateTime.Now
                        };

                        await db.MessageQueues.AddAsync(MessageQueueTasks);
                        result = await db.SaveChangesAsync();
                    }
                }
                else if (Status == "TemplateDefault")
                {
                    foreach (var Operator in OperatingCompanies)
                    {
                        string DefaultStatus = Checklist.IsDefault == true ? "Changed as Default" : "Removed as Default";
                        var MessageQueueTasks = new MessageQueue
                        {
                            From_id = userId,
                            To_id = Operator.UserId,
                            Type = 9,
                            IsActive = 1,
                            EntityId = Checklist.CheckListTemplateId,
                            JobName = "Checklist Template",
                            TaskName = "Checklist Update On " + DateTime.Now.ToString("MM/dd/yyyy h:mm tt") + " - " + Checklist.TemplateName + " - " + DefaultStatus,
                            CreatedDate = DateTime.Now
                        };

                        await db.MessageQueues.AddAsync(MessageQueueTasks);
                        result = await db.SaveChangesAsync();
                    }
                }
                return await Task.FromResult(result);

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository ChangeChecklistDefaultForTenant", null);
                return 0;
            }
        }


        //DWOP
        public List<ServiceStageModel> GetServiceStage()
        {
            try
            {

                var stagesList = db.Stages.Select(x => new ServiceStageModel
                {
                    Stage_id = x.Id,
                    Stage_Type = x.Name
                }).ToList();

                return stagesList;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetServiceStage", null);
                return null;
            }
        }

        public List<Model.Administration.ServiceCategoryModel> GetCategoriesList()
        {
            try
            {
                var categoriesList = db.serviceCategories.Where(x => x.ServiceCategoryId == x.ParentId).Select(x => new Model.Administration.ServiceCategoryModel
                {
                    ServiceCategoryId = x.ServiceCategoryId,
                    Name = x.Name
                }).OrderBy(x => x.Name).ToList();

                return categoriesList;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetCategoriesList", null);
                return null;
            }
        }

        public List<TaskModel> GetTaskDependencyList(string taskId)
        {
            try
            {
                var result = db.Tasks.Select(x => new TaskModel
                {
                    Name = x.Name,
                    Description = x.Description,
                    TaskId = x.TaskId,
                    Day = x.Day,
                    Depth = x.Depth,
                    SeletedDependency = x.Dependency.Replace(";", ","),
                    Duration = x.Duration,
                    LeadTime = x.LeadTime,
                    ScheduleTime = (TimeSpan)x.ScheduleTime,
                    IsActive = x.IsActive,
                    IsSpecialServices = x.IsSpecialServices == 0 ? "1" : x.IsSpecialServices.ToString(),
                    IsBiddable = x.IsBiddable,
                    StageType = x.StageType
                }).ToList();

                if (!string.IsNullOrEmpty(taskId))
                {
                    result = result.Where(x => x.TaskId != taskId).ToList();
                }
                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetTaskDependencyList", null);
                return null;
            }

        }

        public  Task<List<TaskModel>> GetTasksList()
         {
           
            try
            {
                var result = (from task in db.Tasks
                              join category in db.CategoryTasks on task.TaskId equals category.TaskId
                              join service in db.serviceCategories on category.ServiceCategoryId equals service.ServiceCategoryId
                              join Stage in db.Stages on task.StageType equals Stage.Id into Stage1
                              from Stage in Stage1.DefaultIfEmpty()
                              where category.IsActive == true && task.IsActive == true
                              select new TaskModel
                              {
                                  Name = task.Name,
                                  Description = task.Description,
                                  TaskId = task.TaskId,
                                  Day = task.Day,
                                  Depth = task.Depth,
                                  //SeletedDependency = task.Dependency.Replace(";", ","),
                                  Duration = task.Duration,
                                  LeadTime = task.LeadTime,
                                  ScheduleTime = (TimeSpan?)task.ScheduleTime,
                                  IsActive = task.IsActive,
                                  IsSpecialServices = task.IsSpecialServices == 0 ? "1" : task.IsSpecialServices.ToString(),
                                  IsBiddable = task.IsBiddable,
                                  StageType = task.StageType,
                                  ServiceCategoryId = category.ServiceCategoryId,
                                  StageTypeName = Stage.Name == null ? "N/A" : Stage.Name,
                                  CategoryName = service.Name,
                                  ServiceDuration = task.ServiceDuration == null ? "00:00:00" : task.ServiceDuration,
                                  ServiceDurationDays = "00",
                                  ServiceDurationHours = "00",
                                  ServiceDurationMinutes = "00",
                                  IsActiveCategory = service.IsActive
                              }).ToList();
               
                    //DWOP - Get Category Active Status
               var itemList = (from res in result
                        select new TaskModel
                        {
                            Name = res.Name,
                            Description = res.Description,
                            TaskId = res.TaskId,
                            Day = res.Day,
                            Depth = res.Depth,
                            // SeletedDependency =res.Dependency.Replace(";", ","),
                            Duration = res.Duration,
                            LeadTime = res.LeadTime,
                            ScheduleTime = (TimeSpan?)res.ScheduleTime,
                            IsActive = res.IsActive,
                            IsSpecialServices = res.IsSpecialServices == "0" ? "1" : res.IsSpecialServices.ToString(),
                            IsBiddable = res.IsBiddable,
                            StageType = res.StageType,
                            ServiceCategoryId = res.ServiceCategoryId,
                            StageTypeName = res.Name == null ? "N/A" : res.Name,
                            CategoryName = res.Name,
                           
                            ServiceDuration = res.ServiceDuration,

                      //      ServiceDurationDays = res.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries).Length == 2 ? "00" : res.ServiceDuration.Split(':',StringSplitOptions.RemoveEmptyEntries)[0],
                      //      ServiceDurationHours = res.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries).Length == 2 ? res.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries)[0] : res.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries)[1],                            
                      //      ServiceDurationMinutes = res.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries).Length == 2 ? res.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries)[1] : res.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries)[2],

                            ServiceDurationDays = res.ServiceDurationDays,
                            ServiceDurationHours = res.ServiceDurationHours,
                            ServiceDurationMinutes = res.ServiceDurationMinutes,

                            IsActiveCategory = res.IsActive
                        }).ToList();
                return Task.FromResult(itemList);
            }
            
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetTaskDependencyList", null);
                return null;
            }
            
        }

        //DWOP
        public async Task<List<WellAI.Advisor.Model.OperatingCompany.Models.ChecklistTemplateModel>> GetChecklistTemplateList(string wellDesign, string tenantId)
        {
            try
            {
                List<WellAI.Advisor.Model.OperatingCompany.Models.ChecklistTemplateModel> templateList = new List<WellAI.Advisor.Model.OperatingCompany.Models.ChecklistTemplateModel>();
                //var checklistTemplate = db.ChecklistTemplate.Where(x => x.TenantId == operTenantId).FirstOrDefault();

                templateList = await (from template in db.ChecklistTemplate
                                      join type in db.WellType on template.WellTypeId equals type.welltype_id
                                      join user in db.WellIdentityUser on template.CreatedBy equals user.Id
                                      where template.WellTypeId == wellDesign && template.TenantId == tenantId
                                      select new WellAI.Advisor.Model.OperatingCompany.Models.ChecklistTemplateModel
                                      {
                                          TemplateId = template.CheckListTemplateId,
                                          TemplateName = template.TemplateName
                                      }).ToListAsync(); 

                templateList.OrderBy(t => t.TemplateName).ToList();
                return await Task.FromResult(templateList);
            }
            catch (Exception ex)
           {
               CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
               customErrorHandler.WriteError(ex, "CommonRepository ReadChecklistTemplateList",null);
                return null;
           }
        }

        public Task<List<WellMasterDataViewModel>> GetWellRegister(string tenantId, string rigId, bool checkWellFilter)
        {
            try
            {
                var groupList = GetRIGAIGroup();
                List<WellMasterDataViewModel> wellMaster = new List<WellMasterDataViewModel>();
                if (rigId != "")
                {
                    wellMaster = (from wel in db.WellRegister
                                  join rig in db.rig_register on wel.RigID equals rig.Rig_id into t1
                                  from rigresult in t1.DefaultIfEmpty()
                                  join pad in db.pad_register on wel.PadID equals pad.Pad_id into t2
                                  from padresult in t2.DefaultIfEmpty()
                                  join batch in db.BatchDillingType_Register on wel.BatchDrillingTypeID equals batch.BatchDrillingType_Id into t3
                                  from batchresult in t3.DefaultIfEmpty()
                                  join typ in db.WellType on wel.welltype_id equals typ.welltype_id into tj
                                  from subresult in tj.DefaultIfEmpty()
                                 // where wel.customer_id.Equals(tenantId) && (rigresult.Rig_id == rigId && !checkWellFilter || checkWellFilter)
                                  join bas in db.BasinTypes on wel.Basin equals bas.Basin_ID into t4
                                  from basinresult in t4.DefaultIfEmpty()
                                  join chklist in db.ChecklistTemplate on wel.ChecklistTemplateId equals chklist.CheckListTemplateId into t5
                                  from chklistresult in t5.DefaultIfEmpty()
                                  select new WellMasterDataViewModel
                                  {
                                      wellId = wel.well_id,
                                      wellName = wel.wellname,
                                      wellType = subresult.welltype_name ?? String.Empty,
                                      wellTypeId = new Model.OperatingCompany.Models.WellTypeModel { wellTypeId = subresult.welltype_id, wellTypeName = subresult.welltype_name },
                                      county = wel.County,
                                      complete_well_drill = wel.Conplete_well_drill == 1 ? true : false,
                                      batch_drill_casing = wel.Batch_drill_casing == 1 ? true : false,
                                      batch_drill_horizontal = wel.Batch_drill_horizontal == 1 ? true : false,
                                      casing_string = wel.Casing_string == 1 ? true : false,
                                      numAPI = wel.NumAPI,
                                      numAFE = wel.NumAFE,
                                      rigName = rigresult.Rig_Name ?? String.Empty,
                                      padName = padresult.Pad_Name ?? String.Empty,
                                      state = wel.State,
                                      batchFlag = wel.BatchFlag == 1 ? true : false,
                                      batchDrillingTypeId = wel.BatchDrillingTypeID ?? String.Empty,
                                      casingString = wel.CasingString,
                                      padID = wel.PadID,
                                      rigID = wel.RigID,
                                      latitude = wel.Latitude,
                                      longitude = wel.Longitude,
                                      Prediction = wel.Prediction,
                                      OldPredictionForUpdate = wel.Prediction,
                                      chartColor = wel.ChartColor,
                                      fieldName = wel.FieldName,
                                      basin = basinresult.BasinType_name ?? String.Empty,
                                      Basin_ID = new BasinTypeModel { Basin_ID = basinresult.Basin_ID, BasinType_name = basinresult.BasinType_name },
                                      ChecklistTemplateName = chklistresult.TemplateName,
                                      ChecklistTemplateId = wel.ChecklistTemplateId
                                  }).ToList();
                }
                else
                {
                    wellMaster = (from wel in db.WellRegister
                                  join rig in db.rig_register on wel.RigID equals rig.Rig_id into t1
                                  from rigresult in t1.DefaultIfEmpty()
                                  join pad in db.pad_register on wel.PadID equals pad.Pad_id into t2
                                  from padresult in t2.DefaultIfEmpty()
                                  join batch in db.BatchDillingType_Register on wel.BatchDrillingTypeID equals batch.BatchDrillingType_Id into t3
                                  from batchresult in t3.DefaultIfEmpty()
                                  join typ in db.WellType on wel.welltype_id equals typ.welltype_id into tj
                                  from subresult in tj.DefaultIfEmpty()
                                  where wel.customer_id.Equals(tenantId)
                                  join bas in db.BasinTypes on wel.Basin equals bas.Basin_ID into t4
                                  from basinresult in t4.DefaultIfEmpty()
                                  join chklist in db.ChecklistTemplate on wel.ChecklistTemplateId equals chklist.CheckListTemplateId into t5
                                  from chklistresult in t5.DefaultIfEmpty()
                                  select new WellMasterDataViewModel
                                  {
                                      wellId = wel.well_id,
                                      wellName = wel.wellname,
                                      wellType = subresult.welltype_name ?? String.Empty,
                                      wellTypeId = new Model.OperatingCompany.Models.WellTypeModel { wellTypeId = subresult.welltype_id, wellTypeName = subresult.welltype_name },
                                      county = wel.County,
                                      complete_well_drill = wel.Conplete_well_drill == 1 ? true : false,
                                      batch_drill_casing = wel.Batch_drill_casing == 1 ? true : false,
                                      batch_drill_horizontal = wel.Batch_drill_horizontal == 1 ? true : false,
                                      casing_string = wel.Casing_string == 1 ? true : false,
                                      numAPI = wel.NumAPI,
                                      numAFE = wel.NumAFE,
                                      rigName = rigresult.Rig_Name ?? String.Empty,
                                      padName = padresult.Pad_Name ?? String.Empty,
                                      state = wel.State,
                                      batchFlag = wel.BatchFlag == 1 ? true : false,
                                      batchDrillingTypeId = wel.BatchDrillingTypeID ?? String.Empty,
                                      casingString = wel.CasingString,
                                      padID = wel.PadID,
                                      rigID = wel.RigID,
                                      latitude = wel.Latitude,
                                      longitude = wel.Longitude,
                                      Prediction = wel.Prediction,
                                      OldPredictionForUpdate = wel.Prediction,
                                      chartColor = wel.ChartColor,
                                      fieldName = wel.FieldName,
                                      basin = basinresult.BasinType_name ?? String.Empty,
                                      Basin_ID = new BasinTypeModel { Basin_ID = basinresult.Basin_ID, BasinType_name = basinresult.BasinType_name },
                                      ChecklistTemplateName = chklistresult.TemplateName,
                                      ChecklistTemplateId = wel.ChecklistTemplateId
                                  }).ToList();
                }

                var wellMasterResult = (from wel in wellMaster
                                        join gp in groupList on wel.wellId equals gp.wellId into gj
                                        from subresult1 in gj.DefaultIfEmpty()
                                        orderby wel.wellName
                                        select new WellMasterDataViewModel
                                        {
                                            wellId = wel.wellId,
                                            wellName = wel.wellName,
                                            wellType = wel.wellType,
                                            taskCount = subresult1?.taskCount ?? String.Empty,
                                            minSchdDate = subresult1?.minSchdDate ?? String.Empty,
                                            maxSchdDate = subresult1?.maxSchdDate ?? String.Empty,
                                            wellTypeId = wel.wellTypeId,
                                            county = wel.county,
                                            complete_well_drill = wel.complete_well_drill,
                                            batch_drill_casing = wel.batch_drill_casing,
                                            batch_drill_horizontal = wel.batch_drill_horizontal,
                                            casing_string = wel.casing_string,
                                            numAPI = wel.numAPI,
                                            numAFE = wel.numAFE,
                                            rigName = wel.rigName,
                                            padName = wel.padName,
                                            state = wel.state,
                                            batchFlag = wel.batchFlag,
                                            batchDrillingTypeId = wel.batchDrillingTypeId,
                                            casingString = wel.casingString,
                                            padID = wel.padID,
                                            rigID = wel.rigID,
                                            latitude = wel.latitude,
                                            longitude = wel.longitude,
                                            fieldName = wel.fieldName,
                                            basin = wel.basin,
                                            Basin_ID = wel.Basin_ID,
                                            Prediction = wel.Prediction,
                                            OldPredictionForUpdate = wel.Prediction,
                                            chartColor = wel.chartColor,
                                            ChecklistTemplateName = wel.ChecklistTemplateName,
                                            ChecklistTemplateId = wel.ChecklistTemplateId ?? ""
                                        }
                                        ).ToList();
                return Task.FromResult(wellMasterResult);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetWellRegister", null);
                return null;
            }

        }

        private IList<WellMasterGroupDataViewModel> GetRIGAIGroup()
        {
            try
            {
                List<AIWellDataModel> AIWellDataModelList = new List<AIWellDataModel>();
                var associatedList = db.AIAssociatedTasks.AsNoTracking();
                var predictiveList = db.AIPredictiveTasks.AsNoTracking();
                var exemptionlist = db.AIExemptionTasks.AsNoTracking();
                var associatedtasks = (from ai in associatedList
                                       join dp in db.WellTask on ai.dependency equals dp.welltask_id into tj
                                       from subresult in tj.DefaultIfEmpty()
                                       join wl in db.WellRegister on ai.well_id equals wl.well_id into wj
                                       from subresult1 in wj.DefaultIfEmpty()
                                       select new AIWellDataModel
                                       {
                                           actionDate = ai.ActionDate,
                                           adt = ai.ADT,
                                           customerId = ai.customer_id,
                                           dependency = ai.dependency,
                                           dependencyFlag = ai.dependency_flag,
                                           depth = ai.depth,
                                           duration = ai.duration,
                                           eFlag = ai.Eflag,
                                           leadTime = ai.leadtime,
                                           scheduleDate = ai.ScheduleDate,
                                           startTime = ai.StartTime,
                                           taskName = ai.taskname,
                                           taskStatus = ai.taskstatus,
                                           time = ai.time,
                                           wellTaskId = ai.welltask_id,
                                           wellTypeId = ai.welltype_id,
                                           wellId = ai.well_id,
                                           dependencyTask = subresult.taskname ?? String.Empty,
                                           wellName = subresult1.wellname ?? String.Empty,
                                       }).ToList();
                var predictiveTasks = (from ai in predictiveList
                                       join dp in db.WellTask on ai.dependency equals dp.welltask_id into tj
                                       from subresult in tj.DefaultIfEmpty()
                                       join wl in db.WellRegister on ai.well_id equals wl.well_id into wj
                                       from subresult1 in wj.DefaultIfEmpty()
                                       select new AIWellDataModel
                                       {
                                           actionDate = ai.ActionDate,
                                           adt = ai.ADT,
                                           customerId = ai.customer_id,
                                           dependency = ai.dependency,
                                           dependencyFlag = ai.dependency_flag,
                                           depth = ai.depth,
                                           duration = ai.duration,
                                           eFlag = ai.Eflag,
                                           leadTime = ai.leadtime,
                                           scheduleDate = ai.ScheduleDate,
                                           startTime = ai.StartTime,
                                           taskName = ai.taskname,
                                           taskStatus = ai.taskstatus,
                                           time = ai.time,
                                           wellTaskId = ai.welltask_id,
                                           wellTypeId = ai.welltype_id,
                                           wellId = ai.well_id,
                                           dependencyTask = subresult.taskname ?? String.Empty,
                                           wellName = subresult1.wellname ?? String.Empty,
                                       }).ToList();
                var exemptionTasks = (from ai in exemptionlist
                                      join dp in db.WellTask on ai.dependency equals dp.welltask_id into tj
                                      from subresult in tj.DefaultIfEmpty()
                                      join wl in db.WellRegister on ai.well_id equals wl.well_id into wj
                                      from subresult1 in wj.DefaultIfEmpty()
                                      select new AIWellDataModel
                                      {
                                          actionDate = ai.ActionDate,
                                          adt = ai.ADT,
                                          customerId = ai.customer_id,
                                          dependency = ai.dependency,
                                          dependencyFlag = ai.dependency_flag,
                                          depth = ai.depth,
                                          duration = ai.duration,
                                          eFlag = ai.Eflag,
                                          leadTime = ai.leadtime,
                                          scheduleDate = ai.ScheduleDate,
                                          startTime = ai.StartTime,
                                          taskName = ai.taskname,
                                          taskStatus = ai.taskstatus,
                                          time = ai.time,
                                          wellTaskId = ai.welltask_id,
                                          wellTypeId = ai.welltype_id,
                                          wellId = ai.well_id,
                                          dependencyTask = subresult.taskname ?? String.Empty,
                                          wellName = subresult1.wellname ?? String.Empty,
                                      }).ToList();
                var welldata = associatedtasks.Union(predictiveTasks).Union(exemptionTasks);
                var groupdata = (from wel in welldata
                                 group wel by wel.wellId into g
                                 select new WellMasterGroupDataViewModel
                                 {
                                     wellId = g.Key,
                                     taskCount = g.Count().ToString(),
                                     minSchdDate = g.Min(c => c.scheduleDate).ToString(),
                                     maxSchdDate = g.Max(c => c.scheduleDate).ToString()
                                 }).ToList();
                return groupdata.ToList();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetRIGAIGroup", null);
                return null;

            }
        }

        public async Task<int> DeleteChecklistTemplate(string templateId)
        {
            int Result = 0;

            try
            {
                if (!string.IsNullOrEmpty(templateId))
                {
                    var Template = db.ChecklistTemplate.Where(x => x.CheckListTemplateId == templateId).FirstOrDefault();

                    if (Template != null)
                    {
                        db.ChecklistTemplate.Remove(Template);
                        Result = await db.SaveChangesAsync();
                    }

                    var userid = db.CorporateProfile.Where(x => x.TenantId == Template.TenantId).Select(y => y.UserId).FirstOrDefault();

                    await SendChecklistTemplateNotifications(userid, Template.TenantId, Template, false, "TemplateDelete");
                }
                return await Task.FromResult(Result);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(Result);
            }
        }


        public async Task<float> CalculateHours(int days, int hours, int minutes)
        {
            float result = 00;
            try
            {
                //int calDays = days != 00 ? days * 24 : 00;
                //int calMinutes = minutes != 00 ? ((minutes / 100) * 60) / 100 : 00;
                //result = calDays + hours + calMinutes;
                int calDays = days != 00 ? days * 24 : 00;
                string calMinutes = (minutes / 100.00).ToString("0.00");
                result = calDays + hours + (float)Math.Round(Convert.ToDouble(calMinutes), 2); //15.30
                result = (float)Math.Round(result, 2);
                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository CalculateHours", null);
                return result;
            }
        }


        public async Task<DrillPlanWellViewModel> DrillingPlanTabContent(string wellid, string DrillingPlanId, string tenantId)
        {
            try
            {
                DrillPlanWellViewModel Planlist = new DrillPlanWellViewModel();
                if (!string.IsNullOrEmpty(DrillingPlanId))
                {
                    //2/9/2022


                    Planlist = (from Dh in db.DrillPlanHeader
                                join Dw in db.DrillPlanWells on Dh.DrillPlanId equals Dw.DrillPlanId
                                join well in db.WellRegister on Dw.Wellid equals well.well_id
                                join rig in db.rig_register on well.RigID equals rig.Rig_id into r
                                from Rig in r.DefaultIfEmpty()
                                join ct in db.ChecklistTemplate on well.ChecklistTemplateId equals ct.CheckListTemplateId into Template
                                from ct in Template.DefaultIfEmpty()
                                where Dh.DrillPlanId.Equals(DrillingPlanId) && Dh.TenantId.Equals(tenantId) && Dw.Wellid.Equals(wellid)
                                select new DrillPlanWellViewModel
                                {
                                    DrillPlanId = Dw.DrillPlanId,
                                    DrillPlanName = Dh.DrillPlanName,
                                    Wellid = Dw.Wellid,
                                    DrillPlanWellsId = Dw.DrillPlanWellsId,
                                    LastBOPTest = Dw.LastBOPTest != null ? Dw.LastBOPTest : null,
                                    NextBOPTest = Dw.LastBOPTest == null ? (DateTime?)null : ct.BopFrequency == null ? Convert.ToDateTime(Dw.LastBOPTest).AddDays(Convert.ToInt32(db.ChecklistTemplate.Where(x => x.WellTypeId == well.welltype_id && x.IsDefault == true).Select(y => y.BopFrequency).FirstOrDefault())) : Convert.ToDateTime(Dw.LastBOPTest).AddDays(Convert.ToInt32(ct.BopFrequency)),
                                    SPUDWell = Dw.SPUDWell != null ? Dw.SPUDWell : null,
                                    PlannedTD = Dw.PlannedTD,
                                    RigRealese = Dw.RigRealese != null ? Dw.RigRealese : null,
                                    WellName = well.wellname,
                                    RigId = Rig.Rig_id,
                                    Rigname = Rig.Rig_Name,
                                    TenantId = tenantId,
                                    BopFrequency = (int)ct.BopFrequency
                                }).FirstOrDefault();
                }


                if (Planlist == null || Planlist.Wellid == null)
                {
                    var welldata = db.WellRegister.Where(x => x.well_id == wellid).FirstOrDefault();
                    Planlist = new DrillPlanWellViewModel
                    {
                        DrillPlanId = DrillingPlanId,
                        Wellid = wellid,
                        RigId = welldata.RigID,
                        LastBOPTest = null,
                        NextBOPTest = null,
                        RigRealese = null,
                        SPUDWell = null,
                        Rigname = db.rig_register.Where(x => x.Rig_id == welldata.RigID).Select(y => y.Rig_Name).First(),
                        TenantId = tenantId
                    };
                }

                return await Task.FromResult(Planlist);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository DrillingPlanTabContent", null);
                return null;
            }
        }


        public async Task<List<ChecklistTaskTemplateModel>> ChecklistTemplateFordrillplan(string welltype, string wellid)
        {
            try
            {
                List<ChecklistTaskTemplateModel> Planlist = new List<ChecklistTaskTemplateModel>();
                string checklistId = "";
                string checklist = "";
                Model.OperatingCompany.Models.ChecklistTemplateTaskListModel checklistTemplateList = new Model.OperatingCompany.Models.ChecklistTemplateTaskListModel();
                if (wellid != "")
                {
                    checklistId = db.WellRegister.Where(x => x.well_id == wellid).Select(s => s.ChecklistTemplateId).FirstOrDefault();
                }

                if (checklistId != null)
                {
                    checklist = db.ChecklistTemplate.Where(x => x.CheckListTemplateId == checklistId).Select(s => s.Checklist).FirstOrDefault();
                }
                if (checklist != "" && checklist != null)
                {
                    checklistTemplateList = (Model.OperatingCompany.Models.ChecklistTemplateTaskListModel)JsonConvert.DeserializeObject<Model.OperatingCompany.Models.ChecklistTemplateTaskListModel>(checklist);
                    Planlist = checklistTemplateList.checklist;
                }
                else if (checklist == "" || checklist == null)
                {
                    checklist = db.ChecklistTemplate.Where(x => x.WellTypeId == welltype & x.IsDefault == true).Select(s => s.Checklist).FirstOrDefault();
                    if (checklist != "" && checklist != null)
                    {
                        checklistTemplateList = (Model.OperatingCompany.Models.ChecklistTemplateTaskListModel)JsonConvert.DeserializeObject<Model.OperatingCompany.Models.ChecklistTemplateTaskListModel>(checklist);
                        if (checklistTemplateList != null)
                        {
                            Planlist = checklistTemplateList.checklist;
                        }
                        else
                        {
                            Planlist = new List<ChecklistTaskTemplateModel>();
                        }

                    }
                    else
                    {
                        Planlist = new List<ChecklistTaskTemplateModel>();
                    }
                }
                else
                {
                    WellType GetPlanList = new WellType();
                    GetPlanList = db.WellType.Where(x => x.welltype_id == welltype).FirstOrDefault();
                    if (GetPlanList.DrillPlanChecklist != null)
                    {
                        Planlist = JsonConvert.DeserializeObject<List<ChecklistTaskTemplateModel>>(GetPlanList.DrillPlanChecklist);
                    }
                    else
                    {
                        var tasks = GetTasksList().Result;

                        Planlist = (from task in tasks
                                    join category in db.CategoryTasks on task.TaskId equals category.TaskId
                                    join service in db.serviceCategories on category.ServiceCategoryId equals service.ServiceCategoryId
                                    join Stage in db.Stages on task.StageType equals Stage.Id into Stage1
                                    from Stage in Stage1.DefaultIfEmpty()
                                    select new ChecklistTaskTemplateModel
                                    {
                                        TaskOrder = 1,
                                        Name = task.Name,
                                        Description = task.Description,
                                        TaskId = task.TaskId,
                                        Day = task.Day,
                                        Depth = task.Depth,
                                        SeletedDependency = task.SeletedDependency == null ? "" : task.SeletedDependency.Replace(";", ","),
                                        LeadTime = task.LeadTime,
                                        ScheduleTime = Convert.ToString(task.ScheduleTime),
                                        IsActive = task.IsActive,
                                        IsSpecialServices = task.IsSpecialServices == "0" ? "1" : task.IsSpecialServices.ToString(),
                                        IsBiddable = task.IsBiddable,
                                        StageType = task.StageType,
                                        ServiceCategoryId = task.ServiceCategoryId,
                                        StageTypeName = Stage == null ? null : Stage.Name == null ? "N/A" : Stage.Name,
                                        CategoryName = service.Name,
                                        ServiceDuration = task.ServiceDuration,
                                        ServiceDurationDays = task.ServiceDuration != null ? Convert.ToString(task.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries)[0]) : "00",
                                        ServiceDurationHours = task.ServiceDuration != null ? Convert.ToString(task.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries)[1]) : "00",
                                        ServiceDurationMinutes = task.ServiceDuration != null ? Convert.ToString(task.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries)[2]) : "00",
                                        IsActiveCategory = service.IsActive

                                    }).ToList();

                        Planlist = Planlist.OrderBy(x => x.StageTypeName).ThenBy(x => x.Name).ToList();
                    }
                }

                return await Task.FromResult(Planlist);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository ReadChecklistTemplate", null);
                return null;
            }
        }


        public async Task<int> SaveDrillplanHeader(IFormCollection form, DrillPlandetailsViewModel Input, string TenantId, string UserId)
        {
            try
            {
                int Result = 0;
                DrillPlanHeader drillPlanHeader = new DrillPlanHeader();
                DrillPlanWells drillPlanWells = new DrillPlanWells();

                if (Input != null)
                {
                    if (Input.DrillingPlanId == null)
                    {

                        drillPlanHeader = new DrillPlanHeader
                        {
                            DrillPlanId = Guid.NewGuid().ToString(),
                            DrillPlanName = Input.DrillingPlanName,
                            PlanStartDate = form["PlanStartDate"] == "" ? (DateTime?)null : Input.PlanStartDate,
                            PlanCreateDate = DateTime.Now,
                            Prediction = true,
                            TenantId = TenantId,
                        };

                        await db.DrillPlanHeader.AddAsync(drillPlanHeader);

                        if (Input.WellId != null)
                        {
                            int i = 0;

                            foreach (var well in Input.WellId)
                            {
                                DateTime? RigRealese = form["RigRealese"][i] == "" ? (DateTime?)null : Input.RigRealese[i];
                                DateTime? SpudWell = form["SpudWell"][i] == "" ? (DateTime?)null : Input.SpudWell[i];
                                DateTime? LastBOPTest = form["LastBOPTest"][i] == "" ? (DateTime?)null : Input.LastBopTest[i];
                                DateTime? NextBOPTest = form["NextBOPTest"][i] == "" ? (DateTime?)null : Input.NextBopTest[i];

                                drillPlanWells = new DrillPlanWells
                                {
                                    DrillPlanWellsId = Guid.NewGuid().ToString(),
                                    DrillPlanId = drillPlanHeader.DrillPlanId,
                                    Wellid = well,
                                    RigRealese = RigRealese,
                                    SPUDWell = SpudWell,
                                    LastBOPTest = LastBOPTest,
                                    NextBOPTest = NextBOPTest,
                                    PlannedTD = Input.PlannedTD[i],
                                    RigId = Input.RigId[i]
                                };

                                await db.DrillPlanWells.AddAsync(drillPlanWells);

                                i += 1;
                            }
                        }

                        await DrillPlanNotification(null, null, "DrillPlanCreate", drillPlanHeader, drillPlanWells, Input);
                    }
                    else
                    {
                        var Plan = db.DrillPlanHeader.Where(x => x.DrillPlanId == Input.DrillingPlanId).FirstOrDefault();

                        if (Plan != null)
                        {
                            Plan.DrillPlanName = Input.DrillingPlanName;
                            Plan.PlanStartDate = Input.PlanStartDate;
                            Plan.Prediction = Input.Predictable;
                            Plan.PlanLastModifyDate = DateTime.Now;

                            db.DrillPlanHeader.Update(Plan);
                            //await db.SaveChangesAsync();
                        }


                        if (!string.IsNullOrEmpty(Input.WellIdList))
                        {
                            var ExistWells = db.DrillPlanWells.Where(x => x.DrillPlanId == Input.DrillingPlanId).ToList();
                            var WellsList = Input.WellIdList.Split(";");

                            var Removedwells = from w in ExistWells
                                               where !WellsList.Contains(w.Wellid)
                                               select w;

                            foreach (var well in Removedwells)
                            {
                                var WellTasks = (from dw in db.DrillPlanWells
                                                 join dt in db.DrillPlanDetails on dw.DrillPlanWellsId equals dt.DrillPlanWellsId
                                                 where dw.DrillPlanId == Input.DrillingPlanId && dw.Wellid == well.Wellid
                                                 select dt).ToList();
                                var Wells = db.DrillPlanWells.Where(x => x.Wellid == well.Wellid && x.DrillPlanId == Input.DrillingPlanId).FirstOrDefault();
                                db.DrillPlanDetails.RemoveRange(WellTasks);
                                await db.SaveChangesAsync();
                                db.DrillPlanWells.RemoveRange(Wells);
                                await db.SaveChangesAsync();
                                await DrillPlanNotification(null, null, "DrillPlanWellRemove", Plan, well, Input);

                            }
                        }
                        else if (string.IsNullOrEmpty(Input.WellIdList))
                        {
                            var ExistWells = db.DrillPlanWells.Where(x => x.DrillPlanId == Input.DrillingPlanId).ToList();
                            foreach (var well in ExistWells)
                            {
                                var WellTasks = (from dw in db.DrillPlanWells
                                                 join dt in db.DrillPlanDetails on dw.DrillPlanWellsId equals dt.DrillPlanWellsId
                                                 where dw.DrillPlanId == Input.DrillingPlanId && dw.Wellid == well.Wellid
                                                 select dt).ToList();
                                var Wells = db.DrillPlanWells.Where(x => x.Wellid == well.Wellid && x.DrillPlanId == Input.DrillingPlanId).FirstOrDefault();
                                db.DrillPlanDetails.RemoveRange(WellTasks);
                                await db.SaveChangesAsync();
                                db.DrillPlanWells.RemoveRange(Wells);
                                await db.SaveChangesAsync();
                                await DrillPlanNotification(null, null, "DrillPlanWellRemove", Plan, well, Input);
                            }
                        }


                        if (Input.WellId != null)
                        {
                            int i = 0;
                            foreach (var well in Input.WellId)
                            {
                                var IsWellsdetailsExists = db.DrillPlanWells.Where(x => x.DrillPlanId == Input.DrillingPlanId && x.Wellid == well).FirstOrDefault();
                                DateTime? RigRealese = form["RigRealese"][i] == "" ? (DateTime?)null : Input.RigRealese[i];
                                DateTime? SpudWell = form["SpudWell"][i] == "" ? (DateTime?)null : Input.SpudWell[i];
                                DateTime? LastBOPTest = form["LastBOPTest"][i] == "" ? (DateTime?)null : Input.LastBopTest[i];
                                DateTime? NextBOPTest = form["NextBOPTest"][i] == "" ? (DateTime?)null : Input.NextBopTest[i];

                                if (IsWellsdetailsExists != null)
                                {
                                    IsWellsdetailsExists.RigRealese = RigRealese;
                                    IsWellsdetailsExists.SPUDWell = SpudWell;
                                    IsWellsdetailsExists.NextBOPTest = NextBOPTest;
                                    IsWellsdetailsExists.LastBOPTest = LastBOPTest;
                                    IsWellsdetailsExists.PlannedTD = Input.PlannedTD[i];
                                    IsWellsdetailsExists.RigId = Input.RigId[i];

                                    db.DrillPlanWells.Update(IsWellsdetailsExists);
                                    await DrillPlanNotification(null, null, "DrillPlanUpdate", Plan, IsWellsdetailsExists, Input);
                                }
                                else
                                {
                                    drillPlanWells = new DrillPlanWells
                                    {
                                        DrillPlanWellsId = Guid.NewGuid().ToString(),
                                        DrillPlanId = Plan.DrillPlanId,
                                        Wellid = well,
                                        RigRealese = RigRealese,
                                        SPUDWell = SpudWell,
                                        LastBOPTest = LastBOPTest,
                                        NextBOPTest = NextBOPTest,
                                        PlannedTD = Input.PlannedTD[i],
                                        RigId = Input.RigId[i]
                                    };
                                    await db.DrillPlanWells.AddAsync(drillPlanWells);
                                    await DrillPlanNotification(null, null, "DrillPlanUpdate", Plan, drillPlanWells, Input);
                                }

                                i += 1;
                            }
                        }
                    }

                    Result = await db.SaveChangesAsync();
                }

                return Result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository DrillingPlanTabContent", null);
                return 0;
            }
        }

        public async Task<int> SaveUpdatePlandetails(PlanDetailsModel PlanDetails, string Tenantid)
        {
            try
            {
                var Result = 0;

                DrillPlanWells drillPlanWells = new DrillPlanWells();
                DrillPlanDetails drillPlanDetails = new DrillPlanDetails();

                if (PlanDetails != null)
                {
                    var wellExist = (from dw in db.DrillPlanWells
                                     join Dh in db.DrillPlanHeader on dw.DrillPlanId equals Dh.DrillPlanId
                                     where Dh.DrillPlanId == PlanDetails.DrillingPlanId && dw.Wellid == PlanDetails.WellId && Dh.TenantId == Tenantid
                                     select dw).FirstOrDefault();

                    if (wellExist != null)
                    {
                        wellExist.RigRealese = PlanDetails.RigRealese;
                        wellExist.SPUDWell = PlanDetails.SpudWell;
                        wellExist.LastBOPTest = PlanDetails.LastBopTest;
                        wellExist.NextBOPTest = PlanDetails.NextBopTest;
                        wellExist.PlannedTD = PlanDetails.PlannedTD;
                        wellExist.RigId = PlanDetails.RigId;
                        db.DrillPlanWells.Update(wellExist);
                        Result = await db.SaveChangesAsync();

                        if (Result == 1)
                        {
                            var Newtasks = await UpdateWelldetailsTasks(PlanDetails, wellExist);
                            //await DrillPlanNotification(null, PlanDetails, "TasksUpdate", null, null, null);

                            if (Newtasks != null && Newtasks.Count > 0)
                            {
                                PlanDetails.drillPlanTasks = Newtasks;
                                await SaveWelldetailsTasks(PlanDetails, wellExist);
                                //await DrillPlanNotification(null, PlanDetails, "TasksCreate", null, null, null);
                            }

                            if (PlanDetails.DeleteTasks.Count > 0)
                            {
                                Result = await Deletetasks(PlanDetails, wellExist);
                            }
                        }
                    }
                    else
                    {
                        drillPlanWells = new DrillPlanWells
                        {
                            DrillPlanWellsId = Guid.NewGuid().ToString(),
                            DrillPlanId = PlanDetails.DrillingPlanId,
                            Wellid = PlanDetails.WellId,
                            RigRealese = PlanDetails.RigRealese,
                            SPUDWell = PlanDetails.SpudWell,
                            LastBOPTest = PlanDetails.LastBopTest,
                            NextBOPTest = PlanDetails.NextBopTest,
                            PlannedTD = PlanDetails.PlannedTD,
                            RigId = PlanDetails.RigId
                        };

                        await db.DrillPlanWells.AddAsync(drillPlanWells);
                        Result = await db.SaveChangesAsync();

                        if (Result == 1)
                        {
                            Result = await SaveWelldetailsTasks(PlanDetails, drillPlanWells);
                            //await DrillPlanNotification(null, PlanDetails, "TasksCreate", null, null, null);
                        }
                    }


                }

                return await Task.FromResult(Result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository SaveUpdatePlandetails", null);
                return 0;
            }
        }


        public async Task<int> Deletetasks(PlanDetailsModel PlanDetails, DrillPlanWells wellExist)
        {
            try
            {
                var Result = 0;
                if (PlanDetails.DeleteTasks.Count > 0)
                {
                    foreach (var task in PlanDetails.DeleteTasks)
                    {
                        var isTasksExist = db.DrillPlanDetails.Where(x => x.TaskId == task && x.DrillPlanWellsId == wellExist.DrillPlanWellsId).FirstOrDefault();
                        if (isTasksExist != null)
                        {
                            db.DrillPlanDetails.Remove(isTasksExist);
                        }
                    }

                    await DrillPlanNotification(null, PlanDetails, "TasksDelete", null, null, null);

                }

                return await Task.FromResult(Result = await db.SaveChangesAsync());
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository SaveUpdatePlandetails", null);
                return 0;
            }
        }

        public async Task<List<PlannedTasksModel>> UpdateWelldetailsTasks(PlanDetailsModel PlanDetails, DrillPlanWells wellExist)
        {
            try
            {
                var result = 0;
                List<PlannedTasksModel> PlannedTasksModelListUpadete = new List<PlannedTasksModel>();
                List<DrillPlanDetails> drillPlanDetailsList = new List<DrillPlanDetails>();
                List<DrillPlanDetails> drillPlanDetailsListUpadete = new List<DrillPlanDetails>();

                if (PlanDetails.drillPlanTasks != null)
                {
                    foreach (var task in PlanDetails.drillPlanTasks)
                    {
                        var IsTaskexist = db.DrillPlanDetails.Where(x => x.DrillPlanWellsId == wellExist.DrillPlanWellsId && x.DrillPlanId == wellExist.DrillPlanId && x.TaskId == task.TaskId).FirstOrDefault();
                        var days = task.ServiceDurationDays == "" ? "00" : task.ServiceDurationDays == null ? "00" : task.ServiceDurationDays;
                        var hours = task.ServiceDurationHours == null ? "00" : task.ServiceDurationHours;
                        var minitus = task.ServiceDurationMinutes == null ? "00" : task.ServiceDurationMinutes;
                        var ServiceDuration = days + ":" + hours + ":" + minitus;
                        dynamic ScheduleTime = task.ScheduleTime == "" ? "00:00" : task.ScheduleTime == null ? "00:00" : task.ScheduleTime;

                        if (IsTaskexist != null)
                        {
                            IsTaskexist.TaskId = task.TaskId;
                            IsTaskexist.TaskName = task.TaskName;
                            IsTaskexist.EmployeeId = task.EmployeeId;
                            IsTaskexist.PlanStartDate = task.PlanStart;
                            IsTaskexist.OperationHours = (decimal?)await CalculateHours(Convert.ToInt32(days), Convert.ToInt32(hours), Convert.ToInt32(minitus));//(decimal?)await CalculateHours(Convert.ToInt32(days), Convert.ToInt32(hours), Convert.ToInt32(minitus));
                            IsTaskexist.PlanFinishedDate = GetPlanFinshDate(task);//task.PlanFinishedDate;
                            if (task.TaskId == "")
                            {
                                IsTaskexist.IsPlanTask = true;
                            }
                            IsTaskexist.StageId = task.StageType;
                            IsTaskexist.CategoryId = task.ServiceCategoryId;
                            IsTaskexist.ServiceDuration = ServiceDuration;
                            IsTaskexist.ScheduleTime = (TimeSpan?)TimeSpan.Parse(ScheduleTime);

                            IsTaskexist.StageId = task.StageType;
                            IsTaskexist.IsSpecialServices = Convert.ToByte(task.IsSpecialServices);
                            ScheduleTime = (TimeSpan?)TimeSpan.Parse(ScheduleTime);
                            IsTaskexist.Day = task.Day;
                            IsTaskexist.Dependency = task.SeletedDependency;
                            IsTaskexist.Depth = task.Depth;
                            IsTaskexist.Description = task.Description;
                            IsTaskexist.LeadTime = task.LeadTime;
                            //IsTaskexist.TaskOrder =  task.TaskOrder;
                            IsTaskexist.TaskOrder = task.existingTaskOrder;
                            IsTaskexist.ActualStartDate = task.ActualPlanStart == null ? null : task.ActualPlanStart;
                            IsTaskexist.ActualFinishedDate = task.ActualPlanFinishedDate == null ? null : task.ActualPlanFinishedDate;
                            IsTaskexist.ServiceOperatorId = task.Vendor;
                            IsTaskexist.IsBiddable = task.IsBiddable;
                            IsTaskexist.Comments = task.commands;

                            drillPlanDetailsListUpadete.Add(IsTaskexist);
                            PlannedTasksModelListUpadete.Add(task);
                            //await DrillPlanNotification(IsTaskexist, PlanDetails, "AssignedTasks", null,null,null);
                        }

                    }

                    db.DrillPlanDetails.UpdateRange(drillPlanDetailsListUpadete);
                }

                result = await db.SaveChangesAsync();

                return await Task.FromResult(PlanDetails.drillPlanTasks.Except(PlannedTasksModelListUpadete).ToList());
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository UpdateWelldetailsTasks", null);
                return null;
            }
        }


        public async Task<int> SaveWelldetailsTasks(PlanDetailsModel PlanDetails, DrillPlanWells wellExist)
        {
            try
            {
                var result = 0;

                if (PlanDetails.drillPlanTasks != null)
                {
                    DrillPlanDetails drillPlanDetails = new DrillPlanDetails();

                    List<DrillPlanDetails> drillPlanDetailsList = new List<DrillPlanDetails>();

                    //if(PlanDetails.DrillingPlanId)
                    string planId = PlanDetails.DrillingPlanId;

                    var lastPlan = db.DrillPlanDetails.Where(x => x.DrillPlanId == PlanDetails.DrillingPlanId).OrderByDescending(x => x.TaskOrder).FirstOrDefault();
                    int? lastTaskOrder = 0;
                    if (lastPlan != null)
                    {
                        lastTaskOrder = lastPlan.TaskOrder == null ? 0 : lastPlan.TaskOrder;
                    }
                    foreach (var task in PlanDetails.drillPlanTasks)
                    {
                        lastTaskOrder = lastTaskOrder + 1;
                        var days = task.ServiceDurationDays == "" ? "00" : task.ServiceDurationDays == null ? "00" : task.ServiceDurationDays;
                        var hours = task.ServiceDurationHours == null ? "00" : task.ServiceDurationHours;
                        var minitus = task.ServiceDurationMinutes == null ? "00" : task.ServiceDurationMinutes;
                        var ServiceDuration = days + ":" + hours + ":" + minitus;
                        dynamic ScheduleTime = task.ScheduleTime == "" ? "00:00" : task.ScheduleTime == null ? "00:00" : task.ScheduleTime;

                        DateTime planDate = Convert.ToDateTime(task.PlanStart);

                        drillPlanDetails = new DrillPlanDetails
                        {
                            DrillPlanDetailsId = Guid.NewGuid().ToString(),
                            DrillPlanId = PlanDetails.DrillingPlanId,
                            DrillPlanWellsId = wellExist.DrillPlanWellsId,
                            TaskId = task.TaskId == null ? Guid.NewGuid().ToString() : task.TaskId == "" ? Guid.NewGuid().ToString() : task.TaskId,
                            TaskName = task.TaskName,
                            EmployeeId = task.EmployeeId,
                            PlanStartDate = task.PlanStart == null ? null : planDate.Year.ToString() == "1" ? null : task.PlanStart,
                            OperationHours = (decimal?)await CalculateHours(Convert.ToInt32(days), Convert.ToInt32(hours), Convert.ToInt32(minitus)),
                            PlanFinishedDate = GetPlanFinshDate(task),//task.PlanFinishedDate,
                            IsPlanTask = task.TaskId == "" ? true : false,
                            StageId = task.StageType,
                            CategoryId = task.ServiceCategoryId,
                            ServiceDuration = ServiceDuration,
                            LeadTime = task.LeadTime,
                            IsSpecialServices = Convert.ToByte(task.IsSpecialServices),
                            ScheduleTime = (TimeSpan?)TimeSpan.Parse(ScheduleTime),
                            Dependency = task.SeletedDependency,
                            Description = task.Description,
                            Day = task.Day,
                            CreatedDate = DateTime.Now,
                            Depth = task.Depth,
                            ActualStartDate = task.ActualPlanStart,
                            ActualFinishedDate = task.ActualPlanFinishedDate,
                            ServiceOperatorId = task.Vendor,
                            Comments = task.commands,
                            TaskOrder = lastTaskOrder
                        };

                        drillPlanDetailsList.Add(drillPlanDetails);
                        //await DrillPlanNotification(drillPlanDetails,PlanDetails, "AssignedTasks", null,null, null);
                    }
                    await db.DrillPlanDetails.AddRangeAsync(drillPlanDetailsList);
                }

                return await Task.FromResult(result = await db.SaveChangesAsync());

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository SaveWelldetailsTasks", null);
                return 0;
            }
        }

        public async Task<List<PlannedTasksModel>> GetPlanDetailsTasks(string wellId, string drillPlanId, string TenantId)
        {
            try
            {
                List<PlannedTasksModel> PlanDetails = new List<PlannedTasksModel>();

                if (!string.IsNullOrEmpty(wellId) && !string.IsNullOrEmpty(drillPlanId))
                {

                    //b8561b66-eab0-489e-86ad-a6aaa16adf12
                    //36220e3b-c4cc-463e-af2b-609f1e89202d
                    var PlanDetail = (from planheader in db.DrillPlanHeader
                                      join planwells in db.DrillPlanWells on planheader.DrillPlanId equals planwells.DrillPlanId
                                      join plandetails in db.DrillPlanDetails on planwells.DrillPlanWellsId equals plandetails.DrillPlanWellsId
                                      where planheader.DrillPlanId == drillPlanId && planwells.Wellid == wellId && planheader.TenantId == TenantId
                                      select new PlannedTasksModel
                                      {
                                          TaskId = plandetails.TaskId,
                                          TaskName = plandetails.TaskName,
                                          ServiceCategoryId = plandetails.CategoryId,
                                          CategoryName = db.serviceCategories.Where(x => x.ServiceCategoryId == plandetails.CategoryId).Select(y => y.Name).FirstOrDefault(),
                                          PlanStart = plandetails.PlanStartDate,
                                          PlanFinishedDate = plandetails.PlanFinishedDate != null ? Convert.ToDateTime(plandetails.PlanFinishedDate) : plandetails.PlanFinishedDate,
                                          OperationDays = (decimal)Math.Round((double)plandetails.OperationHours / 24, 2),
                                          OperationHours = (decimal)Math.Round((decimal)plandetails.OperationHours, 2),
                                          AccumulatedDays = (decimal?)Math.Round((double)plandetails.OperationHours / 24, 2),
                                          //Category = plandetails.CategoryId,
                                          commands = plandetails.Comments,
                                          Day = plandetails.Day,
                                          //Dependency = plandetails.Dependency.Replace(";",","),
                                          Depth = plandetails.Depth,
                                          Description = plandetails.Description,
                                          EmployeeId = plandetails.EmployeeId == null ? "" : plandetails.EmployeeId,
                                          EmployeeName = plandetails.EmployeeId != null && plandetails.EmployeeId != "" ? (string)db.WellIdentityUser.Where(x => x.Id == plandetails.EmployeeId).Select(y => string.Concat(y.FirstName + " ", y.LastName)).FirstOrDefault() : "",
                                          IsPlanTask = plandetails.IsPlanTask,
                                          IsBiddable = (bool?)plandetails.IsBiddable,
                                          IsSpecialServices = plandetails.IsSpecialServices,
                                          ScheduleTime = Convert.ToString(plandetails.ScheduleTime),
                                          //ScheduleTimePicker = plandetails.ScheduleTime,
                                          LeadTime = plandetails.LeadTime,
                                          ServiceDurationDays = plandetails.ServiceDuration != null ? Convert.ToString(plandetails.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries).Length != 3 ? "00" : Convert.ToString(plandetails.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries)[0])) : "00",
                                          ServiceDurationHours = plandetails.ServiceDuration != null ? Convert.ToString(plandetails.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries).Length != 3 ? "00" : Convert.ToString(plandetails.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries)[1])) : "00",
                                          ServiceDurationMinutes = plandetails.ServiceDuration != null ? Convert.ToString(plandetails.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries).Length != 3 ? "00" : Convert.ToString(plandetails.ServiceDuration.Split(':', StringSplitOptions.RemoveEmptyEntries)[2])) : "00",
                                          Serviceoperator = plandetails.ServiceOperatorId,
                                          SeletedDependency = plandetails.Dependency != null ? plandetails.Dependency.Replace(";", ",") : null,
                                          StageType = plandetails.StageId,
                                          StageTypeName = db.Stages.Where(x => x.Id == plandetails.StageId).Select(y => y.Name).FirstOrDefault(),
                                          ActualPlanStart = plandetails.ActualStartDate,
                                          Vendor = plandetails.ServiceOperatorId == null ? "" : plandetails.ServiceOperatorId,
                                          VendorName = plandetails.ServiceOperatorId != null && plandetails.ServiceOperatorId != "" ? db.CorporateProfile.Where(x => x.TenantId == plandetails.ServiceOperatorId).Select(x => x.Name).FirstOrDefault() : "",
                                          TaskOrder = (int)plandetails.TaskOrder,
                                          ActualPlanFinishedDate = plandetails.ActualFinishedDate,
                                          IsRowModified = false,
                                          IsBenchMark = (bool)plandetails.IsBenchMark,
                                          IsPreSpud = (bool)plandetails.IsPreSpud,
                                          PhoneNumber = plandetails.ServiceOperatorId != null && plandetails.ServiceOperatorId != "" ?
                                                      (string)db.CorporateProfile.Where(x => x.TenantId == plandetails.ServiceOperatorId).Select(x => x.Phone).FirstOrDefault() :
                                                     plandetails.EmployeeId != null && plandetails.EmployeeId != "" ? (string)db.WellIdentityUser.Where(x => x.Id == plandetails.EmployeeId).Select(x => x.PhoneNumber).FirstOrDefault() : ""
                                          //ActualPlanFinishedDate = plandetails.ActualStartDate != null ? Convert.ToDateTime(plandetails.ActualStartDate).AddHours((double)plandetails.OperationHours) : plandetails.ActualStartDate
                                      }).OrderByDescending(x => x.IsPreSpud).ThenBy(t => t.TaskOrder).ToList();

                    //PhoneNumber = plandetails.ServiceOperatorId != null && plandetails.ServiceOperatorId != "" ?
                    //(string)db.CorporateProfile.Where(x => x.TenantId == plandetails.ServiceOperatorId).Select(x => x.Phone).FirstOrDefault() :
                    //plandetails.EmployeeId != null && plandetails.EmployeeId != "" ? (string)db.WellIdentityUser.Where(x => x.Id == plandetails.EmployeeId).Select(x => x.PhoneNumber).FirstOrDefault() : ""

                    PlanDetails.AddRange(PlanDetail);

                    //Accum Days calculation
                    float accumDays = 0;
                    //float operationDisplayHours = 0;
                    foreach (var item in PlanDetail)
                    {
                        var operationDisplayHoursArr = item.OperationHours.ToString("#.##").Split(".");
                        var operationDisplayHours = "";
                        var operationMinutes = "";
                        if (operationDisplayHoursArr.Length > 1)
                        {
                            if (operationDisplayHoursArr[1].ToString() == "3" || operationDisplayHoursArr[1].ToString() == "0")
                            {
                                operationMinutes = operationDisplayHoursArr[1].ToString() + "0";
                            }
                            else
                            {
                                operationMinutes = operationDisplayHoursArr[1].ToString();
                            }
                            operationDisplayHours = operationDisplayHoursArr[0].ToString();
                            operationDisplayHours = operationDisplayHours.ToString() + ".";
                            operationMinutes = operationMinutes.ToString() == "15" ? "25" : operationMinutes.ToString() == "30" ? "50" : operationMinutes.ToString() == "45" ? "75" : "00";

                            operationDisplayHours = operationDisplayHours.ToString() + operationMinutes.ToString();
                        }
                        else
                        {
                            operationDisplayHours = operationDisplayHoursArr[0].ToString() + "." + "00".ToString();
                        }

                        item.OperationDays = (decimal)Math.Round(Convert.ToDouble(operationDisplayHours) / 24, 2);
                        accumDays = accumDays + (float)item.OperationDays;
                        item.AccumulatedDays = (decimal)accumDays;
                    }
                }

                return await Task.FromResult(PlanDetails);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetPlanDetailsTasks", null);
                return null;
            }
        }
        private string GetProviderOrEmployeePhoneNumber(DrillPlanDetails planDetails)
        {
            string phoneNumber = "";
            try
            {
                if (planDetails.ServiceOperatorId != null)
                {
                    phoneNumber = (string)db.CorporateProfile.Where(x => x.TenantId == planDetails.ServiceOperatorId).Select(x => x.Phone).FirstOrDefault();
                }
                else if (planDetails.EmployeeId != null)
                {
                    phoneNumber = (string)db.WellIdentityUser.Where(x => x.Id == planDetails.EmployeeId).Select(x => x.PhoneNumber).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetProviderOrEmployeePhoneNumber", null);
                return null;
            }
            return phoneNumber;
        }

        public Task<List<wellmodel>> GetDrillPlanWells(string drillPlanId, string tenantId)
        {
            try
            {
                List<wellmodel> WellList = new List<wellmodel>();
                if (!string.IsNullOrEmpty(drillPlanId))
                {

                    WellList = (from planHeader in db.DrillPlanHeader
                                join PlanWells in db.DrillPlanWells on planHeader.DrillPlanId equals PlanWells.DrillPlanId
                                join well in db.WellRegister on PlanWells.Wellid equals well.well_id
                                where planHeader.TenantId == tenantId && planHeader.DrillPlanId == drillPlanId
                                select new wellmodel
                                {
                                    wellId = PlanWells.Wellid,
                                    wellName = well.wellname
                                }).Distinct().OrderBy(x => x.wellName).ToList();
                }

                return Task.FromResult(WellList);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetDrillPlanWells", null);
                return null;
            }
        }


        public async Task<List<DrillPlanModel>> GetDrillPlanList(string tenantId, string rigId)
        {
            try
            {
                var checkwellFilter = rigId == DLL.Constants.NoSpecificWellFilterKey;
                List<DrillPlanModel> drillplanlist = new List<DrillPlanModel>();

                if (!string.IsNullOrEmpty(tenantId))
                {
                    drillplanlist = (from planHeader in db.DrillPlanHeader
                                     join PlanWels in db.DrillPlanWells on planHeader.DrillPlanId equals PlanWels.DrillPlanId
                                     where planHeader.TenantId == tenantId && (PlanWels.RigId == rigId && !checkwellFilter || checkwellFilter)
                                     select new DrillPlanModel
                                     {
                                         DrillPlanId = planHeader.DrillPlanId,
                                         DrillPlanName = planHeader.DrillPlanName
                                     }).Distinct().OrderBy(x => x.DrillPlanName).ToList();
                }

                return await Task.FromResult(drillplanlist);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetPlanDetailsTasks", null);
                return null;
            }
        }


        public async Task<int> DeleteDrillPlan(string planId, string tenantId)
        {
            try
            {
                int Result = 0;

                if (!string.IsNullOrEmpty(planId))
                {
                    var DrillPlanTask = db.DrillPlanDetails.Where(x => x.DrillPlanId == planId).ToList();
                    var DrillPlanWells = db.DrillPlanWells.Where(x => x.DrillPlanId == planId).ToList();
                    var DrillPlan = db.DrillPlanHeader.Where(x => x.DrillPlanId == planId).FirstOrDefault();

                    db.DrillPlanDetails.RemoveRange(DrillPlanTask);
                    await db.SaveChangesAsync();
                    db.DrillPlanWells.RemoveRange(DrillPlanWells);
                    await db.SaveChangesAsync();
                    db.DrillPlanHeader.RemoveRange(DrillPlan);
                    Result = await db.SaveChangesAsync();
                    DrillPlanNotification(null, null, "DrillPlanDelete", DrillPlan, null, null);
                }

                return await Task.FromResult(Result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetDrillPlanWells", null);
                return 0;
            }
        }


        public async Task<int> DrillPlanNotification(DrillPlanDetails PlanTasks, PlanDetailsModel PlanDetails, string Status, DrillPlanHeader DrillPlanHeader, DrillPlanWells DrillPlanWells, DrillPlandetailsViewModel DrillPlan)
        {
            try
            {
                int Result = 0;
                MessageQueue MessageQueue = new MessageQueue();
                var TenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var Operator = db.CorporateProfile.Where(x => x.TenantId == TenantId).FirstOrDefault();
                if (Status == "AssignedTasks")
                {
                    var Vendor = db.CorporateProfile.Where(x => x.TenantId == PlanTasks.ServiceOperatorId).FirstOrDefault();
                    var PlanName = db.DrillPlanHeader.Where(x => x.DrillPlanId == PlanDetails.DrillingPlanId).FirstOrDefault();

                    if (Vendor != null)
                    {
                        var Well = db.WellRegister.Where(x => x.well_id == PlanDetails.WellId).FirstOrDefault();

                        MessageQueue = new MessageQueue
                        {
                            From_id = Operator.UserId,
                            To_id = Vendor.UserId,
                            Type = 10,
                            IsActive = 1,
                            EntityId = PlanDetails.DrillingPlanId,
                            JobName = "Drill plan",
                            TaskName = "Drill Plan :  " + PlanName.DrillPlanName + ", " + "Well Name : " + Well.wellname + ", " + "Operator Name : " + Operator.Name + ", " + "Task Name : " + PlanTasks.TaskName + ", " + "Task Date & Time : " + DateTime.Now.ToString("MM/dd/yyyy h:mm tt"),
                            CreatedDate = DateTime.Now
                        };

                        await db.MessageQueues.AddAsync(MessageQueue);
                        Result = await db.SaveChangesAsync();
                    }

                }//group notification
                else if (Status == "DrillPlanCreate")
                {
                    var OperatorUsers = db.WellIdentityUser.Where(x => x.TenantId == TenantId).ToList();
                    List<string> Wellnames = new List<string>();

                    if (DrillPlan.WellId != null)
                    {
                        foreach (var well in DrillPlan.WellId)
                        {
                            Wellnames.Add(db.WellRegister.Where(x => x.well_id == well).Select(y => y.wellname).FirstOrDefault());
                        }
                    }

                    foreach (var OpeUsers in OperatorUsers)
                    {
                        MessageQueue = new MessageQueue
                        {
                            From_id = Operator.UserId,
                            To_id = OpeUsers.Id,
                            Type = 10,
                            IsActive = 1,
                            EntityId = DrillPlanHeader.DrillPlanId,
                            JobName = "Drill plan",
                            TaskName = "Drill Plan Created On " + DateTime.Now.ToString("MM/dd/yyyy h:mm tt") + "  Drill Plan :  " + DrillPlanHeader.DrillPlanName + ", " + "Wells Added : " + string.Join(",", Wellnames),
                            CreatedDate = DateTime.Now
                        };
                        await db.MessageQueues.AddAsync(MessageQueue);
                        Result = await db.SaveChangesAsync();
                    }

                }
                else if (Status == "DrillPlanUpdate")
                {
                    var OperatorUsers = db.WellIdentityUser.Where(x => x.TenantId == TenantId).ToList();
                    List<string> Wellnames = new List<string>();
                    foreach (var well in DrillPlan.WellId)
                    {
                        Wellnames.Add(db.WellRegister.Where(x => x.well_id == well).Select(y => y.wellname).FirstOrDefault());
                    }

                    foreach (var OpeUsers in OperatorUsers)
                    {
                        MessageQueue = new MessageQueue
                        {
                            From_id = Operator.UserId,
                            To_id = OpeUsers.Id,
                            Type = 10,
                            IsActive = 1,
                            EntityId = DrillPlanHeader.DrillPlanId,
                            JobName = "Drill plan",
                            TaskName = "Drill Plan Modified On " + DateTime.Now.ToString("MM/dd/yyyy h:mm tt") + "  Drill Plan :  " + DrillPlanHeader.DrillPlanName + ", " + "Wells Updated  : " + string.Join(",", Wellnames),
                            CreatedDate = DateTime.Now
                        };
                        await db.MessageQueues.AddAsync(MessageQueue);
                        Result = await db.SaveChangesAsync();
                    }


                }
                else if (Status == "DrillPlanWellRemove")
                {
                    var OperatorUsers = db.WellIdentityUser.Where(x => x.TenantId == TenantId).ToList();

                    var Wellnames = db.WellRegister.Where(x => x.well_id == DrillPlanWells.Wellid).Select(y => y.wellname).FirstOrDefault();

                    foreach (var OpeUsers in OperatorUsers)
                    {
                        MessageQueue = new MessageQueue
                        {
                            From_id = Operator.UserId,
                            To_id = OpeUsers.Id,
                            Type = 10,
                            IsActive = 1,
                            EntityId = DrillPlanHeader.DrillPlanId,
                            JobName = "Drill plan",
                            TaskName = "Drill Plan Modified On " + DateTime.Now.ToString("MM/dd/yyyy h:mm tt") + "  Drill Plan :  " + DrillPlanHeader.DrillPlanName + ", " + "Wells Deleted : " + Wellnames,
                            CreatedDate = DateTime.Now
                        };
                        await db.MessageQueues.AddAsync(MessageQueue);
                        Result = await db.SaveChangesAsync();
                    }

                }
                else if (Status == "TasksCreate")
                {
                    var OperatorUsers = db.WellIdentityUser.Where(x => x.TenantId == TenantId).ToList();
                    var Wellnames = db.WellRegister.Where(x => x.well_id == PlanDetails.WellId).Select(y => y.wellname).FirstOrDefault();
                    var PlanName = db.DrillPlanHeader.Where(x => x.DrillPlanId == PlanDetails.DrillingPlanId).FirstOrDefault();
                    foreach (var OpeUsers in OperatorUsers)
                    {
                        MessageQueue = new MessageQueue
                        {
                            From_id = Operator.UserId,
                            To_id = OpeUsers.Id,
                            Type = 10,
                            IsActive = 1,
                            EntityId = PlanDetails.DrillingPlanId,
                            JobName = "Drill plan",
                            TaskName = "Drill Plan Tasks Created On " + DateTime.Now.ToString("MM/dd/yyyy h:mm tt") + "  Drill Plan :  " + PlanName.DrillPlanName + ", " + "Well : " + Wellnames + ", " + "Tasks Added : " + PlanDetails.drillPlanTasks.Count(),
                            CreatedDate = DateTime.Now
                        };
                        await db.MessageQueues.AddAsync(MessageQueue);
                        Result = await db.SaveChangesAsync();
                    }
                }
                else if (Status == "TasksUpdated")
                {
                    var OperatorUsers = db.WellIdentityUser.Where(x => x.TenantId == TenantId).ToList();
                    var Wellnames = db.WellRegister.Where(x => x.well_id == PlanDetails.WellId).Select(y => y.wellname).FirstOrDefault();
                    var PlanName = db.DrillPlanHeader.Where(x => x.DrillPlanId == PlanDetails.DrillingPlanId).FirstOrDefault();

                    foreach (var OpeUsers in OperatorUsers)
                    {
                        MessageQueue = new MessageQueue
                        {
                            From_id = Operator.UserId,
                            To_id = OpeUsers.Id,
                            Type = 10,
                            IsActive = 1,
                            EntityId = PlanDetails.DrillingPlanId,
                            JobName = "Drill plan",
                            TaskName = "Drill Plan Tasks Updated On " + DateTime.Now.ToString("MM/dd/yyyy h:mm tt") + "  Drill Plan :  " + PlanName.DrillPlanName + ", " + "Well : " + Wellnames + ", " + "Tasks Updated : " + PlanDetails.drillPlanTasks.Count(),
                            CreatedDate = DateTime.Now
                        };
                        await db.MessageQueues.AddAsync(MessageQueue);
                        Result = await db.SaveChangesAsync();
                    }
                }
                else if (Status == "TasksDelete")
                {
                    var OperatorUsers = db.WellIdentityUser.Where(x => x.TenantId == TenantId).ToList();
                    var Wellnames = db.WellRegister.Where(x => x.well_id == PlanDetails.WellId).Select(y => y.wellname).FirstOrDefault();
                    var PlanName = db.DrillPlanHeader.Where(x => x.DrillPlanId == PlanDetails.DrillingPlanId).FirstOrDefault();

                    List<string> TasksName = new List<string>();
                    foreach (var task in PlanDetails.DeleteTasks)
                    {
                        TasksName.Add(db.DrillPlanDetails.Where(x => x.TaskId == task && x.DrillPlanId == PlanDetails.DrillingPlanId).Select(y => y.TaskName).FirstOrDefault());
                    }
                    foreach (var OpeUsers in OperatorUsers)
                    {
                        MessageQueue = new MessageQueue
                        {
                            From_id = Operator.UserId,
                            To_id = OpeUsers.Id,
                            Type = 10,
                            IsActive = 1,
                            EntityId = PlanDetails.DrillingPlanId,
                            JobName = "Drill plan",
                            TaskName = "Drill Plan Tasks Deleted On " + DateTime.Now.ToString("MM/dd/yyyy h:mm tt") + "  Drill Plan :  " + PlanName.DrillPlanName + ", " + "Well : " + Wellnames + ", " + "Tasks Deleted : " + string.Join(",", TasksName),
                            CreatedDate = DateTime.Now
                        };
                        await db.MessageQueues.AddAsync(MessageQueue);
                        Result = await db.SaveChangesAsync();
                    }
                }
                else if (Status == "DrillPlanDelete")
                {
                    var OperatorUsers = db.WellIdentityUser.Where(x => x.TenantId == TenantId).ToList();
                    foreach (var OpeUsers in OperatorUsers)
                    {
                        MessageQueue = new MessageQueue
                        {
                            From_id = Operator.UserId,
                            To_id = OpeUsers.Id,
                            Type = 10,
                            IsActive = 1,
                            EntityId = null,
                            JobName = "Drill plan",
                            TaskName = "Drill Plan Deleted On " + DateTime.Now.ToString("MM/dd/yyyy h:mm tt") + "  Drill Plan :  " + DrillPlanHeader.DrillPlanName,
                            CreatedDate = DateTime.Now
                        };
                        await db.MessageQueues.AddAsync(MessageQueue);
                        Result = await db.SaveChangesAsync();
                    }
                }

                return Result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository DrillPlanNotification", null);
                return 0;
            }
        }


        public async Task<int> ImportTaskForDrillingPlan(string wellId, string drillPlanId, string TenantId)
        {
            try
            {
                int IsExisting = 0;
                //List<Model.Administration.TaskModel> MasterTasks = new List<Model.Administration.TaskModel>();
                List<DrillPlanDetails> UpadtePlanDetailsList = new List<DrillPlanDetails>();
                DrillPlanDetails PlanDetails = new DrillPlanDetails();

                if (!string.IsNullOrEmpty(wellId) && !string.IsNullOrEmpty(drillPlanId))
                {
                    var MasterTasks = await GetTasksList();
                    var PlanTasks = (from Dpd in db.DrillPlanDetails
                                     where Dpd.DrillPlanId == drillPlanId && Dpd.DrillPlanWellsId == wellId
                                     select Dpd).ToList();

                    if (MasterTasks.Count > 0 && PlanTasks != null)
                    {

                        foreach (var Tasks in PlanTasks)
                        {
                            var MasterTask = MasterTasks.Where(x => x.TaskId == Tasks.TaskId).FirstOrDefault();

                            if (MasterTask != null)
                            {
                                //MasterTask.ServiceDuration = MasterTask.ServiceDuration == null ? "00:00:00:" : MasterTask.ServiceDuration;
                                if (MasterTask.ServiceDuration == null)
                                {
                                    MasterTask.ServiceDuration = "00:00:00";
                                }
                                //IsChanged 
                                if (Tasks.TaskName == MasterTask.Name && Tasks.CategoryId == MasterTask.ServiceCategoryId && Tasks.StageId == MasterTask.StageType && Convert.ToByte(Tasks.IsSpecialServices) == Convert.ToByte(MasterTask.IsSpecialServices) && Tasks.ServiceDuration == MasterTask.ServiceDuration)
                                {
                                    if (IsExisting == 0)
                                        IsExisting = 0;
                                }
                                else
                                {
                                    PlanDetails = db.DrillPlanDetails.Where(x => x.TaskId == Tasks.TaskId && x.DrillPlanId == drillPlanId && x.DrillPlanWellsId == wellId).FirstOrDefault();
                                    var Duration = MasterTask.ServiceDuration == null ? null : MasterTask.ServiceDuration.Split(":");
                                    PlanDetails.TaskId = MasterTask.TaskId;
                                    PlanDetails.TaskName = MasterTask.Name;
                                    PlanDetails.StageId = MasterTask.StageType;
                                    PlanDetails.CategoryId = MasterTask.ServiceCategoryId;
                                    PlanDetails.IsSpecialServices = Convert.ToByte(MasterTask.IsSpecialServices);
                                    PlanDetails.ServiceDuration = MasterTask.ServiceDuration == null ? "00:00:00" : MasterTask.ServiceDuration;
                                    PlanDetails.Dependency = MasterTask.SeletedDependency == null ? null : MasterTask.SeletedDependency.Replace(",", ";");
                                    PlanDetails.ScheduleTime = Tasks.ScheduleTime;
                                    PlanDetails.OperationHours = Duration == null ? null : (byte?)await CalculateHours(Convert.ToInt32(Duration[0]), Convert.ToInt32(Duration[1]), Convert.ToInt32(Duration[2]));

                                    db.DrillPlanDetails.Update(PlanDetails);
                                    UpadtePlanDetailsList.Add(PlanDetails);
                                    IsExisting = 1;
                                }
                            }
                        }
                        await db.SaveChangesAsync();
                    }
                }

                return IsExisting;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository ImportTaskForDrillingPlan", null);
                return 0;
            }
        }


        //Common Repository

        public bool GetComponentPermission(string componentName, string userId, string operTenantId)
        {
            try
            {
                var componentId = 0;
                componentId = db.Components.Where(c => c.ComponentName == componentName).Select(c => c.ComponentId).FirstOrDefault();

                if (componentId == null)
                {
                    componentId = 0;
                }

                IList<string> userRoleNames = null;

                WellIdentityUser user = new WellIdentityUser();
                user = GetUserDetail(userId);

                userRoleNames = _userManager.GetRolesAsync(user).Result;

                var tenantRoles = (from r in _roleManager.Roles
                                   join tr in db.TenantRoles on r.Id equals tr.RoleId
                                   where tr.TenantId == operTenantId
                                   select r).ToList();

                UserViewModel userViewModel = new UserViewModel();
                userViewModel.roles = new List<IdentityRole>();
                userViewModel.SelectedRoles = "";

                List<string> userRoles = new List<string>();
                foreach (var tenantRole in tenantRoles)
                {
                    if (userRoleNames != null && userRoleNames.Contains(tenantRole.Name))
                    {

                        userRoles.Add(tenantRole.Id);
                    }
                }

                var componentPermission = (from CL in db.RolePermissionComponentLinks
                                           join CM in db.Components on CL.ComponentId equals CM.ComponentId
                                           join RP in db.RolePermissionLinks on CL.RolePermissionId equals RP.RolePermissionId
                                           join UR in db.UserRoles on RP.RoleId equals UR.RoleId
                                           join US in db.Users on UR.UserId equals US.Id
                                           where CM.ComponentId == componentId && CL.IsPermitted == true
                                           && userRoles.Contains(RP.RoleId)
                                           select RP.IsPermitted
                                  ).FirstOrDefault();

                bool permission = Convert.ToBoolean(componentPermission);
                return permission;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetComponentPermission", null);
                return false;
            }
        }

        private DateTime? GetPlanFinshDate(PlannedTasksModel PlanTasks)
        {
            try
            {
                PlanTasks.PlanFinishedDate = null;

                if (PlanTasks.PlanStart != null)
                {
                    string[] hoursArray = Convert.ToString(PlanTasks.OperationHours).Split(".");
                    int mins = 0;
                    if (hoursArray != null)
                    {
                        if (hoursArray.Length > 0)
                        {
                            if (hoursArray.Length == 2)
                            {
                                mins = Convert.ToInt32(hoursArray[1]);

                                if (mins.ToString().Length == 1)
                                {
                                    var value = mins.ToString() + "0";
                                    mins = Convert.ToInt32(value);

                                }

                                mins = mins == 15 ? mins : mins == 30 ? mins : mins == 45 ? mins : 00;
                            }
                            else if (hoursArray.Length == 1)
                            {
                                mins = Convert.ToInt32("00");
                            }

                        }
                    }

                    PlanTasks.PlanFinishedDate = Convert.ToDateTime(PlanTasks.PlanStart).AddHours(Convert.ToInt32(hoursArray[0])).AddMinutes(mins);
                }
                else
                {
                    PlanTasks.PlanFinishedDate = PlanTasks.PlanFinishedDate == null ? null : PlanTasks.PlanFinishedDate;
                }

                return PlanTasks.PlanFinishedDate;
            }
            catch (Exception ex)
            {
                return PlanTasks.PlanFinishedDate;
            }
        }

        public async Task<List<DispatchRoutesModel>> GetDispatchRoutes(string UserId, bool isForApi)
        {
            List<DispatchRoutesModel> dispatchObj = new List<DispatchRoutesModel>();
            List<DispatchRoutesModel> result = new List<DispatchRoutesModel>();

            try
            {
                var dispatchCurrentLocations = GetDispatchUsersCurrentLocation();
                if (UserId != "" && UserId != null)
                {
                    if (isForApi == false)
                    {
                        dispatchObj = (from dr in db.DispatchRoutes
                                       join us in db.Users on dr.UserId equals us.Id
                                       where dr.UserId == UserId
                                       orderby dr.CurrentRouterOrder ascending
                                       select new DispatchRoutesModel
                                       {
                                           dispatchid = dr.DispatchId,
                                           //dispatchnotes = dr.DispatchNotes,
                                           userid = dr.UserId,
                                           customer = dr.Customer,
                                           address = dr.LocationAddress,
                                           locationname = dr.LocationName,
                                           city = dr.LocationCity,
                                           state = dr.LocationState,
                                           zip = dr.LocationZip,
                                           routeorder = dr.RouteOrder,
                                           username = us.FirstName + " " + us.LastName,
                                           latitude = dr.Latitude,
                                           longitude = dr.Longitude,
                                          
                                           createddate = dr.CreatedDate,
                                           //modifieddate = dr.ModifiedDate,
                                           //ismodified = dr.IsModified,
                                           api = dr.APINumber,
                                           rigname = dr.RigName,
                                           rigid = dr.RigId,
                                           wellname = dr.WellName
                                       }
                                  ).ToList();                      
                    }
                    else
                    {
                        dispatchObj = (from dr in db.DispatchRoutes
                                       join us in db.Users on dr.UserId equals us.Id
                                       where dr.UserId == UserId
                                       orderby dr.CurrentRouterOrder ascending
                                       select new DispatchRoutesModel
                                       {
                                           dispatchid = dr.DispatchId,
                                           dispatchnotes = dr.DispatchNotes,
                                           userid = dr.UserId,
                                           customer = dr.Customer,
                                           address = dr.LocationAddress,
                                           locationname = dr.LocationName,
                                           city = dr.LocationCity,
                                           state = dr.LocationState,
                                           zip = dr.LocationZip,
                                           routeorder = dr.RouteOrder,
                                           username = us.FirstName + " " + us.LastName,
                                           latitude = dr.Latitude,
                                           longitude = dr.Longitude,
                                         
                                           createddate = dr.CreatedDate,
                                           //modifieddate = dr.ModifiedDate,
                                           //ismodified = dr.IsModified,
                                           api = dr.APINumber,
                                           rigname = dr.RigName,
                                           wellid = dr.WellId,
                                           rigid = dr.RigId,
                                           wellname = dr.WellName
                                       }
                                     ).ToList();

                    }
                   
                }
                else
                {
                    
                    if (isForApi == false)
                    {                        
                        dispatchObj = (from dr in db.DispatchRoutes
                                       join us in db.Users on dr.UserId equals us.Id
                                      
                                       select new DispatchRoutesModel
                                       {
                                           dispatchid = dr.DispatchId,
                                           dispatchnotes = "",//dr.DispatchNotes,
                                           userid = dr.UserId,
                                           customer = dr.Customer,
                                           address = dr.LocationAddress,
                                           locationname = dr.LocationName,
                                           city = dr.LocationCity,
                                           state = dr.LocationState,
                                           zip = dr.LocationZip,
                                           routeorder = dr.RouteOrder,
                                           username = us.FirstName + " " + us.LastName,
                                           latitude = dr.Latitude,
                                           longitude = dr.Longitude,
                                        
                                           createddate = dr.CreatedDate,
                                           //modifieddate = dr.ModifiedDate,
                                           //ismodified = dr.IsModified,
                                           api = dr.APINumber,
                                           rigname = dr.RigName,
                                           wellid = dr.WellId,
                                           rigid = dr.RigId,
                                           wellname = dr.WellName
                                       }
                                   ).ToList();

                        if (dispatchObj.Count > 0)
                        {

                        }
                            if (dispatchCurrentLocations.Count > 0)
                        {                            
                            foreach (var dispatchitem in dispatchCurrentLocations)
                            {
                                if (dispatchObj.Count > 0)
                                {
                                    List<DispatchRoutesModel> dispatchRoute = (from dp in dispatchObj
                                                                         where dp.userid == dispatchitem.userid
                                                                         select new DispatchRoutesModel
                                                                         {
                                                                             userid = dp.userid
                                                                         }).ToList();                                             

                                    if (dispatchRoute.Count > 0)
                                    {
                                        if (dispatchRoute[0].userid == "")
                                        {
                                            dispatchObj.Add(dispatchitem);
                                        }
                                    }
                                    else
                                    {
                                        dispatchObj.Add(dispatchitem);
                                    }
                                }
                                else
                                {
                                    dispatchObj.Add(dispatchitem);
                                }                                
                                                               
                            }
                        }
                    }
                    else
                    {
                        dispatchObj = (from dr in db.DispatchRoutes
                                       join us in db.Users on dr.UserId equals us.Id
                                      
                                       select new DispatchRoutesModel
                                       {
                                           dispatchid = dr.DispatchId,
                                           dispatchnotes = "",//dr.DispatchNotes,
                                           userid = dr.UserId,
                                           customer = dr.Customer,
                                           address = dr.LocationAddress,
                                           locationname = dr.LocationName,
                                           city = dr.LocationCity,
                                           state = dr.LocationState,
                                           zip = dr.LocationZip,
                                           routeorder = dr.RouteOrder,
                                           username = us.FirstName + " " + us.LastName,
                                           latitude = dr.Latitude,
                                           longitude = dr.Longitude,
                                           
                                           createddate = dr.CreatedDate,
                                           //modifieddate = dr.ModifiedDate,
                                           //ismodified = dr.IsModified,
                                           api = dr.APINumber,
                                           rigname = dr.RigName,
                                           wellid = dr.WellId,
                                           rigid=dr.RigId,
                                           wellname = dr.WellName
                                       }
                                   ).ToList();
                        
                    }
                    
                }

                if (UserId != "" && UserId != null)
                {
                    dispatchObj = dispatchObj.OrderBy(x => x.currentrouterorder).ToList();
                }

                foreach (var item in dispatchObj)
                {
                    DispatchRoutesModel dispatchRouterModelObj = new DispatchRoutesModel();

                    dispatchRouterModelObj.dispatchid = item.dispatchid;
                    dispatchRouterModelObj.dispatchnotes = "";//item.dispatchnotes;
                    dispatchRouterModelObj.userid = item.userid;
                    dispatchRouterModelObj.customer = item.customer;
                    dispatchRouterModelObj.address = item.address;
                    dispatchRouterModelObj.locationname = item.locationname;
                    dispatchRouterModelObj.city = item.city;
                    dispatchRouterModelObj.state = item.state;
                    dispatchRouterModelObj.zip = item.zip;
                    dispatchRouterModelObj.routeorder = item.routeorder;
                    dispatchRouterModelObj.username = item.username;
                    dispatchRouterModelObj.latitude = item.latitude;
                    dispatchRouterModelObj.longitude = item.longitude;
                    
                    dispatchRouterModelObj.createddate = item.createddate;
                    //dispatchRouterModelObj.modifieddate = item.modifieddate;
                    //dispatchRouterModelObj.ismodified = item.ismodified;
                    dispatchRouterModelObj.api = item.api??"";
                    dispatchRouterModelObj.rigname = item.rigname??"";
                    dispatchRouterModelObj.wellid = item.wellid??"";
                    dispatchRouterModelObj.rigid = item.rigid ?? "";
                    dispatchRouterModelObj.wellname = item.wellname??"";
                    dispatchRouterModelObj.rigwellandlocation = Convert.ToString(item.locationname ?? "") == "" ? Convert.ToString(item.rigname ?? "") + " " + Convert.ToString(item.wellname ?? "") : Convert.ToString(item.locationname);// : Convert.ToString(item.rigname ?? "") + " " + Convert.ToString(item.wellname ?? "") + " " + Convert.ToString(item.locationname ?? "");
                    result.Add(dispatchRouterModelObj);
                }

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetDispatchRoutes", null);
            }
            return result;
        }

        public async Task<List<DispatchRoutesModel>> GetDispatchRoutes_Preview(string UserId, bool isForApi)
        {
            List<DispatchRoutesModel> dispatchObj = new List<DispatchRoutesModel>();
            List<DispatchRoutesModel> result = new List<DispatchRoutesModel>();

            try
            {
                var dispatchCurrentLocations = GetDispatchUsersCurrentLocation();
                if (UserId != "" && UserId != null)
                {
                    if (isForApi == false)
                    {
                        dispatchObj = (from dr in db.DispatchRoutes
                                       join us in db.Users on dr.UserId equals us.Id
                                       where dr.UserId == UserId && dr.RecordStatus != "0" && dr.RouteOrder != -1
                                       orderby dr.CurrentRouterOrder ascending
                                       select new DispatchRoutesModel
                                       {
                                           dispatchid = dr.DispatchId,
                                           //dispatchnotes = dr.DispatchNotes,
                                           userid = dr.UserId,
                                           customer = dr.Customer,
                                           address = dr.LocationAddress,
                                           locationname = dr.LocationName,
                                           city = dr.LocationCity,
                                           state = dr.LocationState,
                                           zip = dr.LocationZip,
                                           routeorder = dr.RouteOrder,
                                           username = us.FirstName + " " + us.LastName,
                                           latitude = dr.Latitude,
                                           longitude = dr.Longitude,

                                           createddate = dr.CreatedDate,
                                           //modifieddate = dr.ModifiedDate,
                                           //ismodified = dr.IsModified,
                                           api = dr.APINumber,
                                           rigname = dr.RigName,
                                           rigid = dr.RigId,
                                           wellname = dr.WellName,
                                           wellid = dr.WellId
                                       }
                                  ).ToList();
                    }
                    else
                    {
                        dispatchObj = (from dr in db.DispatchRoutes
                                       join us in db.Users on dr.UserId equals us.Id
                                       where dr.UserId == UserId && dr.RecordStatus != "0" && dr.RouteOrder != -1
                                       orderby dr.CurrentRouterOrder ascending
                                       select new DispatchRoutesModel
                                       {
                                           dispatchid = dr.DispatchId,
                                           dispatchnotes = dr.DispatchNotes,
                                           userid = dr.UserId,
                                           customer = dr.Customer,
                                           address = dr.LocationAddress,
                                           locationname = dr.LocationName,
                                           city = dr.LocationCity,
                                           state = dr.LocationState,
                                           zip = dr.LocationZip,
                                           routeorder = dr.RouteOrder,
                                           username = us.FirstName + " " + us.LastName,
                                           latitude = dr.Latitude,
                                           longitude = dr.Longitude,

                                           createddate = dr.CreatedDate,
                                           //modifieddate = dr.ModifiedDate,
                                           //ismodified = dr.IsModified,
                                           api = dr.APINumber,
                                           rigname = dr.RigName,
                                           wellid = dr.WellId,
                                           rigid = dr.RigId,
                                           wellname = dr.WellName
                                       }
                                     ).ToList();

                    }

                }
                else
                {

                    if (isForApi == false)
                    {
                        dispatchObj = (from dr in db.DispatchRoutes
                                       join us in db.Users on dr.UserId equals us.Id
                                      select new DispatchRoutesModel
                                       {
                                           dispatchid = dr.DispatchId,
                                           dispatchnotes = "",//dr.DispatchNotes,
                                           userid = dr.UserId,
                                           customer = dr.Customer,
                                           address = dr.LocationAddress,
                                           locationname = dr.LocationName,
                                           city = dr.LocationCity,
                                           state = dr.LocationState,
                                           zip = dr.LocationZip,
                                           routeorder = dr.RouteOrder,
                                           username = us.FirstName + " " + us.LastName,
                                           latitude = dr.Latitude,
                                           longitude = dr.Longitude,

                                           createddate = dr.CreatedDate,
                                           //modifieddate = dr.ModifiedDate,
                                           //ismodified = dr.IsModified,
                                           api = dr.APINumber,
                                           rigname = dr.RigName,
                                           wellid = dr.WellId,
                                           rigid = dr.RigId,
                                           wellname = dr.WellName
                                       }
                                   ).ToList();

                        if (dispatchObj.Count > 0)
                        {

                        }
                        if (dispatchCurrentLocations.Count > 0)
                        {
                            foreach (var dispatchitem in dispatchCurrentLocations)
                            {
                                if (dispatchObj.Count > 0)
                                {
                                    List<DispatchRoutesModel> dispatchRoute = (from dp in dispatchObj
                                                                               where dp.userid == dispatchitem.userid
                                                                               select new DispatchRoutesModel
                                                                               {
                                                                                   userid = dp.userid
                                                                               }).ToList();

                                    if (dispatchRoute.Count > 0)
                                    {
                                        if (dispatchRoute[0].userid == "")
                                        {
                                            dispatchObj.Add(dispatchitem);
                                        }
                                    }
                                    else
                                    {
                                        dispatchObj.Add(dispatchitem);
                                    }
                                }
                                else
                                {
                                    dispatchObj.Add(dispatchitem);
                                }

                            }
                        }
                    }
                    else
                    {
                        dispatchObj = (from dr in db.DispatchRoutes
                                       join us in db.Users on dr.UserId equals us.Id
                                       select new DispatchRoutesModel
                                       {
                                           dispatchid = dr.DispatchId,
                                           dispatchnotes = "",//dr.DispatchNotes,
                                           userid = dr.UserId,
                                           customer = dr.Customer,
                                           address = dr.LocationAddress,
                                           locationname = dr.LocationName,
                                           city = dr.LocationCity,
                                           state = dr.LocationState,
                                           zip = dr.LocationZip,
                                           routeorder = dr.RouteOrder,
                                           username = us.FirstName + " " + us.LastName,
                                           latitude = dr.Latitude,
                                           longitude = dr.Longitude,

                                           createddate = dr.CreatedDate,
                                           //modifieddate = dr.ModifiedDate,
                                           //ismodified = dr.IsModified,
                                           api = dr.APINumber,
                                           rigname = dr.RigName,
                                           wellid = dr.WellId,
                                           rigid = dr.RigId,
                                           wellname = dr.WellName
                                       }
                                   ).ToList();

                    }

                }

                if (UserId != "" && UserId != null)
                {
                    dispatchObj = dispatchObj.OrderBy(x => x.currentrouterorder).ToList();
                }

                foreach (var item in dispatchObj)
                {
                    DispatchRoutesModel dispatchRouterModelObj = new DispatchRoutesModel();

                    dispatchRouterModelObj.dispatchid = item.dispatchid;
                    dispatchRouterModelObj.dispatchnotes = "";//item.dispatchnotes;
                    dispatchRouterModelObj.userid = item.userid;
                    dispatchRouterModelObj.customer = item.customer;
                    dispatchRouterModelObj.address = item.address;
                    dispatchRouterModelObj.locationname = item.locationname;
                    dispatchRouterModelObj.city = item.city;
                    dispatchRouterModelObj.state = item.state;
                    dispatchRouterModelObj.zip = item.zip;
                    dispatchRouterModelObj.routeorder = item.routeorder;
                    dispatchRouterModelObj.username = item.username;
                    dispatchRouterModelObj.latitude = item.latitude;
                    dispatchRouterModelObj.longitude = item.longitude;

                    dispatchRouterModelObj.createddate = item.createddate;
                    //dispatchRouterModelObj.modifieddate = item.modifieddate;
                    //dispatchRouterModelObj.ismodified = item.ismodified;
                    dispatchRouterModelObj.api = item.api ?? "";
                    dispatchRouterModelObj.rigname = item.rigname ?? "";
                    dispatchRouterModelObj.wellid = item.wellid ?? "";
                    dispatchRouterModelObj.rigid = item.rigid ?? "";
                    dispatchRouterModelObj.wellname = item.wellname ?? "";
                    dispatchRouterModelObj.rigwellandlocation = Convert.ToString(item.locationname ?? "") == "" ? Convert.ToString(item.rigname ?? "") + " " + Convert.ToString(item.wellname ?? "") : Convert.ToString(item.locationname);// : Convert.ToString(item.rigname ?? "") + " " + Convert.ToString(item.wellname ?? "") + " " + Convert.ToString(item.locationname ?? "");
                    result.Add(dispatchRouterModelObj);
                }

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetDispatchRoutes", null);
            }
            return result;
        }


        public async Task<List<DispatchRoutesModel>> GetDispatchRoutes_V2(string userId, bool isForApi, string tenantId)
        {
            List<DispatchRoutesModel> dispatchObj = new List<DispatchRoutesModel>();
            List<DispatchRoutesModel> result = new List<DispatchRoutesModel>();
            string subscriptionType = "";
            string subscriptionActiveStatus = "";
            try
            {
                var productSubscription = await GetProductSubscription(tenantId);
                var dispatchCurrentLocations = GetDispatchUsersCurrentLocation();
                var typeVal = productSubscription.SubscriptionType ?? -1;

                subscriptionType = typeVal == -1 ? "-1" : typeVal.ToString();
                subscriptionActiveStatus = productSubscription.IsEnable ? "Active" : "InActive";

                if (subscriptionType == "0" || subscriptionType == "1" || subscriptionType == "3" || subscriptionType == "4")//--0 - Operator, 1 - Service/Provider, 2 - Dispatch Only, 3 - Operator with Dispatch, 4 - Provider with Dispatch
                {
                    if (subscriptionActiveStatus == "Active")
                    {
                        subscriptionActiveStatus = "Active";
                    }
                    else
                    {
                        subscriptionActiveStatus = "InActive";
                    }
                }
                else
                {
                    subscriptionActiveStatus = "InActive";
                }

                if (userId != "" && userId != null)
                {
                    if (isForApi == false)
                    {
                        dispatchObj = (from dr in db.DispatchRoutes
                                       join us in db.Users on dr.UserId equals us.Id
                                       //   where dr.UserId == userId
                                       where us.TenantId == userId && dr.RouteStatus != "InActive" && dr.RouteOrder != -1
                                       orderby dr.CurrentRouterOrder
                                       select new DispatchRoutesModel
                                       {
                                           dispatchid = dr.DispatchId,
                                           //dispatchnotes = dr.DispatchNotes,
                                           userid = dr.UserId,
                                           customer = dr.Customer,
                                           address = dr.LocationAddress,
                                           locationname = dr.LocationName,
                                           city = dr.LocationCity,
                                           state = dr.LocationState,
                                           zip = dr.LocationZip,
                                           routeorder = dr.RouteOrder,
                                           username = us.FirstName + " " + us.LastName,
                                           latitude = dr.Latitude,
                                           longitude = dr.Longitude,
                                           createddate = dr.CreatedDate,
                                           //modifieddate = dr.ModifiedDate,
                                           //ismodified = dr.IsModified,
                                           api = dr.APINumber,
                                           rigname = dr.RigName,
                                           rigid = dr.RigId,
                                           wellname = dr.WellName,
                                           currentrouterorder = dr.CurrentRouterOrder,
                                           recordstatus = dr.RecordStatus,
                                           islocationshared = dr.IsLocationShared,
                                           activityid = dr.ActivityId,

                                       }
                                  ).ToList();
                    }
                    else
                    {
                        dispatchObj = (from dr in db.DispatchRoutes
                                       join us in db.Users on dr.UserId equals us.Id
                                       //  where dr.UserId == userId
                                       where us.TenantId == userId && dr.RouteStatus != "InActive" && dr.RouteOrder != -1
                                       orderby dr.CurrentRouterOrder
                                       select new DispatchRoutesModel
                                       {
                                           dispatchid = dr.DispatchId,
                                           dispatchnotes = dr.DispatchNotes,
                                           userid = dr.UserId,
                                           customer = dr.Customer,
                                           address = dr.LocationAddress,
                                           locationname = dr.LocationName,
                                           city = dr.LocationCity,
                                           state = dr.LocationState,
                                           zip = dr.LocationZip,
                                           routeorder = dr.RouteOrder,
                                           username = us.FirstName + " " + us.LastName,
                                           latitude = dr.Latitude,
                                           longitude = dr.Longitude,

                                           createddate = dr.CreatedDate,
                                           //modifieddate = dr.ModifiedDate,
                                           //ismodified = dr.IsModified,
                                           api = dr.APINumber,
                                           rigname = dr.RigName,
                                           wellid = dr.WellId,
                                           rigid = dr.RigId,
                                           wellname = dr.WellName,
                                           currentrouterorder = dr.CurrentRouterOrder,
                                           recordstatus = dr.RecordStatus,
                                           islocationshared = dr.IsLocationShared,
                                           activityid = dr.ActivityId
                                       }
                                     ).ToList();

                    }

                }
                else
                {
                    if (isForApi == false)
                    {
                        dispatchObj = (from dr in db.DispatchRoutes
                                       join us in db.Users on dr.UserId equals us.Id
                                       select new DispatchRoutesModel
                                       {
                                           dispatchid = dr.DispatchId,
                                           dispatchnotes = "",//dr.DispatchNotes,
                                           userid = dr.UserId,
                                           customer = dr.Customer,
                                           address = dr.LocationAddress,
                                           locationname = dr.LocationName,
                                           city = dr.LocationCity,
                                           state = dr.LocationState,
                                           zip = dr.LocationZip,
                                           routeorder = dr.RouteOrder,
                                           username = us.FirstName + " " + us.LastName,
                                           latitude = dr.Latitude,
                                           longitude = dr.Longitude,

                                           createddate = dr.CreatedDate,
                                           //modifieddate = dr.ModifiedDate,
                                           //ismodified = dr.IsModified,
                                           api = dr.APINumber,
                                           rigname = dr.RigName,
                                           wellid = dr.WellId,
                                           rigid = dr.RigId,
                                           wellname = dr.WellName,
                                           currentrouterorder = dr.CurrentRouterOrder,
                                           recordstatus = dr.RecordStatus,
                                           islocationshared = dr.IsLocationShared,
                                           activityid = dr.ActivityId
                                       }
                                   ).ToList();

                        if (dispatchObj.Count > 0)
                        {

                        }
                        if (dispatchCurrentLocations.Count > 0)
                        {
                            foreach (var dispatchitem in dispatchCurrentLocations)
                            {
                                if (dispatchObj.Count > 0)
                                {
                                    List<DispatchRoutesModel> dispatchRoute = (from dp in dispatchObj
                                                                               where dp.userid == dispatchitem.userid
                                                                               select new DispatchRoutesModel
                                                                               {
                                                                                   userid = dp.userid
                                                                               }).ToList();

                                    if (dispatchRoute.Count > 0)
                                    {
                                        if (dispatchRoute[0].userid == "")
                                        {
                                            dispatchObj.Add(dispatchitem);
                                        }
                                    }

                                }


                            }
                        }
                    }
                    else
                    {
                        dispatchObj = (from dr in db.DispatchRoutes
                                       join us in db.Users on dr.UserId equals us.Id
                                       select new DispatchRoutesModel
                                       {
                                           dispatchid = dr.DispatchId,
                                           dispatchnotes = "",//dr.DispatchNotes,
                                           userid = dr.UserId,
                                           customer = dr.Customer,
                                           address = dr.LocationAddress,
                                           locationname = dr.LocationName,
                                           city = dr.LocationCity,
                                           state = dr.LocationState,
                                           zip = dr.LocationZip,
                                           routeorder = dr.RouteOrder,
                                           username = us.FirstName + " " + us.LastName,
                                           latitude = dr.Latitude,
                                           longitude = dr.Longitude,

                                           createddate = dr.CreatedDate,
                                           //modifieddate = dr.ModifiedDate,
                                           //ismodified = dr.IsModified,
                                           api = dr.APINumber,
                                           rigname = dr.RigName,
                                           wellid = dr.WellId,
                                           rigid = dr.RigId,
                                           wellname = dr.WellName,
                                           currentrouterorder = dr.CurrentRouterOrder,
                                           recordstatus = dr.RecordStatus,
                                           activityid = dr.ActivityId,
                                           islocationshared = dr.IsLocationShared,
                                       }
                                   ).ToList();

                    }
                }

                if (userId != "" && userId != null)
                {
                    dispatchObj = dispatchObj.OrderBy(x => x.currentrouterorder).ToList();
                }

                var currentUser = "";
                string modificationStatus = "0";
                foreach (var item in dispatchObj)
                {
                    DispatchRoutesModel dispatchRouterModelObj = new DispatchRoutesModel();

                    //if(currentUser != item.username)
                    //{
                    //    currentUser = item.username;
                    //    modifificationStatus = false;
                    //}
                    //else
                    //{

                    var userResult = db.Users.Where(x => x.Id == item.userid).FirstOrDefault();
                    UsersOptions userOption = new UsersOptions();
                    if (userResult.UsersOptions != null)
                    {
                        userOption = JsonConvert.DeserializeObject<UsersOptions>(userResult.UsersOptions);
                        if (userOption.CurrentStatus == "1")
                        {
                            modificationStatus = "2";

                        }
                        else
                        {
                            string[] routeOrderArray = dispatchObj.Where(x => (x.routeorder != x.currentrouterorder || x.recordstatus == "0"
                      || x.recordstatus == "1") && x.currentrouterorder != null && x.dispatchid == x.dispatchid
                      && x.userid == item.userid).Select(x => x.dispatchid).ToArray();
                            //int[] currentRouteOrderArray = dispatchObj.Select(x => x.currentrouterorder).ToArray();
                            if (routeOrderArray.Length > 0)
                            {
                                modificationStatus = "1";
                            }
                            else
                            {
                                modificationStatus = "0";
                            }
                        }
                    }
                    else
                    {
                        string[] routeOrderArray = dispatchObj.Where(x => (x.routeorder != x.currentrouterorder || x.recordstatus == "0"
                  || x.recordstatus == "1") && x.dispatchid == x.dispatchid
                  && x.userid == item.userid).Select(x => x.dispatchid).ToArray();
                        //int[] currentRouteOrderArray = dispatchObj.Select(x => x.currentrouterorder).ToArray();
                        if (routeOrderArray.Length > 0)
                        {
                            modificationStatus = "1";
                        }
                        else
                        {
                            modificationStatus = "0";
                        }
                    }




                    // }
                    dispatchRouterModelObj.modificationStatus = modificationStatus;

                    dispatchRouterModelObj.dispatchid = item.dispatchid;
                    dispatchRouterModelObj.dispatchnotes = "";//item.dispatchnotes;
                    dispatchRouterModelObj.userid = item.userid;
                    dispatchRouterModelObj.customer = item.customer;
                    dispatchRouterModelObj.address = item.address;
                    dispatchRouterModelObj.locationname = item.locationname;
                    dispatchRouterModelObj.city = item.city;
                    dispatchRouterModelObj.state = item.state;
                    dispatchRouterModelObj.zip = item.zip;
                    dispatchRouterModelObj.routeorder = item.routeorder;
                    dispatchRouterModelObj.username = item.username + "_" + modificationStatus;
                    dispatchRouterModelObj.latitude = item.latitude;
                    dispatchRouterModelObj.longitude = item.longitude;

                    dispatchRouterModelObj.createddate = item.createddate;
                    //dispatchRouterModelObj.modifieddate = item.modifieddate;
                    //dispatchRouterModelObj.ismodified = item.ismodified;
                    dispatchRouterModelObj.api = item.api ?? "";
                    dispatchRouterModelObj.rigname = item.rigname ?? "";
                    dispatchRouterModelObj.wellid = item.wellid ?? "";
                    dispatchRouterModelObj.rigid = item.rigid ?? "";
                    dispatchRouterModelObj.wellname = item.wellname ?? "";

                    dispatchRouterModelObj.currentrouterorder = item.currentrouterorder;
                    dispatchRouterModelObj.recordstatus = item.recordstatus;

                    dispatchRouterModelObj.rigwellandlocation = Convert.ToString(item.locationname ?? "") == "" ? Convert.ToString(item.rigname ?? "") + " " + Convert.ToString(item.wellname ?? "") : Convert.ToString(item.locationname);// : Convert.ToString(item.rigname ?? "") + " " + Convert.ToString(item.wellname ?? "") + " " + Convert.ToString(item.locationname ?? "");

                    dispatchRouterModelObj.subscriptionstatus = subscriptionActiveStatus;
                    dispatchRouterModelObj.islocationshared = Convert.ToBoolean(item.islocationshared ?? false);
                    dispatchRouterModelObj.activityid = item.activityid ?? 0;

                    result.Add(dispatchRouterModelObj);
                }

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetDispatchRoutes V2", null);
            }
            return result;
        }

        public async Task<List<DispatchRoutesModel>> GetDispatchRoutes_V2_old(string userId, bool isForApi, string tenantId)
        {
            List<DispatchRoutesModel> dispatchObj = new List<DispatchRoutesModel>();
            List<DispatchRoutesModel> result = new List<DispatchRoutesModel>();
            string subscriptionType = "";
            string subscriptionActiveStatus = "";
            try
            {
                var productSubscription = await GetProductSubscription(tenantId);
                var dispatchCurrentLocations = GetDispatchUsersCurrentLocation();
                var typeVal = productSubscription.SubscriptionType ?? -1;

                subscriptionType = typeVal == -1 ? "-1" : typeVal.ToString();
                subscriptionActiveStatus = productSubscription.IsEnable ? "Active" : "InActive";

                if (subscriptionType == "0" || subscriptionType == "1" || subscriptionType == "3" || subscriptionType == "4")//--0 - Operator, 1 - Service/Provider, 2 - Dispatch Only, 3 - Operator with Dispatch, 4 - Provider with Dispatch
                {
                    if (subscriptionActiveStatus == "Active")
                    {
                        subscriptionActiveStatus = "Active";
                    }
                    else
                    {
                        subscriptionActiveStatus = "InActive";
                    }
                }
                else
                {
                    subscriptionActiveStatus = "InActive";
                }

                if (userId != "" && userId != null)
                {
                    if (isForApi == false)
                    {
                        dispatchObj = (from dr in db.DispatchRoutes
                                       join us in db.Users on dr.UserId equals us.Id
                                       where dr.UserId == userId
                                       orderby dr.CurrentRouterOrder
                                       select new DispatchRoutesModel
                                       {
                                           dispatchid = dr.DispatchId,
                                           //dispatchnotes = dr.DispatchNotes,
                                           userid = dr.UserId,
                                           customer = dr.Customer,
                                           address = dr.LocationAddress,
                                           locationname = dr.LocationName,
                                           city = dr.LocationCity,
                                           state = dr.LocationState,
                                           zip = dr.LocationZip,
                                           routeorder = dr.RouteOrder,
                                           username = us.FirstName + " " + us.LastName,
                                           latitude = dr.Latitude,
                                           longitude = dr.Longitude,
                                           createddate = dr.CreatedDate,
                                           //modifieddate = dr.ModifiedDate,
                                           //ismodified = dr.IsModified,
                                           api = dr.APINumber,
                                           rigname = dr.RigName,
                                           rigid = dr.RigId,
                                           wellname = dr.WellName,
                                           currentrouterorder = dr.CurrentRouterOrder,
                                           recordstatus = dr.RecordStatus,
                                           islocationshared = dr.IsLocationShared,
                                           activityid= dr.ActivityId,
                                      
                                       }
                                  ).ToList();
                    }
                    else
                    {
                        dispatchObj = (from dr in db.DispatchRoutes
                                       join us in db.Users on dr.UserId equals us.Id
                                       where dr.UserId == userId
                                       orderby dr.CurrentRouterOrder
                                       select new DispatchRoutesModel
                                       {
                                           dispatchid = dr.DispatchId,
                                           dispatchnotes = dr.DispatchNotes,
                                           userid = dr.UserId,
                                           customer = dr.Customer,
                                           address = dr.LocationAddress,
                                           locationname = dr.LocationName,
                                           city = dr.LocationCity,
                                           state = dr.LocationState,
                                           zip = dr.LocationZip,
                                           routeorder = dr.RouteOrder,
                                           username = us.FirstName + " " + us.LastName,
                                           latitude = dr.Latitude,
                                           longitude = dr.Longitude,

                                           createddate = dr.CreatedDate,
                                           //modifieddate = dr.ModifiedDate,
                                           //ismodified = dr.IsModified,
                                           api = dr.APINumber,
                                           rigname = dr.RigName,
                                           wellid = dr.WellId,
                                           rigid = dr.RigId,
                                           wellname = dr.WellName,
                                           currentrouterorder = dr.CurrentRouterOrder,
                                           recordstatus = dr.RecordStatus,
                                           islocationshared = dr.IsLocationShared,
                                           activityid = dr.ActivityId
                                       }
                                     ).ToList();

                    }

                }
                else
                {
                    if (isForApi == false)
                    {
                        dispatchObj = (from dr in db.DispatchRoutes
                                       join us in db.Users on dr.UserId equals us.Id
                                       select new DispatchRoutesModel
                                       {
                                           dispatchid = dr.DispatchId,
                                           dispatchnotes = "",//dr.DispatchNotes,
                                           userid = dr.UserId,
                                           customer = dr.Customer,
                                           address = dr.LocationAddress,
                                           locationname = dr.LocationName,
                                           city = dr.LocationCity,
                                           state = dr.LocationState,
                                           zip = dr.LocationZip,
                                           routeorder = dr.RouteOrder,
                                           username = us.FirstName + " " + us.LastName,
                                           latitude = dr.Latitude,
                                           longitude = dr.Longitude,

                                           createddate = dr.CreatedDate,
                                           //modifieddate = dr.ModifiedDate,
                                           //ismodified = dr.IsModified,
                                           api = dr.APINumber,
                                           rigname = dr.RigName,
                                           wellid = dr.WellId,
                                           rigid = dr.RigId,
                                           wellname = dr.WellName,
                                           currentrouterorder = dr.CurrentRouterOrder,
                                           recordstatus = dr.RecordStatus,
                                           islocationshared = dr.IsLocationShared,
                                           activityid = dr.ActivityId
                                       }
                                   ).ToList();

                        if (dispatchObj.Count > 0)
                        {

                        }
                        if (dispatchCurrentLocations.Count > 0)
                        {
                            foreach (var dispatchitem in dispatchCurrentLocations)
                            {
                                if (dispatchObj.Count > 0)
                                {
                                    List<DispatchRoutesModel> dispatchRoute = (from dp in dispatchObj
                                                                               where dp.userid == dispatchitem.userid
                                                                               select new DispatchRoutesModel
                                                                               {
                                                                                   userid = dp.userid
                                                                               }).ToList();

                                    if (dispatchRoute.Count > 0)
                                    {
                                        if (dispatchRoute[0].userid == "")
                                        {
                                            dispatchObj.Add(dispatchitem);
                                        }
                                    }
                                   
                                }
                               

                            }
                        }
                    }
                    else
                    {
                        dispatchObj = (from dr in db.DispatchRoutes
                                       join us in db.Users on dr.UserId equals us.Id
                                       select new DispatchRoutesModel
                                       {
                                           dispatchid = dr.DispatchId,
                                           dispatchnotes = "",//dr.DispatchNotes,
                                           userid = dr.UserId,
                                           customer = dr.Customer,
                                           address = dr.LocationAddress,
                                           locationname = dr.LocationName,
                                           city = dr.LocationCity,
                                           state = dr.LocationState,
                                           zip = dr.LocationZip,
                                           routeorder = dr.RouteOrder,
                                           username = us.FirstName + " " + us.LastName,
                                           latitude = dr.Latitude,
                                           longitude = dr.Longitude,

                                           createddate = dr.CreatedDate,
                                           //modifieddate = dr.ModifiedDate,
                                           //ismodified = dr.IsModified,
                                           api = dr.APINumber,
                                           rigname = dr.RigName,
                                           wellid = dr.WellId,
                                           rigid = dr.RigId,
                                           wellname = dr.WellName,
                                           currentrouterorder = dr.CurrentRouterOrder,
                                           recordstatus = dr.RecordStatus,
                                           activityid = dr.ActivityId,
                                           islocationshared = dr.IsLocationShared,
                                       }
                                   ).ToList();

                    }
                }

                if (userId != "" && userId != null)
                {
                    dispatchObj = dispatchObj.OrderBy(x => x.currentrouterorder).ToList();
                }

                var currentUser = "";
                string modificationStatus = "0";
                foreach (var item in dispatchObj)
                {
                    DispatchRoutesModel dispatchRouterModelObj = new DispatchRoutesModel();

                    //if(currentUser != item.username)
                    //{
                    //    currentUser = item.username;
                    //    modifificationStatus = false;
                    //}
                    //else
                    //{

                    var userResult = db.Users.Where(x => x.Id == item.userid).FirstOrDefault();
                    UsersOptions userOption = new UsersOptions();
                    if (userResult.UsersOptions != null)
                    {
                        userOption = JsonConvert.DeserializeObject<UsersOptions>(userResult.UsersOptions);
                        if (userOption.CurrentStatus == "1")
                        {
                            modificationStatus = "2";

                        }
                        else
                        {
                            string[] routeOrderArray = dispatchObj.Where(x => (x.routeorder != x.currentrouterorder || x.recordstatus == "0"
                      || x.recordstatus == "1") && x.currentrouterorder != null && x.dispatchid == x.dispatchid
                      && x.userid == item.userid).Select(x => x.dispatchid).ToArray();
                            //int[] currentRouteOrderArray = dispatchObj.Select(x => x.currentrouterorder).ToArray();
                            if (routeOrderArray.Length > 0)
                            {
                                modificationStatus = "1";
                            }
                            else
                            {
                                modificationStatus = "0";
                            }
                        }
                    }
                    else
                    {
                        string[] routeOrderArray = dispatchObj.Where(x => (x.routeorder != x.currentrouterorder || x.recordstatus == "0"
                  || x.recordstatus == "1") && x.dispatchid == x.dispatchid
                  && x.userid == item.userid).Select(x => x.dispatchid).ToArray();
                        //int[] currentRouteOrderArray = dispatchObj.Select(x => x.currentrouterorder).ToArray();
                        if (routeOrderArray.Length > 0)
                        {
                            modificationStatus = "1";
                        }
                        else
                        {
                            modificationStatus = "0";
                        }
                    }




                    // }
                    dispatchRouterModelObj.modificationStatus = modificationStatus;

                    dispatchRouterModelObj.dispatchid = item.dispatchid;
                    dispatchRouterModelObj.dispatchnotes = "";//item.dispatchnotes;
                    dispatchRouterModelObj.userid = item.userid;
                    dispatchRouterModelObj.customer = item.customer;
                    dispatchRouterModelObj.address = item.address;
                    dispatchRouterModelObj.locationname = item.locationname;
                    dispatchRouterModelObj.city = item.city;
                    dispatchRouterModelObj.state = item.state;
                    dispatchRouterModelObj.zip = item.zip;
                    dispatchRouterModelObj.routeorder = item.routeorder;
                    dispatchRouterModelObj.username = item.username + "_" + modificationStatus;
                    dispatchRouterModelObj.latitude = item.latitude;
                    dispatchRouterModelObj.longitude = item.longitude;

                    dispatchRouterModelObj.createddate = item.createddate;
                    //dispatchRouterModelObj.modifieddate = item.modifieddate;
                    //dispatchRouterModelObj.ismodified = item.ismodified;
                    dispatchRouterModelObj.api = item.api ?? "";
                    dispatchRouterModelObj.rigname = item.rigname ?? "";
                    dispatchRouterModelObj.wellid = item.wellid ?? "";
                    dispatchRouterModelObj.rigid = item.rigid ?? "";
                    dispatchRouterModelObj.wellname = item.wellname ?? "";

                    dispatchRouterModelObj.currentrouterorder = item.currentrouterorder;
                    dispatchRouterModelObj.recordstatus = item.recordstatus;

                    dispatchRouterModelObj.rigwellandlocation = Convert.ToString(item.locationname ?? "") == "" ? Convert.ToString(item.rigname ?? "") + " " + Convert.ToString(item.wellname ?? "") : Convert.ToString(item.locationname);// : Convert.ToString(item.rigname ?? "") + " " + Convert.ToString(item.wellname ?? "") + " " + Convert.ToString(item.locationname ?? "");

                    dispatchRouterModelObj.subscriptionstatus = subscriptionActiveStatus;
                    dispatchRouterModelObj.islocationshared = Convert.ToBoolean(item.islocationshared ?? false);
                    dispatchRouterModelObj.activityid = item.activityid ?? 0;
             
                    result.Add(dispatchRouterModelObj);
                }

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetDispatchRoutes V2", null);
            }
            return result;
        }


        public async Task<WellIdentityUser> GetUserById(string userId)
        {
            try
            {
                //var result = await _userManager.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
                //return result;// await _userManager.Users.Where(x => x.Email == email).FirstOrDefault();
                return await _userManager.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetUserById", null);
                return null;
            }
        }

        //Dispatch  save
        public bool CreateDispatchRoute(DispatchRoutes  dispatch)
        {
            try
            {
                var result = db.DispatchRoutes.Where(x => x.DispatchId == dispatch.DispatchId).FirstOrDefault();

                var lastDispatchRoute = db.DispatchRoutes.Where(x => x.UserId == dispatch.UserId).OrderByDescending(x => x.RouteOrder).FirstOrDefault();

                //var rept_max = (from c in nameentity.name
                //                where c.name == "John"
                //                select c).Max(c => c.amount);
                //var currentUserDispatch = db.DispatchRoutes.Where(x => x.UserId == dispatch.UserId).ToList();
                //var routeOrder = currentUserDispatch.Where(p => (int)p.RouteOrder == currentUserDispatch.Max(r => (int) r.RouteOrder)).FirstOrDefault();

                if (lastDispatchRoute != null)
                {
                    dispatch.RouteOrder = lastDispatchRoute.RouteOrder + 1;
                }
                else
                {
                    dispatch.RouteOrder = 1;
                }

                if (result == null)
                {
                    dispatch.DispatchId = Guid.NewGuid().ToString("D");
                    db.DispatchRoutes.Add(dispatch);
                    db.SaveChanges();
                }
                else
                {
                    result.LocationAddress = dispatch.LocationAddress;
                    result.LocationName = dispatch.LocationName;
                    result.Customer = dispatch.Customer;
                    result.LocationCity = dispatch.LocationCity;
                    result.LocationState = dispatch.LocationState;
                    result.LocationZip = dispatch.LocationZip;
                    result.Latitude = dispatch.Latitude;
                    result.Longitude = dispatch.Longitude;
                    ////result.DispatchNotes = dispatch.DispatchNotes;
                    result.RouteOrder = dispatch.RouteOrder;
                    result.UserId = dispatch.UserId;
                    result.CreatedDate = dispatch.CreatedDate;
                    result.APINumber = dispatch.APINumber;
                    result.WellName = dispatch.WellName;
                    result.RigName = dispatch.RigName;
                    result.WellId = dispatch.WellId;
                    result.RigId = dispatch.RigId;
                    db.SaveChanges();
                }

                var userResult = db.Users.Where(x => x.Id == dispatch.UserId).FirstOrDefault();
                UsersOptions userOption = new UsersOptions();
                userOption = JsonConvert.DeserializeObject<UsersOptions>(userResult.UsersOptions);
                if (dispatch.DispatchNotes != null)
                {
                    userOption.DispatchNotes = dispatch.DispatchNotes;
                }
                
                userResult.UsersOptions = JsonConvert.SerializeObject(userOption); //JsonConvert.SerializeObject<UsersOptions>(userOption);
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository CreateCompanyDetail", null);
                return false;
            }
        }


        public bool CreateDispatchRoute_RefreshRout(DispatchRoutes dispatch)
        {
            try
            {
                //var result = db.DispatchRoutes.Where(x => x.UserId == dispatch.UserId && x.RouteOrder == dispatch.RouteOrder).FirstOrDefault();
                // var result = db.DispatchRoutes.Where(x => x.UserId == dispatch.UserId && x.LocationName == dispatch.LocationName 
                // && x.Latitude == dispatch.Latitude && x.Longitude == dispatch.Longitude).FirstOrDefault();
                var result = db.DispatchRoutes.Where(x => x.UserId == dispatch.UserId && x.ActivityId == dispatch.ActivityId && x.ScheduledArrival != null).FirstOrDefault();

                var lastDispatchRoute = db.DispatchRoutes.Where(x => x.UserId == dispatch.UserId).OrderByDescending(x => x.RouteOrder).FirstOrDefault();

                if (lastDispatchRoute != null)
                {
                    // dispatch.RouteOrder = lastDispatchRoute.RouteOrder + 1;
                }
                else
                {
                    dispatch.RouteOrder = 1;
                }

                if (result == null)
                {
                    //dispatch.CurrentRouterOrder = 0;
                    dispatch.DispatchId = Guid.NewGuid().ToString("D");
                    db.DispatchRoutes.Add(dispatch);
                    db.SaveChanges();
                }
                else
                {
                    result.LocationAddress = dispatch.LocationAddress;
                    result.LocationName = dispatch.LocationName;
                    result.Customer = dispatch.Customer;
                    result.LocationCity = dispatch.LocationCity;
                    result.LocationState = dispatch.LocationState;
                    result.LocationZip = dispatch.LocationZip;
                    result.Latitude = dispatch.Latitude;
                    result.Longitude = dispatch.Longitude;
                    ////result.DispatchNotes = dispatch.DispatchNotes;
                    // result.RouteOrder = dispatch.RouteOrder;
                    result.UserId = dispatch.UserId;
                    result.CreatedDate = dispatch.CreatedDate;
                    result.APINumber = dispatch.APINumber;
                    result.WellName = dispatch.WellName;
                    result.RigName = dispatch.RigName;
                    result.WellId = dispatch.WellId;
                    result.RigId = dispatch.RigId;
                    // result.CurrentRouterOrder = 0;
                    result.ETA = dispatch.ETA;
                    result.RouteStatus = dispatch.RouteStatus;
                    result.CurrentRouterOrder = dispatch.CurrentRouterOrder;

                    db.SaveChanges();
                }

                var userResult = db.Users.Where(x => x.Id == dispatch.UserId).FirstOrDefault();
                UsersOptions userOption = new UsersOptions();
                userOption = JsonConvert.DeserializeObject<UsersOptions>(userResult.UsersOptions);
                if (dispatch.DispatchNotes != null)
                {
                    userOption.DispatchNotes = dispatch.DispatchNotes;
                }

                userResult.UsersOptions = JsonConvert.SerializeObject(userOption); //JsonConvert.SerializeObject<UsersOptions>(userOption);
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository CreateCompanyDetail", null);
                return false;
            }
        }

        public bool CreateDispatchRoute_RefreshRout_Update(DispatchRoutes dispatch)
        {
            try
            {
                //var result = db.DispatchRoutes.Where(x => x.UserId == dispatch.UserId && x.RouteOrder == dispatch.RouteOrder).FirstOrDefault();
                // var result = db.DispatchRoutes.Where(x => x.UserId == dispatch.UserId && x.LocationName == dispatch.LocationName 
                // && x.Latitude == dispatch.Latitude && x.Longitude == dispatch.Longitude).FirstOrDefault();
                var result = db.DispatchRoutes.Where(x => x.UserId == dispatch.UserId && x.ActivityId == dispatch.ActivityId).FirstOrDefault();

                //var lastDispatchRoute = db.DispatchRoutes.Where(x => x.UserId == dispatch.UserId).OrderByDescending(x => x.RouteOrder).FirstOrDefault();

                //if (lastDispatchRoute != null)
                //{
                //    // dispatch.RouteOrder = lastDispatchRoute.RouteOrder + 1;
                //}
                //else
                //{
                //    dispatch.RouteOrder = 1;
                //}

                //if (result == null)
                //{
                //    //dispatch.CurrentRouterOrder = 0;
                //    dispatch.DispatchId = Guid.NewGuid().ToString("D");
                //    db.DispatchRoutes.Add(dispatch);
                //    db.SaveChanges();
                //}
                //else
                //{
                result.LocationAddress = dispatch.LocationAddress;
                result.LocationName = dispatch.LocationName;
                result.Customer = dispatch.Customer;
                result.LocationCity = dispatch.LocationCity;
                result.LocationState = dispatch.LocationState;
                result.LocationZip = dispatch.LocationZip;
                result.Latitude = dispatch.Latitude;
                result.Longitude = dispatch.Longitude;
                ////result.DispatchNotes = dispatch.DispatchNotes;
                // result.RouteOrder = dispatch.RouteOrder;
                result.UserId = dispatch.UserId;
                result.CreatedDate = dispatch.CreatedDate;
                result.APINumber = dispatch.APINumber;
                result.WellName = dispatch.WellName;
                result.RigName = dispatch.RigName;
                result.WellId = dispatch.WellId;
                result.RigId = dispatch.RigId;
                // result.CurrentRouterOrder = 0;
                result.ETA = dispatch.ETA;
                result.RouteStatus = dispatch.RouteStatus;
                result.CurrentRouterOrder = dispatch.CurrentRouterOrder;

                db.SaveChanges();
                // }

                var userResult = db.Users.Where(x => x.Id == dispatch.UserId).FirstOrDefault();
                UsersOptions userOption = new UsersOptions();
                userOption = JsonConvert.DeserializeObject<UsersOptions>(userResult.UsersOptions);
                if (dispatch.DispatchNotes != null)
                {
                    userOption.DispatchNotes = dispatch.DispatchNotes;
                }

                userResult.UsersOptions = JsonConvert.SerializeObject(userOption); //JsonConvert.SerializeObject<UsersOptions>(userOption);
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository CreateCompanyDetail", null);
                return false;
            }
        }


        public async Task<bool> UpdateOperatorId(List<DispatchRoutes> dispatchRoutes)
        {
            try
            {

                foreach (var route in dispatchRoutes)
                {
                   
                        var result = db.DispatchRoutes.Where(x => x.ActivityId == route.ActivityId).AsNoTracking().FirstOrDefault();
                      result.OperatorId = route.OperatorId;
                    result.IsLocationShared = route.IsLocationShared;
                    //db.DispatchRoutes.AsNoTracking();
                    db.DispatchRoutes.Update(result);
                    
                    
                }
              await  db.SaveChangesAsync();


                return true;               

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository UpdateOperaorId", null);
                return false;
            }
        }

        public async Task<bool> UpdateRouterStatus(DispatchRoutes dispatchRoutes)
        {
            try
            {
                             
                var result1 = db.DispatchRoutes.Where(x => x.UserId == dispatchRoutes.UserId).AsNoTracking().ToList();
                if(result1 != null)
                {
                    foreach(var result in result1)
                    {
                        result.RouteStatus = dispatchRoutes.RouteStatus;
                        result.ScheduledArrival = dispatchRoutes.ScheduledArrival;
                        //db.DispatchRoutes.AsNoTracking();
                        db.DispatchRoutes.Update(result);
                    }
                   
                    await db.SaveChangesAsync();
                }
                else
                {

                    //dispatchRoutes.DispatchId = Guid.NewGuid().ToString("D");
                    //dispatchRoutes.CreatedDate = DateTime.Now;
                    //dispatchRoutes.RouteOrder = -1;
                    //string name = db.CrmUserBasicDetail.Where(x => x.UserId == (dispatchRoutes.UserId)).Select(x => x.Name).FirstOrDefault();                    
                    //dispatchRoutes.Customer = name;
                    //db.DispatchRoutes.Add(dispatchRoutes);

                    //await db.SaveChangesAsync();
                }

                var userResult = db.Users.Where(x => x.Id == dispatchRoutes.UserId).FirstOrDefault();
                if (userResult != null)
                {
                    userResult.UserScheduledArrival = dispatchRoutes.ScheduledArrival;
                    userResult.UserETA = dispatchRoutes.ETA;
                    userResult.UserRouteStatus = dispatchRoutes.RouteStatus;
                    userResult.UserLocation = dispatchRoutes.LocationName;
                    await db.SaveChangesAsync();
                }
                

                return true;

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository CreateCompanyDetail", null);
                return false;
            }
        }

        public async Task<List<DispatchRoutesHistoryDetailsModel>> CreateDispatchRoute_V2(List<DispatchRoutes> dispatchRoutes, List<DispatchRoutesHistoryDetailsModel> dispatchRoutes2)
        {
            string DispatchId_maintable = "";
            try
            {
                //foreach (var route in dispatchRoutes)
                //{
                var loop = 0;
                foreach (var route in dispatchRoutes)
                {
                    if (route.DispatchId == null)
                    {
                        route.DispatchId = Guid.NewGuid().ToString("D");
                        dispatchRoutes2[loop].dispatchid = route.DispatchId;
                        db.DispatchRoutes.Add(route);
                    }
                    else
                    {
                        var result = db.DispatchRoutes.Where(x => x.DispatchId == route.DispatchId).AsNoTracking().FirstOrDefault();
                        result.RouteOrder = result.RouteOrder;
                        result.DispatchNotes = route.DispatchNotes;
                        dispatchRoutes2[loop].dispatchid = route.DispatchId;
                        //db.DispatchRoutes.AsNoTracking();
                        db.DispatchRoutes.Update(result);
                    }
                    loop = loop + 1;

                    string notes = route.DispatchNotes;

                    var userResult = db.Users.Where(x => x.Id == route.UserId).FirstOrDefault();
                    UsersOptions userOption = new UsersOptions();
                    if (userResult.UsersOptions != null)
                    {
                        userOption = JsonConvert.DeserializeObject<UsersOptions>(userResult.UsersOptions);
                        if (notes != "")
                        {
                            userOption.DispatchNotes = notes;
                            
                            //userOption.IsDispatchUser = true;
                            //userOption.CurrentStatus = "0";//0-Empty,1-Destinations Changing,2-Waiting For Approval (if Destinations sent to Driver)
                        }
                        userResult.UserRouteStatus = "ON-ROUTE";
                        userResult.UsersOptions = JsonConvert.SerializeObject(userOption);
                        await db.SaveChangesAsync();
                    }

                }
                await db.SaveChangesAsync();

                ////DispatchRoutes dispatch = new DispatchRoutes();

                ////foreach(var route in dispatchRoutes)
                ////{
                ////    dispatch.LocationAddress = route.LocationAddress;
                ////    dispatch.LocationName = route.LocationName;
                ////    dispatch.LocationCity = route.LocationCity;
                ////    dispatch.LocationState = route.LocationState;
                ////    dispatch.LocationZip = route.LocationZip;
                ////    dispatch.Latitude = route.Latitude;
                ////    dispatch.Longitude = route.Longitude;
                ////    dispatch.DispatchNotes = route.DispatchNotes;
                ////    dispatch.RouteOrder = route.RouteOrder;
                ////    dispatch.DispatchId = route.DispatchId;
                ////    dispatch.UserId = route.UserId;
                ////    dispatch.CreatedDate = route.CreatedDate;
                ////    dispatch.Customer = route.Customer;
                ////    dispatch.APINumber = route.APINumber;
                ////    dispatch.WellName = route.WellName;
                ////    dispatch.RigName = route.RigName;
                ////    dispatch.WellId = route.WellId;
                ////    dispatch.RigId = route.RigId;
                ////    dispatch.ModifiedDate = route.ModifiedDate;
                ////    //dispatch.CurrentRouterOrder = route.CurrentRouterOrder;
                ////    dispatch.RecordStatus = route.RecordStatus;

                ////}



                //var result = db.DispatchRoutes.Where(x => x.DispatchId == dispatch.DispatchId).FirstOrDefault();

                //string DispatchId = "";
                //if (result == null)
                //{

                //    dispatch.DispatchId = Guid.NewGuid().ToString("D");
                //    DispatchId = dispatch.DispatchId;
                //    db.DispatchRoutes.AddRange(dispatchRoutes);
                //   // db.SaveChanges();

                //    //-----------DispatchRoutesHistoryDetails table 
                //    //dispatchRoutes.DispatchId = DispatchId;
                //    //dispatchRoutes.RouteOrder = dispatch.RouteOrder;
                //    //db.DispatchRoutesHistoryDetails.Add(dispatchhistory);
                //    db.SaveChanges();
                //}
                //else
                //{
                //    DispatchId = dispatch.DispatchId;
                //    result.ModifiedDate = dispatch.ModifiedDate;
                //    result.CurrentRouterOrder = dispatch.CurrentRouterOrder;                   
                //    //db.SaveChanges();


                //    //-----------DispatchRoutesHistoryDetails table 
                //    //dispatchhistory.DispatchId = DispatchId;
                //    //dispatchhistory.RouteOrder = result.RouteOrder;
                //   // db.DispatchRoutesHistoryDetails.Add(dispatchhistory);

                //    db.SaveChanges();
                //}



            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository CreateCompanyDetail", null);
                //  return "false";
            }
            return dispatchRoutes2;
        }

        //public bool CreateDispatchRoutesHistory_V2(DispatchRoutesHistoryHead historyhead)
        //{
        //    try
        //    {
        //            db.DispatchRoutesHistoryHead.Add(historyhead);
        //            db.SaveChanges();

        //      //  var result = db.DispatchRoutesHistoryHead.Where(x => x.HistoryId == historyhead.HistoryId).FirstOrDefault();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
        //        customErrorHandler.WriteError(ex, "CommonRepository CreateCompanyDetail", null);
        //        return false;
        //    }
        //}
        public async Task<List<DispatchRoutesHistoryDetailsModel>> DeleteDispatchRoute_V2(List<DispatchRoutes> dispatchRoutes, List<DispatchRoutesHistoryDetailsModel> dispatchRoutes2)
        {
            try
            {
                var loop = 0;
                foreach (var route in dispatchRoutes)
                {
                    var result = db.DispatchRoutes.Where(x => x.DispatchId == route.DispatchId).AsNoTracking().FirstOrDefault();
                    if(result!=null)
                    {
                        if (result.RouteOrder == 0)
                        {
                            db.DispatchRoutes.Remove(result);
                            dispatchRoutes2[loop].dispatchid = route.DispatchId;
                            //route.DispatchId = Guid.NewGuid().ToString("D");
                            //db.DispatchRoutes.Add(route);
                            //    db.DispatchRoutes.Remove(route);
                        }
                        else
                        {
                            result.RouteOrder = 0;
                            result.RecordStatus = "0";
                            result.CurrentRouterOrder = 0;
                            //db.DispatchRoutes.AsNoTracking();
                            db.DispatchRoutes.Update(result);
                        }
                        loop = loop + 1;
                    }
                   
                }
                await db.SaveChangesAsync();
                //    DispatchRoutes dispatch = new DispatchRoutes();


                //    dispatch.LocationAddress = dispatchhistory.LocationAddress;
                //    dispatch.LocationName = dispatchhistory.LocationName;
                //    dispatch.LocationCity = dispatchhistory.LocationCity;
                //    dispatch.LocationState = dispatchhistory.LocationState;
                //    dispatch.LocationZip = dispatchhistory.LocationZip;
                //    dispatch.Latitude = dispatchhistory.Latitude;
                //    dispatch.Longitude = dispatchhistory.Longitude;
                //    dispatch.DispatchNotes = dispatchhistory.DispatchNotes;
                //    dispatch.RouteOrder = dispatchhistory.RouteOrder;
                //    dispatch.DispatchId = dispatchhistory.DispatchId;
                //    dispatch.UserId = dispatchhistory.UserId;
                //    dispatch.CreatedDate = dispatchhistory.CreatedDate;
                //    dispatch.Customer = dispatchhistory.Customer;
                //    dispatch.APINumber = dispatchhistory.APINumber;
                //    dispatch.WellName = dispatchhistory.WellName;
                //    dispatch.RigName = dispatchhistory.RigName;
                //    dispatch.WellId = dispatchhistory.WellId;
                //    dispatch.RigId = dispatchhistory.RigId;
                //    dispatch.ModifiedDate = dispatchhistory.ModifiedDate;
                //    dispatch.CurrentRouterOrder = dispatchhistory.CurrentRouterOrder;
                //    dispatch.RecordStatus = dispatchhistory.RecordStatus;



                //    var count = db.DispatchRoutes.Where(x => x.UserId == dispatch.UserId).Count();
                //    var result = db.DispatchRoutes.Where(x => x.DispatchId == dispatch.DispatchId).FirstOrDefault();            

                //    int CurrentRouterOrder = 0;
                //    if (result == null)
                //    {

                //    }
                //    else
                //    {
                //      if(result.RouteOrder==0)
                //        {
                //            result.ModifiedDate = dispatch.ModifiedDate;
                //            result.CurrentRouterOrder = 0;
                //            result.RecordStatus = "0";

                //            //---------DispatchRoutesHistoryDetails
                //            dispatchhistory.RouteOrder = result.RouteOrder;
                //            dispatchhistory.CurrentRouterOrder = CurrentRouterOrder;

                //            db.DispatchRoutes.Remove(result);

                //            db.DispatchRoutesHistoryDetails.Add(dispatchhistory);

                //            db.SaveChanges();
                //        }
                //        else
                //        {
                //            result.ModifiedDate = dispatch.ModifiedDate;
                //          //  result.CurrentRouterOrder = (Convert.ToInt32(count) - dispatch.CurrentRouterOrder);
                //            result.CurrentRouterOrder =0;
                //            result.RecordStatus ="0";
                //            CurrentRouterOrder = Convert.ToInt32((Convert.ToInt32(count) - dispatch.CurrentRouterOrder));

                //            //db.SaveChanges();


                //            //---------DispatchRoutesHistoryDetails
                //            dispatchhistory.RouteOrder = result.RouteOrder;
                //            dispatchhistory.CurrentRouterOrder = 0;
                //            //dispatchhistory.CurrentRouterOrder = CurrentRouterOrder;
                //            db.DispatchRoutesHistoryDetails.Add(dispatchhistory);

                //            db.SaveChanges();
                //        }


                //    }
             

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository CreateCompanyDetail", null);
               // return false;
            }

            return dispatchRoutes2;
        }

        public List<WellIdentityUser> GetComponentPermissionUsers(string componentName, string tenantId)
        {
            List<WellIdentityUser> componentPermissionUsers = new List<WellIdentityUser>();

            try
            {
                var componentId = 0;
                componentId = db.Components.Where(c => c.ComponentName == componentName).Select(c => c.ComponentId).FirstOrDefault();

                if (componentId == null)
                {
                    componentId = 0;
                }

                IList<string> userRoleNames = null;

                //WellIdentityUser user = new WellIdentityUser();
                //user = GetUserDetail(userId);

                //userRoleNames = _userManager.GetRolesAsync(user).Result;

                var tenantRoles = (from r in _roleManager.Roles
                                   join tr in db.TenantRoles on r.Id equals tr.RoleId
                                   where tr.TenantId == tenantId
                                   select r).ToList();

                UserViewModel userViewModel = new UserViewModel();
                userViewModel.roles = new List<IdentityRole>();
                userViewModel.SelectedRoles = "";

                List<string> userRoles = new List<string>();
                foreach (var tenantRole in tenantRoles)
                {
                    //if (userRoleNames != null && userRoleNames.Contains(tenantRole.Name))
                    //{
                        userRoles.Add(tenantRole.Id);
                    //}
                }
                    
                //componentPermissionUsers = (from CL in db.RolePermissionComponentLinks
                //                           join CM in db.Components on CL.ComponentId equals CM.ComponentId
                //                           join RP in db.RolePermissionLinks on CL.RolePermissionId equals RP.RolePermissionId
                //                           join UR in db.UserRoles on RP.RoleId equals UR.RoleId
                //                           join US in db.Users on UR.UserId equals US.Id
                //                           where CM.ComponentId == componentId && CL.IsPermitted == true && RP.IsPermitted == true
                //                           && userRoles.Contains(RP.RoleId)
                //                           select new WellIdentityUser
                //                           {
                //                               FirstName = US.FirstName,
                //                               MiddleName = US.MiddleName,
                //                               LastName = US.LastName,
                //                               Id = US.Id,
                //                               TenantId = US.TenantId,
                //                               JobTitle=US.JobTitle,
                //                               Mobile=US.Mobile ?? US.PhoneNumber,
                //                               State = US.State,
                //                               City = US.City,
                //                               Zip = US.Zip,
                //                               Email=US.Email,
                //                           }
                //                  ).ToList();

                var total = _userManager.Users.Where(x => x.TenantId == tenantId).Count();
                 componentPermissionUsers = (from US in _userManager.Users
                               join tu in db.TenantUsers on US.Id equals tu.UserId
                               join crm in db.CrmUserBasicDetail on US.Id equals crm.UserId into crmLJ
                               from crm in crmLJ.DefaultIfEmpty()
                               where tu.TenantId == tenantId
                               select new WellIdentityUser
                               {

                                FirstName = US.FirstName,
                                MiddleName = US.MiddleName,
                                LastName = US.LastName,
                                Id = US.Id,
                                TenantId = US.TenantId,
                                JobTitle=US.JobTitle,
                                Mobile=US.Mobile ?? US.PhoneNumber,
                                State = US.State,
                                City = US.City,
                                Zip = US.Zip,
                                Email=US.Email

                               }).ToList();


                componentPermissionUsers = componentPermissionUsers.GroupBy(x => x.Id).Select(g => g.First()).ToList();

                    //result.GroupBy(x => x.ProjectId).Select(g => g.First()).ToList();

                //bool permission = Convert.ToBoolean(componentPermission);
                return componentPermissionUsers;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetComponentPermissionUsers", null);
                return componentPermissionUsers;
            }
        }
        public bool UpdateDispatch(DispatchRoutes dispatch)
        {
            try
            {
                var result = db.Users.Where(x => x.Id == dispatch.UserId).FirstOrDefault();
                //if (result != null)
                //{
                //    if (dispatch.DispatchNotes != "")
                //    {
                //        result.DispatchNotes = dispatch.DispatchNotes;
                //        db.SaveChanges();
                //    }


                var userResult = db.Users.Where(x => x.Id == dispatch.UserId).FirstOrDefault();
                UsersOptions userOption = new UsersOptions();
                if (userResult.UsersOptions != null)
                {
                  

                    // var userResult = db.Users.Where(x => x.Id == dispatch.UserId).FirstOrDefault();
                    //  UsersOptions userOption = new UsersOptions();
                    userOption = JsonConvert.DeserializeObject<UsersOptions>(userResult.UsersOptions);
                    userOption.DispatchNotes = dispatch.DispatchNotes;
                    userResult.UsersOptions = JsonConvert.SerializeObject(userOption); //JsonConvert.SerializeObject<UsersOptions>(userOption);
                    db.SaveChanges();

                }
                else
                {
                //    userOption = JsonConvert.DeserializeObject<UsersOptions>(userResult.UsersOptions);
                    //  if (dispatch.DispatchNotes != "")
                    //  {
                    userOption.DispatchNotes = dispatch.DispatchNotes;
                    userOption.IsDispatchUser = true;
                    userOption.CurrentStatus = "0";//0-Empty,1-Destinations Changing,2-Waiting For Approval (if Destinations sent to Driver)
                                                   //  }
                    userResult.UsersOptions = JsonConvert.SerializeObject(userOption);
                    db.SaveChanges();

                }



                //var userResult = db.Users.Where(x => x.Id == dispatch.UserId).FirstOrDefault();
                //    UsersOptions userOption = new UsersOptions();
                //    userOption = JsonConvert.DeserializeObject<UsersOptions>(userResult.UsersOptions);
                //    userOption.DispatchNotes = dispatch.DispatchNotes;
                //    userResult.UsersOptions = JsonConvert.SerializeObject(userOption); //JsonConvert.SerializeObject<UsersOptions>(userOption);
                //    db.SaveChanges();
                //}
                return true;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository UpdateDispatch", null);
                return false;
            }
        }

       
        public string GetActiveUserNotes(string userId)
        {
            try
            {
                var userResult = db.Users.Where(x => x.Id == userId).FirstOrDefault();
                UsersOptions userOption = new UsersOptions();
                if (userResult.UsersOptions != null)
                {
                    userOption = JsonConvert.DeserializeObject<UsersOptions>(userResult.UsersOptions);
                    return userOption.DispatchNotes;
                }
                else
                {
                    return "";
                }
                
                
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public async Task<DispatchNotification> GetActiveDispatchRoutes(string userId,string dispatchNotes)
        {
            List<Destination> destinationsList = new List<Destination>();
            DispatchNotification notification = new DispatchNotification();
            try
            {
                var routListresult = (from r in db.DispatchRoutes
                                      where r.UserId == userId orderby r.CurrentRouterOrder ascending
                                      select new DispatchRoutesModel
                                      {
                                          dispatchid = r.DispatchId,
                                          userid = r.UserId,
                                          customer = r.Customer,
                                          locationname = r.LocationName,
                                          address = r.LocationAddress,
                                          city = r.LocationCity,
                                          state = r.LocationState,
                                          zip = r.LocationZip,
                                          latitude = r.Latitude,
                                          longitude = r.Longitude,
                                          routeorder = r.RouteOrder,
                                          api = r.APINumber,
                                          wellid = r.WellId,
                                          wellname = r.WellName,
                                          rigid = r.RigId,
                                          rigname = r.RigName


                                      }
                                     ).ToList();

                var userResult = db.Users.Where(x => x.Id == userId).FirstOrDefault();
                UsersOptions userOption = new UsersOptions();
                if (userResult.UsersOptions != null){
                    userOption = JsonConvert.DeserializeObject<UsersOptions>(userResult.UsersOptions);
                    if (dispatchNotes == "")
                    {
                        dispatchNotes = userOption.DispatchNotes;
                    }
                }

                if (userOption != null)
                {
                    notification.message = dispatchNotes;
                }
                else
                {
                    notification.message = "";
                }


                notification.optimize = true;
                notification.user_key = userId;
                
                foreach(var item in routListresult)
                {
                    Destination destination = new Destination();
                    var address1 = item.address ?? "";
                    var address2 = item.city ?? "";
                    var address3 = item.state ?? "";
                    var address4 = item.zip ?? "";
                    var address5 = item.latitude ?? 0.00;
                    var address6 = item.longitude ?? 0.00;
                    
                    if ((item.wellid == null || item.wellid == "") && (item.rigid == null || item.rigid == ""))
                    {
                        destination.type = "location";
                        //destination.address = 
                        var addressFinal = String.Concat(address1, address2 != "" ? "," + address2 : address2);
                        addressFinal = String.Concat(addressFinal, address3 != "" ? "," + address3 : address3);
                        addressFinal = String.Concat(addressFinal, address4 != "" ? "," + address4 : address4);
                        addressFinal = String.Concat(addressFinal, addressFinal + Convert.ToString(address5) == "0.00" ? Convert.ToString("," + address5) : Convert.ToString(address5));
                        destination.address = addressFinal;// address1 + address2 != "" ? "," + address2 : address2 + address3 != "" ? "," + address3 : address3 + address4 != "" ? "," + address4 : address4 +
                                                     
                    }
                    else if ((item.wellid != null || item.wellid != "") && (item.rigid == null || item.rigid == "" || item.rigid == "0"))
                    {
                        destination.type = "well";
                        destination.id = Convert.ToInt64(item.wellid);
                    }
                    else if (item.wellid == null || item.wellid == "" && item.rigid != null || item.rigid != "")
                    {
                        destination.type = "rig";
                        destination.id = Convert.ToInt64(item.rigid);
                    }
                    destinationsList.Add(destination);
                }
                notification.destinations = destinationsList.ToArray();


                return notification;
            }             
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetActiveDispatchRoutes", null);
                return notification;
            }
        }

        public async Task<DispatchNotification> GetActiveDispatchRoutes_V2(string userId, string dispatchNotes)
        {
            List<Destination> destinationsList = new List<Destination>();
            DispatchNotification notification = new DispatchNotification();
            try
            {                
                var routListresult = (from r in db.DispatchRoutes
                                      where r.UserId == userId && r.RecordStatus != "0"
                                      orderby r.CurrentRouterOrder ascending
                                      select new DispatchRoutesModel
                                      {
                                          dispatchid = r.DispatchId,
                                          userid = r.UserId,
                                          customer = r.Customer,
                                          locationname = r.LocationName,
                                          address = r.LocationAddress,
                                          city = r.LocationCity,
                                          state = r.LocationState,
                                          zip = r.LocationZip,
                                          latitude = r.Latitude,
                                          longitude = r.Longitude,
                                          routeorder = Convert.ToInt32(r.CurrentRouterOrder),
                                          api = r.APINumber,
                                          wellid = r.WellId,
                                          wellname = r.WellName,
                                          rigid = r.RigId,
                                          rigname = r.RigName

                                      }
                                     ).ToList();

                //var userResult = db.Users.Where(x => x.Id == userId).FirstOrDefault();
                //UsersOptions userOption = new UsersOptions();
                //if (userResult.UsersOptions != null)
                //{
                //    userOption = JsonConvert.DeserializeObject<UsersOptions>(userResult.UsersOptions);
                //    if (dispatchNotes == "")
                //    {
                //        dispatchNotes = userOption.DispatchNotes;
                //    }
                //}

                var userResult = db.Users.Where(x => x.Id == userId).FirstOrDefault();
                UsersOptions userOption = new UsersOptions();
                if (userResult.UsersOptions != null)
                {
                    userOption = JsonConvert.DeserializeObject<UsersOptions>(userResult.UsersOptions);
                    //  if (notes != "")
                    //  {
                    userOption.DispatchNotes = userOption.DispatchNotes;
                    userOption.IsDispatchUser = true;
                    userOption.CurrentStatus = "1";//0-Empty,1-Destinations Changing,2-Waiting For Approval (if Destinations sent to Driver)
                                                   //  }
                    userResult.UsersOptions = JsonConvert.SerializeObject(userOption);
                    await db.SaveChangesAsync();
                }

                if (userOption != null)
                {
                    notification.message = dispatchNotes;
                }
                else
                {
                    notification.message = "";
                }


                notification.optimize = true;
                notification.user_key = userId;

                foreach (var item in routListresult)
                {
                    Destination destination = new Destination();
                    var address1 = item.address ?? "";
                    var address2 = item.city ?? "";
                    var address3 = item.state ?? "";
                    var address4 = item.zip ?? "";

                    address1 = address1.Replace(",", "");
                    address2 = address2.Replace(",", "");
                    address3 = address3.Replace(",", "");
                    address4 = address4.Replace(",", "");

                    var address5 = item.latitude ?? 0.00;
                    var address6 = item.longitude ?? 0.00;
                    var address7 = item.locationname ?? "";
                    address7 = address7.Replace(",", "");

                    if ((item.wellid == null || item.wellid == "") && (item.rigid == null || item.rigid == ""))
                    {
                        destination.type = "location";


                        if (address5.ToString() != "" && address5 != null && address5 != 0.00)
                        {
                            var addressFinal = String.Concat(address7 != "" ? address7 : address7);
                            addressFinal = String.Concat(addressFinal, addressFinal + Convert.ToString(address5) == "0.00" ? Convert.ToString("," + address5) : Convert.ToString("," + address5));
                            addressFinal = String.Concat(addressFinal, addressFinal + Convert.ToString(address6) == "0.00" ? Convert.ToString("," + address6) : Convert.ToString("," + address6));
                            destination.address = addressFinal;

                      

                            //}
                            destination.address = addressFinal;
                        }
                        else
                        {
                            //var addressFinal = String.Concat(address1, address2 != "" ? "," + address2 : address2);
                            //addressFinal = String.Concat(addressFinal, address3 != "" ? "," + address3 : address3);
                            //addressFinal = String.Concat(addressFinal, address4 != "" ? "," + address4 : address4);
                            //destination.address = addressFinal;

                            //var addressFinal = String.Concat(address1, address2 != "" ? "," + address2 : address2);
                            //addressFinal = String.Concat(addressFinal, address3 != "" ? "," + address3 : address3);
                            //addressFinal = String.Concat(addressFinal, address4 != "" ? "," + address4 : address4);
                            //destination.address = addressFinal;


                            var addressFinal = String.Concat(address7, address1 != "" ? "," + address1 : address1);
                            addressFinal = String.Concat(addressFinal, address2 != "" ? "," + address2 : address2);
                            addressFinal = String.Concat(addressFinal, address3 != "" ? "," + address3 : address3);
                            addressFinal = String.Concat(addressFinal, address4 != "" ? "," + address4 : address4);
                            destination.address = addressFinal;
                        }

                        // address1 + address2 != "" ? "," + address2 : address2 + address3 != "" ? "," + address3 : address3 + address4 != "" ? "," + address4 : address4 +

                    }
                    else if ((item.wellid != null || item.wellid != "") && (item.rigid == null || item.rigid == "" || item.rigid == "0"))
                    {
                        destination.type = "well";
                        destination.id = Convert.ToInt64(item.wellid);
                    }
                    else if (item.wellid == null || item.wellid == "" && item.rigid != null || item.rigid != "")
                    {

                        destination.type = "rig";
                        destination.id = Convert.ToInt64(item.rigid);
                    }
                    destinationsList.Add(destination);
                }
                notification.destinations = destinationsList.ToArray();


                return notification;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetActiveDispatchRoutes", null);
                return notification;
            }
        }

        public async Task<List<DispatchRoutesModel>> GetOperatorsharedetails(List<AuctionProposalViewModel> auctionActiveList, string operatorId, string userId)
        {
            List<DispatchRoutesModel> dispatchOtherRouteObj = new List<DispatchRoutesModel>();
            List<DispatchRoutesModel> dispatchWellObj = new List<DispatchRoutesModel>();
            List<DispatchRoutesModel> dispatchObj = new List<DispatchRoutesModel>();
            List<DispatchRoutesModel> result = new List<DispatchRoutesModel>();
            try
            {
                var rigArray = auctionActiveList.Select(r => r.RigId).ToArray();
                var wellArray = auctionActiveList.Select(r => r.WellId).ToArray();

            

                // dispatchObj = dispatchRigObj.Union(dispatchWellObj).ToList();

                dispatchOtherRouteObj = (from dr in db.DispatchRoutes
                                         join us in db.Users on dr.UserId equals us.Id
                                         //join crm in db.CrmUserBasicDetail on us.Id equals crm.UserId
                                         //join cp in db.CorporateProfile on crm.CorporateProfileId equals cp.ID
                                         where dr.OperatorId == operatorId && dr.UserId==userId
                                         select new DispatchRoutesModel
                                         {
                                             dispatchid = dr.DispatchId,
                                             dispatchnotes = dr.DispatchNotes,
                                             userid = dr.UserId,
                                             customer = dr.Customer,
                                             address = dr.LocationAddress,
                                             locationname = dr.LocationName,
                                             city = dr.LocationCity,
                                             state = dr.LocationState,
                                             zip = dr.LocationZip,
                                             routeorder = dr.RouteOrder,
                                             username = us.FirstName + " " + us.LastName,
                                             latitude = dr.Latitude,
                                             longitude = dr.Longitude,
                                             createddate = dr.CreatedDate,
                                             api = dr.APINumber,
                                             rigname = dr.RigName,
                                             rigid = dr.RigId,
                                             wellname = dr.WellName,
                                             wellid = dr.WellId,
                                             operatorId = dr.OperatorId,
                                             activityid = dr.ActivityId
                                         }
                                         ).ToList();

                dispatchObj = dispatchWellObj.Union(dispatchOtherRouteObj).ToList();

                foreach (var item in dispatchObj)
                {
                    DispatchRoutesModel dispatchRouterModelObj = new DispatchRoutesModel();

                    dispatchRouterModelObj.dispatchid = item.dispatchid;
                    dispatchRouterModelObj.dispatchnotes = "";//item.dispatchnotes;
                    dispatchRouterModelObj.userid = item.userid;
                    dispatchRouterModelObj.customer = item.customer;
                    dispatchRouterModelObj.address = item.address;
                    dispatchRouterModelObj.locationname = item.locationname;
                    dispatchRouterModelObj.city = item.city;
                    dispatchRouterModelObj.state = item.state;
                    dispatchRouterModelObj.zip = item.zip;
                    dispatchRouterModelObj.routeorder = item.routeorder;


                    dispatchRouterModelObj.username = item.username;
                    dispatchRouterModelObj.latitude = item.latitude;
                    dispatchRouterModelObj.longitude = item.longitude;
                    dispatchRouterModelObj.createddate = item.createddate;
                    //dispatchRouterModelObj.modifieddate = item.modifieddate;
                    //dispatchRouterModelObj.ismodified = item.ismodified;
                    dispatchRouterModelObj.api = item.api ?? "";
                    dispatchRouterModelObj.rigname = item.rigname ?? "";
                    dispatchRouterModelObj.wellid = item.wellid ?? "";
                    dispatchRouterModelObj.rigid = item.rigid ?? "";
                    dispatchRouterModelObj.wellname = item.wellname ?? "";

                    dispatchRouterModelObj.activityid = item.activityid;
                    result.Add(dispatchRouterModelObj);
                }

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetOperatorsharedetails", null);
            }
            return result;
        }



        public async Task<List<DispatchRoutesModel>> GetDispatchRoutesForOperator(List<AuctionProposalViewModel> auctionActiveList, string operatorId)
        {
            List<DispatchRoutesModel> dispatchOtherRouteObj = new List<DispatchRoutesModel>();
            List<DispatchRoutesModel> dispatchWellObj = new List<DispatchRoutesModel>();
            List<DispatchRoutesModel> dispatchObj = new List<DispatchRoutesModel>();
            List<DispatchRoutesModel> result = new List<DispatchRoutesModel>();
            try
            {
                var rigArray = auctionActiveList.Select(r => r.RigId).ToArray();
                var wellArray = auctionActiveList.Select(r => r.WellId).ToArray();

                dispatchWellObj = (from well in db.WELL_REGISTERs
                                   join dr in db.DispatchRoutes on well.Router_WellId equals dr.WellId
                                   join us in db.Users on dr.UserId equals us.Id
                                   //join crm in db.CrmUserBasicDetail on us.Id equals crm.UserId
                                   //join cp in db.CorporateProfile on crm.CorporateProfileId equals cp.ID
                                   where wellArray.Contains(well.well_id)
                                   select new DispatchRoutesModel
                                   {
                                       dispatchid = dr.DispatchId,
                                       dispatchnotes = dr.DispatchNotes,
                                       userid = dr.UserId,
                                       customer = dr.Customer,
                                       address = dr.LocationAddress,
                                       locationname = dr.LocationName,
                                       city = dr.LocationCity,
                                       state = dr.LocationState,
                                       zip = dr.LocationZip,
                                       routeorder = dr.RouteOrder,
                                       username = us.FirstName + " " + us.LastName,
                                       latitude = dr.Latitude,
                                       longitude = dr.Longitude,
                                       createddate = dr.CreatedDate,
                                       api = dr.APINumber,
                                       rigname = dr.RigName,
                                       rigid = dr.RigId,
                                       wellname = dr.WellName,
                                       wellid = dr.WellId
                                   }
                            ).ToList();

                // dispatchObj = dispatchRigObj.Union(dispatchWellObj).ToList();

                dispatchOtherRouteObj = (from dr in db.DispatchRoutes
                                         join us in db.Users on dr.UserId equals us.Id
                                         //join crm in db.CrmUserBasicDetail on us.Id equals crm.UserId
                                         //join cp in db.CorporateProfile on crm.CorporateProfileId equals cp.ID
                                         join cp in db.CorporateProfile on us.TenantId equals cp.TenantId
                                         where dr.OperatorId == operatorId
                                         select new DispatchRoutesModel
                                         {
                                             dispatchid = dr.DispatchId,
                                             dispatchnotes = dr.DispatchNotes,
                                             userid = dr.UserId,
                                             customer = cp.Name,
                                             address = dr.LocationAddress,
                                             locationname = dr.LocationName,
                                             city = dr.LocationCity,
                                             state = dr.LocationState,
                                             zip = dr.LocationZip,
                                             routeorder = dr.RouteOrder,
                                             username = us.FirstName + " " + us.LastName,
                                             latitude = dr.Latitude,
                                             longitude = dr.Longitude,
                                             createddate = dr.CreatedDate,
                                             api = dr.APINumber,
                                             rigname = dr.RigName,
                                             rigid = dr.RigId,
                                             wellname = dr.WellName,
                                             wellid = dr.WellId,
                                             operatorId = dr.OperatorId
                                         }
                                         ).ToList();

                dispatchObj = dispatchWellObj.Union(dispatchOtherRouteObj).ToList();

                foreach (var item in dispatchObj)
                {
                    DispatchRoutesModel dispatchRouterModelObj = new DispatchRoutesModel();

                    dispatchRouterModelObj.dispatchid = item.dispatchid;
                    dispatchRouterModelObj.dispatchnotes = "";//item.dispatchnotes;
                    dispatchRouterModelObj.userid = item.userid;
                    dispatchRouterModelObj.customer = item.customer;
                    dispatchRouterModelObj.address = item.address;
                    dispatchRouterModelObj.locationname = item.locationname;
                    dispatchRouterModelObj.city = item.city;
                    dispatchRouterModelObj.state = item.state;
                    dispatchRouterModelObj.zip = item.zip;
                    dispatchRouterModelObj.routeorder = item.routeorder;

                    dispatchRouterModelObj.username = item.username;
                    dispatchRouterModelObj.latitude = item.latitude;
                    dispatchRouterModelObj.longitude = item.longitude;
                    dispatchRouterModelObj.createddate = item.createddate;
                    //dispatchRouterModelObj.modifieddate = item.modifieddate;
                    //dispatchRouterModelObj.ismodified = item.ismodified;
                    dispatchRouterModelObj.api = item.api ?? "";
                    dispatchRouterModelObj.rigname = item.rigname ?? "";
                    dispatchRouterModelObj.wellid = item.wellid ?? "";
                    dispatchRouterModelObj.rigid = item.rigid ?? "";
                    dispatchRouterModelObj.wellname = item.wellname ?? "";
                    result.Add(dispatchRouterModelObj);
                }

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetDispatchRoutesForOperator", null);
            }
            return result;
        }

        public async Task<bool> UpdateDispatchRouteOrders(DispatchRouteOrderModel dispatch)
        {
            try
            {
                var dispatchRoute = dispatch.DispatchRoutesModel.ToList();

                var result = db.DispatchRoutes.ToList();

                int i = 1;


                foreach (var item in dispatch.DispatchRoutesModel)
                {
                    //Convert.ToInt32(dispatchRoute.Where(x => x.dispatchid == item.dispatchid).FirstOrDefault());
                    
                    foreach(var dbItem in result)
                    {
                        if(dbItem.DispatchId == item.dispatchid)
                        {
                            dbItem.RouteOrder = i;
                        }                                                
                    }
                    i++;
                }                   
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository UpdateDispatchRouteOrders", null);
                return false;
            }
        }
        private List<DispatchRoutesModel> GetDispatchUsersCurrentLocation()
        {
            try
            {
                var userOptionsUsers = (from us in db.Users
                                        where us.UsersOptions != null
                                        select new DispatchRoutesModel
                                        {
                                            userid = us.Id,
                                            usersoptions = us.UsersOptions == null || us.UsersOptions == "" ? new UsersOptions() : JsonConvert.DeserializeObject<UsersOptions>(us.UsersOptions),
                                            username = us.FirstName + " " + us.LastName
                                        }).ToList();

                var dispatchUsers = (from us in userOptionsUsers
                                     where us.usersoptions.IsDispatchUser == true
                                     select new DispatchViewModel
                                     {
                                         userId = us.userid,
                                         username = us.username
                                     }).ToList();

                var dispatchCurrentLocationUsers = (from us in dispatchUsers
                                                    select new DispatchRoutesModel
                                                    {
                                                        userid = us.userId,
                                                        username = us.username,
                                                        dispatchid = new Guid().ToString(),
                                                        locationname="",
                                                        address="",
                                                        city="",
                                                        rigname="",
                                                        wellname=""
                                                    }).ToList();
                return dispatchCurrentLocationUsers;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository UpdateDispatchRouteOrders", null);
                return new List<DispatchRoutesModel>();
            }

        }

        public async Task<IdentityResult> EditProfilePasswordChange(WellIdentityUser resultUser)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(resultUser.Id);
                if (user != null)
                {
                    user.PasswordHash = resultUser.PasswordHash;
                    return await _userManager.UpdateAsync(user);
                }

                return null;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository UpdateUser", null);
                return null;
            }
        }

        public Task<CorporateProfile> GetCorporateProfileByUserId(string userId)
        {
            var crmUser = db.CrmUserBasicDetail.FirstOrDefault(x => x.UserId == userId);

            if (crmUser != null)
            {
                var result = db.CorporateProfile.FirstOrDefault(x => x.ID == crmUser.CorporateProfileId);

                return Task.FromResult(result ?? new CorporateProfile());
            }
            else
            {
                return Task.FromResult(new CorporateProfile());
            }

        }

        public async Task<bool> CreateDispatchRoutesHistory(DispatchRoutesHistoryModel destinationChanges)
        {
            try
            {
                //   string historyId = Guid.NewGuid().ToString("D");

                DateTime timeUtc = (TimeZoneInfo.ConvertTimeToUtc(DateTime.Now, TimeZoneInfo.Local));
                TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);

                DispatchRoutesHistoryHead historyHead = new DispatchRoutesHistoryHead();
                // historyHead.HistoryId = historyId;
                historyHead.HistoryId = destinationChanges.historyId;
                historyHead.CreateDate = cstTime;
                historyHead.RouteSource = destinationChanges.dispatchfrom;
                historyHead.UserId = destinationChanges.userid;
                historyHead.DispatchNotes = destinationChanges.dispatchnotes;
                db.DispatchRoutesHistoryHead.Add(historyHead);
                int changedRouteOrder = 1;
                foreach (var route in destinationChanges.routes)
                {

                    DispatchRoutesHistoryDetails historyDetails = new DispatchRoutesHistoryDetails();

                    historyDetails.HistoryHeadId = destinationChanges.historyId;
                    historyDetails.HistoryDetailId = Guid.NewGuid().ToString("D");
                    historyDetails.Customer = route.customer;
                    historyDetails.LocationName = route.locationname;
                    historyDetails.LocationAddress = route.address;
                    historyDetails.LocationCity = route.city;
                    historyDetails.LocationState = route.state;
                    historyDetails.LocationZip = route.zip;
                    historyDetails.Latitude = route.latitude;
                    historyDetails.Longitude = route.longitude;
                    historyDetails.WellId = route.wellid==""?"0": route.wellid;
                    historyDetails.RigId = route.rigid==""?"0": route.rigid;
                    historyDetails.APINumber = route.apinumber;
                    historyDetails.RigName = route.rigname;
                    historyDetails.WellName = route.wellname;
                    //historyDetails.ChangedRouterOrder = route.changedrouterorder;
                    historyDetails.ChangedRouteOrder = changedRouteOrder;
                    //           if (route.dispatchid == null || route.dispatchid == "-1")
                    if (route.dispatchid == null)
                    {
                        historyDetails.DispatchId = Guid.NewGuid().ToString("D");
                        historyDetails.RouteOrder = 0;
                        //db.DispatchRoutes.Add(route);

                    }
                    else
                    {
                        var result = db.DispatchRoutes.Where(x => x.DispatchId == route.dispatchid).AsNoTracking().FirstOrDefault();
                        if (result != null)
                        {
                            historyDetails.RouteOrder = result.RouteOrder;
                            historyDetails.DispatchId = result.DispatchId;
                        }
                        else
                        {
                            historyDetails.DispatchId = Guid.NewGuid().ToString("D");
                            historyDetails.RouteOrder = 0;
                        }
                        
                        //db.DispatchRoutes.Update(route);
                    }
                    changedRouteOrder = changedRouteOrder + 1;

                    db.DispatchRoutesHistoryDetails.Add(historyDetails);
                }
                await db.SaveChangesAsync();

                //db.DispatchRoutesHistoryHead.Add(historyhead);
                //db.SaveChanges();

                //    //-----------DispatchRoutesHistoryDetails table 
                //    //dispatchhistory.DispatchId = DispatchId;
                //    //dispatchhistory.RouteOrder = result.RouteOrder;
                //   // db.DispatchRoutesHistoryDetails.Add(dispatchhistory);

                return true;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository CreateDispatchRoutesHistory", null);
                return false;
            }
        }

        public async Task<bool> CreateDispatchRoutesHistory_Deleted(DispatchRoutesHistoryModel destinationChanges)
        {
            try
            {
                //   string historyId = Guid.NewGuid().ToString("D");

                DateTime timeUtc = (TimeZoneInfo.ConvertTimeToUtc(DateTime.Now, TimeZoneInfo.Local));
                TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);

                //DispatchRoutesHistoryHead historyHead = new DispatchRoutesHistoryHead();
                //// historyHead.HistoryId = historyId;
                //historyHead.HistoryId = destinationChanges.historyId;
                //historyHead.CreateDate = cstTime;
                //historyHead.RouteSource = destinationChanges.dispatchfrom;
                //historyHead.UserId = destinationChanges.userid;
                //historyHead.DispatchNotes = destinationChanges.dispatchnotes;
                //db.DispatchRoutesHistoryHead.Add(historyHead);
                int changedRouteOrder = 1;
                foreach (var route in destinationChanges.routes)
                {

                    DispatchRoutesHistoryDetails historyDetails = new DispatchRoutesHistoryDetails();

                    historyDetails.HistoryHeadId = destinationChanges.historyId;
                    historyDetails.HistoryDetailId = Guid.NewGuid().ToString("D");
                    historyDetails.Customer = route.customer;
                    historyDetails.LocationName = route.locationname;
                    historyDetails.LocationAddress = route.address;
                    historyDetails.LocationCity = route.city;
                    historyDetails.LocationState = route.state;
                    historyDetails.LocationZip = route.zip;
                    historyDetails.Latitude = route.latitude;
                    historyDetails.Longitude = route.longitude;
                    historyDetails.WellId = route.wellid == "" ? "0" : route.wellid;
                    historyDetails.RigId = route.rigid == "" ? "0" : route.rigid;
                    historyDetails.APINumber = route.apinumber;
                    historyDetails.RigName = route.rigname;
                    historyDetails.WellName = route.wellname;
                    //historyDetails.ChangedRouterOrder = route.changedrouterorder;
                    historyDetails.ChangedRouteOrder = changedRouteOrder;
                    //           if (route.dispatchid == null || route.dispatchid == "-1")
                    if (route.dispatchid == null)
                    {
                        historyDetails.DispatchId = Guid.NewGuid().ToString("D");
                        historyDetails.RouteOrder = 0;
                        //db.DispatchRoutes.Add(route);

                    }
                    else
                    {
                        var result = db.DispatchRoutes.Where(x => x.DispatchId == route.dispatchid).AsNoTracking().FirstOrDefault();
                        if (result != null)
                        {
                            historyDetails.RouteOrder = result.RouteOrder;
                            historyDetails.DispatchId = result.DispatchId;
                        }
                        else
                        {
                            historyDetails.DispatchId = Guid.NewGuid().ToString("D");
                            historyDetails.RouteOrder = 0;
                        }

                        //db.DispatchRoutes.Update(route);
                    }
                    changedRouteOrder = changedRouteOrder + 1;

                    db.DispatchRoutesHistoryDetails.Add(historyDetails);
                }
                await db.SaveChangesAsync();

                //db.DispatchRoutesHistoryHead.Add(historyhead);
                //db.SaveChanges();

                //    //-----------DispatchRoutesHistoryDetails table 
                //    //dispatchhistory.DispatchId = DispatchId;
                //    //dispatchhistory.RouteOrder = result.RouteOrder;
                //   // db.DispatchRoutesHistoryDetails.Add(dispatchhistory);

                return true;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository CreateDispatchRoutesHistory", null);
                return false;
            }
        }

        public int GetStateId(string name)
        {
            return db.USAStates.Where(x => x.Name == name).FirstOrDefault().StateId;
        }

        public async Task<int> UpdateCustomerCorporateProfile(CorporateProfileAdmin input, string userId, string tenantId)
        {
            try
            {
                var crf = db.CorporateProfile.Where(x => x.TenantId == tenantId).FirstOrDefault();
                if (crf != null)
                {
                    // crf.UserId = userId;
                    //crf.TenantId = tenantId;
                    crf.ModifiedDate = DateTime.Now;
                    crf.LogoPath = input.LogoPath == null ? crf.LogoPath : input.LogoPath;
                    crf.Name = input.Name;
                    crf.Phone = input.Phone;

                    var stateid = GetStateId(input.State);

                    crf.State = stateid.ToString();
                    crf.Website = input.Website;
                    crf.Zip = input.Zip;
                    crf.Country = input.Country;
                    crf.Address1 = input.Address1;
                    crf.Address2 = input.Address2;
                    crf.City = input.City;
                    // crf.CServices = input.CServices;
                    WellAIAppContext.Current.Session.SetString("CompanyName", crf.Name);
                }
                else
                {
                    // CorporateProfile crf1 = new CorporateProfile();
                    // crf1.UserId = userId;
                    //// crf1.TenantId = tenantId;
                    // crf1.ID = Guid.NewGuid().ToString("D");
                    // crf1.CreatedDate = DateTime.Now;
                    // crf1.ModifiedDate = DateTime.Now;
                    // crf1.LogoPath = input.LogoPath;
                    // crf1.Name = input.Name;
                    // crf1.Phone = input.Phone;
                    // crf1.State = input.State;
                    // crf1.Website = input.Website;
                    // crf1.Zip = input.Zip;
                    // crf1.Country = input.Country;
                    // crf1.Address1 = input.Address1;
                    // crf1.Address2 = input.Address2;
                    // crf1.City = input.City;
                    //// crf.CServices = input.CServices;
                    // db.CorporateProfile.Add(crf1);
                    // WellAIAppContext.Current.Session.SetString("CompanyName", crf.Name);
                }

                var result = await db.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository UpdateServiceCorporateProfile", null);
                return 0;
            }
        }
        public async Task<int> UpdateUsersession(UserSessions user, string email)
        {
            try
            {
                int Result = 0;

                if (user != null)
                {
                    Result = await DeleteUsersession(email);
                    if (Result != 0)
                    {
                        Result = await SaveUsersession(user);
                    }
                    //db.UserSessions.Add(user);
                    //db.Entry(user).State = EntityState.Modified;
                    //Result = await db.SaveChangesAsync();
                }

                return await Task.FromResult(Result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository SaveUsersession", null);
                return 0;
            }
        }


        public List<MessageQueue> GetUserNotificationDetailsScroll(string userId, int skip, int take)
        {
            try
            {
                var userType = (from usr in db.Users
                                where usr.Id == userId
                                select new
                                {
                                    usr.WellUser,
                                    usr.TenantId
                                }
                           ).ToList();

                if (userType != null && userType.Count > 0)
                {
                    if (userType[0].WellUser == false || userType[0].WellUser == null) //
                    {
                        var messageDetails = (from message in db.MessageQueues
                                              join user in _userManager.Users on message.From_id equals user.Id into gj
                                              from x in gj.DefaultIfEmpty()
                                              where message.To_id == userId && message.IsActive == 1 /*|| (message.From_id== userId && message.IsActive == 1)*/
                                              select new MessageQueue
                                              {
                                                  Messagequeue_id = message.Messagequeue_id,
                                                  From_id = (message.From_id.Contains("Support") ? message.From_id : x.FirstName + " " + x.LastName),
                                                  To_id = message.To_id,
                                                  Type = message.Type,
                                                  EntityId = message.EntityId,
                                                  RigId = message.RigId,
                                                  TaskName = message.TaskName,
                                                  IsActive = message.IsActive,
                                                  JobName = message.JobName,
                                                  CreatedDate = message.CreatedDate
                                              }).Distinct().OrderByDescending(x => x.CreatedDate).Take(take).ToList();


                        //foreach (var item in messageDetails)
                        //{
                        //    if (item.Type == 4 || item.Type == 5 || item.Type == 6 || item.Type == 3 || item.JobName == "Chatmessage")
                        //    {
                        //        if (item.Type == 4)
                        //        {
                        //            item.From_id = db.RigRegisters.Where(r => r.Rig_Id == item.RigId).Select(r => r.Rig_Name).FirstOrDefault() + ":" + item.TaskName;
                        //        }
                        //        if (item.JobName == "Chatmessage")
                        //        {
                        //            item.From_id = item.From_id + " Says:" + item.TaskName;
                        //            item.TaskName = item.From_id;
                        //        }

                        //        if (item.Type == 5 || item.Type == 3)
                        //        {
                        //            var auctionproposalObj = db.AuctionProposals.Where(x => x.RigId == item.RigId && x.ProposalId == item.EntityId).FirstOrDefault();

                        //            if (item.JobName == "Request Closing")
                        //            {
                        //                item.CreatedDate = auctionproposalObj.AuctionEnd;
                        //            }
                        //            //else if (item.JobName == "Bid Accepted" || item.JobName == "Bid Rejected" || item.JobName == "Bid")
                        //            //{
                        //            //    item.TaskName = item.TaskName;
                        //            //}
                        //        }
                        //        else if (item.Type == 6)
                        //        {
                        //            var auctionproposalObj = db.AuctionProposals.Where(x => x.RigId == item.RigId && x.ProposalId == item.EntityId).FirstOrDefault();

                        //            if (item.JobName == "Service Request")
                        //            {
                        //                item.CreatedDate = auctionproposalObj.AuctionEnd;
                        //            }
                        //            //else if (item.JobName == "Service Accepted" || item.JobName == "Service Rejected" || item.JobName == "Service Closing")
                        //            //{
                        //            //    //Phase II Changes
                        //            //    //item.TaskName = item.JobName + " " + item.TaskName;
                        //            //    item.TaskName = item.TaskName;
                        //            //}
                        //        }
                        //    }

                        //}

                        return messageDetails;
                    }
                    else
                    {
                        IEnumerable<string> usersListArray = from usr in db.Users
                                                             where usr.TenantId == userType[0].TenantId
                                                             select usr.Id;

                        var messageTenant = (from usrmsg in db.MessageQueues
                                             join fromuser in _userManager.Users on usrmsg.From_id equals fromuser.Id
                                             where usrmsg.IsActive == 1 && fromuser.TenantId == userType[0].TenantId
                                             select new MessageQueue
                                             {
                                                 From_id = Convert.ToString(usrmsg.From_id),
                                             }).Distinct().ToList();

                        var messageDetails1 = (from usrmsg in db.MessageQueues
                                               where usrmsg.IsActive == 1
                                               select new MessageQueue
                                               {
                                                   Messagequeue_id = usrmsg.Messagequeue_id,
                                                   From_id = usrmsg.From_id,
                                                   To_id = usrmsg.To_id,
                                                   Type = usrmsg.Type,
                                                   EntityId = usrmsg.EntityId,
                                                   RigId = usrmsg.RigId,
                                                   TaskName = usrmsg.TaskName,
                                                   IsActive = usrmsg.IsActive,
                                                   JobName = usrmsg.JobName,
                                                   CreatedDate = usrmsg.CreatedDate,
                                                   //TenantId = usrmsg.TenantId
                                               }).Distinct().OrderByDescending(x => x.CreatedDate).ToList();

                        var messageDetails = (from usrmsg in messageDetails1
                                              join msg in messageTenant on usrmsg.From_id equals msg.From_id
                                              join user in _userManager.Users on usrmsg.From_id equals user.Id into gj
                                              from x in gj.DefaultIfEmpty()
                                              where usrmsg.IsActive == 1
                                              select new MessageQueue
                                              {
                                                  Messagequeue_id = usrmsg.Messagequeue_id,
                                                  From_id = (usrmsg.From_id.Contains("Support") ? usrmsg.From_id : x.FirstName + " " + x.LastName),
                                                  To_id = usrmsg.To_id,
                                                  Type = usrmsg.Type,
                                                  EntityId = usrmsg.EntityId,
                                                  RigId = usrmsg.RigId,
                                                  TaskName = usrmsg.TaskName,
                                                  IsActive = usrmsg.IsActive,
                                                  JobName = usrmsg.JobName,
                                                  CreatedDate = usrmsg.CreatedDate,
                                              }).Distinct().OrderByDescending(x => x.CreatedDate).ToList();

                        foreach (var item in messageDetails)
                        {
                            if (item.Type == 4)
                            {
                                item.From_id = db.RigRegisters.Where(r => r.Rig_Id == item.RigId).Select(r => r.Rig_Name).FirstOrDefault() + ":" + item.TaskName;
                            }
                            if (item.JobName == "Chatmessage")
                            {
                                item.From_id = item.From_id + " Says:" + item.TaskName;
                                item.TaskName = item.From_id;
                            }

                            if (item.Type == 5 || item.Type == 3)
                            {
                                var auctionproposalObj = db.AuctionProposals.Where(x => x.RigId == item.RigId && x.ProposalId == item.EntityId).FirstOrDefault();
                                if (item.JobName == "Rquest Closing")
                                {
                                    item.CreatedDate = auctionproposalObj.AuctionEnd;
                                }
                                else if (item.JobName == "Bid Accepted" || item.JobName == "Bid Rejected" || item.JobName == "Bid")
                                {
                                    //Phase II Changes
                                    //item.TaskName = item.JobName + " " + item.TaskName;
                                    item.TaskName = item.TaskName;
                                }
                            }
                            else if (item.Type == 6)
                            {
                                var auctionproposalObj = db.AuctionProposals.Where(x => x.RigId == item.RigId && x.ProposalId == item.EntityId).FirstOrDefault();
                                if (item.JobName == "Service Request")
                                {
                                    item.CreatedDate = auctionproposalObj.AuctionEnd;
                                }
                                else if (item.JobName == "Service Accepted" || item.JobName == "Service Rejected" || item.JobName == "Service Closing")
                                {
                                    //Phase II Changes
                                    //item.TaskName = item.JobName + " " + item.TaskName;
                                    item.TaskName = item.TaskName;
                                }
                            }
                        }
                        return messageDetails;
                    }
                }
                else
                {
                    List<MessageQueue> msgqueue = new List<MessageQueue>();
                    return msgqueue;
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "CommonRepository GetUserNotificationDetailsScroll", null);
                List<MessageQueue> msgqueue = new List<MessageQueue>();
                return msgqueue;
            }
        }

        public string GetCompanyName(string tenantId)
        {
            string userid = db.CorporateProfile.Where(x => x.TenantId == tenantId).Select(x => x.UserId).FirstOrDefault();

            return db.CrmUserBasicDetail.Where(x => x.UserId == userid).Select(x => x.Company).FirstOrDefault();
        }
    }
}