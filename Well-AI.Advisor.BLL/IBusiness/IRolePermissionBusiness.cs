using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.Model.ServiceCompany.Models;


namespace WellAI.Advisor.BLL.Business
{
    public interface IRolePermissionBusiness
    {
        List<RolePermissionModel> GetRolePermissions(string tenantId);
        Task<List<RoleModel>> GetRoles(string tenantId);
        List<ComponentModelRec> GetComponentsBasedOnRole(string roleId);
        Task<List<ComponentModelRec>> GetComponentsBasedOnRoles(List<string> roleIds);
        List<ComponentSRVModelRec> GetSrvComponentsBasedOnRole(string roleId);
        Task<List<ComponentSRVModelRec>> GetSrvComponentsBasedOnRoles(List<string> roleIds);
        Task<List<ComponentModelRec>> GetAllPermittedComponents();

        Task<List<ComponentSRVModelRec>> GetAllPermittedComponentsSRV();
        List<RolePermissionSRVModel> GetSRVRolePermissions(string tenantId);

        Task<List<RoleSRVModel>> GetSRVRoles();
        List<ComponentSRVModelRec> GetSRVComponentsBasedOnRole(string roleId);
        Task<List<RolePermissions>> GetAllPermissions(string tenantId);
      
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
        bool GetComponentBasedOnRolesList(List<string> roleIds, string componentName, string TenantId);
        bool GetSRVComponentBasedOnRolesList(List<string> roleIds, string componentName, string TenantId);

        Task<bool> UpdatePermissionSRVComponents(int permissionId, string permissionName, List<RolePermissionComponentSRVModel> actualPermComps);
        
        Task<bool> CreatePermissionSRVComponents(string permissionName, List<RolePermissionComponentSRVModel> actualPermComps, string userName, string tenatId);

        Task<bool> DeleteRoles(string roleId);
        Task<bool> DeletePermissions(int permissionId);
        List<ComponentModelRec> GetAllComponentForMaster(string tenatId);
    }
}
