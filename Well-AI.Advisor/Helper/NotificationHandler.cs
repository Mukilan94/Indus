using System;
using System.Collections.Generic;
using System.Linq;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.DLL.Entity;

namespace WellAI.Advisor.Helper
{
    public class NotificationHandler
    {
        private readonly WebAIAdvisorContext db;

        public NotificationHandler(WebAIAdvisorContext dbContext)
        {
            db = dbContext;
        }

        /// <summary>
        /// Save notifications
        /// </summary>
        /// <param name="messageQueue"></param>
        public void AddNotification(MessageQueue messageQueue)
        {
            messageQueue.IsActive = 1;
            messageQueue.CreatedDate = DateTime.Now;
            db.MessageQueues.Add(messageQueue);
            db.SaveChanges();
        }

        /// <summary>
        /// Get all notifications by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<MessageQueue> GetNotifications(string userId)
        {
            List<MessageQueue> messageQueues = db.MessageQueues.Where(x => x.To_id == userId && x.IsActive == 1).ToList();
            return messageQueues;
        }
    }
}
