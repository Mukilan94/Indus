using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;
using WellAI.Advisor.Model.Tenant.Models;
using WellAI.Advisor.Model.Common;
using WellAI.Advisor.Model.ServiceCompany.Models;

namespace WellAI.Advisor
{
    public class TenantServiceDbContext : MultiTenantDbContext
    {
        public TenantServiceDbContext(TenantInfo tenantInfo) : base(tenantInfo)
        {
        }

        public TenantServiceDbContext(TenantInfo tenantInfo, DbContextOptions<TenantServiceDbContext> options) : base(tenantInfo, options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //Phase II Changes - 03/29/2021
                optionsBuilder.UseSqlServer(base.TenantInfo.ConnectionString, options => options.EnableRetryOnFailure());
            }

            base.OnConfiguring(optionsBuilder);
        }

        #region dbsets
        //public DbSet<PaymentMethod> ServicePaymentMethods { get; set; }
        public DbSet<ServiceBillingHistory> ServiceBillingHistories { get; set; }
        //public DbSet<PaymentTypeModel> ServicePaymentTypes { get; set; }
        //public DbSet<CreditCardTypeModel> CreditcardType { get; set; }
        public DbSet<OperatingDirectoryApproval> OperatingDirectoryAppovals { get; set; }
        public DbSet<OperatingDirectoryStatus> OperatingDirectoryStatuses { get; set; }
        public DbSet<OperatingDirectoryPEC> OperatingDirectoryPECs { get; set; }
        public DbSet<OperatingDirectory> OperatingDirectory { get; set; }
        public DbSet<SubscriptionOperator> SubscriptionOperators { get; set; }
        public DbSet<SubscriptionOperatorRig> subscriptionOperatorRigs { get; set; }
        public virtual DbSet<CrmAccountExecutive> CrmAccountExecutive { get; set; }
        public virtual DbSet<CrmActivity> CrmActivity { get; set; }
        public virtual DbSet<CrmChannel> CrmChannel { get; set; }
        public virtual DbSet<CrmComments> CrmComments { get; set; }
        public virtual DbSet<CrmCompanies> CrmCompanies { get; set; }
        public virtual DbSet<CrmCompanyImages> CrmCompanyImages { get; set; }
        public virtual DbSet<CrmCompanySubscribers> CrmCompanySubscribers { get; set; }
        public virtual DbSet<CrmCompanyTags> CrmCompanyTags { get; set; }
        public virtual DbSet<CrmCompanyViewHistory> CrmCompanyViewHistory { get; set; }
        public virtual DbSet<CrmContactImages> CrmContactImages { get; set; }
        public virtual DbSet<CrmContactSubscribers> CrmContactSubscribers { get; set; }
        public virtual DbSet<CrmContactTags> CrmContactTags { get; set; }
        public virtual DbSet<CrmContactViewHistory> CrmContactViewHistory { get; set; }
        public virtual DbSet<CrmContacts> CrmContacts { get; set; }
        public virtual DbSet<CrmCustomFieldCompanyValues> CrmCustomFieldCompanyValues { get; set; }
        public virtual DbSet<CrmCustomFieldContactValues> CrmCustomFieldContactValues { get; set; }
        public virtual DbSet<CrmCustomFieldOptions> CrmCustomFieldOptions { get; set; }
        public virtual DbSet<CrmCustomFieldProjectValues> CrmCustomFieldProjectValues { get; set; }
        public virtual DbSet<CrmCustomFields> CrmCustomFields { get; set; }
        public virtual DbSet<CrmDepartments> CrmDepartments { get; set; }
        public virtual DbSet<CrmFileAttachments> CrmFileAttachments { get; set; }
        public virtual DbSet<CrmLead> CrmLead { get; set; }
        public virtual DbSet<CrmLeadLine> CrmLeadLine { get; set; }
        public virtual DbSet<CrmOpportunity> CrmOpportunity { get; set; }
        public virtual DbSet<CrmOpportunityLine> CrmOpportunityLine { get; set; }
        public virtual DbSet<CrmPaymentMethods> CrmPaymentMethods { get; set; }
        public virtual DbSet<CrmProjectCompanies> CrmProjectCompanies { get; set; }
        public virtual DbSet<CrmProjectContacts> CrmProjectContacts { get; set; }
        public virtual DbSet<CrmProjectSubscribers> CrmProjectSubscribers { get; set; }
        public virtual DbSet<CrmProjectTags> CrmProjectTags { get; set; }
        public virtual DbSet<CrmProjectViewHistory> CrmProjectViewHistory { get; set; }
        public virtual DbSet<CrmProjects> CrmProjects { get; set; }
        public virtual DbSet<CrmPurchaseOrderDetail> CrmPurchaseOrderDetail { get; set; }
        public virtual DbSet<CrmPurchaseOrderHeader> CrmPurchaseOrderHeader { get; set; }
        public virtual DbSet<CrmRating> CrmRating { get; set; }
        public virtual DbSet<CrmSettings> CrmSettings { get; set; }
        public virtual DbSet<CrmSharedDocuments> CrmSharedDocuments { get; set; }
        public virtual DbSet<CrmSubscriptions> CrmSubscriptions { get; set; }
        public virtual DbSet<CrmTags> CrmTags { get; set; }
        public virtual DbSet<CrmTaskComments> CrmTaskComments { get; set; }
        public virtual DbSet<CrmTasks> CrmTasks { get; set; }
        public virtual DbSet<CrmUserBasicDetail> CrmUserBasicDetail { get; set; }
        public virtual DbSet<CrmUserGridColumns> CrmUserGridColumns { get; set; }
        public virtual DbSet<CrmUsers> CrmUsers { get; set; }
        public virtual DbSet<ClientContact> ClientContacts { get; set; }
        public virtual DbSet<ErdosDrillingDepthBased> ErdosDrillingDepthBased { get; set; }
        public virtual DbSet<ErdosGeneralTimeBased> ErdosGeneralTimeBased { get; set; }
        public virtual DbSet<TaskSchedule> TasksSchedule { get; set; }

        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CrmAccountExecutive>(entity =>
            {
                entity.HasKey(e => e.AccountExecutiveId)
                    .HasName("PK_AccountExecutive");

                entity.ToTable("crmAccountExecutive");

                entity.Property(e => e.AccountExecutiveId)
                    .HasColumnName("accountExecutiveId")
                    .HasMaxLength(38);

                entity.Property(e => e.AccountExecutiveName)
                    .IsRequired()
                    .HasColumnName("accountExecutiveName")
                    .HasMaxLength(50);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(30);

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(30);

                entity.Property(e => e.CreatedAt).HasColumnName("createdAt");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(50);

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(50);

                entity.Property(e => e.Province)
                    .HasColumnName("province")
                    .HasMaxLength(30);

                entity.Property(e => e.Street1)
                    .IsRequired()
                    .HasColumnName("street1")
                    .HasMaxLength(50);

                entity.Property(e => e.Street2)
                    .HasColumnName("street2")
                    .HasMaxLength(50);

                entity.Property(e => e.SystemUserId)
                    .HasColumnName("systemUserId")
                    .HasMaxLength(38);
            });

            modelBuilder.Entity<CrmActivity>(entity =>
            {
                entity.HasKey(e => e.ActivityId)
                    .HasName("PK_Activity");

                entity.ToTable("crmActivity");

                entity.Property(e => e.ActivityId)
                    .HasColumnName("activityId")
                    .HasMaxLength(38);

                entity.Property(e => e.ActivityName)
                    .IsRequired()
                    .HasColumnName("activityName")
                    .HasMaxLength(50);

                entity.Property(e => e.ColorHex)
                    .HasColumnName("colorHex")
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedAt).HasColumnName("createdAt");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CrmChannel>(entity =>
            {
                entity.HasKey(e => e.ChannelId)
                    .HasName("PK_Channel");

                entity.ToTable("crmChannel");

                entity.Property(e => e.ChannelId)
                    .HasColumnName("channelId")
                    .HasMaxLength(38);

                entity.Property(e => e.ChannelName)
                    .IsRequired()
                    .HasColumnName("channelName")
                    .HasMaxLength(50);

                entity.Property(e => e.ColorHex)
                    .HasColumnName("colorHex")
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedAt).HasColumnName("createdAt");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CrmComments>(entity =>
            {
                entity.HasKey(e => e.CommentId)
                    .HasName("PK__crmComme__C3B4DFAAB4A032B7");

                entity.ToTable("crmComments");

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.Body).IsRequired();

                entity.Property(e => e.CommentDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CommentType).HasDefaultValueSql("((1))");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.InstanceId).HasColumnName("InstanceID");

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<CrmCompanies>(entity =>
            {
                entity.HasKey(e => e.CompanyId)
                    .HasName("PK__crmCompa__2D971C4CF739EDBA");

                entity.ToTable("crmCompanies");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.Address1).HasMaxLength(1000);

                entity.Property(e => e.Address2).HasMaxLength(1000);

                entity.Property(e => e.AssignedToUserId).HasColumnName("AssignedToUserID");

                entity.Property(e => e.Category)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.City).HasMaxLength(200);

                entity.Property(e => e.Country).HasMaxLength(200);

                entity.Property(e => e.Ein)
                    .HasColumnName("EIN")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.InstanceId).HasColumnName("InstanceID");

                entity.Property(e => e.LatestCommentId).HasColumnName("LatestCommentID");

                entity.Property(e => e.MobilePhone)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Phone)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode).HasMaxLength(20);

                entity.Property(e => e.StateRegion).HasMaxLength(200);

                entity.Property(e => e.UserId)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Website)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CrmCompanyImages>(entity =>
            {
                entity.HasKey(e => e.CompanyId)
                    .HasName("PK__crmCompa__2D971C4C4ECD2085");

                entity.ToTable("crmCompanyImages");

                entity.Property(e => e.CompanyId)
                    .HasColumnName("CompanyID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ImageFileData)
                    .IsRequired()
                    .HasColumnType("image");

                entity.Property(e => e.ImageFileName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<CrmCompanySubscribers>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.UserId });

                entity.ToTable("crmCompanySubscribers");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<CrmCompanyTags>(entity =>
            {
                entity.HasKey(e => new { e.TagId, e.CompanyId });

                entity.ToTable("crmCompanyTags");

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            });

            modelBuilder.Entity<CrmCompanyViewHistory>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.CompanyId });

                entity.ToTable("crmCompanyViewHistory");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.ViewDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<CrmContactImages>(entity =>
            {
                entity.HasKey(e => e.ContactId)
                    .HasName("PK__crmConta__5C6625BB48131C13");

                entity.ToTable("crmContactImages");

                entity.Property(e => e.ContactId)
                    .HasColumnName("ContactID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ImageFileData)
                    .IsRequired()
                    .HasColumnType("image");

                entity.Property(e => e.ImageFileName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<CrmContactSubscribers>(entity =>
            {
                entity.HasKey(e => new { e.ContactId, e.UserId });

                entity.ToTable("crmContactSubscribers");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<CrmContactTags>(entity =>
            {
                entity.HasKey(e => new { e.TagId, e.ContactId });

                entity.ToTable("crmContactTags");

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");
            });

            modelBuilder.Entity<CrmContactViewHistory>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ContactId });

                entity.ToTable("crmContactViewHistory");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.ViewDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<CrmContacts>(entity =>
            {
                entity.HasKey(e => e.ContactId)
                    .HasName("PK__crmConta__5C6625BB6A47C1E4");

                entity.ToTable("crmContacts");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.Address1).HasMaxLength(1000);

                entity.Property(e => e.Address2).HasMaxLength(1000);

                entity.Property(e => e.AssignedToUserId).HasColumnName("AssignedToUserID");

                entity.Property(e => e.City).HasMaxLength(200);

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.Country).HasMaxLength(200);

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.InstanceId).HasColumnName("InstanceID");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.LatestCommentId).HasColumnName("LatestCommentID");

                entity.Property(e => e.MobilePhone)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode).HasMaxLength(20);

                entity.Property(e => e.StateRegion).HasMaxLength(200);

                entity.Property(e => e.Title).HasMaxLength(200);
            });

            modelBuilder.Entity<CrmCustomFieldCompanyValues>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.FieldId });

                entity.ToTable("crmCustomFieldCompanyValues");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.FieldId).HasColumnName("FieldID");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnType("sql_variant");
            });

            modelBuilder.Entity<CrmCustomFieldContactValues>(entity =>
            {
                entity.HasKey(e => new { e.ContactId, e.FieldId });

                entity.ToTable("crmCustomFieldContactValues");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.FieldId).HasColumnName("FieldID");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnType("sql_variant");
            });

            modelBuilder.Entity<CrmCustomFieldOptions>(entity =>
            {
                entity.HasKey(e => e.OptionId)
                    .HasName("PK__crmCusto__92C7A1DF339B3397");

                entity.ToTable("crmCustomFieldOptions");

                entity.Property(e => e.OptionId).HasColumnName("OptionID");

                entity.Property(e => e.FieldId).HasColumnName("FieldID");

                entity.Property(e => e.OptionValue)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<CrmCustomFieldProjectValues>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.FieldId });

                entity.ToTable("crmCustomFieldProjectValues");

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.FieldId).HasColumnName("FieldID");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnType("sql_variant");
            });

            modelBuilder.Entity<CrmCustomFields>(entity =>
            {
                entity.HasKey(e => e.FieldId)
                    .HasName("PK__crmCusto__C8B6FF27EF1A0CD0");

                entity.ToTable("crmCustomFields");

                entity.Property(e => e.FieldId).HasColumnName("FieldID");

                entity.Property(e => e.FieldName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.InstanceId).HasColumnName("InstanceID");

                entity.Property(e => e.OrderByNumber).HasDefaultValueSql("((100))");
            });

            modelBuilder.Entity<CrmDepartments>(entity =>
            {
                entity.HasKey(e => e.DepartmentId)
                    .HasName("PK__Departme__B2079BCD17DE0E3D");

                entity.ToTable("crmDepartments");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.InstanceId).HasColumnName("InstanceID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<CrmFileAttachments>(entity =>
            {
                entity.HasKey(e => e.FileId)
                    .HasName("PK__crmFileA__6F0F989F3FE7AE7D");

                entity.ToTable("crmFileAttachments");

                entity.Property(e => e.FileId).HasColumnName("FileID");

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.FileData).HasColumnType("image");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.GoogleDriveUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CrmLead>(entity =>
            {
                entity.HasKey(e => e.LeadId)
                    .HasName("PK_Lead");

                entity.ToTable("crmLead");

                entity.Property(e => e.LeadId)
                    .HasColumnName("leadId")
                    .HasMaxLength(38);

                entity.Property(e => e.AccountExecutiveId)
                    .HasColumnName("accountExecutiveId")
                    .HasMaxLength(38);

                entity.Property(e => e.ChannelId)
                    .HasColumnName("channelId")
                    .HasMaxLength(38);

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(30);

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(30);

                entity.Property(e => e.CreatedAt).HasColumnName("createdAt");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("customerId")
                    .HasMaxLength(38);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(50);

                entity.Property(e => e.IsConverted).HasColumnName("isConverted");

                entity.Property(e => e.IsQualified).HasColumnName("isQualified");

                entity.Property(e => e.LeadName)
                    .IsRequired()
                    .HasColumnName("leadName")
                    .HasMaxLength(50);

                entity.Property(e => e.Province)
                    .HasColumnName("province")
                    .HasMaxLength(30);

                entity.Property(e => e.Street1)
                    .IsRequired()
                    .HasColumnName("street1")
                    .HasMaxLength(50);

                entity.Property(e => e.Street2)
                    .HasColumnName("street2")
                    .HasMaxLength(50);

                entity.HasOne(d => d.AccountExecutive)
                    .WithMany(p => p.CrmLead)
                    .HasForeignKey(d => d.AccountExecutiveId)
                    .HasConstraintName("FK_Lead_AccountExecutive_accountExecutiveId");

                entity.HasOne(d => d.Channel)
                    .WithMany(p => p.CrmLead)
                    .HasForeignKey(d => d.ChannelId)
                    .HasConstraintName("FK_Lead_Channel_channelId");
            });

            modelBuilder.Entity<CrmLeadLine>(entity =>
            {
                entity.HasKey(e => e.LeadLineId)
                    .HasName("PK_LeadLine");

                entity.ToTable("crmLeadLine");

                entity.Property(e => e.LeadLineId)
                    .HasColumnName("leadLineId")
                    .HasMaxLength(38);

                entity.Property(e => e.ActivityId)
                    .HasColumnName("activityId")
                    .HasMaxLength(38);

                entity.Property(e => e.CreatedAt).HasColumnName("createdAt");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(200);

                entity.Property(e => e.EndDate).HasColumnName("endDate");

                entity.Property(e => e.LeadId)
                    .HasColumnName("leadId")
                    .HasMaxLength(38);

                entity.Property(e => e.StartDate).HasColumnName("startDate");

                entity.HasOne(d => d.Lead)
                    .WithMany(p => p.CrmLeadLine)
                    .HasForeignKey(d => d.LeadId)
                    .HasConstraintName("FK_LeadLine_Lead_leadId");
            });

            modelBuilder.Entity<CrmOpportunity>(entity =>
            {
                entity.HasKey(e => e.OpportunityId)
                    .HasName("PK_Opportunity");

                entity.ToTable("crmOpportunity");

                entity.Property(e => e.OpportunityId)
                    .HasColumnName("opportunityId")
                    .HasMaxLength(38);

                entity.Property(e => e.AccountExecutiveId)
                    .HasColumnName("accountExecutiveId")
                    .HasMaxLength(38);

                entity.Property(e => e.CreatedAt).HasColumnName("createdAt");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("customerId")
                    .HasMaxLength(38);

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(50);

                entity.Property(e => e.EstimatedClosingDate).HasColumnName("estimatedClosingDate");

                entity.Property(e => e.EstimatedRevenue)
                    .HasColumnName("estimatedRevenue")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OpportunityName)
                    .IsRequired()
                    .HasColumnName("opportunityName")
                    .HasMaxLength(50);

                entity.Property(e => e.Probability).HasColumnName("probability");

                entity.Property(e => e.RatingId)
                    .HasColumnName("ratingId")
                    .HasMaxLength(38);

                entity.Property(e => e.StageId)
                    .HasColumnName("stageId")
                    .HasMaxLength(38);

                entity.HasOne(d => d.AccountExecutive)
                    .WithMany(p => p.CrmOpportunity)
                    .HasForeignKey(d => d.AccountExecutiveId)
                    .HasConstraintName("FK_Opportunity_AccountExecutive_accountExecutiveId");

                entity.HasOne(d => d.Rating)
                    .WithMany(p => p.CrmOpportunity)
                    .HasForeignKey(d => d.RatingId)
                    .HasConstraintName("FK_Opportunity_Rating_ratingId");
            });

            modelBuilder.Entity<CrmOpportunityLine>(entity =>
            {
                entity.HasKey(e => e.OpportunityLineId)
                    .HasName("PK_OpportunityLine");

                entity.ToTable("crmOpportunityLine");

                entity.Property(e => e.OpportunityLineId)
                    .HasColumnName("opportunityLineId")
                    .HasMaxLength(38);

                entity.Property(e => e.ActivityId)
                    .HasColumnName("activityId")
                    .HasMaxLength(38);

                entity.Property(e => e.CreatedAt).HasColumnName("createdAt");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(200);

                entity.Property(e => e.EndDate).HasColumnName("endDate");

                entity.Property(e => e.OpportunityId)
                    .HasColumnName("opportunityId")
                    .HasMaxLength(38);

                entity.Property(e => e.StartDate).HasColumnName("startDate");

                entity.HasOne(d => d.Activity)
                    .WithMany(p => p.CrmOpportunityLine)
                    .HasForeignKey(d => d.ActivityId)
                    .HasConstraintName("FK_OpportunityLine_Activity_activityId");

                entity.HasOne(d => d.Opportunity)
                    .WithMany(p => p.CrmOpportunityLine)
                    .HasForeignKey(d => d.OpportunityId)
                    .HasConstraintName("FK_OpportunityLine_Opportunity_opportunityId");
            });

            modelBuilder.Entity<CrmPaymentMethods>(entity =>
            {
                entity.ToTable("crmPaymentMethods");

                entity.Property(e => e.CreditCardNumber)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ValidUptoDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CrmProjectCompanies>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.CompanyId });

                entity.ToTable("crmProjectCompanies");

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            });

            modelBuilder.Entity<CrmProjectContacts>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.ContactId });

                entity.ToTable("crmProjectContacts");

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");
            });

            modelBuilder.Entity<CrmProjectSubscribers>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.UserId });

                entity.ToTable("crmProjectSubscribers");

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<CrmProjectTags>(entity =>
            {
                entity.HasKey(e => new { e.TagId, e.ProjectId });

                entity.ToTable("crmProjectTags");

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
            });

            modelBuilder.Entity<CrmProjectViewHistory>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ProjectId });

                entity.ToTable("crmProjectViewHistory");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.ViewDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<CrmProjects>(entity =>
            {
                entity.HasKey(e => e.ProjectId)
                    .HasName("PK__crmProje__761ABED0194ECC7F");

                entity.ToTable("crmProjects");

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.AssignedToUserId).HasColumnName("AssignedToUserID");

                entity.Property(e => e.InstanceId).HasColumnName("InstanceID");

                entity.Property(e => e.LatestCommentId).HasColumnName("LatestCommentID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Value).HasColumnType("money");

                entity.Property(e => e.ValueCurrency)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("('USD')");
            });

            modelBuilder.Entity<CrmPurchaseOrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.PurchaseOrderId, e.PurchaseOrderDetailId })
                    .HasName("PK_PurchaseOrderDetail_PurchaseOrderID_PurchaseOrderDetailID");

                entity.ToTable("crmPurchaseOrderDetail");

                entity.Property(e => e.PurchaseOrderId).HasColumnName("PurchaseOrderID");

                entity.Property(e => e.PurchaseOrderDetailId)
                    .HasColumnName("PurchaseOrderDetailID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.LineTotal)
                    .HasColumnType("money")
                    .HasComputedColumnSql("(isnull([OrderQty]*[UnitPrice],(0.00)))");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ReceivedQty).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.RejectedQty).HasColumnType("decimal(8, 2)");

                entity.Property(e => e.StockedQty)
                    .HasColumnType("decimal(9, 2)")
                    .HasComputedColumnSql("(isnull([ReceivedQty]-[RejectedQty],(0.00)))");

                entity.Property(e => e.UnitPrice).HasColumnType("money");
            });

            modelBuilder.Entity<CrmPurchaseOrderHeader>(entity =>
            {
                entity.HasKey(e => e.PurchaseOrderId)
                    .HasName("PK_PurchaseOrderHeader_PurchaseOrderID");

                entity.ToTable("crmPurchaseOrderHeader");

                entity.Property(e => e.PurchaseOrderId).HasColumnName("PurchaseOrderID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.Freight)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ShipDate).HasColumnType("datetime");

                entity.Property(e => e.ShipMethodId).HasColumnName("ShipMethodID");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.SubTotal)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.TaxAmt)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.TotalDue)
                    .HasColumnType("money")
                    .HasComputedColumnSql("(isnull(([SubTotal]+[TaxAmt])+[Freight],(0)))");

                entity.Property(e => e.VendorId).HasColumnName("VendorID");
            });

            modelBuilder.Entity<CrmRating>(entity =>
            {
                entity.HasKey(e => e.RatingId)
                    .HasName("PK_Rating");

                entity.ToTable("crmRating");

                entity.Property(e => e.RatingId)
                    .HasColumnName("ratingId")
                    .HasMaxLength(38);

                entity.Property(e => e.ColorHex)
                    .HasColumnName("colorHex")
                    .HasMaxLength(10);

                entity.Property(e => e.CreatedAt).HasColumnName("createdAt");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(50);

                entity.Property(e => e.RatingName)
                    .IsRequired()
                    .HasColumnName("ratingName")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CrmSettings>(entity =>
            {
                entity.HasKey(e => e.InstanceId)
                    .HasName("PK__crmSetti__5C51996FBC5F1E0E");

                entity.ToTable("crmSettings");

                entity.Property(e => e.InstanceId)
                    .HasColumnName("InstanceID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AllowUserRegistration)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.AutoLoginSharedSecret)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FromAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HdrootUrl)
                    .IsRequired()
                    .HasColumnName("HDRootUrl")
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.HdsharedKey)
                    .IsRequired()
                    .HasColumnName("HDSharedKey")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.HeaderBgcolor)
                    .IsRequired()
                    .HasColumnName("HeaderBGColor")
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("('34383E')");

                entity.Property(e => e.Lang)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('en-US')");

                entity.Property(e => e.LogoImage).HasColumnType("image");

                entity.Property(e => e.MailCheckerInterval).HasDefaultValueSql("((600))");

                entity.Property(e => e.MailCheckerPoplogin)
                    .HasColumnName("MailCheckerPOPLogin")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MailCheckerPoppassword)
                    .HasColumnName("MailCheckerPOPPassword")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MailCheckerPopport)
                    .HasColumnName("MailCheckerPOPPort")
                    .HasDefaultValueSql("((110))");

                entity.Property(e => e.MailCheckerPopserver)
                    .HasColumnName("MailCheckerPOPServer")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MailCheckerUseSsl).HasColumnName("MailCheckerUseSSL");

                entity.Property(e => e.MaxAttachSize).HasDefaultValueSql("((1000))");

                entity.Property(e => e.MenuBarBgcolor)
                    .IsRequired()
                    .HasColumnName("MenuBarBGColor")
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("('4C5B6B')");

                entity.Property(e => e.MenuBarTabColor)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasDefaultValueSql("('D5783A')");

                entity.Property(e => e.ReplyToAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SmtpauthRequired).HasColumnName("SMTPAuthRequired");

                entity.Property(e => e.Smtppassword)
                    .HasColumnName("SMTPPassword")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Smtpport)
                    .HasColumnName("SMTPPort")
                    .HasDefaultValueSql("((25))");

                entity.Property(e => e.Smtpserver)
                    .HasColumnName("SMTPServer")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SmtpuseSsl).HasColumnName("SMTPUseSSL");

                entity.Property(e => e.SmtpuserName)
                    .HasColumnName("SMTPUserName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasDefaultValueSql("('Jitbit CRM')");
            });

            modelBuilder.Entity<CrmSharedDocuments>(entity =>
            {
                entity.ToTable("crmSharedDocuments");

                entity.Property(e => e.DocumentName)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentPath)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CrmSubscriptions>(entity =>
            {
                entity.ToTable("crmSubscriptions");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(8, 2)");
            });

            modelBuilder.Entity<CrmTags>(entity =>
            {
                entity.HasKey(e => e.TagId)
                    .HasName("PK__crmTags__657CFA4C2D22C2A7");

                entity.ToTable("crmTags");

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.Property(e => e.InstanceId).HasColumnName("InstanceID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<CrmTaskComments>(entity =>
            {
                entity.HasKey(e => e.CommentId);

                entity.ToTable("crmTaskComments");

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.Body)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.TaskId).HasColumnName("TaskID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.CrmTaskComments)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_crmTaskComments_crmTasks");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CrmTaskComments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_crmTaskComments_crmUsers");
            });

            modelBuilder.Entity<CrmTasks>(entity =>
            {
                entity.HasKey(e => e.TaskId)
                    .HasName("PK__crmTasks__7C6949D1AD4AFCEA");

                entity.ToTable("crmTasks");

                entity.Property(e => e.TaskId).HasColumnName("TaskID");

                entity.Property(e => e.AssignedToUserId).HasColumnName("AssignedToUserID");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.TaskText)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<CrmUserBasicDetail>(entity =>
            {
                entity.ToTable("crmUserBasicDetail");

                entity.Property(e => e.Company)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CrmUserGridColumns>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__crmUserG__1788CCAC8D06CFA3");

                entity.ToTable("crmUserGridColumns");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CustomFieldsToShowInCompanies)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CustomFieldsToShowInContacts)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<CrmUsers>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__crmUsers__1788CCAC8453990A");

                entity.ToTable("crmUsers");

                entity.HasIndex(e => new { e.UserName, e.InstanceId })
                    .HasName("IX_crmUsers_UniqueNameInstance")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmailSignature).HasMaxLength(1000);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.InstanceId).HasColumnName("InstanceID");

                entity.Property(e => e.Ipaddress)
                    .HasColumnName("IPAddress")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LastSeen).HasColumnType("datetime");

                entity.Property(e => e.Location).HasMaxLength(1000);

                entity.Property(e => e.Notes)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SubscriptionOperatorRig>(entity =>
            {
                entity.ToTable("SubscriptionOperatorRigs");

                entity.Property(e => e.ID)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.CompanyId)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.RigId)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);                
            });
        }
    }
}
