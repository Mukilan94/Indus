using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.OperatingCompany.Models;

namespace WellAI.Advisor
{
    public class WellFilterViewComponent : ViewComponent
    {
        private readonly SignInManager<WellIdentityUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        private readonly WebAIAdvisorContext db;

        public WellFilterViewComponent(SignInManager<WellIdentityUser> signInManager, RoleManager<IdentityRole> roleManager,
            UserManager<WellIdentityUser> userManager, WebAIAdvisorContext dbContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            db = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var model = new WellFilterLayoutViewModel();

                var controller = ((string)RouteData.Values["controller"]).ToLower();

                if (controller != "operatingdashboard" && controller != "indepthrigdata" && controller != "upcomingprojects" &&
                   controller != "activityview" && controller != "activedrillplan" && controller != "projectauctions" && controller != "welldata" && controller != "drillingplan")
                {
                    return View("_WellFilter", model);
                }

                if (_signInManager.IsSignedIn(HttpContext.User))
                {
                    var wells = await GetRigDataAsync();

                    model.Wells = wells;

                    var wellIdCookie = Request.Cookies["wellfilterlayout"];
                    var wellId = string.IsNullOrEmpty(wellIdCookie) ? "" : wellIdCookie.ToString();
                    if (string.IsNullOrEmpty(wellId) || wellId == DLL.Constants.NoSpecificWellFilterKey || wells.FirstOrDefault(x => x.WellId == wellId) == null)
                        model.SelectedWellId = model.Wells.First().WellId;
                    else
                        model.SelectedWellId = wellId;
                }

                return View("_WellFilter", await Task.FromResult(model));
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, null, Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "WellFilterViewComponent InvokeAsync", User.Identity.Name);
                return null;
            }
        }

        private async Task<List<WellViewModel>> GetWellsDataAsync()
        {
            try
            {
                var userId = WellAIAppContext.Current.Session.GetString(DLL.Constants.SessionNotExpireKey);
                var userWells = db.UsersWells.Where(x => x.UserId == userId).ToList();

                var wells = new List<WellViewModel>();

                if (userWells.Count > 0)
                {
                    wells = (from uwell in userWells
                             join wellreg in db.WellRegister on uwell.WellId equals wellreg.well_id
                             select new WellViewModel
                             {
                                 Name = wellreg.wellname,
                                 WellId = wellreg.well_id
                             }).OrderBy(x => x.Name).ToList();
                }
                else
                {
                    var operId = WellAIAppContext.Current.Session.GetString("TenantId");
                    wells = db.WellRegister.Where(x => x.customer_id == operId)
                        .Select(x => new WellViewModel
                        {
                            Name = x.wellname,
                            WellId = x.well_id
                        }).OrderBy(x => x.Name).ToList();
                }

                for (var i = 0; i < wells.Count; i++)
                {
                    var curdepthItem = db.WellDepthDataStages.FirstOrDefault(x => x.WID == wells[i].WellId);

                    wells[i].Name += ", Depth: " + (curdepthItem == null ? "N/A" : (curdepthItem.DMEA.HasValue ? String.Format("{0:0.##}", curdepthItem.DMEA.Value) : "N/A"));
                }

                if (userWells.Count == 0)
                {
                    wells.Insert(0, new WellViewModel { WellId = DLL.Constants.NoSpecificWellFilterKey, Name = "All Wells" });
                }

                return await Task.FromResult(wells);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, null, Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "WellFilterViewComponent GetWellsDataAsync", User.Identity.Name);
                return null;
            }
        }

        private async Task<List<WellViewModel>> GetRigDataAsync()
        {
            try
            {
                var userId = WellAIAppContext.Current.Session.GetString(DLL.Constants.SessionNotExpireKey);
                var userRigs = db.UserRigs.Where(x => x.UserId == userId).ToList();

                var Rigs = new List<WellViewModel>();

                if (userRigs.Count > 0)
                {
                    Rigs = (from uwell in userRigs
                            join Rigreg in db.rig_register on uwell.RigID equals Rigreg.Rig_id
                            where Rigreg.isActive == true
                            select new WellViewModel
                            {
                                Name = Rigreg.Rig_Name,
                                //WellId = wellreg.well_id
                                WellId = Rigreg.Rig_id
                            }).OrderBy(x => x.Name).ToList();
                }
                else
                {
                    var operId = WellAIAppContext.Current.Session.GetString("TenantId");
                    Rigs = db.rig_register.Where(x => x.TenantID == operId && x.isActive == true)
                        .Select(x => new WellViewModel
                        {
                            Name = x.Rig_Name,
                            WellId = x.Rig_id
                        }).OrderBy(x => x.Name).ToList();
                }

                if (userRigs.Count == 0 || userRigs.Count > 0)
                {
                    Rigs.Insert(0, new WellViewModel { WellId = DLL.Constants.NoSpecificWellFilterKey, Name = "All Rigs" });
                }

                return await Task.FromResult(Rigs);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, null, Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "WellFilterViewComponent GetRigDataAsync", User.Identity.Name);
                return null;
            }
        }
    }
}
