using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Model.Administration;
using WellAI.Advisor.Model.Identity;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace WellAI.Advisor.BLL.Administration
{
    public interface IServiceCategoryBusiness
    {
        Task<List<ServiceCategoryModel>> GetServiceCategories();
        Task<List<ServiceCategoryModel>> GetServiceSubCategories(string ParentId);
        Task<List<ServiceCategoryModel>> GetParentServiceCategories();
        Task<string> AddServiceCategory(ServiceCategoryModel serviceCategory);
        Task<string> UpdateServiceCategory(ServiceCategoryModel serviceCategory);
        Task<int> CheckParentServiceCategory(ServiceCategoryModel serviceCategory);
        Task<int> CheckServiceCategoryNamebyParentId(ServiceCategoryModel serviceCategory);
        Task<List<ServiceCategoryModel>> GetCategoryParentList();
        Task<List<ServiceCategoryModel>> GetSubCategoryList(string Parentid);
    }
    public class ServiceCategoryBusiness : IServiceCategoryBusiness
    {
        private readonly WebAIAdvisorContext db;
        UserManager<WellIdentityUser> _userManager;
        protected readonly IMapper _mapper;

        public ServiceCategoryBusiness(WebAIAdvisorContext db, UserManager<WellIdentityUser> userManager, IMapper mapper)
        {
            this.db = db;
            _userManager = userManager;
            _mapper = mapper;
        }

        public Task<string> AddServiceCategory(ServiceCategoryModel model)
        {
            try
            {
                if (model.ServiceCategoryId == model.ParentId && CheckParentServiceCategory(model).Result > 0)
                {
                    return Task.FromResult("Category name already exist");
                }
                else if (CheckServiceCategoryNamebyParentId(model).Result > 0)
                {
                    return Task.FromResult("Sup-category name already exist");
                }
                ServiceCategory service = new ServiceCategory()
                {
                    ServiceCategoryId = model.ServiceCategoryId,
                    CreatedDate = DateTime.UtcNow,
                    Description = model.Description,
                    Name = model.Name,
                    IsActive = model.IsActive,
                    ParentId = model.ParentId,
                    CreatedBy = model.CreatedBy,
                };
                db.serviceCategories.Add(service);
                db.SaveChanges();
                return Task.FromResult(true.ToString());
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, _userManager, db, null);
                customErrorHandler.WriteError(ex, "IServiceCategory AddServiceCategory", null);
                return null;
            }
        }

        public Task<int> CheckParentServiceCategory(ServiceCategoryModel serviceCategory)
        {
            try
            {
                return Task.FromResult(db.serviceCategories.Where(x => x.ServiceCategoryId == x.ParentId && x.Name == serviceCategory.Name).Count());
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, _userManager, db, null);
                customErrorHandler.WriteError(ex, "IServiceCategory CheckParentServiceCategory", null);
                return null;
            }
        }

        public Task<int> CheckServiceCategoryNamebyParentId(ServiceCategoryModel model)
        {
            try
            {
                var count = db.serviceCategories.Where(x => x.ParentId == model.ParentId && x.ServiceCategoryId == model.ServiceCategoryId);
                return Task.FromResult(db.serviceCategories.Where(x => x.ParentId == model.ParentId && x.ServiceCategoryId == model.ServiceCategoryId).Count());
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, _userManager, db, null);
                customErrorHandler.WriteError(ex, "IServiceCategory CheckServiceCategoryNamebyParentId", null);
                return null;
            }
        }



        public async Task<List<ServiceCategoryModel>> GetParentServiceCategories()
        {
            try
            {
                return await db.serviceCategories.Where(x => x.ServiceCategoryId == x.ParentId).Select(x => new ServiceCategoryModel { Name = x.Name, ParentId = x.ParentId }).ToListAsync();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, _userManager, db, null);
                customErrorHandler.WriteError(ex, "IServiceCategory GetParentServiceCategories", null);
                return null;
            }
        }

        public async Task<List<ServiceCategoryModel>> GetServiceCategories()
        {
            try
            {
                return await db.serviceCategories.Where(x => x.ServiceCategoryId == x.ParentId)
                    .Select(x => new ServiceCategoryModel
                    {
                        ServiceCategoryId = x.ServiceCategoryId,
                        Name = x.Name,
                        Description = x.Description,
                        ParentName = x.Name,
                        ParentId = x.ParentId,
                        IsActive = x.IsActive,
                    }).ToListAsync();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, _userManager, db, null);
                customErrorHandler.WriteError(ex, "IServiceCategory GetServiceCategories", null);
                return null;
            }
        }

        public async Task<List<ServiceCategoryModel>> GetServiceSubCategories(string ParentId)
        {
            try
            {
                return await db.serviceCategories.Where(x => x.ParentId == ParentId && x.ServiceCategoryId != ParentId && x.IsActive == true)
                        .Select(x => new ServiceCategoryModel
                        {
                            ServiceCategoryId = x.ServiceCategoryId,
                            Name = x.Name,
                            Description = x.Description,
                            ParentName = x.Name,
                            ParentId = x.ParentId,
                            IsActive = x.IsActive,
                        }).OrderBy(x => x.Name).ToListAsync();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, _userManager, db, null);
                customErrorHandler.WriteError(ex, "IServiceCategory GetServiceSubCategories", null);
                return null;
            }
        }



        public async Task<List<ServiceCategoryModel>> GetCategoryParentList()
        {
            try
            {
                return await db.serviceCategories.Where(x => x.ServiceCategoryId == x.ParentId).
                            Select(x => new ServiceCategoryModel { ParentId = x.ServiceCategoryId, ParentName = x.Name }).OrderBy(x => x.ParentName).ToListAsync();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, _userManager, db, null);
                customErrorHandler.WriteError(ex, "IServiceCategory GetCategoryParentList", null);
                return null;
            }
        }
        public async Task<List<ServiceCategoryModel>> GetSubCategoryList(string Parentid)
        {
            try
            {
                return await db.serviceCategories.Where(x => x.ParentId == Parentid && x.ServiceCategoryId != Parentid).
                            Select(x => new ServiceCategoryModel { ServiceCategoryId = x.ServiceCategoryId, Name = x.Name }).OrderBy(x => x.Name).ToListAsync();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, _userManager, db, null);
                customErrorHandler.WriteError(ex, "IServiceCategory GetSubCategoryList", null);
                return null;
            }
        }



        public Task<string> UpdateServiceCategory(ServiceCategoryModel model)
        {
            try
            {
                if (model.ServiceCategoryId == model.ParentId && CheckParentServiceCategory(model).Result > 1)
                {
                    return Task.FromResult("Category name already existing");
                }
                else if (CheckServiceCategoryNamebyParentId(model).Result > 1)
                {
                    return Task.FromResult("Sup-category name already existing");
                }
                var result = db.serviceCategories.Where(x => x.ServiceCategoryId.Equals(model.ServiceCategoryId)).FirstOrDefault();
                if (result == null)
                {
                    return Task.FromResult(false.ToString());
                }

                result.Name = model.Name;
                result.ParentId = model.ParentId;
                result.Description = model.Description;
                result.ModifiedBy = model.ModifiedBy;
                //DWOP
                result.IsActive = model.IsActive;
                db.SaveChanges();
                return Task.FromResult(true.ToString());
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorBLL customErrorHandler = new CustomErrorHandlerForAdvisorBLL(null, _userManager, db, null);
                customErrorHandler.WriteError(ex, "IServiceCategory UpdateServiceCategory", null);
                return null;
            }
        }
    }
}
