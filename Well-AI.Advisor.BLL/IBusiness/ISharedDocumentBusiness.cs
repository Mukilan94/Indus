using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Model.OperatingCompany.Models;

namespace WellAI.Advisor.BLL.Business
{
    public interface ISharedDocumentBusiness
    {
        Task<bool> CreateSharedDocument(List<CrmSharedDocuments> model);
        Task<List<CrmSharedDocuments>> GetSharedDocuments(string useId);
    }
}
