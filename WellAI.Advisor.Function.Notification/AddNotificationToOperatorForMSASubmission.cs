using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;

namespace WellAI.Advisor.Function.Notification
{   
    public static class AddNotificationToOperatorForMSASubmission
    {
        [FunctionName("AddNotificationToOperatorForMSASubmission")]
        public static async System.Threading.Tasks.Task RunAsync([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer, ILogger log)
        {
            try
            {
                log.LogInformation($"MSA Notification to Operator executed at : {DateTime.Now}");
                var str = Environment.GetEnvironmentVariable("sqldb_connection");
                var notificationtriggerinterval = Environment.GetEnvironmentVariable("MSANotificationTriggerIntervalInHours");
                DataTable dataTable = new DataTable();
                DataTable dataTablePredictionLog = new DataTable();
                using (SqlConnection conn = new SqlConnection(str))
                {
                    conn.Open();
                    //Notification Time
                    string sqlQuery = "SELECT ProviderMSALink .*,WellFile.UserId FROM ProviderMSALink " +
                                      " INNER JOIN WellFile ON ProviderMSALink.FileId = WellFile.FileId " +
                                      " INNER JOIN CrmUserBasicDetail ON WellFile.UserId = CrmUserBasicDetail.UserId " +
                                      " WHERE " +
                                      " NotificationStatus IS NULL AND isApproved IS NULL AND " +
                                      " DATEDIFF(hour,COALESCE(FileUploadTime,GETDATE()),GETDATE()) >= '"+ Convert.ToString(notificationtriggerinterval) + "' AND " +
                                      " CrmUserBasicDetail.AccountType = 1 ";

                    log.LogInformation($"MSA Notification Query : {sqlQuery}");

                    SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet dataSet = new DataSet();

                    log.LogInformation($"MSA Notification Query Ran ");
                    da.Fill(dataSet);
                    dataTable = dataSet.Tables[0];
                    log.LogInformation($"MSA Notification Data Assign to datatable ");
                }
                DataView view = new DataView(dataTable);
                DataTable distinctValues = view.ToTable();

                int i = 0;
                foreach (DataRow row in distinctValues.Rows)
                {
                    log.LogInformation($"Notification loop {i}");
                    i++;
                    List<MessageQueue> messageQueueList = new List<MessageQueue>();
                    string MSALinkId = Convert.ToString(row["Id"]);
                    string serviceUserId = Convert.ToString(row["UserId"]);

                    var context = new WellAINotificationHandlerContext();
                    
                    NotificationHandler notificationHandler = new NotificationHandler(context);
                    log.LogInformation($"Before Operator Manager Users Get {i}");
                    var OperatingUserIds = await notificationHandler.GetOperatorManagerUsers(Convert.ToString(row["OperationTenantId"]));

                    log.LogInformation($"Before Service Company User {i}");
                    string companyName = await notificationHandler.GetServiceCompanyUser(serviceUserId);
                    string SerTenantId = Convert.ToString(row["ServiceTenantId"]);
                    var dbprefix = "oper";
                    var tId = Guid.Parse(Convert.ToString(row["OperationTenantId"]));
                    log.LogInformation($"sqldb_connection {Environment.GetEnvironmentVariable("sqldb_connection")}");
                    log.LogInformation($"tId {tId}");
                    log.LogInformation($"dbprefix {dbprefix}");

                    var strOperConnection = Environment.GetEnvironmentVariable("sqldb_connection");
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(strOperConnection)
                    { InitialCatalog = "wellai_" + dbprefix + "db_" + tId.ToString("N") };


                    log.LogInformation($"strOperConnection from builder {builder.ConnectionString}");
                    string providerId = "";

                    using (SqlConnection con = new SqlConnection(builder.ConnectionString))
                    {
                        con.Open();
                        string qry = "SELECT ID from ProviderDirectory where companyId = '"+ SerTenantId + "'";
                        SqlCommand cmdProvider = new SqlCommand(qry, con);
                        providerId = Convert.ToString(cmdProvider.ExecuteScalar());
                    }

                    foreach (var item in OperatingUserIds)
                    {
                        log.LogInformation($"Message queue loop ");
                        MessageQueue messageQueue = new MessageQueue { From_id = serviceUserId, To_id = item.UserId, EntityId = Convert.ToString(providerId), Type = Convert.ToInt32(7), IsActive = 1, TaskName = "MSA Document Uploaded by " + companyName + ". Please Review.", JobName = "MSA Upload Notification", CreatedDate = DateTime.Now };
                        messageQueueList.Add(messageQueue);
                    }

                    notificationHandler.AddMessageNotifications(messageQueueList);
                    log.LogInformation($"Messages addeded to Notification");

                    string sqlUpdateQuery = "UPDATE ProviderMSALink SET NotificationStatus=1 " +
                                    " WHERE Id='" + MSALinkId + "'";

                    using (SqlConnection conn = new SqlConnection(str))
                    {
                        conn.Open();

                        log.LogInformation($"MSALink Notification Status Update Starts");
                        SqlCommand cmd = new SqlCommand(sqlUpdateQuery, conn);
                        cmd.ExecuteNonQuery();
                        log.LogInformation($"MSALink Notification Status Update Completed");
                    }
                }
            }
            catch (Exception ex)
            {
                log.LogInformation($"MSA Notification Error Message : {DateTime.Now}, Message : {ex.Message.ToString()} ");
            }                        
        }        
    }
}

