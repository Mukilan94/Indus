using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WellAI.Advisor.API.Models.Dtos
{
    public class AttachmentDto
    {
        public string uidWell { get; set; }
        public string uidWellbore { get; set; }
        public string uid { get; set; }
        public string nameWell { get; set; }
        public string nameWellbore { get; set; }
        public string name { get; set; }
        public AttachmentObjectReferenceDto objectReference { get; set; }
        public string description { get; set; }
        public string fileType { get; set; }
        public string content { get; set; }
        public AttchmentCommonDataDto commonData { get; set; }
    }

    public class AttachmentObjectReferenceDto
    {
        public string Objects { get; set; }
        public string uidRef { get; set; }
        public string text { get; set; }
    }

    public class AttchmentCommonDataDto
    {
        public int CommonDataId { get; set; }
        public DateTime dTimCreation { get; set; }
        public DateTime dTimLastChange { get; set; }
        public string itemState { get; set; }
    }
}
