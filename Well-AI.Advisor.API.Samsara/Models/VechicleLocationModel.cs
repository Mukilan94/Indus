using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Well_AI.Advisor.API.Samsara.Models
{

    public class ReverseGeo
    {
        public string formattedLocation { get; set; }
    }

    public class VechicleLocation
    {
        public DateTime time { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double heading { get; set; }
        public double speed { get; set; }
        public ReverseGeo reverseGeo { get; set; }
    }

    public class VechicleLocatiomDatum
    {
        public string id { get; set; }
        public string name { get; set; }


        public VechicleLocation location { get; set; }
    }

    public class Pagination
    {
        public string endCursor { get; set; }
        public bool hasNextPage { get; set; }
    }



    public class VechicleLocationModel
    {
        public IList<VechicleLocatiomDatum> data { get; set; }
        public Pagination pagination { get; set; }
    }
 

}
