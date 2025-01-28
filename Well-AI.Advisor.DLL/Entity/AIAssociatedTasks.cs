using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WellAI.Advisor.Model.Tenant.Models;

namespace WellAI.Advisor.DLL.Entity
{
    [Table("AIAssociatedTasks")]
    public class AIAssociatedTasks
    {
        [Key]
        public string customer_id { get; set; }
        public string well_id { get; set; }
        public string welltype_id { get; set; }
        public string welltask_id { get; set; }
        public double day { get; set; }
        public double depth { get; set; }
        public string time { get; set; }
        public double duration { get; set; }
        public double leadtime { get; set; }
        public double dependency_flag { get; set; }
        public string dependency { get; set; }
        public string taskname { get; set; }
        public string ActionDate { get; set; }
        public double taskstatus { get; set; }
        public DateTime ADT { get; set; }
        public DateTime ScheduleDate { get; set; }
        public DateTime StartTime { get; set; }
        public double Eflag { get; set; }
    }


    [Table("AIPredictiveTasks")]
    public class AIPredictiveTasks
    {
        //[Key]
        public string customer_id { get; set; }
        public string well_id { get; set; }
        public string welltype_id { get; set; }
        public string welltask_id { get; set; }
        public double day { get; set; }
        public double depth { get; set; }
        public string time { get; set; }
        public double duration { get; set; }
        public double leadtime { get; set; }
        public double dependency_flag { get; set; }
        public string dependency { get; set; }
        public string taskname { get; set; }
        [Key]
        public string ActionDate { get; set; }
        public double taskstatus { get; set; }
        public DateTime ADT { get; set; }
        public DateTime ScheduleDate { get; set; }
        public DateTime StartTime { get; set; }
        public double Eflag { get; set; }
    }

    [Table("AIExemptionTasks")]
    public class AIExemptionTasks
    {
        [Key]
        public string customer_id { get; set; }
        public string well_id { get; set; }
        public string welltype_id { get; set; }
        public string welltask_id { get; set; }
        public double day { get; set; }
        public double depth { get; set; }
        public string time { get; set; }
        public double duration { get; set; }
        public double leadtime { get; set; }
        public double dependency_flag { get; set; }
        public string dependency { get; set; }
        public string taskname { get; set; }
        public string ActionDate { get; set; }
        public double taskstatus { get; set; }
        public DateTime ADT { get; set; }
        public DateTime ScheduleDate { get; set; }
        public DateTime StartTime { get; set; }
        public double Eflag { get; set; }
    }
}
