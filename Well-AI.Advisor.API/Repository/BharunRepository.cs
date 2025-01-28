using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Well_AI.Advisor.API.Controllers;
using Well_AI.Advisor.API.Data;
using Well_AI.Advisor.API.Dtos;
using Well_AI.Advisor.API.Models;
using Well_AI.Advisor.API.Repository.IRepository;
using WellAI.Advisor.API.Repository.IRepository;
using WellAI.Advisor.DLL.Data;

namespace Well_AI.Advisor.API.Repository
{
    public class BharunRepository : IBharunRepository
    {
        private readonly WellAIAdvisiorContext _db;
        private readonly WebAIAdvisorContext _wdb;
        private readonly IMapper _mapper;
        private ITenantRepository _tenantRepository;
        public BharunRepository(WellAIAdvisiorContext db, IHttpContextAccessor httpContextAccessor ,IMapper mapper, ITenantRepository tenantRepository, WebAIAdvisorContext wdb)
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
        public bool BharunExists(string uid)
        {
            bool value = _db.Bharuns.Any(x => x.Uid.ToLower().Trim() == uid.ToLower().Trim());
            return value;
        }

        public bool CreateBharun(Bharun bharun)
        {
            try
            {
                #region Insert method for other objects
                _db.Bharuns.Add(bharun);
                ActDogleg(bharun);
                ActDoglegMx(bharun);
                ETimOpBit(bharun);
                MdHoleStart(bharun);
                MdHoleStop(bharun);
                HkldRot(bharun);
                OverPull(bharun);
                SlackOff(bharun);
                HkldUp(bharun);
                HkldDn(bharun);
                TqOnBotAv(bharun);
                TqOnBotMx(bharun);
                TqOnBotMn(bharun);
                TqOffBotAv(bharun);
                TqDhAv(bharun);
                WtAboveJar(bharun);
                WtBelowJar(bharun);
                WtMud(bharun);
                FlowratePump(bharun);
                PowBit(bharun);
                VelNozzleAv(bharun);
                PresDropBit(bharun);
                CTimHold(bharun);
                CTimSteering(bharun);
                CTimDrillRot(bharun);
                CTimDrillSlid(bharun);
                CTimCirc(bharun);
                CTimReam(bharun);
                DistDrillRot(bharun);
                DistDrillSlid(bharun);
                DistReam(bharun);
                DistHold(bharun);
                DistSteering(bharun);
                RpmMx(bharun);
                RpmMn(bharun);
                RpmAvDh(bharun);
                WobMx(bharun);
                WobMn(bharun);
                WobAvDh(bharun);
                ETimOpBit(bharun);
                MdHoleStart(bharun);
                MdHoleStop(bharun);
                Tubular(bharun);
                HkldRot(bharun);
                OverPull(bharun);
                SlackOff(bharun);
                HkldUp(bharun);
                HkldDn(bharun);
                TqOnBotAv(bharun);
                TqOnBotMx(bharun);
                TqOnBotMn(bharun);
                TqOffBotAv(bharun);
                TqDhAv(bharun);
                WtAboveJar(bharun);
                WtBelowJar(bharun);
                WtMud(bharun);
                FlowratePump(bharun);
                PowBit(bharun);
                VelNozzleAv(bharun);
                PresDropBit(bharun);
                CTimHold(bharun);
                CTimSteering(bharun);
                CTimDrillRot(bharun);
                CTimDrillSlid(bharun);
                CTimCirc(bharun);
                CTimReam(bharun);
                DistDrillRot(bharun);
                DistDrillSlid(bharun);
                DistReam(bharun);
                DistHold(bharun);
                RpmAv(bharun);
                RpmMx(bharun);
                RpmMn(bharun);
                RpmAvDh(bharun);
                RopAv(bharun);
                RopMx(bharun);
                RopMn(bharun);
                WobAv(bharun);
                WobMx(bharun);
                WobMn(bharun);
                WobAvDh(bharun);
                AziTop(bharun);
                AziBottom(bharun);
                InclStart(bharun);
                InclMx(bharun);
                InclMn(bharun);
                InclStop(bharun);
                TempMudDhMx(bharun);
                PresPumpAv(bharun);
                FlowrateBit(bharun);
                DrillingParams(bharun);
                BharunTubular(bharun);
                BharunCommonData(bharun);
                PlanDogleg(bharun);
               
                #endregion
                
                return Save();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "BharunRepository CreateBharun", null);
                return Save();
            }
        }       
        public bool DeleteBharun(Bharun bharun)
        {
            _db.Bharuns.Remove(bharun);
            return Save();
        }

        public Bharun GetBharunDetail(string Uid)
        {
            Bharun objBharun = new Bharun();
            objBharun= _db.Bharuns.FirstOrDefault(x=>x.Uid==Uid);
           
            return objBharun;
        }

        public ICollection<Bharun> GetBharunDetails()
        {
            return _db.Bharuns.OrderBy(x => x.NameWell).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateBharun(Bharun bharun)
        {
            try
            {
                #region Update method for other objects
                _db.Bharuns.Update(bharun);
                UpdateActDogleg(bharun);
                UpdateActDoglegMx(bharun);
                UpdateETimOpBit(bharun);
                UpdateMdHoleStart(bharun);
                UpdateMdHoleStop(bharun);
                UpdateHkldRot(bharun);
                UpdateHkldUp(bharun);
                UpdateHkldDn(bharun);
                UpdateTqOnBotAv(bharun);
                UpdateTqOnBotMx(bharun);
                UpdateTqOnBotMn(bharun);
                UpdateTqOffBotAv(bharun);
                UpdateTqDhAv(bharun);
                UpdateWtAboveJar(bharun);
                UpdateWtBelowJar(bharun);
                UpdateWtMud(bharun);
                UpdateFlowratePump(bharun);
                UpdatePowBit(bharun);
                UpdateVelNozzleAv(bharun);
                UpdatePresDropBit(bharun);
                UpdateCTimHold(bharun);
                UpdateCTimSteering(bharun);
                UpdateCTimDrillRot(bharun);
                UpdateCTimDrillSlid(bharun);
                UpdateCTimCirc(bharun);
                UpdateCTimReam(bharun);
                UpdateDistDrillRot(bharun);
                UpdateDistDrillSlid(bharun);
                UpdateDistReam(bharun);
                UpdateDistHold(bharun);
                UpdateDistSteering(bharun);
                UpdateRpmMx(bharun);
                UpdateRpmMn(bharun);
                UpdateRpmAvDh(bharun);
                UpdateWobMx(bharun);
                UpdateWobMn(bharun);
                UpdateWobAvDh(bharun);
                UpdateETimOpBit(bharun);
                UpdateMdHoleStart(bharun);
                UpdateMdHoleStop(bharun);
                UpdateTubular(bharun);
                UpdateHkldRot(bharun);
                UpdateOverPull(bharun);
                UpdateSlackOff(bharun);
                UpdateHkldUp(bharun);
                UpdateHkldDn(bharun);
                UpdateTqOnBotAv(bharun);
                UpdateTqOnBotMx(bharun);
                UpdateTqOnBotMn(bharun);
                UpdateTqOffBotAv(bharun);
                UpdateTqDhAv(bharun);
                UpdateWtAboveJar(bharun);
                UpdateWtBelowJar(bharun);
                UpdateWtMud(bharun);
                UpdateFlowratePump(bharun);
                UpdatePowBit(bharun);
                UpdateVelNozzleAv(bharun);
                UpdatePresDropBit(bharun);
                UpdateCTimHold(bharun);
                UpdateCTimSteering(bharun);
                UpdateCTimDrillRot(bharun);
                UpdateCTimDrillSlid(bharun);
                UpdateCTimCirc(bharun);
                UpdateCTimReam(bharun);
                UpdateDistDrillRot(bharun);
                UpdateDistDrillSlid(bharun);
                UpdateDistReam(bharun);
                UpdateDistHold(bharun);
                UpdateRpmAv(bharun);
                UpdateRpmMx(bharun);
                UpdateRpmMn(bharun);
                UpdateRpmAvDh(bharun);
                UpdateRopAv(bharun);
                UpdateRopMx(bharun);
                UpdateRopMn(bharun);
                UpdateWobAv(bharun);
                UpdateWobMx(bharun);
                UpdateWobMn(bharun);
                UpdateWobAvDh(bharun);
                UpdateAziTop(bharun);
                UpdateAziBottom(bharun);
                UpdateInclStart(bharun);
                UpdateInclMx(bharun);
                UpdateInclMn(bharun);
                UpdateInclStop(bharun);
                UpdateTempMudDhMx(bharun);
                UpdatePresPumpAv(bharun);
                UpdateFlowrateBit(bharun);
                UpdateDrillingParams(bharun);
                UpdateBharunTubular(bharun);
                UpdateBharunCommonData(bharun);
                UpdatePlanDogleg(bharun);

                #endregion
                return Save();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "BharunRepository UpdateBharun", null);
                return Save();
            }
        }


        #region Create Bharun Method
        private void ActDogleg(Bharun bharun)
        {
            if (bharun.ActDogleg.Uom != null)
            {
                var obj = _mapper.Map<BharunActDogleg>(bharun.ActDogleg);
                _db.BharunActDoglegs.Add(obj);
            }
        }
        private void ActDoglegMx(Bharun bharun)
        {
            if (bharun.ActDoglegMx.Uom != null)
            {
                var obj = _mapper.Map<BharunActDoglegMx>(bharun.ActDoglegMx);
                _db.BharunActDoglegMxs.Add(obj);
            }
        }
        private void ETimOpBit(Bharun bharun)
        {
            if (bharun.DrillingParams.ETimOpBit.Uom!=null)
            {
                var obj = _mapper.Map<BharunETimOpBit>(bharun.DrillingParams.ETimOpBit);
                _db.BharunETimOpBits.Add(obj);
            }
        }
        private void MdHoleStart(Bharun bharun)
        {
            if (bharun.DrillingParams.MdHoleStart.Uom != null)
            {
                var obj = _mapper.Map<BharunMdHoleStart>(bharun.DrillingParams.MdHoleStart);
                _db.BharunMdHoleStarts.Add(obj);
            }
        }

        private void Tubular(Bharun bharun)
        {
            if (bharun.DrillingParams.Tubular.UidRef != null)
            {
                var obj = _mapper.Map<BharunTubular>(bharun.DrillingParams.Tubular);
                _db.BharunTubulars.Add(obj);
            }
        }
        
        private void MdHoleStop(Bharun bharun)
        {
            if (bharun.DrillingParams.MdHoleStop.Uom != null)
            {
                var obj = _mapper.Map<BharunMdHoleStop>(bharun.DrillingParams.MdHoleStop);
                _db.BharunMdHoleStops.Add(obj);
            }
        }
        private void HkldRot(Bharun bharun)
        {
            if (bharun.DrillingParams.HkldRot.Uom != null)
            {
                var obj = _mapper.Map<BharunHkldRot>(bharun.DrillingParams.HkldRot);
                _db.BharunHkldRots.Add(obj);
            }
        }
        private void OverPull(Bharun bharun)
        {
            if (bharun.DrillingParams.OverPull.Uom != null)
            {
                var obj = _mapper.Map<BharunOverPull>(bharun.DrillingParams.OverPull);
                _db.BharunOverPulls.Add(obj);
            }
        }

        private void SlackOff(Bharun bharun)
        {
            if (bharun.DrillingParams.SlackOff.Uom != null)
            {
                var obj = _mapper.Map<BharunSlackOff>(bharun.DrillingParams.SlackOff);
                _db.BharunSlackOff.Add(obj);
            }
        }

        private void HkldUp(Bharun bharun)
        {
            if (bharun.DrillingParams.HkldUp.Uom != null)
            {
                var obj = _mapper.Map<BharunHkldUp>(bharun.DrillingParams.HkldUp);
                _db.BharunHkldUps.Add(obj);
            }
        }
        private void HkldDn(Bharun bharun)
        {
            if (bharun.DrillingParams.HkldDn.Uom != null)
            {
                var obj = _mapper.Map<BharunHkldDn>(bharun.DrillingParams.HkldDn);
                _db.BharunHkldDns.Add(obj);
            }
        }
        private void TqOnBotAv(Bharun bharun)
        {
            if (bharun.DrillingParams.TqOnBotAv.Uom != null)
            {
                var obj = _mapper.Map<BharunTqOnBotAv>(bharun.DrillingParams.TqOnBotAv);
                _db.BharunTqOnBotAvs.Add(obj);
            }
        }
        private void TqOnBotMx(Bharun bharun)
        {
            if (bharun.DrillingParams.TqOnBotMx.Uom != null)
            {
                var obj = _mapper.Map<BharunTqOnBotMx>(bharun.DrillingParams.TqOnBotMx);
                _db.BharunTqOnBotMxs.Add(obj);
            }
        }
        private void TqOnBotMn(Bharun bharun)
        {
            if (bharun.DrillingParams.TqOnBotMn.Uom != null)
            {
                var obj = _mapper.Map<BharunTqOnBotMn>(bharun.DrillingParams.TqOnBotMn);
                _db.BharunTqOnBotMns.Add(obj);
            }
        }
        private void TqOffBotAv(Bharun bharun)
        {
            if (bharun.DrillingParams.TqOffBotAv.Uom != null)
            {
                var obj = _mapper.Map<BharunTqOffBotAv>(bharun.DrillingParams.TqOffBotAv);
                _db.BharunTqOffBotAvs.Add(obj);
            }
        }
        private void TqDhAv(Bharun bharun)
        {
            if (bharun.DrillingParams.TqDhAv.Uom != null)
            {
                var obj = _mapper.Map<BharunTqDhAv>(bharun.DrillingParams.TqDhAv);
                _db.BharunTqDhAvs.Add(obj);
            }
        }
        private void WtAboveJar(Bharun bharun)
        {
            if (bharun.DrillingParams.WtAboveJar.Uom != null)
            {
                var obj = _mapper.Map<BharunWtAboveJar>(bharun.DrillingParams.WtAboveJar);
                _db.BharunWtAboveJars.Add(obj);
            }
        }
        private void WtBelowJar(Bharun bharun)
        {
            if (bharun.DrillingParams.WtBelowJar.Uom != null)
            {
                var obj = _mapper.Map<BharunWtBelowJar>(bharun.DrillingParams.WtBelowJar);
                _db.BharunWtBelowJars.Add(obj);
            }
        }
        private void WtMud(Bharun bharun)
        {
            if (bharun.DrillingParams.WtMud.Uom != null)
            {
                var obj = _mapper.Map<BharunWtMud>(bharun.DrillingParams.WtMud);
                _db.BharunWtMuds.Add(obj);
            }
        }
        private void FlowratePump(Bharun bharun)
        {
            if (bharun.DrillingParams.FlowratePump.Uom != null)
            {
                var obj = _mapper.Map<BharunFlowratePump>(bharun.DrillingParams.FlowratePump);
                _db.BharunFlowratePumps.Add(obj);
            }
        }
        private void PowBit(Bharun bharun)
        {
            if (bharun.DrillingParams.PowBit.Uom != null)
            {
                var obj = _mapper.Map<BharunPowBit>(bharun.DrillingParams.PowBit);
                _db.BharunPowBits.Add(obj);
            }
        }
        private void VelNozzleAv(Bharun bharun)
        {
            if (bharun.DrillingParams.VelNozzleAv.Uom != null)
            {
                var obj = _mapper.Map<BharunVelNozzleAv>(bharun.DrillingParams.VelNozzleAv);
                _db.BharunVelNozzleAvs.Add(obj);
            }
        }
        private void PresDropBit(Bharun bharun)
        {
            if (bharun.DrillingParams.PresDropBit.Uom != null)
            {
                var obj = _mapper.Map<BharunPresDropBit>(bharun.DrillingParams.PresDropBit);
                _db.BharunPresDropBits.Add(obj);
            }
        }
        private void CTimHold(Bharun bharun)
        {
            if (bharun.DrillingParams.CTimHold.Uom != null)
            {
                var obj = _mapper.Map<BharunCTimHold>(bharun.DrillingParams.CTimHold);
                _db.BharunCTimHolds.Add(obj);
            }
        }
        private void CTimSteering(Bharun bharun)
        {
            if (bharun.DrillingParams.CTimSteering.Uom != null)
            {
                var obj = _mapper.Map<BharunCTimSteering>(bharun.DrillingParams.CTimSteering);
                _db.BharunCTimSteerings.Add(obj);
            }
        }
        private void CTimDrillRot(Bharun bharun)
        {
            if (bharun.DrillingParams.CTimDrillRot.Uom != null)
            {
                var obj = _mapper.Map<BharunCTimDrillRot>(bharun.DrillingParams.CTimDrillRot);
                _db.BharunCTimDrillRots.Add(obj);
            }
        }
        private void CTimDrillSlid(Bharun bharun)
        {
            if (bharun.DrillingParams.CTimDrillSlid.Uom != null)
            {
                var obj = _mapper.Map<BharunCTimDrillSlid>(bharun.DrillingParams.CTimDrillSlid);
                _db.BharunCTimDrillSlids.Add(obj);
            }
        }

        private void CTimCirc(Bharun bharun)
        {
            if (bharun.DrillingParams.CTimCirc.Uom != null)
            {
                var obj = _mapper.Map<BharunCTimCirc>(bharun.DrillingParams.CTimCirc);
                _db.BharunCTimCircs.Add(obj);
            }
        }

        private void CTimReam(Bharun bharun)
        {
            if (bharun.DrillingParams.CTimReam.Uom != null)
            {
                var obj = _mapper.Map<BharunCTimReam>(bharun.DrillingParams.CTimReam);
                _db.BharunCTimReams.Add(obj);
            }
        }

        private void DistDrillRot(Bharun bharun)
        {
            if (bharun.DrillingParams.DistDrillRot.Uom != null)
            {
                var obj = _mapper.Map<BharunDistDrillRot>(bharun.DrillingParams.DistDrillRot);
                _db.BharunDistDrillRots.Add(obj);
            }
        }
        private void DistDrillSlid(Bharun bharun)
        {
            if (bharun.DrillingParams.DistDrillSlid.Uom != null)
            {
                var obj = _mapper.Map<BharunDistDrillSlid>(bharun.DrillingParams.DistDrillSlid);
                _db.BharunDistDrillSlids.Add(obj);
            }
        }
        private void DistReam(Bharun bharun)
        {
            if (bharun.DrillingParams.DistReam.Uom != null)
            {
                var obj = _mapper.Map<BharunDistReam>(bharun.DrillingParams.DistReam);
                _db.BharunDistReams.Add(obj);
            }
        }
        private void DistHold(Bharun bharun)
        {
            if (bharun.DrillingParams.DistHold.Uom != null)
            {
                var obj = _mapper.Map<BharunDistHold>(bharun.DrillingParams.DistHold);
                _db.BharunDistHolds.Add(obj);
            }
        }

        private void RpmAv(Bharun bharun)
        {
            if (bharun.DrillingParams.RpmAv.Uom != null)
            {
                var obj = _mapper.Map<BharunRpmAv>(bharun.DrillingParams.RpmAv);
                _db.BharunRpmAvs.Add(obj);
            }
        }
       
        private void DistSteering(Bharun bharun)
        {
            if (bharun.DrillingParams.DistSteering.Uom != null)
            {
                var obj = _mapper.Map<BharunDistSteering>(bharun.DrillingParams.DistSteering);
                _db.BharunDistSteerings.Add(obj);
            }
        }
        private void RpmMx(Bharun bharun)
        {
            if (bharun.DrillingParams.RpmMx.Uom != null)
            {
                var obj = _mapper.Map<BharunRpmMx>(bharun.DrillingParams.RpmMx);
                _db.BharunRpmMxs.Add(obj);
            }
        }
        private void RpmMn(Bharun bharun)
        {
            if (bharun.DrillingParams.RpmMn.Uom != null)
            {
                var obj = _mapper.Map<BharunRpmMn>(bharun.DrillingParams.RpmMn);
                _db.BharunRpmMns.Add(obj);
            }
        }

        private void RpmAvDh(Bharun bharun)
        {
            if (bharun.DrillingParams.RpmAvDh.Uom != null)
            {
                var obj = _mapper.Map<BharunRpmAvDh>(bharun.DrillingParams.RpmAvDh);
                _db.BharunRpmAvDhs.Add(obj);
            }
        }

        private void RopAv(Bharun bharun)
        {
            if (bharun.DrillingParams.RopAv.Uom != null)
            {
                var obj = _mapper.Map<BharunRopAv>(bharun.DrillingParams.RopAv);
                _db.BharunRopAvs.Add(obj);
            }
        }

        private void RopMx(Bharun bharun)
        {
            if (bharun.DrillingParams.RopMx.Uom != null)
            {
                var obj = _mapper.Map<BharunRopMx>(bharun.DrillingParams.RopMx);
                _db.BharunRopMxs.Add(obj);
            }
        }

        private void RopMn(Bharun bharun)
        {
            if (bharun.DrillingParams.RopMn.Uom != null)
            {
                var obj = _mapper.Map<BharunRopMn>(bharun.DrillingParams.RopMn);
                _db.BharunRopMns.Add(obj);
            }
        }

        private void WobAv(Bharun bharun)
        {
            if (bharun.DrillingParams.WobAv.Uom != null)
            {
                var obj = _mapper.Map<BharunWobAv>(bharun.DrillingParams.WobAv);
                _db.BharunWobAvs.Add(obj);
            }
        }
       

        private void WobMx(Bharun bharun)
        {
            if (bharun.DrillingParams.WobMx.Uom != null)
            {
                var obj = _mapper.Map<BharunWobMx>(bharun.DrillingParams.WobMx);
                _db.BharunWobMxs.Add(obj);
            }
        }
        private void WobMn(Bharun bharun)
        {
            if (bharun.DrillingParams.WobMn.Uom != null)
            {
                var obj = _mapper.Map<BharunWobMn>(bharun.DrillingParams.WobMn);
                _db.BharunWobMns.Add(obj);
            }
        }
        private void WobAvDh(Bharun bharun)
        {
            if (bharun.DrillingParams.WobAvDh.Uom != null)
            {
                var obj = _mapper.Map<BharunWobAvDh>(bharun.DrillingParams.WobAvDh);
                _db.BharunWobAvDhs.Add(obj);
            }
        }
        private void AziTop(Bharun bharun)
        {
            if (bharun.DrillingParams.AziTop.Uom != null)
            {
                var obj = _mapper.Map<BharunAziTop>(bharun.DrillingParams.AziTop);
                _db.BharunAziTops.Add(obj);
            }
        }
        private void AziBottom(Bharun bharun)
        {
            if (bharun.DrillingParams.AziBottom.Uom != null)
            {
                var obj = _mapper.Map<BharunAziBottom>(bharun.DrillingParams.AziBottom);
                _db.BharunAziBottoms.Add(obj);
            }
        }
        private void InclStart(Bharun bharun)
        {
            if (bharun.DrillingParams.InclStart.Uom != null)
            {
                var obj = _mapper.Map<BharunInclStart>(bharun.DrillingParams.InclStart);
                _db.BharunInclStarts.Add(obj);
            }
        }
        private void InclMx(Bharun bharun)
        {
            if (bharun.DrillingParams.InclMx.Uom != null)
            {
                var obj = _mapper.Map<BharunInclMx>(bharun.DrillingParams.InclMx);
                _db.BharunInclMxs.Add(obj);
            }
        }
        private void InclMn(Bharun bharun)
        {
            if (bharun.DrillingParams.InclMn.Uom != null)
            {
                var obj = _mapper.Map<BharunInclMn>(bharun.DrillingParams.InclMn);
                _db.BharunInclMns.Add(obj);
            }
        }
        private void InclStop(Bharun bharun)
        {
            if (bharun.DrillingParams.InclStop.Uom != null)
            {
                var obj = _mapper.Map<BharunInclStop>(bharun.DrillingParams.InclStop);
                _db.BharunInclStops.Add(obj);
            }
        }
        private void TempMudDhMx(Bharun bharun)
        {
            if (bharun.DrillingParams.TempMudDhMx.Uom != null)
            {
                var obj = _mapper.Map<BharunTempMudDhMx>(bharun.DrillingParams.TempMudDhMx);
                _db.BharunTempMudDhMxs.Add(obj);
            }
        }
        private void PresPumpAv(Bharun bharun)
        {
            if (bharun.DrillingParams.PresPumpAv.Uom != null)
            {
                var obj = _mapper.Map<BharunPresPumpAv>(bharun.DrillingParams.PresPumpAv);
                _db.BharunPresPumpAvs.Add(obj);
            }
        }
        private void FlowrateBit(Bharun bharun)
        {
            if (bharun.DrillingParams.FlowrateBit.Uom != null)
            {
                var obj = _mapper.Map<BharunFlowrateBit>(bharun.DrillingParams.FlowrateBit);
                _db.BharunFlowrateBits.Add(obj);
            }
        }

        
        private void DrillingParams(Bharun bharun)
        {
            if (bharun.DrillingParams.Uid != null)
            {
                var obj = _mapper.Map<BharunDrillingParams>(bharun.DrillingParams);
                _db.BharunDrillingParamss.Add(obj);
            }
        }
        private void BharunTubular(Bharun bharun)
        {
            if (bharun.Tubular.UidRef != null)
            {
                var obj = _mapper.Map<BharunTubular>(bharun.Tubular);
                _db.BharunTubulars.Add(obj);
            }
        }
        private void BharunCommonData(Bharun bharun)
        {
                var obj = _mapper.Map<BharunCommonData>(bharun.CommonData);
                _db.BharunCommonDatas.Add(obj);
        }
        private void PlanDogleg(Bharun bharun)
        {
            if (bharun.PlanDogleg.Uom != null)
            {
                var obj = _mapper.Map<BharunPlanDogleg>(bharun.PlanDogleg);
                _db.BharunPlanDoglegs.Add(obj);
            }
        }
        #endregion Create Bharun Method

        #region Update Bharun Method

        private void UpdateActDogleg(Bharun bharun)
        {
            if (bharun.ActDogleg.Uom != null)
            {
                var obj = _mapper.Map<BharunActDogleg>(bharun.ActDogleg);
                _db.BharunActDoglegs.Add(obj);
            }
        }
        private void UpdateActDoglegs(Bharun bharun)
        {
            if (bharun.ActDogleg.Uom != null)
            {
                var obj = _mapper.Map<BharunActDogleg>(bharun.ActDogleg);
                _db.BharunActDoglegs.Update(obj);
            }
        }
        private void UpdateActDoglegMx(Bharun bharun)
        {
            if (bharun.ActDoglegMx.Uom != null)
            {
                var obj = _mapper.Map<BharunActDoglegMx>(bharun.ActDoglegMx);
                _db.BharunActDoglegMxs.Update(obj);
            }
        }
        private void UpdateETimOpBit(Bharun bharun)
        {
            if (bharun.DrillingParams.ETimOpBit.Uom != null)
            {
                var obj = _mapper.Map<BharunETimOpBit>(bharun.DrillingParams.ETimOpBit);
                _db.BharunETimOpBits.Update(obj);
            }
        }

        private void UpdateTubular(Bharun bharun)
        {
            if (bharun.DrillingParams.Tubular.UidRef != null)
            {
                var obj = _mapper.Map<BharunTubular>(bharun.DrillingParams.Tubular);
                _db.BharunTubulars.Update(obj);
            }
        }
        private void UpdateMdHoleStart(Bharun bharun)
        {
            if (bharun.DrillingParams.MdHoleStart.Uom != null)
            {
                var obj = _mapper.Map<BharunMdHoleStart>(bharun.DrillingParams.MdHoleStart);
                _db.BharunMdHoleStarts.Update(obj);
            }
        }
        private void UpdateMdHoleStop(Bharun bharun)
        {
            if (bharun.DrillingParams.MdHoleStop.Uom != null)
            {
                var obj = _mapper.Map<BharunMdHoleStop>(bharun.DrillingParams.MdHoleStop);
                _db.BharunMdHoleStops.Update(obj);
            }
        }
        private void UpdateHkldRot(Bharun bharun)
        {
            if (bharun.DrillingParams.HkldRot.Uom != null)
            {
                var obj = _mapper.Map<BharunHkldRot>(bharun.DrillingParams.HkldRot);
                _db.BharunHkldRots.Update(obj);
            }
        }
        private void UpdateHkldUp(Bharun bharun)
        {
            if (bharun.DrillingParams.HkldUp.Uom != null)
            {
                var obj = _mapper.Map<BharunHkldUp>(bharun.DrillingParams.HkldUp);
                _db.BharunHkldUps.Update(obj);
            }
        }

        private void UpdateOverPull(Bharun bharun)
        {
            if (bharun.DrillingParams.OverPull.Uom != null)
            {
                var obj = _mapper.Map<BharunOverPull>(bharun.DrillingParams.OverPull);
                _db.BharunOverPulls.Update(obj);
            }
        }

        private void UpdateSlackOff(Bharun bharun)
        {
            if (bharun.DrillingParams.SlackOff.Uom != null)
            {
                var obj = _mapper.Map<BharunSlackOff>(bharun.DrillingParams.SlackOff);
                _db.BharunSlackOff.Update(obj);
            }
        }
        private void UpdateHkldDn(Bharun bharun)
        {
            if (bharun.DrillingParams.HkldDn.Uom != null)
            {
                var obj = _mapper.Map<BharunHkldDn>(bharun.DrillingParams.HkldDn);
                _db.BharunHkldDns.Update(obj);
            }
        }
        private void UpdateTqOnBotAv(Bharun bharun)
        {
            if (bharun.DrillingParams.TqOnBotAv.Uom != null)
            {
                var obj = _mapper.Map<BharunTqOnBotAv>(bharun.DrillingParams.TqOnBotAv);
                _db.BharunTqOnBotAvs.Update(obj);
            }
        }
        private void UpdateTqOnBotMx(Bharun bharun)
        {
            if (bharun.DrillingParams.TqOnBotMx.Uom != null)
            {
                var obj = _mapper.Map<BharunTqOnBotMx>(bharun.DrillingParams.TqOnBotMx);
                _db.BharunTqOnBotMxs.Update(obj);
            }
        }
        private void UpdateTqOnBotMn(Bharun bharun)
        {
            if (bharun.DrillingParams.TqOnBotMn.Uom != null)
            {
                var obj = _mapper.Map<BharunTqOnBotMn>(bharun.DrillingParams.TqOnBotMn);
                _db.BharunTqOnBotMns.Update(obj);
            }
        }
        private void UpdateTqOffBotAv(Bharun bharun)
        {
            if (bharun.DrillingParams.TqOffBotAv.Uom != null)
            {
                var obj = _mapper.Map<BharunTqOffBotAv>(bharun.DrillingParams.TqOffBotAv);
                _db.BharunTqOffBotAvs.Update(obj);
            }
        }
        private void UpdateTqDhAv(Bharun bharun)
        {
            if (bharun.DrillingParams.TqDhAv.Uom != null)
            {
                var obj = _mapper.Map<BharunTqDhAv>(bharun.DrillingParams.TqDhAv);
                _db.BharunTqDhAvs.Update(obj);
            }
        }
        private void UpdateWtAboveJar(Bharun bharun)
        {
            if (bharun.DrillingParams.WtAboveJar.Uom != null)
            {
                var obj = _mapper.Map<BharunWtAboveJar>(bharun.DrillingParams.WtAboveJar);
                _db.BharunWtAboveJars.Update(obj);
            }
        }
        private void UpdateWtBelowJar(Bharun bharun)
        {
            if (bharun.DrillingParams.WtBelowJar.Uom != null)
            {
                var obj = _mapper.Map<BharunWtBelowJar>(bharun.DrillingParams.WtBelowJar);
                _db.BharunWtBelowJars.Update(obj);
            }
        }
        private void UpdateWtMud(Bharun bharun)
        {
            if (bharun.DrillingParams.WtMud.Uom != null)
            {
                var obj = _mapper.Map<BharunWtMud>(bharun.DrillingParams.WtMud);
                _db.BharunWtMuds.Update(obj);
            }
        }
        private void UpdateFlowratePump(Bharun bharun)
        {
            if (bharun.DrillingParams.FlowratePump.Uom != null)
            {
                var obj = _mapper.Map<BharunFlowratePump>(bharun.DrillingParams.FlowratePump);
                _db.BharunFlowratePumps.Update(obj);
            }
        }
        private void UpdatePowBit(Bharun bharun)
        {
            if (bharun.DrillingParams.PowBit.Uom != null)
            {
                var obj = _mapper.Map<BharunPowBit>(bharun.DrillingParams.PowBit);
                _db.BharunPowBits.Update(obj);
            }
        }
        private void UpdateVelNozzleAv(Bharun bharun)
        {
            if (bharun.DrillingParams.VelNozzleAv.Uom != null)
            {
                var obj = _mapper.Map<BharunVelNozzleAv>(bharun.DrillingParams.VelNozzleAv);
                _db.BharunVelNozzleAvs.Update(obj);
            }
        }
        private void UpdatePresDropBit(Bharun bharun)
        {
            if (bharun.DrillingParams.PresDropBit.Uom != null)
            {
                var obj = _mapper.Map<BharunPresDropBit>(bharun.DrillingParams.PresDropBit);
                _db.BharunPresDropBits.Update(obj);
            }
        }
        private void UpdateCTimHold(Bharun bharun)
        {
            if (bharun.DrillingParams.CTimHold.Uom != null)
            {
                var obj = _mapper.Map<BharunCTimHold>(bharun.DrillingParams.CTimHold);
                _db.BharunCTimHolds.Update(obj);
            }
        }
        private void UpdateCTimSteering(Bharun bharun)
        {
            if (bharun.DrillingParams.CTimSteering.Uom != null)
            {
                var obj = _mapper.Map<BharunCTimSteering>(bharun.DrillingParams.CTimSteering);
                _db.BharunCTimSteerings.Update(obj);
            }
        }
        private void UpdateCTimDrillRot(Bharun bharun)
        {
            if (bharun.DrillingParams.CTimDrillRot.Uom != null)
            {
                var obj = _mapper.Map<BharunCTimDrillRot>(bharun.DrillingParams.CTimDrillRot);
                _db.BharunCTimDrillRots.Update(obj);
            }
        }
        private void UpdateCTimDrillSlid(Bharun bharun)
        {
            if (bharun.DrillingParams.CTimDrillSlid.Uom != null)
            {
                var obj = _mapper.Map<BharunCTimDrillSlid>(bharun.DrillingParams.CTimDrillSlid);
                _db.BharunCTimDrillSlids.Update(obj);
            }
        }

        private void UpdateCTimCirc(Bharun bharun)
        {
            if (bharun.DrillingParams.CTimCirc.Uom != null)
            {
                var obj = _mapper.Map<BharunCTimCirc>(bharun.DrillingParams.CTimCirc);
                _db.BharunCTimCircs.Update(obj);
            }
        }

        private void UpdateCTimReam(Bharun bharun)
        {
            if (bharun.DrillingParams.CTimReam.Uom != null)
            {
                var obj = _mapper.Map<BharunCTimReam>(bharun.DrillingParams.CTimReam);
                _db.BharunCTimReams.Update(obj);
            }
        }

        private void UpdateDistDrillRot(Bharun bharun)
        {
            if (bharun.DrillingParams.DistDrillRot.Uom != null)
            {
                var obj = _mapper.Map<BharunDistDrillRot>(bharun.DrillingParams.DistDrillRot);
                _db.BharunDistDrillRots.Update(obj);
            }
        }
        private void UpdateDistDrillSlid(Bharun bharun)
        {
            if (bharun.DrillingParams.DistDrillSlid.Uom != null)
            {
                var obj = _mapper.Map<BharunDistDrillSlid>(bharun.DrillingParams.DistDrillSlid);
                _db.BharunDistDrillSlids.Update(obj);
            }
        }
        private void UpdateDistReam(Bharun bharun)
        {
            if (bharun.DrillingParams.DistReam.Uom != null)
            {
                var obj = _mapper.Map<BharunDistReam>(bharun.DrillingParams.DistReam);
                _db.BharunDistReams.Update(obj);
            }
        }
        private void UpdateDistHold(Bharun bharun)
        {
            if (bharun.DrillingParams.DistHold.Uom != null)
            {
                var obj = _mapper.Map<BharunDistHold>(bharun.DrillingParams.DistHold);
                _db.BharunDistHolds.Update(obj);
            }
        }

        private void UpdateRpmAv(Bharun bharun)
        {
            if (bharun.DrillingParams.RpmAv.Uom != null)
            {
                var obj = _mapper.Map<BharunRpmAv>(bharun.DrillingParams.RpmAv);
                _db.BharunRpmAvs.Update(obj);
            }
        }
        private void UpdateDistSteering(Bharun bharun)
        {
            if (bharun.DrillingParams.DistSteering.Uom != null)
            {
                var obj = _mapper.Map<BharunDistSteering>(bharun.DrillingParams.DistSteering);
                _db.BharunDistSteerings.Update(obj);
            }
        }
        private void UpdateRpmMx(Bharun bharun)
        {
            if (bharun.DrillingParams.RpmMx.Uom != null)
            {
                var obj = _mapper.Map<BharunRpmMx>(bharun.DrillingParams.RpmMx);
                _db.BharunRpmMxs.Update(obj);
            }
        }
        private void UpdateRpmMn(Bharun bharun)
        {
            if (bharun.DrillingParams.RpmMn.Uom != null)
            {
                var obj = _mapper.Map<BharunRpmMn>(bharun.DrillingParams.RpmMn);
                _db.BharunRpmMns.Update(obj);
            }
        }

        private void UpdateRpmAvDh(Bharun bharun)
        {
            if (bharun.DrillingParams.RpmAvDh.Uom != null)
            {
                var obj = _mapper.Map<BharunRpmAvDh>(bharun.DrillingParams.RpmAvDh);
                _db.BharunRpmAvDhs.Update(obj);
            }
        }

        private void UpdateRopAv(Bharun bharun)
        {
            if (bharun.DrillingParams.RopAv.Uom != null)
            {
                var obj = _mapper.Map<BharunRopAv>(bharun.DrillingParams.RopAv);
                _db.BharunRopAvs.Update(obj);
            }
        }

        private void UpdateRopMx(Bharun bharun)
        {
            if (bharun.DrillingParams.RopMx.Uom != null)
            {
                var obj = _mapper.Map<BharunRopMx>(bharun.DrillingParams.RopMx);
                _db.BharunRopMxs.Update(obj);
            }
        }

        private void UpdateRopMn(Bharun bharun)
        {
            if (bharun.DrillingParams.RopMn.Uom != null)
            {
                var obj = _mapper.Map<BharunRopMn>(bharun.DrillingParams.RopMn);
                _db.BharunRopMns.Update(obj);
            }
        }

        private void UpdateWobAv(Bharun bharun)
        {
            if (bharun.DrillingParams.WobAv.Uom != null)
            {
                var obj = _mapper.Map<BharunWobAv>(bharun.DrillingParams.WobAv);
                _db.BharunWobAvs.Update(obj);
            }
        }

        private void UpdateWobMx(Bharun bharun)
        {
            if (bharun.DrillingParams.WobMx.Uom != null)
            {
                var obj = _mapper.Map<BharunWobMx>(bharun.DrillingParams.WobMx);
                _db.BharunWobMxs.Update(obj);
            }
        }
        private void UpdateWobMn(Bharun bharun)
        {
            if (bharun.DrillingParams.WobMn.Uom != null)
            {
                var obj = _mapper.Map<BharunWobMn>(bharun.DrillingParams.WobMn);
                _db.BharunWobMns.Update(obj);
            }
        }
        private void UpdateWobAvDh(Bharun bharun)
        {
            if (bharun.DrillingParams.WobAvDh.Uom != null)
            {
                var obj = _mapper.Map<BharunWobAvDh>(bharun.DrillingParams.WobAvDh);
                _db.BharunWobAvDhs.Update(obj);
            }
        }
        private void UpdateAziTop(Bharun bharun)
        {
            if (bharun.DrillingParams.AziTop.Uom != null)
            {
                var obj = _mapper.Map<BharunAziTop>(bharun.DrillingParams.AziTop);
                _db.BharunAziTops.Update(obj);
            }
        }
        private void UpdateAziBottom(Bharun bharun)
        {
            if (bharun.DrillingParams.AziBottom.Uom != null)
            {
                var obj = _mapper.Map<BharunAziBottom>(bharun.DrillingParams.AziBottom);
                _db.BharunAziBottoms.Update(obj);
            }
        }
        private void UpdateInclStart(Bharun bharun)
        {
            if (bharun.DrillingParams.InclStart.Uom != null)
            {
                var obj = _mapper.Map<BharunInclStart>(bharun.DrillingParams.InclStart);
                _db.BharunInclStarts.Update(obj);
            }
        }
        private void UpdateInclMx(Bharun bharun)
        {
            if (bharun.DrillingParams.InclMx.Uom != null)
            {
                var obj = _mapper.Map<BharunInclMx>(bharun.DrillingParams.InclMx);
                _db.BharunInclMxs.Update(obj);
            }
        }
        private void UpdateInclMn(Bharun bharun)
        {
            if (bharun.DrillingParams.InclMn.Uom != null)
            {
                var obj = _mapper.Map<BharunInclMn>(bharun.DrillingParams.InclMn);
                _db.BharunInclMns.Update(obj);
            }
        }
        private void UpdateInclStop(Bharun bharun)
        {
            if (bharun.DrillingParams.InclStop.Uom != null)
            {
                var obj = _mapper.Map<BharunInclStop>(bharun.DrillingParams.InclStop);
                _db.BharunInclStops.Update(obj);
            }
        }
        private void UpdateTempMudDhMx(Bharun bharun)
        {
            if (bharun.DrillingParams.TempMudDhMx.Uom != null)
            {
                var obj = _mapper.Map<BharunTempMudDhMx>(bharun.DrillingParams.TempMudDhMx);
                _db.BharunTempMudDhMxs.Update(obj);
            }
        }
        private void UpdatePresPumpAv(Bharun bharun)
        {
            if (bharun.DrillingParams.PresPumpAv.Uom != null)
            {
                var obj = _mapper.Map<BharunPresPumpAv>(bharun.DrillingParams.PresPumpAv);
                _db.BharunPresPumpAvs.Update(obj);
            }
        }
        private void UpdateFlowrateBit(Bharun bharun)
        {
            if (bharun.DrillingParams.FlowrateBit.Uom != null)
            {
                var obj = _mapper.Map<BharunFlowrateBit>(bharun.DrillingParams.FlowrateBit);
                _db.BharunFlowrateBits.Update(obj);
            }
        }
        private void UpdateDrillingParams(Bharun bharun)
        {
            if (bharun.DrillingParams.Uid != null)
            {
                var obj = _mapper.Map<BharunDrillingParams>(bharun.DrillingParams);
                _db.BharunDrillingParamss.Update(obj);
            }
        }
        private void UpdateBharunTubular(Bharun bharun)
        {
            if (bharun.Tubular.UidRef != null)
            {
                var obj = _mapper.Map<BharunTubular>(bharun.Tubular);
                _db.BharunTubulars.Update(obj);
            }
        }
        private void UpdateBharunCommonData(Bharun bharun)
        {
            if (bharun.CommonData.CommonDataId != 0)
            {
                var obj = _mapper.Map<BharunCommonData>(bharun.CommonData);
                _db.BharunCommonDatas.Update(obj);
            }
        }
        private void UpdatePlanDogleg(Bharun bharun)
        {
            if (bharun.PlanDogleg.Uom != null)
            {
                var obj = _mapper.Map<BharunPlanDogleg>(bharun.PlanDogleg);
                _db.BharunPlanDoglegs.Update(obj);
            }
        }
        #endregion Create Bharun Method

    }
}
