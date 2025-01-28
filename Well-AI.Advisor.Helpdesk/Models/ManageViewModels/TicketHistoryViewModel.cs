using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Well_AI.Helpdesk.Models.ManageViewModels
{
    public class TicketHistoryViewModel
    {
        public TicketHistoryViewModel()
        {
            this.replyDate = DateTime.Now;
        }
        public string transactionId { get; set; }
        public int issueId { get; set; }

        public string userName { get; set; }
        public string userId { get; set; }
        public string replyBy { get; set; }

        public string ticketTitle { get; set; }
        public string ticketDescription { get; set; }

        public DateTime replyDate { get; set; }

        public string replyMessage { get; set; }

        public string ticketStatus { get; set; } 
        public string closedBy { get; set; }
        public string custName { get; set; }
    }
}