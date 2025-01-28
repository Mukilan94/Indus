using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.ServiceCompany.Models;
using WellAI.Advisor.Model.Identity;
using Microsoft.AspNetCore.Http;

namespace WellAI.Advisor.DLL.Repository
{
    public class RolePermissionRepository : IRolePermissionRepository
    {
        private readonly WebAIAdvisorContext db;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        public RolePermissionRepository(WebAIAdvisorContext db, RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager)
        {
            this.db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public async Task<bool> CreateRolePermission(RolePermissions rolePermission, List<RolePermissionComponentLinks> rolePermissionComponentLinks)
        {
            try
            {
                db.RolePermissions.Add(rolePermission);
                db.SaveChanges();

                db.RolePermissionComponentLinks.AddRange(rolePermissionComponentLinks);
                db.SaveChanges();
                return await Task.FromResult(true);
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "RolePermission CreateRolePermission", null);
                return false;
            }
            
        }

        public async Task<bool> CreateRolePermissionLink(List<RolePermissionLinks> rolePermissionLinks)
        {
            try
            {
                db.RolePermissionLinks.AddRange(rolePermissionLinks);
                db.SaveChanges();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "RolePermission CreateRolePermissionLink", null);
                return false;
            }
        }

        public IdentityRole GetRoleByName(string roleName)
        {
            return db.Roles.Where(x => x.Name == roleName).FirstOrDefault();
        }

        public Task<List<IdentityRole>> GetRolesByNames(List<string> roleNames)
        {
            return Task.FromResult(db.Roles.Where(x => roleNames.Contains(x.Name)).ToList());
        }

        public Task<List<RoleModel>> GetRoles(string tenantId)
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
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "RolePermission GetRoles", null);
                return null;
            }
        }

        public Task<List<RolePermissions>> GetAllPermissions(string tenantId)
        {
            return Task.FromResult(db.RolePermissions.Where(x => x.TenantId == tenantId).ToList());
        }

        public List<RolePermissionModel> GetRolePermissions(string tenantId)
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

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "RolePermission GetRolePermissions", null);
                return null;
            }
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

                var oldRoleComps = db.RolePermissionComponentLinks.Where(x => x.RolePermissionId == permissionId ).ToList(); // all components that were before current updates

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
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "RolePermission UpdatePermissionComponents", null);
            }

            return result;
        }
        public async Task<bool> CreatePermissionComponents(string permissionName, List<RolePermissionComponentModel> actualPermComps, string userName, string tenatId)
        {
            var result = false;

            try
            {
                if (db.RolePermissions.FirstOrDefault(x => x.RolePermissionName == permissionName && x.TenantId == tenatId) == null)// permission with same name not exists
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
                        oldpermcomp.IsPermitted = actualPermComps[i].IsPermitted;
                    }
                    else
                    {
                        db.RolePermissionComponentLinks.Add(new RolePermissionComponentLinks
                        {
                            RolePermissionId = permission.RolePermissionId,
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
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "RolePermission CreatePermissionComponents", null);
                result = false;
            }

            return result;
        }

        //Phase II Changes - TenantId passed
        public async Task<bool> UpdateRolePermissions(string roleId, string roleName, List<RolePermissions> actualRolePerms, string tenantId)
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
                //Phase II Changes - TenantId Passed
                var permissions = db.RolePermissions.Where(x => x.TenantId == tenantId);


                List<int> oldRolePermissionsList = (from permlink in db.RolePermissionLinks
                                          join perm in permissions on permlink.RolePermissionId equals perm.RolePermissionId
                                          where permlink.RoleId == roleId && perm.TenantId == tenantId
                                          select 
                                          Convert.ToInt32(permlink.RolePermissionLinkId)
                                         ).ToList();

                var oldRolePermissions = db.RolePermissionLinks.Where(x => oldRolePermissionsList.Contains(x.RolePermissionLinkId)).ToList();

                for (int i = 0; i < actualRolePerms.Count; i++)
                {
                    if (permissions != null)
                    {

                    }
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
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "RolePermission UpdateRolePermissions", null);
                result = false;
            }

            return result;
        }

        public async Task<bool> CreateRolePermissions(string roleName, List<RolePermissions> actualRolePerms, string userName, string companyId)
        {
            var result = false;

            try
            {
                var exists = (from r in db.Roles
                              join rt in db.TenantRoles on r.Id equals rt.RoleId
                              where r.Name.ToLower() == roleName.ToLower() && rt.TenantId == companyId
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

                if (string.IsNullOrWhiteSpace(companyId))
                    companyId = Guid.Empty.ToString();

                db.TenantRoles.Add(new TenantRoles { RoleId = role.Id, TenantId = companyId });
                db.SaveChanges();

                var oldRolePermissions = db.RolePermissionLinks.Where(x => x.RoleId == role.Id).ToList();
                var permissions = db.RolePermissions.Where(x=>x.TenantId == companyId);

                for (int i = 0; i < actualRolePerms.Count; i++)
                {
                    var odlPermission = permissions.First(x => x.RolePermissionName == actualRolePerms[i].RolePermissionName);
                    var oldroleperm = oldRolePermissions.FirstOrDefault(x => x.RolePermissionId == odlPermission.RolePermissionId);

                    if (oldroleperm != null)
                    {
                        oldroleperm.IsPermitted = actualRolePerms[i].IsActive;
                    }
                    else// new permission in role
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
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "RolePermission CreateRolePermissions", null);
                result = false;
            }

            return result;
        }

        public List<ComponentModelRec> GetComponentsBasedOnRole(string roleId)
        {
            try
            {
                var result = (from rpl in db.RolePermissionLinks
                              join rp in db.RolePermissions on rpl.RolePermissionId equals rp.RolePermissionId
                              join rpcl in db.RolePermissionComponentLinks on rp.RolePermissionId equals rpcl.RolePermissionId
                              join c in db.Components on rpcl.ComponentId equals c.ComponentId
                              where rpl.RoleId == roleId
                              select new ComponentModelRec
                              {
                                  ComponentId = c.ComponentId,
                                  ComponentName = c.ComponentName,
                                  IsPermitted = rpcl.IsPermitted
                              }).Distinct().ToList();
                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "RolePermission GetComponentsBasedOnRole", null);
                return null;
            }
        }

        public Task<List<ComponentModelRec>> GetComponentsBasedOnRoles(List<string> roleIds)
        {
            try
            {
                var tenantid = WellAIAppContext.Current.Session.GetString("TenantId");
                List<ComponentModelRec> result = new List<ComponentModelRec>();
                List<ComponentModelRec> resultDashboardPaermission = new List<ComponentModelRec>();
                result = (from rpl in db.RolePermissionLinks
                                         join rp in db.RolePermissions on rpl.RolePermissionId equals rp.RolePermissionId
                                         join rpcl in db.RolePermissionComponentLinks on rpl.RolePermissionId equals rpcl.RolePermissionId
                                         join cp in db.Components on rpcl.ComponentId equals cp.ComponentId
                                         where roleIds.Contains(rpl.RoleId) && rpl.IsPermitted && rp.TenantId == tenantid && (rpcl.IsPermitted == true)
                                         select new ComponentModelRec { ComponentId =  rpcl.ComponentId,ComponentName = cp.ComponentName, IsPermitted = cp.ComponentName == "ViewDashboard"?true:rpcl.IsPermitted}).ToList();

                //resultDashboardPaermission = (from rpl in db.RolePermissionLinks
                //          join rp in db.RolePermissions on rpl.RolePermissionId equals rp.RolePermissionId
                //          join rpcl in db.RolePermissionComponentLinks on rpl.RolePermissionId equals rpcl.RolePermissionId
                //          join cp in db.Components on rpcl.ComponentId equals cp.ComponentId
                //          where roleIds.Contains(rpl.RoleId) && rp.TenantId == tenantid && cp.ComponentName == "ViewDashboard"
                //           select new ComponentModelRec { ComponentId = rpcl.ComponentId, ComponentName = cp.ComponentName }).ToList();

                //result = result.Union(resultDashboardPaermission).ToList();

                

                var resultAgg = result.GroupBy(x => x.ComponentId).Select(g => g.First()).ToList();

                //var componentsIds = db.RolePermissionComponentLinks.Where(x => rolepermissionIds.Contains(x.RolePermissionId) && x.IsPermitted).Select(x => x.ComponentId).ToList();
                //result = (from x in db.Components 
                //          where rolepermissionIds.Contains(x.ComponentId)
                //          select new ComponentModelRec
                //          {
                //              ComponentId = x.ComponentId,
                //              ComponentName = x.ComponentName,
                //          }).ToList();

                return Task.FromResult(resultAgg);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "RolePermission GetComponentsBasedOnRoles", null);
                return null;
            }
        }

        public bool GetComponentBasedOnRole(string roleId, string componentName)
        {
            try
            {
                var result = (from rpl in db.RolePermissionLinks
                              join rp in db.RolePermissions on rpl.RolePermissionId equals rp.RolePermissionId
                              join rpcl in db.RolePermissionComponentLinks on rp.RolePermissionId equals rpcl.RolePermissionId
                              join c in db.Components on rpcl.ComponentId equals c.ComponentId
                              where rpl.RoleId == roleId && c.ComponentName == componentName && rpl.IsPermitted
                              select new ComponentModelRec
                              {
                                  ComponentId = c.ComponentId,
                                  ComponentName = c.ComponentName,
                                  IsPermitted = rpcl.IsPermitted
                              }).ToList(); ////Distinct().FirstOrDefault()
                //Phase II Changes -03/26/2021
                var permission = result.Where(x => x.IsPermitted == true).FirstOrDefault();
                
                if (permission != null)
                {
                    return permission.IsPermitted;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "RolePermission GetComponentBasedOnRole", null);
                return false;
            }
        }

        public Task<List<ComponentModelRec>> GetAllPermittedComponents()
        {
            try
            {
                List<ComponentModelRec> result = new List<ComponentModelRec>();
                var accounttype = 1;
                if (accounttype == 1)
                {
                    result = (from c in db.Components
                              where c.AccountType == accounttype && c.IsActive == true
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
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "RolePermission GetAllPermittedComponents", null);
                return null;
            }
        }

        public async Task<List<RoleSRVModel>> GetSRVRoles()
        {
            try
            {
              var Roles = (from r in _roleManager.Roles
                 select new RoleSRVModel
                 {
                     Id = r.Id,
                     RoleName = r.Name,
                     RolePermissions = (from rpl in db.RolePermissionLinks
                                        join rp in db.RolePermissions on rpl.RolePermissionId equals rp.RolePermissionId
                                        where rpl.RoleId == r.Id
                                        select new RolePermissionSRVModel
                                        {
                                            PermissionId = rp.RolePermissionId,
                                            PermissionName = rp.RolePermissionName,
                                            IsPermitted = rpl.IsPermitted
                                        }).ToList()
                 }).ToList();

                return await Task.FromResult(Roles); 
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "RolePermission GetSRVRoles", null);
                return null;
            }
        }

        public List<RolePermissionSRVModel> GetSRVRolePermissions(string tenantId)
        {
            try
            {
                var result = (from rp in db.RolePermissions
                              where rp.TenantId == tenantId
                              select new RolePermissionSRVModel
                              {
                                  PermissionId = rp.RolePermissionId,
                                  PermissionName = rp.RolePermissionName,
                                  RolePermissionComponent = (from rpc in db.RolePermissionComponentLinks
                                                             join c in db.Components on rpc.ComponentId equals c.ComponentId
                                                             where rpc.RolePermissionId == rp.RolePermissionId
                                                             select new RolePermissionComponentSRVModel
                                                             {
                                                                 ComponentId = c.ComponentId,
                                                                 ComponentName = c.Label,
                                                                 IsPermitted = rpc.IsPermitted
                                                             }).ToList()
                              }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "RolePermission GetSRVRolePermissions", null);
                return null;
            }
        }

        public List<ComponentSRVModelRec> GetSRVComponentsBasedOnRole(string roleId)
        {
            try
            {
                var result = (from rpl in db.RolePermissionLinks
                              join rp in db.RolePermissions on rpl.RolePermissionId equals rp.RolePermissionId
                              join rpcl in db.RolePermissionComponentLinks on rp.RolePermissionId equals rpcl.RolePermissionId
                              join c in db.Components on rpcl.ComponentId equals c.ComponentId
                              where rpl.RoleId == roleId
                              select new ComponentSRVModelRec
                              {
                                  ComponentId = c.ComponentId,
                                  ComponentName = c.ComponentName,
                                  IsPermitted = rpcl.IsPermitted
                              }).Distinct().ToList();
                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "RolePermission GetSRVComponentsBasedOnRole", null);
                return null;
            }
        }

        public Task<List<ComponentSRVModelRec>> GetSRVComponentsBasedOnRoles(List<string> roleIds)
        {
            try
            {
                List<ComponentSRVModelRec> result = new List<ComponentSRVModelRec>();
                var rolepermissionIds = db.RolePermissionLinks.Where(x => roleIds.Contains(x.RoleId)).Select(x => x.RolePermissionId).ToList();
                var componentsIds = db.RolePermissionComponentLinks.Where(x => rolepermissionIds.Contains(x.RolePermissionId) && x.IsPermitted).Select(x => x.ComponentId).ToList();
                result = (from x in db.Components
                          where componentsIds.Contains(x.ComponentId)
                          select new ComponentSRVModelRec
                          {
                              ComponentId = x.ComponentId,
                              ComponentName = x.ComponentName,
                          }).ToList();

                //var resultAgg = result.GroupBy(x => x.ComponentId).Select(g => g.First()).ToList();
                var resultAgg = result.GroupBy(x => x.ComponentId).Select(g => g.First()).ToList();
                //return Task.FromResult(result);
                return Task.FromResult(resultAgg);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "RolePermission GetSRVComponentsBasedOnRoles", null);
                return null;
            }
        }

        public bool GetSRVComponentsBasedOnRole(string roleId, string componentName)
        {
            try
            {
                var result = (from rpl in db.RolePermissionLinks
                              join rp in db.RolePermissions on rpl.RolePermissionId equals rp.RolePermissionId
                              join rpcl in db.RolePermissionComponentLinks on rp.RolePermissionId equals rpcl.RolePermissionId
                              join c in db.Components on rpcl.ComponentId equals c.ComponentId
                              where rpl.RoleId == roleId && c.ComponentName == componentName
                              select new ComponentSRVModelRec
                              {
                                  ComponentId = c.ComponentId,
                                  ComponentName = c.ComponentName,
                                  IsPermitted = rpcl.IsPermitted
                              }).Distinct().FirstOrDefault();
                if (result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "RolePermission GetSRVComponentsBasedOnRole", null);
                return false;
            }
        }


        public Task<List<ComponentSRVModelRec>> GetAllPermittedComponentsSRV()
        {
            try
            {
                List<ComponentSRVModelRec> result = new List<ComponentSRVModelRec>();
                var accounttype = 2;
                if (accounttype == 2)
                {
                    result = (from c in db.Components
                              where c.SrvAccountType == accounttype && c.IsActive == true
                              select new ComponentSRVModelRec
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
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "RolePermission GetAllPermittedComponentsSRV", null);
                return null;
            }
        }



        public async Task<bool> UpdatePermissionSRVComponents(int permissionId, string permissionName, List<RolePermissionComponentModel> actualPermComps)
        {
            var result = false;

            try
            {
                var role = db.RolePermissions.FirstOrDefault(x => x.RolePermissionId == permissionId);
                if (role.RolePermissionName != permissionName)
                {
                    if (db.RolePermissions.FirstOrDefault(x => x.RolePermissionName == permissionName) == null)
                        role.RolePermissionName = permissionName;
                }

                var components = db.Components;

                var oldRoleComps = db.RolePermissionComponentLinks.Where(x => x.RolePermissionId == permissionId).ToList(); 

                for (int i = 0; i < actualPermComps.Count; i++)
                {
                    var odlcomponent = components.First(x => x.ComponentName == actualPermComps[i].ComponentName);
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
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "RolePermission UpdatePermissionSRVComponents", null);
                result = false;
            }

            return result;
        }


        public async Task<bool> CreatePermissionSRVComponents(string permissionName, List<RolePermissionComponentSRVModel> actualPermComps, string userName, string tenantId)
        {
            var result = false;

            try
            {
                if (db.RolePermissions.FirstOrDefault(x => x.RolePermissionName.ToLower() == permissionName.ToLower() && x.TenantId == tenantId) == null)// permission with same name not exists
                {
                    var newperm = db.RolePermissions.Add(new RolePermissions
                    {
                        RolePermissionName = permissionName,
                        IsActive = true,
                        TenantId = tenantId,
                        CreatedBy = userName,
                        ModifiedBy = userName,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    });
                    await db.SaveChangesAsync();
                }
                else
                    return false;

                var permission = db.RolePermissions.FirstOrDefault(x => x.RolePermissionName == permissionName && x.TenantId == tenantId);

                var oldRoleComps = db.RolePermissionComponentLinks.Where(x => x.RolePermissionId == permission.RolePermissionId).ToList(); // all components that were before current updates
                var components = db.Components;

                for (int i = 0; i < actualPermComps.Count; i++)
                {
                    var odlcomponent = components.First(x => x.ComponentName == actualPermComps[i].ComponentName);
                    var oldpermcomp = oldRoleComps.FirstOrDefault(x => x.ComponentId == actualPermComps[i].ComponentId);

                    if (oldpermcomp != null)
                    {
                        oldpermcomp.IsPermitted = actualPermComps[i].IsPermitted;
                    }
                    else
                    {
                        db.RolePermissionComponentLinks.Add(new RolePermissionComponentLinks
                        {
                            RolePermissionId = permission.RolePermissionId,
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
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "RolePermission CreatePermissionSRVComponents", null);
                result = false;
            }

            return result;
        }


        public async Task<bool> UpdatePermissionSRVComponents(int permissionId, string permissionName, List<RolePermissionComponentSRVModel> actualPermComps)
        {
            var result = false;

            try
            {
                var rolePermissions = db.RolePermissions.FirstOrDefault(x => x.RolePermissionId == permissionId);
                if (rolePermissions.RolePermissionName != permissionName)
                {
                    if (db.RolePermissions.FirstOrDefault(x => x.RolePermissionName == permissionName && x.TenantId == rolePermissions.TenantId) == null)// no role exists with the same name
                        rolePermissions.RolePermissionName = permissionName;
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
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "RolePermission UpdatePermissionSRVComponents", null);
                result = false;
            }

            return result;
        }

        public bool GetSRVComponentBasedOnRole(string roleId, string componentName)
        {
            try
            {
                var result = (from rpl in db.RolePermissionLinks
                              join rp in db.RolePermissions on rpl.RolePermissionId equals rp.RolePermissionId
                              join rpcl in db.RolePermissionComponentLinks on rp.RolePermissionId equals rpcl.RolePermissionId
                              join c in db.Components on rpcl.ComponentId equals c.ComponentId
                              where rpl.RoleId == roleId && c.ComponentName == componentName
                              select new ComponentSRVModelRec
                              {
                                  ComponentId = c.ComponentId,
                                  ComponentName = c.ComponentName,
                                  IsPermitted = rpcl.IsPermitted
                              }).Distinct().FirstOrDefault();
                if (result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "RolePermission GetSRVComponentBasedOnRole", null);
                return false;
            }
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
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "RolePermission DeleteRolePermission", null);
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
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "RolePermission DeletePermission", null);
            }
            return result;
        }

        public List<ComponentModelRec> GetAllCoomponentForMaster(string tenantId)
        {
            try
            {
                var result = (from x in db.Components
                              where x.IsActive == true
                              select new ComponentModelRec
                              {
                                  ComponentId = x.ComponentId,
                                  ComponentName = x.ComponentName,
                                  SrvAccountType = x.SrvAccountType,
                                  AccountType = x.AccountType
                              }).ToList();


                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "RolePermission GetAllCoomponentForMaster", null);
                return null;
            }
        }

        //Phase II Changes - Permission fixes
        public bool GetComponentBasedOnRolesList(List<string> roleIds, string componentName,string TenantId)
        {
            try
            {
                var result = (from rpl in db.RolePermissionLinks
                              join rp in db.RolePermissions on rpl.RolePermissionId equals rp.RolePermissionId
                              join rpcl in db.RolePermissionComponentLinks on rp.RolePermissionId equals rpcl.RolePermissionId
                              join c in db.Components on rpcl.ComponentId equals c.ComponentId
                              where roleIds.Contains(rpl.RoleId) && c.ComponentName == componentName && rpl.IsPermitted && rp.TenantId == TenantId
                              select new ComponentModelRec
                              {
                                  ComponentId = c.ComponentId,
                                  ComponentName = c.ComponentName,
                                  IsPermitted = rpcl.IsPermitted
                              }).ToList();
                var permission = result.Where(x => x.IsPermitted == true).FirstOrDefault();

                if (permission != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "RolePermission GetComponentBasedOnRolesList", null);
                return false;
            }
        }
        //Phase II Changes - Permission fixes
        public bool GetSRVComponentBasedOnRolesList(List<string> roleIds, string componentName,string TenantId)
        {
            try
            {

                var permissionList = (from rpl in db.RolePermissionLinks
                                      join rp in db.RolePermissions on rpl.RolePermissionId equals rp.RolePermissionId
                                      where roleIds.Contains(rpl.RoleId) && rpl.IsPermitted == true && rp.TenantId == TenantId
                                      select rp).ToList();

                var result = (from pr in permissionList
                              join rpcl in db.RolePermissionComponentLinks on pr.RolePermissionId equals rpcl.RolePermissionId
                              join c in db.Components on rpcl.ComponentId equals c.ComponentId
                              where c.ComponentName == componentName
                              select new ComponentSRVModelRec
                              {
                                  ComponentId = c.ComponentId,
                                  ComponentName = c.ComponentName,
                                  IsPermitted = rpcl.IsPermitted
                              }).ToList();

                var permission = result.Where(x => x.IsPermitted == true).FirstOrDefault();

                if (permission != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "RolePermission GetSRVComponentBasedOnRolesList", null);
                return false;
            }
        }
    }
}
