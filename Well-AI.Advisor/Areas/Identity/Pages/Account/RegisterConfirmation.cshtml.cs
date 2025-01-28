using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WellAI.Advisor.Model.Identity;

namespace WellAI.Advisor.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterConfirmationModel : PageModel
    {
        private readonly UserManager<WellIdentityUser> _userManager;
        
        public RegisterConfirmationModel(UserManager<WellIdentityUser> userManager)
        {
            _userManager = userManager;
            
        }

        public string Email { get; set; }
        public string Type { get; set; }

        public bool DisplayConfirmAccountLink { get; set; }

        public string EmailConfirmationUrl { get; set; }

        public IActionResult OnGetAsync(string email, string type)
        {
            if (string.IsNullOrEmpty(email) == true && string.IsNullOrEmpty(type) == true)
            {
                return RedirectToPage("/Index");
            }
            
            Email = email;
            Type = type;
            
            return Page();
        }
    }
}
