using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Function.DrillPlan;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Well_AI_Advisior.API.Authorize.Net.Services.IServices;
using Microsoft.EntityFrameworkCore;
using Well_AI_Advisior.API.Authorize.Net;
using System.Linq;

namespace Test
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            WellAIFunctionHandlerContext db = new WellAIFunctionHandlerContext();

            List<ProductSubscriptionModel> sublist = new List<ProductSubscriptionModel>();
            sublist = db.ProductSubscriptionModel.ToList();
            int Result = 0;
            for (int i = 0; i < sublist.Count; i++)
            {

                string subscriptionId = sublist[i].SubscriptionId;
                var services = new ServiceCollection();
                services.UseServices();
                var serviceProvider = services.BuildServiceProvider();
                var service = serviceProvider.GetRequiredService<IRecurringBillingService>();
                var tasks = service.GetSubscriptionStatus(subscriptionId, "");
                var aa = service.GetListOfSubscriptions("1");

                if (subscriptionId != null)
                {
                    var SUB = db.ProductSubscriptionModel.Where(X => X.SubscriptionId == subscriptionId).FirstOrDefault();

                    if (SUB != null)
                    {
                        SUB.IsEnable = true;
                        db.ProductSubscriptionModel.Add(SUB);
                        db.Entry(SUB).State = EntityState.Modified;
                        Result = db.SaveChanges();
                    }
                }
            }

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}
