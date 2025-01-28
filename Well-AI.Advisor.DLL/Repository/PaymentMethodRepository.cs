using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Identity;
using System.Text;

namespace WellAI.Advisor.DLL.Repository
{
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly WebAIAdvisorContext db;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        public PaymentMethodRepository(WebAIAdvisorContext db, RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager)
        {
            this.db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public async Task<bool> CreatePaymentMethod(CrmPaymentMethods model)
        {
            try
            {
                model.CreditCardNumber = await EncryptData(model.CreditCardNumber);
                model.ValidUptoDate = await EncryptData(model.ValidUptoDate);

                var result = (from p in db.CrmPaymentMethods
                              where p.UserId == model.UserId
                              select p).FirstOrDefault();
                if (result == null)
                {
                    db.CrmPaymentMethods.Add(model);
                    db.SaveChanges();
                }
                else
                {
                    result.CustomerName = model.CustomerName;
                    result.CreditCardNumber = model.CreditCardNumber;
                    result.ValidUptoDate = model.ValidUptoDate;
                    db.SaveChanges();
                }
                return true;
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "PaymentMethod CreatePaymentMethod", null);
                return false;
            }
        }

        public async Task<CrmPaymentMethods> GetPaymentMethod(string userId)
        {
            try
            {
                return await Task.FromResult((from p in db.CrmPaymentMethods
                        where p.UserId == userId
                        select p).FirstOrDefault());
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "PaymentMethod GetPaymentMethod", null);
                return null;
            }
        }


        public async Task<string> EncryptData(string CardNum)
        {
            try
            {
                var CardNumBytes = Encoding.UTF32.GetBytes(Convert.ToString(CardNum));
                var CardHashCode = Convert.ToBase64String(CardNumBytes);
                return await Task.FromResult(CardHashCode);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "PaymentMethod EncryptData", null);
                return CardNum;
            }
        }

        public async Task<string> DecryptData(string EncodeData)
        {
            try
            {
                if (!string.IsNullOrEmpty(EncodeData) && !EncodeData.All(Char.IsDigit))
                {
                    var CardDetailBytes = Convert.FromBase64String(EncodeData);

                    var CardDetail = Encoding.UTF32.GetString(CardDetailBytes);

                    return CardDetail;
                }

                return await Task.FromResult(EncodeData);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "PaymentMethod DecryptData", null);
                return EncodeData;
            }
        }

    }
}
