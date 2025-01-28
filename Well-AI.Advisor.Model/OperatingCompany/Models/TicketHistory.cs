using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WellAI.Advisor.Model.OperatingCompany.Models
{
    public class TicketHistory
    {
        public int New { get; set; }
        public int Open { get; set; }
        public int Closed { get; set; }
        public int Unapproved { get; set; }
        public int Deleted { get; set; }
    }

    public class TicketHistoryItem
    {
        public string TicketId { get; set; }
        public string Subject { get; set; }
        public string Status { get; set; }
        public int StatusID { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Required")]
        public int? CategoryID { get; set; }
        public string UserID { get; set; }
        public string Department { get; set; }
        public string HelpTopic { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime CloseDate { get; set; }
        public DateTime LastActivity { get; set; }
        public string LastMessage { get; set; }
        public string ReplyMessage { get; set; }
        public string to { get; set; }
        public string LeaveReplay { get; set; }

        public List<Comment> Comment;


    }

    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 100)]
        public string Content { get; set; }

        public string Author { get; set; }
        public DateTime Date
        {
            get;
            set;
        }

        public string TicketId { get; set; }
    }
}
