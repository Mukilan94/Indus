using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Build.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;

namespace WellAI.Advisor.Function
{
    public static class AddNotificationForWellDepthAndTask
    {
        [FunctionName("AddNotificationForWellDepthAndTask")]
        public static async System.Threading.Tasks.Task RunAsync([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            var str = Environment.GetEnvironmentVariable("sqldb_connection");
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(str))
            {
                conn.Open();
                var sqlQuery = Environment.GetEnvironmentVariable("sqlAddNotificationForWellDepthAndTask");
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataTable);
            }
            DataView view = new DataView(dataTable);
            DataTable distinctValues = view.ToTable(true, "wellID", "Current_depth", "OperatorID", "RigId");
            List<MessageQueue> messageQueues = new List<MessageQueue>();
            int i = 0;
            foreach (DataRow row in distinctValues.Rows)
            {
                i++;
                string wellID = row["wellID"].ToString();
                string Current_depth = row["Current_depth"].ToString();
                string OperatorID = row["OperatorID"].ToString();
                string RigId = row["RigId"].ToString();
                // string To_Id = row["To_Id"].ToString();
                string taskName = await getTaskNameAsync(wellID, Current_depth,log);
                if (taskName != "Error")
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
            }
            var context = new WellAINotificationHandlerContext();
            NotificationHandler notificationHandler = new NotificationHandler(context);
            notificationHandler.AddRangeNotifications(messageQueues);
        }

        public static async System.Threading.Tasks.Task<string> getTaskNameAsync(string well_Id,string depth, ILogger log)
        {
            var handler = new HttpClientHandler()
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback =  (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
            using (var client = new HttpClient(handler))
            {
                var scoreRequest = new
                {
                    Inputs = new Dictionary<string, List<Dictionary<string, string>>>() {
                        {
                            "WebServiceInput0",
                            new List<Dictionary<string, string>>(){
                                new Dictionary<string, string>(){
                                    {
                                        "well_id", well_Id
                                    },
                                    {
                                        "day", "4"
                                    },
                                    {
                                        "depth", depth
                                    },
                                    {
                                        "time", "00:21"
                                    },
                                    {
                                        "taskname", "Call Trrc"
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
                string apiUrl= Environment.GetEnvironmentVariable("apiUrl");
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
                    // var detail = JObject.Parse(details["WebServiceOutput0"].ToString());
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string sqlCon= Environment.GetEnvironmentVariable("sqldb_connection");
            optionsBuilder.UseSqlServer(sqlCon);
        }
    }

}
