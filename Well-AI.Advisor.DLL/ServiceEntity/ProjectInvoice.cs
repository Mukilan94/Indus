using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WellAI.Advisor.DLL.ServiceEntity
{
    [Table("ProjectInvoice")]
    public class ProjectInvoice
    {
        [Key]
        public int ID { get; set; }
        public int ProjectID { get; set; }
        [StringLength(100)]
        public string InvoiceNum { get; set; }
        public DateTime InvoiceDate { get; set; }    
        public decimal InvoiceAmount { get; set; }
        [StringLength(450)]
        public string SubmitedyID { get; set; }
        public string PDFPath { get; set; }
        
       
    }
}

		
		
