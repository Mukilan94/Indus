using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.DLL.Repository;
//using WellAI.Advisor.Areas.OperatingCompany.Models;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.Identity;

namespace WellAI.Advisor.BLL.Business
{
    public class SharedDocumentBusiness : ISharedDocumentBusiness
    {
        private readonly WebAIAdvisorContext db;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        public SharedDocumentBusiness(WebAIAdvisorContext db, RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager)
        {
            this.db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<bool> CreateSharedDocument(List<CrmSharedDocuments> model)
        {
            ISharedDocumentRepository repostirory = new SharedDocumentRepository(db, _roleManager, _userManager);
            return await repostirory.CreateSharedDocument(model);
        }

        public async Task<List<CrmSharedDocuments>> GetSharedDocuments(string useId)
        {
            ISharedDocumentRepository repostirory = new SharedDocumentRepository(db, _roleManager, _userManager);
            return await repostirory.GetSharedDocuments(useId);
        }
    }
}
