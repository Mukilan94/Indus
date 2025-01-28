using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
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

namespace WellAI.Advisor.Function.SubscriptionStatusUpdate
{
    public class ReadSubscription
    {
        [FunctionName("ReadSubscription")]
        public void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            WellAIFunctionHandlerContext db = new WellAIFunctionHandlerContext();

            List<ProductSubscriptionModel> sublist = new List<ProductSubscriptionModel>();

            List<BillingHistory> sublist1 = new List<BillingHistory>();

            sublist = db.ProductSubscriptionModel.ToList();
            int status = 1;
            int Result = 0;
            for (int i = 0; i < sublist.Count - 1; i++)
            {
                string subscriptionId = sublist[i].SubscriptionId;
                // string subscriptionId = "8410840";
                var services = new ServiceCollection();
                services.UseServices();
                var serviceProvider = services.BuildServiceProvider();
                var service = serviceProvider.GetRequiredService<IRecurringBillingService>();
                var tasks = service.GetSubscription(subscriptionId, "");

                var resultJson = JsonConvert.SerializeObject(new { BodyParameter = tasks });
                dynamic ReadStatus = JsonConvert.DeserializeObject(resultJson);

                if (ReadStatus.BodyParameter.subscription != null)
                {
                    status = ReadStatus.BodyParameter.subscription.status;
                }

                if (subscriptionId != null)
                {
                    var SUB = db.ProductSubscriptionModel.Where(X => X.SubscriptionId == subscriptionId).FirstOrDefault();
                    if (SUB != null)
                    {
                        if (status == 0)
                        {
                            SUB.IsEnable = true;
                        }
                        else
                        {
                            SUB.IsEnable = false;
                        }
                        db.ProductSubscriptionModel.Add(SUB);
                        db.Entry(SUB).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                }
            }

            var services1 = new ServiceCollection();
            services1.UseServices();
            var serviceProvider1 = services1.BuildServiceProvider();
            var service1 = serviceProvider1.GetRequiredService<IRecurringBillingService>();

            //string sFirstdate = "2022-09-01T16:00:00Z";        
            //DateTime Firstdate = Convert.ToDateTime(sFirstdate);
            DateTime Firstdate = DateTime.Now.AddDays(-1);

            //string sLastdate = "2022-10-01T16:00:00Z";
            //DateTime Lastdate = Convert.ToDateTime(sLastdate);
            DateTime Lastdate = DateTime.Now;

            var Batch = service1.GetSettledBatchListStatus("", Firstdate, Lastdate);

            var JsonBatch = JsonConvert.SerializeObject(new { BodyParameter = Batch });
            dynamic BatchStatus = JsonConvert.DeserializeObject(JsonBatch);

            int RecCount = Enumerable.Count(BatchStatus.BodyParameter.batchList);
            string type = "";
            string subscriptionnId = "";
            string AddressId = "";
            for (int i = 1; i < RecCount; i++)
            {
                string batchId = BatchStatus.BodyParameter.batchList[i].batchId;
                if (batchId != null)
                {

                    type = BatchStatus.BodyParameter.batchList[i].paymentMethod;
                    var Trans = service1.GetTransactionListRequestStatus("", batchId);
                    var JsonTrans = JsonConvert.SerializeObject(new { BodyParameter = Trans });
                    dynamic TransStatus = JsonConvert.DeserializeObject(JsonTrans);
                    int trCount = Enumerable.Count(TransStatus.BodyParameter.transactions);
                    for (int j = 0; j < trCount; j++)
                    {
                        if (TransStatus.BodyParameter.transactions[j].subscription.id != null)
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
                            //for (int k = 0; k < RecCount1; k++)
                            //{
                            //    string subid = SubDetailsStatus.BodyParameter.subscriptionDetails[k].id;
                            //    if (subid == subscriptionnId)
                            //    {
                            //        type = SubDetailsStatus.BodyParameter.subscriptionDetails[k].paymentMethod;
                            //        break;
                            //    }
                            //}

                            var Subitem = (from x in db.ProductSubscriptionModel
                                           where x.SubscriptionId.Equals(subscriptionnId)
                                           select x).FirstOrDefault();
                            if (Subitem != null)
                            {
                                if (Subitem.TenantId.ToString() != null)
                                {
                                    var Addid = (from x in db.PaymentMethod
                                                 where x.TenantId.Equals(Subitem.TenantId.ToString())
                                                 select x).FirstOrDefault();
                                    if (Addid != null)
                                    {
                                        if (Addid.ID.ToString() != null)
                                        {
                                            AddressId = Addid.ID.ToString();
                                        }
                                    }

                                    if (subscriptionnId != null && TransactionID != null)
                                    {
                                        var item = (from x in db.billingHistoryModel
                                                    where x.TransactionID.Equals(TransactionID)
                                                    select x).FirstOrDefault();
                                        if (item == null)
                                        {
                                            BillingHistory bh = new BillingHistory();
                                            bh.ID = Guid.NewGuid().ToString("D");
                                            bh.Name = TransStatus.BodyParameter.transactions[j].firstName + " " + TransStatus.BodyParameter.transactions[j].lastName;
                                            bh.Invoice = Invoice;
                                            bh.BillDate = Billdate;
                                            bh.Amount = TransStatus.BodyParameter.transactions[j].settleAmount;
                                            bh.Subscriptions = 1;
                                            if (type == "creditCard")
                                            {
                                                type = "1";
                                            }
                                            else if (type == "debitCard")
                                            {
                                                type = "2";
                                            }
                                            else
                                            {
                                                type = "0";
                                            }
                                            bh.PayMethod = type;
                                            bh.AddressId = AddressId;
                                            bh.TenantId = Subitem.TenantId.ToString();
                                            bh.TransactionID = TransactionID;
                                            db.billingHistoryModel.Add(bh);
                                            db.SaveChanges();
                                        }
                                    }
                                }
                            }



                        }
                    }



                }
            }

        }
    }
}
