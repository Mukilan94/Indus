
namespace WellAI.Advisor.Model.OperatingCompany.Models
{
    public class InDepthRigData
    {
        public string WellId {get;set;}
        public double Value { get; set; }
        public int Day { get; set; }
        public int Series { get; set; }
        public string Color { get; set; }

        public float min { get; set; }
        public float max { get; set; }

        public int Series1 { get; set; }
        public int Series2 { get; set; }

    }

    public class InDepthRigDataGridModel
    {
        public string Name { get; set; }
        public float Depth { get; set; }
        public int Day { get; set; }
        public string RFP { get; set; }
        public string TaskId { get; set; }
        public string WellId { get; set; }
        public string RigId { get; set; }
        public string RigName { get; set; }
        public bool IsBiddable { get; set; }
        public string StageType { get; set;}
    }

    public class InDepthRigDataModel
    {
        public string OperatorName { get; set; }
        public string InitWellId { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string State { get; set; }
        public UserViewModel User { get; set; }
        public bool MSAExist { get; set; }
        public string PrimaryContact { get; set; }
        public AddAuctionProposalViewModel AddAuction { get; set; }
        public string SRVTenantId { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string DrillPlaName { get; set; }

    }

    public class InDepthRigDataGridProposal
    {
        public string Name { get; set; }
        public string TenantId { get; set; }
    }

    public class InDepthRigDataGridCurrent
    {
        public string Name { get; set; }
        public string TenantId { get; set; }
    }

    public class RigsModel
    {
        public string WellId { get; set; }
        public string Category { get; set; }
    }

    public class DrillPlanModel
    {
        public string DrillPlanId { get; set; }
        public string DrillPlanName { get; set; }
    }

}
