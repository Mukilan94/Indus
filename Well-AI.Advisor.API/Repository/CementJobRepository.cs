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
    public class CementJobRepository : ICementJobRepository
    {


        private readonly WebAIAdvisorContext _wdb;
        private readonly WellAIAdvisiorContext _db;
        private readonly IMapper _mapper;
        private ITenantRepository _tenantRepository;
        public CementJobRepository(WellAIAdvisiorContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper, ITenantRepository tenantRepository, WebAIAdvisorContext wdb)
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
        public bool CementJobExists(string uid)
        {
            bool value = _db.CementJobs.Any(x => x.Uid.ToLower().Trim() == uid.ToLower().Trim());
            return value;
        }

        public bool CreateCementJob(CementJob cementJob)
        {
            try
            {
                
                MdWater(cementJob);
                Woc(cementJob);
                MdPlugTop(cementJob);
                MdPlugBot(cementJob);
                MdHole(cementJob);
                MdShoe(cementJob);
                TvdShoe(cementJob);
                MdStringSet(cementJob);
                TvdStringSet(cementJob);
                VolExcess(cementJob);
                FlowrateDisplaceAv(cementJob);
                FlowrateDisplaceMx(cementJob);
                PresDisplace(cementJob);
                VolReturns(cementJob);
                ETimMudCirculation(cementJob);
                FlowrateMudCirc(cementJob);
                PresMudCirc(cementJob);
                FlowrateEnd(cementJob);
                MdFluidTop(cementJob);
                MdFluidBottom(cementJob);
                VolWater(cementJob);
                VolCement(cementJob);
                RatioMixWater(cementJob);
                VolFluid(cementJob);
                ETimPump(cementJob);
                RatePump(cementJob);
                VolPump(cementJob);
                PresBack(cementJob);
                ETimShutdown(cementJob);
                CementPumpSchedule(cementJob);
                ExcessPc(cementJob);
                Density(cementJob);
                VolYield(cementJob);
                SolidVolumeFraction(cementJob);
                VolPumped(cementJob);
                VolOther(cementJob);
                Vis(cementJob);
                Yp(cementJob);
                N(cementJob);
                K(cementJob);
                Gel10SecReading(cementJob);
                Gel10SecStrength(cementJob);
                Gel1MinReading(cementJob);
                Gel1MinStrength(cementJob);
                Gel10MinReading(cementJob);
                Gel10MinStrength(cementJob);
                DensBaseFluid(cementJob);
                MassDryBlend(cementJob);
                DensDryBlend(cementJob);
                MassSackDryBlend(cementJob);
                DensAdd(cementJob);
                Concentration(cementJob);
                Additive(cementJob);
                CementAdditive(cementJob);
                VolGasFoam(cementJob);
                RatioConstGasMethodAv(cementJob);
                DensConstGasMethod(cementJob);
                RatioConstGasMethodStart(cementJob);
                RatioConstGasMethodEnd(cementJob);
                DensConstGasFoam(cementJob);
                ETimThickening(cementJob);
                TempThickening(cementJob);
                PresTestThickening(cementJob);
                ConsTestThickening(cementJob);
                PcFreeWater(cementJob);
                TempFreeWater(cementJob);
                VolTestFluidLoss(cementJob);
                TempFluidLoss(cementJob);
                PresTestFluidLoss(cementJob);
                TimeFluidLoss(cementJob);
                VolAPIFluidLoss(cementJob);
                ETimComprStren1(cementJob);
                ETimComprStren2(cementJob);
                PresComprStren1(cementJob);
                PresComprStren2(cementJob);
                TempComprStren1(cementJob);
                TempComprStren2(cementJob);
                DensAtPres(cementJob);
                VolReserved(cementJob);
                VolTotSlurry(cementJob);
                MdString(cementJob);
                MdTool(cementJob);
                MdCoilTbg(cementJob);
                VolCsgIn(cementJob);
                VolCsgOut(cementJob);
                DiaTailPipe(cementJob);
                PresTbgStart(cementJob);
                PresTbgEnd(cementJob);
                PresCsgStart(cementJob);
                PresCsgEnd(cementJob);
                PresBackPressure(cementJob);
                PresCoilTbgStart(cementJob);
                PresCoilTbgEnd(cementJob);
                PresBreakDown(cementJob);
                FlowrateBreakDown(cementJob);
                PresSqueezeAv(cementJob);
                PresSqueezeEnd(cementJob);
                PresSqueeze(cementJob);
                ETimPresHeld(cementJob);
                FlowrateSqueezeAv(cementJob);
                FlowrateSqueezeMx(cementJob);
                FlowratePumpStart(cementJob);
                FlowratePumpEnd(cementJob);
                MdCircOut(cementJob);
                VolCircPrior(cementJob);
                VisFunnelMud(cementJob);
                PvMud(cementJob);
                YpMud(cementJob);
                Gel10Sec(cementJob);
                Gel10Min(cementJob);
                PresPriorBump(cementJob);
                PresBump(cementJob);
                PresHeld(cementJob);
                VolMudLost(cementJob);
                DensDisplaceFluid(cementJob);
                VolDisplaceFluid(cementJob);
                
                PresTest(cementJob);
                ETimTest(cementJob);
                CblPres(cementJob);
                ETimCementLog(cementJob);
                FormPit(cementJob);
                ETimPitStart(cementJob);
                MdCementTop(cementJob);
                LinerTop(cementJob);
                LinerLap(cementJob);
                TempBHCT(cementJob);
                ETimBeforeTest(cementJob);
                TestNegativeEmw(cementJob);
                TestPositiveEmw(cementJob);
                MdDVTool(cementJob);
                CementTest(cementJob);
                MdSqueeze(cementJob);
                RpmPipe(cementJob);
                TqInitPipeRot(cementJob);
                TqPipeAv(cementJob);
                TqPipeMx(cementJob);
                OverPull(cementJob);
                SlackOff(cementJob);
                RpmPipeRecip(cementJob);
                MdTop(cementJob);
                LenPipeRecipStroke(cementJob);
                CementJobCommonData(cementJob);
                CementStage(cementJob);
                CementingFluid(cementJob);
                _db.CementJobs.Add(cementJob);
                return Save();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "CementRepository CreateCementJob", null);
                return Save();
            }
        }

        

        public bool DeleteCementJob(CementJob cementJob)
        {
            _db.CementJobs.Remove(cementJob);
            return Save();
        }

        public CementJob GetCementJobDetail(string Uid)
        {
            return _db.CementJobs.FirstOrDefault(x=>x.Uid==Uid);
            
        }

        public ICollection<CementJob> GetCementJobDetails()
        {
            return _db.CementJobs.OrderBy(x => x.NameWell).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateCementJob(CementJob cementJob)
        {
            #region Update CementJob
            try
            {
                UpdateMdWater(cementJob);
                UpdateWoc(cementJob);
                UpdateMdPlugTop(cementJob);
                UpdateMdPlugBot(cementJob);
                UpdateMdHole(cementJob);
                UpdateMdShoe(cementJob);
                UpdateTvdShoe(cementJob);
                UpdateMdStringSet(cementJob);
                UpdateTvdStringSet(cementJob);
                UpdateVolExcess(cementJob);
                UpdateFlowrateDisplaceAv(cementJob);
                UpdateFlowrateDisplaceMx(cementJob);
                UpdatePresDisplace(cementJob);
                UpdateVolReturns(cementJob);
                UpdateETimMudCirculation(cementJob);
                UpdateFlowrateMudCirc(cementJob);
                UpdatePresMudCirc(cementJob);
                UpdateFlowrateEnd(cementJob);
                UpdateMdFluidTop(cementJob);
                UpdateMdFluidBottom(cementJob);
                UpdateVolWater(cementJob);
                UpdateVolCement(cementJob);
                UpdateRatioMixWater(cementJob);
                UpdateVolFluid(cementJob);
                UpdateETimPump(cementJob);
                UpdateRatePump(cementJob);
                UpdateVolPump(cementJob);
                UpdatePresBack(cementJob);
                UpdateETimShutdown(cementJob);
                UpdateCementPumpSchedule(cementJob);
                UpdateExcessPc(cementJob);
                UpdateDensity(cementJob);
                UpdateVolYield(cementJob);
                UpdateSolidVolumeFraction(cementJob);
                UpdateVolPumped(cementJob);
                UpdateVolOther(cementJob);
                UpdateVis(cementJob);
                UpdateYp(cementJob);
                UpdateN(cementJob);
                UpdateK(cementJob);
                UpdateGel10SecReading(cementJob);
                UpdateGel10SecStrength(cementJob);
                UpdateGel1MinReading(cementJob);
                UpdateGel1MinStrength(cementJob);
                UpdateGel10MinReading(cementJob);
                UpdateGel10MinStrength(cementJob);
                UpdateDensBaseFluid(cementJob);
                UpdateMassDryBlend(cementJob);
                UpdateDensDryBlend(cementJob);
                UpdateMassSackDryBlend(cementJob);
                UpdateDensAdd(cementJob);
                UpdateConcentration(cementJob);
                UpdateAdditive(cementJob);
                UpdateCementAdditive(cementJob);
                UpdateVolGasFoam(cementJob);
                UpdateRatioConstGasMethodAv(cementJob);
                UpdateDensConstGasMethod(cementJob);
                UpdateRatioConstGasMethodStart(cementJob);
                UpdateRatioConstGasMethodEnd(cementJob);
                UpdateDensConstGasFoam(cementJob);
                UpdateETimThickening(cementJob);
                UpdateTempThickening(cementJob);
                UpdatePresTestThickening(cementJob);
                UpdateConsTestThickening(cementJob);
                UpdatePcFreeWater(cementJob);
                UpdateTempFreeWater(cementJob);
                UpdateVolTestFluidLoss(cementJob);
                UpdateTempFluidLoss(cementJob);
                UpdatePresTestFluidLoss(cementJob);
                UpdateTimeFluidLoss(cementJob);
                UpdateVolAPIFluidLoss(cementJob);
                UpdateETimComprStren1(cementJob);
                UpdateETimComprStren2(cementJob);
                UpdatePresComprStren1(cementJob);
                UpdatePresComprStren2(cementJob);
                UpdateTempComprStren1(cementJob);
                UpdateTempComprStren2(cementJob);
                UpdateDensAtPres(cementJob);
                UpdateVolReserved(cementJob);
                UpdateVolTotSlurry(cementJob);
             
                UpdateMdString(cementJob);
                UpdateMdTool(cementJob);
                UpdateMdCoilTbg(cementJob);
                UpdateVolCsgIn(cementJob);
                UpdateVolCsgOut(cementJob);
                UpdateDiaTailPipe(cementJob);
                UpdatePresTbgStart(cementJob);
                UpdatePresTbgEnd(cementJob);
                UpdatePresCsgStart(cementJob);
                UpdatePresCsgEnd(cementJob);
                UpdatePresBackPressure(cementJob);
                UpdatePresCoilTbgStart(cementJob);
                UpdatePresCoilTbgEnd(cementJob);
                UpdatePresBreakDown(cementJob);
                UpdateFlowrateBreakDown(cementJob);
                UpdatePresSqueezeAv(cementJob);
                UpdatePresSqueezeEnd(cementJob);
                UpdatePresSqueeze(cementJob);
                UpdateETimPresHeld(cementJob);
                UpdateFlowrateSqueezeAv(cementJob);
                UpdateFlowrateSqueezeMx(cementJob);
                UpdateFlowratePumpStart(cementJob);
                UpdateFlowratePumpEnd(cementJob);
                UpdateMdCircOut(cementJob);
                UpdateVolCircPrior(cementJob);
                UpdateVisFunnelMud(cementJob);
                UpdatePvMud(cementJob);
                UpdateYpMud(cementJob);
                UpdateGel10Sec(cementJob);
                UpdateGel10Min(cementJob);
                UpdatePresPriorBump(cementJob);
                UpdatePresBump(cementJob);
                UpdatePresHeld(cementJob);
                UpdateVolMudLost(cementJob);
                UpdateDensDisplaceFluid(cementJob);
                UpdateVolDisplaceFluid(cementJob);
                UpdateCementStage(cementJob);
                UpdatePresTest(cementJob);
                UpdateETimTest(cementJob);
                UpdateCblPres(cementJob);
                UpdateETimCementLog(cementJob);
                UpdateFormPit(cementJob);
                UpdateETimPitStart(cementJob);
                UpdateMdCementTop(cementJob);
                UpdateLinerTop(cementJob);
                UpdateLinerLap(cementJob);
                UpdateTempBHCT(cementJob);
                UpdateTempBHST(cementJob);
                UpdateETimBeforeTest(cementJob);
                UpdateTestNegativeEmw(cementJob);
                UpdateTestPositiveEmw(cementJob);
                UpdateMdDVTool(cementJob);
                UpdateCementTest(cementJob);
                UpdateMdSqueeze(cementJob);
                UpdateRpmPipe(cementJob);
                UpdateTqInitPipeRot(cementJob);
                UpdateTqPipeAv(cementJob);
                UpdateTqPipeMx(cementJob);
                UpdateOverPull(cementJob);
                UpdateSlackOff(cementJob);
                UpdateRpmPipeRecip(cementJob);
                UpdateMdTop(cementJob);
                UpdateMdBottom(cementJob);
                UpdateLenPipeRecipStroke(cementJob);
                UpdateCementJobCommonData(cementJob);
                _db.CementJobs.Update(cementJob);
                return Save();
                #endregion Update CementJob
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "CementRepository UpdateCementJob", null);
                return Save();
            }
            
        }


        #region Create CementJob Method
        private void MdWater(CementJob cementJob)
        {
            if (cementJob.MdWater.Uom != null)
            {
                var obj = _mapper.Map<CementJobMdWater>(cementJob.MdWater);
                _db.CementJobMdWaters.Add(obj);
                
            }
        }
        private void Woc(CementJob cementJob)
        {
            if (cementJob.Woc.Uom != null)
            {
                var obj = _mapper.Map<CementJobWoc>(cementJob.Woc);
                _db.CementJobWocs.Add(obj);
                
            }
        }
        private void MdPlugTop(CementJob cementJob)
        {
            if (cementJob.MdPlugTop.Uom != null)
            {
                var obj = _mapper.Map<CementJobMdPlugTop>(cementJob.MdPlugTop);
                _db.CementJobMdPlugTops.Add(obj);

            }
        }
        
        private void MdPlugBot(CementJob cementJob)
        {
            if (cementJob.MdPlugBot.Uom != null)
            {
                var obj = _mapper.Map<CementJobMdPlugBot>(cementJob.MdPlugBot);
                _db.CementJobMdPlugBots.Add(obj);
               
            }
        }
        private void MdHole(CementJob cementJob)
        {
            if (cementJob.MdHole.Uom != null)
            {
                var obj = _mapper.Map<CementJobMdHole>(cementJob.MdHole);
                _db.CementJobMdHoles.Add(obj);
               
            }
        }
        private void MdShoe(CementJob cementJob)
        {
            if (cementJob.MdShoe.Uom != null)
            {
                var obj = _mapper.Map<CementJobMdShoe>(cementJob.MdShoe);
                _db.CementJobMdShoes.Add(obj);
               
            }
        }
        private void TvdShoe(CementJob cementJob)
        {
            if (cementJob.TvdShoe.Uom != null)
            {
                var obj = _mapper.Map<CementJobTvdShoe>(cementJob.TvdShoe);
                _db.CementJobTvdShoes.Add(obj);
               
            }
        }
        private void MdStringSet(CementJob cementJob)
        {
            if (cementJob.MdStringSet.Uom != null)
            {
                var obj = _mapper.Map<CementJobMdStringSet>(cementJob.MdStringSet);
                _db.CementJobMdStringSets.Add(obj);
               
            }
        }
        private void TvdStringSet(CementJob cementJob)
        {
            if (cementJob.TvdStringSet.Uom != null)
            {
                var obj = _mapper.Map<CementJobTvdStringSet>(cementJob.TvdStringSet);
                _db.CementJobTvdStringSets.Add(obj);
               
            }
        }
        private void VolExcess(CementJob cementJob)
        {
            if (cementJob.CementStage.VolExcess.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolExcess>(cementJob.CementStage.VolExcess);
                _db.CementJobVolExcesss.Add(obj);
               
            }
        }
        private void FlowrateDisplaceAv(CementJob cementJob)
        {
            if (cementJob.CementStage.FlowrateDisplaceAv.Uom != null)
            {
                var obj = _mapper.Map<CementJobFlowrateDisplaceAv>(cementJob.CementStage.FlowrateDisplaceAv);
                _db.CementJobFlowrateDisplaceAvs.Add(obj);
               
            }
        }
        private void FlowrateDisplaceMx(CementJob cementJob)
        {
            if (cementJob.CementStage.FlowrateDisplaceMx.Uom != null)
            {
                var obj = _mapper.Map<CementJobFlowrateDisplaceMx>(cementJob.CementStage.FlowrateDisplaceMx);
                _db.CementJobFlowrateDisplaceMxs.Add(obj);
               
            }
        }
        private void PresDisplace(CementJob cementJob)
        {
            if (cementJob.CementStage.PresDisplace.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresDisplace>(cementJob.CementStage.PresDisplace);
                _db.CementJobPresDisplaces.Add(obj);
               
            }
        }
        private void VolReturns(CementJob cementJob)
        {
            if (cementJob.CementStage.VolReturns.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolReturns>(cementJob.CementStage.VolReturns);
                _db.CementJobVolReturnss.Add(obj);
               
            }
        }
        private void ETimMudCirculation(CementJob cementJob)
        {
            if (cementJob.CementStage.ETimMudCirculation.Uom != null)
            {
                var obj = _mapper.Map<CementJobETimMudCirculation>(cementJob.CementStage.ETimMudCirculation);
                _db.CementJobETimMudCirculations.Add(obj);
               
            }
        }
        private void FlowrateMudCirc(CementJob cementJob)
        {
            if (cementJob.CementStage.FlowrateMudCirc.Uom != null)
            {
                var obj = _mapper.Map<CementJobFlowrateMudCirc>(cementJob.CementStage.FlowrateMudCirc);
                _db.CementJobFlowrateMudCircs.Add(obj);
               
            }
        }
        private void PresMudCirc(CementJob cementJob)
        {
            if (cementJob.CementStage.PresMudCirc.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresMudCirc>(cementJob.CementStage.PresMudCirc);
                _db.CementJobPresMudCircs.Add(obj);
               
            }
        }
        private void FlowrateEnd(CementJob cementJob)
        {
            if (cementJob.CementStage.FlowrateEnd.Uom != null)
            {
                var obj = _mapper.Map<CementJobFlowrateEnd>(cementJob.CementStage.FlowrateEnd);
                _db.CementJobFlowrateEnds.Add(obj);
               
            }
        }
        private void MdFluidTop(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.MdFluidTop.Uom != null)
            {
                var obj = _mapper.Map<CementJobMdFluidTop>(cementJob.CementStage.CementingFluid.MdFluidTop);
                _db.CementJobMdFluidTops.Add(obj);
               
            }
        }
        private void MdFluidBottom(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.MdFluidBottom.Uom != null)
            {
                var obj = _mapper.Map<CementJobMdFluidBottom>(cementJob.CementStage.CementingFluid.MdFluidBottom);
                _db.CementJobMdFluidBottoms.Add(obj);
               
            }
        }
        private void VolWater(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.VolWater.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolWater>(cementJob.CementStage.CementingFluid.VolWater);
                _db.CementJobVolWaters.Add(obj);
               
            }
        }
        private void VolCement(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.VolCement.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolCement>(cementJob.CementStage.CementingFluid.VolCement);
                _db.CementJobVolCements.Add(obj);
               
            }
        }
        private void RatioMixWater(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.RatioMixWater.Uom != null)
            {
                var obj = _mapper.Map<CementJobRatioMixWater>(cementJob.CementStage.CementingFluid.RatioMixWater);
                _db.CementJobRatioMixWaters.Add(obj);
               
            }
        }
        private void VolFluid(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.VolFluid.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolFluid>(cementJob.CementStage.CementingFluid.VolFluid);
                _db.CementJobVolFluids.Add(obj);
               
            }
        }
        private void ETimPump(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.CementPumpSchedule.ETimPump.Uom != null)
            {
                var obj = _mapper.Map<CementJobETimPump>(cementJob.CementStage.CementingFluid.CementPumpSchedule.ETimPump);
                _db.CementJobETimPumps.Add(obj);
               
            }
        }
        private void RatePump(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.CementPumpSchedule.RatePump.Uom != null)
            {
                var obj = _mapper.Map<CementJobRatePump>(cementJob.CementStage.CementingFluid.CementPumpSchedule.RatePump);
                _db.CementJobRatePumps.Add(obj);
               
            }
        }
        private void VolPump(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.CementPumpSchedule.VolPump.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolPump>(cementJob.CementStage.CementingFluid.CementPumpSchedule.VolPump);
                _db.CementJobVolPumps.Add(obj);
               
            }
        }
        private void PresBack(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.CementPumpSchedule.PresBack.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresBack>(cementJob.CementStage.CementingFluid.CementPumpSchedule.PresBack);
                _db.CementJobPresBacks.Add(obj);
               
            }
        }
        private void ETimShutdown(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.CementPumpSchedule.ETimShutdown.Uom != null)
            {
                var obj = _mapper.Map<CementJobETimShutdown>(cementJob.CementStage.CementingFluid.CementPumpSchedule.ETimShutdown);
                _db.CementJobETimShutdowns.Add(obj);
               
            }
        }

        private void ExcessPc(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.ExcessPc.Uom != null)
            {
                var obj = _mapper.Map<CementJobExcessPc>(cementJob.CementStage.CementingFluid.ExcessPc);
                _db.CementJobExcessPcs.Add(obj);
               
            }
        }
        private void VolYield(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.VolYield.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolYield>(cementJob.CementStage.CementingFluid.VolYield);
                _db.CementJobVolYields.Add(obj);
               
            }
        }
        private void SolidVolumeFraction(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.SolidVolumeFraction.Uom != null)
            {
                var obj = _mapper.Map<CementJobSolidVolumeFraction>(cementJob.CementStage.CementingFluid.SolidVolumeFraction);
                _db.CementJobSolidVolumeFractions.Add(obj);
               
            }
        }
        private void VolPumped(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.VolPumped.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolPumped>(cementJob.CementStage.CementingFluid.VolPumped);
                _db.CementJobVolPumpeds.Add(obj);
               
            }
        }
        private void VolOther(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.VolOther.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolOther>(cementJob.CementStage.CementingFluid.VolOther);
                _db.CementJobVolOthers.Add(obj);
               
            }
        }
        private void Vis(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.Vis.Uom != null)
            {
                var obj = _mapper.Map<CementJobVis>(cementJob.CementStage.CementingFluid.Vis);
                _db.CementJobViss.Add(obj);
               
            }
        }
        private void Yp(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.Yp.Uom != null)
            {
                var obj = _mapper.Map<CementJobYp>(cementJob.CementStage.CementingFluid.Yp);
                _db.CementJobYps.Add(obj);
               
            }
        }
        private void N(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.N.Uom != null)
            {
                var obj = _mapper.Map<CementJobN>(cementJob.CementStage.CementingFluid.N);
                _db.CementJobNs.Add(obj);
               
            }
        }
       
        private void K(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.K.Uom != null)
            {
                var obj = _mapper.Map<CementJobK>(cementJob.CementStage.CementingFluid.K);
                _db.CementJobKs.Add(obj);
               
            }
        }
        private void Gel10SecReading(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.Gel10SecReading.Uom != null)
            {
                var obj = _mapper.Map<CementJobGel10SecReading>(cementJob.CementStage.CementingFluid.Gel10SecReading);
                _db.CementJobGel10SecReadings.Add(obj);
               
            }
        }
        private void Gel10SecStrength(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.Gel10SecStrength.Uom != null)
            {
                var obj = _mapper.Map<CementJobGel10SecStrength>(cementJob.CementStage.CementingFluid.Gel10SecStrength);
                _db.CementJobGel10SecStrengths.Add(obj);
               
            }
        }
        private void Gel1MinReading(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.Gel1MinReading.Uom != null)
            {
                var obj = _mapper.Map<CementJobGel1MinReading>(cementJob.CementStage.CementingFluid.Gel1MinReading);
                _db.CementJobGel1MinReadings.Add(obj);
               
            }
        }
        private void Gel1MinStrength(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.Gel1MinStrength.Text != null)
            {
                var obj = _mapper.Map<CementJobGel1MinStrength>(cementJob.CementStage.CementingFluid.Gel1MinStrength);
                _db.CementJobGel1MinStrengths.Add(obj);
               
            }
        }
        private void Gel10MinReading(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.Gel10MinReading.Uom != null)
            {
                var obj = _mapper.Map<CementJobGel10MinReading>(cementJob.CementStage.CementingFluid.Gel10MinReading);
                _db.CementJobGel10MinReadings.Add(obj);
               
            }
        }
        private void Gel10MinStrength(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.Gel10MinStrength.Uom != null)
            {
                var obj = _mapper.Map<CementJobGel10MinStrength>(cementJob.CementStage.CementingFluid.Gel10MinStrength);
                _db.CementJobGel10MinStrengths.Add(obj);
               
            }
        }

        private void DensBaseFluid(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.DensBaseFluid.Uom != null)
            {
                var obj = _mapper.Map<CementJobDensBaseFluid>(cementJob.CementStage.CementingFluid.DensBaseFluid);
                _db.CementJobDensBaseFluids.Add(obj);
               
            }
        }
        private void MassDryBlend(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.MassDryBlend.Uom != null)
            {
                var obj = _mapper.Map<CementJobMassDryBlend>(cementJob.CementStage.CementingFluid.MassDryBlend);
                _db.CementJobMassDryBlends.Add(obj);
               
            }
        }
        private void DensDryBlend(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.DensDryBlend.Uom != null)
            {
                var obj = _mapper.Map<CementJobDensDryBlend>(cementJob.CementStage.CementingFluid.DensDryBlend);
                _db.CementJobDensDryBlends.Add(obj);
               
            }
        }
        private void MassSackDryBlend(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.MassSackDryBlend.Uom != null)
            {
                var obj = _mapper.Map<CementJobMassSackDryBlend>(cementJob.CementStage.CementingFluid.MassSackDryBlend);
                _db.CementJobMassSackDryBlends.Add(obj);
               
            }
        }
        private void DensAdd(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.CementAdditive.DensAdd.Uom != null)
            {
                var obj = _mapper.Map<CementJobDensAdd>(cementJob.CementStage.CementingFluid.CementAdditive.DensAdd);
                _db.CementJobDensAdds.Add(obj);
               
            }
        }
        private void Concentration(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.CementAdditive.Concentration.Uom != null)
            {
                var obj = _mapper.Map<CementJobConcentration>(cementJob.CementStage.CementingFluid.CementAdditive.Concentration);
                _db.CementJobConcentrations.Add(obj);
               
            }
        }
        private void Additive(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.CementAdditive.Additive.Uom != null)
            {
                var obj = _mapper.Map<CementJobAdditive>(cementJob.CementStage.CementingFluid.CementAdditive.Additive);
                _db.CementJobAdditives.Add(obj);
               
            }
        }
        private void CementAdditive(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.CementAdditive.Uid != null)
            {
                var obj = _mapper.Map<CementJobCementAdditive>(cementJob.CementStage.CementingFluid.CementAdditive);
                _db.CementJobCementAdditives.Add(obj);
               
            }
        }
        private void VolGasFoam(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.VolGasFoam.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolGasFoam>(cementJob.CementStage.CementingFluid.VolGasFoam);
                _db.CementJobVolGasFoams.Add(obj);
               
            }
        }
        private void RatioConstGasMethodAv(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.RatioConstGasMethodAv.Uom != null)
            {
                var obj = _mapper.Map<CementJobRatioConstGasMethodAv>(cementJob.CementStage.CementingFluid.RatioConstGasMethodAv);
                _db.CementJobRatioConstGasMethodAvs.Add(obj);
               
            }
        }
        private void DensConstGasMethod(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.DensConstGasMethod.Uom != null)
            {
                var obj = _mapper.Map<CementJobDensConstGasMethod>(cementJob.CementStage.CementingFluid.DensConstGasMethod);
                _db.CementJobDensConstGasMethods.Add(obj);
               
            }
        }
        private void RatioConstGasMethodStart(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.RatioConstGasMethodStart.Uom != null)
            {
                var obj = _mapper.Map<CementJobRatioConstGasMethodStart>(cementJob.CementStage.CementingFluid.RatioConstGasMethodStart);
                _db.CementJobRatioConstGasMethodStarts.Add(obj);
               
            }
        }

        private void RatioConstGasMethodEnd(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.RatioConstGasMethodEnd.Uom != null)
            {
                var obj = _mapper.Map<CementJobRatioConstGasMethodEnd>(cementJob.CementStage.CementingFluid.RatioConstGasMethodEnd);
                _db.CementJobRatioConstGasMethodEnds.Add(obj);
               
            }
        }
        private void DensConstGasFoam(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.DensConstGasFoam.Uom != null)
            {
                var obj = _mapper.Map<CementJobDensConstGasFoam>(cementJob.CementStage.CementingFluid.DensConstGasFoam);
                _db.CementJobDensConstGasFoams.Add(obj);
               
            }
        }
        private void ETimThickening(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.ETimThickening.Uom != null)
            {
                var obj = _mapper.Map<CementJobETimThickening>(cementJob.CementStage.CementingFluid.ETimThickening);
                _db.CementJobETimThickenings.Add(obj);
               
            }
        }
        private void TempThickening(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.TempThickening.Uom != null)
            {
                var obj = _mapper.Map<CementJobTempThickening>(cementJob.CementStage.CementingFluid.TempThickening);
                _db.CementJobTempThickenings.Add(obj);
               
            }
        }
        private void PresTestThickening(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.PresTestThickening.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresTestThickening>(cementJob.CementStage.CementingFluid.PresTestThickening);
                _db.CementJobPresTestThickenings.Add(obj);
               
            }
        }
        private void ConsTestThickening(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.ConsTestThickening.Uom != null)
            {
                var obj = _mapper.Map<CementJobConsTestThickening>(cementJob.CementStage.CementingFluid.ConsTestThickening);
                _db.CementJobConsTestThickenings.Add(obj);
               
            }

        }
        private void PcFreeWater(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.PcFreeWater.Uom != null)
            {
                var obj = _mapper.Map<CementJobPcFreeWater>(cementJob.CementStage.CementingFluid.PcFreeWater);
                _db.CementJobPcFreeWaters.Add(obj);
               
            }
        }
        private void TempFreeWater(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.TempFreeWater.Uom != null)
            {
                var obj = _mapper.Map<CementJobTempFreeWater>(cementJob.CementStage.CementingFluid.TempFreeWater);
                _db.CementJobTempFreeWaters.Add(obj);
               
            }
        }
        private void VolTestFluidLoss(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.VolTestFluidLoss.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolTestFluidLoss>(cementJob.CementStage.CementingFluid.VolTestFluidLoss);
                _db.CementJobVolTestFluidLosss.Add(obj);
               
            }
        }
        private void TempFluidLoss(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.TempFluidLoss.Uom != null)
            {
                var obj = _mapper.Map<CementJobTempFluidLoss>(cementJob.CementStage.CementingFluid.TempFluidLoss);
                _db.CementJobTempFluidLosss.Add(obj);
               
            }
        }
        private void PresTestFluidLoss(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.PresTestFluidLoss.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresTestFluidLoss>(cementJob.CementStage.CementingFluid.PresTestFluidLoss);
                _db.CementJobPresTestFluidLosss.Add(obj);
               
            }
        }
        private void TimeFluidLoss(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.TimeFluidLoss.Uom != null)
            {
                var obj = _mapper.Map<CementJobTimeFluidLoss>(cementJob.CementStage.CementingFluid.TimeFluidLoss);
                _db.CementJobTimeFluidLosss.Add(obj);
               
            }
        }
        private void VolAPIFluidLoss(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.VolAPIFluidLoss.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolAPIFluidLoss>(cementJob.CementStage.CementingFluid.VolAPIFluidLoss);
                _db.CementJobVolAPIFluidLosss.Add(obj);
               
            }
        }
        private void ETimComprStren1(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.ETimComprStren1.Uom != null)
            {
                var obj = _mapper.Map<CementJobETimComprStren1>(cementJob.CementStage.CementingFluid.ETimComprStren1);
                _db.CementJobETimComprStren1s.Add(obj);
               
            }
        }
        private void ETimComprStren2(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.ETimComprStren2.Uom != null)
            {
                var obj = _mapper.Map<CementJobETimComprStren2>(cementJob.CementStage.CementingFluid.ETimComprStren2);
                _db.CementJobETimComprStren2s.Add(obj);
               
            }
        }
        private void PresComprStren1(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.PresComprStren1.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresComprStren1>(cementJob.CementStage.CementingFluid.PresComprStren1);
                _db.CementJobPresComprStren1s.Add(obj);
               
            }
        }
        private void PresComprStren2(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.PresComprStren2.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresComprStren2>(cementJob.CementStage.CementingFluid.PresComprStren2);
                _db.CementJobPresComprStren2s.Add(obj);
               
            }
        }
        private void TempComprStren1(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.TempComprStren1.Uom != null)
            {
                var obj = _mapper.Map<CementJobTempComprStren1>(cementJob.CementStage.CementingFluid.TempComprStren1);
                _db.CementJobTempComprStren1s.Add(obj);
               
            }
        }
        private void TempComprStren2(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.TempComprStren2.Uom != null)
            {
                var obj = _mapper.Map<CementJobTempComprStren2>(cementJob.CementStage.CementingFluid.TempComprStren2);
                _db.CementJobTempComprStren2s.Add(obj);
               
            }
        }
        private void DensAtPres(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.DensAtPres.Uom != null)
            {
                var obj = _mapper.Map<CementJobDensAtPres>(cementJob.CementStage.CementingFluid.DensAtPres);
                _db.CementJobDensAtPress.Add(obj);
               
            }
        }
        private void VolReserved(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.VolReserved.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolReserved>(cementJob.CementStage.CementingFluid.VolReserved);
                _db.CementJobVolReserveds.Add(obj);
               
            }
        }
        private void VolTotSlurry(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.VolTotSlurry.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolTotSlurry>(cementJob.CementStage.CementingFluid.VolTotSlurry);
                _db.CementJobVolTotSlurrys.Add(obj);
               
            }
        }
        private void CementingFluid(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.FluidIndex != 0)
            {
                var obj = _mapper.Map<CementJobCementingFluid>(cementJob.CementStage.CementingFluid);
                
                _db.CementJobCementingFluids.Add(obj);
              
               
            }
        }
        private void CementPumpSchedule(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.CementPumpSchedule.ETimPump.Uom != null)
            {
                var obj = _mapper.Map<CementJobCementPumpSchedule>(cementJob.CementStage.CementingFluid.CementPumpSchedule);
                _db.CementJobCementPumpSchedules.Add(obj);
               
            }
        }
        private void Density(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.Density.Uom != null)
            {
                var obj = _mapper.Map<CementJobDensity>(cementJob.CementStage.CementingFluid.Density);
                _db.CementJobDensitys.Add(obj);
               
            }
        }
        private void MdString(CementJob cementJob)
        {
            if (cementJob.CementStage.MdString.Uom != null)
            {
                var obj = _mapper.Map<CementJobMdString>(cementJob.CementStage.MdString);
                _db.CementJobMdStrings.Add(obj);
               
            }
        }

        private void MdTool(CementJob cementJob)
        {
            if (cementJob.CementStage.MdTool.Uom != null)
            {
                var obj = _mapper.Map<CementJobMdTool>(cementJob.CementStage.MdTool);
                _db.CementJobMdTools.Add(obj);
               
            }
        }

        private void MdCoilTbg(CementJob cementJob)
        {
            if (cementJob.CementStage.MdCoilTbg.Uom != null)
            {
                var obj = _mapper.Map<CementJobMdCoilTbg>(cementJob.CementStage.MdCoilTbg);
                _db.CementJobMdCoilTbgs.Add(obj);
               
            }
        }

        private void VolCsgIn(CementJob cementJob)
        {
            if (cementJob.CementStage.VolCsgIn.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolCsgIn>(cementJob.CementStage.VolCsgIn);
                _db.CementJobVolCsgIns.Add(obj);
               
            }
        }

        private void VolCsgOut(CementJob cementJob)
        {
            if (cementJob.CementStage.VolCsgOut.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolCsgOut>(cementJob.CementStage.VolCsgOut);
                _db.CementJobVolCsgOuts.Add(obj);
               
            }
        }

        private void DiaTailPipe(CementJob cementJob)
        {
            if (cementJob.CementStage.DiaTailPipe.Uom != null)
            {
                var obj = _mapper.Map<CementJobDiaTailPipe>(cementJob.CementStage.DiaTailPipe);
                _db.CementJobDiaTailPipes.Add(obj);
               
            }
        }

        private void PresTbgStart(CementJob cementJob)
        {
            if (cementJob.CementStage.PresTbgStart.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresTbgStart>(cementJob.CementStage.PresTbgStart);
                _db.CementJobPresTbgStarts.Add(obj);
               
            }
        }

        private void PresTbgEnd(CementJob cementJob)
        {
            if (cementJob.CementStage.PresTbgEnd.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresTbgEnd>(cementJob.CementStage.PresTbgEnd);
                _db.CementJobPresTbgEnds.Add(obj);
               
            }
        }

       

        private void PresCsgStart(CementJob cementJob)
        {
            if (cementJob.CementStage.PresCsgStart.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresCsgStart>(cementJob.CementStage.PresCsgStart);
                _db.CementJobPresCsgStarts.Add(obj);
               
            }
        }

        private void PresCsgEnd(CementJob cementJob)
        {
            if (cementJob.CementStage.PresCsgEnd.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresCsgEnd>(cementJob.CementStage.PresCsgEnd);
                _db.CementJobPresCsgEnds.Add(obj);
               
            }
        }
        private void PresBackPressure(CementJob cementJob)
        {
            if (cementJob.CementStage.PresBackPressure.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresBackPressure>(cementJob.CementStage.PresBackPressure);
                _db.CementJobPresBackPressures.Add(obj);
               
            }
        }
        private void PresCoilTbgStart(CementJob cementJob)
        {
            if (cementJob.CementStage.PresCoilTbgStart.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresCoilTbgStart>(cementJob.CementStage.PresCoilTbgStart);
                _db.CementJobPresCoilTbgStarts.Add(obj);
               
            }
        }
        private void PresCoilTbgEnd(CementJob cementJob)
        {
            if (cementJob.CementStage.PresCoilTbgEnd.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresCoilTbgEnd>(cementJob.CementStage.PresCoilTbgEnd);
                _db.CementJobPresCoilTbgEnds.Add(obj);
               
            }
        }
        private void PresBreakDown(CementJob cementJob)
        {
            if (cementJob.CementStage.PresBreakDown.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresBreakDown>(cementJob.CementStage.PresBreakDown);
                _db.CementJobPresBreakDowns.Add(obj);
               
            }
        }
        private void FlowrateBreakDown(CementJob cementJob)
        {
            if (cementJob.CementStage.FlowrateBreakDown.Uom != null)
            {
                var obj = _mapper.Map<CementJobFlowrateBreakDown>(cementJob.CementStage.FlowrateBreakDown);
                _db.CementJobFlowrateBreakDowns.Add(obj);
               
            }
        }
        private void PresSqueezeAv(CementJob cementJob)
        {
            if (cementJob.CementStage.PresSqueezeAv.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresSqueezeAv>(cementJob.CementStage.PresSqueezeAv);
                _db.CementJobPresSqueezeAvs.Add(obj);
               
            }
        }
        private void PresSqueezeEnd(CementJob cementJob)
        {
            if (cementJob.CementStage.PresSqueezeEnd.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresSqueezeEnd>(cementJob.CementStage.PresSqueezeEnd);
                _db.CementJobPresSqueezeEnds.Add(obj);
               
            }
        }
        private void PresSqueeze(CementJob cementJob)
        {
            if (cementJob.CementStage.PresSqueeze.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresSqueeze>(cementJob.CementStage.PresSqueeze);
                _db.CementJobPresSqueezes.Add(obj);
               
            }
        }
        private void ETimPresHeld(CementJob cementJob)
        {
            if (cementJob.CementStage.ETimPresHeld.Uom != null)
            {
                var obj = _mapper.Map<CementJobETimPresHeld>(cementJob.CementStage.ETimPresHeld);
                _db.CementJobETimPresHelds.Add(obj);
               
            }
        }
        private void FlowrateSqueezeAv(CementJob cementJob)
        {
            if (cementJob.CementStage.FlowrateSqueezeAv.Uom != null)
            {
                var obj = _mapper.Map<CementJobFlowrateSqueezeAv>(cementJob.CementStage.FlowrateSqueezeAv);
                _db.CementJobFlowrateSqueezeAvs.Add(obj);
               
            }
        }
        
        private void FlowrateSqueezeMx(CementJob cementJob)
        {
            if (cementJob.CementStage.FlowrateSqueezeMx.Uom != null)
            {
                var obj = _mapper.Map<CementJobFlowrateSqueezeMx>(cementJob.CementStage.FlowrateSqueezeMx);
                _db.CementJobFlowrateSqueezeMxs.Add(obj);
               
            }
        }
        private void FlowratePumpStart(CementJob cementJob)
        {
            if (cementJob.CementStage.FlowratePumpStart.Uom != null)
            {
                var obj = _mapper.Map<CementJobFlowratePumpStart>(cementJob.CementStage.FlowratePumpStart);
                _db.CementJobFlowratePumpStarts.Add(obj);
               
            }
        }
        private void FlowratePumpEnd(CementJob cementJob)
        {
            if (cementJob.CementStage.FlowratePumpEnd.Uom != null)
            {
                var obj = _mapper.Map<CementJobFlowratePumpEnd>(cementJob.CementStage.FlowratePumpEnd);
                _db.CementJobFlowratePumpEnds.Add(obj);
               
            }
        }
        private void MdCircOut(CementJob cementJob)
        {
            if (cementJob.CementStage.MdCircOut.Uom != null)
            {
                var obj = _mapper.Map<CementJobMdCircOut>(cementJob.CementStage.MdCircOut);
                _db.CementJobMdCircOuts.Add(obj);
               
            }
        }
        private void VolCircPrior(CementJob cementJob)
        {
            if (cementJob.CementStage.VolCircPrior.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolCircPrior>(cementJob.CementStage.VolCircPrior);
                _db.CementJobVolCircPriors.Add(obj);
               
            }
        }
        private void VisFunnelMud(CementJob cementJob)
        {
            if (cementJob.CementStage.VisFunnelMud.Uom != null)
            {
                var obj = _mapper.Map<CementJobVisFunnelMud>(cementJob.CementStage.VisFunnelMud);
                _db.CementJobVisFunnelMuds.Add(obj);
               
            }
        }
        private void PvMud(CementJob cementJob)
        {
            if (cementJob.CementStage.PvMud.Uom != null)
            {
                var obj = _mapper.Map<CementJobPvMud>(cementJob.CementStage.PvMud);
                _db.CementJobPvMuds.Add(obj);
               
            }
        }
        private void YpMud(CementJob cementJob)
        {
            if (cementJob.CementStage.YpMud.Uom != null)
            {
                var obj = _mapper.Map<CementJobYpMud>(cementJob.CementStage.YpMud);
                _db.CementJobYpMuds.Add(obj);
               
            }
        }
        private void Gel10Sec(CementJob cementJob)
        {
            if (cementJob.CementStage.Gel10Sec.Uom != null)
            {
                var obj = _mapper.Map<CementJobGel10Sec>(cementJob.CementStage.Gel10Sec);
                _db.CementJobGel10Secs.Add(obj);
               
            }
        }
        private void Gel10Min(CementJob cementJob)
        {
            if (cementJob.CementStage.Gel10Min.Uom != null)
            {
                var obj = _mapper.Map<CementJobGel10Min>(cementJob.CementStage.Gel10Min);
                _db.CementJobGel10Mins.Add(obj);
               
            }
        }
        private void PresPriorBump(CementJob cementJob)
        {
            if (cementJob.CementStage.PresPriorBump.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresPriorBump>(cementJob.CementStage.PresPriorBump);
                _db.CementJobPresPriorBumps.Add(obj);
               
            }
        }
        private void PresBump(CementJob cementJob)
        {
            if (cementJob.CementStage.PresBump.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresBump>(cementJob.CementStage.PresBump);
                _db.CementJobPresBumps.Add(obj);
               
            }
        }
        private void PresHeld(CementJob cementJob)
        {
            if (cementJob.CementStage.PresHeld.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresHeld>(cementJob.CementStage.PresHeld);
                _db.CementJobPresHelds.Add(obj);
               
            }
        }
        private void VolMudLost(CementJob cementJob)
        {
            if (cementJob.CementStage.VolMudLost.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolMudLost>(cementJob.CementStage.VolMudLost);
                _db.CementJobVolMudLosts.Add(obj);
               
            }
        }
        private void DensDisplaceFluid(CementJob cementJob)
        {
            if (cementJob.CementStage.DensDisplaceFluid.Uom != null)
            {
                var obj = _mapper.Map<CementJobDensDisplaceFluid>(cementJob.CementStage.DensDisplaceFluid);
                _db.CementJobDensDisplaceFluids.Add(obj);
               
            }
        }
        private void VolDisplaceFluid(CementJob cementJob)
        {
            if (cementJob.CementStage.VolDisplaceFluid.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolDisplaceFluid>(cementJob.CementStage.VolDisplaceFluid);
                _db.CementJobVolDisplaceFluids.Add(obj);
               
            }
        }
        private void CementStage(CementJob cementJob)
        {
            if (cementJob.CementStage.Uid != null)
            {
                var obj = _mapper.Map<CementJobCementStage>(cementJob.CementStage);
                _db.CementJobCementStages.Add(obj);
               
            }
        }
        private void PresTest(CementJob cementJob)
        {
            if (cementJob.CementTest.PresTest.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresTest>(cementJob.CementTest.PresTest);
                _db.CementJobPresTests.Add(obj);
               
            }
        }
        private void ETimTest(CementJob cementJob)
        {
            if (cementJob.CementTest.ETimTest.Uom != null)
            {
                var obj = _mapper.Map<CementJobETimTest>(cementJob.CementTest.ETimTest);
                _db.CementJobETimTests.Add(obj);
               
            }
        }
        private void CblPres(CementJob cementJob)
        {
            if (cementJob.CementTest.CblPres.Uom != null)
            {
                var obj = _mapper.Map<CementJobCblPres>(cementJob.CementTest.CblPres);
                _db.CementJobCblPress.Add(obj);
               
            }
        }
        private void ETimCementLog(CementJob cementJob)
        {
            if (cementJob.CementTest.ETimCementLog.Uom != null)
            {
                var obj = _mapper.Map<CementJobETimCementLog>(cementJob.CementTest.ETimCementLog);
                _db.CementJobETimCementLogs.Add(obj);
               
            }
        }
        private void FormPit(CementJob cementJob)
        {
            if (cementJob.CementTest.FormPit.Uom != null)
            {
                var obj = _mapper.Map<CementJobFormPit>(cementJob.CementTest.FormPit);
                _db.CementJobFormPits.Add(obj);
               
            }
        }
        private void ETimPitStart(CementJob cementJob)
        {
            if (cementJob.CementTest.ETimPitStart.Uom != null)
            {
                var obj = _mapper.Map<CementJobETimPitStart>(cementJob.CementTest.ETimPitStart);
                _db.CementJobETimPitStarts.Add(obj);
               
            }
        }
        private void MdCementTop(CementJob cementJob)
        {
            if (cementJob.CementTest.MdCementTop.Uom != null)
            {
                var obj = _mapper.Map<CementJobMdCementTop>(cementJob.CementTest.MdCementTop);
                _db.CementJobMdCementTops.Add(obj);
               
            }
        }
        private void LinerTop(CementJob cementJob)
        {
            if (cementJob.CementTest.LinerTop.Uom != null)
            {
                var obj = _mapper.Map<CementJobLinerTop>(cementJob.CementTest.LinerTop);
                _db.CementJobLinerTops.Add(obj);
               
            }
        }
        private void LinerLap(CementJob cementJob)
        {
            if (cementJob.CementTest.LinerLap.Uom != null)
            {
                var obj = _mapper.Map<CementJobLinerLap>(cementJob.CementTest.LinerLap);
                _db.CementJobLinerLaps.Add(obj);
               
            }
        }

        private void TempBHCT(CementJob cementJob)
        {
            if (cementJob.CementStage.TempBHCT.Uom != null)
            {
                var obj = _mapper.Map<CementJobTempBHCT>(cementJob.CementStage.TempBHCT);
                _db.CementJobTempBHCTs.Add(obj);

            }
        }

        private void TempBHST(CementJob cementJob)
        {
            if (cementJob.CementStage.TempBHST.Uom != null)
            {
                var obj = _mapper.Map<CementJobTempBHST>(cementJob.CementStage.TempBHST);
                _db.CementJobTempBHSTs.Add(obj);

            }
        }

        private void ETimBeforeTest(CementJob cementJob)
        {
            if (cementJob.CementTest.ETimBeforeTest.Uom != null)
            {
                var obj = _mapper.Map<CementJobETimBeforeTest>(cementJob.CementTest.ETimBeforeTest);
                _db.CementJobETimBeforeTests.Add(obj);
               
            }
        }
        private void TestNegativeEmw(CementJob cementJob)
        {
            if (cementJob.CementTest.TestNegativeEmw.Uom != null)
            {
                var obj = _mapper.Map<CementJobTestNegativeEmw>(cementJob.CementTest.TestNegativeEmw);
                _db.CementJobTestNegativeEmws.Add(obj);
               
            }
        }

        private void TestPositiveEmw(CementJob cementJob)
        {
            if (cementJob.CementTest.TestPositiveEmw.Uom != null)
            {
                var obj = _mapper.Map<CementJobTestPositiveEmw>(cementJob.CementTest.TestPositiveEmw);
                _db.CementJobTestPositiveEmws.Add(obj);
               
            }
        }
        private void MdDVTool(CementJob cementJob)
        {
            if (cementJob.CementTest.MdDVTool.Uom != null)
            {
                var obj = _mapper.Map<CementJobMdDVTool>(cementJob.CementTest.MdDVTool);
                _db.CementJobMdDVTools.Add(obj);
               
            }
        }
        private void CementTest(CementJob cementJob)
        {
            if (cementJob.CementTest.PresTest != null)
            {
                var obj = _mapper.Map<CementJobCementTest>(cementJob.CementTest);
                _db.CementJobCementTests.Add(obj);
               
              
            }
        }
        private void MdSqueeze(CementJob cementJob)
        {
            if (cementJob.MdSqueeze.Uom != null)
            {
                var obj = _mapper.Map<CementJobMdSqueeze>(cementJob.MdSqueeze);
                _db.CementJobMdSqueezes.Add(obj);
               
            }
        }
        private void RpmPipe(CementJob cementJob)
        {
            if (cementJob.RpmPipe.Uom != null)
            {
                var obj = _mapper.Map<CementJobRpmPipe>(cementJob.RpmPipe);
                _db.CementJobRpmPipes.Add(obj);
               
            }
        }
        private void TqInitPipeRot(CementJob cementJob)
        {
            if (cementJob.TqInitPipeRot.Uom != null)
            {
                var obj = _mapper.Map<CementJobTqInitPipeRot>(cementJob.TqInitPipeRot);
                _db.CementJobTqInitPipeRots.Add(obj);
               
            }
        }
        private void TqPipeAv(CementJob cementJob)
        {
            if (cementJob.TqPipeAv.Uom != null)
            {
                var obj = _mapper.Map<CementJobTqPipeAv>(cementJob.TqPipeAv);
                _db.CementJobTqPipeAvs.Add(obj);
               
            }
        }
        private void TqPipeMx(CementJob cementJob)
        {
            if (cementJob.TqPipeMx.Uom != null)
            {
                var obj = _mapper.Map<CementJobTqPipeMx>(cementJob.TqPipeMx);
                _db.CementJobTqPipeMxs.Add(obj);
               
            }
        }
        private void OverPull(CementJob cementJob)
        {
            if (cementJob.OverPull.Uom != null)
            {
                var obj = _mapper.Map<CementJobOverPull>(cementJob.OverPull);
                _db.CementJobOverPulls.Add(obj);
               
            }
        }
        private void SlackOff(CementJob cementJob)
        {
            if (cementJob.SlackOff.Uom != null)
            {
                var obj = _mapper.Map<CementJobSlackOff>(cementJob.SlackOff);
                _db.CementJobSlackOffs.Add(obj);
               
            }
        }
        private void RpmPipeRecip(CementJob cementJob)
        {
            if (cementJob.RpmPipeRecip.Uom != null)
            {
                var obj = _mapper.Map<CementJobRpmPipeRecip>(cementJob.RpmPipeRecip);
                _db.CementJobRpmPipeRecips.Add(obj);
               
            }
        }

        private void MdTop(CementJob cementJob)
        {
            if (cementJob.CementStage.MdTop.Uom != null)
            {
                var obj = _mapper.Map<CementJobMdTop>(cementJob.CementStage.MdTop);
                _db.CementJobMdTops.Add(obj);

            }
        }

        private void MdBottom(CementJob cementJob)
        {
            if (cementJob.CementStage.MdBottom.Uom != null)
            {
                var obj = _mapper.Map<CementJobMdBottom>(cementJob.CementStage.MdBottom);
                _db.CementJobMdBottoms.Add(obj);

            }
        }
        


        private void LenPipeRecipStroke(CementJob cementJob)
        {
            if (cementJob.LenPipeRecipStroke.Uom != null)
            {
                var obj = _mapper.Map<CementJobLenPipeRecipStroke>(cementJob.LenPipeRecipStroke);
                _db.CementJobLenPipeRecipStrokes.Add(obj);
               
            }
        }
        private void CementJobCommonData(CementJob cementJob)
        {
            
                var obj = _mapper.Map<CementJobCommonData>(cementJob.CommonData);
                _db.CementJobCommonDatas.Add(obj);
           

        }

        #endregion Create CementJob Method

        #region Update CementJob Method
        private void UpdateMdWater(CementJob cementJob)
        {
            if (cementJob.MdWater.Uom != null)
            {
                var obj = _mapper.Map<CementJobMdWater>(cementJob.MdWater);
                _db.CementJobMdWaters.Update(obj);
            }
        }
        private void UpdateWoc(CementJob cementJob)
        {
            if (cementJob.Woc.Uom != null)
            {
                var obj = _mapper.Map<CementJobWoc>(cementJob.Woc);
                _db.CementJobWocs.Update(obj);
            }
        }

        private void UpdateMdPlugTop(CementJob cementJob)
        {
            if (cementJob.MdPlugTop.Uom != null)
            {
                var obj = _mapper.Map<CementJobMdPlugTop>(cementJob.MdPlugTop);
                _db.CementJobMdPlugTops.Update(obj);

            }
        }
        private void UpdateMdPlugBot(CementJob cementJob)
        {
            if (cementJob.MdPlugBot.Uom != null)
            {
                var obj = _mapper.Map<CementJobMdPlugBot>(cementJob.MdPlugBot);
                _db.CementJobMdPlugBots.Update(obj);
            }
        }
        private void UpdateMdHole(CementJob cementJob)
        {
            if (cementJob.MdHole.Uom != null)
            {
                var obj = _mapper.Map<CementJobMdHole>(cementJob.MdHole);
                _db.CementJobMdHoles.Update(obj);
            }
        }
        private void UpdateMdShoe(CementJob cementJob)
        {
            if (cementJob.MdShoe.Uom != null)
            {
                var obj = _mapper.Map<CementJobMdShoe>(cementJob.MdShoe);
                _db.CementJobMdShoes.Update(obj);
            }
        }
        private void UpdateTvdShoe(CementJob cementJob)
        {
            if (cementJob.TvdShoe.Uom != null)
            {
                var obj = _mapper.Map<CementJobTvdShoe>(cementJob.TvdShoe);
                _db.CementJobTvdShoes.Update(obj);
            }
        }
        private void UpdateMdStringSet(CementJob cementJob)
        {
            if (cementJob.MdStringSet.Uom != null)
            {
                var obj = _mapper.Map<CementJobMdStringSet>(cementJob.MdStringSet);
                _db.CementJobMdStringSets.Update(obj);
            }
        }
        private void UpdateTvdStringSet(CementJob cementJob)
        {
            if (cementJob.TvdStringSet.Uom != null)
            {
                var obj = _mapper.Map<CementJobTvdStringSet>(cementJob.TvdStringSet);
                _db.CementJobTvdStringSets.Update(obj);
            }
        }
        private void UpdateVolExcess(CementJob cementJob)
        {
            if (cementJob.CementStage.VolExcess.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolExcess>(cementJob.CementStage.VolExcess.Uom);
                _db.CementJobVolExcesss.Update(obj);
            }
        }
        private void UpdateFlowrateDisplaceAv(CementJob cementJob)
        {
            if (cementJob.CementStage.FlowrateDisplaceAv.Uom != null)
            {
                var obj = _mapper.Map<CementJobFlowrateDisplaceAv>(cementJob.CementStage.FlowrateDisplaceAv);
                _db.CementJobFlowrateDisplaceAvs.Update(obj);
            }
        }
        private void UpdateFlowrateDisplaceMx(CementJob cementJob)
        {
            if (cementJob.CementStage.FlowrateDisplaceMx.Uom != null)
            {
                var obj = _mapper.Map<CementJobFlowrateDisplaceMx>(cementJob.CementStage.FlowrateDisplaceMx);
                _db.CementJobFlowrateDisplaceMxs.Update(obj);
            }
        }
        private void UpdatePresDisplace(CementJob cementJob)
        {
            if (cementJob.CementStage.PresDisplace.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresDisplace>(cementJob.CementStage.PresDisplace);
                _db.CementJobPresDisplaces.Update(obj);
            }
        }
        private void UpdateVolReturns(CementJob cementJob)
        {
            if (cementJob.CementStage.VolReturns.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolReturns>(cementJob.CementStage.VolReturns);
                _db.CementJobVolReturnss.Update(obj);
            }
        }
        private void UpdateETimMudCirculation(CementJob cementJob)
        {
            if (cementJob.CementStage.ETimMudCirculation.Uom != null)
            {
                var obj = _mapper.Map<CementJobETimMudCirculation>(cementJob.CementStage.ETimMudCirculation);
                _db.CementJobETimMudCirculations.Update(obj);
            }
        }
        private void UpdateFlowrateMudCirc(CementJob cementJob)
        {
            if (cementJob.CementStage.FlowrateMudCirc.Uom != null)
            {
                var obj = _mapper.Map<CementJobFlowrateMudCirc>(cementJob.CementStage.FlowrateMudCirc);
                _db.CementJobFlowrateMudCircs.Update(obj);
            }
        }
        private void UpdatePresMudCirc(CementJob cementJob)
        {
            if (cementJob.CementStage.PresMudCirc.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresMudCirc>(cementJob.CementStage.PresMudCirc);
                _db.CementJobPresMudCircs.Update(obj);
            }
        }
        private void UpdateFlowrateEnd(CementJob cementJob)
        {
            if (cementJob.CementStage.FlowrateEnd.Uom != null)
            {
                var obj = _mapper.Map<CementJobFlowrateEnd>(cementJob.CementStage.FlowrateEnd);
                _db.CementJobFlowrateEnds.Update(obj);
            }
        }
        private void UpdateMdFluidTop(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.MdFluidTop.Uom != null)
            {
                var obj = _mapper.Map<CementJobMdFluidTop>(cementJob.CementStage.CementingFluid.MdFluidTop.Uom);
                _db.CementJobMdFluidTops.Update(obj);
            }
        }
        private void UpdateMdFluidBottom(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.MdFluidBottom.Uom != null)
            {
                var obj = _mapper.Map<CementJobMdFluidBottom>(cementJob.CementStage.CementingFluid.MdFluidBottom);
                _db.CementJobMdFluidBottoms.Update(obj);
            }
        }
        private void UpdateVolWater(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.VolWater.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolWater>(cementJob.CementStage.CementingFluid.VolWater.Uom);
                _db.CementJobVolWaters.Update(obj);
            }
        }
        private void UpdateVolCement(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.VolCement.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolCement>(cementJob.CementStage.CementingFluid.VolCement);
                _db.CementJobVolCements.Update(obj);
            }
        }
        private void UpdateRatioMixWater(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.RatioMixWater.Uom != null)
            {
                var obj = _mapper.Map<CementJobRatioMixWater>(cementJob.CementStage.CementingFluid.RatioMixWater);
                _db.CementJobRatioMixWaters.Update(obj);
            }
        }
        private void UpdateVolFluid(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.VolFluid.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolFluid>(cementJob.CementStage.CementingFluid.VolFluid);
                _db.CementJobVolFluids.Update(obj);
            }
        }
        private void UpdateETimPump(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.CementPumpSchedule.ETimPump.Uom != null)
            {
                var obj = _mapper.Map<CementJobETimPump>(cementJob.CementStage.CementingFluid.CementPumpSchedule.ETimPump);
                _db.CementJobETimPumps.Update(obj);
            }
        }
        private void UpdateRatePump(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.CementPumpSchedule.RatePump.Uom != null)
            {
                var obj = _mapper.Map<CementJobRatePump>(cementJob.CementStage.CementingFluid.CementPumpSchedule.RatePump);
                _db.CementJobRatePumps.Update(obj);
            }
        }
        private void UpdateVolPump(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.CementPumpSchedule.VolPump.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolPump>(cementJob.CementStage.CementingFluid.CementPumpSchedule.VolPump);
                _db.CementJobVolPumps.Update(obj);
            }
        }
        private void UpdatePresBack(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.CementPumpSchedule.PresBack.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresBack>(cementJob.CementStage.CementingFluid.CementPumpSchedule.PresBack);
                _db.CementJobPresBacks.Update(obj);
            }
        }
        private void UpdateETimShutdown(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.CementPumpSchedule.ETimShutdown.Uom != null)
            {
                var obj = _mapper.Map<CementJobETimShutdown>(cementJob.CementStage.CementingFluid.CementPumpSchedule.ETimShutdown);
                _db.CementJobETimShutdowns.Update(obj);
            }
        }

        private void UpdateExcessPc(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.ExcessPc.Uom != null)
            {
                var obj = _mapper.Map<CementJobExcessPc>(cementJob.CementStage.CementingFluid.ExcessPc);
                _db.CementJobExcessPcs.Update(obj);
            }
        }
        private void UpdateVolYield(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.VolYield.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolYield>(cementJob.CementStage.CementingFluid.VolYield);
                _db.CementJobVolYields.Update(obj);
            }
        }
        private void UpdateSolidVolumeFraction(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.SolidVolumeFraction.Uom != null)
            {
                var obj = _mapper.Map<CementJobSolidVolumeFraction>(cementJob.CementStage.CementingFluid.SolidVolumeFraction);
                _db.CementJobSolidVolumeFractions.Update(obj);
            }
        }
        private void UpdateVolPumped(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.VolPumped.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolPumped>(cementJob.CementStage.CementingFluid.VolPumped);
                _db.CementJobVolPumpeds.Update(obj);
            }
        }
        private void UpdateVolOther(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.VolOther.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolOther>(cementJob.CementStage.CementingFluid.VolOther);
                _db.CementJobVolOthers.Update(obj);
            }
        }
        private void UpdateVis(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.Vis.Uom != null)
            {
                var obj = _mapper.Map<CementJobVis>(cementJob.CementStage.CementingFluid.Vis);
                _db.CementJobViss.Update(obj);
            }
        }
        private void UpdateYp(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.Yp.Uom != null)
            {
                var obj = _mapper.Map<CementJobYp>(cementJob.CementStage.CementingFluid.Yp);
                _db.CementJobYps.Update(obj);
            }
        }
        private void UpdateN(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.N.Uom != null)
            {
                var obj = _mapper.Map<CementJobN>(cementJob.CementStage.CementingFluid.N);
                _db.CementJobNs.Update(obj);
            }
        }

        private void UpdateK(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.K.Uom != null)
            {
                var obj = _mapper.Map<CementJobK>(cementJob.CementStage.CementingFluid.K);
                _db.CementJobKs.Update(obj);
            }
        }
        private void UpdateGel10SecReading(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.Gel10SecReading.Uom != null)
            {
                var obj = _mapper.Map<CementJobGel10SecReading>(cementJob.CementStage.CementingFluid.Gel10SecReading);
                _db.CementJobGel10SecReadings.Update(obj);
            }
        }
        private void UpdateGel10SecStrength(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.Gel10SecStrength.Uom != null)
            {
                var obj = _mapper.Map<CementJobGel10SecStrength>(cementJob.CementStage.CementingFluid.Gel10SecStrength);
                _db.CementJobGel10SecStrengths.Update(obj);
            }
        }
        private void UpdateGel1MinReading(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.Gel1MinReading.Uom != null)
            {
                var obj = _mapper.Map<CementJobGel1MinReading>(cementJob.CementStage.CementingFluid.Gel1MinReading);
                _db.CementJobGel1MinReadings.Update(obj);
            }
        }
        private void UpdateGel1MinStrength(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.Gel1MinStrength.Text != null)
            {
                var obj = _mapper.Map<CementJobGel1MinStrength>(cementJob.CementStage.CementingFluid.Gel1MinStrength);
                _db.CementJobGel1MinStrengths.Update(obj);
            }
        }
        private void UpdateGel10MinReading(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.Gel10MinReading.Uom != null)
            {
                var obj = _mapper.Map<CementJobGel10MinReading>(cementJob.CementStage.CementingFluid.Gel10MinReading);
                _db.CementJobGel10MinReadings.Update(obj);
            }
        }
        private void UpdateGel10MinStrength(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.Gel10MinStrength.Uom != null)
            {
                var obj = _mapper.Map<CementJobGel10MinStrength>(cementJob.CementStage.CementingFluid.Gel10MinStrength);
                _db.CementJobGel10MinStrengths.Update(obj);
            }
        }

        private void UpdateDensBaseFluid(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.DensBaseFluid.Uom != null)
            {
                var obj = _mapper.Map<CementJobDensBaseFluid>(cementJob.CementStage.CementingFluid.DensBaseFluid);
                _db.CementJobDensBaseFluids.Update(obj);
            }
        }
        private void UpdateMassDryBlend(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.MassDryBlend.Uom != null)
            {
                var obj = _mapper.Map<CementJobMassDryBlend>(cementJob.CementStage.CementingFluid.MassDryBlend);
                _db.CementJobMassDryBlends.Update(obj);
            }
        }
        private void UpdateDensDryBlend(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.DensDryBlend.Uom != null)
            {
                var obj = _mapper.Map<CementJobDensDryBlend>(cementJob.CementStage.CementingFluid.DensDryBlend);
                _db.CementJobDensDryBlends.Update(obj);
            }
        }
        private void UpdateMassSackDryBlend(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.MassSackDryBlend.Uom != null)
            {
                var obj = _mapper.Map<CementJobMassSackDryBlend>(cementJob.CementStage.CementingFluid.MassSackDryBlend);
                _db.CementJobMassSackDryBlends.Update(obj);
            }
        }
        private void UpdateDensAdd(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.CementAdditive.DensAdd.Uom != null)
            {
                var obj = _mapper.Map<CementJobDensAdd>(cementJob.CementStage.CementingFluid.CementAdditive.DensAdd);
                _db.CementJobDensAdds.Update(obj);
            }
        }
        private void UpdateConcentration(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.CementAdditive.Concentration.Uom != null)
            {
                var obj = _mapper.Map<CementJobConcentration>(cementJob.CementStage.CementingFluid.CementAdditive.Concentration);
                _db.CementJobConcentrations.Update(obj);
            }
        }
        private void UpdateAdditive(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.CementAdditive.Additive.Uom != null)
            {
                var obj = _mapper.Map<CementJobAdditive>(cementJob.CementStage.CementingFluid.CementAdditive.Additive);
                _db.CementJobAdditives.Update(obj);
            }
        }
        private void UpdateCementAdditive(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.CementAdditive.Uid != null)
            {
                var obj = _mapper.Map<CementJobCementAdditive>(cementJob.CementStage.CementingFluid.CementAdditive);
                _db.CementJobCementAdditives.Update(obj);
            }
        }
        private void UpdateVolGasFoam(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.VolGasFoam.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolGasFoam>(cementJob.CementStage.CementingFluid.VolGasFoam);
                _db.CementJobVolGasFoams.Update(obj);
            }
        }
        private void UpdateRatioConstGasMethodAv(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.RatioConstGasMethodAv.Uom != null)
            {
                var obj = _mapper.Map<CementJobRatioConstGasMethodAv>(cementJob.CementStage.CementingFluid.RatioConstGasMethodAv);
                _db.CementJobRatioConstGasMethodAvs.Update(obj);
            }
        }
        private void UpdateDensConstGasMethod(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.DensConstGasMethod.Uom != null)
            {
                var obj = _mapper.Map<CementJobDensConstGasMethod>(cementJob.CementStage.CementingFluid.DensConstGasMethod);
                _db.CementJobDensConstGasMethods.Update(obj);
            }
        }
        private void UpdateRatioConstGasMethodStart(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.RatioConstGasMethodStart.Uom != null)
            {
                var obj = _mapper.Map<CementJobRatioConstGasMethodStart>(cementJob.CementStage.CementingFluid.RatioConstGasMethodStart);
                _db.CementJobRatioConstGasMethodStarts.Update(obj);
            }
        }

        private void UpdateRatioConstGasMethodEnd(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.RatioConstGasMethodEnd.Uom != null)
            {
                var obj = _mapper.Map<CementJobRatioConstGasMethodEnd>(cementJob.CementStage.CementingFluid.RatioConstGasMethodEnd);
                _db.CementJobRatioConstGasMethodEnds.Update(obj);
            }
        }
        private void UpdateDensConstGasFoam(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.DensConstGasFoam.Uom != null)
            {
                var obj = _mapper.Map<CementJobDensConstGasFoam>(cementJob.CementStage.CementingFluid.DensConstGasFoam);
                _db.CementJobDensConstGasFoams.Update(obj);
            }
        }
        private void UpdateETimThickening(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.ETimThickening.Uom != null)
            {
                var obj = _mapper.Map<CementJobETimThickening>(cementJob.CementStage.CementingFluid.ETimThickening);
                _db.CementJobETimThickenings.Update(obj);
            }
        }
        private void UpdateTempThickening(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.TempThickening.Uom != null)
            {
                var obj = _mapper.Map<CementJobTempThickening>(cementJob.CementStage.CementingFluid.TempThickening);
                _db.CementJobTempThickenings.Update(obj);
            }
        }
        private void UpdatePresTestThickening(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.PresTestThickening.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresTestThickening>(cementJob.CementStage.CementingFluid.PresTestThickening);
                _db.CementJobPresTestThickenings.Update(obj);
            }
        }
        private void UpdateConsTestThickening(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.ConsTestThickening.Uom != null)
            {
                var obj = _mapper.Map<CementJobConsTestThickening>(cementJob.CementStage.CementingFluid.ConsTestThickening);
                _db.CementJobConsTestThickenings.Update(obj);
            }

        }
        private void UpdatePcFreeWater(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.PcFreeWater.Uom != null)
            {
                var obj = _mapper.Map<CementJobPcFreeWater>(cementJob.CementStage.CementingFluid.PcFreeWater);
                _db.CementJobPcFreeWaters.Update(obj);
            }
        }
        private void UpdateTempFreeWater(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.TempFreeWater.Uom != null)
            {
                var obj = _mapper.Map<CementJobTempFreeWater>(cementJob.CementStage.CementingFluid.TempFreeWater);
                _db.CementJobTempFreeWaters.Update(obj);
            }
        }
        private void UpdateVolTestFluidLoss(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.VolTestFluidLoss.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolTestFluidLoss>(cementJob.CementStage.CementingFluid.VolTestFluidLoss);
                _db.CementJobVolTestFluidLosss.Update(obj);
            }
        }
        private void UpdateTempFluidLoss(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.TempFluidLoss.Uom != null)
            {
                var obj = _mapper.Map<CementJobTempFluidLoss>(cementJob.CementStage.CementingFluid.TempFluidLoss);
                _db.CementJobTempFluidLosss.Update(obj);
            }
        }
        private void UpdatePresTestFluidLoss(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.PresTestFluidLoss.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresTestFluidLoss>(cementJob.CementStage.CementingFluid.PresTestFluidLoss);
                _db.CementJobPresTestFluidLosss.Update(obj);
            }
        }
        private void UpdateTimeFluidLoss(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.TimeFluidLoss.Uom != null)
            {
                var obj = _mapper.Map<CementJobTimeFluidLoss>(cementJob.CementStage.CementingFluid.TimeFluidLoss);
                _db.CementJobTimeFluidLosss.Update(obj);
            }
        }
        private void UpdateVolAPIFluidLoss(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.VolAPIFluidLoss.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolAPIFluidLoss>(cementJob.CementStage.CementingFluid.VolAPIFluidLoss);
                _db.CementJobVolAPIFluidLosss.Update(obj);
            }
        }
        private void UpdateETimComprStren1(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.ETimComprStren1.Uom != null)
            {
                var obj = _mapper.Map<CementJobETimComprStren1>(cementJob.CementStage.CementingFluid.ETimComprStren1);
                _db.CementJobETimComprStren1s.Update(obj);
            }
        }
        private void UpdateETimComprStren2(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.ETimComprStren2.Uom != null)
            {
                var obj = _mapper.Map<CementJobETimComprStren2>(cementJob.CementStage.CementingFluid.ETimComprStren2);
                _db.CementJobETimComprStren2s.Update(obj);
            }
        }
        private void UpdatePresComprStren1(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.PresComprStren1.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresComprStren1>(cementJob.CementStage.CementingFluid.PresComprStren1);
                _db.CementJobPresComprStren1s.Update(obj);
            }
        }
        private void UpdatePresComprStren2(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.PresComprStren2.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresComprStren2>(cementJob.CementStage.CementingFluid.PresComprStren2);
                _db.CementJobPresComprStren2s.Update(obj);
            }
        }
        private void UpdateTempComprStren1(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.TempComprStren1.Uom != null)
            {
                var obj = _mapper.Map<CementJobTempComprStren1>(cementJob.CementStage.CementingFluid.TempComprStren1);
                _db.CementJobTempComprStren1s.Update(obj);
            }
        }
        private void UpdateTempComprStren2(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.TempComprStren2.Uom != null)
            {
                var obj = _mapper.Map<CementJobTempComprStren2>(cementJob.CementStage.CementingFluid.TempComprStren2);
                _db.CementJobTempComprStren2s.Update(obj);
            }
        }
        private void UpdateDensAtPres(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.DensAtPres.Uom != null)
            {
                var obj = _mapper.Map<CementJobDensAtPres>(cementJob.CementStage.CementingFluid.DensAtPres);
                _db.CementJobDensAtPress.Update(obj);
            }
        }
        private void UpdateVolReserved(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.VolReserved.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolReserved>(cementJob.CementStage.CementingFluid.VolReserved);
                _db.CementJobVolReserveds.Update(obj);
            }
        }
        private void UpdateVolTotSlurry(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.VolTotSlurry.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolTotSlurry>(cementJob.CementStage.CementingFluid.VolTotSlurry);
                _db.CementJobVolTotSlurrys.Update(obj);
            }
        }
        private void UpdateCementingFluid(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.FluidIndex != 0)
            {
                var obj = _mapper.Map<CementJobCementingFluid>(cementJob.CementStage.CementingFluid.FluidIndex);
                _db.CementJobCementingFluids.Update(obj);
            }
        }
        private void UpdateCementPumpSchedule(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.CementPumpSchedule.ETimPump.Uom != null)
            {
                var obj = _mapper.Map<CementJobCementPumpSchedule>(cementJob.CementStage.CementingFluid.CementPumpSchedule);
                _db.CementJobCementPumpSchedules.Update(obj);
            }
        }
        private void UpdateDensity(CementJob cementJob)
        {
            if (cementJob.CementStage.CementingFluid.Density.Uom != null)
            {
                var obj = _mapper.Map<CementJobDensity>(cementJob.CementStage.CementingFluid.Density);
                _db.CementJobDensitys.Update(obj);
            }
        }
        private void UpdateMdString(CementJob cementJob)
        {
            if (cementJob.CementStage.MdString.Uom != null)
            {
                var obj = _mapper.Map<CementJobMdString>(cementJob.CementStage.MdString);
                _db.CementJobMdStrings.Update(obj);
            }
        }

        private void UpdateMdTool(CementJob cementJob)
        {
            if (cementJob.CementStage.MdTool.Uom != null)
            {
                var obj = _mapper.Map<CementJobMdTool>(cementJob.CementStage.MdTool);
                _db.CementJobMdTools.Update(obj);
            }
        }

        private void UpdateMdCoilTbg(CementJob cementJob)
        {
            if (cementJob.CementStage.MdCoilTbg.Uom != null)
            {
                var obj = _mapper.Map<CementJobMdCoilTbg>(cementJob.CementStage.MdCoilTbg);
                _db.CementJobMdCoilTbgs.Update(obj);
            }
        }

        private void UpdateVolCsgIn(CementJob cementJob)
        {
            if (cementJob.CementStage.VolCsgIn.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolCsgIn>(cementJob.CementStage.VolCsgIn);
                _db.CementJobVolCsgIns.Update(obj);
            }
        }

        private void UpdateVolCsgOut(CementJob cementJob)
        {
            if (cementJob.CementStage.VolCsgOut.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolCsgOut>(cementJob.CementStage.VolCsgOut);
                _db.CementJobVolCsgOuts.Update(obj);
            }
        }

        private void UpdateDiaTailPipe(CementJob cementJob)
        {
            if (cementJob.CementStage.DiaTailPipe.Uom != null)
            {
                var obj = _mapper.Map<CementJobDiaTailPipe>(cementJob.CementStage.DiaTailPipe);
                _db.CementJobDiaTailPipes.Update(obj);
            }
        }

        private void UpdatePresTbgStart(CementJob cementJob)
        {
            if (cementJob.CementStage.PresTbgStart.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresTbgStart>(cementJob.CementStage.PresTbgStart);
                _db.CementJobPresTbgStarts.Update(obj);
            }
        }

        private void UpdatePresTbgEnd(CementJob cementJob)
        {
            if (cementJob.CementStage.PresTbgEnd.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresTbgEnd>(cementJob.CementStage.PresTbgEnd);
                _db.CementJobPresTbgEnds.Update(obj);
            }
        }

      

        private void UpdatePresCsgStart(CementJob cementJob)
        {
            if (cementJob.CementStage.PresCsgStart.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresCsgStart>(cementJob.CementStage.PresCsgStart);
                _db.CementJobPresCsgStarts.Update(obj);
            }
        }

        private void UpdatePresCsgEnd(CementJob cementJob)
        {
            if (cementJob.CementStage.PresCsgEnd.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresCsgEnd>(cementJob.CementStage.PresCsgEnd);
                _db.CementJobPresCsgEnds.Update(obj);
            }
        }
        private void UpdatePresBackPressure(CementJob cementJob)
        {
            if (cementJob.CementStage.PresBackPressure.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresBackPressure>(cementJob.CementStage.PresBackPressure);
                _db.CementJobPresBackPressures.Update(obj);
            }
        }
        private void UpdatePresCoilTbgStart(CementJob cementJob)
        {
            if (cementJob.CementStage.PresCoilTbgStart.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresCoilTbgStart>(cementJob.CementStage.PresCoilTbgStart);
                _db.CementJobPresCoilTbgStarts.Update(obj);
            }
        }
        private void UpdatePresCoilTbgEnd(CementJob cementJob)
        {
            if (cementJob.CementStage.PresCoilTbgEnd.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresCoilTbgEnd>(cementJob.CementStage.PresCoilTbgEnd);
                _db.CementJobPresCoilTbgEnds.Update(obj);
            }
        }
        private void UpdatePresBreakDown(CementJob cementJob)
        {
            if (cementJob.CementStage.PresBreakDown.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresBreakDown>(cementJob.CementStage.PresBreakDown);
                _db.CementJobPresBreakDowns.Update(obj);
            }
        }
        private void UpdateFlowrateBreakDown(CementJob cementJob)
        {
            if (cementJob.CementStage.FlowrateBreakDown.Uom != null)
            {
                var obj = _mapper.Map<CementJobFlowrateBreakDown>(cementJob.CementStage.FlowrateBreakDown);
                _db.CementJobFlowrateBreakDowns.Update(obj);
            }
        }
        private void UpdatePresSqueezeAv(CementJob cementJob)
        {
            if (cementJob.CementStage.PresSqueezeAv.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresSqueezeAv>(cementJob.CementStage.PresSqueezeAv);
                _db.CementJobPresSqueezeAvs.Update(obj);
            }
        }
        private void UpdatePresSqueezeEnd(CementJob cementJob)
        {
            if (cementJob.CementStage.PresSqueezeEnd.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresSqueezeEnd>(cementJob.CementStage.PresSqueezeEnd);
                _db.CementJobPresSqueezeEnds.Update(obj);
            }
        }
        private void UpdatePresSqueeze(CementJob cementJob)
        {
            if (cementJob.CementStage.PresSqueeze.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresSqueeze>(cementJob.CementStage.PresSqueeze);
                _db.CementJobPresSqueezes.Update(obj);
            }
        }
        private void UpdateETimPresHeld(CementJob cementJob)
        {
            if (cementJob.CementStage.ETimPresHeld.Uom != null)
            {
                var obj = _mapper.Map<CementJobETimPresHeld>(cementJob.CementStage.ETimPresHeld);
                _db.CementJobETimPresHelds.Update(obj);
            }
        }
        private void UpdateFlowrateSqueezeAv(CementJob cementJob)
        {
            if (cementJob.CementStage.FlowrateSqueezeAv.Uom != null)
            {
                var obj = _mapper.Map<CementJobFlowrateSqueezeAv>(cementJob.CementStage.FlowrateSqueezeAv);
                _db.CementJobFlowrateSqueezeAvs.Update(obj);
            }
        }
      
        private void UpdateFlowrateSqueezeMx(CementJob cementJob)
        {
            if (cementJob.CementStage.FlowrateSqueezeMx.Uom != null)
            {
                var obj = _mapper.Map<CementJobFlowrateSqueezeMx>(cementJob.CementStage.FlowrateSqueezeMx);
                _db.CementJobFlowrateSqueezeMxs.Update(obj);
            }
        }
        private void UpdateFlowratePumpStart(CementJob cementJob)
        {
            if (cementJob.CementStage.FlowratePumpStart.Uom != null)
            {
                var obj = _mapper.Map<CementJobFlowratePumpStart>(cementJob.CementStage.FlowratePumpStart);
                _db.CementJobFlowratePumpStarts.Update(obj);
            }
        }
        private void UpdateFlowratePumpEnd(CementJob cementJob)
        {
            if (cementJob.CementStage.FlowratePumpEnd.Uom != null)
            {
                var obj = _mapper.Map<CementJobFlowratePumpEnd>(cementJob.CementStage.FlowratePumpEnd);
                _db.CementJobFlowratePumpEnds.Update(obj);
            }
        }
        private void UpdateMdCircOut(CementJob cementJob)
        {
            if (cementJob.CementStage.MdCircOut.Uom != null)
            {
                var obj = _mapper.Map<CementJobMdCircOut>(cementJob.CementStage.MdCircOut);
                _db.CementJobMdCircOuts.Update(obj);
            }
        }
        private void UpdateVolCircPrior(CementJob cementJob)
        {
            if (cementJob.CementStage.VolCircPrior.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolCircPrior>(cementJob.CementStage.VolCircPrior);
                _db.CementJobVolCircPriors.Update(obj);
            }
        }
        private void UpdateVisFunnelMud(CementJob cementJob)
        {
            if (cementJob.CementStage.VisFunnelMud.Uom != null)
            {
                var obj = _mapper.Map<CementJobVisFunnelMud>(cementJob.CementStage.VisFunnelMud);
                _db.CementJobVisFunnelMuds.Update(obj);
            }
        }
        private void UpdatePvMud(CementJob cementJob)
        {
            if (cementJob.CementStage.PvMud.Uom != null)
            {
                var obj = _mapper.Map<CementJobPvMud>(cementJob.CementStage.PvMud);
                _db.CementJobPvMuds.Update(obj);
            }
        }
        private void UpdateYpMud(CementJob cementJob)
        {
            if (cementJob.CementStage.YpMud.Uom != null)
            {
                var obj = _mapper.Map<CementJobYpMud>(cementJob.CementStage.YpMud);
                _db.CementJobYpMuds.Update(obj);
            }
        }
        private void UpdateGel10Sec(CementJob cementJob)
        {
            if (cementJob.CementStage.Gel10Sec.Uom != null)
            {
                var obj = _mapper.Map<CementJobGel10Sec>(cementJob.CementStage.Gel10Sec);
                _db.CementJobGel10Secs.Update(obj);
            }
        }
        private void UpdateGel10Min(CementJob cementJob)
        {
            if (cementJob.CementStage.Gel10Min.Uom != null)
            {
                var obj = _mapper.Map<CementJobGel10Min>(cementJob.CementStage.Gel10Min);
                _db.CementJobGel10Mins.Update(obj);
            }
        }
        private void UpdatePresPriorBump(CementJob cementJob)
        {
            if (cementJob.CementStage.PresPriorBump.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresPriorBump>(cementJob.CementStage.PresPriorBump);
                _db.CementJobPresPriorBumps.Update(obj);
            }
        }
        private void UpdatePresBump(CementJob cementJob)
        {
            if (cementJob.CementStage.PresBump.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresBump>(cementJob.CementStage.PresBump);
                _db.CementJobPresBumps.Update(obj);
            }
        }
        private void UpdatePresHeld(CementJob cementJob)
        {
            if (cementJob.CementStage.PresHeld.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresHeld>(cementJob.CementStage.PresHeld);
                _db.CementJobPresHelds.Update(obj);
            }
        }
        private void UpdateVolMudLost(CementJob cementJob)
        {
            if (cementJob.CementStage.VolMudLost.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolMudLost>(cementJob.CementStage.VolMudLost);
                _db.CementJobVolMudLosts.Update(obj);
            }
        }
        private void UpdateDensDisplaceFluid(CementJob cementJob)
        {
            if (cementJob.CementStage.DensDisplaceFluid.Uom != null)
            {
                var obj = _mapper.Map<CementJobDensDisplaceFluid>(cementJob.CementStage.DensDisplaceFluid);
                _db.CementJobDensDisplaceFluids.Update(obj);
            }
        }
        private void UpdateVolDisplaceFluid(CementJob cementJob)
        {
            if (cementJob.CementStage.VolDisplaceFluid.Uom != null)
            {
                var obj = _mapper.Map<CementJobVolDisplaceFluid>(cementJob.CementStage.VolDisplaceFluid);
                _db.CementJobVolDisplaceFluids.Update(obj);
            }
        }
        private void UpdateCementStage(CementJob cementJob)
        {
            if (cementJob.CementStage.Uid != null)
            {
                var obj = _mapper.Map<CementJobCementStage>(cementJob.CementStage);
                _db.CementJobCementStages.Update(obj);
            }
        }
        private void UpdatePresTest(CementJob cementJob)
        {
            if (cementJob.CementTest.PresTest.Uom != null)
            {
                var obj = _mapper.Map<CementJobPresTest>(cementJob.CementTest.PresTest);
                _db.CementJobPresTests.Update(obj);
            }
        }
        private void UpdateETimTest(CementJob cementJob)
        {
            if (cementJob.CementTest.ETimTest.Uom != null)
            {
                var obj = _mapper.Map<CementJobETimTest>(cementJob.CementTest.ETimTest);
                _db.CementJobETimTests.Update(obj);
            }
        }
        private void UpdateCblPres(CementJob cementJob)
        {
            if (cementJob.CementTest.CblPres.Uom != null)
            {
                var obj = _mapper.Map<CementJobCblPres>(cementJob.CementTest.CblPres);
                _db.CementJobCblPress.Update(obj);
            }
        }
        private void UpdateETimCementLog(CementJob cementJob)
        {
            if (cementJob.CementTest.ETimCementLog.Uom != null)
            {
                var obj = _mapper.Map<CementJobETimCementLog>(cementJob.CementTest.ETimCementLog);
                _db.CementJobETimCementLogs.Update(obj);
            }
        }
        private void UpdateFormPit(CementJob cementJob)
        {
            if (cementJob.CementTest.FormPit.Uom != null)
            {
                var obj = _mapper.Map<CementJobFormPit>(cementJob.CementTest.FormPit);
                _db.CementJobFormPits.Update(obj);
            }
        }
        private void UpdateETimPitStart(CementJob cementJob)
        {
            if (cementJob.CementTest.ETimPitStart.Uom != null)
            {
                var obj = _mapper.Map<CementJobETimPitStart>(cementJob.CementTest.ETimPitStart);
                _db.CementJobETimPitStarts.Update(obj);
            }
        }
        private void UpdateMdCementTop(CementJob cementJob)
        {
            if (cementJob.CementTest.MdCementTop.Uom != null)
            {
                var obj = _mapper.Map<CementJobMdCementTop>(cementJob.CementTest.MdCementTop);
                _db.CementJobMdCementTops.Update(obj);
            }
        }
        private void UpdateLinerTop(CementJob cementJob)
        {
            if (cementJob.CementTest.LinerTop.Uom != null)
            {
                var obj = _mapper.Map<CementJobLinerTop>(cementJob.CementTest.LinerTop);
                _db.CementJobLinerTops.Update(obj);
            }
        }
        private void UpdateLinerLap(CementJob cementJob)
        {
            if (cementJob.CementTest.LinerLap.Uom != null)
            {
                var obj = _mapper.Map<CementJobLinerLap>(cementJob.CementTest.LinerLap);
                _db.CementJobLinerLaps.Update(obj);
            }
        }
        private void UpdateETimBeforeTest(CementJob cementJob)
        {
            if (cementJob.CementTest.ETimBeforeTest.Uom != null)
            {
                var obj = _mapper.Map<CementJobETimBeforeTest>(cementJob.CementTest.ETimBeforeTest);
                _db.CementJobETimBeforeTests.Update(obj);
            }
        }
        private void UpdateTestNegativeEmw(CementJob cementJob)
        {
            if (cementJob.CementTest.TestNegativeEmw.Uom != null)
            {
                var obj = _mapper.Map<CementJobTestNegativeEmw>(cementJob.CementTest.TestNegativeEmw);
                _db.CementJobTestNegativeEmws.Update(obj);
            }
        }

        private void UpdateTestPositiveEmw(CementJob cementJob)
        {
            if (cementJob.CementTest.TestPositiveEmw.Uom != null)
            {
                var obj = _mapper.Map<CementJobTestPositiveEmw>(cementJob.CementTest.TestPositiveEmw);
                _db.CementJobTestPositiveEmws.Update(obj);
            }
        }
        private void UpdateMdDVTool(CementJob cementJob)
        {
            if (cementJob.CementTest.MdDVTool.Uom != null)
            {
                var obj = _mapper.Map<CementJobMdDVTool>(cementJob.CementTest.MdDVTool);
                _db.CementJobMdDVTools.Update(obj);
            }
        }
        private void UpdateCementTest(CementJob cementJob)
        {
            if (cementJob.CementTest.PresTest != null)
            {
                var obj = _mapper.Map<CementJobCementTest>(cementJob.CementTest);
                _db.CementJobCementTests.Update(obj);
            }
        }
        private void UpdateMdSqueeze(CementJob cementJob)
        {
            if (cementJob.MdSqueeze.Uom != null)
            {
                var obj = _mapper.Map<CementJobMdSqueeze>(cementJob.MdSqueeze);
                _db.CementJobMdSqueezes.Update(obj);
            }
        }
        private void UpdateRpmPipe(CementJob cementJob)
        {
            if (cementJob.RpmPipe.Uom != null)
            {
                var obj = _mapper.Map<CementJobRpmPipe>(cementJob.RpmPipe);
                _db.CementJobRpmPipes.Update(obj);
            }
        }
        private void UpdateTqInitPipeRot(CementJob cementJob)
        {
            if (cementJob.TqInitPipeRot.Uom != null)
            {
                var obj = _mapper.Map<CementJobTqInitPipeRot>(cementJob.TqInitPipeRot);
                _db.CementJobTqInitPipeRots.Update(obj);
            }
        }
        private void UpdateTqPipeAv(CementJob cementJob)
        {
            if (cementJob.TqPipeAv.Uom != null)
            {
                var obj = _mapper.Map<CementJobTqPipeAv>(cementJob.TqPipeAv);
                _db.CementJobTqPipeAvs.Update(obj);
            }
        }
        private void UpdateTqPipeMx(CementJob cementJob)
        {
            if (cementJob.TqPipeMx.Uom != null)
            {
                var obj = _mapper.Map<CementJobTqPipeMx>(cementJob.TqPipeMx);
                _db.CementJobTqPipeMxs.Update(obj);
            }
        }
        private void UpdateOverPull(CementJob cementJob)
        {
            if (cementJob.OverPull.Uom != null)
            {
                var obj = _mapper.Map<CementJobOverPull>(cementJob.OverPull);
                _db.CementJobOverPulls.Update(obj);
            }
        }
        private void UpdateSlackOff(CementJob cementJob)
        {
            if (cementJob.SlackOff.Uom != null)
            {
                var obj = _mapper.Map<CementJobSlackOff>(cementJob.SlackOff);
                _db.CementJobSlackOffs.Update(obj);
            }
        }
        private void UpdateRpmPipeRecip(CementJob cementJob)
        {
            if (cementJob.RpmPipeRecip.Uom != null)
            {
                var obj = _mapper.Map<CementJobRpmPipeRecip>(cementJob.RpmPipeRecip);
                _db.CementJobRpmPipeRecips.Update(obj);
            }
        }

        private void UpdateMdTop(CementJob cementJob)
        {
            if (cementJob.CementStage.MdTop.Uom != null)
            {
                var obj = _mapper.Map<CementJobMdTop>(cementJob.CementStage.MdTop);
                _db.CementJobMdTops.Update(obj);

            }
        }

        private void UpdateMdBottom(CementJob cementJob)
        {
            if (cementJob.CementStage.MdBottom.Uom != null)
            {
                var obj = _mapper.Map<CementJobMdBottom>(cementJob.CementStage.MdBottom);
                _db.CementJobMdBottoms.Update(obj);

            }
        }

        private void UpdateTempBHCT(CementJob cementJob)
        {
            if (cementJob.CementStage.TempBHCT.Uom != null)
            {
                var obj = _mapper.Map<CementJobTempBHCT>(cementJob.CementStage.TempBHCT);
                _db.CementJobTempBHCTs.Update(obj);

            }
        }
        private void UpdateTempBHST(CementJob cementJob)
        {
            if (cementJob.CementStage.TempBHST.Uom != null)
            {
                var obj = _mapper.Map<CementJobTempBHST>(cementJob.CementStage.TempBHST);
                _db.CementJobTempBHSTs.Update(obj);

            }
        }
        

        private void UpdateLenPipeRecipStroke(CementJob cementJob)
        {
            if (cementJob.LenPipeRecipStroke.Uom != null)
            {
                var obj = _mapper.Map<CementJobLenPipeRecipStroke>(cementJob.LenPipeRecipStroke);
                _db.CementJobLenPipeRecipStrokes.Update(obj);
            }
        }
        private void UpdateCementJobCommonData(CementJob cementJob)
        {

            var obj = _mapper.Map<CementJobCommonData>(cementJob.CommonData);
            _db.CementJobCommonDatas.Update(obj);

        }

        #endregion Update CementJob Method

    }
}
