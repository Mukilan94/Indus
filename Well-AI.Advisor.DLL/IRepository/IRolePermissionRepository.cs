using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.Model.ServiceCompany.Models;

namespace WellAI.Advisor.DLL.Repository
{
    public interface IRolePermissionRepository
    {
        Task<bool> CreateRolePermission(RolePermissions rolePermission, List<RolePermissionComponentLinks> rolePermissionComponentLinks);
        Task<bool> CreateRolePermissionLink(List<RolePermissionLinks> rolePermissionLinks);
        Task<List<RoleModel>> GetRoles(string tenantId);
        Task<List<RoleSRVModel>> GetSRVRoles();
        Task<List<RolePermissions>> GetAllPermissions(string tenantId);

        List<RolePermissionModel> GetRolePermissions(string tenantId);
        List<RolePermissionSRVModel> GetSRVRolePermissions(string tenantId);

        List<ComponentModelRec> GetComponentsBasedOnRole(string roleId);
        Task<List<ComponentModelRec>> GetComponentsBasedOnRoles(List<string> roleId);
        Task<List<ComponentModelRec>> GetAllPermittedComponents();
        Task<List<ComponentSRVModelRec>> GetAllPermittedComponentsSRV();
        List<ComponentSRVModelRec> GetSRVComponentsBasedOnRole(string roleId);
        Task<List<ComponentSRVModelRec>> GetSRVComponentsBasedOnRoles(List<string> roleIds);

        Task<bool> UpdatePermissionSRVComponents(int permissionId, string permissionName, List<RolePermissionComponentSRVModel> actualPermComps);
        Task<bool> CreatePermissionSRVComponents(string permissionName, List<RolePermissionComponentSRVModel> actualPermComps, string userName, string tenantId);

        Task<bool> UpdatePermissionComponents(int permissionId, string permissionName, List<RolePermissionComponentModel> actualPermComps);
        Task<bool> CreatePermissionComponents(string permissionName, List<RolePermissionComponentModel> actualPermComps, string userName, string tenatId);
        //Phase II Changes - TenantId passed
        Task<bool> UpdateRolePermissions(string roleId, string roleName, List<RolePermissions> actualRolePerms, string tenantId);
        Task<bool> CreateRolePermissions(string roleName, List<RolePermissions> actualRolePerms, string userName, string companyId);
        IdentityRole GetRoleByName(string roleName);
        Task<List<IdentityRole>> GetRolesByNames(List<string> roleNames);
        bool GetComponentBasedOnRole(string roleId, string componentName);
        bool GetSRVComponentBasedOnRole(string roleId, string componentName);

        //Phase II Changes - Permission fixes
        bool GetComponentBasedOnRolesList(List<string> roleIds, string componentName,string TenantId);
        bool GetSRVComponentBasedOnRolesList(List<string> roleIds, string componentName, string TenantId);

        Task<bool> DeleteRolePermission(string roleId);
        Task<bool> DeletePermission(int permissionId);
        List<ComponentModelRec> GetAllCoomponentForMaster(string tenantId);
    }
}
