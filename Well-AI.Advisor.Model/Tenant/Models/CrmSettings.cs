using System;
using Finbuckle.MultiTenant;
using System.Collections.Generic;

namespace WellAI.Advisor.Model.Tenant.Models
{
    
	public partial class CrmSettings
    {
        public int InstanceId { get; set; }
        public bool? AllowUserRegistration { get; set; }
        public string Lang { get; set; }
        public int MaxAttachSize { get; set; }
        public string Smtpserver { get; set; }
        public bool SmtpauthRequired { get; set; }
        public string SmtpuserName { get; set; }
        public string Smtppassword { get; set; }
        public bool SmtpuseSsl { get; set; }
        public string FromAddress { get; set; }
        public string ReplyToAddress { get; set; }
        public int Smtpport { get; set; }
        public string HeaderBgcolor { get; set; }
        public string MenuBarBgcolor { get; set; }
        public string MenuBarTabColor { get; set; }
        public bool UseDefaultSmtp { get; set; }
        public byte[] LogoImage { get; set; }
        public int ServerTimeOffset { get; set; }
        public bool MailCheckerEnabled { get; set; }
        public int MailCheckerInterval { get; set; }
        public string MailCheckerPopserver { get; set; }
        public int MailCheckerPopport { get; set; }
        public string MailCheckerPoplogin { get; set; }
        public string MailCheckerPoppassword { get; set; }
        public bool MailCheckerUseSsl { get; set; }
        public bool MailCheckerIsImap { get; set; }
        public bool ShowTagListAsMenu { get; set; }
        public string AutoLoginSharedSecret { get; set; }
        public string Title { get; set; }
        public string HdrootUrl { get; set; }
        public string HdsharedKey { get; set; }
        public bool ShowEntitiesToSubscribersOnly { get; set; }
        public bool MailCheckerAllowUnregisteredContacts { get; set; }
    }
}
