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
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Model;
using WellAI.Advisor.Model.OperatingCompany.Models;

namespace WellAI.Advisor.Function.Notification
{
    public static class AddNotificationForWellDepthAndTask
    {
        [FunctionName("AddNotificationForWellDepthAndTask")]
        public static async System.Threading.Tasks.Task RunAsync([TimerTrigger("0 */30 * * * *",RunOnStartup = true)] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            var str = Environment.GetEnvironmentVariable("sqldb_connection");
            DataTable dataTable = new DataTable();
            DataTable dataTablePredictionLog = new DataTable();
            using (SqlConnection conn = new SqlConnection(str))
            {
                conn.Open();
                var sqlQuery = Environment.GetEnvironmentVariable("sqlAddNotificationForWellDepthAndTask");
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet dataSet = new DataSet();

                da.Fill(dataSet);
                dataTable = dataSet.Tables[0];
                dataTablePredictionLog = dataSet.Tables[1];
            }
            DataView view = new DataView(dataTable);
            DataTable distinctValues = view.ToTable(true, "wellID", "Current_depth", "OperatorID", "RigId");
            List<MessageQueue> messageQueues = new List<MessageQueue>();
            List<PredictionLog> addPredictionLogs = new List<PredictionLog>();
            List<PredictionLog> updatePredictionLogs = new List<PredictionLog>();
            int i = 0;
            foreach (DataRow row in distinctValues.Rows)
            {
                i++;
                string wellID = row["wellID"].ToString();
                string Current_depth = row["Current_depth"].ToString();
                string OperatorID = row["OperatorID"].ToString();
                string RigId = row["RigId"].ToString();
                //string RigId = row["RigId"].ToString();
                string taskName = await getTaskNameAsync(wellID, Current_depth, log);
                if (taskName != "Error")
                {
                    var predictionLogScoredLabels = (from predictionLog in dataTablePredictionLog.AsEnumerable()
                                                     where predictionLog.Field<string>("WellId") == wellID
                                                     select new
                                                     {
                                                         ScoredLabels = predictionLog.Field<string>("ScoredLabels")
                                                     }).Distinct().FirstOrDefault();
                    if (predictionLogScoredLabels != null && predictionLogScoredLabels.ScoredLabels != taskName)
                    {
                        PredictionLog predictionLog = new PredictionLog
                        {
                            ScoredLabels = taskName,
                            WellId = wellID
                        };
                        updatePredictionLogs.Add(predictionLog);
                        AddNotificationMessageQueues(dataTable, messageQueues, wellID, OperatorID, RigId, taskName);
                    }
                    else if (predictionLogScoredLabels == null)
                    {
                        PredictionLog predictionLog = new PredictionLog
                        {
                            ScoredLabels = taskName,
                            WellId = wellID,
                            ScoreDateTime = DateTime.Now
                        };
                        addPredictionLogs.Add(predictionLog);
                        AddNotificationMessageQueues(dataTable, messageQueues, wellID, OperatorID, RigId, taskName);
                    }
                }
            }
            var context = new WellAINotificationHandlerContext();
            NotificationHandler notificationHandler = new NotificationHandler(context);
            notificationHandler.AddRangeNotifications(messageQueues, addPredictionLogs, updatePredictionLogs);
        }

        private static void AddNotificationMessageQueues(DataTable dataTable, List<MessageQueue> messageQueues, string wellID, string OperatorID, string RigId, string taskName)
        {
            var userIds = (from user in dataTable.AsEnumerable()
                           where user.Field<string>("OperatorID") == OperatorID
                           select new
                           {
                               userId = user.Field<string>("To_Id")
                           }).Distinct().ToList();
            foreach (var userId in userIds)
            {
                MessageQueue messageQueue = new MessageQueue
                {
                    CreatedDate = DateTime.Now,
                    IsActive = 1,
                    TaskName = taskName,
                    Type = 4,
                    JobName = taskName,
                    From_id = "Well-AI Support",
                    EntityId = wellID,
                    OperatorId = OperatorID,
                    To_id = userId.userId,
                    RigId = RigId
                };
                messageQueues.Add(messageQueue);
            }
        }

        //adding new fields for other parameters
        //public static async System.Threading.Tasks.Task<string> getTaskNameAsync(string well_Id, string depth, ILogger log)
        public static async System.Threading.Tasks.Task<string> getTaskNameAsync(string well_Id, string depth, ILogger log)
        {
            var handler = new HttpClientHandler()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
            using (var client = new HttpClient(handler))
            {
                var scoreRequest = new
                {
                    Inputs = new Dictionary<string, List<Dictionary<string, string>>>() {
                        {
                            "WebServiceInput0",
                            new List<Dictionary<string, string>>(){
                                //new Dictionary<string, string>(){
                                //    {
                                //        "well_id", well_Id
                                //    },
                                //    {
                                //        "day", "4"
                                //    },
                                //    {
                                //        "depth", depth
                                //    },
                                //    {
                                //        "time", "00:21"
                                //    },
                                //    {
                                //        "taskname", "Call Trrc"
                                //    },
                                //}
                                 new Dictionary<string, string>(){
                                    {
                                        "customer_id", ""
                                    },
                                     {
                                        "PadID", ""
                                    },                                     
                                    {
                                        "BatchFlag", "0"
                                    },
                                    {
                                        "RigID", ""
                                    },
                                    {
                                        "well_id", well_Id
                                    },
                                    {
                                        "welltype_id", ""
                                    },
                                    {
                                        "welltask_id", ""
                                    },                                    
                                    {
                                        "day", "0"
                                    },
                                    {
                                        "depth", depth
                                    },
                                    {
                                        "time", ""
                                    },
                                    {
                                        "duration", "0.0"
                                    },
                                    {
                                        "leadtime", "0"
                                    },
                                      {
                                        "dependency_flag", "0"
                                    },
                                      {
                                        "dependency", ""
                                    },
                                    {
                                        "taskname", ""
                                    },
                                    {
                                        "ActionDate", ""
                                    },
                                    {
                                        "taskstatus", "0"
                                    },
                                }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };
                string apiKeyValue = Environment.GetEnvironmentVariable("apiKeyValue");/// "J9umMNgmSAENr2wGT6ZQzeqnjC9XHBxo"; // Replace this with the API key for the web service
                string apiUrl = Environment.GetEnvironmentVariable("apiUrl");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKeyValue);
                client.BaseAddress = new Uri(apiUrl);

                var requestString = JsonConvert.SerializeObject(scoreRequest);
                var content = new StringContent(requestString);

                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await client.PostAsync("", content);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    var details = JObject.Parse(result);
                    var obj = JArray.Parse(details["Results"]["WebServiceOutput0"].ToString());
                    string taskName = (string)obj[0]["Scored Labels"];
                    return taskName;
                }
                else
                {
                    log.LogInformation(string.Format("The request failed with status code: {0}", response.StatusCode));
                    log.LogInformation(response.Headers.ToString());
                    string responseContent = await response.Content.ReadAsStringAsync();
                    log.LogInformation(responseContent);
                    return "Error";
                }
            }
        }
    }

    public class WellAINotificationHandlerContext : DbContext
    {
        public DbSet<MessageQueue> MessageQueues { get; set; }
        public DbSet<ErrorLog> ErrorLog { get; set; }
        public DbSet<PredictionLog> PredictionLogs { get; set; }
        public DbSet<CorporateProfile> CorporateProfile { get; set; }
        public DbSet<CrmUserBasicDetail> CrmUserBasicDetail { get; set; }
        public DbSet<CorporateProfile> CorporateProfiles { get; set;}
        public DbSet<CrmCompanies> CrmCompanies { get; set; }
        public DbSet<WellFile> WellFile { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string sqlCon = Environment.GetEnvironmentVariable("sqldb_connection");
            //Phase II Changes - 03/292/2021
            optionsBuilder.UseSqlServer(sqlCon,options => options.EnableRetryOnFailure());
        }
    }
}