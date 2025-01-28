using System.Collections.Generic;
using System.Threading.Tasks;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.Model.Common;
using WellAI.Advisor.DLL.Entity;
namespace WellAI.Advisor.DLL.Repository
{
    public interface IOperatingTenantRepository
    {
        Task<List<PaymentMethod>> GetPaymentMethods(string tenantId);
        Task<int> UpdatePaymentMethod(PaymentMethod input);
        Task<int> DeletePaymentMethod(string methodId);
        Task<List<BillingHistory>> GetBillingHistoryInvoices(string tenantId);

        Task<List<ClientContact>> GetClientContactsList(string userId);
        Task<bool> UpdateClientContacts(int clientContactId);
        Task<List<string>> GetProviderDirectoryId(string tenantId);
        Task<List<ProviderDirectory>> GetProviderDirectory(string tenantId);
    }
}
