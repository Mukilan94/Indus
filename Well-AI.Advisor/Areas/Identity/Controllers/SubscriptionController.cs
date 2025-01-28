using Microsoft.AspNetCore.Mvc;
using WellAI.Advisor.Model.Identity;

namespace WellAI.Advisor.Areas.Controllers
{
    public class SubscriptionController : Controller
    {
        public IActionResult Index()
        {
            var model = new UserDetailsModel()
            {
                AccountDetails = new AccountDetailsModel() { Username = "johny", Email = "john.doe@email.com", Password = "pass123" },
                PersonalDetails = new PersonalDetailsModel() { FullName = "", Country = "", Gender = "", About = "" },
                PaymentDetails = new PaymentDetailsModel() { PaymentType = "", CardNumber = "", CSVNumber = "", ExpirationDate = "", CardHolderName = "" }
            };
            return View(model);
        }
    }
}
