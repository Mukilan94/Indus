using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Administration;
using WellAI.Advisor.Model.Identity;

namespace WellAI.Advisor.BLL.Administration
{
    public interface IStaffBusiness
    {
        Task<List<RegisterStaffViewModel>> GetStaffs(bool IsActive);
        Task<RegisterStaffViewModel> AddStaff(RegisterStaffViewModel input);
        Task<RegisterStaffViewModel> UpdateStaff(RegisterStaffViewModel input);
    }


    public class StaffBusiness : IStaffBusiness
    {
        private readonly UserManager<StaffWellIdentityUser> userManager;
        private readonly SignInManager<StaffWellIdentityUser> signInManager;
        private readonly WebAIAdvisorContext db;
        public StaffBusiness(UserManager<StaffWellIdentityUser> userManager, SignInManager<StaffWellIdentityUser> signInManager, 
            WebAIAdvisorContext db)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.db = db;
        }
        public async Task<RegisterStaffViewModel> AddStaff(RegisterStaffViewModel model)
        {
            try
            {
                var user = new StaffWellIdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    FullName = model.FullName,
                    EmailConfirmed = true,
                    IsActive = true
                };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var code = await userManager.GeneratePasswordResetTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    return model;
                }
                return model;
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "IStaff UpdateServiceCategory", null);
                return null;
            }
        }

        public async Task<List<RegisterStaffViewModel>> GetStaffs(bool IsActive)
        {
            try
            {
                var staff = userManager.Users.ToList();
                var staffs = await userManager.Users.Where(x => x.IsActive == IsActive).Select(x => new RegisterStaffViewModel
                {
                    Email = x.Email,
                    FullName = x.FullName,
                    PhoneNumber = x.PhoneNumber,
                    Id = x.Id,
                    IsActive = x.IsActive
                }).ToListAsync();
                return staffs;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "IStaff GetStaffs", null);
                return null;
            }
        }

        public async Task<RegisterStaffViewModel> UpdateStaff(RegisterStaffViewModel input)
        {
            try
            {
                var output = await userManager.FindByIdAsync(input.Id);
                if (output != null)
                {
                    output.UserName = input.Email;
                    output.Email = input.Email;
                    output.PhoneNumber = input.PhoneNumber;
                    output.FullName = input.FullName;
                    output.IsActive = input.IsActive;
                    var result = await userManager.UpdateAsync(output);
                };
                return input;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, null, db, null);
                customErrorHandler.WriteError(ex, "IStaff UpdateStaff", null);
                return null;
            } 
        }
    }

}
