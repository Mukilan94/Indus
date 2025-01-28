using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace WellAI.Advisor.Function
{
    public static class QueueSendEmail
    {

        [FunctionName("QueueSendEmail")]
        public static void Run([QueueTrigger("queuenotification", Connection = "AzureWebJobsStorage")]EmailNotification email, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {email}");
            int smtpPort = Int32.Parse(System.Environment.GetEnvironmentVariable("SmtpPort"));
            string smtpHost = System.Environment.GetEnvironmentVariable("SmtpHost");
            string smtpUser = System.Environment.GetEnvironmentVariable("SmtpUser"); // your smtp user
            string smtpPass = System.Environment.GetEnvironmentVariable("SmtpPassword"); // your smtp password
            try
            {
                using (MailMessage mailMsg = new MailMessage())
                {
                    string MessageBody = $"{email.MsgBody}";
                    mailMsg.From = new MailAddress(smtpUser);
                    mailMsg.To.Add(email.ToEmail);
                    mailMsg.Subject = email.MsgSubject.Replace("\r", "").Replace("\n", "");
                    mailMsg.Body = MessageBody;
                    mailMsg.IsBodyHtml = true;
                    if(email.FromEmail!=null && email.FromEmail!="")
                    mailMsg.ReplyToList.Add(new MailAddress(email.FromEmail, "Reply-to")); 
                    using (SmtpClient smtp = new SmtpClient(smtpHost, smtpPort))
                    {
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Credentials = new NetworkCredential(smtpUser, smtpPass);
                        smtp.EnableSsl = true;
                        smtp.Send(mailMsg);
                    }
                }
                log.LogInformation("Email sent");
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
            }
        }
    }
    public class EmailNotification
    {
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string ToEmail { get; set; }
        public string ToName { get; set; }
        public string MsgSubject { get; set; }
        public string MsgBody { get; set; }
        public string MsgFooter { get; set; }
    }
}
