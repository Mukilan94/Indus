using System.Collections.Generic;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Model.Common;
using WellAI.Advisor.Model.ServiceCompany.Models;

namespace WellAI.Advisor.DLL.IRepository
{
    public interface IServiceTenantRepository
    {
        Task<List<ClientContact>> GetClientContactsList(string userId);
        Task<bool> UpdateClientContacts(int clientContactId);
        Task<List<PaymentMethod>> GetPaymentMethods(string tenantId);
        Task<int> UpdatePaymentMethod(PaymentMethod input);
        Task<int> DeletePaymentMethod(string methodId);
        Task<List<ServiceBillingHistory>> GetBillingHistoryInvoices();
  
        Task<List<OperatingProviderProfile>> GetProviderDirectoriesByTenantId(string tenantId);

        Task<WellAI.Advisor.Model.OperatingCompany.Models.CorporateProfile> GetProviderDirectoryByTenantId(string tenantId);
        /// <summary>
        /// Phase II Changes - 02/08/2021 - Getting OperatingDirector Id
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public Task<OperatingProviderProfile> GetOperatingDirectoryID(string tenantId,string OprTenantid);
    }
}
