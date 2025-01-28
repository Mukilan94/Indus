using Azure.Storage.Queues;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Well_AI.Advisor.Log.Error;
using WellAI.Advisor.Function.Notification;
using WellAI.Advisor.Model.Common;

namespace WellAI.Advisor.Helper
{
    public class EmailHandler
    {
        public static IConfiguration Configuration { get; set; }
        public async Task<bool> SendEmailAsync(string name, String email, String subject, String messages)
        {
           
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
                Configuration = builder.Build();
                string baseServer = Configuration.GetValue<string>("Smtp:Server");
                string baseUser = Configuration.GetValue<string>("Smtp:User");
                string basePass = Configuration.GetValue<string>("Smtp:Pass");
                string basePort = Configuration.GetValue<string>("Smtp:Port");

                string to = email; //To address    
                //string baseUrlApi = Configuration.GetValue<string>("_Url:apiUrl");
                string from = baseUser; //From address    
                MailMessage message = new MailMessage(from, to);
                string mailbody = messages;
                message.Subject = subject;
                message.Body = mailbody;
                message.IsBodyHtml = true;
                SmtpClient client = new SmtpClient(baseServer, int.Parse(basePort));
                System.Net.NetworkCredential basicCredential = new System.Net.NetworkCredential(baseUser, basePass);
                client.EnableSsl = true;
                client.UseDefaultCredentials = true;
                client.Credentials = basicCredential;
                client.Send(message);
                return await Task.FromResult(true);
        }
        /// <summary>
        /// Pass null in from email if you don't now the from email.
        /// </summary>
        /// <param name="messageToQueue"></param>
        public void SendMessageToQueue(MessageToQueue messageToQueue)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string connectionString = Configuration.GetValue<string>("QueueStorage:QueueStorageCon"); 
            string queueName = Configuration.GetValue<string>("QueueStorage:QueueName");
            QueueClient queueClient = new QueueClient(connectionString, queueName.ToLower());

            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(messageToQueue);
            Encoding encoding= Encoding.UTF8;
            var bytes = encoding.GetBytes(jsonString);
            string message= Convert.ToBase64String(bytes);
            var context = new WellAINotificationHandlerContext();
            try
            {
                queueClient.CreateIfNotExistsAsync();
                queueClient.SendMessage(message);
            }
            catch (Exception ex)
            {
                ErrorHandlerForNotification customErrorHandler = new ErrorHandlerForNotification(context);
                customErrorHandler.WriteError(ex, "EmailHandler SendMessageToQueue", null);
                throw;
            }
        }

    }
}
