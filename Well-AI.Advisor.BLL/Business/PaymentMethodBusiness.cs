using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.DLL.Repository;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Identity;

namespace WellAI.Advisor.BLL.Business
{
    public class PaymentMethodBusiness : IPaymentMethodBusiness
    {
        private readonly WebAIAdvisorContext db;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        public PaymentMethodBusiness(WebAIAdvisorContext db, RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager)
        {
            this.db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<bool> CreatePaymentMethod(CrmPaymentMethods model)
        {
            IPaymentMethodRepository repostirory = new PaymentMethodRepository(db, _roleManager, _userManager);
            return await repostirory.CreatePaymentMethod(model);
        }

        public async Task<CrmPaymentMethods> GetPaymentMethod(string userId)
        {
            IPaymentMethodRepository repostirory = new PaymentMethodRepository(db, _roleManager, _userManager);
            return await repostirory.GetPaymentMethod(userId);
        }
        public async Task<string> EncryptData(string CardNum)
        {
            IPaymentMethodRepository repostirory = new PaymentMethodRepository(db, _roleManager, _userManager);
            return await repostirory.EncryptData(CardNum);
        }
        public async Task<string> DecryptData(string EncodeData)
        {
            IPaymentMethodRepository repostirory = new PaymentMethodRepository(db, _roleManager, _userManager);
            return await repostirory.DecryptData(EncodeData);
        }
    }
}
