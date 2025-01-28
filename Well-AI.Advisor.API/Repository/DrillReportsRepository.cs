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
using WellAI.Advisor.DLL.Data;

namespace Well_AI.Advisor.API.Repository
{
    public class DrillReportsRepository : IDrillReportsRepository
    {
        private readonly WellAIAdvisiorContext _db;
        private readonly IMapper _mapper;
        private ITenantRepository _tenantRepository;
        private readonly WebAIAdvisorContext _wdb;
        public DrillReportsRepository(WellAIAdvisiorContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper, ITenantRepository tenantRepository, WebAIAdvisorContext wdb)
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
        public bool DrillReportExists(string uid)
        {
            bool value = _db.DrillReports.Any(x => x.Uid.ToLower().Trim() == uid.ToLower().Trim());
            return value;
        }

        public bool CreateDrillReport(DrillReport drillReport)
        {
            try
            {
                WellAlias(drillReport);
                WellboreAlias(drillReport);
                Elevation(drillReport);
                WellDatum(drillReport);
                GeodeticCRS(drillReport);
                WellCRS(drillReport);
                RigAlias(drillReport);
                WellboreInfo(drillReport);
                Md(drillReport);
                Tvd(drillReport);
                MdPlugTop(drillReport);
                DiaHole(drillReport);
                MdDiaHoleStart(drillReport);
                DiaPilot(drillReport);
                MdDiaPilotPlan(drillReport);
                MdKickoff(drillReport);
                StrengthForm(drillReport);
                MdStrengthForm(drillReport);
                DiaCsgLast(drillReport);
                MdCsgLast(drillReport);
                DistDrill(drillReport);
                RopCurrent(drillReport);
                StatusInfo(drillReport);
                DiaBit(drillReport);
                BitRecord(drillReport);
                PresBopRating(drillReport);
                Density(drillReport);
                TempVis(drillReport);
                Pv(drillReport);
                Fluid(drillReport);
                EquivalentMudWeight(drillReport);
                PorePressure(drillReport);
                ExtendedReport(drillReport);
                Incl(drillReport);
                Azi(drillReport);
                SurveyStation(drillReport);
                Activity(drillReport);
                MdTop(drillReport);
                MdBottom(drillReport);
                TvdTop(drillReport);
                TvdBottom(drillReport);
                TempBHCT(drillReport);
                MdTempTool(drillReport);
                TvdTempTool(drillReport);
                LogInfo(drillReport);
                TempBHST(drillReport);
                ETimStatic(drillReport);
                LenRecovered(drillReport);
                RecoverPc(drillReport);
                LenBarrel(drillReport);
                CoreInfo(drillReport);
                ChokeOrificeSize(drillReport);
                DensityOil(drillReport);
                DensityWater(drillReport);
                DensityGas(drillReport);
                FlowRateOil(drillReport);
                FlowRateWater(drillReport);
                FlowRateGas(drillReport);
                PresShutIn(drillReport);
                PresFlowing(drillReport);
                PresBottom(drillReport);
                GasOilRatio(drillReport);
                WaterOilRatio(drillReport);
                Chloride(drillReport);
                CarbonDioxide(drillReport);
                HydrogenSulfide(drillReport);
                VolOilTotal(drillReport);
                WellTestInfo(drillReport);
                PresPore(drillReport);
                MdSample(drillReport);
                DensityHC(drillReport);
                VolumeSample(drillReport);
                FormTestInfo(drillReport);
                LithShowInfo(drillReport);
                ETimMissProduction(drillReport);
                EquipFailureInfo(drillReport);
                MdInflow(drillReport);
                TvdInflow(drillReport);
                ETimLost(drillReport);
                MdBit(drillReport);
                WtMud(drillReport);
                VolMudGained(drillReport);
                PresShutInCasing(drillReport);
                PresShutInDrill(drillReport);
                TempBottom(drillReport);
                PresMaxChoke(drillReport);
                ControlIncidentInfo(drillReport);
                StratInfo(drillReport);
                PerfInfo(drillReport);
                GasHigh(drillReport);
                GasLow(drillReport);
                Meth(drillReport);
                Eth(drillReport);
                Prop(drillReport);
                Ibut(drillReport);
                Nbut(drillReport);
                Ipent(drillReport);
                GasReadingInfo(drillReport);
                DefaultDatum(drillReport);
                CommonData(drillReport);
                _db.DrillReports.Add(drillReport);
                return Save();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "DrillReportsRepository CreateDrillReport", null);
                return Save();
            }
        }

        
        public bool DeleteDrillReport(DrillReport drillReport)
        {
            _db.DrillReports.Remove(drillReport);
            return Save();
        }

        public DrillReport GetDrillReportDetail(string Uid)
        {
            return _db.DrillReports.FirstOrDefault(x=>x.Uid==Uid);
            
        }

        public ICollection<DrillReport> GetDrillReportDetails()
        {
            return _db.DrillReports.OrderBy(x => x.NameWell).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateDrillReport(DrillReport drillReport)
        {
            try
            {
                UpdateWellAlias(drillReport);
                UpdateWellboreAlias(drillReport);
                UpdateElevation(drillReport);
                UpdateWellDatum(drillReport);
                UpdateGeodeticCRS(drillReport);
                UpdateWellCRS(drillReport);
                UpdateRigAlias(drillReport);
                UpdateWellboreInfo(drillReport);
                UpdateMd(drillReport);
                UpdateTvd(drillReport);
                UpdateMdPlugTop(drillReport);
                UpdateDiaHole(drillReport);
                UpdateMdDiaHoleStart(drillReport);
                UpdateDiaPilot(drillReport);
                UpdateMdDiaPilotPlan(drillReport);
                UpdateMdKickoff(drillReport);
                UpdateStrengthForm(drillReport);
                UpdateMdStrengthForm(drillReport);
                UpdateDiaCsgLast(drillReport);
                UpdateMdCsgLast(drillReport);
                UpdateDistDrill(drillReport);
                UpdateRopCurrent(drillReport);
                UpdateStatusInfo(drillReport);
                UpdateDiaBit(drillReport);
                UpdateBitRecord(drillReport);
                UpdatePresBopRating(drillReport);
                UpdateDensity(drillReport);
                UpdateTempVis(drillReport);
                UpdatePv(drillReport);
                UpdateFluid(drillReport);
                UpdateEquivalentMudWeight(drillReport);
                UpdatePorePressure(drillReport);
                UpdateExtendedReport(drillReport);
                UpdateIncl(drillReport);
                UpdateAzi(drillReport);
                UpdateSurveyStation(drillReport);
                UpdateActivity(drillReport);
                UpdateMdTop(drillReport);
                UpdateMdBottom(drillReport);
                UpdateTvdTop(drillReport);
                UpdateTvdBottom(drillReport);
                UpdateTempBHCT(drillReport);
                UpdateMdTempTool(drillReport);
                UpdateTvdTempTool(drillReport);
                UpdateLogInfo(drillReport);
                UpdateTempBHST(drillReport);
                UpdateETimStatic(drillReport);
                UpdateLenRecovered(drillReport);
                UpdateRecoverPc(drillReport);
                UpdateLenBarrel(drillReport);
                UpdateCoreInfo(drillReport);
                UpdateChokeOrificeSize(drillReport);
                UpdateDensityOil(drillReport);
                UpdateDensityWater(drillReport);
                UpdateDensityGas(drillReport);
                UpdateFlowRateOil(drillReport);
                UpdateFlowRateWater(drillReport);
                UpdateFlowRateGas(drillReport);
                UpdatePresShutIn(drillReport);
                UpdatePresFlowing(drillReport);
                UpdatePresBottom(drillReport);
                UpdateGasOilRatio(drillReport);
                UpdateWaterOilRatio(drillReport);
                UpdateChloride(drillReport);
                UpdateCarbonDioxide(drillReport);
                UpdateHydrogenSulfide(drillReport);
                UpdateVolOilTotal(drillReport);
                UpdateWellTestInfo(drillReport);
                UpdatePresPore(drillReport);
                UpdateMdSample(drillReport);
                UpdateDensityHC(drillReport);
                UpdateVolumeSample(drillReport);
                UpdateFormTestInfo(drillReport);
                UpdateLithShowInfo(drillReport);
                UpdateETimMissProduction(drillReport);
                UpdateEquipFailureInfo(drillReport);
                UpdateMdInflow(drillReport);
                UpdateTvdInflow(drillReport);
                UpdateETimLost(drillReport);
                UpdateMdBit(drillReport);
                UpdateWtMud(drillReport);
                UpdateVolMudGained(drillReport);
                UpdatePresShutInCasing(drillReport);
                UpdatePresShutInDrill(drillReport);
                UpdateTempBottom(drillReport);
                UpdatePresMaxChoke(drillReport);
                UpdateControlIncidentInfo(drillReport);
                UpdateStratInfo(drillReport);
                UpdatePerfInfo(drillReport);
                UpdateGasHigh(drillReport);
                UpdateGasLow(drillReport);
                UpdateMeth(drillReport);
                UpdateEth(drillReport);
                UpdateProp(drillReport);
                UpdateIbut(drillReport);
                UpdateNbut(drillReport);
                UpdateIpent(drillReport);
                UpdateGasReadingInfo(drillReport);
                UpdateDefaultDatum(drillReport);
                UpdateCommonData(drillReport);
                _db.DrillReports.Update(drillReport);
                return Save();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "DrillReportsRepository UpdateDrillReport", null);
                return Save();
            }
        }

        #region Insert DrillReport
        private void WellAlias(DrillReport drillReport)
        {
            if (drillReport.WellAlias.WellAliasId != 0)
            {
                var obj = _mapper.Map<DrillReportWellAlias>(drillReport.WellAlias);
                _db.DrillReportWellAlias.Add(obj);
            }
        }
        private void WellboreAlias(DrillReport drillReport)
        {
            foreach (var item in drillReport.WellboreAlias)
            {
                if (item.Uid != null)
                {
                    var obj = _mapper.Map<DrillReportWellboreAlias>(item);
                    _db.DrillReportWellboreAlias.Add(obj);
                }
            }
        }
        private void Elevation(DrillReport drillReport)
        {
            foreach (var item in drillReport.WellDatum)
            {
                if (item.Elevation !=null && item.Elevation.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportElevation>(item.Elevation);
                    _db.DrillReportElevation.Add(obj);
                }
            }
        }
        private void WellDatum(DrillReport drillReport)
        {
            foreach (var item in drillReport.WellDatum)
            {
                if (item.Uid != null)
                {
                    var obj = _mapper.Map<DrillReportWellDatum>(item);
                    _db.DrillReportWellDatum.Add(obj);
                }
            }
        }
        private void GeodeticCRS(DrillReport drillReport)
        {
            
                if (drillReport.WellCRS.GeodeticCRS.UidRef != null)
                {
                    var obj = _mapper.Map<DrillReportGeodeticCRS>(drillReport.WellCRS.GeodeticCRS);
                    _db.DrillReportGeodeticCRS.Add(obj);
            }
        }
        private void WellCRS(DrillReport drillReport)
        {

            if (drillReport.WellCRS.Uid != null)
            {
                var obj = _mapper.Map<DrillReportWellCRS> (drillReport.WellCRS);
                _db.DrillReportWellCR.Add(obj);
            }
        }
        private void RigAlias(DrillReport drillReport)
        {
            foreach (var item in drillReport.WellboreInfo.RigAlias)
            {
                if (item.Uid != null)
                {
                    var obj = _mapper.Map<DrillReportRigAlias>(item);
                    _db.DrillReportRigAlias.Add(obj);
                }
            }
        }
        private void WellboreInfo(DrillReport drillReport)
        {
                if (drillReport.WellboreInfo != null)
                {
                    var obj = _mapper.Map<DrillReportWellboreInfo>(drillReport.WellboreInfo);
                    _db.DrillReportWellboreInfo.Add(obj);
                }
        }
        private void Md(DrillReport drillReport)
        {
            if (drillReport.StatusInfo.Md.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMd>(drillReport.StatusInfo.Md);
                _db.DrillReportMd.Add(obj);
            }
            foreach (var item in drillReport.PorePressure)
            {
                if (item.Md.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportMd>(item.Md);
                    _db.DrillReportMd.Add(obj);
                }
            }

            if (drillReport.SurveyStation.Md.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMd>(drillReport.SurveyStation.Md);
                _db.DrillReportMd.Add(obj);
            }
            foreach (var item in drillReport.Activity)
            {
                if (item.Md.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportMd>(item.Md);
                    _db.DrillReportMd.Add(obj);
                }
            }
            if (drillReport.FormTestInfo.Md.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMd>(drillReport.FormTestInfo.Md);
                _db.DrillReportMd.Add(obj);
            }
            if (drillReport.EquipFailureInfo.Md.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMd>(drillReport.EquipFailureInfo.Md);
                _db.DrillReportMd.Add(obj);
            }
        }
        private void Tvd(DrillReport drillReport)
        {
            if (drillReport.StatusInfo.Tvd.Uom != null)
            {
                var obj = _mapper.Map<DrillReportTvd>(drillReport.StatusInfo.Tvd);
                _db.DrillReportTvd.Add(obj);
            }
            foreach (var item in drillReport.PorePressure)
            {
                if (item.Tvd.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportTvd>(item.Tvd);
                    _db.DrillReportTvd.Add(obj);
                }
            }
            if (drillReport.SurveyStation.Tvd.Uom != null)
            {
                var obj = _mapper.Map<DrillReportTvd>(drillReport.SurveyStation.Tvd);
                _db.DrillReportTvd.Add(obj);
            }
            foreach (var item in drillReport.Activity)
            {
                if (item.Tvd.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportTvd>(item.Tvd);
                    _db.DrillReportTvd.Add(obj);
                }
            }

            if (drillReport.FormTestInfo.Tvd.Uom != null)
            {
                var obj = _mapper.Map<DrillReportTvd>(drillReport.FormTestInfo.Tvd);
                _db.DrillReportTvd.Add(obj);
            }
        }
        private void MdPlugTop(DrillReport drillReport)
        {
            if (drillReport.StatusInfo.MdPlugTop.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdPlugTop>(drillReport.StatusInfo.MdPlugTop);
                _db.DrillReportMdPlugTop.Add(obj);
            }
        }
        private void DiaHole(DrillReport drillReport)
        {
            if (drillReport.StatusInfo.DiaHole.Uom != null)
            {
                var obj = _mapper.Map<DrillReportDiaHole>(drillReport.StatusInfo.DiaHole);
                _db.DrillReportDiaHole.Add(obj);
            }
        }
        private void MdDiaHoleStart(DrillReport drillReport)
        {
            if (drillReport.StatusInfo.MdDiaHoleStart.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdDiaHoleStart>(drillReport.StatusInfo.MdDiaHoleStart);
                _db.DrillReportMdDiaHoleStart.Add(obj);
            }
        }
        private void DiaPilot(DrillReport drillReport)
        {
            if (drillReport.StatusInfo.DiaPilot.Uom != null)
            {
                var obj = _mapper.Map<DrillReportDiaPilot>(drillReport.StatusInfo.DiaPilot);
                _db.DrillReportDiaPilot.Add(obj);
            }
        }
        private void MdDiaPilotPlan(DrillReport drillReport)
        {
            if (drillReport.StatusInfo.MdDiaPilotPlan.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdDiaPilotPlan>(drillReport.StatusInfo.MdDiaPilotPlan);
                _db.DrillReportMdDiaPilotPlan.Add(obj);
            }
        }
        private void MdKickoff(DrillReport drillReport)
        {
            if (drillReport.StatusInfo.MdKickoff.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdKickoff>(drillReport.StatusInfo.MdKickoff);
                _db.DrillReportMdKickoff.Add(obj);
            }
        }
        private void StrengthForm(DrillReport drillReport)
        {
            if (drillReport.StatusInfo.StrengthForm.Uom != null)
            {
                var obj = _mapper.Map<DrillReportStrengthForm>(drillReport.StatusInfo.StrengthForm);
                _db.DrillReportStrengthForm.Add(obj);
            }
        }
        private void MdStrengthForm(DrillReport drillReport)
        {
            if (drillReport.StatusInfo.MdStrengthForm.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdStrengthForm>(drillReport.StatusInfo.MdStrengthForm);
                _db.DrillReportMdStrengthForm.Add(obj);
            }
        }
        private void DiaCsgLast(DrillReport drillReport)
        {
            if (drillReport.StatusInfo.DiaCsgLast.Uom != null)
            {
                var obj = _mapper.Map<DrillReportDiaCsgLast>(drillReport.StatusInfo.DiaCsgLast);
                _db.DrillReportDiaCsgLast.Add(obj);
            }
        }
        private void MdCsgLast(DrillReport drillReport)
        {
            if (drillReport.StatusInfo.MdCsgLast.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdCsgLast>(drillReport.StatusInfo.MdCsgLast);
                _db.DrillReportMdCsgLast.Add(obj);
            }
        }
        private void DistDrill(DrillReport drillReport)
        {
            if (drillReport.StatusInfo.DistDrill.Uom != null)
            {
                var obj = _mapper.Map<DrillReportDistDrill>(drillReport.StatusInfo.DistDrill);
                _db.DrillReportDistDrill.Add(obj);
            }
        }
        private void RopCurrent(DrillReport drillReport)
        {
            if (drillReport.StatusInfo.RopCurrent.Uom != null)
            {
                var obj = _mapper.Map<DrillReportRopCurrent>(drillReport.StatusInfo.RopCurrent);
                _db.DrillReportRopCurrent.Add(obj);
            }
        }
        private void StatusInfo(DrillReport drillReport)
        {
            if (drillReport.StatusInfo.Uid != null)
            {
                var obj = _mapper.Map<DrillReportStatusInfo>(drillReport.StatusInfo);
                _db.DrillReportStatusInfo.Add(obj);
            }
        }
        private void DiaBit(DrillReport drillReport)
        {
            if (drillReport.BitRecord.DiaBit.Uom != null)
            {
                var obj = _mapper.Map<DrillReportDiaBit>(drillReport.BitRecord.DiaBit);
                _db.DrillReportDiaBit.Add(obj);
            }
        }
        private void BitRecord(DrillReport drillReport)
        {
            if (drillReport.BitRecord.Uid != null)
            {
                var obj = _mapper.Map<DrillReportBitRecord>(drillReport.BitRecord);
                _db.DrillReportBitRecord.Add(obj);
            }
        }
        private void PresBopRating(DrillReport drillReport)
        {
            foreach (var item in drillReport.Fluid)
            {
                if (item.PresBopRating.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportPresBopRating>(item.PresBopRating);
                    _db.DrillReportPresBopRating.Add(obj);
                }
            }
        }
        private void Density(DrillReport drillReport)
        {
            foreach (var item in drillReport.Fluid)
            {
                if (item.Density.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportDensity>(item.Density);
                    _db.DrillReportDensity.Add(obj);
                }
            }
        }
        private void TempVis(DrillReport drillReport)
        {
            foreach (var item in drillReport.Fluid)
            {
                if (item.TempVis.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportTempVis>(item.TempVis);
                    _db.DrillReportTempVis.Add(obj);
                }
            }
        }
        private void Pv(DrillReport drillReport)
        {
            foreach (var item in drillReport.Fluid)
            {
                if (item.Pv.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportPv>(item.Pv);
                    _db.DrillReportPv.Add(obj);
                }
            }
        }
        private void Fluid(DrillReport drillReport)
        {
            foreach (var item in drillReport.Fluid)
            {
                if (item.Uid != null)
                {
                    var obj = _mapper.Map<DrillReportFluid>(item);
                    _db.DrillReportFluids.Add(obj);
                }
            }
        }
        private void EquivalentMudWeight(DrillReport drillReport)
        {
            foreach (var item in drillReport.PorePressure)
            {
                if (item.EquivalentMudWeight.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportEquivalentMudWeight>(item.EquivalentMudWeight);
                    _db.DrillReportEquivalentMudWeight.Add(obj);
                }
            }
        }
        private void PorePressure(DrillReport drillReport)
        {
            foreach (var item in drillReport.PorePressure)
            {
                if (item.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportPorePressure>(item);
                    _db.DrillReportPorePressure.Add(obj);
                }
            }
        }
        private void ExtendedReport(DrillReport drillReport)
        {
                if (drillReport.ExtendedReport.DTim != null)
                {
                    var obj = _mapper.Map<DrillReportExtendedReport>(drillReport.ExtendedReport);
                    _db.DrillReportExtendedReport.Add(obj);
                }
        }
        private void Incl(DrillReport drillReport)
        {
            if (drillReport.SurveyStation.Incl.Uom != null)
            {
                var obj = _mapper.Map<DrillReportIncl>(drillReport.SurveyStation.Incl);
                _db.DrillReportIncl.Add(obj);
            }
        }
        private void Azi(DrillReport drillReport)
        {
            if (drillReport.SurveyStation.Azi.Uom != null)
            {
                var obj = _mapper.Map<DrillReportAzi>(drillReport.SurveyStation.Azi);
                _db.DrillReportAzi.Add(obj);
            }
        }
        private void SurveyStation(DrillReport drillReport)
        {
            if (drillReport.SurveyStation.DTim != null)
            {
                var obj = _mapper.Map<DrillReportSurveyStation>(drillReport.SurveyStation);
                _db.DrillReportSurveyStation.Add(obj);
            }
        }
        private void Activity(DrillReport drillReport)
        {
            foreach (var item in drillReport.Activity)
            {

                if (item.Uid != null)
                {
                    var obj = _mapper.Map<DrillReportActivity>(item);
                    _db.DrillReportActivity.Add(obj);
                }
            }
        }
        private void MdTop(DrillReport drillReport)
        {
            foreach (var item in drillReport.LogInfo)
            {
                if (item.MdTop.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportMdTop>(item.MdTop);
                    _db.DrillReportMdTop.Add(obj);
                }
            }
            if (drillReport.CoreInfo.MdTop.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdTop>(drillReport.CoreInfo.MdTop);
                _db.DrillReportMdTop.Add(obj);
            }

            if (drillReport.WellTestInfo.MdTop.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdTop>(drillReport.WellTestInfo.MdTop);
                _db.DrillReportMdTop.Add(obj);
            }

            if (drillReport.LithShowInfo.MdTop.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdTop>(drillReport.LithShowInfo.MdTop);
                _db.DrillReportMdTop.Add(obj);
            }
            if (drillReport.StratInfo.MdTop.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdTop>(drillReport.StratInfo.MdTop);
                _db.DrillReportMdTop.Add(obj);
            }
            if (drillReport.PerfInfo.MdTop.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdTop>(drillReport.PerfInfo.MdTop);
                _db.DrillReportMdTop.Add(obj);
            }
            if (drillReport.GasReadingInfo.MdTop.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdTop>(drillReport.GasReadingInfo.MdTop);
                _db.DrillReportMdTop.Add(obj);
            }
        }
        private void MdBottom(DrillReport drillReport)
        {
            foreach (var item in drillReport.LogInfo)
            {
                if (item.MdBottom.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportMdBottom>(item.MdBottom);
                    _db.DrillReportMdBottom.Add(obj);
                }
            }
            if (drillReport.CoreInfo.MdBottom.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdBottom>(drillReport.CoreInfo.MdBottom);
                _db.DrillReportMdBottom.Add(obj);
            }
            if (drillReport.WellTestInfo.MdBottom.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdBottom>(drillReport.WellTestInfo.MdBottom);
                _db.DrillReportMdBottom.Add(obj);
            }
            if (drillReport.LithShowInfo.MdBottom.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdBottom>(drillReport.LithShowInfo.MdBottom);
                _db.DrillReportMdBottom.Add(obj);
            }
           
            if (drillReport.PerfInfo.MdBottom.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdBottom>(drillReport.PerfInfo.MdBottom);
                _db.DrillReportMdBottom.Add(obj);
            }
            if (drillReport.GasReadingInfo.MdBottom.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdBottom>(drillReport.GasReadingInfo.MdBottom);
                _db.DrillReportMdBottom.Add(obj);
            }
        }
        private void TvdTop(DrillReport drillReport)
        {
            foreach (var item in drillReport.LogInfo)
            {
                if (item.TvdTop.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportTvdTop>(item.TvdTop);
                    _db.DrillReportTvdTop.Add(obj);
                }
            }
            if (drillReport.CoreInfo.TvdTop.Uom != null)
            {
                var obj = _mapper.Map<DrillReportTvdTop>(drillReport.CoreInfo.TvdTop);
                _db.DrillReportTvdTop.Add(obj);
            }
            if (drillReport.WellTestInfo.TvdTop.Uom != null)
            {
                var obj = _mapper.Map<DrillReportTvdTop>(drillReport.WellTestInfo.TvdTop);
                _db.DrillReportTvdTop.Add(obj);
            }
            if (drillReport.LithShowInfo.TvdTop.Uom != null)
            {
                var obj = _mapper.Map<DrillReportTvdTop>(drillReport.LithShowInfo.TvdTop);
                _db.DrillReportTvdTop.Add(obj);
            }
            if (drillReport.StratInfo.TvdTop.Uom != null)
            {
                var obj = _mapper.Map<DrillReportTvdTop>(drillReport.StratInfo.TvdTop);
                _db.DrillReportTvdTop.Add(obj);
            }
            if (drillReport.PerfInfo.TvdTop.Uom != null)
            {
                var obj = _mapper.Map<DrillReportTvdTop>(drillReport.PerfInfo.TvdTop);
                _db.DrillReportTvdTop.Add(obj);
            }
            if (drillReport.GasReadingInfo.TvdTop.Uom != null)
            {
                var obj = _mapper.Map<DrillReportTvdTop>(drillReport.GasReadingInfo.TvdTop);
                _db.DrillReportTvdTop.Add(obj);
            }
        }
        private void TvdBottom(DrillReport drillReport)
        {
            foreach (var item in drillReport.LogInfo)
            {
                if (item.TvdBottom.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportTvdBottom>(item.TvdBottom);
                    _db.DrillReportTvdBottom.Add(obj);
                }
            }
            if (drillReport.CoreInfo.TvdBottom.Uom != null)
            {
                var obj = _mapper.Map<DrillReportTvdBottom>(drillReport.CoreInfo.TvdBottom);
                _db.DrillReportTvdBottom.Add(obj);
            }
            if (drillReport.WellTestInfo.TvdBottom.Uom != null)
            {
                var obj = _mapper.Map<DrillReportTvdBottom>(drillReport.WellTestInfo.TvdBottom);
                _db.DrillReportTvdBottom.Add(obj);
            }
            if (drillReport.LithShowInfo.TvdBottom.Uom != null)
            {
                var obj = _mapper.Map<DrillReportTvdBottom>(drillReport.LithShowInfo.TvdBottom);
                _db.DrillReportTvdBottom.Add(obj);
            }
            if (drillReport.PerfInfo.TvdBottom.Uom != null)
            {
                var obj = _mapper.Map<DrillReportTvdBottom>(drillReport.PerfInfo.TvdBottom);
                _db.DrillReportTvdBottom.Add(obj);
            }
            if (drillReport.GasReadingInfo.TvdBottom.Uom != null)
            {
                var obj = _mapper.Map<DrillReportTvdBottom>(drillReport.GasReadingInfo.TvdBottom);
                _db.DrillReportTvdBottom.Add(obj);
            }
        }
        private void TempBHCT(DrillReport drillReport)
        {
            foreach (var item in drillReport.LogInfo)
            {
                if (item.TempBHCT!=null && item.TempBHCT.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportTempBHCT>(item.TempBHCT);
                    _db.DrillReportTempBHCT.Add(obj);
                }
            }
        }
        private void MdTempTool(DrillReport drillReport)
        {
            foreach (var item in drillReport.LogInfo)
            {
                if (item.MdTempTool!=null && item.MdTempTool.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportMdTempTool>(item.MdTempTool);
                    _db.DrillReportMdTempTool.Add(obj);
                }
            }
        }
        private void TvdTempTool(DrillReport drillReport)
        {
            foreach (var item in drillReport.LogInfo)
            {
                if (item.TvdTempTool!=null && item.TvdTempTool.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportTvdTempTool>(item.TvdTempTool);
                    _db.DrillReportTvdTempTool.Add(obj);
                }
            }
        }
        private void TempBHST(DrillReport drillReport)
        {
            foreach (var item in drillReport.LogInfo)
            {
                if (item.TempBHST!=null && item.TempBHST.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportTempBHST>(item.TempBHST);
                    _db.DrillReportTempBHST.Add(obj);
                }
            }
        }
        private void ETimStatic(DrillReport drillReport)
        {
            foreach (var item in drillReport.LogInfo)
            {
                if (item.ETimStatic!=null && item.ETimStatic.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportETimStatic>(item.ETimStatic);
                    _db.DrillReportETimStatic.Add(obj);
                }
            }
        }
        private void LogInfo(DrillReport drillReport)
        {
            foreach (var item in drillReport.LogInfo)
            {
                if (item.Uid != null)
                {
                    var obj = _mapper.Map<DrillReportLogInfo>(item);
                    _db.DrillReportLogInfo.Add(obj);
                }
            }
        }

        private void LenRecovered(DrillReport drillReport)
        {
            
                if (drillReport.CoreInfo.LenRecovered.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportLenRecovered>(drillReport.CoreInfo.LenRecovered);
                    _db.DrillReportLenRecovered.Add(obj);
                }
        }
        private void RecoverPc(DrillReport drillReport)
        {

            if (drillReport.CoreInfo.RecoverPc.Uom != null)
            {
                var obj = _mapper.Map<DrillReportRecoverPc>(drillReport.CoreInfo.RecoverPc);
                _db.DrillReportRecoverPc.Add(obj);
            }
        }
        private void LenBarrel(DrillReport drillReport)
        {

            if (drillReport.CoreInfo.LenBarrel.Uom != null)
            {
                var obj = _mapper.Map<DrillReportLenBarrel>(drillReport.CoreInfo.LenBarrel);
                _db.DrillReportLenBarrel.Add(obj);
            }
        }
        private void CoreInfo(DrillReport drillReport)
        {

            if (drillReport.CoreInfo.Uid != null)
            {
                var obj = _mapper.Map<DrillReportCoreInfo>(drillReport.CoreInfo);
                _db.DrillReportCoreInfo.Add(obj);
            }
        }
        private void ChokeOrificeSize(DrillReport drillReport)
        {

            if (drillReport.WellTestInfo.ChokeOrificeSize.Uom != null)
            {
                var obj = _mapper.Map<DrillReportChokeOrificeSize>(drillReport.WellTestInfo.ChokeOrificeSize);
                _db.DrillReportChokeOrificeSize.Add(obj);
            }
        }
        private void DensityOil(DrillReport drillReport)
        {

            if (drillReport.WellTestInfo.DensityOil.Uom != null)
            {
                var obj = _mapper.Map<DrillReportDensityOil>(drillReport.WellTestInfo.DensityOil);
                _db.DrillReportDensityOil.Add(obj);
            }
        }
        private void DensityWater(DrillReport drillReport)
        {

            if (drillReport.WellTestInfo.DensityWater.Uom != null)
            {
                var obj = _mapper.Map<DrillReportDensityWater>(drillReport.WellTestInfo.DensityWater);
                _db.DrillReportDensityWater.Add(obj);
            }
        }
        private void DensityGas(DrillReport drillReport)
        {

            if (drillReport.WellTestInfo.DensityGas.Uom != null)
            {
                var obj = _mapper.Map<DrillReportDensityGas>(drillReport.WellTestInfo.DensityGas);
                _db.DrillReportDensityGas.Add(obj);
            }
        }
        private void FlowRateOil(DrillReport drillReport)
        {

            if (drillReport.WellTestInfo.FlowRateOil.Uom != null)
            {
                var obj = _mapper.Map<DrillReportFlowRateOil>(drillReport.WellTestInfo.FlowRateOil);
                _db.DrillReportFlowRateOil.Add(obj);
            }
        }
        private void FlowRateWater(DrillReport drillReport)
        {

            if (drillReport.WellTestInfo.FlowRateWater.Uom != null)
            {
                var obj = _mapper.Map<DrillReportFlowRateWater>(drillReport.WellTestInfo.FlowRateWater);
                _db.DrillReportFlowRateWater.Add(obj);
            }
        }
        private void FlowRateGas(DrillReport drillReport)
        {

            if (drillReport.WellTestInfo.FlowRateGas.Uom != null)
            {
                var obj = _mapper.Map<DrillReportFlowRateGas>(drillReport.WellTestInfo.FlowRateGas);
                _db.DrillReportFlowRateGas.Add(obj);
            }
        }
        private void PresShutIn(DrillReport drillReport)
        {
            if (drillReport.WellTestInfo.PresShutIn.Uom != null)
            {
                var obj = _mapper.Map<DrillReportPresShutIn>(drillReport.WellTestInfo.PresShutIn);
                _db.DrillReportPresShutIn.Add(obj);
            }
        }
        private void PresFlowing(DrillReport drillReport)
        {
            if (drillReport.WellTestInfo.PresFlowing.Uom != null)
            {
                var obj = _mapper.Map<DrillReportPresFlowing>(drillReport.WellTestInfo.PresFlowing);
                _db.DrillReportPresFlowing.Add(obj);
            }
        }
        private void PresBottom(DrillReport drillReport)
        {
            if (drillReport.WellTestInfo.PresBottom.Uom != null)
            {
                var obj = _mapper.Map<DrillReportPresBottom>(drillReport.WellTestInfo.PresBottom);
                _db.DrillReportPresBottom.Add(obj);
            }
        }
        private void GasOilRatio(DrillReport drillReport)
        {
            if (drillReport.WellTestInfo.GasOilRatio.Uom != null)
            {
                var obj = _mapper.Map<DrillReportGasOilRatio>(drillReport.WellTestInfo.GasOilRatio);
                _db.DrillReportGasOilRatio.Add(obj);
            }
        }
        private void WaterOilRatio(DrillReport drillReport)
        {
            if (drillReport.WellTestInfo.WaterOilRatio.Uom != null)
            {
                var obj = _mapper.Map<DrillReportWaterOilRatio>(drillReport.WellTestInfo.WaterOilRatio);
                _db.DrillReportWaterOilRatio.Add(obj);
            }
        }
        private void Chloride(DrillReport drillReport)
        {
            if (drillReport.WellTestInfo.Chloride.Uom != null)
            {
                var obj = _mapper.Map<DrillReportChloride>(drillReport.WellTestInfo.Chloride);
                _db.DrillReportChloride.Add(obj);
            }
        }
        private void CarbonDioxide(DrillReport drillReport)
        {
            if (drillReport.WellTestInfo.CarbonDioxide.Uom != null)
            {
                var obj = _mapper.Map<DrillReportCarbonDioxide>(drillReport.WellTestInfo.CarbonDioxide);
                _db.DrillReportCarbonDioxide.Add(obj);
            }
        }
        private void HydrogenSulfide(DrillReport drillReport)
        {
            if (drillReport.WellTestInfo.HydrogenSulfide.Uom != null)
            {
                var obj = _mapper.Map<DrillReportHydrogenSulfide>(drillReport.WellTestInfo.HydrogenSulfide);
                _db.DrillReportHydrogenSulfide.Add(obj);
            }
        }
        private void VolOilTotal(DrillReport drillReport)
        {
            if (drillReport.WellTestInfo.VolOilTotal.Uom != null)
            {
                var obj = _mapper.Map<DrillReportVolOilTotal>(drillReport.WellTestInfo.VolOilTotal);
                _db.DrillReportVolOilTotal.Add(obj);
            }
        }
        private void VolGasTotal(DrillReport drillReport)
        {
            if (drillReport.WellTestInfo.VolGasTotal.Uom != null)
            {
                var obj = _mapper.Map<DrillReportVolGasTotal>(drillReport.WellTestInfo.VolGasTotal);
                _db.DrillReportVolGasTotal.Add(obj);
            }
        }
        private void VolWaterTotal(DrillReport drillReport)
        {
            if (drillReport.WellTestInfo.VolWaterTotal.Uom != null)
            {
                var obj = _mapper.Map<DrillReportVolWaterTotal>(drillReport.WellTestInfo.VolWaterTotal);
                _db.DrillReportVolWaterTotal.Add(obj);
            }
        }
        private void VolOilStored(DrillReport drillReport)
        {
            if (drillReport.WellTestInfo.VolOilStored.Uom != null)
            {
                var obj = _mapper.Map<DrillReportVolOilStored>(drillReport.WellTestInfo.VolOilStored);
                _db.DrillReportVolOilStored.Add(obj);
            }
        }
        private void WellTestInfo(DrillReport drillReport)
        {
            if (drillReport.WellTestInfo.Uid != null)
            {
                var obj = _mapper.Map<DrillReportWellTestInfo>(drillReport.WellTestInfo);
                _db.DrillReportWellTestInfo.Add(obj);
            }
        }
        private void PresPore(DrillReport drillReport)
        {
            if (drillReport.FormTestInfo.PresPore.Uom != null)
            {
                var obj = _mapper.Map<DrillReportPresPore>(drillReport.FormTestInfo.PresPore);
                _db.DrillReportPresPore.Add(obj);
            }
        }
        private void MdSample(DrillReport drillReport)
        {
            if (drillReport.FormTestInfo.MdSample.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdSample>(drillReport.FormTestInfo.MdSample);
                _db.DrillReportMdSample.Add(obj);
            }
        }
        private void DensityHC(DrillReport drillReport)
        {
            if (drillReport.FormTestInfo.DensityHC.Uom != null)
            {
                var obj = _mapper.Map<DrillReportDensityHC>(drillReport.FormTestInfo.DensityHC);
                _db.DrillReportDensityHC.Add(obj);
            }
        }
        private void VolumeSample(DrillReport drillReport)
        {
            if (drillReport.FormTestInfo.VolumeSample.Uom != null)
            {
                var obj = _mapper.Map<DrillReportVolumeSample>(drillReport.FormTestInfo.VolumeSample);
                _db.DrillReportVolumeSample.Add(obj);
            }
        }
        private void FormTestInfo(DrillReport drillReport)
        {
            if (drillReport.FormTestInfo.Uid != null)
            {
                var obj = _mapper.Map<DrillReportFormTestInfo>(drillReport.FormTestInfo);
                _db.DrillReportFormTestInfo.Add(obj);
            }
        }
        private void LithShowInfo(DrillReport drillReport)
        {
            if (drillReport.LithShowInfo.Uid != null)
            {
                var obj = _mapper.Map<DrillReportLithShowInfo>(drillReport.LithShowInfo);
                _db.DrillReportLithShowInfo.Add(obj);
            }
        }
        private void ETimMissProduction(DrillReport drillReport)
        {
            if (drillReport.EquipFailureInfo.ETimMissProduction.Uom != null)
            {
                var obj = _mapper.Map<DrillReportETimMissProduction>(drillReport.EquipFailureInfo.ETimMissProduction);
                _db.DrillReportETimMissProduction.Add(obj);
            }
        }
        private void EquipFailureInfo(DrillReport drillReport)
        {
            if (drillReport.EquipFailureInfo.Uid != null)
            {
                var obj = _mapper.Map<DrillReportEquipFailureInfo>(drillReport.EquipFailureInfo);
                _db.DrillReportEquipFailureInfo.Add(obj);
            }
        }
        private void MdInflow(DrillReport drillReport)
        {
            if (drillReport.ControlIncidentInfo.MdInflow.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdInflow>(drillReport.ControlIncidentInfo.MdInflow);
                _db.DrillReportMdInflow.Add(obj);
            }
        }
        private void TvdInflow(DrillReport drillReport)
        {
            if (drillReport.ControlIncidentInfo.TvdInflow.Uom != null)
            {
                var obj = _mapper.Map<DrillReportTvdInflow>(drillReport.ControlIncidentInfo.TvdInflow);
                _db.DrillReportTvdInflow.Add(obj);
            }
        }
        private void ETimLost(DrillReport drillReport)
        {
            if (drillReport.ControlIncidentInfo.ETimLost.Uom != null)
            {
                var obj = _mapper.Map<DrillReportETimLost>(drillReport.ControlIncidentInfo.ETimLost);
                _db.DrillReportETimLost.Add(obj);
            }
        }
        private void MdBit(DrillReport drillReport)
        {
            if (drillReport.ControlIncidentInfo.MdBit.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdBit>(drillReport.ControlIncidentInfo.MdBit);
                _db.DrillReportMdBit.Add(obj);
            }
        }
        private void WtMud(DrillReport drillReport)
        {
            if (drillReport.ControlIncidentInfo.WtMud.Uom != null)
            {
                var obj = _mapper.Map<DrillReportWtMud>(drillReport.ControlIncidentInfo.WtMud);
                _db.DrillReportWtMuds.Add(obj);
            }
        }
        private void VolMudGained(DrillReport drillReport)
        {
            if (drillReport.ControlIncidentInfo.VolMudGained.Uom != null)
            {
                var obj = _mapper.Map<DrillReportVolMudGained>(drillReport.ControlIncidentInfo.VolMudGained);
                _db.DrillReportVolMudGained.Add(obj);
            }
        }
        private void PresShutInCasing(DrillReport drillReport)
        {
            if (drillReport.ControlIncidentInfo.PresShutInCasing.Uom != null)
            {
                var obj = _mapper.Map<DrillReportPresShutInCasing>(drillReport.ControlIncidentInfo.PresShutInCasing);
                _db.DrillReportPresShutInCasing.Add(obj);
            }
        }
        private void PresShutInDrill(DrillReport drillReport)
        {
            if (drillReport.ControlIncidentInfo.PresShutInDrill.Uom != null)
            {
                var obj = _mapper.Map<DrillReportPresShutInDrill>(drillReport.ControlIncidentInfo.PresShutInDrill);
                _db.DrillReportPresShutInDrill.Add(obj);
            }
        }
        private void TempBottom(DrillReport drillReport)
        {
            if (drillReport.ControlIncidentInfo.TempBottom.Uom != null)
            {
                var obj = _mapper.Map<DrillReportTempBottom>(drillReport.ControlIncidentInfo.TempBottom);
                _db.DrillReportTempBottom.Add(obj);
            }
        }
        private void PresMaxChoke(DrillReport drillReport)
        {
            if (drillReport.ControlIncidentInfo.PresMaxChoke.Uom != null)
            {
                var obj = _mapper.Map<DrillReportPresMaxChoke>(drillReport.ControlIncidentInfo.PresMaxChoke);
                _db.DrillReportPresMaxChoke.Add(obj);
            }
        }
        private void ControlIncidentInfo(DrillReport drillReport)
        {
            if (drillReport.ControlIncidentInfo.Uid != null)
            {
                var obj = _mapper.Map<DrillReportControlIncidentInfo>(drillReport.ControlIncidentInfo);
                _db.DrillReportControlIncidentInfo.Add(obj);
            }
        }
        private void StratInfo(DrillReport drillReport)
        {
            if (drillReport.StratInfo.Uid != null)
            {
                var obj = _mapper.Map<DrillReportStratInfo>(drillReport.StratInfo);
                _db.DrillReportStratInfo.Add(obj);
            }
        }
        private void PerfInfo(DrillReport drillReport)
        {
            if (drillReport.PerfInfo.Uid != null)
            {
                var obj = _mapper.Map<DrillReportPerfInfo>(drillReport.PerfInfo);
                _db.DrillReportPerfInfo.Add(obj);
            }
        }
        private void GasHigh(DrillReport drillReport)
        {
            if (drillReport.GasReadingInfo.GasHigh.Uom != null)
            {
                var obj = _mapper.Map<DrillReportGasHigh>(drillReport.GasReadingInfo.GasHigh);
                _db.DrillReportGasHigh.Add(obj);
            }
        }
        private void GasLow(DrillReport drillReport)
        {
            if (drillReport.GasReadingInfo.GasLow.Uom != null)
            {
                var obj = _mapper.Map<DrillReportGasLow>(drillReport.GasReadingInfo.GasLow);
                _db.DrillReportGasLow.Add(obj);
            }
        }
        private void Meth(DrillReport drillReport)
        {
            if (drillReport.GasReadingInfo.Meth.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMeth>(drillReport.GasReadingInfo.Meth);
                _db.DrillReportMeth.Add(obj);
            }
        }
        private void Eth(DrillReport drillReport)
        {
            if (drillReport.GasReadingInfo.Eth.Uom != null)
            {
                var obj = _mapper.Map<DrillReportEth>(drillReport.GasReadingInfo.Eth);
                _db.DrillReportEth.Add(obj);
            }
        }
        private void Prop(DrillReport drillReport)
        {
            if (drillReport.GasReadingInfo.Prop.Uom != null)
            {
                var obj = _mapper.Map<DrillReportProp>(drillReport.GasReadingInfo.Prop);
                _db.DrillReportProp.Add(obj);
            }
        }
        private void Ibut(DrillReport drillReport)
        {
            if (drillReport.GasReadingInfo.Ibut.Uom != null)
            {
                var obj = _mapper.Map<DrillReportIbut>(drillReport.GasReadingInfo.Ibut);
                _db.DrillReportIbut.Add(obj);
            }
        }
        private void Nbut(DrillReport drillReport)
        {
            if (drillReport.GasReadingInfo.Nbut.Uom != null)
            {
                var obj = _mapper.Map<DrillReportNbut>(drillReport.GasReadingInfo.Nbut);
                _db.DrillReportNbut.Add(obj);
            }
        }
        private void Ipent(DrillReport drillReport)
        {
            if (drillReport.GasReadingInfo.Ipent.Uom != null)
            {
                var obj = _mapper.Map<DrillReportIpent>(drillReport.GasReadingInfo.Ipent);
                _db.DrillReportIpent.Add(obj);
            }
        }
        private void GasReadingInfo(DrillReport drillReport)
        {
            if (drillReport.GasReadingInfo.Uid != null)
            {
                var obj = _mapper.Map<DrillReportGasReadingInfo>(drillReport.GasReadingInfo);
                _db.DrillReportGasReadingInfo.Add(obj);
            }
        }

        private void DefaultDatum(DrillReport drillReport)
        {
            if (drillReport.CommonData.DefaultDatum.UidRef != null)
            {
                var obj = _mapper.Map<DrillReportDefaultDatum>(drillReport.CommonData.DefaultDatum);
                _db.DrillReportDefaultDatum.Add(obj);
            }
        }

        private void CommonData(DrillReport drillReport)
        {
            if (drillReport.CommonData != null)
            {
                var obj = _mapper.Map<DrillReportCommonData>(drillReport.CommonData);
                _db.DrillReportCommonData.Add(obj);
            }
        }

        #endregion Insert DrillReport

        #region Update DrillReport
        private void UpdateWellAlias(DrillReport drillReport)
        {
            if (drillReport.WellAlias.WellAliasId != 0)
            {
                var obj = _mapper.Map<DrillReportWellAlias>(drillReport.WellAlias);
                _db.DrillReportWellAlias.Update(obj);
            }
        }
        private void UpdateWellboreAlias(DrillReport drillReport)
        {
            foreach (var item in drillReport.WellboreAlias)
            {
                if (item.Uid != null)
                {
                    var obj = _mapper.Map<DrillReportWellboreAlias>(item);
                    _db.DrillReportWellboreAlias.Update(obj);
                }
            }
        }
        private void UpdateElevation(DrillReport drillReport)
        {
            foreach (var item in drillReport.WellDatum)
            {
                if (item.Elevation.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportElevation>(item.Elevation);
                    _db.DrillReportElevation.Update(obj);
                }
            }
        }
        private void UpdateWellDatum(DrillReport drillReport)
        {
            foreach (var item in drillReport.WellDatum)
            {
                if (item.Uid != null)
                {
                    var obj = _mapper.Map<DrillReportWellDatum>(item);
                    _db.DrillReportWellDatum.Update(obj);
                }
            }
        }
        private void UpdateGeodeticCRS(DrillReport drillReport)
        {

            if (drillReport.WellCRS.GeodeticCRS.UidRef != null)
            {
                var obj = _mapper.Map<DrillReportGeodeticCRS>(drillReport.WellCRS.GeodeticCRS);
                _db.DrillReportGeodeticCRS.Update(obj);
            }
        }
        private void UpdateWellCRS(DrillReport drillReport)
        {

            if (drillReport.WellCRS.Uid != null)
            {
                var obj = _mapper.Map<DrillReportWellCRS>(drillReport.WellCRS);
                _db.DrillReportWellCR.Update(obj);
            }
        }
        private void UpdateRigAlias(DrillReport drillReport)
        {
            foreach (var item in drillReport.WellboreInfo.RigAlias)
            {
                if (item.Uid != null)
                {
                    var obj = _mapper.Map<DrillReportRigAlias>(item);
                    _db.DrillReportRigAlias.Update(obj);
                }
            }
        }
        private void UpdateWellboreInfo(DrillReport drillReport)
        {
            if (drillReport.WellboreInfo != null)
            {
                var obj = _mapper.Map<DrillReportWellboreInfo>(drillReport.WellboreInfo);
                _db.DrillReportWellboreInfo.Update(obj);
            }
        }
        private void UpdateMd(DrillReport drillReport)
        {
            if (drillReport.StatusInfo.Md.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMd>(drillReport.StatusInfo.Md);
                _db.DrillReportMd.Update(obj);
            }
            foreach (var item in drillReport.PorePressure)
            {
                if (item.Md.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportMd>(item.Md);
                    _db.DrillReportMd.Update(obj);
                }
            }

            if (drillReport.SurveyStation.Md.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMd>(drillReport.SurveyStation.Md);
                _db.DrillReportMd.Update(obj);
            }
            foreach (var item in drillReport.Activity)
            {
                if (item.Md.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportMd>(item.Md);
                    _db.DrillReportMd.Update(obj);
                }
            }
            if (drillReport.FormTestInfo.Md.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMd>(drillReport.FormTestInfo.Md);
                _db.DrillReportMd.Update(obj);
            }
            if (drillReport.EquipFailureInfo.Md.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMd>(drillReport.EquipFailureInfo.Md);
                _db.DrillReportMd.Update(obj);
            }
        }
        private void UpdateTvd(DrillReport drillReport)
        {
            if (drillReport.StatusInfo.Tvd.Uom != null)
            {
                var obj = _mapper.Map<DrillReportTvd>(drillReport.StatusInfo.Tvd);
                _db.DrillReportTvd.Update(obj);
            }
            foreach (var item in drillReport.PorePressure)
            {
                if (item.Tvd.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportTvd>(item.Tvd);
                    _db.DrillReportTvd.Update(obj);
                }
            }
            if (drillReport.SurveyStation.Tvd.Uom != null)
            {
                var obj = _mapper.Map<DrillReportTvd>(drillReport.SurveyStation.Tvd);
                _db.DrillReportTvd.Update(obj);
            }
            foreach (var item in drillReport.Activity)
            {
                if (item.Tvd.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportTvd>(item.Tvd);
                    _db.DrillReportTvd.Update(obj);
                }
            }

            if (drillReport.FormTestInfo.Tvd.Uom != null)
            {
                var obj = _mapper.Map<DrillReportTvd>(drillReport.FormTestInfo.Tvd);
                _db.DrillReportTvd.Update(obj);
            }
        }
        private void UpdateMdPlugTop(DrillReport drillReport)
        {
            if (drillReport.StatusInfo.MdPlugTop.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdPlugTop>(drillReport.StatusInfo.MdPlugTop);
                _db.DrillReportMdPlugTop.Update(obj);
            }
        }
        private void UpdateDiaHole(DrillReport drillReport)
        {
            if (drillReport.StatusInfo.DiaHole.Uom != null)
            {
                var obj = _mapper.Map<DrillReportDiaHole>(drillReport.StatusInfo.DiaHole);
                _db.DrillReportDiaHole.Update(obj);
            }
        }
        private void UpdateMdDiaHoleStart(DrillReport drillReport)
        {
            if (drillReport.StatusInfo.MdDiaHoleStart.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdDiaHoleStart>(drillReport.StatusInfo.MdDiaHoleStart);
                _db.DrillReportMdDiaHoleStart.Update(obj);
            }
        }
        private void UpdateDiaPilot(DrillReport drillReport)
        {
            if (drillReport.StatusInfo.DiaPilot.Uom != null)
            {
                var obj = _mapper.Map<DrillReportDiaPilot>(drillReport.StatusInfo.DiaPilot);
                _db.DrillReportDiaPilot.Update(obj);
            }
        }
        private void UpdateMdDiaPilotPlan(DrillReport drillReport)
        {
            if (drillReport.StatusInfo.MdDiaPilotPlan.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdDiaPilotPlan>(drillReport.StatusInfo.MdDiaPilotPlan);
                _db.DrillReportMdDiaPilotPlan.Update(obj);
            }
        }
        private void UpdateMdKickoff(DrillReport drillReport)
        {
            if (drillReport.StatusInfo.MdKickoff.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdKickoff>(drillReport.StatusInfo.MdKickoff);
                _db.DrillReportMdKickoff.Update(obj);
            }
        }
        private void UpdateStrengthForm(DrillReport drillReport)
        {
            if (drillReport.StatusInfo.StrengthForm.Uom != null)
            {
                var obj = _mapper.Map<DrillReportStrengthForm>(drillReport.StatusInfo.StrengthForm);
                _db.DrillReportStrengthForm.Update(obj);
            }
        }
        private void UpdateMdStrengthForm(DrillReport drillReport)
        {
            if (drillReport.StatusInfo.MdStrengthForm.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdStrengthForm>(drillReport.StatusInfo.MdStrengthForm);
                _db.DrillReportMdStrengthForm.Update(obj);
            }
        }
        private void UpdateDiaCsgLast(DrillReport drillReport)
        {
            if (drillReport.StatusInfo.DiaCsgLast.Uom != null)
            {
                var obj = _mapper.Map<DrillReportDiaCsgLast>(drillReport.StatusInfo.DiaCsgLast);
                _db.DrillReportDiaCsgLast.Update(obj);
            }
        }
        private void UpdateMdCsgLast(DrillReport drillReport)
        {
            if (drillReport.StatusInfo.MdCsgLast.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdCsgLast>(drillReport.StatusInfo.MdCsgLast);
                _db.DrillReportMdCsgLast.Update(obj);
            }
        }
        private void UpdateDistDrill(DrillReport drillReport)
        {
            if (drillReport.StatusInfo.DistDrill.Uom != null)
            {
                var obj = _mapper.Map<DrillReportDistDrill>(drillReport.StatusInfo.DistDrill);
                _db.DrillReportDistDrill.Update(obj);
            }
        }
        private void UpdateRopCurrent(DrillReport drillReport)
        {
            if (drillReport.StatusInfo.RopCurrent.Uom != null)
            {
                var obj = _mapper.Map<DrillReportRopCurrent>(drillReport.StatusInfo.RopCurrent);
                _db.DrillReportRopCurrent.Update(obj);
            }
        }
        private void UpdateStatusInfo(DrillReport drillReport)
        {
            if (drillReport.StatusInfo.Uid != null)
            {
                var obj = _mapper.Map<DrillReportStatusInfo>(drillReport.StatusInfo);
                _db.DrillReportStatusInfo.Update(obj);
            }
        }
        private void UpdateDiaBit(DrillReport drillReport)
        {
            if (drillReport.BitRecord.DiaBit.Uom != null)
            {
                var obj = _mapper.Map<DrillReportDiaBit>(drillReport.BitRecord.DiaBit);
                _db.DrillReportDiaBit.Update(obj);
            }
        }
        private void UpdateBitRecord(DrillReport drillReport)
        {
            if (drillReport.BitRecord.Uid != null)
            {
                var obj = _mapper.Map<DrillReportBitRecord>(drillReport.BitRecord);
                _db.DrillReportBitRecord.Update(obj);
            }
        }
        private void UpdatePresBopRating(DrillReport drillReport)
        {
            foreach (var item in drillReport.Fluid)
            {
                if (item.PresBopRating.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportPresBopRating>(item.PresBopRating);
                    _db.DrillReportPresBopRating.Update(obj);
                }
            }
        }
        private void UpdateDensity(DrillReport drillReport)
        {
            foreach (var item in drillReport.Fluid)
            {
                if (item.Density.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportDensity>(item.Density);
                    _db.DrillReportDensity.Update(obj);
                }
            }
        }
        private void UpdateTempVis(DrillReport drillReport)
        {
            foreach (var item in drillReport.Fluid)
            {
                if (item.TempVis.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportTempVis>(item.TempVis);
                    _db.DrillReportTempVis.Update(obj);
                }
            }
        }
        private void UpdatePv(DrillReport drillReport)
        {
            foreach (var item in drillReport.Fluid)
            {
                if (item.Pv.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportPv>(item.Pv);
                    _db.DrillReportPv.Update(obj);
                }
            }
        }
        private void UpdateFluid(DrillReport drillReport)
        {
            foreach (var item in drillReport.Fluid)
            {
                if (item.Uid != null)
                {
                    var obj = _mapper.Map<DrillReportFluid>(item);
                    _db.DrillReportFluids.Update(obj);
                }
            }
        }
        private void UpdateEquivalentMudWeight(DrillReport drillReport)
        {
            foreach (var item in drillReport.PorePressure)
            {
                if (item.EquivalentMudWeight.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportEquivalentMudWeight>(item.EquivalentMudWeight);
                    _db.DrillReportEquivalentMudWeight.Update(obj);
                }
            }
        }
        private void UpdatePorePressure(DrillReport drillReport)
        {
            foreach (var item in drillReport.PorePressure)
            {
                if (item.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportPorePressure>(item);
                    _db.DrillReportPorePressure.Update(obj);
                }
            }
        }
        private void UpdateExtendedReport(DrillReport drillReport)
        {
            if (drillReport.ExtendedReport.DTim != null)
            {
                var obj = _mapper.Map<DrillReportExtendedReport>(drillReport.ExtendedReport);
                _db.DrillReportExtendedReport.Update(obj);
            }
        }
        private void UpdateIncl(DrillReport drillReport)
        {
            if (drillReport.SurveyStation.Incl.Uom != null)
            {
                var obj = _mapper.Map<DrillReportIncl>(drillReport.SurveyStation.Incl);
                _db.DrillReportIncl.Update(obj);
            }
        }
        private void UpdateAzi(DrillReport drillReport)
        {
            if (drillReport.SurveyStation.Azi.Uom != null)
            {
                var obj = _mapper.Map<DrillReportAzi>(drillReport.SurveyStation.Azi);
                _db.DrillReportAzi.Update(obj);
            }
        }
        private void UpdateSurveyStation(DrillReport drillReport)
        {
            if (drillReport.SurveyStation.DTim != null)
            {
                var obj = _mapper.Map<DrillReportSurveyStation>(drillReport.SurveyStation);
                _db.DrillReportSurveyStation.Update(obj);
            }
        }
        private void UpdateActivity(DrillReport drillReport)
        {
            foreach (var item in drillReport.Activity)
            {

                if (item.Uid != null)
                {
                    var obj = _mapper.Map<DrillReportActivity>(item);
                    _db.DrillReportActivity.Update(obj);
                }
            }
        }
        private void UpdateMdTop(DrillReport drillReport)
        {
            foreach (var item in drillReport.LogInfo)
            {
                if (item.MdTop.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportMdTop>(item.MdTop);
                    _db.DrillReportMdTop.Update(obj);
                }
            }
            if (drillReport.CoreInfo.MdTop.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdTop>(drillReport.CoreInfo.MdTop);
                _db.DrillReportMdTop.Update(obj);
            }

            if (drillReport.WellTestInfo.MdTop.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdTop>(drillReport.WellTestInfo.MdTop);
                _db.DrillReportMdTop.Update(obj);
            }

            if (drillReport.LithShowInfo.MdTop.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdTop>(drillReport.LithShowInfo.MdTop);
                _db.DrillReportMdTop.Update(obj);
            }
            if (drillReport.StratInfo.MdTop.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdTop>(drillReport.StratInfo.MdTop);
                _db.DrillReportMdTop.Update(obj);
            }
            if (drillReport.PerfInfo.MdTop.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdTop>(drillReport.PerfInfo.MdTop);
                _db.DrillReportMdTop.Update(obj);
            }
            if (drillReport.GasReadingInfo.MdTop.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdTop>(drillReport.GasReadingInfo.MdTop);
                _db.DrillReportMdTop.Update(obj);
            }
        }
        private void UpdateMdBottom(DrillReport drillReport)
        {
            foreach (var item in drillReport.LogInfo)
            {
                if (item.MdBottom.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportMdBottom>(item.MdBottom);
                    _db.DrillReportMdBottom.Update(obj);
                }
            }
            if (drillReport.CoreInfo.MdBottom.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdBottom>(drillReport.CoreInfo.MdBottom);
                _db.DrillReportMdBottom.Update(obj);
            }
            if (drillReport.WellTestInfo.MdBottom.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdBottom>(drillReport.WellTestInfo.MdBottom);
                _db.DrillReportMdBottom.Update(obj);
            }
            if (drillReport.LithShowInfo.MdBottom.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdBottom>(drillReport.LithShowInfo.MdBottom);
                _db.DrillReportMdBottom.Update(obj);
            }

            if (drillReport.PerfInfo.MdBottom.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdBottom>(drillReport.PerfInfo.MdBottom);
                _db.DrillReportMdBottom.Update(obj);
            }
            if (drillReport.GasReadingInfo.MdBottom.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdBottom>(drillReport.GasReadingInfo.MdBottom);
                _db.DrillReportMdBottom.Update(obj);
            }
        }
        private void UpdateTvdTop(DrillReport drillReport)
        {
            foreach (var item in drillReport.LogInfo)
            {
                if (item.TvdTop.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportTvdTop>(item.TvdTop);
                    _db.DrillReportTvdTop.Update(obj);
                }
            }
            if (drillReport.CoreInfo.TvdTop.Uom != null)
            {
                var obj = _mapper.Map<DrillReportTvdTop>(drillReport.CoreInfo.TvdTop);
                _db.DrillReportTvdTop.Update(obj);
            }
            if (drillReport.WellTestInfo.TvdTop.Uom != null)
            {
                var obj = _mapper.Map<DrillReportTvdTop>(drillReport.WellTestInfo.TvdTop);
                _db.DrillReportTvdTop.Update(obj);
            }
            if (drillReport.LithShowInfo.TvdTop.Uom != null)
            {
                var obj = _mapper.Map<DrillReportTvdTop>(drillReport.LithShowInfo.TvdTop);
                _db.DrillReportTvdTop.Update(obj);
            }
            if (drillReport.StratInfo.TvdTop.Uom != null)
            {
                var obj = _mapper.Map<DrillReportTvdTop>(drillReport.StratInfo.TvdTop);
                _db.DrillReportTvdTop.Update(obj);
            }
            if (drillReport.PerfInfo.TvdTop.Uom != null)
            {
                var obj = _mapper.Map<DrillReportTvdTop>(drillReport.PerfInfo.TvdTop);
                _db.DrillReportTvdTop.Update(obj);
            }
            if (drillReport.GasReadingInfo.TvdTop.Uom != null)
            {
                var obj = _mapper.Map<DrillReportTvdTop>(drillReport.GasReadingInfo.TvdTop);
                _db.DrillReportTvdTop.Update(obj);
            }
        }
        private void UpdateTvdBottom(DrillReport drillReport)
        {
            foreach (var item in drillReport.LogInfo)
            {
                if (item.TvdBottom.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportTvdBottom>(item.TvdBottom);
                    _db.DrillReportTvdBottom.Update(obj);
                }
            }
            if (drillReport.CoreInfo.TvdBottom.Uom != null)
            {
                var obj = _mapper.Map<DrillReportTvdBottom>(drillReport.CoreInfo.TvdBottom);
                _db.DrillReportTvdBottom.Update(obj);
            }
            if (drillReport.WellTestInfo.TvdBottom.Uom != null)
            {
                var obj = _mapper.Map<DrillReportTvdBottom>(drillReport.WellTestInfo.TvdBottom);
                _db.DrillReportTvdBottom.Update(obj);
            }
            if (drillReport.LithShowInfo.TvdBottom.Uom != null)
            {
                var obj = _mapper.Map<DrillReportTvdBottom>(drillReport.LithShowInfo.TvdBottom);
                _db.DrillReportTvdBottom.Update(obj);
            }
            if (drillReport.PerfInfo.TvdBottom.Uom != null)
            {
                var obj = _mapper.Map<DrillReportTvdBottom>(drillReport.PerfInfo.TvdBottom);
                _db.DrillReportTvdBottom.Update(obj);
            }
            if (drillReport.GasReadingInfo.TvdBottom.Uom != null)
            {
                var obj = _mapper.Map<DrillReportTvdBottom>(drillReport.GasReadingInfo.TvdBottom);
                _db.DrillReportTvdBottom.Update(obj);
            }
        }
        private void UpdateTempBHCT(DrillReport drillReport)
        {
            foreach (var item in drillReport.LogInfo)
            {
                if (item.TempBHCT.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportTempBHCT>(item.TempBHCT);
                    _db.DrillReportTempBHCT.Update(obj);
                }
            }
        }
        private void UpdateMdTempTool(DrillReport drillReport)
        {
            foreach (var item in drillReport.LogInfo)
            {
                if (item.MdTempTool.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportMdTempTool>(item.MdTempTool);
                    _db.DrillReportMdTempTool.Update(obj);
                }
            }
        }
        private void UpdateTvdTempTool(DrillReport drillReport)
        {
            foreach (var item in drillReport.LogInfo)
            {
                if (item.TvdTempTool.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportTvdTempTool>(item.TvdTempTool);
                    _db.DrillReportTvdTempTool.Update(obj);
                }
            }
        }
        private void UpdateTempBHST(DrillReport drillReport)
        {
            foreach (var item in drillReport.LogInfo)
            {
                if (item.TempBHST.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportTempBHST>(item.TempBHST);
                    _db.DrillReportTempBHST.Update(obj);
                }
            }
        }
        private void UpdateETimStatic(DrillReport drillReport)
        {
            foreach (var item in drillReport.LogInfo)
            {
                if (item.ETimStatic.Uom != null)
                {
                    var obj = _mapper.Map<DrillReportETimStatic>(item.ETimStatic);
                    _db.DrillReportETimStatic.Update(obj);
                }
            }
        }
        private void UpdateLogInfo(DrillReport drillReport)
        {
            foreach (var item in drillReport.LogInfo)
            {
                if (item.Uid != null)
                {
                    var obj = _mapper.Map<DrillReportLogInfo>(item);
                    _db.DrillReportLogInfo.Update(obj);
                }
            }
        }

        private void UpdateLenRecovered(DrillReport drillReport)
        {

            if (drillReport.CoreInfo.LenRecovered.Uom != null)
            {
                var obj = _mapper.Map<DrillReportLenRecovered>(drillReport.CoreInfo.LenRecovered);
                _db.DrillReportLenRecovered.Update(obj);
            }
        }
        private void UpdateRecoverPc(DrillReport drillReport)
        {

            if (drillReport.CoreInfo.RecoverPc.Uom != null)
            {
                var obj = _mapper.Map<DrillReportRecoverPc>(drillReport.CoreInfo.RecoverPc);
                _db.DrillReportRecoverPc.Update(obj);
            }
        }
        private void UpdateLenBarrel(DrillReport drillReport)
        {

            if (drillReport.CoreInfo.LenBarrel.Uom != null)
            {
                var obj = _mapper.Map<DrillReportLenBarrel>(drillReport.CoreInfo.LenBarrel);
                _db.DrillReportLenBarrel.Update(obj);
            }
        }
        private void UpdateCoreInfo(DrillReport drillReport)
        {

            if (drillReport.CoreInfo.Uid != null)
            {
                var obj = _mapper.Map<DrillReportCoreInfo>(drillReport.CoreInfo);
                _db.DrillReportCoreInfo.Update(obj);
            }
        }
        private void UpdateChokeOrificeSize(DrillReport drillReport)
        {

            if (drillReport.WellTestInfo.ChokeOrificeSize.Uom != null)
            {
                var obj = _mapper.Map<DrillReportChokeOrificeSize>(drillReport.WellTestInfo.ChokeOrificeSize);
                _db.DrillReportChokeOrificeSize.Update(obj);
            }
        }
        private void UpdateDensityOil(DrillReport drillReport)
        {

            if (drillReport.WellTestInfo.DensityOil.Uom != null)
            {
                var obj = _mapper.Map<DrillReportDensityOil>(drillReport.WellTestInfo.DensityOil);
                _db.DrillReportDensityOil.Update(obj);
            }
        }
        private void UpdateDensityWater(DrillReport drillReport)
        {

            if (drillReport.WellTestInfo.DensityWater.Uom != null)
            {
                var obj = _mapper.Map<DrillReportDensityWater>(drillReport.WellTestInfo.DensityWater);
                _db.DrillReportDensityWater.Update(obj);
            }
        }
        private void UpdateDensityGas(DrillReport drillReport)
        {

            if (drillReport.WellTestInfo.DensityGas.Uom != null)
            {
                var obj = _mapper.Map<DrillReportDensityGas>(drillReport.WellTestInfo.DensityGas);
                _db.DrillReportDensityGas.Update(obj);
            }
        }
        private void UpdateFlowRateOil(DrillReport drillReport)
        {

            if (drillReport.WellTestInfo.FlowRateOil.Uom != null)
            {
                var obj = _mapper.Map<DrillReportFlowRateOil>(drillReport.WellTestInfo.FlowRateOil);
                _db.DrillReportFlowRateOil.Update(obj);
            }
        }
        private void UpdateFlowRateWater(DrillReport drillReport)
        {

            if (drillReport.WellTestInfo.FlowRateWater.Uom != null)
            {
                var obj = _mapper.Map<DrillReportFlowRateWater>(drillReport.WellTestInfo.FlowRateWater);
                _db.DrillReportFlowRateWater.Update(obj);
            }
        }
        private void UpdateFlowRateGas(DrillReport drillReport)
        {

            if (drillReport.WellTestInfo.FlowRateGas.Uom != null)
            {
                var obj = _mapper.Map<DrillReportFlowRateGas>(drillReport.WellTestInfo.FlowRateGas);
                _db.DrillReportFlowRateGas.Update(obj);
            }
        }
        private void UpdatePresShutIn(DrillReport drillReport)
        {
            if (drillReport.WellTestInfo.PresShutIn.Uom != null)
            {
                var obj = _mapper.Map<DrillReportPresShutIn>(drillReport.WellTestInfo.PresShutIn);
                _db.DrillReportPresShutIn.Update(obj);
            }
        }
        private void UpdatePresFlowing(DrillReport drillReport)
        {
            if (drillReport.WellTestInfo.PresFlowing.Uom != null)
            {
                var obj = _mapper.Map<DrillReportPresFlowing>(drillReport.WellTestInfo.PresFlowing);
                _db.DrillReportPresFlowing.Update(obj);
            }
        }
        private void UpdatePresBottom(DrillReport drillReport)
        {
            if (drillReport.WellTestInfo.PresBottom.Uom != null)
            {
                var obj = _mapper.Map<DrillReportPresBottom>(drillReport.WellTestInfo.PresBottom);
                _db.DrillReportPresBottom.Update(obj);
            }
        }
        private void UpdateGasOilRatio(DrillReport drillReport)
        {
            if (drillReport.WellTestInfo.GasOilRatio.Uom != null)
            {
                var obj = _mapper.Map<DrillReportGasOilRatio>(drillReport.WellTestInfo.GasOilRatio);
                _db.DrillReportGasOilRatio.Update(obj);
            }
        }
        private void UpdateWaterOilRatio(DrillReport drillReport)
        {
            if (drillReport.WellTestInfo.WaterOilRatio.Uom != null)
            {
                var obj = _mapper.Map<DrillReportWaterOilRatio>(drillReport.WellTestInfo.WaterOilRatio);
                _db.DrillReportWaterOilRatio.Update(obj);
            }
        }
        private void UpdateChloride(DrillReport drillReport)
        {
            if (drillReport.WellTestInfo.Chloride.Uom != null)
            {
                var obj = _mapper.Map<DrillReportChloride>(drillReport.WellTestInfo.Chloride);
                _db.DrillReportChloride.Update(obj);
            }
        }
        private void UpdateCarbonDioxide(DrillReport drillReport)
        {
            if (drillReport.WellTestInfo.CarbonDioxide.Uom != null)
            {
                var obj = _mapper.Map<DrillReportCarbonDioxide>(drillReport.WellTestInfo.CarbonDioxide);
                _db.DrillReportCarbonDioxide.Update(obj);
            }
        }
        private void UpdateHydrogenSulfide(DrillReport drillReport)
        {
            if (drillReport.WellTestInfo.HydrogenSulfide.Uom != null)
            {
                var obj = _mapper.Map<DrillReportHydrogenSulfide>(drillReport.WellTestInfo.HydrogenSulfide);
                _db.DrillReportHydrogenSulfide.Update(obj);
            }
        }
        private void UpdateVolOilTotal(DrillReport drillReport)
        {
            if (drillReport.WellTestInfo.VolOilTotal.Uom != null)
            {
                var obj = _mapper.Map<DrillReportVolOilTotal>(drillReport.WellTestInfo.VolOilTotal);
                _db.DrillReportVolOilTotal.Update(obj);
            }
        }
        private void UpdateVolGasTotal(DrillReport drillReport)
        {
            if (drillReport.WellTestInfo.VolGasTotal.Uom != null)
            {
                var obj = _mapper.Map<DrillReportVolGasTotal>(drillReport.WellTestInfo.VolGasTotal);
                _db.DrillReportVolGasTotal.Update(obj);
            }
        }
        private void UpdateVolWaterTotal(DrillReport drillReport)
        {
            if (drillReport.WellTestInfo.VolWaterTotal.Uom != null)
            {
                var obj = _mapper.Map<DrillReportVolWaterTotal>(drillReport.WellTestInfo.VolWaterTotal);
                _db.DrillReportVolWaterTotal.Update(obj);
            }
        }
        private void UpdateVolOilStored(DrillReport drillReport)
        {
            if (drillReport.WellTestInfo.VolOilStored.Uom != null)
            {
                var obj = _mapper.Map<DrillReportVolOilStored>(drillReport.WellTestInfo.VolOilStored);
                _db.DrillReportVolOilStored.Update(obj);
            }
        }
        private void UpdateWellTestInfo(DrillReport drillReport)
        {
            if (drillReport.WellTestInfo.Uid != null)
            {
                var obj = _mapper.Map<DrillReportWellTestInfo>(drillReport.WellTestInfo);
                _db.DrillReportWellTestInfo.Update(obj);
            }
        }
        private void UpdatePresPore(DrillReport drillReport)
        {
            if (drillReport.FormTestInfo.PresPore.Uom != null)
            {
                var obj = _mapper.Map<DrillReportPresPore>(drillReport.FormTestInfo.PresPore);
                _db.DrillReportPresPore.Update(obj);
            }
        }
        private void UpdateMdSample(DrillReport drillReport)
        {
            if (drillReport.FormTestInfo.MdSample.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdSample>(drillReport.FormTestInfo.MdSample);
                _db.DrillReportMdSample.Update(obj);
            }
        }
        private void UpdateDensityHC(DrillReport drillReport)
        {
            if (drillReport.FormTestInfo.DensityHC.Uom != null)
            {
                var obj = _mapper.Map<DrillReportDensityHC>(drillReport.FormTestInfo.DensityHC);
                _db.DrillReportDensityHC.Update(obj);
            }
        }
        private void UpdateVolumeSample(DrillReport drillReport)
        {
            if (drillReport.FormTestInfo.VolumeSample.Uom != null)
            {
                var obj = _mapper.Map<DrillReportVolumeSample>(drillReport.FormTestInfo.VolumeSample);
                _db.DrillReportVolumeSample.Update(obj);
            }
        }
        private void UpdateFormTestInfo(DrillReport drillReport)
        {
            if (drillReport.FormTestInfo.Uid != null)
            {
                var obj = _mapper.Map<DrillReportFormTestInfo>(drillReport.FormTestInfo);
                _db.DrillReportFormTestInfo.Update(obj);
            }
        }
        private void UpdateLithShowInfo(DrillReport drillReport)
        {
            if (drillReport.LithShowInfo.Uid != null)
            {
                var obj = _mapper.Map<DrillReportLithShowInfo>(drillReport.LithShowInfo);
                _db.DrillReportLithShowInfo.Update(obj);
            }
        }
        private void UpdateETimMissProduction(DrillReport drillReport)
        {
            if (drillReport.EquipFailureInfo.ETimMissProduction.Uom != null)
            {
                var obj = _mapper.Map<DrillReportETimMissProduction>(drillReport.EquipFailureInfo.ETimMissProduction);
                _db.DrillReportETimMissProduction.Update(obj);
            }
        }
        private void UpdateEquipFailureInfo(DrillReport drillReport)
        {
            if (drillReport.EquipFailureInfo.Uid != null)
            {
                var obj = _mapper.Map<DrillReportEquipFailureInfo>(drillReport.EquipFailureInfo);
                _db.DrillReportEquipFailureInfo.Update(obj);
            }
        }
        private void UpdateMdInflow(DrillReport drillReport)
        {
            if (drillReport.ControlIncidentInfo.MdInflow.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdInflow>(drillReport.ControlIncidentInfo.MdInflow);
                _db.DrillReportMdInflow.Update(obj);
            }
        }
        private void UpdateTvdInflow(DrillReport drillReport)
        {
            if (drillReport.ControlIncidentInfo.TvdInflow.Uom != null)
            {
                var obj = _mapper.Map<DrillReportTvdInflow>(drillReport.ControlIncidentInfo.TvdInflow);
                _db.DrillReportTvdInflow.Update(obj);
            }
        }
        private void UpdateETimLost(DrillReport drillReport)
        {
            if (drillReport.ControlIncidentInfo.ETimLost.Uom != null)
            {
                var obj = _mapper.Map<DrillReportETimLost>(drillReport.ControlIncidentInfo.ETimLost);
                _db.DrillReportETimLost.Update(obj);
            }
        }
        private void UpdateMdBit(DrillReport drillReport)
        {
            if (drillReport.ControlIncidentInfo.MdBit.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMdBit>(drillReport.ControlIncidentInfo.MdBit);
                _db.DrillReportMdBit.Update(obj);
            }
        }
        private void UpdateWtMud(DrillReport drillReport)
        {
            if (drillReport.ControlIncidentInfo.WtMud.Uom != null)
            {
                var obj = _mapper.Map<DrillReportWtMud>(drillReport.ControlIncidentInfo.WtMud);
                _db.DrillReportWtMuds.Update(obj);
            }
        }
        private void UpdateVolMudGained(DrillReport drillReport)
        {
            if (drillReport.ControlIncidentInfo.VolMudGained.Uom != null)
            {
                var obj = _mapper.Map<DrillReportVolMudGained>(drillReport.ControlIncidentInfo.VolMudGained);
                _db.DrillReportVolMudGained.Update(obj);
            }
        }
        private void UpdatePresShutInCasing(DrillReport drillReport)
        {
            if (drillReport.ControlIncidentInfo.PresShutInCasing.Uom != null)
            {
                var obj = _mapper.Map<DrillReportPresShutInCasing>(drillReport.ControlIncidentInfo.PresShutInCasing);
                _db.DrillReportPresShutInCasing.Update(obj);
            }
        }
        private void UpdatePresShutInDrill(DrillReport drillReport)
        {
            if (drillReport.ControlIncidentInfo.PresShutInDrill.Uom != null)
            {
                var obj = _mapper.Map<DrillReportPresShutInDrill>(drillReport.ControlIncidentInfo.PresShutInDrill);
                _db.DrillReportPresShutInDrill.Update(obj);
            }
        }
        private void UpdateTempBottom(DrillReport drillReport)
        {
            if (drillReport.ControlIncidentInfo.TempBottom.Uom != null)
            {
                var obj = _mapper.Map<DrillReportTempBottom>(drillReport.ControlIncidentInfo.TempBottom);
                _db.DrillReportTempBottom.Update(obj);
            }
        }
        private void UpdatePresMaxChoke(DrillReport drillReport)
        {
            if (drillReport.ControlIncidentInfo.PresMaxChoke.Uom != null)
            {
                var obj = _mapper.Map<DrillReportPresMaxChoke>(drillReport.ControlIncidentInfo.PresMaxChoke);
                _db.DrillReportPresMaxChoke.Update(obj);
            }
        }
        private void UpdateControlIncidentInfo(DrillReport drillReport)
        {
            if (drillReport.ControlIncidentInfo.Uid != null)
            {
                var obj = _mapper.Map<DrillReportControlIncidentInfo>(drillReport.ControlIncidentInfo);
                _db.DrillReportControlIncidentInfo.Update(obj);
            }
        }
        private void UpdateStratInfo(DrillReport drillReport)
        {
            if (drillReport.StratInfo.Uid != null)
            {
                var obj = _mapper.Map<DrillReportStratInfo>(drillReport.StratInfo);
                _db.DrillReportStratInfo.Update(obj);
            }
        }
        private void UpdatePerfInfo(DrillReport drillReport)
        {
            if (drillReport.PerfInfo.Uid != null)
            {
                var obj = _mapper.Map<DrillReportPerfInfo>(drillReport.PerfInfo);
                _db.DrillReportPerfInfo.Update(obj);
            }
        }
        private void UpdateGasHigh(DrillReport drillReport)
        {
            if (drillReport.GasReadingInfo.GasHigh.Uom != null)
            {
                var obj = _mapper.Map<DrillReportGasHigh>(drillReport.GasReadingInfo.GasHigh);
                _db.DrillReportGasHigh.Update(obj);
            }
        }
        private void UpdateGasLow(DrillReport drillReport)
        {
            if (drillReport.GasReadingInfo.GasLow.Uom != null)
            {
                var obj = _mapper.Map<DrillReportGasLow>(drillReport.GasReadingInfo.GasLow);
                _db.DrillReportGasLow.Update(obj);
            }
        }
        private void UpdateMeth(DrillReport drillReport)
        {
            if (drillReport.GasReadingInfo.Meth.Uom != null)
            {
                var obj = _mapper.Map<DrillReportMeth>(drillReport.GasReadingInfo.Meth);
                _db.DrillReportMeth.Update(obj);
            }
        }
        private void UpdateEth(DrillReport drillReport)
        {
            if (drillReport.GasReadingInfo.Eth.Uom != null)
            {
                var obj = _mapper.Map<DrillReportEth>(drillReport.GasReadingInfo.Eth);
                _db.DrillReportEth.Update(obj);
            }
        }
        private void UpdateProp(DrillReport drillReport)
        {
            if (drillReport.GasReadingInfo.Prop.Uom != null)
            {
                var obj = _mapper.Map<DrillReportProp>(drillReport.GasReadingInfo.Prop);
                _db.DrillReportProp.Update(obj);
            }
        }
        private void UpdateIbut(DrillReport drillReport)
        {
            if (drillReport.GasReadingInfo.Ibut.Uom != null)
            {
                var obj = _mapper.Map<DrillReportIbut>(drillReport.GasReadingInfo.Ibut);
                _db.DrillReportIbut.Update(obj);
            }
        }
        private void UpdateNbut(DrillReport drillReport)
        {
            if (drillReport.GasReadingInfo.Nbut.Uom != null)
            {
                var obj = _mapper.Map<DrillReportNbut>(drillReport.GasReadingInfo.Nbut);
                _db.DrillReportNbut.Update(obj);
            }
        }
        private void UpdateIpent(DrillReport drillReport)
        {
            if (drillReport.GasReadingInfo.Ipent.Uom != null)
            {
                var obj = _mapper.Map<DrillReportIpent>(drillReport.GasReadingInfo.Ipent);
                _db.DrillReportIpent.Update(obj);
            }
        }
        private void UpdateGasReadingInfo(DrillReport drillReport)
        {
            if (drillReport.GasReadingInfo.Uid != null)
            {
                var obj = _mapper.Map<DrillReportGasReadingInfo>(drillReport.GasReadingInfo);
                _db.DrillReportGasReadingInfo.Update(obj);
            }
        }

        private void UpdateDefaultDatum(DrillReport drillReport)
        {
            if (drillReport.CommonData.DefaultDatum.UidRef != null)
            {
                var obj = _mapper.Map<DrillReportDefaultDatum>(drillReport.CommonData.DefaultDatum);
                _db.DrillReportDefaultDatum.Update(obj);
            }
        }

        private void UpdateCommonData(DrillReport drillReport)
        {
            if (drillReport.CommonData != null)
            {
                var obj = _mapper.Map<DrillReportCommonData>(drillReport.CommonData);
                _db.DrillReportCommonData.Update(obj);
            }
        }
        #endregion Update DrillReport
    }
}
