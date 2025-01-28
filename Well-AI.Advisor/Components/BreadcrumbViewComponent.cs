using Microsoft.AspNetCore.Mvc;

namespace WellAI.Advisor
{
    public class BreadcrumbViewComponent : ViewComponent
    {
        public BreadcrumbViewComponent()
        {
        }

        public IViewComponentResult Invoke()
        {
            return View("_Breadcrumb");
        }
    }
}
