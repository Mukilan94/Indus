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
    public class TubularRepository : ITubularRepository
    {
        private readonly WellAIAdvisiorContext _db;
        private readonly WebAIAdvisorContext _wdb;
        private readonly IMapper _mapper;
        private ITenantRepository _tenantRepository;
        public TubularRepository(WellAIAdvisiorContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper, ITenantRepository tenantRepository, WebAIAdvisorContext wdb)
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
        public bool TubularExists(string uid)
        {
            bool value = _db.Tubulars.Any(x => x.Uid.ToLower().Trim() == uid.ToLower().Trim());
            return value;
        }

        public bool CreateTubular(Tubular tubular)
        {
            try
            {
                DiaHoleAssy(tubular);
                Id(tubular);
                Od(tubular);
                Len(tubular);
                LenJointAv(tubular);
                WtPerLen(tubular);
                OdDrift(tubular);
                TensYield(tubular);
                TqYield(tubular);
                StressFatig(tubular);
                LenFishneck(tubular);
                IdFishneck(tubular);
                OdFishneck(tubular);
                Disp(tubular);
                PresBurst(tubular);
                PresCollapse(tubular);
                WearWall(tubular);
                ThickWall(tubular);
                BendStiffness(tubular);
                AxialStiffness(tubular);
                TorsionalStiffness(tubular);
                DoglegMx(tubular);
                NameTag(tubular);
                DiaBit(tubular);
                DiaPassThru(tubular);
                DiaPilot(tubular);
                Cost(tubular);
                BitRecord(tubular);
                AreaNozzleFlow(tubular);
                DiaNozzle(tubular);
                BitRecord(tubular);
                Nozzle(tubular);
                HoleOpener(tubular);
                Stabilizer(tubular);
                Motor(tubular);
                Bend(tubular);
                MwdTool(tubular);
                Connection(tubular);
                Jar(tubular);
                TubularComponent(tubular);
                DiaHoleOpener(tubular);
                LenBlade(tubular);
                OdBladeMx(tubular);
                OdBladeMn(tubular);
                DistBladeBot(tubular);
                OffsetTool(tubular);
                FlowrateMn(tubular);
                FlowrateMx(tubular);
                DiaRotorNozzle(tubular);
                ClearanceBearBox(tubular);
                TempOpMx(tubular);
                BendSettingsMn(tubular);
                BendSettingsMx(tubular);
                Motor(tubular);
                Angle(tubular);
                DistBendBot(tubular);
                Bend(tubular);
                TempMx(tubular);
                IdEquv(tubular);
                OffsetBot(tubular);
                Sensor(tubular);
                SizeThread(tubular);
                CriticalCrossSection(tubular);
                PresLeak(tubular);
                TqMakeup(tubular);
                ForUpSet(tubular);
                ForDownSet(tubular);
                ForUpTrip(tubular);
                ForDownTrip(tubular);
                ForPmpOpen(tubular);
                ForSealFric(tubular);
                CommonData(tubular);
                _db.Tubulars.Add(tubular);
                return Save();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "TubularRepositroy CreateTubular", null);
                return Save();
            }
        }



        public bool DeleteTubular(Tubular tubular)
        {
            _db.Tubulars.Remove(tubular);
            return Save();
        }

        public Tubular GetTubularDetail(string Uid)
        {
            return _db.Tubulars.FirstOrDefault(x => x.Uid == Uid);

        }

        public ICollection<Tubular> GetTubularDetails()
        {
            return _db.Tubulars.OrderBy(x => x.NameWell).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateTubular(Tubular tubular)
        {
            try
            {
                UpdateDiaHoleAssy(tubular);
                UpdateId(tubular);
                UpdateOd(tubular);
                UpdateLen(tubular);
                UpdateLenJointAv(tubular);
                UpdateWtPerLen(tubular);
                UpdateOdDrift(tubular);
                UpdateTensYield(tubular);
                UpdateTqYield(tubular);
                UpdateStressFatig(tubular);
                UpdateLenFishneck(tubular);
                UpdateIdFishneck(tubular);
                UpdateOdFishneck(tubular);
                UpdateDisp(tubular);
                UpdatePresBurst(tubular);
                UpdatePresCollapse(tubular);
                UpdateWearWall(tubular);
                UpdateThickWall(tubular);
                UpdateBendStiffness(tubular);
                UpdateAxialStiffness(tubular);
                UpdateTorsionalStiffness(tubular);
                UpdateDoglegMx(tubular);
                UpdateNameTag(tubular);
                UpdateDiaBit(tubular);
                UpdateDiaPassThru(tubular);
                UpdateDiaPilot(tubular);
                UpdateCost(tubular);
                UpdateBitRecord(tubular);
                UpdateAreaNozzleFlow(tubular);
                UpdateDiaNozzle(tubular);
                UpdateBitRecord(tubular);
                UpdateNozzle(tubular);
                UpdateHoleOpener(tubular);
                UpdateStabilizer(tubular);
                UpdateMotor(tubular);
                UpdateBend(tubular);
                UpdateMwdTool(tubular);
                UpdateConnection(tubular);
                UpdateJar(tubular);
                UpdateTubularComponent(tubular);
                UpdateDiaHoleOpener(tubular);
                UpdateLenBlade(tubular);
                UpdateOdBladeMx(tubular);
                UpdateOdBladeMn(tubular);
                UpdateDistBladeBot(tubular);
                UpdateOffsetTool(tubular);
                UpdateFlowrateMn(tubular);
                UpdateFlowrateMx(tubular);
                UpdateDiaRotorNozzle(tubular);
                UpdateClearanceBearBox(tubular);
                UpdateTempOpMx(tubular);
                UpdateBendSettingsMn(tubular);
                UpdateBendSettingsMx(tubular);
                UpdateMotor(tubular);
                UpdateAngle(tubular);
                UpdateDistBendBot(tubular);
                UpdateBend(tubular);
                UpdateTempMx(tubular);
                UpdateIdEquv(tubular);
                UpdateOffsetBot(tubular);
                UpdateSensor(tubular);
                UpdateSizeThread(tubular);
                UpdateCriticalCrossSection(tubular);
                UpdatePresLeak(tubular);
                UpdateTqMakeup(tubular);
                UpdateForUpSet(tubular);
                UpdateForDownSet(tubular);
                UpdateForUpTrip(tubular);
                UpdateForDownTrip(tubular);
                UpdateForPmpOpen(tubular);
                UpdateForSealFric(tubular);
                UpdateCommonData(tubular);
                _db.Tubulars.Update(tubular);
                return Save();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "TubularRepositroy UpdateTubular", null);
                return Save();
            }
        }
        #region Insert Tubular
        private void DiaHoleAssy(Tubular tubular)
        {
            if (tubular.DiaHoleAssy.Uom != null)
            {
                var obj = _mapper.Map<TubularDiaHoleAssy>(tubular.DiaHoleAssy);
                _db.TubularDiaHoleAssy.Add(obj);
            }
        }

        private void Id(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Id != null && item.Id.Uom != null)
                {
                    var obj = _mapper.Map<TubularId>(item.Id);
                    _db.TubularId.Add(obj);
                }

                if (item.Connection!=null && item.Connection.Id.Uom != null)
                {
                    var obj = _mapper.Map<TubularId>(item.Connection.Id);
                    _db.TubularId.Add(obj);
                }
            }
        }

        private void Od(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Od != null && item.Od.Uom != null)
                {
                    var obj = _mapper.Map<TubularOd>(item.Od);
                    _db.TubularOd.Add(obj);
                }
                if (item.Connection !=null && item.Connection.Od.Uom != null)
                {
                    var obj = _mapper.Map<TubularOd>(item.Connection.Od);
                    _db.TubularOd.Add(obj);
                }
            }
        }

        private void Len(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Len != null &&  item.Len.Uom != null)
                {
                    var obj = _mapper.Map<TubularLen>(item.Len);
                    _db.TubularLen.Add(obj);
                }

                if (item.Connection != null &&  item.Connection.Len.Uom != null)
                {
                    var obj = _mapper.Map<TubularLen>(item.Connection.Len);
                    _db.TubularLen.Add(obj);
                }
            }
        }


        private void LenJointAv(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.LenJointAv != null && item.LenJointAv.Uom != null)
                {
                    var obj = _mapper.Map<TubularLenJointAv>(item.LenJointAv);
                    _db.TubularLenJointAv.Add(obj);
                }
            }
        }

        private void WtPerLen(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.WtPerLen != null && item.WtPerLen.Uom != null)
                {
                    var obj = _mapper.Map<TubularWtPerLen>(item.WtPerLen);
                    _db.TubularWtPerLen.Add(obj);
                }
            }
        }

        private void OdDrift(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.OdDrift != null && item.OdDrift.Uom != null)
                {
                    var obj = _mapper.Map<TubularOdDrift>(item.OdDrift);
                    _db.TubularOdDrifts.Add(obj);
                }
            }
        }

        private void TensYield(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.TensYield != null && item.TensYield.Uom != null)
                {
                    var obj = _mapper.Map<TubularTensYield>(item.TensYield);
                    _db.TubularTensYield.Add(obj);
                }
                if (item.Connection != null &&  item.Connection.TensYield.Uom != null)
                {
                    var obj = _mapper.Map<TubularTensYield>(item.Connection.TensYield);
                    _db.TubularTensYield.Add(obj);
                }
            }
        }

        private void TqYield(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.TqYield != null && item.TqYield.Uom != null)
                {
                    var obj = _mapper.Map<TubularTqYield>(item.TqYield);
                    _db.TubularTqYield.Add(obj);
                }
                if (item.Connection != null &&  item.Connection.TqYield.Uom != null)
                {
                    var obj = _mapper.Map<TubularTqYield>(item.Connection.TqYield);
                    _db.TubularTqYield.Add(obj);
                }
            }
        }

        private void CriticalCrossSection(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {

                if (item.Connection != null &&  item.Connection.CriticalCrossSection.Uom != null)
                {
                    var obj = _mapper.Map<TubularCriticalCrossSection>(item.Connection.CriticalCrossSection);
                    _db.TubularCriticalCrossSection.Add(obj);
                }
            }
        }
        private void PresLeak(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {

                if (item.Connection != null &&  item.Connection.PresLeak.Uom != null)
                {
                    var obj = _mapper.Map<TubularPresLeak>(item.Connection.PresLeak);
                    _db.TubularPresLeak.Add(obj);
                }
            }
        }

        private void TqMakeup(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {

                if (item.Connection != null &&  item.Connection.TqMakeup.Uom != null)
                {
                    var obj = _mapper.Map<TubularTqMakeup>(item.Connection.TqMakeup);
                    _db.TubularTqMakeup.Add(obj);
                }
            }
        }
        private void StressFatig(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.StressFatig != null &&  item.StressFatig.Uom != null)
                {
                    var obj = _mapper.Map<TubularStressFatig>(item.StressFatig);
                    _db.TubularStressFatig.Add(obj);
                }
            }
        }

        private void LenFishneck(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.LenFishneck != null &&  item.LenFishneck.Uom != null)
                {
                    var obj = _mapper.Map<TubularLenFishneck>(item.LenFishneck);
                    _db.TubularLenFishneck.Add(obj);
                }
            }
        }

        private void IdFishneck(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.IdFishneck != null && item.IdFishneck.Uom != null)
                {
                    var obj = _mapper.Map<TubularIdFishneck>(item.IdFishneck);
                    _db.TubularIdFishneck.Add(obj);
                }
            }
        }

        private void OdFishneck(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.OdFishneck != null && item.OdFishneck.Uom != null)
                {
                    var obj = _mapper.Map<TubularOdFishneck>(item.OdFishneck);
                    _db.TubularOdFishneck.Add(obj);
                }
            }
        }
        private void Disp(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Disp != null &&  item.Disp.Uom != null)
                {
                    var obj = _mapper.Map<TubularDisp>(item.Disp);
                    _db.TubularDisp.Add(obj);
                }
            }
        }
        private void PresBurst(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.PresBurst != null && item.PresBurst.Uom != null)
                {
                    var obj = _mapper.Map<TubularPresBurst>(item.PresBurst);
                    _db.TubularPresBurst.Add(obj);
                }
            }
        }

        private void PresCollapse(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.PresCollapse != null && item.PresCollapse.Uom != null)
                {
                    var obj = _mapper.Map<TubularPresCollapse>(item.PresCollapse);
                    _db.TubularPresCollapse.Add(obj);
                }
            }
        }

        private void WearWall(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.WearWall != null &&  item.WearWall.Uom != null)
                {
                    var obj = _mapper.Map<TubularWearWall>(item.WearWall);
                    _db.TubularWearWall.Add(obj);
                }
            }
        }

        private void ThickWall(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.ThickWall != null && item.ThickWall.Uom != null)
                {
                    var obj = _mapper.Map<TubularThickWall>(item.ThickWall);
                    _db.TubularThickWall.Add(obj);
                }
            }
        }

        private void BendStiffness(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.BendStiffness != null && item.BendStiffness.Uom != null)
                {
                    var obj = _mapper.Map<TubularBendStiffness>(item.BendStiffness);
                    _db.TubularBendStiffness.Add(obj);
                }
            }
        }

        private void AxialStiffness(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.AxialStiffness != null && item.AxialStiffness.Uom != null)
                {
                    var obj = _mapper.Map<TubularAxialStiffness>(item.AxialStiffness);
                    _db.TubularAxialStiffness.Add(obj);
                }
            }
        }

        private void TorsionalStiffness(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.TorsionalStiffness != null && item.TorsionalStiffness.Uom != null)
                {
                    var obj = _mapper.Map<TubularTorsionalStiffness>(item.TorsionalStiffness);
                    _db.TubularTorsionalStiffness.Add(obj);
                }
            }
        }

        private void DoglegMx(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.DoglegMx != null && item.DoglegMx.Uom != null)
                {
                    var obj = _mapper.Map<TubularDoglegMx>(item.DoglegMx);
                    _db.TubularDoglegMx.Add(obj);
                }
            }
        }

        private void NameTag(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.NameTag != null && item.NameTag.Uid != null)
                {
                    var obj = _mapper.Map<TubularNameTag>(item.NameTag);
                    _db.TubularNameTag.Add(obj);
                }
            }
        }

        private void DiaBit(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.BitRecord!=null && item.BitRecord.DiaBit.Uom != null)
                {
                    var obj = _mapper.Map<TubularDiaBit>(item.BitRecord.DiaBit);
                    _db.TubularDiaBits.Add(obj);
                }
            }
        }
        private void DiaPassThru(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.BitRecord!=null && item.BitRecord.DiaPassThru != null)
                {
                    var obj = _mapper.Map<TubularDiaPassThru>(item.BitRecord.DiaPassThru);
                    _db.TubularDiaPassThrus.Add(obj);
                }
            }
        }

        private void DiaPilot(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.BitRecord!=null && item.BitRecord.DiaPilot!=null && item.BitRecord.DiaPilot.Uom != null)
                {
                    var obj = _mapper.Map<TubularDiaPilot>(item.BitRecord.DiaPilot);
                    _db.TubularDiaPilot.Add(obj);
                }
            }
        }

        private void Cost(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.BitRecord!=null && item.BitRecord.Cost!=null && item.BitRecord.Cost.Currency != null)
                {
                    var obj = _mapper.Map<TubularCost>(item.BitRecord.Cost);
                    _db.TubularCosts.Add(obj);
                }
            }
        }
        

        private void BitRecord(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.BitRecord!=null && item.BitRecord.Uid != null)
                {
                    var obj = _mapper.Map<TubularBitRecord>(item.BitRecord);
                    _db.TubularBitRecord.Add(obj);
                }
            }
        }

        private void AreaNozzleFlow(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.AreaNozzleFlow!=null && item.AreaNozzleFlow.Uom != null)
                {
                    var obj = _mapper.Map<TubularAreaNozzleFlow>(item.AreaNozzleFlow);
                    _db.TubularAreaNozzleFlow.Add(obj);
                }
            }
        }

        private void DiaNozzle(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                foreach (var subItem in item.Nozzle)
                {
                    if (subItem.DiaNozzle != null &&  subItem.DiaNozzle.Uom != null)
                    {
                        var obj = _mapper.Map<TubularDiaNozzle>(subItem.DiaNozzle);
                        _db.TubularDiaNozzle.Add(obj);
                    }
                    if (item.Motor!=null && item.Motor.DiaNozzle !=null && item.Motor.DiaNozzle.Uom != null)
                    {
                        var obj = _mapper.Map<TubularDiaNozzle>(item.Motor.DiaNozzle);
                        _db.TubularDiaNozzle.Add(obj);
                    }
                }
            }
        }

        private void Nozzle(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                foreach (var subItem in item.Nozzle)
                {
                    if (subItem!=null && subItem.Uid != null)
                    {
                        var obj = _mapper.Map<TubularNozzle>(subItem);
                        _db.TubularNozzle.Add(obj);
                    }
                }
            }
        }

        private void HoleOpener(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.HoleOpener!=null && item.HoleOpener.TypeHoleOpener != null)
                {
                    var obj = _mapper.Map<TubularHoleOpener>(item.HoleOpener);
                    _db.TubularHoleOpener.Add(obj);
                }
            }
        }

        private void LenBlade(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Stabilizer != null &&  item.Stabilizer.LenBlade !=null && item.Stabilizer.LenBlade.Uom != null)
                {
                    var obj = _mapper.Map<TubularLenBlade>(item.Stabilizer.LenBlade);
                    _db.TubularLenBlade.Add(obj);
                }
            }
        }

        private void OdBladeMx(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Stabilizer!=null && item.Stabilizer.OdBladeMx!=null && item.Stabilizer.OdBladeMx.Uom != null)
                {
                    var obj = _mapper.Map<TubularOdBladeMx>(item.Stabilizer.OdBladeMx);
                    _db.TubularOdBladeMx.Add(obj);
                }
            }
        }
        private void OdBladeMn(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Stabilizer!=null && item.Stabilizer.OdBladeMn!=null && item.Stabilizer.OdBladeMn.Uom != null)
                {
                    var obj = _mapper.Map<TubularOdBladeMn>(item.Stabilizer.OdBladeMn);
                    _db.TubularOdBladeMn.Add(obj);
                }
            }
        }
        private void DistBladeBot(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Stabilizer !=null && item.Stabilizer.DistBladeBot != null && item.Stabilizer.DistBladeBot.Uom != null)
                {
                    var obj = _mapper.Map<TubularDistBladeBot>(item.Stabilizer.DistBladeBot);
                    _db.TubularDistBladeBot.Add(obj);
                }
            }
        }

        private void Stabilizer(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Stabilizer!=null && item.Stabilizer.Uid != null)
                {
                    var obj = _mapper.Map<TubularStabilizer>(item.Stabilizer);
                    _db.TubularStabilizer.Add(obj);
                }
            }
        }

        private void OffsetTool(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Motor != null && item.Motor.OffsetTool!=null && item.Motor.OffsetTool.Uom != null)
                {
                    var obj = _mapper.Map<TubularOffsetTool>(item.Motor.OffsetTool);
                    _db.TubularOffsetTool.Add(obj);
                }
            }
        }
        private void FlowrateMn(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Motor!=null && item.Motor.FlowrateMn!=null && item.Motor.FlowrateMn.Uom != null)
                {
                    var obj = _mapper.Map<TubularFlowrateMn>(item.Motor.FlowrateMn);
                    _db.TubularFlowrateMn.Add(obj);
                }
            }
        }
        private void FlowrateMx(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Motor != null && item.Motor.FlowrateMx != null &&  item.Motor.FlowrateMx.Uom != null)
                {
                    var obj = _mapper.Map<TubularFlowrateMx>(item.Motor.FlowrateMx);
                    _db.TubularFlowrateMx.Add(obj);
                }
            }
        }
        private void DiaRotorNozzle(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Motor != null && item.Motor.DiaRotorNozzle != null && item.Motor.DiaRotorNozzle.Uom != null)
                {
                    var obj = _mapper.Map<TubularDiaRotorNozzle>(item.Motor.DiaRotorNozzle);
                    _db.TubularDiaRotorNozzle.Add(obj);
                }
            }
        }

        private void ClearanceBearBox(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Motor != null && item.Motor.ClearanceBearBox != null &&  item.Motor.ClearanceBearBox.Uom != null)
                {
                    var obj = _mapper.Map<TubularClearanceBearBox>(item.Motor.ClearanceBearBox);
                    _db.TubularClearanceBearBox.Add(obj);
                }
            }
        }
        private void TempOpMx(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Motor != null && item.Motor.TempOpMx != null &&  item.Motor.TempOpMx.Uom != null)
                {
                    var obj = _mapper.Map<TubularTempOpMx>(item.Motor.TempOpMx);
                    _db.TubularTempOpMx.Add(obj);
                }
            }
        }
        private void BendSettingsMn(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Motor != null && item.Motor.BendSettingsMn != null &&  item.Motor.BendSettingsMn.Uom != null)
                {
                    var obj = _mapper.Map<TubularBendSettingsMn>(item.Motor.BendSettingsMn);
                    _db.TubularBendSettingsMn.Add(obj);
                }
            }
        }
        private void BendSettingsMx(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Motor != null && item.Motor.BendSettingsMx != null &&  item.Motor.BendSettingsMx.Uom != null)
                {
                    var obj = _mapper.Map<TubularBendSettingsMx>(item.Motor.BendSettingsMx);
                    _db.TubularBendSettingsMx.Add(obj);
                }
            }
        }

        private void Bend(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Bend !=null && item.Bend.Uid != null)
                {
                    var obj = _mapper.Map<TubularBend>(item.Bend);
                    _db.TubularBend.Add(obj);
                }
            }
        }

        private void TempMx(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.MwdTool != null &&  item.MwdTool.TempMx.Uom != null)
                {
                    var obj = _mapper.Map<TubularTempMx>(item.MwdTool.TempMx);
                    _db.TubularTempMx.Add(obj);
                }
            }
        }

        private void IdEquv(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.MwdTool != null && item.MwdTool.IdEquv!=null &&  item.MwdTool.IdEquv.Uom != null)
                {
                    var obj = _mapper.Map<TubularIdEquv>(item.MwdTool.IdEquv);
                    _db.TubularIdEquv.Add(obj);
                }
            }
        }

        private void OffsetBot(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if(item.MwdTool!=null && item.MwdTool.Sensor!=null)
                { 
                foreach (var subItem in item.MwdTool.Sensor)
                {
                    if (subItem.OffsetBot !=null && subItem.OffsetBot.Uom != null)
                    {
                        var obj = _mapper.Map<TubularOffsetBot>(subItem.OffsetBot);
                        _db.TubularOffsetBot.Add(obj);
                    }
                }
                }
            }
        }

        private void Sensor(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.MwdTool != null && item.MwdTool.Sensor != null)
                {
                    foreach (var subItem in item.MwdTool.Sensor)
                    {
                        if (subItem != null && subItem.Uid != null)
                        {
                            var obj = _mapper.Map<TubularSensor>(subItem);
                            _db.TubularSensor.Add(obj);
                        }
                    }
                }
            }
        }
        private void MwdTool(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item!=null && item.MwdTool != null)
                {
                    var obj = _mapper.Map<TubularMwdTool>(item.MwdTool);
                    _db.TubularMwdTool.Add(obj);
                }
            }
        }

        private void SizeThread(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Connection != null && item.Connection.SizeThread.Uom != null)
                {
                    var obj = _mapper.Map<TubularSizeThread>(item.Connection.SizeThread);
                    _db.TubularSizeThread.Add(obj);
                }
            }
        }

        private void Connection(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Connection != null &&  item.Connection.Uid != null)
                {
                    var obj = _mapper.Map<TubularConnection>(item.Connection);
                    _db.TubularConnection.Add(obj);
                }
            }
        }
        private void ForUpSet(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Jar !=null && item.Jar.ForUpSet!=null  && item.Jar.ForUpSet.Uom != null)
                {
                    var obj = _mapper.Map<TubularForUpSet>(item.Jar.ForUpSet);
                    _db.TubularForUpSet.Add(obj);
                }
            }
        }
        private void ForDownSet(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Jar != null && item.Jar.ForDownSet != null &&  item.Jar.ForDownSet.Uom != null)
                {
                    var obj = _mapper.Map<TubularForDownSet>(item.Jar.ForDownSet);
                    _db.TubularForDownSet.Add(obj);
                }
            }
        }
        private void ForUpTrip(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Jar != null && item.Jar.ForUpTrip != null &&  item.Jar.ForUpTrip.Uom != null)
                {
                    var obj = _mapper.Map<TubularForUpTrip>(item.Jar.ForUpTrip);
                    _db.TubularForUpTrip.Add(obj);
                }
            }
        }
        private void ForDownTrip(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Jar != null && item.Jar.ForDownTrip != null &&  item.Jar.ForDownTrip.Uom != null)
                {
                    var obj = _mapper.Map<TubularForDownTrip>(item.Jar.ForDownTrip);
                    _db.TubularForDownTrip.Add(obj);
                }
            }
        }
        private void ForPmpOpen(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Jar != null && item.Jar.ForPmpOpen != null &&  item.Jar.ForPmpOpen.Uom != null)
                {
                    var obj = _mapper.Map<TubularForPmpOpen>(item.Jar.ForPmpOpen);
                    _db.TubularForPmpOpen.Add(obj);
                }
            }
        }
        private void ForSealFric(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Jar != null && item.Jar.ForSealFric != null &&  item.Jar.ForSealFric.Uom != null)
                {
                    var obj = _mapper.Map<TubularForSealFric>(item.Jar.ForSealFric);
                    _db.TubularForSealFric.Add(obj);
                }
            }
        }

        private void CommonData(Tubular tubular)
        {
            var obj = _mapper.Map<TubularCommonData>(tubular.CommonData);
            _db.TubularCommonDatas.Add(obj);
        }
        private void Jar(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Jar != null &&  item.Jar.JarId != 0)
                {
                    var obj = _mapper.Map<TubularJar>(item.Jar);
                    _db.TubularJar.Add(obj);
                }
            }
        }

        private void DiaHoleOpener(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.HoleOpener != null && item.HoleOpener.DiaHoleOpener !=null && item.HoleOpener.DiaHoleOpener.Uom != null)
                {
                    var obj = _mapper.Map<TubularDiaHoleOpener>(item.HoleOpener.DiaHoleOpener);
                    _db.TubularDiaHoleOpener.Add(obj);
                }
            }
        }

        private void Angle(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Bend != null && item.Bend.Angle != null &&  item.Bend.Angle.Uom != null)
                {
                    var obj = _mapper.Map<TubularAngle>(item.Bend.Angle);
                    _db.TubularAngle.Add(obj);
                }
            }
        }

        private void DistBendBot(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Bend != null && item.Bend.DistBendBot != null &&  item.Bend.DistBendBot.Uom != null)
                {
                    var obj = _mapper.Map<TubularDistBendBot>(item.Bend.DistBendBot);
                    _db.TubularDistBendBot.Add(obj);
                }
            }
        }

        private void Motor(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Motor != null)
                {
                    var obj = _mapper.Map<TubularMotor>(item.Motor);
                    _db.TubularMotor.Add(obj);
                }
            }
        }

        private void TubularComponent(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item != null && item.Uid != null)
                {
                    var obj = _mapper.Map<TubularComponent>(item);
                    _db.TubularComponent.Add(obj);
                }
            }
        }


        #endregion

        #region Update Tubular
        private void UpdateDiaHoleAssy(Tubular tubular)
        {
            if (tubular.DiaHoleAssy.Uom != null)
            {
                var obj = _mapper.Map<TubularDiaHoleAssy>(tubular.DiaHoleAssy);
                _db.TubularDiaHoleAssy.Update(obj);
            }
        }

        private void UpdateId(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Id != null && item.Id.Uom != null)
                {
                    var obj = _mapper.Map<TubularId>(item.Id);
                    _db.TubularId.Update(obj);
                }

                if (item.Connection != null && item.Connection.Id.Uom != null)
                {
                    var obj = _mapper.Map<TubularId>(item.Connection.Id);
                    _db.TubularId.Update(obj);
                }
            }
        }

        private void UpdateOd(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Od != null && item.Od.Uom != null)
                {
                    var obj = _mapper.Map<TubularOd>(item.Od);
                    _db.TubularOd.Update(obj);
                }
                if (item.Connection != null && item.Connection.Od.Uom != null)
                {
                    var obj = _mapper.Map<TubularOd>(item.Connection.Od);
                    _db.TubularOd.Update(obj);
                }
            }
        }

        private void UpdateLen(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Len != null && item.Len.Uom != null)
                {
                    var obj = _mapper.Map<TubularLen>(item.Len);
                    _db.TubularLen.Update(obj);
                }

                if (item.Connection != null && item.Connection.Len.Uom != null)
                {
                    var obj = _mapper.Map<TubularLen>(item.Connection.Len);
                    _db.TubularLen.Update(obj);
                }
            }
        }


        private void UpdateLenJointAv(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.LenJointAv != null && item.LenJointAv.Uom != null)
                {
                    var obj = _mapper.Map<TubularLenJointAv>(item.LenJointAv);
                    _db.TubularLenJointAv.Update(obj);
                }
            }
        }

        private void UpdateWtPerLen(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.WtPerLen != null && item.WtPerLen.Uom != null)
                {
                    var obj = _mapper.Map<TubularWtPerLen>(item.WtPerLen);
                    _db.TubularWtPerLen.Update(obj);
                }
            }
        }

        private void UpdateOdDrift(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.OdDrift != null && item.OdDrift.Uom != null)
                {
                    var obj = _mapper.Map<TubularOdDrift>(item.OdDrift);
                    _db.TubularOdDrifts.Update(obj);
                }
            }
        }

        private void UpdateTensYield(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.TensYield != null && item.TensYield.Uom != null)
                {
                    var obj = _mapper.Map<TubularTensYield>(item.TensYield);
                    _db.TubularTensYield.Update(obj);
                }
                if (item.Connection != null && item.Connection.TensYield.Uom != null)
                {
                    var obj = _mapper.Map<TubularTensYield>(item.Connection.TensYield);
                    _db.TubularTensYield.Update(obj);
                }
            }
        }

        private void UpdateTqYield(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.TqYield != null && item.TqYield.Uom != null)
                {
                    var obj = _mapper.Map<TubularTqYield>(item.TqYield);
                    _db.TubularTqYield.Update(obj);
                }
                if (item.Connection != null && item.Connection.TqYield.Uom != null)
                {
                    var obj = _mapper.Map<TubularTqYield>(item.Connection.TqYield);
                    _db.TubularTqYield.Update(obj);
                }
            }
        }

        private void UpdateCriticalCrossSection(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {

                if (item.Connection != null && item.Connection.CriticalCrossSection.Uom != null)
                {
                    var obj = _mapper.Map<TubularCriticalCrossSection>(item.Connection.CriticalCrossSection);
                    _db.TubularCriticalCrossSection.Update(obj);
                }
            }
        }
        private void UpdatePresLeak(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {

                if (item.Connection != null && item.Connection.PresLeak.Uom != null)
                {
                    var obj = _mapper.Map<TubularPresLeak>(item.Connection.PresLeak);
                    _db.TubularPresLeak.Update(obj);
                }
            }
        }

        private void UpdateTqMakeup(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {

                if (item.Connection != null && item.Connection.TqMakeup.Uom != null)
                {
                    var obj = _mapper.Map<TubularTqMakeup>(item.Connection.TqMakeup);
                    _db.TubularTqMakeup.Update(obj);
                }
            }
        }
        private void UpdateStressFatig(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.StressFatig != null && item.StressFatig.Uom != null)
                {
                    var obj = _mapper.Map<TubularStressFatig>(item.StressFatig);
                    _db.TubularStressFatig.Update(obj);
                }
            }
        }

        private void UpdateLenFishneck(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.LenFishneck != null && item.LenFishneck.Uom != null)
                {
                    var obj = _mapper.Map<TubularLenFishneck>(item.LenFishneck);
                    _db.TubularLenFishneck.Update(obj);
                }
            }
        }

        private void UpdateIdFishneck(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.IdFishneck != null && item.IdFishneck.Uom != null)
                {
                    var obj = _mapper.Map<TubularIdFishneck>(item.IdFishneck);
                    _db.TubularIdFishneck.Update(obj);
                }
            }
        }

        private void UpdateOdFishneck(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.OdFishneck != null && item.OdFishneck.Uom != null)
                {
                    var obj = _mapper.Map<TubularOdFishneck>(item.OdFishneck);
                    _db.TubularOdFishneck.Update(obj);
                }
            }
        }
        private void UpdateDisp(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Disp != null && item.Disp.Uom != null)
                {
                    var obj = _mapper.Map<TubularDisp>(item.Disp);
                    _db.TubularDisp.Update(obj);
                }
            }
        }
        private void UpdatePresBurst(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.PresBurst != null && item.PresBurst.Uom != null)
                {
                    var obj = _mapper.Map<TubularPresBurst>(item.PresBurst);
                    _db.TubularPresBurst.Update(obj);
                }
            }
        }

        private void UpdatePresCollapse(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.PresCollapse != null && item.PresCollapse.Uom != null)
                {
                    var obj = _mapper.Map<TubularPresCollapse>(item.PresCollapse);
                    _db.TubularPresCollapse.Update(obj);
                }
            }
        }

        private void UpdateWearWall(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.WearWall != null && item.WearWall.Uom != null)
                {
                    var obj = _mapper.Map<TubularWearWall>(item.WearWall);
                    _db.TubularWearWall.Update(obj);
                }
            }
        }

        private void UpdateThickWall(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.ThickWall != null && item.ThickWall.Uom != null)
                {
                    var obj = _mapper.Map<TubularThickWall>(item.ThickWall);
                    _db.TubularThickWall.Update(obj);
                }
            }
        }

        private void UpdateBendStiffness(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.BendStiffness != null && item.BendStiffness.Uom != null)
                {
                    var obj = _mapper.Map<TubularBendStiffness>(item.BendStiffness);
                    _db.TubularBendStiffness.Update(obj);
                }
            }
        }

        private void UpdateAxialStiffness(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.AxialStiffness != null && item.AxialStiffness.Uom != null)
                {
                    var obj = _mapper.Map<TubularAxialStiffness>(item.AxialStiffness);
                    _db.TubularAxialStiffness.Update(obj);
                }
            }
        }

        private void UpdateTorsionalStiffness(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.TorsionalStiffness != null && item.TorsionalStiffness.Uom != null)
                {
                    var obj = _mapper.Map<TubularTorsionalStiffness>(item.TorsionalStiffness);
                    _db.TubularTorsionalStiffness.Update(obj);
                }
            }
        }

        private void UpdateDoglegMx(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.DoglegMx != null && item.DoglegMx.Uom != null)
                {
                    var obj = _mapper.Map<TubularDoglegMx>(item.DoglegMx);
                    _db.TubularDoglegMx.Update(obj);
                }
            }
        }

        private void UpdateNameTag(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.NameTag != null && item.NameTag.Uid != null)
                {
                    var obj = _mapper.Map<TubularNameTag>(item.NameTag);
                    _db.TubularNameTag.Update(obj);
                }
            }
        }

        private void UpdateDiaBit(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.BitRecord != null && item.BitRecord.DiaBit.Uom != null)
                {
                    var obj = _mapper.Map<TubularDiaBit>(item.BitRecord.DiaBit);
                    _db.TubularDiaBits.Update(obj);
                }
            }
        }
        private void UpdateDiaPassThru(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.BitRecord != null && item.BitRecord.DiaPassThru != null)
                {
                    var obj = _mapper.Map<TubularDiaPassThru>(item.BitRecord.DiaPassThru);
                    _db.TubularDiaPassThrus.Update(obj);
                }
            }
        }

        private void UpdateDiaPilot(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.BitRecord != null && item.BitRecord.DiaPilot != null && item.BitRecord.DiaPilot.Uom != null)
                {
                    var obj = _mapper.Map<TubularDiaPilot>(item.BitRecord.DiaPilot);
                    _db.TubularDiaPilot.Update(obj);
                }
            }
        }

        private void UpdateCost(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.BitRecord != null && item.BitRecord.Cost != null && item.BitRecord.Cost.Currency != null)
                {
                    var obj = _mapper.Map<TubularCost>(item.BitRecord.Cost);
                    _db.TubularCosts.Update(obj);
                }
            }
        }
        
        private void UpdateBitRecord(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.BitRecord != null && item.BitRecord.Uid != null)
                {
                    var obj = _mapper.Map<TubularBitRecord>(item.BitRecord);
                    _db.TubularBitRecord.Update(obj);
                }
            }
        }

        private void UpdateAreaNozzleFlow(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.AreaNozzleFlow != null && item.AreaNozzleFlow.Uom != null)
                {
                    var obj = _mapper.Map<TubularAreaNozzleFlow>(item.AreaNozzleFlow);
                    _db.TubularAreaNozzleFlow.Update(obj);
                }
            }
        }

        private void UpdateDiaNozzle(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                foreach (var subItem in item.Nozzle)
                {
                    if (subItem.DiaNozzle != null && subItem.DiaNozzle.Uom != null)
                    {
                        var obj = _mapper.Map<TubularDiaNozzle>(subItem.DiaNozzle);
                        _db.TubularDiaNozzle.Update(obj);
                    }
                    if (item.Motor != null && item.Motor.DiaNozzle != null && item.Motor.DiaNozzle.Uom != null)
                    {
                        var obj = _mapper.Map<TubularDiaNozzle>(item.Motor.DiaNozzle);
                        _db.TubularDiaNozzle.Update(obj);
                    }
                }
            }
        }

        private void UpdateNozzle(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                foreach (var subItem in item.Nozzle)
                {
                    if (subItem != null && subItem.Uid != null)
                    {
                        var obj = _mapper.Map<TubularNozzle>(subItem);
                        _db.TubularNozzle.Update(obj);
                    }
                }
            }
        }

        private void UpdateHoleOpener(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.HoleOpener != null && item.HoleOpener.TypeHoleOpener != null)
                {
                    var obj = _mapper.Map<TubularHoleOpener>(item.HoleOpener);
                    _db.TubularHoleOpener.Update(obj);
                }
            }
        }

        private void UpdateLenBlade(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Stabilizer != null && item.Stabilizer.LenBlade != null && item.Stabilizer.LenBlade.Uom != null)
                {
                    var obj = _mapper.Map<TubularLenBlade>(item.Stabilizer.LenBlade);
                    _db.TubularLenBlade.Update(obj);
                }
            }
        }

        private void UpdateOdBladeMx(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Stabilizer != null && item.Stabilizer.OdBladeMx != null && item.Stabilizer.OdBladeMx.Uom != null)
                {
                    var obj = _mapper.Map<TubularOdBladeMx>(item.Stabilizer.OdBladeMx);
                    _db.TubularOdBladeMx.Update(obj);
                }
            }
        }
        private void UpdateOdBladeMn(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Stabilizer != null && item.Stabilizer.OdBladeMn != null && item.Stabilizer.OdBladeMn.Uom != null)
                {
                    var obj = _mapper.Map<TubularOdBladeMn>(item.Stabilizer.OdBladeMn);
                    _db.TubularOdBladeMn.Update(obj);
                }
            }
        }
        private void UpdateDistBladeBot(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Stabilizer != null && item.Stabilizer.DistBladeBot != null && item.Stabilizer.DistBladeBot.Uom != null)
                {
                    var obj = _mapper.Map<TubularDistBladeBot>(item.Stabilizer.DistBladeBot);
                    _db.TubularDistBladeBot.Update(obj);
                }
            }
        }

        private void UpdateStabilizer(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Stabilizer != null && item.Stabilizer.Uid != null)
                {
                    var obj = _mapper.Map<TubularStabilizer>(item.Stabilizer);
                    _db.TubularStabilizer.Update(obj);
                }
            }
        }

        private void UpdateOffsetTool(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Motor != null && item.Motor.OffsetTool != null && item.Motor.OffsetTool.Uom != null)
                {
                    var obj = _mapper.Map<TubularOffsetTool>(item.Motor.OffsetTool);
                    _db.TubularOffsetTool.Update(obj);
                }
            }
        }
        private void UpdateFlowrateMn(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Motor != null && item.Motor.FlowrateMn != null && item.Motor.FlowrateMn.Uom != null)
                {
                    var obj = _mapper.Map<TubularFlowrateMn>(item.Motor.FlowrateMn);
                    _db.TubularFlowrateMn.Update(obj);
                }
            }
        }
        private void UpdateFlowrateMx(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Motor != null && item.Motor.FlowrateMx != null && item.Motor.FlowrateMx.Uom != null)
                {
                    var obj = _mapper.Map<TubularFlowrateMx>(item.Motor.FlowrateMx);
                    _db.TubularFlowrateMx.Update(obj);
                }
            }
        }
        private void UpdateDiaRotorNozzle(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Motor != null && item.Motor.DiaRotorNozzle != null && item.Motor.DiaRotorNozzle.Uom != null)
                {
                    var obj = _mapper.Map<TubularDiaRotorNozzle>(item.Motor.DiaRotorNozzle);
                    _db.TubularDiaRotorNozzle.Update(obj);
                }
            }
        }

        private void UpdateClearanceBearBox(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Motor != null && item.Motor.ClearanceBearBox != null && item.Motor.ClearanceBearBox.Uom != null)
                {
                    var obj = _mapper.Map<TubularClearanceBearBox>(item.Motor.ClearanceBearBox);
                    _db.TubularClearanceBearBox.Update(obj);
                }
            }
        }
        private void UpdateTempOpMx(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Motor != null && item.Motor.TempOpMx != null && item.Motor.TempOpMx.Uom != null)
                {
                    var obj = _mapper.Map<TubularTempOpMx>(item.Motor.TempOpMx);
                    _db.TubularTempOpMx.Update(obj);
                }
            }
        }
        private void UpdateBendSettingsMn(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Motor != null && item.Motor.BendSettingsMn != null && item.Motor.BendSettingsMn.Uom != null)
                {
                    var obj = _mapper.Map<TubularBendSettingsMn>(item.Motor.BendSettingsMn);
                    _db.TubularBendSettingsMn.Update(obj);
                }
            }
        }
        private void UpdateBendSettingsMx(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Motor != null && item.Motor.BendSettingsMx != null && item.Motor.BendSettingsMx.Uom != null)
                {
                    var obj = _mapper.Map<TubularBendSettingsMx>(item.Motor.BendSettingsMx);
                    _db.TubularBendSettingsMx.Update(obj);
                }
            }
        }

        private void UpdateBend(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Bend != null && item.Bend.Uid != null)
                {
                    var obj = _mapper.Map<TubularBend>(item.Bend);
                    _db.TubularBend.Update(obj);
                }
            }
        }

        private void UpdateTempMx(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.MwdTool != null && item.MwdTool.TempMx.Uom != null)
                {
                    var obj = _mapper.Map<TubularTempMx>(item.MwdTool.TempMx);
                    _db.TubularTempMx.Update(obj);
                }
            }
        }

        private void UpdateIdEquv(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.MwdTool != null && item.MwdTool.IdEquv != null && item.MwdTool.IdEquv.Uom != null)
                {
                    var obj = _mapper.Map<TubularIdEquv>(item.MwdTool.IdEquv);
                    _db.TubularIdEquv.Update(obj);
                }
            }
        }

        private void UpdateOffsetBot(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.MwdTool != null && item.MwdTool.Sensor != null)
                {
                    foreach (var subItem in item.MwdTool.Sensor)
                    {
                        if (subItem.OffsetBot != null && subItem.OffsetBot.Uom != null)
                        {
                            var obj = _mapper.Map<TubularOffsetBot>(subItem.OffsetBot);
                            _db.TubularOffsetBot.Update(obj);
                        }
                    }
                }
            }
        }

        private void UpdateSensor(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.MwdTool != null && item.MwdTool.Sensor != null)
                {
                    foreach (var subItem in item.MwdTool.Sensor)
                    {
                        if (subItem != null && subItem.Uid != null)
                        {
                            var obj = _mapper.Map<TubularSensor>(subItem);
                            _db.TubularSensor.Update(obj);
                        }
                    }
                }
            }
        }
        private void UpdateMwdTool(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item != null && item.MwdTool != null)
                {
                    var obj = _mapper.Map<TubularMwdTool>(item.MwdTool);
                    _db.TubularMwdTool.Update(obj);
                }
            }
        }

        private void UpdateSizeThread(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Connection != null && item.Connection.SizeThread.Uom != null)
                {
                    var obj = _mapper.Map<TubularSizeThread>(item.Connection.SizeThread);
                    _db.TubularSizeThread.Update(obj);
                }
            }
        }

        private void UpdateConnection(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Connection != null && item.Connection.Uid != null)
                {
                    var obj = _mapper.Map<TubularConnection>(item.MwdTool);
                    _db.TubularConnection.Update(obj);
                }
            }
        }
        private void UpdateForUpSet(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Jar != null && item.Jar.ForUpSet != null && item.Jar.ForUpSet.Uom != null)
                {
                    var obj = _mapper.Map<TubularForUpSet>(item.Jar.ForUpSet.Uom);
                    _db.TubularForUpSet.Update(obj);
                }
            }
        }
        private void UpdateForDownSet(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Jar != null && item.Jar.ForDownSet != null && item.Jar.ForDownSet.Uom != null)
                {
                    var obj = _mapper.Map<TubularForDownSet>(item.Jar.ForDownSet.Uom);
                    _db.TubularForDownSet.Update(obj);
                }
            }
        }
        private void UpdateForUpTrip(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Jar != null && item.Jar.ForUpTrip != null && item.Jar.ForUpTrip.Uom != null)
                {
                    var obj = _mapper.Map<TubularForUpTrip>(item.Jar.ForUpTrip);
                    _db.TubularForUpTrip.Update(obj);
                }
            }
        }
        private void UpdateForDownTrip(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Jar != null && item.Jar.ForDownTrip != null && item.Jar.ForDownTrip.Uom != null)
                {
                    var obj = _mapper.Map<TubularForDownTrip>(item.Jar.ForDownTrip);
                    _db.TubularForDownTrip.Update(obj);
                }
            }
        }
        private void UpdateForPmpOpen(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Jar != null && item.Jar.ForPmpOpen != null && item.Jar.ForPmpOpen.Uom != null)
                {
                    var obj = _mapper.Map<TubularForPmpOpen>(item.Jar.ForPmpOpen);
                    _db.TubularForPmpOpen.Update(obj);
                }
            }
        }
        private void UpdateForSealFric(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Jar != null && item.Jar.ForSealFric != null && item.Jar.ForSealFric.Uom != null)
                {
                    var obj = _mapper.Map<TubularForSealFric>(item.Jar.ForSealFric);
                    _db.TubularForSealFric.Update(obj);
                }
            }
        }

        private void UpdateCommonData(Tubular tubular)
        {
            var obj = _mapper.Map<TubularCommonData>(tubular.CommonData);
            _db.TubularCommonDatas.Update(obj);
        }
        private void UpdateJar(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Jar != null && item.Jar.JarId != 0)
                {
                    var obj = _mapper.Map<TubularJar>(item.Jar);
                    _db.TubularJar.Update(obj);
                }
            }
        }

        private void UpdateDiaHoleOpener(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.HoleOpener != null && item.HoleOpener.DiaHoleOpener != null && item.HoleOpener.DiaHoleOpener.Uom != null)
                {
                    var obj = _mapper.Map<TubularDiaHoleOpener>(item.HoleOpener.DiaHoleOpener);
                    _db.TubularDiaHoleOpener.Update(obj);
                }
            }
        }

        private void UpdateAngle(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Bend != null && item.Bend.Angle != null && item.Bend.Angle.Uom != null)
                {
                    var obj = _mapper.Map<TubularAngle>(item.Bend.Angle);
                    _db.TubularAngle.Update(obj);
                }
            }
        }

        private void UpdateDistBendBot(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Bend != null && item.Bend.DistBendBot != null && item.Bend.DistBendBot.Uom != null)
                {
                    var obj = _mapper.Map<TubularDistBendBot>(item.Bend.DistBendBot);
                    _db.TubularDistBendBot.Update(obj);
                }
            }
        }

        private void UpdateMotor(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item.Motor != null)
                {
                    var obj = _mapper.Map<TubularMotor>(item.Motor);
                    _db.TubularMotor.Update(obj);
                }
            }
        }

        private void UpdateTubularComponent(Tubular tubular)
        {
            foreach (var item in tubular.TubularComponent)
            {
                if (item != null && item.Uid != null)
                {
                    var obj = _mapper.Map<TubularComponent>(item);
                    _db.TubularComponent.Update(obj);
                }
            }
        }

        #endregion
    }
}
