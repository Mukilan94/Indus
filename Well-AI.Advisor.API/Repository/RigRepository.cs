
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
    public class RigRepository : IRigRepository
    {
        private readonly WellAIAdvisiorContext _db;
        private readonly IMapper _mapper;
        private ITenantRepository _tenantRepository;
        private readonly WellAI.Advisor.DLL.Data.WebAIAdvisorContext _wdb;
        public RigRepository(WellAIAdvisiorContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper, ITenantRepository tenantRepository, WellAI.Advisor.DLL.Data.WebAIAdvisorContext wdb)
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
        public bool RigExists(string uid)
        {
            bool value = _db.Rigs.Any(x => x.Uid.ToLower().Trim() == uid.ToLower().Trim());
            return value;
        }

        public bool CreateRig(Rig rig)
        {
            try
            {
                RatingDrillDepth(rig);
                RatingWaterDepth(rig);
                AirGap(rig);
                SizeConnectionBop(rig);
                PresBopRating(rig);
                SizeBopSys(rig);
                IdBoosterLine(rig);
                OdBoosterLine(rig);
                LenBoosterLine(rig);
                IdSurfLine(rig);
                OdSurfLine(rig);
                LenSurfLine(rig);
                IdChkLine(rig);
                OdChkLine(rig);
                LenChkLine(rig);
                IdKillLine(rig);
                OdKillLine(rig);
                LenKillLine(rig);
                IdPassThru(rig);
                PresWork(rig);
                DiaCloseMn(rig);
                DiaCloseMx(rig);
                BopComponent(rig);
                DiaDiverter(rig);
                PresWorkDiverter(rig);
                CapAccFluid(rig);
                PresAccPreCharge(rig);
                VolAccPreCharge(rig);
                PresAccOpRating(rig);
                PresChokeManifold(rig);
                Bop(rig);
                CapMx(rig);
                Pit(rig);
                OdRod(rig);
                IdLiner(rig);
                Eff(rig);
                LenStroke(rig);
                PresMx(rig);
                PowHydMx(rig);
                SpmMx(rig);
                Displacement(rig);
                PresDamp(rig);
                VolDamp(rig);
                PowMechMx(rig);
                Pump(rig);
                CapFlow(rig);
                SizeMeshMn(rig);
                Shaker(rig);
                Centrifuge(rig);
                Hydrocyclone(rig);
                Height(rig);
                Len(rig);
                Id(rig);
                AreaSeparatorFlow(rig);
                HtMudSeal(rig);
                IdInlet(rig);
                IdVentLine(rig);
                LenVentLine(rig);
                CapGasSep(rig);
                CapBlowdown(rig);
                PresRating(rig);
                TempRating(rig);
                Degasser(rig);
                IdStandpipe(rig);
                LenStandpipe(rig);
                IdHose(rig);
                LenHose(rig);
                IdSwivel(rig);
                LenSwivel(rig);
                IdKelly(rig);
                LenKelly(rig);
                IdDischargeLine(rig);
                LenDischargeLine(rig);
                OdReel(rig);
                OdCore(rig);
                WidReelWrap(rig);
                LenReel(rig);
                HtInjStk(rig);
                OdUmbilical(rig);
                LenUmbilical(rig);
                IdTopStk(rig);
                HtTopStk(rig);
                HtFlange(rig);
                SurfaceEquipment(rig);
                RatingDerrick(rig);
                HtDerrick(rig);
                RatingHkld(rig);
                CapWindDerrick(rig);
                WtBlock(rig);
                RatingBlock(rig);
                RatingHook(rig);
                SizeDrillLine(rig);
                PowerDrawWorks(rig);
                RatingDrawWorks(rig);
                RatingSwivel(rig);
                RatingTqRotSys(rig);
                RotSizeOpening(rig);
                RatingRotSystem(rig);
                CapBulkMud(rig);
                CapLiquidMud(rig);
                CapDrillWater(rig);
                CapPotableWater(rig);
                CapFuel(rig);
                CapBulkCement(rig);
                VarDeckLdMx(rig);
                VdlStorm(rig);
                MotionCompensationMn(rig);
                MotionCompensationMx(rig);
                StrokeMotionCompensation(rig);
                RiserAngleLimit(rig);
                HeaveMx(rig);
                RigCommonData(rig);
                _db.Rigs.Add(rig);
                return Save();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "RigRepository CreateRig", null);
                return Save();
            }
        }

        public bool DeleteRig(Rig rig)
        {
            _db.Rigs.Remove(rig);
            return Save();
        }

        public Rig GetRigDetail(string Uid)
        {
            return _db.Rigs.FirstOrDefault(x=>x.Uid==Uid);
            
        }

        public ICollection<Rig> GetRigDetails()
        {
            return _db.Rigs.OrderBy(x => x.NameWell).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateRig(Rig rig)
        {
            try
            {
                UpdateRatingDrillDepth(rig);
                UpdateRatingWaterDepth(rig);
                UpdateAirGap(rig);
                UpdateSizeConnectionBop(rig);
                UpdatePresBopRating(rig);
                UpdateSizeBopSys(rig);
                UpdateIdBoosterLine(rig);
                UpdateOdBoosterLine(rig);
                UpdateLenBoosterLine(rig);
                UpdateIdSurfLine(rig);
                UpdateOdSurfLine(rig);
                UpdateLenSurfLine(rig);
                UpdateIdChkLine(rig);
                UpdateOdChkLine(rig);
                UpdateLenChkLine(rig);
                UpdateIdKillLine(rig);
                UpdateOdKillLine(rig);
                UpdateLenKillLine(rig);
                UpdateIdPassThru(rig);
                UpdatePresWork(rig);
                UpdateDiaCloseMn(rig);
                UpdateDiaCloseMx(rig);
                UpdateBopComponent(rig);
                UpdateDiaDiverter(rig);
                UpdatePresWorkDiverter(rig);
                UpdateCapAccFluid(rig);
                UpdatePresAccPreCharge(rig);
                UpdateVolAccPreCharge(rig);
                UpdatePresAccOpRating(rig);
                UpdatePresChokeManifold(rig);
                UpdateBop(rig);
                UpdateCapMx(rig);
                UpdatePit(rig);
                UpdateOdRod(rig);
                UpdateIdLiner(rig);
                UpdateEff(rig);
                UpdateLenStroke(rig);
                UpdatePresMx(rig);
                UpdatePowHydMx(rig);
                UpdateSpmMx(rig);
                UpdateDisplacement(rig);
                UpdatePresDamp(rig);
                UpdateVolDamp(rig);
                UpdatePowMechMx(rig);
                UpdatePump(rig);
                UpdateCapFlow(rig);
                UpdateSizeMeshMn(rig);
                UpdateShaker(rig);
                UpdateCentrifuge(rig);
                UpdateHydrocyclone(rig);
                UpdateHeight(rig);
                UpdateLen(rig);
                UpdateId(rig);
                UpdateAreaSeparatorFlow(rig);
                UpdateHtMudSeal(rig);
                UpdateIdInlet(rig);
                UpdateIdVentLine(rig);
                UpdateLenVentLine(rig);
                UpdateCapGasSep(rig);
                UpdateCapBlowdown(rig);
                UpdatePresRating(rig);
                UpdateTempRating(rig);
                UpdateDegasser(rig);
                UpdateIdStandpipe(rig);
                UpdateLenStandpipe(rig);
                UpdateIdHose(rig);
                UpdateLenHose(rig);
                UpdateIdSwivel(rig);
                UpdateLenSwivel(rig);
                UpdateIdKelly(rig);
                UpdateLenKelly(rig);
                UpdateIdDischargeLine(rig);
                UpdateLenDischargeLine(rig);
                UpdateOdReel(rig);
                UpdateOdCore(rig);
                UpdateWidReelWrap(rig);
                UpdateLenReel(rig);
                UpdateHtInjStk(rig);
                UpdateOdUmbilical(rig);
                UpdateLenUmbilical(rig);
                UpdateIdTopStk(rig);
                UpdateHtTopStk(rig);
                UpdateHtFlange(rig);
                UpdateSurfaceEquipment(rig);
                UpdateRatingDerrick(rig);
                UpdateHtDerrick(rig);
                UpdateRatingHkld(rig);
                UpdateCapWindDerrick(rig);
                UpdateWtBlock(rig);
                UpdateRatingBlock(rig);
                UpdateRatingHook(rig);
                UpdateSizeDrillLine(rig);
                UpdatePowerDrawWorks(rig);
                UpdateRatingDrawWorks(rig);
                UpdateRatingSwivel(rig);
                UpdateRatingTqRotSys(rig);
                UpdateRotSizeOpening(rig);
                UpdateRatingRotSystem(rig);
                UpdateCapBulkMud(rig);
                UpdateCapLiquidMud(rig);
                UpdateCapDrillWater(rig);
                UpdateCapPotableWater(rig);
                UpdateCapFuel(rig);
                UpdateCapBulkCement(rig);
                UpdateVarDeckLdMx(rig);
                UpdateVdlStorm(rig);
                UpdateMotionCompensationMn(rig);
                UpdateMotionCompensationMx(rig);
                UpdateStrokeMotionCompensation(rig);
                UpdateRiserAngleLimit(rig);
                UpdateHeaveMx(rig);
                UpdateRigCommonData(rig);
                _db.Rigs.Update(rig);
                return Save();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "RigRepository UpdateRig", null);
                return Save();
            }
        }

        #region Insert Rig
        private void RatingDrillDepth(Rig rig)
        {
            if (rig.RatingDrillDepth.Uom != null)
            {
                var obj = _mapper.Map<RigRatingDrillDepth>(rig.RatingDrillDepth);
                _db.RigRatingDrillDepths.Add(obj);
            }
        }
        private void RatingWaterDepth(Rig rig)
        {
            if (rig.RatingWaterDepth.Uom != null)
            {
                var obj = _mapper.Map<RigRatingWaterDepth>(rig.RatingWaterDepth);
                _db.RigRatingWaterDepths.Add(obj);
            }
        }
        private void AirGap(Rig rig)
        {
            if (rig.AirGap.Uom != null)
            {
                var obj = _mapper.Map<RigAirGap>(rig.AirGap);
                _db.RigAirGaps.Add(obj);
            }
        }
        private void SizeConnectionBop(Rig rig)
        {
            if (rig.Bop.SizeConnectionBop.Uom != null)
            {
                var obj = _mapper.Map<RigSizeConnectionBop>(rig.Bop.SizeConnectionBop);
                _db.RigSizeConnectionBops.Add(obj);
            }
        }
        private void PresBopRating(Rig rig)
        {
            if (rig.Bop.PresBopRating.Uom != null)
            {
                var obj = _mapper.Map<RigPresBopRating>(rig.Bop.PresBopRating);
                _db.RigPresBopRatings.Add(obj);
            }
        }
        private void SizeBopSys(Rig rig)
        {
            if (rig.Bop.SizeBopSys.Uom != null)
            {
                var obj = _mapper.Map<RigSizeBopSys>(rig.Bop.SizeBopSys);
                _db.RigSizeBopSyss.Add(obj);
            }
        }
        private void IdBoosterLine(Rig rig)
        {
            if (rig.Bop.IdBoosterLine.Uom != null)
            {
                var obj = _mapper.Map<RigIdBoosterLine>(rig.Bop.IdBoosterLine);
                _db.RigIdBoosterLines.Add(obj);
            }
        }
        private void OdBoosterLine(Rig rig)
        {
            if (rig.Bop.OdBoosterLine.Uom != null)
            {
                var obj = _mapper.Map<RigOdBoosterLine>(rig.Bop.OdBoosterLine);
                _db.RigOdBoosterLines.Add(obj);
            }
        }
        private void LenBoosterLine(Rig rig)
        {
            if (rig.Bop.LenBoosterLine.Uom != null)
            {
                var obj = _mapper.Map<RigLenBoosterLine>(rig.Bop.LenBoosterLine);
                _db.RigLenBoosterLines.Add(obj);
            }
        }
        private void IdSurfLine(Rig rig)
        {
            if (rig.Bop.IdSurfLine.Uom != null)
            {
                var obj = _mapper.Map<RigIdSurfLine>(rig.Bop.IdSurfLine);
                _db.RigIdSurfLines.Add(obj);
            }
        }
        private void OdSurfLine(Rig rig)
        {
            if (rig.Bop.OdSurfLine.Uom != null)
            {
                var obj = _mapper.Map<RigOdSurfLine>(rig.Bop.OdSurfLine);
                _db.RigOdSurfLines.Add(obj);
            }
        }
        private void LenSurfLine(Rig rig)
        {
            if (rig.Bop.OdBoosterLine.Uom != null)
            {
                var obj = _mapper.Map<RigLenSurfLine>(rig.Bop.LenSurfLine);
                _db.RigLenSurfLines.Add(obj);
            }
        }
        private void IdChkLine(Rig rig)
        {
            if (rig.Bop.IdChkLine.Uom != null)
            {
                var obj = _mapper.Map<RigIdChkLine>(rig.Bop.IdChkLine);
                _db.RigIdChkLines.Add(obj);
            }
        }
        private void OdChkLine(Rig rig)
        {
            if (rig.Bop.OdChkLine.Uom != null)
            {
                var obj = _mapper.Map<RigOdChkLine>(rig.Bop.OdChkLine);
                _db.RigOdChkLines.Add(obj);
            }
        }
        private void LenChkLine(Rig rig)
        {
            if (rig.Bop.LenChkLine.Uom != null)
            {
                var obj = _mapper.Map<RigLenChkLine>(rig.Bop.LenChkLine);
                _db.RigLenChkLines.Add(obj);
            }
        }
        private void IdKillLine(Rig rig)
        {
            if (rig.Bop.IdKillLine.Uom != null)
            {
                var obj = _mapper.Map<RigIdKillLine>(rig.Bop.IdKillLine);
                _db.RigIdKillLines.Add(obj);
            }
        }
        private void OdKillLine(Rig rig)
        {
            if (rig.Bop.OdKillLine.Uom != null)
            {
                var obj = _mapper.Map<RigOdKillLine>(rig.Bop.OdKillLine);
                _db.RigOdKillLines.Add(obj);
            }
        }
        private void LenKillLine(Rig rig)
        {
            if (rig.Bop.LenKillLine.Uom != null)
            {
                var obj = _mapper.Map<RigLenKillLine>(rig.Bop.LenKillLine);
                _db.RigLenKillLines.Add(obj);
            }
        }

        private void IdPassThru(Rig rig)
        {
            foreach (var item in rig.Bop.BopComponent)
            {

                if (item.IdPassThru.Uom != null)
                {
                    var obj = _mapper.Map<RigIdPassThru>(item.IdPassThru);
                    _db.RigIdPassThrus.Add(obj);
                }
            }
        }

        private void PresWork(Rig rig)
        {
            foreach (var item in rig.Bop.BopComponent)
            {
                if (item.PresWork.Uom != null)
                {
                    var obj = _mapper.Map<RigPresWork>(item.PresWork);
                    _db.RigPresWorks.Add(obj);
                }
            }
        }

        private void DiaCloseMn(Rig rig)
        {
            foreach (var item in rig.Bop.BopComponent)
            {
                if (item.DiaCloseMn.Uom != null)
                {
                    var obj = _mapper.Map<RigDiaCloseMn>(item.DiaCloseMn);
                    _db.RigDiaCloseMns.Add(obj);
                }
            }
        }
        private void DiaCloseMx(Rig rig)
        {
            foreach (var item in rig.Bop.BopComponent)
            {
                if (item.DiaCloseMx.Uom != null)
                {
                    var obj = _mapper.Map<RigDiaCloseMx>(item.DiaCloseMx);
                    _db.RigDiaCloseMxs.Add(obj);
                }
            }
        }
        private void BopComponent(Rig rig)
        {
            foreach (var item in rig.Bop.BopComponent)
            {
                if (item.Uid != null)
                {
                    var obj = _mapper.Map<RigBopComponent>(item);
                    _db.RigBopComponents.Add(obj);
                }
            }
        }
        private void DiaDiverter(Rig rig)
        {
                if (rig.Bop.DiaDiverter.Uom != null)
                {
                    var obj = _mapper.Map<RigDiaDiverter>(rig.Bop.DiaDiverter);
                    _db.RigDiaDiverters.Add(obj);
                }
        }
        private void PresWorkDiverter(Rig rig)
        {
            if (rig.Bop.PresWorkDiverter.Uom != null)
            {
                var obj = _mapper.Map<RigPresWorkDiverter>(rig.Bop.PresWorkDiverter);
                _db.RigPresWorkDiverters.Add(obj);
            }
        }
        private void CapAccFluid(Rig rig)
        {
            if (rig.Bop.CapAccFluid.Uom != null)
            {
                var obj = _mapper.Map<RigCapAccFluid>(rig.Bop.CapAccFluid);
                _db.RigCapAccFluids.Add(obj);
            }
        }
        private void PresAccPreCharge(Rig rig)
        {
            if (rig.Bop.PresAccPreCharge.Uom != null)
            {
                var obj = _mapper.Map<RigPresAccPreCharge>(rig.Bop.PresAccPreCharge);
                _db.RigPresAccPreCharges.Add(obj);
            }
        }
        private void VolAccPreCharge(Rig rig)
        {
            if (rig.Bop.VolAccPreCharge.Uom != null)
            {
                var obj = _mapper.Map<RigVolAccPreCharge>(rig.Bop.VolAccPreCharge);
                _db.RigVolAccPreCharges.Add(obj);
            }
        }
        private void PresAccOpRating(Rig rig)
        {
            if (rig.Bop.PresAccOpRating.Uom != null)
            {
                var obj = _mapper.Map<RigPresAccOpRating>(rig.Bop.PresAccOpRating);
                _db.RigPresAccOpRatings.Add(obj);
            }
        }
        private void PresChokeManifold(Rig rig)
        {
            if (rig.Bop.PresChokeManifold.Uom != null)
            {
                var obj = _mapper.Map<RigPresChokeManifold>(rig.Bop.PresChokeManifold);
                _db.RigPresChokeManifolds.Add(obj);
            }
        }

        private void Bop(Rig rig)
        {
            if (rig.Bop != null)
            {
                var obj = _mapper.Map<RigBop>(rig.Bop);
                _db.RigBops.Add(obj);
            }
        }
        private void CapMx(Rig rig)
        {
            if (rig.Pit.CapMx.Uom != null)
            {
                var obj = _mapper.Map<RigCapMx>(rig.Pit.CapMx);
                _db.RigCapMxs.Add(obj);
            }
        }
        private void Pit(Rig rig)
        {
            if (rig.Pit.Uid != null)
            {
                var obj = _mapper.Map<RigPit>(rig.Pit);
                _db.RigPits.Add(obj);
            }
        }
        private void OdRod(Rig rig)
        {
            if (rig.Pump.OdRod.Uom != null)
            {
                var obj = _mapper.Map<RigOdRod>(rig.Pump.OdRod);
                _db.RigOdRods.Add(obj);
            }
        }
        private void IdLiner(Rig rig)
        {
            if (rig.Pump.IdLiner.Uom != null)
            {
                var obj = _mapper.Map<RigIdLiner>(rig.Pump.IdLiner);
                _db.RigIdLiners.Add(obj);
            }
        }
        private void Eff(Rig rig)
        {
            if (rig.Pump.Eff.Uom != null)
            {
                var obj = _mapper.Map<RigEff>(rig.Pump.Eff);
                _db.RigEffs.Add(obj);
            }
        }
        private void LenStroke(Rig rig)
        {
            if (rig.Pump.LenStroke.Uom != null)
            {
                var obj = _mapper.Map<RigLenStroke>(rig.Pump.LenStroke);
                _db.RigLenStrokes.Add(obj);
            }
        }
        private void PresMx(Rig rig)
        {
            if (rig.Pump.PresMx.Uom != null)
            {
                var obj = _mapper.Map<RigPresMx>(rig.Pump.PresMx);
                _db.RigPresMxs.Add(obj);
            }
        }
        private void PowHydMx(Rig rig)
        {
            if (rig.Pump.PowHydMx.Uom != null)
            {
                var obj = _mapper.Map<RigPowHydMx>(rig.Pump.PowHydMx);
                _db.RigPowHydMxs.Add(obj);
            }
        }
        private void SpmMx(Rig rig)
        {
            if (rig.Pump.SpmMx.Uom != null)
            {
                var obj = _mapper.Map<RigSpmMx>(rig.Pump.SpmMx);
                _db.RigSpmMxs.Add(obj);
            }
        }
        private void Displacement(Rig rig)
        {
            if (rig.Pump.Displacement.Uom != null)
            {
                var obj = _mapper.Map<RigDisplacement>(rig.Pump.Displacement);
                _db.RigDisplacements.Add(obj);
            }
        }
        private void PresDamp(Rig rig)
        {
            if (rig.Pump.PresDamp.Uom != null)
            {
                var obj = _mapper.Map<RigPresDamp>(rig.Pump.PresDamp);
                _db.RigPresDamps.Add(obj);
            }
        }
        private void VolDamp(Rig rig)
        {
            if (rig.Pump.VolDamp.Uom != null)
            {
                var obj = _mapper.Map<RigVolDamp>(rig.Pump.VolDamp);
                _db.RigVolDamps.Add(obj);
            }
        }
        private void PowMechMx(Rig rig)
        {
            if (rig.Pump.PowMechMx.Uom != null)
            {
                var obj = _mapper.Map<RigPowMechMx>(rig.Pump.PowMechMx);
                _db.RigPowMechMxs.Add(obj);
            }
        }
        private void Pump(Rig rig)
        {
            if (rig.Pump.Uid != null)
            {
                var obj = _mapper.Map<RigPump>(rig.Pump);
                _db.RigPumps.Add(obj);
            }
        }
        private void CapFlow(Rig rig)
        {
            if (rig.Shaker.CapFlow.Uom != null)
            {
                var obj = _mapper.Map<RigCapFlow>(rig.Shaker.CapFlow);
                _db.RigCapFlows.Add(obj);
            }
            if (rig.Centrifuge.CapFlow.Uom != null)
            {
                var obj = _mapper.Map<RigCapFlow>(rig.Centrifuge.CapFlow);
                _db.RigCapFlows.Add(obj);
            }
        }
        private void SizeMeshMn(Rig rig)
        {
            if (rig.Shaker.SizeMeshMn.Uom != null)
            {
                var obj = _mapper.Map<RigSizeMeshMn>(rig.Shaker.SizeMeshMn);
                _db.RigSizeMeshMns.Add(obj);
            }
        }
        private void Shaker(Rig rig)
        {
            if (rig.Shaker.Uid != null)
            {
                var obj = _mapper.Map<RigShaker>(rig.Shaker);
                _db.RigShakers.Add(obj);
            }
        }
        private void Centrifuge(Rig rig)
        {
            if (rig.Centrifuge.Uid != null)
            {
                var obj = _mapper.Map<RigCentrifuge>(rig.Centrifuge);
                _db.RigCentrifuges.Add(obj);
            }
        }
        private void Hydrocyclone(Rig rig)
        {
            if (rig.Hydrocyclone.Uid != null)
            {
                var obj = _mapper.Map<RigHydrocyclone>(rig.Hydrocyclone);
                _db.RigHydrocyclones.Add(obj);
            }
        }
        private void Height(Rig rig)
        {
            if (rig.Degasser.Height.Uom != null)
            {
                var obj = _mapper.Map<RigHeight>(rig.Degasser.Height);
                _db.RigHeights.Add(obj);
            }
        }
        private void Len(Rig rig)
        {
            if (rig.Degasser.Len.Uom != null)
            {
                var obj = _mapper.Map<RigLen>(rig.Degasser.Len);
                _db.RigLens.Add(obj);
            }
        }
        private void Id(Rig rig)
        {
            if (rig.Degasser.Id.Uom != null)
            {
                var obj = _mapper.Map<RigId>(rig.Degasser.Id);
                _db.RigIds.Add(obj);
            }
        }
        private void AreaSeparatorFlow(Rig rig)
        {
            if (rig.Degasser.AreaSeparatorFlow.Uom != null)
            {
                var obj = _mapper.Map<RigAreaSeparatorFlow>(rig.Degasser.AreaSeparatorFlow);
                _db.RigAreaSeparatorFlows.Add(obj);
            }
        }
        private void HtMudSeal(Rig rig)
        {
            if (rig.Degasser.HtMudSeal.Uom != null)
            {
                var obj = _mapper.Map<RigHtMudSeal>(rig.Degasser.HtMudSeal);
                _db.RigHtMudSeals.Add(obj);
            }
        }
        private void IdInlet(Rig rig)
        {
            if (rig.Degasser.IdInlet.Uom != null)
            {
                var obj = _mapper.Map<RigIdInlet>(rig.Degasser.IdInlet);
                _db.RigIdInlets.Add(obj);
            }
        }
        private void IdVentLine(Rig rig)
        {
            if (rig.Degasser.IdVentLine.Uom != null)
            {
                var obj = _mapper.Map<RigIdVentLine>(rig.Degasser.IdVentLine);
                _db.RigIdVentLines.Add(obj);
            }
        }
        private void LenVentLine(Rig rig)
        {
            if (rig.Degasser.LenVentLine.Uom != null)
            {
                var obj = _mapper.Map<RigLenVentLine>(rig.Degasser.LenVentLine);
                _db.RigLenVentLines.Add(obj);
            }
        }
        private void CapGasSep(Rig rig)
        {
            if (rig.Degasser.CapGasSep.Uom != null)
            {
                var obj = _mapper.Map<RigCapGasSep>(rig.Degasser.CapGasSep);
                _db.RigCapGasSeps.Add(obj);
            }
        }
        private void CapBlowdown(Rig rig)
        {
            if (rig.Degasser.CapBlowdown.Uom != null)
            {
                var obj = _mapper.Map<RigCapBlowdown>(rig.Degasser.CapBlowdown);
                _db.RigCapBlowdowns.Add(obj);
            }
        }
        private void PresRating(Rig rig)
        {
            if (rig.Degasser.PresRating.Uom != null)
            {
                var obj = _mapper.Map<RigPresRating>(rig.Degasser.PresRating);
                _db.RigPresRatings.Add(obj);
            }
        }
        private void TempRating(Rig rig)
        {
            if (rig.Degasser.TempRating.Uom != null)
            {
                var obj = _mapper.Map<RigTempRating>(rig.Degasser.TempRating);
                _db.RigTempRatings.Add(obj);
            }
        }
        private void Degasser(Rig rig)
        {
            if (rig.Degasser.Uid != null)
            {
                var obj = _mapper.Map<RigDegasser>(rig.Degasser);
                _db.RigDegassers.Add(obj);
            }
        }
        private void IdStandpipe(Rig rig)
        {
            if (rig.SurfaceEquipment.IdStandpipe.Uom != null)
            {
                var obj = _mapper.Map<RigIdStandpipe>(rig.SurfaceEquipment.IdStandpipe);
                _db.RigIdStandpipes.Add(obj);
            }
        }
        private void IdHose(Rig rig)
        {
            if (rig.SurfaceEquipment.IdHose.Uom != null)
            {
                var obj = _mapper.Map<RigIdHose>(rig.SurfaceEquipment.IdHose);
                _db.RigIdHoses.Add(obj);
            }
        }
        private void LenHose(Rig rig)
        {
            if (rig.SurfaceEquipment.LenHose.Uom != null)
            {
                var obj = _mapper.Map<RigLenHose>(rig.SurfaceEquipment.LenHose);
                _db.RigLenHoses.Add(obj);
            }
        }
        private void IdSwivel(Rig rig)
        {
            if (rig.SurfaceEquipment.IdSwivel.Uom != null)
            {
                var obj = _mapper.Map<RigIdSwivel>(rig.SurfaceEquipment.IdSwivel);
                _db.RigIdSwivels.Add(obj);
            }
        }
        private void LenStandpipe(Rig rig)
        {
            if (rig.SurfaceEquipment.LenStandpipe.Uom != null)
            {
                var obj = _mapper.Map<RigLenStandpipe>(rig.SurfaceEquipment.LenStandpipe);
                _db.RigLenStandpipes.Add(obj);
            }
        }
        private void LenSwivel(Rig rig)
        {
            if (rig.SurfaceEquipment.LenSwivel.Uom != null)
            {
                var obj = _mapper.Map<RigLenSwivel>(rig.SurfaceEquipment.LenSwivel);
                _db.RigLenSwivels.Add(obj);
            }
        }
        private void IdKelly(Rig rig)
        {
            if (rig.SurfaceEquipment.IdKelly.Uom != null)
            {
                var obj = _mapper.Map<RigIdKelly>(rig.SurfaceEquipment.IdKelly);
                _db.RigIdKellys.Add(obj);
            }
        }
        private void LenKelly(Rig rig)
        {
            if (rig.SurfaceEquipment.LenKelly.Uom != null)
            {
                var obj = _mapper.Map<RigLenKelly>(rig.SurfaceEquipment.LenKelly);
                _db.RigLenKellys.Add(obj);
            }
        }
        private void IdDischargeLine(Rig rig)
        {
            if (rig.SurfaceEquipment.IdDischargeLine.Uom != null)
            {
                var obj = _mapper.Map<RigIdDischargeLine>(rig.SurfaceEquipment.IdDischargeLine);
                _db.RigIdDischargeLines.Add(obj);
            }

        }
        private void LenDischargeLine(Rig rig)
        {
            if (rig.SurfaceEquipment.LenDischargeLine.Uom != null)
            {
                var obj = _mapper.Map<RigLenDischargeLine>(rig.SurfaceEquipment.LenDischargeLine);
                _db.RigLenDischargeLines.Add(obj);
            }

        }
        private void OdReel(Rig rig)
        {
            if (rig.SurfaceEquipment.OdReel.Uom != null)
            {
                var obj = _mapper.Map<RigOdReel>(rig.SurfaceEquipment.OdReel);
                _db.RigOdReels.Add(obj);
            }

        }
        private void OdCore(Rig rig)
        {
            if (rig.SurfaceEquipment.OdCore.Uom != null)
            {
                var obj = _mapper.Map<RigOdCore>(rig.SurfaceEquipment.OdCore);
                _db.RigOdCores.Add(obj);
            }

        }
        private void WidReelWrap(Rig rig)
        {
            if (rig.SurfaceEquipment.WidReelWrap.Uom != null)
            {
                var obj = _mapper.Map<RigWidReelWrap>(rig.SurfaceEquipment.WidReelWrap);
                _db.RigWidReelWraps.Add(obj);
            }

        }
        private void LenReel(Rig rig)
        {
            if (rig.SurfaceEquipment.LenReel.Uom != null)
            {
                var obj = _mapper.Map<RigLenReel>(rig.SurfaceEquipment.LenReel);
                _db.RigLenReels.Add(obj);
            }

        }
        private void HtInjStk(Rig rig)
        {
            if (rig.SurfaceEquipment.HtInjStk.Uom != null)
            {
                var obj = _mapper.Map<RigHtInjStk>(rig.SurfaceEquipment.HtInjStk);
                _db.RigHtInjStks.Add(obj);
            }

        }
        private void OdUmbilical(Rig rig)
        {
            if (rig.SurfaceEquipment.OdUmbilical.Uom != null)
            {
                var obj = _mapper.Map<RigOdUmbilical>(rig.SurfaceEquipment.OdUmbilical);
                _db.RigOdUmbilicals.Add(obj);
            }

        }
        private void LenUmbilical(Rig rig)
        {
            if (rig.SurfaceEquipment.LenUmbilical.Uom != null)
            {
                var obj = _mapper.Map<RigLenUmbilical>(rig.SurfaceEquipment.LenUmbilical);
                _db.RigLenUmbilicals.Add(obj);
            }

        }
        private void IdTopStk(Rig rig)
        {
            if (rig.SurfaceEquipment.IdTopStk.Uom != null)
            {
                var obj = _mapper.Map<RigIdTopStk>(rig.SurfaceEquipment.IdTopStk);
                _db.RigIdTopStks.Add(obj);
            }

        }
        private void HtTopStk(Rig rig)
        {
            if (rig.SurfaceEquipment.HtTopStk.Uom != null)
            {
                var obj = _mapper.Map<RigHtTopStk>(rig.SurfaceEquipment.HtTopStk);
                _db.RigHtTopStks.Add(obj);
            }

        }
        private void HtFlange(Rig rig)
        {
            if (rig.SurfaceEquipment.HtFlange.Uom != null)
            {
                var obj = _mapper.Map<RigHtFlange>(rig.SurfaceEquipment.HtFlange);
                _db.RigHtFlanges.Add(obj);
            }

        }
        private void SurfaceEquipment(Rig rig)
        {
            if (rig.SurfaceEquipment != null)
            {
                var obj = _mapper.Map<RigSurfaceEquipment>(rig.SurfaceEquipment);
                _db.RigSurfaceEquipments.Add(obj);
            }

        }
        private void RatingDerrick(Rig rig)
        {
            if (rig.RatingDerrick.Uom != null)
            {
                var obj = _mapper.Map<RigRatingDerrick>(rig.RatingDerrick);
                _db.RigRatingDerricks.Add(obj);
            }

        }
        private void HtDerrick(Rig rig)
        {
            if (rig.HtDerrick.Uom != null)
            {
                var obj = _mapper.Map<RigHtDerrick>(rig.HtDerrick);
                _db.RigHtDerricks.Add(obj);
            }

        }
        private void RatingHkld(Rig rig)
        {
            if (rig.RatingHkld.Uom != null)
            {
                var obj = _mapper.Map<RigRatingHkld>(rig.RatingHkld);
                _db.RigRatingHklds.Add(obj);
            }

        }
        private void CapWindDerrick(Rig rig)
        {
            if (rig.CapWindDerrick.Uom != null)
            {
                var obj = _mapper.Map<RigCapWindDerrick>(rig.CapWindDerrick);
                _db.RigCapWindDerricks.Add(obj);
            }

        }
        private void WtBlock(Rig rig)
        {
            if (rig.WtBlock.Uom != null)
            {
                var obj = _mapper.Map<RigWtBlock>(rig.WtBlock);
                _db.RigWtBlocks.Add(obj);
            }

        }
        private void RatingBlock(Rig rig)
        {
            if (rig.RatingBlock.Uom != null)
            {
                var obj = _mapper.Map<RigRatingBlock>(rig.RatingBlock);
                _db.RigRatingBlocks.Add(obj);
            }

        }
        private void RatingHook(Rig rig)
        {
            if (rig.RatingHook.Uom != null)
            {
                var obj = _mapper.Map<RigRatingHook>(rig.RatingHook);
                _db.RigRatingHooks.Add(obj);
            }

        }
        private void SizeDrillLine(Rig rig)
        {
            if (rig.SizeDrillLine.Uom != null)
            {
                var obj = _mapper.Map<RigSizeDrillLine>(rig.SizeDrillLine);
                _db.RigSizeDrillLines.Add(obj);
            }

        }
        private void PowerDrawWorks(Rig rig)
        {
            if (rig.PowerDrawWorks.Uom != null)
            {
                var obj = _mapper.Map<RigPowerDrawWorks>(rig.PowerDrawWorks);
                _db.RigPowerDrawWork.Add(obj);
            }

        }
        private void RatingDrawWorks(Rig rig)
        {
            if (rig.RatingDrawWorks.Uom != null)
            {
                var obj = _mapper.Map<RigRatingDrawWorks>(rig.RatingDrawWorks);
                _db.RigRatingDrawWork.Add(obj);
            }

        }
        private void RatingSwivel(Rig rig)
        {
            if (rig.RatingSwivel.Uom != null)
            {
                var obj = _mapper.Map<RigRatingSwivel>(rig.RatingSwivel);
                _db.RigRatingSwivels.Add(obj);
            }

        }
        private void RatingTqRotSys(Rig rig)
        {
            if (rig.RatingTqRotSys.Uom != null)
            {
                var obj = _mapper.Map<RigRatingTqRotSys>(rig.RatingTqRotSys);
                _db.RigRatingTqRotSy.Add(obj);
            }

        }
        private void RotSizeOpening(Rig rig)
        {
            if (rig.RotSizeOpening.Uom != null)
            {
                var obj = _mapper.Map<RigRotSizeOpening>(rig.RotSizeOpening);
                _db.RigRotSizeOpenings.Add(obj);
            }

        }
        private void RatingRotSystem(Rig rig)
        {
            if (rig.RatingRotSystem.Uom != null)
            {
                var obj = _mapper.Map<RigRatingRotSystem>(rig.RatingRotSystem);
                _db.RigRatingRotSystems.Add(obj);
            }

        }
        private void CapBulkMud(Rig rig)
        {
            if (rig.CapBulkMud.Uom != null)
            {
                var obj = _mapper.Map<RigCapBulkMud>(rig.CapBulkMud);
                _db.RigCapBulkMuds.Add(obj);
            }

        }
        private void CapLiquidMud(Rig rig)
        {
            if (rig.CapLiquidMud.Uom != null)
            {
                var obj = _mapper.Map<RigCapLiquidMud>(rig.CapLiquidMud);
                _db.RigCapLiquidMuds.Add(obj);
            }

        }
        private void CapDrillWater(Rig rig)
        {
            if (rig.CapDrillWater.Uom != null)
            {
                var obj = _mapper.Map<RigCapDrillWater>(rig.CapDrillWater);
                _db.RigCapDrillWaters.Add(obj);
            }

        }
        private void CapPotableWater(Rig rig)
        {
            if (rig.CapPotableWater.Uom != null)
            {
                var obj = _mapper.Map<RigCapPotableWater>(rig.CapPotableWater);
                _db.RigCapPotableWaters.Add(obj);
            }

        }
        private void CapFuel(Rig rig)
        {
            if (rig.CapFuel.Uom != null)
            {
                var obj = _mapper.Map<RigCapFuel>(rig.CapFuel);
                _db.RigCapFuels.Add(obj);
            }

        }
        private void CapBulkCement(Rig rig)
        {
            if (rig.CapBulkCement.Uom != null)
            {
                var obj = _mapper.Map<RigCapBulkCement>(rig.CapBulkCement);
                _db.RigCapBulkCements.Add(obj);
            }

        }
        private void VarDeckLdMx(Rig rig)
        {
            if (rig.VarDeckLdMx.Uom != null)
            {
                var obj = _mapper.Map<RigVarDeckLdMx>(rig.VarDeckLdMx);
                _db.RigVarDeckLdMxs.Add(obj);
            }

        }
        private void VdlStorm(Rig rig)
        {
            if (rig.VdlStorm.Uom != null)
            {
                var obj = _mapper.Map<RigVdlStorm>(rig.VdlStorm);
                _db.RigVdlStorms.Add(obj);
            }

        }
        private void MotionCompensationMn(Rig rig)
        {
            if (rig.MotionCompensationMn.Uom != null)
            {
                var obj = _mapper.Map<RigMotionCompensationMn>(rig.MotionCompensationMn);
                _db.RigMotionCompensationMns.Add(obj);
            }

        }
        private void MotionCompensationMx(Rig rig)
        {
            if (rig.MotionCompensationMx.Uom != null)
            {
                var obj = _mapper.Map<RigMotionCompensationMx>(rig.MotionCompensationMx);
                _db.RigMotionCompensationMxs.Add(obj);
            }

        }
        private void StrokeMotionCompensation(Rig rig)
        {
            if (rig.StrokeMotionCompensation.Uom != null)
            {
                var obj = _mapper.Map<RigStrokeMotionCompensation>(rig.StrokeMotionCompensation);
                _db.RigStrokeMotionCompensations.Add(obj);
            }

        }
        private void RiserAngleLimit(Rig rig)
        {
            if (rig.RiserAngleLimit.Uom != null)
            {
                var obj = _mapper.Map<RigRiserAngleLimit>(rig.RiserAngleLimit);
                _db.RigRiserAngleLimits.Add(obj);
            }

        }
        private void HeaveMx(Rig rig)
        {
            if (rig.HeaveMx.Uom != null)
            {
                var obj = _mapper.Map<RigHeaveMx>(rig.HeaveMx);
                _db.RigHeaveMxs.Add(obj);
            }

        }
        private void RigCommonData(Rig rig)
        {
            if (rig.CommonData != null)
            {
                var obj = _mapper.Map<RigCommonData>(rig.CommonData);
                _db.RigCommonDatas.Add(obj);
            }

        }
        #endregion Update Rig

        #region Update Rig
        private void UpdateRatingDrillDepth(Rig rig)
        {
            if (rig.RatingDrillDepth.Uom != null)
            {
                var obj = _mapper.Map<RigRatingDrillDepth>(rig.RatingDrillDepth);
                _db.RigRatingDrillDepths.Update(obj);
            }
        }
        private void UpdateRatingWaterDepth(Rig rig)
        {
            if (rig.RatingWaterDepth.Uom != null)
            {
                var obj = _mapper.Map<RigRatingWaterDepth>(rig.RatingWaterDepth);
                _db.RigRatingWaterDepths.Update(obj);
            }
        }
        private void UpdateAirGap(Rig rig)
        {
            if (rig.AirGap.Uom != null)
            {
                var obj = _mapper.Map<RigAirGap>(rig.AirGap);
                _db.RigAirGaps.Update(obj);
            }
        }
        private void UpdateSizeConnectionBop(Rig rig)
        {
            if (rig.Bop.SizeConnectionBop.Uom != null)
            {
                var obj = _mapper.Map<RigSizeConnectionBop>(rig.Bop.SizeConnectionBop);
                _db.RigSizeConnectionBops.Update(obj);
            }
        }
        private void UpdatePresBopRating(Rig rig)
        {
            if (rig.Bop.PresBopRating.Uom != null)
            {
                var obj = _mapper.Map<RigPresBopRating>(rig.Bop.PresBopRating);
                _db.RigPresBopRatings.Update(obj);
            }
        }
        private void UpdateSizeBopSys(Rig rig)
        {
            if (rig.Bop.SizeBopSys.Uom != null)
            {
                var obj = _mapper.Map<RigSizeBopSys>(rig.Bop.SizeBopSys);
                _db.RigSizeBopSyss.Update(obj);
            }
        }
        private void UpdateIdBoosterLine(Rig rig)
        {
            if (rig.Bop.IdBoosterLine.Uom != null)
            {
                var obj = _mapper.Map<RigIdBoosterLine>(rig.Bop.IdBoosterLine);
                _db.RigIdBoosterLines.Update(obj);
            }
        }
        private void UpdateOdBoosterLine(Rig rig)
        {
            if (rig.Bop.OdBoosterLine.Uom != null)
            {
                var obj = _mapper.Map<RigOdBoosterLine>(rig.Bop.OdBoosterLine);
                _db.RigOdBoosterLines.Update(obj);
            }
        }
        private void UpdateLenBoosterLine(Rig rig)
        {
            if (rig.Bop.LenBoosterLine.Uom != null)
            {
                var obj = _mapper.Map<RigLenBoosterLine>(rig.Bop.LenBoosterLine);
                _db.RigLenBoosterLines.Update(obj);
            }
        }
        private void UpdateIdSurfLine(Rig rig)
        {
            if (rig.Bop.IdSurfLine.Uom != null)
            {
                var obj = _mapper.Map<RigIdSurfLine>(rig.Bop.IdSurfLine);
                _db.RigIdSurfLines.Update(obj);
            }
        }
        private void UpdateOdSurfLine(Rig rig)
        {
            if (rig.Bop.OdSurfLine.Uom != null)
            {
                var obj = _mapper.Map<RigOdSurfLine>(rig.Bop.OdSurfLine);
                _db.RigOdSurfLines.Update(obj);
            }
        }
        private void UpdateLenSurfLine(Rig rig)
        {
            if (rig.Bop.OdBoosterLine.Uom != null)
            {
                var obj = _mapper.Map<RigLenSurfLine>(rig.Bop.LenSurfLine);
                _db.RigLenSurfLines.Update(obj);
            }
        }
        private void UpdateIdChkLine(Rig rig)
        {
            if (rig.Bop.IdChkLine.Uom != null)
            {
                var obj = _mapper.Map<RigIdChkLine>(rig.Bop.IdChkLine);
                _db.RigIdChkLines.Update(obj);
            }
        }
        private void UpdateOdChkLine(Rig rig)
        {
            if (rig.Bop.OdChkLine.Uom != null)
            {
                var obj = _mapper.Map<RigOdChkLine>(rig.Bop.OdChkLine);
                _db.RigOdChkLines.Update(obj);
            }
        }
        private void UpdateLenChkLine(Rig rig)
        {
            if (rig.Bop.LenChkLine.Uom != null)
            {
                var obj = _mapper.Map<RigLenChkLine>(rig.Bop.LenChkLine);
                _db.RigLenChkLines.Update(obj);
            }
        }
        private void UpdateIdKillLine(Rig rig)
        {
            if (rig.Bop.IdKillLine.Uom != null)
            {
                var obj = _mapper.Map<RigIdKillLine>(rig.Bop.IdKillLine);
                _db.RigIdKillLines.Update(obj);
            }
        }
        private void UpdateOdKillLine(Rig rig)
        {
            if (rig.Bop.OdKillLine.Uom != null)
            {
                var obj = _mapper.Map<RigOdKillLine>(rig.Bop.OdKillLine);
                _db.RigOdKillLines.Update(obj);
            }
        }
        private void UpdateLenKillLine(Rig rig)
        {
            if (rig.Bop.LenKillLine.Uom != null)
            {
                var obj = _mapper.Map<RigLenKillLine>(rig.Bop.LenKillLine);
                _db.RigLenKillLines.Update(obj);
            }
        }

        private void UpdateIdPassThru(Rig rig)
        {
            foreach (var item in rig.Bop.BopComponent)
            {

                if (item.IdPassThru.Uom != null)
                {
                    var obj = _mapper.Map<RigIdPassThru>(item.IdPassThru);
                    _db.RigIdPassThrus.Update(obj);
                }
            }
        }

        private void UpdatePresWork(Rig rig)
        {
            foreach (var item in rig.Bop.BopComponent)
            {
                if (item.PresWork.Uom != null)
                {
                    var obj = _mapper.Map<RigPresWork>(item.PresWork);
                    _db.RigPresWorks.Update(obj);
                }
            }
        }

        private void UpdateDiaCloseMn(Rig rig)
        {
            foreach (var item in rig.Bop.BopComponent)
            {
                if (item.DiaCloseMn.Uom != null)
                {
                    var obj = _mapper.Map<RigDiaCloseMn>(item.DiaCloseMn);
                    _db.RigDiaCloseMns.Update(obj);
                }
            }
        }
        private void UpdateDiaCloseMx(Rig rig)
        {
            foreach (var item in rig.Bop.BopComponent)
            {
                if (item.DiaCloseMx.Uom != null)
                {
                    var obj = _mapper.Map<RigDiaCloseMx>(item.DiaCloseMx);
                    _db.RigDiaCloseMxs.Update(obj);
                }
            }
        }
        private void UpdateBopComponent(Rig rig)
        {
            foreach (var item in rig.Bop.BopComponent)
            {
                if (item.Uid != null)
                {
                    var obj = _mapper.Map<RigBopComponent>(item);
                    _db.RigBopComponents.Update(obj);
                }
            }
        }
        private void UpdateDiaDiverter(Rig rig)
        {
            if (rig.Bop.DiaDiverter.Uom != null)
            {
                var obj = _mapper.Map<RigDiaDiverter>(rig.Bop.DiaDiverter);
                _db.RigDiaDiverters.Update(obj);
            }
        }
        private void UpdatePresWorkDiverter(Rig rig)
        {
            if (rig.Bop.PresWorkDiverter.Uom != null)
            {
                var obj = _mapper.Map<RigPresWorkDiverter>(rig.Bop.PresWorkDiverter);
                _db.RigPresWorkDiverters.Update(obj);
            }
        }
        private void UpdateCapAccFluid(Rig rig)
        {
            if (rig.Bop.CapAccFluid.Uom != null)
            {
                var obj = _mapper.Map<RigCapAccFluid>(rig.Bop.CapAccFluid);
                _db.RigCapAccFluids.Update(obj);
            }
        }
        private void UpdatePresAccPreCharge(Rig rig)
        {
            if (rig.Bop.PresAccPreCharge.Uom != null)
            {
                var obj = _mapper.Map<RigPresAccPreCharge>(rig.Bop.PresAccPreCharge);
                _db.RigPresAccPreCharges.Update(obj);
            }
        }
        private void UpdateVolAccPreCharge(Rig rig)
        {
            if (rig.Bop.VolAccPreCharge.Uom != null)
            {
                var obj = _mapper.Map<RigVolAccPreCharge>(rig.Bop.VolAccPreCharge);
                _db.RigVolAccPreCharges.Update(obj);
            }
        }
        private void UpdatePresAccOpRating(Rig rig)
        {
            if (rig.Bop.PresAccOpRating.Uom != null)
            {
                var obj = _mapper.Map<RigPresAccOpRating>(rig.Bop.PresAccOpRating);
                _db.RigPresAccOpRatings.Update(obj);
            }
        }
        private void UpdatePresChokeManifold(Rig rig)
        {
            if (rig.Bop.PresChokeManifold.Uom != null)
            {
                var obj = _mapper.Map<RigPresChokeManifold>(rig.Bop.PresChokeManifold);
                _db.RigPresChokeManifolds.Update(obj);
            }
        }

        private void UpdateBop(Rig rig)
        {
            if (rig.Bop != null)
            {
                var obj = _mapper.Map<RigBop>(rig.Bop);
                _db.RigBops.Update(obj);
            }
        }
        private void UpdateCapMx(Rig rig)
        {
            if (rig.Pit.CapMx.Uom != null)
            {
                var obj = _mapper.Map<RigCapMx>(rig.Pit.CapMx);
                _db.RigCapMxs.Update(obj);
            }
        }
        private void UpdatePit(Rig rig)
        {
            if (rig.Pit.Uid != null)
            {
                var obj = _mapper.Map<RigPit>(rig.Pit);
                _db.RigPits.Update(obj);
            }
        }
        private void UpdateOdRod(Rig rig)
        {
            if (rig.Pump.OdRod.Uom != null)
            {
                var obj = _mapper.Map<RigOdRod>(rig.Pump.OdRod);
                _db.RigOdRods.Update(obj);
            }
        }
        private void UpdateIdLiner(Rig rig)
        {
            if (rig.Pump.IdLiner.Uom != null)
            {
                var obj = _mapper.Map<RigIdLiner>(rig.Pump.IdLiner);
                _db.RigIdLiners.Update(obj);
            }
        }
        private void UpdateEff(Rig rig)
        {
            if (rig.Pump.Eff.Uom != null)
            {
                var obj = _mapper.Map<RigEff>(rig.Pump.Eff);
                _db.RigEffs.Update(obj);
            }
        }
        private void UpdateLenStroke(Rig rig)
        {
            if (rig.Pump.LenStroke.Uom != null)
            {
                var obj = _mapper.Map<RigLenStroke>(rig.Pump.LenStroke);
                _db.RigLenStrokes.Update(obj);
            }
        }
        private void UpdatePresMx(Rig rig)
        {
            if (rig.Pump.PresMx.Uom != null)
            {
                var obj = _mapper.Map<RigPresMx>(rig.Pump.PresMx);
                _db.RigPresMxs.Update(obj);
            }
        }
        private void UpdatePowHydMx(Rig rig)
        {
            if (rig.Pump.PowHydMx.Uom != null)
            {
                var obj = _mapper.Map<RigPowHydMx>(rig.Pump.PowHydMx);
                _db.RigPowHydMxs.Update(obj);
            }
        }
        private void UpdateSpmMx(Rig rig)
        {
            if (rig.Pump.SpmMx.Uom != null)
            {
                var obj = _mapper.Map<RigSpmMx>(rig.Pump.SpmMx);
                _db.RigSpmMxs.Update(obj);
            }
        }
        private void UpdateDisplacement(Rig rig)
        {
            if (rig.Pump.Displacement.Uom != null)
            {
                var obj = _mapper.Map<RigDisplacement>(rig.Pump.Displacement);
                _db.RigDisplacements.Update(obj);
            }
        }
        private void UpdatePresDamp(Rig rig)
        {
            if (rig.Pump.PresDamp.Uom != null)
            {
                var obj = _mapper.Map<RigPresDamp>(rig.Pump.PresDamp);
                _db.RigPresDamps.Update(obj);
            }
        }
        private void UpdateVolDamp(Rig rig)
        {
            if (rig.Pump.VolDamp.Uom != null)
            {
                var obj = _mapper.Map<RigVolDamp>(rig.Pump.VolDamp);
                _db.RigVolDamps.Update(obj);
            }
        }
        private void UpdatePowMechMx(Rig rig)
        {
            if (rig.Pump.PowMechMx.Uom != null)
            {
                var obj = _mapper.Map<RigPowMechMx>(rig.Pump.PowMechMx);
                _db.RigPowMechMxs.Update(obj);
            }
        }
        private void UpdatePump(Rig rig)
        {
            if (rig.Pump.Uid != null)
            {
                var obj = _mapper.Map<RigPump>(rig.Pump);
                _db.RigPumps.Update(obj);
            }
        }
        private void UpdateCapFlow(Rig rig)
        {
            if (rig.Shaker.CapFlow.Uom != null)
            {
                var obj = _mapper.Map<RigCapFlow>(rig.Shaker.CapFlow);
                _db.RigCapFlows.Update(obj);
            }
            if (rig.Centrifuge.CapFlow.Uom != null)
            {
                var obj = _mapper.Map<RigCapFlow>(rig.Centrifuge.CapFlow);
                _db.RigCapFlows.Update(obj);
            }
        }
        private void UpdateSizeMeshMn(Rig rig)
        {
            if (rig.Shaker.SizeMeshMn.Uom != null)
            {
                var obj = _mapper.Map<RigSizeMeshMn>(rig.Shaker.SizeMeshMn);
                _db.RigSizeMeshMns.Update(obj);
            }
        }
        private void UpdateShaker(Rig rig)
        {
            if (rig.Shaker.Uid != null)
            {
                var obj = _mapper.Map<RigShaker>(rig.Shaker);
                _db.RigShakers.Update(obj);
            }
        }
        private void UpdateCentrifuge(Rig rig)
        {
            if (rig.Centrifuge.Uid != null)
            {
                var obj = _mapper.Map<RigCentrifuge>(rig.Centrifuge);
                _db.RigCentrifuges.Update(obj);
            }
        }
        private void UpdateHydrocyclone(Rig rig)
        {
            if (rig.Hydrocyclone.Uid != null)
            {
                var obj = _mapper.Map<RigHydrocyclone>(rig.Hydrocyclone);
                _db.RigHydrocyclones.Update(obj);
            }
        }
        private void UpdateHeight(Rig rig)
        {
            if (rig.Degasser.Height.Uom != null)
            {
                var obj = _mapper.Map<RigHeight>(rig.Degasser.Height);
                _db.RigHeights.Update(obj);
            }
        }
        private void UpdateLen(Rig rig)
        {
            if (rig.Degasser.Len.Uom != null)
            {
                var obj = _mapper.Map<RigLen>(rig.Degasser.Len);
                _db.RigLens.Update(obj);
            }
        }
        private void UpdateId(Rig rig)
        {
            if (rig.Degasser.Id.Uom != null)
            {
                var obj = _mapper.Map<RigId>(rig.Degasser.Id);
                _db.RigIds.Update(obj);
            }
        }
        private void UpdateAreaSeparatorFlow(Rig rig)
        {
            if (rig.Degasser.AreaSeparatorFlow.Uom != null)
            {
                var obj = _mapper.Map<RigAreaSeparatorFlow>(rig.Degasser.AreaSeparatorFlow);
                _db.RigAreaSeparatorFlows.Update(obj);
            }
        }
        private void UpdateHtMudSeal(Rig rig)
        {
            if (rig.Degasser.HtMudSeal.Uom != null)
            {
                var obj = _mapper.Map<RigHtMudSeal>(rig.Degasser.HtMudSeal);
                _db.RigHtMudSeals.Update(obj);
            }
        }
        private void UpdateIdInlet(Rig rig)
        {
            if (rig.Degasser.IdInlet.Uom != null)
            {
                var obj = _mapper.Map<RigIdInlet>(rig.Degasser.IdInlet);
                _db.RigIdInlets.Update(obj);
            }
        }
        private void UpdateIdVentLine(Rig rig)
        {
            if (rig.Degasser.IdVentLine.Uom != null)
            {
                var obj = _mapper.Map<RigIdVentLine>(rig.Degasser.IdVentLine);
                _db.RigIdVentLines.Update(obj);
            }
        }
        private void UpdateLenVentLine(Rig rig)
        {
            if (rig.Degasser.LenVentLine.Uom != null)
            {
                var obj = _mapper.Map<RigLenVentLine>(rig.Degasser.LenVentLine);
                _db.RigLenVentLines.Update(obj);
            }
        }
        private void UpdateCapGasSep(Rig rig)
        {
            if (rig.Degasser.CapGasSep.Uom != null)
            {
                var obj = _mapper.Map<RigCapGasSep>(rig.Degasser.CapGasSep);
                _db.RigCapGasSeps.Update(obj);
            }
        }
        private void UpdateCapBlowdown(Rig rig)
        {
            if (rig.Degasser.CapBlowdown.Uom != null)
            {
                var obj = _mapper.Map<RigCapBlowdown>(rig.Degasser.CapBlowdown);
                _db.RigCapBlowdowns.Update(obj);
            }
        }
        private void UpdatePresRating(Rig rig)
        {
            if (rig.Degasser.PresRating.Uom != null)
            {
                var obj = _mapper.Map<RigPresRating>(rig.Degasser.PresRating);
                _db.RigPresRatings.Update(obj);
            }
        }
        private void UpdateTempRating(Rig rig)
        {
            if (rig.Degasser.TempRating.Uom != null)
            {
                var obj = _mapper.Map<RigTempRating>(rig.Degasser.TempRating);
                _db.RigTempRatings.Update(obj);
            }
        }
        private void UpdateDegasser(Rig rig)
        {
            if (rig.Degasser.Uid != null)
            {
                var obj = _mapper.Map<RigDegasser>(rig.Degasser);
                _db.RigDegassers.Update(obj);
            }
        }
        private void UpdateIdStandpipe(Rig rig)
        {
            if (rig.SurfaceEquipment.IdStandpipe.Uom != null)
            {
                var obj = _mapper.Map<RigIdStandpipe>(rig.SurfaceEquipment.IdStandpipe);
                _db.RigIdStandpipes.Update(obj);
            }
        }
        private void UpdateIdHose(Rig rig)
        {
            if (rig.SurfaceEquipment.IdHose.Uom != null)
            {
                var obj = _mapper.Map<RigIdHose>(rig.SurfaceEquipment.IdHose);
                _db.RigIdHoses.Update(obj);
            }
        }
        private void UpdateLenHose(Rig rig)
        {
            if (rig.SurfaceEquipment.LenHose.Uom != null)
            {
                var obj = _mapper.Map<RigLenHose>(rig.SurfaceEquipment.LenHose);
                _db.RigLenHoses.Update(obj);
            }
        }
        private void UpdateIdSwivel(Rig rig)
        {
            if (rig.SurfaceEquipment.IdSwivel.Uom != null)
            {
                var obj = _mapper.Map<RigIdSwivel>(rig.SurfaceEquipment.IdSwivel);
                _db.RigIdSwivels.Update(obj);
            }
        }
        private void UpdateLenStandpipe(Rig rig)
        {
            if (rig.SurfaceEquipment.LenStandpipe.Uom != null)
            {
                var obj = _mapper.Map<RigLenStandpipe>(rig.SurfaceEquipment.LenStandpipe);
                _db.RigLenStandpipes.Update(obj);
            }
        }
        private void UpdateLenSwivel(Rig rig)
        {
            if (rig.SurfaceEquipment.LenSwivel.Uom != null)
            {
                var obj = _mapper.Map<RigLenSwivel>(rig.SurfaceEquipment.LenSwivel);
                _db.RigLenSwivels.Update(obj);
            }
        }
        private void UpdateIdKelly(Rig rig)
        {
            if (rig.SurfaceEquipment.IdKelly.Uom != null)
            {
                var obj = _mapper.Map<RigIdKelly>(rig.SurfaceEquipment.IdKelly);
                _db.RigIdKellys.Update(obj);
            }
        }
        private void UpdateLenKelly(Rig rig)
        {
            if (rig.SurfaceEquipment.LenKelly.Uom != null)
            {
                var obj = _mapper.Map<RigLenKelly>(rig.SurfaceEquipment.LenKelly);
                _db.RigLenKellys.Update(obj);
            }
        }
        private void UpdateIdDischargeLine(Rig rig)
        {
            if (rig.SurfaceEquipment.IdDischargeLine.Uom != null)
            {
                var obj = _mapper.Map<RigIdDischargeLine>(rig.SurfaceEquipment.IdDischargeLine);
                _db.RigIdDischargeLines.Update(obj);
            }

        }
        private void UpdateLenDischargeLine(Rig rig)
        {
            if (rig.SurfaceEquipment.LenDischargeLine.Uom != null)
            {
                var obj = _mapper.Map<RigLenDischargeLine>(rig.SurfaceEquipment.LenDischargeLine);
                _db.RigLenDischargeLines.Update(obj);
            }

        }
        private void UpdateOdReel(Rig rig)
        {
            if (rig.SurfaceEquipment.OdReel.Uom != null)
            {
                var obj = _mapper.Map<RigOdReel>(rig.SurfaceEquipment.OdReel);
                _db.RigOdReels.Update(obj);
            }

        }
        private void UpdateOdCore(Rig rig)
        {
            if (rig.SurfaceEquipment.OdCore.Uom != null)
            {
                var obj = _mapper.Map<RigOdCore>(rig.SurfaceEquipment.OdCore);
                _db.RigOdCores.Update(obj);
            }

        }
        private void UpdateWidReelWrap(Rig rig)
        {
            if (rig.SurfaceEquipment.WidReelWrap.Uom != null)
            {
                var obj = _mapper.Map<RigWidReelWrap>(rig.SurfaceEquipment.WidReelWrap);
                _db.RigWidReelWraps.Update(obj);
            }

        }
        private void UpdateLenReel(Rig rig)
        {
            if (rig.SurfaceEquipment.LenReel.Uom != null)
            {
                var obj = _mapper.Map<RigLenReel>(rig.SurfaceEquipment.LenReel);
                _db.RigLenReels.Update(obj);
            }

        }
        private void UpdateHtInjStk(Rig rig)
        {
            if (rig.SurfaceEquipment.HtInjStk.Uom != null)
            {
                var obj = _mapper.Map<RigHtInjStk>(rig.SurfaceEquipment.HtInjStk);
                _db.RigHtInjStks.Update(obj);
            }

        }
        private void UpdateOdUmbilical(Rig rig)
        {
            if (rig.SurfaceEquipment.OdUmbilical.Uom != null)
            {
                var obj = _mapper.Map<RigOdUmbilical>(rig.SurfaceEquipment.OdUmbilical);
                _db.RigOdUmbilicals.Update(obj);
            }

        }
        private void UpdateLenUmbilical(Rig rig)
        {
            if (rig.SurfaceEquipment.LenUmbilical.Uom != null)
            {
                var obj = _mapper.Map<RigLenUmbilical>(rig.SurfaceEquipment.LenUmbilical);
                _db.RigLenUmbilicals.Update(obj);
            }

        }
        private void UpdateIdTopStk(Rig rig)
        {
            if (rig.SurfaceEquipment.IdTopStk.Uom != null)
            {
                var obj = _mapper.Map<RigIdTopStk>(rig.SurfaceEquipment.IdTopStk);
                _db.RigIdTopStks.Update(obj);
            }

        }
        private void UpdateHtTopStk(Rig rig)
        {
            if (rig.SurfaceEquipment.HtTopStk.Uom != null)
            {
                var obj = _mapper.Map<RigHtTopStk>(rig.SurfaceEquipment.HtTopStk);
                _db.RigHtTopStks.Update(obj);
            }

        }
        private void UpdateHtFlange(Rig rig)
        {
            if (rig.SurfaceEquipment.HtFlange.Uom != null)
            {
                var obj = _mapper.Map<RigHtFlange>(rig.SurfaceEquipment.HtFlange);
                _db.RigHtFlanges.Update(obj);
            }

        }
        private void UpdateSurfaceEquipment(Rig rig)
        {
            if (rig.SurfaceEquipment != null)
            {
                var obj = _mapper.Map<RigSurfaceEquipment>(rig.SurfaceEquipment);
                _db.RigSurfaceEquipments.Update(obj);
            }

        }
        private void UpdateRatingDerrick(Rig rig)
        {
            if (rig.RatingDerrick.Uom != null)
            {
                var obj = _mapper.Map<RigRatingDerrick>(rig.RatingDerrick);
                _db.RigRatingDerricks.Update(obj);
            }

        }
        private void UpdateHtDerrick(Rig rig)
        {
            if (rig.HtDerrick.Uom != null)
            {
                var obj = _mapper.Map<RigHtDerrick>(rig.HtDerrick);
                _db.RigHtDerricks.Update(obj);
            }

        }
        private void UpdateRatingHkld(Rig rig)
        {
            if (rig.RatingHkld.Uom != null)
            {
                var obj = _mapper.Map<RigRatingHkld>(rig.RatingHkld);
                _db.RigRatingHklds.Update(obj);
            }

        }
        private void UpdateCapWindDerrick(Rig rig)
        {
            if (rig.CapWindDerrick.Uom != null)
            {
                var obj = _mapper.Map<RigCapWindDerrick>(rig.CapWindDerrick);
                _db.RigCapWindDerricks.Update(obj);
            }

        }
        private void UpdateWtBlock(Rig rig)
        {
            if (rig.WtBlock.Uom != null)
            {
                var obj = _mapper.Map<RigWtBlock>(rig.WtBlock);
                _db.RigWtBlocks.Update(obj);
            }

        }
        private void UpdateRatingBlock(Rig rig)
        {
            if (rig.RatingBlock.Uom != null)
            {
                var obj = _mapper.Map<RigRatingBlock>(rig.RatingBlock);
                _db.RigRatingBlocks.Update(obj);
            }

        }
        private void UpdateRatingHook(Rig rig)
        {
            if (rig.RatingHook.Uom != null)
            {
                var obj = _mapper.Map<RigRatingHook>(rig.RatingHook);
                _db.RigRatingHooks.Update(obj);
            }

        }
        private void UpdateSizeDrillLine(Rig rig)
        {
            if (rig.SizeDrillLine.Uom != null)
            {
                var obj = _mapper.Map<RigSizeDrillLine>(rig.SizeDrillLine);
                _db.RigSizeDrillLines.Update(obj);
            }

        }
        private void UpdatePowerDrawWorks(Rig rig)
        {
            if (rig.PowerDrawWorks.Uom != null)
            {
                var obj = _mapper.Map<RigPowerDrawWorks>(rig.PowerDrawWorks);
                _db.RigPowerDrawWork.Update(obj);
            }

        }
        private void UpdateRatingDrawWorks(Rig rig)
        {
            if (rig.RatingDrawWorks.Uom != null)
            {
                var obj = _mapper.Map<RigRatingDrawWorks>(rig.RatingDrawWorks);
                _db.RigRatingDrawWork.Update(obj);
            }

        }
        private void UpdateRatingSwivel(Rig rig)
        {
            if (rig.RatingSwivel.Uom != null)
            {
                var obj = _mapper.Map<RigRatingSwivel>(rig.RatingSwivel);
                _db.RigRatingSwivels.Update(obj);
            }

        }
        private void UpdateRatingTqRotSys(Rig rig)
        {
            if (rig.RatingTqRotSys.Uom != null)
            {
                var obj = _mapper.Map<RigRatingTqRotSys>(rig.RatingTqRotSys);
                _db.RigRatingTqRotSy.Update(obj);
            }

        }
        private void UpdateRotSizeOpening(Rig rig)
        {
            if (rig.RotSizeOpening.Uom != null)
            {
                var obj = _mapper.Map<RigRotSizeOpening>(rig.RotSizeOpening);
                _db.RigRotSizeOpenings.Update(obj);
            }

        }
        private void UpdateRatingRotSystem(Rig rig)
        {
            if (rig.RatingRotSystem.Uom != null)
            {
                var obj = _mapper.Map<RigRatingRotSystem>(rig.RatingRotSystem);
                _db.RigRatingRotSystems.Update(obj);
            }

        }
        private void UpdateCapBulkMud(Rig rig)
        {
            if (rig.CapBulkMud.Uom != null)
            {
                var obj = _mapper.Map<RigCapBulkMud>(rig.CapBulkMud);
                _db.RigCapBulkMuds.Update(obj);
            }

        }
        private void UpdateCapLiquidMud(Rig rig)
        {
            if (rig.CapLiquidMud.Uom != null)
            {
                var obj = _mapper.Map<RigCapLiquidMud>(rig.CapLiquidMud);
                _db.RigCapLiquidMuds.Update(obj);
            }

        }
        private void UpdateCapDrillWater(Rig rig)
        {
            if (rig.CapDrillWater.Uom != null)
            {
                var obj = _mapper.Map<RigCapDrillWater>(rig.CapDrillWater);
                _db.RigCapDrillWaters.Update(obj);
            }

        }
        private void UpdateCapPotableWater(Rig rig)
        {
            if (rig.CapPotableWater.Uom != null)
            {
                var obj = _mapper.Map<RigCapPotableWater>(rig.CapPotableWater);
                _db.RigCapPotableWaters.Update(obj);
            }

        }
        private void UpdateCapFuel(Rig rig)
        {
            if (rig.CapFuel.Uom != null)
            {
                var obj = _mapper.Map<RigCapFuel>(rig.CapFuel);
                _db.RigCapFuels.Update(obj);
            }

        }
        private void UpdateCapBulkCement(Rig rig)
        {
            if (rig.CapBulkCement.Uom != null)
            {
                var obj = _mapper.Map<RigCapBulkCement>(rig.CapBulkCement);
                _db.RigCapBulkCements.Update(obj);
            }

        }
        private void UpdateVarDeckLdMx(Rig rig)
        {
            if (rig.VarDeckLdMx.Uom != null)
            {
                var obj = _mapper.Map<RigVarDeckLdMx>(rig.VarDeckLdMx);
                _db.RigVarDeckLdMxs.Update(obj);
            }

        }
        private void UpdateVdlStorm(Rig rig)
        {
            if (rig.VdlStorm.Uom != null)
            {
                var obj = _mapper.Map<RigVdlStorm>(rig.VdlStorm);
                _db.RigVdlStorms.Update(obj);
            }

        }
        private void UpdateMotionCompensationMn(Rig rig)
        {
            if (rig.MotionCompensationMn.Uom != null)
            {
                var obj = _mapper.Map<RigMotionCompensationMn>(rig.MotionCompensationMn);
                _db.RigMotionCompensationMns.Update(obj);
            }

        }
        private void UpdateMotionCompensationMx(Rig rig)
        {
            if (rig.MotionCompensationMx.Uom != null)
            {
                var obj = _mapper.Map<RigMotionCompensationMx>(rig.MotionCompensationMx);
                _db.RigMotionCompensationMxs.Update(obj);
            }

        }
        private void UpdateStrokeMotionCompensation(Rig rig)
        {
            if (rig.StrokeMotionCompensation.Uom != null)
            {
                var obj = _mapper.Map<RigStrokeMotionCompensation>(rig.StrokeMotionCompensation);
                _db.RigStrokeMotionCompensations.Update(obj);
            }

        }
        private void UpdateRiserAngleLimit(Rig rig)
        {
            if (rig.RiserAngleLimit.Uom != null)
            {
                var obj = _mapper.Map<RigRiserAngleLimit>(rig.RiserAngleLimit);
                _db.RigRiserAngleLimits.Update(obj);
            }

        }
        private void UpdateHeaveMx(Rig rig)
        {
            if (rig.HeaveMx.Uom != null)
            {
                var obj = _mapper.Map<RigHeaveMx>(rig.HeaveMx);
                _db.RigHeaveMxs.Update(obj);
            }

        }
        private void UpdateRigCommonData(Rig rig)
        {
            if (rig.CommonData != null)
            {
                var obj = _mapper.Map<RigCommonData>(rig.CommonData);
                _db.RigCommonDatas.Update(obj);
            }

        }
        #endregion Update Rig
    }
}
