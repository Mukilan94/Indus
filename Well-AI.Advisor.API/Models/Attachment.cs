using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WellAI.Advisor.API.Models
{
    public class Attachment
    {
        [Key]
        public string uid { get; set; }
        public string uidWell { get; set; }
        public string uidWellbore { get; set; }
        
        public string nameWell { get; set; }
        public string nameWellbore { get; set; }
        public string name { get; set; }
        public AttachmentObjectReference objectReference { get; set; }
        public string description { get; set; }
        public string fileType { get; set; }
        public string content { get; set; }
        public AttchmentCommonData commonData { get; set; }
    }

    public class AttachmentObjectReference
    {
        public string Objects { get; set; }
        [Key]
        public string uidRef { get; set; }
        public string text { get; set; }
    }

    public class AttchmentCommonData
    {
        [Key]
        public int CommonDataId { get; set; }
        public DateTime dTimCreation { get; set; }
        public DateTime dTimLastChange { get; set; }
        public string itemState { get; set; }
    }
}
