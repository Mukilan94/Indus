using System;
using System.Collections.Generic;
using System.Text;

namespace Well_AI.Advisor.API.Samsara.Models
{


    public class BatteryMilliVolt
    {
        public DateTime time { get; set; }
        public int value { get; set; }
    }

    public class BatteryMilliVoltStatusDatum
    {
        public string id { get; set; }
        public string name { get; set; }
        public IList<BatteryMilliVolt> batteryMilliVolts { get; set; }
    }

   

    public class BatteryMilliVoltStatusModel
    {
        public IList<BatteryMilliVoltStatusDatum> data { get; set; }
        public Pagination pagination { get; set; }
    }
}
