using Finbuckle.MultiTenant;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using WellAI.Advisor.DLL.Entity;

namespace WellAI.Advisor.Function.Notification
{
    public static class AddNotificationForInsuranceExpiration
    {
        private static TenantOperatingDbContext _tdbContext;
        private static readonly IConfiguration _configuration;

        [FunctionName("AddNotificationForInsuranceExpiration")]
        public static async System.Threading.Tasks.Task RunAsync([TimerTrigger("0 0 0 * * *")] TimerInfo myTimer, ILogger log)
        {
            try
            {
                var context = new WellAINotificationHandlerContext();
                List<MessageQueue> messageQueueList = new List<MessageQueue>();
                NotificationHandler notificationHandler = new NotificationHandler(context);
                WellAINotificationHandlerContext db = new WellAINotificationHandlerContext();
                var InsuranceNotificationInterval = Environment.GetEnvironmentVariable("InsuranceDocumentNotificationTriggerDays");
                log.LogInformation($"Insurance Expired interval days : " + InsuranceNotificationInterval);
                var Companies = db.CorporateProfile.ToList();
                var ServiceCompanies = db.CrmCompanies.Select(x => x.TenantId).ToList();
                log.LogInformation($"get operating companies");
                var OperatingCompanies = from c in Companies
                                         where !ServiceCompanies.Contains(c.TenantId)
                                         select c;
                log.LogInformation($"Before operating companies loop");
                foreach (var Company in OperatingCompanies)
                {
                    log.LogInformation($"operating companies Loop");

                    var TenantId = Company.TenantId;
                    log.LogInformation($"TenantId : " + TenantId);
                    var tId = Guid.Parse(TenantId);
                    var dbprefix = "oper";
                    log.LogInformation($"Before GetConnectionString");
                   
                    var strOperConnection = Environment.GetEnvironmentVariable("sqldb_connection");
                    log.LogInformation($"strOperConnection :" + strOperConnection);
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(strOperConnection)
                    { InitialCatalog = "wellai_" + dbprefix + "db_" + tId.ToString("N") };
                    log.LogInformation($"Get ConnectionString :" + builder.ConnectionString);
                    var ti = new TenantInfo(TenantId, TenantId, TenantId, builder.ConnectionString, null);
                    var operContext = new TenantOperatingDbContext(ti);
                    _tdbContext = operContext;
                    log.LogInformation($"Before taking Vendor list ");
                    var ProviderDiretoryList = _tdbContext.ProvidersDirectory.ToList();

                    log.LogInformation($"After taking taking Vendor list ");

                    foreach (var insurance in ProviderDiretoryList)
                    {
                        log.LogInformation($"insurance document loop");

                        var IsInsuranceExits = insurance.Insurance;
                        if(IsInsuranceExits != null)
                        {
                            log.LogInformation($"insurance document Expire date" + insurance.InsuranceExpire);

                            var InsuranceExpireDate = insurance.InsuranceExpire;
                            log.LogInformation($"Get Expire date : " + InsuranceExpireDate);
                            if (InsuranceExpireDate != null)
                            {
                                log.LogInformation($"after taking insurance document Expire date");
                              
                                int days;
                                using (SqlConnection Conn = new SqlConnection(builder.ConnectionString))
                                {
                                    Conn.Open();
                                    string Query = "select DATEDIFF(DAY,GETDATE(),insuranceExpire) as days from ProviderDirectory where CompanyId='" + insurance.CompanyId + "'";
                                    SqlCommand cmd = new SqlCommand(Query, Conn);
                                    log.LogInformation($"connection open");
                                    days = (int)cmd.ExecuteScalar();
                                }

                                log.LogInformation($"connection close");
                                var OperatorCompany = db.CorporateProfile.Where(x => x.TenantId == insurance.TenantId).FirstOrDefault();
                                var VendorCompany= db.CorporateProfile.Where(x => x.TenantId == insurance.CompanyId).FirstOrDefault();
                                var InsDoc = db.WellFile.Where(x => x.FileId == insurance.Insurance).FirstOrDefault();

                                if (Convert.ToInt32(days) >= 0 && Convert.ToInt32(days) <= Convert.ToInt32(InsuranceNotificationInterval))
                                {
                                    log.LogInformation($"Get date difference for insurance expire");
                                    string day = days.ToString() == "0" ? "today" : days.ToString();

                                    string taskName = "";

                                    if (days == 0)
                                    {
                                        taskName = "Insurance Document " + InsDoc.FileName + " will expire " + day.ToString() + " , Please reneval the insurance document.";
                                    }
                                    else
                                    {
                                        taskName = "Insurance Document " + InsDoc.FileName + " will expire within " + day.ToString() + " days, Please reneval the insurance document.";
                                    }
                                   
                                    MessageQueue messageQueue = new MessageQueue { From_id = OperatorCompany.UserId, To_id = VendorCompany.UserId, EntityId = Convert.ToString(insurance.Insurance), Type = Convert.ToInt32(8), IsActive = 1, TaskName = taskName, JobName = "Insurance Expire Notification", CreatedDate = DateTime.Now };
                                    db.MessageQueues.Add(messageQueue);
                                    await db.SaveChangesAsync(); 
                                    log.LogInformation($"Added notification for insurance expire");
                                }
                                else if(Convert.ToInt32(days) < 0)
                                {
                                    //Service Companies
                                    log.LogInformation($"Before add Service companies notification for expired");
                                    MessageQueue messageQueueForVendor = new MessageQueue { From_id = OperatorCompany.UserId, To_id = VendorCompany.UserId, EntityId = Convert.ToString(insurance.Insurance), Type = Convert.ToInt32(8), IsActive = 1, TaskName = "Insurance Document " + InsDoc.FileName + " Expired, Please reneval the insurance document. ", JobName = "Insurance Expired Notification", CreatedDate = DateTime.Now };
                                    db.MessageQueues.Add(messageQueueForVendor);
                                    await db.SaveChangesAsync();
                                    log.LogInformation($"Service companies notification for expired");

                                    log.LogInformation($"Before add Operating companies notification for expired");
                                    //Operating companies
                                    MessageQueue messageQueueForOperator = new MessageQueue { From_id = VendorCompany.UserId, To_id = OperatorCompany.UserId, EntityId = Convert.ToString(insurance.ID), Type = Convert.ToInt32(7), IsActive = 1, TaskName = VendorCompany.Name + " Insurance Document " + InsDoc.FileName + " expired", JobName = "Insurance Expired Notification", CreatedDate = DateTime.Now };
                                    db.MessageQueues.Add(messageQueueForOperator);
                                    await db.SaveChangesAsync();
                                    log.LogInformation($"Operating companies notification for expired");
                                    log.LogInformation($"Added notification for insurance expired");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.LogInformation($"Insurance Notification Error Message : {DateTime.Now}, Message : {ex.Message.ToString()} ");
            }
        }
    }
}