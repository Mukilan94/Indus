using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WellAI.Advisor.Function.BidExpirationScheduler.Models
{
   
        [Table("MessageQueue")]
        public class MessageQueue
        {
            [Key]
            public int Messagequeue_id { get; set; }
            public string From_id { get; set; }
            public string To_id { get; set; }
            public int Type { get; set; }
            public string EntityId { get; set; }
            public string OperatorId { get; set; }
            public string RigId { get; set; }
            public string TaskName { get; set; }
            public string PersonalName { get; set; }
            public string JobName { get; set; }
            public int IsActive { get; set; }
            public DateTime CreatedDate { get; set; }
        }

    }

