using System.Collections.Generic;

namespace WellAI.Advisor.Model.OperatingCompany.Models
{
    public class WellMetricsModel
    {
        public int TotalWells { get; set; }
        public int ActiveWells { get; set; }
        public int InactiveWells { get; set; }
        public int WellsOnService { get; set; }

        public List<Well> ChartWellData;
        public List<CompanyWellData> CompanyWells;
    }
    public class CompanyWellData
    {
        public string Company { get; set; }
        public int TotalWells { get; set; }
        public int ActiveWells { get; set; }
        public int InactiveWells { get; set; }
        public int WellsOnService { get; set; }
    }

    public class Well
    {
        public double Value { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }

        public double RotarySpeed { get; set; }
        public double PumpRate1 { get; set; }
        public double PumpRate2 { get; set; }
        public double Standpipe { get; set; }
        public double BlockHeight { get; set; }
        public bool Circ { get; set; }
        public bool Drill { get; set; }
        public bool WobRop { get; set; }
        public double HaltOfBootom { get; set; }
        public double BitDepth { get; set; }
        public double HoleDepth { get; set; }
        public double Hookload { get; set; }
        public double WOB { get; set; }
        public double ROP { get; set; }
    }
    public class WellViewModel
    {
        public string WellId { get; set; }
        public string Name { get; set; }
        public string RigId { get; set; }
        public string RigName { get; set;}
    }
    public class RigViewModel
    {
        public string RigId { get; set; }
        public string RigName { get; set; }
        public bool isSelected { get; set; }
        public string CompanyId { get; set; }
    }
}
