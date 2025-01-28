using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Helper;
using WellAI.Advisor.Model.Identity;
using System.Threading.Tasks;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using WellAI.Advisor.Areas.Identity;
using Microsoft.AspNetCore.Http;
using Finbuckle.MultiTenant;
using Microsoft.Extensions.Configuration;
using Well_AI.Advisor.Log.Error;
using System.Linq;
using WellAI.Advisor.Model.Common;

namespace WellAI.Advisor.Areas.OperatingCompany.Controllers
{
    [Area("OperatingCompany")]
    [SessionTimeOut]
    public class EditProfileController : BaseController
    {
        private readonly SignInManager<WellIdentityUser> _signInManager;
        private readonly ILogger<EditProfileController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly WebAIAdvisorContext db;
        private readonly IConfiguration _configuration;
        private IMapper _mapper;
        public EditProfileController(SignInManager<WellIdentityUser> signInManager, UserManager<WellIdentityUser> userManager,
                                     RoleManager<IdentityRole> roleManager,WebAIAdvisorContext dbContext, ILogger<EditProfileController> logger, 
                                     IConfiguration configuration, IMapper mapper)
            : base(userManager, dbContext)
        {
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _userManager = userManager;
            db = dbContext;
            _mapper = mapper;
            _configuration = configuration;
            var config = new MapperConfiguration(crf =>
            {
                crf.CreateMap<InputModel, WellIdentityUser>();
            });
            _mapper = config.CreateMapper();
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }
        public class InputModel
        {
            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Display(Name = "Middle Name")]
            public string MiddleName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [ScaffoldColumn(false)]
            public string UserId { get; set; }

            [Display(Name = "Title")]
            public string JobTitle { get; set; }

            [Display(Name = "Mobile")]
            public string Mobile { get; set; }

            [Display(Name = "City")]
            public string City { get; set; }

            [Display(Name = "State")]
            public string State { get; set; }

            [Display(Name = "Zip")]
            public string Zip { get; set; }

            [Display(Name = "Address")]
            public string Address { get; set; }

            [Display(Name = "Phone")]
            public string Phone { get; set; }

            [HiddenInput]
            [Display(Name = "Phone")]
            public string Email { get; set; }
            public string ProfileImageName { get; set; }
            public IFormFile ProfileImage { get; set; }
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                if (_signInManager.IsSignedIn(User) == false)
                {
                    string returnUrl = @"/Identity/Account/Login";
                    return LocalRedirect(returnUrl);
                }
                var user = await _userManager.GetUserAsync(User);
                Input = new InputModel
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    LastName = user.LastName,
                    City = user.City,
                    State = user.State,
                    Zip = user.Zip,
                    Phone = user.PhoneNumber,
                    Address = user.Address,
                    JobTitle = user.JobTitle,
                    Mobile = user.Mobile,
                    Email = user.Email,
                    ProfileImageName=user.ProfileImageName
                };
                if (Input.ProfileImageName != null && Input.ProfileImageName != "")
                {
                    Input.ProfileImageName = await GetUrlOfImage(Input.ProfileImageName);
                    WellAIAppContext.Current.Session.SetString("ProfileImageName", Input.ProfileImageName);
                }
                else
                {
                    var profileImageName = "/img/nophotouser1.png";
                    WellAIAppContext.Current.Session.SetString("ProfileImageName", profileImageName);
                }
                return View(Input);
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "EditProfile Index", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
       private async Task<string> GetUrlOfImage(string filename)
        {
            try
            {
                var ti = HttpContext.GetMultiTenantContext().TenantInfo;
                var blobSection = _configuration.GetSection("AzureBlob");
                var folderName = _configuration.GetSection("FolderName");
                var items = await AzureBlobStorage.GetFileBlobContainer(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                    blobSection["ContainerPrefixName"], ti.Id, folderName["CompanyUserProfile"], filename);
                return items;
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "EditProfile Index", User.Identity.Name);
                return string.Empty;
            }
        }
        [HttpPost]
        public async Task<ActionResult> Update()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                        var tenantId = await commonBusiness.GetTenantIdByUserId(Input.UserId);
                        if (Input.ProfileImage != null)
                        {
                            string filename = await SaveFile(Input.ProfileImage, Input.UserId);
                            Input.ProfileImageName = filename;
                        }
                        var welluser = new WellIdentityUser
                        {
                            Id = Input.UserId,
                            FirstName = Input.FirstName,
                            MiddleName = Input.MiddleName,
                            LastName = Input.LastName,
                            City = Input.City,
                            State = Input.State,
                            Zip = Input.Zip,
                            PhoneNumber = Input.Phone,
                            Address = Input.Address,
                            JobTitle = Input.JobTitle,
                            Mobile = Input.Mobile,
                            Email = Input.Email,
                            TenantId = tenantId,
                            ProfileImageName = Input.ProfileImageName,
                            EmailConfirmed = true
                        };
                        var result = await commonBusiness.UpdateUser(welluser);

                        //UpdateuserCrmcomapies
                        var crmUserBasicDetail = new DLL.Entity.CrmUserBasicDetail
                        {
                            UserId = welluser.Id,
                            Name = string.Format("{0} {1} {2}", welluser.FirstName, welluser.MiddleName, welluser.LastName),
                            ModifiedDate = DateTime.UtcNow
                        };

                        var status = commonBusiness.UpdateUserBasicDetail(crmUserBasicDetail);
                    }
                    catch (Exception ex)
                    {
                        ErrorHandler errorHandler = new ErrorHandler(_signInManager, _roleManager, _userManager, db);
                        errorHandler.ErrorLog(ex.Message, "Edit profile page.", ex.HResult.ToString());
                        _logger.LogInformation(ex.Message);
                        ModelState.AddModelError(string.Empty, "Server side error is coming, please check it with support team.");
                    }
                }
                return RedirectToAction("Index", "EditProfile");
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "EditProfile Update", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                string returnUrl = @"/Dashboard/Error";
                return LocalRedirect(returnUrl);
            }
        }
        private async Task<string> SaveFile(IFormFile file,string fileName)
        {
            object result = null;
            try
            {
                var ti = HttpContext.GetMultiTenantContext().TenantInfo;
                if (ti != null)
                {
                    var folderName = _configuration.GetSection("FolderName");
                    var blobSection = _configuration.GetSection("AzureBlob");
                    result = await AzureBlobStorage.UploadFileToBlobContainerWithFileName(blobSection["StorageAccountName"], blobSection["StorageAccountKey"],
                        blobSection["ContainerPrefixName"], ti.Id, file, folderName["CompanyUserProfile"], fileName);
                    string AuthorId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    System.Type type = result.GetType();
                    Uri docUri = (Uri)type.GetProperty("uri").GetValue(result, null);
                    return System.IO.Path.GetFileName(docUri.OriginalString);
                }
                return "";
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "EditProfile Savefile", User.Identity.Name);
                return "";
            }
        }

        public IActionResult OnPassWordChange()
        {

            return PartialView("_PassWordChanges");
        }


        [HttpPost]
        public async Task<bool> ChangePassword([FromBody] Password password)
        {
            try
            {

                ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                //var tenantId = await commonBusiness.GetTenantIdByUserId(UserId1);

                var user = await _userManager.GetUserAsync(User);
                var changePasswordResult = await _userManager.ChangePasswordAsync(user, password.CurrentPassWord, password.NewPassWord);
                if (!changePasswordResult.Succeeded)
                {
                    foreach (var error in changePasswordResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return false;
                }
                else
                {
                    return true;
                    //await _signInManager.RefreshSignInAsync(user);

                }                
            }
            catch (Exception ex)
            {
                ErrorHandler errorHandler = new ErrorHandler(_signInManager, _roleManager, _userManager, db);
                errorHandler.ErrorLog(ex.Message, "Change Password", ex.HResult.ToString());
                _logger.LogInformation(ex.Message);
                return false;
            }
        }
        [HttpGet]
        public async Task<bool> IsValidPassword(string passWord)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        string userName = WellAIAppContext.Current.Session.GetString("Email").Trim();

                        ICommonBusiness commonBusiness = new CommonBusiness(db, _roleManager, _userManager);
                        var result = await _signInManager.PasswordSignInAsync(userName, passWord, false, lockoutOnFailure: false);
                        if (result.Succeeded)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorHandler errorHandler = new ErrorHandler(_signInManager, _roleManager, _userManager, db);
                        errorHandler.ErrorLog(ex.Message, "Edit profile page.", ex.HResult.ToString());
                        _logger.LogInformation(ex.Message);
                        ModelState.AddModelError(string.Empty, "IsValidPassword");
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                CustomErrorHandler customErrorHandler = new CustomErrorHandler(_roleManager, _userManager, db, Guid.Parse(_userManager.GetUserId(User)), Guid.Parse(WellAIAppContext.Current.Session.GetString("TenantId")));
                customErrorHandler.WriteError(ex, "EditProfile Update", User.Identity.Name);
                _logger.LogInformation(ex.Message);
                return false;
            }
        }
    }
}