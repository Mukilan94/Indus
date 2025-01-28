using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WellAI.Advisor.Model.Common;
using WellAI.Advisor.DLL.Entity;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.DLL.ServiceEntity;
using WellAI.Advisor.Model.ServiceCompany.Models;
using AuctionBidAmountHistory = WellAI.Advisor.DLL.Entity.AuctionBidAmountHistory;

namespace WellAI.Advisor.DLL.Data
{
    public class WebAIAdvisorContext : IdentityDbContext<WellIdentityUser>
    {
        public WebAIAdvisorContext(DbContextOptions<WebAIAdvisorContext> options)
            : base(options)
        {
        }
        public DbSet<WellIdentityUser> WellIdentityUser { get; set; }
        public DbSet<ServiceOffer> ServiceOffers { get; set; }
        public DbSet<OperatingOffer> OperatingOffers { get; set; }
        public DbSet<ServiceCompanyOffering> ServiceCompanyOfferings { get; set; }
        public DbSet<OperatingCompanyOffering> OperatingCompanyOfferings { get; set; }
        public DbSet<ProviderMSALink> ProviderMSALinks { get; set; }
        public DbSet<ProviderInsuranceLink> ProviderInsuranceLinks { get; set; }
        public DbSet<TenantRoles> TenantRoles { get; set; }
        public DbSet<TenantUsers> TenantUsers { get; set; }
        public DbSet<RolePermissions> RolePermissions { get; set; }
        public DbSet<RolePermissionLinks> RolePermissionLinks { get; set; }
        public DbSet<Components> Components { get; set; }
        public DbSet<RolePermissionComponentLinks> RolePermissionComponentLinks { get; set; }
        public DbSet<CrmUserBasicDetail> CrmUserBasicDetail { get; set; }
        public DbSet<CrmCompanies> CrmCompanies { get; set; }
        public DbSet<CrmSharedDocuments> CrmSharedDocuments { get; set; }
        public DbSet<CrmSubscriptions> CrmSubscriptions { get; set; }
        public DbSet<CrmPaymentMethods> CrmPaymentMethods { get; set; }
        public DbSet<ErrorLog> ErrorLog { get; set; }
        public DbSet<WellTenantInfo> WellTenants { get; set; }
        public DbSet<USAState> USAStates { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<WellFile> WellFiles { get; set; }
        public DbSet<WellFileFolder> WellFileFolders { get; set; }
        public DbSet<WellConfig> WellConfig { get; set; }
        public DbSet<CorporateProfile> CorporateProfile { get; set; }
        public DbSet<CorporateProfileHistory> CorporateProfileHistory { get; set; }
        public DbSet<BillingHistory> BillingHistoryInvoices { get; set; }
        public DbSet<hdIssues> hdIssues { get; set; }
        public DbSet<hdCategories> hdCategories { get; set; }
        public DbSet<hdStatus> hdStatus { get; set; }
        public DbSet<hdComments> hdComments { get; set; }
        public DbSet<ServiceBillingHistory> ServiceBillingHistories { get; set; }
        public DbSet<ProductSubscriptionModel> Subscription { get; set; }
        public virtual DbSet<AuctionProposal> AuctionProposals { get; set; }
        public virtual DbSet<AuctionBidStatus> AuctionBidStatuses { get; set; }
        public virtual DbSet<AuctionBids> AuctionBids { get; set; }
        public virtual DbSet<AuctionProject> AuctionProjects { get; set; }
        public DbSet<WELL_REGISTER> WELL_REGISTERs { get; set; }
        public virtual DbSet<RigRegister> RigRegisters { get; set; }
        public virtual DbSet<AuctionBidAmountHistory> AuctionBidAmountHistories { get; set; }
        public DbSet<AIAssociatedTasks> AIAssociatedTasks { get; set; }
        public DbSet<AIPredictiveTasks> AIPredictiveTasks { get; set; }
        public DbSet<AIExemptionTasks> AIExemptionTasks { get; set; }
        public DbSet<WellTask> WellTask { get; set; }
        public DbSet<WellRegister> WellRegister { get; set; }
        public DbSet<UserWell> UsersWells { get; set; }
        public DbSet<WellType> WellType { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<ConfigurationSetting> ConfigurationSettings { get; set; }

        public DbSet<ProjectNote> ProjectNotes { get; set; }
        public DbSet<ProjectAttachment> ProjectAttachments { get; set; }
        public DbSet<ServiceVehicle> ServiceVehicles { get; set; }
        public DbSet<ProjectInvoice> ProjectInvoices { get; set; }
        public DbSet<ProjectTechnician> ProjectTechnicians { get; set; }
        public DbSet<AuctionProposalAttachments> AuctionProposalAttachments { get; set; }
        public DbSet<AuctionProposalOperatorAttachments> AuctionProposalOperatorAttachments { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }

        public DbSet<TaskTable> TaskTable { get; set; }
        public DbSet<TaskStatus> TaskStatus { get; set; }

        public DbSet<ServiceTypeHead> ServiceTypeHead { get; set; }
        public DbSet<ServiceTypeDetail> ServiceTypeDetail { get; set; }

        public DbSet<TwilioChatUserMappings> TwilioChatUserMappings { get; set; }
        public DbSet<BatchDillingType_Register> BatchDillingType_Register { get; set; }
        public DbSet<BasinType> BasinTypes { get; set; }
        public DbSet<Pad_register> pad_register { get; set; }
        public DbSet<Rig_register> rig_register { get; set; }
        public DbSet<Audit> AuditLogs { get; set; }
        public DbSet<WellTasks> WellTasks { get; set; }
        public DbSet<Supplies> Supplies { get; set; }
        public DbSet<CategoryTask> CategoryTasks { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<ServiceCategory> serviceCategories { get; set; }
        public DbSet<WellCheckList> WellCheckList { get; set; }
        public DbSet<MessageQueue> MessageQueues { get; set; }
        public DbSet<UserActivityStatus> UserActivityStatuses { get; set; }
        public DbSet<SubscriptionPackage> SubscriptionPackage { get; set; }
        public DbSet<PaymentTypeEntity> PaymentType { get; set; }
        public DbSet<WorkstationRegister> WorkstationRegister { get; set; }

        public DbSet<WellDepthDataStage> WellDepthDataStages { get; set; }
        public DbSet<AIRawData> AIRawDatas { get; set; }
        public DbSet<TenantConfiguration> TenantConfigurations { get; set; }
        public DbSet<UserRig> UserRigs { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<staffuser> staffusers { get; set; }
        public DbSet<Entity.ServiceStage> ServiceStages { get; set; }
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //}
        public DbSet<RigsDepth_Permission> RigsDepth_Permissions { get; set; }

        public DbSet<UserSessions> UserSessions { get; set; }
        public DbSet<StaffUserSessions> StaffUserSessions { get; set; }
        public DbSet<DrillingPlan> DrillingPlan { get; set; }

        //DWOP
        public DbSet<CheckListTemplate> ChecklistTemplate { get; set; }
        public DbSet<DrillPlanWells> DrillPlanWells { get; set; }
        public DbSet<DrillPlanHeader> DrillPlanHeader { get; set; }
        public DbSet<DrillPlanDetails> DrillPlanDetails { get; set; }

        //Dispatch
        public DbSet<DispatchRoutes> DispatchRoutes { get; set; }

        //Dispatch History
        public DbSet<DispatchRoutesHistoryHead> DispatchRoutesHistoryHead { get; set; }
        public DbSet<DispatchRoutesHistoryDetails> DispatchRoutesHistoryDetails { get; set; }

        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<CreditCardModel> CreditcardType { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PaymentTypeEntity>().HasData(new PaymentTypeEntity { ID = System.Guid.NewGuid().ToString("D"), Name = "Credit card" });
            modelBuilder.Entity<PaymentTypeEntity>().HasData(new PaymentTypeEntity { ID = System.Guid.NewGuid().ToString("D"), Name = "Debit card" });
            //modelBuilder.Entity<PaymentTypeModel>().HasData(new PaymentTypeModel { ID = System.Guid.NewGuid().ToString("D"), Name = "Checks" });

            //Payment methods
            //modelBuilder.Entity<PaymentMethod>(entity =>
            //{
            //    entity.ToTable("PaymentMethod");

            //    entity.Property(e => e.Number)
            //        .IsRequired()
            //        .HasMaxLength(100)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ExpireMonth)
            //        .IsRequired()
            //        .HasMaxLength(100)
            //        .IsUnicode(false);

            //    entity.Property(e => e.ExpireYear)
            //        .IsRequired()
            //        .HasMaxLength(100)
            //        .IsUnicode(false);
            //});
        }
    }
}