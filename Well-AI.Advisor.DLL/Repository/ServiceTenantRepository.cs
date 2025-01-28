using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WellAI.Advisor.DLL.IRepository;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.Common;
using WellAI.Advisor.Model.ServiceCompany.Models;
using WellAI.Advisor.Model.Tenant.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.Model.OperatingCompany.Models;
using PaymentMethod = WellAI.Advisor.DLL.Entity.PaymentMethod;// WellAI.Advisor.Model.ServiceCompany.Models.PaymentMethod;
using SubscriptionViewModel = WellAI.Advisor.Model.ServiceCompany.Models.SubscriptionViewModel;
using ActivityViewModel = WellAI.Advisor.Model.ServiceCompany.Models.ActivityViewModel;
using TaskSchedule = WellAI.Advisor.Model.ServiceCompany.Models.TaskSchedule;
using System.Text;

//NOTE : _servicedb - changed to _db in all paymentmethod details statement
//Payment method details will be saved into main DB instead of Operating tenant DB. Since Dispatch type will not
//be having separate db. So all types need to have payment method details saved at main DB.

//ServicePaymentMethods - changed to PaymentMethod
namespace WellAI.Advisor.DLL.Repository
{
    public class ServiceTenantRepository : IServiceTenantRepository
    {
        private readonly TenantServiceDbContext _servicedb;
        private readonly HttpContext _httpcontext;
        UserManager<WellIdentityUser> _userManager;
        private readonly WebAIAdvisorContext _db;

        public ServiceTenantRepository(TenantServiceDbContext servicedb, HttpContext httpcontext, UserManager<WellIdentityUser> userManager, 
            WebAIAdvisorContext db)
        {
            _servicedb = servicedb;
            _httpcontext = httpcontext;
            _userManager = userManager;
            _db = db;
        }

        public ServiceTenantRepository(WebAIAdvisorContext db)
        {
            _db = db;
        }

        public ServiceTenantRepository(TenantServiceDbContext servicedb)
        {
            _servicedb = servicedb;
        }

        public async Task<List<ErdosDrillingDepthBased>> GetDepthChartData(string connectionstring)
        {
            List<ErdosDrillingDepthBased> depthdata = new List<ErdosDrillingDepthBased>();
            try
            {
              using (var sc = GetSqlConnection(connectionstring))
                    depthdata=  (List<ErdosDrillingDepthBased>)sc.Query<ErdosDrillingDepthBased>("SELECT [DRILLINGDEPTHBASEDId],[WID],[SKNO],[RID],[SQID],[DATE],[TIME],[ACTC],[DMEA],[DVER],[ROPA],[WOBA],[HKLA],[SPPA],[TQA],[RPMA],[BRVC],[MDIA],[ECDT],[MFIA],[MFOA],[MFOP],[TVA],[CPDI],[CPDC],[BDTI]      ,[BDDI]      ,[DXC]      ,[SPR1]      ,[SPR2]      ,[SPR3],[SPR4],[SPR5],[SPR6],[SPR7],[SPR8],[SPR9] FROM[dbo].[erdos_DrillingDepthBased]", commandType: CommandType.Text);

                if (depthdata !=null || depthdata.Count == 0)
                {
                    return await Task.FromResult(depthdata);
                }
                else
                {
                    return await Task.FromResult(depthdata);
                }
                             
            }
            catch (Exception ex )
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant GetDepthChartData", null);
                return depthdata;
            }
               
   
           
        }
        protected SqlConnection GetSqlConnection(string connectionstring)
        {
            return new SqlConnection(connectionstring);
        }
        public async Task<List<ErdosGeneralTimeBased>> GetTimeChartData(string connectionstring)
        {
            List<ErdosGeneralTimeBased> TimechartData = new List<ErdosGeneralTimeBased>();
            try
            {
                using (var sc = GetSqlConnection(connectionstring))
                    TimechartData=(List<ErdosGeneralTimeBased>)sc.Query<ErdosGeneralTimeBased>("SELECT [GeneralTimeBasedId],[WID],[SKNO],[RID],[SQID],[DATE],[TIME],[ACTC],[DBTM],[DBTV],[DMEA],[DVER],[BPOS],[ROPA],[HKLA],[HKLX],[WOBA],[WOBX],[TQA],[TQX],[RPMA],[SPPA],[CHKP],[SPM1],[SPM2],[SPM3],[TVA],[TVCA],[MFOP],[MFOA],[MFIA],[MDOA],[MDIA],[MTOA],[MTIA],[MCOA],[MCIA],[STKC],[LSTK],[DRTM],[GASA],[SPR1],[SPR2],[SPR3],[SPR4],[SPR5]  FROM [dbo].[erdos_GeneralTimeBased]", commandType: CommandType.Text);
              
                if (TimechartData != null || TimechartData.Count == 0)
                {
                    return await Task.FromResult(TimechartData);
                }
                else
                {
                    return await Task.FromResult(TimechartData);
                }

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant GetTimeChartData", null);
                return TimechartData;
            }
        }
        public async Task<bool> CreateClientContacts(List<ClientContact> clients)
        {
            try
            {
                if (_httpcontext.GetMultiTenantContext().TenantInfo != null)
                {
                    clients.ForEach(x => x.IsActive = true);
                    _servicedb.ClientContacts.UpdateRange(clients);

                    await _servicedb.SaveChangesAsync();

                    var result = AddCRMContacts(clients);

                    return await Task.FromResult(result.Result);
                }
                return false;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant CreateClientContacts", null);
                return false;
            }
        }

        public async Task<bool> UpdateClientContacts(int clientContactId)
        {
            try
            {
                if (_httpcontext.GetMultiTenantContext().TenantInfo != null)
                {
                    var client = _servicedb.ClientContacts.Where(x => x.ClientContactId == clientContactId).FirstOrDefault();
                    if (client != null)
                    {
                        client.IsActive = false;
                        _servicedb.ClientContacts.Update(client);
                        await _servicedb.SaveChangesAsync();

                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant UpdateClientContacts", null);
                return false;
            }
        }
       
        public Task<List<PaymentMethod>> GetPaymentMethods(string tenantId)
        {
            try
            {
                List<PaymentMethod> result = null;

          //      var tid = _httpcontext.GetMultiTenantContext().TenantInfo;

              //  if (_httpcontext.GetMultiTenantContext().TenantInfo != null)
               // {
                    result = _db.PaymentMethods.Where(x => x.TenantId == tenantId).ToList();
              //  }
              
                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant GetPaymentMethods", null);
                return null;
            }
        }

        public Task<PaymentMethod> GetSelectedPaymentDetail(string id)
        {
            try
            {
                PaymentMethod result = null;

                if (_httpcontext.GetMultiTenantContext().TenantInfo != null)
                {
                    result = _db.PaymentMethods.Where(x => x.ID == id).FirstOrDefault();
                }

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant GetSelectedPaymentDetail", null);
                return null;
            }
        }

        public async Task<int> UpdatePaymentMethod(PaymentMethod input)
        {
            try{
                int result = 0;

        //        if (_httpcontext.GetMultiTenantContext().TenantInfo != null)
              //  {
                    var welluser = await _userManager.GetUserAsync(_httpcontext.User);

                    if (string.IsNullOrEmpty(input.ID))
                    {
                        input.ID = Guid.NewGuid().ToString("D");

                        _db.PaymentMethods.Add(input);
                    }
                    else
                    {
                        var paymenthod = _db.PaymentMethods.FirstOrDefault(x => x.ID == input.ID);
                        if (paymenthod != null)
                        {
                            paymenthod.Default = input.Default;
                            paymenthod.ExpireMonth = input.ExpireMonth;
                            paymenthod.ExpireYear = input.ExpireYear;
                            paymenthod.Holder = input.Holder;
                            paymenthod.Number = input.Number;
                            paymenthod.Nickname = input.Nickname;
                            paymenthod.FirstName = input.FirstName;
                            paymenthod.LastName = input.LastName;
                            paymenthod.MiddleInitial = input.MiddleInitial;
                            paymenthod.PayType = input.PayType;
                            paymenthod.Address1 = input.Address1;
                            paymenthod.Address2 = input.Address2;
                            paymenthod.City = input.City;
                            paymenthod.Country = input.Country;
                            paymenthod.Zip = input.Zip;
                            paymenthod.State = input.State;
                            paymenthod.Agreement = input.Agreement;
                            paymenthod.TenantId = input.TenantId;
                            paymenthod.CVV = input.CVV;
                        }
                    }

                    result = await _db.SaveChangesAsync();
                //}

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant UpdatePaymentMethod", null);
                return 0;
            }
        }

        public async Task<int> DeletePaymentMethod(string methodId)
        {
            try
            {
                int result = 0;

                if (_httpcontext.GetMultiTenantContext().TenantInfo != null)
                {

                    var paymenthod = _db.PaymentMethods.FirstOrDefault(x => x.ID == methodId);
                    if (paymenthod != null)
                    {
                        _db.PaymentMethods.Remove(paymenthod);
                    }

                    result = await _db.SaveChangesAsync();
                }

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant DeletePaymentMethod", null);
                return 0;
            }
        }
        public Task<List<ServiceBillingHistory>> GetBillingHistoryInvoices()
        {
            try
            {
                List<ServiceBillingHistory> result = null;

                if (_httpcontext.GetMultiTenantContext().TenantInfo != null)
                {
                    result = _servicedb.ServiceBillingHistories.ToList();
                }
                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant GetBillingHistoryInvoices", null);
                return null;
            }
        }

        public Task<List<OperatingProviderProfile>> GetProviderDirectories()
        {
            try
            {
                List<OperatingProviderProfile> result = new List<OperatingProviderProfile>();

                if (_httpcontext.GetMultiTenantContext().TenantInfo != null)
                {
                    var providers = _servicedb.OperatingDirectory.ToList();
                    var approvals = _servicedb.OperatingDirectoryAppovals.ToList();
                    var statuses = _servicedb.OperatingDirectoryStatuses.ToList();
                    var pecs = _servicedb.OperatingDirectoryPECs.ToList();

                    foreach (var provider in providers)
                    {
                        var approval = approvals.FirstOrDefault(x => x.Id == provider.Approval);
                        var status = statuses.FirstOrDefault(x => x.Id == provider.Status);
                        var pec = pecs.FirstOrDefault(x => x.Id == provider.PEC);

                        var newprof = new OperatingProviderProfile
                        {
                            CompanyId = provider.CompanyId,
                            ProviderId = provider.ID,
                            ApprovalId = provider.Approval,
                            Approval = approval == null ? "" : approval.Name,
                            StatusId = provider.Status,
                            Status = status == null ? "" : status.Name,
                            PecStatusId = provider.PEC,
                            PecStatus = pec == null ? "" : pec.Name,
                            MSADocumentId = provider.MSA,
                            InsuranceStart = provider.InsuranceStart,
                            InsuranceExpire = provider.InsuranceExpire,
                            Preferred = provider.Preferred,
                            Secondary = provider.Secondary,
                            Rating = provider.Rating == 0 ? null : provider.Rating,
                            InsuranceId = provider.Insurance
                        };

                        result.Add(newprof);
                    }
                }

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant GetProviderDirectories", null);
                return null;
            }
        }

        public Task<List<OperatingProviderProfile>> GetSubsriptionOperators()
        {
            try
            {
                List<OperatingProviderProfile> result = new List<OperatingProviderProfile>();
                var providers = _servicedb.SubscriptionOperators.ToList();
                var approvals = _servicedb.OperatingDirectoryAppovals.ToList();
                var statuses = _servicedb.OperatingDirectoryStatuses.ToList();
                var pecs = _servicedb.OperatingDirectoryPECs.ToList();

                foreach (var provider in providers)
                {
                    var approval = approvals.FirstOrDefault(x => x.Id == provider.Approval);
                    var status = statuses.FirstOrDefault(x => x.Id == provider.Status);
                    var pec = pecs.FirstOrDefault(x => x.Id == provider.PEC);
                    var SubRigs = _servicedb.subscriptionOperatorRigs.Where(x => x.CompanyId == provider.CompanyId).ToList();
                    var RigList = (from rig in SubRigs
                                   join RigReg in _db.rig_register on rig.RigId equals RigReg.Rig_id
                                   where rig.CompanyId == provider.CompanyId
                                   select new
                                   {
                                       RigName = RigReg.Rig_Name
                                   }).Select(x => x.RigName).ToList();

                    var RigListId = (from rig in SubRigs
                                   join RigReg in _db.rig_register on rig.RigId equals RigReg.Rig_id
                                   where rig.CompanyId == provider.CompanyId
                                   select new
                                   {
                                       RigId = RigReg.Rig_id
                                   }).Select(x => x.RigId).ToList();

                    string RigName = String.Join(",", RigList);
                    string RigId = String.Join(",", RigListId);


                    var newprof = new OperatingProviderProfile
                    {
                        CompanyId = provider.CompanyId,
                        ProviderId = provider.ID,
                        ApprovalId = provider.Approval,
                        Approval = approval == null ? "" : approval.Name,
                        StatusId = provider.Status,
                        Status = status == null ? "" : status.Name,
                        PecStatusId = provider.PEC,
                        PecStatus = pec == null ? "" : pec.Name,
                        MSADocumentId = provider.MSA,
                        InsuranceStart = provider.InsuranceStart,
                        InsuranceExpire = provider.InsuranceExpire,
                        Preferred = provider.Preferred,
                        Secondary = provider.Secondary,
                        Rating = provider.Rating,
                        RigName = RigName,
                        RigId = RigId
                    };

                    result.Add(newprof);
                }

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant GetSubsriptionOperators", null);
                return null;
            }
        }
        //Phase II Changes - 05/21/2021
        public Task<List<OperatingProviderProfile>> GetSubsriptionOperatorsForAdmin(string tenantId="")
        {
            try
            {
                List<OperatingProviderProfile> result = new List<OperatingProviderProfile>();

                //Phase II Changes - 05/21/2021 - Start
                //Getting Subscibed Operators from SubscriptionOperators and MSA, Insurance from Operator Directory
                //var providers = _servicedb.SubscriptionOperators.ToList();
                var subscribedOperatorProviders = _servicedb.SubscriptionOperators.ToList();
                var OperatorDirectory = _servicedb.OperatingDirectory.ToList();


                var providers = (from sOpr in subscribedOperatorProviders
                                 join oPr in OperatorDirectory on sOpr.CompanyId equals oPr.CompanyId into operators
                                 from oPr in operators.DefaultIfEmpty()
                                 where sOpr.TenantId == tenantId
                                 select new SubscriptionOperatorModel
                                 {
                                     ID = sOpr.ID,
                                     CompanyId = sOpr.CompanyId,
                                     Approval = oPr?.Approval ?? "",
                                     Status = oPr?.Status ?? "",
                                     PEC= oPr?.PEC  ?? "",
                                     MSA= oPr?.MSA ?? "",
                                     Rating= oPr?.Rating ?? 0 ,
                                     Preferred  = false,
                                     Secondary = false,
                                     InsuranceStart = oPr?.InsuranceStart ?? null,
                                     InsuranceExpire = oPr?.InsuranceExpire ?? null,
                                     InsuranceId = oPr?.Insurance ?? "",
                                     InsuranceDocument = "",
                                     TenantId = sOpr.TenantId,
                                     MSADocumentId = sOpr?.MSA ??""
                                 }).ToList();
                //Phase II Changes - 05/21/2021  - End

                var approvals = _servicedb.OperatingDirectoryAppovals.ToList();
                var statuses = _servicedb.OperatingDirectoryStatuses.ToList();
                var pecs = _servicedb.OperatingDirectoryPECs.ToList();

                foreach (var provider in providers)
                {
                    var approval = approvals.FirstOrDefault(x => x.Id == provider.Approval);
                    var status = statuses.FirstOrDefault(x => x.Id == provider.Status);
                    var pec = pecs.FirstOrDefault(x => x.Id == provider.PEC);
                    var SubRigs = _servicedb.subscriptionOperatorRigs.Where(x => x.CompanyId == provider.CompanyId).ToList();
                    var RigList = (from rig in SubRigs
                                   join RigReg in _db.rig_register on rig.RigId equals RigReg.Rig_id
                                   where rig.CompanyId == provider.CompanyId
                                   select new
                                   {
                                       RigName = RigReg.Rig_Name
                                   }).Select(x => x.RigName).ToList();

                    string RigName = String.Join(",", RigList);

                    var newprof = new OperatingProviderProfile
                    {
                        CompanyId = provider.CompanyId,
                        ProviderId = provider.ID,
                        ApprovalId = provider.Approval,
                        Approval = approval == null ? "" : approval.Name,
                        StatusId = provider.Status,
                        Status = status == null ? "" : status.Name,
                        PecStatusId = provider.PEC,
                        PecStatus = pec == null ? "" : pec.Name,
                        MSADocumentId = provider.MSA,
                        InsuranceStart = provider.InsuranceStart,
                        InsuranceExpire = provider.InsuranceExpire,
                        Preferred = provider.Preferred,
                        Secondary = provider.Secondary,
                        Rating = provider.Rating,
                        RigName = RigName,
                        InsuranceId=provider.InsuranceId,
                        ServiceTenantId = provider.TenantId
                    };

                    result.Add(newprof);
                }

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant GetSubsriptionOperators", null);
                return null;
            }
        }

        public async Task<int> UpdateProviderDirectory(Model.ServiceCompany.Models.OperatingProviderProfile input)
        {
            try
            {
                int result = 0;

                if (string.IsNullOrEmpty(input.ProviderId))
                {
                    var added = _servicedb.OperatingDirectory.FirstOrDefault(x => x.CompanyId == input.CompanyId);
                    if (added != null)
                        return -1;
                }

                var ti = _httpcontext.GetMultiTenantContext().TenantInfo;

                if (ti != null)
                {
                    var welluser = await _userManager.GetUserAsync(_httpcontext.User);

                    //Phase II Changes - 02/26/2021 - Change Preferred Status type to Byte (1-Welcome,2-Authorized,3-Preferred)
                    //Preferred Status will not Update from the Edit
                    if (input.Preferred)
                    {
                        var preffered = _servicedb.OperatingDirectory.FirstOrDefault(x => x.Preferred);
                        if (preffered != null)
                            preffered.Preferred = false;
                    }
                    if (input.Secondary)
                    {
                        var secondary = _servicedb.OperatingDirectory.FirstOrDefault(x => x.Secondary);
                        if (secondary != null)
                            secondary.Secondary = false;
                    }

                    if (string.IsNullOrEmpty(input.ProviderId))
                    {
                        var newdir = new OperatingDirectory
                        {
                            ID = Guid.NewGuid().ToString("D"),
                            Approval = input.ApprovalId,
                            Status = input.StatusId,
                            PEC = input.PecStatusId,
                            CompanyId = input.CompanyId,
                            InsuranceStart = input.InsuranceStart,
                            InsuranceExpire = input.InsuranceExpire,
                            //Phase II Changes - 02/26/2021 - Change Preferred Status type to Byte (1-Welcome,2-Authorized,3-Preferred)
                            //Add Preferred Status as 1 at Insert
                            //Preferred = Convert.ToByte(1),
                            Preferred = input.Preferred,
                            Secondary = input.Secondary,
                            TenantId = ti.Id,
                            Rating = (int?)input.Rating == null ? 0 : input.Rating,
                            MSA = input.MSADocumentId,
                            Insurance=input.InsuranceId //Phase II Changes - 05/19/2021
                        };
                        _servicedb.OperatingDirectory.Add(newdir);

                        var oprcompanyId = newdir.CompanyId;

                        var getDetails = _db.CorporateProfile.Where(x => x.TenantId == oprcompanyId).FirstOrDefault();
                        var company = new WellAI.Advisor.Model.Tenant.Models.CrmCompanies
                        {
                            Name = getDetails.Name,
                            Address1 = getDetails.Address1,
                            Address2 = getDetails.Address2,
                            City = getDetails.City,
                            StateRegion = getDetails.State,
                            Country = getDetails.Country,
                            Phone = getDetails.Phone,
                            Website = getDetails.Website,
                            TenantId = oprcompanyId,
                        };
                        _servicedb.CrmCompanies.Add(company);

                    }
                    else
                    {
                        var provDirectory = _servicedb.OperatingDirectory.FirstOrDefault(x => x.ID == input.ProviderId);
                        if (provDirectory != null)
                        {
                            provDirectory.Approval = input.ApprovalId;
                            provDirectory.Status = input.StatusId;
                            provDirectory.PEC = input.PecStatusId;
                            provDirectory.CompanyId = input.CompanyId;
                            provDirectory.MSA = input.MSADocumentId;
                            provDirectory.InsuranceStart = input.InsuranceStart;
                            provDirectory.InsuranceExpire = input.InsuranceExpire;
                            //Phase II Changes - 02/26/2021 - Change Preferred Status type to Byte (1-Welcome,2-Authorized,3-Preferred)
                            //Prevent update Preferred at Update
                            //provDirectory.Preferred = input.Preferred;
                            provDirectory.Secondary = input.Secondary;
                            provDirectory.TenantId = ti.Id;
                            provDirectory.Rating = (int?)input.Rating == null ? 0 : input.Rating;

                            //Phase II Changes - 05/19/2021
                            provDirectory.Insurance = input.InsuranceId;
                        }

                    }
                   
                    result = await _servicedb.SaveChangesAsync();
                }

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant UpdateProviderDirectory", null);
                return 0;
            }
        }

        public async Task<int> UpdateSubsriptionOperators(OperatingProviderProfile input)
        {
            try
            {
                int result = 0;

                if (string.IsNullOrEmpty(input.ProviderId))
                {
                    var added = _servicedb.SubscriptionOperators.FirstOrDefault(x => x.CompanyId == input.CompanyId);
                    if (added != null)
                        return -1;
                }

                var ti = _httpcontext.GetMultiTenantContext().TenantInfo;

                if (ti != null)
                {
                    var welluser = await _userManager.GetUserAsync(_httpcontext.User);
                   
                    if (input.Secondary)
                    {
                        var secondary = _servicedb.SubscriptionOperators.FirstOrDefault(x => x.Secondary);
                        if (secondary != null)
                            secondary.Secondary = false;
                    }

                    if (string.IsNullOrEmpty(input.ProviderId))
                    {
                        var newdir = new SubscriptionOperator
                        {
                            ID = Guid.NewGuid().ToString("D"),
                            Approval = input.ApprovalId,
                            Status = input.StatusId,
                            PEC = input.PecStatusId,
                            CompanyId = input.CompanyId,
                            InsuranceStart = input.InsuranceStart,
                            InsuranceExpire = input.InsuranceExpire,
                            //Phase II Changes - 02/26/2021 - Change Preferred Status type to Byte (1-Welcome,2-Authorized,3-Preferred)
                            //Add Preferred as 1
                            Preferred = input.Preferred,
                            //Preferred = Convert.ToByte(1),
                            Secondary = input.Secondary,
                            TenantId = ti.Id,
                            Rating = (int?)input.Rating,
                            MSA = input.MSADocumentId
                        };
                        _servicedb.SubscriptionOperators.Add(newdir);
                    }
                    else
                    {
                        var provDirectory = _servicedb.SubscriptionOperators.FirstOrDefault(x => x.ID == input.ProviderId);
                        if (provDirectory != null)
                        {
                            provDirectory.Approval = input.ApprovalId;
                            provDirectory.Status = input.StatusId;
                            provDirectory.PEC = input.PecStatusId;
                            provDirectory.CompanyId = input.CompanyId;
                            provDirectory.MSA = input.MSADocumentId;
                            provDirectory.InsuranceStart = input.InsuranceStart;
                            provDirectory.InsuranceExpire = input.InsuranceExpire;

                            //Phase II Changes - 02/26/2021 - Change Preferred Status type to Byte (1-Welcome,2-Authorized,3-Preferred)
                            //provDirectory.Preferred = input.Preferred;
                            provDirectory.Secondary = input.Secondary;
                            provDirectory.TenantId = ti.Id;
                            provDirectory.Rating = (int?)input.Rating;
                        }
                    }

                    result = await _servicedb.SaveChangesAsync();
                }

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant UpdateSubsriptionOperators", null);
                return 0;
            }
        }

        public Task<List<OperatingProviderProfile>> GetProviderDirectoriesByTenantId(string tenantId)
        {
            try
            {
                var operatingDirectory = _servicedb.OperatingDirectory.Where(x => x.TenantId == tenantId).ToList();
                var operators = (from od in operatingDirectory
                                 join cp in _db.CorporateProfile on od.CompanyId equals cp.TenantId
                                 where od.TenantId == tenantId
                                 select new OperatingProviderProfile { Name = cp.Name, CompanyId = cp.TenantId }
                                     ).ToList();
                return Task.FromResult(operators);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant GetProviderDirectoriesByTenantId", null);
                return null;
            }
        }

        public Task<List<OperatingProviderProfile>> GetSubscribedOperatorsByTenantId(string tenantId)
        {
            try
            {
                var subsOpers = _servicedb.SubscriptionOperators.Where(x => x.TenantId == tenantId).ToList();
                var operators = (from od in subsOpers
                                 join cp in _db.CorporateProfile on od.CompanyId equals cp.TenantId
                                 where od.TenantId == tenantId
                                 select new OperatingProviderProfile { Name = cp.Name, CompanyId = cp.TenantId }
                                     ).ToList();
                return Task.FromResult(operators);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant GetSubscribedOperatorsByTenantId", null);
                return null;
            }
        }

        public async Task<WellAI.Advisor.Model.OperatingCompany.Models.CorporateProfile> GetProviderDirectoryByTenantId(string tenantId)
        {
            try
            {
                var operatorDir = _db.CorporateProfile.FirstOrDefault(x => x.TenantId == tenantId);
                return await Task.FromResult(operatorDir);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant GetProviderDirectoryByTenantId", null);
                return null;
            }
        }

        public async Task<int> DeleteProviderDirectory(string providerDirId)
        {
            try
            {
                int result = 0;

                if (_httpcontext.GetMultiTenantContext().TenantInfo != null)
                {

                    var provDir = _servicedb.OperatingDirectory.FirstOrDefault(x => x.ID == providerDirId);
                    if (provDir != null)
                    {
                        _servicedb.OperatingDirectory.Remove(provDir);
                    }

                    result = await _servicedb.SaveChangesAsync();
                }

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant DeleteProviderDirectory", null);
                return 0;
            }
        }

        /// <summary>
        /// Phase II Changes - 12/01/2021 - Save Subscribe Rigs
        /// </summary>
        /// <param name="SelectedRigs"></param>
        /// <returns></returns>
        public async Task<int> SaveSubsciberProviderRigs(List<SubscriptionOperatorRigs> SelectedRigs)
        {
            int result = 0;
            try
            {
                SubscriptionOperatorRig Rigs = new SubscriptionOperatorRig();

               

                if (SelectedRigs.Count > 0)
                {
                    for (var i = 0; i < SelectedRigs.Count; i++)
                    {
                        var rigId = SelectedRigs[i].RigId;
                        var CompanyId = SelectedRigs[i].CompanyId;
                        var GetRigs = Get_SubsciberProviderRigs(CompanyId);
                        var RigList = rigId.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (var Rig in RigList)
                        {
                            Rigs.ID = Guid.NewGuid().ToString("D");
                            Rigs.CompanyId = CompanyId;
                            Rigs.RigId = Rig;

                            _servicedb.subscriptionOperatorRigs.Add(Rigs);
                            result = await _servicedb.SaveChangesAsync();

                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant SaveSubsciberProviderRigs", null);
                return 0;
            }
        }
        /// <summary>
        /// Phase II Changes - 18/01/2021 - Get Subscribe Rigs
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public Task<List<SubscriptionOperatorRigs>> Get_SubsciberProviderRigs(string CompanyId)
        {
            try
            {
                List<SubscriptionOperatorRigs> RigList = new List<SubscriptionOperatorRigs>();

                //var RigList = (from subrig in _servicedb.subscriptionOperatorRigs
                //               where subrig.CompanyId == CompanyId || CompanyId == null
                //               select new SubscriptionOperatorRigs
                //               {
                //                   RigId = subrig.RigId,
                //                   ID = subrig.ID,
                //                   CompanyId = subrig.CompanyId
                //               }).ToList();
                if (CompanyId== "00000000-0000-0000-0000-000000000000")
                {
                    RigList = (from subrig in _servicedb.subscriptionOperatorRigs
                               select new SubscriptionOperatorRigs
                               {
                                   RigId = subrig.RigId,
                                   ID = subrig.ID,
                                   CompanyId = subrig.CompanyId
                               }).ToList();
                }
                else
                {
                    //|| CompanyId == null
                    RigList = (from subrig in _servicedb.subscriptionOperatorRigs
                               where subrig.CompanyId == CompanyId 
                               select new SubscriptionOperatorRigs
                               {
                                   RigId = subrig.RigId,
                                   ID = subrig.ID,
                                   CompanyId = subrig.CompanyId
                               }).ToList();
                }
               
                return Task.FromResult(RigList);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant Get_SubsciberProviderRigs", null);
                return null;
            }
        }

        /// <summary>
        /// Phase II Changes - 18/01/2021 - Get all Subscribed Rigs
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public Task<List<SubscriptionOperatorRigs>> Get_AllSubsciberProviderRigs()
        {
            try
            {
                List<SubscriptionOperatorRig> Rigs = new List<SubscriptionOperatorRig>();
                var RigList = (from subrig in _servicedb.subscriptionOperatorRigs
                               select new SubscriptionOperatorRigs
                               {
                                   RigId = subrig.RigId,
                                   ID = subrig.ID,
                                   CompanyId = subrig.CompanyId
                               }).ToList();
                return Task.FromResult(RigList);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant Get_AllSubsciberProviderRigs", null);
                return null;
            }
        }

        /// <summary>
        /// Phase II Chnages - 18/01/2021 - Remove Existing Subscribe Rigs
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        public async Task<int> Remove_SubsciberProviderRigs(string CompanyId)
        {
            try
            {
                List<SubscriptionOperatorRig> Rigs = new List<SubscriptionOperatorRig>();
                var result = 0;
                var Rig = (from subrig in _servicedb.subscriptionOperatorRigs
                           where subrig.CompanyId == CompanyId
                           select new SubscriptionOperatorRig
                           {
                               RigId = subrig.RigId,
                               ID = subrig.ID,
                               CompanyId = subrig.CompanyId
                           }).ToList();

                foreach (var rig in Rig)
                {

                    _servicedb.subscriptionOperatorRigs.Remove(rig);
                    await _servicedb.SaveChangesAsync();
                }
                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant Remove_SubsciberProviderRigs", null);
                return 0;
            }
        }
        
        public async Task<int> DeleteProviderDirectoryByCompanyId(string companyId)
        {
            try
            {
                int result = 0;              

                var provDir = _servicedb.OperatingDirectory.FirstOrDefault(x => x.CompanyId == companyId);
                if (provDir != null)
                {
                    _servicedb.OperatingDirectory.Remove(provDir);
                }

                result = await _servicedb.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant DeleteProviderDirectoryByCompanyId", null);
                return 0;
            }
        }

        /// <summary>
        /// Phase II Changes - Delted subscribed rigs when un subscribe operator
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public async Task<int> DeleteSubscribeRigs(string companyId)
        {
            try
            {
                int result = 0;

              
                List<SubscriptionOperatorRig> Rigs = new List<SubscriptionOperatorRig>();
                Rigs = _servicedb.subscriptionOperatorRigs.Where(x => x.CompanyId == companyId).ToList();

                for (var i = 0; i < Rigs.Count; i++)
                {
                    _servicedb.subscriptionOperatorRigs.Remove(Rigs[i]);
                    await _servicedb.SaveChangesAsync();
                }

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant DeleteSubscribeRigs", null);
                return 0;
            }
        }

        public async Task<int> DeleteSubsriptionOperators(string providerDirId)
        {
            try
            {
                int result = 0;

               
                var provDir = _servicedb.SubscriptionOperators.FirstOrDefault(x => x.CompanyId == providerDirId);
                if (provDir != null)
                {
                    _servicedb.SubscriptionOperators.Remove(provDir);
                }

                result = await _servicedb.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant DeleteSubsriptionOperators", null);
                return 0;
            }
        }

        public async Task<int> UpdateRatingProviderDirectory(string provider, int rate)
        {
            try
            {
                int result = 0;

                if (_httpcontext.GetMultiTenantContext().TenantInfo != null)
                {

                    var provDir = _servicedb.OperatingDirectory.FirstOrDefault(x => x.ID == provider);
                    if (provDir != null)
                    {
                        provDir.Rating = rate;
                    }

                    result = await _servicedb.SaveChangesAsync();
                }

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant UpdateRatingProviderDirectory", null);
                return 0;
            }
        }

        public async Task<string> CreateBillingInvoice(PaymentMethod paymentModel, SubscriptionViewModel subscriptionDetail, string subscriptionId)
        {
            try
            {
                int result = 0;
                string InvoiceId = Guid.NewGuid().ToString("D");
                if (_httpcontext.GetMultiTenantContext().TenantInfo != null)
                {
                    var tenantId = _httpcontext.GetMultiTenantContext().TenantInfo.Id;
                    BillingHistory billingDetail = new BillingHistory()
                    {
                        ID = InvoiceId,
                        Name = paymentModel.Holder,
                        Subscriptions = int.Parse(subscriptionDetail.RigCount),
                        Invoice = subscriptionId,
                        Amount = double.Parse(subscriptionDetail.TotalAmount),
                        BillDate = DateTime.Now,
                        PayMethod = paymentModel.PayType,
                        AddressId = subscriptionDetail.AddressId,
                        TenantId = tenantId,
                    };
                    _db.BillingHistoryInvoices.Add(billingDetail);
                    result = await _db.SaveChangesAsync();
                }
                return InvoiceId;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant CreateBillingInvoice", null);
                return null;
            }
        }

        public async Task<List<ClientContact>> GetClientContactsList(string userId)
        {
            try
            {
                List<ClientContact> clientContacts = null;
                if (_httpcontext.GetMultiTenantContext().TenantInfo != null)
                {
                    clientContacts = _servicedb.ClientContacts.Where(x => x.UserId.Equals(userId) && x.IsActive == true).ToList();
                }
                return await Task.FromResult(clientContacts);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant GetClientContactsList", null);
                return null;
            }
        }

        public Task<List<ActivityViewModel>> GetActivityTasks(string operId)
        {
            try
            {
                var opernocheck = operId == DLL.Constants.NoSpecificWellFilterKey;

                var tasks = _servicedb.TasksSchedule.Where(y => y.TenantId == operId && !opernocheck || opernocheck).Select(x => new ActivityViewModel     /*Where(y => y.TenantId == operId)*/
                {
                    ProjectId = x.Id,
                    ActivityIsTask = true,
                    Description = x.Description,
                    Start = x.Start.Value,
                    End = x.End.Value,
                    StartTimezone = x.StartTimezone,
                    EndTimezone = x.EndTimezone,
                    IsAllDay = x.IsAllDay == null ? false : x.IsAllDay.Value,
                    Title = x.Title,
                    RecurrenceID = x.RecurrenceID,
                    RecurrenceRule = x.RecurrenceRule,
                    RecurrenceException = x.RecurrenceException
                }).ToList();

                return Task.FromResult(tasks);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant GetActivityTasks", null);
                return null;
            }
        }

        public async Task<string> UpdateActivityTask(ActivityViewModel input)
        {
            try
            {
                string result = "";


                if (string.IsNullOrEmpty(input.ProjectId))
                {
                    result = Guid.NewGuid().ToString("D");

                    _servicedb.TasksSchedule.Add(new TaskSchedule
                    {
                        Id = result,
                        Description = input.Description,
                        Start = input.Start,
                        End = input.End,
                        StartTimezone = input.StartTimezone,
                        EndTimezone = input.EndTimezone,
                        IsAllDay = input.IsAllDay,
                        Title = input.Title,
                        RecurrenceID = input.RecurrenceID,
                        RecurrenceRule = input.RecurrenceRule,
                        RecurrenceException = input.RecurrenceException
                    });
                }
                else
                {
                    var task = _servicedb.TasksSchedule.FirstOrDefault(x => x.Id == input.ProjectId);
                    if (task != null)
                    {
                        task.Description = input.Description;
                        task.Start = input.Start;
                        task.End = input.End;
                        task.StartTimezone = input.StartTimezone;
                        task.EndTimezone = input.EndTimezone;
                        task.IsAllDay = input.IsAllDay;
                        task.Title = input.Title;
                        task.RecurrenceID = input.RecurrenceID;
                        task.RecurrenceRule = input.RecurrenceRule;
                        task.RecurrenceException = input.RecurrenceException;
                    }
                }

                await _servicedb.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant UpdateActivityTask", null);
                return null;
            }
        }

        public async Task<int> DeleteActivityTask(string taskId)
        {
            try
            {
                int result = 0;

                var task = _servicedb.TasksSchedule.FirstOrDefault(x => x.Id == taskId);
                if (task != null)
                {
                    _servicedb.TasksSchedule.Remove(task);
                }

                result = await _servicedb.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant DeleteActivityTask", null);
                return 0;
            }
        }

        public async Task<int> DeleteCRMContact(int contactID)
        {
            try
            {
                int result = 0;

                if (_httpcontext.GetMultiTenantContext().TenantInfo != null)
                {

                    var contact = _servicedb.CrmContacts.FirstOrDefault(x => x.ContactId == contactID);
                    if (contact != null)
                    {
                        _servicedb.CrmContacts.Remove(contact);
                    }

                    result = await _servicedb.SaveChangesAsync();
                }

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant DeleteCRMContact", null);
                return 0;
            }
        }
        public async Task<int> DeleteCRMCompany(int companyID)
        {
            try
            {
                int result = 0;

                if (_httpcontext.GetMultiTenantContext().TenantInfo != null)
                {

                    var company = _servicedb.CrmCompanies.FirstOrDefault(x => x.CompanyId == companyID);
                    if (company != null)
                    {
                        _servicedb.CrmCompanies.Remove(company);
                    }

                    result = await _servicedb.SaveChangesAsync();
                }

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant DeleteCRMCompany", null);
                return 0;
            }
        }
        public async Task<int> DeleteCRMNotes(int commentsID)
        {
            try
            {
                int result = 0;

                if (_httpcontext.GetMultiTenantContext().TenantInfo != null)
                {

                    var company = _servicedb.CrmComments.FirstOrDefault(x => x.CommentId == commentsID);
                    if (company != null)
                    {
                        _servicedb.CrmComments.Remove(company);
                    }

                    result = await _servicedb.SaveChangesAsync();
                }

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant DeleteCRMNotes", null);
                return 0;
            }
        }

        private async Task<bool> AddCRMContacts(List<ClientContact> contactsList)
        {
            try
            {
                foreach (var contactitem in contactsList)
                {
                    if (contactitem.TenantId != null)
                    {
                        var crmCompany = (from company in _servicedb.CrmCompanies
                                          where company.TenantId == contactitem.TenantId
                                          select new CrmCompanies
                                          {
                                              CompanyId = company.CompanyId
                                          }
                                         ).FirstOrDefault();

                        var userdetails = (from user in _db.Users
                                           where user.Id == contactitem.ContactId
                                           select new WellAI.Advisor.Model.Tenant.Models.CrmContacts
                                           {
                                               FirstName = user.FirstName,
                                               LastName = user.LastName,
                                               Address1 = user.Address,
                                               City = user.City,
                                               StateRegion = user.State,
                                               PostalCode = user.Zip,
                                               Email = user.Email,
                                               Phone = user.PhoneNumber,
                                               MobilePhone = user.Mobile,
                                               InstanceId = 0,
                                               CompanyId = crmCompany == null ? 0 : crmCompany.CompanyId,
                                               Title = user.JobTitle
                                           }
                                          ).FirstOrDefault();

                        if (userdetails.FirstName != null)
                        {
                            _servicedb.CrmContacts.Add(userdetails);
                        }
                    }
                }
                await _servicedb.SaveChangesAsync();

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant AddCRMContacts", null);
                return false;
            }
        }

        public async Task<string> UpdateCRMActivityTask(ActivityViewModel input)
        {
            try
            {
                string result = "";


                if (string.IsNullOrEmpty(input.ProjectId))
                {
                    result = Guid.NewGuid().ToString("D");

                    _servicedb.TasksSchedule.Add(new TaskSchedule
                    {
                        Id = result,
                        Description = input.Description,
                        Start = input.Start,
                        End = input.End,
                        StartTimezone = input.StartTimezone,
                        EndTimezone = input.EndTimezone,
                        IsAllDay = input.IsAllDay,
                        Title = input.Title,
                        RecurrenceID = input.RecurrenceID,
                        RecurrenceRule = input.RecurrenceRule,
                        RecurrenceException = input.RecurrenceException,
                        TenantId = input.TenantId
                    });
                }
                else
                {
                    var task = _servicedb.TasksSchedule.FirstOrDefault(x => x.Id == input.ProjectId);
                    if (task != null)
                    {
                        task.Description = input.Description;
                        task.Start = input.Start;
                        task.End = input.End;
                        task.StartTimezone = input.StartTimezone;
                        task.EndTimezone = input.EndTimezone;
                        task.IsAllDay = input.IsAllDay;
                        task.Title = input.Title;
                        task.RecurrenceID = input.RecurrenceID;
                        task.RecurrenceRule = input.RecurrenceRule;
                        task.RecurrenceException = input.RecurrenceException;
                        task.TenantId = input.TenantId;
                    }
                }

                await _servicedb.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant UpdateCRMActivityTask", null);
                return null;
            }
        }
        public async Task<List<InDepthRigData>> GetWellDepthData(string connectionstring, string wellId)
        {
            List<ErdosGeneralTimeBased> depthChartDataMain = new List<ErdosGeneralTimeBased>();
            List<InDepthRigData> depthChartGroupData = new List<InDepthRigData>();
            try
            {
                using (var sc = GetSqlConnection(connectionstring))
                    depthChartDataMain = (List<ErdosGeneralTimeBased>)sc.Query<ErdosGeneralTimeBased>("" +
                                    "SELECT " +
                                    " DENSE_RANK() OVER (ORDER BY X.[DATE] ASC) AS [DATE],X.GeneralTimeBasedId,X.WID,X.[TIME],X.DBTM,X.DMEA " +
                                    " FROM "+
                                    " ( "+
                                    " SELECT "+
                                    "erdos_GeneralTimeBased.GeneralTimeBasedId,"+
                                    "erdos_GeneralTimeBased.WID," +
                                    "erdos_GeneralTimeBased.[DATE],"+
                                    "erdos_GeneralTimeBased.[TIME],"+
                                    "erdos_GeneralTimeBased.DBTM,"+
                                    "FLOOR(erdos_GeneralTimeBased.DMEA) AS DMEA"+
                                    " FROM [dbo].[erdos_GeneralTimeBased] "+
                                    " WHERE WID='" + wellId+ "' AND erdos_GeneralTimeBased.GeneralTimeBasedId%1000=0)X", commandType: CommandType.Text);

                if (depthChartDataMain.Count == 0)
                {
                    using (var con1 = GetSqlConnection(connectionstring))
                        depthChartDataMain = (List<ErdosGeneralTimeBased>)con1.Query<ErdosGeneralTimeBased>("" +
                                        "SELECT " +
                                        " DENSE_RANK() OVER (ORDER BY X.[DATE] ASC) AS [DATE],X.GeneralTimeBasedId,X.WID,X.[TIME],X.DBTM,X.DMEA " +
                                        " FROM " +
                                        " ( " +
                                        " SELECT " +
                                        "erdos_GeneralTimeBased.GeneralTimeBasedId," +
                                        "erdos_GeneralTimeBased.WID," +
                                        "erdos_GeneralTimeBased.[DATE]," +
                                        "erdos_GeneralTimeBased.[TIME]," +
                                        "erdos_GeneralTimeBased.DBTM," +
                                        "FLOOR(erdos_GeneralTimeBased.DMEA) AS DMEA" +
                                        " FROM [dbo].[erdos_GeneralTimeBased] " +
                                        " WHERE WID='" + wellId + "')X", commandType: CommandType.Text);
                }

               depthChartGroupData = depthChartDataMain
               .OrderBy(o => o.Date)
               .GroupBy(g => g.Date)
               .Select(s => new { s, Count = s.Count() })
               .SelectMany(sm => sm.s.Select(s => s)
                  .Zip(Enumerable.Range(1, sm.Count), (row, index) =>
                  new InDepthRigData
                  {
                      Day = Convert.ToInt32(row.Date),
                      WellId = row.Wid,
                      Value = Convert.ToDouble(row.Dmea)
                  }))
               .ToList();

                
               
                if (depthChartGroupData != null || depthChartGroupData.Count == 0)
                {
                    return await Task.FromResult(depthChartGroupData);
                }
                else
                {
                    return await Task.FromResult(depthChartGroupData); ;
                }

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant GetWellDepthData", null);
                return depthChartGroupData;
            }
        }

        /// <summary>
        /// Phase II Changes - 02/08/2021 - Getting OperatingDirector Id
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public Task<OperatingProviderProfile> GetOperatingDirectoryID(string tenantId, string OprTenantId)
        {
            try
            {
                var operatingDirectory = _servicedb.OperatingDirectory.Where(x => x.TenantId == tenantId && x.CompanyId == OprTenantId).ToList();
                var operatingDirId = (from od in operatingDirectory
                                      join cp in _db.CorporateProfile on od.CompanyId equals cp.TenantId
                                      where od.TenantId == tenantId
                                      select new OperatingProviderProfile { ProviderId = od.ID }
                                     ).FirstOrDefault();
                return Task.FromResult(operatingDirId);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant GetOperatingDirectoryID", null);
                return null;
            }
        }

        public async Task<PaymentMethod> EncryptData(PaymentMethod method)
        {
            try
            {
                var PaymentDetails = new PaymentMethod();

                if (method != null)
                {
                    var CardNumBytes = Encoding.UTF32.GetBytes(Convert.ToString(method.Number));
                    var CardHashCode = Convert.ToBase64String(CardNumBytes);

                    var MothBytes = Encoding.UTF32.GetBytes(Convert.ToString(method.ExpireMonth));
                    var ExpireMothHashCode = Convert.ToBase64String(MothBytes);

                    var ExpireYearBytes = Encoding.UTF32.GetBytes(Convert.ToString(method.ExpireYear));
                    var ExpireYearHashCode = Convert.ToBase64String(ExpireYearBytes);

                    method.Number = CardHashCode;
                    method.ExpireMonth = ExpireMothHashCode;
                    method.ExpireYear = ExpireYearHashCode;
                }

                return await Task.FromResult(method);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant EncryptData", null);
                return method;
            }
        }

        public async Task<PaymentMethod> DecryptDataall(PaymentMethod method)
        {
            try
            {
                var PaymentDetails = new PaymentMethod();

                if (method != null)
                {
                    var CardNumBytes = Convert.FromBase64String(Convert.ToString(method.Number));
                    var CardHashCode = Encoding.UTF32.GetString(CardNumBytes);

                    var MothBytes = Convert.FromBase64String(Convert.ToString(method.ExpireMonth));
                    var ExpireMothHashCode = Encoding.UTF32.GetString(MothBytes);

                    var ExpireYearBytes = Convert.FromBase64String(Convert.ToString(method.ExpireYear));
                    var ExpireYearHashCode = Encoding.UTF32.GetString(ExpireYearBytes);

                 

                    method.Number = CardHashCode;
                    method.ExpireMonth = ExpireMothHashCode;
                    method.ExpireYear = ExpireYearHashCode;
                }

                return await Task.FromResult(method);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant EncryptData", null);
                return method;
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

                    return await Task.FromResult(CardDetail);
                }

                return EncodeData;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant DecryptData", null);
                return EncodeData;
            }
        }

        public Task<OperatingProviderProfile> GetOperatingDirectorByProfileId(string profileId)
        {
            try
            {
                List<OperatingProviderProfile> result = new List<OperatingProviderProfile>();
                //var providers = _servicedb.OperatingDirectory.ToList();
                var approvals = _servicedb.OperatingDirectoryAppovals.ToList();
                var statuses = _servicedb.OperatingDirectoryStatuses.ToList();
                var pecs = _servicedb.OperatingDirectoryPECs.ToList();


                var profile = (from provider in _servicedb.OperatingDirectory
                               where provider.ID == profileId
                               select new OperatingProviderProfile
                               {
                                   CompanyId = provider.CompanyId,
                                   ProviderId = provider.ID,
                                   ApprovalId = provider.Approval,
                                   Approval = provider.Approval,
                                   StatusId = provider.Status,
                                   Status = provider.Status,
                                   PecStatusId = provider.PEC,
                                   PecStatus = provider.PEC,
                                   MSADocumentId = provider.MSA,
                                   InsuranceStart = provider.InsuranceStart,
                                   InsuranceExpire = provider.InsuranceExpire,

                                   //Phase II Changes - 02/26/2021 - Change Preferred Status type to Byte (1-Welcome,2-Authorized,3-Preferred)
                                   //Prevent Preferred status at update
                                   //Preferred = provider.Preferred,
                                   Secondary = provider.Secondary,
                                   Rating = provider.Rating == 0 ? null : provider.Rating,
                               }
                              ).FirstOrDefault();

                var approval = approvals.FirstOrDefault(x => x.Id == profile.Approval);
                var status = statuses.FirstOrDefault(x => x.Id == profile.Status);
                var pec = pecs.FirstOrDefault(x => x.Id == profile.PecStatus);

                profile.Approval = approval == null ? "" : approval.Name;
                profile.Status = status == null ? "" : status.Name;
                profile.PecStatus = pec == null ? "" : pec.Name;


                return Task.FromResult(profile);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant GetOperatingDirectorByProfileId", null);
                return null;
            }
        }


        public async Task<int> GetProductSubscribedRigs(string RigId,string TenantId)
        {
            try
            {
            int SubscribedRigs = 0;
            if (RigId != null && TenantId != null)
            {
                SubscribedRigs = _servicedb.subscriptionOperatorRigs.Where(x => x.RigId == RigId && x.CompanyId == TenantId).Count();
            }

            return await Task.FromResult(SubscribedRigs);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "ServiceTenant GetProductSubscribedRigs", null);
                return 0;
            }
        }
        public Task<List<BillingHistory>> GetBillingHistoryInvoices(string tenantId)
        {
            try
            {
                //(int?)input.Rating == null ? 0 : input.Rating;
                List<BillingHistory> result = null;

                result = (from his in _db.BillingHistoryInvoices
                        //  join ptype in _db.PaymentType on his.PayMethod equals ptype.ID
                          where his.TenantId == tenantId && his.TransactionID != null
                          select new BillingHistory { Invoice = his.Invoice, BillDate = his.BillDate, Name = his.Name, Amount =his.Amount,
                              TransactionID=his.TransactionID, PayMethod= his.PayMethod=="1" ? "Credit Card"  : his.PayMethod == "2" ? "Debit Card" : "ACH" }
                             ).ToList();
                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "OperatingTenant GetBillingHistoryInvoices", null);
                return null;
            }
        }

    

        public Task<List<BillingHistory>> GetBillingHistoryInvoices_old(string tenantId)
        {
            try
            {
                List<BillingHistory> result = null;

                result = (from his in _db.BillingHistoryInvoices
                          join ptype in _db.PaymentType on his.PayMethod equals ptype.ID
                          where his.TenantId == tenantId
                          select new BillingHistory { Invoice = his.Invoice, BillDate = his.BillDate, Name = his.Name, Amount = his.Amount, PayMethod = ptype.Name }
                             ).ToList();
                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "OperatingTenant GetBillingHistoryInvoices", null);
                return null;
            }
        }
    }
}
