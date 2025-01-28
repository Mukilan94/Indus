using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Controllers;
using Well_AI.Advisor.API.Data;
using Well_AI.Advisor.API.Models;
using Well_AI.Advisor.API.Repository.IRepository;
using WellAI.Advisor.API.Repository.IRepository;

namespace Well_AI.Advisor.API.Repository
{
    public class StimJobRepository : IStimJobRepository
    {
        private readonly WellAIAdvisiorContext _db;
        private readonly IMapper _mapper;
        private ITenantRepository _tenantRepository;
        private readonly WellAI.Advisor.DLL.Data.WebAIAdvisorContext _wdb;
        public StimJobRepository(WellAIAdvisiorContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper, ITenantRepository tenantRepository, WellAI.Advisor.DLL.Data.WebAIAdvisorContext wdb)
        {
            _mapper = mapper;
            _tenantRepository = tenantRepository;
            string tenantId = httpContextAccessor.HttpContext.User.Identity.Name;
            _tenantRepository = tenantRepository;
            var options = _tenantRepository.SetDbContext(tenantId);
            db = new WellAIAdvisiorContext(options);
            _db = db;
            _wdb = wdb;
        }
        public bool StimJobExists(string uid)
        {
            bool value = _db.StimJobs.Any(x => x.Uid.ToLower().Trim() == uid.ToLower().Trim());
            return value;
        }

        public bool CreateStimJob(StimJob stimJob)
        {
            try
            {
                TotalPumpTime(stimJob);
                MaxJobPres(stimJob);
                MaxFluidRate(stimJob);
                AvgJobPres(stimJob);
                TotalPumpTime(stimJob);
                TotalJobVolume(stimJob);
                TotalProppantWt(stimJob);
                TotalN2StdVolume(stimJob);
                TotalCO2Mass(stimJob);
                HhpOrdered(stimJob);
                HhpUsed(stimJob);
                FluidEfficiency(stimJob);
                FlowBackPres(stimJob);
                FlowBackRate(stimJob);
                FlowBackVolume(stimJob);
                BottomholeStaticTemperature(stimJob);
                TreatingBottomholeTemperature(stimJob);
                MdFormationTop(stimJob);
                MdFormationBottom(stimJob);
                TvdFormationTop(stimJob);
                TvdFormationBottom(stimJob);
                OpenHoleDiameter(stimJob);
                MdOpenHoleTop(stimJob);
                MdOpenHoleBottom(stimJob);
                TvdOpenHoleTop(stimJob);
                TvdOpenHoleBottom(stimJob);
                TotalFrictionPresLoss(stimJob);
                MaxPresTubing(stimJob);
                MaxPresAnnulus(stimJob);
                MaxFluidRateTubing(stimJob);
                MaxFluidRateAnnulus(stimJob);
                AvgPresTubing(stimJob);
                AvgPresCasing(stimJob);
                BreakDownPres(stimJob);
                AveragePres(stimJob);
                AvgBaseFluidReturnRate(stimJob);
                AvgSlurryReturnRate(stimJob);
                AvgBottomholeRate(stimJob);
                TotalVolume(stimJob);
                MaxProppantConcSurface(stimJob);
                MaxProppantConcBottomhole(stimJob);
                AvgProppantConcSurface(stimJob);
                AvgProppantConcBottomhole(stimJob);
                PerfproppantConc(stimJob);
                TotalProppantMass(stimJob);
                Mass(stimJob);
                TotalProppantUsage(stimJob);
                PercentProppantPumped(stimJob);
                WellboreProppantMass(stimJob);
                FormationProppantMass(stimJob);
                FractureGradient(stimJob);
                FinalFractureGradient(stimJob);
                InitialShutinPres(stimJob);
                Pres(stimJob);
                TimeAfterShutin(stimJob);
                ShutinPres(stimJob);
                ScreenOutPres(stimJob);
                HhpOrderedCO2(stimJob);
                HhpOrderedFluid(stimJob);
                HhpUsedCO2(stimJob);
                HhpUsedFluid(stimJob);
                PerfBallSize(stimJob);
                AvgFractureWidth(stimJob);
                AvgConductivity(stimJob);
                NetPres(stimJob);
                ClosurePres(stimJob);
                ClosureDuration(stimJob);
                MaxTreatmentPres(stimJob);
                MaxSlurryRate(stimJob);
                MaxWellheadRate(stimJob);
                MaxN2StdRate(stimJob);
                MaxCO2LiquidRate(stimJob);
                MaxGelRate(stimJob);
                MaxOilRate(stimJob);
                MaxAcidRate(stimJob);
                MaxPropConc(stimJob);
                MaxSlurryPropConc(stimJob);
                AvgTreatPres(stimJob);
                AvgBaseFluidRate(stimJob);
                AvgSlurryRate(stimJob);
                AvgWellheadRate(stimJob);
                AvgN2StdRate(stimJob);
                AvgCO2LiquidRate(stimJob);
                AvgGelRate(stimJob);
                AvgOilRate(stimJob);
                AvgAcidRate(stimJob);
                AvgPropConc(stimJob);
                AvgSlurryPropConc(stimJob);
                AvgTemperature(stimJob);
                AvgBaseFluidQuality(stimJob);
                AvgN2BaseFluidQuality(stimJob);
                AvgCO2BaseFluidQuality(stimJob);
                AvgHydraulicPower(stimJob);
                BaseFluidVol(stimJob);
                SlurryVol(stimJob);
                WellheadVol(stimJob);
                StdVolN2(stimJob);
                MassCO2(stimJob);
                GelVol(stimJob);
                OilVol(stimJob);
                AcidVol(stimJob);
                BaseFluidBypassVol(stimJob);
                PropMass(stimJob);
                MaxPmaxPacPres(stimJob);
                MaxPmaxWeaklinkPres(stimJob);
                AvgPmaxPacPres(stimJob);
                AvgPmaxWeaklinkPres(stimJob);
                ShutinPres5Min(stimJob);
                ShutinPres10Min(stimJob);
                ShutinPres15Min(stimJob);
                PercentPad(stimJob);
                Id(stimJob);
                Od(stimJob);
                Weight(stimJob);
                MdTop(stimJob);
                MdBottom(stimJob);
                VolumeFactor(stimJob);
                Tubular(stimJob);
                PumpTime(stimJob);
                StartRateSurfaceLiquid(stimJob);
                EndRateSurfaceLiquid(stimJob);
                AvgRateSurfaceLiquid(stimJob);
                StartRateSurfaceCO2(stimJob);
                EndRateSurfaceCO2(stimJob);
                AvgRateSurfaceCO2(stimJob);
                StartStdRateSurfaceN2(stimJob);
                EndStdRateSurfaceN2(stimJob);
                AvgStdRateSurfaceN2(stimJob);
                StartPresSurface(stimJob);
                EndPresSurface(stimJob);
                AveragePresSurface(stimJob);
                StartPumpRateBottomhole(stimJob);
                EndPumpRateBottomhole(stimJob);
                AvgPumpRateBottomhole(stimJob);
                StartPresBottomhole(stimJob);
                EndPresBottomhole(stimJob);
                AveragePresBottomhole(stimJob);
                StartProppantConcSurface(stimJob);
                EndProppantConcSurface(stimJob);
                StartProppantConcBottomhole(stimJob);
                EndProppantConcBottomhole(stimJob);
                StartFoamRateN2(stimJob);
                EndFoamRateN2(stimJob);
                StartFoamRateCO2(stimJob);
                EndFoamRateCO2(stimJob);
                FluidVolBase(stimJob);
                FluidVolSlurry(stimJob);
                SlurryRateBegin(stimJob);
                SlurryRateEnd(stimJob);
                ProppantMassWellHead(stimJob);
                ProppantMass(stimJob);
                MaxPres(stimJob);
                AvgInternalPhaseFraction(stimJob);
                AvgCO2Rate(stimJob);
                GelVolume(stimJob);
                OilVolume(stimJob);
                AcidVolume(stimJob);
                FluidVol(stimJob);
                Volume(stimJob);
                Additive(stimJob);
                Proppant(stimJob);
                StageFluid(stimJob);
                JobStage(stimJob);
                JobEvent(stimJob);
                FlowPath(stimJob);
                PumpDuration(stimJob);
                AvgBottomholeTreatmentPres(stimJob);
                BottomholeHydrostaticPres(stimJob);
                BubblePointPres(stimJob);
                FractureClosePres(stimJob);
                FrictionPres(stimJob);
                PorePres(stimJob);
                AvgBottomholeTreatmentRate(stimJob);
                FluidDensity(stimJob);
                WellboreVolume(stimJob);
                MdSurface(stimJob);
                MdBottomhole(stimJob);
                MdMidPerforation(stimJob);
                TvdMidPerforation(stimJob);
                SurfaceTemperature(stimJob);
                BottomholeTemperature(stimJob);
                SurfaceFluidTemperature(stimJob);
                FluidCompressibility(stimJob);
                NetPayFluidViscosity(stimJob);
                ReservoirTotalCompressibility(stimJob);
                FluidSpecificHeat(stimJob);
                FluidThermalConductivity(stimJob);
                FluidThermalExpansionCoefficient(stimJob);
                FoamQuality(stimJob);
                BottomholeRate(stimJob);
                PresMeasurement(stimJob);
                FractureExtensionPres(stimJob);
                StepRateTest(stimJob);
                EndPdlDuration(stimJob);
                FractureCloseDuration(stimJob);
                PseudoRadialPres(stimJob);
                FractureLength(stimJob);
                FractureWidth(stimJob);
                ResidualPermeability(stimJob);
                FluidEfficiencyTest(stimJob);
                PumpFlowBackTest(stimJob);
                BottomholeFluidDensity(stimJob);
                DiameterEntryHole(stimJob);
                PipeFriction(stimJob);
                EntryFriction(stimJob);
                PerfFriction(stimJob);
                NearWellboreFriction(stimJob);
                Step(stimJob);
                StepDownTest(stimJob);
                PdatSession(stimJob);
                MdLithTop(stimJob);
                MdLithBottom(stimJob);
                LithFormationPermeability(stimJob);
                LithYoungsModulus(stimJob);
                LithPorePres(stimJob);
                LithNetPayThickness(stimJob);
                MdGrossPayTop(stimJob);
                MdGrossPayBottom(stimJob);
                GrossPayThickness(stimJob);
                NetPayThickness(stimJob);
                NetPayPorePres(stimJob);
                NetPayFluidCompressibility(stimJob);
                NetPayFormationPermeability(stimJob);
                LithPoissonsRatio(stimJob);
                NetPayFormationPorosity(stimJob);
                FormationPermeability(stimJob);
                FormationPorosity(stimJob);
                ReservoirInterval(stimJob);
                MdPerforationsTop(stimJob);
                MdPerforationsBottom(stimJob);
                TvdPerforationsTop(stimJob);
                TvdPerforationsBottom(stimJob);
                Size(stimJob);
                DensityPerforation(stimJob);
                PhasingPerforation(stimJob);
                PerforationInterval(stimJob);
                JobInterval(stimJob);
                _db.StimJobs.Add(stimJob);
                return Save();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "StimJobRepository CreateStimJob", null);
                return Save();
            }
        }



        public bool DeleteStimJob(StimJob stimJob)
        {
            _db.StimJobs.Remove(stimJob);
            return Save();
        }

        public StimJob GetStimJobDetail(string Uid)
        {
            return _db.StimJobs.FirstOrDefault(x => x.Uid == Uid);

        }

        public ICollection<StimJob> GetStimJobDetails()
        {
            return _db.StimJobs.OrderBy(x => x.NameWell).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateStimJob(StimJob stimJob)
        {
            try

            {
                UpdateTotalPumpTime(stimJob);
                UpdateMaxJobPres(stimJob);
                UpdateMaxFluidRate(stimJob);
                UpdateAvgJobPres(stimJob);
                UpdateTotalPumpTime(stimJob);
                UpdateTotalJobVolume(stimJob);
                UpdateTotalProppantWt(stimJob);
                UpdateTotalN2StdVolume(stimJob);
                UpdateTotalCO2Mass(stimJob);
                UpdateHhpOrdered(stimJob);
                UpdateHhpUsed(stimJob);
                UpdateFluidEfficiency(stimJob);
                UpdateFlowBackPres(stimJob);
                UpdateFlowBackRate(stimJob);
                UpdateFlowBackVolume(stimJob);
                UpdateBottomholeStaticTemperature(stimJob);
                UpdateTreatingBottomholeTemperature(stimJob);
                UpdateMdFormationTop(stimJob);
                UpdateMdFormationBottom(stimJob);
                UpdateTvdFormationBottom(stimJob);
                UpdateOpenHoleDiameter(stimJob);
                UpdateMdOpenHoleTop(stimJob);
                UpdateMdOpenHoleBottom(stimJob);
                UpdateTvdOpenHoleTop(stimJob);
                UpdateTvdOpenHoleBottom(stimJob);
                UpdateTotalFrictionPresLoss(stimJob);
                UpdateMaxPresTubing(stimJob);
                UpdateMaxPresAnnulus(stimJob);
                UpdateMaxFluidRateTubing(stimJob);
                UpdateMaxFluidRateAnnulus(stimJob);
                UpdateAvgPresTubing(stimJob);
                UpdateAvgPresCasing(stimJob);
                UpdateBreakDownPres(stimJob);
                UpdateAveragePres(stimJob);
                UpdateAvgBaseFluidReturnRate(stimJob);
                UpdateAvgSlurryReturnRate(stimJob);
                UpdateAvgBottomholeRate(stimJob);
                UpdateTotalVolume(stimJob);
                UpdateMaxProppantConcSurface(stimJob);
                UpdateMaxProppantConcBottomhole(stimJob);
                UpdateAvgProppantConcSurface(stimJob);
                UpdateAvgProppantConcBottomhole(stimJob);
                UpdatePerfproppantConc(stimJob);
                UpdateTotalProppantMass(stimJob);
                UpdateMass(stimJob);
                UpdateTotalProppantUsage(stimJob);
                UpdatePercentProppantPumped(stimJob);
                UpdateWellboreProppantMass(stimJob);
                UpdateFormationProppantMass(stimJob);
                UpdateFractureGradient(stimJob);
                UpdateFinalFractureGradient(stimJob);
                UpdateInitialShutinPres(stimJob);
                UpdatePres(stimJob);
                UpdateTimeAfterShutin(stimJob);
                UpdateShutinPres(stimJob);
                UpdateScreenOutPres(stimJob);
                UpdateHhpOrderedCO2(stimJob);
                UpdateHhpOrderedFluid(stimJob);
                UpdateHhpUsedCO2(stimJob);
                UpdateHhpUsedFluid(stimJob);
                UpdatePerfBallSize(stimJob);
                UpdateAvgFractureWidth(stimJob);
                UpdateAvgConductivity(stimJob);
                UpdateNetPres(stimJob);
                UpdateClosurePres(stimJob);
                UpdateClosureDuration(stimJob);
                UpdateMaxTreatmentPres(stimJob);
                UpdateMaxSlurryRate(stimJob);
                UpdateMaxWellheadRate(stimJob);
                UpdateMaxN2StdRate(stimJob);
                UpdateMaxCO2LiquidRate(stimJob);
                UpdateMaxGelRate(stimJob);
                UpdateMaxOilRate(stimJob);
                UpdateMaxAcidRate(stimJob);
                UpdateMaxPropConc(stimJob);
                UpdateMaxSlurryPropConc(stimJob);
                UpdateAvgTreatPres(stimJob);
                UpdateAvgBaseFluidRate(stimJob);
                UpdateAvgSlurryRate(stimJob);
                UpdateAvgWellheadRate(stimJob);
                UpdateAvgN2StdRate(stimJob);
                UpdateAvgCO2LiquidRate(stimJob);
                UpdateAvgGelRate(stimJob);
                UpdateAvgOilRate(stimJob);
                UpdateAvgAcidRate(stimJob);
                UpdateAvgPropConc(stimJob);
                UpdateAvgSlurryPropConc(stimJob);
                UpdateAvgTemperature(stimJob);
                UpdateAvgBaseFluidQuality(stimJob);
                UpdateAvgN2BaseFluidQuality(stimJob);
                UpdateAvgCO2BaseFluidQuality(stimJob);
                UpdateAvgHydraulicPower(stimJob);
                UpdateBaseFluidVol(stimJob);
                UpdateSlurryVol(stimJob);
                UpdateWellheadVol(stimJob);
                UpdateStdVolN2(stimJob);
                UpdateMassCO2(stimJob);
                UpdateGelVol(stimJob);
                UpdateOilVol(stimJob);
                UpdateAcidVol(stimJob);
                UpdateBaseFluidBypassVol(stimJob);
                UpdatePropMass(stimJob);
                UpdateMaxPmaxPacPres(stimJob);
                UpdateMaxPmaxWeaklinkPres(stimJob);
                UpdateAvgPmaxPacPres(stimJob);
                UpdateAvgPmaxWeaklinkPres(stimJob);
                UpdateShutinPres5Min(stimJob);
                UpdateShutinPres10Min(stimJob);
                UpdateShutinPres15Min(stimJob);
                UpdatePercentPad(stimJob);
                UpdateId(stimJob);
                UpdateOd(stimJob);
                UpdateWeight(stimJob);
                UpdateMdTop(stimJob);
                UpdateMdBottom(stimJob);
                UpdateVolumeFactor(stimJob);
                UpdateTubular(stimJob);
                UpdatePumpTime(stimJob);
                UpdateStartRateSurfaceLiquid(stimJob);
                UpdateEndRateSurfaceLiquid(stimJob);
                UpdateAvgRateSurfaceLiquid(stimJob);
                UpdateStartRateSurfaceCO2(stimJob);
                UpdateEndRateSurfaceCO2(stimJob);
                UpdateAvgRateSurfaceCO2(stimJob);
                UpdateStartStdRateSurfaceN2(stimJob);
                UpdateEndStdRateSurfaceN2(stimJob);
                UpdateAvgStdRateSurfaceN2(stimJob);
                UpdateStartPresSurface(stimJob);
                UpdateEndPresSurface(stimJob);
                UpdateAveragePresSurface(stimJob);
                UpdateStartPumpRateBottomhole(stimJob);
                UpdateEndPumpRateBottomhole(stimJob);
                UpdateAvgPumpRateBottomhole(stimJob);
                UpdateStartPresBottomhole(stimJob);
                UpdateEndPresBottomhole(stimJob);
                UpdateAveragePresBottomhole(stimJob);
                UpdateStartProppantConcSurface(stimJob);
                UpdateEndProppantConcSurface(stimJob);
                UpdateStartProppantConcBottomhole(stimJob);
                UpdateEndProppantConcBottomhole(stimJob);
                UpdateStartFoamRateN2(stimJob);
                UpdateEndFoamRateN2(stimJob);
                UpdateStartFoamRateCO2(stimJob);
                UpdateEndFoamRateCO2(stimJob);
                UpdateFluidVolBase(stimJob);
                UpdateFluidVolSlurry(stimJob);
                UpdateSlurryRateBegin(stimJob);
                UpdateSlurryRateEnd(stimJob);
                UpdateProppantMassWellHead(stimJob);
                UpdateProppantMass(stimJob);
                UpdateMaxPres(stimJob);
                UpdateAvgInternalPhaseFraction(stimJob);
                UpdateAvgCO2Rate(stimJob);
                UpdateGelVolume(stimJob);
                UpdateOilVolume(stimJob);
                UpdateAcidVolume(stimJob);
                UpdateFluidVol(stimJob);
                UpdateVolume(stimJob);
                UpdateAdditive(stimJob);
                UpdateProppant(stimJob);
                UpdateStageFluid(stimJob);
                UpdateJobStage(stimJob);
                UpdateJobEvent(stimJob);
                UpdateFlowPath(stimJob);
                UpdatePumpDuration(stimJob);
                UpdateAvgBottomholeTreatmentPres(stimJob);
                UpdateBottomholeHydrostaticPres(stimJob);
                UpdateBubblePointPres(stimJob);
                UpdateFractureClosePres(stimJob);
                UpdateFrictionPres(stimJob);
                UpdatePorePres(stimJob);
                UpdateAvgBottomholeTreatmentRate(stimJob);
                UpdateFluidDensity(stimJob);
                UpdateWellboreVolume(stimJob);
                UpdateMdSurface(stimJob);
                UpdateMdBottomhole(stimJob);
                UpdateMdMidPerforation(stimJob);
                UpdateTvdMidPerforation(stimJob);
                UpdateSurfaceTemperature(stimJob);
                UpdateBottomholeTemperature(stimJob);
                UpdateSurfaceFluidTemperature(stimJob);
                UpdateFluidCompressibility(stimJob);
                UpdateReservoirTotalCompressibility(stimJob);
                UpdateFluidSpecificHeat(stimJob);
                UpdateFluidThermalConductivity(stimJob);
                UpdateFluidThermalExpansionCoefficient(stimJob);
                UpdateFoamQuality(stimJob);
                UpdateBottomholeRate(stimJob);
                UpdatePresMeasurement(stimJob);
                UpdateFractureExtensionPres(stimJob);
                UpdateStepRateTest(stimJob);
                UpdateEndPdlDuration(stimJob);
                UpdateFractureCloseDuration(stimJob);
                UpdatePseudoRadialPres(stimJob);
                UpdateFractureLength(stimJob);
                UpdateFractureWidth(stimJob);
                UpdateResidualPermeability(stimJob);
                UpdateFluidEfficiencyTest(stimJob);
                UpdatePumpFlowBackTest(stimJob);
                UpdateBottomholeFluidDensity(stimJob);
                UpdateDiameterEntryHole(stimJob);
                UpdatePipeFriction(stimJob);
                UpdateEntryFriction(stimJob);
                UpdatePerfFriction(stimJob);
                UpdateNearWellboreFriction(stimJob);
                UpdateStep(stimJob);
                UpdateStepDownTest(stimJob);
                UpdatePdatSession(stimJob);
                UpdateMdLithTop(stimJob);
                UpdateMdLithBottom(stimJob);
                UpdateLithFormationPermeability(stimJob);
                UpdateLithYoungsModulus(stimJob);
                UpdateLithPorePres(stimJob);
                UpdateLithNetPayThickness(stimJob);
                UpdateMdGrossPayTop(stimJob);
                UpdateMdGrossPayBottom(stimJob);
                UpdateGrossPayThickness(stimJob);
                UpdateNetPayThickness(stimJob);
                UpdateNetPayPorePres(stimJob);
                UpdateNetPayFluidCompressibility(stimJob);
                UpdateNetPayFormationPermeability(stimJob);
                UpdateLithPoissonsRatio(stimJob);
                UpdateNetPayFormationPorosity(stimJob);
                UpdateFormationPermeability(stimJob);
                UpdateFormationPorosity(stimJob);
                UpdateReservoirInterval(stimJob);
                UpdateMdPerforationsTop(stimJob);
                UpdateMdPerforationsBottom(stimJob);
                UpdateTvdPerforationsTop(stimJob);
                UpdateTvdPerforationsBottom(stimJob);
                UpdateSize(stimJob);
                UpdateDensityPerforation(stimJob);
                UpdatePhasingPerforation(stimJob);
                UpdatePerforationInterval(stimJob);
                UpdateJobInterval(stimJob);
                _db.StimJobs.Update(stimJob);
                return Save();
            }

            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "StimJobRepository UpdateStimJob", null);
                return Save();
            }
        }

        #region Insert StimJob
        private void TotalPumpTime(StimJob stimJob)
        {
            if (stimJob.TotalPumpTime != null && stimJob.TotalPumpTime.Uom != null)
            {
                var obj = _mapper.Map<TotalPumpTime>(stimJob.TotalPumpTime);
                _db.StimJobTotalPumpTimes.Add(obj);
            }
        }

        private void MaxJobPres(StimJob stimJob)
        {
            if (stimJob.MaxJobPres != null && stimJob.MaxJobPres.Uom != null)
            {
                var obj = _mapper.Map<MaxJobPres>(stimJob.MaxJobPres);
                _db.StimJobMaxJobPress.Add(obj);
            }
        }

        private void MaxFluidRate(StimJob stimJob)
        {
            if (stimJob.MaxFluidRate != null && stimJob.MaxFluidRate.Uom != null)
            {
                var obj = _mapper.Map<MaxFluidRate>(stimJob.MaxFluidRate);
                _db.StimJobMaxFluidRates.Add(obj);
            }
        }

        private void AvgJobPres(StimJob stimJob)
        {
            if (stimJob.AvgJobPres != null && stimJob.AvgJobPres.Uom != null)
            {
                var obj = _mapper.Map<AvgJobPres>(stimJob.AvgJobPres);
                _db.StimJobAvgJobPress.Add(obj);
            }
        }

        private void TotalJobVolume(StimJob stimJob)
        {
            if (stimJob.TotalJobVolume != null && stimJob.TotalJobVolume.Uom != null)
            {
                var obj = _mapper.Map<TotalJobVolume>(stimJob.TotalJobVolume);
                _db.StimJobTotalJobVolumes.Add(obj);
            }
        }

        private void TotalProppantWt(StimJob stimJob)
        {
            if (stimJob.TotalProppantWt != null && stimJob.TotalProppantWt.Uom != null)
            {
                var obj = _mapper.Map<TotalProppantWt>(stimJob.TotalProppantWt);
                _db.StimJobTotalProppantWts.Add(obj);
            }
        }

        private void TotalN2StdVolume(StimJob stimJob)
        {
            if (stimJob.TotalN2StdVolume != null && stimJob.TotalN2StdVolume.Uom != null)
            {
                var obj = _mapper.Map<TotalN2StdVolume>(stimJob.TotalN2StdVolume);
                _db.StimJobTotalN2StdVolumes.Add(obj);
            }
        }

        private void TotalCO2Mass(StimJob stimJob)
        {
            if (stimJob.TotalCO2Mass != null && stimJob.TotalCO2Mass.Uom != null)
            {
                var obj = _mapper.Map<TotalCO2Mass>(stimJob.TotalCO2Mass);
                _db.StimJobTotalCO2Masss.Add(obj);
            }
        }

        private void HhpOrdered(StimJob stimJob)
        {
            if (stimJob.HhpOrdered != null && stimJob.HhpOrdered.Uom != null)
            {
                var obj = _mapper.Map<HhpOrdered>(stimJob.HhpOrdered);
                _db.StimJobHhpOrdereds.Add(obj);
            }
        }

        private void HhpUsed(StimJob stimJob)
        {
            if (stimJob.HhpUsed != null && stimJob.HhpUsed.Uom != null)
            {
                var obj = _mapper.Map<HhpUsed>(stimJob.HhpUsed);
                _db.StimJobHhpUseds.Add(obj);
            }
        }

        private void FluidEfficiency(StimJob stimJob)
        {
            if (stimJob.FluidEfficiency != null && stimJob.FluidEfficiency.Uom != null)
            {
                var obj = _mapper.Map<FluidEfficiency>(stimJob.FluidEfficiency);
                _db.StimJobFluidEfficiencys.Add(obj);
            }
        }

        private void FlowBackPres(StimJob stimJob)
        {
            if (stimJob.FlowBackPres != null && stimJob.FlowBackPres.Uom != null)
            {
                var obj = _mapper.Map<FlowBackPres>(stimJob.FlowBackPres);
                _db.StimJobFlowBackPress.Add(obj);
            }
        }

        private void FlowBackRate(StimJob stimJob)
        {
            if (stimJob.FlowBackRate != null && stimJob.FlowBackRate.Uom != null)
            {
                var obj = _mapper.Map<FlowBackRate>(stimJob.FlowBackRate);
                _db.StimJobFlowBackRates.Add(obj);
            }
        }

        private void FlowBackVolume(StimJob stimJob)
        {
            if (stimJob.FlowBackVolume != null && stimJob.FlowBackVolume.Uom != null)
            {
                var obj = _mapper.Map<FlowBackVolume>(stimJob.FlowBackVolume);
                _db.StimJobFlowBackVolumes.Add(obj);
            }
        }

        private void BottomholeStaticTemperature(StimJob stimJob)
        {
            if (stimJob.BottomholeStaticTemperature != null && stimJob.BottomholeStaticTemperature.Uom != null)
            {
                var obj = _mapper.Map<BottomholeStaticTemperature>(stimJob.BottomholeStaticTemperature);
                _db.StimJobBottomholeStaticTemperatures.Add(obj);
            }
        }

        private void TreatingBottomholeTemperature(StimJob stimJob)
        {
            if (stimJob.TreatingBottomholeTemperature != null && stimJob.TreatingBottomholeTemperature.Uom != null)
            {
                var obj = _mapper.Map<TreatingBottomholeTemperature>(stimJob.TreatingBottomholeTemperature);
                _db.StimJobTreatingBottomholeTemperatures.Add(obj);
            }
        }

        private void MdFormationTop(StimJob stimJob)
        {
            if (stimJob.JobInterval.MdFormationTop != null && stimJob.JobInterval.MdFormationTop.Uom != null)
            {
                var obj = _mapper.Map<MdFormationTop>(stimJob.JobInterval.MdFormationTop);
                _db.StimJobMdFormationTops.Add(obj);
            }
        }

        private void MdFormationBottom(StimJob stimJob)
        {
            if (stimJob.JobInterval.MdFormationBottom != null && stimJob.JobInterval.MdFormationBottom.Uom != null)
            {
                var obj = _mapper.Map<MdFormationBottom>(stimJob.JobInterval.MdFormationBottom);
                _db.StimJobMdFormationBottoms.Add(obj);
            }
        }

        private void TvdFormationTop(StimJob stimJob)
        {
            if (stimJob.JobInterval.TvdFormationTop != null && stimJob.JobInterval.TvdFormationTop.Uom != null)
            {
                var obj = _mapper.Map<TvdFormationTop>(stimJob.JobInterval.TvdFormationTop);
                _db.StimJobTvdFormationTops.Add(obj);
            }
        }

        private void TvdFormationBottom(StimJob stimJob)
        {
            if (stimJob.JobInterval.TvdFormationBottom != null && stimJob.JobInterval.TvdFormationBottom.Uom != null)
            {
                var obj = _mapper.Map<TvdFormationBottom>(stimJob.JobInterval.TvdFormationBottom);
                _db.StimJobTvdFormationBottoms.Add(obj);
            }
        }

        private void OpenHoleDiameter(StimJob stimJob)
        {
            if (stimJob.JobInterval.OpenHoleDiameter != null && stimJob.JobInterval.OpenHoleDiameter.Uom != null)
            {
                var obj = _mapper.Map<OpenHoleDiameter>(stimJob.JobInterval.OpenHoleDiameter);
                _db.StimJobOpenHoleDiameters.Add(obj);
            }
        }

        private void MdOpenHoleTop(StimJob stimJob)
        {
            if (stimJob.JobInterval.MdOpenHoleTop != null && stimJob.JobInterval.MdOpenHoleTop.Uom != null)
            {
                var obj = _mapper.Map<MdOpenHoleTop>(stimJob.JobInterval.MdOpenHoleTop);
                _db.StimJobMdOpenHoleTops.Add(obj);
            }
        }

        private void MdOpenHoleBottom(StimJob stimJob)
        {
            if (stimJob.JobInterval.MdOpenHoleBottom != null && stimJob.JobInterval.MdOpenHoleBottom.Uom != null)
            {
                var obj = _mapper.Map<MdOpenHoleBottom>(stimJob.JobInterval.MdOpenHoleBottom);
                _db.StimJobMdOpenHoleBottoms.Add(obj);
            }
        }

        private void TvdOpenHoleTop(StimJob stimJob)
        {
            if (stimJob.JobInterval.TvdOpenHoleTop != null && stimJob.JobInterval.TvdOpenHoleTop.Uom != null)
            {
                var obj = _mapper.Map<TvdOpenHoleTop>(stimJob.JobInterval.TvdOpenHoleTop);
                _db.StimJobTvdOpenHoleTops.Add(obj);
            }
        }

        private void TvdOpenHoleBottom(StimJob stimJob)
        {
            if (stimJob.JobInterval.TvdOpenHoleBottom != null && stimJob.JobInterval.TvdOpenHoleBottom.Uom != null)
            {
                var obj = _mapper.Map<TvdOpenHoleBottom>(stimJob.JobInterval.TvdOpenHoleBottom);
                _db.StimJobTvdOpenHoleBottoms.Add(obj);
            }
        }

        private void TotalFrictionPresLoss(StimJob stimJob)
        {
            if (stimJob.JobInterval.TotalFrictionPresLoss != null && stimJob.JobInterval.TotalFrictionPresLoss.Uom != null)
            {
                var obj = _mapper.Map<TotalFrictionPresLoss>(stimJob.JobInterval.TotalFrictionPresLoss);
                _db.StimJobTotalFrictionPresLosss.Add(obj);
            }
        }

        private void MaxPresTubing(StimJob stimJob)
        {
            if (stimJob.JobInterval.MaxPresTubing != null && stimJob.JobInterval.MaxPresTubing.Uom != null)
            {
                var obj = _mapper.Map<MaxPresTubing>(stimJob.JobInterval.MaxPresTubing);
                _db.StimJobMaxPresTubings.Add(obj);
            }
        }

        private void MaxPresAnnulus(StimJob stimJob)
        {
            if (stimJob.JobInterval.MaxPresAnnulus != null && stimJob.JobInterval.MaxPresAnnulus.Uom != null)
            {
                var obj = _mapper.Map<MaxPresAnnulus>(stimJob.JobInterval.MaxPresAnnulus);
                _db.StimJobMaxPresAnnuluss.Add(obj);
            }
        }

        private void MaxFluidRateTubing(StimJob stimJob)
        {
            if (stimJob.JobInterval.MaxFluidRateTubing != null && stimJob.JobInterval.MaxFluidRateTubing.Uom != null)
            {
                var obj = _mapper.Map<MaxFluidRateTubing>(stimJob.JobInterval.MaxFluidRateTubing);
                _db.StimJobMaxFluidRateTubings.Add(obj);
            }
        }

        private void MaxFluidRateAnnulus(StimJob stimJob)
        {
            if (stimJob.JobInterval.MaxFluidRateAnnulus != null && stimJob.JobInterval.MaxFluidRateAnnulus.Uom != null)
            {
                var obj = _mapper.Map<MaxFluidRateAnnulus>(stimJob.JobInterval.MaxFluidRateAnnulus);
                _db.StimJobMaxFluidRateAnnuluss.Add(obj);
            }
        }

        private void AvgPresTubing(StimJob stimJob)
        {
            if (stimJob.JobInterval.AvgPresTubing != null && stimJob.JobInterval.AvgPresTubing.Uom != null)
            {
                var obj = _mapper.Map<AvgPresTubing>(stimJob.JobInterval.AvgPresTubing);
                _db.StimJobAvgPresTubings.Add(obj);
            }
        }

        private void AvgPresCasing(StimJob stimJob)
        {
            if (stimJob.JobInterval.AvgPresCasing != null && stimJob.JobInterval.AvgPresCasing.Uom != null)
            {
                var obj = _mapper.Map<AvgPresCasing>(stimJob.JobInterval.AvgPresCasing);
                _db.StimJobAvgPresCasings.Add(obj);
            }
        }

        private void BreakDownPres(StimJob stimJob)
        {
            if (stimJob.JobInterval.BreakDownPres != null && stimJob.JobInterval.BreakDownPres.Uom != null)
            {
                var obj = _mapper.Map<BreakDownPres>(stimJob.JobInterval.BreakDownPres);
                _db.StimJobBreakDownPress.Add(obj);
            }
        }

        private void AveragePres(StimJob stimJob)
        {
            if (stimJob.JobInterval.AveragePres != null && stimJob.JobInterval.AveragePres.Uom != null)
            {
                var obj = _mapper.Map<AveragePres>(stimJob.JobInterval.AveragePres);
                _db.StimJobAveragePress.Add(obj);
            }
        }

        private void AvgBaseFluidReturnRate(StimJob stimJob)
        {
            if (stimJob.JobInterval.AvgBaseFluidReturnRate != null && stimJob.JobInterval.AvgBaseFluidReturnRate.Uom != null)
            {
                var obj = _mapper.Map<AvgBaseFluidReturnRate>(stimJob.JobInterval.AvgBaseFluidReturnRate);
                _db.StimJobAvgBaseFluidReturnRates.Add(obj);
            }
        }

        private void AvgSlurryReturnRate(StimJob stimJob)
        {
            if (stimJob.JobInterval.AvgSlurryReturnRate != null && stimJob.JobInterval.AvgSlurryReturnRate.Uom != null)
            {
                var obj = _mapper.Map<AvgSlurryReturnRate>(stimJob.JobInterval.AvgSlurryReturnRate);
                _db.StimJobAvgSlurryReturnRates.Add(obj);
            }
        }

        private void AvgBottomholeRate(StimJob stimJob)
        {
            if (stimJob.JobInterval.AvgBottomholeRate != null && stimJob.JobInterval.AvgBottomholeRate.Uom != null)
            {
                var obj = _mapper.Map<AvgBottomholeRate>(stimJob.JobInterval.AvgBottomholeRate);
                _db.StimJobAvgBottomholeRates.Add(obj);
            }
        }

        private void TotalVolume(StimJob stimJob)
        {
            if (stimJob.JobInterval.TotalVolume != null && stimJob.JobInterval.TotalVolume.Uom != null)
            {
                var obj = _mapper.Map<TotalVolume>(stimJob.JobInterval.TotalVolume);
                _db.StimJobTotalVolumes.Add(obj);
            }
        }

        private void MaxProppantConcSurface(StimJob stimJob)
        {
            if (stimJob.JobInterval.MaxProppantConcSurface != null && stimJob.JobInterval.MaxProppantConcSurface.Uom != null)
            {
                var obj = _mapper.Map<MaxProppantConcSurface>(stimJob.JobInterval.MaxProppantConcSurface);
                _db.StimJobMaxProppantConcSurfaces.Add(obj);
            }
        }

        private void MaxProppantConcBottomhole(StimJob stimJob)
        {
            if (stimJob.JobInterval.MaxProppantConcBottomhole != null && stimJob.JobInterval.MaxProppantConcBottomhole.Uom != null)
            {
                var obj = _mapper.Map<MaxProppantConcBottomhole>(stimJob.JobInterval.MaxProppantConcBottomhole);
                _db.StimJobMaxProppantConcBottomholes.Add(obj);
            }
        }

        private void AvgProppantConcSurface(StimJob stimJob)
        {
            if (stimJob.JobInterval.AvgProppantConcSurface != null && stimJob.JobInterval.AvgProppantConcSurface.Uom != null)
            {
                var obj = _mapper.Map<AvgProppantConcSurface>(stimJob.JobInterval.AvgProppantConcSurface);
                _db.StimJobAvgProppantConcSurfaces.Add(obj);
            }
        }

        private void AvgProppantConcBottomhole(StimJob stimJob)
        {
            if (stimJob.JobInterval.AvgProppantConcBottomhole != null && stimJob.JobInterval.AvgProppantConcBottomhole.Uom != null)
            {
                var obj = _mapper.Map<AvgProppantConcBottomhole>(stimJob.JobInterval.AvgProppantConcBottomhole);
                _db.StimJobAvgProppantConcBottomholes.Add(obj);
            }
        }

        private void PerfproppantConc(StimJob stimJob)
        {
            if (stimJob.JobInterval.PerfproppantConc != null && stimJob.JobInterval.PerfproppantConc.Uom != null)
            {
                var obj = _mapper.Map<PerfproppantConc>(stimJob.JobInterval.PerfproppantConc);
                _db.StimJobPerfproppantConcs.Add(obj);
            }
        }

        private void TotalProppantMass(StimJob stimJob)
        {
            if (stimJob.JobInterval.TotalProppantMass != null && stimJob.JobInterval.TotalProppantMass.Uom != null)
            {
                var obj = _mapper.Map<TotalProppantMass>(stimJob.JobInterval.TotalProppantMass);
                _db.StimJobTotalProppantMasss.Add(obj);
            }
        }

        private void Mass(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.TotalProppantUsage)
            {

                if (item.Mass != null && item.Mass.Uom != null)
                {
                    var obj = _mapper.Map<Mass>(item.Mass);
                    _db.StimJobMasss.Add(obj);
                }
            }

            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    foreach (var childitem in subItem.StageFluid.Additive)
                    {
                        if (childitem.Mass != null && childitem.Mass.Uom != null)
                        {
                            var obj = _mapper.Map<Mass>(childitem.Mass);
                            _db.StimJobMasss.Add(obj);
                        }
                    }
                }
            }
        }

        private void TotalProppantUsage(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.TotalProppantUsage)
            {

                if (item != null && item.Uid != null)
                {
                    var obj = _mapper.Map<TotalProppantUsage>(item);
                    _db.StimJobTotalProppantUsages.Add(obj);
                }
            }
        }

        private void PercentProppantPumped(StimJob stimJob)
        {
            if (stimJob.JobInterval.PercentProppantPumped != null && stimJob.JobInterval.PercentProppantPumped.Uom != null)
            {
                var obj = _mapper.Map<PercentProppantPumped>(stimJob.JobInterval.PercentProppantPumped);
                _db.StimJobPercentProppantPumpeds.Add(obj);
            }
        }

        private void WellboreProppantMass(StimJob stimJob)
        {
            if (stimJob.JobInterval.WellboreProppantMass != null && stimJob.JobInterval.WellboreProppantMass.Uom != null)
            {
                var obj = _mapper.Map<WellboreProppantMass>(stimJob.JobInterval.WellboreProppantMass);
                _db.StimJobWellboreProppantMasss.Add(obj);
            }
        }

        private void FormationProppantMass(StimJob stimJob)
        {
            if (stimJob.JobInterval.FormationProppantMass != null && stimJob.JobInterval.FormationProppantMass.Uom != null)
            {
                var obj = _mapper.Map<FormationProppantMass>(stimJob.JobInterval.FormationProppantMass);
                _db.StimJobFormationProppantMasss.Add(obj);
            }
        }

        private void FractureGradient(StimJob stimJob)
        {
            if (stimJob.JobInterval.FractureGradient != null && stimJob.JobInterval.FractureGradient.Uom != null)
            {
                var obj = _mapper.Map<FractureGradient>(stimJob.JobInterval.FractureGradient);
                _db.StimJobFractureGradients.Add(obj);
            }
        }

        private void FinalFractureGradient(StimJob stimJob)
        {
            if (stimJob.JobInterval.FinalFractureGradient != null && stimJob.JobInterval.FinalFractureGradient.Uom != null)
            {
                var obj = _mapper.Map<FinalFractureGradient>(stimJob.JobInterval.FinalFractureGradient);
                _db.StimJobFinalFractureGradients.Add(obj);
            }
        }

        private void InitialShutinPres(StimJob stimJob)
        {
            if (stimJob.JobInterval.InitialShutinPres != null && stimJob.JobInterval.InitialShutinPres.Uom != null)
            {
                var obj = _mapper.Map<InitialShutinPres>(stimJob.JobInterval.InitialShutinPres);
                _db.StimJobInitialShutinPress.Add(obj);
            }

            if (stimJob.JobInterval.PdatSession.StepDownTest != null && stimJob.JobInterval.PdatSession.StepDownTest.InitialShutinPres.Uom != null)
            {
                var obj = _mapper.Map<InitialShutinPres>(stimJob.JobInterval.PdatSession.StepDownTest.InitialShutinPres);
                _db.StimJobInitialShutinPress.Add(obj);
            }
        }

        private void Pres(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.ShutinPres)
            {
                if (item.Pres != null && item.Pres.Uom != null)
                {
                    var obj = _mapper.Map<Pres>(item.Pres);
                    _db.StimJobPress.Add(obj);
                }
            }
        }

        private void TimeAfterShutin(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.ShutinPres)
            {
                if (item.TimeAfterShutin != null && item.TimeAfterShutin.Uom != null)
                {
                    var obj = _mapper.Map<TimeAfterShutin>(item.TimeAfterShutin);
                    _db.StimJobTimeAfterShutins.Add(obj);
                }
            }
        }

        private void ShutinPres(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.ShutinPres)
            {
                if (item != null && item.Uid != null)
                {
                    var obj = _mapper.Map<ShutinPres>(item);
                    _db.StimJobShutinPress.Add(obj);
                }
            }
        }

        private void ScreenOutPres(StimJob stimJob)
        {
            if (stimJob.JobInterval.ScreenOutPres != null && stimJob.JobInterval.ScreenOutPres.Uom != null)
            {
                var obj = _mapper.Map<ScreenOutPres>(stimJob.JobInterval.ScreenOutPres);
                _db.StimJobScreenOutPress.Add(obj);
            }
        }

        private void HhpOrderedCO2(StimJob stimJob)
        {
            if (stimJob.JobInterval.HhpOrderedCO2 != null && stimJob.JobInterval.HhpOrderedCO2.Uom != null)
            {
                var obj = _mapper.Map<HhpOrderedCO2>(stimJob.JobInterval.HhpOrderedCO2);
                _db.StimJobHhpOrderedCO2s.Add(obj);
            }
        }

        private void HhpOrderedFluid(StimJob stimJob)
        {
            if (stimJob.JobInterval.HhpOrderedFluid != null && stimJob.JobInterval.HhpOrderedFluid.Uom != null)
            {
                var obj = _mapper.Map<HhpOrderedFluid>(stimJob.JobInterval.HhpOrderedFluid);
                _db.StimJobHhpOrderedFluids.Add(obj);
            }
        }

        private void HhpUsedCO2(StimJob stimJob)
        {
            if (stimJob.JobInterval.HhpUsedCO2 != null && stimJob.JobInterval.HhpUsedCO2.Uom != null)
            {
                var obj = _mapper.Map<HhpUsedCO2>(stimJob.JobInterval.HhpUsedCO2);
                _db.StimJobHhpUsedCO2s.Add(obj);
            }
        }

        private void HhpUsedFluid(StimJob stimJob)
        {
            if (stimJob.JobInterval.HhpUsedFluid != null && stimJob.JobInterval.HhpUsedFluid.Uom != null)
            {
                var obj = _mapper.Map<HhpUsedFluid>(stimJob.JobInterval.HhpUsedFluid);
                _db.StimJobHhpUsedFluids.Add(obj);
            }
        }

        private void PerfBallSize(StimJob stimJob)
        {
            if (stimJob.JobInterval.PerfBallSize != null && stimJob.JobInterval.PerfBallSize.Uom != null)
            {
                var obj = _mapper.Map<PerfBallSize>(stimJob.JobInterval.PerfBallSize);
                _db.StimJobPerfBallSizes.Add(obj);
            }
        }
        private void AvgFractureWidth(StimJob stimJob)
        {
            if (stimJob.JobInterval.AvgFractureWidth != null && stimJob.JobInterval.AvgFractureWidth.Uom != null)
            {
                var obj = _mapper.Map<AvgFractureWidth>(stimJob.JobInterval.AvgFractureWidth);
                _db.StimJobAvgFractureWidths.Add(obj);
            }
        }

        private void AvgConductivity(StimJob stimJob)
        {
            if (stimJob.JobInterval.AvgConductivity != null && stimJob.JobInterval.AvgConductivity.Uom != null)
            {
                var obj = _mapper.Map<AvgConductivity>(stimJob.JobInterval.AvgConductivity);
                _db.StimJobAvgConductivitys.Add(obj);
            }
        }

        private void NetPres(StimJob stimJob)
        {
            if (stimJob.JobInterval.NetPres != null && stimJob.JobInterval.NetPres.Uom != null)
            {
                var obj = _mapper.Map<NetPres>(stimJob.JobInterval.NetPres);
                _db.StimJobNetPress.Add(obj);
            }
        }

        private void ClosurePres(StimJob stimJob)
        {
            if (stimJob.JobInterval.ClosurePres != null && stimJob.JobInterval.ClosurePres.Uom != null)
            {
                var obj = _mapper.Map<ClosurePres>(stimJob.JobInterval.ClosurePres);
                _db.StimJobClosurePress.Add(obj);
            }
        }

        private void ClosureDuration(StimJob stimJob)
        {
            if (stimJob.JobInterval.ClosureDuration != null && stimJob.JobInterval.ClosureDuration.Uom != null)
            {
                var obj = _mapper.Map<ClosureDuration>(stimJob.JobInterval.ClosureDuration);
                _db.StimJobClosureDurations.Add(obj);
            }
        }

        private void MaxTreatmentPres(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.MaxTreatmentPres != null && item.MaxTreatmentPres.Uom != null)
                {
                    var obj = _mapper.Map<MaxTreatmentPres>(item.MaxTreatmentPres);
                    _db.StimJobMaxTreatmentPress.Add(obj);
                }
            }
        }

        private void MaxSlurryRate(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.MaxSlurryRate != null && item.MaxSlurryRate.Uom != null)
                {
                    var obj = _mapper.Map<MaxSlurryRate>(item.MaxSlurryRate);
                    _db.StimJobMaxSlurryRates.Add(obj);
                }
            }
        }

        private void MaxWellheadRate(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.MaxWellheadRate != null && item.MaxWellheadRate.Uom != null)
                {
                    var obj = _mapper.Map<MaxWellheadRate>(item.MaxWellheadRate);
                    _db.StimJobMaxWellheadRates.Add(obj);
                }
            }
        }

        private void MaxN2StdRate(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.MaxN2StdRate != null && item.MaxN2StdRate.Uom != null)
                {
                    var obj = _mapper.Map<MaxN2StdRate>(item.MaxN2StdRate);
                    _db.StimJobMaxN2StdRates.Add(obj);
                }
            }
        }

        private void MaxCO2LiquidRate(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.MaxCO2LiquidRate != null && item.MaxCO2LiquidRate.Uom != null)
                {
                    var obj = _mapper.Map<MaxCO2LiquidRate>(item.MaxCO2LiquidRate);
                    _db.StimJobMaxCO2LiquidRates.Add(obj);
                }
            }
        }

        private void MaxGelRate(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.MaxGelRate != null && item.MaxGelRate.Uom != null)
                {
                    var obj = _mapper.Map<MaxGelRate>(item.MaxGelRate);
                    _db.StimJobMaxGelRates.Add(obj);
                }
            }
        }

        private void MaxOilRate(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.MaxOilRate != null && item.MaxOilRate.Uom != null)
                {
                    var obj = _mapper.Map<MaxOilRate>(item.MaxOilRate);
                    _db.StimJobMaxOilRates.Add(obj);
                }
            }
        }

        private void MaxAcidRate(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.MaxAcidRate != null && item.MaxAcidRate.Uom != null)
                {
                    var obj = _mapper.Map<MaxAcidRate>(item.MaxAcidRate);
                    _db.StimJobMaxAcidRates.Add(obj);
                }
            }
        }

        private void MaxPropConc(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.MaxPropConc != null && item.MaxPropConc.Uom != null)
                {
                    var obj = _mapper.Map<MaxPropConc>(item.MaxPropConc);
                    _db.StimJobMaxPropConcs.Add(obj);
                }
            }
        }

        private void MaxSlurryPropConc(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.MaxSlurryPropConc != null && item.MaxSlurryPropConc.Uom != null)
                {
                    var obj = _mapper.Map<MaxSlurryPropConc>(item.MaxSlurryPropConc);
                    _db.StimJobMaxSlurryPropConcs.Add(obj);
                }
            }
        }

        private void AvgTreatPres(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AvgTreatPres != null && item.AvgTreatPres.Uom != null)
                {
                    var obj = _mapper.Map<AvgTreatPres>(item.AvgTreatPres);
                    _db.StimJobAvgTreatPress.Add(obj);
                }
            }
        }

        private void AvgBaseFluidRate(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AvgBaseFluidRate != null && item.AvgBaseFluidRate.Uom != null)
                {
                    var obj = _mapper.Map<AvgBaseFluidRate>(item.AvgBaseFluidRate);
                    _db.StimJobAvgBaseFluidRates.Add(obj);
                }
            }
        }

        private void AvgSlurryRate(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AvgSlurryRate != null && item.AvgSlurryRate.Uom != null)
                {
                    var obj = _mapper.Map<AvgSlurryRate>(item.AvgSlurryRate);
                    _db.StimJobAvgSlurryRates.Add(obj);
                }
            }
        }

        private void AvgWellheadRate(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AvgWellheadRate != null && item.AvgWellheadRate.Uom != null)
                {
                    var obj = _mapper.Map<AvgWellheadRate>(item.AvgWellheadRate);
                    _db.StimJobAvgWellheadRates.Add(obj);
                }
            }
        }

        private void AvgN2StdRate(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AvgN2StdRate != null && item.AvgN2StdRate.Uom != null)
                {
                    var obj = _mapper.Map<AvgN2StdRate>(item.AvgN2StdRate);
                    _db.StimJobAvgN2StdRates.Add(obj);
                }
            }
        }

        private void AvgCO2LiquidRate(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AvgCO2LiquidRate != null && item.AvgCO2LiquidRate.Uom != null)
                {
                    var obj = _mapper.Map<AvgCO2LiquidRate>(item.AvgCO2LiquidRate);
                    _db.StimJobAvgCO2LiquidRates.Add(obj);
                }
            }
        }

        private void AvgGelRate(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AvgGelRate != null && item.AvgGelRate.Uom != null)
                {
                    var obj = _mapper.Map<AvgGelRate>(item.AvgGelRate);
                    _db.StimJobAvgGelRates.Add(obj);
                }
            }
        }

        private void AvgOilRate(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AvgOilRate != null && item.AvgOilRate.Uom != null)
                {
                    var obj = _mapper.Map<AvgOilRate>(item.AvgOilRate);
                    _db.StimJobAvgOilRates.Add(obj);
                }
            }
        }

        private void AvgAcidRate(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AvgAcidRate != null && item.AvgAcidRate.Uom != null)
                {
                    var obj = _mapper.Map<AvgAcidRate>(item.AvgAcidRate);
                    _db.StimJobAvgAcidRates.Add(obj);
                }
            }
        }

        private void AvgPropConc(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AvgPropConc != null && item.AvgPropConc.Uom != null)
                {
                    var obj = _mapper.Map<AvgPropConc>(item.AvgPropConc);
                    _db.StimJobAvgPropConcs.Add(obj);
                }
            }
        }

        private void AvgSlurryPropConc(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AvgSlurryPropConc != null && item.AvgSlurryPropConc.Uom != null)
                {
                    var obj = _mapper.Map<AvgSlurryPropConc>(item.AvgSlurryPropConc);
                    _db.StimJobAvgSlurryPropConcs.Add(obj);
                }
            }
        }

        private void AvgTemperature(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AvgTemperature != null && item.AvgTemperature.Uom != null)
                {
                    var obj = _mapper.Map<AvgTemperature>(item.AvgTemperature);
                    _db.StimJobAvgTemperatures.Add(obj);
                }
            }
        }

        private void AvgBaseFluidQuality(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AvgBaseFluidQuality != null && item.AvgBaseFluidQuality.Uom != null)
                {
                    var obj = _mapper.Map<AvgBaseFluidQuality>(item.AvgBaseFluidQuality);
                    _db.StimJobAvgBaseFluidQualitys.Add(obj);
                }
            }
        }

        private void AvgN2BaseFluidQuality(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AvgN2BaseFluidQuality != null && item.AvgN2BaseFluidQuality.Uom != null)
                {
                    var obj = _mapper.Map<AvgN2BaseFluidQuality>(item.AvgN2BaseFluidQuality);
                    _db.StimJobAvgN2BaseFluidQualitys.Add(obj);
                }
            }
        }

        private void AvgCO2BaseFluidQuality(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AvgCO2BaseFluidQuality != null && item.AvgCO2BaseFluidQuality.Uom != null)
                {
                    var obj = _mapper.Map<AvgCO2BaseFluidQuality>(item.AvgCO2BaseFluidQuality);
                    _db.StimJobAvgCO2BaseFluidQualitys.Add(obj);
                }
            }
        }

        private void AvgHydraulicPower(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AvgHydraulicPower != null && item.AvgHydraulicPower.Uom != null)
                {
                    var obj = _mapper.Map<AvgHydraulicPower>(item.AvgHydraulicPower);
                    _db.StimJobAvgHydraulicPowers.Add(obj);
                }
            }
        }

        private void BaseFluidVol(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.BaseFluidVol != null && item.BaseFluidVol.Uom != null)
                {
                    var obj = _mapper.Map<BaseFluidVol>(item.BaseFluidVol);
                    _db.StimJobBaseFluidVols.Add(obj);
                }
            }
        }

        private void SlurryVol(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.SlurryVol != null && item.SlurryVol.Uom != null)
                {
                    var obj = _mapper.Map<SlurryVol>(item.SlurryVol);
                    _db.StimJobSlurryVols.Add(obj);
                }
            }
        }

        private void WellheadVol(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.WellheadVol != null && item.WellheadVol.Uom != null)
                {
                    var obj = _mapper.Map<WellheadVol>(item.WellheadVol);
                    _db.StimJobWellheadVols.Add(obj);
                }
            }
        }

        private void StdVolN2(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.StdVolN2 != null && item.StdVolN2.Uom != null)
                {
                    var obj = _mapper.Map<StdVolN2>(item.StdVolN2);
                    _db.StimJobStdVolN2s.Add(obj);
                }
            }
        }

        private void MassCO2(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.MassCO2 != null && item.MassCO2.Uom != null)
                {
                    var obj = _mapper.Map<MassCO2>(item.MassCO2);
                    _db.StimJobMassCO2s.Add(obj);
                }
            }
        }

        private void GelVol(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.GelVol != null && item.GelVol.Uom != null)
                {
                    var obj = _mapper.Map<GelVol>(item.GelVol);
                    _db.StimJobGelVols.Add(obj);
                }
            }
        }

        private void OilVol(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.OilVol != null && item.OilVol.Uom != null)
                {
                    var obj = _mapper.Map<OilVol>(item.OilVol);
                    _db.StimJobOilVols.Add(obj);
                }
            }
        }

        private void AcidVol(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AcidVol != null && item.AcidVol.Uom != null)
                {
                    var obj = _mapper.Map<AcidVol>(item.AcidVol);
                    _db.StimJobAcidVols.Add(obj);
                }
            }
        }

        private void BaseFluidBypassVol(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.BaseFluidBypassVol != null && item.BaseFluidBypassVol.Uom != null)
                {
                    var obj = _mapper.Map<BaseFluidBypassVol>(item.BaseFluidBypassVol);
                    _db.StimJobBaseFluidBypassVols.Add(obj);
                }
            }
        }

        private void PropMass(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.PropMass != null && item.PropMass.Uom != null)
                {
                    var obj = _mapper.Map<PropMass>(item.PropMass);
                    _db.StimJobPropMasss.Add(obj);
                }
            }
        }

        private void MaxPmaxPacPres(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.MaxPmaxPacPres != null && item.MaxPmaxPacPres.Uom != null)
                {
                    var obj = _mapper.Map<MaxPmaxPacPres>(item.MaxPmaxPacPres);
                    _db.StimJobMaxPmaxPacPress.Add(obj);
                }
            }
        }

        private void MaxPmaxWeaklinkPres(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.MaxPmaxWeaklinkPres != null && item.MaxPmaxWeaklinkPres.Uom != null)
                {
                    var obj = _mapper.Map<MaxPmaxWeaklinkPres>(item.MaxPmaxWeaklinkPres);
                    _db.StimJobMaxPmaxWeaklinkPress.Add(obj);
                }
            }
        }

        private void AvgPmaxPacPres(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AvgPmaxPacPres != null && item.AvgPmaxPacPres.Uom != null)
                {
                    var obj = _mapper.Map<AvgPmaxPacPres>(item.AvgPmaxPacPres);
                    _db.StimJobAvgPmaxPacPress.Add(obj);
                }
            }
        }

        private void AvgPmaxWeaklinkPres(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AvgPmaxWeaklinkPres != null && item.AvgPmaxWeaklinkPres.Uom != null)
                {
                    var obj = _mapper.Map<AvgPmaxWeaklinkPres>(item.AvgPmaxWeaklinkPres);
                    _db.StimJobAvgPmaxWeaklinkPress.Add(obj);
                }
            }
        }

        private void ShutinPres5Min(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.ShutinPres5Min != null && item.ShutinPres5Min.Uom != null)
                {
                    var obj = _mapper.Map<ShutinPres5Min>(item.ShutinPres5Min);
                    _db.StimJobShutinPres5Mins.Add(obj);
                }
            }
        }

        private void ShutinPres10Min(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.ShutinPres10Min != null && item.ShutinPres10Min.Uom != null)
                {
                    var obj = _mapper.Map<ShutinPres10Min>(item.ShutinPres10Min);
                    _db.StimJobShutinPres10Mins.Add(obj);
                }
            }
        }

        private void ShutinPres15Min(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.ShutinPres15Min != null && item.ShutinPres15Min.Uom != null)
                {
                    var obj = _mapper.Map<ShutinPres15Min>(item.ShutinPres15Min);
                    _db.StimJobShutinPres15Mins.Add(obj);
                }
            }
        }

        private void PercentPad(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.PercentPad != null && item.PercentPad.Uom != null)
                {
                    var obj = _mapper.Map<PercentPad>(item.PercentPad);
                    _db.StimJobPercentPads.Add(obj);
                }
            }
        }

        private void Od(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.Tubular)
                {
                    if (subItem != null && subItem.Od != null && subItem.Od.Uom != null)
                    {
                        var obj = _mapper.Map<StimJobOd>(subItem.Od);
                        _db.StimJobOds.Add(obj);
                    }
                }
            }
        }

        private void Weight(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.Tubular)
                {
                    if (subItem != null && subItem.Weight != null && subItem.Weight.Uom != null)
                    {
                        var obj = _mapper.Map<StimJobWeight>(subItem.Weight);
                        _db.StimJobWeights.Add(obj);
                    }
                }
            }

            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.StageFluid.Proppant.Weight != null && subItem.StageFluid.Proppant.Weight.Uom != null)
                    {
                        var obj = _mapper.Map<StimJobWeight>(subItem.StageFluid.Proppant.Weight);
                        _db.StimJobWeights.Add(obj);
                    }
                }
            }

        }

        private void MdTop(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.Tubular)
                {
                    if (subItem != null && subItem.MdTop != null && subItem.MdTop.Uom != null)
                    {
                        var obj = _mapper.Map<StimJobMdTop>(subItem.MdTop);
                        _db.StimJobMdTops.Add(obj);
                    }
                }
            }
        }

        private void Id(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.Tubular)
                {
                    if (subItem != null && subItem.Id != null && subItem.Id.Uom != null)
                    {
                        var obj = _mapper.Map<StimJobId>(subItem.Id);
                        _db.StimJobIds.Add(obj);
                    }
                }
            }
        }

        private void MdBottom(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.Tubular)
                {
                    if (subItem != null && subItem.MdBottom != null && subItem.MdBottom.Uom != null)
                    {
                        var obj = _mapper.Map<StimJobMdBottom>(subItem.MdBottom);
                        _db.StimJobMdBottoms.Add(obj);
                    }
                }
            }
        }

        private void VolumeFactor(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.Tubular)
                {
                    if (subItem != null && subItem.VolumeFactor != null && subItem.VolumeFactor.Uom != null)
                    {
                        var obj = _mapper.Map<StimJobVolumeFactor>(subItem.VolumeFactor);
                        _db.StimJobVolumeFactors.Add(obj);
                    }
                }
            }
        }

        private void Tubular(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.Tubular)
                {
                    if (subItem != null && subItem.Uid != null)
                    {
                        var obj = _mapper.Map<StimJobTubular>(subItem);
                        _db.StimJobTubulars.Add(obj);
                    }
                }
            }
        }

        private void PumpTime(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.PumpTime != null && subItem.PumpTime.Uom != null)
                    {
                        var obj = _mapper.Map<PumpTime>(subItem.PumpTime);
                        _db.StimJobPumpTimes.Add(obj);
                    }
                }
            }
        }

        private void StartRateSurfaceLiquid(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.StartRateSurfaceLiquid != null && subItem.StartRateSurfaceLiquid.Uom != null)
                    {
                        var obj = _mapper.Map<StartRateSurfaceLiquid>(subItem.StartRateSurfaceLiquid);
                        _db.StimJobStartRateSurfaceLiquids.Add(obj);
                    }
                }
            }
        }

        private void EndRateSurfaceLiquid(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.EndRateSurfaceLiquid != null && subItem.EndRateSurfaceLiquid.Uom != null)
                    {
                        var obj = _mapper.Map<EndRateSurfaceLiquid>(subItem.EndRateSurfaceLiquid);
                        _db.StimJobEndRateSurfaceLiquids.Add(obj);
                    }
                }
            }
        }

        private void AvgRateSurfaceLiquid(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.AvgRateSurfaceLiquid != null && subItem.AvgRateSurfaceLiquid.Uom != null)
                    {
                        var obj = _mapper.Map<AvgRateSurfaceLiquid>(subItem.AvgRateSurfaceLiquid);
                        _db.StimJobAvgRateSurfaceLiquids.Add(obj);
                    }
                }
            }
        }

        private void StartRateSurfaceCO2(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.StartRateSurfaceCO2 != null && subItem.StartRateSurfaceCO2.Uom != null)
                    {
                        var obj = _mapper.Map<StartRateSurfaceCO2>(subItem.StartRateSurfaceCO2);
                        _db.StimJobStartRateSurfaceCO2s.Add(obj);
                    }
                }
            }
        }

        private void EndRateSurfaceCO2(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.EndRateSurfaceCO2 != null && subItem.EndRateSurfaceCO2.Uom != null)
                    {
                        var obj = _mapper.Map<EndRateSurfaceCO2>(subItem.EndRateSurfaceCO2);
                        _db.StimJobEndRateSurfaceCO2s.Add(obj);
                    }
                }
            }
        }

        private void AvgRateSurfaceCO2(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.AvgRateSurfaceCO2 != null && subItem.AvgRateSurfaceCO2.Uom != null)
                    {
                        var obj = _mapper.Map<AvgRateSurfaceCO2>(subItem.AvgRateSurfaceCO2);
                        _db.StimJobAvgRateSurfaceCO2s.Add(obj);
                    }
                }
            }
        }

        private void StartStdRateSurfaceN2(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.StartStdRateSurfaceN2 != null && subItem.StartStdRateSurfaceN2.Uom != null)
                    {
                        var obj = _mapper.Map<StartStdRateSurfaceN2>(subItem.StartStdRateSurfaceN2);
                        _db.StimJobStartStdRateSurfaceN2s.Add(obj);
                    }
                }
            }
        }

        private void EndStdRateSurfaceN2(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.EndStdRateSurfaceN2 != null && subItem.EndStdRateSurfaceN2.Uom != null)
                    {
                        var obj = _mapper.Map<EndStdRateSurfaceN2>(subItem.EndStdRateSurfaceN2);
                        _db.StimJobEndStdRateSurfaceN2s.Add(obj);
                    }
                }
            }
        }

        private void AvgStdRateSurfaceN2(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.AvgStdRateSurfaceN2 != null && subItem.AvgStdRateSurfaceN2.Uom != null)
                    {
                        var obj = _mapper.Map<AvgStdRateSurfaceN2>(subItem.AvgStdRateSurfaceN2);
                        _db.StimJobAvgStdRateSurfaceN2s.Add(obj);
                    }
                }
            }
        }

        private void StartPresSurface(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.StartPresSurface != null && subItem.StartPresSurface.Uom != null)
                    {
                        var obj = _mapper.Map<StartPresSurface>(subItem.StartPresSurface);
                        _db.StimJobStartPresSurfaces.Add(obj);
                    }
                }
            }
        }

        private void EndPresSurface(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.EndPresSurface != null && subItem.EndPresSurface.Uom != null)
                    {
                        var obj = _mapper.Map<EndPresSurface>(subItem.EndPresSurface);
                        _db.StimJobEndPresSurfaces.Add(obj);
                    }
                }
            }
        }

        private void AveragePresSurface(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.AveragePresSurface != null && subItem.AveragePresSurface.Uom != null)
                    {
                        var obj = _mapper.Map<AveragePresSurface>(subItem.AveragePresSurface);
                        _db.StimJobAveragePresSurfaces.Add(obj);
                    }
                }
            }
        }

        private void StartPumpRateBottomhole(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.StartPumpRateBottomhole != null && subItem.StartPumpRateBottomhole.Uom != null)
                    {
                        var obj = _mapper.Map<StartPumpRateBottomhole>(subItem.StartPumpRateBottomhole);
                        _db.StimJobStartPumpRateBottomholes.Add(obj);
                    }
                }
            }
        }

        private void EndPumpRateBottomhole(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.EndPumpRateBottomhole != null && subItem.EndPumpRateBottomhole.Uom != null)
                    {
                        var obj = _mapper.Map<EndPumpRateBottomhole>(subItem.EndPumpRateBottomhole);
                        _db.StimJobEndPumpRateBottomholes.Add(obj);
                    }
                }
            }
        }

        private void AvgPumpRateBottomhole(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.AvgPumpRateBottomhole != null && subItem.AvgPumpRateBottomhole.Uom != null)
                    {
                        var obj = _mapper.Map<AvgPumpRateBottomhole>(subItem.AvgPumpRateBottomhole);
                        _db.StimJobAvgPumpRateBottomholes.Add(obj);
                    }
                }
            }
        }

        private void StartPresBottomhole(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.StartPresBottomhole != null && subItem.StartPresBottomhole.Uom != null)
                    {
                        var obj = _mapper.Map<StartPresBottomhole>(subItem.StartPresBottomhole);
                        _db.StimJobStartPresBottomholes.Add(obj);
                    }
                }
            }
        }

        private void EndPresBottomhole(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.EndPresBottomhole != null && subItem.EndPresBottomhole.Uom != null)
                    {
                        var obj = _mapper.Map<EndPresBottomhole>(subItem.EndPresBottomhole);
                        _db.StimJobEndPresBottomholes.Add(obj);
                    }
                }
            }
        }

        private void AveragePresBottomhole(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.AveragePresBottomhole != null && subItem.AveragePresBottomhole.Uom != null)
                    {
                        var obj = _mapper.Map<AveragePresBottomhole>(subItem.AveragePresBottomhole);
                        _db.StimJobAveragePresBottomholes.Add(obj);
                    }
                }
            }
        }

        private void StartProppantConcSurface(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.StartProppantConcSurface != null && subItem.StartProppantConcSurface.Uom != null)
                    {
                        var obj = _mapper.Map<StartProppantConcSurface>(subItem.StartProppantConcSurface);
                        _db.StimJobStartProppantConcSurfaces.Add(obj);
                    }
                }
            }
        }

        private void EndProppantConcSurface(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.EndProppantConcSurface != null && subItem.EndProppantConcSurface.Uom != null)
                    {
                        var obj = _mapper.Map<EndProppantConcSurface>(subItem.EndProppantConcSurface);
                        _db.StimJobEndProppantConcSurfaces.Add(obj);
                    }
                }
            }
        }

        private void StartProppantConcBottomhole(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.StartProppantConcBottomhole != null && subItem.StartProppantConcBottomhole.Uom != null)
                    {
                        var obj = _mapper.Map<StartProppantConcBottomhole>(subItem.StartProppantConcBottomhole);
                        _db.StimJobStartProppantConcBottomholes.Add(obj);
                    }
                }
            }
        }

        private void EndProppantConcBottomhole(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.EndProppantConcBottomhole != null && subItem.EndProppantConcBottomhole.Uom != null)
                    {
                        var obj = _mapper.Map<EndProppantConcBottomhole>(subItem.EndProppantConcBottomhole);
                        _db.StimJobEndProppantConcBottomholes.Add(obj);
                    }
                }
            }
        }

        private void StartFoamRateN2(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.StartFoamRateN2 != null && subItem.StartFoamRateN2.Uom != null)
                    {
                        var obj = _mapper.Map<StartFoamRateN2>(subItem.StartFoamRateN2);
                        _db.StimJobStartFoamRateN2s.Add(obj);
                    }
                }
            }
        }

        private void EndFoamRateN2(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.EndFoamRateN2 != null && subItem.EndFoamRateN2.Uom != null)
                    {
                        var obj = _mapper.Map<EndFoamRateN2>(subItem.EndFoamRateN2);
                        _db.StimJobEndFoamRateN2s.Add(obj);
                    }
                }
            }
        }

        private void StartFoamRateCO2(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.StartFoamRateCO2 != null && subItem.StartFoamRateCO2.Uom != null)
                    {
                        var obj = _mapper.Map<StartFoamRateCO2>(subItem.StartFoamRateCO2);
                        _db.StimJobStartFoamRateCO2s.Add(obj);
                    }
                }
            }
        }

        private void EndFoamRateCO2(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.EndFoamRateCO2 != null && subItem.EndFoamRateCO2.Uom != null)
                    {
                        var obj = _mapper.Map<EndFoamRateCO2>(subItem.EndFoamRateCO2);
                        _db.StimJobEndFoamRateCO2s.Add(obj);
                    }
                }
            }
        }

        private void FluidVolBase(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.FluidVolBase != null && subItem.FluidVolBase.Uom != null)
                    {
                        var obj = _mapper.Map<FluidVolBase>(subItem.FluidVolBase);
                        _db.StimJobFluidVolBases.Add(obj);
                    }
                }
            }
        }

        private void FluidVolSlurry(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.FluidVolSlurry != null && subItem.FluidVolSlurry.Uom != null)
                    {
                        var obj = _mapper.Map<FluidVolSlurry>(subItem.FluidVolSlurry);
                        _db.StimJobFluidVolSlurrys.Add(obj);
                    }
                }
            }
        }

        private void SlurryRateBegin(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.SlurryRateBegin != null && subItem.SlurryRateBegin.Uom != null)
                    {
                        var obj = _mapper.Map<SlurryRateBegin>(subItem.SlurryRateBegin);
                        _db.StimJobSlurryRateBegins.Add(obj);
                    }
                }
            }
        }

        private void SlurryRateEnd(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.SlurryRateEnd != null && subItem.SlurryRateEnd.Uom != null)
                    {
                        var obj = _mapper.Map<SlurryRateEnd>(subItem.SlurryRateEnd);
                        _db.StimJobSlurryRateEnds.Add(obj);
                    }
                }
            }
        }

        private void ProppantMassWellHead(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.ProppantMassWellHead != null && subItem.ProppantMassWellHead.Uom != null)
                    {
                        var obj = _mapper.Map<ProppantMassWellHead>(subItem.ProppantMassWellHead);
                        _db.StimJobProppantMassWellHeads.Add(obj);
                    }
                }
            }
        }

        private void ProppantMass(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.ProppantMass != null && subItem.ProppantMass.Uom != null)
                    {
                        var obj = _mapper.Map<ProppantMass>(subItem.ProppantMass);
                        _db.StimJobProppantMasss.Add(obj);
                    }
                }
            }
        }

        private void MaxPres(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.MaxPres != null && subItem.MaxPres.Uom != null)
                    {
                        var obj = _mapper.Map<MaxPres>(subItem.MaxPres);
                        _db.StimJobMaxPress.Add(obj);
                    }
                }
            }
        }

        private void AvgInternalPhaseFraction(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.AvgInternalPhaseFraction != null && subItem.AvgInternalPhaseFraction.Uom != null)
                    {
                        var obj = _mapper.Map<AvgInternalPhaseFraction>(subItem.AvgInternalPhaseFraction);
                        _db.StimJobAvgInternalPhaseFractions.Add(obj);
                    }
                }
            }
        }

        private void AvgCO2Rate(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.AvgCO2Rate != null && subItem.AvgCO2Rate.Uom != null)
                    {
                        var obj = _mapper.Map<AvgCO2Rate>(subItem.AvgCO2Rate);
                        _db.StimJobAvgCO2Rates.Add(obj);
                    }
                }
            }
        }

        private void GelVolume(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.GelVolume != null && subItem.GelVolume.Uom != null)
                    {
                        var obj = _mapper.Map<GelVolume>(subItem.GelVolume);
                        _db.StimJobGelVolumes.Add(obj);
                    }
                }
            }
        }

        private void OilVolume(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.OilVolume != null && subItem.OilVolume.Uom != null)
                    {
                        var obj = _mapper.Map<OilVolume>(subItem.OilVolume);
                        _db.StimJobOilVolumes.Add(obj);
                    }
                }
            }
        }

        private void AcidVolume(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.AcidVolume != null && subItem.AcidVolume.Uom != null)
                    {
                        var obj = _mapper.Map<AcidVolume>(subItem.AcidVolume);
                        _db.StimJobAcidVolumes.Add(obj);
                    }
                }
            }
        }

        private void FluidVol(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.StageFluid.FluidVol != null && subItem.StageFluid.FluidVol.Uom != null)
                    {
                        var obj = _mapper.Map<FluidVol>(subItem.StageFluid.FluidVol);
                        _db.StimJobFluidVols.Add(obj);
                    }
                }
            }
        }

        private void Volume(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    foreach (var childitem in subItem.StageFluid.Additive)
                    {
                        if (childitem.Volume != null && childitem.Volume.Uom != null)
                        {
                            var obj = _mapper.Map<Volume>(childitem.Volume);
                            _db.StimJobVolumes.Add(obj);
                        }
                    }
                }
            }
        }

        private void Additive(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    foreach (var childitem in subItem.StageFluid.Additive)
                    {
                        if (childitem != null && childitem.Uid != null)
                        {
                            var obj = _mapper.Map<StimJobAdditive>(childitem);
                            _db.StimJobAdditives.Add(obj);
                        }
                    }
                }
            }
        }

        private void Proppant(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {

                    if (subItem.StageFluid.Proppant != null)
                    {
                        var obj = _mapper.Map<Proppant>(subItem.StageFluid.Proppant);
                        _db.StimJobProppants.Add(obj);
                    }
                }
            }
        }

        private void StageFluid(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {

                    if (subItem.StageFluid != null)
                    {
                        var obj = _mapper.Map<StageFluid>(subItem.StageFluid);
                        _db.StimJobStageFluids.Add(obj);
                    }
                }
            }
        }

        private void JobStage(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {

                    if (subItem != null &&  subItem.Uid != null)
                    {
                        var obj = _mapper.Map<JobStage>(subItem);
                        _db.StimJobJobStages.Add(obj);
                    }
                }
            }
        }

        private void JobEvent(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobEvent)
                {

                    if (subItem != null && subItem.Uid != null)
                    {
                        var obj = _mapper.Map<JobEvent>(subItem);
                        _db.StimJobJobEvents.Add(obj);
                    }
                }
            }
        }

        private void FlowPath(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                if (item != null && item.Uid != null)
                {
                    var obj = _mapper.Map<FlowPath>(item);
                    _db.StimJobFlowPaths.Add(obj);
                }
            }
        }

        private void PumpDuration(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.PumpDuration != null && stimJob.JobInterval.PdatSession.PumpDuration.Uom != null)
            {
                var obj = _mapper.Map<PumpDuration>(stimJob.JobInterval.PdatSession.PumpDuration);
                _db.StimJobPumpDurations.Add(obj);
            }
        }

        private void AvgBottomholeTreatmentPres(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.AvgBottomholeTreatmentPres != null && stimJob.JobInterval.PdatSession.AvgBottomholeTreatmentPres.Uom != null)
            {
                var obj = _mapper.Map<AvgBottomholeTreatmentPres>(stimJob.JobInterval.PdatSession.AvgBottomholeTreatmentPres);
                _db.StimJobAvgBottomholeTreatmentPress.Add(obj);
            }
        }
        private void BottomholeHydrostaticPres(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.BottomholeHydrostaticPres != null && stimJob.JobInterval.PdatSession.BottomholeHydrostaticPres.Uom != null)
            {
                var obj = _mapper.Map<BottomholeHydrostaticPres>(stimJob.JobInterval.PdatSession.BottomholeHydrostaticPres);
                _db.StimJobBottomholeHydrostaticPress.Add(obj);
            }
        }

        private void BubblePointPres(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.BubblePointPres != null && stimJob.JobInterval.PdatSession.BubblePointPres.Uom != null)
            {
                var obj = _mapper.Map<BubblePointPres>(stimJob.JobInterval.PdatSession.BubblePointPres);
                _db.StimJobBubblePointPress.Add(obj);
            }
        }

        private void FrictionPres(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.FrictionPres != null && stimJob.JobInterval.PdatSession.FrictionPres.Uom != null)
            {
                var obj = _mapper.Map<FrictionPres>(stimJob.JobInterval.PdatSession.FrictionPres);
                _db.StimJobFrictionPress.Add(obj);
            }
        }

        private void PorePres(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.PorePres != null && stimJob.JobInterval.PdatSession.PorePres.Uom != null)
            {
                var obj = _mapper.Map<PorePres>(stimJob.JobInterval.PdatSession.PorePres);
                _db.StimJobPorePress.Add(obj);
            }
        }


        private void AvgBottomholeTreatmentRate(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.AvgBottomholeTreatmentRate != null && stimJob.JobInterval.PdatSession.AvgBottomholeTreatmentRate.Uom != null)
            {
                var obj = _mapper.Map<AvgBottomholeTreatmentRate>(stimJob.JobInterval.PdatSession.AvgBottomholeTreatmentRate);
                _db.StimJobAvgBottomholeTreatmentRates.Add(obj);
            }
        }

        private void FluidDensity(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.FluidDensity != null && stimJob.JobInterval.PdatSession.FluidDensity.Uom != null)
            {
                var obj = _mapper.Map<FluidDensity>(stimJob.JobInterval.PdatSession.FluidDensity);
                _db.StimJobFluidDensitys.Add(obj);
            }
        }

        private void WellboreVolume(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.WellboreVolume != null && stimJob.JobInterval.PdatSession.WellboreVolume.Uom != null)
            {
                var obj = _mapper.Map<WellboreVolume>(stimJob.JobInterval.PdatSession.WellboreVolume);
                _db.StimJobWellboreVolumes.Add(obj);
            }
        }

        private void MdSurface(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.MdSurface != null && stimJob.JobInterval.PdatSession.MdSurface.Uom != null)
            {
                var obj = _mapper.Map<MdSurface>(stimJob.JobInterval.PdatSession.MdSurface);
                _db.StimJobMdSurfaces.Add(obj);
            }
        }

        private void MdBottomhole(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.MdBottomhole != null && stimJob.JobInterval.PdatSession.MdBottomhole.Uom != null)
            {
                var obj = _mapper.Map<MdBottomhole>(stimJob.JobInterval.PdatSession.MdBottomhole);
                _db.StimJobMdBottomholes.Add(obj);
            }
        }

        private void MdMidPerforation(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.MdMidPerforation != null && stimJob.JobInterval.PdatSession.MdMidPerforation.Uom != null)
            {
                var obj = _mapper.Map<MdMidPerforation>(stimJob.JobInterval.PdatSession.MdMidPerforation);
                _db.StimJobMdMidPerforations.Add(obj);
            }
        }

        private void TvdMidPerforation(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.TvdMidPerforation != null && stimJob.JobInterval.PdatSession.TvdMidPerforation.Uom != null)
            {
                var obj = _mapper.Map<TvdMidPerforation>(stimJob.JobInterval.PdatSession.TvdMidPerforation);
                _db.StimJobTvdMidPerforations.Add(obj);
            }
        }

        private void SurfaceTemperature(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.SurfaceTemperature != null && stimJob.JobInterval.PdatSession.SurfaceTemperature.Uom != null)
            {
                var obj = _mapper.Map<SurfaceTemperature>(stimJob.JobInterval.PdatSession.SurfaceTemperature);
                _db.StimJobSurfaceTemperatures.Add(obj);
            }
        }

        private void BottomholeTemperature(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.BottomholeTemperature != null && stimJob.JobInterval.PdatSession.BottomholeTemperature.Uom != null)
            {
                var obj = _mapper.Map<BottomholeTemperature>(stimJob.JobInterval.PdatSession.BottomholeTemperature);
                _db.StimJobBottomholeTemperatures.Add(obj);
            }
        }

        private void SurfaceFluidTemperature(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.SurfaceFluidTemperature != null && stimJob.JobInterval.PdatSession.SurfaceFluidTemperature.Uom != null)
            {
                var obj = _mapper.Map<SurfaceFluidTemperature>(stimJob.JobInterval.PdatSession.SurfaceFluidTemperature);
                _db.StimJobSurfaceFluidTemperatures.Add(obj);
            }
        }

        private void FluidCompressibility(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.FluidCompressibility != null && stimJob.JobInterval.PdatSession.FluidCompressibility.Uom != null)
            {
                var obj = _mapper.Map<FluidCompressibility>(stimJob.JobInterval.PdatSession.FluidCompressibility);
                _db.StimJobFluidCompressibilitys.Add(obj);
            }
        }

        private void ReservoirTotalCompressibility(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.ReservoirTotalCompressibility != null && stimJob.JobInterval.PdatSession.ReservoirTotalCompressibility.Uom != null)
            {
                var obj = _mapper.Map<ReservoirTotalCompressibility>(stimJob.JobInterval.PdatSession.ReservoirTotalCompressibility);
                _db.StimJobReservoirTotalCompressibilitys.Add(obj);
            }
        }

        private void FluidSpecificHeat(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.FluidSpecificHeat != null && stimJob.JobInterval.PdatSession.FluidSpecificHeat.Uom != null)
            {
                var obj = _mapper.Map<FluidSpecificHeat>(stimJob.JobInterval.PdatSession.FluidSpecificHeat);
                _db.StimJobFluidSpecificHeats.Add(obj);
            }
        }

        private void FluidThermalConductivity(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.FluidThermalConductivity != null && stimJob.JobInterval.PdatSession.FluidThermalConductivity.Uom != null)
            {
                var obj = _mapper.Map<FluidThermalConductivity>(stimJob.JobInterval.PdatSession.FluidThermalConductivity);
                _db.StimJobFluidThermalConductivitys.Add(obj);
            }
        }

        private void FluidThermalExpansionCoefficient(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.FluidThermalExpansionCoefficient != null && stimJob.JobInterval.PdatSession.FluidThermalExpansionCoefficient.Uom != null)
            {
                var obj = _mapper.Map<FluidThermalExpansionCoefficient>(stimJob.JobInterval.PdatSession.FluidThermalExpansionCoefficient);
                _db.StimJobFluidThermalExpansionCoefficients.Add(obj);
            }
        }

        private void FoamQuality(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.FoamQuality != null && stimJob.JobInterval.PdatSession.FoamQuality.Uom != null)
            {
                var obj = _mapper.Map<FoamQuality>(stimJob.JobInterval.PdatSession.FoamQuality);
                _db.StimJobFoamQualitys.Add(obj);
            }
        }

        private void BottomholeRate(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.PdatSession.StepRateTest.PresMeasurement)
            {

                if (item.BottomholeRate != null && item.BottomholeRate.Uom != null)
                {
                    var obj = _mapper.Map<BottomholeRate>(item.BottomholeRate);
                    _db.StimJobBottomholeRates.Add(obj);
                }
            }
        }

        private void PresMeasurement(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.PdatSession.StepRateTest.PresMeasurement)
            {

                if (item != null)
                {
                    var obj = _mapper.Map<PresMeasurement>(item);
                    _db.StimJobPresMeasurements.Add(obj);
                }
            }
        }

        private void FractureExtensionPres(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.StepRateTest.FractureExtensionPres != null && stimJob.JobInterval.PdatSession.StepRateTest.FractureExtensionPres.Uom != null)
            {
                var obj = _mapper.Map<FractureExtensionPres>(stimJob.JobInterval.PdatSession.StepRateTest.FractureExtensionPres);
                _db.StimJobFractureExtensionPress.Add(obj);
            }

        }

        private void StepRateTest(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.StepRateTest != null && stimJob.JobInterval.PdatSession.StepRateTest.Uid != null)
            {
                var obj = _mapper.Map<StepRateTest>(stimJob.JobInterval.PdatSession.StepRateTest);
                _db.StimJobStepRateTests.Add(obj);
            }

        }
        private void EndPdlDuration(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.FluidEfficiencyTest.EndPdlDuration != null && stimJob.JobInterval.PdatSession.FluidEfficiencyTest.EndPdlDuration.Uom != null)
            {
                var obj = _mapper.Map<EndPdlDuration>(stimJob.JobInterval.PdatSession.FluidEfficiencyTest.EndPdlDuration);
                _db.StimJobEndPdlDurations.Add(obj);
            }
        }

        private void FractureCloseDuration(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.FluidEfficiencyTest.FractureCloseDuration != null && stimJob.JobInterval.PdatSession.FluidEfficiencyTest.FractureCloseDuration.Uom != null)
            {
                var obj = _mapper.Map<FractureCloseDuration>(stimJob.JobInterval.PdatSession.FluidEfficiencyTest.FractureCloseDuration);
                _db.StimJobFractureCloseDurations.Add(obj);
            }

            if (stimJob.JobInterval.PdatSession.PumpFlowBackTest.FractureCloseDuration != null && stimJob.JobInterval.PdatSession.PumpFlowBackTest.FractureCloseDuration.Uom != null)
            {
                var obj = _mapper.Map<FractureCloseDuration>(stimJob.JobInterval.PdatSession.PumpFlowBackTest.FractureCloseDuration);
                _db.StimJobFractureCloseDurations.Add(obj);
            }

        }

        private void FractureClosePres(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.FluidEfficiencyTest.FractureClosePres != null && stimJob.JobInterval.PdatSession.FluidEfficiencyTest.FractureClosePres.Uom != null)
            {
                var obj = _mapper.Map<FractureClosePres>(stimJob.JobInterval.PdatSession.FluidEfficiencyTest.FractureClosePres);
                _db.StimJobFractureClosePress.Add(obj);
            }

            if (stimJob.JobInterval.PdatSession.PumpFlowBackTest.FractureClosePres != null && stimJob.JobInterval.PdatSession.PumpFlowBackTest.FractureClosePres.Uom != null)
            {
                var obj = _mapper.Map<FractureClosePres>(stimJob.JobInterval.PdatSession.PumpFlowBackTest.FractureClosePres);
                _db.StimJobFractureClosePress.Add(obj);
            }

        }

        private void PseudoRadialPres(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.FluidEfficiencyTest.PseudoRadialPres != null && stimJob.JobInterval.PdatSession.FluidEfficiencyTest.PseudoRadialPres.Uom != null)
            {
                var obj = _mapper.Map<PseudoRadialPres>(stimJob.JobInterval.PdatSession.FluidEfficiencyTest.PseudoRadialPres);
                _db.StimJobPseudoRadialPress.Add(obj);
            }
        }

        private void FractureLength(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.FluidEfficiencyTest.FractureLength != null && stimJob.JobInterval.PdatSession.FluidEfficiencyTest.FractureLength.Uom != null)
            {
                var obj = _mapper.Map<FractureLength>(stimJob.JobInterval.PdatSession.FluidEfficiencyTest.FractureLength);
                _db.StimJobFractureLengths.Add(obj);
            }
        }

        private void FractureWidth(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.FluidEfficiencyTest.FractureWidth != null && stimJob.JobInterval.PdatSession.FluidEfficiencyTest.FractureWidth.Uom != null)
            {
                var obj = _mapper.Map<FractureWidth>(stimJob.JobInterval.PdatSession.FluidEfficiencyTest.FractureWidth);
                _db.StimJobFractureWidths.Add(obj);
            }
        }

        private void ResidualPermeability(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.FluidEfficiencyTest.ResidualPermeability != null && stimJob.JobInterval.PdatSession.FluidEfficiencyTest.ResidualPermeability.Uom != null)
            {
                var obj = _mapper.Map<ResidualPermeability>(stimJob.JobInterval.PdatSession.FluidEfficiencyTest.ResidualPermeability);
                _db.StimJobResidualPermeabilitys.Add(obj);
            }
        }

        private void FluidEfficiencyTest(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.FluidEfficiencyTest != null && stimJob.JobInterval.PdatSession.FluidEfficiencyTest.Uid != null)
            {
                var obj = _mapper.Map<FluidEfficiencyTest>(stimJob.JobInterval.PdatSession.FluidEfficiencyTest);
                _db.StimJobFluidEfficiencyTests.Add(obj);
            }
        }

        private void PumpFlowBackTest(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.PumpFlowBackTest != null && stimJob.JobInterval.PdatSession.PumpFlowBackTest.Uid != null)
            {
                var obj = _mapper.Map<PumpFlowBackTest>(stimJob.JobInterval.PdatSession.PumpFlowBackTest);
                _db.StimJobPumpFlowBackTests.Add(obj);
            }
        }


        private void BottomholeFluidDensity(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.StepDownTest.BottomholeFluidDensity != null && stimJob.JobInterval.PdatSession.StepDownTest.BottomholeFluidDensity.Uom != null)
            {
                var obj = _mapper.Map<BottomholeFluidDensity>(stimJob.JobInterval.PdatSession.StepDownTest.BottomholeFluidDensity);
                _db.StimJobBottomholeFluidDensitys.Add(obj);
            }

            if (stimJob.JobInterval.PdatSession.StepDownTest != null && stimJob.JobInterval.PdatSession.StepDownTest.BottomholeFluidDensity.Uom != null)
            {
                var obj = _mapper.Map<BottomholeFluidDensity>(stimJob.JobInterval.PdatSession.StepDownTest.BottomholeFluidDensity);
                _db.StimJobBottomholeFluidDensitys.Add(obj);
            }
        }

        private void DiameterEntryHole(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.StepDownTest.DiameterEntryHole != null && stimJob.JobInterval.PdatSession.StepDownTest.DiameterEntryHole.Uom != null)
            {
                var obj = _mapper.Map<DiameterEntryHole>(stimJob.JobInterval.PdatSession.StepDownTest.DiameterEntryHole);
                _db.StimJobDiameterEntryHoles.Add(obj);
            }

            if (stimJob.JobInterval.PdatSession.StepDownTest != null && stimJob.JobInterval.PdatSession.StepDownTest.DiameterEntryHole.Uom != null)
            {
                var obj = _mapper.Map<DiameterEntryHole>(stimJob.JobInterval.PdatSession.StepDownTest.DiameterEntryHole);
                _db.StimJobDiameterEntryHoles.Add(obj);
            }


        }

        private void PipeFriction(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.PdatSession.StepDownTest.Step)
            {

                if (item.PipeFriction != null && item.PipeFriction.Uom != null)
                {
                    var obj = _mapper.Map<PipeFriction>(item.PipeFriction);
                    _db.StimJobPipeFrictions.Add(obj);
                }
            }
        }

        private void EntryFriction(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.PdatSession.StepDownTest.Step)
            {

                if (item.EntryFriction != null && item.EntryFriction.Uom != null)
                {
                    var obj = _mapper.Map<EntryFriction>(item.EntryFriction);
                    _db.StimJobEntryFrictions.Add(obj);
                }
            }
        }

        

        private void PerfFriction(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.PdatSession.StepDownTest.Step)
            {

                if (item.PerfFriction != null && item.PerfFriction.Uom != null)
                {
                    var obj = _mapper.Map<PerfFriction>(item.PerfFriction);
                    _db.StimJobPerfFrictions.Add(obj);
                }
            }
        }

        private void NearWellboreFriction(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.PdatSession.StepDownTest.Step)
            {

                if (item.NearWellboreFriction != null && item.NearWellboreFriction.Uom != null)
                {
                    var obj = _mapper.Map<NearWellboreFriction>(item.NearWellboreFriction);
                    _db.StimJobNearWellboreFrictions.Add(obj);
                }
            }
        }

        private void Step(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.PdatSession.StepDownTest.Step)
            {

                if (item != null && item.Uid != null)
                {
                    var obj = _mapper.Map<Step>(item);
                    _db.StimJobSteps.Add(obj);
                }
            }
        }

        private void StepDownTest(StimJob stimJob)
        {

            if (stimJob.JobInterval.PdatSession.StepDownTest != null && stimJob.JobInterval.PdatSession.StepDownTest.Uid != null)
            {
                var obj = _mapper.Map<StepDownTest>(stimJob.JobInterval.PdatSession.StepDownTest);
                _db.StimJobStepDownTests.Add(obj);
            }
        }

        private void PdatSession(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession != null && stimJob.JobInterval.PdatSession.Uid != null)
            {
                var obj = _mapper.Map<PdatSession>(stimJob.JobInterval.PdatSession);
                _db.StimJobPdatSessions.Add(obj);
            }
        }

        private void MdLithTop(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval.MdLithTop != null && stimJob.JobInterval.ReservoirInterval.MdLithTop.Uom != null)
            {
                var obj = _mapper.Map<MdLithTop>(stimJob.JobInterval.ReservoirInterval.MdLithTop);
                _db.StimJobMdLithTops.Add(obj);
            }
        }

       
        private void MdLithBottom(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval.MdLithBottom != null && stimJob.JobInterval.ReservoirInterval.MdLithBottom.Uom != null)
            {
                var obj = _mapper.Map<MdLithBottom>(stimJob.JobInterval.ReservoirInterval.MdLithBottom);
                _db.StimJobMdLithBottoms.Add(obj);
            }
        }

        private void LithFormationPermeability(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval.LithFormationPermeability != null && stimJob.JobInterval.ReservoirInterval.LithFormationPermeability.Uom != null)
            {
                var obj = _mapper.Map<LithFormationPermeability>(stimJob.JobInterval.ReservoirInterval.LithFormationPermeability);
                _db.StimJobLithFormationPermeabilitys.Add(obj);
            }
        }

        private void LithYoungsModulus(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval.LithYoungsModulus != null && stimJob.JobInterval.ReservoirInterval.LithYoungsModulus.Uom != null)
            {
                var obj = _mapper.Map<LithYoungsModulus>(stimJob.JobInterval.ReservoirInterval.LithYoungsModulus);
                _db.StimJobLithYoungsModuluss.Add(obj);
            }
        }

        private void LithPorePres(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval.LithPorePres != null && stimJob.JobInterval.ReservoirInterval.LithPorePres.Uom != null)
            {
                var obj = _mapper.Map<LithPorePres>(stimJob.JobInterval.ReservoirInterval.LithPorePres);
                _db.StimJobLithPorePress.Add(obj);
            }
        }

        private void LithNetPayThickness(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval.LithNetPayThickness != null && stimJob.JobInterval.ReservoirInterval.LithNetPayThickness.Uom != null)
            {
                var obj = _mapper.Map<LithNetPayThickness>(stimJob.JobInterval.ReservoirInterval.LithNetPayThickness);
                _db.StimJobLithNetPayThicknesss.Add(obj);
            }
        }

        private void MdGrossPayTop(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval.MdGrossPayTop != null && stimJob.JobInterval.ReservoirInterval.MdGrossPayTop.Uom != null)
            {
                var obj = _mapper.Map<MdGrossPayTop>(stimJob.JobInterval.ReservoirInterval.MdGrossPayTop);
                _db.StimJobMdGrossPayTops.Add(obj);
            }
        }

        private void MdGrossPayBottom(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval.MdGrossPayBottom != null && stimJob.JobInterval.ReservoirInterval.MdGrossPayBottom.Uom != null)
            {
                var obj = _mapper.Map<MdGrossPayBottom>(stimJob.JobInterval.ReservoirInterval.MdGrossPayBottom);
                _db.StimJobMdGrossPayBottoms.Add(obj);
            }
        }

        private void GrossPayThickness(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval.GrossPayThickness != null && stimJob.JobInterval.ReservoirInterval.GrossPayThickness.Uom != null)
            {
                var obj = _mapper.Map<GrossPayThickness>(stimJob.JobInterval.ReservoirInterval.GrossPayThickness);
                _db.StimJobGrossPayThicknesss.Add(obj);
            }
        }

        private void NetPayThickness(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval.NetPayThickness != null && stimJob.JobInterval.ReservoirInterval.NetPayThickness.Uom != null)
            {
                var obj = _mapper.Map<NetPayThickness>(stimJob.JobInterval.ReservoirInterval.NetPayThickness);
                _db.StimJobNetPayThicknesss.Add(obj);
            }
        }

        private void NetPayPorePres(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval.NetPayPorePres != null && stimJob.JobInterval.ReservoirInterval.NetPayPorePres.Uom != null)
            {
                var obj = _mapper.Map<NetPayPorePres>(stimJob.JobInterval.ReservoirInterval.NetPayPorePres);
                _db.StimJobNetPayPorePress.Add(obj);
            }
        }

        private void NetPayFluidCompressibility(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval.NetPayFluidCompressibility != null && stimJob.JobInterval.ReservoirInterval.NetPayFluidCompressibility.Uom != null)
            {
                var obj = _mapper.Map<NetPayFluidCompressibility>(stimJob.JobInterval.ReservoirInterval.NetPayFluidCompressibility);
                _db.StimJobNetPayFluidCompressibilitys.Add(obj);
            }
        }

        private void NetPayFluidViscosity(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval.NetPayFluidViscosity != null && stimJob.JobInterval.ReservoirInterval.NetPayFluidViscosity.Uom != null)
            {
                var obj = _mapper.Map<NetPayFluidViscosity>(stimJob.JobInterval.ReservoirInterval.NetPayFluidViscosity);
                _db.StimJobNetPayFluidViscositys.Add(obj);
            }
        }

        private void NetPayFormationPermeability(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval.NetPayFormationPermeability != null && stimJob.JobInterval.ReservoirInterval.NetPayFormationPermeability.Uom != null)
            {
                var obj = _mapper.Map<NetPayFormationPermeability>(stimJob.JobInterval.ReservoirInterval.NetPayFormationPermeability);
                _db.StimJobNetPayFormationPermeabilitys.Add(obj);
            }
        }

        private void LithPoissonsRatio(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval.LithPoissonsRatio != null && stimJob.JobInterval.ReservoirInterval.LithPoissonsRatio.Uom != null)
            {
                var obj = _mapper.Map<LithPoissonsRatio>(stimJob.JobInterval.ReservoirInterval.LithPoissonsRatio);
                _db.StimJobLithPoissonsRatios.Add(obj);
            }
        }

        private void NetPayFormationPorosity(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval.NetPayFormationPorosity != null && stimJob.JobInterval.ReservoirInterval.NetPayFormationPorosity.Uom != null)
            {
                var obj = _mapper.Map<NetPayFormationPorosity>(stimJob.JobInterval.ReservoirInterval.NetPayFormationPorosity);
                _db.StimJobNetPayFormationPorositys.Add(obj);
            }
        }

        private void FormationPermeability(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval.FormationPermeability != null && stimJob.JobInterval.ReservoirInterval.FormationPermeability.Uom != null)
            {
                var obj = _mapper.Map<FormationPermeability>(stimJob.JobInterval.ReservoirInterval.FormationPermeability);
                _db.StimJobFormationPermeabilitys.Add(obj);
            }
        }

        private void FormationPorosity(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval.FormationPorosity != null && stimJob.JobInterval.ReservoirInterval.FormationPorosity.Uom != null)
            {
                var obj = _mapper.Map<FormationPorosity>(stimJob.JobInterval.ReservoirInterval.FormationPorosity);
                _db.StimJobFormationPorositys.Add(obj);
            }
        }

        private void ReservoirInterval(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval != null && stimJob.JobInterval.ReservoirInterval.Uid != null)
            {
                var obj = _mapper.Map<ReservoirInterval>(stimJob.JobInterval.ReservoirInterval);
                _db.StimJobReservoirIntervals.Add(obj);
            }
        }

        private void MdPerforationsTop(StimJob stimJob)
        {
            if (stimJob.JobInterval.PerforationInterval.MdPerforationsTop != null && stimJob.JobInterval.PerforationInterval.MdPerforationsTop.Uom != null)
            {
                var obj = _mapper.Map<MdPerforationsTop>(stimJob.JobInterval.PerforationInterval.MdPerforationsTop);
                _db.StimJobMdPerforationsTops.Add(obj);
            }
        }

        private void MdPerforationsBottom(StimJob stimJob)
        {
            if (stimJob.JobInterval.PerforationInterval.MdPerforationsBottom != null && stimJob.JobInterval.PerforationInterval.MdPerforationsBottom.Uom != null)
            {
                var obj = _mapper.Map<MdPerforationsBottom>(stimJob.JobInterval.PerforationInterval.MdPerforationsBottom);
                _db.StimJobMdPerforationsBottoms.Add(obj);
            }
        }

        private void TvdPerforationsTop(StimJob stimJob)
        {
            if (stimJob.JobInterval.PerforationInterval.TvdPerforationsTop != null && stimJob.JobInterval.PerforationInterval.TvdPerforationsTop.Uom != null)
            {
                var obj = _mapper.Map<TvdPerforationsTop>(stimJob.JobInterval.PerforationInterval.TvdPerforationsTop);
                _db.StimJobTvdPerforationsTops.Add(obj);
            }
        }

        private void TvdPerforationsBottom(StimJob stimJob)
        {
            if (stimJob.JobInterval.PerforationInterval.TvdPerforationsBottom != null && stimJob.JobInterval.PerforationInterval.TvdPerforationsBottom.Uom != null)
            {
                var obj = _mapper.Map<TvdPerforationsBottom>(stimJob.JobInterval.PerforationInterval.TvdPerforationsBottom);
                _db.StimJobTvdPerforationsBottoms.Add(obj);
            }
        }

        private void Size(StimJob stimJob)
        {
            if (stimJob.JobInterval.PerforationInterval.Size != null && stimJob.JobInterval.PerforationInterval.Size.Uom != null)
            {
                var obj = _mapper.Map<Size>(stimJob.JobInterval.PerforationInterval.Size);
                _db.StimJobSizes.Add(obj);
            }
        }

        private void DensityPerforation(StimJob stimJob)
        {
            if (stimJob.JobInterval.PerforationInterval.DensityPerforation != null && stimJob.JobInterval.PerforationInterval.DensityPerforation.Uom != null)
            {
                var obj = _mapper.Map<DensityPerforation>(stimJob.JobInterval.PerforationInterval.DensityPerforation);
                _db.StimJobDensityPerforations.Add(obj);
            }
        }

        private void PhasingPerforation(StimJob stimJob)
        {
            if (stimJob.JobInterval.PerforationInterval.PhasingPerforation != null && stimJob.JobInterval.PerforationInterval.PhasingPerforation.Uom != null)
            {
                var obj = _mapper.Map<PhasingPerforation>(stimJob.JobInterval.PerforationInterval.PhasingPerforation);
                _db.StimJobPhasingPerforations.Add(obj);
            }
        }

        private void PerforationInterval(StimJob stimJob)
        {
            if (stimJob.JobInterval.PerforationInterval != null && stimJob.JobInterval.PerforationInterval.Uid != null)
            {
                var obj = _mapper.Map<PerforationInterval>(stimJob.JobInterval.PerforationInterval);
                _db.StimJobPerforationIntervals.Add(obj);
            }
        }

        private void JobInterval(StimJob stimJob)
        {
            if (stimJob.JobInterval != null && stimJob.JobInterval.Uid != null)
            {
                var obj = _mapper.Map<JobInterval>(stimJob.JobInterval);
                _db.StimJobJobIntervals.Add(obj);
            }
        }

        #endregion Insert StimJob

        #region Update StimJob
        private void UpdateTotalPumpTime(StimJob stimJob)
        {
            if (stimJob.TotalPumpTime != null && stimJob.TotalPumpTime.Uom != null)
            {
                var obj = _mapper.Map<TotalPumpTime>(stimJob.TotalPumpTime);
                _db.StimJobTotalPumpTimes.Update(obj);
            }
        }

        private void UpdateMaxJobPres(StimJob stimJob)
        {
            if (stimJob.MaxJobPres != null && stimJob.MaxJobPres.Uom != null)
            {
                var obj = _mapper.Map<MaxJobPres>(stimJob.MaxJobPres);
                _db.StimJobMaxJobPress.Update(obj);
            }
        }

        private void UpdateMaxFluidRate(StimJob stimJob)
        {
            if (stimJob.MaxFluidRate != null && stimJob.MaxFluidRate.Uom != null)
            {
                var obj = _mapper.Map<MaxFluidRate>(stimJob.MaxFluidRate);
                _db.StimJobMaxFluidRates.Update(obj);
            }
        }

        private void UpdateAvgJobPres(StimJob stimJob)
        {
            if (stimJob.AvgJobPres != null && stimJob.AvgJobPres.Uom != null)
            {
                var obj = _mapper.Map<AvgJobPres>(stimJob.AvgJobPres);
                _db.StimJobAvgJobPress.Update(obj);
            }
        }

        private void UpdateTotalJobVolume(StimJob stimJob)
        {
            if (stimJob.TotalJobVolume != null && stimJob.TotalJobVolume.Uom != null)
            {
                var obj = _mapper.Map<TotalJobVolume>(stimJob.TotalJobVolume);
                _db.StimJobTotalJobVolumes.Update(obj);
            }
        }

        private void UpdateTotalProppantWt(StimJob stimJob)
        {
            if (stimJob.TotalProppantWt != null && stimJob.TotalProppantWt.Uom != null)
            {
                var obj = _mapper.Map<TotalProppantWt>(stimJob.TotalProppantWt);
                _db.StimJobTotalProppantWts.Update(obj);
            }
        }

        private void UpdateTotalN2StdVolume(StimJob stimJob)
        {
            if (stimJob.TotalN2StdVolume != null && stimJob.TotalN2StdVolume.Uom != null)
            {
                var obj = _mapper.Map<TotalN2StdVolume>(stimJob.TotalN2StdVolume);
                _db.StimJobTotalN2StdVolumes.Update(obj);
            }
        }

        private void UpdateTotalCO2Mass(StimJob stimJob)
        {
            if (stimJob.TotalCO2Mass != null && stimJob.TotalCO2Mass.Uom != null)
            {
                var obj = _mapper.Map<TotalCO2Mass>(stimJob.TotalCO2Mass);
                _db.StimJobTotalCO2Masss.Update(obj);
            }
        }

        private void UpdateHhpOrdered(StimJob stimJob)
        {
            if (stimJob.HhpOrdered != null && stimJob.HhpOrdered.Uom != null)
            {
                var obj = _mapper.Map<HhpOrdered>(stimJob.HhpOrdered);
                _db.StimJobHhpOrdereds.Update(obj);
            }
        }

        private void UpdateHhpUsed(StimJob stimJob)
        {
            if (stimJob.HhpUsed != null && stimJob.HhpUsed.Uom != null)
            {
                var obj = _mapper.Map<HhpUsed>(stimJob.HhpUsed);
                _db.StimJobHhpUseds.Update(obj);
            }
        }

        private void UpdateFluidEfficiency(StimJob stimJob)
        {
            if (stimJob.FluidEfficiency != null && stimJob.FluidEfficiency.Uom != null)
            {
                var obj = _mapper.Map<FluidEfficiency>(stimJob.FluidEfficiency);
                _db.StimJobFluidEfficiencys.Update(obj);
            }
        }

        private void UpdateFlowBackPres(StimJob stimJob)
        {
            if (stimJob.FlowBackPres != null && stimJob.FlowBackPres.Uom != null)
            {
                var obj = _mapper.Map<FlowBackPres>(stimJob.FlowBackPres);
                _db.StimJobFlowBackPress.Update(obj);
            }
        }

        private void UpdateFlowBackRate(StimJob stimJob)
        {
            if (stimJob.FlowBackRate != null && stimJob.FlowBackRate.Uom != null)
            {
                var obj = _mapper.Map<FlowBackRate>(stimJob.FlowBackRate);
                _db.StimJobFlowBackRates.Update(obj);
            }
        }

        private void UpdateFlowBackVolume(StimJob stimJob)
        {
            if (stimJob.FlowBackVolume != null && stimJob.FlowBackVolume.Uom != null)
            {
                var obj = _mapper.Map<FlowBackVolume>(stimJob.FlowBackVolume);
                _db.StimJobFlowBackVolumes.Update(obj);
            }
        }

        private void UpdateBottomholeStaticTemperature(StimJob stimJob)
        {
            if (stimJob.BottomholeStaticTemperature != null && stimJob.BottomholeStaticTemperature.Uom != null)
            {
                var obj = _mapper.Map<BottomholeStaticTemperature>(stimJob.BottomholeStaticTemperature);
                _db.StimJobBottomholeStaticTemperatures.Update(obj);
            }
        }

        private void UpdateTreatingBottomholeTemperature(StimJob stimJob)
        {
            if (stimJob.TreatingBottomholeTemperature != null && stimJob.TreatingBottomholeTemperature.Uom != null)
            {
                var obj = _mapper.Map<TreatingBottomholeTemperature>(stimJob.TreatingBottomholeTemperature);
                _db.StimJobTreatingBottomholeTemperatures.Update(obj);
            }
        }

        private void UpdateMdFormationTop(StimJob stimJob)
        {
            if (stimJob.JobInterval.MdFormationTop != null && stimJob.JobInterval.MdFormationTop.Uom != null)
            {
                var obj = _mapper.Map<MdFormationTop>(stimJob.JobInterval.MdFormationTop);
                _db.StimJobMdFormationTops.Update(obj);
            }
        }

        private void UpdateMdFormationBottom(StimJob stimJob)
        {
            if (stimJob.JobInterval.MdFormationBottom != null && stimJob.JobInterval.MdFormationBottom.Uom != null)
            {
                var obj = _mapper.Map<MdFormationBottom>(stimJob.JobInterval.MdFormationBottom);
                _db.StimJobMdFormationBottoms.Update(obj);
            }
        }

        private void UpdateTvdFormationTop(StimJob stimJob)
        {
            if (stimJob.JobInterval.TvdFormationTop != null && stimJob.JobInterval.TvdFormationTop.Uom != null)
            {
                var obj = _mapper.Map<TvdFormationTop>(stimJob.JobInterval.TvdFormationTop);
                _db.StimJobTvdFormationTops.Update(obj);
            }
        }

        private void UpdateTvdFormationBottom(StimJob stimJob)
        {
            if (stimJob.JobInterval.TvdFormationBottom != null && stimJob.JobInterval.TvdFormationBottom.Uom != null)
            {
                var obj = _mapper.Map<TvdFormationBottom>(stimJob.JobInterval.TvdFormationBottom);
                _db.StimJobTvdFormationBottoms.Update(obj);
            }
        }

        private void UpdateOpenHoleDiameter(StimJob stimJob)
        {
            if (stimJob.JobInterval.OpenHoleDiameter != null && stimJob.JobInterval.OpenHoleDiameter.Uom != null)
            {
                var obj = _mapper.Map<OpenHoleDiameter>(stimJob.JobInterval.OpenHoleDiameter);
                _db.StimJobOpenHoleDiameters.Update(obj);
            }
        }

        private void UpdateMdOpenHoleTop(StimJob stimJob)
        {
            if (stimJob.JobInterval.MdOpenHoleTop != null && stimJob.JobInterval.MdOpenHoleTop.Uom != null)
            {
                var obj = _mapper.Map<MdOpenHoleTop>(stimJob.JobInterval.MdOpenHoleTop);
                _db.StimJobMdOpenHoleTops.Update(obj);
            }
        }

        private void UpdateMdOpenHoleBottom(StimJob stimJob)
        {
            if (stimJob.JobInterval.MdOpenHoleBottom != null && stimJob.JobInterval.MdOpenHoleBottom.Uom != null)
            {
                var obj = _mapper.Map<MdOpenHoleBottom>(stimJob.JobInterval.MdOpenHoleBottom);
                _db.StimJobMdOpenHoleBottoms.Update(obj);
            }
        }

        private void UpdateTvdOpenHoleTop(StimJob stimJob)
        {
            if (stimJob.JobInterval.TvdOpenHoleTop != null && stimJob.JobInterval.TvdOpenHoleTop.Uom != null)
            {
                var obj = _mapper.Map<TvdOpenHoleTop>(stimJob.JobInterval.TvdOpenHoleTop);
                _db.StimJobTvdOpenHoleTops.Update(obj);
            }
        }

        private void UpdateTvdOpenHoleBottom(StimJob stimJob)
        {
            if (stimJob.JobInterval.TvdOpenHoleBottom != null && stimJob.JobInterval.TvdOpenHoleBottom.Uom != null)
            {
                var obj = _mapper.Map<TvdOpenHoleBottom>(stimJob.JobInterval.TvdOpenHoleBottom);
                _db.StimJobTvdOpenHoleBottoms.Update(obj);
            }
        }

        private void UpdateTotalFrictionPresLoss(StimJob stimJob)
        {
            if (stimJob.JobInterval.TotalFrictionPresLoss != null && stimJob.JobInterval.TotalFrictionPresLoss.Uom != null)
            {
                var obj = _mapper.Map<TotalFrictionPresLoss>(stimJob.JobInterval.TotalFrictionPresLoss);
                _db.StimJobTotalFrictionPresLosss.Update(obj);
            }
        }

        private void UpdateMaxPresTubing(StimJob stimJob)
        {
            if (stimJob.JobInterval.MaxPresTubing != null && stimJob.JobInterval.MaxPresTubing.Uom != null)
            {
                var obj = _mapper.Map<MaxPresTubing>(stimJob.JobInterval.MaxPresTubing);
                _db.StimJobMaxPresTubings.Update(obj);
            }
        }

        private void UpdateMaxPresAnnulus(StimJob stimJob)
        {
            if (stimJob.JobInterval.MaxPresAnnulus != null && stimJob.JobInterval.MaxPresAnnulus.Uom != null)
            {
                var obj = _mapper.Map<MaxPresAnnulus>(stimJob.JobInterval.MaxPresAnnulus);
                _db.StimJobMaxPresAnnuluss.Update(obj);
            }
        }

        private void UpdateMaxFluidRateTubing(StimJob stimJob)
        {
            if (stimJob.JobInterval.MaxFluidRateTubing != null && stimJob.JobInterval.MaxFluidRateTubing.Uom != null)
            {
                var obj = _mapper.Map<MaxFluidRateTubing>(stimJob.JobInterval.MaxFluidRateTubing);
                _db.StimJobMaxFluidRateTubings.Update(obj);
            }
        }

        private void UpdateMaxFluidRateAnnulus(StimJob stimJob)
        {
            if (stimJob.JobInterval.MaxFluidRateAnnulus != null && stimJob.JobInterval.MaxFluidRateAnnulus.Uom != null)
            {
                var obj = _mapper.Map<MaxFluidRateAnnulus>(stimJob.JobInterval.MaxFluidRateAnnulus);
                _db.StimJobMaxFluidRateAnnuluss.Update(obj);
            }
        }

        private void UpdateAvgPresTubing(StimJob stimJob)
        {
            if (stimJob.JobInterval.AvgPresTubing != null && stimJob.JobInterval.AvgPresTubing.Uom != null)
            {
                var obj = _mapper.Map<AvgPresTubing>(stimJob.JobInterval.AvgPresTubing);
                _db.StimJobAvgPresTubings.Update(obj);
            }
        }

        private void UpdateAvgPresCasing(StimJob stimJob)
        {
            if (stimJob.JobInterval.AvgPresCasing != null && stimJob.JobInterval.AvgPresCasing.Uom != null)
            {
                var obj = _mapper.Map<AvgPresCasing>(stimJob.JobInterval.AvgPresCasing);
                _db.StimJobAvgPresCasings.Update(obj);
            }
        }

        private void UpdateBreakDownPres(StimJob stimJob)
        {
            if (stimJob.JobInterval.BreakDownPres != null && stimJob.JobInterval.BreakDownPres.Uom != null)
            {
                var obj = _mapper.Map<BreakDownPres>(stimJob.JobInterval.BreakDownPres);
                _db.StimJobBreakDownPress.Update(obj);
            }
        }

        private void UpdateAveragePres(StimJob stimJob)
        {
            if (stimJob.JobInterval.AveragePres != null && stimJob.JobInterval.AveragePres.Uom != null)
            {
                var obj = _mapper.Map<AveragePres>(stimJob.JobInterval.AveragePres);
                _db.StimJobAveragePress.Update(obj);
            }
        }

        private void UpdateAvgBaseFluidReturnRate(StimJob stimJob)
        {
            if (stimJob.JobInterval.AvgBaseFluidReturnRate != null && stimJob.JobInterval.AvgBaseFluidReturnRate.Uom != null)
            {
                var obj = _mapper.Map<AvgBaseFluidReturnRate>(stimJob.JobInterval.AvgBaseFluidReturnRate);
                _db.StimJobAvgBaseFluidReturnRates.Update(obj);
            }
        }

        private void UpdateAvgSlurryReturnRate(StimJob stimJob)
        {
            if (stimJob.JobInterval.AvgSlurryReturnRate != null && stimJob.JobInterval.AvgSlurryReturnRate.Uom != null)
            {
                var obj = _mapper.Map<AvgSlurryReturnRate>(stimJob.JobInterval.MaxPresAnnulus);
                _db.StimJobAvgSlurryReturnRates.Update(obj);
            }
        }

        private void UpdateAvgBottomholeRate(StimJob stimJob)
        {
            if (stimJob.JobInterval.AvgBottomholeRate != null && stimJob.JobInterval.AvgBottomholeRate.Uom != null)
            {
                var obj = _mapper.Map<AvgBottomholeRate>(stimJob.JobInterval.AvgBottomholeRate);
                _db.StimJobAvgBottomholeRates.Update(obj);
            }
        }

        private void UpdateTotalVolume(StimJob stimJob)
        {
            if (stimJob.JobInterval.TotalVolume != null && stimJob.JobInterval.TotalVolume.Uom != null)
            {
                var obj = _mapper.Map<TotalVolume>(stimJob.JobInterval.TotalVolume);
                _db.StimJobTotalVolumes.Update(obj);
            }
        }

        private void UpdateMaxProppantConcSurface(StimJob stimJob)
        {
            if (stimJob.JobInterval.MaxProppantConcSurface != null && stimJob.JobInterval.MaxProppantConcSurface.Uom != null)
            {
                var obj = _mapper.Map<MaxProppantConcSurface>(stimJob.JobInterval.MaxProppantConcSurface);
                _db.StimJobMaxProppantConcSurfaces.Update(obj);
            }
        }

        private void UpdateMaxProppantConcBottomhole(StimJob stimJob)
        {
            if (stimJob.JobInterval.MaxProppantConcBottomhole != null && stimJob.JobInterval.MaxProppantConcBottomhole.Uom != null)
            {
                var obj = _mapper.Map<MaxProppantConcBottomhole>(stimJob.JobInterval.MaxProppantConcBottomhole);
                _db.StimJobMaxProppantConcBottomholes.Update(obj);
            }
        }

        private void UpdateAvgProppantConcSurface(StimJob stimJob)
        {
            if (stimJob.JobInterval.AvgProppantConcSurface != null && stimJob.JobInterval.AvgProppantConcSurface.Uom != null)
            {
                var obj = _mapper.Map<AvgProppantConcSurface>(stimJob.JobInterval.AvgProppantConcSurface);
                _db.StimJobAvgProppantConcSurfaces.Update(obj);
            }
        }

        private void UpdateAvgProppantConcBottomhole(StimJob stimJob)
        {
            if (stimJob.JobInterval.AvgProppantConcBottomhole != null && stimJob.JobInterval.AvgProppantConcBottomhole.Uom != null)
            {
                var obj = _mapper.Map<AvgProppantConcBottomhole>(stimJob.JobInterval.AvgProppantConcBottomhole);
                _db.StimJobAvgProppantConcBottomholes.Update(obj);
            }
        }

        private void UpdatePerfproppantConc(StimJob stimJob)
        {
            if (stimJob.JobInterval.PerfproppantConc != null && stimJob.JobInterval.PerfproppantConc.Uom != null)
            {
                var obj = _mapper.Map<PerfproppantConc>(stimJob.JobInterval.PerfproppantConc);
                _db.StimJobPerfproppantConcs.Update(obj);
            }
        }

        private void UpdateTotalProppantMass(StimJob stimJob)
        {
            if (stimJob.JobInterval.TotalProppantMass != null && stimJob.JobInterval.TotalProppantMass.Uom != null)
            {
                var obj = _mapper.Map<TotalProppantMass>(stimJob.JobInterval.TotalProppantMass);
                _db.StimJobTotalProppantMasss.Update(obj);
            }
        }

        private void UpdateMass(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.TotalProppantUsage)
            {

                if (item.Mass != null && item.Mass.Uom != null)
                {
                    var obj = _mapper.Map<Mass>(item.Mass);
                    _db.StimJobMasss.Update(obj);
                }
            }

            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    foreach (var childitem in subItem.StageFluid.Additive)
                    {
                        if (childitem.Mass != null && childitem.Mass.Uom != null)
                        {
                            var obj = _mapper.Map<Mass>(childitem.Mass);
                            _db.StimJobMasss.Update(obj);
                        }
                    }
                }
            }
        }

        private void UpdateTotalProppantUsage(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.TotalProppantUsage)
            {

                if (item != null && item.Uid != null)
                {
                    var obj = _mapper.Map<TotalProppantUsage>(item);
                    _db.StimJobTotalProppantUsages.Update(obj);
                }
            }
        }

        private void UpdatePercentProppantPumped(StimJob stimJob)
        {
            if (stimJob.JobInterval.PercentProppantPumped != null && stimJob.JobInterval.PercentProppantPumped.Uom != null)
            {
                var obj = _mapper.Map<PercentProppantPumped>(stimJob.JobInterval.PercentProppantPumped);
                _db.StimJobPercentProppantPumpeds.Update(obj);
            }
        }

        private void UpdateWellboreProppantMass(StimJob stimJob)
        {
            if (stimJob.JobInterval.WellboreProppantMass != null && stimJob.JobInterval.WellboreProppantMass.Uom != null)
            {
                var obj = _mapper.Map<WellboreProppantMass>(stimJob.JobInterval.WellboreProppantMass);
                _db.StimJobWellboreProppantMasss.Update(obj);
            }
        }

        private void UpdateFormationProppantMass(StimJob stimJob)
        {
            if (stimJob.JobInterval.FormationProppantMass != null && stimJob.JobInterval.FormationProppantMass.Uom != null)
            {
                var obj = _mapper.Map<FormationProppantMass>(stimJob.JobInterval.FormationProppantMass);
                _db.StimJobFormationProppantMasss.Update(obj);
            }
        }

        private void UpdateFractureGradient(StimJob stimJob)
        {
            if (stimJob.JobInterval.FractureGradient != null && stimJob.JobInterval.FractureGradient.Uom != null)
            {
                var obj = _mapper.Map<FractureGradient>(stimJob.JobInterval.FractureGradient);
                _db.StimJobFractureGradients.Update(obj);
            }
        }

        private void UpdateFinalFractureGradient(StimJob stimJob)
        {
            if (stimJob.JobInterval.FinalFractureGradient != null && stimJob.JobInterval.FinalFractureGradient.Uom != null)
            {
                var obj = _mapper.Map<FinalFractureGradient>(stimJob.JobInterval.FinalFractureGradient);
                _db.StimJobFinalFractureGradients.Update(obj);
            }
        }

        private void UpdateInitialShutinPres(StimJob stimJob)
        {
            if (stimJob.JobInterval.InitialShutinPres != null && stimJob.JobInterval.InitialShutinPres.Uom != null)
            {
                var obj = _mapper.Map<InitialShutinPres>(stimJob.JobInterval.InitialShutinPres);
                _db.StimJobInitialShutinPress.Update(obj);
            }

            if (stimJob.JobInterval.PdatSession.StepDownTest != null && stimJob.JobInterval.PdatSession.StepDownTest.InitialShutinPres.Uom != null)
            {
                var obj = _mapper.Map<InitialShutinPres>(stimJob.JobInterval.PdatSession.StepDownTest.InitialShutinPres);
                _db.StimJobInitialShutinPress.Update(obj);
            }
        }

        private void UpdatePres(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.ShutinPres)
            {
                if (item.Pres != null && item.Pres.Uom != null)
                {
                    var obj = _mapper.Map<Pres>(item.Pres);
                    _db.StimJobPress.Update(obj);
                }
            }
        }

        private void UpdateTimeAfterShutin(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.ShutinPres)
            {
                if (item.TimeAfterShutin != null && item.TimeAfterShutin.Uom != null)
                {
                    var obj = _mapper.Map<TimeAfterShutin>(item.TimeAfterShutin);
                    _db.StimJobTimeAfterShutins.Update(obj);
                }
            }
        }

        private void UpdateShutinPres(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.ShutinPres)
            {
                if (item != null && item.Uid != null)
                {
                    var obj = _mapper.Map<ShutinPres>(item);
                    _db.StimJobShutinPress.Update(obj);
                }
            }
        }

        private void UpdateScreenOutPres(StimJob stimJob)
        {
            if (stimJob.JobInterval.ScreenOutPres != null && stimJob.JobInterval.ScreenOutPres.Uom != null)
            {
                var obj = _mapper.Map<ScreenOutPres>(stimJob.JobInterval.ScreenOutPres);
                _db.StimJobScreenOutPress.Update(obj);
            }
        }

        private void UpdateHhpOrderedCO2(StimJob stimJob)
        {
            if (stimJob.JobInterval.HhpOrderedCO2 != null && stimJob.JobInterval.HhpOrderedCO2.Uom != null)
            {
                var obj = _mapper.Map<HhpOrderedCO2>(stimJob.JobInterval.HhpOrderedCO2);
                _db.StimJobHhpOrderedCO2s.Update(obj);
            }
        }

        private void UpdateHhpOrderedFluid(StimJob stimJob)
        {
            if (stimJob.JobInterval.HhpOrderedFluid != null && stimJob.JobInterval.HhpOrderedFluid.Uom != null)
            {
                var obj = _mapper.Map<HhpOrderedFluid>(stimJob.JobInterval.HhpOrderedFluid);
                _db.StimJobHhpOrderedFluids.Update(obj);
            }
        }

        private void UpdateHhpUsedCO2(StimJob stimJob)
        {
            if (stimJob.JobInterval.HhpUsedCO2 != null && stimJob.JobInterval.HhpUsedCO2.Uom != null)
            {
                var obj = _mapper.Map<HhpUsedCO2>(stimJob.JobInterval.HhpUsedCO2);
                _db.StimJobHhpUsedCO2s.Update(obj);
            }
        }

        private void UpdateHhpUsedFluid(StimJob stimJob)
        {
            if (stimJob.JobInterval.HhpUsedFluid != null && stimJob.JobInterval.HhpUsedFluid.Uom != null)
            {
                var obj = _mapper.Map<HhpUsedFluid>(stimJob.JobInterval.HhpUsedFluid);
                _db.StimJobHhpUsedFluids.Update(obj);
            }
        }

        private void UpdatePerfBallSize(StimJob stimJob)
        {
            if (stimJob.JobInterval.PerfBallSize != null && stimJob.JobInterval.PerfBallSize.Uom != null)
            {
                var obj = _mapper.Map<PerfBallSize>(stimJob.JobInterval.PerfBallSize);
                _db.StimJobPerfBallSizes.Update(obj);
            }
        }
        private void UpdateAvgFractureWidth(StimJob stimJob)
        {
            if (stimJob.JobInterval.AvgFractureWidth != null && stimJob.JobInterval.AvgFractureWidth.Uom != null)
            {
                var obj = _mapper.Map<AvgFractureWidth>(stimJob.JobInterval.AvgFractureWidth);
                _db.StimJobAvgFractureWidths.Update(obj);
            }
        }

        private void UpdateAvgConductivity(StimJob stimJob)
        {
            if (stimJob.JobInterval.AvgConductivity != null && stimJob.JobInterval.AvgConductivity.Uom != null)
            {
                var obj = _mapper.Map<AvgConductivity>(stimJob.JobInterval.AvgConductivity);
                _db.StimJobAvgConductivitys.Update(obj);
            }
        }

        private void UpdateNetPres(StimJob stimJob)
        {
            if (stimJob.JobInterval.NetPres != null && stimJob.JobInterval.NetPres.Uom != null)
            {
                var obj = _mapper.Map<NetPres>(stimJob.JobInterval.NetPres);
                _db.StimJobNetPress.Update(obj);
            }
        }

        private void UpdateClosurePres(StimJob stimJob)
        {
            if (stimJob.JobInterval.ClosurePres != null && stimJob.JobInterval.ClosurePres.Uom != null)
            {
                var obj = _mapper.Map<ClosurePres>(stimJob.JobInterval.HhpUsedCO2);
                _db.StimJobClosurePress.Update(obj);
            }
        }

        private void UpdateClosureDuration(StimJob stimJob)
        {
            if (stimJob.JobInterval.ClosureDuration != null && stimJob.JobInterval.ClosureDuration.Uom != null)
            {
                var obj = _mapper.Map<ClosureDuration>(stimJob.JobInterval.ClosureDuration);
                _db.StimJobClosureDurations.Update(obj);
            }
        }

        private void UpdateMaxTreatmentPres(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.MaxTreatmentPres != null && item.MaxTreatmentPres.Uom != null)
                {
                    var obj = _mapper.Map<MaxTreatmentPres>(item.MaxTreatmentPres);
                    _db.StimJobMaxTreatmentPress.Update(obj);
                }
            }
        }

        private void UpdateMaxSlurryRate(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.MaxSlurryRate != null && item.MaxSlurryRate.Uom != null)
                {
                    var obj = _mapper.Map<MaxSlurryRate>(item.MaxSlurryRate);
                    _db.StimJobMaxSlurryRates.Update(obj);
                }
            }
        }

        private void UpdateMaxWellheadRate(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.MaxWellheadRate != null && item.MaxWellheadRate.Uom != null)
                {
                    var obj = _mapper.Map<MaxWellheadRate>(item.MaxWellheadRate);
                    _db.StimJobMaxWellheadRates.Update(obj);
                }
            }
        }

        private void UpdateMaxN2StdRate(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.MaxN2StdRate != null && item.MaxN2StdRate.Uom != null)
                {
                    var obj = _mapper.Map<MaxN2StdRate>(item.MaxN2StdRate);
                    _db.StimJobMaxN2StdRates.Update(obj);
                }
            }
        }

        private void UpdateMaxCO2LiquidRate(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.MaxCO2LiquidRate != null && item.MaxCO2LiquidRate.Uom != null)
                {
                    var obj = _mapper.Map<MaxCO2LiquidRate>(item.MaxCO2LiquidRate);
                    _db.StimJobMaxCO2LiquidRates.Update(obj);
                }
            }
        }

        private void UpdateMaxGelRate(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.MaxGelRate != null && item.MaxGelRate.Uom != null)
                {
                    var obj = _mapper.Map<MaxGelRate>(item.MaxGelRate);
                    _db.StimJobMaxGelRates.Update(obj);
                }
            }
        }

        private void UpdateMaxOilRate(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.MaxOilRate != null && item.MaxOilRate.Uom != null)
                {
                    var obj = _mapper.Map<MaxOilRate>(item.MaxOilRate);
                    _db.StimJobMaxOilRates.Update(obj);
                }
            }
        }

        private void UpdateMaxAcidRate(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.MaxAcidRate != null && item.MaxAcidRate.Uom != null)
                {
                    var obj = _mapper.Map<MaxAcidRate>(item.MaxAcidRate);
                    _db.StimJobMaxAcidRates.Update(obj);
                }
            }
        }

        private void UpdateMaxPropConc(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.MaxPropConc != null && item.MaxPropConc.Uom != null)
                {
                    var obj = _mapper.Map<MaxPropConc>(item.MaxPropConc);
                    _db.StimJobMaxPropConcs.Update(obj);
                }
            }
        }

        private void UpdateMaxSlurryPropConc(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.MaxSlurryPropConc != null && item.MaxSlurryPropConc.Uom != null)
                {
                    var obj = _mapper.Map<MaxSlurryPropConc>(item.MaxSlurryPropConc);
                    _db.StimJobMaxSlurryPropConcs.Update(obj);
                }
            }
        }

        private void UpdateAvgTreatPres(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AvgTreatPres != null && item.AvgTreatPres.Uom != null)
                {
                    var obj = _mapper.Map<AvgTreatPres>(item.AvgTreatPres);
                    _db.StimJobAvgTreatPress.Update(obj);
                }
            }
        }

        private void UpdateAvgBaseFluidRate(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AvgBaseFluidRate != null && item.AvgBaseFluidRate.Uom != null)
                {
                    var obj = _mapper.Map<AvgBaseFluidRate>(item.AvgBaseFluidRate);
                    _db.StimJobAvgBaseFluidRates.Update(obj);
                }
            }
        }

        private void UpdateAvgSlurryRate(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AvgSlurryRate != null && item.AvgSlurryRate.Uom != null)
                {
                    var obj = _mapper.Map<AvgSlurryRate>(item.AvgSlurryRate);
                    _db.StimJobAvgSlurryRates.Update(obj);
                }
            }
        }

        private void UpdateAvgWellheadRate(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AvgWellheadRate != null && item.AvgWellheadRate.Uom != null)
                {
                    var obj = _mapper.Map<AvgWellheadRate>(item.AvgWellheadRate);
                    _db.StimJobAvgWellheadRates.Update(obj);
                }
            }
        }

        private void UpdateAvgN2StdRate(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AvgN2StdRate != null && item.AvgN2StdRate.Uom != null)
                {
                    var obj = _mapper.Map<AvgN2StdRate>(item.AvgN2StdRate);
                    _db.StimJobAvgN2StdRates.Update(obj);
                }
            }
        }

        private void UpdateAvgCO2LiquidRate(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AvgCO2LiquidRate != null && item.AvgCO2LiquidRate.Uom != null)
                {
                    var obj = _mapper.Map<AvgCO2LiquidRate>(item.AvgCO2LiquidRate);
                    _db.StimJobAvgCO2LiquidRates.Update(obj);
                }
            }
        }

        private void UpdateAvgGelRate(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AvgGelRate != null && item.AvgGelRate.Uom != null)
                {
                    var obj = _mapper.Map<AvgGelRate>(item.AvgGelRate);
                    _db.StimJobAvgGelRates.Update(obj);
                }
            }
        }

        private void UpdateAvgOilRate(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AvgOilRate != null && item.AvgOilRate.Uom != null)
                {
                    var obj = _mapper.Map<AvgOilRate>(item.AvgOilRate);
                    _db.StimJobAvgOilRates.Update(obj);
                }
            }
        }

        private void UpdateAvgAcidRate(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AvgAcidRate != null && item.AvgAcidRate.Uom != null)
                {
                    var obj = _mapper.Map<AvgAcidRate>(item.AvgAcidRate);
                    _db.StimJobAvgAcidRates.Update(obj);
                }
            }
        }

        private void UpdateAvgPropConc(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AvgPropConc != null && item.AvgPropConc.Uom != null)
                {
                    var obj = _mapper.Map<AvgPropConc>(item.AvgPropConc);
                    _db.StimJobAvgPropConcs.Update(obj);
                }
            }
        }

        private void UpdateAvgSlurryPropConc(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AvgSlurryPropConc != null && item.AvgSlurryPropConc.Uom != null)
                {
                    var obj = _mapper.Map<AvgSlurryPropConc>(item.AvgSlurryPropConc);
                    _db.StimJobAvgSlurryPropConcs.Update(obj);
                }
            }
        }

        private void UpdateAvgTemperature(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AvgTemperature != null && item.AvgTemperature.Uom != null)
                {
                    var obj = _mapper.Map<AvgTemperature>(item.AvgTemperature);
                    _db.StimJobAvgTemperatures.Update(obj);
                }
            }
        }

        private void UpdateAvgBaseFluidQuality(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AvgBaseFluidQuality != null && item.AvgBaseFluidQuality.Uom != null)
                {
                    var obj = _mapper.Map<AvgBaseFluidQuality>(item.AvgBaseFluidQuality);
                    _db.StimJobAvgBaseFluidQualitys.Update(obj);
                }
            }
        }

        private void UpdateAvgN2BaseFluidQuality(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AvgN2BaseFluidQuality != null && item.AvgN2BaseFluidQuality.Uom != null)
                {
                    var obj = _mapper.Map<AvgN2BaseFluidQuality>(item.AvgN2BaseFluidQuality);
                    _db.StimJobAvgN2BaseFluidQualitys.Update(obj);
                }
            }
        }

        private void UpdateAvgCO2BaseFluidQuality(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AvgCO2BaseFluidQuality != null && item.AvgCO2BaseFluidQuality.Uom != null)
                {
                    var obj = _mapper.Map<AvgCO2BaseFluidQuality>(item.AvgCO2BaseFluidQuality);
                    _db.StimJobAvgCO2BaseFluidQualitys.Update(obj);
                }
            }
        }

        private void UpdateAvgHydraulicPower(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AvgHydraulicPower != null && item.AvgHydraulicPower.Uom != null)
                {
                    var obj = _mapper.Map<AvgHydraulicPower>(item.AvgHydraulicPower);
                    _db.StimJobAvgHydraulicPowers.Update(obj);
                }
            }
        }

        private void UpdateBaseFluidVol(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.BaseFluidVol != null && item.BaseFluidVol.Uom != null)
                {
                    var obj = _mapper.Map<BaseFluidVol>(item.BaseFluidVol);
                    _db.StimJobBaseFluidVols.Update(obj);
                }
            }
        }

        private void UpdateSlurryVol(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.SlurryVol != null && item.SlurryVol.Uom != null)
                {
                    var obj = _mapper.Map<SlurryVol>(item.SlurryVol);
                    _db.StimJobSlurryVols.Update(obj);
                }
            }
        }

        private void UpdateWellheadVol(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.WellheadVol != null && item.WellheadVol.Uom != null)
                {
                    var obj = _mapper.Map<WellheadVol>(item.WellheadVol);
                    _db.StimJobWellheadVols.Update(obj);
                }
            }
        }

        private void UpdateStdVolN2(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.StdVolN2 != null && item.StdVolN2.Uom != null)
                {
                    var obj = _mapper.Map<StdVolN2>(item.StdVolN2);
                    _db.StimJobStdVolN2s.Update(obj);
                }
            }
        }

        private void UpdateMassCO2(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.MassCO2 != null && item.MassCO2.Uom != null)
                {
                    var obj = _mapper.Map<MassCO2>(item.MassCO2);
                    _db.StimJobMassCO2s.Update(obj);
                }
            }
        }

        private void UpdateGelVol(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.GelVol != null && item.GelVol.Uom != null)
                {
                    var obj = _mapper.Map<GelVol>(item.GelVol);
                    _db.StimJobGelVols.Update(obj);
                }
            }
        }

        private void UpdateOilVol(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.OilVol != null && item.OilVol.Uom != null)
                {
                    var obj = _mapper.Map<OilVol>(item.OilVol);
                    _db.StimJobOilVols.Update(obj);
                }
            }
        }

        private void UpdateAcidVol(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AcidVol != null && item.AcidVol.Uom != null)
                {
                    var obj = _mapper.Map<AcidVol>(item.AcidVol);
                    _db.StimJobAcidVols.Update(obj);
                }
            }
        }

        private void UpdateBaseFluidBypassVol(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.BaseFluidBypassVol != null && item.BaseFluidBypassVol.Uom != null)
                {
                    var obj = _mapper.Map<BaseFluidBypassVol>(item.BaseFluidBypassVol);
                    _db.StimJobBaseFluidBypassVols.Update(obj);
                }
            }
        }

        private void UpdatePropMass(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.PropMass != null && item.PropMass.Uom != null)
                {
                    var obj = _mapper.Map<PropMass>(item.PropMass);
                    _db.StimJobPropMasss.Update(obj);
                }
            }
        }

        private void UpdateMaxPmaxPacPres(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.MaxPmaxPacPres != null && item.MaxPmaxPacPres.Uom != null)
                {
                    var obj = _mapper.Map<MaxPmaxPacPres>(item.MaxPmaxPacPres);
                    _db.StimJobMaxPmaxPacPress.Update(obj);
                }
            }
        }

        private void UpdateMaxPmaxWeaklinkPres(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.MaxPmaxWeaklinkPres != null && item.MaxPmaxWeaklinkPres.Uom != null)
                {
                    var obj = _mapper.Map<MaxPmaxWeaklinkPres>(item.MaxPmaxWeaklinkPres);
                    _db.StimJobMaxPmaxWeaklinkPress.Update(obj);
                }
            }
        }

        private void UpdateAvgPmaxPacPres(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AvgPmaxPacPres != null && item.AvgPmaxPacPres.Uom != null)
                {
                    var obj = _mapper.Map<AvgPmaxPacPres>(item.AvgPmaxPacPres);
                    _db.StimJobAvgPmaxPacPress.Update(obj);
                }
            }
        }

        private void UpdateAvgPmaxWeaklinkPres(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.AvgPmaxWeaklinkPres != null && item.AvgPmaxWeaklinkPres.Uom != null)
                {
                    var obj = _mapper.Map<AvgPmaxWeaklinkPres>(item.AvgPmaxWeaklinkPres);
                    _db.StimJobAvgPmaxWeaklinkPress.Update(obj);
                }
            }
        }

        private void UpdateShutinPres5Min(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.ShutinPres5Min != null && item.ShutinPres5Min.Uom != null)
                {
                    var obj = _mapper.Map<ShutinPres5Min>(item.ShutinPres5Min);
                    _db.StimJobShutinPres5Mins.Update(obj);
                }
            }
        }

        private void UpdateShutinPres10Min(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.ShutinPres10Min != null && item.ShutinPres10Min.Uom != null)
                {
                    var obj = _mapper.Map<ShutinPres10Min>(item.ShutinPres10Min);
                    _db.StimJobShutinPres10Mins.Update(obj);
                }
            }
        }

        private void UpdateShutinPres15Min(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.ShutinPres15Min != null && item.ShutinPres15Min.Uom != null)
                {
                    var obj = _mapper.Map<ShutinPres15Min>(item.ShutinPres15Min);
                    _db.StimJobShutinPres15Mins.Update(obj);
                }
            }
        }

        private void UpdatePercentPad(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {

                if (item.PercentPad != null && item.PercentPad.Uom != null)
                {
                    var obj = _mapper.Map<PercentPad>(item.PercentPad);
                    _db.StimJobPercentPads.Update(obj);
                }
            }
        }

        private void UpdateOd(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.Tubular)
                {
                    if (subItem != null && subItem.Od != null && subItem.Od.Uom != null)
                    {
                        var obj = _mapper.Map<StimJobOd>(subItem.Od);
                        _db.StimJobOds.Update(obj);
                    }
                }
            }
        }

        private void UpdateWeight(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.Tubular)
                {
                    if (subItem != null && subItem.Weight != null && subItem.Weight.Uom != null)
                    {
                        var obj = _mapper.Map<StimJobWeight>(subItem.Weight);
                        _db.StimJobWeights.Update(obj);
                    }
                }
            }

            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.StageFluid.Proppant.Weight != null && subItem.StageFluid.Proppant.Weight.Uom != null)
                    {
                        var obj = _mapper.Map<StimJobWeight>(subItem.StageFluid.Proppant.Weight);
                        _db.StimJobWeights.Update(obj);
                    }
                }
            }

        }

        private void UpdateMdTop(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.Tubular)
                {
                    if (subItem != null && subItem.MdTop != null && subItem.MdTop.Uom != null)
                    {
                        var obj = _mapper.Map<StimJobMdTop>(subItem.MdTop);
                        _db.StimJobMdTops.Update(obj);
                    }
                }
            }
        }

        private void UpdateId(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.Tubular)
                {
                    if (subItem != null && subItem.Id != null && subItem.Id.Uom != null)
                    {
                        var obj = _mapper.Map<StimJobId>(subItem.Id);
                        _db.StimJobIds.Update(obj);
                    }
                }
            }
        }

        private void UpdateMdBottom(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.Tubular)
                {
                    if (subItem != null && subItem.MdBottom != null && subItem.MdBottom.Uom != null)
                    {
                        var obj = _mapper.Map<StimJobMdBottom>(subItem.MdBottom);
                        _db.StimJobMdBottoms.Update(obj);
                    }
                }
            }
        }

        private void UpdateVolumeFactor(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.Tubular)
                {
                    if (subItem != null && subItem.VolumeFactor != null && subItem.VolumeFactor.Uom != null)
                    {
                        var obj = _mapper.Map<StimJobVolumeFactor>(subItem.VolumeFactor);
                        _db.StimJobVolumeFactors.Update(obj);
                    }
                }
            }
        }

        private void UpdateTubular(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.Tubular)
                {
                    if (subItem != null && subItem.Uid != null)
                    {
                        var obj = _mapper.Map<StimJobTubular>(subItem);
                        _db.StimJobTubulars.Update(obj);
                    }
                }
            }
        }

        private void UpdatePumpTime(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.PumpTime != null && subItem.PumpTime.Uom != null)
                    {
                        var obj = _mapper.Map<PumpTime>(subItem.PumpTime);
                        _db.StimJobPumpTimes.Update(obj);
                    }
                }
            }
        }

        private void UpdateStartRateSurfaceLiquid(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.StartRateSurfaceLiquid != null && subItem.StartRateSurfaceLiquid.Uom != null)
                    {
                        var obj = _mapper.Map<StartRateSurfaceLiquid>(subItem.StartRateSurfaceLiquid);
                        _db.StimJobStartRateSurfaceLiquids.Update(obj);
                    }
                }
            }
        }

        private void UpdateEndRateSurfaceLiquid(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.EndRateSurfaceLiquid != null && subItem.EndRateSurfaceLiquid.Uom != null)
                    {
                        var obj = _mapper.Map<EndRateSurfaceLiquid>(subItem.EndRateSurfaceLiquid);
                        _db.StimJobEndRateSurfaceLiquids.Update(obj);
                    }
                }
            }
        }

        private void UpdateAvgRateSurfaceLiquid(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.AvgRateSurfaceLiquid != null && subItem.AvgRateSurfaceLiquid.Uom != null)
                    {
                        var obj = _mapper.Map<AvgRateSurfaceLiquid>(subItem.AvgRateSurfaceLiquid);
                        _db.StimJobAvgRateSurfaceLiquids.Update(obj);
                    }
                }
            }
        }

        private void UpdateStartRateSurfaceCO2(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.StartRateSurfaceCO2 != null && subItem.StartRateSurfaceCO2.Uom != null)
                    {
                        var obj = _mapper.Map<StartRateSurfaceCO2>(subItem.StartRateSurfaceCO2);
                        _db.StimJobStartRateSurfaceCO2s.Update(obj);
                    }
                }
            }
        }

        private void UpdateEndRateSurfaceCO2(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.EndRateSurfaceCO2 != null && subItem.EndRateSurfaceCO2.Uom != null)
                    {
                        var obj = _mapper.Map<EndRateSurfaceCO2>(subItem.EndRateSurfaceCO2);
                        _db.StimJobEndRateSurfaceCO2s.Update(obj);
                    }
                }
            }
        }

        private void UpdateAvgRateSurfaceCO2(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.AvgRateSurfaceCO2 != null && subItem.AvgRateSurfaceCO2.Uom != null)
                    {
                        var obj = _mapper.Map<AvgRateSurfaceCO2>(subItem.AvgRateSurfaceCO2);
                        _db.StimJobAvgRateSurfaceCO2s.Update(obj);
                    }
                }
            }
        }

        private void UpdateStartStdRateSurfaceN2(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.StartStdRateSurfaceN2 != null && subItem.StartStdRateSurfaceN2.Uom != null)
                    {
                        var obj = _mapper.Map<StartStdRateSurfaceN2>(subItem.StartStdRateSurfaceN2);
                        _db.StimJobStartStdRateSurfaceN2s.Update(obj);
                    }
                }
            }
        }

        private void UpdateEndStdRateSurfaceN2(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.EndStdRateSurfaceN2 != null && subItem.EndStdRateSurfaceN2.Uom != null)
                    {
                        var obj = _mapper.Map<EndStdRateSurfaceN2>(subItem.EndStdRateSurfaceN2);
                        _db.StimJobEndStdRateSurfaceN2s.Update(obj);
                    }
                }
            }
        }

        private void UpdateAvgStdRateSurfaceN2(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.AvgStdRateSurfaceN2 != null && subItem.AvgStdRateSurfaceN2.Uom != null)
                    {
                        var obj = _mapper.Map<AvgStdRateSurfaceN2>(subItem.AvgStdRateSurfaceN2);
                        _db.StimJobAvgStdRateSurfaceN2s.Update(obj);
                    }
                }
            }
        }

        private void UpdateStartPresSurface(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.StartPresSurface != null && subItem.StartPresSurface.Uom != null)
                    {
                        var obj = _mapper.Map<StartPresSurface>(subItem.StartPresSurface);
                        _db.StimJobStartPresSurfaces.Update(obj);
                    }
                }
            }
        }

        private void UpdateEndPresSurface(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.EndPresSurface != null && subItem.EndPresSurface.Uom != null)
                    {
                        var obj = _mapper.Map<EndPresSurface>(subItem.EndPresSurface);
                        _db.StimJobEndPresSurfaces.Update(obj);
                    }
                }
            }
        }

        private void UpdateAveragePresSurface(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.AveragePresSurface != null && subItem.AveragePresSurface.Uom != null)
                    {
                        var obj = _mapper.Map<AveragePresSurface>(subItem.AveragePresSurface);
                        _db.StimJobAveragePresSurfaces.Update(obj);
                    }
                }
            }
        }

        private void UpdateStartPumpRateBottomhole(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.StartPumpRateBottomhole != null && subItem.StartPumpRateBottomhole.Uom != null)
                    {
                        var obj = _mapper.Map<StartPumpRateBottomhole>(subItem.StartPumpRateBottomhole);
                        _db.StimJobStartPumpRateBottomholes.Update(obj);
                    }
                }
            }
        }

        private void UpdateEndPumpRateBottomhole(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.EndPumpRateBottomhole != null && subItem.EndPumpRateBottomhole.Uom != null)
                    {
                        var obj = _mapper.Map<EndPumpRateBottomhole>(subItem.EndPumpRateBottomhole);
                        _db.StimJobEndPumpRateBottomholes.Update(obj);
                    }
                }
            }
        }

        private void UpdateAvgPumpRateBottomhole(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.AvgPumpRateBottomhole != null && subItem.AvgPumpRateBottomhole.Uom != null)
                    {
                        var obj = _mapper.Map<AvgPumpRateBottomhole>(subItem.AvgPumpRateBottomhole);
                        _db.StimJobAvgPumpRateBottomholes.Update(obj);
                    }
                }
            }
        }

        private void UpdateStartPresBottomhole(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.StartPresBottomhole != null && subItem.StartPresBottomhole.Uom != null)
                    {
                        var obj = _mapper.Map<StartPresBottomhole>(subItem.StartPresBottomhole);
                        _db.StimJobStartPresBottomholes.Update(obj);
                    }
                }
            }
        }

        private void UpdateEndPresBottomhole(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.EndPresBottomhole != null && subItem.EndPresBottomhole.Uom != null)
                    {
                        var obj = _mapper.Map<EndPresBottomhole>(subItem.EndPresBottomhole);
                        _db.StimJobEndPresBottomholes.Update(obj);
                    }
                }
            }
        }

        private void UpdateAveragePresBottomhole(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.AveragePresBottomhole != null && subItem.AveragePresBottomhole.Uom != null)
                    {
                        var obj = _mapper.Map<AveragePresBottomhole>(subItem.AveragePresBottomhole);
                        _db.StimJobAveragePresBottomholes.Update(obj);
                    }
                }
            }
        }

        private void UpdateStartProppantConcSurface(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.StartProppantConcSurface != null && subItem.StartProppantConcSurface.Uom != null)
                    {
                        var obj = _mapper.Map<StartProppantConcSurface>(subItem.StartProppantConcSurface);
                        _db.StimJobStartProppantConcSurfaces.Update(obj);
                    }
                }
            }
        }

        private void UpdateEndProppantConcSurface(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.EndProppantConcSurface != null && subItem.EndProppantConcSurface.Uom != null)
                    {
                        var obj = _mapper.Map<EndProppantConcSurface>(subItem.EndProppantConcSurface);
                        _db.StimJobEndProppantConcSurfaces.Update(obj);
                    }
                }
            }
        }

        private void UpdateStartProppantConcBottomhole(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.StartProppantConcBottomhole != null && subItem.StartProppantConcBottomhole.Uom != null)
                    {
                        var obj = _mapper.Map<StartProppantConcBottomhole>(subItem.StartProppantConcBottomhole);
                        _db.StimJobStartProppantConcBottomholes.Update(obj);
                    }
                }
            }
        }

        private void UpdateEndProppantConcBottomhole(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.EndProppantConcBottomhole != null && subItem.EndProppantConcBottomhole.Uom != null)
                    {
                        var obj = _mapper.Map<EndProppantConcBottomhole>(subItem.EndProppantConcBottomhole);
                        _db.StimJobEndProppantConcBottomholes.Update(obj);
                    }
                }
            }
        }

        private void UpdateStartFoamRateN2(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.StartFoamRateN2 != null && subItem.StartFoamRateN2.Uom != null)
                    {
                        var obj = _mapper.Map<StartFoamRateN2>(subItem.StartFoamRateN2);
                        _db.StimJobStartFoamRateN2s.Update(obj);
                    }
                }
            }
        }

        private void UpdateEndFoamRateN2(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.EndFoamRateN2 != null && subItem.EndFoamRateN2.Uom != null)
                    {
                        var obj = _mapper.Map<EndFoamRateN2>(subItem.EndFoamRateN2);
                        _db.StimJobEndFoamRateN2s.Update(obj);
                    }
                }
            }
        }

        private void UpdateStartFoamRateCO2(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.StartFoamRateCO2 != null && subItem.StartFoamRateCO2.Uom != null)
                    {
                        var obj = _mapper.Map<StartFoamRateCO2>(subItem.StartFoamRateCO2);
                        _db.StimJobStartFoamRateCO2s.Update(obj);
                    }
                }
            }
        }

        private void UpdateEndFoamRateCO2(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.EndFoamRateCO2 != null && subItem.EndFoamRateCO2.Uom != null)
                    {
                        var obj = _mapper.Map<EndFoamRateCO2>(subItem.EndFoamRateCO2);
                        _db.StimJobEndFoamRateCO2s.Update(obj);
                    }
                }
            }
        }

        private void UpdateFluidVolBase(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.FluidVolBase != null && subItem.FluidVolBase.Uom != null)
                    {
                        var obj = _mapper.Map<FluidVolBase>(subItem.FluidVolBase);
                        _db.StimJobFluidVolBases.Update(obj);
                    }
                }
            }
        }

        private void UpdateFluidVolSlurry(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.FluidVolSlurry != null && subItem.FluidVolSlurry.Uom != null)
                    {
                        var obj = _mapper.Map<FluidVolSlurry>(subItem.FluidVolSlurry);
                        _db.StimJobFluidVolSlurrys.Update(obj);
                    }
                }
            }
        }

        private void UpdateSlurryRateBegin(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.SlurryRateBegin != null && subItem.SlurryRateBegin.Uom != null)
                    {
                        var obj = _mapper.Map<SlurryRateBegin>(subItem.SlurryRateBegin);
                        _db.StimJobSlurryRateBegins.Update(obj);
                    }
                }
            }
        }

        private void UpdateSlurryRateEnd(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.SlurryRateEnd != null && subItem.SlurryRateEnd.Uom != null)
                    {
                        var obj = _mapper.Map<SlurryRateEnd>(subItem.SlurryRateEnd);
                        _db.StimJobSlurryRateEnds.Update(obj);
                    }
                }
            }
        }

        private void UpdateProppantMassWellHead(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.ProppantMassWellHead != null && subItem.ProppantMassWellHead.Uom != null)
                    {
                        var obj = _mapper.Map<ProppantMassWellHead>(subItem.ProppantMassWellHead);
                        _db.StimJobProppantMassWellHeads.Update(obj);
                    }
                }
            }
        }

        private void UpdateProppantMass(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.ProppantMass != null && subItem.ProppantMass.Uom != null)
                    {
                        var obj = _mapper.Map<ProppantMass>(subItem.ProppantMass);
                        _db.StimJobProppantMasss.Update(obj);
                    }
                }
            }
        }

        private void UpdateMaxPres(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.MaxPres != null && subItem.MaxPres.Uom != null)
                    {
                        var obj = _mapper.Map<MaxPres>(subItem.MaxPres);
                        _db.StimJobMaxPress.Update(obj);
                    }
                }
            }
        }

        private void UpdateAvgInternalPhaseFraction(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.AvgInternalPhaseFraction != null && subItem.AvgInternalPhaseFraction.Uom != null)
                    {
                        var obj = _mapper.Map<AvgInternalPhaseFraction>(subItem.AvgInternalPhaseFraction);
                        _db.StimJobAvgInternalPhaseFractions.Update(obj);
                    }
                }
            }
        }

        private void UpdateAvgCO2Rate(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.AvgCO2Rate != null && subItem.AvgCO2Rate.Uom != null)
                    {
                        var obj = _mapper.Map<AvgCO2Rate>(subItem.AvgCO2Rate);
                        _db.StimJobAvgCO2Rates.Update(obj);
                    }
                }
            }
        }

        private void UpdateGelVolume(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.GelVolume != null && subItem.GelVolume.Uom != null)
                    {
                        var obj = _mapper.Map<GelVolume>(subItem.GelVolume);
                        _db.StimJobGelVolumes.Update(obj);
                    }
                }
            }
        }

        private void UpdateOilVolume(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.OilVolume != null && subItem.OilVolume.Uom != null)
                    {
                        var obj = _mapper.Map<OilVolume>(subItem.OilVolume);
                        _db.StimJobOilVolumes.Update(obj);
                    }
                }
            }
        }

        private void UpdateAcidVolume(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.AcidVolume != null && subItem.AcidVolume.Uom != null)
                    {
                        var obj = _mapper.Map<AcidVolume>(subItem.AcidVolume);
                        _db.StimJobAcidVolumes.Update(obj);
                    }
                }
            }
        }

        private void UpdateFluidVol(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    if (subItem.StageFluid.FluidVol != null && subItem.StageFluid.FluidVol.Uom != null)
                    {
                        var obj = _mapper.Map<FluidVol>(subItem.StageFluid.FluidVol);
                        _db.StimJobFluidVols.Update(obj);
                    }
                }
            }
        }

        private void UpdateVolume(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    foreach (var childitem in subItem.StageFluid.Additive)
                    {
                        if (childitem.Volume != null && childitem.Volume.Uom != null)
                        {
                            var obj = _mapper.Map<Volume>(childitem.Volume);
                            _db.StimJobVolumes.Update(obj);
                        }
                    }
                }
            }
        }

        private void UpdateAdditive(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {
                    foreach (var childitem in subItem.StageFluid.Additive)
                    {
                        if (childitem != null && childitem.Uid != null)
                        {
                            var obj = _mapper.Map<StimJobAdditive>(childitem);
                            _db.StimJobAdditives.Update(obj);
                        }
                    }
                }
            }
        }

        private void UpdateProppant(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {

                    if (subItem.StageFluid.Proppant != null)
                    {
                        var obj = _mapper.Map<Proppant>(subItem.StageFluid.Proppant);
                        _db.StimJobProppants.Update(obj);
                    }
                }
            }
        }

        private void UpdateStageFluid(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {

                    if (subItem.StageFluid != null)
                    {
                        var obj = _mapper.Map<StageFluid>(subItem.StageFluid);
                        _db.StimJobStageFluids.Update(obj);
                    }
                }
            }
        }

        private void UpdateJobStage(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobStage)
                {

                    if (subItem != null && subItem.Uid != null)
                    {
                        var obj = _mapper.Map<JobStage>(subItem);
                        _db.StimJobJobStages.Update(obj);
                    }
                }
            }
        }

        private void UpdateJobEvent(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                foreach (var subItem in item.JobEvent)
                {

                    if (subItem != null && subItem.Uid != null)
                    {
                        var obj = _mapper.Map<JobEvent>(subItem);
                        _db.StimJobJobEvents.Update(obj);
                    }
                }
            }
        }

        private void UpdateFlowPath(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.FlowPath)
            {
                if (item != null && item.Uid != null)
                {
                    var obj = _mapper.Map<FlowPath>(item);
                    _db.StimJobFlowPaths.Update(obj);
                }
            }
        }

        private void UpdatePumpDuration(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.PumpDuration != null && stimJob.JobInterval.PdatSession.PumpDuration.Uom != null)
            {
                var obj = _mapper.Map<PumpDuration>(stimJob.JobInterval.PdatSession.PumpDuration);
                _db.StimJobPumpDurations.Update(obj);
            }
        }

        private void UpdateAvgBottomholeTreatmentPres(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.AvgBottomholeTreatmentPres != null && stimJob.JobInterval.PdatSession.AvgBottomholeTreatmentPres.Uom != null)
            {
                var obj = _mapper.Map<AvgBottomholeTreatmentPres>(stimJob.JobInterval.PdatSession.AvgBottomholeTreatmentPres);
                _db.StimJobAvgBottomholeTreatmentPress.Update(obj);
            }
        }
        private void UpdateBottomholeHydrostaticPres(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.BottomholeHydrostaticPres != null && stimJob.JobInterval.PdatSession.BottomholeHydrostaticPres.Uom != null)
            {
                var obj = _mapper.Map<BottomholeHydrostaticPres>(stimJob.JobInterval.PdatSession.BottomholeHydrostaticPres);
                _db.StimJobBottomholeHydrostaticPress.Update(obj);
            }
        }

        private void UpdateBubblePointPres(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.BubblePointPres != null && stimJob.JobInterval.PdatSession.BubblePointPres.Uom != null)
            {
                var obj = _mapper.Map<BubblePointPres>(stimJob.JobInterval.PdatSession.BubblePointPres);
                _db.StimJobBubblePointPress.Update(obj);
            }
        }

        private void UpdateFrictionPres(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.FrictionPres != null && stimJob.JobInterval.PdatSession.FrictionPres.Uom != null)
            {
                var obj = _mapper.Map<FrictionPres>(stimJob.JobInterval.PdatSession.FrictionPres);
                _db.StimJobFrictionPress.Update(obj);
            }
        }

        private void UpdatePorePres(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.PorePres != null && stimJob.JobInterval.PdatSession.PorePres.Uom != null)
            {
                var obj = _mapper.Map<PorePres>(stimJob.JobInterval.PdatSession.PorePres);
                _db.StimJobPorePress.Update(obj);
            }
        }


        private void UpdateAvgBottomholeTreatmentRate(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.AvgBottomholeTreatmentRate != null && stimJob.JobInterval.PdatSession.AvgBottomholeTreatmentRate.Uom != null)
            {
                var obj = _mapper.Map<AvgBottomholeTreatmentRate>(stimJob.JobInterval.PdatSession.AvgBottomholeTreatmentRate);
                _db.StimJobAvgBottomholeTreatmentRates.Update(obj);
            }
        }

        private void UpdateFluidDensity(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.FluidDensity != null && stimJob.JobInterval.PdatSession.FluidDensity.Uom != null)
            {
                var obj = _mapper.Map<FluidDensity>(stimJob.JobInterval.PdatSession.FluidDensity);
                _db.StimJobFluidDensitys.Update(obj);
            }
        }

        private void UpdateWellboreVolume(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.WellboreVolume != null && stimJob.JobInterval.PdatSession.WellboreVolume.Uom != null)
            {
                var obj = _mapper.Map<WellboreVolume>(stimJob.JobInterval.PdatSession.WellboreVolume);
                _db.StimJobWellboreVolumes.Update(obj);
            }
        }

        private void UpdateMdSurface(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.MdSurface != null && stimJob.JobInterval.PdatSession.MdSurface.Uom != null)
            {
                var obj = _mapper.Map<MdSurface>(stimJob.JobInterval.PdatSession.MdSurface);
                _db.StimJobMdSurfaces.Update(obj);
            }
        }

        private void UpdateMdBottomhole(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.MdBottomhole != null && stimJob.JobInterval.PdatSession.MdBottomhole.Uom != null)
            {
                var obj = _mapper.Map<MdBottomhole>(stimJob.JobInterval.PdatSession.MdBottomhole);
                _db.StimJobMdBottomholes.Update(obj);
            }
        }

        private void UpdateMdMidPerforation(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.MdMidPerforation != null && stimJob.JobInterval.PdatSession.MdMidPerforation.Uom != null)
            {
                var obj = _mapper.Map<MdMidPerforation>(stimJob.JobInterval.PdatSession.MdMidPerforation);
                _db.StimJobMdMidPerforations.Update(obj);
            }
        }

        private void UpdateTvdMidPerforation(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.TvdMidPerforation != null && stimJob.JobInterval.PdatSession.TvdMidPerforation.Uom != null)
            {
                var obj = _mapper.Map<TvdMidPerforation>(stimJob.JobInterval.PdatSession.TvdMidPerforation);
                _db.StimJobTvdMidPerforations.Update(obj);
            }
        }

        private void UpdateSurfaceTemperature(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.SurfaceTemperature != null && stimJob.JobInterval.PdatSession.SurfaceTemperature.Uom != null)
            {
                var obj = _mapper.Map<SurfaceTemperature>(stimJob.JobInterval.PdatSession.SurfaceTemperature);
                _db.StimJobSurfaceTemperatures.Update(obj);
            }
        }

        private void UpdateBottomholeTemperature(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.BottomholeTemperature != null && stimJob.JobInterval.PdatSession.BottomholeTemperature.Uom != null)
            {
                var obj = _mapper.Map<BottomholeTemperature>(stimJob.JobInterval.PdatSession.BottomholeTemperature);
                _db.StimJobBottomholeTemperatures.Update(obj);
            }
        }

        private void UpdateSurfaceFluidTemperature(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.SurfaceFluidTemperature != null && stimJob.JobInterval.PdatSession.SurfaceFluidTemperature.Uom != null)
            {
                var obj = _mapper.Map<SurfaceFluidTemperature>(stimJob.JobInterval.PdatSession.SurfaceFluidTemperature);
                _db.StimJobSurfaceFluidTemperatures.Update(obj);
            }
        }

        private void UpdateFluidCompressibility(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.FluidCompressibility != null && stimJob.JobInterval.PdatSession.FluidCompressibility.Uom != null)
            {
                var obj = _mapper.Map<FluidCompressibility>(stimJob.JobInterval.PdatSession.FluidCompressibility);
                _db.StimJobFluidCompressibilitys.Update(obj);
            }
        }

        private void UpdateReservoirTotalCompressibility(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.ReservoirTotalCompressibility != null && stimJob.JobInterval.PdatSession.ReservoirTotalCompressibility.Uom != null)
            {
                var obj = _mapper.Map<ReservoirTotalCompressibility>(stimJob.JobInterval.PdatSession.ReservoirTotalCompressibility);
                _db.StimJobReservoirTotalCompressibilitys.Update(obj);
            }
        }

        private void UpdateFluidSpecificHeat(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.FluidSpecificHeat != null && stimJob.JobInterval.PdatSession.FluidSpecificHeat.Uom != null)
            {
                var obj = _mapper.Map<FluidSpecificHeat>(stimJob.JobInterval.PdatSession.FluidSpecificHeat);
                _db.StimJobFluidSpecificHeats.Update(obj);
            }
        }

        private void UpdateFluidThermalConductivity(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.FluidThermalConductivity != null && stimJob.JobInterval.PdatSession.FluidThermalConductivity.Uom != null)
            {
                var obj = _mapper.Map<FluidThermalConductivity>(stimJob.JobInterval.PdatSession.FluidThermalConductivity);
                _db.StimJobFluidThermalConductivitys.Update(obj);
            }
        }

        private void UpdateFluidThermalExpansionCoefficient(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.FluidThermalExpansionCoefficient != null && stimJob.JobInterval.PdatSession.FluidThermalExpansionCoefficient.Uom != null)
            {
                var obj = _mapper.Map<FluidThermalExpansionCoefficient>(stimJob.JobInterval.PdatSession.FluidThermalExpansionCoefficient);
                _db.StimJobFluidThermalExpansionCoefficients.Update(obj);
            }
        }

        private void UpdateFoamQuality(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.FoamQuality != null && stimJob.JobInterval.PdatSession.FoamQuality.Uom != null)
            {
                var obj = _mapper.Map<FoamQuality>(stimJob.JobInterval.PdatSession.FoamQuality);
                _db.StimJobFoamQualitys.Update(obj);
            }
        }

        private void UpdateBottomholeRate(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.PdatSession.StepRateTest.PresMeasurement)
            {

                if (item.BottomholeRate != null && item.BottomholeRate.Uom != null)
                {
                    var obj = _mapper.Map<BottomholeRate>(item.BottomholeRate);
                    _db.StimJobBottomholeRates.Update(obj);
                }
            }
        }

        private void UpdatePresMeasurement(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.PdatSession.StepRateTest.PresMeasurement)
            {

                if (item != null)
                {
                    var obj = _mapper.Map<PresMeasurement>(item);
                    _db.StimJobPresMeasurements.Update(obj);
                }
            }
        }

        private void UpdateFractureExtensionPres(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.StepRateTest.FractureExtensionPres != null && stimJob.JobInterval.PdatSession.StepRateTest.FractureExtensionPres.Uom != null)
            {
                var obj = _mapper.Map<FractureExtensionPres>(stimJob.JobInterval.PdatSession.StepRateTest.FractureExtensionPres);
                _db.StimJobFractureExtensionPress.Update(obj);
            }

        }

        private void UpdateStepRateTest(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.StepRateTest != null && stimJob.JobInterval.PdatSession.StepRateTest.Uid != null)
            {
                var obj = _mapper.Map<StepRateTest>(stimJob.JobInterval.PdatSession.StepRateTest);
                _db.StimJobStepRateTests.Update(obj);
            }

        }
        private void UpdateEndPdlDuration(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.FluidEfficiencyTest.EndPdlDuration != null && stimJob.JobInterval.PdatSession.FluidEfficiencyTest.EndPdlDuration.Uom != null)
            {
                var obj = _mapper.Map<EndPdlDuration>(stimJob.JobInterval.PdatSession.FluidEfficiencyTest.EndPdlDuration);
                _db.StimJobEndPdlDurations.Update(obj);
            }
        }

        private void UpdateFractureCloseDuration(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.FluidEfficiencyTest.FractureCloseDuration != null && stimJob.JobInterval.PdatSession.FluidEfficiencyTest.FractureCloseDuration.Uom != null)
            {
                var obj = _mapper.Map<FractureCloseDuration>(stimJob.JobInterval.PdatSession.FluidEfficiencyTest.FractureCloseDuration);
                _db.StimJobFractureCloseDurations.Update(obj);
            }

            if (stimJob.JobInterval.PdatSession.PumpFlowBackTest.FractureCloseDuration != null && stimJob.JobInterval.PdatSession.PumpFlowBackTest.FractureCloseDuration.Uom != null)
            {
                var obj = _mapper.Map<FractureCloseDuration>(stimJob.JobInterval.PdatSession.PumpFlowBackTest.FractureCloseDuration);
                _db.StimJobFractureCloseDurations.Update(obj);
            }

        }

        private void UpdateFractureClosePres(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.FluidEfficiencyTest.FractureClosePres != null && stimJob.JobInterval.PdatSession.FluidEfficiencyTest.FractureClosePres.Uom != null)
            {
                var obj = _mapper.Map<FractureClosePres>(stimJob.JobInterval.PdatSession.FluidEfficiencyTest.FractureClosePres);
                _db.StimJobFractureClosePress.Update(obj);
            }

            if (stimJob.JobInterval.PdatSession.PumpFlowBackTest.FractureClosePres != null && stimJob.JobInterval.PdatSession.PumpFlowBackTest.FractureClosePres.Uom != null)
            {
                var obj = _mapper.Map<FractureClosePres>(stimJob.JobInterval.PdatSession.PumpFlowBackTest.FractureClosePres);
                _db.StimJobFractureClosePress.Update(obj);
            }

        }

        private void UpdatePseudoRadialPres(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.FluidEfficiencyTest.PseudoRadialPres != null && stimJob.JobInterval.PdatSession.FluidEfficiencyTest.PseudoRadialPres.Uom != null)
            {
                var obj = _mapper.Map<PseudoRadialPres>(stimJob.JobInterval.PdatSession.FluidEfficiencyTest.PseudoRadialPres);
                _db.StimJobPseudoRadialPress.Update(obj);
            }
        }

        private void UpdateFractureLength(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.FluidEfficiencyTest.FractureLength != null && stimJob.JobInterval.PdatSession.FluidEfficiencyTest.FractureLength.Uom != null)
            {
                var obj = _mapper.Map<FractureLength>(stimJob.JobInterval.PdatSession.FluidEfficiencyTest.FractureLength);
                _db.StimJobFractureLengths.Update(obj);
            }
        }

        private void UpdateFractureWidth(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.FluidEfficiencyTest.FractureWidth != null && stimJob.JobInterval.PdatSession.FluidEfficiencyTest.FractureWidth.Uom != null)
            {
                var obj = _mapper.Map<FractureWidth>(stimJob.JobInterval.PdatSession.FluidEfficiencyTest.FractureWidth);
                _db.StimJobFractureWidths.Update(obj);
            }
        }

        private void UpdateResidualPermeability(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.FluidEfficiencyTest.ResidualPermeability != null && stimJob.JobInterval.PdatSession.FluidEfficiencyTest.ResidualPermeability.Uom != null)
            {
                var obj = _mapper.Map<ResidualPermeability>(stimJob.JobInterval.PdatSession.FluidEfficiencyTest.ResidualPermeability);
                _db.StimJobResidualPermeabilitys.Update(obj);
            }
        }

        private void UpdateFluidEfficiencyTest(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.FluidEfficiencyTest != null && stimJob.JobInterval.PdatSession.FluidEfficiencyTest.Uid != null)
            {
                var obj = _mapper.Map<FluidEfficiencyTest>(stimJob.JobInterval.PdatSession.FluidEfficiencyTest);
                _db.StimJobFluidEfficiencyTests.Update(obj);
            }
        }

        private void UpdatePumpFlowBackTest(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.PumpFlowBackTest != null && stimJob.JobInterval.PdatSession.PumpFlowBackTest.Uid != null)
            {
                var obj = _mapper.Map<PumpFlowBackTest>(stimJob.JobInterval.PdatSession.PumpFlowBackTest);
                _db.StimJobPumpFlowBackTests.Update(obj);
            }
        }


        private void UpdateBottomholeFluidDensity(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.StepDownTest.BottomholeFluidDensity != null && stimJob.JobInterval.PdatSession.StepDownTest.BottomholeFluidDensity.Uom != null)
            {
                var obj = _mapper.Map<BottomholeFluidDensity>(stimJob.JobInterval.PdatSession.StepDownTest.BottomholeFluidDensity);
                _db.StimJobBottomholeFluidDensitys.Update(obj);
            }

            if (stimJob.JobInterval.PdatSession.StepDownTest != null && stimJob.JobInterval.PdatSession.StepDownTest.BottomholeFluidDensity.Uom != null)
            {
                var obj = _mapper.Map<BottomholeFluidDensity>(stimJob.JobInterval.PdatSession.StepDownTest.BottomholeFluidDensity);
                _db.StimJobBottomholeFluidDensitys.Update(obj);
            }
        }

        private void UpdateDiameterEntryHole(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession.StepDownTest.DiameterEntryHole != null && stimJob.JobInterval.PdatSession.StepDownTest.DiameterEntryHole.Uom != null)
            {
                var obj = _mapper.Map<DiameterEntryHole>(stimJob.JobInterval.PdatSession.StepDownTest.DiameterEntryHole);
                _db.StimJobDiameterEntryHoles.Update(obj);
            }

            if (stimJob.JobInterval.PdatSession.StepDownTest != null && stimJob.JobInterval.PdatSession.StepDownTest.DiameterEntryHole.Uom != null)
            {
                var obj = _mapper.Map<DiameterEntryHole>(stimJob.JobInterval.PdatSession.StepDownTest.DiameterEntryHole);
                _db.StimJobDiameterEntryHoles.Update(obj);
            }


        }

        private void UpdatePipeFriction(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.PdatSession.StepDownTest.Step)
            {

                if (item.PipeFriction != null && item.PipeFriction.Uom != null)
                {
                    var obj = _mapper.Map<PipeFriction>(item.PipeFriction);
                    _db.StimJobPipeFrictions.Update(obj);
                }
            }
        }

        private void UpdateEntryFriction(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.PdatSession.StepDownTest.Step)
            {

                if (item.EntryFriction != null && item.EntryFriction.Uom != null)
                {
                    var obj = _mapper.Map<EntryFriction>(item.EntryFriction);
                    _db.StimJobEntryFrictions.Update(obj);
                }
            }
        }



        private void UpdatePerfFriction(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.PdatSession.StepDownTest.Step)
            {

                if (item.PerfFriction != null && item.PerfFriction.Uom != null)
                {
                    var obj = _mapper.Map<PerfFriction>(item.PerfFriction);
                    _db.StimJobPerfFrictions.Update(obj);
                }
            }
        }

        private void UpdateNearWellboreFriction(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.PdatSession.StepDownTest.Step)
            {

                if (item.NearWellboreFriction != null && item.NearWellboreFriction.Uom != null)
                {
                    var obj = _mapper.Map<NearWellboreFriction>(item.NearWellboreFriction);
                    _db.StimJobNearWellboreFrictions.Update(obj);
                }
            }
        }

        private void UpdateStep(StimJob stimJob)
        {
            foreach (var item in stimJob.JobInterval.PdatSession.StepDownTest.Step)
            {

                if (item != null && item.Uid != null)
                {
                    var obj = _mapper.Map<Step>(item);
                    _db.StimJobSteps.Update(obj);
                }
            }
        }

        private void UpdateStepDownTest(StimJob stimJob)
        {

            if (stimJob.JobInterval.PdatSession.StepDownTest != null && stimJob.JobInterval.PdatSession.StepDownTest.Uid != null)
            {
                var obj = _mapper.Map<StepDownTest>(stimJob.JobInterval.PdatSession.StepDownTest);
                _db.StimJobStepDownTests.Update(obj);
            }
        }

        private void UpdatePdatSession(StimJob stimJob)
        {
            if (stimJob.JobInterval.PdatSession != null && stimJob.JobInterval.PdatSession.Uid != null)
            {
                var obj = _mapper.Map<PdatSession>(stimJob.JobInterval.PdatSession);
                _db.StimJobPdatSessions.Update(obj);
            }
        }

        private void UpdateMdLithTop(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval.MdLithTop != null && stimJob.JobInterval.ReservoirInterval.MdLithTop.Uom != null)
            {
                var obj = _mapper.Map<MdLithTop>(stimJob.JobInterval.ReservoirInterval.MdLithTop);
                _db.StimJobMdLithTops.Update(obj);
            }
        }


        private void UpdateMdLithBottom(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval.MdLithBottom != null && stimJob.JobInterval.ReservoirInterval.MdLithBottom.Uom != null)
            {
                var obj = _mapper.Map<MdLithBottom>(stimJob.JobInterval.ReservoirInterval.MdLithBottom);
                _db.StimJobMdLithBottoms.Update(obj);
            }
        }

        private void UpdateLithFormationPermeability(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval.LithFormationPermeability != null && stimJob.JobInterval.ReservoirInterval.LithFormationPermeability.Uom != null)
            {
                var obj = _mapper.Map<LithFormationPermeability>(stimJob.JobInterval.ReservoirInterval.LithFormationPermeability);
                _db.StimJobLithFormationPermeabilitys.Update(obj);
            }
        }

        private void UpdateLithYoungsModulus(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval.LithYoungsModulus != null && stimJob.JobInterval.ReservoirInterval.LithYoungsModulus.Uom != null)
            {
                var obj = _mapper.Map<LithYoungsModulus>(stimJob.JobInterval.ReservoirInterval.LithYoungsModulus);
                _db.StimJobLithYoungsModuluss.Update(obj);
            }
        }

        private void UpdateLithPorePres(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval.LithPorePres != null && stimJob.JobInterval.ReservoirInterval.LithPorePres.Uom != null)
            {
                var obj = _mapper.Map<LithPorePres>(stimJob.JobInterval.ReservoirInterval.LithPorePres);
                _db.StimJobLithPorePress.Update(obj);
            }
        }

        private void UpdateLithNetPayThickness(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval.LithNetPayThickness != null && stimJob.JobInterval.ReservoirInterval.LithNetPayThickness.Uom != null)
            {
                var obj = _mapper.Map<LithNetPayThickness>(stimJob.JobInterval.ReservoirInterval.LithNetPayThickness);
                _db.StimJobLithNetPayThicknesss.Update(obj);
            }
        }

        private void UpdateMdGrossPayTop(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval.MdGrossPayTop != null && stimJob.JobInterval.ReservoirInterval.MdGrossPayTop.Uom != null)
            {
                var obj = _mapper.Map<MdGrossPayTop>(stimJob.JobInterval.ReservoirInterval.MdGrossPayTop);
                _db.StimJobMdGrossPayTops.Update(obj);
            }
        }

        private void UpdateMdGrossPayBottom(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval.MdGrossPayBottom != null && stimJob.JobInterval.ReservoirInterval.MdGrossPayBottom.Uom != null)
            {
                var obj = _mapper.Map<MdGrossPayBottom>(stimJob.JobInterval.ReservoirInterval.MdGrossPayBottom);
                _db.StimJobMdGrossPayBottoms.Update(obj);
            }
        }

        private void UpdateGrossPayThickness(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval.GrossPayThickness != null && stimJob.JobInterval.ReservoirInterval.GrossPayThickness.Uom != null)
            {
                var obj = _mapper.Map<GrossPayThickness>(stimJob.JobInterval.ReservoirInterval.GrossPayThickness);
                _db.StimJobGrossPayThicknesss.Update(obj);
            }
        }

        private void UpdateNetPayThickness(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval.NetPayThickness != null && stimJob.JobInterval.ReservoirInterval.NetPayThickness.Uom != null)
            {
                var obj = _mapper.Map<NetPayThickness>(stimJob.JobInterval.ReservoirInterval.NetPayThickness);
                _db.StimJobNetPayThicknesss.Update(obj);
            }
        }

        private void UpdateNetPayPorePres(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval.NetPayPorePres != null && stimJob.JobInterval.ReservoirInterval.NetPayPorePres.Uom != null)
            {
                var obj = _mapper.Map<NetPayPorePres>(stimJob.JobInterval.ReservoirInterval.NetPayPorePres);
                _db.StimJobNetPayPorePress.Update(obj);
            }
        }

        private void UpdateNetPayFluidCompressibility(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval.NetPayFluidCompressibility != null && stimJob.JobInterval.ReservoirInterval.NetPayFluidCompressibility.Uom != null)
            {
                var obj = _mapper.Map<NetPayFluidCompressibility>(stimJob.JobInterval.ReservoirInterval.NetPayFluidCompressibility);
                _db.StimJobNetPayFluidCompressibilitys.Update(obj);
            }
        }

        private void UpdateNetPayFluidViscosity(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval.NetPayFluidViscosity != null && stimJob.JobInterval.ReservoirInterval.NetPayFluidViscosity.Uom != null)
            {
                var obj = _mapper.Map<NetPayFluidViscosity>(stimJob.JobInterval.ReservoirInterval.NetPayFluidViscosity);
                _db.StimJobNetPayFluidViscositys.Update(obj);
            }
        }

        private void UpdateNetPayFormationPermeability(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval.NetPayFormationPermeability != null && stimJob.JobInterval.ReservoirInterval.NetPayFormationPermeability.Uom != null)
            {
                var obj = _mapper.Map<NetPayFormationPermeability>(stimJob.JobInterval.ReservoirInterval.NetPayFormationPermeability);
                _db.StimJobNetPayFormationPermeabilitys.Update(obj);
            }
        }

        private void UpdateLithPoissonsRatio(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval.LithPoissonsRatio != null && stimJob.JobInterval.ReservoirInterval.LithPoissonsRatio.Uom != null)
            {
                var obj = _mapper.Map<LithPoissonsRatio>(stimJob.JobInterval.ReservoirInterval.LithPoissonsRatio);
                _db.StimJobLithPoissonsRatios.Update(obj);
            }
        }

        private void UpdateNetPayFormationPorosity(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval.NetPayFormationPorosity != null && stimJob.JobInterval.ReservoirInterval.NetPayFormationPorosity.Uom != null)
            {
                var obj = _mapper.Map<NetPayFormationPorosity>(stimJob.JobInterval.ReservoirInterval.NetPayFormationPorosity);
                _db.StimJobNetPayFormationPorositys.Update(obj);
            }
        }

        private void UpdateFormationPermeability(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval.FormationPermeability != null && stimJob.JobInterval.ReservoirInterval.FormationPermeability.Uom != null)
            {
                var obj = _mapper.Map<FormationPermeability>(stimJob.JobInterval.ReservoirInterval.FormationPermeability);
                _db.StimJobFormationPermeabilitys.Update(obj);
            }
        }

        private void UpdateFormationPorosity(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval.FormationPorosity != null && stimJob.JobInterval.ReservoirInterval.FormationPorosity.Uom != null)
            {
                var obj = _mapper.Map<FormationPorosity>(stimJob.JobInterval.ReservoirInterval.FormationPorosity);
                _db.StimJobFormationPorositys.Update(obj);
            }
        }

        private void UpdateReservoirInterval(StimJob stimJob)
        {
            if (stimJob.JobInterval.ReservoirInterval != null && stimJob.JobInterval.ReservoirInterval.Uid != null)
            {
                var obj = _mapper.Map<ReservoirInterval>(stimJob.JobInterval.ReservoirInterval);
                _db.StimJobReservoirIntervals.Update(obj);
            }
        }

        private void UpdateMdPerforationsTop(StimJob stimJob)
        {
            if (stimJob.JobInterval.PerforationInterval.MdPerforationsTop != null && stimJob.JobInterval.PerforationInterval.MdPerforationsTop.Uom != null)
            {
                var obj = _mapper.Map<MdPerforationsTop>(stimJob.JobInterval.PerforationInterval.MdPerforationsTop);
                _db.StimJobMdPerforationsTops.Update(obj);
            }
        }

        private void UpdateMdPerforationsBottom(StimJob stimJob)
        {
            if (stimJob.JobInterval.PerforationInterval.MdPerforationsBottom != null && stimJob.JobInterval.PerforationInterval.MdPerforationsBottom.Uom != null)
            {
                var obj = _mapper.Map<MdPerforationsBottom>(stimJob.JobInterval.PerforationInterval.MdPerforationsBottom);
                _db.StimJobMdPerforationsBottoms.Update(obj);
            }
        }

        private void UpdateTvdPerforationsTop(StimJob stimJob)
        {
            if (stimJob.JobInterval.PerforationInterval.TvdPerforationsTop != null && stimJob.JobInterval.PerforationInterval.TvdPerforationsTop.Uom != null)
            {
                var obj = _mapper.Map<TvdPerforationsTop>(stimJob.JobInterval.PerforationInterval.TvdPerforationsTop);
                _db.StimJobTvdPerforationsTops.Update(obj);
            }
        }

        private void UpdateTvdPerforationsBottom(StimJob stimJob)
        {
            if (stimJob.JobInterval.PerforationInterval.TvdPerforationsBottom != null && stimJob.JobInterval.PerforationInterval.TvdPerforationsBottom.Uom != null)
            {
                var obj = _mapper.Map<TvdPerforationsBottom>(stimJob.JobInterval.PerforationInterval.TvdPerforationsBottom);
                _db.StimJobTvdPerforationsBottoms.Update(obj);
            }
        }

        private void UpdateSize(StimJob stimJob)
        {
            if (stimJob.JobInterval.PerforationInterval.Size != null && stimJob.JobInterval.PerforationInterval.Size.Uom != null)
            {
                var obj = _mapper.Map<Size>(stimJob.JobInterval.PerforationInterval.Size);
                _db.StimJobSizes.Update(obj);
            }
        }

        private void UpdateDensityPerforation(StimJob stimJob)
        {
            if (stimJob.JobInterval.PerforationInterval.DensityPerforation != null && stimJob.JobInterval.PerforationInterval.DensityPerforation.Uom != null)
            {
                var obj = _mapper.Map<DensityPerforation>(stimJob.JobInterval.PerforationInterval.DensityPerforation);
                _db.StimJobDensityPerforations.Update(obj);
            }
        }

        private void UpdatePhasingPerforation(StimJob stimJob)
        {
            if (stimJob.JobInterval.PerforationInterval.PhasingPerforation != null && stimJob.JobInterval.PerforationInterval.PhasingPerforation.Uom != null)
            {
                var obj = _mapper.Map<PhasingPerforation>(stimJob.JobInterval.PerforationInterval.PhasingPerforation);
                _db.StimJobPhasingPerforations.Update(obj);
            }
        }

        private void UpdatePerforationInterval(StimJob stimJob)
        {
            if (stimJob.JobInterval.PerforationInterval != null && stimJob.JobInterval.PerforationInterval.Uid != null)
            {
                var obj = _mapper.Map<PerforationInterval>(stimJob.JobInterval.PerforationInterval);
                _db.StimJobPerforationIntervals.Update(obj);
            }
        }

        private void UpdateJobInterval(StimJob stimJob)
        {
            if (stimJob.JobInterval != null && stimJob.JobInterval.Uid != null)
            {
                var obj = _mapper.Map<JobInterval>(stimJob.JobInterval);
                _db.StimJobJobIntervals.Update(obj);
            }
        }

        #endregion Update StimJob
    }
}
