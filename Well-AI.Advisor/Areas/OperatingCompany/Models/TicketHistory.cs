using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAI.Advisor.Areas.OperatingCompany.Models
{
    public class TicketHistory
    {
        public List<TicketHistoryItem> Tickets;
        public int Open;
        public int Closed;
        public int Unapproved;
        public int Deleted;
    }

    public class TicketHistoryItem
    {
        public string TicketId
        {
            get;
            set;
        }

        public string Subject { get; set; }
        public string Status { get; set; }
        public string Department { get; set; }
        public string HelpTopic { get; set; }

        public DateTime? CreateDate
        {
            get;
            set;
        }
        public DateTime? CloseDate
        {
            get;
            set;
        }
        public DateTime LastActivity
        {
            get;
            set;
        }
        public string LastMessage { get; set; }
        public string ReplyMessage { get; set; }

        public List<Comment> Comments;
    }

    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 100)]
        public string Content { get; set; }

        public string AuthorId { get; set; }

        public string Author { get; set; }
        public DateTime Date
        {
            get;
            set;
        }

        public string TicketId { get; set; }
    }
}
