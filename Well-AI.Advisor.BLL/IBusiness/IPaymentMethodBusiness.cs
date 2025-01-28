using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Model.OperatingCompany.Models;

namespace WellAI.Advisor.BLL.Business
{
    public interface IPaymentMethodBusiness
    {
        Task<bool> CreatePaymentMethod(CrmPaymentMethods model);
        Task<CrmPaymentMethods> GetPaymentMethod(string userId);
        Task<string> EncryptData(string CardNum);
        Task<string> DecryptData(string EncodeData);

    }
}
