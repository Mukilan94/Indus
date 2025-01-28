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
    public class FluidsReportRepository : IFluidsReportCoreRepository
    {
        private readonly WellAIAdvisiorContext _db;
        private readonly WebAIAdvisorContext _wdb;
        private readonly IMapper _mapper;
        private ITenantRepository _tenantRepository;

        public FluidsReportRepository(WellAIAdvisiorContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper, ITenantRepository tenantRepository, WebAIAdvisorContext wdb)
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
        public bool FluidsReportExists(string uid)
        {
            bool value = _db.FluidsReports.Any(x => x.Uid.ToLower().Trim() == uid.ToLower().Trim());
            return value;
        }

        public bool CreateFluidsReport(FluidsReport fluidsReport)
        {
            try
            {
                Md(fluidsReport);
                Tvd(fluidsReport);
                Density(fluidsReport);
                VisFunnel(fluidsReport);
                TempVis(fluidsReport);
                Pv(fluidsReport);
                Yp(fluidsReport);
                Gel10Sec(fluidsReport);
                Gel10Min(fluidsReport);
                Gel30Min(fluidsReport);
                FilterCakeLtlp(fluidsReport);
                FiltrateLtlp(fluidsReport);
                TempHthp(fluidsReport);
                PresHthp(fluidsReport);
                FiltrateHthp(fluidsReport);
                FilterCakeHthp(fluidsReport);
                SolidsPc(fluidsReport);
                WaterPc(fluidsReport);
                OilPc(fluidsReport);
                SandPc(fluidsReport);
                SolidsLowGravPc(fluidsReport);
                SolidsCalcPc(fluidsReport);
                BaritePc(fluidsReport);
                Lcm(fluidsReport);
                Mbt(fluidsReport);
                TempPh(fluidsReport);
                Pm(fluidsReport);
                PmFiltrate(fluidsReport);
                Mf(fluidsReport);
                AlkalinityP1(fluidsReport);
                AlkalinityP2(fluidsReport);
                Chloride(fluidsReport);
                Calcium(fluidsReport);
                Magnesium(fluidsReport);
                Potassium(fluidsReport);
                TempRheom(fluidsReport);
                BrinePc(fluidsReport);
                Lime(fluidsReport);
                ElectStab(fluidsReport);
                CalciumChloride(fluidsReport);
                SolidsHiGravPc(fluidsReport);
                Polymer(fluidsReport);
                SolCorPc(fluidsReport);
                OilCtg(fluidsReport);
                HardnessCa(fluidsReport);
                Sulfide(fluidsReport);
                Fluid(fluidsReport);
                Fluid(fluidsReport);
                CommonData(fluidsReport);
                _db.FluidsReports.Add(fluidsReport);
                return Save();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "FluidsReportCoreRepository CreateFluidsReport", null);
                return Save();
            }
        }

        

        public bool DeleteFluidsReport(FluidsReport fluidsReport)
        {
            _db.FluidsReports.Remove(fluidsReport);
            return Save();
        }

        public FluidsReport GetFluidsReportDetail(string Uid)
        {
            return _db.FluidsReports.FirstOrDefault(x => x.Uid == Uid);

        }

        public ICollection<FluidsReport> GetFluidsReportDetails()
        {
            return _db.FluidsReports.OrderBy(x => x.NameWell).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateFluidsReport(FluidsReport fluidsReport)
        {
            try
            {
                UpdateMd(fluidsReport);
                UpdateTvd(fluidsReport);
                UpdateDensity(fluidsReport);
                UpdateVisFunnel(fluidsReport);
                UpdateTempVis(fluidsReport);
                UpdatePv(fluidsReport);
                UpdateYp(fluidsReport);
                UpdateGel10Sec(fluidsReport);
                UpdateGel10Min(fluidsReport);
                UpdateGel30Min(fluidsReport);
                UpdateFilterCakeLtlp(fluidsReport);
                UpdateFiltrateLtlp(fluidsReport);
                UpdateTempHthp(fluidsReport);
                UpdatePresHthp(fluidsReport);
                UpdateFiltrateHthp(fluidsReport);
                UpdateFilterCakeHthp(fluidsReport);
                UpdateSolidsPc(fluidsReport);
                UpdateWaterPc(fluidsReport);
                UpdateOilPc(fluidsReport);
                UpdateSandPc(fluidsReport);
                UpdateSolidsLowGravPc(fluidsReport);
                UpdateSolidsCalcPc(fluidsReport);
                UpdateBaritePc(fluidsReport);
                UpdateLcm(fluidsReport);
                UpdateMbt(fluidsReport);
                UpdateTempPh(fluidsReport);
                UpdatePm(fluidsReport);
                UpdatePmFiltrate(fluidsReport);
                UpdateMf(fluidsReport);
                UpdateAlkalinityP1(fluidsReport);
                UpdateAlkalinityP2(fluidsReport);
                UpdateChloride(fluidsReport);
                UpdateCalcium(fluidsReport);
                UpdateMagnesium(fluidsReport);
                UpdatePotassium(fluidsReport);
                UpdateTempRheom(fluidsReport);
                UpdateBrinePc(fluidsReport);
                UpdateLime(fluidsReport);
                UpdateElectStab(fluidsReport);
                UpdateCalciumChloride(fluidsReport);
                UpdateSolidsHiGravPc(fluidsReport);
                UpdatePolymer(fluidsReport);
                UpdateSolCorPc(fluidsReport);
                UpdateOilCtg(fluidsReport);
                UpdateHardnessCa(fluidsReport);
                UpdateSulfide(fluidsReport);
                UpdateFluid(fluidsReport);
                UpdateFluid(fluidsReport);
                UpdateCommonData(fluidsReport);
                _db.FluidsReports.Update(fluidsReport);
                return Save();
            }

            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "FluidsReport UpdateFluidsReport", null);
                return Save();
            }
          
        }

        #region Insert FluidsReport
        private void Md(FluidsReport fluidsReport)
        {
            if (fluidsReport.Md.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportMd>(fluidsReport.Md);
                _db.FluidsReportMd.Add(obj);
            }
        }
        private void Tvd(FluidsReport fluidsReport)
        {
            if (fluidsReport.Tvd.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportTvd>(fluidsReport.Tvd);
                _db.FluidsReportTvd.Add(obj);
            }
        }
        private void Density(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.Density.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportDensity>(fluidsReport.Fluid.Density);
                _db.FluidsReportDensity.Add(obj);
            }
        }
        private void VisFunnel(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.VisFunnel.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportVisFunnel>(fluidsReport.Fluid.VisFunnel);
                _db.FluidsReportVisFunnel.Add(obj);
            }
        }
        private void TempVis(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.TempVis.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportTempVis>(fluidsReport.Fluid.TempVis);
                _db.FluidsReportTempVis.Add(obj);
            }
        }
        private void Pv(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.Pv.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportPv>(fluidsReport.Fluid.Pv);
                _db.FluidsReportPv.Add(obj);
            }
        }
        private void Yp(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.Yp.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportYp>(fluidsReport.Fluid.Yp);
                _db.FluidsReportYp.Add(obj);
            }
        }
        private void Gel10Sec(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.Gel10Sec.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportGel10Sec>(fluidsReport.Fluid.Gel10Sec);
                _db.FluidsReportGel10Sec.Add(obj);
            }
        }
        private void Gel10Min(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.Gel10Min.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportGel10Min>(fluidsReport.Fluid.Gel10Min);
                _db.FluidsReportGel10Min.Add(obj);
            }
        }
        private void Gel30Min(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.Gel30Min.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportGel30Min>(fluidsReport.Fluid.Gel30Min);
                _db.FluidsReportGel30Min.Add(obj);
            }
        }
        private void FilterCakeLtlp(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.FilterCakeLtlp.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportFilterCakeLtlp>(fluidsReport.Fluid.FilterCakeLtlp);
                _db.FluidsReportFilterCakeLtlp.Add(obj);
            }
        }
        private void FiltrateLtlp(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.FiltrateLtlp.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportFiltrateLtlp>(fluidsReport.Fluid.FiltrateLtlp);
                _db.FluidsReportFiltrateLtlp.Add(obj);
            }
        }
        private void TempHthp(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.TempHthp.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportTempHthp>(fluidsReport.Fluid.TempHthp);
                _db.FluidsReportTempHthp.Add(obj);
            }
        }
        private void PresHthp(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.PresHthp.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportPresHthp>(fluidsReport.Fluid.PresHthp);
                _db.FluidsReportPresHthp.Add(obj);
            }
        }
        private void FiltrateHthp(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.FiltrateHthp.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportFiltrateHthp>(fluidsReport.Fluid.FiltrateHthp);
                _db.FluidsReportFiltrateHthp.Add(obj);
            }
        }

        private void FilterCakeHthp(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.FilterCakeHthp.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportFilterCakeHthp>(fluidsReport.Fluid.FilterCakeHthp);
                _db.FluidsReportFilterCakeHthp.Add(obj);
            }
        }

        private void SolidsPc(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.SolidsPc.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportSolidsPc>(fluidsReport.Fluid.SolidsPc);
                _db.FluidsReportSolidsPc.Add(obj);
            }
        }

        private void WaterPc(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.WaterPc.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportWaterPc>(fluidsReport.Fluid.WaterPc);
                _db.FluidsReportWaterPc.Add(obj);
            }
        }

        private void OilPc(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.OilPc.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportOilPc>(fluidsReport.Fluid.OilPc);
                _db.FluidsReportOilPc.Add(obj);
            }
        }

        private void SandPc(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.SandPc.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportSandPc>(fluidsReport.Fluid.SandPc);
                _db.FluidsReportSandPc.Add(obj);
            }
        }

        private void SolidsLowGravPc(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.SolidsLowGravPc.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportSolidsLowGravPc>(fluidsReport.Fluid.SolidsLowGravPc);
                _db.FluidsReportSolidsLowGravPc.Add(obj);
            }
        }
        private void SolidsCalcPc(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.SolidsCalcPc.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportSolidsCalcPc>(fluidsReport.Fluid.SolidsCalcPc);
                _db.FluidsReportSolidsCalcPc.Add(obj);
            }
        }
        private void BaritePc(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.BaritePc.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportBaritePc>(fluidsReport.Fluid.BaritePc);
                _db.FluidsReportBaritePc.Add(obj);
            }
        }
        private void Lcm(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.Lcm.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportLcm>(fluidsReport.Fluid.Lcm);
                _db.FluidsReportLcm.Add(obj);
            }
        }
        private void Mbt(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.Mbt.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportMbt>(fluidsReport.Fluid.Mbt);
                _db.FluidsReportMbt.Add(obj);
            }
        }
        private void TempPh(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.TempPh.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportTempPh>(fluidsReport.Fluid.TempPh);
                _db.FluidsReportTempPh.Add(obj);
            }
        }
        private void Pm(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.Pm.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportPm>(fluidsReport.Fluid.Pm);
                _db.FluidsReportPm.Add(obj);
            }
        }
        private void PmFiltrate(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.PmFiltrate.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportPmFiltrate>(fluidsReport.Fluid.PmFiltrate);
                _db.FluidsReportPmFiltrate.Add(obj);
            }
        }
        private void Mf(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.Mf.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportMf>(fluidsReport.Fluid.Mf);
                _db.FluidsReportMf.Add(obj);
            }
        }
        private void AlkalinityP1(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.AlkalinityP1.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportAlkalinityP1>(fluidsReport.Fluid.AlkalinityP1);
                _db.FluidsReportAlkalinityP1.Add(obj);
            }
        }
        private void AlkalinityP2(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.AlkalinityP2.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportAlkalinityP2>(fluidsReport.Fluid.AlkalinityP2);
                _db.FluidsReportAlkalinityP2.Add(obj);
            }
        }
        private void Chloride(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.Chloride.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportChloride>(fluidsReport.Fluid.Chloride);
                _db.FluidsReportChloride.Add(obj);
            }
        }
        private void Calcium(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.Calcium.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportCalcium>(fluidsReport.Fluid.Calcium);
                _db.FluidsReportCalcium.Add(obj);
            }
        }
        private void Magnesium(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.Magnesium.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportMagnesium>(fluidsReport.Fluid.Magnesium);
                _db.FluidsReportMagnesium.Add(obj);
            }
        }
        private void Potassium(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.Potassium.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportPotassium>(fluidsReport.Fluid.Potassium);
                _db.FluidsReportPotassium.Add(obj);
            }
        }
        private void TempRheom(FluidsReport fluidsReport)
        {
            foreach (var item in fluidsReport.Fluid.Rheometer)
            {

                if (item.TempRheom.Uom != null)
                {
                    var obj = _mapper.Map<FluidsReportTempRheom>(item.TempRheom);
                    _db.FluidsReportTempRheom.Add(obj);
                }
            }
        }
        private void PresRheom(FluidsReport fluidsReport)
        {
            foreach (var item in fluidsReport.Fluid.Rheometer)
            {

                if (item.PresRheom.Uom != null)
                {
                    var obj = _mapper.Map<FluidsReportPresRheom>(item.PresRheom);
                    _db.FluidsReportPresRheom.Add(obj);
                }
            }
        }
        private void Rheometer(FluidsReport fluidsReport)
        {
            foreach (var item in fluidsReport.Fluid.Rheometer)
            {

                if (item.Uid != null)
                {
                    var obj = _mapper.Map<FluidsReportRheometer>(item);
                    _db.FluidsReportRheometer.Add(obj);
                }
            }
        }
        private void BrinePc(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.BrinePc.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportBrinePc>(fluidsReport.Fluid.BrinePc);
                _db.FluidsReportBrinePc.Add(obj);
            }
        }
        private void Lime(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.Lime.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportLime>(fluidsReport.Fluid.Lime);
                _db.FluidsReportLime.Add(obj);
            }
        }
        private void ElectStab(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.ElectStab.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportElectStab>(fluidsReport.Fluid.ElectStab);
                _db.FluidsReportElectStab.Add(obj);
            }
        }
        private void CalciumChloride(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.CalciumChloride.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportCalciumChloride>(fluidsReport.Fluid.CalciumChloride);
                _db.FluidsReportCalciumChloride.Add(obj);
            }
        }
        private void SolidsHiGravPc(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.SolidsHiGravPc.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportSolidsHiGravPc>(fluidsReport.Fluid.SolidsHiGravPc);
                _db.FluidsReportSolidsHiGravPc.Add(obj);
            }
        }
        private void Polymer(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.Polymer.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportPolymer>(fluidsReport.Fluid.Polymer);
                _db.FluidsReportPolymer.Add(obj);
            }
        }
        private void SolCorPc(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.SolCorPc.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportSolCorPc>(fluidsReport.Fluid.SolCorPc);
                _db.FluidsReportSolCorPc.Add(obj);
            }
        }
        private void OilCtg(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.OilCtg.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportOilCtg>(fluidsReport.Fluid.OilCtg);
                _db.FluidsReportOilCtg.Add(obj);
            }
        }
        private void HardnessCa(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.HardnessCa.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportHardnessCa>(fluidsReport.Fluid.HardnessCa);
                _db.FluidsReportHardnessCa.Add(obj);
            }
        }
        private void Sulfide(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.Sulfide.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportSulfide>(fluidsReport.Fluid.Sulfide);
                _db.FluidsReportSulfide.Add(obj);
            }
        }
        private void Fluid(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.Uid != null)
            {
                var obj = _mapper.Map<FluidsReportFluid>(fluidsReport.Fluid);
                _db.FluidsReportFluid.Add(obj);
            }
        }
        private void CommonData(FluidsReport fluidsReport)
        {
            if (fluidsReport.CommonData != null)
            {
                var obj = _mapper.Map<FluidsReportCommonData>(fluidsReport.CommonData);
                _db.FluidsReportCommonDatas.Add(obj);
            }
        }

        #endregion Insert FluidsReport

        #region Update FluidsReport


        private void UpdateMd(FluidsReport fluidsReport)
        {
            if (fluidsReport.Md.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportMd>(fluidsReport.Md);
                _db.FluidsReportMd.Update(obj);
            }
        }
        private void UpdateTvd(FluidsReport fluidsReport)
        {
            if (fluidsReport.Tvd.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportTvd>(fluidsReport.Tvd);
                _db.FluidsReportTvd.Update(obj);
            }
        }
        private void UpdateDensity(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.Density.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportDensity>(fluidsReport.Fluid.Density);
                _db.FluidsReportDensity.Update(obj);
            }
        }
        private void UpdateVisFunnel(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.VisFunnel.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportVisFunnel>(fluidsReport.Fluid.VisFunnel);
                _db.FluidsReportVisFunnel.Update(obj);
            }
        }
        private void UpdateTempVis(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.TempVis.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportTempVis>(fluidsReport.Fluid.TempVis);
                _db.FluidsReportTempVis.Update(obj);
            }
        }
        private void UpdatePv(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.Pv.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportPv>(fluidsReport.Fluid.Pv);
                _db.FluidsReportPv.Update(obj);
            }
        }
        private void UpdateYp(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.Yp.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportYp>(fluidsReport.Fluid.Yp);
                _db.FluidsReportYp.Update(obj);
            }
        }
        private void UpdateGel10Sec(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.Gel10Sec.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportGel10Sec>(fluidsReport.Fluid.Gel10Sec);
                _db.FluidsReportGel10Sec.Update(obj);
            }
        }
        private void UpdateGel10Min(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.Gel10Min.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportGel10Min>(fluidsReport.Fluid.Gel10Min);
                _db.FluidsReportGel10Min.Update(obj);
            }
        }
        private void UpdateGel30Min(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.Gel30Min.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportGel30Min>(fluidsReport.Fluid.Gel30Min);
                _db.FluidsReportGel30Min.Update(obj);
            }
        }
        private void UpdateFilterCakeLtlp(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.FilterCakeLtlp.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportFilterCakeLtlp>(fluidsReport.Fluid.FilterCakeLtlp);
                _db.FluidsReportFilterCakeLtlp.Update(obj);
            }
        }
        private void UpdateFiltrateLtlp(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.FiltrateLtlp.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportFiltrateLtlp>(fluidsReport.Fluid.FiltrateLtlp);
                _db.FluidsReportFiltrateLtlp.Update(obj);
            }
        }
        private void UpdateTempHthp(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.TempHthp.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportTempHthp>(fluidsReport.Fluid.TempHthp);
                _db.FluidsReportTempHthp.Update(obj);
            }
        }
        private void UpdatePresHthp(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.PresHthp.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportPresHthp>(fluidsReport.Fluid.PresHthp);
                _db.FluidsReportPresHthp.Update(obj);
            }
        }
        private void UpdateFiltrateHthp(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.FiltrateHthp.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportFiltrateHthp>(fluidsReport.Fluid.FiltrateHthp);
                _db.FluidsReportFiltrateHthp.Update(obj);
            }
        }

        private void UpdateFilterCakeHthp(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.FilterCakeHthp.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportFilterCakeHthp>(fluidsReport.Fluid.FilterCakeHthp);
                _db.FluidsReportFilterCakeHthp.Update(obj);
            }
        }

        private void UpdateSolidsPc(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.SolidsPc.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportSolidsPc>(fluidsReport.Fluid.SolidsPc);
                _db.FluidsReportSolidsPc.Update(obj);
            }
        }

        private void UpdateWaterPc(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.WaterPc.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportWaterPc>(fluidsReport.Fluid.WaterPc);
                _db.FluidsReportWaterPc.Update(obj);
            }
        }

        private void UpdateOilPc(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.OilPc.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportOilPc>(fluidsReport.Fluid.OilPc);
                _db.FluidsReportOilPc.Update(obj);
            }
        }

        private void UpdateSandPc(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.SandPc.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportSandPc>(fluidsReport.Fluid.SandPc);
                _db.FluidsReportSandPc.Update(obj);
            }
        }

        private void UpdateSolidsLowGravPc(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.SolidsLowGravPc.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportSolidsLowGravPc>(fluidsReport.Fluid.SolidsLowGravPc);
                _db.FluidsReportSolidsLowGravPc.Update(obj);
            }
        }
        private void UpdateSolidsCalcPc(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.SolidsCalcPc.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportSolidsCalcPc>(fluidsReport.Fluid.SolidsCalcPc);
                _db.FluidsReportSolidsCalcPc.Update(obj);
            }
        }
        private void UpdateBaritePc(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.BaritePc.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportBaritePc>(fluidsReport.Fluid.BaritePc);
                _db.FluidsReportBaritePc.Update(obj);
            }
        }
        private void UpdateLcm(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.Lcm.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportLcm>(fluidsReport.Fluid.Lcm);
                _db.FluidsReportLcm.Update(obj);
            }
        }
        private void UpdateMbt(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.Mbt.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportMbt>(fluidsReport.Fluid.Mbt);
                _db.FluidsReportMbt.Update(obj);
            }
        }
        private void UpdateTempPh(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.TempPh.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportTempPh>(fluidsReport.Fluid.TempPh);
                _db.FluidsReportTempPh.Update(obj);
            }
        }
        private void UpdatePm(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.Pm.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportPm>(fluidsReport.Fluid.Pm);
                _db.FluidsReportPm.Update(obj);
            }
        }
        private void UpdatePmFiltrate(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.PmFiltrate.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportPmFiltrate>(fluidsReport.Fluid.PmFiltrate);
                _db.FluidsReportPmFiltrate.Update(obj);
            }
        }
        private void UpdateMf(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.Mf.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportMf>(fluidsReport.Fluid.Mf);
                _db.FluidsReportMf.Update(obj);
            }
        }
        private void UpdateAlkalinityP1(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.AlkalinityP1.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportAlkalinityP1>(fluidsReport.Fluid.AlkalinityP1);
                _db.FluidsReportAlkalinityP1.Update(obj);
            }
        }
        private void UpdateAlkalinityP2(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.AlkalinityP2.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportAlkalinityP2>(fluidsReport.Fluid.AlkalinityP2);
                _db.FluidsReportAlkalinityP2.Update(obj);
            }
        }
        private void UpdateChloride(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.Chloride.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportChloride>(fluidsReport.Fluid.Chloride);
                _db.FluidsReportChloride.Update(obj);
            }
        }
        private void UpdateCalcium(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.Calcium.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportCalcium>(fluidsReport.Fluid.Calcium);
                _db.FluidsReportCalcium.Update(obj);
            }
        }
        private void UpdateMagnesium(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.Magnesium.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportMagnesium>(fluidsReport.Fluid.Magnesium);
                _db.FluidsReportMagnesium.Update(obj);
            }
        }
        private void UpdatePotassium(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.Potassium.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportPotassium>(fluidsReport.Fluid.Potassium);
                _db.FluidsReportPotassium.Update(obj);
            }
        }
        private void UpdateTempRheom(FluidsReport fluidsReport)
        {
            foreach (var item in fluidsReport.Fluid.Rheometer)
            {

                if (item.TempRheom.Uom != null)
                {
                    var obj = _mapper.Map<FluidsReportTempRheom>(item.TempRheom);
                    _db.FluidsReportTempRheom.Update(obj);
                }
            }
        }
        private void UpdatePresRheom(FluidsReport fluidsReport)
        {
            foreach (var item in fluidsReport.Fluid.Rheometer)
            {

                if (item.PresRheom.Uom != null)
                {
                    var obj = _mapper.Map<FluidsReportPresRheom>(item.PresRheom);
                    _db.FluidsReportPresRheom.Update(obj);
                }
            }
        }
        private void UpdateRheometer(FluidsReport fluidsReport)
        {
            foreach (var item in fluidsReport.Fluid.Rheometer)
            {

                if (item.Uid != null)
                {
                    var obj = _mapper.Map<FluidsReportRheometer>(item);
                    _db.FluidsReportRheometer.Update(obj);
                }
            }
        }
        private void UpdateBrinePc(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.BrinePc.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportBrinePc>(fluidsReport.Fluid.BrinePc);
                _db.FluidsReportBrinePc.Update(obj);
            }
        }
        private void UpdateLime(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.Lime.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportLime>(fluidsReport.Fluid.Lime);
                _db.FluidsReportLime.Update(obj);
            }
        }
        private void UpdateElectStab(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.ElectStab.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportElectStab>(fluidsReport.Fluid.ElectStab);
                _db.FluidsReportElectStab.Update(obj);
            }
        }
        private void UpdateCalciumChloride(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.CalciumChloride.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportCalciumChloride>(fluidsReport.Fluid.CalciumChloride);
                _db.FluidsReportCalciumChloride.Update(obj);
            }
        }
        private void UpdateSolidsHiGravPc(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.SolidsHiGravPc.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportSolidsHiGravPc>(fluidsReport.Fluid.SolidsHiGravPc);
                _db.FluidsReportSolidsHiGravPc.Update(obj);
            }
        }
        private void UpdatePolymer(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.Polymer.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportPolymer>(fluidsReport.Fluid.Polymer);
                _db.FluidsReportPolymer.Update(obj);
            }
        }
        private void UpdateSolCorPc(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.SolCorPc.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportSolCorPc>(fluidsReport.Fluid.SolCorPc);
                _db.FluidsReportSolCorPc.Update(obj);
            }
        }
        private void UpdateOilCtg(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.OilCtg.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportOilCtg>(fluidsReport.Fluid.OilCtg);
                _db.FluidsReportOilCtg.Update(obj);
            }
        }
        private void UpdateHardnessCa(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.HardnessCa.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportHardnessCa>(fluidsReport.Fluid.HardnessCa);
                _db.FluidsReportHardnessCa.Update(obj);
            }
        }
        private void UpdateSulfide(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.Sulfide.Uom != null)
            {
                var obj = _mapper.Map<FluidsReportSulfide>(fluidsReport.Fluid.Sulfide);
                _db.FluidsReportSulfide.Update(obj);
            }
        }
        private void UpdateFluid(FluidsReport fluidsReport)
        {
            if (fluidsReport.Fluid.Uid != null)
            {
                var obj = _mapper.Map<FluidsReportFluid>(fluidsReport.Fluid);
                _db.FluidsReportFluid.Update(obj);
            }
        }
        private void UpdateCommonData(FluidsReport fluidsReport)
        {
            if (fluidsReport.CommonData != null)
            {
                var obj = _mapper.Map<FluidsReportCommonData>(fluidsReport.CommonData);
                _db.FluidsReportCommonDatas.Update(obj);
            }
        }

        #endregion Update FluidsReport
    }
}
