using System;
using System.ComponentModel.DataAnnotations;

namespace WebAI.Advisor.Areas.OperatingCompany.Models
{
    public class WellData
    {
        [ScaffoldColumn(false)]
        public int Id
        {
            get;
            set;
        }
        public string Rig
        {
            get;
            set;
        }

        public string CementStage { get; set; }
        public string ServiceCompany { get; set; }

        public string BitDepth
        {
            get;
            set;
        }
        public string HoleDepth
        {
            get;
            set;
        }
        public string Hookload
        {
            get;
            set;
        }
        public string WOB
        {
            get;
            set;
        }
        public string ROP
        {
            get;
            set;
        }
        public DateTime Updated
        {
            get;
            set;
        }
    }
}
