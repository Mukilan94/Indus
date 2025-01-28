using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace WellAI.Advisor.Function.WellDataStage
{
    public static class WellDataStage
    {
        [FunctionName("WellDataStage")]
        public static void Run([TimerTrigger("0 */15 * * * *")]TimerInfo myTimer, ILogger log)
        {

            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            var str = Environment.GetEnvironmentVariable("sqldb_connection");
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = new SqlConnection(str))
            {
                conn.Open();
                var sqlQuery= Environment.GetEnvironmentVariable("sqlQuery");
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataTable);
            }
        }
    }
}
