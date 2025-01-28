using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Model.Administration;
using WellAI.Advisor.Model.Identity;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.BLL.Business;
using Finbuckle.MultiTenant;
using Newtonsoft.Json;

namespace WellAI.Advisor.BLL.Administration
{
    public interface ICustomerProfileBusiess
    {
        Task<Tuple<List<CustomerProfileModel>, int>> GetCustomerProfiles(int accountType, int pageNumber, int pageSize);
        Task<Tuple<List<CustomerUsersModel>, int>> GetCustomerUsers(string  tenantId, int pageNumber, int pageSize);
        Task<Tuple<List<WellMasterDataViewModel>, int>> GetCustomerWellData(string  tenantId, int pageNumber, int pageSize);
        Task<WellMasterDataViewModel> UpdateCustomerWellData(string  tenantId, WellMasterDataViewModel input);
        Task<WellMasterDataViewModel> CreateCustomerWellData(string  tenantId, WellMasterDataViewModel input);
        Task<bool> WellDataDestroy(string wellId);
        Task<List<RigModel>> GetCustomerRigs(string tenantId);
        Task<RigModel> UpdateCustomerRigs(RigModel input);
        Task<RigModel> CreateCustomerRigs(RigModel input);
        Task<List<PadModel>> GetCustomePads(string tenantId);
        Task<PadModel> UpdateCustomerPads(PadModel input);
        Task<PadModel> CreateCustomerPads(PadModel input);
        Task<CustomerProfileModel> GetCustomerDetail(string id);
        Task<CustomerUsersModel> CustomerUserDetailById(string userId);
        Task<List<IdentityRole>> GetRoles(string tenantId);
        Task<List<RoleModel>> GetRoleWithRolePermissions(string tenantId);
        Task<List<ComponentModelRec>> GetAllPermittedComponents(int accounttype);
        Task<List<RolePermissionModel>> GetRolePermissions(string tenantId);
        Task<bool> CreatePermissionComponents(string permissionName, List<RolePermissionComponentModel> actualPermComps, string userName, string tenatId);
        Task<bool> UpdatePermissionComponents(int permissionId, string permissionName, List<RolePermissionComponentModel> actualPermComps);
        Task<bool> DeletePermission(int permissionId);
        Task<List<RolePermissions>> GetAllPermissions(string tenantId);
        Task<bool> CreateRolePermissions(string roleName, List<RolePermissions> actualRolePerms, string userName, string tenantId);
        Task<bool> UpdateRolePermissions(string roleId, string roleName, List<RolePermissions> actualRolePerms);
        Task<bool> DeleteRolePermission(string roleId);
        Task<List<WellMasterDataViewModel>> GetUserAssignedWells(string userId);
        Task<List<WellMasterDataViewModel>> GetUserAssignedRigs(string userId);
        Task<List<BillingHistory>> GetCustomerInvoiceHistory(string tenantId);
        Task<int> AssignWellsToUser(string userId,List<string> wells);
        Task<int> AssignRigsToUser(string userId, List<string> RigId);
        Task<string> UpdateSubscriptioIsEnable(string SubsciptionId);
    }
    public class CustomerProfileBusiess : ICustomerProfileBusiess
    {
        private readonly WebAIAdvisorContext db;

        UserManager<WellIdentityUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        protected readonly IMapper _mapper;

        public CustomerProfileBusiess(WebAIAdvisorContext db, UserManager<WellIdentityUser> userManager, RoleManager<IdentityRole> roleManager,IMapper mapper)
        {
            this.db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;

        }
        public Task<List<WellMasterDataViewModel>> GetUserAssignedWells(string userId)
        {
            try
            {
                var result = new List<WellMasterDataViewModel>();

                if (string.IsNullOrEmpty(userId))
                {
                    result = db.WellRegister.Select(x => new WellMasterDataViewModel
                    {
                        wellId = x.well_id,
                        wellName = x.wellname
                    }).ToList();
                }
                else
                {
                    result = (from uw in db.UsersWells
                              join wr in db.WellRegister on uw.WellId equals wr.well_id
                              where uw.UserId == userId
                              select new WellMasterDataViewModel
                              {
                                  wellId = uw.WellId,
                                  wellName = wr.wellname
                              }).ToList();
                }

                return Task.FromResult(result);
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "ICustomerProfile GetUserAssignedWells", null);
                return null;
            }
        }
        public Task<List<WellMasterDataViewModel>> GetUserAssignedRigs(string userId)
        {
            try
            {
                var result = new List<WellMasterDataViewModel>();

                if (string.IsNullOrEmpty(userId))
                {
                    result = db.rig_register.Select(x => new WellMasterDataViewModel
                    {
                        rigID = x.Rig_id,
                        rigName = x.Rig_Name
                    }).ToList();
                }
                else
                {
                    result = (from uw in db.UserRigs
                              join wr in db.rig_register on uw.RigID equals wr.Rig_id
                              where uw.UserId == userId
                              select new WellMasterDataViewModel
                              {
                                  rigID = uw.RigID,
                                  rigName = wr.Rig_Name
                              }).ToList();
                }

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "ICustomerProfile GetUserAssignedRigs", null);
                return null;
            }
        }
        //public async Task<Tuple<List<CustomerProfileModel>, int>> GetCustomerProfiles(int accountType, int pageNumber, int pageSize)
        //{
        //    try
        //    {
        //        int skip = (pageNumber - 1) * Convert.ToInt32(pageSize);
        //        var total = (from cp in db.CorporateProfile
        //                     join stname in db.USAStates on cp.State equals stname.StateId.ToString()
        //                     join cub in db.CrmUserBasicDetail on cp.UserId equals cub.UserId
        //                     where cub.AccountType == accountType
        //                     select new { cp }).Count();
        //        List<CustomerProfileModel> result = await (from cp in db.CorporateProfile
        //                                                   join stname in db.USAStates on Convert.ToInt32(cp.State) equals stname.StateId
        //                                                   join cub in db.CrmUserBasicDetail on cp.UserId equals cub.UserId
        //                                                   where cub.AccountType == accountType
        //                                                   select new CustomerProfileModel
        //                                                   {
        //                                                       Address1 = cp.Address1,
        //                                                       Address2 = cp.Address2,
        //                                                       City = cp.City,
        //                                                       State = stname.Name,
        //                                                       Country = cp.Country,
        //                                                       Name = cp.Name,
        //                                                       Phone = cp.Phone,
        //                                                       ID = cp.ID,
        //                                                       TenantId = cp.TenantId,
        //                                                       Zip = cp.Zip,
        //                                                       LogoPath = cp.LogoPath,
        //                                                       Website = cp.Website,
        //                                                       ContactName = cub.Company
        //                                                   }
        //                            ).Skip(skip).Take(pageSize).ToListAsync();

        //        return new Tuple<List<CustomerProfileModel>, int>(result, total);
        //    }
        //    catch (Exception ex)
        //    {
        //        CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
        //        customErrorHandler.WriteError(ex, "ICustomerProfile GetCustomerProfiles", null);
        //        return null;
        //    }
        //}

        public async Task<Tuple<List<CustomerProfileModel>, int>> GetCustomerProfiles(int accountType, int pageNumber, int pageSize)
        {
            try
            {
                int skip;
                var total=0;
                List<CustomerProfileModel> result= new List<CustomerProfileModel>();
                if (accountType==1)
                {

                     skip = (pageNumber - 1) * Convert.ToInt32(pageSize);
                     total = (from cp in db.CorporateProfile
                                 join stname in db.USAStates on cp.State equals stname.StateId.ToString()
                                 join cub in db.CrmUserBasicDetail on cp.UserId equals cub.UserId
                                 where cub.AccountType == 1 || cub.AccountType == 4
                                 //where cub.AccountType.ToString().Contains(accountType.ToString())
                                 select new { cp }).Count();
                result = await (from cp in db.CorporateProfile
                                                               join stname in db.USAStates on Convert.ToInt32(cp.State) equals stname.StateId
                                                               join cub in db.CrmUserBasicDetail on cp.UserId equals cub.UserId
                                                               where cub.AccountType == 1 || cub.AccountType == 4
                                                               select new CustomerProfileModel
                                                               {
                                                                   Address1 = cp.Address1,
                                                                   Address2 = cp.Address2,
                                                                   City = cp.City,
                                                                   State = stname.Name,
                                                                   Country = cp.Country,
                                                                   Name = cp.Name,
                                                                   Phone = cp.Phone,
                                                                   ID = cp.ID,
                                                                   TenantId = cp.TenantId,
                                                                   Zip = cp.Zip,
                                                                   LogoPath = cp.LogoPath,
                                                                   Website = cp.Website,
                                                                   ContactName = cub.Company
                                                               }
                                        ).ToListAsync();


                }
               else if (accountType == 0)
                { 
                     skip = (pageNumber - 1) * Convert.ToInt32(pageSize);
                 total = (from cp in db.CorporateProfile
                             join stname in db.USAStates on cp.State equals stname.StateId.ToString()
                             join cub in db.CrmUserBasicDetail on cp.UserId equals cub.UserId
                             //where cub.AccountType == accountType
                             where cub.AccountType == 0 || cub.AccountType == 3
                             select new { cp }).Count();
               result = await (from cp in db.CorporateProfile
                                                           join stname in db.USAStates on Convert.ToInt32(cp.State) equals stname.StateId
                                                           join cub in db.CrmUserBasicDetail on cp.UserId equals cub.UserId
                                                           where cub.AccountType == 0 || cub.AccountType == 3
                                                           select new CustomerProfileModel
                                                           {
                                                               Address1 = cp.Address1,
                                                               Address2 = cp.Address2,
                                                               City = cp.City,
                                                               State = stname.Name,
                                                               Country = cp.Country,
                                                               Name = cp.Name,
                                                               Phone = cp.Phone,
                                                               ID = cp.ID,
                                                               TenantId = cp.TenantId,
                                                               Zip = cp.Zip,
                                                               LogoPath = cp.LogoPath,
                                                               Website = cp.Website,
                                                               ContactName = cub.Company
                                                           }
                                    ).ToListAsync();

                

                }
                else if (accountType == 2)
                {

                     skip = (pageNumber - 1) * Convert.ToInt32(pageSize);
                     total = (from cp in db.CorporateProfile
                                 join stname in db.USAStates on cp.State equals stname.StateId.ToString()
                                 join cub in db.CrmUserBasicDetail on cp.UserId equals cub.UserId
                                 //where cub.AccountType == accountType
                                 where cub.AccountType == 2 
                                 select new { cp }).Count();
                    result = await (from cp in db.CorporateProfile
                                                               join stname in db.USAStates on Convert.ToInt32(cp.State) equals stname.StateId
                                                               join cub in db.CrmUserBasicDetail on cp.UserId equals cub.UserId
                                                               where cub.AccountType == 2
                                                               select new CustomerProfileModel
                                                               {
                                                                   Address1 = cp.Address1,
                                                                   Address2 = cp.Address2,
                                                                   City = cp.City,
                                                                   State = stname.Name,
                                                                   Country = cp.Country,
                                                                   Name = cp.Name,
                                                                   Phone = cp.Phone,
                                                                   ID = cp.ID,
                                                                   TenantId = cp.TenantId,
                                                                   Zip = cp.Zip,
                                                                   LogoPath = cp.LogoPath,
                                                                   Website = cp.Website,
                                                                   ContactName = cub.Company
                                                               }
                                        ).ToListAsync();

              
                }

                return new Tuple<List<CustomerProfileModel>, int>(result, total);

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "ICustomerProfile GetCustomerProfiles", null);
                return null;
            }
        }


        public async Task<CustomerProfileModel> GetCustomerDetail(string id)
        {
            try
            {
                CustomerProfileModel result = await (from cp in db.CorporateProfile
                                                     join stname in db.USAStates on Convert.ToInt32(cp.State) equals stname.StateId
                                                     join crm in db.CrmUserBasicDetail on cp.UserId equals crm.UserId
                                                     join custSub in db.Subscription on cp.TenantId equals custSub.TenantId into custSubLJ
                                                     from custSub in custSubLJ.DefaultIfEmpty()
                                                     join sub in db.SubscriptionPackage on custSub.PackageId equals sub.PackageId.ToString() into subLj
                                                     from sub in subLj.DefaultIfEmpty()
                                                     orderby custSub.SubStartdate descending
                                                     where cp.TenantId == id
                                                     select new CustomerProfileModel
                                                     {
                                                         Address1 = cp.Address1,
                                                         Address2 = cp.Address2,
                                                         City = cp.City,
                                                         State = stname.Name,
                                                         Country = cp.Country,
                                                         Name = cp.Name,
                                                         Phone = cp.Phone,
                                                         ContactName=crm.Name,
                                                         ID = cp.ID,
                                                         TenantId = cp.TenantId,
                                                         Zip = cp.Zip,
                                                         LogoPath = cp.LogoPath,
                                                         Website = cp.Website,
                                                         AccountType=Convert.ToInt32(crm.AccountType),
                                                         SubscriptionId = custSub == null ? "" :custSub.SubscriptionId,
                                                         IsEnableSubscription = custSub == null ? "Deactivate" :  (custSub.IsEnable==true? "Activate" : "Deactivate"),
                                                         SubscriptionName = sub == null ? null : sub.PackageName,
                                                         SubscriptionDescription = sub == null ? "N/A" : sub.AccountTypeDescription,
                                                         SubscriptionPrice = sub == null ? "00" : sub.PackageAmount,
                                                         SubscriptionRigsCount = custSub == null ? 0 : custSub.SubscriptionCount,
                                                         SubscriptionTotalAmount = custSub == null ? 00 : (double)custSub.PackageAmount,
                                                         SubscriptionEnd = (DateTime?)null,
                                                         SubscriptionStart = custSub != null ? custSub.SubStartdate.Value.Date : (DateTime?)null,
                                                         PackageOrder=sub.PackageOrder
                                                     }
                                ).FirstOrDefaultAsync();
                var subscriptionPkg= JsonConvert.SerializeObject(db.SubscriptionPackage.Where(x => x.AccountType == (result.AccountType==0?2:1)).OrderBy(x=>x.PackageOrder).ToList());
                result.SubscriptionPackages=JsonConvert.DeserializeObject<List<Model.Administration.SubscriptionPackage>>(subscriptionPkg);
                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "ICustomerProfile GetCustomerDetail", null);
                return null;
            }
            
        }
        public async Task<Tuple<List<CustomerUsersModel>, int>> GetCustomerUsers(string tenantId, int pageNumber, int pageSize)
        {
            try
            {
                int skip = (pageNumber - 1) * Convert.ToInt32(pageSize);
                var total = _userManager.Users.Where(x => x.TenantId == tenantId).Count();
                var result = await (from u in _userManager.Users
                                    join tu in db.TenantUsers on u.Id equals tu.UserId
                                    join crm in db.CrmUserBasicDetail on u.Id equals crm.UserId into crmLJ
                                    from crm in crmLJ.DefaultIfEmpty()
                                    where tu.TenantId == tenantId
                                    select new CustomerUsersModel
                                    {
                                        UserID = u.Id,
                                        PhoneNumber = u.PhoneNumber,
                                        Email = u.Email,
                                        FirstName = u.FirstName,
                                        MiddleName = u.MiddleName,
                                        FullName = @$"{u.FirstName} {u.MiddleName} {u.LastName}",
                                        LastName = u.LastName,
                                        Mobile = u.Mobile,
                                        JobTitle = u.JobTitle,
                                        Address = u.Address,
                                        City = u.City,
                                        State = u.State,
                                        Zip = u.Zip,
                                        AccountType = crm == null ? 0 : (int)crm.AccountType,
                                        IsPrimary = u.Primary.HasValue ? u.Primary.Value : false,
                                        AdditionalNotes = u.AdditionalNotes,
                                        WellOfficeUser = u.WellUser.HasValue ? u.WellUser.Value : false,
                                        Field = u.Field.HasValue ? u.Field.Value : false,
                                        IsActive = crm == null ? false : crm.IsActive,
                                        IsMaster = crm == null ? false : crm.IsMaster,
                                        ProfileImageName = u.ProfileImageName,
                                        UserTenantId = u.TenantId,
                                        //}).Skip(skip).Take(pageSize).ToListAsync();
                                    }).ToListAsync();

                return new Tuple<List<CustomerUsersModel>, int>(result, total);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "ICustomerProfile GetCustomerUsers", null);
                return null;
            }
        }     
        public async Task<CustomerUsersModel> CustomerUserDetailById(string userId)
        {
            try
            {
                var result = await (from user in _userManager.Users
                                    join crm in db.CrmUserBasicDetail on user.Id equals crm.UserId into crmLJ
                                    from crm in crmLJ.DefaultIfEmpty()
                                    where user.Id == userId
                                    select new CustomerUsersModel
                                    {
                                        UserID = user.Id,
                                        PhoneNumber = user.PhoneNumber,
                                        Email = user.Email,
                                        FirstName = user.FirstName,
                                        MiddleName = user.MiddleName,
                                        LastName = user.LastName,
                                        FullName = @$"{user.FirstName} {user.MiddleName} {user.LastName}",
                                        Mobile = user.Mobile,
                                        JobTitle = user.JobTitle,
                                        Address = user.Address,
                                        City = user.City,
                                        State = user.State,
                                        Zip = user.Zip,
                                        IsPrimary = user.Primary.HasValue ? user.Primary.Value : false,
                                        AdditionalNotes = user.AdditionalNotes,
                                        WellOfficeUser = user.WellUser.HasValue ? user.WellUser.Value : false,
                                        Field = user.Field.HasValue ? user.Field.Value : false,
                                        IsActive = crm == null ? false : crm.IsActive,
                                        IsMaster = crm == null ? false : crm.IsMaster,
                                        UserTenantId = user.TenantId
                                    }).FirstOrDefaultAsync();
                
                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "ICustomerProfile CustomerUserDetailById", null);
                return null;
            }
        }
        public Task<List<IdentityRole>> GetRoles(string tenantId)
        {
            try
            {
                var roles = (from r in _roleManager.Roles
                             join tr in db.TenantRoles on r.Id equals tr.RoleId
                             where tr.TenantId == tenantId
                             select r).ToList();
                //var roles = _roleManager.Roles.ToList();
                return Task.FromResult(roles);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "ICustomerProfile GetRoles", null);
                return null;
            }
        }
        public async Task<int> AssignWellsToUser(string userId, List<string> wellIds)
        {
            try
            {
                var AIBusiness = new AIBuiness(db, _roleManager, _userManager);
                return await AIBusiness.AssignWellsToUser(userId, wellIds);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "ICustomerProfile AssignWellsToUser", null);
                return 0;
            }
        }
        public async Task<int> AssignRigsToUser(string userId, List<string> wellIds)
        {
            try
            {
                var AIBusiness = new AIBuiness(db, _roleManager, _userManager);
                return await AIBusiness.AssignRigsToUser(userId, wellIds);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "ICustomerProfile AssignRigsToUser", null);
                return 0;
            }
        }
        public Task<string> UpdateSubscriptioIsEnable(string SubscriptionId)
        {
            try
            {
                var result = db.Subscription.Where(x => (x.SubscriptionId)== SubscriptionId).FirstOrDefault();
                string IsEnableSubscription = "Deactivate";
                if (result != null)
                {
                    if (!result.IsEnable) 
                    {
                        IsEnableSubscription = "Activate";
                    }
                    result.IsEnable = !result.IsEnable;
                    db.SaveChanges();
                }
                return Task.FromResult(IsEnableSubscription);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "ICustomerProfile UpdateSubscriptioIsEnable", null);
                return null;
            }
            
        }
        public async Task<List<BillingHistory>> GetCustomerInvoiceHistory(string tenantId)
        {
            try
            {
                List<BillingHistory> result = null;
                Guid tenantid = new Guid(tenantId);
               result = (from his in db.BillingHistoryInvoices
                          join ptype in db.PaymentType on his.PayMethod equals ptype.ID
                          where his.TenantId == tenantId
                          select new BillingHistory { Invoice = his.Invoice, BillDate = his.BillDate, Name = his.Name, Amount = his.Amount, PayMethod = ptype.Name }
                            ).ToList();
               return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "ICustomerProfile GetCustomerInvoiceHistory", null);
                return Task.FromResult(new List<BillingHistory>()).Result;
            }
            
        }
        public Task<Tuple<List<WellMasterDataViewModel>, int>> GetCustomerWellData(string tenantId, int pageNumber, int pageSize)
        {
            try
            {
                // int skip = (pageNumber - 1) * Convert.ToInt32(pageSize);

                var groupList = GetRIGAIGroup();
                var total = db.WellRegister.Where(x => x.customer_id == tenantId).Count();

                var wellMaster = (from wel in db.WellRegister
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
                                      Basin_ID = new BasinTypeModel { Basin_ID = basinresult.Basin_ID, BasinType_name = basinresult.BasinType_name }

                                  }).ToList();

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
                                            RigRelease = wel.RigRelease,//DWOP
                                            SpudWell = wel.SpudWell,
                                            Lastboptest = wel.Lastboptest,
                                            NextbopTest = wel.NextbopTest,
                                            PlannedTd = wel.PlannedTd,
                                            chartColor = wel.chartColor
                                        }
                                       ).ToList();

                return Task.FromResult(new Tuple<List<WellMasterDataViewModel>, int>(wellMasterResult, total));
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "ICustomerProfile GetCustomerWellData", null);
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
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "ICustomerProfile GetRIGAIGroup", null);
                return null;
            }
        }
        public async Task<WellMasterDataViewModel> UpdateCustomerWellData(string tenantId, WellMasterDataViewModel input)
        {
            try
            {
                byte BatchFlag;
                if (input.batchFlag)
                {
                    BatchFlag = 1;
                }
                else
                {
                    BatchFlag = 0;
                }
                var well = db.WellRegister.Where(x => x.well_id == input.wellId).FirstOrDefault();
                well.wellname = input.wellName;
                well.welltype_id = input.wellTypeId.wellTypeId;
                well.County = input.county;
                well.customer_id = tenantId;
                well.Conplete_well_drill = input.complete_well_drill == true ? Convert.ToByte(1) : Convert.ToByte(0);
                well.Batch_drill_casing = input.batch_drill_casing == true ? Convert.ToByte(1) : Convert.ToByte(0);
                well.Batch_drill_horizontal = input.batch_drill_horizontal == true ? Convert.ToByte(1) : Convert.ToByte(0);
                well.Casing_string = input.casing_string == true ? Convert.ToByte(1) : Convert.ToByte(0);
                well.NumAPI = input.numAPI;
                well.NumAFE = input.numAFE;
                well.RigID = input.rigID;
                well.PadID = input.padID;
                well.BatchFlag = BatchFlag;
                well.CasingString = input.casingString;
                well.Latitude = input.latitude;
                well.Longitude = input.longitude;
                well.FieldName = input.fieldName;
                well.BatchDrillingTypeID = input.batchDrillingTypeId;
                well.State = input.state;
                well.Basin = input.Basin_ID.Basin_ID;                
                well.Prediction = true;
                //DWOP
                well.ChecklistTemplateId = input.ChecklistTemplateId;

                if (input.Prediction == true && input.OldPredictionForUpdate != input.Prediction)
                {
                    well.StartDate = DateTime.Now.Date;
                }
                else
                {
                    well.StartDate = null;
                }
                //well.Prediction = input.Prediction;
                if (!string.IsNullOrEmpty(input.chartColor))
                    well.ChartColor = input.chartColor;
                input.chartColor = well.ChartColor;
                await db.SaveChangesAsync();
                return input;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "ICustomerProfile UpdateCustomerWellData", null);
                return null;
            }
        }
        public async Task<WellMasterDataViewModel> CreateCustomerWellData(string tenantId, WellMasterDataViewModel input)
        {
            try
            {
                byte BatchFlag;
                WellRegister well = new WellRegister();
                if (input.wellId == null)
                {
                    if (input.batchFlag)
                        BatchFlag = 1;
                    else
                        BatchFlag = 0;

                    well.well_id = Guid.NewGuid().ToString();
                    well.wellname = input.wellName;
                    well.welltype_id = input.wellTypeId.wellTypeId;
                    well.County = input.county;
                    well.customer_id = tenantId;
                    well.Conplete_well_drill = input.complete_well_drill == true ? Convert.ToByte(1) : Convert.ToByte(0);
                    well.Batch_drill_casing = input.batch_drill_casing == true ? Convert.ToByte(1) : Convert.ToByte(0);
                    well.Batch_drill_horizontal = input.batch_drill_horizontal == true ? Convert.ToByte(1) : Convert.ToByte(0);
                    well.Casing_string = input.casing_string == true ? Convert.ToByte(1) : Convert.ToByte(0);
                    well.NumAPI = input.numAPI;
                    well.NumAFE = input.numAFE;

                    well.RigID = input.rigID;
                    well.PadID = input.padID;
                    well.BatchFlag = BatchFlag;
                    well.CasingString = input.casingString;
                    well.Latitude = input.latitude;
                    well.Longitude = input.longitude;
                    well.FieldName = input.fieldName;
                    well.BatchDrillingTypeID = input.batchDrillingTypeId;
                    well.State = input.state;
                    well.Basin = input.Basin_ID.Basin_ID;                   
                    well.Prediction = true;
                    //DWOP
                    well.ChecklistTemplateId = input.ChecklistTemplateId;

                    if (input.Prediction == true)
                    {
                        well.StartDate = DateTime.Now.Date;
                    }
                    else
                    {
                        well.StartDate = null;
                    }
                    //well.Prediction = input.Prediction;
                    well.ChartColor = input.chartColor;
                    db.WellRegister.Add(well);
                    await db.SaveChangesAsync();
                }
                return input;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "ICustomerProfile CreateCustomerWellData", null);
                return null;
            }
        }
        public async Task<bool> WellDataDestroy(string wellId)
        {
            try
            {
                bool IsRemove = false;
                var itemToDelete = (from wl in db.WellRegister
                                    where wl.well_id == wellId
                                    select wl).SingleOrDefault();

                if (itemToDelete != null)
                {
                    db.WellRegister.Remove(itemToDelete);
                    await db.SaveChangesAsync();
                    IsRemove = true;
                }
                return IsRemove;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "ICustomerProfile WellDataDestroy", null);
                return false;
            }
        }
        public async Task<List<RigModel>> GetCustomerRigs(string tenantId)
        {
            try
            {
                var total = db.rig_register.Where(x => x.TenantID == tenantId && x.isActive == true).Count();
                var result = await (from rig in db.rig_register
                                    where rig.TenantID == tenantId && rig.isActive == true
                                    select new WellAI.Advisor.Model.OperatingCompany.Models.RigModel
                                    {
                                        Rig_id = rig.Rig_id,
                                        Rig_Name = rig.Rig_Name,
                                        Latitude = rig.Latitude,
                                        Longitude = rig.Longitude,
                                        TenantID = rig.TenantID
                                    }).ToListAsync(); ;
                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "ICustomerProfile GetCustomerRigs", null);
                return null;
            }
        }
        public async Task<RigModel> UpdateCustomerRigs(RigModel input)
        {
            try
            {
                Rig_register rig = new Rig_register();
                rig.Rig_id = input.Rig_id;
                rig.Rig_Name = input.Rig_Name;
                rig.Latitude = input.Latitude.Value;
                rig.Longitude = input.Longitude.Value;
                rig.TenantID = input.TenantID;
                rig.isActive = input.isActive;
                db.rig_register.Update(rig);
                await db.SaveChangesAsync();
                return input;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "ICustomerProfile UpdateCustomerRigs", null);
                return null;
            }
        }
        public async Task<RigModel> CreateCustomerRigs(RigModel input)
        {
            try
            {
                bool IsActive = true;
                Rig_register rig = new Rig_register();

                rig.Rig_id = Guid.NewGuid().ToString();
                rig.Rig_Name = input.Rig_Name;
                rig.Latitude = input.Latitude.Value;
                rig.Longitude = input.Longitude.Value;
                rig.TenantID = input.TenantID;
                rig.isActive = IsActive;
                db.rig_register.Add(rig);
                await db.SaveChangesAsync();
                return input;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "ICustomerProfile CreateCustomerRigs", null);
                return null;
            }
        }

        public async Task<List<PadModel>> GetCustomePads(string tenantId)
        {
            try
            {
                //int skip = (pageNumber - 1) * Convert.ToInt32(pageSize);
                var total = db.pad_register.Where(x => x.TenantID == tenantId && x.isActive == true).Count();
                var result = await (from rig in db.pad_register
                                    where rig.TenantID == tenantId && rig.isActive == true
                                    select new WellAI.Advisor.Model.OperatingCompany.Models.PadModel
                                    {
                                        Pad_id = rig.Pad_id,
                                        Pad_Name = rig.Pad_Name,
                                        Latitude = rig.Latitude,
                                        Longitude = rig.Longitude,
                                        TenantID = rig.TenantID
                                    }).ToListAsync(); ;

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "ICustomerProfile GetCustomePads", null);
                return null;
            }
        }

       

        public async Task<PadModel> CreateCustomerPads(PadModel input)
        {
            try
            {
                Pad_register pad = new Pad_register();
                pad.Pad_id = Guid.NewGuid().ToString();
                pad.Pad_Name = input.Pad_Name;
                pad.Latitude = input.Latitude.Value;
                pad.Longitude = input.Longitude.Value;
                pad.TenantID = input.TenantID;
                pad.isActive = true;
                db.pad_register.Add(pad);
                await db.SaveChangesAsync();
                return input;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "ICustomerProfile CreateCustomerPads", null);
                return null;
            }
        }
        public async Task<PadModel> UpdateCustomerPads(PadModel input)
        {
            try
            {
                Pad_register pad = new Pad_register();
                pad.Pad_id = input.Pad_id;
                pad.Pad_Name = input.Pad_Name;
                pad.Latitude = input.Latitude.Value;
                pad.Longitude = input.Longitude.Value;
                pad.TenantID = input.TenantID;
                pad.isActive = input.isActive;
                db.pad_register.Update(pad);
                await db.SaveChangesAsync();
                return input;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "ICustomerProfile UpdateCustomerPads", null);
                return null;
            }
        }

        public Task<List<ComponentModelRec>> GetAllPermittedComponents(int accounttype)
        {
            try
            {
                List<ComponentModelRec> result = new List<ComponentModelRec>();
                if (accounttype == 1)
                {
                    result = (from c in db.Components
                              where c.AccountType == accounttype
                              select new ComponentModelRec
                              {
                                  ComponentId = c.ComponentId,
                                  ComponentName = c.Label,
                                  IsPermitted = c.IsActive
                              }).ToList();
                }
                else
                {
                    result = (from c in db.Components
                              where c.SrvAccountType == accounttype
                              select new ComponentModelRec
                              {
                                  ComponentId = c.ComponentId,
                                  ComponentName = c.Label,
                                  IsPermitted = c.IsActive
                              }).ToList();
                }
                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "ICustomerProfile GetAllPermittedComponents", null);
                return null;
            }
        }

        public Task<List<RolePermissionModel>> GetRolePermissions(string tenantId)
        {
            try
            {
                var result = (from rp in db.RolePermissions
                              where rp.TenantId == tenantId
                              select new RolePermissionModel
                              {
                                  PermissionId = rp.RolePermissionId,
                                  PermissionName = rp.RolePermissionName,
                                  RolePermissionComponent = (from rpc in db.RolePermissionComponentLinks
                                                             join c in db.Components on rpc.ComponentId equals c.ComponentId
                                                             where rpc.RolePermissionId == rp.RolePermissionId
                                                             select new RolePermissionComponentModel
                                                             {
                                                                 ComponentId = c.ComponentId,
                                                                 ComponentName = c.Label,
                                                                 IsPermitted = rpc.IsPermitted
                                                             }).ToList()
                              }).ToList();
                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "ICustomerProfile GetRolePermissions", null);
                return null;
            }
        }

        public async Task<bool> CreatePermissionComponents(string permissionName, List<RolePermissionComponentModel> actualPermComps, string userName, string tenatId)
        {
            var result = false;

            try
            {
                if (db.RolePermissions.FirstOrDefault(x => x.RolePermissionName == permissionName && x.TenantId==tenatId) == null)// permission with same name not exists
                {
                    var newperm = db.RolePermissions.Add(new RolePermissions
                    {
                        RolePermissionName = permissionName,
                        IsActive = true,
                        TenantId = tenatId,
                        CreatedBy = userName,
                        ModifiedBy = userName,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    });
                    await db.SaveChangesAsync();
                }
                else
                    return false;

                var permission = db.RolePermissions.FirstOrDefault(x => x.RolePermissionName == permissionName && x.TenantId == tenatId);
                var oldRoleComps = db.RolePermissionComponentLinks.Where(x => x.RolePermissionId == permission.RolePermissionId).ToList(); // all components that were before current updates
                var components = db.Components;

                for (int i = 0; i < actualPermComps.Count; i++)
                {
                    var odlcomponent = components.First(x => x.Label == actualPermComps[i].ComponentName);
                    var oldpermcomp = oldRoleComps.FirstOrDefault(x => x.ComponentId == actualPermComps[i].ComponentId);
                    
                    if (oldpermcomp != null)
                    {
                        oldpermcomp.IsPermitted = true;
                    }
                    else
                    {
                        db.RolePermissionComponentLinks.Add(new RolePermissionComponentLinks
                        {
                            RolePermissionId = permission.RolePermissionId,
                            ComponentId = odlcomponent.ComponentId,
                            IsPermitted = true
                        });
                    }
                }

                await db.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "ICustomerProfile CreatePermissionComponents", null);
                result = false;
            }

            return result;
        }

        public async Task<bool> UpdatePermissionComponents(int permissionId, string permissionName, List<RolePermissionComponentModel> actualPermComps)
        {
            var result = false;

            try
            {
                var role = db.RolePermissions.FirstOrDefault(x => x.RolePermissionId == permissionId);
                if (role.RolePermissionName != permissionName)
                {
                    if (db.RolePermissions.FirstOrDefault(x => x.RolePermissionName == permissionName) == null)// no role exists with the same name
                        role.RolePermissionName = permissionName;
                }

                var components = db.Components;

                var oldRoleComps = db.RolePermissionComponentLinks.Where(x => x.RolePermissionId == permissionId).ToList(); // all components that were before current updates

                for (int i = 0; i < actualPermComps.Count; i++)
                {
                    var odlcomponent = components.First(x => x.Label == actualPermComps[i].ComponentName);
                    var oldpermcomp = oldRoleComps.FirstOrDefault(x => x.ComponentId == odlcomponent.ComponentId);

                    if (oldpermcomp != null)
                    {
                        oldpermcomp.IsPermitted = actualPermComps[i].IsPermitted;
                    }
                    else
                    {
                        db.RolePermissionComponentLinks.Add(new RolePermissionComponentLinks
                        {
                            RolePermissionId = permissionId,
                            ComponentId = odlcomponent.ComponentId,
                            IsPermitted = actualPermComps[i].IsPermitted
                        });
                    }
                }

                await db.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "ICustomerProfile UpdatePermissionComponents", null);
            }

            return result;
        }

        public async Task<bool> DeletePermission(int permissionId)
        {
            bool result = true;
            try
            {
                var rolePermissionComponentLink = db.RolePermissionComponentLinks.Where(x => x.RolePermissionId == permissionId).ToList();

                if (rolePermissionComponentLink != null)
                {
                    db.RolePermissionComponentLinks.RemoveRange(rolePermissionComponentLink);
                    await db.SaveChangesAsync();
                }

                var rolePermission = db.RolePermissions.FirstOrDefault(x => x.RolePermissionId == permissionId);
                if (rolePermission != null)
                {
                    db.RolePermissions.Remove(rolePermission);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "ICustomerProfile DeletePermission", null);
            }
            return result;
        }

        public Task<List<RoleModel>> GetRoleWithRolePermissions(string tenantId)
        {
            try
            {
                var roles = (from r in _roleManager.Roles
                             join tr in db.TenantRoles on r.Id equals tr.RoleId
                             where tr.TenantId == tenantId && r.Name != "Operator" && r.Name != "Service Provider"
                             select new RoleModel
                             {
                                 Id = r.Id,
                                 RoleName = r.Name,
                                 RolePermissions = (from rpl in db.RolePermissionLinks
                                                    join rp in db.RolePermissions on rpl.RolePermissionId equals rp.RolePermissionId
                                                    where rpl.RoleId == r.Id
                                                    select new RolePermissionModel
                                                    {
                                                        PermissionId = rp.RolePermissionId,
                                                        PermissionName = rp.RolePermissionName,
                                                        IsPermitted = rpl.IsPermitted
                                                    }).ToList()
                             }).ToList();

                return Task.FromResult(roles);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "ICustomerProfile GetRoleWithRolePermissions", null);
                return null;
            }
        }

        public Task<List<RolePermissions>> GetAllPermissions(string tenantId)
        {
            try
            {
                return Task.FromResult(db.RolePermissions.Where(x => x.TenantId == tenantId).ToList());
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "ICustomerProfile GetAllPermissions", null);
                return null;
            }
        }

        public async Task<bool> CreateRolePermissions(string roleName, List<RolePermissions> actualRolePerms, string userName, string tenantId)
        {
            var result = false;

            try
            {
                var exists = (from r in db.Roles
                              join rt in db.TenantRoles on r.Id equals rt.RoleId
                              where r.Name.ToLower() == roleName.ToLower() && rt.TenantId == tenantId
                              select r).FirstOrDefault();

                if (exists == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole
                    {
                        Name = roleName,
                        NormalizedName = roleName.ToUpper()
                    });
                }
                else
                    return false;

                var role = (from r in db.Roles
                            where r.Name.ToLower() == roleName.ToLower()
                            select r).FirstOrDefault();

                db.TenantRoles.Add(new TenantRoles { RoleId = role.Id, TenantId = tenantId });
                db.SaveChanges();

                var oldRolePermissions = db.RolePermissionLinks.Where(x => x.RoleId == role.Id).ToList();
                var permissions = db.RolePermissions;

                for (int i = 0; i < actualRolePerms.Count; i++)
                {
                    var odlPermission = permissions.First(x => x.RolePermissionName == actualRolePerms[i].RolePermissionName);
                    var oldroleperm = oldRolePermissions.FirstOrDefault(x => x.RolePermissionId == odlPermission.RolePermissionId);

                    if (oldroleperm != null)
                    {
                        oldroleperm.IsPermitted = actualRolePerms[i].IsActive;
                    }
                    else
                    {
                        db.RolePermissionLinks.Add(new RolePermissionLinks
                        {
                            RoleId = role.Id,
                            RolePermissionId = odlPermission.RolePermissionId,
                            IsPermitted = actualRolePerms[i].IsActive
                        });
                    }
                }

                await db.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "ICustomerProfile CreateRolePermissions", null);
                result = false;
            }

            return result;
        }

        public async Task<bool> UpdateRolePermissions(string roleId, string roleName, List<RolePermissions> actualRolePerms)
        {
            var result = false;

            try
            {
                var role = db.Roles.FirstOrDefault(x => x.Id == roleId);
                if (role.Name != roleName)
                {
                    if (db.Roles.FirstOrDefault(x => x.Name == roleName) == null) 
                    {
                        role.Name = roleName;
                        role.NormalizedName = roleName.ToUpper();
                        await _roleManager.UpdateAsync(role);
                    }
                }

                var permissions = db.RolePermissions;

                var oldRolePermissions = db.RolePermissionLinks.Where(x => x.RoleId == roleId).ToList();

                for (int i = 0; i < actualRolePerms.Count; i++)
                {
                    var odlPermission = permissions.First(x => x.RolePermissionName == actualRolePerms[i].RolePermissionName);
                    var oldroleperm = oldRolePermissions.FirstOrDefault(x => x.RolePermissionId == odlPermission.RolePermissionId);

                    if (oldroleperm != null)
                    {
                        oldroleperm.IsPermitted = actualRolePerms[i].IsActive;
                    }
                    else
                    {
                        db.RolePermissionLinks.Add(new RolePermissionLinks
                        {
                            RoleId = roleId,
                            RolePermissionId = odlPermission.RolePermissionId,
                            IsPermitted = actualRolePerms[i].IsActive
                        });
                    }
                }

                await db.SaveChangesAsync();

                result = true;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "ICustomerProfile UpdateRolePermissions", null);
                result = false;
            }

            return result;
        }

        public async Task<bool> DeleteRolePermission(string roleId)
        {
            bool result = true;
            try
            {
                var rolePermissionLinksList = db.RolePermissionLinks.Where(x => x.RoleId == roleId).ToList();

                if (rolePermissionLinksList != null)
                {
                    db.RolePermissionLinks.RemoveRange(rolePermissionLinksList);
                    await db.SaveChangesAsync();
                }
                var role = await _roleManager.FindByIdAsync(roleId);
                await _roleManager.DeleteAsync(role);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "ICustomerProfile DeleteRolePermission", null);
            }
            return result;
        }
    }
    
    public static class SubscriptionPackage
    {

        public static int GetSubscriptionPackageYear(int PackageOrder)
        {
            List<Properties> subscriptionDuration = new List<Properties>();
            subscriptionDuration.Add(new Properties { Month = 12, PackageOrder = 6 });
            subscriptionDuration.Add(new Properties { Month = 3, PackageOrder = 5 });
            subscriptionDuration.Add(new Properties { Month = 1, PackageOrder = 4 });
            subscriptionDuration.Add(new Properties { Month = 12, PackageOrder = 3 });
            subscriptionDuration.Add(new Properties { Month = 3, PackageOrder = 2 });
            subscriptionDuration.Add(new Properties { Month = 1, PackageOrder = 1 });
            return subscriptionDuration.Where(x => x.PackageOrder == PackageOrder).FirstOrDefault().Month;
        }
    }
    class Properties
    {
        public int PackageOrder { get; set; }
        public int Month { get; set; }
    }
}
