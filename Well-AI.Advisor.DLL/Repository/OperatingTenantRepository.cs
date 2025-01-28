using Microsoft.AspNetCore.Http;
using System;
using Finbuckle.MultiTenant;
using System.Threading.Tasks;
using WellAI.Advisor.Model.OperatingCompany.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using WellAI.Advisor.Model.Identity;
using System.Collections.Generic;
using WellAI.Advisor.Model.Common;
using WellAI.Advisor.DLL.Data;
using Newtonsoft.Json;
using WellAI.Advisor.Model.Tenant.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;
using Microsoft.AspNetCore.DataProtection;
using System.Text;
using WellAI.Advisor.DLL.Entity;

namespace WellAI.Advisor.DLL.Repository
{
    public class OperatingTenantRepository : IOperatingTenantRepository
    {
        private readonly WebAIAdvisorContext _db;
        private readonly TenantOperatingDbContext _operdb;
        private readonly HttpContext _httpcontext;
        UserManager<WellIdentityUser> _userManager;
      
        public OperatingTenantRepository(WebAIAdvisorContext db)
        {
            _db = db;
        }

        public OperatingTenantRepository(TenantOperatingDbContext operdb, HttpContext httpcontext, UserManager<WellIdentityUser> userManager)
        {
            _operdb = operdb;
            _httpcontext = httpcontext;
            _userManager = userManager;

        }
        public OperatingTenantRepository(TenantOperatingDbContext operdb, HttpContext httpcontext, UserManager<WellIdentityUser> userManager, WebAIAdvisorContext db)
        {
            _operdb = operdb;
            _httpcontext = httpcontext;
            _userManager = userManager;
            _db = db;
        }

        public OperatingTenantRepository(TenantOperatingDbContext operdb)
        {
            _operdb = operdb;
        }

        public Task<List<PaymentMethod>> GetPaymentMethods(string tenantId)
        {
            try
            {
                List<PaymentMethod> result = null;

                if (_httpcontext.GetMultiTenantContext().TenantInfo != null)
                {

                    result = _db.PaymentMethods.Where(x => x.TenantId == tenantId).ToList();
                    //  result = _db.PaymentMethods.ToList();
                }

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "OperatingTenant GetPaymentMethods", null);
                return null;
            }
        }

        public Task<List<CorporateProfile>> GetBillingMethods(string tenantId)
        {
            try
            {
                List<CorporateProfile> result = null;

                if (_httpcontext.GetMultiTenantContext().TenantInfo != null)
                {

                    result = _db.CorporateProfile.Where(x => x.TenantId == tenantId).ToList();
                    //result = _db.PaymentMethods.ToList();
                }

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "OperatingTenant GetPaymentMethods", null);
                return null;
            }
        }

        public async Task<int> UpdatePaymentMethod(PaymentMethod input)
        {
            try
            {
                int result = 0;

                if (_httpcontext.GetMultiTenantContext().TenantInfo != null)
                {
                    var welluser = await _userManager.GetUserAsync(_httpcontext.User);

                    if (string.IsNullOrEmpty(input.ID))
                    {
                        input.ID = Guid.NewGuid().ToString("D");

                        //_operdb.PaymentMethods.Add(input);
                        _db.PaymentMethods.Add(input);
                    }
                    else
                    {
                        var paymenthod = _db.PaymentMethods.FirstOrDefault(x => x.ID == input.ID);
                        
                            paymenthod.Default = input.Default;
                            paymenthod.ExpireMonth = input.ExpireMonth;
                            paymenthod.ExpireYear = input.ExpireYear;
                            paymenthod.Holder = input.Holder;
                            paymenthod.Number = (input.Number);
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
                        
                    }

                    result = await _db.SaveChangesAsync();
                }
                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "OperatingTenant UpdatePaymentMethod", null);
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
                customErrorHandler.WriteError(ex, "OperatingTenant DeletePaymentMethod", null);
                return 0;
            }
        }

        public Task<List<BillingHistory>> GetBillingHistoryInvoices(string tenantId)
        {
            //try
            //{
            //    List<BillingHistory> result = null;

            //    result = (from his in _db.BillingHistoryInvoices
            //              join ptype in _db.PaymentType on his.PayMethod equals ptype.ID
            //              where his.TenantId == tenantId
            //              select new BillingHistory { Invoice = his.Invoice, BillDate = his.BillDate, Name = his.Name, Amount = his.Amount, PayMethod = ptype.Name }
            //                 ).ToList();
            //    return Task.FromResult(result);
            //}
            //catch (Exception ex)
            //{
            //    CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
            //    customErrorHandler.WriteError(ex, "OperatingTenant GetBillingHistoryInvoices", null);
            //    return null;
            //}
            try
            {
                //(int?)input.Rating == null ? 0 : input.Rating;
                List<BillingHistory> result = null;

                result = (from his in _db.BillingHistoryInvoices
                              //  join ptype in _db.PaymentType on his.PayMethod equals ptype.ID
                          where his.TenantId == tenantId && his.TransactionID != null
                          select new BillingHistory
                          {
                              Invoice = his.Invoice,
                              BillDate = his.BillDate,
                              Name = his.Name,
                              Amount = his.Amount,
                              TransactionID = his.TransactionID,
                              PayMethod = his.PayMethod == "1" ? "Credit Card" : his.PayMethod == "2" ? "Debit Card" : "ACH"
                          }
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
        /// <summary>
        /// Phase II Changes added isFromAdmin to handle MultiTenant - 01/21/2021
        /// </summary>
        /// <param name="isFromAdmin"></param>
        /// <returns></returns>
        public Task<List<ProviderProfile>> GetProviderDirectories(bool isFromAdmin=false)
        {
            try
            {
                List<ProviderProfile> result = new List<ProviderProfile>();

                //Phase II Changes added isFromAdmin to handle MultiTenant - 01/21/2021
                TenantInfo tenantObj = new TenantInfo();
                if (isFromAdmin == false)
                {
                    tenantObj = _httpcontext.GetMultiTenantContext().TenantInfo;
                }

                //Phase II Changes added isFromAdmin to handle MultiTenant - 01/21/2021
                if ((isFromAdmin == false && tenantObj != null) || isFromAdmin == true)
                {
                    
                 
                     var  providers = _operdb.ProvidersDirectory.ToList();
                                      
                    var approvals = _operdb.ProviderDirectoryAppovals.ToList();
                    var statuses = _operdb.ProviderDirectoryStatuses.ToList();
                    var pecs = _operdb.ProviderDirectoryPECs.ToList();

                    foreach (var provider in providers)
                    {
                        var approval = approvals.FirstOrDefault(x => x.Id == provider.Approval);
                        var status = statuses.FirstOrDefault(x => x.Id == provider.Status);
                        var pec = pecs.FirstOrDefault(x => x.Id == provider.PEC);

                        var newprof = new ProviderProfile
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
                customErrorHandler.WriteError(ex, "OperatingTenant GetProviderDirectories", null);
                List<ProviderProfile> pd = new List<ProviderProfile>();
                return Task.FromResult(pd); 
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
                customErrorHandler.WriteError(ex, "OperatingTenant GetSelectedPaymentDetail", null);
                return null;
            }
        }

        public async Task<List<ErdosDrillingDepthBased>> GetDepthChartData(string id)
        {
            try
            {
                List<ErdosDrillingDepthBased> DrillingDepth = null;
                DrillingDepth = _operdb.ErdosDrillingDepthBased.ToList();
                return await Task.FromResult(DrillingDepth);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "OperatingTenant GetDepthChartData", null);
                return null;
            }
        }
        public async Task<List<ErdosGeneralTimeBased>> GetTimeChartData(string id)
        {
            try
            {
                List<ErdosGeneralTimeBased> DrillingDepth = null;
                DrillingDepth = _operdb.ErdosGeneralTimeBased.ToList();
                return await Task.FromResult( DrillingDepth);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "OperatingTenant GetTimeChartData", null);
                return null;
            }
        }
        public async Task<int> UpdateProviderDirectory(Model.OperatingCompany.Models.ProviderProfile input, string pecstatus,WebAIAdvisorContext db)
        {
            try
            {
                int result = 0;
                var tenantId = WellAIAppContext.Current.Session.GetString("TenantId");
                var UserDetails = db.CorporateProfile.Where(x => x.TenantId == tenantId).FirstOrDefault();
                var VendorUserDetails = db.CorporateProfile.Where(x => x.TenantId == input.CompanyId).FirstOrDefault();

                if (string.IsNullOrEmpty(input.ProviderId))
                {
                    var added = _operdb.ProvidersDirectory.FirstOrDefault(x => x.CompanyId == input.CompanyId);
                    if (added != null)
                        return -1;
                }

                var ti = _httpcontext.GetMultiTenantContext().TenantInfo;

                if (ti != null)
                {
                    var welluser = await _userManager.GetUserAsync(_httpcontext.User);


                    //Phase II Changes - 02/26/2021 - Change Preferred Status type to Byte (1-Welcome,2-Authorized,3-Preferred)
                    //Preferred Status will not Update from the Edit
                    if (input.Preferred == Convert.ToByte(3))
                    {
                        var preffered = _operdb.ProvidersDirectory.FirstOrDefault(x => x.Preferred == Convert.ToByte(3));
                    }

                    if (input.Secondary)
                    {
                        var PrefferedStatus = _operdb.ProvidersDirectory.Where(x => x.Secondary == true).ToList();

                        var secondary = _operdb.ProvidersDirectory.FirstOrDefault(x => x.Secondary);                       
                    }

                    var pecstatusid = (from sta in _operdb.ProviderDirectoryPECs where sta.Name == pecstatus select sta).FirstOrDefault();

                    if (string.IsNullOrEmpty(input.ProviderId))
                    {
                        var newdir = new ProviderDirectory
                        {
                            ID = Guid.NewGuid().ToString("D"),
                            Approval = input.ApprovalId,
                            Status = input.StatusId,
                            PEC = pecstatusid.Id,
                            CompanyId = input.CompanyId,
                            InsuranceStart = input.InsuranceStart,
                            InsuranceExpire = input.InsuranceExpire,
                            //Phase II Changes - 02/26/2021 - Change Preferred Status type to Byte (1-Welcome,2-Authorized,3-Preferred)
                            Preferred = Convert.ToByte(1),
                            Secondary = input.Secondary,
                            TenantId = ti.Id,
                            Rating = (int?)input.Rating,
                            MSA = input.MSADocumentId,
                            Insurance=input.InsuranceId
                        };

                        _operdb.ProvidersDirectory.Add(newdir);

                        var InsuranceDocument = db.WellFiles.Where(x => x.VendorId == tenantId && x.TenantId == input.CompanyId).FirstOrDefault();
                        if(InsuranceDocument == null)
                        {
                            MessageQueue messageQueue = new MessageQueue { From_id = UserDetails.UserId, To_id = VendorUserDetails.UserId, EntityId = Convert.ToString(newdir.ID), Type = Convert.ToInt32(8), IsActive = 1, TaskName = "Please Upload MSA document for" + "" + UserDetails.Name , JobName = "Request MSA Document", CreatedDate = DateTime.Now };
                            db.MessageQueues.Add(messageQueue);
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        var provDirectory = _operdb.ProvidersDirectory.FirstOrDefault(x => x.ID == input.ProviderId);
                        if (provDirectory != null)
                        {
                            provDirectory.Approval = input.ApprovalId;
                            provDirectory.Status = input.StatusId;
                            provDirectory.PEC = pecstatusid.Id;
                            provDirectory.CompanyId = input.CompanyId;
                            provDirectory.MSA = input.MSADocumentId;
                            provDirectory.InsuranceStart = input.InsuranceStart;
                            provDirectory.InsuranceExpire = input.InsuranceExpire;
                            //Phase II Changes - 02/26/2021 - Change Preferred Status type to Byte (1-Welcome,2-Authorized,3-Preferred)
                            //Prevent update Preferred
                            //provDirectory.Preferred = input.Preferred;
                            provDirectory.Secondary = input.Secondary;
                            provDirectory.TenantId = ti.Id;
                            provDirectory.Rating = (int?)input.Rating;
                            provDirectory.Insurance = input.InsuranceId;
                        }
              
                    }

                    result = await _operdb.SaveChangesAsync();
                }

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "OperatingTenant UpdateProviderDirectory", null);
                return 0;
            }
        }

        public async Task<string> CreateBillingInvoice(PaymentMethod paymentModel, SubscriptionViewModel subscriptionDetail, string subscriptionId,string tenantId)
        {
            try
            {
                int result = 0;
                string InvoiceId = Guid.NewGuid().ToString("D");
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
                return InvoiceId;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "OperatingTenant CreateBillingInvoice", null);
                return null;
            }
        }

        public async Task<int> DeleteProviderDirectory(string providerDirId)
        {
            try
            {
                int result = 0;

                if (providerDirId != null)
                {

                    var provDir = _operdb.ProvidersDirectory.FirstOrDefault(x => x.ID == providerDirId);
                    if (provDir != null)
                    {
                        _operdb.ProvidersDirectory.Remove(provDir);
                    }

                    result = await _operdb.SaveChangesAsync();
                }

                return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "OperatingTenant DeleteProviderDirectory", null);
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

                    var provDir = _operdb.ProvidersDirectory.FirstOrDefault(x => x.ID == provider);
                    if (provDir != null)
                    {
                        provDir.Rating = rate;
                    }

                    result = await _operdb.SaveChangesAsync();
                }

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "OperatingTenant UpdateRatingProviderDirectory", null);
                return 0;
            }
        }

        public async Task<List<ClientContact>> GetClientContactsList(string userId)
        {
            try
            {
                List<ClientContact> clientContacts = null;
                if (_httpcontext.GetMultiTenantContext().TenantInfo != null)
                {
                    clientContacts = _operdb.ClientContacts.Where(x => x.UserId.Equals(userId) && x.IsActive == true).ToList();
                }
                return await Task.FromResult(clientContacts);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "OperatingTenant GetClientContactsList", null);
                return null;
            }
        }
        public async Task<bool> CreateClientContacts(List<ClientContact> clients)
        {
            try
            {
                if (_httpcontext.GetMultiTenantContext().TenantInfo != null)
                {
                    clients.ForEach(x => x.IsActive = true);
                    _operdb.ClientContacts.UpdateRange(clients);
                    await _operdb.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "OperatingTenant CreateClientContacts", null);
                return false;
            }
        }
        public async Task<bool> UpdateClientContacts(int clientContactId)
        {
            try
            {
                if (_httpcontext.GetMultiTenantContext().TenantInfo != null)
                {
                    var client = _operdb.ClientContacts.Where(x => x.ClientContactId == clientContactId).FirstOrDefault();
                    if (client != null)
                    {
                        client.IsActive = false;
                        _operdb.ClientContacts.Update(client);
                        await _operdb.SaveChangesAsync();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "OperatingTenant UpdateClientContacts", null);
                return false;
            }
        }

        public Task<List<ActivityViewModel>> GetActivityTasks(string wellId)
        {
            try
            {
                var checkwellFilter = wellId == DLL.Constants.NoSpecificWellFilterKey;

                var tasks = _operdb.TasksSchedule.Where(y => y.tenantId == wellId && !checkwellFilter || checkwellFilter).Select(x => new ActivityViewModel
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
                customErrorHandler.WriteError(ex, "OperatingTenant GetActivityTasks", null);
                return null;
            }
        }

        public async Task<string> UpdateActivityTask(ActivityViewModel input,string tenantId)
        {
            try
            {
                string result = "";


                if (string.IsNullOrEmpty(input.ProjectId))
                {
                    result = Guid.NewGuid().ToString("D");
                    _operdb.TasksSchedule.Add(new TaskSchedule
                    {
                        Id = result,
                        tenantId = tenantId,
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
                    var task = _operdb.TasksSchedule.FirstOrDefault(x => x.Id == input.ProjectId);
                    if (task != null)
                    {
                        task.tenantId = tenantId;
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

                await _operdb.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "OperatingTenant UpdateActivityTask", null);
                return null;
            }
        }

        public async Task<int> DeleteActivityTask(string taskId)
        {
            try
            {
                int result = 0;

                var task = _operdb.TasksSchedule.FirstOrDefault(x => x.Id == taskId);
                if (task != null)
                {
                    _operdb.TasksSchedule.Remove(task);
                }

                result = await _operdb.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "OperatingTenant DeleteActivityTask", null);
                return 0;
            }
        }

        public async Task<List<string>> GetProviderDirectoryId(string tenantId)
        {
            try
            {
                return await _operdb.ProvidersDirectory.Where(x => x.TenantId == tenantId).Select(x => x.CompanyId).ToListAsync();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "OperatingTenant DeleteActivityTask", null);
                List<string> s = new List<string>();
                return s;
            }
        }

        public async Task<List<ProviderDirectory>> GetProviderDirectory(string tenantId)
        {
            return await _operdb.ProvidersDirectory.Where(x => x.TenantId == tenantId).ToListAsync();
        }

        /// <summary>
        /// Phase II Changes - 02/25/2021-Get Provider Profile by Profile Id
        /// </summary>
        /// <param name="profileId"></param>
        /// <returns></returns>
        public Task<ProviderProfile> GetProviderDirectoryByProfileId(string profileId)
        {
            ProviderProfile objProfile = new ProviderProfile();
            try
            {
                var approvals = _operdb.ProviderDirectoryAppovals.ToList();
                var statuses = _operdb.ProviderDirectoryStatuses.ToList();
                var pecs = _operdb.ProviderDirectoryPECs.ToList();


                var profile = (from provider in _operdb.ProvidersDirectory
                               where provider.ID == profileId
                               select new ProviderProfile
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
                                   Preferred = provider.Preferred,
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
            catch(Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "OperatingTenant GetProviderDirectoryByProfileId", null);
                return Task.FromResult(objProfile);
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
                                    " WHERE WID='" + wellId + "' AND erdos_GeneralTimeBased.GeneralTimeBasedId%1000=0)X", commandType: CommandType.Text);

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
                if (depthChartDataMain.Count == 0)
                {
                    using (var con2 = GetSqlConnection(connectionstring))
                        depthChartDataMain = (List<ErdosGeneralTimeBased>)con2.Query<ErdosGeneralTimeBased>("" +
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
                                    " WHERE WID='" + wellId + "' AND erdos_GeneralTimeBased.GeneralTimeBasedId%10=0)X", commandType: CommandType.Text);
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
                    return await Task.FromResult(depthChartGroupData);
                }

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "OperatingTenant GetWellDepthData", null);
                return depthChartGroupData;
            }
        }

        protected SqlConnection GetSqlConnection(string connectionstring)
        {
            return new SqlConnection(connectionstring);
        }

        /// <summary>
        /// Phase II - 01/13/2021 Get ServiceProviderDirectories for Admin
        /// </summary>
        /// <returns></returns>
        public Task<List<ProviderProfile>> GetServiceProviderDirectoriesForAdmin()
        {
            try
            {
                List<ProviderProfile> result = new List<ProviderProfile>();

                //if (_httpcontext.GetMultiTenantContext().TenantInfo != null)
                //{
                var providers = _operdb.ProvidersDirectory.ToList();
                var approvals = _operdb.ProviderDirectoryAppovals.ToList();
                var statuses = _operdb.ProviderDirectoryStatuses.ToList();
                var pecs = _operdb.ProviderDirectoryPECs.ToList();

                foreach (var provider in providers)
                {
                    var approval = approvals.FirstOrDefault(x => x.Id == provider.Approval);
                    var status = statuses.FirstOrDefault(x => x.Id == provider.Status);
                    var pec = pecs.FirstOrDefault(x => x.Id == provider.PEC);

                    var newprof = new ProviderProfile
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
                //}

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "OperatingTenant GetServiceProviderDirectoriesForAdmin", null);
                return null;
            }
            
        }

        /// <summary>
        /// UpdateProviderDirectory
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pecstatus"></param>
        /// <returns></returns>
        public async Task<int> UpdateProviderDirectoryFromAdmin(Model.OperatingCompany.Models.ProviderProfile input, string pecstatus,string tenantId)
        {
            try
            {
                int result = 0;

                if (string.IsNullOrEmpty(input.ProviderId))
                {
                    var added = _operdb.ProvidersDirectory.FirstOrDefault(x => x.CompanyId == input.CompanyId);
                    if (added != null)
                        return -1;
                }

                var welluser = await _userManager.GetUserAsync(_httpcontext.User);


                //Phase II Changes - 02/26/2021 - Change Preferred Status type to Byte (1-Welcome,2-Authorized,3-Preferred)
                //Prevent update Prevent on Update
                if (input.Preferred == Convert.ToByte(3))
                {
                    var PrefferedStatus = _operdb.ProvidersDirectory.Where(x => x.Preferred == Convert.ToByte(3)).ToList();
                    var preffered = _operdb.ProvidersDirectory.FirstOrDefault(x => x.Preferred == Convert.ToByte(3));
                }
                if (input.Secondary)
                {
                    var PrefferedStatus = _operdb.ProvidersDirectory.Where(x => x.Secondary == true).ToList();

                   

                    var secondary = _operdb.ProvidersDirectory.FirstOrDefault(x => x.Secondary);
                   
                }

                var pecstatusid = (from sta in _operdb.ProviderDirectoryPECs where sta.Name == pecstatus select sta).FirstOrDefault();

                if (string.IsNullOrEmpty(input.ProviderId))
                {
                    var newdir = new ProviderDirectory
                    {
                        ID = Guid.NewGuid().ToString("D"),
                        Approval = input.ApprovalId,
                        Status = input.StatusId,
                        PEC = pecstatusid.Id,
                        CompanyId = input.CompanyId,
                        InsuranceStart = input.InsuranceStart,
                        InsuranceExpire = input.InsuranceExpire,
                        //Phase II Changes - 02/26/2021 - Change Preferred Status type to Byte (1-Welcome,2-Authorized,3-Preferred)
                        Preferred = Convert.ToByte(1),
                        Secondary = input.Secondary,
                        TenantId = tenantId,
                        Rating = (int?)input.Rating,
                        MSA = input.MSADocumentId
                    };

                    _operdb.ProvidersDirectory.Add(newdir);
                }
                else
                {
                    var provDirectory = _operdb.ProvidersDirectory.FirstOrDefault(x => x.ID == input.ProviderId);
                    if (provDirectory != null)
                    {
                        provDirectory.Approval = input.ApprovalId;
                        provDirectory.Status = input.StatusId;
                        provDirectory.PEC = pecstatusid.Id;
                        provDirectory.CompanyId = input.CompanyId;
                        provDirectory.MSA = input.MSADocumentId;
                        provDirectory.InsuranceStart = input.InsuranceStart;
                        provDirectory.InsuranceExpire = input.InsuranceExpire;
                        //Phase II Changes - 02/26/2021 - Change Preferred Status type to Byte (1-Welcome,2-Authorized,3-Preferred)    
                        //Prevent update Preferred at update
                        //provDirectory.Preferred = input.Preferred;
                        provDirectory.Secondary = input.Secondary;
                        provDirectory.TenantId = tenantId;
                        provDirectory.Rating = (int?)input.Rating;
                    }
                }

                result = await _operdb.SaveChangesAsync();
                //}

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "OperatingTenant UpdateProviderDirectoryFromAdmin", null);
                return 0;
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
                customErrorHandler.WriteError(ex, "OperatingTenant EncryptData", null);
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

                    return CardDetail;
                }

                return await Task.FromResult(EncodeData);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "OperatingTenant DecryptData", null);
                return EncodeData;
            }
        }

        public Task<List<MessageQueue>> GetNotifications(string tenantId)
        {
            try
            {
                List<MessageQueue> result = null;

                if (_httpcontext.GetMultiTenantContext().TenantInfo != null)
                {

                    result = _db.MessageQueues.Where(x => x.To_id == tenantId).ToList();

                }

                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "OperatingTenant GetPaymentMethods", null);
                return null;
            }
        }
        public async Task<int> DeleteNotification(int msgId)
        {
            try
            {
                int result = 0;

                if (_httpcontext.GetMultiTenantContext().TenantInfo != null)
                {

                    var message = _db.MessageQueues.Where(x => x.Messagequeue_id == msgId).FirstOrDefault();
                    if (message != null)
                    {
                        _db.MessageQueues.Remove(message);
                    }

                    result = await _db.SaveChangesAsync();
                }

                return result;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "OperatingTenant DeleteNotification", null);
                return 0;
            }
        }
        public async Task<bool> DeleteSelectedNotifications(List<MessageQueue> notification)
        {
            try
            {
                if (_httpcontext.GetMultiTenantContext().TenantInfo != null)
                {
                    //notification.ForEach(x => x.IsActive = true);
                    _db.MessageQueues.RemoveRange(notification);
                    await _operdb.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForRepository customErrorHandler = new CustomErrorHandlerForRepository(null, _userManager, _db, null);
                customErrorHandler.WriteError(ex, "OperatingTenant DeleteSelectedNotifications", null);
                return false;
            }
        }
    }
}
