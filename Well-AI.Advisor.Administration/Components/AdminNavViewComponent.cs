using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Well_AI.Advisor.Administration.Components
{
    public class AdminNavViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var controller = (string)RouteData.Values["controller"];
            var action = (string)RouteData.Values["action"];
            var area = (string)RouteData.Values["area"];

            return View("_SideNav", await GetNavigationItems(area, controller, action));
        }

        private async Task<IEnumerable<PanelBarItemModel>> GetNavigationItems(string area, string controller, string action)
        {
            try
            {

                List<PanelBarItemModel> data = new List<PanelBarItemModel>();

                PanelBarItemModel parentRoot = new PanelBarItemModel();
                parentRoot.Text = "Customer";
                //parentRoot.Url = "/corporateprofile";
                parentRoot.SpriteCssClass = "fa fa-users";
                parentRoot.Expanded = controller == "Customer";
                // parentRoot.Selected = controller == "Customer";
                parentRoot.Id = "1";

                PanelBarItemModel model1 = new PanelBarItemModel();
                model1.Text = "Operators";
                model1.SpriteCssClass = "fa fa-user";
                model1.Url = "/Customer/Operator";
                model1.Selected = controller == "Customer" && action == "Operator";
                parentRoot.Items.Add(model1);

                PanelBarItemModel model2 = new PanelBarItemModel();
                model2.Text = "Service";
                model2.SpriteCssClass = "fa fa-wrench";
                model2.Url = "/Customer/Service";
                model2.Selected = controller == "Customer" && action == "Service";
                parentRoot.Items.Add(model2);
              

                PanelBarItemModel model_3 = new PanelBarItemModel();
                model_3.Text = "Dispatch";
                model_3.SpriteCssClass = "fa fa-truck";
                model_3.Url = "/Customer/Dispatch";
                model_3.Selected = controller == "Customer" && action == "Dispatch";
                parentRoot.Items.Add(model_3);
                data.Add(parentRoot);

                //            PanelBarItemModel model3 = new PanelBarItemModel();
                //            model3.Text = "Dispatch";
                //            model3.Url = "/Customer/Dispatch";
                //            model3.Selected = controller == "Customer" && action == "Dispatch";
                //            parentRoot.Items.Add(model3);
                //            data.Add(parentRoot);


                PanelBarItemModel parentRoot1 = new PanelBarItemModel();
                parentRoot1.Text = "Prediction";
                parentRoot1.Url = "/WellPrediction/Index";
                parentRoot1.SpriteCssClass = "fa fa-line-chart";
                parentRoot1.Expanded = controller == "WellPrediction";
                parentRoot1.Selected = controller == "WellPrediction";
                parentRoot1.Id = "2";
                data.Add(parentRoot1);


                PanelBarItemModel parentRoot2 = new PanelBarItemModel();
                parentRoot2.Text = "Setup";
                //parentRoot2.Url = "/corporateprofile";
                parentRoot2.SpriteCssClass = "fa fa-cogs";
                parentRoot2.Expanded = controller == "ServiceCategory";
                // parentRoot2.Selected = controller == "ServiceCategory";
                parentRoot2.Id = "3";

                PanelBarItemModel model3 = new PanelBarItemModel();
                model3.Text = "Phase";
                model3.Url = "/ServiceCategory";
                model3.Selected = controller == "ServiceCategory";
                model3.SpriteCssClass = "fa fa-step-forward";
                //  model3.Id = "1";
                parentRoot2.Items.Add(model3);

                PanelBarItemModel model4 = new PanelBarItemModel();
                model4.Text = "Services";
                model4.Url = "/Welltasks/CategoryTask";
                model4.SpriteCssClass = "fa fa-wrench";
                model4.Selected = controller == "WellTasks" && action == "CategoryTask";
              //  model4.Id = "2";
                parentRoot2.Items.Add(model4);

                PanelBarItemModel model5 = new PanelBarItemModel();
                model5.Text = "Tasks";
                model5.Url = "/Welltasks";
                model5.SpriteCssClass = "fa fa-tasks";
                model5.Selected = controller == "WellTasks" && action == "index";
               // model5.Id = "3";
                parentRoot2.Items.Add(model5);

                PanelBarItemModel model6 = new PanelBarItemModel();
                model6.Text = "Checklist Template";
                model6.Url = "/ChecklistTemplate/Index";
                model6.SpriteCssClass = "fa fa-check-square-o";
              //  model5.Id = "4";
                model6.Selected = controller == "CheckListTemplate" && action == "Index";
                parentRoot2.Items.Add(model6);
                data.Add(parentRoot2);

                PanelBarItemModel model_7 = new PanelBarItemModel();
                model_7.Text = "Subscription";
                model_7.Url = "/SubscriptionPackage/Index";
                model_7.SpriteCssClass = "fa fa-rocket";
                model_7.Expanded = controller == "SubscriptionPackage";
                model_7.Selected = controller == "SubscriptionPackage";
            //    model_7.Id = "5";
              //  parentRoot2.Items.Add(model_7);
                data.Add(model_7);


                //PanelBarItemModel parentRoot5 = new PanelBarItemModel();
                //parentRoot5.Text = "Subscription";
                //parentRoot5.Url = "/SubscriptionPackage/Index";
                //parentRoot5.SpriteCssClass = "fa fa-rocket";
                //parentRoot5.Expanded = controller == "SubscriptionPackage";
                //parentRoot5.Selected = controller == "SubscriptionPackage";
                //parentRoot5.Id = "6";
                //data.Add(parentRoot5);

                PanelBarItemModel parentRoot3 = new PanelBarItemModel();
                parentRoot3.Text = "Configuration";
                parentRoot3.Url = "/Configuration/Index";
                parentRoot3.SpriteCssClass = "fa fa-cog";
                parentRoot3.Expanded = controller == "Configuration";
                parentRoot3.Selected = controller == "Configuration";
                parentRoot3.Id = "4";
                data.Add(parentRoot3);


                PanelBarItemModel parentRoot4 = new PanelBarItemModel();
                parentRoot4.Text = "Staff";
                //parentRoot.Url = "/staff?status=active";
                parentRoot4.SpriteCssClass = "fa fa-users";
                parentRoot4.Expanded = controller == "Staff";
                //parentRoot4.Selected = controller == "Staff";
                parentRoot.Id = "5";
                string StaffIsactive = "";
                if (controller == "Staff")
                {
                    StaffIsactive = ViewContext.ViewBag.Status;
                }
                PanelBarItemModel model7 = new PanelBarItemModel();
                model7.Text = "Active";
                model7.Url = "/staff?status=active";
                model7.SpriteCssClass = "fa fa-toggle-on";
                model7.Selected = controller == "Staff" && StaffIsactive == "active";
                parentRoot4.Items.Add(model7);


                PanelBarItemModel model8 = new PanelBarItemModel();
                model8.Text = "In-Active";
                model8.SpriteCssClass = "fa fa-toggle-off";
                model8.Url = "/staff?status=deactivate";
                model8.Selected = controller == "Staff" && StaffIsactive == "deactivate";
                parentRoot4.Items.Add(model8);
                data.Add(parentRoot4);


                //PanelBarItemModel parentRoot5 = new PanelBarItemModel();
                //parentRoot5.Text = "Subscription";
                //parentRoot5.Url = "/SubscriptionPackage/Index";
                //parentRoot5.SpriteCssClass = "fa fa-rocket";
                //parentRoot5.Expanded = controller == "SubscriptionPackage";
                //parentRoot5.Selected = controller == "SubscriptionPackage";
                //parentRoot5.Id = "6";
                //data.Add(parentRoot5);

                //PanelBarItemModel parentRoot7 = new PanelBarItemModel();
                //parentRoot7.Enabled = false;
                //parentRoot7.Text = "Dispatch";
                //parentRoot7.Url = "/DispatchSRV";
                //parentRoot7.SpriteCssClass = "fa fa-truck";
                //parentRoot7.Selected = controller == "DispatchSRV";
                //parentRoot7.Id = "7";
                //data.Add(parentRoot7);

                return data;
            }
            catch (Exception exc)
            {
                List<PanelBarItemModel> data = new List<PanelBarItemModel>();
                return data;
            }

        }

        //private async Task<IEnumerable<PanelBarItemModel>> GetNavigationItems(string area, string controller, string action)
        //{
        //    try
        //    {

        //        List<PanelBarItemModel> data = new List<PanelBarItemModel>();

        //            PanelBarItemModel parentRoot = new PanelBarItemModel();
        //            parentRoot.Text = "Customer";
        //            //parentRoot.Url = "/corporateprofile";
        //            parentRoot.SpriteCssClass = "fa fa-users";
        //            parentRoot.Expanded = controller == "Customer";
        //           // parentRoot.Selected = controller == "Customer";
        //            parentRoot.Id = "1";

        //            PanelBarItemModel model1 = new PanelBarItemModel();
        //            model1.Text = "Operators";
        //            model1.Url = "/Customer/Operator";
        //            model1.Selected = controller == "Customer" && action == "Operator";
        //            parentRoot.Items.Add(model1);

        //            PanelBarItemModel model2 = new PanelBarItemModel();
        //            model2.Text = "Service";
        //            model2.Url = "/Customer/Service";
        //            model2.Selected = controller == "Customer" && action == "Service";
        //            parentRoot.Items.Add(model2);
        //            data.Add(parentRoot);

        //            PanelBarItemModel model3 = new PanelBarItemModel();
        //            model3.Text = "Dispatch";
        //            model3.Url = "/Customer/Dispatch";
        //            model3.Selected = controller == "Customer" && action == "Dispatch";
        //            parentRoot.Items.Add(model3);
        //            data.Add(parentRoot);

        //        PanelBarItemModel parentRoot1 = new PanelBarItemModel();
        //        parentRoot1.Text = "Prediction";
        //        parentRoot1.Url = "/WellPrediction/Index";
        //        parentRoot1.SpriteCssClass = "fa fa-line-chart";
        //        parentRoot1.Expanded = controller == "WellPrediction";
        //        parentRoot1.Selected = controller == "WellPrediction";
        //        parentRoot1.Id = "2";
        //        data.Add(parentRoot1);


        //        PanelBarItemModel parentRoot2 = new PanelBarItemModel();
        //        parentRoot2.Text = "Setup";
        //        //parentRoot2.Url = "/corporateprofile";
        //        parentRoot2.SpriteCssClass = "fa fa-cogs";
        //        parentRoot2.Expanded = controller == "ServiceCategory";
        //       // parentRoot2.Selected = controller == "ServiceCategory";
        //        parentRoot2.Id = "3";

        //        PanelBarItemModel modelDis = new PanelBarItemModel();
        //        modelDis.Text = "Phase";
        //        modelDis.Url = "/ServiceCategory";
        //        modelDis.Selected = controller == "ServiceCategory";
        //        parentRoot2.Items.Add(modelDis);

        //        PanelBarItemModel model4 = new PanelBarItemModel();
        //        model4.Text = "Services";
        //        model4.Url = "/Welltasks/CategoryTask";
        //        model4.Selected = controller == "WellTasks" && action == "CategoryTask";
        //        parentRoot2.Items.Add(model4);

        //        PanelBarItemModel model5 = new PanelBarItemModel();
        //        model5.Text = "Tasks";
        //        model5.Url = "/Welltasks";
        //        model5.Selected = controller == "WellTasks" && action == "index";
        //        parentRoot2.Items.Add(model5);

        //        PanelBarItemModel model6 = new PanelBarItemModel();
        //        model6.Text = "Checklist Template";
        //        model6.Url = "/ChecklistTemplate/Index";
        //        model6.Selected = controller == "CheckListTemplate" && action == "Index";
        //        parentRoot2.Items.Add(model6);
        //        data.Add(parentRoot2);

        //        PanelBarItemModel parentRoot3 = new PanelBarItemModel();
        //        parentRoot3.Text = "Configuration";
        //        parentRoot3.Url = "/Configuration/Index";
        //        parentRoot3.SpriteCssClass = "fa fa-cog";
        //        parentRoot3.Expanded = controller == "Configuration";
        //        parentRoot3.Selected = controller == "Configuration";
        //        parentRoot3.Id = "4";
        //        data.Add(parentRoot3);


        //        PanelBarItemModel parentRoot4 = new PanelBarItemModel();
        //        parentRoot4.Text = "Staff";
        //        //parentRoot.Url = "/staff?status=active";
        //        parentRoot4.SpriteCssClass = "fa fa-users";
        //        parentRoot4.Expanded = controller == "Staff";
        //        //parentRoot4.Selected = controller == "Staff";
        //        parentRoot.Id = "5";
        //        string StaffIsactive = "";
        //        if (controller == "Staff")
        //        {
        //             StaffIsactive = ViewContext.ViewBag.Status;
        //        }
        //        PanelBarItemModel model7 = new PanelBarItemModel();
        //        model7.Text = "Active";
        //        model7.Url = "/staff?status=active";
        //        model7.Selected = controller == "Staff" && StaffIsactive == "active";
        //        parentRoot4.Items.Add(model7);


        //        PanelBarItemModel model8 = new PanelBarItemModel();
        //        model8.Text = "In-Active";
        //        model8.Url = "/staff?status=deactivate";
        //        model8.Selected = controller == "Staff" && StaffIsactive == "deactivate";
        //        parentRoot4.Items.Add(model8);
        //        data.Add(parentRoot4);


        //        PanelBarItemModel parentRoot5 = new PanelBarItemModel();
        //        parentRoot5.Text = "Subscription";
        //        parentRoot5.Url = "/SubscriptionPackage/Index";
        //        parentRoot5.SpriteCssClass = "fa fa-rocket";
        //        parentRoot5.Expanded = controller == "SubscriptionPackage";
        //        parentRoot5.Selected = controller == "SubscriptionPackage";
        //        parentRoot5.Id = "6";
        //        data.Add(parentRoot5);

        //        //PanelBarItemModel parentRoot7 = new PanelBarItemModel();
        //        //parentRoot7.Enabled = false;
        //        //parentRoot7.Text = "Dispatch";
        //        //parentRoot7.Url = "/DispatchSRV";
        //        //parentRoot7.SpriteCssClass = "fa fa-truck";
        //        //parentRoot7.Selected = controller == "DispatchSRV";
        //        //parentRoot7.Id = "7";
        //        //data.Add(parentRoot7);

        //        return data;
        //    }
        //    catch (Exception exc)
        //    {
        //        List<PanelBarItemModel> data = new List<PanelBarItemModel>();
        //        return data;
        //    }

        //}


    }
}
