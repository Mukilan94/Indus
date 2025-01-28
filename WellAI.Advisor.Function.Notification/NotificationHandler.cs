using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Model.OperatingCompany.Models;

namespace WellAI.Advisor.Function.Notification
{
    public class NotificationHandler
    {
        private readonly WellAINotificationHandlerContext db;
        public NotificationHandler(WellAINotificationHandlerContext dbContext)
        {
            db = dbContext;
        }
        public void AddNotification(MessageQueue messageQueue)
        {
            messageQueue.IsActive = 1;
            messageQueue.CreatedDate = DateTime.Now;
            db.MessageQueues.Add(messageQueue);
            db.SaveChanges();
        }
        public void AddRangeNotifications(List<MessageQueue> messageQueues,List<PredictionLog> addPredictionLogs,List<PredictionLog> updatePredictionLogs)
        {
            try
            {
                db.MessageQueues.AddRange(messageQueues);
                if(addPredictionLogs.Count>0)
                    db.PredictionLogs.AddRange(addPredictionLogs);
                if(updatePredictionLogs.Count>0)
                    db.PredictionLogs.UpdateRange(updatePredictionLogs);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ErrorHandlerForNotification customErrorHandler = new ErrorHandlerForNotification(db);
                customErrorHandler.WriteError(ex, "NotificationHandler AddRangeNotifications", null);
                throw;
            }
            
        }
        public List<MessageQueue> GetNotifications(string userId)
        {
            List<MessageQueue> messageQueues = db.MessageQueues.Where(x => x.To_id == userId && x.IsActive == 1).ToList();
            return messageQueues;
        }
        //Phase II Changes - 01/30/2021
        public void AddMessageNotifications(List<MessageQueue> messageQueues)
        {
            try
            {
                db.MessageQueues.AddRange(messageQueues);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ErrorHandlerForNotification customErrorHandler = new ErrorHandlerForNotification(db);
                customErrorHandler.WriteError(ex, "NotificationHandler AddMessageNotification", null);
                throw;
            }
        }
        //Phase II Changes - 01/30/2021
        //Get Manager User of Operator to Send Notification
        public Task<List<CrmUserBasicDetailModel>> GetOperatorManagerUsers(string tenantId)
        {
            try
            {
                var OperatingUserIds = (from cp in db.CorporateProfile
                                        join cub in db.CrmUserBasicDetail on cp.UserId equals cub.UserId
                                        where cub.AccountType == 0 && cub.IsMaster == true && cp.TenantId == tenantId
                                        select new CrmUserBasicDetailModel { Id = cub.Id, UserId = cp.UserId, Name = cub.Name }).ToList();

                return Task.FromResult(OperatingUserIds);
            }
            catch (Exception ex)
            {
                ErrorHandlerForNotification customErrorHandler = new ErrorHandlerForNotification(db);
                customErrorHandler.WriteError(ex, "NotificationHandler GetOperatorManagerUsers", null);
                throw;
            }           
        }
        public Task<string> GetServiceCompanyUser(string serviceUserId)
        {
            try
            {
                var companyName = (from cp in db.CorporateProfile                                        
                                        where cp.UserId== serviceUserId
                                        select cp.Name);

                return Task.FromResult(companyName.FirstOrDefault());
            }
            catch (Exception ex)
            {
                ErrorHandlerForNotification customErrorHandler = new ErrorHandlerForNotification(db);
                customErrorHandler.WriteError(ex, "NotificationHandler GetServiceCompanyUser", null);
                throw;
            }
        }

        

    }
}
