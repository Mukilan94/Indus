using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.Function.Notification
{
    [Table("PredictionLog")]
    public class PredictionLog
    {
        [Key]
        public string WellId { get; set; }
        public string ScoredLabels { get; set; }
        public DateTime? ScoreDateTime { get; set; }
    }
}