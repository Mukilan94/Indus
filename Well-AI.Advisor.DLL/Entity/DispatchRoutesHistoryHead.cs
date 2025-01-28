using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WellAI.Advisor.Model.Tenant.Models;


namespace WellAI.Advisor.DLL.Entity
{
    [Table("DispatchRoutesHistoryHead")]
    public class DispatchRoutesHistoryHead
    {
        [Key]
        public string HistoryId { get; set; }
        public string UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string DispatchNotes { get; set; }
        public string RouteSource { get; set; }



    }
   
}
