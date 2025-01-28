using System;
using System.Collections.Generic;
using System.Text;

namespace Well_AI.Advisor.API.Samsara.Models
{

   

    public class VechicleFollowFeedLocationLocation
    {
        public DateTime time { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double heading { get; set; }
        public double speed { get; set; }
        public ReverseGeo reverseGeo { get; set; }
    }

    public class VechicleFollowFeedLocationDatum
    {
        public string id { get; set; }
        public string name { get; set; }
        public IList<VechicleFollowFeedLocationLocation> locations { get; set; }
    }

    
    public class VechicleFollowFeedLocationModel
    {
        public IList<VechicleFollowFeedLocationDatum> data { get; set; }
        public Pagination pagination { get; set; }
    }
    
}
