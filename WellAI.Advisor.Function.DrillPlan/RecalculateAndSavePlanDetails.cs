using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WellAI.Advisor.Model.OperatingCompany;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.Model.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WellAI.Advisor.BLL.Business;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Model.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WellAI.Advisor.Function.DrillPlan
{
    public static class RecalculateAndSavePlanDetails
    {
        
        [FunctionName("RecalculateAndSavePlanDetails")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {           
            try
            {
                log.LogInformation("RecalculateAndSavePlanDetails function started");
                //WebAIAdvisorContext dbcontext = new WebAIAdvisorContext();
                
                WellAIFunctionHandlerContext dbcontext = new WellAIFunctionHandlerContext();
                //return null;
                var content = await new StreamReader(req.Body).ReadToEndAsync();
                PlanDetailsModel planDetails = JsonConvert.DeserializeObject<PlanDetailsModel>(content);
                log.LogInformation("Request parsing completed");
                //var context = new WellAIFunctionHandlerContext();

                var result = await DrillingPlanRepository.SaveUpdatePlandetails(planDetails, planDetails.TenantId, dbcontext, log);
                log.LogInformation("Request parsing completed" + Convert.ToString(result));

                var responseObj = new GeneralHttpResponse{resultCode = result, resultMessage = "Success", messageContent = ""};
                var response = JsonConvert.SerializeObject(responseObj);

                //return new JsonResult(response);
                return (ActionResult)new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                var responseObj = new GeneralHttpResponse { resultCode = 0, resultMessage = "Failure", messageContent = ex.Message.ToString() };
                var response = JsonConvert.SerializeObject(responseObj);
                log.LogInformation($"Recalculate and Save Drill Plan Error Message : {DateTime.Now}, Message : {ex.Message.ToString()} ");
                return new JsonResult(response);
            }                       
        }
        
    }
    public class WellAIFunctionHandlerContext : IdentityDbContext<WellIdentityUser>
    {
        //public DbSet<MessageQueue> MessageQueues { get; set; }
        public DbSet<ErrorLog> ErrorLog { get; set; }
        public DbSet<BillingHistory> billingHistoryModel { get; set; }
        public DbSet<CorporateProfile> CorporateProfile { get; set; }
        public DbSet<WellFile> WellFile { get; set; }
        public DbSet<DrillPlanWells> DrillPlanWells { get; set; }
        public DbSet<DrillPlanHeader> DrillPlanHeader { get; set; }
        public DbSet<DrillPlanDetails> DrillPlanDetails { get; set; }
        public DbSet<MessageQueue> MessageQueues { get; set; }
        public DbSet<WellRegister> WellRegister { get; set; }
        public DbSet<WellIdentityUser> WellIdentityUser { get; set; }
        public DbSet<UserRig> UserRigs { get;set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<CategoryTask> CategoryTask { get; set; }
        public DbSet<ProductSubscriptionModel> ProductSubscriptionModel { get; set; }
        public DbSet<PaymentMethod> PaymentMethod { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //  string sqlCon = Environment.GetEnvironmentVariable("sqldb_connection");
            //Beta
            //string sqlCon = "Server=tcp:wellai.database.windows.net,1433 ; Database=wellaidb; user id= wellaiadmin ; password = Wellaidb#;";
            ////Test
            //string sqlCon = "Server=tcp:wellai-mmsql.database.windows.net,1433 ; Database=wellai-testing; user id= WellMaster ; password = aIWWCqsged4RSKOjd6we5Zbb;";
            //Test
            string sqlCon = "Server=tcp:wellai-mmsql.database.windows.net,1433;Initial Catalog=wellai-main;Persist Security Info=False;User ID=WellMaster;Password=aIWWCqsged4RSKOjd6we5Zbb;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;MultipleActiveResultSets=true;Timeout=300";

            //Phase II Changes - 09/07/2021
            optionsBuilder.UseSqlServer(sqlCon, options => options.EnableRetryOnFailure());
        }
    }
}
