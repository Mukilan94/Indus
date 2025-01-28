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
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.DLL.Data;
using Newtonsoft.Json.Linq;

namespace WellAI.Advisor.Function.SubscriptionStatus
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

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            WellAIFunctionHandlerContext dbFun = new WellAIFunctionHandlerContext();
          
            List<ProductSubscriptionModel> sublist = new List<ProductSubscriptionModel>();

            List<BillingHistory> sublist1 = new List<BillingHistory>();

            //List<PaymentMethod> PayM = new List<PaymentMethod>();
            //PayM = db.PaymentMethod.ToList();

            //  sublist = db.ProductSubscriptionModel.ToList();
            //  int status = 1;
            //  int Result = 0;
            //  //for (int i = 0; i < sublist.Count-1; i++)
            //  //{
            //       //string subscriptionId = sublist[i].SubscriptionId;
            //      string subscriptionId = "8534499";
            //      var services = new ServiceCollection();
            //      services.UseServices();
            //      var serviceProvider = services.BuildServiceProvider();
            //      var service = serviceProvider.GetRequiredService<IRecurringBillingService>();
            //      var tasks = service.GetSubscription(subscriptionId, "");

            //      var resultJson = JsonConvert.SerializeObject(new { BodyParameter = tasks });
            //      dynamic ReadStatus = JsonConvert.DeserializeObject(resultJson);

            //      if (ReadStatus.BodyParameter.subscription != null)
            //      {
            //                status = ReadStatus.BodyParameter.subscription.status;
            //      }

            //      if (subscriptionId != null)
            //      {
            //          var SUB = db.ProductSubscriptionModel.Where(X => X.SubscriptionId == subscriptionId).FirstOrDefault();
            //          if (SUB != null)
            //          {
            //              if (status == 0)
            //              {
            //                  SUB.IsEnable = true;
            //              }
            //              else
            //              {
            //                  SUB.IsEnable = false;
            //              }
            //              db.ProductSubscriptionModel.Add(SUB);
            //              db.Entry(SUB).State = EntityState.Modified;
            //              db.SaveChanges();

            //          }
            //      }
            ////  }

            //  var services1 = new ServiceCollection();
            //  services1.UseServices();
            //  var serviceProvider1 = services1.BuildServiceProvider();
            //  var service1 = serviceProvider1.GetRequiredService<IRecurringBillingService>();

            //  // string sFirstdate = "2022-09-10T16:00:00Z";
            //  DateTime Firstdate = DateTime.Now.AddDays(-1);

            //  // string sLastdate = "2022-09-30T16:00:00Z";
            //  // DateTime Lastdate = Convert.ToDateTime(sLastdate);
            //  DateTime Lastdate = DateTime.Now;

            //  var Batch = service1.GetSettledBatchListStatus("", Firstdate,Lastdate);

            //  var JsonBatch = JsonConvert.SerializeObject(new { BodyParameter = Batch });
            //  dynamic BatchStatus = JsonConvert.DeserializeObject(JsonBatch);

            //  int RecCount = Enumerable.Count(BatchStatus.BodyParameter.batchList);
            //  String type = "";
            //  string subscriptionnId = "";
            //  string AddressId = "";
            //  for (int i = 1; i < RecCount; i++)
            //  {
            //      string batchId = BatchStatus.BodyParameter.batchList[i].batchId;
            //      if (batchId != null)
            //      {
            //          var Trans = service1.GetTransactionListRequestStatus("", batchId);
            //          var JsonTrans = JsonConvert.SerializeObject(new { BodyParameter = Trans });
            //          dynamic TransStatus = JsonConvert.DeserializeObject(JsonTrans);

            //          if(TransStatus.BodyParameter.transactions[0].subscription.id != null)
            //          {
            //              subscriptionnId = TransStatus.BodyParameter.transactions[0].subscription.id;
            //              string Invoice = TransStatus.BodyParameter.transactions[0].invoice;
            //              string Billdatee = TransStatus.BodyParameter.transactions[0].submitTimeUTC;
            //              string TransactionID = TransStatus.BodyParameter.transactions[0].transId;
            //              DateTime Billdate = Convert.ToDateTime(Billdatee);

            //              var SubDetails = service1.GetListOfSubscriptions("");
            //              var JsonSubDetails = JsonConvert.SerializeObject(new { BodyParameter = SubDetails });
            //              dynamic SubDetailsStatus = JsonConvert.DeserializeObject(JsonSubDetails);

            //              int RecCount1 = Enumerable.Count(SubDetailsStatus.BodyParameter.subscriptionDetails);
            //              for (int j = 0; j < RecCount; j++)
            //              {
            //                  string subid = SubDetailsStatus.BodyParameter.subscriptionDetails[j].id;
            //                  if (subid == subscriptionnId)
            //                  {
            //                      type = SubDetailsStatus.BodyParameter.subscriptionDetails[j].paymentMethod;
            //                      break;
            //                  }
            //              }
            //              // string id = "7225836";
            //              var Subitem = (from x in db.ProductSubscriptionModel
            //                             where x.SubscriptionId.Equals(subscriptionnId)
            //                             select x).FirstOrDefault();
            //              if(Subitem.TenantId.ToString() != null)
            //              {
            //                  var Addid = (from x in db.PaymentMethod
            //                               where x.TenantId.Equals(Subitem.TenantId.ToString())
            //                               select x).FirstOrDefault();
            //                  if (Addid.ID.ToString() != null)
            //                  {
            //                      AddressId = Addid.ID.ToString();
            //                  }

            //              }

            //              if (subscriptionnId != null && TransactionID != null)
            //              {
            //                  var item = (from x in db.billingHistoryModel
            //                              where x.TransactionID.Equals(TransactionID)
            //                              select x).FirstOrDefault();
            //                  if (item == null)
            //                  {
            //                      BillingHistory bh = new BillingHistory();
            //                      bh.ID = Guid.NewGuid().ToString("D");
            //                      bh.Name = TransStatus.BodyParameter.transactions[0].firstName + " " + TransStatus.BodyParameter.transactions[0].lastName;
            //                      bh.Invoice = Invoice;
            //                      bh.BillDate = Billdate;
            //                      bh.Amount = TransStatus.BodyParameter.transactions[0].settleAmount;
            //                      bh.Subscriptions = 1;
            //                      bh.PayMethod = type;
            //                      bh.AddressId = AddressId;
            //                      bh.TenantId = Subitem.TenantId.ToString();
            //                      bh.TransactionID = TransactionID;
            //                      db.billingHistoryModel.Add(bh);
            //                      db.SaveChanges();
            //                  }
            //              }
            //          }                  
            //      }             
            //  }                    

            string SubscriptionID = "";
            var services1 = new ServiceCollection();
            services1.UseServices();
            var serviceProvider1 = services1.BuildServiceProvider();
            var service1 = serviceProvider1.GetRequiredService<IRecurringBillingService>();

            string sFirstdate = "2022-11-14T11:00:00Z";
            DateTime Firstdate = Convert.ToDateTime(sFirstdate);
            //DateTime Firstdate = DateTime.Now.AddHours(-1);

             string sLastdate = "2022-11-16T13:00:00Z";
            DateTime Lastdate = Convert.ToDateTime(sLastdate);
           // DateTime Lastdate = DateTime.Now.AddDays(1);

            var Batch = service1.GetSettledBatchListStatus("", Firstdate, Lastdate);

            var JsonBatch = JsonConvert.SerializeObject(new { BodyParameter = Batch });
            dynamic BatchStatus = JsonConvert.DeserializeObject(JsonBatch);

            int RecCount = Enumerable.Count(BatchStatus.BodyParameter.batchList);
            String type = "";
            string subscriptionnId = "";
            string AddressId = "";
            for (int i = 0; i < RecCount; i++)
            {
                string batchId = BatchStatus.BodyParameter.batchList[i].batchId;
                if (batchId != null)
                {
                    var Trans = service1.GetTransactionListRequestStatus("", batchId);
                    var JsonTrans = JsonConvert.SerializeObject(new { BodyParameter = Trans });
                    dynamic TransStatus = JsonConvert.DeserializeObject(JsonTrans);

                    int TransRecCount = Enumerable.Count(TransStatus.BodyParameter.transactions);
                    for (int j = 0; j < TransRecCount; j++)
                    {
                        if (TransStatus.BodyParameter.transactions[j].subscription.id != null)
                        {
                            if (TransStatus.BodyParameter.transactions[j].subscription.id == SubscriptionID)
                            {
                                subscriptionnId = TransStatus.BodyParameter.transactions[j].subscription.id;
                                string Invoice = TransStatus.BodyParameter.transactions[j].invoice;
                                string Billdatee = TransStatus.BodyParameter.transactions[j].submitTimeUTC;
                                string TransactionID = TransStatus.BodyParameter.transactions[j].transId;
                                DateTime Billdate = Convert.ToDateTime(Billdatee);

                                var SubDetails = service1.GetListOfSubscriptions("");
                                var JsonSubDetails = JsonConvert.SerializeObject(new { BodyParameter = SubDetails });
                                dynamic SubDetailsStatus = JsonConvert.DeserializeObject(JsonSubDetails);

                                int RecCount1 = Enumerable.Count(SubDetailsStatus.BodyParameter.subscriptionDetails);
                                for (int k = 0; k < RecCount; k++)
                                {
                                    string subid = SubDetailsStatus.BodyParameter.subscriptionDetails[k].id;
                                    if (subid == SubscriptionID)
                                    {
                                        type = SubDetailsStatus.BodyParameter.subscriptionDetails[k].paymentMethod;
                                        break;
                                    }
                                }

                                var Subitem = (from x in dbFun.ProductSubscriptionModel
                                               where x.SubscriptionId.Equals(SubscriptionID)
                                               select x).FirstOrDefault();
                                if (Subitem.TenantId.ToString() != null)
                                {
                                    var Addid = (from x in dbFun.PaymentMethod
                                                 where x.TenantId.Equals(Subitem.TenantId.ToString())
                                                 select x).FirstOrDefault();
                                    if (Addid.ID.ToString() != null)
                                    {
                                        AddressId = Addid.ID.ToString();
                                    }

                                }

                                if (subscriptionnId == SubscriptionID && TransactionID != null)
                                {
                                    var item = (from x in dbFun.billingHistoryModel
                                                where x.TransactionID.Equals(TransactionID)
                                                select x).FirstOrDefault();
                                    if (item == null)
                                    {
                                        BillingHistory bh = new BillingHistory();
                                        bh.ID = Guid.NewGuid().ToString("D");
                                        bh.Name = TransStatus.BodyParameter.transactions[0].firstName + " " + TransStatus.BodyParameter.transactions[0].lastName;
                                        bh.Invoice = Invoice;
                                        bh.BillDate = Billdate;
                                        bh.Amount = TransStatus.BodyParameter.transactions[0].settleAmount;
                                        bh.Subscriptions = 1;
                                        bh.PayMethod = type;
                                        bh.AddressId = AddressId;
                                        bh.TenantId = Subitem.TenantId.ToString();
                                        bh.TransactionID = TransactionID;
                                        dbFun.billingHistoryModel.Add(bh);
                                        dbFun.SaveChanges();
                                    }
                                }
                                break;
                            }


                        }
                    }

                }
            }
            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
     
    }
}
