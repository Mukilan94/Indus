using System;
using System.Collections.Generic;
using System.Text;

namespace Well_AI.Advisor.API.Samsara.Models
{
    

    public class Data
    {
        public string auxInputType1 { get; set; }
        public string auxInputType2 { get; set; }
        public ExternalIds externalIds { get; set; }
        public string harshAccelerationSettingType { get; set; }
        public string id { get; set; }
        public string licensePlate { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public string name { get; set; }
        public string notes { get; set; }
        public string serial { get; set; }
        public StaticAssignedDriver staticAssignedDriver { get; set; }
        public IList<Tag> tags { get; set; }
        public string vin { get; set; }
        public string year { get; set; }
    }

    public class VechicleUpdateModel
    {
        public Data data { get; set; }
    }

    class VechicleUpdateResponse
    {
        public VechicleUpdateModel Response { get; set; }
    }
}
