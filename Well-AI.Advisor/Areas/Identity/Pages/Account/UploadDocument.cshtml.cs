using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using WellAI.Advisor.BLL.Business;
 
using WellAI.Advisor.Helper;
using Microsoft.Net.Http.Headers;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Identity;
using Well_AI.Advisor.Log.Error;

namespace WellAI.Advisor.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class UploadDocumentModel : PageModel
    {
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly ILogger<UploadDocumentModel> _logger;
        RoleManager<IdentityRole> roleManager;
        private readonly WebAIAdvisorContext db;
        private readonly IWebHostEnvironment _env;
        [Obsolete]
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        public static IConfiguration Configuration { get; set; }

        [Obsolete]
        public UploadDocumentModel(
            IWebHostEnvironment env,
            UserManager<WellIdentityUser> userManager,
            SignInManager<WellIdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<UploadDocumentModel> logger,
            WebAIAdvisorContext dbContext, IHostingEnvironment environment)
        {
            _env = env;
            _userManager = userManager;
            this.roleManager = roleManager;
            _signInManager = signInManager;
            _logger = logger;
            db = dbContext;
            _hostingEnvironment = environment;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            public List<IFormFile> files { get; set; }

            [Required]
            public string UserId { get; set; }

            public bool DocumentUploadStatus { get; set; }
        }

        public async Task<IActionResult> OnGet(string userId = null)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    ModelState.AddModelError(string.Empty, "A userId must be supplied to add documents.");
                }
                ISharedDocumentBusiness sharedDocumentBusiness = new SharedDocumentBusiness(db, roleManager, _userManager);
                var result = await sharedDocumentBusiness.GetSharedDocuments(userId);

                if (result.Count > 0)
                {
                    Input = new InputModel
                    {
                        UserId = userId,
                        DocumentUploadStatus = true
                    };
                }
                else
                {
                    Input = new InputModel
                    {
                        UserId = userId,
                        DocumentUploadStatus = false
                    };
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "UploadDocuments - OnGet", User.Identity.Name);

                _logger.LogInformation(ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return Page();
        }

        [Obsolete]
        private IEnumerable<string> GetFileInfo(IEnumerable<IFormFile> files, string userId)
        {
            List<string> fileInfo = new List<string>();
            List<CrmSharedDocuments> crmSharedDocuments = new List<CrmSharedDocuments>();
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
                    Configuration = builder.Build();
                    string sharedDocument = Configuration.GetValue<string>("Documents:SharedDocument");

                    var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
                    var path7 = Path.Combine(_hostingEnvironment.WebRootPath + sharedDocument + filename);
                    using (var filestream1 = new FileStream(Path.Combine(path7), FileMode.Create))
                    {
                        file.CopyTo(filestream1);
                        filestream1.Flush();
                    }
                    CrmSharedDocuments crmSharedDocument = new CrmSharedDocuments();
                    crmSharedDocument.UserId = userId;
                    crmSharedDocument.DocumentPath = (sharedDocument + filename);
                    crmSharedDocument.DocumentName = filename.ToString();
                    crmSharedDocuments.Add(crmSharedDocument);
                }
            }
            ISharedDocumentBusiness sharedDocumentBusiness = new SharedDocumentBusiness(db, roleManager, _userManager);
            sharedDocumentBusiness.CreateSharedDocument(crmSharedDocuments);
            return fileInfo;
        }

        [Obsolete]
        public IActionResult OnPostAsync(IEnumerable<IFormFile> files)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    ICommonBusiness commonBusiness = new CommonBusiness(db, roleManager, _userManager);
                    IEnumerable<string> fileInfo = new List<string>();
                    if (files != null)
                    {
                        fileInfo = GetFileInfo(files, Input.UserId);
                        //Updating page status//
                    commonBusiness.UpdateUserPagesCompleteStatus(5, Input.UserId);

                        return RedirectToPage("Subscription", new { userId = Input.UserId });
                    }
                }
                catch (Exception ex)
                {
                    CustomErrorHandler customErrorHandler = new CustomErrorHandler(roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                    customErrorHandler.WriteError(ex, "Register- Shared document.", User.Identity.Name);

                    _logger.LogInformation(ex.Message);
                    ModelState.AddModelError(string.Empty, "Server side error is coming, please check it with support team.");
                }
            }
            return Page();
        }
    }
}

