using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.DLL.Repository;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.ServiceCompany.Models;
using WellAI.Advisor.Model.Identity;

namespace WellAI.Advisor.BLL.Business
{
    public class RolePermissionBusiness : IRolePermissionBusiness
    {
        private readonly WebAIAdvisorContext db;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        public RolePermissionBusiness(WebAIAdvisorContext db, RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager)
        {
            this.db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public List<RolePermissionModel> GetRolePermissions(string tenantId)
        {
            IRolePermissionRepository repostirory = new RolePermissionRepository(db, _roleManager, _userManager);
            return repostirory.GetRolePermissions(tenantId);
        }

        public async Task<List<RoleModel>> GetRoles(string tenantId)
        {
            IRolePermissionRepository repostirory = new RolePermissionRepository(db, _roleManager, _userManager);
            return await repostirory.GetRoles(tenantId);
        }

        public async Task<List<RolePermissions>> GetAllPermissions(string tenantId)
        {
            IRolePermissionRepository repository = new RolePermissionRepository(db, _roleManager, _userManager);
            return await repository.GetAllPermissions(tenantId);
        }

        public IdentityRole GetRoleByName(string roleName)
        {
            IRolePermissionRepository repostirory = new RolePermissionRepository(db, _roleManager, _userManager);
            return repostirory.GetRoleByName(roleName);
        }

        public async Task<List<IdentityRole>> GetRolesByNames(List<string> roleNames)
        {
            IRolePermissionRepository repostirory = new RolePermissionRepository(db, _roleManager, _userManager);
            return await repostirory.GetRolesByNames(roleNames);
        }

        public List<ComponentModelRec> GetComponentsBasedOnRole(string roleId)
        {
            IRolePermissionRepository repostirory = new RolePermissionRepository(db, _roleManager, _userManager);
            return repostirory.GetComponentsBasedOnRole(roleId);
        }
        public async Task<List<ComponentModelRec>> GetComponentsBasedOnRoles(List<string> roleIds)
        {
            IRolePermissionRepository repostirory = new RolePermissionRepository(db, _roleManager, _userManager);
            return await repostirory.GetComponentsBasedOnRoles(roleIds);
        }

        public async Task<List<ComponentModelRec>> GetAllPermittedComponents()
        {
            IRolePermissionRepository repostirory = new RolePermissionRepository(db, _roleManager, _userManager);
            return await repostirory.GetAllPermittedComponents();
        }

        public bool GetComponentBasedOnRole(string roleId, string componentName)
        {
            IRolePermissionRepository repostirory = new RolePermissionRepository(db, _roleManager, _userManager);
            return repostirory.GetComponentBasedOnRole(roleId, componentName);
        }

        public async Task<bool> UpdatePermissionComponents(int permissionId, string permissionName, List<RolePermissionComponentModel> actualPermComps)
        {
            IRolePermissionRepository repostirory = new RolePermissionRepository(db, _roleManager, _userManager);

            return await repostirory.UpdatePermissionComponents(permissionId, permissionName, actualPermComps);
        }

        public async Task<bool> CreatePermissionComponents(string permissionName, List<RolePermissionComponentModel> actualPermComps, string userName, string tenatId)
        {
            IRolePermissionRepository repostirory = new RolePermissionRepository(db, _roleManager, _userManager);

            return await repostirory.CreatePermissionComponents(permissionName, actualPermComps, userName,tenatId);
        }
        //Phase II Changes - TenantId passed
        public async Task<bool> UpdateRolePermissions(string roleId, string roleName, List<RolePermissions> actualRolePerms, string tenantId)
        {
            IRolePermissionRepository repository = new RolePermissionRepository(db, _roleManager, _userManager);

            return await repository.UpdateRolePermissions(roleId, roleName, actualRolePerms, tenantId);
        }

        public async Task<bool> CreateRolePermissions(string roleName, List<RolePermissions> actualRolePerms, string userName, string companyId)
        {
            IRolePermissionRepository repository = new RolePermissionRepository(db, _roleManager, _userManager);

            return await repository.CreateRolePermissions(roleName, actualRolePerms, userName, companyId);
        }

        public List<RolePermissionSRVModel> GetSRVRolePermissions(string tenatId)
        {
            IRolePermissionRepository repostirory = new RolePermissionRepository(db, _roleManager, _userManager);
            return repostirory.GetSRVRolePermissions( tenatId);
        }

        public async Task<List<RoleSRVModel>> GetSRVRoles()
        {
            IRolePermissionRepository repostirory = new RolePermissionRepository(db, _roleManager, _userManager);
            return await repostirory.GetSRVRoles();
        }

        public List<ComponentSRVModelRec> GetSRVComponentsBasedOnRole(string roleId)
        {
            IRolePermissionRepository repostirory = new RolePermissionRepository(db, _roleManager, _userManager);
            return repostirory.GetSRVComponentsBasedOnRole(roleId);
        }

        public async Task<List<ComponentSRVModelRec>> GetAllPermittedComponentsSRV()
        {
            IRolePermissionRepository repostirory = new RolePermissionRepository(db, _roleManager, _userManager);
            return await repostirory.GetAllPermittedComponentsSRV();
        }


        public async Task<bool> UpdatePermissionSRVComponents(int permissionId, string permissionName, List<RolePermissionComponentSRVModel> actualPermComps)
        {
            IRolePermissionRepository repostirory = new RolePermissionRepository(db, _roleManager, _userManager);

            return await repostirory.UpdatePermissionSRVComponents(permissionId, permissionName, actualPermComps);
        }

        public async Task<bool> CreatePermissionSRVComponents(string permissionName, List<RolePermissionComponentSRVModel> actualPermComps, string userName, string tenatId)
        {
            IRolePermissionRepository repostirory = new RolePermissionRepository(db, _roleManager, _userManager);

            return await repostirory.CreatePermissionSRVComponents(permissionName, actualPermComps, userName, tenatId);
        }



        public bool GetSRVComponentBasedOnRole(string roleId, string componentName)
        {
            IRolePermissionRepository repostirory = new RolePermissionRepository(db, _roleManager, _userManager);
            return repostirory.GetSRVComponentBasedOnRole(roleId, componentName);
        }

        public List<ComponentSRVModelRec> GetSrvComponentsBasedOnRole(string roleId)
        {
            IRolePermissionRepository repostirory = new RolePermissionRepository(db, _roleManager, _userManager);
            return repostirory.GetSRVComponentsBasedOnRole(roleId);
        }

        public async Task<List<ComponentSRVModelRec>> GetSrvComponentsBasedOnRoles(List<string> roleIds)
        {
            IRolePermissionRepository repostirory = new RolePermissionRepository(db, _roleManager, _userManager);
            return await repostirory.GetSRVComponentsBasedOnRoles(roleIds);
        }

        public async Task<bool> DeleteRoles(string roleId)
        {
            IRolePermissionRepository repostirory = new RolePermissionRepository(db, _roleManager, _userManager);
            return await repostirory.DeleteRolePermission(roleId);
        }

        public async Task<bool> DeletePermissions(int permissionId)
        {
            IRolePermissionRepository repostirory = new RolePermissionRepository(db, _roleManager, _userManager);
            return await repostirory.DeletePermission(permissionId);
        }

        public List<ComponentModelRec> GetAllComponentForMaster(string tenatId)
        {
            IRolePermissionRepository repostirory = new RolePermissionRepository(db, _roleManager, _userManager);
            return repostirory.GetAllCoomponentForMaster(tenatId);
        }
        //Phase II Changes - Permission fixes
        public bool GetComponentBasedOnRolesList(List<string> roleIds, string componentName, string TenantId)
        {
            IRolePermissionRepository repostirory = new RolePermissionRepository(db, _roleManager, _userManager);
            return repostirory.GetComponentBasedOnRolesList(roleIds, componentName, TenantId);
        }

        public bool GetSRVComponentBasedOnRolesList(List<string> roleIds, string componentName,string TenantId)//,string TenantId
        {
            IRolePermissionRepository repostirory = new RolePermissionRepository(db, _roleManager, _userManager);
            return repostirory.GetSRVComponentBasedOnRolesList(roleIds, componentName, TenantId);//,TenantId
        }
    }
}
