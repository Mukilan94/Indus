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

namespace WellAI.Advisor.DLL.Repository
{
    public class SharedDocumentRepository : ISharedDocumentRepository
    {
        private readonly WebAIAdvisorContext db;
        RoleManager<IdentityRole> _roleManager;
        UserManager<WellIdentityUser> _userManager;
        public SharedDocumentRepository(WebAIAdvisorContext db, RoleManager<IdentityRole> roleManager, UserManager<WellIdentityUser> userManager)
        {
            this.db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public async Task<bool> CreateSharedDocument(List<CrmSharedDocuments> model)
        {
            try
            {
                var result = db.CrmSharedDocuments.Where(x => x.UserId == model.FirstOrDefault().UserId).ToList();
                if (result.Count > 0)
                {
                    db.CrmSharedDocuments.RemoveRange(result);
                    db.SaveChanges();
                }
                db.CrmSharedDocuments.AddRange(model);
                db.SaveChanges();
                return await Task.FromResult(true);
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "SharedDocument CreateSharedDocument", null);
                return false;
            }
        }

        public async Task<List<CrmSharedDocuments>> GetSharedDocuments(string useId)
        {
            try
            {
                return await Task.FromResult(db.CrmSharedDocuments.Where(x => x.UserId == useId).ToList());
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(_roleManager, _userManager, db, null);
                customErrorHandler.WriteError(ex, "SharedDocument GetSharedDocuments", null);
                return null;
            }
        }
    }
}
