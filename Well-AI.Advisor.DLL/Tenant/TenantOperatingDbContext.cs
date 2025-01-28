using Finbuckle.MultiTenant;
using Microsoft.EntityFrameworkCore;
using WellAI.Advisor.Model.Tenant.Models;
using WellAI.Advisor.Model.OperatingCompany.Models;
using WellAI.Advisor.Model.Common;

namespace WellAI.Advisor
{
    //PaymentTypeModel - Payment method moved to Main DB for Dispatch subscription logic.
    public class TenantOperatingDbContext : MultiTenantDbContext
    {
        public TenantOperatingDbContext(TenantInfo tenantInfo) : base(tenantInfo)
        {
        }

        public TenantOperatingDbContext(TenantInfo tenantInfo, DbContextOptions<TenantOperatingDbContext> options) : base(tenantInfo, options)
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

        #region #dbsets

        public DbSet<TaskSchedule> TasksSchedule { get; set; }
        public DbSet<ProviderDirectoryApproval> ProviderDirectoryAppovals { get; set; }
        public DbSet<ProviderDirectoryStatus> ProviderDirectoryStatuses { get; set; }
        public DbSet<ProviderDirectoryPEC> ProviderDirectoryPECs { get; set; }
        public DbSet<ProviderDirectory> ProvidersDirectory { get; set; }
        //public DbSet<PaymentTypeModel> PaymentTypes { get; set; }
        //public DbSet<CreditCardTypeModel> CreditcardType { get; set; }
        //public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<BillingHistory> BillingHistoryInvoices { get; set; }

        public virtual DbSet<AttachmentObjectReferences> AttachmentObjectReferences { get; set; }
        public virtual DbSet<Attachments> Attachments { get; set; }
        public virtual DbSet<AttchmentCommonDatas> AttchmentCommonDatas { get; set; }
        public virtual DbSet<BharunActDoglegMxs> BharunActDoglegMxs { get; set; }
        public virtual DbSet<BharunActDoglegs> BharunActDoglegs { get; set; }
        public virtual DbSet<BharunAziBottoms> BharunAziBottoms { get; set; }
        public virtual DbSet<BharunAziTops> BharunAziTops { get; set; }
        public virtual DbSet<BharunCommonDatas> BharunCommonDatas { get; set; }
        public virtual DbSet<BharunCtimCircs> BharunCtimCircs { get; set; }
        public virtual DbSet<BharunCtimDrillRots> BharunCtimDrillRots { get; set; }
        public virtual DbSet<BharunCtimDrillSlids> BharunCtimDrillSlids { get; set; }
        public virtual DbSet<BharunCtimHolds> BharunCtimHolds { get; set; }
        public virtual DbSet<BharunCtimReams> BharunCtimReams { get; set; }
        public virtual DbSet<BharunCtimSteerings> BharunCtimSteerings { get; set; }
        public virtual DbSet<BharunDistDrillRots> BharunDistDrillRots { get; set; }
        public virtual DbSet<BharunDistDrillSlids> BharunDistDrillSlids { get; set; }
        public virtual DbSet<BharunDistHolds> BharunDistHolds { get; set; }
        public virtual DbSet<BharunDistReams> BharunDistReams { get; set; }
        public virtual DbSet<BharunDistSteerings> BharunDistSteerings { get; set; }
        public virtual DbSet<BharunDrillingParamss> BharunDrillingParamss { get; set; }
        public virtual DbSet<BharunEtimOpBits> BharunEtimOpBits { get; set; }
        public virtual DbSet<BharunFlowrateBits> BharunFlowrateBits { get; set; }
        public virtual DbSet<BharunFlowratePumps> BharunFlowratePumps { get; set; }
        public virtual DbSet<BharunHkldDns> BharunHkldDns { get; set; }
        public virtual DbSet<BharunHkldRots> BharunHkldRots { get; set; }
        public virtual DbSet<BharunHkldUps> BharunHkldUps { get; set; }
        public virtual DbSet<BharunInclMns> BharunInclMns { get; set; }
        public virtual DbSet<BharunInclMxs> BharunInclMxs { get; set; }
        public virtual DbSet<BharunInclStarts> BharunInclStarts { get; set; }
        public virtual DbSet<BharunInclStops> BharunInclStops { get; set; }
        public virtual DbSet<BharunMdHoleStarts> BharunMdHoleStarts { get; set; }
        public virtual DbSet<BharunMdHoleStops> BharunMdHoleStops { get; set; }
        public virtual DbSet<BharunOverPulls> BharunOverPulls { get; set; }
        public virtual DbSet<BharunPlanDoglegs> BharunPlanDoglegs { get; set; }
        public virtual DbSet<BharunPowBits> BharunPowBits { get; set; }
        public virtual DbSet<BharunPresDropBits> BharunPresDropBits { get; set; }
        public virtual DbSet<BharunPresPumpAvs> BharunPresPumpAvs { get; set; }
        public virtual DbSet<BharunRopAvs> BharunRopAvs { get; set; }
        public virtual DbSet<BharunRopMns> BharunRopMns { get; set; }
        public virtual DbSet<BharunRopMxs> BharunRopMxs { get; set; }
        public virtual DbSet<BharunRpmAvDhs> BharunRpmAvDhs { get; set; }
        public virtual DbSet<BharunRpmAvs> BharunRpmAvs { get; set; }
        public virtual DbSet<BharunRpmMns> BharunRpmMns { get; set; }
        public virtual DbSet<BharunRpmMxs> BharunRpmMxs { get; set; }
        public virtual DbSet<BharunSlackOff> BharunSlackOff { get; set; }
        public virtual DbSet<BharunTempMudDhMxs> BharunTempMudDhMxs { get; set; }
        public virtual DbSet<BharunTqDhAvs> BharunTqDhAvs { get; set; }
        public virtual DbSet<BharunTqOffBotAvs> BharunTqOffBotAvs { get; set; }
        public virtual DbSet<BharunTqOnBotAvs> BharunTqOnBotAvs { get; set; }
        public virtual DbSet<BharunTqOnBotMns> BharunTqOnBotMns { get; set; }
        public virtual DbSet<BharunTqOnBotMxs> BharunTqOnBotMxs { get; set; }
        public virtual DbSet<BharunTubulars> BharunTubulars { get; set; }
        public virtual DbSet<BharunVelNozzleAvs> BharunVelNozzleAvs { get; set; }
        public virtual DbSet<BharunWobAvDhs> BharunWobAvDhs { get; set; }
        public virtual DbSet<BharunWobAvs> BharunWobAvs { get; set; }
        public virtual DbSet<BharunWobMns> BharunWobMns { get; set; }
        public virtual DbSet<BharunWobMxs> BharunWobMxs { get; set; }
        public virtual DbSet<BharunWtAboveJars> BharunWtAboveJars { get; set; }
        public virtual DbSet<BharunWtBelowJars> BharunWtBelowJars { get; set; }
        public virtual DbSet<BharunWtMuds> BharunWtMuds { get; set; }
        public virtual DbSet<Bharuns> Bharuns { get; set; }
        public virtual DbSet<CementJobAdditives> CementJobAdditives { get; set; }
        public virtual DbSet<CementJobCblPress> CementJobCblPress { get; set; }
        public virtual DbSet<CementJobCementAdditives> CementJobCementAdditives { get; set; }
        public virtual DbSet<CementJobCementPumpSchedules> CementJobCementPumpSchedules { get; set; }
        public virtual DbSet<CementJobCementStages> CementJobCementStages { get; set; }
        public virtual DbSet<CementJobCementTests> CementJobCementTests { get; set; }
        public virtual DbSet<CementJobCementingFluids> CementJobCementingFluids { get; set; }
        public virtual DbSet<CementJobCommonDatas> CementJobCommonDatas { get; set; }
        public virtual DbSet<CementJobConcentrations> CementJobConcentrations { get; set; }
        public virtual DbSet<CementJobConsTestThickenings> CementJobConsTestThickenings { get; set; }
        public virtual DbSet<CementJobDensAdds> CementJobDensAdds { get; set; }
        public virtual DbSet<CementJobDensAtPress> CementJobDensAtPress { get; set; }
        public virtual DbSet<CementJobDensBaseFluids> CementJobDensBaseFluids { get; set; }
        public virtual DbSet<CementJobDensConstGasFoams> CementJobDensConstGasFoams { get; set; }
        public virtual DbSet<CementJobDensConstGasMethods> CementJobDensConstGasMethods { get; set; }
        public virtual DbSet<CementJobDensDisplaceFluids> CementJobDensDisplaceFluids { get; set; }
        public virtual DbSet<CementJobDensDryBlends> CementJobDensDryBlends { get; set; }
        public virtual DbSet<CementJobDensitys> CementJobDensitys { get; set; }
        public virtual DbSet<CementJobDiaTailPipes> CementJobDiaTailPipes { get; set; }
        public virtual DbSet<CementJobEtimBeforeTests> CementJobEtimBeforeTests { get; set; }
        public virtual DbSet<CementJobEtimCementLogs> CementJobEtimCementLogs { get; set; }
        public virtual DbSet<CementJobEtimComprStren1s> CementJobEtimComprStren1s { get; set; }
        public virtual DbSet<CementJobEtimComprStren2s> CementJobEtimComprStren2s { get; set; }
        public virtual DbSet<CementJobEtimMudCirculations> CementJobEtimMudCirculations { get; set; }
        public virtual DbSet<CementJobEtimPitStarts> CementJobEtimPitStarts { get; set; }
        public virtual DbSet<CementJobEtimPresHelds> CementJobEtimPresHelds { get; set; }
        public virtual DbSet<CementJobEtimPumps> CementJobEtimPumps { get; set; }
        public virtual DbSet<CementJobEtimShutdowns> CementJobEtimShutdowns { get; set; }
        public virtual DbSet<CementJobEtimTests> CementJobEtimTests { get; set; }
        public virtual DbSet<CementJobEtimThickenings> CementJobEtimThickenings { get; set; }
        public virtual DbSet<CementJobExcessPcs> CementJobExcessPcs { get; set; }
        public virtual DbSet<CementJobFlowrateBreakDowns> CementJobFlowrateBreakDowns { get; set; }
        public virtual DbSet<CementJobFlowrateDisplaceAvs> CementJobFlowrateDisplaceAvs { get; set; }
        public virtual DbSet<CementJobFlowrateDisplaceMxs> CementJobFlowrateDisplaceMxs { get; set; }
        public virtual DbSet<CementJobFlowrateEnds> CementJobFlowrateEnds { get; set; }
        public virtual DbSet<CementJobFlowrateMudCircs> CementJobFlowrateMudCircs { get; set; }
        public virtual DbSet<CementJobFlowratePumpEnds> CementJobFlowratePumpEnds { get; set; }
        public virtual DbSet<CementJobFlowratePumpStarts> CementJobFlowratePumpStarts { get; set; }
        public virtual DbSet<CementJobFlowrateSqueezeAvs> CementJobFlowrateSqueezeAvs { get; set; }
        public virtual DbSet<CementJobFlowrateSqueezeMxs> CementJobFlowrateSqueezeMxs { get; set; }
        public virtual DbSet<CementJobFormPits> CementJobFormPits { get; set; }
        public virtual DbSet<CementJobGel10MinReadings> CementJobGel10MinReadings { get; set; }
        public virtual DbSet<CementJobGel10MinStrengths> CementJobGel10MinStrengths { get; set; }
        public virtual DbSet<CementJobGel10Mins> CementJobGel10Mins { get; set; }
        public virtual DbSet<CementJobGel10SecReadings> CementJobGel10SecReadings { get; set; }
        public virtual DbSet<CementJobGel10SecStrengths> CementJobGel10SecStrengths { get; set; }
        public virtual DbSet<CementJobGel10Secs> CementJobGel10Secs { get; set; }
        public virtual DbSet<CementJobGel1MinReadings> CementJobGel1MinReadings { get; set; }
        public virtual DbSet<CementJobGel1MinStrengths> CementJobGel1MinStrengths { get; set; }
        public virtual DbSet<CementJobKs> CementJobKs { get; set; }
        public virtual DbSet<CementJobLenPipeRecipStrokes> CementJobLenPipeRecipStrokes { get; set; }
        public virtual DbSet<CementJobLinerLaps> CementJobLinerLaps { get; set; }
        public virtual DbSet<CementJobLinerTops> CementJobLinerTops { get; set; }
        public virtual DbSet<CementJobMassDryBlends> CementJobMassDryBlends { get; set; }
        public virtual DbSet<CementJobMassSackDryBlends> CementJobMassSackDryBlends { get; set; }
        public virtual DbSet<CementJobMdBottoms> CementJobMdBottoms { get; set; }
        public virtual DbSet<CementJobMdCementTops> CementJobMdCementTops { get; set; }
        public virtual DbSet<CementJobMdCircOuts> CementJobMdCircOuts { get; set; }
        public virtual DbSet<CementJobMdCoilTbgs> CementJobMdCoilTbgs { get; set; }
        public virtual DbSet<CementJobMdDvtools> CementJobMdDvtools { get; set; }
        public virtual DbSet<CementJobMdFluidBottoms> CementJobMdFluidBottoms { get; set; }
        public virtual DbSet<CementJobMdFluidTops> CementJobMdFluidTops { get; set; }
        public virtual DbSet<CementJobMdHoles> CementJobMdHoles { get; set; }
        public virtual DbSet<CementJobMdPlugBots> CementJobMdPlugBots { get; set; }
        public virtual DbSet<CementJobMdPlugTops> CementJobMdPlugTops { get; set; }
        public virtual DbSet<CementJobMdShoes> CementJobMdShoes { get; set; }
        public virtual DbSet<CementJobMdSqueezes> CementJobMdSqueezes { get; set; }
        public virtual DbSet<CementJobMdStringSets> CementJobMdStringSets { get; set; }
        public virtual DbSet<CementJobMdStrings> CementJobMdStrings { get; set; }
        public virtual DbSet<CementJobMdTools> CementJobMdTools { get; set; }
        public virtual DbSet<CementJobMdTops> CementJobMdTops { get; set; }
        public virtual DbSet<CementJobMdWaters> CementJobMdWaters { get; set; }
        public virtual DbSet<CementJobNs> CementJobNs { get; set; }
        public virtual DbSet<CementJobOverPulls> CementJobOverPulls { get; set; }
        public virtual DbSet<CementJobPcFreeWaters> CementJobPcFreeWaters { get; set; }
        public virtual DbSet<CementJobPresBackPressures> CementJobPresBackPressures { get; set; }
        public virtual DbSet<CementJobPresBacks> CementJobPresBacks { get; set; }
        public virtual DbSet<CementJobPresBreakDowns> CementJobPresBreakDowns { get; set; }
        public virtual DbSet<CementJobPresBumps> CementJobPresBumps { get; set; }
        public virtual DbSet<CementJobPresCoilTbgEnds> CementJobPresCoilTbgEnds { get; set; }
        public virtual DbSet<CementJobPresCoilTbgStarts> CementJobPresCoilTbgStarts { get; set; }
        public virtual DbSet<CementJobPresComprStren1s> CementJobPresComprStren1s { get; set; }
        public virtual DbSet<CementJobPresComprStren2s> CementJobPresComprStren2s { get; set; }
        public virtual DbSet<CementJobPresCsgEnds> CementJobPresCsgEnds { get; set; }
        public virtual DbSet<CementJobPresCsgStarts> CementJobPresCsgStarts { get; set; }
        public virtual DbSet<CementJobPresDisplaces> CementJobPresDisplaces { get; set; }
        public virtual DbSet<CementJobPresHelds> CementJobPresHelds { get; set; }
        public virtual DbSet<CementJobPresMudCircs> CementJobPresMudCircs { get; set; }
        public virtual DbSet<CementJobPresPriorBumps> CementJobPresPriorBumps { get; set; }
        public virtual DbSet<CementJobPresSqueezeAvs> CementJobPresSqueezeAvs { get; set; }
        public virtual DbSet<CementJobPresSqueezeEnds> CementJobPresSqueezeEnds { get; set; }
        public virtual DbSet<CementJobPresSqueezes> CementJobPresSqueezes { get; set; }
        public virtual DbSet<CementJobPresTbgEnds> CementJobPresTbgEnds { get; set; }
        public virtual DbSet<CementJobPresTbgStarts> CementJobPresTbgStarts { get; set; }
        public virtual DbSet<CementJobPresTestFluidLosss> CementJobPresTestFluidLosss { get; set; }
        public virtual DbSet<CementJobPresTestThickenings> CementJobPresTestThickenings { get; set; }
        public virtual DbSet<CementJobPresTests> CementJobPresTests { get; set; }
        public virtual DbSet<CementJobPvMuds> CementJobPvMuds { get; set; }
        public virtual DbSet<CementJobRatePumps> CementJobRatePumps { get; set; }
        public virtual DbSet<CementJobRatioConstGasMethodAvs> CementJobRatioConstGasMethodAvs { get; set; }
        public virtual DbSet<CementJobRatioConstGasMethodEnds> CementJobRatioConstGasMethodEnds { get; set; }
        public virtual DbSet<CementJobRatioConstGasMethodStarts> CementJobRatioConstGasMethodStarts { get; set; }
        public virtual DbSet<CementJobRatioMixWaters> CementJobRatioMixWaters { get; set; }
        public virtual DbSet<CementJobRpmPipeRecips> CementJobRpmPipeRecips { get; set; }
        public virtual DbSet<CementJobRpmPipes> CementJobRpmPipes { get; set; }
        public virtual DbSet<CementJobSlackOffs> CementJobSlackOffs { get; set; }
        public virtual DbSet<CementJobSolidVolumeFractions> CementJobSolidVolumeFractions { get; set; }
        public virtual DbSet<CementJobTempBhcts> CementJobTempBhcts { get; set; }
        public virtual DbSet<CementJobTempBhsts> CementJobTempBhsts { get; set; }
        public virtual DbSet<CementJobTempComprStren1s> CementJobTempComprStren1s { get; set; }
        public virtual DbSet<CementJobTempComprStren2s> CementJobTempComprStren2s { get; set; }
        public virtual DbSet<CementJobTempFluidLosss> CementJobTempFluidLosss { get; set; }
        public virtual DbSet<CementJobTempFreeWaters> CementJobTempFreeWaters { get; set; }
        public virtual DbSet<CementJobTempThickenings> CementJobTempThickenings { get; set; }
        public virtual DbSet<CementJobTestNegativeEmws> CementJobTestNegativeEmws { get; set; }
        public virtual DbSet<CementJobTestPositiveEmws> CementJobTestPositiveEmws { get; set; }
        public virtual DbSet<CementJobTimeFluidLosss> CementJobTimeFluidLosss { get; set; }
        public virtual DbSet<CementJobTqInitPipeRots> CementJobTqInitPipeRots { get; set; }
        public virtual DbSet<CementJobTqPipeAvs> CementJobTqPipeAvs { get; set; }
        public virtual DbSet<CementJobTqPipeMxs> CementJobTqPipeMxs { get; set; }
        public virtual DbSet<CementJobTvdShoes> CementJobTvdShoes { get; set; }
        public virtual DbSet<CementJobTvdStringSets> CementJobTvdStringSets { get; set; }
        public virtual DbSet<CementJobVisFunnelMuds> CementJobVisFunnelMuds { get; set; }
        public virtual DbSet<CementJobViss> CementJobViss { get; set; }
        public virtual DbSet<CementJobVolApifluidLosss> CementJobVolApifluidLosss { get; set; }
        public virtual DbSet<CementJobVolCements> CementJobVolCements { get; set; }
        public virtual DbSet<CementJobVolCircPriors> CementJobVolCircPriors { get; set; }
        public virtual DbSet<CementJobVolCsgIns> CementJobVolCsgIns { get; set; }
        public virtual DbSet<CementJobVolCsgOuts> CementJobVolCsgOuts { get; set; }
        public virtual DbSet<CementJobVolDisplaceFluids> CementJobVolDisplaceFluids { get; set; }
        public virtual DbSet<CementJobVolExcesss> CementJobVolExcesss { get; set; }
        public virtual DbSet<CementJobVolFluids> CementJobVolFluids { get; set; }
        public virtual DbSet<CementJobVolGasFoams> CementJobVolGasFoams { get; set; }
        public virtual DbSet<CementJobVolMudLosts> CementJobVolMudLosts { get; set; }
        public virtual DbSet<CementJobVolOthers> CementJobVolOthers { get; set; }
        public virtual DbSet<CementJobVolPumpeds> CementJobVolPumpeds { get; set; }
        public virtual DbSet<CementJobVolPumps> CementJobVolPumps { get; set; }
        public virtual DbSet<CementJobVolReserveds> CementJobVolReserveds { get; set; }
        public virtual DbSet<CementJobVolReturnss> CementJobVolReturnss { get; set; }
        public virtual DbSet<CementJobVolTestFluidLosss> CementJobVolTestFluidLosss { get; set; }
        public virtual DbSet<CementJobVolTotSlurrys> CementJobVolTotSlurrys { get; set; }
        public virtual DbSet<CementJobVolWaters> CementJobVolWaters { get; set; }
        public virtual DbSet<CementJobVolYields> CementJobVolYields { get; set; }
        public virtual DbSet<CementJobWocs> CementJobWocs { get; set; }
        public virtual DbSet<CementJobWtMuds> CementJobWtMuds { get; set; }
        public virtual DbSet<CementJobYpMuds> CementJobYpMuds { get; set; }
        public virtual DbSet<CementJobYps> CementJobYps { get; set; }
        public virtual DbSet<CementJobs> CementJobs { get; set; }
        public virtual DbSet<ChangeLogChangeHistory> ChangeLogChangeHistory { get; set; }
        public virtual DbSet<ChangeLogCommonData> ChangeLogCommonData { get; set; }
        public virtual DbSet<ChangeLogEndIndexs> ChangeLogEndIndexs { get; set; }
        public virtual DbSet<ChangeLogStartIndexs> ChangeLogStartIndexs { get; set; }
        public virtual DbSet<ChangeLogs> ChangeLogs { get; set; }
        public virtual DbSet<Configuration> Configuration { get; set; }
        public virtual DbSet<ConvCoreAcetylenes> ConvCoreAcetylenes { get; set; }
        public virtual DbSet<ConvCoreCalcStabs> ConvCoreCalcStabs { get; set; }
        public virtual DbSet<ConvCoreCalcites> ConvCoreCalcites { get; set; }
        public virtual DbSet<ConvCoreCecs> ConvCoreCecs { get; set; }
        public virtual DbSet<ConvCoreChromatographs> ConvCoreChromatographs { get; set; }
        public virtual DbSet<ConvCoreCo2Avs> ConvCoreCo2Avs { get; set; }
        public virtual DbSet<ConvCoreCo2Mns> ConvCoreCo2Mns { get; set; }
        public virtual DbSet<ConvCoreCo2Mxs> ConvCoreCo2Mxs { get; set; }
        public virtual DbSet<ConvCoreCommonDatas> ConvCoreCommonDatas { get; set; }
        public virtual DbSet<ConvCoreDensBulks> ConvCoreDensBulks { get; set; }
        public virtual DbSet<ConvCoreDensShales> ConvCoreDensShales { get; set; }
        public virtual DbSet<ConvCoreDiaBits> ConvCoreDiaBits { get; set; }
        public virtual DbSet<ConvCoreDiaCores> ConvCoreDiaCores { get; set; }
        public virtual DbSet<ConvCoreDolomites> ConvCoreDolomites { get; set; }
        public virtual DbSet<ConvCoreEcdTdAvs> ConvCoreEcdTdAvs { get; set; }
        public virtual DbSet<ConvCoreEpentAvs> ConvCoreEpentAvs { get; set; }
        public virtual DbSet<ConvCoreEpentMns> ConvCoreEpentMns { get; set; }
        public virtual DbSet<ConvCoreEpentMxs> ConvCoreEpentMxs { get; set; }
        public virtual DbSet<ConvCoreEthAvs> ConvCoreEthAvs { get; set; }
        public virtual DbSet<ConvCoreEthMns> ConvCoreEthMns { get; set; }
        public virtual DbSet<ConvCoreEthMxs> ConvCoreEthMxs { get; set; }
        public virtual DbSet<ConvCoreEtimChromCycles> ConvCoreEtimChromCycles { get; set; }
        public virtual DbSet<ConvCoreGasAvs> ConvCoreGasAvs { get; set; }
        public virtual DbSet<ConvCoreGasBackgnds> ConvCoreGasBackgnds { get; set; }
        public virtual DbSet<ConvCoreGasConAvs> ConvCoreGasConAvs { get; set; }
        public virtual DbSet<ConvCoreGasConMxs> ConvCoreGasConMxs { get; set; }
        public virtual DbSet<ConvCoreGasPeaks> ConvCoreGasPeaks { get; set; }
        public virtual DbSet<ConvCoreGasTrips> ConvCoreGasTrips { get; set; }
        public virtual DbSet<ConvCoreGeologyIntervals> ConvCoreGeologyIntervals { get; set; }
        public virtual DbSet<ConvCoreH2sAvs> ConvCoreH2sAvs { get; set; }
        public virtual DbSet<ConvCoreH2sMns> ConvCoreH2sMns { get; set; }
        public virtual DbSet<ConvCoreH2sMxs> ConvCoreH2sMxs { get; set; }
        public virtual DbSet<ConvCoreIbutAvs> ConvCoreIbutAvs { get; set; }
        public virtual DbSet<ConvCoreIbutMns> ConvCoreIbutMns { get; set; }
        public virtual DbSet<ConvCoreIbutMxs> ConvCoreIbutMxs { get; set; }
        public virtual DbSet<ConvCoreIhexAvs> ConvCoreIhexAvs { get; set; }
        public virtual DbSet<ConvCoreIhexMns> ConvCoreIhexMns { get; set; }
        public virtual DbSet<ConvCoreIhexMxs> ConvCoreIhexMxs { get; set; }
        public virtual DbSet<ConvCoreInclHoles> ConvCoreInclHoles { get; set; }
        public virtual DbSet<ConvCoreIpentAvs> ConvCoreIpentAvs { get; set; }
        public virtual DbSet<ConvCoreIpentMns> ConvCoreIpentMns { get; set; }
        public virtual DbSet<ConvCoreIpentMxs> ConvCoreIpentMxs { get; set; }
        public virtual DbSet<ConvCoreLenBarrels> ConvCoreLenBarrels { get; set; }
        public virtual DbSet<ConvCoreLenCoreds> ConvCoreLenCoreds { get; set; }
        public virtual DbSet<ConvCoreLenPlugs> ConvCoreLenPlugs { get; set; }
        public virtual DbSet<ConvCoreLenRecovereds> ConvCoreLenRecovereds { get; set; }
        public virtual DbSet<ConvCoreLithPcs> ConvCoreLithPcs { get; set; }
        public virtual DbSet<ConvCoreLithologys> ConvCoreLithologys { get; set; }
        public virtual DbSet<ConvCoreMdBottoms> ConvCoreMdBottoms { get; set; }
        public virtual DbSet<ConvCoreMdCoreBottoms> ConvCoreMdCoreBottoms { get; set; }
        public virtual DbSet<ConvCoreMdCoreTops> ConvCoreMdCoreTops { get; set; }
        public virtual DbSet<ConvCoreMdTops> ConvCoreMdTops { get; set; }
        public virtual DbSet<ConvCoreMethAvs> ConvCoreMethAvs { get; set; }
        public virtual DbSet<ConvCoreMethMns> ConvCoreMethMns { get; set; }
        public virtual DbSet<ConvCoreMethMxs> ConvCoreMethMxs { get; set; }
        public virtual DbSet<ConvCoreMudGass> ConvCoreMudGass { get; set; }
        public virtual DbSet<ConvCoreNatFlorPcs> ConvCoreNatFlorPcs { get; set; }
        public virtual DbSet<ConvCoreNbutAvs> ConvCoreNbutAvs { get; set; }
        public virtual DbSet<ConvCoreNbutMns> ConvCoreNbutMns { get; set; }
        public virtual DbSet<ConvCoreNbutMxs> ConvCoreNbutMxs { get; set; }
        public virtual DbSet<ConvCoreNhexAvs> ConvCoreNhexAvs { get; set; }
        public virtual DbSet<ConvCoreNhexMns> ConvCoreNhexMns { get; set; }
        public virtual DbSet<ConvCoreNhexMxs> ConvCoreNhexMxs { get; set; }
        public virtual DbSet<ConvCoreNpentAvs> ConvCoreNpentAvs { get; set; }
        public virtual DbSet<ConvCoreNpentMns> ConvCoreNpentMns { get; set; }
        public virtual DbSet<ConvCoreNpentMxs> ConvCoreNpentMxs { get; set; }
        public virtual DbSet<ConvCorePropAvs> ConvCorePropAvs { get; set; }
        public virtual DbSet<ConvCorePropMns> ConvCorePropMns { get; set; }
        public virtual DbSet<ConvCorePropMxs> ConvCorePropMxs { get; set; }
        public virtual DbSet<ConvCoreQualifiers> ConvCoreQualifiers { get; set; }
        public virtual DbSet<ConvCoreRecoverPcs> ConvCoreRecoverPcs { get; set; }
        public virtual DbSet<ConvCoreRopAvs> ConvCoreRopAvs { get; set; }
        public virtual DbSet<ConvCoreRopMns> ConvCoreRopMns { get; set; }
        public virtual DbSet<ConvCoreRopMxs> ConvCoreRopMxs { get; set; }
        public virtual DbSet<ConvCoreRpmAvs> ConvCoreRpmAvs { get; set; }
        public virtual DbSet<ConvCoreShows> ConvCoreShows { get; set; }
        public virtual DbSet<ConvCoreSizeMns> ConvCoreSizeMns { get; set; }
        public virtual DbSet<ConvCoreSizeMxs> ConvCoreSizeMxs { get; set; }
        public virtual DbSet<ConvCoreStainPcs> ConvCoreStainPcs { get; set; }
        public virtual DbSet<ConvCoreTqAvs> ConvCoreTqAvs { get; set; }
        public virtual DbSet<ConvCoreTvdBases> ConvCoreTvdBases { get; set; }
        public virtual DbSet<ConvCoreTvdTops> ConvCoreTvdTops { get; set; }
        public virtual DbSet<ConvCoreWobAvs> ConvCoreWobAvs { get; set; }
        public virtual DbSet<ConvCoreWtMudAvs> ConvCoreWtMudAvs { get; set; }
        public virtual DbSet<ConvCoreWtMudIns> ConvCoreWtMudIns { get; set; }
        public virtual DbSet<ConvCoreWtMudOuts> ConvCoreWtMudOuts { get; set; }
        public virtual DbSet<ConvCores> ConvCores { get; set; }
        public virtual DbSet<CoordinateReferenceSystem> CoordinateReferenceSystem { get; set; }
        public virtual DbSet<CoordinateReferenceSystemAxisDirection> CoordinateReferenceSystemAxisDirection { get; set; }
        public virtual DbSet<CoordinateReferenceSystemBaseGeographicCrs> CoordinateReferenceSystemBaseGeographicCrs { get; set; }
        public virtual DbSet<CoordinateReferenceSystemCartesianCs> CoordinateReferenceSystemCartesianCs { get; set; }
        public virtual DbSet<CoordinateReferenceSystemConversion> CoordinateReferenceSystemConversion { get; set; }
        public virtual DbSet<CoordinateReferenceSystemCoordinateSystemAxis> CoordinateReferenceSystemCoordinateSystemAxis { get; set; }
        public virtual DbSet<CoordinateReferenceSystemDefinedByConversion> CoordinateReferenceSystemDefinedByConversion { get; set; }
        public virtual DbSet<CoordinateReferenceSystemEllipsoid> CoordinateReferenceSystemEllipsoid { get; set; }
        public virtual DbSet<CoordinateReferenceSystemEllipsoidalCs> CoordinateReferenceSystemEllipsoidalCs { get; set; }
        public virtual DbSet<CoordinateReferenceSystemGeodeticCrs> CoordinateReferenceSystemGeodeticCrs { get; set; }
        public virtual DbSet<CoordinateReferenceSystemGeodeticDatum> CoordinateReferenceSystemGeodeticDatum { get; set; }
        public virtual DbSet<CoordinateReferenceSystemGeographicCrs> CoordinateReferenceSystemGeographicCrs { get; set; }
        public virtual DbSet<CoordinateReferenceSystemGmlGeodeticCrs> CoordinateReferenceSystemGmlGeodeticCrs { get; set; }
        public virtual DbSet<CoordinateReferenceSystemGreenwichLongitude> CoordinateReferenceSystemGreenwichLongitude { get; set; }
        public virtual DbSet<CoordinateReferenceSystemIdentifier> CoordinateReferenceSystemIdentifier { get; set; }
        public virtual DbSet<CoordinateReferenceSystemInverseFlattening> CoordinateReferenceSystemInverseFlattening { get; set; }
        public virtual DbSet<CoordinateReferenceSystemName> CoordinateReferenceSystemName { get; set; }
        public virtual DbSet<CoordinateReferenceSystemNameCrs> CoordinateReferenceSystemNameCrs { get; set; }
        public virtual DbSet<CoordinateReferenceSystemOperationMethod> CoordinateReferenceSystemOperationMethod { get; set; }
        public virtual DbSet<CoordinateReferenceSystemOperationParameter> CoordinateReferenceSystemOperationParameter { get; set; }
        public virtual DbSet<CoordinateReferenceSystemParameterValue> CoordinateReferenceSystemParameterValue { get; set; }
        public virtual DbSet<CoordinateReferenceSystemPrimeMeridian> CoordinateReferenceSystemPrimeMeridian { get; set; }
        public virtual DbSet<CoordinateReferenceSystemProjectedCrs> CoordinateReferenceSystemProjectedCrs { get; set; }
        public virtual DbSet<CoordinateReferenceSystemSecondDefiningParameter> CoordinateReferenceSystemSecondDefiningParameter { get; set; }
        public virtual DbSet<CoordinateReferenceSystemSemiMajorAxis> CoordinateReferenceSystemSemiMajorAxis { get; set; }
        public virtual DbSet<CoordinateReferenceSystemUsesAxis> CoordinateReferenceSystemUsesAxis { get; set; }
        public virtual DbSet<CoordinateReferenceSystemUsesCartesianCs> CoordinateReferenceSystemUsesCartesianCs { get; set; }
        public virtual DbSet<CoordinateReferenceSystemUsesEllipsoid> CoordinateReferenceSystemUsesEllipsoid { get; set; }
        public virtual DbSet<CoordinateReferenceSystemUsesEllipsoidalCs> CoordinateReferenceSystemUsesEllipsoidalCs { get; set; }
        public virtual DbSet<CoordinateReferenceSystemUsesGeodeticDatum> CoordinateReferenceSystemUsesGeodeticDatum { get; set; }
        public virtual DbSet<CoordinateReferenceSystemUsesMethod> CoordinateReferenceSystemUsesMethod { get; set; }
        public virtual DbSet<CoordinateReferenceSystemUsesParameter> CoordinateReferenceSystemUsesParameter { get; set; }
        public virtual DbSet<CoordinateReferenceSystemUsesPrimeMeridian> CoordinateReferenceSystemUsesPrimeMeridian { get; set; }
        public virtual DbSet<CoordinateReferenceSystemUsesValue> CoordinateReferenceSystemUsesValue { get; set; }
        public virtual DbSet<CoordinateReferenceSystemValueOfParameter> CoordinateReferenceSystemValueOfParameter { get; set; }
        public virtual DbSet<CoordinateReferenceSystemValues> CoordinateReferenceSystemValues { get; set; }
        public virtual DbSet<CoordinateReferenceSystemVerticalCrs> CoordinateReferenceSystemVerticalCrs { get; set; }
        public virtual DbSet<DiaPlug> DiaPlug { get; set; }
        public virtual DbSet<DrillReportActivity> DrillReportActivity { get; set; }
        public virtual DbSet<DrillReportAzi> DrillReportAzi { get; set; }
        public virtual DbSet<DrillReportBitRecord> DrillReportBitRecord { get; set; }
        public virtual DbSet<DrillReportCarbonDioxide> DrillReportCarbonDioxide { get; set; }
        public virtual DbSet<DrillReportChloride> DrillReportChloride { get; set; }
        public virtual DbSet<DrillReportChokeOrificeSize> DrillReportChokeOrificeSize { get; set; }
        public virtual DbSet<DrillReportCommonData> DrillReportCommonData { get; set; }
        public virtual DbSet<DrillReportControlIncidentInfo> DrillReportControlIncidentInfo { get; set; }
        public virtual DbSet<DrillReportCoreInfo> DrillReportCoreInfo { get; set; }
        public virtual DbSet<DrillReportDefaultDatum> DrillReportDefaultDatum { get; set; }
        public virtual DbSet<DrillReportDensity> DrillReportDensity { get; set; }
        public virtual DbSet<DrillReportDensityGas> DrillReportDensityGas { get; set; }
        public virtual DbSet<DrillReportDensityHc> DrillReportDensityHc { get; set; }
        public virtual DbSet<DrillReportDensityOil> DrillReportDensityOil { get; set; }
        public virtual DbSet<DrillReportDensityWater> DrillReportDensityWater { get; set; }
        public virtual DbSet<DrillReportDiaBit> DrillReportDiaBit { get; set; }
        public virtual DbSet<DrillReportDiaCsgLast> DrillReportDiaCsgLast { get; set; }
        public virtual DbSet<DrillReportDiaHole> DrillReportDiaHole { get; set; }
        public virtual DbSet<DrillReportDiaPilot> DrillReportDiaPilot { get; set; }
        public virtual DbSet<DrillReportDistDrill> DrillReportDistDrill { get; set; }
        public virtual DbSet<DrillReportElevation> DrillReportElevation { get; set; }
        public virtual DbSet<DrillReportEquipFailureInfo> DrillReportEquipFailureInfo { get; set; }
        public virtual DbSet<DrillReportEquivalentMudWeight> DrillReportEquivalentMudWeight { get; set; }
        public virtual DbSet<DrillReportEth> DrillReportEth { get; set; }
        public virtual DbSet<DrillReportEtimLost> DrillReportEtimLost { get; set; }
        public virtual DbSet<DrillReportEtimMissProduction> DrillReportEtimMissProduction { get; set; }
        public virtual DbSet<DrillReportEtimStatic> DrillReportEtimStatic { get; set; }
        public virtual DbSet<DrillReportExtendedReport> DrillReportExtendedReport { get; set; }
        public virtual DbSet<DrillReportFlowRateGas> DrillReportFlowRateGas { get; set; }
        public virtual DbSet<DrillReportFlowRateOil> DrillReportFlowRateOil { get; set; }
        public virtual DbSet<DrillReportFlowRateWater> DrillReportFlowRateWater { get; set; }
        public virtual DbSet<DrillReportFluids> DrillReportFluids { get; set; }
        public virtual DbSet<DrillReportFormTestInfo> DrillReportFormTestInfo { get; set; }
        public virtual DbSet<DrillReportGasHigh> DrillReportGasHigh { get; set; }
        public virtual DbSet<DrillReportGasLow> DrillReportGasLow { get; set; }
        public virtual DbSet<DrillReportGasOilRatio> DrillReportGasOilRatio { get; set; }
        public virtual DbSet<DrillReportGasReadingInfo> DrillReportGasReadingInfo { get; set; }
        public virtual DbSet<DrillReportGeodeticCrs> DrillReportGeodeticCrs { get; set; }
        public virtual DbSet<DrillReportHydrogenSulfide> DrillReportHydrogenSulfide { get; set; }
        public virtual DbSet<DrillReportIbut> DrillReportIbut { get; set; }
        public virtual DbSet<DrillReportIncl> DrillReportIncl { get; set; }
        public virtual DbSet<DrillReportIpent> DrillReportIpent { get; set; }
        public virtual DbSet<DrillReportLenBarrel> DrillReportLenBarrel { get; set; }
        public virtual DbSet<DrillReportLenRecovered> DrillReportLenRecovered { get; set; }
        public virtual DbSet<DrillReportLithShowInfo> DrillReportLithShowInfo { get; set; }
        public virtual DbSet<DrillReportLogInfo> DrillReportLogInfo { get; set; }
        public virtual DbSet<DrillReportMd> DrillReportMd { get; set; }
        public virtual DbSet<DrillReportMdBit> DrillReportMdBit { get; set; }
        public virtual DbSet<DrillReportMdBottom> DrillReportMdBottom { get; set; }
        public virtual DbSet<DrillReportMdCsgLast> DrillReportMdCsgLast { get; set; }
        public virtual DbSet<DrillReportMdDiaHoleStart> DrillReportMdDiaHoleStart { get; set; }
        public virtual DbSet<DrillReportMdDiaPilotPlan> DrillReportMdDiaPilotPlan { get; set; }
        public virtual DbSet<DrillReportMdInflow> DrillReportMdInflow { get; set; }
        public virtual DbSet<DrillReportMdKickoff> DrillReportMdKickoff { get; set; }
        public virtual DbSet<DrillReportMdPlugTop> DrillReportMdPlugTop { get; set; }
        public virtual DbSet<DrillReportMdSample> DrillReportMdSample { get; set; }
        public virtual DbSet<DrillReportMdStrengthForm> DrillReportMdStrengthForm { get; set; }
        public virtual DbSet<DrillReportMdTempTool> DrillReportMdTempTool { get; set; }
        public virtual DbSet<DrillReportMdTop> DrillReportMdTop { get; set; }
        public virtual DbSet<DrillReportMeth> DrillReportMeth { get; set; }
        public virtual DbSet<DrillReportNbut> DrillReportNbut { get; set; }
        public virtual DbSet<DrillReportPerfInfo> DrillReportPerfInfo { get; set; }
        public virtual DbSet<DrillReportPorePressure> DrillReportPorePressure { get; set; }
        public virtual DbSet<DrillReportPresBopRating> DrillReportPresBopRating { get; set; }
        public virtual DbSet<DrillReportPresBottom> DrillReportPresBottom { get; set; }
        public virtual DbSet<DrillReportPresFlowing> DrillReportPresFlowing { get; set; }
        public virtual DbSet<DrillReportPresMaxChoke> DrillReportPresMaxChoke { get; set; }
        public virtual DbSet<DrillReportPresPore> DrillReportPresPore { get; set; }
        public virtual DbSet<DrillReportPresShutIn> DrillReportPresShutIn { get; set; }
        public virtual DbSet<DrillReportPresShutInCasing> DrillReportPresShutInCasing { get; set; }
        public virtual DbSet<DrillReportPresShutInDrill> DrillReportPresShutInDrill { get; set; }
        public virtual DbSet<DrillReportProp> DrillReportProp { get; set; }
        public virtual DbSet<DrillReportPv> DrillReportPv { get; set; }
        public virtual DbSet<DrillReportRecoverPc> DrillReportRecoverPc { get; set; }
        public virtual DbSet<DrillReportRigAlias> DrillReportRigAlias { get; set; }
        public virtual DbSet<DrillReportRopCurrent> DrillReportRopCurrent { get; set; }
        public virtual DbSet<DrillReportStatusInfo> DrillReportStatusInfo { get; set; }
        public virtual DbSet<DrillReportStratInfo> DrillReportStratInfo { get; set; }
        public virtual DbSet<DrillReportStrengthForm> DrillReportStrengthForm { get; set; }
        public virtual DbSet<DrillReportSurveyStation> DrillReportSurveyStation { get; set; }
        public virtual DbSet<DrillReportTempBhct> DrillReportTempBhct { get; set; }
        public virtual DbSet<DrillReportTempBhst> DrillReportTempBhst { get; set; }
        public virtual DbSet<DrillReportTempBottom> DrillReportTempBottom { get; set; }
        public virtual DbSet<DrillReportTempVis> DrillReportTempVis { get; set; }
        public virtual DbSet<DrillReportTvd> DrillReportTvd { get; set; }
        public virtual DbSet<DrillReportTvdBottom> DrillReportTvdBottom { get; set; }
        public virtual DbSet<DrillReportTvdInflow> DrillReportTvdInflow { get; set; }
        public virtual DbSet<DrillReportTvdTempTool> DrillReportTvdTempTool { get; set; }
        public virtual DbSet<DrillReportTvdTop> DrillReportTvdTop { get; set; }
        public virtual DbSet<DrillReportVolGasTotal> DrillReportVolGasTotal { get; set; }
        public virtual DbSet<DrillReportVolMudGained> DrillReportVolMudGained { get; set; }
        public virtual DbSet<DrillReportVolOilStored> DrillReportVolOilStored { get; set; }
        public virtual DbSet<DrillReportVolOilTotal> DrillReportVolOilTotal { get; set; }
        public virtual DbSet<DrillReportVolWaterTotal> DrillReportVolWaterTotal { get; set; }
        public virtual DbSet<DrillReportVolumeSample> DrillReportVolumeSample { get; set; }
        public virtual DbSet<DrillReportWaterOilRatio> DrillReportWaterOilRatio { get; set; }
        public virtual DbSet<DrillReportWellAlias> DrillReportWellAlias { get; set; }
        public virtual DbSet<DrillReportWellCr> DrillReportWellCr { get; set; }
        public virtual DbSet<DrillReportWellDatum> DrillReportWellDatum { get; set; }
        public virtual DbSet<DrillReportWellTestInfo> DrillReportWellTestInfo { get; set; }
        public virtual DbSet<DrillReportWellboreAlias> DrillReportWellboreAlias { get; set; }
        public virtual DbSet<DrillReportWellboreInfo> DrillReportWellboreInfo { get; set; }
        public virtual DbSet<DrillReportWtMuds> DrillReportWtMuds { get; set; }
        public virtual DbSet<DrillReports> DrillReports { get; set; }
        public virtual DbSet<ErdosDrillingConnections> ErdosDrillingConnections { get; set; }
        public virtual DbSet<ErdosDrillingDepthBased> ErdosDrillingDepthBased { get; set; }
        public virtual DbSet<ErdosGeneralTimeBased> ErdosGeneralTimeBased { get; set; }
        public virtual DbSet<FluidsReportAlkalinityP1> FluidsReportAlkalinityP1 { get; set; }
        public virtual DbSet<FluidsReportAlkalinityP2> FluidsReportAlkalinityP2 { get; set; }
        public virtual DbSet<FluidsReportBaritePc> FluidsReportBaritePc { get; set; }
        public virtual DbSet<FluidsReportBrinePc> FluidsReportBrinePc { get; set; }
        public virtual DbSet<FluidsReportCalcium> FluidsReportCalcium { get; set; }
        public virtual DbSet<FluidsReportCalciumChloride> FluidsReportCalciumChloride { get; set; }
        public virtual DbSet<FluidsReportChloride> FluidsReportChloride { get; set; }
        public virtual DbSet<FluidsReportCommonDatas> FluidsReportCommonDatas { get; set; }
        public virtual DbSet<FluidsReportDensity> FluidsReportDensity { get; set; }
        public virtual DbSet<FluidsReportElectStab> FluidsReportElectStab { get; set; }
        public virtual DbSet<FluidsReportFilterCakeHthp> FluidsReportFilterCakeHthp { get; set; }
        public virtual DbSet<FluidsReportFilterCakeLtlp> FluidsReportFilterCakeLtlp { get; set; }
        public virtual DbSet<FluidsReportFiltrateHthp> FluidsReportFiltrateHthp { get; set; }
        public virtual DbSet<FluidsReportFiltrateLtlp> FluidsReportFiltrateLtlp { get; set; }
        public virtual DbSet<FluidsReportFluid> FluidsReportFluid { get; set; }
        public virtual DbSet<FluidsReportGel10Min> FluidsReportGel10Min { get; set; }
        public virtual DbSet<FluidsReportGel10Sec> FluidsReportGel10Sec { get; set; }
        public virtual DbSet<FluidsReportGel30Min> FluidsReportGel30Min { get; set; }
        public virtual DbSet<FluidsReportHardnessCa> FluidsReportHardnessCa { get; set; }
        public virtual DbSet<FluidsReportLcm> FluidsReportLcm { get; set; }
        public virtual DbSet<FluidsReportLime> FluidsReportLime { get; set; }
        public virtual DbSet<FluidsReportMagnesium> FluidsReportMagnesium { get; set; }
        public virtual DbSet<FluidsReportMbt> FluidsReportMbt { get; set; }
        public virtual DbSet<FluidsReportMd> FluidsReportMd { get; set; }
        public virtual DbSet<FluidsReportMf> FluidsReportMf { get; set; }
        public virtual DbSet<FluidsReportOilCtg> FluidsReportOilCtg { get; set; }
        public virtual DbSet<FluidsReportOilPc> FluidsReportOilPc { get; set; }
        public virtual DbSet<FluidsReportPm> FluidsReportPm { get; set; }
        public virtual DbSet<FluidsReportPmFiltrate> FluidsReportPmFiltrate { get; set; }
        public virtual DbSet<FluidsReportPolymer> FluidsReportPolymer { get; set; }
        public virtual DbSet<FluidsReportPotassium> FluidsReportPotassium { get; set; }
        public virtual DbSet<FluidsReportPresHthp> FluidsReportPresHthp { get; set; }
        public virtual DbSet<FluidsReportPresRheom> FluidsReportPresRheom { get; set; }
        public virtual DbSet<FluidsReportPv> FluidsReportPv { get; set; }
        public virtual DbSet<FluidsReportRheometer> FluidsReportRheometer { get; set; }
        public virtual DbSet<FluidsReportSandPc> FluidsReportSandPc { get; set; }
        public virtual DbSet<FluidsReportSolCorPc> FluidsReportSolCorPc { get; set; }
        public virtual DbSet<FluidsReportSolidsCalcPc> FluidsReportSolidsCalcPc { get; set; }
        public virtual DbSet<FluidsReportSolidsHiGravPc> FluidsReportSolidsHiGravPc { get; set; }
        public virtual DbSet<FluidsReportSolidsLowGravPc> FluidsReportSolidsLowGravPc { get; set; }
        public virtual DbSet<FluidsReportSolidsPc> FluidsReportSolidsPc { get; set; }
        public virtual DbSet<FluidsReportSulfide> FluidsReportSulfide { get; set; }
        public virtual DbSet<FluidsReportTempHthp> FluidsReportTempHthp { get; set; }
        public virtual DbSet<FluidsReportTempPh> FluidsReportTempPh { get; set; }
        public virtual DbSet<FluidsReportTempRheom> FluidsReportTempRheom { get; set; }
        public virtual DbSet<FluidsReportTempVis> FluidsReportTempVis { get; set; }
        public virtual DbSet<FluidsReportTvd> FluidsReportTvd { get; set; }
        public virtual DbSet<FluidsReportVisFunnel> FluidsReportVisFunnel { get; set; }
        public virtual DbSet<FluidsReportWaterPc> FluidsReportWaterPc { get; set; }
        public virtual DbSet<FluidsReportYp> FluidsReportYp { get; set; }
        public virtual DbSet<FluidsReports> FluidsReports { get; set; }
        public virtual DbSet<FormationMarkerChronostratigraphics> FormationMarkerChronostratigraphics { get; set; }
        public virtual DbSet<FormationMarkerCommonDatas> FormationMarkerCommonDatas { get; set; }
        public virtual DbSet<FormationMarkerDipDirections> FormationMarkerDipDirections { get; set; }
        public virtual DbSet<FormationMarkerDips> FormationMarkerDips { get; set; }
        public virtual DbSet<FormationMarkerLithostratigraphics> FormationMarkerLithostratigraphics { get; set; }
        public virtual DbSet<FormationMarkerMdLogSamples> FormationMarkerMdLogSamples { get; set; }
        public virtual DbSet<FormationMarkerMdPrognoseds> FormationMarkerMdPrognoseds { get; set; }
        public virtual DbSet<FormationMarkerMdTopSamples> FormationMarkerMdTopSamples { get; set; }
        public virtual DbSet<FormationMarkerThicknessApparents> FormationMarkerThicknessApparents { get; set; }
        public virtual DbSet<FormationMarkerThicknessBeds> FormationMarkerThicknessBeds { get; set; }
        public virtual DbSet<FormationMarkerThicknessPerpens> FormationMarkerThicknessPerpens { get; set; }
        public virtual DbSet<FormationMarkerTvdLogSamples> FormationMarkerTvdLogSamples { get; set; }
        public virtual DbSet<FormationMarkerTvdPrognoseds> FormationMarkerTvdPrognoseds { get; set; }
        public virtual DbSet<FormationMarkerTvdTopSamples> FormationMarkerTvdTopSamples { get; set; }
        public virtual DbSet<FormationMarkers> FormationMarkers { get; set; }
        public virtual DbSet<LogCommonDatas> LogCommonDatas { get; set; }
        public virtual DbSet<LogCurveInfos> LogCurveInfos { get; set; }
        public virtual DbSet<LogDatas> LogDatas { get; set; }
        public virtual DbSet<LogEndIndex> LogEndIndex { get; set; }
        public virtual DbSet<LogMaxIndexs> LogMaxIndexs { get; set; }
        public virtual DbSet<LogMinIndexs> LogMinIndexs { get; set; }
        public virtual DbSet<LogParams> LogParams { get; set; }
        public virtual DbSet<LogSensorOffsets> LogSensorOffsets { get; set; }
        public virtual DbSet<LogStartIndex> LogStartIndex { get; set; }
        public virtual DbSet<LogStepIncrements> LogStepIncrements { get; set; }
        public virtual DbSet<Logs> Logs { get; set; }
        public virtual DbSet<MessageCommonDatas> MessageCommonDatas { get; set; }
        public virtual DbSet<MessageMd> MessageMd { get; set; }
        public virtual DbSet<MessageMdBit> MessageMdBit { get; set; }
        public virtual DbSet<MessageParam> MessageParam { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<MudLogAbundance> MudLogAbundance { get; set; }
        public virtual DbSet<MudLogAcetylene> MudLogAcetylene { get; set; }
        public virtual DbSet<MudLogCalcStab> MudLogCalcStab { get; set; }
        public virtual DbSet<MudLogCalcite> MudLogCalcite { get; set; }
        public virtual DbSet<MudLogCec> MudLogCec { get; set; }
        public virtual DbSet<MudLogChromatograph> MudLogChromatograph { get; set; }
        public virtual DbSet<MudLogChronostratigraphic> MudLogChronostratigraphic { get; set; }
        public virtual DbSet<MudLogCo2Av> MudLogCo2Av { get; set; }
        public virtual DbSet<MudLogCo2Mn> MudLogCo2Mn { get; set; }
        public virtual DbSet<MudLogCo2Mx> MudLogCo2Mx { get; set; }
        public virtual DbSet<MudLogCommonDatas> MudLogCommonDatas { get; set; }
        public virtual DbSet<MudLogCommonTime> MudLogCommonTime { get; set; }
        public virtual DbSet<MudLogDensBulk> MudLogDensBulk { get; set; }
        public virtual DbSet<MudLogDensShale> MudLogDensShale { get; set; }
        public virtual DbSet<MudLogDolomite> MudLogDolomite { get; set; }
        public virtual DbSet<MudLogEcdTdAv> MudLogEcdTdAv { get; set; }
        public virtual DbSet<MudLogEndMd> MudLogEndMd { get; set; }
        public virtual DbSet<MudLogEpentAv> MudLogEpentAv { get; set; }
        public virtual DbSet<MudLogEpentMn> MudLogEpentMn { get; set; }
        public virtual DbSet<MudLogEpentMx> MudLogEpentMx { get; set; }
        public virtual DbSet<MudLogEthAv> MudLogEthAv { get; set; }
        public virtual DbSet<MudLogEthMn> MudLogEthMn { get; set; }
        public virtual DbSet<MudLogEthMx> MudLogEthMx { get; set; }
        public virtual DbSet<MudLogEtimChromCycle> MudLogEtimChromCycle { get; set; }
        public virtual DbSet<MudLogForce> MudLogForce { get; set; }
        public virtual DbSet<MudLogGasAv> MudLogGasAv { get; set; }
        public virtual DbSet<MudLogGasBackgnd> MudLogGasBackgnd { get; set; }
        public virtual DbSet<MudLogGasConAv> MudLogGasConAv { get; set; }
        public virtual DbSet<MudLogGasConMx> MudLogGasConMx { get; set; }
        public virtual DbSet<MudLogGasPeak> MudLogGasPeak { get; set; }
        public virtual DbSet<MudLogGasTrip> MudLogGasTrip { get; set; }
        public virtual DbSet<MudLogGeologyInterval> MudLogGeologyInterval { get; set; }
        public virtual DbSet<MudLogH2sAv> MudLogH2sAv { get; set; }
        public virtual DbSet<MudLogH2sMn> MudLogH2sMn { get; set; }
        public virtual DbSet<MudLogH2sMx> MudLogH2sMx { get; set; }
        public virtual DbSet<MudLogIbutAv> MudLogIbutAv { get; set; }
        public virtual DbSet<MudLogIbutMn> MudLogIbutMn { get; set; }
        public virtual DbSet<MudLogIbutMx> MudLogIbutMx { get; set; }
        public virtual DbSet<MudLogIhexAv> MudLogIhexAv { get; set; }
        public virtual DbSet<MudLogIhexMn> MudLogIhexMn { get; set; }
        public virtual DbSet<MudLogIhexMx> MudLogIhexMx { get; set; }
        public virtual DbSet<MudLogIpentAv> MudLogIpentAv { get; set; }
        public virtual DbSet<MudLogIpentMn> MudLogIpentMn { get; set; }
        public virtual DbSet<MudLogIpentMx> MudLogIpentMx { get; set; }
        public virtual DbSet<MudLogLenPlug> MudLogLenPlug { get; set; }
        public virtual DbSet<MudLogLithology> MudLogLithology { get; set; }
        public virtual DbSet<MudLogLithostratigraphic> MudLogLithostratigraphic { get; set; }
        public virtual DbSet<MudLogMdBottom> MudLogMdBottom { get; set; }
        public virtual DbSet<MudLogMdTop> MudLogMdTop { get; set; }
        public virtual DbSet<MudLogMethAv> MudLogMethAv { get; set; }
        public virtual DbSet<MudLogMethMn> MudLogMethMn { get; set; }
        public virtual DbSet<MudLogMethMx> MudLogMethMx { get; set; }
        public virtual DbSet<MudLogMudGas> MudLogMudGas { get; set; }
        public virtual DbSet<MudLogNatFlorPc> MudLogNatFlorPc { get; set; }
        public virtual DbSet<MudLogNbutAv> MudLogNbutAv { get; set; }
        public virtual DbSet<MudLogNbutMn> MudLogNbutMn { get; set; }
        public virtual DbSet<MudLogNbutMx> MudLogNbutMx { get; set; }
        public virtual DbSet<MudLogNhexAv> MudLogNhexAv { get; set; }
        public virtual DbSet<MudLogNhexMn> MudLogNhexMn { get; set; }
        public virtual DbSet<MudLogNhexMx> MudLogNhexMx { get; set; }
        public virtual DbSet<MudLogNpentAv> MudLogNpentAv { get; set; }
        public virtual DbSet<MudLogNpentMn> MudLogNpentMn { get; set; }
        public virtual DbSet<MudLogNpentMx> MudLogNpentMx { get; set; }
        public virtual DbSet<MudLogParameter> MudLogParameter { get; set; }
        public virtual DbSet<MudLogPropAv> MudLogPropAv { get; set; }
        public virtual DbSet<MudLogPropMn> MudLogPropMn { get; set; }
        public virtual DbSet<MudLogPropMx> MudLogPropMx { get; set; }
        public virtual DbSet<MudLogQualifier> MudLogQualifier { get; set; }
        public virtual DbSet<MudLogRopAv> MudLogRopAv { get; set; }
        public virtual DbSet<MudLogRopMn> MudLogRopMn { get; set; }
        public virtual DbSet<MudLogRopMx> MudLogRopMx { get; set; }
        public virtual DbSet<MudLogRpmAv> MudLogRpmAv { get; set; }
        public virtual DbSet<MudLogShow> MudLogShow { get; set; }
        public virtual DbSet<MudLogSizeMn> MudLogSizeMn { get; set; }
        public virtual DbSet<MudLogSizeMx> MudLogSizeMx { get; set; }
        public virtual DbSet<MudLogStainPc> MudLogStainPc { get; set; }
        public virtual DbSet<MudLogStartMd> MudLogStartMd { get; set; }
        public virtual DbSet<MudLogTqAv> MudLogTqAv { get; set; }
        public virtual DbSet<MudLogTvdBase> MudLogTvdBase { get; set; }
        public virtual DbSet<MudLogTvdTop> MudLogTvdTop { get; set; }
        public virtual DbSet<MudLogWobAv> MudLogWobAv { get; set; }
        public virtual DbSet<MudLogWtMudAv> MudLogWtMudAv { get; set; }
        public virtual DbSet<MudLogWtMudIn> MudLogWtMudIn { get; set; }
        public virtual DbSet<MudLogWtMudOut> MudLogWtMudOut { get; set; }
        public virtual DbSet<MudLogs> MudLogs { get; set; }
        public virtual DbSet<ObjectGroupAcquisitionTimeZones> ObjectGroupAcquisitionTimeZones { get; set; }
        public virtual DbSet<ObjectGroupCommonDatas> ObjectGroupCommonDatas { get; set; }
        public virtual DbSet<ObjectGroupDefaultDatum> ObjectGroupDefaultDatum { get; set; }
        public virtual DbSet<ObjectGroupExtensionNameValues> ObjectGroupExtensionNameValues { get; set; }
        public virtual DbSet<ObjectGroupMd> ObjectGroupMd { get; set; }
        public virtual DbSet<ObjectGroupMemberObjects> ObjectGroupMemberObjects { get; set; }
        public virtual DbSet<ObjectGroupObjectReference> ObjectGroupObjectReference { get; set; }
        public virtual DbSet<ObjectGroupParam> ObjectGroupParam { get; set; }
        public virtual DbSet<ObjectGroupRangeMaxs> ObjectGroupRangeMaxs { get; set; }
        public virtual DbSet<ObjectGroupRangeMins> ObjectGroupRangeMins { get; set; }
        public virtual DbSet<ObjectGroupReferenceDepths> ObjectGroupReferenceDepths { get; set; }
        public virtual DbSet<ObjectGroupSequence1s> ObjectGroupSequence1s { get; set; }
        public virtual DbSet<ObjectGroupSequence2s> ObjectGroupSequence2s { get; set; }
        public virtual DbSet<ObjectGroupSequence3s> ObjectGroupSequence3s { get; set; }
        public virtual DbSet<ObjectGroupValue> ObjectGroupValue { get; set; }
        public virtual DbSet<ObjectGroups> ObjectGroups { get; set; }
        public virtual DbSet<OpsReportActivitys> OpsReportActivitys { get; set; }
        public virtual DbSet<OpsReportAlkalinityP1s> OpsReportAlkalinityP1s { get; set; }
        public virtual DbSet<OpsReportAlkalinityP2s> OpsReportAlkalinityP2s { get; set; }
        public virtual DbSet<OpsReportAmtPrecips> OpsReportAmtPrecips { get; set; }
        public virtual DbSet<OpsReportAnchorAngles> OpsReportAnchorAngles { get; set; }
        public virtual DbSet<OpsReportAnchorTensions> OpsReportAnchorTensions { get; set; }
        public virtual DbSet<OpsReportAziBottoms> OpsReportAziBottoms { get; set; }
        public virtual DbSet<OpsReportAziCurrentSeas> OpsReportAziCurrentSeas { get; set; }
        public virtual DbSet<OpsReportAziTops> OpsReportAziTops { get; set; }
        public virtual DbSet<OpsReportAziWaves> OpsReportAziWaves { get; set; }
        public virtual DbSet<OpsReportAziWinds> OpsReportAziWinds { get; set; }
        public virtual DbSet<OpsReportAzis> OpsReportAzis { get; set; }
        public virtual DbSet<OpsReportBallJointAngles> OpsReportBallJointAngles { get; set; }
        public virtual DbSet<OpsReportBallJointDirections> OpsReportBallJointDirections { get; set; }
        public virtual DbSet<OpsReportBaritePcs> OpsReportBaritePcs { get; set; }
        public virtual DbSet<OpsReportBarometricPressures> OpsReportBarometricPressures { get; set; }
        public virtual DbSet<OpsReportBiasEs> OpsReportBiasEs { get; set; }
        public virtual DbSet<OpsReportBiasNs> OpsReportBiasNs { get; set; }
        public virtual DbSet<OpsReportBiasVerts> OpsReportBiasVerts { get; set; }
        public virtual DbSet<OpsReportBrinePcs> OpsReportBrinePcs { get; set; }
        public virtual DbSet<OpsReportBulks> OpsReportBulks { get; set; }
        public virtual DbSet<OpsReportCalciumChlorides> OpsReportCalciumChlorides { get; set; }
        public virtual DbSet<OpsReportCalciums> OpsReportCalciums { get; set; }
        public virtual DbSet<OpsReportCeilingClouds> OpsReportCeilingClouds { get; set; }
        public virtual DbSet<OpsReportChlorides> OpsReportChlorides { get; set; }
        public virtual DbSet<OpsReportCorUseds> OpsReportCorUseds { get; set; }
        public virtual DbSet<OpsReportCostAmounts> OpsReportCostAmounts { get; set; }
        public virtual DbSet<OpsReportCostDayMuds> OpsReportCostDayMuds { get; set; }
        public virtual DbSet<OpsReportCostDays> OpsReportCostDays { get; set; }
        public virtual DbSet<OpsReportCostItems> OpsReportCostItems { get; set; }
        public virtual DbSet<OpsReportCostLostGrosss> OpsReportCostLostGrosss { get; set; }
        public virtual DbSet<OpsReportCostPerItems> OpsReportCostPerItems { get; set; }
        public virtual DbSet<OpsReportCtimCircs> OpsReportCtimCircs { get; set; }
        public virtual DbSet<OpsReportCtimDrillRots> OpsReportCtimDrillRots { get; set; }
        public virtual DbSet<OpsReportCtimDrillSlids> OpsReportCtimDrillSlids { get; set; }
        public virtual DbSet<OpsReportCtimHolds> OpsReportCtimHolds { get; set; }
        public virtual DbSet<OpsReportCtimReams> OpsReportCtimReams { get; set; }
        public virtual DbSet<OpsReportCtimSteerings> OpsReportCtimSteerings { get; set; }
        public virtual DbSet<OpsReportCurrentSeas> OpsReportCurrentSeas { get; set; }
        public virtual DbSet<OpsReportCutPoints> OpsReportCutPoints { get; set; }
        public virtual DbSet<OpsReportDayCosts> OpsReportDayCosts { get; set; }
        public virtual DbSet<OpsReportDaysIncFrees> OpsReportDaysIncFrees { get; set; }
        public virtual DbSet<OpsReportDensFluids> OpsReportDensFluids { get; set; }
        public virtual DbSet<OpsReportDensitys> OpsReportDensitys { get; set; }
        public virtual DbSet<OpsReportDiaCsgLasts> OpsReportDiaCsgLasts { get; set; }
        public virtual DbSet<OpsReportDiaHoles> OpsReportDiaHoles { get; set; }
        public virtual DbSet<OpsReportDipAngleUncerts> OpsReportDipAngleUncerts { get; set; }
        public virtual DbSet<OpsReportDirSensorOffsets> OpsReportDirSensorOffsets { get; set; }
        public virtual DbSet<OpsReportDispEws> OpsReportDispEws { get; set; }
        public virtual DbSet<OpsReportDispNss> OpsReportDispNss { get; set; }
        public virtual DbSet<OpsReportDispRigs> OpsReportDispRigs { get; set; }
        public virtual DbSet<OpsReportDistDrillRots> OpsReportDistDrillRots { get; set; }
        public virtual DbSet<OpsReportDistDrillSlids> OpsReportDistDrillSlids { get; set; }
        public virtual DbSet<OpsReportDistDrills> OpsReportDistDrills { get; set; }
        public virtual DbSet<OpsReportDistHolds> OpsReportDistHolds { get; set; }
        public virtual DbSet<OpsReportDistReams> OpsReportDistReams { get; set; }
        public virtual DbSet<OpsReportDistSteerings> OpsReportDistSteerings { get; set; }
        public virtual DbSet<OpsReportDlss> OpsReportDlss { get; set; }
        public virtual DbSet<OpsReportDrillingParams> OpsReportDrillingParams { get; set; }
        public virtual DbSet<OpsReportDurations> OpsReportDurations { get; set; }
        public virtual DbSet<OpsReportElectStabs> OpsReportElectStabs { get; set; }
        public virtual DbSet<OpsReportEtimCircs> OpsReportEtimCircs { get; set; }
        public virtual DbSet<OpsReportEtimDrillRots> OpsReportEtimDrillRots { get; set; }
        public virtual DbSet<OpsReportEtimDrillSlids> OpsReportEtimDrillSlids { get; set; }
        public virtual DbSet<OpsReportEtimDrills> OpsReportEtimDrills { get; set; }
        public virtual DbSet<OpsReportEtimHolds> OpsReportEtimHolds { get; set; }
        public virtual DbSet<OpsReportEtimLocs> OpsReportEtimLocs { get; set; }
        public virtual DbSet<OpsReportEtimLostGrosss> OpsReportEtimLostGrosss { get; set; }
        public virtual DbSet<OpsReportEtimOpBits> OpsReportEtimOpBits { get; set; }
        public virtual DbSet<OpsReportEtimReams> OpsReportEtimReams { get; set; }
        public virtual DbSet<OpsReportEtimSpuds> OpsReportEtimSpuds { get; set; }
        public virtual DbSet<OpsReportEtimStarts> OpsReportEtimStarts { get; set; }
        public virtual DbSet<OpsReportEtimSteerings> OpsReportEtimSteerings { get; set; }
        public virtual DbSet<OpsReportFilterCakeHthps> OpsReportFilterCakeHthps { get; set; }
        public virtual DbSet<OpsReportFilterCakeLtlps> OpsReportFilterCakeLtlps { get; set; }
        public virtual DbSet<OpsReportFiltrateHthps> OpsReportFiltrateHthps { get; set; }
        public virtual DbSet<OpsReportFiltrateLtlps> OpsReportFiltrateLtlps { get; set; }
        public virtual DbSet<OpsReportFlowrateBits> OpsReportFlowrateBits { get; set; }
        public virtual DbSet<OpsReportFlowratePumps> OpsReportFlowratePumps { get; set; }
        public virtual DbSet<OpsReportFluidDischargeds> OpsReportFluidDischargeds { get; set; }
        public virtual DbSet<OpsReportFluids> OpsReportFluids { get; set; }
        public virtual DbSet<OpsReportGel10Mins> OpsReportGel10Mins { get; set; }
        public virtual DbSet<OpsReportGel10Secs> OpsReportGel10Secs { get; set; }
        public virtual DbSet<OpsReportGel30Mins> OpsReportGel30Mins { get; set; }
        public virtual DbSet<OpsReportGravAxialAccelCors> OpsReportGravAxialAccelCors { get; set; }
        public virtual DbSet<OpsReportGravAxialRaws> OpsReportGravAxialRaws { get; set; }
        public virtual DbSet<OpsReportGravTotalFieldCalcs> OpsReportGravTotalFieldCalcs { get; set; }
        public virtual DbSet<OpsReportGravTotalUncerts> OpsReportGravTotalUncerts { get; set; }
        public virtual DbSet<OpsReportGravTran1AccelCors> OpsReportGravTran1AccelCors { get; set; }
        public virtual DbSet<OpsReportGravTran1Raws> OpsReportGravTran1Raws { get; set; }
        public virtual DbSet<OpsReportGravTran2AccelCors> OpsReportGravTran2AccelCors { get; set; }
        public virtual DbSet<OpsReportGravTran2Raws> OpsReportGravTran2Raws { get; set; }
        public virtual DbSet<OpsReportGtfs> OpsReportGtfs { get; set; }
        public virtual DbSet<OpsReportGuideBaseAngles> OpsReportGuideBaseAngles { get; set; }
        public virtual DbSet<OpsReportHardnessCas> OpsReportHardnessCas { get; set; }
        public virtual DbSet<OpsReportHkldDns> OpsReportHkldDns { get; set; }
        public virtual DbSet<OpsReportHkldRots> OpsReportHkldRots { get; set; }
        public virtual DbSet<OpsReportHkldUps> OpsReportHkldUps { get; set; }
        public virtual DbSet<OpsReportHoursRuns> OpsReportHoursRuns { get; set; }
        public virtual DbSet<OpsReportHses> OpsReportHses { get; set; }
        public virtual DbSet<OpsReportHtWaves> OpsReportHtWaves { get; set; }
        public virtual DbSet<OpsReportIdLiners> OpsReportIdLiners { get; set; }
        public virtual DbSet<OpsReportIncidents> OpsReportIncidents { get; set; }
        public virtual DbSet<OpsReportInclMns> OpsReportInclMns { get; set; }
        public virtual DbSet<OpsReportInclMxs> OpsReportInclMxs { get; set; }
        public virtual DbSet<OpsReportInclStarts> OpsReportInclStarts { get; set; }
        public virtual DbSet<OpsReportInclStops> OpsReportInclStops { get; set; }
        public virtual DbSet<OpsReportIncls> OpsReportIncls { get; set; }
        public virtual DbSet<OpsReportItemVolPerUnits> OpsReportItemVolPerUnits { get; set; }
        public virtual DbSet<OpsReportItemWtPerUnits> OpsReportItemWtPerUnits { get; set; }
        public virtual DbSet<OpsReportLatitudes> OpsReportLatitudes { get; set; }
        public virtual DbSet<OpsReportLcms> OpsReportLcms { get; set; }
        public virtual DbSet<OpsReportLenStrokes> OpsReportLenStrokes { get; set; }
        public virtual DbSet<OpsReportLimes> OpsReportLimes { get; set; }
        public virtual DbSet<OpsReportLoadLeg1s> OpsReportLoadLeg1s { get; set; }
        public virtual DbSet<OpsReportLoadLeg2s> OpsReportLoadLeg2s { get; set; }
        public virtual DbSet<OpsReportLoadLeg3s> OpsReportLoadLeg3s { get; set; }
        public virtual DbSet<OpsReportLoadLeg4s> OpsReportLoadLeg4s { get; set; }
        public virtual DbSet<OpsReportLocations> OpsReportLocations { get; set; }
        public virtual DbSet<OpsReportLongitudes> OpsReportLongitudes { get; set; }
        public virtual DbSet<OpsReportMaasps> OpsReportMaasps { get; set; }
        public virtual DbSet<OpsReportMagAxialDrlstrCors> OpsReportMagAxialDrlstrCors { get; set; }
        public virtual DbSet<OpsReportMagAxialRaws> OpsReportMagAxialRaws { get; set; }
        public virtual DbSet<OpsReportMagDipAngleCalcs> OpsReportMagDipAngleCalcs { get; set; }
        public virtual DbSet<OpsReportMagTotalFieldCalcs> OpsReportMagTotalFieldCalcs { get; set; }
        public virtual DbSet<OpsReportMagTotalUncerts> OpsReportMagTotalUncerts { get; set; }
        public virtual DbSet<OpsReportMagTran1DrlstrCors> OpsReportMagTran1DrlstrCors { get; set; }
        public virtual DbSet<OpsReportMagTran1Raws> OpsReportMagTran1Raws { get; set; }
        public virtual DbSet<OpsReportMagTran2DrlstrCors> OpsReportMagTran2DrlstrCors { get; set; }
        public virtual DbSet<OpsReportMagTran2Raws> OpsReportMagTran2Raws { get; set; }
        public virtual DbSet<OpsReportMagnesiums> OpsReportMagnesiums { get; set; }
        public virtual DbSet<OpsReportMatrixCovs> OpsReportMatrixCovs { get; set; }
        public virtual DbSet<OpsReportMbts> OpsReportMbts { get; set; }
        public virtual DbSet<OpsReportMdBitEnds> OpsReportMdBitEnds { get; set; }
        public virtual DbSet<OpsReportMdBitStarts> OpsReportMdBitStarts { get; set; }
        public virtual DbSet<OpsReportMdBits> OpsReportMdBits { get; set; }
        public virtual DbSet<OpsReportMdCsgLasts> OpsReportMdCsgLasts { get; set; }
        public virtual DbSet<OpsReportMdDeltas> OpsReportMdDeltas { get; set; }
        public virtual DbSet<OpsReportMdHoleEnds> OpsReportMdHoleEnds { get; set; }
        public virtual DbSet<OpsReportMdHoleStarts> OpsReportMdHoleStarts { get; set; }
        public virtual DbSet<OpsReportMdHoleStops> OpsReportMdHoleStops { get; set; }
        public virtual DbSet<OpsReportMdHoles> OpsReportMdHoles { get; set; }
        public virtual DbSet<OpsReportMdPlanneds> OpsReportMdPlanneds { get; set; }
        public virtual DbSet<OpsReportMdReports> OpsReportMdReports { get; set; }
        public virtual DbSet<OpsReportMds> OpsReportMds { get; set; }
        public virtual DbSet<OpsReportMeanDrafts> OpsReportMeanDrafts { get; set; }
        public virtual DbSet<OpsReportMeshXs> OpsReportMeshXs { get; set; }
        public virtual DbSet<OpsReportMeshYs> OpsReportMeshYs { get; set; }
        public virtual DbSet<OpsReportMfs> OpsReportMfs { get; set; }
        public virtual DbSet<OpsReportMtfs> OpsReportMtfs { get; set; }
        public virtual DbSet<OpsReportMudInventorys> OpsReportMudInventorys { get; set; }
        public virtual DbSet<OpsReportMudLossess> OpsReportMudLossess { get; set; }
        public virtual DbSet<OpsReportMudVolumes> OpsReportMudVolumes { get; set; }
        public virtual DbSet<OpsReportOffsetRigs> OpsReportOffsetRigs { get; set; }
        public virtual DbSet<OpsReportOilCtgs> OpsReportOilCtgs { get; set; }
        public virtual DbSet<OpsReportOilPcs> OpsReportOilPcs { get; set; }
        public virtual DbSet<OpsReportOverPulls> OpsReportOverPulls { get; set; }
        public virtual DbSet<OpsReportPcEfficiencys> OpsReportPcEfficiencys { get; set; }
        public virtual DbSet<OpsReportPcScreenCovereds> OpsReportPcScreenCovereds { get; set; }
        public virtual DbSet<OpsReportPenetrationLeg1s> OpsReportPenetrationLeg1s { get; set; }
        public virtual DbSet<OpsReportPenetrationLeg2s> OpsReportPenetrationLeg2s { get; set; }
        public virtual DbSet<OpsReportPenetrationLeg3s> OpsReportPenetrationLeg3s { get; set; }
        public virtual DbSet<OpsReportPenetrationLeg4s> OpsReportPenetrationLeg4s { get; set; }
        public virtual DbSet<OpsReportPeriodWaves> OpsReportPeriodWaves { get; set; }
        public virtual DbSet<OpsReportPersonnels> OpsReportPersonnels { get; set; }
        public virtual DbSet<OpsReportPitVolumes> OpsReportPitVolumes { get; set; }
        public virtual DbSet<OpsReportPits> OpsReportPits { get; set; }
        public virtual DbSet<OpsReportPmFiltrates> OpsReportPmFiltrates { get; set; }
        public virtual DbSet<OpsReportPms> OpsReportPms { get; set; }
        public virtual DbSet<OpsReportPolymers> OpsReportPolymers { get; set; }
        public virtual DbSet<OpsReportPowBits> OpsReportPowBits { get; set; }
        public virtual DbSet<OpsReportPresAnnulars> OpsReportPresAnnulars { get; set; }
        public virtual DbSet<OpsReportPresChokeLines> OpsReportPresChokeLines { get; set; }
        public virtual DbSet<OpsReportPresChokeMans> OpsReportPresChokeMans { get; set; }
        public virtual DbSet<OpsReportPresDiverters> OpsReportPresDiverters { get; set; }
        public virtual DbSet<OpsReportPresDropBits> OpsReportPresDropBits { get; set; }
        public virtual DbSet<OpsReportPresHthps> OpsReportPresHthps { get; set; }
        public virtual DbSet<OpsReportPresKellyHoses> OpsReportPresKellyHoses { get; set; }
        public virtual DbSet<OpsReportPresKickTols> OpsReportPresKickTols { get; set; }
        public virtual DbSet<OpsReportPresLastCsgs> OpsReportPresLastCsgs { get; set; }
        public virtual DbSet<OpsReportPresLotEmws> OpsReportPresLotEmws { get; set; }
        public virtual DbSet<OpsReportPresPumpAvs> OpsReportPresPumpAvs { get; set; }
        public virtual DbSet<OpsReportPresRamss> OpsReportPresRamss { get; set; }
        public virtual DbSet<OpsReportPresRecordeds> OpsReportPresRecordeds { get; set; }
        public virtual DbSet<OpsReportPresRheoms> OpsReportPresRheoms { get; set; }
        public virtual DbSet<OpsReportPresStdPipes> OpsReportPresStdPipes { get; set; }
        public virtual DbSet<OpsReportPressures> OpsReportPressures { get; set; }
        public virtual DbSet<OpsReportPricePerUnits> OpsReportPricePerUnits { get; set; }
        public virtual DbSet<OpsReportProjectedXs> OpsReportProjectedXs { get; set; }
        public virtual DbSet<OpsReportProjectedYs> OpsReportProjectedYs { get; set; }
        public virtual DbSet<OpsReportPumpOps> OpsReportPumpOps { get; set; }
        public virtual DbSet<OpsReportPumpOutputs> OpsReportPumpOutputs { get; set; }
        public virtual DbSet<OpsReportPumps> OpsReportPumps { get; set; }
        public virtual DbSet<OpsReportPvs> OpsReportPvs { get; set; }
        public virtual DbSet<OpsReportRateBuilds> OpsReportRateBuilds { get; set; }
        public virtual DbSet<OpsReportRateStrokes> OpsReportRateStrokes { get; set; }
        public virtual DbSet<OpsReportRateTurns> OpsReportRateTurns { get; set; }
        public virtual DbSet<OpsReportRawDatas> OpsReportRawDatas { get; set; }
        public virtual DbSet<OpsReportRheometers> OpsReportRheometers { get; set; }
        public virtual DbSet<OpsReportRigHeadings> OpsReportRigHeadings { get; set; }
        public virtual DbSet<OpsReportRigHeaves> OpsReportRigHeaves { get; set; }
        public virtual DbSet<OpsReportRigPitchAngles> OpsReportRigPitchAngles { get; set; }
        public virtual DbSet<OpsReportRigResponses> OpsReportRigResponses { get; set; }
        public virtual DbSet<OpsReportRigRollAngles> OpsReportRigRollAngles { get; set; }
        public virtual DbSet<OpsReportRigs> OpsReportRigs { get; set; }
        public virtual DbSet<OpsReportRiserAngles> OpsReportRiserAngles { get; set; }
        public virtual DbSet<OpsReportRiserDirections> OpsReportRiserDirections { get; set; }
        public virtual DbSet<OpsReportRiserTensions> OpsReportRiserTensions { get; set; }
        public virtual DbSet<OpsReportRopAvs> OpsReportRopAvs { get; set; }
        public virtual DbSet<OpsReportRopCurrents> OpsReportRopCurrents { get; set; }
        public virtual DbSet<OpsReportRopMns> OpsReportRopMns { get; set; }
        public virtual DbSet<OpsReportRopMxs> OpsReportRopMxs { get; set; }
        public virtual DbSet<OpsReportRpmAvDhs> OpsReportRpmAvDhs { get; set; }
        public virtual DbSet<OpsReportRpmAvs> OpsReportRpmAvs { get; set; }
        public virtual DbSet<OpsReportRpmMns> OpsReportRpmMns { get; set; }
        public virtual DbSet<OpsReportRpmMxs> OpsReportRpmMxs { get; set; }
        public virtual DbSet<OpsReportSagAziCors> OpsReportSagAziCors { get; set; }
        public virtual DbSet<OpsReportSagIncCors> OpsReportSagIncCors { get; set; }
        public virtual DbSet<OpsReportSandPcs> OpsReportSandPcs { get; set; }
        public virtual DbSet<OpsReportScrs> OpsReportScrs { get; set; }
        public virtual DbSet<OpsReportShakerOps> OpsReportShakerOps { get; set; }
        public virtual DbSet<OpsReportShakerScreens> OpsReportShakerScreens { get; set; }
        public virtual DbSet<OpsReportShakers> OpsReportShakers { get; set; }
        public virtual DbSet<OpsReportSlackOffs> OpsReportSlackOffs { get; set; }
        public virtual DbSet<OpsReportSolCorPcs> OpsReportSolCorPcs { get; set; }
        public virtual DbSet<OpsReportSolidsCalcPcs> OpsReportSolidsCalcPcs { get; set; }
        public virtual DbSet<OpsReportSolidsHiGravPcs> OpsReportSolidsHiGravPcs { get; set; }
        public virtual DbSet<OpsReportSolidsLowGravPcs> OpsReportSolidsLowGravPcs { get; set; }
        public virtual DbSet<OpsReportSolidsPcs> OpsReportSolidsPcs { get; set; }
        public virtual DbSet<OpsReportStnGridCorUseds> OpsReportStnGridCorUseds { get; set; }
        public virtual DbSet<OpsReportStnMagDeclUseds> OpsReportStnMagDeclUseds { get; set; }
        public virtual DbSet<OpsReportSulfides> OpsReportSulfides { get; set; }
        public virtual DbSet<OpsReportSupportCrafts> OpsReportSupportCrafts { get; set; }
        public virtual DbSet<OpsReportTempHthps> OpsReportTempHthps { get; set; }
        public virtual DbSet<OpsReportTempMudDhMxs> OpsReportTempMudDhMxs { get; set; }
        public virtual DbSet<OpsReportTempPhs> OpsReportTempPhs { get; set; }
        public virtual DbSet<OpsReportTempRheoms> OpsReportTempRheoms { get; set; }
        public virtual DbSet<OpsReportTempSurfaceMns> OpsReportTempSurfaceMns { get; set; }
        public virtual DbSet<OpsReportTempSurfaceMxs> OpsReportTempSurfaceMxs { get; set; }
        public virtual DbSet<OpsReportTempViss> OpsReportTempViss { get; set; }
        public virtual DbSet<OpsReportTempWindChills> OpsReportTempWindChills { get; set; }
        public virtual DbSet<OpsReportTempseas> OpsReportTempseas { get; set; }
        public virtual DbSet<OpsReportTotalDeckLoads> OpsReportTotalDeckLoads { get; set; }
        public virtual DbSet<OpsReportTotalTimes> OpsReportTotalTimes { get; set; }
        public virtual DbSet<OpsReportTqDhAvs> OpsReportTqDhAvs { get; set; }
        public virtual DbSet<OpsReportTqOffBotAvs> OpsReportTqOffBotAvs { get; set; }
        public virtual DbSet<OpsReportTqOnBotAvs> OpsReportTqOnBotAvs { get; set; }
        public virtual DbSet<OpsReportTqOnBotMns> OpsReportTqOnBotMns { get; set; }
        public virtual DbSet<OpsReportTqOnBotMxs> OpsReportTqOnBotMxs { get; set; }
        public virtual DbSet<OpsReportTrajectoryStations> OpsReportTrajectoryStations { get; set; }
        public virtual DbSet<OpsReportTubulars> OpsReportTubulars { get; set; }
        public virtual DbSet<OpsReportTvdCsgLasts> OpsReportTvdCsgLasts { get; set; }
        public virtual DbSet<OpsReportTvdDeltas> OpsReportTvdDeltas { get; set; }
        public virtual DbSet<OpsReportTvdHoleEnds> OpsReportTvdHoleEnds { get; set; }
        public virtual DbSet<OpsReportTvdHoleStarts> OpsReportTvdHoleStarts { get; set; }
        public virtual DbSet<OpsReportTvdLots> OpsReportTvdLots { get; set; }
        public virtual DbSet<OpsReportTvdReports> OpsReportTvdReports { get; set; }
        public virtual DbSet<OpsReportTvds> OpsReportTvds { get; set; }
        public virtual DbSet<OpsReportValids> OpsReportValids { get; set; }
        public virtual DbSet<OpsReportVariableDeckLoads> OpsReportVariableDeckLoads { get; set; }
        public virtual DbSet<OpsReportVarianceEes> OpsReportVarianceEes { get; set; }
        public virtual DbSet<OpsReportVarianceEverts> OpsReportVarianceEverts { get; set; }
        public virtual DbSet<OpsReportVarianceNes> OpsReportVarianceNes { get; set; }
        public virtual DbSet<OpsReportVarianceNns> OpsReportVarianceNns { get; set; }
        public virtual DbSet<OpsReportVarianceNverts> OpsReportVarianceNverts { get; set; }
        public virtual DbSet<OpsReportVarianceVertVerts> OpsReportVarianceVertVerts { get; set; }
        public virtual DbSet<OpsReportVelNozzleAvs> OpsReportVelNozzleAvs { get; set; }
        public virtual DbSet<OpsReportVelWinds> OpsReportVelWinds { get; set; }
        public virtual DbSet<OpsReportVertSects> OpsReportVertSects { get; set; }
        public virtual DbSet<OpsReportVisFunnels> OpsReportVisFunnels { get; set; }
        public virtual DbSet<OpsReportVisibilitys> OpsReportVisibilitys { get; set; }
        public virtual DbSet<OpsReportVolCtgDischargeds> OpsReportVolCtgDischargeds { get; set; }
        public virtual DbSet<OpsReportVolKickTols> OpsReportVolKickTols { get; set; }
        public virtual DbSet<OpsReportVolLostAbandonHoles> OpsReportVolLostAbandonHoles { get; set; }
        public virtual DbSet<OpsReportVolLostBhdCsgHoles> OpsReportVolLostBhdCsgHoles { get; set; }
        public virtual DbSet<OpsReportVolLostCircHoles> OpsReportVolLostCircHoles { get; set; }
        public virtual DbSet<OpsReportVolLostCmtHoles> OpsReportVolLostCmtHoles { get; set; }
        public virtual DbSet<OpsReportVolLostCsgHoles> OpsReportVolLostCsgHoles { get; set; }
        public virtual DbSet<OpsReportVolLostMudCleanerSurfs> OpsReportVolLostMudCleanerSurfs { get; set; }
        public virtual DbSet<OpsReportVolLostOtherHoles> OpsReportVolLostOtherHoles { get; set; }
        public virtual DbSet<OpsReportVolLostOtherSurfs> OpsReportVolLostOtherSurfs { get; set; }
        public virtual DbSet<OpsReportVolLostPitsSurfs> OpsReportVolLostPitsSurfs { get; set; }
        public virtual DbSet<OpsReportVolLostShakerSurfs> OpsReportVolLostShakerSurfs { get; set; }
        public virtual DbSet<OpsReportVolLostTrippingSurfs> OpsReportVolLostTrippingSurfs { get; set; }
        public virtual DbSet<OpsReportVolMudBuilts> OpsReportVolMudBuilts { get; set; }
        public virtual DbSet<OpsReportVolMudCasings> OpsReportVolMudCasings { get; set; }
        public virtual DbSet<OpsReportVolMudDumpeds> OpsReportVolMudDumpeds { get; set; }
        public virtual DbSet<OpsReportVolMudHoles> OpsReportVolMudHoles { get; set; }
        public virtual DbSet<OpsReportVolMudReceiveds> OpsReportVolMudReceiveds { get; set; }
        public virtual DbSet<OpsReportVolMudReturneds> OpsReportVolMudReturneds { get; set; }
        public virtual DbSet<OpsReportVolMudRisers> OpsReportVolMudRisers { get; set; }
        public virtual DbSet<OpsReportVolMudStrings> OpsReportVolMudStrings { get; set; }
        public virtual DbSet<OpsReportVolOilCtgDischarges> OpsReportVolOilCtgDischarges { get; set; }
        public virtual DbSet<OpsReportVolPits> OpsReportVolPits { get; set; }
        public virtual DbSet<OpsReportVolTotMudEnds> OpsReportVolTotMudEnds { get; set; }
        public virtual DbSet<OpsReportVolTotMudLostHoles> OpsReportVolTotMudLostHoles { get; set; }
        public virtual DbSet<OpsReportVolTotMudLostSurfs> OpsReportVolTotMudLostSurfs { get; set; }
        public virtual DbSet<OpsReportVolTotMudStarts> OpsReportVolTotMudStarts { get; set; }
        public virtual DbSet<OpsReportWasteDischargeds> OpsReportWasteDischargeds { get; set; }
        public virtual DbSet<OpsReportWaterPcs> OpsReportWaterPcs { get; set; }
        public virtual DbSet<OpsReportWeathers> OpsReportWeathers { get; set; }
        public virtual DbSet<OpsReportWellCrss> OpsReportWellCrss { get; set; }
        public virtual DbSet<OpsReportWobAvDhs> OpsReportWobAvDhs { get; set; }
        public virtual DbSet<OpsReportWobAvs> OpsReportWobAvs { get; set; }
        public virtual DbSet<OpsReportWobMns> OpsReportWobMns { get; set; }
        public virtual DbSet<OpsReportWobMxs> OpsReportWobMxs { get; set; }
        public virtual DbSet<OpsReportWtAboveJars> OpsReportWtAboveJars { get; set; }
        public virtual DbSet<OpsReportWtBelowJars> OpsReportWtBelowJars { get; set; }
        public virtual DbSet<OpsReportWtMuds> OpsReportWtMuds { get; set; }
        public virtual DbSet<OpsReportYps> OpsReportYps { get; set; }
        public virtual DbSet<OpsReports> OpsReports { get; set; }
        public virtual DbSet<OpsReportsCommonDatas> OpsReportsCommonDatas { get; set; }
        public virtual DbSet<RigAirGaps> RigAirGaps { get; set; }
        public virtual DbSet<RigAreaSeparatorFlows> RigAreaSeparatorFlows { get; set; }
        public virtual DbSet<RigBopComponents> RigBopComponents { get; set; }
        public virtual DbSet<RigBops> RigBops { get; set; }
        public virtual DbSet<RigCapAccFluids> RigCapAccFluids { get; set; }
        public virtual DbSet<RigCapBlowdowns> RigCapBlowdowns { get; set; }
        public virtual DbSet<RigCapBulkCements> RigCapBulkCements { get; set; }
        public virtual DbSet<RigCapBulkMuds> RigCapBulkMuds { get; set; }
        public virtual DbSet<RigCapDrillWaters> RigCapDrillWaters { get; set; }
        public virtual DbSet<RigCapFlows> RigCapFlows { get; set; }
        public virtual DbSet<RigCapFuels> RigCapFuels { get; set; }
        public virtual DbSet<RigCapGasSeps> RigCapGasSeps { get; set; }
        public virtual DbSet<RigCapLiquidMuds> RigCapLiquidMuds { get; set; }
        public virtual DbSet<RigCapMxs> RigCapMxs { get; set; }
        public virtual DbSet<RigCapPotableWaters> RigCapPotableWaters { get; set; }
        public virtual DbSet<RigCapWindDerricks> RigCapWindDerricks { get; set; }
        public virtual DbSet<RigCentrifuges> RigCentrifuges { get; set; }
        public virtual DbSet<RigCommonDatas> RigCommonDatas { get; set; }
        public virtual DbSet<RigDegassers> RigDegassers { get; set; }
        public virtual DbSet<RigDiaCloseMns> RigDiaCloseMns { get; set; }
        public virtual DbSet<RigDiaCloseMxs> RigDiaCloseMxs { get; set; }
        public virtual DbSet<RigDiaDiverters> RigDiaDiverters { get; set; }
        public virtual DbSet<RigDisplacements> RigDisplacements { get; set; }
        public virtual DbSet<RigEffs> RigEffs { get; set; }
        public virtual DbSet<RigHeaveMxs> RigHeaveMxs { get; set; }
        public virtual DbSet<RigHeights> RigHeights { get; set; }
        public virtual DbSet<RigHtDerricks> RigHtDerricks { get; set; }
        public virtual DbSet<RigHtFlanges> RigHtFlanges { get; set; }
        public virtual DbSet<RigHtInjStks> RigHtInjStks { get; set; }
        public virtual DbSet<RigHtMudSeals> RigHtMudSeals { get; set; }
        public virtual DbSet<RigHtTopStks> RigHtTopStks { get; set; }
        public virtual DbSet<RigHydrocyclones> RigHydrocyclones { get; set; }
        public virtual DbSet<RigIdBoosterLines> RigIdBoosterLines { get; set; }
        public virtual DbSet<RigIdChkLines> RigIdChkLines { get; set; }
        public virtual DbSet<RigIdDischargeLines> RigIdDischargeLines { get; set; }
        public virtual DbSet<RigIdHoses> RigIdHoses { get; set; }
        public virtual DbSet<RigIdInlets> RigIdInlets { get; set; }
        public virtual DbSet<RigIdKellys> RigIdKellys { get; set; }
        public virtual DbSet<RigIdKillLines> RigIdKillLines { get; set; }
        public virtual DbSet<RigIdLiners> RigIdLiners { get; set; }
        public virtual DbSet<RigIdPassThrus> RigIdPassThrus { get; set; }
        public virtual DbSet<RigIdStandpipes> RigIdStandpipes { get; set; }
        public virtual DbSet<RigIdSurfLines> RigIdSurfLines { get; set; }
        public virtual DbSet<RigIdSwivels> RigIdSwivels { get; set; }
        public virtual DbSet<RigIdTopStks> RigIdTopStks { get; set; }
        public virtual DbSet<RigIdVentLines> RigIdVentLines { get; set; }
        public virtual DbSet<RigIds> RigIds { get; set; }
        public virtual DbSet<RigLenBoosterLines> RigLenBoosterLines { get; set; }
        public virtual DbSet<RigLenChkLines> RigLenChkLines { get; set; }
        public virtual DbSet<RigLenDischargeLines> RigLenDischargeLines { get; set; }
        public virtual DbSet<RigLenHoses> RigLenHoses { get; set; }
        public virtual DbSet<RigLenKellys> RigLenKellys { get; set; }
        public virtual DbSet<RigLenKillLines> RigLenKillLines { get; set; }
        public virtual DbSet<RigLenReels> RigLenReels { get; set; }
        public virtual DbSet<RigLenStandpipes> RigLenStandpipes { get; set; }
        public virtual DbSet<RigLenStrokes> RigLenStrokes { get; set; }
        public virtual DbSet<RigLenSurfLines> RigLenSurfLines { get; set; }
        public virtual DbSet<RigLenSwivels> RigLenSwivels { get; set; }
        public virtual DbSet<RigLenUmbilicals> RigLenUmbilicals { get; set; }
        public virtual DbSet<RigLenVentLines> RigLenVentLines { get; set; }
        public virtual DbSet<RigLens> RigLens { get; set; }
        public virtual DbSet<RigMotionCompensationMns> RigMotionCompensationMns { get; set; }
        public virtual DbSet<RigMotionCompensationMxs> RigMotionCompensationMxs { get; set; }
        public virtual DbSet<RigOdBoosterLines> RigOdBoosterLines { get; set; }
        public virtual DbSet<RigOdChkLines> RigOdChkLines { get; set; }
        public virtual DbSet<RigOdCores> RigOdCores { get; set; }
        public virtual DbSet<RigOdKillLines> RigOdKillLines { get; set; }
        public virtual DbSet<RigOdReels> RigOdReels { get; set; }
        public virtual DbSet<RigOdRods> RigOdRods { get; set; }
        public virtual DbSet<RigOdSurfLines> RigOdSurfLines { get; set; }
        public virtual DbSet<RigOdUmbilicals> RigOdUmbilicals { get; set; }
        public virtual DbSet<RigPits> RigPits { get; set; }
        public virtual DbSet<RigPowHydMxs> RigPowHydMxs { get; set; }
        public virtual DbSet<RigPowMechMxs> RigPowMechMxs { get; set; }
        public virtual DbSet<RigPowerDrawWork> RigPowerDrawWork { get; set; }
        public virtual DbSet<RigPresAccOpRatings> RigPresAccOpRatings { get; set; }
        public virtual DbSet<RigPresAccPreCharges> RigPresAccPreCharges { get; set; }
        public virtual DbSet<RigPresBopRatings> RigPresBopRatings { get; set; }
        public virtual DbSet<RigPresChokeManifolds> RigPresChokeManifolds { get; set; }
        public virtual DbSet<RigPresDamps> RigPresDamps { get; set; }
        public virtual DbSet<RigPresMxs> RigPresMxs { get; set; }
        public virtual DbSet<RigPresRatings> RigPresRatings { get; set; }
        public virtual DbSet<RigPresWorkDiverters> RigPresWorkDiverters { get; set; }
        public virtual DbSet<RigPresWorks> RigPresWorks { get; set; }
        public virtual DbSet<RigPumps> RigPumps { get; set; }
        public virtual DbSet<RigRatingBlocks> RigRatingBlocks { get; set; }
        public virtual DbSet<RigRatingDerricks> RigRatingDerricks { get; set; }
        public virtual DbSet<RigRatingDrawWork> RigRatingDrawWork { get; set; }
        public virtual DbSet<RigRatingDrillDepths> RigRatingDrillDepths { get; set; }
        public virtual DbSet<RigRatingHklds> RigRatingHklds { get; set; }
        public virtual DbSet<RigRatingHooks> RigRatingHooks { get; set; }
        public virtual DbSet<RigRatingRotSystems> RigRatingRotSystems { get; set; }
        public virtual DbSet<RigRatingSwivels> RigRatingSwivels { get; set; }
        public virtual DbSet<RigRatingTqRotSy> RigRatingTqRotSy { get; set; }
        public virtual DbSet<RigRatingWaterDepths> RigRatingWaterDepths { get; set; }
        public virtual DbSet<RigRiserAngleLimits> RigRiserAngleLimits { get; set; }
        public virtual DbSet<RigRotSizeOpenings> RigRotSizeOpenings { get; set; }
        public virtual DbSet<RigShakers> RigShakers { get; set; }
        public virtual DbSet<RigSizeBopSyss> RigSizeBopSyss { get; set; }
        public virtual DbSet<RigSizeConnectionBops> RigSizeConnectionBops { get; set; }
        public virtual DbSet<RigSizeDrillLines> RigSizeDrillLines { get; set; }
        public virtual DbSet<RigSizeMeshMns> RigSizeMeshMns { get; set; }
        public virtual DbSet<RigSpmMxs> RigSpmMxs { get; set; }
        public virtual DbSet<RigStrokeMotionCompensations> RigStrokeMotionCompensations { get; set; }
        public virtual DbSet<RigSurfaceEquipments> RigSurfaceEquipments { get; set; }
        public virtual DbSet<RigTempRatings> RigTempRatings { get; set; }
        public virtual DbSet<RigVarDeckLdMxs> RigVarDeckLdMxs { get; set; }
        public virtual DbSet<RigVdlStorms> RigVdlStorms { get; set; }
        public virtual DbSet<RigVolAccPreCharges> RigVolAccPreCharges { get; set; }
        public virtual DbSet<RigVolDamps> RigVolDamps { get; set; }
        public virtual DbSet<RigWidReelWraps> RigWidReelWraps { get; set; }
        public virtual DbSet<RigWorkstationRegister> RigWorkstationRegister { get; set; }
        public virtual DbSet<RigWtBlocks> RigWtBlocks { get; set; }
        public virtual DbSet<Rigs> Rigs { get; set; }
        public virtual DbSet<RiskDiaHole> RiskDiaHole { get; set; }
        public virtual DbSet<RiskMdHoleEnds> RiskMdHoleEnds { get; set; }
        public virtual DbSet<RiskMdHoleStarts> RiskMdHoleStarts { get; set; }
        public virtual DbSet<RiskObjectReferences> RiskObjectReferences { get; set; }
        public virtual DbSet<RiskTvdHoleEnds> RiskTvdHoleEnds { get; set; }
        public virtual DbSet<RiskTvdHoleStarts> RiskTvdHoleStarts { get; set; }
        public virtual DbSet<Risks> Risks { get; set; }
        public virtual DbSet<SideWallCoreAbundance> SideWallCoreAbundance { get; set; }
        public virtual DbSet<SideWallCoreCommonData> SideWallCoreCommonData { get; set; }
        public virtual DbSet<SideWallCoreDensShale> SideWallCoreDensShale { get; set; }
        public virtual DbSet<SideWallCoreDiaHole> SideWallCoreDiaHole { get; set; }
        public virtual DbSet<SideWallCoreLithology> SideWallCoreLithology { get; set; }
        public virtual DbSet<SideWallCoreNatFlorPc> SideWallCoreNatFlorPc { get; set; }
        public virtual DbSet<SideWallCoreQualifier> SideWallCoreQualifier { get; set; }
        public virtual DbSet<SideWallCoreShow> SideWallCoreShow { get; set; }
        public virtual DbSet<SideWallCoreStainPc> SideWallCoreStainPc { get; set; }
        public virtual DbSet<SideWallCoreSwcSample> SideWallCoreSwcSample { get; set; }
        public virtual DbSet<SideWallLithPcs> SideWallLithPcs { get; set; }
        public virtual DbSet<SideWallMd> SideWallMd { get; set; }
        public virtual DbSet<SideWallMdCore> SideWallMdCore { get; set; }
        public virtual DbSet<SideWallMdToolReference> SideWallMdToolReference { get; set; }
        public virtual DbSet<SidewallCores> SidewallCores { get; set; }
        public virtual DbSet<StimJobAcidVols> StimJobAcidVols { get; set; }
        public virtual DbSet<StimJobAcidVolumes> StimJobAcidVolumes { get; set; }
        public virtual DbSet<StimJobAdditives> StimJobAdditives { get; set; }
        public virtual DbSet<StimJobAveragePresBottomholes> StimJobAveragePresBottomholes { get; set; }
        public virtual DbSet<StimJobAveragePresSurfaces> StimJobAveragePresSurfaces { get; set; }
        public virtual DbSet<StimJobAveragePress> StimJobAveragePress { get; set; }
        public virtual DbSet<StimJobAvgAcidRates> StimJobAvgAcidRates { get; set; }
        public virtual DbSet<StimJobAvgBaseFluidQualitys> StimJobAvgBaseFluidQualitys { get; set; }
        public virtual DbSet<StimJobAvgBaseFluidRates> StimJobAvgBaseFluidRates { get; set; }
        public virtual DbSet<StimJobAvgBaseFluidReturnRates> StimJobAvgBaseFluidReturnRates { get; set; }
        public virtual DbSet<StimJobAvgBottomholeRates> StimJobAvgBottomholeRates { get; set; }
        public virtual DbSet<StimJobAvgBottomholeTreatmentPress> StimJobAvgBottomholeTreatmentPress { get; set; }
        public virtual DbSet<StimJobAvgBottomholeTreatmentRates> StimJobAvgBottomholeTreatmentRates { get; set; }
        public virtual DbSet<StimJobAvgCo2baseFluidQualitys> StimJobAvgCo2baseFluidQualitys { get; set; }
        public virtual DbSet<StimJobAvgCo2liquidRates> StimJobAvgCo2liquidRates { get; set; }
        public virtual DbSet<StimJobAvgCo2rates> StimJobAvgCo2rates { get; set; }
        public virtual DbSet<StimJobAvgConductivitys> StimJobAvgConductivitys { get; set; }
        public virtual DbSet<StimJobAvgFractureWidths> StimJobAvgFractureWidths { get; set; }
        public virtual DbSet<StimJobAvgGelRates> StimJobAvgGelRates { get; set; }
        public virtual DbSet<StimJobAvgHydraulicPowers> StimJobAvgHydraulicPowers { get; set; }
        public virtual DbSet<StimJobAvgInternalPhaseFractions> StimJobAvgInternalPhaseFractions { get; set; }
        public virtual DbSet<StimJobAvgJobPress> StimJobAvgJobPress { get; set; }
        public virtual DbSet<StimJobAvgN2baseFluidQualitys> StimJobAvgN2baseFluidQualitys { get; set; }
        public virtual DbSet<StimJobAvgN2stdRates> StimJobAvgN2stdRates { get; set; }
        public virtual DbSet<StimJobAvgOilRates> StimJobAvgOilRates { get; set; }
        public virtual DbSet<StimJobAvgPmaxPacPress> StimJobAvgPmaxPacPress { get; set; }
        public virtual DbSet<StimJobAvgPmaxWeaklinkPress> StimJobAvgPmaxWeaklinkPress { get; set; }
        public virtual DbSet<StimJobAvgPresCasings> StimJobAvgPresCasings { get; set; }
        public virtual DbSet<StimJobAvgPresTubings> StimJobAvgPresTubings { get; set; }
        public virtual DbSet<StimJobAvgPropConcs> StimJobAvgPropConcs { get; set; }
        public virtual DbSet<StimJobAvgProppantConcBottomholes> StimJobAvgProppantConcBottomholes { get; set; }
        public virtual DbSet<StimJobAvgProppantConcSurfaces> StimJobAvgProppantConcSurfaces { get; set; }
        public virtual DbSet<StimJobAvgPumpRateBottomholes> StimJobAvgPumpRateBottomholes { get; set; }
        public virtual DbSet<StimJobAvgRateSurfaceCo2s> StimJobAvgRateSurfaceCo2s { get; set; }
        public virtual DbSet<StimJobAvgRateSurfaceLiquids> StimJobAvgRateSurfaceLiquids { get; set; }
        public virtual DbSet<StimJobAvgSlurryPropConcs> StimJobAvgSlurryPropConcs { get; set; }
        public virtual DbSet<StimJobAvgSlurryRates> StimJobAvgSlurryRates { get; set; }
        public virtual DbSet<StimJobAvgSlurryReturnRates> StimJobAvgSlurryReturnRates { get; set; }
        public virtual DbSet<StimJobAvgStdRateSurfaceN2s> StimJobAvgStdRateSurfaceN2s { get; set; }
        public virtual DbSet<StimJobAvgTemperatures> StimJobAvgTemperatures { get; set; }
        public virtual DbSet<StimJobAvgTreatPress> StimJobAvgTreatPress { get; set; }
        public virtual DbSet<StimJobAvgWellheadRates> StimJobAvgWellheadRates { get; set; }
        public virtual DbSet<StimJobBaseFluidBypassVols> StimJobBaseFluidBypassVols { get; set; }
        public virtual DbSet<StimJobBaseFluidVols> StimJobBaseFluidVols { get; set; }
        public virtual DbSet<StimJobBottomholeFluidDensitys> StimJobBottomholeFluidDensitys { get; set; }
        public virtual DbSet<StimJobBottomholeHydrostaticPress> StimJobBottomholeHydrostaticPress { get; set; }
        public virtual DbSet<StimJobBottomholeRates> StimJobBottomholeRates { get; set; }
        public virtual DbSet<StimJobBottomholeStaticTemperatures> StimJobBottomholeStaticTemperatures { get; set; }
        public virtual DbSet<StimJobBottomholeTemperatures> StimJobBottomholeTemperatures { get; set; }
        public virtual DbSet<StimJobBreakDownPress> StimJobBreakDownPress { get; set; }
        public virtual DbSet<StimJobBubblePointPress> StimJobBubblePointPress { get; set; }
        public virtual DbSet<StimJobClosureDurations> StimJobClosureDurations { get; set; }
        public virtual DbSet<StimJobClosurePress> StimJobClosurePress { get; set; }
        public virtual DbSet<StimJobDensityPerforations> StimJobDensityPerforations { get; set; }
        public virtual DbSet<StimJobDiameterEntryHoles> StimJobDiameterEntryHoles { get; set; }
        public virtual DbSet<StimJobEndFoamRateCo2s> StimJobEndFoamRateCo2s { get; set; }
        public virtual DbSet<StimJobEndFoamRateN2s> StimJobEndFoamRateN2s { get; set; }
        public virtual DbSet<StimJobEndPdlDurations> StimJobEndPdlDurations { get; set; }
        public virtual DbSet<StimJobEndPresBottomholes> StimJobEndPresBottomholes { get; set; }
        public virtual DbSet<StimJobEndPresSurfaces> StimJobEndPresSurfaces { get; set; }
        public virtual DbSet<StimJobEndProppantConcBottomholes> StimJobEndProppantConcBottomholes { get; set; }
        public virtual DbSet<StimJobEndProppantConcSurfaces> StimJobEndProppantConcSurfaces { get; set; }
        public virtual DbSet<StimJobEndPumpRateBottomholes> StimJobEndPumpRateBottomholes { get; set; }
        public virtual DbSet<StimJobEndRateSurfaceCo2s> StimJobEndRateSurfaceCo2s { get; set; }
        public virtual DbSet<StimJobEndRateSurfaceLiquids> StimJobEndRateSurfaceLiquids { get; set; }
        public virtual DbSet<StimJobEndStdRateSurfaceN2s> StimJobEndStdRateSurfaceN2s { get; set; }
        public virtual DbSet<StimJobEntryFrictions> StimJobEntryFrictions { get; set; }
        public virtual DbSet<StimJobFinalFractureGradients> StimJobFinalFractureGradients { get; set; }
        public virtual DbSet<StimJobFlowBackPress> StimJobFlowBackPress { get; set; }
        public virtual DbSet<StimJobFlowBackRates> StimJobFlowBackRates { get; set; }
        public virtual DbSet<StimJobFlowBackVolumes> StimJobFlowBackVolumes { get; set; }
        public virtual DbSet<StimJobFlowPaths> StimJobFlowPaths { get; set; }
        public virtual DbSet<StimJobFluidCompressibilitys> StimJobFluidCompressibilitys { get; set; }
        public virtual DbSet<StimJobFluidDensitys> StimJobFluidDensitys { get; set; }
        public virtual DbSet<StimJobFluidEfficiencyTests> StimJobFluidEfficiencyTests { get; set; }
        public virtual DbSet<StimJobFluidEfficiencys> StimJobFluidEfficiencys { get; set; }
        public virtual DbSet<StimJobFluidSpecificHeats> StimJobFluidSpecificHeats { get; set; }
        public virtual DbSet<StimJobFluidThermalConductivitys> StimJobFluidThermalConductivitys { get; set; }
        public virtual DbSet<StimJobFluidThermalExpansionCoefficients> StimJobFluidThermalExpansionCoefficients { get; set; }
        public virtual DbSet<StimJobFluidVolBases> StimJobFluidVolBases { get; set; }
        public virtual DbSet<StimJobFluidVolSlurrys> StimJobFluidVolSlurrys { get; set; }
        public virtual DbSet<StimJobFluidVols> StimJobFluidVols { get; set; }
        public virtual DbSet<StimJobFoamQualitys> StimJobFoamQualitys { get; set; }
        public virtual DbSet<StimJobFormationPermeabilitys> StimJobFormationPermeabilitys { get; set; }
        public virtual DbSet<StimJobFormationPorositys> StimJobFormationPorositys { get; set; }
        public virtual DbSet<StimJobFormationProppantMasss> StimJobFormationProppantMasss { get; set; }
        public virtual DbSet<StimJobFractureCloseDurations> StimJobFractureCloseDurations { get; set; }
        public virtual DbSet<StimJobFractureClosePress> StimJobFractureClosePress { get; set; }
        public virtual DbSet<StimJobFractureExtensionPress> StimJobFractureExtensionPress { get; set; }
        public virtual DbSet<StimJobFractureGradients> StimJobFractureGradients { get; set; }
        public virtual DbSet<StimJobFractureLengths> StimJobFractureLengths { get; set; }
        public virtual DbSet<StimJobFractureWidths> StimJobFractureWidths { get; set; }
        public virtual DbSet<StimJobFrictionPress> StimJobFrictionPress { get; set; }
        public virtual DbSet<StimJobGelVols> StimJobGelVols { get; set; }
        public virtual DbSet<StimJobGelVolumes> StimJobGelVolumes { get; set; }
        public virtual DbSet<StimJobGrossPayThicknesss> StimJobGrossPayThicknesss { get; set; }
        public virtual DbSet<StimJobHhpOrderedCo2s> StimJobHhpOrderedCo2s { get; set; }
        public virtual DbSet<StimJobHhpOrderedFluids> StimJobHhpOrderedFluids { get; set; }
        public virtual DbSet<StimJobHhpOrdereds> StimJobHhpOrdereds { get; set; }
        public virtual DbSet<StimJobHhpUsedCo2s> StimJobHhpUsedCo2s { get; set; }
        public virtual DbSet<StimJobHhpUsedFluids> StimJobHhpUsedFluids { get; set; }
        public virtual DbSet<StimJobHhpUseds> StimJobHhpUseds { get; set; }
        public virtual DbSet<StimJobIds> StimJobIds { get; set; }
        public virtual DbSet<StimJobInitialShutinPress> StimJobInitialShutinPress { get; set; }
        public virtual DbSet<StimJobJobEvents> StimJobJobEvents { get; set; }
        public virtual DbSet<StimJobJobIntervals> StimJobJobIntervals { get; set; }
        public virtual DbSet<StimJobJobStages> StimJobJobStages { get; set; }
        public virtual DbSet<StimJobLithFormationPermeabilitys> StimJobLithFormationPermeabilitys { get; set; }
        public virtual DbSet<StimJobLithNetPayThicknesss> StimJobLithNetPayThicknesss { get; set; }
        public virtual DbSet<StimJobLithPoissonsRatios> StimJobLithPoissonsRatios { get; set; }
        public virtual DbSet<StimJobLithPorePress> StimJobLithPorePress { get; set; }
        public virtual DbSet<StimJobLithYoungsModuluss> StimJobLithYoungsModuluss { get; set; }
        public virtual DbSet<StimJobMassCo2s> StimJobMassCo2s { get; set; }
        public virtual DbSet<StimJobMasss> StimJobMasss { get; set; }
        public virtual DbSet<StimJobMaxAcidRates> StimJobMaxAcidRates { get; set; }
        public virtual DbSet<StimJobMaxCo2liquidRates> StimJobMaxCo2liquidRates { get; set; }
        public virtual DbSet<StimJobMaxFluidRateAnnuluss> StimJobMaxFluidRateAnnuluss { get; set; }
        public virtual DbSet<StimJobMaxFluidRateTubings> StimJobMaxFluidRateTubings { get; set; }
        public virtual DbSet<StimJobMaxFluidRates> StimJobMaxFluidRates { get; set; }
        public virtual DbSet<StimJobMaxGelRates> StimJobMaxGelRates { get; set; }
        public virtual DbSet<StimJobMaxJobPress> StimJobMaxJobPress { get; set; }
        public virtual DbSet<StimJobMaxN2stdRates> StimJobMaxN2stdRates { get; set; }
        public virtual DbSet<StimJobMaxOilRates> StimJobMaxOilRates { get; set; }
        public virtual DbSet<StimJobMaxPmaxPacPress> StimJobMaxPmaxPacPress { get; set; }
        public virtual DbSet<StimJobMaxPmaxWeaklinkPress> StimJobMaxPmaxWeaklinkPress { get; set; }
        public virtual DbSet<StimJobMaxPresAnnuluss> StimJobMaxPresAnnuluss { get; set; }
        public virtual DbSet<StimJobMaxPresTubings> StimJobMaxPresTubings { get; set; }
        public virtual DbSet<StimJobMaxPress> StimJobMaxPress { get; set; }
        public virtual DbSet<StimJobMaxPropConcs> StimJobMaxPropConcs { get; set; }
        public virtual DbSet<StimJobMaxProppantConcBottomholes> StimJobMaxProppantConcBottomholes { get; set; }
        public virtual DbSet<StimJobMaxProppantConcSurfaces> StimJobMaxProppantConcSurfaces { get; set; }
        public virtual DbSet<StimJobMaxSlurryPropConcs> StimJobMaxSlurryPropConcs { get; set; }
        public virtual DbSet<StimJobMaxSlurryRates> StimJobMaxSlurryRates { get; set; }
        public virtual DbSet<StimJobMaxTreatmentPress> StimJobMaxTreatmentPress { get; set; }
        public virtual DbSet<StimJobMaxWellheadRates> StimJobMaxWellheadRates { get; set; }
        public virtual DbSet<StimJobMdBottomholes> StimJobMdBottomholes { get; set; }
        public virtual DbSet<StimJobMdBottoms> StimJobMdBottoms { get; set; }
        public virtual DbSet<StimJobMdFormationBottoms> StimJobMdFormationBottoms { get; set; }
        public virtual DbSet<StimJobMdFormationTops> StimJobMdFormationTops { get; set; }
        public virtual DbSet<StimJobMdGrossPayBottoms> StimJobMdGrossPayBottoms { get; set; }
        public virtual DbSet<StimJobMdGrossPayTops> StimJobMdGrossPayTops { get; set; }
        public virtual DbSet<StimJobMdLithBottoms> StimJobMdLithBottoms { get; set; }
        public virtual DbSet<StimJobMdLithTops> StimJobMdLithTops { get; set; }
        public virtual DbSet<StimJobMdMidPerforations> StimJobMdMidPerforations { get; set; }
        public virtual DbSet<StimJobMdOpenHoleBottoms> StimJobMdOpenHoleBottoms { get; set; }
        public virtual DbSet<StimJobMdOpenHoleTops> StimJobMdOpenHoleTops { get; set; }
        public virtual DbSet<StimJobMdPerforationsBottoms> StimJobMdPerforationsBottoms { get; set; }
        public virtual DbSet<StimJobMdPerforationsTops> StimJobMdPerforationsTops { get; set; }
        public virtual DbSet<StimJobMdSurfaces> StimJobMdSurfaces { get; set; }
        public virtual DbSet<StimJobMdTops> StimJobMdTops { get; set; }
        public virtual DbSet<StimJobNearWellboreFrictions> StimJobNearWellboreFrictions { get; set; }
        public virtual DbSet<StimJobNetPayFluidCompressibilitys> StimJobNetPayFluidCompressibilitys { get; set; }
        public virtual DbSet<StimJobNetPayFluidViscositys> StimJobNetPayFluidViscositys { get; set; }
        public virtual DbSet<StimJobNetPayFormationPermeabilitys> StimJobNetPayFormationPermeabilitys { get; set; }
        public virtual DbSet<StimJobNetPayFormationPorositys> StimJobNetPayFormationPorositys { get; set; }
        public virtual DbSet<StimJobNetPayPorePress> StimJobNetPayPorePress { get; set; }
        public virtual DbSet<StimJobNetPayThicknesss> StimJobNetPayThicknesss { get; set; }
        public virtual DbSet<StimJobNetPress> StimJobNetPress { get; set; }
        public virtual DbSet<StimJobOds> StimJobOds { get; set; }
        public virtual DbSet<StimJobOilVols> StimJobOilVols { get; set; }
        public virtual DbSet<StimJobOilVolumes> StimJobOilVolumes { get; set; }
        public virtual DbSet<StimJobOpenHoleDiameters> StimJobOpenHoleDiameters { get; set; }
        public virtual DbSet<StimJobPdatSessions> StimJobPdatSessions { get; set; }
        public virtual DbSet<StimJobPercentPads> StimJobPercentPads { get; set; }
        public virtual DbSet<StimJobPercentProppantPumpeds> StimJobPercentProppantPumpeds { get; set; }
        public virtual DbSet<StimJobPerfBallSizes> StimJobPerfBallSizes { get; set; }
        public virtual DbSet<StimJobPerfFrictions> StimJobPerfFrictions { get; set; }
        public virtual DbSet<StimJobPerforationIntervals> StimJobPerforationIntervals { get; set; }
        public virtual DbSet<StimJobPerfproppantConcs> StimJobPerfproppantConcs { get; set; }
        public virtual DbSet<StimJobPhasingPerforations> StimJobPhasingPerforations { get; set; }
        public virtual DbSet<StimJobPipeFrictions> StimJobPipeFrictions { get; set; }
        public virtual DbSet<StimJobPorePress> StimJobPorePress { get; set; }
        public virtual DbSet<StimJobPresMeasurements> StimJobPresMeasurements { get; set; }
        public virtual DbSet<StimJobPress> StimJobPress { get; set; }
        public virtual DbSet<StimJobPropMasss> StimJobPropMasss { get; set; }
        public virtual DbSet<StimJobProppantMassWellHeads> StimJobProppantMassWellHeads { get; set; }
        public virtual DbSet<StimJobProppantMasss> StimJobProppantMasss { get; set; }
        public virtual DbSet<StimJobProppants> StimJobProppants { get; set; }
        public virtual DbSet<StimJobPseudoRadialPress> StimJobPseudoRadialPress { get; set; }
        public virtual DbSet<StimJobPumpDurations> StimJobPumpDurations { get; set; }
        public virtual DbSet<StimJobPumpFlowBackTests> StimJobPumpFlowBackTests { get; set; }
        public virtual DbSet<StimJobPumpTimes> StimJobPumpTimes { get; set; }
        public virtual DbSet<StimJobReservoirIntervals> StimJobReservoirIntervals { get; set; }
        public virtual DbSet<StimJobReservoirTotalCompressibilitys> StimJobReservoirTotalCompressibilitys { get; set; }
        public virtual DbSet<StimJobResidualPermeabilitys> StimJobResidualPermeabilitys { get; set; }
        public virtual DbSet<StimJobScreenOutPress> StimJobScreenOutPress { get; set; }
        public virtual DbSet<StimJobShutinPres10Mins> StimJobShutinPres10Mins { get; set; }
        public virtual DbSet<StimJobShutinPres15Mins> StimJobShutinPres15Mins { get; set; }
        public virtual DbSet<StimJobShutinPres5Mins> StimJobShutinPres5Mins { get; set; }
        public virtual DbSet<StimJobShutinPress> StimJobShutinPress { get; set; }
        public virtual DbSet<StimJobSizes> StimJobSizes { get; set; }
        public virtual DbSet<StimJobSlurryRateBegins> StimJobSlurryRateBegins { get; set; }
        public virtual DbSet<StimJobSlurryRateEnds> StimJobSlurryRateEnds { get; set; }
        public virtual DbSet<StimJobSlurryVols> StimJobSlurryVols { get; set; }
        public virtual DbSet<StimJobStageFluids> StimJobStageFluids { get; set; }
        public virtual DbSet<StimJobStartFoamRateCo2s> StimJobStartFoamRateCo2s { get; set; }
        public virtual DbSet<StimJobStartFoamRateN2s> StimJobStartFoamRateN2s { get; set; }
        public virtual DbSet<StimJobStartPresBottomholes> StimJobStartPresBottomholes { get; set; }
        public virtual DbSet<StimJobStartPresSurfaces> StimJobStartPresSurfaces { get; set; }
        public virtual DbSet<StimJobStartProppantConcBottomholes> StimJobStartProppantConcBottomholes { get; set; }
        public virtual DbSet<StimJobStartProppantConcSurfaces> StimJobStartProppantConcSurfaces { get; set; }
        public virtual DbSet<StimJobStartPumpRateBottomholes> StimJobStartPumpRateBottomholes { get; set; }
        public virtual DbSet<StimJobStartRateSurfaceCo2s> StimJobStartRateSurfaceCo2s { get; set; }
        public virtual DbSet<StimJobStartRateSurfaceLiquids> StimJobStartRateSurfaceLiquids { get; set; }
        public virtual DbSet<StimJobStartStdRateSurfaceN2s> StimJobStartStdRateSurfaceN2s { get; set; }
        public virtual DbSet<StimJobStdVolN2s> StimJobStdVolN2s { get; set; }
        public virtual DbSet<StimJobStepDownTests> StimJobStepDownTests { get; set; }
        public virtual DbSet<StimJobStepRateTests> StimJobStepRateTests { get; set; }
        public virtual DbSet<StimJobSteps> StimJobSteps { get; set; }
        public virtual DbSet<StimJobSurfaceFluidTemperatures> StimJobSurfaceFluidTemperatures { get; set; }
        public virtual DbSet<StimJobSurfaceTemperatures> StimJobSurfaceTemperatures { get; set; }
        public virtual DbSet<StimJobTimeAfterShutins> StimJobTimeAfterShutins { get; set; }
        public virtual DbSet<StimJobTotalCo2masss> StimJobTotalCo2masss { get; set; }
        public virtual DbSet<StimJobTotalFrictionPresLosss> StimJobTotalFrictionPresLosss { get; set; }
        public virtual DbSet<StimJobTotalJobVolumes> StimJobTotalJobVolumes { get; set; }
        public virtual DbSet<StimJobTotalN2stdVolumes> StimJobTotalN2stdVolumes { get; set; }
        public virtual DbSet<StimJobTotalProppantMasss> StimJobTotalProppantMasss { get; set; }
        public virtual DbSet<StimJobTotalProppantUsages> StimJobTotalProppantUsages { get; set; }
        public virtual DbSet<StimJobTotalProppantWts> StimJobTotalProppantWts { get; set; }
        public virtual DbSet<StimJobTotalPumpTimes> StimJobTotalPumpTimes { get; set; }
        public virtual DbSet<StimJobTotalVolumes> StimJobTotalVolumes { get; set; }
        public virtual DbSet<StimJobTreatingBottomholeTemperatures> StimJobTreatingBottomholeTemperatures { get; set; }
        public virtual DbSet<StimJobTubulars> StimJobTubulars { get; set; }
        public virtual DbSet<StimJobTvdFormationBottoms> StimJobTvdFormationBottoms { get; set; }
        public virtual DbSet<StimJobTvdFormationTops> StimJobTvdFormationTops { get; set; }
        public virtual DbSet<StimJobTvdMidPerforations> StimJobTvdMidPerforations { get; set; }
        public virtual DbSet<StimJobTvdOpenHoleBottoms> StimJobTvdOpenHoleBottoms { get; set; }
        public virtual DbSet<StimJobTvdOpenHoleTops> StimJobTvdOpenHoleTops { get; set; }
        public virtual DbSet<StimJobTvdPerforationsBottoms> StimJobTvdPerforationsBottoms { get; set; }
        public virtual DbSet<StimJobTvdPerforationsTops> StimJobTvdPerforationsTops { get; set; }
        public virtual DbSet<StimJobVolumeFactors> StimJobVolumeFactors { get; set; }
        public virtual DbSet<StimJobVolumes> StimJobVolumes { get; set; }
        public virtual DbSet<StimJobWeights> StimJobWeights { get; set; }
        public virtual DbSet<StimJobWellboreProppantMasss> StimJobWellboreProppantMasss { get; set; }
        public virtual DbSet<StimJobWellboreVolumes> StimJobWellboreVolumes { get; set; }
        public virtual DbSet<StimJobWellheadVols> StimJobWellheadVols { get; set; }
        public virtual DbSet<StimJobs> StimJobs { get; set; }
        public virtual DbSet<SurveyProgramCommonData> SurveyProgramCommonData { get; set; }
        public virtual DbSet<SurveyProgramFrequencyMx> SurveyProgramFrequencyMx { get; set; }
        public virtual DbSet<SurveyProgramMdEnd> SurveyProgramMdEnd { get; set; }
        public virtual DbSet<SurveyProgramMdStart> SurveyProgramMdStart { get; set; }
        public virtual DbSet<SurveyProgramSurveySection> SurveyProgramSurveySection { get; set; }
        public virtual DbSet<SurveyPrograms> SurveyPrograms { get; set; }
        public virtual DbSet<TargetAngleArcs> TargetAngleArcs { get; set; }
        public virtual DbSet<TargetCommonDatas> TargetCommonDatas { get; set; }
        public virtual DbSet<TargetDips> TargetDips { get; set; }
        public virtual DbSet<TargetDispEwCenters> TargetDispEwCenters { get; set; }
        public virtual DbSet<TargetDispEwOffsets> TargetDispEwOffsets { get; set; }
        public virtual DbSet<TargetDispEwSectOrigs> TargetDispEwSectOrigs { get; set; }
        public virtual DbSet<TargetDispNsCenters> TargetDispNsCenters { get; set; }
        public virtual DbSet<TargetDispNsOffsets> TargetDispNsOffsets { get; set; }
        public virtual DbSet<TargetDispNsSectOrigs> TargetDispNsSectOrigs { get; set; }
        public virtual DbSet<TargetLatitudes> TargetLatitudes { get; set; }
        public virtual DbSet<TargetLenMajorAxiss> TargetLenMajorAxiss { get; set; }
        public virtual DbSet<TargetLenRadiuss> TargetLenRadiuss { get; set; }
        public virtual DbSet<TargetLocations> TargetLocations { get; set; }
        public virtual DbSet<TargetLongitudes> TargetLongitudes { get; set; }
        public virtual DbSet<TargetProjectedXs> TargetProjectedXs { get; set; }
        public virtual DbSet<TargetProjectedYs> TargetProjectedYs { get; set; }
        public virtual DbSet<TargetRotations> TargetRotations { get; set; }
        public virtual DbSet<TargetSections> TargetSections { get; set; }
        public virtual DbSet<TargetStrikes> TargetStrikes { get; set; }
        public virtual DbSet<TargetThickAboves> TargetThickAboves { get; set; }
        public virtual DbSet<TargetThickBelows> TargetThickBelows { get; set; }
        public virtual DbSet<TargetTvds> TargetTvds { get; set; }
        public virtual DbSet<TargetWellCrss> TargetWellCrss { get; set; }
        public virtual DbSet<TargetWidMinorAxiss> TargetWidMinorAxiss { get; set; }
        public virtual DbSet<Targets> Targets { get; set; }
        public virtual DbSet<ToolErrorModelAuthorizations> ToolErrorModelAuthorizations { get; set; }
        public virtual DbSet<ToolErrorModelCommonDatas> ToolErrorModelCommonDatas { get; set; }
        public virtual DbSet<ToolErrorModelEnds> ToolErrorModelEnds { get; set; }
        public virtual DbSet<ToolErrorModelErrorTermValues> ToolErrorModelErrorTermValues { get; set; }
        public virtual DbSet<ToolErrorModelGyroInitializations> ToolErrorModelGyroInitializations { get; set; }
        public virtual DbSet<ToolErrorModelGyroReinitializationDistances> ToolErrorModelGyroReinitializationDistances { get; set; }
        public virtual DbSet<ToolErrorModelMaxs> ToolErrorModelMaxs { get; set; }
        public virtual DbSet<ToolErrorModelMins> ToolErrorModelMins { get; set; }
        public virtual DbSet<ToolErrorModelModelParameter> ToolErrorModelModelParameter { get; set; }
        public virtual DbSet<ToolErrorModelOperatingConditions> ToolErrorModelOperatingConditions { get; set; }
        public virtual DbSet<ToolErrorModelOperatingIntervals> ToolErrorModelOperatingIntervals { get; set; }
        public virtual DbSet<ToolErrorModelSpeeds> ToolErrorModelSpeeds { get; set; }
        public virtual DbSet<ToolErrorModelStarts> ToolErrorModelStarts { get; set; }
        public virtual DbSet<ToolErrorModelTerms> ToolErrorModelTerms { get; set; }
        public virtual DbSet<ToolErrorModelUseErrorTermSets> ToolErrorModelUseErrorTermSets { get; set; }
        public virtual DbSet<ToolErrorModelValues> ToolErrorModelValues { get; set; }
        public virtual DbSet<ToolErrorModels> ToolErrorModels { get; set; }
        public virtual DbSet<ToolErrorTermSetAuthorizations> ToolErrorTermSetAuthorizations { get; set; }
        public virtual DbSet<ToolErrorTermSetConstants> ToolErrorTermSetConstants { get; set; }
        public virtual DbSet<ToolErrorTermSetErrorCoefficients> ToolErrorTermSetErrorCoefficients { get; set; }
        public virtual DbSet<ToolErrorTermSetErrorTerms> ToolErrorTermSetErrorTerms { get; set; }
        public virtual DbSet<ToolErrorTermSetFunctions> ToolErrorTermSetFunctions { get; set; }
        public virtual DbSet<ToolErrorTermSetNomenclatures> ToolErrorTermSetNomenclatures { get; set; }
        public virtual DbSet<ToolErrorTermSetParameters> ToolErrorTermSetParameters { get; set; }
        public virtual DbSet<ToolErrorTermSets> ToolErrorTermSets { get; set; }
        public virtual DbSet<TrajectoryAziVertSects> TrajectoryAziVertSects { get; set; }
        public virtual DbSet<TrajectoryAzis> TrajectoryAzis { get; set; }
        public virtual DbSet<TrajectoryBiasEs> TrajectoryBiasEs { get; set; }
        public virtual DbSet<TrajectoryBiasNs> TrajectoryBiasNs { get; set; }
        public virtual DbSet<TrajectoryBiasVerts> TrajectoryBiasVerts { get; set; }
        public virtual DbSet<TrajectoryCommonDatas> TrajectoryCommonDatas { get; set; }
        public virtual DbSet<TrajectoryCorUseds> TrajectoryCorUseds { get; set; }
        public virtual DbSet<TrajectoryDipAngleUncerts> TrajectoryDipAngleUncerts { get; set; }
        public virtual DbSet<TrajectoryDirSensorOffsets> TrajectoryDirSensorOffsets { get; set; }
        public virtual DbSet<TrajectoryDispEwVertSectOrigs> TrajectoryDispEwVertSectOrigs { get; set; }
        public virtual DbSet<TrajectoryDispEws> TrajectoryDispEws { get; set; }
        public virtual DbSet<TrajectoryDispNsVertSectOrigs> TrajectoryDispNsVertSectOrigs { get; set; }
        public virtual DbSet<TrajectoryDispNss> TrajectoryDispNss { get; set; }
        public virtual DbSet<TrajectoryDlss> TrajectoryDlss { get; set; }
        public virtual DbSet<TrajectoryEastings> TrajectoryEastings { get; set; }
        public virtual DbSet<TrajectoryGravAxialAccelCors> TrajectoryGravAxialAccelCors { get; set; }
        public virtual DbSet<TrajectoryGravAxialRaws> TrajectoryGravAxialRaws { get; set; }
        public virtual DbSet<TrajectoryGravTotalFieldCalcs> TrajectoryGravTotalFieldCalcs { get; set; }
        public virtual DbSet<TrajectoryGravTotalUncerts> TrajectoryGravTotalUncerts { get; set; }
        public virtual DbSet<TrajectoryGravTran1AccelCors> TrajectoryGravTran1AccelCors { get; set; }
        public virtual DbSet<TrajectoryGravTran1Raws> TrajectoryGravTran1Raws { get; set; }
        public virtual DbSet<TrajectoryGravTran2AccelCors> TrajectoryGravTran2AccelCors { get; set; }
        public virtual DbSet<TrajectoryGravTran2Raws> TrajectoryGravTran2Raws { get; set; }
        public virtual DbSet<TrajectoryGridCorUseds> TrajectoryGridCorUseds { get; set; }
        public virtual DbSet<TrajectoryGtfs> TrajectoryGtfs { get; set; }
        public virtual DbSet<TrajectoryIncls> TrajectoryIncls { get; set; }
        public virtual DbSet<TrajectoryLatitudes> TrajectoryLatitudes { get; set; }
        public virtual DbSet<TrajectoryLocations> TrajectoryLocations { get; set; }
        public virtual DbSet<TrajectoryLongitudes> TrajectoryLongitudes { get; set; }
        public virtual DbSet<TrajectoryMagAxialDrlstrCors> TrajectoryMagAxialDrlstrCors { get; set; }
        public virtual DbSet<TrajectoryMagAxialRaws> TrajectoryMagAxialRaws { get; set; }
        public virtual DbSet<TrajectoryMagDeclUseds> TrajectoryMagDeclUseds { get; set; }
        public virtual DbSet<TrajectoryMagDipAngleCalcs> TrajectoryMagDipAngleCalcs { get; set; }
        public virtual DbSet<TrajectoryMagTotalFieldCalcs> TrajectoryMagTotalFieldCalcs { get; set; }
        public virtual DbSet<TrajectoryMagTotalUncerts> TrajectoryMagTotalUncerts { get; set; }
        public virtual DbSet<TrajectoryMagTran1DrlstrCors> TrajectoryMagTran1DrlstrCors { get; set; }
        public virtual DbSet<TrajectoryMagTran1Raws> TrajectoryMagTran1Raws { get; set; }
        public virtual DbSet<TrajectoryMagTran2DrlstrCors> TrajectoryMagTran2DrlstrCors { get; set; }
        public virtual DbSet<TrajectoryMagTran2Raws> TrajectoryMagTran2Raws { get; set; }
        public virtual DbSet<TrajectoryMatrixCovs> TrajectoryMatrixCovs { get; set; }
        public virtual DbSet<TrajectoryMdDeltas> TrajectoryMdDeltas { get; set; }
        public virtual DbSet<TrajectoryMdMns> TrajectoryMdMns { get; set; }
        public virtual DbSet<TrajectoryMdMxs> TrajectoryMdMxs { get; set; }
        public virtual DbSet<TrajectoryMds> TrajectoryMds { get; set; }
        public virtual DbSet<TrajectoryMtfs> TrajectoryMtfs { get; set; }
        public virtual DbSet<TrajectoryNorthings> TrajectoryNorthings { get; set; }
        public virtual DbSet<TrajectoryRateBuilds> TrajectoryRateBuilds { get; set; }
        public virtual DbSet<TrajectoryRateTurns> TrajectoryRateTurns { get; set; }
        public virtual DbSet<TrajectoryRawDatas> TrajectoryRawDatas { get; set; }
        public virtual DbSet<TrajectorySagAziCors> TrajectorySagAziCors { get; set; }
        public virtual DbSet<TrajectorySagIncCors> TrajectorySagIncCors { get; set; }
        public virtual DbSet<TrajectoryStations> TrajectoryStations { get; set; }
        public virtual DbSet<TrajectoryStnGridCorUseds> TrajectoryStnGridCorUseds { get; set; }
        public virtual DbSet<TrajectoryStnMagDeclUseds> TrajectoryStnMagDeclUseds { get; set; }
        public virtual DbSet<TrajectoryTvdDeltas> TrajectoryTvdDeltas { get; set; }
        public virtual DbSet<TrajectoryTvds> TrajectoryTvds { get; set; }
        public virtual DbSet<TrajectoryValids> TrajectoryValids { get; set; }
        public virtual DbSet<TrajectoryVarianceEes> TrajectoryVarianceEes { get; set; }
        public virtual DbSet<TrajectoryVarianceEverts> TrajectoryVarianceEverts { get; set; }
        public virtual DbSet<TrajectoryVarianceNes> TrajectoryVarianceNes { get; set; }
        public virtual DbSet<TrajectoryVarianceNns> TrajectoryVarianceNns { get; set; }
        public virtual DbSet<TrajectoryVarianceNverts> TrajectoryVarianceNverts { get; set; }
        public virtual DbSet<TrajectoryVarianceVertVerts> TrajectoryVarianceVertVerts { get; set; }
        public virtual DbSet<TrajectoryVertSects> TrajectoryVertSects { get; set; }
        public virtual DbSet<TrajectoryWellCrss> TrajectoryWellCrss { get; set; }
        public virtual DbSet<Trajectorys> Trajectorys { get; set; }
        public virtual DbSet<TubularAngle> TubularAngle { get; set; }
        public virtual DbSet<TubularAreaNozzleFlow> TubularAreaNozzleFlow { get; set; }
        public virtual DbSet<TubularAxialStiffness> TubularAxialStiffness { get; set; }
        public virtual DbSet<TubularBend> TubularBend { get; set; }
        public virtual DbSet<TubularBendSettingsMn> TubularBendSettingsMn { get; set; }
        public virtual DbSet<TubularBendSettingsMx> TubularBendSettingsMx { get; set; }
        public virtual DbSet<TubularBendStiffness> TubularBendStiffness { get; set; }
        public virtual DbSet<TubularBitRecord> TubularBitRecord { get; set; }
        public virtual DbSet<TubularClearanceBearBox> TubularClearanceBearBox { get; set; }
        public virtual DbSet<TubularCommonDatas> TubularCommonDatas { get; set; }
        public virtual DbSet<TubularComponent> TubularComponent { get; set; }
        public virtual DbSet<TubularConnection> TubularConnection { get; set; }
        public virtual DbSet<TubularCosts> TubularCosts { get; set; }
        public virtual DbSet<TubularCriticalCrossSection> TubularCriticalCrossSection { get; set; }
        public virtual DbSet<TubularDiaBits> TubularDiaBits { get; set; }
        public virtual DbSet<TubularDiaHoleAssy> TubularDiaHoleAssy { get; set; }
        public virtual DbSet<TubularDiaHoleOpener> TubularDiaHoleOpener { get; set; }
        public virtual DbSet<TubularDiaNozzle> TubularDiaNozzle { get; set; }
        public virtual DbSet<TubularDiaPassThrus> TubularDiaPassThrus { get; set; }
        public virtual DbSet<TubularDiaPilot> TubularDiaPilot { get; set; }
        public virtual DbSet<TubularDiaRotorNozzle> TubularDiaRotorNozzle { get; set; }
        public virtual DbSet<TubularDisp> TubularDisp { get; set; }
        public virtual DbSet<TubularDistBendBot> TubularDistBendBot { get; set; }
        public virtual DbSet<TubularDistBladeBot> TubularDistBladeBot { get; set; }
        public virtual DbSet<TubularDoglegMx> TubularDoglegMx { get; set; }
        public virtual DbSet<TubularFlowrateMn> TubularFlowrateMn { get; set; }
        public virtual DbSet<TubularFlowrateMx> TubularFlowrateMx { get; set; }
        public virtual DbSet<TubularForDownSet> TubularForDownSet { get; set; }
        public virtual DbSet<TubularForDownTrip> TubularForDownTrip { get; set; }
        public virtual DbSet<TubularForPmpOpen> TubularForPmpOpen { get; set; }
        public virtual DbSet<TubularForSealFric> TubularForSealFric { get; set; }
        public virtual DbSet<TubularForUpSet> TubularForUpSet { get; set; }
        public virtual DbSet<TubularForUpTrip> TubularForUpTrip { get; set; }
        public virtual DbSet<TubularHoleOpener> TubularHoleOpener { get; set; }
        public virtual DbSet<TubularId> TubularId { get; set; }
        public virtual DbSet<TubularIdEquv> TubularIdEquv { get; set; }
        public virtual DbSet<TubularIdFishneck> TubularIdFishneck { get; set; }
        public virtual DbSet<TubularJar> TubularJar { get; set; }
        public virtual DbSet<TubularLen> TubularLen { get; set; }
        public virtual DbSet<TubularLenBlade> TubularLenBlade { get; set; }
        public virtual DbSet<TubularLenFishneck> TubularLenFishneck { get; set; }
        public virtual DbSet<TubularLenJointAv> TubularLenJointAv { get; set; }
        public virtual DbSet<TubularMotor> TubularMotor { get; set; }
        public virtual DbSet<TubularMwdTool> TubularMwdTool { get; set; }
        public virtual DbSet<TubularNameTag> TubularNameTag { get; set; }
        public virtual DbSet<TubularNozzle> TubularNozzle { get; set; }
        public virtual DbSet<TubularOd> TubularOd { get; set; }
        public virtual DbSet<TubularOdBladeMn> TubularOdBladeMn { get; set; }
        public virtual DbSet<TubularOdBladeMx> TubularOdBladeMx { get; set; }
        public virtual DbSet<TubularOdDrifts> TubularOdDrifts { get; set; }
        public virtual DbSet<TubularOdFishneck> TubularOdFishneck { get; set; }
        public virtual DbSet<TubularOffsetBot> TubularOffsetBot { get; set; }
        public virtual DbSet<TubularOffsetTool> TubularOffsetTool { get; set; }
        public virtual DbSet<TubularPresBurst> TubularPresBurst { get; set; }
        public virtual DbSet<TubularPresCollapse> TubularPresCollapse { get; set; }
        public virtual DbSet<TubularPresLeak> TubularPresLeak { get; set; }
        public virtual DbSet<TubularSensor> TubularSensor { get; set; }
        public virtual DbSet<TubularSizeThread> TubularSizeThread { get; set; }
        public virtual DbSet<TubularStabilizer> TubularStabilizer { get; set; }
        public virtual DbSet<TubularStressFatig> TubularStressFatig { get; set; }
        public virtual DbSet<TubularTempMx> TubularTempMx { get; set; }
        public virtual DbSet<TubularTempOpMx> TubularTempOpMx { get; set; }
        public virtual DbSet<TubularTensYield> TubularTensYield { get; set; }
        public virtual DbSet<TubularThickWall> TubularThickWall { get; set; }
        public virtual DbSet<TubularTorsionalStiffness> TubularTorsionalStiffness { get; set; }
        public virtual DbSet<TubularTqMakeup> TubularTqMakeup { get; set; }
        public virtual DbSet<TubularTqYield> TubularTqYield { get; set; }
        public virtual DbSet<TubularWearWall> TubularWearWall { get; set; }
        public virtual DbSet<TubularWtPerLen> TubularWtPerLen { get; set; }
        public virtual DbSet<Tubulars> Tubulars { get; set; }
        public virtual DbSet<WbGeometryCommonData> WbGeometryCommonData { get; set; }
        public virtual DbSet<WbGeometryDepthWaterMean> WbGeometryDepthWaterMean { get; set; }
        public virtual DbSet<WbGeometryDiaDrift> WbGeometryDiaDrift { get; set; }
        public virtual DbSet<WbGeometryGapAir> WbGeometryGapAir { get; set; }
        public virtual DbSet<WbGeometryIdSection> WbGeometryIdSection { get; set; }
        public virtual DbSet<WbGeometryMdBottom> WbGeometryMdBottom { get; set; }
        public virtual DbSet<WbGeometryMdTop> WbGeometryMdTop { get; set; }
        public virtual DbSet<WbGeometryOdSection> WbGeometryOdSection { get; set; }
        public virtual DbSet<WbGeometrySection> WbGeometrySection { get; set; }
        public virtual DbSet<WbGeometryTvdBottom> WbGeometryTvdBottom { get; set; }
        public virtual DbSet<WbGeometryTvdTop> WbGeometryTvdTop { get; set; }
        public virtual DbSet<WbGeometryWtPerLen> WbGeometryWtPerLen { get; set; }
        public virtual DbSet<WbGeometrys> WbGeometrys { get; set; }
        public virtual DbSet<WellBoreCommonData> WellBoreCommonData { get; set; }
        public virtual DbSet<WellBores> WellBores { get; set; }
        public virtual DbSet<WellCommonDatas> WellCommonDatas { get; set; }
        public virtual DbSet<WellCrss> WellCrss { get; set; }
        public virtual DbSet<WellDatumNames> WellDatumNames { get; set; }
        public virtual DbSet<WellDatums> WellDatums { get; set; }
        public virtual DbSet<WellDefaultDatums> WellDefaultDatums { get; set; }
        public virtual DbSet<WellEastings> WellEastings { get; set; }
        public virtual DbSet<WellElevations> WellElevations { get; set; }
        public virtual DbSet<WellGeodeticCrss> WellGeodeticCrss { get; set; }
        public virtual DbSet<WellGroundElevations> WellGroundElevations { get; set; }
        public virtual DbSet<WellLatitudes> WellLatitudes { get; set; }
        public virtual DbSet<WellLocalCrss> WellLocalCrss { get; set; }
        public virtual DbSet<WellLocalXs> WellLocalXs { get; set; }
        public virtual DbSet<WellLocalYs> WellLocalYs { get; set; }
        public virtual DbSet<WellLocations> WellLocations { get; set; }
        public virtual DbSet<WellLongitudes> WellLongitudes { get; set; }
        public virtual DbSet<WellMapProjectionCrss> WellMapProjectionCrss { get; set; }
        public virtual DbSet<WellMeasuredDepths> WellMeasuredDepths { get; set; }
        public virtual DbSet<WellNorthings> WellNorthings { get; set; }
        public virtual DbSet<WellPcInterests> WellPcInterests { get; set; }
        public virtual DbSet<WellReferencePoints> WellReferencePoints { get; set; }
        public virtual DbSet<WellWaterDepths> WellWaterDepths { get; set; }
        public virtual DbSet<WellYaxisAzimuths> WellYaxisAzimuths { get; set; }
        public virtual DbSet<WellboreDayTarget> WellboreDayTarget { get; set; }
        public virtual DbSet<WellboreMd> WellboreMd { get; set; }
        public virtual DbSet<WellboreMdKickoff> WellboreMdKickoff { get; set; }
        public virtual DbSet<WellboreMdPlanned> WellboreMdPlanned { get; set; }
        public virtual DbSet<WellboreMdSubSeaPlanned> WellboreMdSubSeaPlanned { get; set; }
        public virtual DbSet<WellboreParentWellbore> WellboreParentWellbore { get; set; }
        public virtual DbSet<WellboreTvd> WellboreTvd { get; set; }
        public virtual DbSet<WellboreTvdKickoff> WellboreTvdKickoff { get; set; }
        public virtual DbSet<WellboreTvdPlanned> WellboreTvdPlanned { get; set; }
        public virtual DbSet<WellboreTvdSubSeaPlanned> WellboreTvdSubSeaPlanned { get; set; }
        public virtual DbSet<WellheadElevations> WellheadElevations { get; set; }
        public virtual DbSet<Wells> Wells { get; set; }

        public virtual DbSet<ClientContact> ClientContacts { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProviderDirectoryApproval>().HasData(new ProviderDirectoryApproval { Id = System.Guid.NewGuid().ToString("D"), Name = "Approved" });
            modelBuilder.Entity<ProviderDirectoryApproval>().HasData(new ProviderDirectoryApproval { Id = System.Guid.NewGuid().ToString("D"), Name = "Pending review" });
            modelBuilder.Entity<ProviderDirectoryApproval>().HasData(new ProviderDirectoryApproval { Id = System.Guid.NewGuid().ToString("D"), Name = "Rejected" });

            modelBuilder.Entity<ProviderDirectoryStatus>().HasData(new ProviderDirectoryStatus { Id = System.Guid.NewGuid().ToString("D"), Name = "Active" });
            modelBuilder.Entity<ProviderDirectoryStatus>().HasData(new ProviderDirectoryStatus { Id = System.Guid.NewGuid().ToString("D"), Name = "Inactive" });

            modelBuilder.Entity<ProviderDirectoryPEC>().HasData(new ProviderDirectoryPEC { Id = System.Guid.NewGuid().ToString("D"), Name = "Good" });
            modelBuilder.Entity<ProviderDirectoryPEC>().HasData(new ProviderDirectoryPEC { Id = System.Guid.NewGuid().ToString("D"), Name = "Average" });
            modelBuilder.Entity<ProviderDirectoryPEC>().HasData(new ProviderDirectoryPEC { Id = System.Guid.NewGuid().ToString("D"), Name = "Bad" });

            //modelBuilder.Entity<PaymentTypeModel>().HasData(new PaymentTypeModel { ID = System.Guid.NewGuid().ToString("D"), Name = "Credit card" });
            //modelBuilder.Entity<PaymentTypeModel>().HasData(new PaymentTypeModel { ID = System.Guid.NewGuid().ToString("D"), Name = "Debit card" });
            //modelBuilder.Entity<PaymentTypeModel>().HasData(new PaymentTypeModel { ID = System.Guid.NewGuid().ToString("D"), Name = "Checks" });

            modelBuilder.Entity<Attachments>(entity =>
            {
                entity.HasIndex(e => e.CommonDataId);

                entity.HasIndex(e => e.ObjectReferenceuidRef);
            });

            modelBuilder.Entity<BharunDrillingParamss>(entity =>
            {
                entity.HasIndex(e => e.AziBottomUom);

                entity.HasIndex(e => e.AziTopUom);

                entity.HasIndex(e => e.CtimCircUom);

                entity.HasIndex(e => e.CtimDrillRotUom);

                entity.HasIndex(e => e.CtimDrillSlidUom);

                entity.HasIndex(e => e.CtimHoldUom);

                entity.HasIndex(e => e.CtimReamUom);

                entity.HasIndex(e => e.CtimSteeringUom);

                entity.HasIndex(e => e.DistDrillRotUom);

                entity.HasIndex(e => e.DistDrillSlidUom);

                entity.HasIndex(e => e.DistHoldUom);

                entity.HasIndex(e => e.DistReamUom);

                entity.HasIndex(e => e.DistSteeringUom);

                entity.HasIndex(e => e.EtimOpBitUom);

                entity.HasIndex(e => e.FlowrateBitUom);

                entity.HasIndex(e => e.FlowratePumpUom);

                entity.HasIndex(e => e.HkldDnUom);

                entity.HasIndex(e => e.HkldRotUom);

                entity.HasIndex(e => e.HkldUpUom);

                entity.HasIndex(e => e.InclMnUom);

                entity.HasIndex(e => e.InclMxUom);

                entity.HasIndex(e => e.InclStartUom);

                entity.HasIndex(e => e.InclStopUom);

                entity.HasIndex(e => e.MdHoleStartUom);

                entity.HasIndex(e => e.MdHoleStopUom);

                entity.HasIndex(e => e.OverPullUom);

                entity.HasIndex(e => e.PowBitUom);

                entity.HasIndex(e => e.PresDropBitUom);

                entity.HasIndex(e => e.PresPumpAvUom);

                entity.HasIndex(e => e.RopAvBharunRopAvId);

                entity.HasIndex(e => e.RopMnBharunRopMnId);

                entity.HasIndex(e => e.RopMxBharunRopMxId);

                entity.HasIndex(e => e.RpmAvBharunRpmAvId);

                entity.HasIndex(e => e.RpmAvDhUom);

                entity.HasIndex(e => e.RpmMnUom);

                entity.HasIndex(e => e.RpmMxUom);

                entity.HasIndex(e => e.SlackOffUom);

                entity.HasIndex(e => e.TempMudDhMxUom);

                entity.HasIndex(e => e.TqDhAvUom);

                entity.HasIndex(e => e.TqOffBotAvUom);

                entity.HasIndex(e => e.TqOnBotAvUom);

                entity.HasIndex(e => e.TqOnBotMnUom);

                entity.HasIndex(e => e.TqOnBotMxUom);

                entity.HasIndex(e => e.TubularUidRef);

                entity.HasIndex(e => e.VelNozzleAvUom);

                entity.HasIndex(e => e.WobAvBharunWobAvId);

                entity.HasIndex(e => e.WobAvDhUom);

                entity.HasIndex(e => e.WobMnUom);

                entity.HasIndex(e => e.WobMxUom);

                entity.HasIndex(e => e.WtAboveJarUom);

                entity.HasIndex(e => e.WtBelowJarUom);

                entity.HasIndex(e => e.WtMudUom);
            });

            modelBuilder.Entity<Bharuns>(entity =>
            {
                entity.HasIndex(e => e.ActDoglegMxUom);

                entity.HasIndex(e => e.ActDoglegUom);

                entity.HasIndex(e => e.CommonDataId);

                entity.HasIndex(e => e.DrillingParamsUid);

                entity.HasIndex(e => e.PlanDoglegUom);

                entity.HasIndex(e => e.TubularUidRef);
            });

            modelBuilder.Entity<CementJobCementAdditives>(entity =>
            {
                entity.HasIndex(e => e.AdditiveId);

                entity.HasIndex(e => e.ConcentrationId);

                entity.HasIndex(e => e.DensAddId);
            });

            modelBuilder.Entity<CementJobCementPumpSchedules>(entity =>
            {
                entity.HasIndex(e => e.EtimPumpId);

                entity.HasIndex(e => e.EtimShutdownId);

                entity.HasIndex(e => e.PresBackId);

                entity.HasIndex(e => e.RatePumpId);

                entity.HasIndex(e => e.VolPumpId);
            });

            modelBuilder.Entity<CementJobCementStages>(entity =>
            {
                entity.HasIndex(e => e.CementingFluidId);

                entity.HasIndex(e => e.DensDisplaceFluidId);

                entity.HasIndex(e => e.DiaTailPipeId);

                entity.HasIndex(e => e.EtimMudCirculationId);

                entity.HasIndex(e => e.EtimPresHeldId);

                entity.HasIndex(e => e.FlowrateBreakDownId);

                entity.HasIndex(e => e.FlowrateDisplaceAvId);

                entity.HasIndex(e => e.FlowrateDisplaceMxId);

                entity.HasIndex(e => e.FlowrateEndId);

                entity.HasIndex(e => e.FlowrateMudCircCementJobFlowrateMudCircId);

                entity.HasIndex(e => e.FlowratePumpEndId);

                entity.HasIndex(e => e.FlowratePumpStartId);

                entity.HasIndex(e => e.FlowrateSqueezeAvId);

                entity.HasIndex(e => e.FlowrateSqueezeMxId);

                entity.HasIndex(e => e.Gel10MinId);

                entity.HasIndex(e => e.Gel10SecId);

                entity.HasIndex(e => e.MdBottomId);

                entity.HasIndex(e => e.MdCircOutId);

                entity.HasIndex(e => e.MdCoilTbgId);

                entity.HasIndex(e => e.MdStringId);

                entity.HasIndex(e => e.MdToolId);

                entity.HasIndex(e => e.MdTopId);

                entity.HasIndex(e => e.PresBackPressureId);

                entity.HasIndex(e => e.PresBreakDownId);

                entity.HasIndex(e => e.PresBumpId);

                entity.HasIndex(e => e.PresCoilTbgEndId);

                entity.HasIndex(e => e.PresCoilTbgStartId);

                entity.HasIndex(e => e.PresCsgEndId);

                entity.HasIndex(e => e.PresCsgStartId);

                entity.HasIndex(e => e.PresDisplaceId);

                entity.HasIndex(e => e.PresHeldId);

                entity.HasIndex(e => e.PresMudCircId);

                entity.HasIndex(e => e.PresPriorBumpId);

                entity.HasIndex(e => e.PresSqueezeAvId);

                entity.HasIndex(e => e.PresSqueezeEndId);

                entity.HasIndex(e => e.PresSqueezeId);

                entity.HasIndex(e => e.PresTbgEndId);

                entity.HasIndex(e => e.PresTbgStartPresTbgStId);

                entity.HasIndex(e => e.PvMudId);

                entity.HasIndex(e => e.TempBhctid);

                entity.HasIndex(e => e.TempBhstid);

                entity.HasIndex(e => e.VisFunnelMudId);

                entity.HasIndex(e => e.VolCircPriorId);

                entity.HasIndex(e => e.VolCsgInId);

                entity.HasIndex(e => e.VolCsgOutId);

                entity.HasIndex(e => e.VolDisplaceFluidId);

                entity.HasIndex(e => e.VolExcessId);

                entity.HasIndex(e => e.VolMudLostId);

                entity.HasIndex(e => e.VolReturnsId);

                entity.HasIndex(e => e.WtMudId);

                entity.HasIndex(e => e.YpMudId);
            });

            modelBuilder.Entity<CementJobCementTests>(entity =>
            {
                entity.HasIndex(e => e.CblPresId);

                entity.HasIndex(e => e.EtimBeforeTestId);

                entity.HasIndex(e => e.EtimCementLogId);

                entity.HasIndex(e => e.EtimPitStartId);

                entity.HasIndex(e => e.EtimTestId);

                entity.HasIndex(e => e.FormPitId);

                entity.HasIndex(e => e.LinerLapId);

                entity.HasIndex(e => e.LinerTopId);

                entity.HasIndex(e => e.MdCementTopId);

                entity.HasIndex(e => e.MdDvtoolJobMdDvtoolId);

                entity.HasIndex(e => e.PresTestId);

                entity.HasIndex(e => e.TestNegativeEmwId);

                entity.HasIndex(e => e.TestPositiveEmwId);
            });

            modelBuilder.Entity<CementJobCementingFluids>(entity =>
            {
                entity.HasIndex(e => e.CementAdditiveId);

                entity.HasIndex(e => e.CementPumpScheduleId);

                entity.HasIndex(e => e.ConsTestThickeningId);

                entity.HasIndex(e => e.DensAtPresId);

                entity.HasIndex(e => e.DensBaseFluidId);

                entity.HasIndex(e => e.DensConstGasFoamId);

                entity.HasIndex(e => e.DensConstGasMethodId);

                entity.HasIndex(e => e.DensDryBlendId);

                entity.HasIndex(e => e.DensityId);

                entity.HasIndex(e => e.EtimComprStren1Id);

                entity.HasIndex(e => e.EtimComprStren2Id);

                entity.HasIndex(e => e.EtimThickeningId);

                entity.HasIndex(e => e.ExcessPcId);

                entity.HasIndex(e => e.Gel10MinReadingId);

                entity.HasIndex(e => e.Gel10MinStrengthId);

                entity.HasIndex(e => e.Gel10SecReadingId);

                entity.HasIndex(e => e.Gel10SecStrengthId);

                entity.HasIndex(e => e.Gel1MinReadingId);

                entity.HasIndex(e => e.Gel1MinStrengthId);

                entity.HasIndex(e => e.Kid);

                entity.HasIndex(e => e.MassDryBlendId);

                entity.HasIndex(e => e.MassSackDryBlendId);

                entity.HasIndex(e => e.MdFluidBottomId);

                entity.HasIndex(e => e.MdFluidTopId);

                entity.HasIndex(e => e.Nid);

                entity.HasIndex(e => e.PcFreeWaterId);

                entity.HasIndex(e => e.PresComprStren1Id);

                entity.HasIndex(e => e.PresComprStren2Id);

                entity.HasIndex(e => e.PresTestFluidLossId);

                entity.HasIndex(e => e.PresTestThickeningId);

                entity.HasIndex(e => e.RatioConstGasMethodAvId);

                entity.HasIndex(e => e.RatioConstGasMethodEndId);

                entity.HasIndex(e => e.RatioConstGasMethodStartId);

                entity.HasIndex(e => e.RatioMixWaterId);

                entity.HasIndex(e => e.SolidVolumeFractionId);

                entity.HasIndex(e => e.TempComprStren1Id);

                entity.HasIndex(e => e.TempComprStren2Id);

                entity.HasIndex(e => e.TempFluidLossId);

                entity.HasIndex(e => e.TempFreeWaterId);

                entity.HasIndex(e => e.TempThickeningId);

                entity.HasIndex(e => e.TimeFluidLossId);

                entity.HasIndex(e => e.VisId);

                entity.HasIndex(e => e.VolApifluidLossId);

                entity.HasIndex(e => e.VolCementId);

                entity.HasIndex(e => e.VolFluidId);

                entity.HasIndex(e => e.VolGasFoamId);

                entity.HasIndex(e => e.VolOtherId);

                entity.HasIndex(e => e.VolPumpedId);

                entity.HasIndex(e => e.VolReservedId);

                entity.HasIndex(e => e.VolTestFluidLossId);

                entity.HasIndex(e => e.VolTotSlurryId);

                entity.HasIndex(e => e.VolWaterId);

                entity.HasIndex(e => e.VolYieldId);

                entity.HasIndex(e => e.YpJobYpId);
            });

            modelBuilder.Entity<CementJobs>(entity =>
            {
                entity.HasIndex(e => e.CementStageId);

                entity.HasIndex(e => e.CementTestId);

                entity.HasIndex(e => e.CommonDataId);

                entity.HasIndex(e => e.LenPipeRecipStrokeId);

                entity.HasIndex(e => e.MdHoleId);

                entity.HasIndex(e => e.MdPlugBotId);

                entity.HasIndex(e => e.MdPlugTopId);

                entity.HasIndex(e => e.MdShoeId);

                entity.HasIndex(e => e.MdSqueezeId);

                entity.HasIndex(e => e.MdStringSetId);

                entity.HasIndex(e => e.MdWaterId);

                entity.HasIndex(e => e.OverPullId);

                entity.HasIndex(e => e.RpmPipeId);

                entity.HasIndex(e => e.RpmPipeRecipId);

                entity.HasIndex(e => e.SlackOffId);

                entity.HasIndex(e => e.TqInitPipeRotId);

                entity.HasIndex(e => e.TqPipeAvId);

                entity.HasIndex(e => e.TqPipeMxId);

                entity.HasIndex(e => e.TvdShoeId);

                entity.HasIndex(e => e.TvdStringSetId);

                entity.HasIndex(e => e.WocId);
            });

            modelBuilder.Entity<ChangeLogChangeHistory>(entity =>
            {
                entity.HasIndex(e => e.ChangeLogUid);

                entity.HasIndex(e => e.EndIndexUom);

                entity.HasIndex(e => e.StartIndexUom);
            });

            modelBuilder.Entity<ChangeLogs>(entity =>
            {
                entity.HasIndex(e => e.CommonDataId);
            });

            modelBuilder.Entity<ConvCoreChromatographs>(entity =>
            {
                entity.HasIndex(e => e.AcetyleneId);

                entity.HasIndex(e => e.Co2AvId);

                entity.HasIndex(e => e.Co2MnId);

                entity.HasIndex(e => e.Co2MxId);

                entity.HasIndex(e => e.EpentAvId);

                entity.HasIndex(e => e.EpentMnId);

                entity.HasIndex(e => e.EpentMxId);

                entity.HasIndex(e => e.EthAvId);

                entity.HasIndex(e => e.EthMnId);

                entity.HasIndex(e => e.EthMxId);

                entity.HasIndex(e => e.EtimChromCycleId);

                entity.HasIndex(e => e.H2sAvId);

                entity.HasIndex(e => e.H2sMnId);

                entity.HasIndex(e => e.H2sMxId);

                entity.HasIndex(e => e.IbutAvId);

                entity.HasIndex(e => e.IbutMnId);

                entity.HasIndex(e => e.IbutMxId);

                entity.HasIndex(e => e.IhexAvId);

                entity.HasIndex(e => e.IhexMnId);

                entity.HasIndex(e => e.IhexMxId);

                entity.HasIndex(e => e.IpentAvId);

                entity.HasIndex(e => e.IpentMnId);

                entity.HasIndex(e => e.IpentMxId);

                entity.HasIndex(e => e.MdBottomId);

                entity.HasIndex(e => e.MdTopId);

                entity.HasIndex(e => e.MethAvId);

                entity.HasIndex(e => e.MethMnId);

                entity.HasIndex(e => e.MethMxId);

                entity.HasIndex(e => e.NbutAvId);

                entity.HasIndex(e => e.NbutMnId);

                entity.HasIndex(e => e.NbutMxId);

                entity.HasIndex(e => e.NhexAvId);

                entity.HasIndex(e => e.NhexMnId);

                entity.HasIndex(e => e.NhexMxId);

                entity.HasIndex(e => e.NpentAvId);

                entity.HasIndex(e => e.NpentMnId);

                entity.HasIndex(e => e.NpentMxId);

                entity.HasIndex(e => e.PropAvId);

                entity.HasIndex(e => e.PropMnId);

                entity.HasIndex(e => e.PropMxId);

                entity.HasIndex(e => e.WtMudInId);

                entity.HasIndex(e => e.WtMudOutId);
            });

            modelBuilder.Entity<ConvCoreGeologyIntervals>(entity =>
            {
                entity.HasIndex(e => e.CalcStabId);

                entity.HasIndex(e => e.CalciteId);

                entity.HasIndex(e => e.CecId);

                entity.HasIndex(e => e.ChromatographId);

                entity.HasIndex(e => e.DensBulkId);

                entity.HasIndex(e => e.DensShaleId);

                entity.HasIndex(e => e.DolomiteId);

                entity.HasIndex(e => e.EcdTdAvId);

                entity.HasIndex(e => e.LenPlugId);

                entity.HasIndex(e => e.LithologyId);

                entity.HasIndex(e => e.MdBottomId);

                entity.HasIndex(e => e.MdTopId);

                entity.HasIndex(e => e.MudGasId);

                entity.HasIndex(e => e.RopAvId);

                entity.HasIndex(e => e.RopMnId);

                entity.HasIndex(e => e.RopMxId);

                entity.HasIndex(e => e.RpmAvId);

                entity.HasIndex(e => e.ShowId);

                entity.HasIndex(e => e.SizeMnId);

                entity.HasIndex(e => e.SizeMxId);

                entity.HasIndex(e => e.TqAvId);

                entity.HasIndex(e => e.TvdBaseId);

                entity.HasIndex(e => e.TvdTopId);

                entity.HasIndex(e => e.WobAvId);

                entity.HasIndex(e => e.WtMudAvId);
            });

            modelBuilder.Entity<ConvCoreLithologys>(entity =>
            {
                entity.HasIndex(e => e.DensShaleId);

                entity.HasIndex(e => e.LithPcId);
            });

            modelBuilder.Entity<ConvCoreMudGass>(entity =>
            {
                entity.HasIndex(e => e.GasAvId);

                entity.HasIndex(e => e.GasBackgndId);

                entity.HasIndex(e => e.GasConAvId);

                entity.HasIndex(e => e.GasConMxId);

                entity.HasIndex(e => e.GasPeakId);

                entity.HasIndex(e => e.GasTripGasConTripId);
            });

            modelBuilder.Entity<ConvCoreQualifiers>(entity =>
            {
                entity.HasIndex(e => e.ConvCoreLithologyLithologyId);
            });

            modelBuilder.Entity<ConvCoreShows>(entity =>
            {
                entity.HasIndex(e => e.NatFlorPcId);

                entity.HasIndex(e => e.StainPcId);
            });

            modelBuilder.Entity<ConvCores>(entity =>
            {
                entity.HasIndex(e => e.CommonDataId);

                entity.HasIndex(e => e.DiaBitId);

                entity.HasIndex(e => e.DiaCoreId);

                entity.HasIndex(e => e.GeologyIntervalId);

                entity.HasIndex(e => e.InclHoleId);

                entity.HasIndex(e => e.LenBarrelId);

                entity.HasIndex(e => e.LenCoredId);

                entity.HasIndex(e => e.LenRecoveredId);

                entity.HasIndex(e => e.MdCoreBottomId);

                entity.HasIndex(e => e.MdCoreTopId);

                entity.HasIndex(e => e.RecoverPcLenRecoveredId);
            });

            modelBuilder.Entity<CoordinateReferenceSystem>(entity =>
            {
                entity.HasIndex(e => e.GeodeticCrscoRefGeodeticCrsid);

                entity.HasIndex(e => e.ProjectedCrsid);

                entity.HasIndex(e => e.VerticalCrsid);
            });

            modelBuilder.Entity<CoordinateReferenceSystemBaseGeographicCrs>(entity =>
            {
                entity.HasIndex(e => e.GeographicCrsid);
            });

            modelBuilder.Entity<CoordinateReferenceSystemCartesianCs>(entity =>
            {
                entity.HasIndex(e => e.IdentifierCodeSpace);

                entity.HasIndex(e => e.NameCodeSpace);
            });

            modelBuilder.Entity<CoordinateReferenceSystemConversion>(entity =>
            {
                entity.HasIndex(e => e.IdentifierCodeSpace);

                entity.HasIndex(e => e.NameCodeSpace);

                entity.HasIndex(e => e.UsesMethodId);
            });

            modelBuilder.Entity<CoordinateReferenceSystemCoordinateSystemAxis>(entity =>
            {
                entity.HasIndex(e => e.AxisDirectionCodeSpace);

                entity.HasIndex(e => e.IdentifierCodeSpace);
            });

            modelBuilder.Entity<CoordinateReferenceSystemDefinedByConversion>(entity =>
            {
                entity.HasIndex(e => e.ConversionId);
            });

            modelBuilder.Entity<CoordinateReferenceSystemEllipsoid>(entity =>
            {
                entity.HasIndex(e => e.IdentifierCodeSpace);

                entity.HasIndex(e => e.SecondDefiningParameterId);

                entity.HasIndex(e => e.SemiMajorAxisUom);
            });

            modelBuilder.Entity<CoordinateReferenceSystemEllipsoidalCs>(entity =>
            {
                entity.HasIndex(e => e.IdentifierCodeSpace);

                entity.HasIndex(e => e.NameCodeSpace);
            });

            modelBuilder.Entity<CoordinateReferenceSystemGeodeticCrs>(entity =>
            {
                entity.HasIndex(e => e.GmlGeodeticCrsid);

                entity.HasIndex(e => e.NameCrscode);
            });

            modelBuilder.Entity<CoordinateReferenceSystemGeodeticDatum>(entity =>
            {
                entity.HasIndex(e => e.IdentifierCodeSpace);

                entity.HasIndex(e => e.UsesEllipsoidId);

                entity.HasIndex(e => e.UsesPrimeMeridianId);
            });

            modelBuilder.Entity<CoordinateReferenceSystemGeographicCrs>(entity =>
            {
                entity.HasIndex(e => e.IdentifierCodeSpace);

                entity.HasIndex(e => e.NameCodeSpace);

                entity.HasIndex(e => e.UsesEllipsoidalCsellipsId);

                entity.HasIndex(e => e.UsesGeodeticDatumId);
            });

            modelBuilder.Entity<CoordinateReferenceSystemGmlGeodeticCrs>(entity =>
            {
                entity.HasIndex(e => e.EllipsoidalCsid);

                entity.HasIndex(e => e.IdentifierCodeSpace);

                entity.HasIndex(e => e.NameCodeSpace);

                entity.HasIndex(e => e.UsesGeodeticDatumId);
            });

            modelBuilder.Entity<CoordinateReferenceSystemName>(entity =>
            {
                entity.HasIndex(e => e.EllipsoidId);

                entity.HasIndex(e => e.GeodeticDatumId);
            });

            modelBuilder.Entity<CoordinateReferenceSystemOperationMethod>(entity =>
            {
                entity.HasIndex(e => e.IdentifierCodeSpace);

                entity.HasIndex(e => e.NameCodeSpace);
            });

            modelBuilder.Entity<CoordinateReferenceSystemOperationParameter>(entity =>
            {
                entity.HasIndex(e => e.IdentifierCodeSpace);

                entity.HasIndex(e => e.NameCodeSpace);
            });

            modelBuilder.Entity<CoordinateReferenceSystemParameterValue>(entity =>
            {
                entity.HasIndex(e => e.ValueId);

                entity.HasIndex(e => e.ValueOfParameterId1);
            });

            modelBuilder.Entity<CoordinateReferenceSystemPrimeMeridian>(entity =>
            {
                entity.HasIndex(e => e.GreenwichLongitudeUom);

                entity.HasIndex(e => e.IdentifierCodeSpace);

                entity.HasIndex(e => e.NameCodeSpace);
            });

            modelBuilder.Entity<CoordinateReferenceSystemProjectedCrs>(entity =>
            {
                entity.HasIndex(e => e.BaseGeographicCrsid);

                entity.HasIndex(e => e.DefinedByConversionId);

                entity.HasIndex(e => e.IdentifierCodeSpace);

                entity.HasIndex(e => e.NameCodeSpace);

                entity.HasIndex(e => e.UsesCartesianCsid);
            });

            modelBuilder.Entity<CoordinateReferenceSystemSecondDefiningParameter>(entity =>
            {
                entity.HasIndex(e => e.InverseFlatteningUom);
            });

            modelBuilder.Entity<CoordinateReferenceSystemUsesAxis>(entity =>
            {
                entity.HasIndex(e => e.CartesianCsid);

                entity.HasIndex(e => e.CoordinateSystemAxisId);

                entity.HasIndex(e => e.EllipsoidalCsid);
            });

            modelBuilder.Entity<CoordinateReferenceSystemUsesCartesianCs>(entity =>
            {
                entity.HasIndex(e => e.CartesianCsid);
            });

            modelBuilder.Entity<CoordinateReferenceSystemUsesEllipsoid>(entity =>
            {
                entity.HasIndex(e => e.EllipsoidId);
            });

            modelBuilder.Entity<CoordinateReferenceSystemUsesEllipsoidalCs>(entity =>
            {
                entity.HasIndex(e => e.EllipsoidalCsid);
            });

            modelBuilder.Entity<CoordinateReferenceSystemUsesGeodeticDatum>(entity =>
            {
                entity.HasIndex(e => e.GeodeticDatumId);
            });

            modelBuilder.Entity<CoordinateReferenceSystemUsesMethod>(entity =>
            {
                entity.HasIndex(e => e.OperationMethodId);
            });

            modelBuilder.Entity<CoordinateReferenceSystemUsesParameter>(entity =>
            {
                entity.HasIndex(e => e.OperationMethodId);

                entity.HasIndex(e => e.OperationParameterId);
            });

            modelBuilder.Entity<CoordinateReferenceSystemUsesPrimeMeridian>(entity =>
            {
                entity.HasIndex(e => e.PrimeMeridianId);
            });

            modelBuilder.Entity<CoordinateReferenceSystemUsesValue>(entity =>
            {
                entity.HasIndex(e => e.ConversionId);

                entity.HasIndex(e => e.ParameterValueValueOfParameterId);
            });

            modelBuilder.Entity<CoordinateReferenceSystemVerticalCrs>(entity =>
            {
                entity.HasIndex(e => e.NameCrscode);
            });

            modelBuilder.Entity<DrillReportActivity>(entity =>
            {
                entity.HasIndex(e => e.DrillReportId);

                entity.HasIndex(e => e.MdId);

                entity.HasIndex(e => e.TvdId);
            });

            modelBuilder.Entity<DrillReportBitRecord>(entity =>
            {
                entity.HasIndex(e => e.DiaBitId);
            });

            modelBuilder.Entity<DrillReportCommonData>(entity =>
            {
                entity.HasIndex(e => e.DefaultDatumId);
            });

            modelBuilder.Entity<DrillReportControlIncidentInfo>(entity =>
            {
                entity.HasIndex(e => e.DiaBitId);

                entity.HasIndex(e => e.DiaCsgLastId);

                entity.HasIndex(e => e.EtimLostId);

                entity.HasIndex(e => e.MdBitId);

                entity.HasIndex(e => e.MdCsgLastId);

                entity.HasIndex(e => e.MdInflowId);

                entity.HasIndex(e => e.PorePressureId);

                entity.HasIndex(e => e.PresMaxChokeId);

                entity.HasIndex(e => e.PresShutInCasingId);

                entity.HasIndex(e => e.PresShutInDrillId);

                entity.HasIndex(e => e.TempBottomId);

                entity.HasIndex(e => e.TvdInflowId);

                entity.HasIndex(e => e.VolMudGainedId);

                entity.HasIndex(e => e.WtMudUom);
            });

            modelBuilder.Entity<DrillReportCoreInfo>(entity =>
            {
                entity.HasIndex(e => e.LenBarrelId);

                entity.HasIndex(e => e.LenRecoveredId);

                entity.HasIndex(e => e.MdBottomDrillReportMdBottomId);

                entity.HasIndex(e => e.MdTopId);

                entity.HasIndex(e => e.RecoverPcId);

                entity.HasIndex(e => e.TvdBottomId);

                entity.HasIndex(e => e.TvdTopDrillReportTvdTopId);
            });

            modelBuilder.Entity<DrillReportEquipFailureInfo>(entity =>
            {
                entity.HasIndex(e => e.EtimMissProductionId);

                entity.HasIndex(e => e.MdId);
            });

            modelBuilder.Entity<DrillReportFluids>(entity =>
            {
                entity.HasIndex(e => e.DensityId);

                entity.HasIndex(e => e.DrillReportId);

                entity.HasIndex(e => e.MdId);

                entity.HasIndex(e => e.PresBopRatingId);

                entity.HasIndex(e => e.PvId);

                entity.HasIndex(e => e.TempVisId);

                entity.HasIndex(e => e.TvdId);
            });

            modelBuilder.Entity<DrillReportFormTestInfo>(entity =>
            {
                entity.HasIndex(e => e.DensityHcid);

                entity.HasIndex(e => e.MdId);

                entity.HasIndex(e => e.MdSampleId);

                entity.HasIndex(e => e.PresPoreId);

                entity.HasIndex(e => e.TvdId);

                entity.HasIndex(e => e.VolumeSampleId);
            });

            modelBuilder.Entity<DrillReportGasReadingInfo>(entity =>
            {
                entity.HasIndex(e => e.EthId);

                entity.HasIndex(e => e.GasHighId);

                entity.HasIndex(e => e.GasLowId);

                entity.HasIndex(e => e.IbutId);

                entity.HasIndex(e => e.IpentId);

                entity.HasIndex(e => e.MdBottomDrillReportMdBottomId);

                entity.HasIndex(e => e.MdTopId);

                entity.HasIndex(e => e.MethId);

                entity.HasIndex(e => e.NbutId);

                entity.HasIndex(e => e.PropId);

                entity.HasIndex(e => e.TvdBottomId);

                entity.HasIndex(e => e.TvdTopDrillReportTvdTopId);
            });

            modelBuilder.Entity<DrillReportLithShowInfo>(entity =>
            {
                entity.HasIndex(e => e.MdBottomDrillReportMdBottomId);

                entity.HasIndex(e => e.MdTopId);

                entity.HasIndex(e => e.TvdBottomId);

                entity.HasIndex(e => e.TvdTopDrillReportTvdTopId);
            });

            modelBuilder.Entity<DrillReportLogInfo>(entity =>
            {
                entity.HasIndex(e => e.DrillReportId);

                entity.HasIndex(e => e.EtimStaticId);

                entity.HasIndex(e => e.MdBottomDrillReportMdBottomId);

                entity.HasIndex(e => e.MdTempToolId);

                entity.HasIndex(e => e.MdTopId);

                entity.HasIndex(e => e.TempBhctid);

                entity.HasIndex(e => e.TempBhstid);

                entity.HasIndex(e => e.TvdBottomId);

                entity.HasIndex(e => e.TvdTempToolId);

                entity.HasIndex(e => e.TvdTopDrillReportTvdTopId);
            });

            modelBuilder.Entity<DrillReportPerfInfo>(entity =>
            {
                entity.HasIndex(e => e.MdBottomDrillReportMdBottomId);

                entity.HasIndex(e => e.MdTopId);

                entity.HasIndex(e => e.TvdBottomId);

                entity.HasIndex(e => e.TvdTopDrillReportTvdTopId);
            });

            modelBuilder.Entity<DrillReportPorePressure>(entity =>
            {
                entity.HasIndex(e => e.DrillReportId);

                entity.HasIndex(e => e.EquivalentMudWeightId);

                entity.HasIndex(e => e.MdId);

                entity.HasIndex(e => e.TvdId);
            });

            modelBuilder.Entity<DrillReportRigAlias>(entity =>
            {
                entity.HasIndex(e => e.DrillReportWellboreInfoWellboreInfoId);
            });

            modelBuilder.Entity<DrillReportStatusInfo>(entity =>
            {
                entity.HasIndex(e => e.DiaCsgLastId);

                entity.HasIndex(e => e.DiaHoleId);

                entity.HasIndex(e => e.DiaPilotId);

                entity.HasIndex(e => e.DistDrillId);

                entity.HasIndex(e => e.MdCsgLastId);

                entity.HasIndex(e => e.MdDiaHoleStartId);

                entity.HasIndex(e => e.MdDiaPilotPlanId);

                entity.HasIndex(e => e.MdId);

                entity.HasIndex(e => e.MdKickoffId);

                entity.HasIndex(e => e.MdPlugTopId);

                entity.HasIndex(e => e.MdStrengthFormId);

                entity.HasIndex(e => e.RopCurrentId);

                entity.HasIndex(e => e.StrengthFormId);

                entity.HasIndex(e => e.TvdId);
            });

            modelBuilder.Entity<DrillReportStratInfo>(entity =>
            {
                entity.HasIndex(e => e.MdTopId);

                entity.HasIndex(e => e.TvdTopDrillReportTvdTopId);
            });

            modelBuilder.Entity<DrillReportSurveyStation>(entity =>
            {
                entity.HasIndex(e => e.AziId);

                entity.HasIndex(e => e.InclId);

                entity.HasIndex(e => e.MdId);

                entity.HasIndex(e => e.TvdId);
            });

            modelBuilder.Entity<DrillReportWellCr>(entity =>
            {
                entity.HasIndex(e => e.GeodeticCrsid);
            });

            modelBuilder.Entity<DrillReportWellDatum>(entity =>
            {
                entity.HasIndex(e => e.DrillReportId);

                entity.HasIndex(e => e.ElevationId);
            });

            modelBuilder.Entity<DrillReportWellTestInfo>(entity =>
            {
                entity.HasIndex(e => e.CarbonDioxideId);

                entity.HasIndex(e => e.ChlorideId);

                entity.HasIndex(e => e.ChokeOrificeSizeId);

                entity.HasIndex(e => e.DensityGasId);

                entity.HasIndex(e => e.DensityOilId);

                entity.HasIndex(e => e.DensityWaterId);

                entity.HasIndex(e => e.FlowRateGasId);

                entity.HasIndex(e => e.FlowRateOilId);

                entity.HasIndex(e => e.FlowRateWaterId);

                entity.HasIndex(e => e.GasOilRatioId);

                entity.HasIndex(e => e.HydrogenSulfideId);

                entity.HasIndex(e => e.MdBottomDrillReportMdBottomId);

                entity.HasIndex(e => e.MdTopId);

                entity.HasIndex(e => e.PresBottomId);

                entity.HasIndex(e => e.PresFlowingId);

                entity.HasIndex(e => e.PresShutInId);

                entity.HasIndex(e => e.TvdBottomId);

                entity.HasIndex(e => e.TvdTopDrillReportTvdTopId);

                entity.HasIndex(e => e.VolGasTotalId);

                entity.HasIndex(e => e.VolOilStoredId);

                entity.HasIndex(e => e.VolOilTotalId);

                entity.HasIndex(e => e.VolWaterTotalId);

                entity.HasIndex(e => e.WaterOilRatioId);
            });

            modelBuilder.Entity<DrillReportWellboreAlias>(entity =>
            {
                entity.HasIndex(e => e.DrillReportId);
            });

            modelBuilder.Entity<DrillReports>(entity =>
            {
                entity.HasIndex(e => e.BitRecordId);

                entity.HasIndex(e => e.CommonDataId);

                entity.HasIndex(e => e.ControlIncidentInfoId);

                entity.HasIndex(e => e.CoreInfoId);

                entity.HasIndex(e => e.EquipFailureInfoId);

                entity.HasIndex(e => e.ExtendedReportId);

                entity.HasIndex(e => e.FormTestInfoId);

                entity.HasIndex(e => e.GasReadingInfoId);

                entity.HasIndex(e => e.LithShowInfoId);

                entity.HasIndex(e => e.PerfInfoId);

                entity.HasIndex(e => e.StatusInfoId);

                entity.HasIndex(e => e.StratInfoId);

                entity.HasIndex(e => e.SurveyStationId);

                entity.HasIndex(e => e.WellAliasId);

                entity.HasIndex(e => e.WellCrsuid);

                entity.HasIndex(e => e.WellTestInfoId);

                entity.HasIndex(e => e.WellboreInfoId);
            });

            modelBuilder.Entity<FluidsReportFluid>(entity =>
            {
                entity.HasIndex(e => e.AlkalinityP1id);

                entity.HasIndex(e => e.AlkalinityP2id);

                entity.HasIndex(e => e.BaritePcId);

                entity.HasIndex(e => e.BrinePcId);

                entity.HasIndex(e => e.CalciumChlorideId);

                entity.HasIndex(e => e.CalciumId);

                entity.HasIndex(e => e.ChlorideId);

                entity.HasIndex(e => e.DensityId);

                entity.HasIndex(e => e.ElectStabId);

                entity.HasIndex(e => e.FilterCakeHthpId);

                entity.HasIndex(e => e.FilterCakeLtlpId);

                entity.HasIndex(e => e.FiltrateHthpId);

                entity.HasIndex(e => e.FiltrateLtlpId);

                entity.HasIndex(e => e.Gel10MinId);

                entity.HasIndex(e => e.Gel10SecId);

                entity.HasIndex(e => e.Gel30MinId);

                entity.HasIndex(e => e.HardnessCaId);

                entity.HasIndex(e => e.LcmId);

                entity.HasIndex(e => e.LimeId);

                entity.HasIndex(e => e.MagnesiumId);

                entity.HasIndex(e => e.MbtId);

                entity.HasIndex(e => e.MfId);

                entity.HasIndex(e => e.OilCtgId);

                entity.HasIndex(e => e.OilPcId);

                entity.HasIndex(e => e.PmFiltrateId);

                entity.HasIndex(e => e.PmId);

                entity.HasIndex(e => e.PolymerId);

                entity.HasIndex(e => e.PotassiumId);

                entity.HasIndex(e => e.PresHthpId);

                entity.HasIndex(e => e.PvReportPvId);

                entity.HasIndex(e => e.SandPcId);

                entity.HasIndex(e => e.SolCorPcId);

                entity.HasIndex(e => e.SolidsCalcPcId);

                entity.HasIndex(e => e.SolidsHiGravPcId);

                entity.HasIndex(e => e.SolidsLowGravPcId);

                entity.HasIndex(e => e.SolidsPcId);

                entity.HasIndex(e => e.SulfideId);

                entity.HasIndex(e => e.TempHthpId);

                entity.HasIndex(e => e.TempPhId);

                entity.HasIndex(e => e.TempVisId);

                entity.HasIndex(e => e.VisFunnelId);

                entity.HasIndex(e => e.WaterPcId);

                entity.HasIndex(e => e.YpReportYpId);
            });

            modelBuilder.Entity<FluidsReportRheometer>(entity =>
            {
                entity.HasIndex(e => e.FluidsReportFluidUid);

                entity.HasIndex(e => e.PresRheomId);

                entity.HasIndex(e => e.TempRheomId);
            });

            modelBuilder.Entity<FluidsReports>(entity =>
            {
                entity.HasIndex(e => e.CommonDataId);

                entity.HasIndex(e => e.FluidUid);

                entity.HasIndex(e => e.MdId);

                entity.HasIndex(e => e.TvdId);
            });

            modelBuilder.Entity<FormationMarkers>(entity =>
            {
                entity.HasIndex(e => e.ChronostratigraphicId);

                entity.HasIndex(e => e.CommonDataFormationMarkerCommonDataId);

                entity.HasIndex(e => e.DipDirectionId);

                entity.HasIndex(e => e.DipId);

                entity.HasIndex(e => e.LithostratigraphicId);

                entity.HasIndex(e => e.MdLogSampleId);

                entity.HasIndex(e => e.MdPrognosedId);

                entity.HasIndex(e => e.MdTopSampleId);

                entity.HasIndex(e => e.ThicknessApparentId);

                entity.HasIndex(e => e.ThicknessBedId);

                entity.HasIndex(e => e.ThicknessPerpenId);

                entity.HasIndex(e => e.TvdLogSampleId);

                entity.HasIndex(e => e.TvdPrognosedId);

                entity.HasIndex(e => e.TvdTopSampleId);
            });

            modelBuilder.Entity<LogCurveInfos>(entity =>
            {
                entity.HasIndex(e => e.LogId);

                entity.HasIndex(e => e.MaxIndexId);

                entity.HasIndex(e => e.MinIndexId);

                entity.HasIndex(e => e.SensorOffsetId);
            });

            modelBuilder.Entity<LogParams>(entity =>
            {
                entity.HasIndex(e => e.LogId);
            });

            modelBuilder.Entity<Logs>(entity =>
            {
                entity.HasIndex(e => e.CommonDataLogCommonDataId);

                entity.HasIndex(e => e.EndIndexLogEndIndexId);

                entity.HasIndex(e => e.LogDataId);

                entity.HasIndex(e => e.StartIndexLogStartIndexId);

                entity.HasIndex(e => e.StepIncrementId);
            });

            modelBuilder.Entity<Messages>(entity =>
            {
                entity.HasIndex(e => e.CommonDataMessageCommonDataId);

                entity.HasIndex(e => e.MdBitId);

                entity.HasIndex(e => e.MdId);

                entity.HasIndex(e => e.ParamIndex);
            });

            modelBuilder.Entity<MudLogChromatograph>(entity =>
            {
                entity.HasIndex(e => e.AcetyleneId);

                entity.HasIndex(e => e.Co2AvId);

                entity.HasIndex(e => e.Co2MnId);

                entity.HasIndex(e => e.Co2MxId);

                entity.HasIndex(e => e.EpentAvId);

                entity.HasIndex(e => e.EpentMnId);

                entity.HasIndex(e => e.EpentMxId);

                entity.HasIndex(e => e.EthAvId);

                entity.HasIndex(e => e.EthMnId);

                entity.HasIndex(e => e.EthMxId);

                entity.HasIndex(e => e.EtimChromCycleId);

                entity.HasIndex(e => e.H2sAvId);

                entity.HasIndex(e => e.H2sMnId);

                entity.HasIndex(e => e.H2sMxId);

                entity.HasIndex(e => e.IbutAvId);

                entity.HasIndex(e => e.IbutMnId);

                entity.HasIndex(e => e.IbutMxId);

                entity.HasIndex(e => e.IhexAvId);

                entity.HasIndex(e => e.IhexMnId);

                entity.HasIndex(e => e.IhexMxId);

                entity.HasIndex(e => e.IpentAvId);

                entity.HasIndex(e => e.IpentMnId);

                entity.HasIndex(e => e.IpentMxId);

                entity.HasIndex(e => e.MdBottomId);

                entity.HasIndex(e => e.MdTopId);

                entity.HasIndex(e => e.MethAvId);

                entity.HasIndex(e => e.MethMnId);

                entity.HasIndex(e => e.MethMxId);

                entity.HasIndex(e => e.NbutAvId);

                entity.HasIndex(e => e.NbutMnId);

                entity.HasIndex(e => e.NbutMxId);

                entity.HasIndex(e => e.NhexAvId);

                entity.HasIndex(e => e.NhexMnId);

                entity.HasIndex(e => e.NhexMxId);

                entity.HasIndex(e => e.NpentAvId);

                entity.HasIndex(e => e.NpentMnId);

                entity.HasIndex(e => e.NpentMxId);

                entity.HasIndex(e => e.PropAvId);

                entity.HasIndex(e => e.PropMnId);

                entity.HasIndex(e => e.PropMxId);

                entity.HasIndex(e => e.WtMudInId);

                entity.HasIndex(e => e.WtMudOutId);
            });

            modelBuilder.Entity<MudLogChronostratigraphic>(entity =>
            {
                entity.HasIndex(e => e.MudLogGeologyIntervalGeologyIntervalId);
            });

            modelBuilder.Entity<MudLogGeologyInterval>(entity =>
            {
                entity.HasIndex(e => e.CalcStabId);

                entity.HasIndex(e => e.CalciteId);

                entity.HasIndex(e => e.CecId);

                entity.HasIndex(e => e.ChromatographId);

                entity.HasIndex(e => e.CommonTimeId);

                entity.HasIndex(e => e.DensBulkId);

                entity.HasIndex(e => e.DensShaleId);

                entity.HasIndex(e => e.DolomiteId);

                entity.HasIndex(e => e.EcdTdAvId);

                entity.HasIndex(e => e.LenPlugId);

                entity.HasIndex(e => e.LithologyId);

                entity.HasIndex(e => e.LithostratigraphicId);

                entity.HasIndex(e => e.MdBottomId);

                entity.HasIndex(e => e.MdTopId);

                entity.HasIndex(e => e.MudGasId);

                entity.HasIndex(e => e.RopAvId);

                entity.HasIndex(e => e.RopMnId);

                entity.HasIndex(e => e.RopMxId);

                entity.HasIndex(e => e.RpmAvId);

                entity.HasIndex(e => e.ShowId);

                entity.HasIndex(e => e.SizeMnId);

                entity.HasIndex(e => e.SizeMxId);

                entity.HasIndex(e => e.TqAvId);

                entity.HasIndex(e => e.TvdBaseId);

                entity.HasIndex(e => e.TvdTopId);

                entity.HasIndex(e => e.WobAvId);

                entity.HasIndex(e => e.WtMudAvId);
            });

            modelBuilder.Entity<MudLogLithology>(entity =>
            {
                entity.HasIndex(e => e.DensShaleId);

                entity.HasIndex(e => e.QualifierId);
            });

            modelBuilder.Entity<MudLogMudGas>(entity =>
            {
                entity.HasIndex(e => e.GasAvId);

                entity.HasIndex(e => e.GasBackgndId);

                entity.HasIndex(e => e.GasConAvId);

                entity.HasIndex(e => e.GasConMxId);

                entity.HasIndex(e => e.GasPeakId);

                entity.HasIndex(e => e.GasTripId);
            });

            modelBuilder.Entity<MudLogParameter>(entity =>
            {
                entity.HasIndex(e => e.CommonTimeId);

                entity.HasIndex(e => e.ForceId);

                entity.HasIndex(e => e.MdBottomId);

                entity.HasIndex(e => e.MdTopId);

                entity.HasIndex(e => e.MudLogId);
            });

            modelBuilder.Entity<MudLogQualifier>(entity =>
            {
                entity.HasIndex(e => e.AbundanceId);
            });

            modelBuilder.Entity<MudLogShow>(entity =>
            {
                entity.HasIndex(e => e.NatFlorPcId);

                entity.HasIndex(e => e.StainPcId);
            });

            modelBuilder.Entity<MudLogs>(entity =>
            {
                entity.HasIndex(e => e.CommonDataMudLogCommonDataId);

                entity.HasIndex(e => e.EndMdId);

                entity.HasIndex(e => e.GeologyIntervalId);

                entity.HasIndex(e => e.StartMdId);
            });

            modelBuilder.Entity<ObjectGroupCommonDatas>(entity =>
            {
                entity.HasIndex(e => e.AcquisitionTimeZoneId);

                entity.HasIndex(e => e.DefaultDatumId);

                entity.HasIndex(e => e.ExtensionNameValueId);
            });

            modelBuilder.Entity<ObjectGroupExtensionNameValues>(entity =>
            {
                entity.HasIndex(e => e.MdId);

                entity.HasIndex(e => e.ValueId);
            });

            modelBuilder.Entity<ObjectGroupMemberObjects>(entity =>
            {
                entity.HasIndex(e => e.ExtensionNameValueId);

                entity.HasIndex(e => e.ObjectReferenceId);

                entity.HasIndex(e => e.ParamId);

                entity.HasIndex(e => e.RangeMaxId);

                entity.HasIndex(e => e.RangeMinId);

                entity.HasIndex(e => e.ReferenceDepthId);

                entity.HasIndex(e => e.Sequence1Id);

                entity.HasIndex(e => e.Sequence2Id);

                entity.HasIndex(e => e.Sequence3Id);
            });

            modelBuilder.Entity<ObjectGroups>(entity =>
            {
                entity.HasIndex(e => e.CommonDataObjectGroupCommonDataId);

                entity.HasIndex(e => e.MemberObjectId);

                entity.HasIndex(e => e.ParamId);
            });

            modelBuilder.Entity<OpsReportActivitys>(entity =>
            {
                entity.HasIndex(e => e.DurationId);

                entity.HasIndex(e => e.MdBitEndId);

                entity.HasIndex(e => e.MdBitStartId);

                entity.HasIndex(e => e.MdHoleEndId);

                entity.HasIndex(e => e.MdHoleStartId);

                entity.HasIndex(e => e.TvdHoleEndId);

                entity.HasIndex(e => e.TvdHoleStartId);
            });

            modelBuilder.Entity<OpsReportAnchorAngles>(entity =>
            {
                entity.HasIndex(e => e.OpsReportRigResponseRigResponseId);
            });

            modelBuilder.Entity<OpsReportAnchorTensions>(entity =>
            {
                entity.HasIndex(e => e.OpsReportRigResponseRigResponseId);
            });

            modelBuilder.Entity<OpsReportBulks>(entity =>
            {
                entity.HasIndex(e => e.CostItemId);

                entity.HasIndex(e => e.ItemVolPerUnitId);

                entity.HasIndex(e => e.PricePerUnitId);
            });

            modelBuilder.Entity<OpsReportCorUseds>(entity =>
            {
                entity.HasIndex(e => e.DirSensorOffsetId);

                entity.HasIndex(e => e.GravAxialAccelCorId);

                entity.HasIndex(e => e.GravTran1AccelCorId);

                entity.HasIndex(e => e.GravTran2AccelCorId);

                entity.HasIndex(e => e.MagAxialDrlstrCorId);

                entity.HasIndex(e => e.MagTran1DrlstrCorId);

                entity.HasIndex(e => e.MagTran2DrlstrCorId);

                entity.HasIndex(e => e.SagAziCorId);

                entity.HasIndex(e => e.SagIncCorId);

                entity.HasIndex(e => e.StnGridCorUsedId);

                entity.HasIndex(e => e.StnMagDeclUsedId);
            });

            modelBuilder.Entity<OpsReportDayCosts>(entity =>
            {
                entity.HasIndex(e => e.CostAmountId);

                entity.HasIndex(e => e.CostPerItemId);

                entity.HasIndex(e => e.OpsReportId);
            });

            modelBuilder.Entity<OpsReportDrillingParams>(entity =>
            {
                entity.HasIndex(e => e.AziBottomId);

                entity.HasIndex(e => e.AziTopId);

                entity.HasIndex(e => e.CtimCircId);

                entity.HasIndex(e => e.CtimDrillRotId);

                entity.HasIndex(e => e.CtimDrillSlidId);

                entity.HasIndex(e => e.CtimHoldId);

                entity.HasIndex(e => e.CtimReamId);

                entity.HasIndex(e => e.CtimSteeringId);

                entity.HasIndex(e => e.DistDrillRotId);

                entity.HasIndex(e => e.DistDrillSlidId);

                entity.HasIndex(e => e.DistHoldId);

                entity.HasIndex(e => e.DistReamId);

                entity.HasIndex(e => e.DistSteeringId);

                entity.HasIndex(e => e.EtimOpBitId);

                entity.HasIndex(e => e.FlowrateBitId);

                entity.HasIndex(e => e.FlowratePumpId);

                entity.HasIndex(e => e.HkldDnId);

                entity.HasIndex(e => e.HkldRotId);

                entity.HasIndex(e => e.HkldUpId);

                entity.HasIndex(e => e.InclMnId);

                entity.HasIndex(e => e.InclMxId);

                entity.HasIndex(e => e.InclStartId);

                entity.HasIndex(e => e.InclStopId);

                entity.HasIndex(e => e.MdHoleStartId);

                entity.HasIndex(e => e.MdHoleStopId);

                entity.HasIndex(e => e.OverPullId);

                entity.HasIndex(e => e.PowBitId);

                entity.HasIndex(e => e.PresDropBitId);

                entity.HasIndex(e => e.PresPumpAvId);

                entity.HasIndex(e => e.RopAvId);

                entity.HasIndex(e => e.RopMnId);

                entity.HasIndex(e => e.RopMxId);

                entity.HasIndex(e => e.RpmAvDhId);

                entity.HasIndex(e => e.RpmAvId);

                entity.HasIndex(e => e.RpmMnId);

                entity.HasIndex(e => e.RpmMxId);

                entity.HasIndex(e => e.SlackOffId);

                entity.HasIndex(e => e.TempMudDhMxId);

                entity.HasIndex(e => e.TqDhAvId);

                entity.HasIndex(e => e.TqOffBotAvId);

                entity.HasIndex(e => e.TqOnBotAvId);

                entity.HasIndex(e => e.TqOnBotMnId);

                entity.HasIndex(e => e.TqOnBotMxId);

                entity.HasIndex(e => e.TubularUidRef);

                entity.HasIndex(e => e.VelNozzleAvId);

                entity.HasIndex(e => e.WobAvDhId);

                entity.HasIndex(e => e.WobAvId);

                entity.HasIndex(e => e.WobMnId);

                entity.HasIndex(e => e.WobMxId);

                entity.HasIndex(e => e.WtAboveJarId);

                entity.HasIndex(e => e.WtBelowJarId);

                entity.HasIndex(e => e.WtMudUom);
            });

            modelBuilder.Entity<OpsReportFluids>(entity =>
            {
                entity.HasIndex(e => e.AlkalinityP1id);

                entity.HasIndex(e => e.AlkalinityP2id);

                entity.HasIndex(e => e.BaritePcId);

                entity.HasIndex(e => e.BrinePcId);

                entity.HasIndex(e => e.CalciumChlorideId);

                entity.HasIndex(e => e.CalciumMagnesiumId);

                entity.HasIndex(e => e.ChlorideId);

                entity.HasIndex(e => e.DensityId);

                entity.HasIndex(e => e.ElectStabId);

                entity.HasIndex(e => e.FilterCakeHthpId);

                entity.HasIndex(e => e.FilterCakeLtlpId);

                entity.HasIndex(e => e.FiltrateHthpId);

                entity.HasIndex(e => e.FiltrateLtlpId);

                entity.HasIndex(e => e.Gel10MinId);

                entity.HasIndex(e => e.Gel10SecId);

                entity.HasIndex(e => e.Gel30MinId);

                entity.HasIndex(e => e.HardnessCaId);

                entity.HasIndex(e => e.LcmId);

                entity.HasIndex(e => e.LimeId);

                entity.HasIndex(e => e.MagnesiumId);

                entity.HasIndex(e => e.MbtId);

                entity.HasIndex(e => e.MfId);

                entity.HasIndex(e => e.OilCtgId);

                entity.HasIndex(e => e.OilPcId);

                entity.HasIndex(e => e.PmFiltrateId);

                entity.HasIndex(e => e.PmId);

                entity.HasIndex(e => e.PolymerId);

                entity.HasIndex(e => e.PresHthpId);

                entity.HasIndex(e => e.PvReportPvId);

                entity.HasIndex(e => e.SandPcId);

                entity.HasIndex(e => e.SolCorPcId);

                entity.HasIndex(e => e.SolidsCalcPcId);

                entity.HasIndex(e => e.SolidsHiGravPcId);

                entity.HasIndex(e => e.SolidsLowGravPcId);

                entity.HasIndex(e => e.SolidsPcId);

                entity.HasIndex(e => e.SulfideId);

                entity.HasIndex(e => e.TempHthpId);

                entity.HasIndex(e => e.TempPhId);

                entity.HasIndex(e => e.TempVisId);

                entity.HasIndex(e => e.VisFunnelId);

                entity.HasIndex(e => e.WaterPcId);

                entity.HasIndex(e => e.YpId);
            });

            modelBuilder.Entity<OpsReportHses>(entity =>
            {
                entity.HasIndex(e => e.DaysIncFreeId);

                entity.HasIndex(e => e.FluidDischargedId);

                entity.HasIndex(e => e.IncidentId);

                entity.HasIndex(e => e.PresAnnularId);

                entity.HasIndex(e => e.PresChokeLineId);

                entity.HasIndex(e => e.PresChokeManId);

                entity.HasIndex(e => e.PresDiverterId);

                entity.HasIndex(e => e.PresKellyHoseId);

                entity.HasIndex(e => e.PresLastCsgId);

                entity.HasIndex(e => e.PresRamsId);

                entity.HasIndex(e => e.PresStdPipeId);

                entity.HasIndex(e => e.VolCtgDischargedId);

                entity.HasIndex(e => e.VolOilCtgDischargeId);

                entity.HasIndex(e => e.WasteDischargedId);
            });

            modelBuilder.Entity<OpsReportIncidents>(entity =>
            {
                entity.HasIndex(e => e.CostLostGrossId);

                entity.HasIndex(e => e.EtimLostGrossId);
            });

            modelBuilder.Entity<OpsReportLocations>(entity =>
            {
                entity.HasIndex(e => e.LatitudeId);

                entity.HasIndex(e => e.LongitudeId);

                entity.HasIndex(e => e.OpsReportTrajectoryStationUid);

                entity.HasIndex(e => e.ProjectedXid);

                entity.HasIndex(e => e.ProjectedYid);

                entity.HasIndex(e => e.WellCrsid);
            });

            modelBuilder.Entity<OpsReportMatrixCovs>(entity =>
            {
                entity.HasIndex(e => e.BiasEid);

                entity.HasIndex(e => e.BiasNid);

                entity.HasIndex(e => e.BiasVertId);

                entity.HasIndex(e => e.VarianceEeid);

                entity.HasIndex(e => e.VarianceEvertId);

                entity.HasIndex(e => e.VarianceNeopsReportsId);

                entity.HasIndex(e => e.VarianceNnid);

                entity.HasIndex(e => e.VarianceNvertId);

                entity.HasIndex(e => e.VarianceVertVertId);
            });

            modelBuilder.Entity<OpsReportMudInventorys>(entity =>
            {
                entity.HasIndex(e => e.CostItemId);

                entity.HasIndex(e => e.ItemWtPerUnitId);

                entity.HasIndex(e => e.PricePerUnitId);
            });

            modelBuilder.Entity<OpsReportMudLossess>(entity =>
            {
                entity.HasIndex(e => e.VolLostAbandonHoleId);

                entity.HasIndex(e => e.VolLostBhdCsgHoleId);

                entity.HasIndex(e => e.VolLostCircHoleId);

                entity.HasIndex(e => e.VolLostCmtHoleId);

                entity.HasIndex(e => e.VolLostCsgHoleId);

                entity.HasIndex(e => e.VolLostMudCleanerSurfId);

                entity.HasIndex(e => e.VolLostOtherHoleId);

                entity.HasIndex(e => e.VolLostOtherSurfId);

                entity.HasIndex(e => e.VolLostPitsSurfId);

                entity.HasIndex(e => e.VolLostShakerSurfId);

                entity.HasIndex(e => e.VolLostTrippingSurfId);

                entity.HasIndex(e => e.VolTotMudLostHoleId);

                entity.HasIndex(e => e.VolTotMudLostSurfId);
            });

            modelBuilder.Entity<OpsReportMudVolumes>(entity =>
            {
                entity.HasIndex(e => e.MudLossesId);

                entity.HasIndex(e => e.VolMudBuiltId);

                entity.HasIndex(e => e.VolMudCasingId);

                entity.HasIndex(e => e.VolMudDumpedId);

                entity.HasIndex(e => e.VolMudHoleId);

                entity.HasIndex(e => e.VolMudReceivedId);

                entity.HasIndex(e => e.VolMudReturnedId);

                entity.HasIndex(e => e.VolMudRiserId);

                entity.HasIndex(e => e.VolMudStringId);

                entity.HasIndex(e => e.VolTotMudEndId);

                entity.HasIndex(e => e.VolTotMudStartId);
            });

            modelBuilder.Entity<OpsReportPersonnels>(entity =>
            {
                entity.HasIndex(e => e.OpsReportId);

                entity.HasIndex(e => e.TotalTimeId);
            });

            modelBuilder.Entity<OpsReportPitVolumes>(entity =>
            {
                entity.HasIndex(e => e.DensFluidId);

                entity.HasIndex(e => e.OpsReportId);

                entity.HasIndex(e => e.PitId);

                entity.HasIndex(e => e.VisFunnelId);

                entity.HasIndex(e => e.VolPitId);
            });

            modelBuilder.Entity<OpsReportPumpOps>(entity =>
            {
                entity.HasIndex(e => e.IdLinerId);

                entity.HasIndex(e => e.LenStrokeId);

                entity.HasIndex(e => e.MdBitId);

                entity.HasIndex(e => e.PcEfficiencyId);

                entity.HasIndex(e => e.PressureId);

                entity.HasIndex(e => e.PumpId);

                entity.HasIndex(e => e.PumpOutputId);

                entity.HasIndex(e => e.RateStrokeId);
            });

            modelBuilder.Entity<OpsReportRawDatas>(entity =>
            {
                entity.HasIndex(e => e.GravAxialRawId);

                entity.HasIndex(e => e.GravTran1RawId);

                entity.HasIndex(e => e.GravTran2RawId);

                entity.HasIndex(e => e.MagAxialRawId);

                entity.HasIndex(e => e.MagTran1RawId);

                entity.HasIndex(e => e.MagTran2RawId);
            });

            modelBuilder.Entity<OpsReportRheometers>(entity =>
            {
                entity.HasIndex(e => e.OpsReportFluidUid);

                entity.HasIndex(e => e.PresRheomId);

                entity.HasIndex(e => e.TempRheomId);
            });

            modelBuilder.Entity<OpsReportRigResponses>(entity =>
            {
                entity.HasIndex(e => e.BallJointAngleId);

                entity.HasIndex(e => e.BallJointDirectionId);

                entity.HasIndex(e => e.DispRigId);

                entity.HasIndex(e => e.GuideBaseAngleId);

                entity.HasIndex(e => e.LoadLeg1Id);

                entity.HasIndex(e => e.LoadLeg2Id);

                entity.HasIndex(e => e.LoadLeg3Id);

                entity.HasIndex(e => e.LoadLeg4Id);

                entity.HasIndex(e => e.MeanDraftId);

                entity.HasIndex(e => e.OffsetRigId);

                entity.HasIndex(e => e.PenetrationLeg1Id);

                entity.HasIndex(e => e.PenetrationLeg2Id);

                entity.HasIndex(e => e.PenetrationLeg3Id);

                entity.HasIndex(e => e.PenetrationLeg4Id);

                entity.HasIndex(e => e.RigHeadingId);

                entity.HasIndex(e => e.RigHeaveId);

                entity.HasIndex(e => e.RigPitchAngleId);

                entity.HasIndex(e => e.RigRollAngleId);

                entity.HasIndex(e => e.RiserAngleId);

                entity.HasIndex(e => e.RiserDirectionId);

                entity.HasIndex(e => e.RiserTensionId);

                entity.HasIndex(e => e.TotalDeckLoadId);

                entity.HasIndex(e => e.VariableDeckLoadId);
            });

            modelBuilder.Entity<OpsReportScrs>(entity =>
            {
                entity.HasIndex(e => e.MdBitId);

                entity.HasIndex(e => e.OpsReportId);

                entity.HasIndex(e => e.PresRecordedId);

                entity.HasIndex(e => e.PumpId);

                entity.HasIndex(e => e.RateStrokeId);
            });

            modelBuilder.Entity<OpsReportShakerOps>(entity =>
            {
                entity.HasIndex(e => e.HoursRunId);

                entity.HasIndex(e => e.MdHoleId);

                entity.HasIndex(e => e.PcScreenCoveredId);

                entity.HasIndex(e => e.ShakerId);

                entity.HasIndex(e => e.ShakerScreenId);
            });

            modelBuilder.Entity<OpsReportShakerScreens>(entity =>
            {
                entity.HasIndex(e => e.CutPointId);

                entity.HasIndex(e => e.MeshXid);

                entity.HasIndex(e => e.MeshYid);
            });

            modelBuilder.Entity<OpsReportTrajectoryStations>(entity =>
            {
                entity.HasIndex(e => e.AziId);

                entity.HasIndex(e => e.CorUsedId);

                entity.HasIndex(e => e.DipAngleUncertId);

                entity.HasIndex(e => e.DispEwId);

                entity.HasIndex(e => e.DispNsId);

                entity.HasIndex(e => e.DlsId);

                entity.HasIndex(e => e.GravTotalUncertId);

                entity.HasIndex(e => e.GtfId);

                entity.HasIndex(e => e.InclId);

                entity.HasIndex(e => e.MagTotalUncertId);

                entity.HasIndex(e => e.MatrixCovId);

                entity.HasIndex(e => e.MdDeltaId);

                entity.HasIndex(e => e.MdId);

                entity.HasIndex(e => e.MtfId);

                entity.HasIndex(e => e.RateBuildId);

                entity.HasIndex(e => e.RateTurnId);

                entity.HasIndex(e => e.RawDataId);

                entity.HasIndex(e => e.TvdDeltaId);

                entity.HasIndex(e => e.TvdId);

                entity.HasIndex(e => e.ValidId);

                entity.HasIndex(e => e.VertSectId);
            });

            modelBuilder.Entity<OpsReportValids>(entity =>
            {
                entity.HasIndex(e => e.GravTotalFieldCalcId);

                entity.HasIndex(e => e.MagDipAngleCalcId);

                entity.HasIndex(e => e.MagTotalFieldCalcId);
            });

            modelBuilder.Entity<OpsReportWeathers>(entity =>
            {
                entity.HasIndex(e => e.AmtPrecipId);

                entity.HasIndex(e => e.AziCurrentSeaId);

                entity.HasIndex(e => e.AziWaveId);

                entity.HasIndex(e => e.AziWindId);

                entity.HasIndex(e => e.BarometricPressureId);

                entity.HasIndex(e => e.CeilingCloudId);

                entity.HasIndex(e => e.CurrentSeaId);

                entity.HasIndex(e => e.HtWaveId);

                entity.HasIndex(e => e.PeriodWaveId);

                entity.HasIndex(e => e.TempSurfaceMnId);

                entity.HasIndex(e => e.TempSurfaceMxId);

                entity.HasIndex(e => e.TempWindChillId);

                entity.HasIndex(e => e.TempseaId);

                entity.HasIndex(e => e.VelWindId);

                entity.HasIndex(e => e.VisibilityId);
            });

            modelBuilder.Entity<OpsReports>(entity =>
            {
                entity.HasIndex(e => e.ActivityUid);

                entity.HasIndex(e => e.BulkId);

                entity.HasIndex(e => e.CommonDataOpsReportsCommonDataid);

                entity.HasIndex(e => e.CostDayId);

                entity.HasIndex(e => e.CostDayMudId);

                entity.HasIndex(e => e.DiaCsgLastId);

                entity.HasIndex(e => e.DiaHoleId);

                entity.HasIndex(e => e.DistDrillId);

                entity.HasIndex(e => e.DistDrillRotId);

                entity.HasIndex(e => e.DistDrillSlidId);

                entity.HasIndex(e => e.DistHoldId);

                entity.HasIndex(e => e.DistReamId);

                entity.HasIndex(e => e.DistSteeringId);

                entity.HasIndex(e => e.DrillingParamsUid);

                entity.HasIndex(e => e.EtimCircId);

                entity.HasIndex(e => e.EtimDrillId);

                entity.HasIndex(e => e.EtimDrillRotId);

                entity.HasIndex(e => e.EtimDrillSlidId);

                entity.HasIndex(e => e.EtimHoldId);

                entity.HasIndex(e => e.EtimLocId);

                entity.HasIndex(e => e.EtimReamId);

                entity.HasIndex(e => e.EtimSpudId);

                entity.HasIndex(e => e.EtimStartId);

                entity.HasIndex(e => e.EtimSteeringId);

                entity.HasIndex(e => e.FluidUid);

                entity.HasIndex(e => e.HseId);

                entity.HasIndex(e => e.MaaspId);

                entity.HasIndex(e => e.MdCsgLastId);

                entity.HasIndex(e => e.MdPlannedId);

                entity.HasIndex(e => e.MdReportId);

                entity.HasIndex(e => e.MudInventoryId);

                entity.HasIndex(e => e.MudVolumeId);

                entity.HasIndex(e => e.PresKickTolId);

                entity.HasIndex(e => e.PresLotEmwId);

                entity.HasIndex(e => e.PumpOpId);

                entity.HasIndex(e => e.RigOpsReportRigId);

                entity.HasIndex(e => e.RigResponseId);

                entity.HasIndex(e => e.RopAvId);

                entity.HasIndex(e => e.RopCurrentId);

                entity.HasIndex(e => e.ShakerOpId);

                entity.HasIndex(e => e.SupportCraftId);

                entity.HasIndex(e => e.TrajectoryStationUid);

                entity.HasIndex(e => e.TubularId);

                entity.HasIndex(e => e.TvdCsgLastId);

                entity.HasIndex(e => e.TvdLotId);

                entity.HasIndex(e => e.TvdReportId);

                entity.HasIndex(e => e.VolKickTolId);

                entity.HasIndex(e => e.WeatherId);
            });

            modelBuilder.Entity<RigBopComponents>(entity =>
            {
                entity.HasIndex(e => e.DiaCloseMnId);

                entity.HasIndex(e => e.DiaCloseMxId);

                entity.HasIndex(e => e.IdPassThruId);

                entity.HasIndex(e => e.PresWorkId);

                entity.HasIndex(e => e.RigBopBopId);
            });

            modelBuilder.Entity<RigBops>(entity =>
            {
                entity.HasIndex(e => e.CapAccFluidId);

                entity.HasIndex(e => e.DiaDiverterId);

                entity.HasIndex(e => e.IdBoosterLineId);

                entity.HasIndex(e => e.IdChkLineId);

                entity.HasIndex(e => e.IdKillLineId);

                entity.HasIndex(e => e.IdSurfLineId);

                entity.HasIndex(e => e.LenBoosterLineId);

                entity.HasIndex(e => e.LenChkLineId);

                entity.HasIndex(e => e.LenKillLineId);

                entity.HasIndex(e => e.LenSurfLineId);

                entity.HasIndex(e => e.OdBoosterLineId);

                entity.HasIndex(e => e.OdChkLineId);

                entity.HasIndex(e => e.OdKillLineId);

                entity.HasIndex(e => e.OdSurfLineId);

                entity.HasIndex(e => e.PresAccOpRatingId);

                entity.HasIndex(e => e.PresAccPreChargeId);

                entity.HasIndex(e => e.PresBopRatingId);

                entity.HasIndex(e => e.PresChokeManifoldId);

                entity.HasIndex(e => e.PresWorkDiverterId);

                entity.HasIndex(e => e.SizeBopSysId);

                entity.HasIndex(e => e.SizeConnectionBopId);

                entity.HasIndex(e => e.VolAccPreChargeId);
            });

            modelBuilder.Entity<RigCentrifuges>(entity =>
            {
                entity.HasIndex(e => e.CapFlowId);
            });

            modelBuilder.Entity<RigDegassers>(entity =>
            {
                entity.HasIndex(e => e.AreaSeparatorFlowId);

                entity.HasIndex(e => e.CapBlowdownId);

                entity.HasIndex(e => e.CapFlowId);

                entity.HasIndex(e => e.CapGasSepId);

                entity.HasIndex(e => e.HeightId);

                entity.HasIndex(e => e.HtMudSealId);

                entity.HasIndex(e => e.IdInletId);

                entity.HasIndex(e => e.IdUniqueId);

                entity.HasIndex(e => e.IdVentLineId);

                entity.HasIndex(e => e.LenId);

                entity.HasIndex(e => e.LenVentLineId);

                entity.HasIndex(e => e.PresRatingId);

                entity.HasIndex(e => e.TempRatingId);
            });

            modelBuilder.Entity<RigPits>(entity =>
            {
                entity.HasIndex(e => e.CapMxId);
            });

            modelBuilder.Entity<RigPumps>(entity =>
            {
                entity.HasIndex(e => e.DisplacementId);

                entity.HasIndex(e => e.EffId);

                entity.HasIndex(e => e.IdLinerId);

                entity.HasIndex(e => e.LenStrokeId);

                entity.HasIndex(e => e.OdRodId);

                entity.HasIndex(e => e.PowHydMxId);

                entity.HasIndex(e => e.PowMechMxId);

                entity.HasIndex(e => e.PresDampId);

                entity.HasIndex(e => e.PresMxId);

                entity.HasIndex(e => e.SpmMxId);

                entity.HasIndex(e => e.VolDampId);
            });

            modelBuilder.Entity<RigShakers>(entity =>
            {
                entity.HasIndex(e => e.CapFlowId);

                entity.HasIndex(e => e.SizeMeshMnId);
            });

            modelBuilder.Entity<RigSurfaceEquipments>(entity =>
            {
                entity.HasIndex(e => e.HtFlangeId);

                entity.HasIndex(e => e.HtInjStkId);

                entity.HasIndex(e => e.HtTopStkId);

                entity.HasIndex(e => e.IdDischargeLineId);

                entity.HasIndex(e => e.IdHoseId);

                entity.HasIndex(e => e.IdKellyId);

                entity.HasIndex(e => e.IdStandpipeId);

                entity.HasIndex(e => e.IdSwivelId);

                entity.HasIndex(e => e.IdTopStkId);

                entity.HasIndex(e => e.LenDischargeLineId);

                entity.HasIndex(e => e.LenHoseId);

                entity.HasIndex(e => e.LenKellyId);

                entity.HasIndex(e => e.LenReelId);

                entity.HasIndex(e => e.LenStandpipeId);

                entity.HasIndex(e => e.LenSwivelId);

                entity.HasIndex(e => e.LenUmbilicalId);

                entity.HasIndex(e => e.OdCoreId);

                entity.HasIndex(e => e.OdReelId);

                entity.HasIndex(e => e.OdUmbilicalId);

                entity.HasIndex(e => e.PresRatingId);

                entity.HasIndex(e => e.WidReelWrapId);
            });

            modelBuilder.Entity<Rigs>(entity =>
            {
                entity.HasIndex(e => e.AirGapId);

                entity.HasIndex(e => e.BopId);

                entity.HasIndex(e => e.CapBulkCementId);

                entity.HasIndex(e => e.CapBulkMudId);

                entity.HasIndex(e => e.CapDrillWaterId);

                entity.HasIndex(e => e.CapFuelId);

                entity.HasIndex(e => e.CapLiquidMudId);

                entity.HasIndex(e => e.CapPotableWaterId);

                entity.HasIndex(e => e.CapWindDerrickId);

                entity.HasIndex(e => e.CentrifugeId);

                entity.HasIndex(e => e.CommonDataRigCommonDataId);

                entity.HasIndex(e => e.DegasserId);

                entity.HasIndex(e => e.HeaveMxId);

                entity.HasIndex(e => e.HtDerrickId);

                entity.HasIndex(e => e.HydrocycloneId);

                entity.HasIndex(e => e.MotionCompensationMnId);

                entity.HasIndex(e => e.MotionCompensationMxId);

                entity.HasIndex(e => e.PitUid);

                entity.HasIndex(e => e.PowerDrawWorksId);

                entity.HasIndex(e => e.PumpUid);

                entity.HasIndex(e => e.RatingBlockId);

                entity.HasIndex(e => e.RatingDerrickId);

                entity.HasIndex(e => e.RatingDrawWorksId);

                entity.HasIndex(e => e.RatingDrillDepthId);

                entity.HasIndex(e => e.RatingHkldId);

                entity.HasIndex(e => e.RatingHookId);

                entity.HasIndex(e => e.RatingRotSystemId);

                entity.HasIndex(e => e.RatingSwivelId);

                entity.HasIndex(e => e.RatingTqRotSysId);

                entity.HasIndex(e => e.RatingWaterDepthId);

                entity.HasIndex(e => e.RiserAngleLimitId);

                entity.HasIndex(e => e.RotSizeOpeningId);

                entity.HasIndex(e => e.ShakerUid);

                entity.HasIndex(e => e.SizeDrillLineId);

                entity.HasIndex(e => e.StrokeMotionCompensationId);

                entity.HasIndex(e => e.SurfaceEquipmentId);

                entity.HasIndex(e => e.VarDeckLdMxId);

                entity.HasIndex(e => e.VdlStormId);

                entity.HasIndex(e => e.WtBlockId);
            });

            modelBuilder.Entity<Risks>(entity =>
            {
                entity.HasIndex(e => e.DiaHoleUom);

                entity.HasIndex(e => e.MdHoleEndId);

                entity.HasIndex(e => e.MdHoleStartId);

                entity.HasIndex(e => e.ObjectReferenceId);

                entity.HasIndex(e => e.TvdHoleEndId);

                entity.HasIndex(e => e.TvdHoleStartId);
            });

            modelBuilder.Entity<SideWallCoreLithology>(entity =>
            {
                entity.HasIndex(e => e.DensShaleUom);

                entity.HasIndex(e => e.LithPcUom);

                entity.HasIndex(e => e.QualifierUid);
            });

            modelBuilder.Entity<SideWallCoreQualifier>(entity =>
            {
                entity.HasIndex(e => e.AbundanceUom);
            });

            modelBuilder.Entity<SideWallCoreShow>(entity =>
            {
                entity.HasIndex(e => e.NatFlorPcUom);

                entity.HasIndex(e => e.StainPcUom);
            });

            modelBuilder.Entity<SideWallCoreSwcSample>(entity =>
            {
                entity.HasIndex(e => e.LithologyUid);

                entity.HasIndex(e => e.MdUom);

                entity.HasIndex(e => e.ShowSideWallCoreId);
            });

            modelBuilder.Entity<SidewallCores>(entity =>
            {
                entity.HasIndex(e => e.CommonDataSidewallCoresCommonDataid);

                entity.HasIndex(e => e.DiaHoleUom);

                entity.HasIndex(e => e.DiaPlugUom);

                entity.HasIndex(e => e.MdCoreUom);

                entity.HasIndex(e => e.MdToolReferenceUom);

                entity.HasIndex(e => e.SwcSampleUid);
            });

            modelBuilder.Entity<StimJobAdditives>(entity =>
            {
                entity.HasIndex(e => e.MassId);

                entity.HasIndex(e => e.StageFluidId);

                entity.HasIndex(e => e.VolumeId);
            });

            modelBuilder.Entity<StimJobFlowPaths>(entity =>
            {
                entity.HasIndex(e => e.AcidVolId);

                entity.HasIndex(e => e.AvgAcidRateId);

                entity.HasIndex(e => e.AvgBaseFluidQualityId);

                entity.HasIndex(e => e.AvgBaseFluidRateId);

                entity.HasIndex(e => e.AvgCo2baseFluidQualityId);

                entity.HasIndex(e => e.AvgCo2liquidRateId);

                entity.HasIndex(e => e.AvgGelRateId);

                entity.HasIndex(e => e.AvgHydraulicPowerId);

                entity.HasIndex(e => e.AvgN2baseFluidQualityId);

                entity.HasIndex(e => e.AvgN2stdRateId);

                entity.HasIndex(e => e.AvgOilRateId);

                entity.HasIndex(e => e.AvgPmaxPacPresId);

                entity.HasIndex(e => e.AvgPmaxWeaklinkPresId);

                entity.HasIndex(e => e.AvgPropConcId);

                entity.HasIndex(e => e.AvgSlurryPropConcId);

                entity.HasIndex(e => e.AvgSlurryRateId);

                entity.HasIndex(e => e.AvgTemperatureId);

                entity.HasIndex(e => e.AvgTreatPresId);

                entity.HasIndex(e => e.AvgWellheadRateId);

                entity.HasIndex(e => e.BaseFluidBypassVolId);

                entity.HasIndex(e => e.BaseFluidVolId);

                entity.HasIndex(e => e.BreakDownPresId);

                entity.HasIndex(e => e.FractureGradientId);

                entity.HasIndex(e => e.GelVolId);

                entity.HasIndex(e => e.MassCo2id);

                entity.HasIndex(e => e.MaxAcidRateId);

                entity.HasIndex(e => e.MaxCo2liquidRateId);

                entity.HasIndex(e => e.MaxGelRateId);

                entity.HasIndex(e => e.MaxN2stdRateId);

                entity.HasIndex(e => e.MaxOilRateId);

                entity.HasIndex(e => e.MaxPmaxPacPresId);

                entity.HasIndex(e => e.MaxPmaxWeaklinkPresId);

                entity.HasIndex(e => e.MaxPropConcId);

                entity.HasIndex(e => e.MaxSlurryPropConcId);

                entity.HasIndex(e => e.MaxSlurryRateId);

                entity.HasIndex(e => e.MaxTreatmentPresId);

                entity.HasIndex(e => e.MaxWellheadRateId);

                entity.HasIndex(e => e.OilVolId);

                entity.HasIndex(e => e.PercentPadId);

                entity.HasIndex(e => e.PropMassId);

                entity.HasIndex(e => e.ShutinPres10MinId);

                entity.HasIndex(e => e.ShutinPres15MinId);

                entity.HasIndex(e => e.ShutinPres5MinId);

                entity.HasIndex(e => e.SlurryVolId);

                entity.HasIndex(e => e.StdVolN2id);

                entity.HasIndex(e => e.WellheadVolStdVolN2id);
            });

            modelBuilder.Entity<StimJobFluidEfficiencyTests>(entity =>
            {
                entity.HasIndex(e => e.EndPdlDurationId);

                entity.HasIndex(e => e.FluidEfficiencyId);

                entity.HasIndex(e => e.FractureCloseDurationId);

                entity.HasIndex(e => e.FractureClosePresId);

                entity.HasIndex(e => e.FractureExtensionPresId);

                entity.HasIndex(e => e.FractureLengthId);

                entity.HasIndex(e => e.FractureWidthId);

                entity.HasIndex(e => e.NetPresId);

                entity.HasIndex(e => e.PorePresId);

                entity.HasIndex(e => e.PseudoRadialPresId);

                entity.HasIndex(e => e.ResidualPermeabilityId);
            });

            modelBuilder.Entity<StimJobJobEvents>(entity =>
            {
                entity.HasIndex(e => e.FlowPathId);
            });

            modelBuilder.Entity<StimJobJobIntervals>(entity =>
            {
                entity.HasIndex(e => e.AveragePresId);

                entity.HasIndex(e => e.AvgBaseFluidReturnRateId);

                entity.HasIndex(e => e.AvgBottomholeRateId);

                entity.HasIndex(e => e.AvgConductivityId);

                entity.HasIndex(e => e.AvgFractureWidthId);

                entity.HasIndex(e => e.AvgPresCasingId);

                entity.HasIndex(e => e.AvgPresTubingId);

                entity.HasIndex(e => e.AvgProppantConcBottomholeId);

                entity.HasIndex(e => e.AvgProppantConcSurfaceId);

                entity.HasIndex(e => e.AvgSlurryReturnRateId);

                entity.HasIndex(e => e.BreakDownPresId);

                entity.HasIndex(e => e.ClosureDurationId);

                entity.HasIndex(e => e.ClosurePresId);

                entity.HasIndex(e => e.FinalFractureGradientId);

                entity.HasIndex(e => e.FormationProppantMassId);

                entity.HasIndex(e => e.FractureGradientId);

                entity.HasIndex(e => e.HhpOrderedCo2id);

                entity.HasIndex(e => e.HhpOrderedFluidId);

                entity.HasIndex(e => e.HhpUsedCo2id);

                entity.HasIndex(e => e.HhpUsedFluidId);

                entity.HasIndex(e => e.InitialShutinPresId);

                entity.HasIndex(e => e.MaxFluidRateAnnulusId);

                entity.HasIndex(e => e.MaxFluidRateTubingId);

                entity.HasIndex(e => e.MaxPresAnnulusId);

                entity.HasIndex(e => e.MaxPresTubingId);

                entity.HasIndex(e => e.MaxProppantConcBottomholeId);

                entity.HasIndex(e => e.MaxProppantConcSurfaceId);

                entity.HasIndex(e => e.MdFormationBottomId);

                entity.HasIndex(e => e.MdFormationTopId);

                entity.HasIndex(e => e.MdOpenHoleBottomId);

                entity.HasIndex(e => e.MdOpenHoleTopId);

                entity.HasIndex(e => e.NetPresId);

                entity.HasIndex(e => e.OpenHoleDiameterId);

                entity.HasIndex(e => e.PdatSessionId);

                entity.HasIndex(e => e.PercentProppantPumpedId);

                entity.HasIndex(e => e.PerfBallSizeId);

                entity.HasIndex(e => e.PerforationIntervalId);

                entity.HasIndex(e => e.PerfproppantConcId);

                entity.HasIndex(e => e.ReservoirIntervalId);

                entity.HasIndex(e => e.ScreenOutPresId);

                entity.HasIndex(e => e.TotalCo2massId);

                entity.HasIndex(e => e.TotalFrictionPresLossId);

                entity.HasIndex(e => e.TotalN2stdVolumeId);

                entity.HasIndex(e => e.TotalProppantMassId);

                entity.HasIndex(e => e.TotalPumpTimeId);

                entity.HasIndex(e => e.TotalVolumeId);

                entity.HasIndex(e => e.TvdFormationBottomId);

                entity.HasIndex(e => e.TvdFormationTopId);

                entity.HasIndex(e => e.TvdOpenHoleBottomId);

                entity.HasIndex(e => e.TvdOpenHoleTopId);

                entity.HasIndex(e => e.WellboreProppantMassId);
            });

            modelBuilder.Entity<StimJobJobStages>(entity =>
            {
                entity.HasIndex(e => e.AcidVolumeId);

                entity.HasIndex(e => e.AveragePresBottomholeId);

                entity.HasIndex(e => e.AveragePresSurfaceId);

                entity.HasIndex(e => e.AvgAcidRateId);

                entity.HasIndex(e => e.AvgBaseFluidQualityId);

                entity.HasIndex(e => e.AvgBaseFluidRateId);

                entity.HasIndex(e => e.AvgCo2baseFluidQualityId);

                entity.HasIndex(e => e.AvgCo2rateId);

                entity.HasIndex(e => e.AvgGelRateId);

                entity.HasIndex(e => e.AvgHydraulicPowerId);

                entity.HasIndex(e => e.AvgInternalPhaseFractionId);

                entity.HasIndex(e => e.AvgN2baseFluidQualityId);

                entity.HasIndex(e => e.AvgN2stdRateId);

                entity.HasIndex(e => e.AvgOilRateId);

                entity.HasIndex(e => e.AvgPropConcId);

                entity.HasIndex(e => e.AvgProppantConcBottomholeId);

                entity.HasIndex(e => e.AvgProppantConcSurfaceId);

                entity.HasIndex(e => e.AvgPumpRateBottomholeId);

                entity.HasIndex(e => e.AvgRateSurfaceCo2id);

                entity.HasIndex(e => e.AvgRateSurfaceLiquidId);

                entity.HasIndex(e => e.AvgSlurryPropConcId);

                entity.HasIndex(e => e.AvgSlurryRateId);

                entity.HasIndex(e => e.AvgStdRateSurfaceN2id);

                entity.HasIndex(e => e.AvgTemperatureId);

                entity.HasIndex(e => e.AvgWellheadRateId);

                entity.HasIndex(e => e.BaseFluidBypassVolId);

                entity.HasIndex(e => e.BaseFluidVolId);

                entity.HasIndex(e => e.EndFoamRateCo2id);

                entity.HasIndex(e => e.EndFoamRateN2id);

                entity.HasIndex(e => e.EndPresBottomholeId);

                entity.HasIndex(e => e.EndPresSurfaceId);

                entity.HasIndex(e => e.EndProppantConcBottomholeId);

                entity.HasIndex(e => e.EndProppantConcSurfaceId);

                entity.HasIndex(e => e.EndPumpRateBottomholeId);

                entity.HasIndex(e => e.EndRateSurfaceCo2id);

                entity.HasIndex(e => e.EndRateSurfaceLiquidId);

                entity.HasIndex(e => e.EndStdRateSurfaceN2id);

                entity.HasIndex(e => e.FlowPathId);

                entity.HasIndex(e => e.FluidVolBaseId);

                entity.HasIndex(e => e.FluidVolSlurryId);

                entity.HasIndex(e => e.GelVolumeId);

                entity.HasIndex(e => e.MaxAcidRateId);

                entity.HasIndex(e => e.MaxCo2liquidRateId);

                entity.HasIndex(e => e.MaxGelRateId);

                entity.HasIndex(e => e.MaxN2stdRateId);

                entity.HasIndex(e => e.MaxOilRateId);

                entity.HasIndex(e => e.MaxPmaxPacPresId);

                entity.HasIndex(e => e.MaxPmaxWeaklinkPresId);

                entity.HasIndex(e => e.MaxPresId);

                entity.HasIndex(e => e.MaxPropConcId);

                entity.HasIndex(e => e.MaxSlurryPropConcId);

                entity.HasIndex(e => e.MaxSlurryRateId);

                entity.HasIndex(e => e.MaxWellheadRateId);

                entity.HasIndex(e => e.OilVolumeId);

                entity.HasIndex(e => e.ProppantMassId);

                entity.HasIndex(e => e.ProppantMassWellHeadId);

                entity.HasIndex(e => e.PumpTimeId);

                entity.HasIndex(e => e.SlurryRateBeginId);

                entity.HasIndex(e => e.SlurryRateEndId);

                entity.HasIndex(e => e.SlurryVolId);

                entity.HasIndex(e => e.StageFluidId);

                entity.HasIndex(e => e.StartFoamRateCo2id);

                entity.HasIndex(e => e.StartFoamRateN2id);

                entity.HasIndex(e => e.StartPresBottomholeId);

                entity.HasIndex(e => e.StartPresSurfaceId);

                entity.HasIndex(e => e.StartProppantConcBottomholeId);

                entity.HasIndex(e => e.StartProppantConcSurfaceId);

                entity.HasIndex(e => e.StartPumpRateBottomholeId);

                entity.HasIndex(e => e.StartRateSurfaceCo2id);

                entity.HasIndex(e => e.StartRateSurfaceLiquidId);

                entity.HasIndex(e => e.StartStdRateSurfaceN2id);

                entity.HasIndex(e => e.WellheadVolStdVolN2id);
            });

            modelBuilder.Entity<StimJobPdatSessions>(entity =>
            {
                entity.HasIndex(e => e.AvgBottomholeTreatmentPresId);

                entity.HasIndex(e => e.AvgBottomholeTreatmentRateId);

                entity.HasIndex(e => e.BaseFluidVolId);

                entity.HasIndex(e => e.BottomholeHydrostaticPresId);

                entity.HasIndex(e => e.BottomholeTemperatureId);

                entity.HasIndex(e => e.BubblePointPresId);

                entity.HasIndex(e => e.FluidCompressibilityId);

                entity.HasIndex(e => e.FluidDensityId);

                entity.HasIndex(e => e.FluidEfficiencyId);

                entity.HasIndex(e => e.FluidEfficiencyTestId);

                entity.HasIndex(e => e.FluidSpecificHeatId);

                entity.HasIndex(e => e.FluidThermalConductivityId);

                entity.HasIndex(e => e.FluidThermalExpansionCoefficientId);

                entity.HasIndex(e => e.FoamQualityId);

                entity.HasIndex(e => e.FractureClosePresId);

                entity.HasIndex(e => e.FrictionPresId);

                entity.HasIndex(e => e.InitialShutinPresId);

                entity.HasIndex(e => e.MdBottomholeId);

                entity.HasIndex(e => e.MdMidPerforationId);

                entity.HasIndex(e => e.MdSurfaceId);

                entity.HasIndex(e => e.PercentPadId);

                entity.HasIndex(e => e.PorePresId);

                entity.HasIndex(e => e.PumpDurationId);

                entity.HasIndex(e => e.PumpFlowBackTestId);

                entity.HasIndex(e => e.ReservoirTotalCompressibilityId);

                entity.HasIndex(e => e.StepDownTestId);

                entity.HasIndex(e => e.StepRateTestId);

                entity.HasIndex(e => e.SurfaceFluidTemperatureId);

                entity.HasIndex(e => e.SurfaceTemperatureId);

                entity.HasIndex(e => e.TvdMidPerforationId);

                entity.HasIndex(e => e.WellboreVolumeId);
            });

            modelBuilder.Entity<StimJobPerforationIntervals>(entity =>
            {
                entity.HasIndex(e => e.DensityPerforationId);

                entity.HasIndex(e => e.FrictionPresId);

                entity.HasIndex(e => e.MdPerforationsBottomId);

                entity.HasIndex(e => e.MdPerforationsTopId);

                entity.HasIndex(e => e.PhasingPerforationId);

                entity.HasIndex(e => e.SizeId);

                entity.HasIndex(e => e.TvdPerforationsBottomId);

                entity.HasIndex(e => e.TvdPerforationsTopId);
            });

            modelBuilder.Entity<StimJobPresMeasurements>(entity =>
            {
                entity.HasIndex(e => e.BottomholeRateId);

                entity.HasIndex(e => e.PresId);

                entity.HasIndex(e => e.StepRateTestId);
            });

            modelBuilder.Entity<StimJobProppants>(entity =>
            {
                entity.HasIndex(e => e.WeightId);
            });

            modelBuilder.Entity<StimJobPumpFlowBackTests>(entity =>
            {
                entity.HasIndex(e => e.FractureCloseDurationId);

                entity.HasIndex(e => e.FractureClosePresId);
            });

            modelBuilder.Entity<StimJobReservoirIntervals>(entity =>
            {
                entity.HasIndex(e => e.FormationPermeabilityId);

                entity.HasIndex(e => e.FormationPorosityId);

                entity.HasIndex(e => e.GrossPayThicknessId);

                entity.HasIndex(e => e.LithFormationPermeabilityId);

                entity.HasIndex(e => e.LithNetPayThicknessId);

                entity.HasIndex(e => e.LithPoissonsRatioId);

                entity.HasIndex(e => e.LithPorePresId);

                entity.HasIndex(e => e.LithYoungsModulusId);

                entity.HasIndex(e => e.MdGrossPayBottomId);

                entity.HasIndex(e => e.MdGrossPayTopId);

                entity.HasIndex(e => e.MdLithBottomId);

                entity.HasIndex(e => e.MdLithTopId);

                entity.HasIndex(e => e.NetPayFluidCompressibilityId);

                entity.HasIndex(e => e.NetPayFluidViscosityId);

                entity.HasIndex(e => e.NetPayFormationPermeabilityId);

                entity.HasIndex(e => e.NetPayFormationPorosityId);

                entity.HasIndex(e => e.NetPayPorePresId);

                entity.HasIndex(e => e.NetPayThicknessId);
            });

            modelBuilder.Entity<StimJobShutinPress>(entity =>
            {
                entity.HasIndex(e => e.PresId);

                entity.HasIndex(e => e.TimeAfterShutinId);
            });

            modelBuilder.Entity<StimJobStageFluids>(entity =>
            {
                entity.HasIndex(e => e.FluidVolId);

                entity.HasIndex(e => e.ProppantId);
            });

            modelBuilder.Entity<StimJobStepDownTests>(entity =>
            {
                entity.HasIndex(e => e.BottomholeFluidDensityId);

                entity.HasIndex(e => e.DiameterEntryHolePipeFrictionId);

                entity.HasIndex(e => e.InitialShutinPresId);
            });

            modelBuilder.Entity<StimJobStepRateTests>(entity =>
            {
                entity.HasIndex(e => e.FractureExtensionPresId);
            });

            modelBuilder.Entity<StimJobSteps>(entity =>
            {
                entity.HasIndex(e => e.BottomholeRateId);

                entity.HasIndex(e => e.EntryFrictionId);

                entity.HasIndex(e => e.NearWellboreFrictionId);

                entity.HasIndex(e => e.PerfFrictionId);

                entity.HasIndex(e => e.PipeFrictionId);

                entity.HasIndex(e => e.PresId);

                entity.HasIndex(e => e.StepDownTestId);
            });

            modelBuilder.Entity<StimJobTotalProppantUsages>(entity =>
            {
                entity.HasIndex(e => e.JobIntervalId);

                entity.HasIndex(e => e.MassId);
            });

            modelBuilder.Entity<StimJobTubulars>(entity =>
            {
                entity.HasIndex(e => e.FlowPathId);

                entity.HasIndex(e => e.IdId);

                entity.HasIndex(e => e.MdBottomId);

                entity.HasIndex(e => e.MdTopId);

                entity.HasIndex(e => e.OdId);

                entity.HasIndex(e => e.VolumeFactorId);

                entity.HasIndex(e => e.WeightId);
            });

            modelBuilder.Entity<StimJobs>(entity =>
            {
                entity.HasIndex(e => e.AvgJobPresId);

                entity.HasIndex(e => e.BottomholeStaticTemperatureId);

                entity.HasIndex(e => e.FlowBackPresId);

                entity.HasIndex(e => e.FlowBackRateId);

                entity.HasIndex(e => e.FlowBackVolumeId);

                entity.HasIndex(e => e.FluidEfficiencyId);

                entity.HasIndex(e => e.HhpOrderedId);

                entity.HasIndex(e => e.HhpUsedId);

                entity.HasIndex(e => e.JobIntervalId);

                entity.HasIndex(e => e.MaxFluidRateId);

                entity.HasIndex(e => e.MaxJobPresId);

                entity.HasIndex(e => e.TotalCo2massId);

                entity.HasIndex(e => e.TotalJobVolumeId);

                entity.HasIndex(e => e.TotalN2stdVolumeId);

                entity.HasIndex(e => e.TotalProppantWtId);

                entity.HasIndex(e => e.TotalPumpTimeId);

                entity.HasIndex(e => e.TreatingBottomholeTemperatureId);
            });

            modelBuilder.Entity<SurveyProgramSurveySection>(entity =>
            {
                entity.HasIndex(e => e.FrequencyMxId);

                entity.HasIndex(e => e.MdEndId);

                entity.HasIndex(e => e.MdStartId);

                entity.HasIndex(e => e.SurveyProgramId);
            });

            modelBuilder.Entity<SurveyPrograms>(entity =>
            {
                entity.HasIndex(e => e.CommonDataSurveyProgramCommonDataId);
            });

            modelBuilder.Entity<TargetLocations>(entity =>
            {
                entity.HasIndex(e => e.LatitudeId);

                entity.HasIndex(e => e.LongitudeId);

                entity.HasIndex(e => e.ProjectedXid);

                entity.HasIndex(e => e.ProjectedYid);

                entity.HasIndex(e => e.TargetId);

                entity.HasIndex(e => e.TargetSectionId);

                entity.HasIndex(e => e.WellCrsid);
            });

            modelBuilder.Entity<TargetSections>(entity =>
            {
                entity.HasIndex(e => e.AngleArcId);

                entity.HasIndex(e => e.LenRadiusId);

                entity.HasIndex(e => e.TargetId);

                entity.HasIndex(e => e.ThickAboveId);

                entity.HasIndex(e => e.ThickBelowId);
            });

            modelBuilder.Entity<Targets>(entity =>
            {
                entity.HasIndex(e => e.CommonDataTargetCommonDataId);

                entity.HasIndex(e => e.DipId);

                entity.HasIndex(e => e.DispEwCenterId);

                entity.HasIndex(e => e.DispEwOffsetId);

                entity.HasIndex(e => e.DispEwSectOrigId);

                entity.HasIndex(e => e.DispNsCenterId);

                entity.HasIndex(e => e.DispNsOffsetId);

                entity.HasIndex(e => e.DispNsSectOrigId);

                entity.HasIndex(e => e.LenMajorAxisId);

                entity.HasIndex(e => e.RotationId);

                entity.HasIndex(e => e.StrikeId);

                entity.HasIndex(e => e.ThickAboveId);

                entity.HasIndex(e => e.ThickBelowId);

                entity.HasIndex(e => e.TvdId);

                entity.HasIndex(e => e.WidMinorAxisId);
            });

            modelBuilder.Entity<ToolErrorModelErrorTermValues>(entity =>
            {
                entity.HasIndex(e => e.TermId);

                entity.HasIndex(e => e.ToolErrorModelId);

                entity.HasIndex(e => e.ValueId);
            });

            modelBuilder.Entity<ToolErrorModelModelParameter>(entity =>
            {
                entity.HasIndex(e => e.GyroInitializationId);

                entity.HasIndex(e => e.GyroReinitializationDistanceId);
            });

            modelBuilder.Entity<ToolErrorModelOperatingConditions>(entity =>
            {
                entity.HasIndex(e => e.MaxId);

                entity.HasIndex(e => e.MinId);
            });

            modelBuilder.Entity<ToolErrorModelOperatingIntervals>(entity =>
            {
                entity.HasIndex(e => e.EndId);

                entity.HasIndex(e => e.SpeedId);

                entity.HasIndex(e => e.StartId);

                entity.HasIndex(e => e.ToolErrorModelId);
            });

            modelBuilder.Entity<ToolErrorModels>(entity =>
            {
                entity.HasIndex(e => e.AuthorizationId);

                entity.HasIndex(e => e.CommonDataToolErrorModelCommonDataId);

                entity.HasIndex(e => e.ModelParametersId);

                entity.HasIndex(e => e.OperatingConditionId);

                entity.HasIndex(e => e.UseErrorTermSetId);
            });

            modelBuilder.Entity<ToolErrorTermSetErrorCoefficients>(entity =>
            {
                entity.HasIndex(e => e.ErrorTermId);
            });

            modelBuilder.Entity<ToolErrorTermSetErrorTerms>(entity =>
            {
                entity.HasIndex(e => e.ToolErrorTermSetId);
            });

            modelBuilder.Entity<ToolErrorTermSetFunctions>(entity =>
            {
                entity.HasIndex(e => e.NomenclatureId);
            });

            modelBuilder.Entity<ToolErrorTermSetNomenclatures>(entity =>
            {
                entity.HasIndex(e => e.ConstantId);
            });

            modelBuilder.Entity<ToolErrorTermSetParameters>(entity =>
            {
                entity.HasIndex(e => e.NomenclatureId);
            });

            modelBuilder.Entity<ToolErrorTermSets>(entity =>
            {
                entity.HasIndex(e => e.AuthorizationId);

                entity.HasIndex(e => e.NomenclatureId);
            });

            modelBuilder.Entity<TrajectoryCorUseds>(entity =>
            {
                entity.HasIndex(e => e.DirSensorOffsetId);

                entity.HasIndex(e => e.GravAxialAccelCorId);

                entity.HasIndex(e => e.GravTran1AccelCorId);

                entity.HasIndex(e => e.GravTran2AccelCorId);

                entity.HasIndex(e => e.MagAxialDrlstrCorId);

                entity.HasIndex(e => e.MagTran1DrlstrCorId);

                entity.HasIndex(e => e.MagTran2DrlstrCorId);

                entity.HasIndex(e => e.SagAziCorId);

                entity.HasIndex(e => e.SagIncCorId);

                entity.HasIndex(e => e.StnGridCorUsedId);

                entity.HasIndex(e => e.StnMagDeclUsedId);
            });

            modelBuilder.Entity<TrajectoryLocations>(entity =>
            {
                entity.HasIndex(e => e.EastingId);

                entity.HasIndex(e => e.LatitudeId);

                entity.HasIndex(e => e.LongitudeId);

                entity.HasIndex(e => e.NorthingId);

                entity.HasIndex(e => e.TrajectoryStationId);

                entity.HasIndex(e => e.WellCrsuidRef);
            });

            modelBuilder.Entity<TrajectoryMatrixCovs>(entity =>
            {
                entity.HasIndex(e => e.BiasEid);

                entity.HasIndex(e => e.BiasNdtId);

                entity.HasIndex(e => e.BiasVertId);

                entity.HasIndex(e => e.VarianceEeid);

                entity.HasIndex(e => e.VarianceEvertId);

                entity.HasIndex(e => e.VarianceNeid);

                entity.HasIndex(e => e.VarianceNnid);

                entity.HasIndex(e => e.VarianceNvertId);

                entity.HasIndex(e => e.VarianceVertVertId);
            });

            modelBuilder.Entity<TrajectoryRawDatas>(entity =>
            {
                entity.HasIndex(e => e.GravAxialRawId);

                entity.HasIndex(e => e.GravTran1RawId);

                entity.HasIndex(e => e.GravTran2RawId);

                entity.HasIndex(e => e.MagAxialRawId);

                entity.HasIndex(e => e.MagTran1RawId);

                entity.HasIndex(e => e.MagTran2RawId);
            });

            modelBuilder.Entity<TrajectoryStations>(entity =>
            {
                entity.HasIndex(e => e.AziId);

                entity.HasIndex(e => e.CorUsedTrajectoryCorUsedId);

                entity.HasIndex(e => e.DipAngleUncertId);

                entity.HasIndex(e => e.DispEwId);

                entity.HasIndex(e => e.DispNsId);

                entity.HasIndex(e => e.DlsId);

                entity.HasIndex(e => e.GravTotalUncertId);

                entity.HasIndex(e => e.GtfId);

                entity.HasIndex(e => e.InclId);

                entity.HasIndex(e => e.MagTotalUncertId);

                entity.HasIndex(e => e.MatrixCovId);

                entity.HasIndex(e => e.MdDeltaId);

                entity.HasIndex(e => e.MdId);

                entity.HasIndex(e => e.MtfId);

                entity.HasIndex(e => e.RateBuildId);

                entity.HasIndex(e => e.RateTurnId);

                entity.HasIndex(e => e.RawDataId);

                entity.HasIndex(e => e.TvdDeltaId);

                entity.HasIndex(e => e.TvdId);

                entity.HasIndex(e => e.ValidId);

                entity.HasIndex(e => e.VertSectId);
            });

            modelBuilder.Entity<TrajectoryValids>(entity =>
            {
                entity.HasIndex(e => e.GravTotalFieldCalcId);

                entity.HasIndex(e => e.MagDipAngleCalcId);

                entity.HasIndex(e => e.MagTotalFieldCalcId);
            });

            modelBuilder.Entity<Trajectorys>(entity =>
            {
                entity.HasIndex(e => e.AziVertSectId);

                entity.HasIndex(e => e.CommonDataTrajectoryCommonDataId);

                entity.HasIndex(e => e.DispEwVertSectOrigId);

                entity.HasIndex(e => e.DispNsVertSectOrigId);

                entity.HasIndex(e => e.GridCorUsedId);

                entity.HasIndex(e => e.MagDeclUsedId);

                entity.HasIndex(e => e.MdMnId);

                entity.HasIndex(e => e.MdMxId);

                entity.HasIndex(e => e.TrajectoryStationId);
            });

            modelBuilder.Entity<TubularBend>(entity =>
            {
                entity.HasIndex(e => e.AngleId);

                entity.HasIndex(e => e.DistBendBotId);
            });

            modelBuilder.Entity<TubularBitRecord>(entity =>
            {
                entity.HasIndex(e => e.CostId);

                entity.HasIndex(e => e.DiaBitId);

                entity.HasIndex(e => e.DiaPassThruId);

                entity.HasIndex(e => e.DiaPilotId);
            });

            modelBuilder.Entity<TubularComponent>(entity =>
            {
                entity.HasIndex(e => e.AreaNozzleFlowId);

                entity.HasIndex(e => e.AxialStiffnessId);

                entity.HasIndex(e => e.BendId);

                entity.HasIndex(e => e.BendStiffnessId);

                entity.HasIndex(e => e.BitRecordBitId);

                entity.HasIndex(e => e.ConnectionId);

                entity.HasIndex(e => e.DispId);

                entity.HasIndex(e => e.DoglegMxId);

                entity.HasIndex(e => e.HoleOpenerId);

                entity.HasIndex(e => e.IdFishneckId);

                entity.HasIndex(e => e.IdTubularIdId);

                entity.HasIndex(e => e.JarId);

                entity.HasIndex(e => e.LenFishneckId);

                entity.HasIndex(e => e.LenId);

                entity.HasIndex(e => e.LenJointAvId);

                entity.HasIndex(e => e.MotorId);

                entity.HasIndex(e => e.MwdToolId);

                entity.HasIndex(e => e.NameTagId);

                entity.HasIndex(e => e.OdDriftId);

                entity.HasIndex(e => e.OdFishneckId);

                entity.HasIndex(e => e.OdId);

                entity.HasIndex(e => e.PresBurstId);

                entity.HasIndex(e => e.PresCollapseId);

                entity.HasIndex(e => e.StabilizerId);

                entity.HasIndex(e => e.StressFatigId);

                entity.HasIndex(e => e.TensYieldId);

                entity.HasIndex(e => e.ThickWallId);

                entity.HasIndex(e => e.TorsionalStiffnessId);

                entity.HasIndex(e => e.TqYieldId);

                entity.HasIndex(e => e.TubularId);

                entity.HasIndex(e => e.WearWallId);

                entity.HasIndex(e => e.WtPerLenId);
            });

            modelBuilder.Entity<TubularConnection>(entity =>
            {
                entity.HasIndex(e => e.CriticalCrossSectionId);

                entity.HasIndex(e => e.IdTubularIdId);

                entity.HasIndex(e => e.LenId);

                entity.HasIndex(e => e.OdId);

                entity.HasIndex(e => e.PresLeakId);

                entity.HasIndex(e => e.SizeThreadId);

                entity.HasIndex(e => e.TensYieldId);

                entity.HasIndex(e => e.TqMakeupId);

                entity.HasIndex(e => e.TqYieldId);
            });

            modelBuilder.Entity<TubularHoleOpener>(entity =>
            {
                entity.HasIndex(e => e.DiaHoleOpenerId);
            });

            modelBuilder.Entity<TubularJar>(entity =>
            {
                entity.HasIndex(e => e.ForDownSetId);

                entity.HasIndex(e => e.ForDownTripId);

                entity.HasIndex(e => e.ForPmpOpenId);

                entity.HasIndex(e => e.ForSealFricId);

                entity.HasIndex(e => e.ForUpSetId);

                entity.HasIndex(e => e.ForUpTripId);
            });

            modelBuilder.Entity<TubularMotor>(entity =>
            {
                entity.HasIndex(e => e.BendSettingsMnId);

                entity.HasIndex(e => e.BendSettingsMxId);

                entity.HasIndex(e => e.ClearanceBearBoxId);

                entity.HasIndex(e => e.DiaNozzleId);

                entity.HasIndex(e => e.DiaRotorNozzleId);

                entity.HasIndex(e => e.FlowrateMnId);

                entity.HasIndex(e => e.FlowrateMxId);

                entity.HasIndex(e => e.OffsetToolId);

                entity.HasIndex(e => e.TempOpMxId);
            });

            modelBuilder.Entity<TubularMwdTool>(entity =>
            {
                entity.HasIndex(e => e.FlowrateMnId);

                entity.HasIndex(e => e.FlowrateMxId);

                entity.HasIndex(e => e.IdEquvId);

                entity.HasIndex(e => e.TempMxId);
            });

            modelBuilder.Entity<TubularNozzle>(entity =>
            {
                entity.HasIndex(e => e.DiaNozzleId);

                entity.HasIndex(e => e.LenId);

                entity.HasIndex(e => e.TubularComponentId);
            });

            modelBuilder.Entity<TubularSensor>(entity =>
            {
                entity.HasIndex(e => e.OffsetBotId);

                entity.HasIndex(e => e.TubularMwdToolMwdToolId);
            });

            modelBuilder.Entity<TubularStabilizer>(entity =>
            {
                entity.HasIndex(e => e.DistBladeBotId);

                entity.HasIndex(e => e.LenBladeId);

                entity.HasIndex(e => e.OdBladeMnId);

                entity.HasIndex(e => e.OdBladeMxId);
            });

            modelBuilder.Entity<Tubulars>(entity =>
            {
                entity.HasIndex(e => e.CommonDataTubularyCommonDataId);

                entity.HasIndex(e => e.DiaHoleAssyId);
            });

            modelBuilder.Entity<WbGeometrySection>(entity =>
            {
                entity.HasIndex(e => e.DiaDriftId);

                entity.HasIndex(e => e.IdSectionId);

                entity.HasIndex(e => e.MdBottomDiaDriftId);

                entity.HasIndex(e => e.MdTopId);

                entity.HasIndex(e => e.OdSectionId);

                entity.HasIndex(e => e.TvdBottomId);

                entity.HasIndex(e => e.TvdTopId);

                entity.HasIndex(e => e.WtPerLenId);
            });

            modelBuilder.Entity<WbGeometrys>(entity =>
            {
                entity.HasIndex(e => e.CommonDataWbGeometryCommonDataId);

                entity.HasIndex(e => e.DepthWaterMeanId);

                entity.HasIndex(e => e.GapAirId);

                entity.HasIndex(e => e.MdBottomDiaDriftId);

                entity.HasIndex(e => e.WbGeometrySectionId);
            });

            modelBuilder.Entity<WellBores>(entity =>
            {
                entity.HasIndex(e => e.CommonDataWellBoreCommonDataId);

                entity.HasIndex(e => e.DayTargetId);

                entity.HasIndex(e => e.MdId);

                entity.HasIndex(e => e.MdKickoffId);

                entity.HasIndex(e => e.MdPlannedId);

                entity.HasIndex(e => e.MdSubSeaPlannedId);

                entity.HasIndex(e => e.TvdId);

                entity.HasIndex(e => e.TvdKickoffId);

                entity.HasIndex(e => e.TvdPlannedId);

                entity.HasIndex(e => e.TvdSubSeaPlannedId);
            });

            modelBuilder.Entity<WellCommonDatas>(entity =>
            {
                entity.HasIndex(e => e.DefaultDatumId);
            });

            modelBuilder.Entity<WellCrss>(entity =>
            {
                entity.HasIndex(e => e.GeodeticCrsid);

                entity.HasIndex(e => e.LocalCrsid);

                entity.HasIndex(e => e.MapProjectionCrsid);

                entity.HasIndex(e => e.WellId);
            });

            modelBuilder.Entity<WellDatums>(entity =>
            {
                entity.HasIndex(e => e.DatumNameId);

                entity.HasIndex(e => e.ElevationId);

                entity.HasIndex(e => e.WellId);
            });

            modelBuilder.Entity<WellLocalCrss>(entity =>
            {
                entity.HasIndex(e => e.YaxisAzimuthId);
            });

            modelBuilder.Entity<WellLocations>(entity =>
            {
                entity.HasIndex(e => e.EastingId);

                entity.HasIndex(e => e.LatitudeId);

                entity.HasIndex(e => e.LocalXid);

                entity.HasIndex(e => e.LocalYid);

                entity.HasIndex(e => e.LongitudeId);

                entity.HasIndex(e => e.NorthingId);

                entity.HasIndex(e => e.WellCrsuid);

                entity.HasIndex(e => e.WellReferencePointReferencePointId);
            });

            modelBuilder.Entity<WellReferencePoints>(entity =>
            {
                entity.HasIndex(e => e.ElevationId);

                entity.HasIndex(e => e.MeasuredDepthId);

                entity.HasIndex(e => e.WellId);
            });

            modelBuilder.Entity<Wells>(entity =>
            {
                entity.HasIndex(e => e.CommonDataId);

                entity.HasIndex(e => e.GroundElevationId);

                entity.HasIndex(e => e.PcInterestId);

                entity.HasIndex(e => e.WaterDepthId);

                entity.HasIndex(e => e.WellLocationLocationId);

                entity.HasIndex(e => e.WellheadElevationElevationId);
            });

            ////Payment methods
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
