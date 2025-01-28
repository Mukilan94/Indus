using System;
using System.Collections.Generic;
using System.Text;

namespace Well_AI.Advisor.API.Samsara.Models
{
    public class Tag
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class ExternalIds
    {
        public string serial { get; set; }
        public string vin { get; set; }
    }

    public class StaticAssignedDriver
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Datum
    {
        public string id { get; set; }
        public string name { get; set; }
        public string vin { get; set; }
        public string serial { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public string year { get; set; }
        public string harshAccelerationSettingType { get; set; }
        public string notes { get; set; }
        public string licensePlate { get; set; }
        public IList<Tag> tags { get; set; }
        public ExternalIds externalIds { get; set; }
        public StaticAssignedDriver staticAssignedDriver { get; set; }
    }
 

    public class VechicleModel
    {
        public IList<Datum> data { get; set; }
        public Pagination pagination { get; set; }
    }

    public class VechicleResponse
    {
        public List<VechicleModel> data { get; set; }
        
    }

    public class VechicleObjectResponse
    {
        public VechicleModel data { get; set; }

    }
}
