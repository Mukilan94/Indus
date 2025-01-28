using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using System.Threading.Tasks;
using WellAI.Advisor.BLL.IBusiness;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.ServiceCompany.Models;
using Newtonsoft.Json;
using WellAI.Advisor.DLL.Entity;
using Well_AI.Advisor.API.Samsara.Models;
using WellAI.Advisor.Model.OperatingCompany.Models;
using ServiceVehicleViewModel = WellAI.Advisor.Model.ServiceCompany.Models.ServiceVehicleViewModel;
using WellAI.Advisor.BLL.Administration;

namespace WellAI.Advisor.BLL.Business
{
    public class ServiceVehicleBusiness : IServiceVehicleBusiness
    {
        private readonly WebAIAdvisorContext db;
        UserManager<WellIdentityUser> _userManager;

        public ServiceVehicleBusiness(WebAIAdvisorContext db, UserManager<WellIdentityUser> userManager)
        {
            this.db = db;
            _userManager = userManager;

        }

        public Task<List<ServiceVehicleViewModel>> GetActiveTechnicianAndProjectByTenantId(string tenantId)
        {
            try
            {
                var output = (from tech in db.ProjectTechnicians
                              join proj in db.Projects on tech.ProjectId equals proj.ID
                              join veh in db.ServiceVehicles on tech.ServiceVehicleId equals veh.Id
                              join user in _userManager.Users on tech.TechnitionId equals user.Id
                              join corp in db.CorporateProfile on proj.OprTenantID equals corp.TenantId
                              join well in db.WELL_REGISTERs on proj.WellID equals well.well_id into welllj
                              from well in welllj.DefaultIfEmpty()
                              join rig in db.RigRegisters on well.RigID equals rig.Rig_Id into riglj
                              from rig in riglj.DefaultIfEmpty()
                              where tech.ProjectTechStatus == 1 && proj.ServiceCompID == tenantId && proj.ProjectStatus == (int)ProjectStatusList.OnGoingProjects
                              select new ServiceVehicleViewModel()
                              { Id = veh.Id, 
                                  TechnicianName = $"{user.FirstName} {user.LastName}", 
                                  TechnicianId=user.Id,
                                  OperatorName=corp.Name,
                                  ProjectName = proj.ProjectTitle, 
                                  ProjectId = proj.ID,
                                  RigName = rig == null ? "No Rig" : rig.Rig_Name,
                                  Samaaraid = veh.Samsaraid
                                  
                              }).ToList();
                List<ServiceVehicleViewModel> results = JsonConvert.DeserializeObject<List<ServiceVehicleViewModel>>(JsonConvert.SerializeObject(output));
                return Task.FromResult(results);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ServiceVechicle GetActiveTechnicianAndProjectByTenantId", null);
                List<ServiceVehicleViewModel> list = new List<ServiceVehicleViewModel>();
                return Task.FromResult(list);
            }
           
        }

        public Task<List<ServiceVehicleViewModel>> GetActiveTechnicianAndProjectByOprTenantId(WellIdentityUser userwell, string wellId)
        {
            try
            {
                List<ServiceVehicleViewModel> output = null;

                var checkwellFilter = wellId == DLL.Constants.NoSpecificWellFilterKey;

                var temp = (from tech in db.ProjectTechnicians
                            join proj in db.Projects on tech.ProjectId equals proj.ID
                            join veh in db.ServiceVehicles on tech.ServiceVehicleId equals veh.Id
                            join user in _userManager.Users on tech.TechnitionId equals user.Id
                            where proj.OprTenantID == userwell.TenantId && proj.ProjectStatus == (int)ProjectStatusList.OnGoingProjects
                            && (proj.WellID == wellId && !checkwellFilter || checkwellFilter)
                            select new ServiceVehicleViewModel()
                            {
                                Id = veh.Id,
                                TechnicianName = $"{user.FirstName} {user.LastName}",
                                ProjectName = proj.ProjectTitle,
                                Samaaraid = veh.Samsaraid,
                                WellId = proj.WellID
                            }).ToList();

                if (userwell != null && userwell.WellUser.HasValue && userwell.WellUser.Value)
                {
                    var userwellIds = db.UsersWells.Where(x => x.UserId == userwell.Id).Select(x => x.WellId).ToList();

                    output = temp.Where(x => userwellIds.FirstOrDefault(y => y == x.WellId) != null).ToList();
                }
                else
                    output = temp;

                List<ServiceVehicleViewModel> results = JsonConvert.DeserializeObject<List<ServiceVehicleViewModel>>(JsonConvert.SerializeObject(output));
                return Task.FromResult(results);
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ServiceVechicle GetActiveTechnicianAndProjectByTenantId", null);
                return null;
            }
        }

        public Task<List<ServiceVehicleViewModel>> GetActiveTechnicianByProjectId(WellIdentityUser userwell, string projectId)
        {
            try
            {
                List<ServiceVehicleViewModel> output = null;


                var temp = (from tech in db.ProjectTechnicians
                            join proj in db.Projects on tech.ProjectId equals proj.ID
                            join veh in db.ServiceVehicles on tech.ServiceVehicleId equals veh.Id
                            join user in _userManager.Users on tech.TechnitionId equals user.Id
                            where proj.OprTenantID == userwell.TenantId && proj.ProjectStatus == (int)ProjectStatusList.OnGoingProjects
                            && (proj.ID == projectId)
                            select new ServiceVehicleViewModel()
                            {
                                Id = veh.Id,
                                TechnicianName = $"{user.FirstName} {user.LastName}",
                                TechnicianId = user.Id,
                                ProjectName = proj.ProjectTitle,
                                Samaaraid = veh.Samsaraid,
                                WellId = proj.WellID
                            }).ToList();

                if (userwell != null && userwell.WellUser.HasValue && userwell.WellUser.Value)
                {
                    var userwellIds = db.UsersWells.Where(x => x.UserId == userwell.Id).Select(x => x.WellId).ToList();
                    output = temp.Where(x => userwellIds.FirstOrDefault(y => y == x.WellId) != null).ToList();
                    output = temp;

                }
                else
                    output = temp;

                List<ServiceVehicleViewModel> results = JsonConvert.DeserializeObject<List<ServiceVehicleViewModel>>(JsonConvert.SerializeObject(output));
                return Task.FromResult(results);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ServiceVechicle GetActiveTechnicianByProjectId", null);
                return null;
            }
        }

        public Task<Model.ServiceCompany.Models.VechicleLocation> GetLatLngOfVehicleBySamsaraId(string SamsaraId)
        {

            throw new NotImplementedException();
        }

        public Task<List<ServiceVehicleViewModel>> GetServiceVehicles(string tenantId)
        {
            try
            {
                var output = (from veh in db.ServiceVehicles
                              where veh.TenantId == tenantId
                              select veh).ToList();
                List<ServiceVehicleViewModel> results = JsonConvert.DeserializeObject<List<ServiceVehicleViewModel>>(JsonConvert.SerializeObject(output));
                return Task.FromResult(results);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ServiceVechicle GetServiceVehicles", null);
                List<ServiceVehicleViewModel> results2 = new List<ServiceVehicleViewModel>();
                return Task.FromResult(results2);
            }

        }
        public Task<List<ServiceVehicleViewModel>> GetServiceVehiclesList(string tenantId)
        {
            try
            {
                var output = (from veh in db.ServiceVehicles
                              where veh.TenantId == tenantId
                              select new ServiceVehicleViewModel() { Id = veh.Id, VehicleName = veh.VehicleName }).ToList();
                List<ServiceVehicleViewModel> results = JsonConvert.DeserializeObject<List<ServiceVehicleViewModel>>(JsonConvert.SerializeObject(output));
                return Task.FromResult(results);
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ServiceVechicle GetServiceVehiclesList", null);
                return null;
            }
        }

        public Task<bool> UpdateSamsaraData(VechicleModel vehicles, string authorId, string tenantId)
        {
            try
            {
                List<string> samsaraId = new List<string>();
                var campareSamsaraid = db.ServiceVehicles.Where(x => x.TenantId == tenantId).Select(x => x.Samsaraid.ToString()).ToList();
                var inputresult = (from v in vehicles.data
                                   where !campareSamsaraid.Contains(v.id)
                                   select new ServiceVehicle()
                                   {
                                       Id = Guid.NewGuid().ToString(),
                                       AuthorId = authorId,
                                       TenantId = tenantId,
                                       Samsaraid = v.id,
                                       VehicleName = v.name,
                                       LicensePlate = v.licensePlate,
                                       Make = v.make,
                                       Model = v.model,
                                       Serial = v.serial,
                                       Vin = v.vin,
                                       Year = v.year
                                   }).ToList();
                if (inputresult.Count > 0)
                {
                    foreach (var samsara in inputresult)
                    {
                        if(samsara.TenantId != null && samsara.VehicleName != null && samsara.AuthorId != null && samsara.Serial != null && samsara.Make != null && samsara.Model != null && samsara.Year != null && samsara.Vin != null && samsara.Samsaraid != null)
                        {
                            db.ServiceVehicles.AddRange(samsara);
                            db.SaveChanges();
                        }                      
                    }
                }
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "ServiceVechicle UpdateSamsaraData", null);
                return null;
            }
        }

        public Task<TechnicianTracker> GetTechnicianTracker(string ProjectId)
        {
            return null;
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
