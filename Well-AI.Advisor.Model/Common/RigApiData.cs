using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WellAI.Advisor.Model.Common
{
    public class RigApiData
    {
       // public string operator { get; set; }
        public int total_results_count { get; set; }
        public int current_results_count { get; set; }
        public string next_set { get; set; }
        public List<RigResult> results { get; set; }
        public string error { get; set; }
        public string message { get; set; }
       
    }

    public class RigResult
    {
        public string rig_id { get; set; }
        public string rig { get; set; }
        public object nearest_point { get; set; }
        public object latitude { get; set; }
        public string longitude { get; set; }        
        public string @operator { get; set; }
        public string error { get; set; }

    }
}
