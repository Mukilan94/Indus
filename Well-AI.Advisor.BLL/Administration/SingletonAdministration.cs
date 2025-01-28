using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.BLL.IBusiness;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.IRepository;
using WellAI.Advisor.DLL.Repository;
using WellAI.Advisor.Model.Identity;

namespace WellAI.Advisor.BLL.Administration
{
    public interface ISingletonAdministration
    {
        IServiceCategoryBusiness serviceCategoryBusiness { get; }
        ISubscriptionBusiness subscriptionBusiness { get; }
        IWellTaskBusiness  wellTaskBusiness{ get; }
        ICustomerProfileBusiess customerProfileBusiess { get; }
        IStaffBusiness staffBusiness { get; }
        IConfigurationBusiness configurationBusiness { get; }     
        IUsersManagerBusiness usersManagerBusiness { get; }
        ICommonBusiness commonBusiness { get; }
        IAIDataRepository aiDataRepositoryBusiess { get; }
        IWellPredictionBusiness wellPredictionBusiness { get; }
        int savechange();
    }

    public class SingletonAdministration: ISingletonAdministration
    {
        private readonly WebAIAdvisorContext _context;
        private readonly UserManager<WellIdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly UserManager<StaffWellIdentityUser> staffUserManager;
        private readonly SignInManager<StaffWellIdentityUser> staffSignInManager;
        private readonly WebAIAdvisorContext db;
        protected readonly IMapper _mapper;

        public SingletonAdministration(WebAIAdvisorContext context, UserManager<WellIdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager,IMapper mapper, SignInManager<StaffWellIdentityUser> staffSignInManager, 
            UserManager<StaffWellIdentityUser> staffUserManager, WebAIAdvisorContext db)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            this.staffSignInManager = staffSignInManager;
            this.staffUserManager = staffUserManager;
            _roleManager = roleManager;
            this.db = db;
        }


        private IWellPredictionBusiness wellPrediction;
        public IWellPredictionBusiness wellPredictionBusiness
        {
            get
            {
                if (wellPrediction == null)
                {
                    wellPrediction = new WellPredictionBusiness(_context,  _mapper);
                }
                return wellPrediction;
            }
        }


        #region Old


        private IUsersManagerBusiness usersManager;
        public IUsersManagerBusiness usersManagerBusiness
        {
            get
            {
                if (usersManager == null)
                {
                    usersManager = new UsersManagerBusiness(_context, _userManager,_mapper);
                }
                return usersManager;
            }
        }
        private IStaffBusiness staff;
        public IStaffBusiness staffBusiness
        {
            get
            {
                if (staff == null)
                {
                    staff = new StaffBusiness(staffUserManager, staffSignInManager,db);
                }
                return staff;
            }
        }


        private ISubscriptionBusiness subscription;
        public ISubscriptionBusiness subscriptionBusiness
        {
            get
            {
                if (subscription == null)
                {
                    subscription = new SubscriptionBusiness(_context);
                }
                return subscription;
            }
        }

        private IConfigurationBusiness configuration;
        public IConfigurationBusiness configurationBusiness
        {
            get
            {
                if (configuration == null)
                {
                    configuration = new ConfigurationBusiness(_context);
                }
                return configuration;
            }
        }

        private IServiceCategoryBusiness serviceCategory;
        public IServiceCategoryBusiness serviceCategoryBusiness
        {
            get
            {
                if (serviceCategory == null)
                {
                    serviceCategory = new ServiceCategoryBusiness(_context, _userManager, _mapper);
                }
                return serviceCategory;
            }
        }

        private IWellTaskBusiness wellTask;
        public IWellTaskBusiness wellTaskBusiness 
        {
            get
            {
                if (wellTask == null)
                {
                    wellTask = new WellTaskBusiness(_context, _userManager,_mapper, _roleManager);
                }
                return wellTask;
            }
        }

        private ICustomerProfileBusiess customerProfile;
        public ICustomerProfileBusiess customerProfileBusiess
        {
            get
            {
                if (customerProfile == null)
                {
                    customerProfile = new CustomerProfileBusiess(_context, _userManager,_roleManager, _mapper);
                }
                return customerProfile;
            }
        }
        private ICommonBusiness common;
        public ICommonBusiness commonBusiness {
            get
            {
                if (common == null)
                {
                    common = new CommonBusiness(_context, _roleManager, _userManager);
                }
                return common;
            }
        }
        private IAIDataRepository aIDataRepository;
        public IAIDataRepository aiDataRepositoryBusiess
        {
            get
            {
                if (aIDataRepository == null)
                {
                    aIDataRepository = new AIDataRepository(_context, _roleManager, _userManager);
                }
                return aIDataRepository;
            }
        }

        public int savechange()
        {
            return _context.SaveChanges();
        }

        #endregion

    }
}
