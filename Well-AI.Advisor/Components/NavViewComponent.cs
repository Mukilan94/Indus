using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.Model.ServiceCompany.Models;

namespace WellAI.Advisor
{
    public class NavViewComponent : ViewComponent
    {
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<WellIdentityUser> _userManager;
        private IEnumerable<PanelBarItemModel> data4;
        private readonly WebAIAdvisorContext db;

        public NavViewComponent(SignInManager<WellIdentityUser> signInManager, RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager, WebAIAdvisorContext dbContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            db = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var controller = (string)RouteData.Values["controller"];
            var action = (string)RouteData.Values["action"];
            var area = (string)RouteData.Values["area"];

            string subscriptionModel = "";

            //subscriptionModel = "Advisor";
            //subscriptionModel = "Dispatch";
            //subscriptionModel = "AdvisorAndDispatch";
            string currentuser = WellAIAppContext.Current.Session.GetString("LoginUserName");
            string accountType = WellAIAppContext.Current.Session.GetString("AccountType");
            WellAIAppContext.Current.Session.SetString("ShareRoute", "0");
            ViewData["ShareRoute"] = false;
            ViewBag.ShareRoute = false;
            if (accountType=="2")
            {
                subscriptionModel = "Dispatch";
            }
            else if (accountType == "0" || accountType == "1")
            {
                subscriptionModel = "Advisor";
            }
            else
            {
                subscriptionModel = "AdvisorAndDispatch";
            }



            if (subscriptionModel == "Advisor")
            {
                return View("_SideNav", await GetNavigationItemsForAdvisorOnly(area, controller, action));
            }
            else if (subscriptionModel == "Dispatch")
            {
                return View("_SideNav", await GetNavigationItemsForDispatchOnly(area, controller, action));
            }
            else
            {
                //both Advisor and Dispatch
                return View("_SideNav", await GetNavigationItems(area, controller, action));
            }

        }

        //Advisor navigation items
        private async Task<IEnumerable<PanelBarItemModel>> GetNavigationItems(string area, string controller, string action)
        {
            try
            {
                if (_signInManager.IsSignedIn(HttpContext.User))
                {
                    var userIdentity = (ClaimsIdentity)User.Identity;
                    var claims = userIdentity.Claims;
                    var roleClaimType = userIdentity.RoleClaimType;
                    var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();
                    if (roles == null && roles.Count == 0)
                    {
                        List<PanelBarItemModel> data3 = new List<PanelBarItemModel>
                    {
                        new PanelBarItemModel
                        {
                            Text = "Register",
                            Url = "/identity/account/Register",
                            SpriteCssClass = "fa fa-home"
                        },
                        new PanelBarItemModel
                        {
                            Text = "Login",
                            Url = "/identity/account/Login",
                            SpriteCssClass = "fa fa-home"
                        }
                    };
                        return data3;
                    }

                    if (string.IsNullOrEmpty(controller))
                    {
                        List<PanelBarItemModel> data3 = new List<PanelBarItemModel>
                    {
                        new PanelBarItemModel
                        {
                            Text = "Register",
                            Url = "/identity/account/Register",
                            SpriteCssClass = "fa fa-home"
                        },
                        new PanelBarItemModel
                        {
                            Text = "Login",
                            Url = "/identity/account/Login",
                            SpriteCssClass = "fa fa-home"
                        }
                    };
                        return data3;
                    }

                    if (WellAIAppContext.Current.Session.GetString("AccountType1") == "0" || WellAIAppContext.Current.Session.GetString("AccountType1") == "3") //Account type 0 operator company, 3 is Operator Advisor and Dispatch
                    {
                        IRolePermissionBusiness rolePermissionBusiness = new RolePermissionBusiness(db, _roleManager, _userManager);
                        var roleNames = roles.Select(x => x.Value).ToList();
                        var rolesResult = await rolePermissionBusiness.GetRolesByNames(roleNames);

                        var roleIds = rolesResult.Select(x => x.Id).ToList();
                        List<ComponentModelRec> allcomponents = new List<ComponentModelRec>();
                        if (Convert.ToBoolean(WellAIAppContext.Current.Session.GetString("IsMaster")))
                            allcomponents = rolePermissionBusiness.GetAllComponentForMaster(WellAIAppContext.Current.Session.GetString("TenantId"));
                        else
                            allcomponents = await rolePermissionBusiness.GetComponentsBasedOnRoles(roleIds);

                        var permittedComponents = allcomponents;//.Where(x => x.IsPermitted).ToList();

                        List<PanelBarItemModel> data2 = new List<PanelBarItemModel>();
                        foreach (var item in permittedComponents)
                        {
                            if (item.ComponentName == "ViewDashboard")
                            {
                                PanelBarItemModel model = new PanelBarItemModel();
                                model.Enabled = true;
                                model.Text = "Dashboard";
                                model.Url = "/OperatingDashboard/AdvisorWithDispatch";
                                model.SpriteCssClass = "fa fa-home";
                                model.Selected = controller == "OperatingDashboard";
                                model.Id = "1";
                                data2.Add(model);
                            }
                            if (item.ComponentName == "InDepthRigData")
                            {
                                PanelBarItemModel model = new PanelBarItemModel();
                                model.Enabled = true;
                                model.Text = "Activity Advisor";
                                model.Url = "/InDepthRigData";
                                model.SpriteCssClass = "fa fa-line-chart";
                                model.Selected = controller == "InDepthRigData";
                                model.Id = "3";
                                data2.Add(model);
                            }

                            if (item.ComponentName == "ViewActivityView")
                            {
                                if (data2.Where(x => x.Text == "Schedule").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = true;
                                    parentRoot.Text = "Schedule";
                                    parentRoot.Url = "/activityview";
                                    parentRoot.SpriteCssClass = "fa fa-calendar";
                                    parentRoot.Expanded = controller.ToLower() == "activityview" || controller.ToLower() == "upcomingprojects" || controller.ToLower() == "communication";
                                    parentRoot.Selected = controller.ToLower() == "activityview";
                                    parentRoot.Id = "4";
                                    data2.Add(parentRoot);
                                }
                            }

                            if (item.ComponentName == "CheckListView")
                            {
                                if (data2.Where(x => x.Text == "Checklist").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = true;
                                    parentRoot.Text = "Drill Plan";
                                    parentRoot.Url = "/ActiveDrillPlan";
                                    parentRoot.SpriteCssClass = "fa fa-th-list";
                                    parentRoot.Expanded = controller.ToLower() == "activedrillplan";
                                    parentRoot.Selected = controller.ToLower() == "activedrillplan";
                                    parentRoot.Id = "2";
                                    data2.Add(parentRoot);
                                }
                            }


                            //Dispatch
                            if (item.ComponentName == "Dispatch")
                            {
                                if (data2.Where(x => x.Text == "Dispatch").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = true;
                                    parentRoot.Text = "Dispatch";
                                    parentRoot.Url = "/Dispatch";
                                    parentRoot.SpriteCssClass = "fa fa-truck";
                                    parentRoot.Selected = controller == "Dispatch";
                                    parentRoot.Id = "5";
                                    data2.Add(parentRoot);
                                }
                            }

                            if (item.ComponentName == "ProviderLocator" || item.ComponentName == "Provider Locator")
                            {
                                if (data2.Where(x => x.Text == "Locator").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = true;
                                    parentRoot.Text = "Provider Locator";
                                    parentRoot.Url = "/ProviderLocator";
                                    parentRoot.SpriteCssClass = "fa fa-map-marker fa-4x";
                                    parentRoot.Expanded = controller.ToLower() == "ProviderLocator";
                                    parentRoot.Selected = controller.ToLower() == "ProviderLocator";
                                    parentRoot.Id = "6";
                                    data2.Add(parentRoot);
                                }
                            }

                            if (item.ComponentName == "UpcomingProjects")
                            {
                                PanelBarItemModel model = new PanelBarItemModel();
                                model.Enabled = true;
                                model.Text = "Services";
                                model.Url = "/upcomingprojects";
                                model.SpriteCssClass = "fa fa-tasks";
                                model.Selected = controller == "upcomingprojects";
                                model.Id = "7";
                                data2.Add(model);
                            }

                            if (item.ComponentName == "ProjectAuctions")
                            {
                                if (data2.Where(x => x.Text == "Bids").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = true;
                                    parentRoot.Text = "Bids";
                                    parentRoot.Url = "/projectauctions";
                                    parentRoot.SpriteCssClass = "fa fa-gavel";
                                    parentRoot.Expanded = controller.ToLower() == "projectauctions";
                                    parentRoot.Selected = controller.ToLower() == "projectauctions";
                                    parentRoot.Id = "8";
                                    data2.Add(parentRoot);
                                }
                            }

                            if (item.ComponentName == "ViewActivityView")
                            {
                                if (data2.Where(x => x.Text == "Vendors").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = true;
                                    parentRoot.Text = "Providers";
                                    parentRoot.Url = "/providerDirectory";
                                    parentRoot.SpriteCssClass = "fa fa-building-o";
                                    parentRoot.Expanded = controller.ToLower() == "providerDirectory";
                                    parentRoot.Selected = controller == "providerDirectory";
                                    parentRoot.Id = "9";
                                    data2.Add(parentRoot);
                                }
                            }
                            if (item.ComponentName == "Connections")
                            {
                                PanelBarItemModel model = new PanelBarItemModel();
                                model.Enabled = true;
                                model.Text = "Communication";
                                model.Url = "/communication";
                                model.SpriteCssClass = "fa fa-whatsapp";
                                model.Selected = controller == "communication";
                                model.Id = "10";
                                //TU-Karthik-Commented Communication menu to test Admin
                                data2.Add(model);
                            }
                            if (item.ComponentName == "WellMetrics")
                            {
                                if (data2.Where(x => x.Text == "Register").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = true;
                                    parentRoot.Text = "Register";
                                    parentRoot.Url = "/DrillingPlan";
                                    parentRoot.SpriteCssClass = "fa fa-industry";
                                    parentRoot.Selected = controller == "DrillingPlan" || controller == "welldata" || controller == "rigs" || controller == "pad";
                                    data2.Add(parentRoot);
                                    parentRoot.Id = "11";
                                }
                            }
                            if (item.ComponentName == "DocumentManager")
                            {
                                if (data2.Where(x => x.Text == "Documents").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = true;
                                    parentRoot.Text = "Documents";
                                    parentRoot.Url = "/documentmanager";
                                    parentRoot.SpriteCssClass = "fa fa-file";
                                    parentRoot.Expanded = controller.ToLower() == "documentmanager";
                                    parentRoot.Selected = controller.ToLower() == "documentmanager";
                                    parentRoot.Id = "12";
                                    data2.Add(parentRoot);
                                }
                            }

                         


                        }

                        data2 = data2.OrderBy(x => int.Parse(x.Id)).ToList();
                        return data2;
                    }

                    if (WellAIAppContext.Current.Session.GetString("AccountType1") == "1" || WellAIAppContext.Current.Session.GetString("AccountType1") == "2" || WellAIAppContext.Current.Session.GetString("AccountType1") == "4") //Account type 1 is service type, 2 is Dispatch, 4 is Service Advisor and Dispatch
                    {                        
                        IRolePermissionBusiness rolePermissionBusiness = new RolePermissionBusiness(db, _roleManager, _userManager);
                        var roleNames = roles.Select(x => x.Value).ToList();
                        var rolesResult = await rolePermissionBusiness.GetRolesByNames(roleNames);
                        var roleIds = rolesResult.Select(x => x.Id).ToList();

                        List<ComponentModelRec> allcomponents = new List<ComponentModelRec>();

                        if (Convert.ToBoolean(WellAIAppContext.Current.Session.GetString("IsMaster")))
                            //allcomponents = rolePermissionBusiness.GetAllComponentForMaster(WellAIAppContext.Current.Session.GetString("AccountType1") == "2" ? WellAIAppContext.Current.Session.GetString("CorporateProfileId") : WellAIAppContext.Current.Session.GetString("TenantId"));
                            allcomponents = rolePermissionBusiness.GetAllComponentForMaster(WellAIAppContext.Current.Session.GetString("TenantId"));
                        else
                            allcomponents = await rolePermissionBusiness.GetComponentsBasedOnRoles(roleIds);

                        var permittedComponent = allcomponents;//Where(x => x.IsPermitted).ToList();

                        List<PanelBarItemModel> data2 = new List<PanelBarItemModel>();
                        foreach (var item in permittedComponent)
                        {
                            if (item.ComponentName == "ViewDashboard")
                            {
                                PanelBarItemModel model = new PanelBarItemModel();
                                model.Enabled = true;
                                model.Text = "Dashboard";
                                model.Url = "/ServiceDashboard/AdvisorWithDispatch";
                                model.SpriteCssClass = "fa fa-home";
                                model.Selected = controller == "ServiceDashboard";
                                model.Id = "1";
                                data2.Add(model);
                            }
                            if (item.ComponentName == "InDepthRigData")
                            {
                                if (data2.Where(x => x.Text == "InDepthRigDataSrv").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = true;
                                    parentRoot.Text = "Activity Advisor";
                                    parentRoot.Url = "/indepthrigdatasrv";
                                    parentRoot.SpriteCssClass = "fa fa-line-chart";
                                    parentRoot.Expanded = controller.ToLower() == "indepthrigdatasrv";
                                    parentRoot.Selected = controller == "indepthrigdatasrv";
                                    parentRoot.Id = "2";
                                    data2.Add(parentRoot);
                                }
                            }
                            if (item.ComponentName == "ViewActivityView")
                            {
                                if (data2.Where(x => x.Text == "Operators").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = true;
                                    parentRoot.Text = "Operator Directory";
                                    parentRoot.Url = "/OperatingDirectory";
                                    parentRoot.SpriteCssClass = "fa fa-building-o";
                                    parentRoot.Expanded = controller.ToLower() == "OperatorDirectory";
                                    parentRoot.Selected = controller == "OperatingDirectory";
                                    parentRoot.Id = "8";
                                    data2.Add(parentRoot);
                                }
                            }

                            if (item.ComponentName == "ViewActivityView")
                            {
                                if (data2.Where(x => x.Text == "Activity").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = true;
                                    parentRoot.Text = "Schedule";
                                    parentRoot.Url = "/ActivityViewSRV";
                                    parentRoot.SpriteCssClass = "fa fa-calendar";
                                    parentRoot.Expanded = controller.ToLower() == "ActivityViewSRV" || controller.ToLower() == "" || controller.ToLower() == "";
                                    parentRoot.Selected = controller == "ActivityViewSRV";
                                    parentRoot.Id = "4";
                                    data2.Add(parentRoot);
                                }
                            }

                            //Dispatch || item.ComponentName == "RouterUser"
                            if (item.ComponentName == "RouterUser")
                            {
                                if (data2.Where(x => x.Text == "Dispatch").FirstOrDefault() == null || data2.Where(x => x.Text == "RouterUser").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = true;
                                    parentRoot.Text = "Dispatch";
                                    parentRoot.Url = "/DispatchSRV";
                                    parentRoot.SpriteCssClass = "fa fa-truck";
                                    parentRoot.Selected = controller == "DispatchSRV";
                                    parentRoot.Id = "5";
                                    data2.Add(parentRoot);
                                }
                            }

                            if (item.ComponentName == "UpcomingProjects")
                            {
                                if (data2.Where(x => x.Text == "Projects").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = true;
                                    parentRoot.Text = "Services";
                                    parentRoot.Url = "/upcomingprojectssrv";
                                    parentRoot.SpriteCssClass = "fa fa-tasks";
                                    parentRoot.Expanded = controller.ToLower() == "upcomingprojectssrv";
                                    parentRoot.Selected = controller == "upcomingprojectssrv";
                                    parentRoot.Id = "6";
                                    data2.Add(parentRoot);
                                }
                            }

                            //if (item.ComponentName == "TechnicianTracker")
                            //{
                            //    if (data2.Where(x => x.Text == "Technician Tracker").FirstOrDefault() == null)
                            //    {
                            //        PanelBarItemModel parentRoot = new PanelBarItemModel();
                            //        parentRoot.Enabled = true;
                                   
                            //        parentRoot.Text = "Technician Tracker";
                            //        parentRoot.Url = "/TechnicianTrackerSRV";
                            //        parentRoot.SpriteCssClass = "fa fa-map";
                            //        parentRoot.Expanded = controller.ToLower() == "TechnicianTrackerSRV";
                            //        parentRoot.Selected = controller == "TechnicianTrackerSRV";
                            //        parentRoot.Id = "9";
                            //        data2.Add(parentRoot);
                            //    }
                            //}

                            if (item.ComponentName == "ProjectAuctions")
                            {
                                if (data2.Where(x => x.Text == "Auctions").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = true;
                                    parentRoot.Text = "Bids";
                                    parentRoot.Url = "/ProjectAuctionsSRV";
                                    parentRoot.SpriteCssClass = "fa fa-gavel";
                                    parentRoot.Expanded = controller.ToLower() == "ProjectAuctionsSRV" || controller.ToLower() == "" || controller.ToLower() == "";
                                    parentRoot.Selected = controller == "ProjectAuctionsSRV";
                                    parentRoot.Id = "7";
                                    data2.Add(parentRoot);
                                }
                            }

                            if (item.ComponentName == "WellMetrics")
                            {
                                if (data2.Where(x => x.Text == "Wells").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = true;
                                    parentRoot.Text = "Rigs";
                                    parentRoot.Url = "/welldataSRV";
                                    parentRoot.SpriteCssClass = "fa fa-industry";
                                    parentRoot.Expanded = controller.ToLower() == "welldataSRV";
                                    parentRoot.Selected = controller == "welldataSRV";
                                    parentRoot.Id = "10";
                                    data2.Add(parentRoot);
                                }
                            }

                            //if (item.ComponentName == "Fleet")
                            //{
                            //    if (data2.Where(x => x.Text == "Fleet").FirstOrDefault() == null)
                            //    {
                            //        PanelBarItemModel parentRoot = new PanelBarItemModel();
                            //        parentRoot.Enabled = true;
                            //        parentRoot.Text = "Fleet";
                            //        parentRoot.Url = "/fleetSRV";
                            //        parentRoot.SpriteCssClass = "fa fa-truck";
                            //        parentRoot.Selected = controller == "fleetSRV";
                            //        parentRoot.Id = "12";
                            //        data2.Add(parentRoot);
                            //    }
                            //}

                            ////Dispatch
                            //if (item.ComponentName == "Dispatch" || item.ComponentName == "RouterUser")
                            //{
                            //    if (data2.Where(x => x.Text == "Dispatch").FirstOrDefault() == null || data2.Where(x => x.Text == "RouterUser").FirstOrDefault() == null)
                            //    {
                            //        PanelBarItemModel parentRoot = new PanelBarItemModel();
                            //        parentRoot.Text = "Dispatch";
                            //        parentRoot.Url = "/Dispatch";
                            //        parentRoot.SpriteCssClass = "fa fa-truck";
                            //        parentRoot.Selected = controller == "Dispatch";
                            //        parentRoot.Id = "12";
                            //        data2.Add(parentRoot);
                            //    }
                            //}

                            if (item.ComponentName == "Connections")
                            {
                                if (data2.Where(x => x.Text == "Connections").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = true;
                                    parentRoot.Text = "Communication";
                                    parentRoot.Url = "/communicationSRV";
                                    parentRoot.SpriteCssClass = "fa fa-whatsapp";
                                    parentRoot.Selected = controller == "communicationSRV";
                                    parentRoot.Id = "11";
                                    //TU-Karthik-Commented Communication menu to test Admin
                                    data2.Add(parentRoot);
                                }
                            }

                            if (item.ComponentName == "DocumentManager")
                            {
                                if (data2.Where(x => x.Text == "Documents").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = true;
                                    parentRoot.Text = "Documents";
                                    parentRoot.Url = "/documentmanagerSRV";
                                    parentRoot.SpriteCssClass = "fa fa-file";
                                    parentRoot.Selected = controller == "documentmanagerSRV";
                                    parentRoot.Id = "13";
                                    data2.Add(parentRoot);
                                }
                            }

                            if (item.ComponentName == "CRM")
                            {
                                if (data2.Where(x => x.Text == "CRM").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = true;
                                    parentRoot.Text = "CRM";
                                    parentRoot.Url = "/CRM";
                                    parentRoot.SpriteCssClass = "fa fa-money";
                                    parentRoot.Expanded = controller.ToLower() == "CRM";
                                    parentRoot.Selected = controller == "CRM";
                                    parentRoot.Id = "3";
                                    data2.Add(parentRoot);
                                }
                            }

                            if (item.ComponentName == "ShareRoute")
                            {
                               // if(item.IsPermitted==true)
                                {
                                    WellAIAppContext.Current.Session.SetString("ShareRoute", "1");
                                    ViewData["ShareRoute"] = true;
                                    ViewBag.ShareRoute = true;
                                }
                             
                            }
                        }



                        data2 = data2.OrderBy(x => int.Parse(x.Id)).ToList();
                        data4 = data2;
                        return data2;
                    }
                    data4 = data4.OrderBy(x => int.Parse(x.Id)).ToList();
                    return data4;
                }
                else
                {
                    List<PanelBarItemModel> data = new List<PanelBarItemModel>
                {
                    new PanelBarItemModel
                    {
                        Text = "Register",
                        Url = "/identity/account/Register",
                        SpriteCssClass = "fa fa-home"
                    },
                    new PanelBarItemModel
                    {
                        Text = "Login",
                        Url = "/identity/account/Login",
                        SpriteCssClass = "fa fa-home"
                    }
                };
                    return data;
                }
            }
            catch (Exception exc)
            {
                List<PanelBarItemModel> data = new List<PanelBarItemModel>
                {
                    new PanelBarItemModel
                    {
                        Text = "Register",
                        Url = "/identity/account/Register",
                        SpriteCssClass = "fa fa-home"
                    },
                    new PanelBarItemModel
                    {
                        Text = "Login",
                        Url = "/identity/account/Login",
                        SpriteCssClass = "fa fa-home"
                    }
                };
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId((ClaimsPrincipal)User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(exc, "NavViewComponent GetNavigationItems", User.Identity.Name);
                return data;
            }

        }

        //Dispatch navigation items
        private async Task<IEnumerable<PanelBarItemModel>> GetNavigationItemsForAdvisorOnly(string area, string controller, string action)
        {
            try
            {
                if (_signInManager.IsSignedIn(HttpContext.User))
                {
                    var userIdentity = (ClaimsIdentity)User.Identity;
                    var claims = userIdentity.Claims;
                    var roleClaimType = userIdentity.RoleClaimType;
                    var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();
                    if (roles == null && roles.Count == 0)
                    {
                        List<PanelBarItemModel> data3 = new List<PanelBarItemModel>
                    {
                        new PanelBarItemModel
                        {
                            Text = "Register",
                            Url = "/identity/account/Register",
                            SpriteCssClass = "fa fa-home"
                        },
                        new PanelBarItemModel
                        {
                            Text = "Login",
                            Url = "/identity/account/Login",
                            SpriteCssClass = "fa fa-home"
                        }
                    };
                        return data3;
                    }

                    if (string.IsNullOrEmpty(controller))
                    {
                        List<PanelBarItemModel> data3 = new List<PanelBarItemModel>
                    {
                        new PanelBarItemModel
                        {
                            Text = "Register",
                            Url = "/identity/account/Register",
                            SpriteCssClass = "fa fa-home"
                        },
                        new PanelBarItemModel
                        {
                            Text = "Login",
                            Url = "/identity/account/Login",
                            SpriteCssClass = "fa fa-home"
                        }
                    };
                        return data3;
                    }

                    if (WellAIAppContext.Current.Session.GetString("AccountType1") == "0") //Account type 0 operator company
                    {
                        IRolePermissionBusiness rolePermissionBusiness = new RolePermissionBusiness(db, _roleManager, _userManager);
                        var roleNames = roles.Select(x => x.Value).ToList();
                        var rolesResult = await rolePermissionBusiness.GetRolesByNames(roleNames);

                        var roleIds = rolesResult.Select(x => x.Id).ToList();
                        List<ComponentModelRec> allcomponents = new List<ComponentModelRec>();
                        if (Convert.ToBoolean(WellAIAppContext.Current.Session.GetString("IsMaster")))
                            allcomponents = rolePermissionBusiness.GetAllComponentForMaster(WellAIAppContext.Current.Session.GetString("TenantId"));
                        else
                            allcomponents = await rolePermissionBusiness.GetComponentsBasedOnRoles(roleIds);

                        var permittedComponents = allcomponents;//.Where(x => x.IsPermitted).ToList();

                        List<PanelBarItemModel> data2 = new List<PanelBarItemModel>();
                        foreach (var item in permittedComponents)
                        {
                            if (item.ComponentName == "ViewDashboard")
                            {
                                PanelBarItemModel model = new PanelBarItemModel();
                                model.Enabled = true;
                                model.Text = "Dashboard";
                                model.Url = "/OperatingDashboard/AdvisorWithDispatch";/// OperatingDashboard";
                                model.SpriteCssClass = "fa fa-home";
                                model.Selected = controller == "OperatingDashboard";
                                model.Id = "1";
                                data2.Add(model);
                            }
                            if (item.ComponentName == "InDepthRigData")
                            {
                                PanelBarItemModel model = new PanelBarItemModel();
                                model.Enabled = true;
                                model.Text = "Activity Advisor";
                                model.Url = "/InDepthRigData";
                                model.SpriteCssClass = "fa fa-line-chart";
                                model.Selected = controller == "InDepthRigData";
                                model.Id = "3";
                                data2.Add(model);
                            }

                            if (item.ComponentName == "ViewActivityView")
                            {
                                if (data2.Where(x => x.Text == "Schedule").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = true;
                                    parentRoot.Text = "Schedule";
                                    parentRoot.Url = "/activityview";
                                    parentRoot.SpriteCssClass = "fa fa-calendar";
                                    parentRoot.Expanded = controller.ToLower() == "activityview" || controller.ToLower() == "upcomingprojects" || controller.ToLower() == "communication";
                                    parentRoot.Selected = controller.ToLower() == "activityview";
                                    parentRoot.Id = "4";
                                    data2.Add(parentRoot);
                                }
                            }

                            if (item.ComponentName == "CheckListView")
                            {
                                if (data2.Where(x => x.Text == "Checklist").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = true;
                                    parentRoot.Text = "Drill Plan";
                                    parentRoot.Url = "/ActiveDrillPlan";
                                    parentRoot.SpriteCssClass = "fa fa-th-list";
                                    parentRoot.Expanded = controller.ToLower() == "activedrillplan";
                                    parentRoot.Selected = controller.ToLower() == "activedrillplan";
                                    parentRoot.Id = "2";
                                    data2.Add(parentRoot);
                                }
                            }


                            ////Dispatch
                            if (item.ComponentName == "Dispatch")
                            {
                                if (data2.Where(x => x.Text == "Dispatch").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = false;
                                    parentRoot.Text = "Dispatch";
                                    parentRoot.Url = "/Dispatch";
                                    parentRoot.SpriteCssClass = "fa fa-truck";
                                    parentRoot.Selected = controller == "Dispatch";
                                    parentRoot.Id = "5";
                                    data2.Add(parentRoot);
                                }
                            }

                            if (item.ComponentName == "ProviderLocator" || item.ComponentName == "Provider Locator")
                            {
                                if (data2.Where(x => x.Text == "Locator").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = true;
                                    parentRoot.Text = "Provider Locator";
                                    parentRoot.Url = "/ProviderLocator";
                                    parentRoot.SpriteCssClass = "fa fa-map-marker fa-2x";
                                    parentRoot.Expanded = controller.ToLower() == "ProviderLocator";
                                    parentRoot.Selected = controller.ToLower() == "ProviderLocator";
                                    parentRoot.Id = "6";
                                    data2.Add(parentRoot);
                                }
                            }

                            if (item.ComponentName == "UpcomingProjects")
                            {
                                PanelBarItemModel model = new PanelBarItemModel();
                                model.Enabled = true;
                                model.Text = "Services";
                                model.Url = "/upcomingprojects";
                                model.SpriteCssClass = "fa fa-tasks";
                                model.Selected = controller == "upcomingprojects";
                                model.Id = "7";
                                data2.Add(model);
                            }

                            if (item.ComponentName == "ProjectAuctions")
                            {
                                if (data2.Where(x => x.Text == "Bids").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = true;
                                    parentRoot.Text = "Bids";
                                    parentRoot.Url = "/projectauctions";
                                    parentRoot.SpriteCssClass = "fa fa-gavel";
                                    parentRoot.Expanded = controller.ToLower() == "projectauctions";
                                    parentRoot.Selected = controller.ToLower() == "projectauctions";
                                    parentRoot.Id = "8";
                                    data2.Add(parentRoot);
                                }
                            }

                            if (item.ComponentName == "ViewActivityView")
                            {
                                if (data2.Where(x => x.Text == "Vendors").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = true;
                                    parentRoot.Text = "Vendors";
                                    parentRoot.Url = "/providerDirectory";
                                    parentRoot.SpriteCssClass = "fa fa-building-o";
                                    parentRoot.Expanded = controller.ToLower() == "providerDirectory";
                                    parentRoot.Selected = controller == "providerDirectory";
                                    parentRoot.Id = "9";
                                    data2.Add(parentRoot);
                                }
                            }
                            if (item.ComponentName == "Connections")
                            {
                                PanelBarItemModel model = new PanelBarItemModel();
                                model.Enabled = true;
                                model.Text = "Communication";
                                model.Url = "/communication";
                                model.SpriteCssClass = "fa fa-whatsapp";
                                model.Selected = controller == "communication";
                                model.Id = "10";
                                //TU-Karthik-Commented Communication menu to test Admin
                                data2.Add(model);
                            }
                            if (item.ComponentName == "WellMetrics")
                            {
                                if (data2.Where(x => x.Text == "Register").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = true;
                                    parentRoot.Text = "Register";
                                    parentRoot.Url = "/DrillingPlan";
                                    parentRoot.SpriteCssClass = "fa fa-industry";
                                    parentRoot.Selected = controller == "DrillingPlan" || controller == "welldata" || controller == "rigs" || controller == "pad";
                                    data2.Add(parentRoot);
                                    parentRoot.Id = "11";
                                }
                            }
                            if (item.ComponentName == "DocumentManager")
                            {
                                if (data2.Where(x => x.Text == "Documents").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = true;
                                    parentRoot.Text = "Documents";
                                    parentRoot.Url = "/documentmanager";
                                    parentRoot.SpriteCssClass = "fa fa-file";
                                    parentRoot.Expanded = controller.ToLower() == "documentmanager";
                                    parentRoot.Selected = controller.ToLower() == "documentmanager";
                                    parentRoot.Id = "12";
                                    data2.Add(parentRoot);
                                }
                            }
                          
                        }

                        data2 = data2.OrderBy(x => int.Parse(x.Id)).ToList();
                        return data2;
                    }

                    if (WellAIAppContext.Current.Session.GetString("AccountType1") == "1") //Account type 1 is service type
                    {
                        IRolePermissionBusiness rolePermissionBusiness = new RolePermissionBusiness(db, _roleManager, _userManager);
                        var roleNames = roles.Select(x => x.Value).ToList();
                        var rolesResult = await rolePermissionBusiness.GetRolesByNames(roleNames);
                        var roleIds = rolesResult.Select(x => x.Id).ToList();

                        List<ComponentModelRec> allcomponents = new List<ComponentModelRec>();
                        if (Convert.ToBoolean(WellAIAppContext.Current.Session.GetString("IsMaster")))
                            allcomponents = rolePermissionBusiness.GetAllComponentForMaster(WellAIAppContext.Current.Session.GetString("TenantId"));
                        else
                            allcomponents = await rolePermissionBusiness.GetComponentsBasedOnRoles(roleIds);

                        var permittedComponent = allcomponents;//Where(x => x.IsPermitted).ToList();

                        List<PanelBarItemModel> data2 = new List<PanelBarItemModel>();
                        foreach (var item in permittedComponent)
                        {
                            if (item.ComponentName == "ViewDashboard")
                            {
                                PanelBarItemModel model = new PanelBarItemModel();
                                model.Enabled = true;
                                model.Text = "Dashboard";
                                //model.Url = "/ServiceDashboard";
                                model.Url = "/ServiceDashboard/AdvisorWithDispatch";/// OperatingDashboard";
                                model.SpriteCssClass = "fa fa-home";
                                model.Selected = controller == "ServiceDashboard";
                                model.Id = "1";
                                data2.Add(model);
                            }
                            if (item.ComponentName == "InDepthRigData")
                            {
                                if (data2.Where(x => x.Text == "InDepthRigDataSrv").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = true;
                                    parentRoot.Text = "Activity Advisor";
                                    parentRoot.Url = "/indepthrigdatasrv";
                                    parentRoot.SpriteCssClass = "fa fa-line-chart";
                                    parentRoot.Expanded = controller.ToLower() == "indepthrigdatasrv";
                                    parentRoot.Selected = controller == "indepthrigdatasrv";
                                    parentRoot.Id = "2";
                                    data2.Add(parentRoot);
                                }
                            }
                            if (item.ComponentName == "ViewActivityView")
                            {
                                if (data2.Where(x => x.Text == "Operators").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = true;
                                    parentRoot.Text = "Operator Directory";
                                    parentRoot.Url = "/OperatingDirectory";
                                    parentRoot.SpriteCssClass = "fa fa-building-o";
                                    parentRoot.Expanded = controller.ToLower() == "OperatorDirectory";
                                    parentRoot.Selected = controller == "OperatingDirectory";
                                    parentRoot.Id = "8";
                                    data2.Add(parentRoot);
                                }
                            }

                            if (item.ComponentName == "ViewActivityView")
                            {
                                if (data2.Where(x => x.Text == "Activity").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = true;
                                    parentRoot.Text = "Schedule";
                                    parentRoot.Url = "/ActivityViewSRV";
                                    parentRoot.SpriteCssClass = "fa fa-calendar";
                                    parentRoot.Expanded = controller.ToLower() == "ActivityViewSRV" || controller.ToLower() == "" || controller.ToLower() == "";
                                    parentRoot.Selected = controller == "ActivityViewSRV";
                                    parentRoot.Id = "4";
                                    data2.Add(parentRoot);
                                }
                            }

                            ////Dispatch || item.ComponentName == "RouterUser"
                            if (item.ComponentName == "RouterUser")
                            {
                                if (data2.Where(x => x.Text == "Dispatch").FirstOrDefault() == null || data2.Where(x => x.Text == "RouterUser").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = false;
                                    parentRoot.Text = "Dispatch";
                                    parentRoot.Url = "/DispatchSRV";
                                    parentRoot.SpriteCssClass = "fa fa-truck";
                                    parentRoot.Selected = controller == "DispatchSRV";
                                    parentRoot.Id = "5";
                                    data2.Add(parentRoot);
                                }
                            }

                            if (item.ComponentName == "UpcomingProjects")
                            {
                                if (data2.Where(x => x.Text == "Projects").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = true;
                                    parentRoot.Text = "Services";
                                    parentRoot.Url = "/upcomingprojectssrv";
                                    parentRoot.SpriteCssClass = "fa fa-tasks";
                                    parentRoot.Expanded = controller.ToLower() == "upcomingprojectssrv";
                                    parentRoot.Selected = controller == "upcomingprojectssrv";
                                    parentRoot.Id = "6";
                                    data2.Add(parentRoot);
                                }
                            }

                            if (item.ComponentName == "TechnicianTracker")
                            {
                                if (data2.Where(x => x.Text == "Technician Tracker").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = true;
                                    parentRoot.Text = "Technician Tracker";
                                    parentRoot.Url = "/TechnicianTrackerSRV";
                                    parentRoot.SpriteCssClass = "fa fa-map";
                                    parentRoot.Expanded = controller.ToLower() == "TechnicianTrackerSRV";
                                    parentRoot.Selected = controller == "TechnicianTrackerSRV";
                                    parentRoot.Id = "9";
                                    data2.Add(parentRoot);
                                }
                            }

                            if (item.ComponentName == "ProjectAuctions")
                            {
                                if (data2.Where(x => x.Text == "Auctions").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = true;
                                    parentRoot.Text = "Bids";
                                    parentRoot.Url = "/ProjectAuctionsSRV";
                                    parentRoot.SpriteCssClass = "fa fa-gavel";
                                    parentRoot.Expanded = controller.ToLower() == "ProjectAuctionsSRV" || controller.ToLower() == "" || controller.ToLower() == "";
                                    parentRoot.Selected = controller == "ProjectAuctionsSRV";
                                    parentRoot.Id = "7";
                                    data2.Add(parentRoot);
                                }
                            }

                            if (item.ComponentName == "WellMetrics")
                            {
                                if (data2.Where(x => x.Text == "Wells").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = true;
                                    parentRoot.Text = "Rigs";
                                    parentRoot.Url = "/welldataSRV";
                                    parentRoot.SpriteCssClass = "fa fa-industry";
                                    parentRoot.Expanded = controller.ToLower() == "welldataSRV";
                                    parentRoot.Selected = controller == "welldataSRV";
                                    parentRoot.Id = "10";
                                    data2.Add(parentRoot);
                                }
                            }

                            //if (item.ComponentName == "Fleet")
                            //{
                            //    if (data2.Where(x => x.Text == "Fleet").FirstOrDefault() == null)
                            //    {
                            //        PanelBarItemModel parentRoot = new PanelBarItemModel();
                            //        parentRoot.Enabled = true;
                            //        parentRoot.Text = "Fleet";
                            //        parentRoot.Url = "/fleetSRV";
                            //        parentRoot.SpriteCssClass = "fa fa-truck";
                            //        parentRoot.Selected = controller == "fleetSRV";
                            //        parentRoot.Id = "12";
                            //        data2.Add(parentRoot);
                            //    }
                            //}

                            ////Dispatch
                            //if (item.ComponentName == "Dispatch" || item.ComponentName == "RouterUser")
                            //{
                            //    if (data2.Where(x => x.Text == "Dispatch").FirstOrDefault() == null || data2.Where(x => x.Text == "RouterUser").FirstOrDefault() == null)
                            //    {
                            //        PanelBarItemModel parentRoot = new PanelBarItemModel();
                            //        parentRoot.Text = "Dispatch";
                            //        parentRoot.Url = "/Dispatch";
                            //        parentRoot.SpriteCssClass = "fa fa-truck";
                            //        parentRoot.Selected = controller == "Dispatch";
                            //        parentRoot.Id = "12";
                            //        data2.Add(parentRoot);
                            //    }
                            //}

                            if (item.ComponentName == "Connections")
                            {
                                if (data2.Where(x => x.Text == "Connections").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = true;
                                    parentRoot.Text = "Communication";
                                    parentRoot.Url = "/communicationSRV";
                                    parentRoot.SpriteCssClass = "fa fa-whatsapp";
                                    parentRoot.Selected = controller == "communicationSRV";
                                    parentRoot.Id = "11";
                                    //TU-Karthik-Commented Communication menu to test Admin
                                    data2.Add(parentRoot);
                                }
                            }

                            if (item.ComponentName == "DocumentManager")
                            {
                                if (data2.Where(x => x.Text == "Documents").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = true;
                                    parentRoot.Text = "Documents";
                                    parentRoot.Url = "/documentmanagerSRV";
                                    parentRoot.SpriteCssClass = "fa fa-file";
                                    parentRoot.Selected = controller == "documentmanagerSRV";
                                    parentRoot.Id = "13";
                                    data2.Add(parentRoot);
                                }
                            }

                            if (item.ComponentName == "CRM")
                            {
                                if (data2.Where(x => x.Text == "CRM").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = true;
                                    parentRoot.Text = "CRM";
                                    parentRoot.Url = "/CRM";
                                    parentRoot.SpriteCssClass = "fa fa-money";
                                    parentRoot.Expanded = controller.ToLower() == "CRM";
                                    parentRoot.Selected = controller == "CRM";
                                    parentRoot.Id = "3";
                                    data2.Add(parentRoot);
                                }
                            }

                            if (item.ComponentName == "ShareRoute")
                            {
                                // if(item.IsPermitted==true)
                                {
                                    WellAIAppContext.Current.Session.SetString("ShareRoute", "1");
                                    ViewData["ShareRoute"] = true;
                                    ViewBag.ShareRoute = true;
                                }

                            }
                        }

                        data2 = data2.OrderBy(x => int.Parse(x.Id)).ToList();
                        data4 = data2;
                        return data2;
                    }
                    data4 = data4.OrderBy(x => int.Parse(x.Id)).ToList();
                    return data4;
                }
                else
                {
                    List<PanelBarItemModel> data = new List<PanelBarItemModel>
                {
                    new PanelBarItemModel
                    {
                        Text = "Register",
                        Url = "/identity/account/Register",
                        SpriteCssClass = "fa fa-home"
                    },
                    new PanelBarItemModel
                    {
                        Text = "Login",
                        Url = "/identity/account/Login",
                        SpriteCssClass = "fa fa-home"
                    }
                };
                    return data;
                }
            }
            catch (Exception exc)
            {
                List<PanelBarItemModel> data = new List<PanelBarItemModel>
                {
                    new PanelBarItemModel
                    {
                        Text = "Register",
                        Url = "/identity/account/Register",
                        SpriteCssClass = "fa fa-home"
                    },
                    new PanelBarItemModel
                    {
                        Text = "Login",
                        Url = "/identity/account/Login",
                        SpriteCssClass = "fa fa-home"
                    }
                };
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId((ClaimsPrincipal)User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(exc, "NavViewComponent GetNavigationItems", User.Identity.Name);
                return data;
            }

        }

        //Dispatch navigation items
        private async Task<IEnumerable<PanelBarItemModel>> GetNavigationItemsForDispatchOnly(string area, string controller, string action)
        {
            try
            {
                if (_signInManager.IsSignedIn(HttpContext.User))
                {
                    var userIdentity = (ClaimsIdentity)User.Identity;
                    var claims = userIdentity.Claims;
                    var roleClaimType = userIdentity.RoleClaimType;
                    var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();
                    if (roles == null && roles.Count == 0)
                    {
                        List<PanelBarItemModel> data3 = new List<PanelBarItemModel>
                    {
                        new PanelBarItemModel
                        {
                            Text = "Register",
                            Url = "/identity/account/Register",
                            SpriteCssClass = "fa fa-home"
                        },
                        new PanelBarItemModel
                        {
                            Text = "Login",
                            Url = "/identity/account/Login",
                            SpriteCssClass = "fa fa-home"
                        }
                    };
                        return data3;
                    }

                    if (string.IsNullOrEmpty(controller))
                    {
                        List<PanelBarItemModel> data3 = new List<PanelBarItemModel>
                    {
                        new PanelBarItemModel
                        {
                            Text = "Register",
                            Url = "/identity/account/Register",
                            SpriteCssClass = "fa fa-home"
                        },
                        new PanelBarItemModel
                        {
                            Text = "Login",
                            Url = "/identity/account/Login",
                            SpriteCssClass = "fa fa-home"
                        }
                    };
                        return data3;
                    }

                    if (WellAIAppContext.Current.Session.GetString("AccountType1") == "0") //Account type 0 operator company
                    {
                        IRolePermissionBusiness rolePermissionBusiness = new RolePermissionBusiness(db, _roleManager, _userManager);
                        var roleNames = roles.Select(x => x.Value).ToList();
                        var rolesResult = await rolePermissionBusiness.GetRolesByNames(roleNames);

                        var roleIds = rolesResult.Select(x => x.Id).ToList();
                        List<ComponentModelRec> allcomponents = new List<ComponentModelRec>();
                        if (Convert.ToBoolean(WellAIAppContext.Current.Session.GetString("IsMaster")))
                            allcomponents = rolePermissionBusiness.GetAllComponentForMaster(WellAIAppContext.Current.Session.GetString("TenantId"));
                        else
                            allcomponents = await rolePermissionBusiness.GetComponentsBasedOnRoles(roleIds);

                        var permittedComponents = allcomponents;//.Where(x => x.IsPermitted).ToList();

                        List<PanelBarItemModel> data2 = new List<PanelBarItemModel>();
                        foreach (var item in permittedComponents)
                        {
                            if (item.ComponentName == "ViewDashboard")
                            {
                                PanelBarItemModel model = new PanelBarItemModel();
                                model.Enabled = true;
                                model.Text = "Dashboard";
                                model.Url = "/OperatingDashboard/AdvisorWithDispatch"; //model.Url = "/OperatingDashboard/AdvisorWithDispatch"
                                model.SpriteCssClass = "fa fa-home";
                                model.Selected = controller == "OperatingDashboard";
                                model.Id = "1";
                                data2.Add(model);
                            }
                            if (item.ComponentName == "InDepthRigData")
                            {
                                PanelBarItemModel model = new PanelBarItemModel();
                                model.Enabled = false;
                                model.Text = "Activity Advisor";
                                model.Url = "/InDepthRigData";
                                model.SpriteCssClass = "fa fa-line-chart";
                                model.Selected = controller == "InDepthRigData";
                                model.Id = "3";
                                data2.Add(model);
                            }

                            if (item.ComponentName == "ViewActivityView")
                            {
                                if (data2.Where(x => x.Text == "Schedule").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = false;
                                    parentRoot.Text = "Schedule";
                                    parentRoot.Url = "/activityview";
                                    parentRoot.SpriteCssClass = "fa fa-calendar";
                                    parentRoot.Expanded = controller.ToLower() == "activityview" || controller.ToLower() == "upcomingprojects" || controller.ToLower() == "communication";
                                    parentRoot.Selected = controller.ToLower() == "activityview";
                                    parentRoot.Id = "4";
                                    data2.Add(parentRoot);
                                }
                            }

                            if (item.ComponentName == "CheckListView")
                            {
                                if (data2.Where(x => x.Text == "Checklist").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = false;
                                    parentRoot.Text = "Drill Plan";
                                    parentRoot.Url = "/ActiveDrillPlan";
                                    parentRoot.SpriteCssClass = "fa fa-th-list";
                                    parentRoot.Expanded = controller.ToLower() == "activedrillplan";
                                    parentRoot.Selected = controller.ToLower() == "activedrillplan";
                                    parentRoot.Id = "2";
                                    data2.Add(parentRoot);
                                }
                            }


                            //Dispatch
                            if (item.ComponentName == "Dispatch")
                            {
                                if (data2.Where(x => x.Text == "Dispatch").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = true;
                                    parentRoot.Text = "Dispatch";
                                    parentRoot.Url = "/Dispatch";
                                    parentRoot.SpriteCssClass = "fa fa-truck";
                                    parentRoot.Selected = controller == "Dispatch";
                                    parentRoot.Id = "5";
                                    data2.Add(parentRoot);
                                }
                            }


                            if (item.ComponentName == "UpcomingProjects")
                            {
                                PanelBarItemModel model = new PanelBarItemModel();
                                model.Enabled = false;
                                model.Text = "Services";
                                model.Url = "/upcomingprojects";
                                model.SpriteCssClass = "fa fa-tasks";
                                model.Selected = controller == "upcomingprojects";
                                model.Id = "6";
                                data2.Add(model);
                            }

                            if (item.ComponentName == "ProjectAuctions")
                            {
                                if (data2.Where(x => x.Text == "Bids").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = false;
                                    parentRoot.Text = "Bids";
                                    parentRoot.Url = "/projectauctions";
                                    parentRoot.SpriteCssClass = "fa fa-gavel";
                                    parentRoot.Expanded = controller.ToLower() == "projectauctions";
                                    parentRoot.Selected = controller.ToLower() == "projectauctions";
                                    parentRoot.Id = "7";
                                    data2.Add(parentRoot);
                                }
                            }

                            if (item.ComponentName == "ViewActivityView")
                            {
                                if (data2.Where(x => x.Text == "Vendors").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = false;
                                    parentRoot.Text = "Vendors";
                                    parentRoot.Url = "/providerDirectory";
                                    parentRoot.SpriteCssClass = "fa fa-building-o";
                                    parentRoot.Expanded = controller.ToLower() == "providerDirectory";
                                    parentRoot.Selected = controller == "providerDirectory";
                                    parentRoot.Id = "8";
                                    data2.Add(parentRoot);
                                }
                            }
                            if (item.ComponentName == "Connections")
                            {
                                PanelBarItemModel model = new PanelBarItemModel();
                                model.Enabled = false;
                                model.Text = "Communication";
                                model.Url = "/communication";
                                model.SpriteCssClass = "fa fa-whatsapp";
                                model.Selected = controller == "communication";
                                model.Id = "9";
                                //TU-Karthik-Commented Communication menu to test Admin
                                data2.Add(model);
                            }
                            if (item.ComponentName == "WellMetrics")
                            {
                                if (data2.Where(x => x.Text == "Register").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = false;
                                    parentRoot.Text = "Register";
                                    parentRoot.Url = "/DrillingPlan";
                                    parentRoot.SpriteCssClass = "fa fa-industry";
                                    parentRoot.Selected = controller == "DrillingPlan" || controller == "welldata" || controller == "rigs" || controller == "pad";
                                    data2.Add(parentRoot);
                                    parentRoot.Id = "10";
                                }
                            }
                            if (item.ComponentName == "DocumentManager")
                            {
                                if (data2.Where(x => x.Text == "Documents").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = false;
                                    parentRoot.Text = "Documents";
                                    parentRoot.Url = "/documentmanager";
                                    parentRoot.SpriteCssClass = "fa fa-file";
                                    parentRoot.Expanded = controller.ToLower() == "documentmanager";
                                    parentRoot.Selected = controller.ToLower() == "documentmanager";
                                    parentRoot.Id = "11";
                                    data2.Add(parentRoot);
                                }
                            }
                        }

                        data2 = data2.OrderBy(x => int.Parse(x.Id)).ToList();
                        return data2;
                    }

                    if (WellAIAppContext.Current.Session.GetString("AccountType1") == "1" || WellAIAppContext.Current.Session.GetString("AccountType1") == "2") //Account type 1 is service type, 2 is Dispatch only
                    {
                        IRolePermissionBusiness rolePermissionBusiness = new RolePermissionBusiness(db, _roleManager, _userManager);
                        var roleNames = roles.Select(x => x.Value).ToList();
                        var rolesResult = await rolePermissionBusiness.GetRolesByNames(roleNames);
                        var roleIds = rolesResult.Select(x => x.Id).ToList();

                        List<ComponentModelRec> allcomponents = new List<ComponentModelRec>();
                        if (Convert.ToBoolean(WellAIAppContext.Current.Session.GetString("IsMaster")))
                            allcomponents = rolePermissionBusiness.GetAllComponentForMaster(WellAIAppContext.Current.Session.GetString("CorporateProfileId"));
                        else
                            allcomponents = await rolePermissionBusiness.GetComponentsBasedOnRoles(roleIds);

                        var permittedComponent = allcomponents.Where(x=>x.SrvAccountType == 2).ToList();//Where(x => x.IsPermitted).ToList();

                        List<PanelBarItemModel> data2 = new List<PanelBarItemModel>();
                        foreach (var item in permittedComponent)
                        {
                            if (item.ComponentName == "ViewDashboard")
                            {
                                PanelBarItemModel model = new PanelBarItemModel();
                                model.Enabled = true;
                                model.Text = "Dashboard";
                                //model.Url = "/ServiceDashboard";
                                model.Url = "/ServiceDashboard/AdvisorWithDispatch"; //model.Url = "/OperatingDashboard/AdvisorWithDispatch";
                                model.SpriteCssClass = "fa fa-home";
                                model.Selected = controller == "DispatchSRV";
                                model.Id = "1";
                                data2.Add(model);
                            }
                            if (item.ComponentName == "InDepthRigData")
                            {
                                if (data2.Where(x => x.Text == "InDepthRigDataSrv").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = false;
                                    parentRoot.Text = "Activity Advisor";
                                    parentRoot.Url = "/indepthrigdatasrv";
                                    parentRoot.SpriteCssClass = "fa fa-line-chart";
                                    parentRoot.Expanded = controller.ToLower() == "indepthrigdatasrv";
                                    parentRoot.Selected = controller == "indepthrigdatasrv";
                                    parentRoot.Id = "2";
                                    data2.Add(parentRoot);
                                }
                            }
                            if (item.ComponentName == "ViewActivityView")
                            {
                                if (data2.Where(x => x.Text == "Operators").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = false;
                                    parentRoot.Text = "Operator Directory";
                                    parentRoot.Url = "/OperatingDirectory";
                                    parentRoot.SpriteCssClass = "fa fa-building-o";
                                    parentRoot.Expanded = controller.ToLower() == "OperatorDirectory";
                                    parentRoot.Selected = controller == "OperatingDirectory";
                                    parentRoot.Id = "8";
                                    data2.Add(parentRoot);
                                }
                            }

                            if (item.ComponentName == "ViewActivityView")
                            {
                                if (data2.Where(x => x.Text == "Activity").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = false;
                                    parentRoot.Text = "Schedule";
                                    parentRoot.Url = "/ActivityViewSRV";
                                    parentRoot.SpriteCssClass = "fa fa-calendar";
                                    parentRoot.Expanded = controller.ToLower() == "ActivityViewSRV" || controller.ToLower() == "" || controller.ToLower() == "";
                                    parentRoot.Selected = controller == "ActivityViewSRV";
                                    parentRoot.Id = "4";
                                    data2.Add(parentRoot);
                                }
                            }

                            //Dispatch || item.ComponentName == "RouterUser"
                            if (item.ComponentName == "RouterUser")
                            {
                                if (data2.Where(x => x.Text == "Dispatch").FirstOrDefault() == null || data2.Where(x => x.Text == "RouterUser").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = true;
                                    parentRoot.Text = "Dispatch";
                                    parentRoot.Url = "/DispatchSRV";
                                    parentRoot.SpriteCssClass = "fa fa-truck";
                                    parentRoot.Selected = controller == "DispatchSRV";
                                    parentRoot.Id = "5";
                                    data2.Add(parentRoot);
                                }
                            }

                            if (item.ComponentName == "UpcomingProjects")
                            {
                                if (data2.Where(x => x.Text == "Projects").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = false;
                                    parentRoot.Text = "Services";
                                    parentRoot.Url = "/upcomingprojectssrv";
                                    parentRoot.SpriteCssClass = "fa fa-tasks";
                                    parentRoot.Expanded = controller.ToLower() == "upcomingprojectssrv";
                                    parentRoot.Selected = controller == "upcomingprojectssrv";
                                    parentRoot.Id = "6";
                                    data2.Add(parentRoot);
                                }
                            }

                            //if (item.ComponentName == "TechnicianTracker")
                            //{
                            //    if (data2.Where(x => x.Text == "Technician Tracker").FirstOrDefault() == null)
                            //    {
                            //        PanelBarItemModel parentRoot = new PanelBarItemModel();
                            //        parentRoot.Enabled = false;
                            //        parentRoot.Text = "Technician Tracker";
                            //        parentRoot.Url = "/TechnicianTrackerSRV";
                            //        parentRoot.SpriteCssClass = "fa fa-map";
                            //        parentRoot.Expanded = controller.ToLower() == "TechnicianTrackerSRV";
                            //        parentRoot.Selected = controller == "TechnicianTrackerSRV";
                            //        parentRoot.Id = "9";
                            //        data2.Add(parentRoot);
                            //    }
                            //}

                            if (item.ComponentName == "ProjectAuctions")
                            {
                                if (data2.Where(x => x.Text == "Auctions").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = false;
                                    parentRoot.Text = "Bids";
                                    parentRoot.Url = "/ProjectAuctionsSRV";
                                    parentRoot.SpriteCssClass = "fa fa-gavel";
                                    parentRoot.Expanded = controller.ToLower() == "ProjectAuctionsSRV" || controller.ToLower() == "" || controller.ToLower() == "";
                                    parentRoot.Selected = controller == "ProjectAuctionsSRV";
                                    parentRoot.Id = "7";
                                    data2.Add(parentRoot);
                                }
                            }

                            if (item.ComponentName == "WellMetrics")
                            {
                                if (data2.Where(x => x.Text == "Wells").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = false;
                                    parentRoot.Text = "Rigs";
                                    parentRoot.Url = "/welldataSRV";
                                    parentRoot.SpriteCssClass = "fa fa-industry";
                                    parentRoot.Expanded = controller.ToLower() == "welldataSRV";
                                    parentRoot.Selected = controller == "welldataSRV";
                                    parentRoot.Id = "10";
                                    data2.Add(parentRoot);
                                }
                            }

                            //if (item.ComponentName == "Fleet")
                            //{
                            //    if (data2.Where(x => x.Text == "Fleet").FirstOrDefault() == null)
                            //    {
                            //        PanelBarItemModel parentRoot = new PanelBarItemModel();
                            //        parentRoot.Enabled = false;
                            //        parentRoot.Text = "Fleet";
                            //        parentRoot.Url = "/fleetSRV";
                            //        parentRoot.SpriteCssClass = "fa fa-truck";
                            //        parentRoot.Selected = controller == "fleetSRV";
                            //        parentRoot.Id = "12";
                            //        data2.Add(parentRoot);
                            //    }
                            //}

                            ////Dispatch
                            //if (item.ComponentName == "Dispatch" || item.ComponentName == "RouterUser")
                            //{
                            //    if (data2.Where(x => x.Text == "Dispatch").FirstOrDefault() == null || data2.Where(x => x.Text == "RouterUser").FirstOrDefault() == null)
                            //    {
                            //        PanelBarItemModel parentRoot = new PanelBarItemModel();
                            //        parentRoot.Enabled = true;
                            //        parentRoot.Text = "Dispatch";
                            //        parentRoot.Url = "/DispatchSRV";
                            //        parentRoot.SpriteCssClass = "fa fa-truck";
                            //        parentRoot.Selected = controller == "Dispatch";
                            //        parentRoot.Id = "12";
                            //        data2.Add(parentRoot);
                            //    }
                            //}

                            if (item.ComponentName == "Connections")
                            {
                                if (data2.Where(x => x.Text == "Connections").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = false;
                                    parentRoot.Text = "Communication";
                                    parentRoot.Url = "/communicationSRV";
                                    parentRoot.SpriteCssClass = "fa fa-whatsapp";
                                    parentRoot.Selected = controller == "communicationSRV";
                                    parentRoot.Id = "11";
                                    //TU-Karthik-Commented Communication menu to test Admin
                                    data2.Add(parentRoot);
                                }
                            }

                            if (item.ComponentName == "DocumentManager")
                            {
                                if (data2.Where(x => x.Text == "Documents").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = false;
                                    parentRoot.Text = "Documents";
                                    parentRoot.Url = "/documentmanagerSRV";
                                    parentRoot.SpriteCssClass = "fa fa-file";
                                    parentRoot.Selected = controller == "documentmanagerSRV";
                                    parentRoot.Id = "13";
                                    data2.Add(parentRoot);
                                }
                            }

                            if (item.ComponentName == "CRM")
                            {
                                if (data2.Where(x => x.Text == "CRM").FirstOrDefault() == null)
                                {
                                    PanelBarItemModel parentRoot = new PanelBarItemModel();
                                    parentRoot.Enabled = false;
                                    parentRoot.Text = "CRM";
                                    parentRoot.Url = "/CRM";
                                    parentRoot.SpriteCssClass = "fa fa-money";
                                    parentRoot.Expanded = controller.ToLower() == "CRM";
                                    parentRoot.Selected = controller == "CRM";
                                    parentRoot.Id = "3";
                                    data2.Add(parentRoot);
                                }
                            }

                            if (item.ComponentName == "ShareRoute")
                            {
                                // if(item.IsPermitted==true)
                                {
                                    WellAIAppContext.Current.Session.SetString("ShareRoute", "1");
                                    ViewData["ShareRoute"] = true;
                                    ViewBag.ShareRoute = true;
                                }

                            }
                        }



                        data2 = data2.OrderBy(x => int.Parse(x.Id)).ToList();
                        data4 = data2;
                        return data2;
                    }
                    data4 = data4.OrderBy(x => int.Parse(x.Id)).ToList();
                    return data4;
                }
                else
                {
                    List<PanelBarItemModel> data = new List<PanelBarItemModel>
                {
                    new PanelBarItemModel
                    {
                        Text = "Register",
                        Url = "/identity/account/Register",
                        SpriteCssClass = "fa fa-home"
                    },
                    new PanelBarItemModel
                    {
                        Text = "Login",
                        Url = "/identity/account/Login",
                        SpriteCssClass = "fa fa-home"
                    }
                };
                    return data;
                }
            }
            catch (Exception exc)
            {
                List<PanelBarItemModel> data = new List<PanelBarItemModel>
                {
                    new PanelBarItemModel
                    {
                        Text = "Register",
                        Url = "/identity/account/Register",
                        SpriteCssClass = "fa fa-home"
                    },
                    new PanelBarItemModel
                    {
                        Text = "Login",
                        Url = "/identity/account/Login",
                        SpriteCssClass = "fa fa-home"
                    }
                };
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId((ClaimsPrincipal)User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(exc, "NavViewComponent GetNavigationItems", User.Identity.Name);
                return data;
            }

        }
    }
}