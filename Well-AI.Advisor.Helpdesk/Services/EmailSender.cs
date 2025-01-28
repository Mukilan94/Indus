using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Services
{
    
    public class EmailSender : IEmailSender
    {
        //dependency injection
        public SendGridOptions _sendGridOptions { get; }
        public IDotnetdesk _dotnetdesk { get; }
        public SmtpOptions _smtpOptions { get; }
        public EmailSender(IOptions<SendGridOptions> sendGridOptions,
                IDotnetdesk dotnetdesk,
                IOptions<SmtpOptions> smtpOptions)
        {
            _sendGridOptions = sendGridOptions.Value;
            _dotnetdesk = dotnetdesk;
            _smtpOptions = smtpOptions.Value;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            //send email using sendgrid via dotnetdesk
            _dotnetdesk.SendEmailBySendGridAsync(_sendGridOptions.SendGridKey, 
                _sendGridOptions.FromEmail, 
                _sendGridOptions.FromFullName, 
                subject, 
                message, 
                email).Wait();

      
            return Task.CompletedTask;
        }
    }
}
