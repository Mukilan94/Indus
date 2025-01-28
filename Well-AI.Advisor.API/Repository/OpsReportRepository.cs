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
	public class OpsReportRepository : IOpsReportRepository
	{
		private readonly WellAIAdvisiorContext _db;
		private readonly WebAIAdvisorContext _wdb;
		private readonly IMapper _mapper;
		private ITenantRepository _tenantRepository;

		public OpsReportRepository(WellAIAdvisiorContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper, ITenantRepository tenantRepository, WebAIAdvisorContext wdb)
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
		public bool OpsReportExists(string uid)
		{
			bool value = _db.OpsReports.Any(x => x.Uid.ToLower().Trim() == uid.ToLower().Trim());
			return value;
		}

		public bool CreateOpsReport(OpsReport opsReport)
		{
			try
			{
				Rig(opsReport);
				ETimStart(opsReport);
				ETimSpud(opsReport);
				ETimLoc(opsReport);
				MdReport(opsReport);
				TvdReport(opsReport);
				DistDrill(opsReport);
				ETimDrill(opsReport);
				MdPlanned(opsReport);
				RopAv(opsReport);
				RopCurrent(opsReport);
				ETimDrillRot(opsReport);
				ETimDrillSlid(opsReport);
				ETimCirc(opsReport);
				ETimReam(opsReport);
				ETimHold(opsReport);
				ETimSteering(opsReport);
				DistDrillRot(opsReport);
				DistDrillSlid(opsReport);
				DistReam(opsReport);
				DistHold(opsReport);
				DistSteering(opsReport);
				Duration(opsReport);
				MdHoleStart(opsReport);
				TvdHoleStart(opsReport);
				MdHoleEnd(opsReport);
				TvdHoleEnd(opsReport);
				MdBitStart(opsReport);
				MdBitEnd(opsReport);
				Activity(opsReport);
				ETimOpBit(opsReport);
				MdHoleStop(opsReport);
				Tubular(opsReport);
				HkldRot(opsReport);
				OverPull(opsReport);
				SlackOff(opsReport);
				HkldUp(opsReport);
				HkldDn(opsReport);
				TqOnBotAv(opsReport);
				TqOnBotMx(opsReport);
				TqOnBotMn(opsReport);
				TqOffBotAv(opsReport);
				TqDhAv(opsReport);
				WtAboveJar(opsReport);
				WtBelowJar(opsReport);
				WtMud(opsReport);
				FlowratePump(opsReport);
				PowBit(opsReport);
				VelNozzleAv(opsReport);
				PresDropBit(opsReport);
				CTimHold(opsReport);
				CTimSteering(opsReport);
				CTimDrillRot(opsReport);
				CTimDrillSlid(opsReport);
				CTimCirc(opsReport);
				CTimReam(opsReport);
				RpmAv(opsReport);
				RpmMx(opsReport);
				RpmMn(opsReport);
				RpmAvDh(opsReport);
				RopMx(opsReport);
				RopMn(opsReport);
				WobAv(opsReport);
				WobMx(opsReport);
				WobMn(opsReport);
				WobAvDh(opsReport);
				AziTop(opsReport);
				AziBottom(opsReport);
				InclStart(opsReport);
				InclMx(opsReport);
				InclMn(opsReport);
				InclStop(opsReport);
				TempMudDhMx(opsReport);
				PresPumpAv(opsReport);
				FlowrateBit(opsReport);
				DrillingParams(opsReport);
				CostPerItem(opsReport);
				CostAmount(opsReport);
				DayCost(opsReport);
				Md(opsReport);
				Tvd(opsReport);
				Incl(opsReport);
				Azi(opsReport);
				Mtf(opsReport);
				Gtf(opsReport);
				DispNs(opsReport);
				DispEw(opsReport);
				VertSect(opsReport);
				Dls(opsReport);
				RateTurn(opsReport);
				RateBuild(opsReport);
				MdDelta(opsReport);
				TvdDelta(opsReport);
				GravTotalUncert(opsReport);
				DipAngleUncert(opsReport);
				MagTotalUncert(opsReport);
				GravAxialRaw(opsReport);
				GravTran1Raw(opsReport);
				GravTran2Raw(opsReport);
				MagAxialRaw(opsReport);
				MagTran1Raw(opsReport);
				MagTran2Raw(opsReport);
				RawData(opsReport);
				GravAxialAccelCor(opsReport);
				GravTran1AccelCor(opsReport);
				GravTran2AccelCor(opsReport);
				MagAxialDrlstrCor(opsReport);
				MagTran1DrlstrCor(opsReport);
				MagTran2DrlstrCor(opsReport);
				SagIncCor(opsReport);
				SagAziCor(opsReport);
				StnMagDeclUsed(opsReport);
				CorUsed(opsReport);
				MagTotalFieldCalc(opsReport);
				MagDipAngleCalc(opsReport);
				GravTotalFieldCalc(opsReport);
				Valid(opsReport);
				VarianceNN(opsReport);
				VarianceNE(opsReport);
				VarianceNVert(opsReport);
				VarianceEE(opsReport);
				VarianceEVert(opsReport);
				VarianceVertVert(opsReport);
				BiasN(opsReport);
				BiasE(opsReport);
				BiasVert(opsReport);
				MatrixCov(opsReport);
				WellCRS(opsReport);
				Latitude(opsReport);
				Longitude(opsReport);
				Location(opsReport);
				ProjectedX(opsReport);
				ProjectedY(opsReport);
				TrajectoryStation(opsReport);
				Density(opsReport);
				VisFunnel(opsReport);
				TempVis(opsReport);
				Pv(opsReport);
				Yp(opsReport);
				Gel10Sec(opsReport);
				Gel10Min(opsReport);
				Gel30Min(opsReport);
				FilterCakeLtlp(opsReport);
				FiltrateLtlp(opsReport);
				TempHthp(opsReport);
				PresHthp(opsReport);
				FiltrateHthp(opsReport);
				FilterCakeHthp(opsReport);
				SolidsPc(opsReport);
				WaterPc(opsReport);
				OilPc(opsReport);
				SandPc(opsReport);
				SolidsLowGravPc(opsReport);
				SolidsCalcPc(opsReport);
				BaritePc(opsReport);
				Lcm(opsReport);
				Mbt(opsReport);
				TempPh(opsReport);
				Pm(opsReport);
				PmFiltrate(opsReport);
				Mf(opsReport);
				AlkalinityP1(opsReport);
				AlkalinityP2(opsReport);
				Chloride(opsReport);
				Calcium(opsReport);
				Magnesium(opsReport);
				TempRheom(opsReport);
				PresRheom(opsReport);
				Rheometer(opsReport);
				BrinePc(opsReport);
				Lime(opsReport);
				ElectStab(opsReport);
				CalciumChloride(opsReport);
				SolidsHiGravPc(opsReport);
				Polymer(opsReport);
				SolCorPc(opsReport);
				OilCtg(opsReport);
				HardnessCa(opsReport);
				Sulfide(opsReport);
				Fluid(opsReport);
				Pump(opsReport);
				RateStroke(opsReport);
				PresRecorded(opsReport);
				MdBit(opsReport);
				Scr(opsReport);
				Pit(opsReport);
				VolPit(opsReport);
				DensFluid(opsReport);
				PitVolume(opsReport);
				VolTotMudStart(opsReport);
				VolMudDumped(opsReport);
				VolMudReceived(opsReport);
				VolMudReturned(opsReport);
				VolLostShakerSurf(opsReport);
				VolLostMudCleanerSurf(opsReport);
				VolLostPitsSurf(opsReport);
				VolLostTrippingSurf(opsReport);
				VolLostOtherSurf(opsReport);
				VolTotMudLostSurf(opsReport);
				VolLostCircHole(opsReport);
				VolLostCsgHole(opsReport);
				VolLostCmtHole(opsReport);
				VolLostBhdCsgHole(opsReport);
				VolLostAbandonHole(opsReport);
				VolLostOtherHole(opsReport);
				VolTotMudLostHole(opsReport);
				MudLosses(opsReport);
				VolMudBuilt(opsReport);
				VolMudString(opsReport);
				VolMudCasing(opsReport);
				VolMudHole(opsReport);
				VolMudRiser(opsReport);
				VolTotMudEnd(opsReport);
				MudVolume(opsReport);
				ItemWtPerUnit(opsReport);
				PricePerUnit(opsReport);
				CostItem(opsReport);
				MudInventory(opsReport);
				ItemVolPerUnit(opsReport);
				Bulk(opsReport);
				AnchorTension(opsReport);
				AnchorAngle(opsReport);
				RigHeading(opsReport);
				RigHeave(opsReport);
				RigPitchAngle(opsReport);
				RigRollAngle(opsReport);
				RiserAngle(opsReport);
				RiserDirection(opsReport);
				RiserTension(opsReport);
				VariableDeckLoad(opsReport);
				TotalDeckLoad(opsReport);
				GuideBaseAngle(opsReport);
				BallJointAngle(opsReport);
				BallJointDirection(opsReport);
				OffsetRig(opsReport);
				LoadLeg1(opsReport);
				LoadLeg2(opsReport);
				LoadLeg3(opsReport);
				LoadLeg4(opsReport);
				PenetrationLeg1(opsReport);
				PenetrationLeg2(opsReport);
				PenetrationLeg3(opsReport);
				PenetrationLeg4(opsReport);
				DispRig(opsReport);
				MeanDraft(opsReport);
				RigResponse(opsReport);
				IdLiner(opsReport);
				LenStroke(opsReport);
				Pressure(opsReport);
				PcEfficiency(opsReport);
				PumpOutput(opsReport);
				PumpOp(opsReport);
				Shaker(opsReport);
				MdHole(opsReport);
				HoursRun(opsReport);
				PcScreenCovered(opsReport);
				MeshX(opsReport);
				MeshY(opsReport);
				CutPoint(opsReport);
				MeshY(opsReport);
				ShakerScreen(opsReport);
				ShakerOp(opsReport);
				DaysIncFree(opsReport);
				ETimLostGross(opsReport);
				CostLostGross(opsReport);
				Incident(opsReport);
				PresLastCsg(opsReport);
				PresStdPipe(opsReport);
				PresKellyHose(opsReport);
				PresDiverter(opsReport);
				PresAnnular(opsReport);
				PresRams(opsReport);
				PresChokeLine(opsReport);
				PresChokeMan(opsReport);
				FluidDischarged(opsReport);
				VolCtgDischarged(opsReport);
				VolOilCtgDischarge(opsReport);
				WasteDischarged(opsReport);
				Hse(opsReport);
				TotalTime(opsReport);
				Personnel(opsReport);
				SupportCraft(opsReport);
				BarometricPressure(opsReport);
				TempSurfaceMn(opsReport);
				TempSurfaceMx(opsReport);
				TempWindChill(opsReport);
				Tempsea(opsReport);
				Visibility(opsReport);
				AziWave(opsReport);
				HtWave(opsReport);
				PeriodWave(opsReport);
				AziWind(opsReport);
				VelWind(opsReport);
				AmtPrecip(opsReport);
				CeilingCloud(opsReport);
				CurrentSea(opsReport);
				AziCurrentSea(opsReport);
				Weather(opsReport);
				CostDay(opsReport);
				CostDayMud(opsReport);
				DiaHole(opsReport);
				DiaCsgLast(opsReport);
				MdCsgLast(opsReport);
				TvdCsgLast(opsReport);
				TvdLot(opsReport);
				PresLotEmw(opsReport);
				PresKickTol(opsReport);
				VolKickTol(opsReport);
				Maasp(opsReport);
				CommonData(opsReport);
				_db.OpsReports.Add(opsReport);
				return Save();
			}
			catch(Exception ex)
			{
				CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
				customErrorHandler.WriteError(ex, "OpsReportRepository CreateOpsReport", null);
				return Save();
			}
		}

		public bool DeleteOpsReport(OpsReport opsReport)
		{
			_db.OpsReports.Remove(opsReport);
			return Save();
		}

		public OpsReport GetOpsReportDetail(string Uid)
		{
			return _db.OpsReports.FirstOrDefault(x=>x.Uid==Uid);
			
		}

		public ICollection<OpsReport> GetOpsReportDetails()
		{
			return _db.OpsReports.OrderBy(x => x.NameWell).ToList();
		}

		public bool Save()
		{
			return _db.SaveChanges() >= 0 ? true : false;
		}

		public bool UpdateOpsReport(OpsReport opsReport)
		{
			try
			{
				UpdateRig(opsReport);
				UpdateETimStart(opsReport);
				UpdateETimSpud(opsReport);
				UpdateETimLoc(opsReport);
				UpdateMdReport(opsReport);
				UpdateTvdReport(opsReport);
				UpdateDistDrill(opsReport);
				UpdateETimDrill(opsReport);
				UpdateMdPlanned(opsReport);
				UpdateRopAv(opsReport);
				UpdateRopCurrent(opsReport);
				UpdateETimDrillRot(opsReport);
				UpdateETimDrillSlid(opsReport);
				UpdateETimCirc(opsReport);
				UpdateETimReam(opsReport);
				UpdateETimHold(opsReport);
				UpdateETimSteering(opsReport);
				UpdateDistDrillRot(opsReport);
				UpdateDistDrillSlid(opsReport);
				UpdateDistReam(opsReport);
				UpdateDistHold(opsReport);
				UpdateDistSteering(opsReport);
				UpdateDuration(opsReport);
				UpdateMdHoleStart(opsReport);
				UpdateTvdHoleStart(opsReport);
				UpdateMdHoleEnd(opsReport);
				UpdateTvdHoleEnd(opsReport);
				UpdateMdBitStart(opsReport);
				UpdateMdBitEnd(opsReport);
				UpdateActivity(opsReport);
				UpdateETimOpBit(opsReport);
				UpdateMdHoleStop(opsReport);
				UpdateTubular(opsReport);
				UpdateHkldRot(opsReport);
				UpdateOverPull(opsReport);
				UpdateSlackOff(opsReport);
				UpdateHkldUp(opsReport);
				UpdateHkldDn(opsReport);
				UpdateTqOnBotAv(opsReport);
				UpdateTqOnBotMx(opsReport);
				UpdateTqOnBotMn(opsReport);
				UpdateTqOffBotAv(opsReport);
				UpdateTqDhAv(opsReport);
				UpdateWtAboveJar(opsReport);
				UpdateWtBelowJar(opsReport);
				UpdateWtMud(opsReport);
				UpdateFlowratePump(opsReport);
				UpdatePowBit(opsReport);
				UpdateVelNozzleAv(opsReport);
				UpdatePresDropBit(opsReport);
				UpdateCTimHold(opsReport);
				UpdateCTimSteering(opsReport);
				UpdateCTimDrillRot(opsReport);
				UpdateCTimDrillSlid(opsReport);
				UpdateCTimCirc(opsReport);
				UpdateCTimReam(opsReport);
				UpdateRpmAv(opsReport);
				UpdateRpmMx(opsReport);
				UpdateRpmMn(opsReport);
				UpdateRpmAvDh(opsReport);
				UpdateRopMx(opsReport);
				UpdateRopMn(opsReport);
				UpdateWobAv(opsReport);
				UpdateWobMx(opsReport);
				UpdateWobMn(opsReport);
				UpdateWobAvDh(opsReport);
				UpdateAziTop(opsReport);
				UpdateAziBottom(opsReport);
				UpdateInclStart(opsReport);
				UpdateInclMx(opsReport);
				UpdateInclMn(opsReport);
				UpdateInclStop(opsReport);
				UpdateTempMudDhMx(opsReport);
				UpdatePresPumpAv(opsReport);
				UpdateFlowrateBit(opsReport);
				UpdateDrillingParams(opsReport);
				UpdateCostPerItem(opsReport);
				UpdateCostAmount(opsReport);
				UpdateDayCost(opsReport);
				UpdateMd(opsReport);
				UpdateTvd(opsReport);
				UpdateIncl(opsReport);
				UpdateAzi(opsReport);
				UpdateMtf(opsReport);
				UpdateGtf(opsReport);
				UpdateDispNs(opsReport);
				UpdateDispEw(opsReport);
				UpdateVertSect(opsReport);
				UpdateDls(opsReport);
				UpdateRateTurn(opsReport);
				UpdateRateBuild(opsReport);
				UpdateMdDelta(opsReport);
				UpdateTvdDelta(opsReport);
				UpdateGravTotalUncert(opsReport);
				UpdateDipAngleUncert(opsReport);
				UpdateMagTotalUncert(opsReport);
				UpdateGravAxialRaw(opsReport);
				UpdateGravTran1Raw(opsReport);
				UpdateGravTran2Raw(opsReport);
				UpdateMagAxialRaw(opsReport);
				UpdateMagTran1Raw(opsReport);
				UpdateMagTran2Raw(opsReport);
				UpdateRawData(opsReport);
				UpdateGravAxialAccelCor(opsReport);
				UpdateGravTran1AccelCor(opsReport);
				UpdateGravTran2AccelCor(opsReport);
				UpdateMagAxialDrlstrCor(opsReport);
				UpdateMagTran1DrlstrCor(opsReport);
				UpdateMagTran2DrlstrCor(opsReport);
				UpdateSagIncCor(opsReport);
				UpdateSagAziCor(opsReport);
				UpdateStnMagDeclUsed(opsReport);
				UpdateCorUsed(opsReport);
				UpdateMagTotalFieldCalc(opsReport);
				UpdateMagDipAngleCalc(opsReport);
				UpdateGravTotalFieldCalc(opsReport);
				UpdateValid(opsReport);
				UpdateVarianceNN(opsReport);
				UpdateVarianceNE(opsReport);
				UpdateVarianceNVert(opsReport);
				UpdateVarianceEE(opsReport);
				UpdateVarianceEVert(opsReport);
				UpdateVarianceVertVert(opsReport);
				UpdateBiasN(opsReport);
				UpdateBiasE(opsReport);
				UpdateBiasVert(opsReport);
				UpdateMatrixCov(opsReport);
				UpdateWellCRS(opsReport);
				UpdateLatitude(opsReport);
				UpdateLongitude(opsReport);
				UpdateLocation(opsReport);
				UpdateProjectedX(opsReport);
				UpdateProjectedY(opsReport);
				UpdateTrajectoryStation(opsReport);
				UpdateDensity(opsReport);
				UpdateVisFunnel(opsReport);
				UpdateTempVis(opsReport);
				UpdatePv(opsReport);
				UpdateYp(opsReport);
				UpdateGel10Sec(opsReport);
				UpdateGel10Min(opsReport);
				UpdateGel30Min(opsReport);
				UpdateFilterCakeLtlp(opsReport);
				UpdateFiltrateLtlp(opsReport);
				UpdateTempHthp(opsReport);
				UpdatePresHthp(opsReport);
				UpdateFiltrateHthp(opsReport);
				UpdateFilterCakeHthp(opsReport);
				UpdateSolidsPc(opsReport);
				UpdateWaterPc(opsReport);
				UpdateOilPc(opsReport);
				UpdateSandPc(opsReport);
				UpdateSolidsLowGravPc(opsReport);
				UpdateSolidsCalcPc(opsReport);
				UpdateBaritePc(opsReport);
				UpdateLcm(opsReport);
				UpdateMbt(opsReport);
				UpdateTempPh(opsReport);
				UpdatePm(opsReport);
				UpdatePmFiltrate(opsReport);
				UpdateMf(opsReport);
				UpdateAlkalinityP1(opsReport);
				UpdateAlkalinityP2(opsReport);
				UpdateChloride(opsReport);
				UpdateCalcium(opsReport);
				UpdateMagnesium(opsReport);
				UpdateTempRheom(opsReport);
				UpdatePresRheom(opsReport);
				UpdateRheometer(opsReport);
				UpdateBrinePc(opsReport);
				UpdateLime(opsReport);
				UpdateElectStab(opsReport);
				UpdateCalciumChloride(opsReport);
				UpdateSolidsHiGravPc(opsReport);
				UpdatePolymer(opsReport);
				UpdateSolCorPc(opsReport);
				UpdateOilCtg(opsReport);
				UpdateHardnessCa(opsReport);
				UpdateSulfide(opsReport);
				UpdateFluid(opsReport);
				UpdatePump(opsReport);
				UpdateRateStroke(opsReport);
				UpdatePresRecorded(opsReport);
				UpdateMdBit(opsReport);
				UpdateScr(opsReport);
				UpdatePit(opsReport);
				UpdateVolPit(opsReport);
				UpdateDensFluid(opsReport);
				UpdatePitVolume(opsReport);
				UpdateVolTotMudStart(opsReport);
				UpdateVolMudDumped(opsReport);
				UpdateVolMudReceived(opsReport);
				UpdateVolMudReturned(opsReport);
				UpdateVolLostShakerSurf(opsReport);
				UpdateVolLostMudCleanerSurf(opsReport);
				UpdateVolLostPitsSurf(opsReport);
				UpdateVolLostTrippingSurf(opsReport);
				UpdateVolLostOtherSurf(opsReport);
				UpdateVolTotMudLostSurf(opsReport);
				UpdateVolLostCircHole(opsReport);
				UpdateVolLostCsgHole(opsReport);
				UpdateVolLostCmtHole(opsReport);
				UpdateVolLostBhdCsgHole(opsReport);
				UpdateVolLostAbandonHole(opsReport);
				UpdateVolLostOtherHole(opsReport);
				UpdateVolTotMudLostHole(opsReport);
				UpdateMudLosses(opsReport);
				UpdateVolMudBuilt(opsReport);
				UpdateVolMudString(opsReport);
				UpdateVolMudCasing(opsReport);
				UpdateVolMudHole(opsReport);
				UpdateVolMudRiser(opsReport);
				UpdateVolTotMudEnd(opsReport);
				UpdateMudVolume(opsReport);
				UpdateItemWtPerUnit(opsReport);
				UpdatePricePerUnit(opsReport);
				UpdateCostItem(opsReport);
				UpdateMudInventory(opsReport);
				UpdateItemVolPerUnit(opsReport);
				UpdateBulk(opsReport);
				UpdateAnchorTension(opsReport);
				UpdateAnchorAngle(opsReport);
				UpdateRigHeading(opsReport);
				UpdateRigHeave(opsReport);
				UpdateRigPitchAngle(opsReport);
				UpdateRigRollAngle(opsReport);
				UpdateRiserAngle(opsReport);
				UpdateRiserDirection(opsReport);
				UpdateRiserTension(opsReport);
				UpdateVariableDeckLoad(opsReport);
				UpdateTotalDeckLoad(opsReport);
				UpdateGuideBaseAngle(opsReport);
				UpdateBallJointAngle(opsReport);
				UpdateBallJointDirection(opsReport);
				UpdateOffsetRig(opsReport);
				UpdateLoadLeg1(opsReport);
				UpdateLoadLeg2(opsReport);
				UpdateLoadLeg3(opsReport);
				UpdateLoadLeg4(opsReport);
				UpdatePenetrationLeg1(opsReport);
				UpdatePenetrationLeg2(opsReport);
				UpdatePenetrationLeg3(opsReport);
				UpdatePenetrationLeg4(opsReport);
				UpdateDispRig(opsReport);
				UpdateMeanDraft(opsReport);
				UpdateRigResponse(opsReport);
				UpdateIdLiner(opsReport);
				UpdateLenStroke(opsReport);
				UpdatePressure(opsReport);
				UpdatePcEfficiency(opsReport);
				UpdatePumpOutput(opsReport);
				UpdatePumpOp(opsReport);
				UpdateShaker(opsReport);
				UpdateMdHole(opsReport);
				UpdateHoursRun(opsReport);
				UpdatePcScreenCovered(opsReport);
				UpdateMeshX(opsReport);
				UpdateMeshY(opsReport);
				UpdateCutPoint(opsReport);
				UpdateMeshY(opsReport);
				UpdateShakerScreen(opsReport);
				UpdateShakerOp(opsReport);
				UpdateDaysIncFree(opsReport);
				UpdateETimLostGross(opsReport);
				UpdateCostLostGross(opsReport);
				UpdateIncident(opsReport);
				UpdatePresLastCsg(opsReport);
				UpdatePresStdPipe(opsReport);
				UpdatePresKellyHose(opsReport);
				UpdatePresDiverter(opsReport);
				UpdatePresAnnular(opsReport);
				UpdatePresRams(opsReport);
				UpdatePresChokeLine(opsReport);
				UpdatePresChokeMan(opsReport);
				UpdateFluidDischarged(opsReport);
				UpdateVolCtgDischarged(opsReport);
				UpdateVolOilCtgDischarge(opsReport);
				UpdateWasteDischarged(opsReport);
				UpdateHse(opsReport);
				UpdateTotalTime(opsReport);
				UpdatePersonnel(opsReport);
				UpdateSupportCraft(opsReport);
				UpdateBarometricPressure(opsReport);
				UpdateTempSurfaceMn(opsReport);
				UpdateTempSurfaceMx(opsReport);
				UpdateTempWindChill(opsReport);
				UpdateTempsea(opsReport);
				UpdateVisibility(opsReport);
				UpdateAziWave(opsReport);
				UpdateHtWave(opsReport);
				UpdatePeriodWave(opsReport);
				UpdateAziWind(opsReport);
				UpdateVelWind(opsReport);
				UpdateAmtPrecip(opsReport);
				UpdateCeilingCloud(opsReport);
				UpdateCurrentSea(opsReport);
				UpdateAziCurrentSea(opsReport);
				UpdateWeather(opsReport);
				UpdateCostDay(opsReport);
				UpdateCostDayMud(opsReport);
				UpdateDiaHole(opsReport);
				UpdateDiaCsgLast(opsReport);
				UpdateMdCsgLast(opsReport);
				UpdateTvdCsgLast(opsReport);
				UpdateTvdLot(opsReport);
				UpdatePresLotEmw(opsReport);
				UpdatePresKickTol(opsReport);
				UpdateVolKickTol(opsReport);
				UpdateMaasp(opsReport);
				UpdateCommonData(opsReport);
				_db.OpsReports.Update(opsReport);
				return Save();
			}
			catch(Exception ex)
			{
				CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
				customErrorHandler.WriteError(ex, "OpsReport UpdateOpsReport", null);
				return Save();
			}
		}

		#region Insert OpsReport
		private void Rig(OpsReport opsReport)
		{
			if (opsReport.Rig.UidRef != null)
			{
				var obj = _mapper.Map<OpsReportRig>(opsReport.Rig);
				_db.OpsReportRigs.Add(obj);
			}
		}
		private void ETimStart(OpsReport opsReport)
		{
			if (opsReport.ETimStart.Uom != null)
			{
				var obj = _mapper.Map<OpsReportETimStart>(opsReport.ETimStart);
				_db.OpsReportETimStarts.Add(obj);
			}
		}
		private void ETimSpud(OpsReport opsReport)
		{
			if (opsReport.ETimSpud.Uom != null)
			{
				var obj = _mapper.Map<OpsReportETimSpud>(opsReport.ETimSpud);
				_db.OpsReportETimSpuds.Add(obj);
			}
		}
		private void ETimLoc(OpsReport opsReport)
		{
			if (opsReport.ETimLoc.Uom != null)
			{
				var obj = _mapper.Map<OpsReportETimLoc>(opsReport.ETimLoc);
				_db.OpsReportETimLocs.Add(obj);
			}
		}
		private void MdReport(OpsReport opsReport)
		{
			if (opsReport.MdReport.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMdReport>(opsReport.MdReport);
				_db.OpsReportMdReports.Add(obj);
			}
		}
		private void TvdReport(OpsReport opsReport)
		{
			if (opsReport.TvdReport.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTvdReport>(opsReport.TvdReport);
				_db.OpsReportTvdReports.Add(obj);
			}
		}
		private void DistDrill(OpsReport opsReport)
		{
			if (opsReport.DistDrill.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDistDrill>(opsReport.DistDrill);
				_db.OpsReportDistDrills.Add(obj);
			}
		}
		private void ETimDrill(OpsReport opsReport)
		{
			if (opsReport.ETimDrill.Uom != null)
			{
				var obj = _mapper.Map<OpsReportETimDrill>(opsReport.ETimDrill);
				_db.OpsReportETimDrills.Add(obj);
			}
		}
		private void MdPlanned(OpsReport opsReport)
		{
			if (opsReport.MdPlanned.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMdPlanned>(opsReport.MdPlanned);
				_db.OpsReportMdPlanneds.Add(obj);
			}
		}
		private void RopAv(OpsReport opsReport)
		{
			if (opsReport.RopAv.Uom != null)
			{
				var obj = _mapper.Map<OpsReportRopAv>(opsReport.RopAv);
				_db.OpsReportRopAvs.Add(obj);
			}
		}
		private void RopCurrent(OpsReport opsReport)
		{
			if (opsReport.RopCurrent.Uom != null)
			{
				var obj = _mapper.Map<OpsReportRopCurrent>(opsReport.RopCurrent);
				_db.OpsReportRopCurrents.Add(obj);
			}
		}
		private void ETimDrillRot(OpsReport opsReport)
		{
			if (opsReport.ETimDrillRot.Uom != null)
			{
				var obj = _mapper.Map<OpsReportETimDrillRot>(opsReport.ETimDrillRot);
				_db.OpsReportETimDrillRots.Add(obj);
			}
		}
		private void ETimDrillSlid(OpsReport opsReport)
		{
			if (opsReport.ETimDrillSlid.Uom != null)
			{
				var obj = _mapper.Map<OpsReportETimDrillSlid>(opsReport.ETimDrillSlid);
				_db.OpsReportETimDrillSlids.Add(obj);
			}
		}
		private void ETimCirc(OpsReport opsReport)
		{
			if (opsReport.ETimCirc.Uom != null)
			{
				var obj = _mapper.Map<OpsReportETimCirc>(opsReport.ETimCirc);
				_db.OpsReportETimCircs.Add(obj);
			}
		}
		private void ETimReam(OpsReport opsReport)
		{
			if (opsReport.ETimReam.Uom != null)
			{
				var obj = _mapper.Map<OpsReportETimReam>(opsReport.ETimReam);
				_db.OpsReportETimReams.Add(obj);
			}
		}

		private void ETimHold(OpsReport opsReport)
		{
			if (opsReport.ETimHold.Uom != null)
			{
				var obj = _mapper.Map<OpsReportETimHold>(opsReport.ETimHold);
				_db.OpsReportETimHolds.Add(obj);
			}
		}
		private void ETimSteering(OpsReport opsReport)
		{
			if (opsReport.ETimSteering.Uom != null)
			{
				var obj = _mapper.Map<OpsReportETimSteering>(opsReport.ETimSteering);
				_db.OpsReportETimSteerings.Add(obj);
			}
		}
		private void DistDrillRot(OpsReport opsReport)
		{
			if (opsReport.DistDrillRot.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDistDrillRot>(opsReport.DistDrillRot);
				_db.OpsReportDistDrillRots.Add(obj);
			}
			if (opsReport.DrillingParams.DistDrillRot.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDistDrillRot>(opsReport.DrillingParams.DistDrillRot);
				_db.OpsReportDistDrillRots.Add(obj);
			}
		}
		private void DistDrillSlid(OpsReport opsReport)
		{
			if (opsReport.DistDrillSlid.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDistDrillSlid>(opsReport.DistDrillSlid);
				_db.OpsReportDistDrillSlids.Add(obj);
			}
			if (opsReport.DrillingParams.DistDrillSlid.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDistDrillSlid>(opsReport.DrillingParams.DistDrillSlid);
				_db.OpsReportDistDrillSlids.Add(obj);
			}
		}
	   
		private void DistReam(OpsReport opsReport)
		{
			if (opsReport.DistReam.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDistReam>(opsReport.DistReam);
				_db.OpsReportDistReams.Add(obj);
			}
			if (opsReport.DrillingParams.DistReam.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDistReam>(opsReport.DrillingParams.DistReam);
				_db.OpsReportDistReams.Add(obj);
			}
		}
		private void DistHold(OpsReport opsReport)
		{
			if (opsReport.DistHold.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDistHold>(opsReport.DistHold);
				_db.OpsReportDistHolds.Add(obj);
			}
			if (opsReport.DrillingParams.DistHold.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDistHold>(opsReport.DrillingParams.DistHold);
				_db.OpsReportDistHolds.Add(obj);
			}
		}
		private void DistSteering(OpsReport opsReport)
		{
			if (opsReport.DistSteering.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDistSteering>(opsReport.DistSteering);
				_db.OpsReportDistSteerings.Add(obj);
			}
			if (opsReport.DrillingParams.DistSteering.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDistSteering>(opsReport.DrillingParams.DistSteering);
				_db.OpsReportDistSteerings.Add(obj);
			}
		}
		private void Duration(OpsReport opsReport)
		{
			if (opsReport.Activity.Duration.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDuration>(opsReport.Activity.Duration);
				_db.OpsReportDurations.Add(obj);
			}
		}
		private void MdHoleStart(OpsReport opsReport)
		{
			if (opsReport.Activity.MdHoleStart.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMdHoleStart>(opsReport.Activity.MdHoleStart);
				_db.OpsReportMdHoleStarts.Add(obj);
			}
			if (opsReport.DrillingParams.MdHoleStart.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMdHoleStart>(opsReport.DrillingParams.MdHoleStart);
				_db.OpsReportMdHoleStarts.Add(obj);
			}
		}
		private void TvdHoleStart(OpsReport opsReport)
		{
			if (opsReport.Activity.TvdHoleStart.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTvdHoleStart>(opsReport.Activity.TvdHoleStart);
				_db.OpsReportTvdHoleStarts.Add(obj);
			}
		}
		private void MdHoleEnd(OpsReport opsReport)
		{
			if (opsReport.Activity.MdHoleEnd.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMdHoleEnd>(opsReport.Activity.MdHoleEnd);
				_db.OpsReportMdHoleEnds.Add(obj);
			}
		}
		private void TvdHoleEnd(OpsReport opsReport)
		{
			if (opsReport.Activity.TvdHoleEnd.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTvdHoleEnd>(opsReport.Activity.TvdHoleEnd);
				_db.OpsReportTvdHoleEnds.Add(obj);
			}
		}
		private void MdBitStart(OpsReport opsReport)
		{
			if (opsReport.Activity.MdBitStart.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMdBitStart>(opsReport.Activity.MdBitStart);
				_db.OpsReportMdBitStarts.Add(obj);
			}
		}
		private void MdBitEnd(OpsReport opsReport)
		{
			if (opsReport.Activity.MdBitEnd.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMdBitEnd>(opsReport.Activity.MdBitEnd);
				_db.OpsReportMdBitEnds.Add(obj);
			}
		}
		private void Activity(OpsReport opsReport)
		{
			if (opsReport.Activity.Uid != null)
			{
				var obj = _mapper.Map<OpsReportActivity>(opsReport.Activity);
				_db.OpsReportActivitys.Add(obj);
			}
		}



		private void ETimOpBit(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.ETimOpBit.Uom != null)
			{
				var obj = _mapper.Map<OpsReportETimOpBit>(opsReport.DrillingParams.ETimOpBit);
				_db.OpsReportETimOpBits.Add(obj);
			}
		}
	   
		private void MdHoleStop(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.MdHoleStop.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMdHoleStop>(opsReport.DrillingParams.MdHoleStop);
				_db.OpsReportMdHoleStops.Add(obj);
			}
		}
		private void Tubular(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.Tubular.UidRef != null)
			{
				var obj = _mapper.Map<OpsReportTubular>(opsReport.DrillingParams.Tubular);
				_db.OpsReportTubulars.Add(obj);
			}
		}
		
		private void HkldRot(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.HkldRot.Uom != null)
			{
				var obj = _mapper.Map<OpsReportHkldRot>(opsReport.DrillingParams.HkldRot);
				_db.OpsReportHkldRots.Add(obj);
			}
		}
		private void OverPull(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.OverPull.Uom != null)
			{
				var obj = _mapper.Map<OpsReportOverPull>(opsReport.DrillingParams.OverPull);
				_db.OpsReportOverPulls.Add(obj);
			}
		}

		private void SlackOff(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.SlackOff.Uom != null)
			{
				var obj = _mapper.Map<OpsReportSlackOff>(opsReport.DrillingParams.SlackOff);
				_db.OpsReportSlackOffs.Add(obj);
			}
		}
		private void HkldUp(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.HkldUp.Uom != null)
			{
				var obj = _mapper.Map<OpsReportHkldUp>(opsReport.DrillingParams.HkldUp);
				_db.OpsReportHkldUps.Add(obj);
			}
		}
		private void HkldDn(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.HkldDn.Uom != null)
			{
				var obj = _mapper.Map<OpsReportHkldDn>(opsReport.DrillingParams.HkldDn);
				_db.OpsReportHkldDns.Add(obj);
			}
		}
		private void TqOnBotAv(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.TqOnBotAv.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTqOnBotAv>(opsReport.DrillingParams.TqOnBotAv);
				_db.OpsReportTqOnBotAvs.Add(obj);
			}
		}
		private void TqOnBotMx(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.TqOnBotMx.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTqOnBotMx>(opsReport.DrillingParams.TqOnBotMx);
				_db.OpsReportTqOnBotMxs.Add(obj);
			}
		}
		private void TqOnBotMn(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.TqOnBotMn.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTqOnBotMn>(opsReport.DrillingParams.TqOnBotMn);
				_db.OpsReportTqOnBotMns.Add(obj);
			}
		}
		private void TqOffBotAv(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.TqOffBotAv.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTqOffBotAv>(opsReport.DrillingParams.TqOffBotAv);
				_db.OpsReportTqOffBotAvs.Add(obj);
			}
		}
		private void TqDhAv(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.TqDhAv.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTqDhAv>(opsReport.DrillingParams.TqDhAv);
				_db.OpsReportTqDhAvs.Add(obj);
			}
		}
		private void WtAboveJar(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.WtAboveJar.Uom != null)
			{
				var obj = _mapper.Map<OpsReportWtAboveJar>(opsReport.DrillingParams.WtAboveJar);
				_db.OpsReportWtAboveJars.Add(obj);
			}
		}
		private void WtBelowJar(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.WtBelowJar.Uom != null)
			{
				var obj = _mapper.Map<OpsReportWtBelowJar>(opsReport.DrillingParams.WtBelowJar);
				_db.OpsReportWtBelowJars.Add(obj);
			}
		}
		private void WtMud(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.WtMud.Uom != null)
			{
				var obj = _mapper.Map<OpsReportWtMud>(opsReport.DrillingParams.WtMud);
				_db.OpsReportWtMuds.Add(obj);
			}
		}
		private void FlowratePump(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.FlowratePump.Uom != null)
			{
				var obj = _mapper.Map<OpsReportFlowratePump>(opsReport.DrillingParams.FlowratePump);
				_db.OpsReportFlowratePumps.Add(obj);
			}
		}
		private void PowBit(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.PowBit.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPowBit>(opsReport.DrillingParams.PowBit);
				_db.OpsReportPowBits.Add(obj);
			}
		}
		private void VelNozzleAv(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.VelNozzleAv.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVelNozzleAv>(opsReport.DrillingParams.VelNozzleAv);
				_db.OpsReportVelNozzleAvs.Add(obj);
			}
		}
		private void PresDropBit(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.PresDropBit.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPresDropBit>(opsReport.DrillingParams.PresDropBit);
				_db.OpsReportPresDropBits.Add(obj);
			}
		}
		private void CTimHold(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.CTimHold.Uom != null)
			{
				var obj = _mapper.Map<OpsReportCTimHold>(opsReport.DrillingParams.CTimHold);
				_db.OpsReportCTimHolds.Add(obj);
			}
		}
		private void CTimSteering(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.CTimSteering.Uom != null)
			{
				var obj = _mapper.Map<OpsReportCTimSteering>(opsReport.DrillingParams.CTimSteering);
				_db.OpsReportCTimSteerings.Add(obj);
			}
		}
		private void CTimDrillRot(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.CTimDrillRot.Uom != null)
			{
				var obj = _mapper.Map<OpsReportCTimDrillRot>(opsReport.DrillingParams.CTimDrillRot);
				_db.OpsReportCTimDrillRots.Add(obj);
			}
		}
		private void CTimDrillSlid(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.CTimDrillSlid.Uom != null)
			{
				var obj = _mapper.Map<OpsReportCTimDrillSlid>(opsReport.DrillingParams.CTimDrillSlid);
				_db.OpsReportCTimDrillSlids.Add(obj);
			}
		}

		private void CTimCirc(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.CTimCirc.Uom != null)
			{
				var obj = _mapper.Map<OpsReportCTimCirc>(opsReport.DrillingParams.CTimCirc);
				_db.OpsReportCTimCircs.Add(obj);
			}
		}

		private void CTimReam(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.CTimReam.Uom != null)
			{
				var obj = _mapper.Map<OpsReportCTimReam>(opsReport.DrillingParams.CTimReam);
				_db.OpsReportCTimReams.Add(obj);
			}
		}
		private void RpmAv(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.RpmAv.Uom != null)
			{
				var obj = _mapper.Map<OpsReportRpmAv>(opsReport.DrillingParams.RpmAv);
				_db.OpsReportRpmAvs.Add(obj);
			}
		}
		private void RpmMx(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.RpmMx.Uom != null)
			{
				var obj = _mapper.Map<OpsReportRpmMx>(opsReport.DrillingParams.RpmMx);
				_db.OpsReportRpmMxs.Add(obj);
			}
		}
		private void RpmMn(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.RpmMn.Uom != null)
			{
				var obj = _mapper.Map<OpsReportRpmMn>(opsReport.DrillingParams.RpmMn);
				_db.OpsReportRpmMns.Add(obj);
			}
		}

		private void RpmAvDh(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.RpmAvDh.Uom != null)
			{
				var obj = _mapper.Map<OpsReportRpmAvDh>(opsReport.DrillingParams.RpmAvDh);
				_db.OpsReportRpmAvDhs.Add(obj);
			}
		}
		private void RopMx(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.RopMx.Uom != null)
			{
				var obj = _mapper.Map<OpsReportRopMx>(opsReport.DrillingParams.RopMx);
				_db.OpsReportRopMxs.Add(obj);
			}
		}
		private void RopMn(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.RopMn.Uom != null)
			{
				var obj = _mapper.Map<OpsReportRopMn>(opsReport.DrillingParams.RopMn);
				_db.OpsReportRopMns.Add(obj);
			}
		}
		private void WobAv(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.WobAv.Uom != null)
			{
				var obj = _mapper.Map<OpsReportWobAv>(opsReport.DrillingParams.WobAv);
				_db.OpsReportWobAvs.Add(obj);
			}
		}
	   

		private void WobMx(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.WobMx.Uom != null)
			{
				var obj = _mapper.Map<OpsReportWobMx>(opsReport.DrillingParams.WobMx);
				_db.OpsReportWobMxs.Add(obj);
			}
		}
		private void WobMn(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.WobMn.Uom != null)
			{
				var obj = _mapper.Map<OpsReportWobMn>(opsReport.DrillingParams.WobMn);
				_db.OpsReportWobMns.Add(obj);
			}
		}
		private void WobAvDh(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.WobAvDh.Uom != null)
			{
				var obj = _mapper.Map<OpsReportWobAvDh>(opsReport.DrillingParams.WobAvDh);
				_db.OpsReportWobAvDhs.Add(obj);
			}
		}
		private void AziTop(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.AziTop.Uom != null)
			{
				var obj = _mapper.Map<OpsReportAziTop>(opsReport.DrillingParams.AziTop);
				_db.OpsReportAziTops.Add(obj);
			}
		}
		private void AziBottom(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.AziBottom.Uom != null)
			{
				var obj = _mapper.Map<OpsReportAziBottom>(opsReport.DrillingParams.AziBottom);
				_db.OpsReportAziBottoms.Add(obj);
			}
		}
		private void InclStart(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.InclStart.Uom != null)
			{
				var obj = _mapper.Map<OpsReportInclStart>(opsReport.DrillingParams.InclStart);
				_db.OpsReportInclStarts.Add(obj);
			}
		}
		private void InclMx(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.InclMx.Uom != null)
			{
				var obj = _mapper.Map<OpsReportInclMx>(opsReport.DrillingParams.InclMx);
				_db.OpsReportInclMxs.Add(obj);
			}
		}
		private void InclMn(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.InclMn.Uom != null)
			{
				var obj = _mapper.Map<OpsReportInclMn>(opsReport.DrillingParams.InclMn);
				_db.OpsReportInclMns.Add(obj);
			}
		}
		private void InclStop(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.InclStop.Uom != null)
			{
				var obj = _mapper.Map<OpsReportInclStop>(opsReport.DrillingParams.InclStop);
				_db.OpsReportInclStops.Add(obj);
			}
		}
		private void TempMudDhMx(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.TempMudDhMx.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTempMudDhMx>(opsReport.DrillingParams.TempMudDhMx);
				_db.OpsReportTempMudDhMxs.Add(obj);
			}
		}
		private void PresPumpAv(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.PresPumpAv.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPresPumpAv>(opsReport.DrillingParams.PresPumpAv);
				_db.OpsReportPresPumpAvs.Add(obj);
			}
		}
		private void FlowrateBit(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.FlowrateBit.Uom != null)
			{
				var obj = _mapper.Map<OpsReportFlowrateBit>(opsReport.DrillingParams.FlowrateBit);
				_db.OpsReportFlowrateBits.Add(obj);
			}
		}
		private void DrillingParams(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.Uid != null)
			{
				var obj = _mapper.Map<OpsReportDrillingParam>(opsReport.DrillingParams);
				_db.OpsReportDrillingParams.Add(obj);
			}
		}
		private void CostPerItem(OpsReport opsReport)
		{
			foreach (var item in opsReport.DayCost)
			{

				if (item.CostPerItem != null)
				{
					var obj = _mapper.Map<OpsReportCostPerItem>(item.CostPerItem);
					_db.OpsReportCostPerItems.Add(obj);
				}
			}
		}
		private void CostAmount(OpsReport opsReport)
		{
			foreach (var item in opsReport.DayCost)
			{

				if (item.CostAmount != null)
				{
					var obj = _mapper.Map<OpsReportCostAmount>(item.CostAmount);
					_db.OpsReportCostAmounts.Add(obj);
				}
			}
		}
		private void DayCost(OpsReport opsReport)
		{
			foreach (var item in opsReport.DayCost)
			{

				if (item != null)
				{
					var obj = _mapper.Map<OpsReportDayCost>(item);
					_db.OpsReportDayCosts.Add(obj);
				}
			}
		}

		private void Md(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.Md.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMd>(opsReport.TrajectoryStation.Md);
				_db.OpsReportMds.Add(obj);
			}
		}
		private void Tvd(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.Tvd.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTvd>(opsReport.TrajectoryStation.Tvd);
				_db.OpsReportTvds.Add(obj);
			}
		}

		private void Incl(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.Incl.Uom != null)
			{
				var obj = _mapper.Map<OpsReportIncl>(opsReport.TrajectoryStation.Incl);
				_db.OpsReportIncls.Add(obj);
			}
		}

		private void Azi(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.Azi.Uom != null)
			{
				var obj = _mapper.Map<OpsReportAzi>(opsReport.TrajectoryStation.Azi);
				_db.OpsReportAzis.Add(obj);
			}
		}
		private void Mtf(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.Mtf.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMtf>(opsReport.TrajectoryStation.Mtf);
				_db.OpsReportMtfs.Add(obj);
			}
		}
		private void Gtf(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.Gtf.Uom != null)
			{
				var obj = _mapper.Map<OpsReportGtf>(opsReport.TrajectoryStation.Gtf);
				_db.OpsReportGtfs.Add(obj);
			}
		}
		private void DispNs(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.DispNs.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDispNs>(opsReport.TrajectoryStation.DispNs);
				_db.OpsReportDispNss.Add(obj);
			}
		}
		private void DispEw(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.DispEw.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDispEw>(opsReport.TrajectoryStation.DispEw);
				_db.OpsReportDispEws.Add(obj);
			}
		}
		private void VertSect(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.VertSect.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVertSect>(opsReport.TrajectoryStation.VertSect);
				_db.OpsReportVertSects.Add(obj);
			}
		}
		private void Dls(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.Dls.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDls>(opsReport.TrajectoryStation.Dls);
				_db.OpsReportDlss.Add(obj);
			}
		}
		private void RateTurn(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.RateTurn.Uom != null)
			{
				var obj = _mapper.Map<OpsReportRateTurn>(opsReport.TrajectoryStation.RateTurn);
				_db.OpsReportRateTurns.Add(obj);
			}
		}
		private void RateBuild(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.RateBuild.Uom != null)
			{
				var obj = _mapper.Map<OpsReportRateBuild>(opsReport.TrajectoryStation.RateBuild);
				_db.OpsReportRateBuilds.Add(obj);
			}
		}
		private void MdDelta(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.MdDelta.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMdDelta>(opsReport.TrajectoryStation.MdDelta);
				_db.OpsReportMdDeltas.Add(obj);
			}
		}
		private void TvdDelta(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.TvdDelta.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTvdDelta>(opsReport.TrajectoryStation.TvdDelta);
				_db.OpsReportTvdDeltas.Add(obj);
			}
		}
		private void GravTotalUncert(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.GravTotalUncert.Uom != null)
			{
				var obj = _mapper.Map<OpsReportGravTotalUncert>(opsReport.TrajectoryStation.GravTotalUncert);
				_db.OpsReportGravTotalUncerts.Add(obj);
			}
		}
		private void DipAngleUncert(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.DipAngleUncert.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDipAngleUncert>(opsReport.TrajectoryStation.DipAngleUncert);
				_db.OpsReportDipAngleUncerts.Add(obj);
			}
		}
		private void MagTotalUncert(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.MagTotalUncert.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMagTotalUncert>(opsReport.TrajectoryStation.MagTotalUncert);
				_db.OpsReportMagTotalUncerts.Add(obj);
			}
		}
		private void GravAxialRaw(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.RawData.GravAxialRaw.Uom != null)
			{
				var obj = _mapper.Map<OpsReportGravAxialRaw>(opsReport.TrajectoryStation.RawData.GravAxialRaw);
				_db.OpsReportGravAxialRaws.Add(obj);
			}
		}
		private void GravTran1Raw(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.RawData.GravTran1Raw.Uom != null)
			{
				var obj = _mapper.Map<OpsReportGravTran1Raw>(opsReport.TrajectoryStation.RawData.GravTran1Raw);
				_db.OpsReportGravTran1Raws.Add(obj);
			}
		}
		private void GravTran2Raw(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.RawData.GravTran2Raw.Uom != null)
			{
				var obj = _mapper.Map<OpsReportGravTran2Raw>(opsReport.TrajectoryStation.RawData.GravTran2Raw);
				_db.OpsReportGravTran2Raws.Add(obj);
			}
		}
	   
		private void GravAxialRaw(Trajectory trajectory)
		{
			if (trajectory.TrajectoryStation.RawData.GravAxialRaw.Uom != null)
			{
				var obj = _mapper.Map<OpsReportGravAxialRaw>(trajectory.TrajectoryStation.RawData.GravAxialRaw);
				_db.OpsReportGravAxialRaws.Add(obj);
			}
		}
		private void GravTran1Raw(Trajectory trajectory)
		{
			if (trajectory.TrajectoryStation.RawData.GravTran1Raw.Uom != null)
			{
				var obj = _mapper.Map<OpsReportGravTran1Raw>(trajectory.TrajectoryStation.RawData.GravTran1Raw);
				_db.OpsReportGravTran1Raws.Add(obj);
			}
		}
		private void GravTran2Raw(Trajectory trajectory)
		{
			if (trajectory.TrajectoryStation.RawData.GravTran2Raw.Uom != null)
			{
				var obj = _mapper.Map<OpsReportGravTran2Raw>(trajectory.TrajectoryStation.RawData.GravTran2Raw);
				_db.OpsReportGravTran2Raws.Add(obj);
			}
		}

		private void MagAxialRaw(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.RawData.MagAxialRaw.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMagAxialRaw>(opsReport.TrajectoryStation.RawData.MagAxialRaw);
				_db.OpsReportMagAxialRaws.Add(obj);
			}
		}
		private void MagTran1Raw(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.RawData.MagTran1Raw.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMagTran1Raw>(opsReport.TrajectoryStation.RawData.MagTran1Raw);
				_db.OpsReportMagTran1Raws.Add(obj);
			}
		}
		private void MagTran2Raw(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.RawData.MagTran2Raw.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMagTran2Raw>(opsReport.TrajectoryStation.RawData.MagTran2Raw);
				_db.OpsReportMagTran2Raws.Add(obj);
			}
		}

		private void RawData(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.RawData != null)
			{
				var obj = _mapper.Map<OpsReportRawData>(opsReport.TrajectoryStation.RawData);
				_db.OpsReportRawDatas.Add(obj);
			}
		}

		private void GravAxialAccelCor(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.CorUsed.GravAxialAccelCor.Uom != null)
			{
				var obj = _mapper.Map<OpsReportGravAxialAccelCor>(opsReport.TrajectoryStation.CorUsed.GravAxialAccelCor);
				_db.OpsReportGravAxialAccelCors.Add(obj);
			}
		}

		private void GravTran2AccelCor(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.CorUsed.GravTran2AccelCor.Uom != null)
			{
				var obj = _mapper.Map<OpsReportGravTran2AccelCor>(opsReport.TrajectoryStation.CorUsed.GravTran2AccelCor);
				_db.OpsReportGravTran2AccelCors.Add(obj);
			}
		}

		private void MagAxialDrlstrCor(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.CorUsed.MagAxialDrlstrCor.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMagAxialDrlstrCor>(opsReport.TrajectoryStation.CorUsed.MagAxialDrlstrCor);
				_db.OpsReportMagAxialDrlstrCors.Add(obj);
			}
		}

		private void MagTran1DrlstrCor(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.CorUsed.MagTran1DrlstrCor.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMagTran1DrlstrCor>(opsReport.TrajectoryStation.CorUsed.MagTran1DrlstrCor);
				_db.OpsReportMagTran1DrlstrCors.Add(obj);
			}
		}

		private void MagTran2DrlstrCor(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.CorUsed.MagTran2DrlstrCor.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMagTran2DrlstrCor>(opsReport.TrajectoryStation.CorUsed.MagTran2DrlstrCor);
				_db.OpsReportMagTran2DrlstrCors.Add(obj);
			}
		}

		private void SagIncCor(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.CorUsed.SagIncCor.Uom != null)
			{
				var obj = _mapper.Map<OpsReportSagIncCor>(opsReport.TrajectoryStation.CorUsed.SagIncCor);
				_db.OpsReportSagIncCors.Add(obj);
			}
		}

		private void SagAziCor(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.CorUsed.SagAziCor.Uom != null)
			{
				var obj = _mapper.Map<OpsReportSagAziCor>(opsReport.TrajectoryStation.CorUsed.SagAziCor);
				_db.OpsReportSagAziCors.Add(obj);
			}
		}
		private void StnMagDeclUsed(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.CorUsed.StnMagDeclUsed.Uom != null)
			{
				var obj = _mapper.Map<OpsReportStnMagDeclUsed>(opsReport.TrajectoryStation.CorUsed.StnMagDeclUsed);
				_db.OpsReportStnMagDeclUseds.Add(obj);
			}
		}
		private void StnGridCorUsed(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.CorUsed.StnGridCorUsed.Uom != null)
			{
				var obj = _mapper.Map<OpsReportStnGridCorUsed>(opsReport.TrajectoryStation.CorUsed.StnGridCorUsed);
				_db.OpsReportStnGridCorUseds.Add(obj);
			}
		}
		private void DirSensorOffset(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.CorUsed.DirSensorOffset.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDirSensorOffset>(opsReport.TrajectoryStation.CorUsed.DirSensorOffset);
				_db.OpsReportDirSensorOffsets.Add(obj);
			}
		}

		private void GravTran1AccelCor(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.CorUsed.GravTran1AccelCor.Uom != null)
			{
				var obj = _mapper.Map<OpsReportGravTran1AccelCor>(opsReport.TrajectoryStation.CorUsed.GravTran1AccelCor);
				_db.OpsReportGravTran1AccelCors.Add(obj);
			}
		}
		private void CorUsed(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.CorUsed != null)
			{
				var obj = _mapper.Map<OpsReportCorUsed>(opsReport.TrajectoryStation.CorUsed);
				_db.OpsReportCorUseds.Add(obj);
			}
		}
		private void MagTotalFieldCalc(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.Valid.MagTotalFieldCalc.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMagTotalFieldCalc>(opsReport.TrajectoryStation.Valid.MagTotalFieldCalc);
				_db.OpsReportMagTotalFieldCalcs.Add(obj);
			}
		}

		private void MagDipAngleCalc(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.Valid.MagDipAngleCalc.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMagDipAngleCalc>(opsReport.TrajectoryStation.Valid.MagDipAngleCalc);
				_db.OpsReportMagDipAngleCalcs.Add(obj);
			}
		}
		private void GravTotalFieldCalc(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.Valid.GravTotalFieldCalc.Uom != null)
			{
				var obj = _mapper.Map<OpsReportGravTotalFieldCalc>(opsReport.TrajectoryStation.Valid.GravTotalFieldCalc);
				_db.OpsReportGravTotalFieldCalcs.Add(obj);
			}
		}
		private void Valid(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.Valid != null)
			{
				var obj = _mapper.Map<OpsReportValid>(opsReport.TrajectoryStation.Valid);
				_db.OpsReportValids.Add(obj);
			}
		}
		private void VarianceNN(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.MatrixCov.VarianceNN.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVarianceNN>(opsReport.TrajectoryStation.MatrixCov.VarianceNN);
				_db.OpsReportVarianceNNs.Add(obj);
			}
		}



		private void VarianceNE(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.MatrixCov.VarianceNE.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVarianceNE>(opsReport.TrajectoryStation.MatrixCov.VarianceNE);
				_db.OpsReportVarianceNEs.Add(obj);
			}
		}

		private void VarianceNVert(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.MatrixCov.VarianceNVert.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVarianceNVert>(opsReport.TrajectoryStation.MatrixCov.VarianceNVert);
				_db.OpsReportVarianceNVerts.Add(obj);
			}
		}
		private void VarianceEE(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.MatrixCov.VarianceEE.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVarianceEE>(opsReport.TrajectoryStation.MatrixCov.VarianceEE);
				_db.OpsReportVarianceEEs.Add(obj);
			}
		}
		private void VarianceEVert(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.MatrixCov.VarianceEVert.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVarianceEVert>(opsReport.TrajectoryStation.MatrixCov.VarianceEVert);
				_db.OpsReportVarianceEVerts.Add(obj);
			}
		}
		private void VarianceVertVert(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.MatrixCov.VarianceVertVert.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVarianceVertVert>(opsReport.TrajectoryStation.MatrixCov.VarianceVertVert);
				_db.OpsReportVarianceVertVerts.Add(obj);
			}
		}
		private void BiasN(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.MatrixCov.BiasN.Uom != null)
			{
				var obj = _mapper.Map<OpsReportBiasN>(opsReport.TrajectoryStation.MatrixCov.BiasN);
				_db.OpsReportBiasNs.Add(obj);
			}
		}
		private void BiasE(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.MatrixCov.BiasE.Uom != null)
			{
				var obj = _mapper.Map<OpsReportBiasE>(opsReport.TrajectoryStation.MatrixCov.BiasE);
				_db.OpsReportBiasEs.Add(obj);
			}
		}
		private void BiasVert(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.MatrixCov.BiasVert.Uom != null)
			{
				var obj = _mapper.Map<OpsReportBiasVert>(opsReport.TrajectoryStation.MatrixCov.BiasVert);
				_db.OpsReportBiasVerts.Add(obj);
			}
		}
		private void MatrixCov(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.MatrixCov != null)
			{
				var obj = _mapper.Map<OpsReportMatrixCov>(opsReport.TrajectoryStation.MatrixCov);
				_db.OpsReportMatrixCovs.Add(obj);
			}
		}
		private void WellCRS(OpsReport opsReport)
		{
			foreach (var item in opsReport.TrajectoryStation.Location)
			{
				if (item.WellCRS!=null && item.WellCRS.UidRef != null)
				{
					var obj = _mapper.Map<OpsReportWellCRS>(item.WellCRS);
					_db.OpsReportWellCRSs.Add(obj);
				}
			}
		}
		private void Latitude(OpsReport opsReport)
		{
			foreach (var item in opsReport.TrajectoryStation.Location)
			{
				if (item.Latitude != null && item.Latitude.Uom != null)
				{
					var obj = _mapper.Map<OpsReportLatitude>(item.Latitude);
					_db.OpsReportLatitudes.Add(obj);
				}
			}
		}
		private void Longitude(OpsReport opsReport)
		{
			foreach (var item in opsReport.TrajectoryStation.Location)
			{
				if (item.Longitude != null && item.Longitude.Uom != null)
				{
					var obj = _mapper.Map<OpsReportLongitude>(item.Longitude);
					_db.OpsReportLongitudes.Add(obj);
				}
			}
		}
	   
		private void Location(OpsReport opsReport)
		{
			foreach (var item in opsReport.TrajectoryStation.Location)
			{
				if (item != null &&  item.Uid != null)
				{
					var obj = _mapper.Map<OpsReportLocation>(item);
					_db.OpsReportLocations.Add(obj);
				}
			}
		}
		private void TrajectoryStation(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.Uid != null)
			{
				var obj = _mapper.Map<OpsReportTrajectoryStation>(opsReport.TrajectoryStation);
				_db.OpsReportTrajectoryStations.Add(obj);
			}
		}

		private void ProjectedX(OpsReport opsReport)
		{
			foreach (var item in opsReport.TrajectoryStation.Location)
			{
				if (item.ProjectedX != null &&  item.ProjectedX.Uom != null)
				{
					var obj = _mapper.Map<OpsReportProjectedX>(item.ProjectedX);
					_db.OpsReportProjectedXs.Add(obj);
				}
			}
		}

		private void ProjectedY(OpsReport opsReport)
		{
			foreach (var item in opsReport.TrajectoryStation.Location)
			{
				if (item.ProjectedY != null && item.ProjectedY.Uom != null)
				{
					var obj = _mapper.Map<OpsReportProjectedY>(item.ProjectedY);
					_db.OpsReportProjectedYs.Add(obj);
				}
			}
		}

		private void Density(OpsReport opsReport)
		{
				if (opsReport.Fluid.Density.Uom != null)
				{
					var obj = _mapper.Map<OpsReportDensity>(opsReport.Fluid.Density);
					_db.OpsReportDensitys.Add(obj);
				}
		}

		private void VisFunnel(OpsReport opsReport)
		{
			if (opsReport.Fluid.VisFunnel.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVisFunnel>(opsReport.Fluid.VisFunnel);
				_db.OpsReportVisFunnels.Add(obj);
			}
		}

		private void TempVis(OpsReport opsReport)
		{
			if (opsReport.Fluid.TempVis.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTempVis>(opsReport.Fluid.TempVis);
				_db.OpsReportTempViss.Add(obj);
			}
		}

		private void Pv(OpsReport opsReport)
		{
			if (opsReport.Fluid.Pv.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPv>(opsReport.Fluid.Pv);
				_db.OpsReportPvs.Add(obj);
			}
		}

		private void Yp(OpsReport opsReport)
		{
			if (opsReport.Fluid.Yp.Uom != null)
			{
				var obj = _mapper.Map<OpsReportYp>(opsReport.Fluid.Yp);
				_db.OpsReportYps.Add(obj);
			}
		}

		private void Gel10Sec(OpsReport opsReport)
		{
			if (opsReport.Fluid.Gel10Sec.Uom != null)
			{
				var obj = _mapper.Map<OpsReportGel10Sec>(opsReport.Fluid.Gel10Sec);
				_db.OpsReportGel10Secs.Add(obj);
			}
		}
		private void Gel10Min(OpsReport opsReport)
		{
			if (opsReport.Fluid.Gel10Min.Uom != null)
			{
				var obj = _mapper.Map<OpsReportGel10Min>(opsReport.Fluid.Gel10Min);
				_db.OpsReportGel10Mins.Add(obj);
			}
		}
		private void Gel30Min(OpsReport opsReport)
		{
			if (opsReport.Fluid.Gel30Min.Uom != null)
			{
				var obj = _mapper.Map<OpsReportGel30Min>(opsReport.Fluid.Gel30Min);
				_db.OpsReportGel30Mins.Add(obj);
			}
		}
		private void FilterCakeLtlp(OpsReport opsReport)
		{
			if (opsReport.Fluid.FilterCakeLtlp.Uom != null)
			{
				var obj = _mapper.Map<OpsReportFilterCakeLtlp>(opsReport.Fluid.FilterCakeLtlp);
				_db.OpsReportFilterCakeLtlps.Add(obj);
			}
		}
		private void FiltrateLtlp(OpsReport opsReport)
		{
			if (opsReport.Fluid.FiltrateLtlp.Uom != null)
			{
				var obj = _mapper.Map<OpsReportFiltrateLtlp>(opsReport.Fluid.FiltrateLtlp);
				_db.OpsReportFiltrateLtlps.Add(obj);
			}
		}
		private void TempHthp(OpsReport opsReport)
		{
			if (opsReport.Fluid.TempHthp.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTempHthp>(opsReport.Fluid.TempHthp);
				_db.OpsReportTempHthps.Add(obj);
			}
		}
		private void PresHthp(OpsReport opsReport)
		{
			if (opsReport.Fluid.PresHthp.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPresHthp>(opsReport.Fluid.PresHthp);
				_db.OpsReportPresHthps.Add(obj);
			}
		}
		private void FiltrateHthp(OpsReport opsReport)
		{
			if (opsReport.Fluid.FiltrateHthp.Uom != null)
			{
				var obj = _mapper.Map<OpsReportFiltrateHthp>(opsReport.Fluid.FiltrateHthp);
				_db.OpsReportFiltrateHthps.Add(obj);
			}
		}

		private void FilterCakeHthp(OpsReport opsReport)
		{
			if (opsReport.Fluid.FilterCakeHthp.Uom != null)
			{
				var obj = _mapper.Map<OpsReportFilterCakeHthp>(opsReport.Fluid.FilterCakeHthp);
				_db.OpsReportFilterCakeHthps.Add(obj);
			}
		}

		private void SolidsPc(OpsReport opsReport)
		{
			if (opsReport.Fluid.SolidsPc.Uom != null)
			{
				var obj = _mapper.Map<OpsReportSolidsPc>(opsReport.Fluid.SolidsPc);
				_db.OpsReportSolidsPcs.Add(obj);
			}
		}

		private void WaterPc(OpsReport opsReport)
		{
			if (opsReport.Fluid.WaterPc.Uom != null)
			{
				var obj = _mapper.Map<OpsReportWaterPc>(opsReport.Fluid.WaterPc);
				_db.OpsReportWaterPcs.Add(obj);
			}
		}

		private void OilPc(OpsReport opsReport)
		{
			if (opsReport.Fluid.OilPc.Uom != null)
			{
				var obj = _mapper.Map<OpsReportOilPc>(opsReport.Fluid.OilPc);
				_db.OpsReportOilPcs.Add(obj);
			}
		}

		private void SandPc(OpsReport opsReport)
		{
			if (opsReport.Fluid.SandPc.Uom != null)
			{
				var obj = _mapper.Map<OpsReportSandPc>(opsReport.Fluid.SandPc);
				_db.OpsReportSandPcs.Add(obj);
			}
		}

		private void SolidsLowGravPc(OpsReport opsReport)
		{
			if (opsReport.Fluid.SolidsLowGravPc.Uom != null)
			{
				var obj = _mapper.Map<OpsReportSolidsLowGravPc>(opsReport.Fluid.SolidsLowGravPc);
				_db.OpsReportSolidsLowGravPcs.Add(obj);
			}
		}
		private void SolidsCalcPc(OpsReport opsReport)
		{
			if (opsReport.Fluid.SolidsCalcPc.Uom != null)
			{
				var obj = _mapper.Map<OpsReportSolidsCalcPc>(opsReport.Fluid.SolidsCalcPc);
				_db.OpsReportSolidsCalcPcs.Add(obj);
			}
		}
		private void BaritePc(OpsReport opsReport)
		{
			if (opsReport.Fluid.BaritePc.Uom != null)
			{
				var obj = _mapper.Map<OpsReportBaritePc>(opsReport.Fluid.BaritePc);
				_db.OpsReportBaritePcs.Add(obj);
			}
		}
		private void Lcm(OpsReport opsReport)
		{
			if (opsReport.Fluid.Lcm.Uom != null)
			{
				var obj = _mapper.Map<OpsReportLcm>(opsReport.Fluid.Lcm);
				_db.OpsReportLcms.Add(obj);
			}
		}
		private void Mbt(OpsReport opsReport)
		{
			if (opsReport.Fluid.Mbt.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMbt>(opsReport.Fluid.Mbt);
				_db.OpsReportMbts.Add(obj);
			}
		}
		private void TempPh(OpsReport opsReport)
		{
			if (opsReport.Fluid.TempPh.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTempPh>(opsReport.Fluid.TempPh);
				_db.OpsReportTempPhs.Add(obj);
			}
		}
		private void Pm(OpsReport opsReport)
		{
			if (opsReport.Fluid.Pm.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPm>(opsReport.Fluid.Pm);
				_db.OpsReportPms.Add(obj);
			}
		}
		private void PmFiltrate(OpsReport opsReport)
		{
			if (opsReport.Fluid.PmFiltrate.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPmFiltrate>(opsReport.Fluid.PmFiltrate);
				_db.OpsReportPmFiltrates.Add(obj);
			}
		}
		private void Mf(OpsReport opsReport)
		{
			if (opsReport.Fluid.Mf.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMf>(opsReport.Fluid.Mf);
				_db.OpsReportMfs.Add(obj);
			}
		}
		private void AlkalinityP1(OpsReport opsReport)
		{
			if (opsReport.Fluid.AlkalinityP1.Uom != null)
			{
				var obj = _mapper.Map<OpsReportAlkalinityP1>(opsReport.Fluid.AlkalinityP1);
				_db.OpsReportAlkalinityP1s.Add(obj);
			}
		}
		private void AlkalinityP2(OpsReport opsReport)
		{
			if (opsReport.Fluid.AlkalinityP2.Uom != null)
			{
				var obj = _mapper.Map<OpsReportAlkalinityP2>(opsReport.Fluid.AlkalinityP2);
				_db.OpsReportAlkalinityP2s.Add(obj);
			}
		}
		private void Chloride(OpsReport opsReport)
		{
			if (opsReport.Fluid.Chloride.Uom != null)
			{
				var obj = _mapper.Map<OpsReportChloride>(opsReport.Fluid.Chloride);
				_db.OpsReportChlorides.Add(obj);
			}
		}
		private void Calcium(OpsReport opsReport)
		{
			if (opsReport.Fluid.Calcium.Uom != null)
			{
				var obj = _mapper.Map<OpsReportCalcium>(opsReport.Fluid.Calcium);
				_db.OpsReportCalciums.Add(obj);
			}
		}
		private void Magnesium(OpsReport opsReport)
		{
			if (opsReport.Fluid.Magnesium.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMagnesium>(opsReport.Fluid.Magnesium);
				_db.OpsReportMagnesiums.Add(obj);
			}
		}
		
		private void TempRheom(OpsReport opsReport)
		{
			foreach (var item in opsReport.Fluid.Rheometer)
			{

				if (item.TempRheom != null &&  item.TempRheom.Uom != null)
				{
					var obj = _mapper.Map<OpsReportTempRheom>(item.TempRheom);
					_db.OpsReportTempRheoms.Add(obj);
				}
			}
		}
		private void PresRheom(OpsReport opsReport)
		{
			foreach (var item in opsReport.Fluid.Rheometer)
			{

				if (item.PresRheom != null &&  item.PresRheom.Uom != null)
				{
					var obj = _mapper.Map<OpsReportPresRheom>(item.PresRheom);
					_db.OpsReportPresRheoms.Add(obj);
				}
			}
		}
		private void Rheometer(OpsReport opsReport)
		{
			foreach (var item in opsReport.Fluid.Rheometer)
			{

				if (item != null && item.Uid != null)
				{
					var obj = _mapper.Map<OpsReportRheometer>(item);
					_db.OpsReportRheometers.Add(obj);
				}
			}
		}
		private void BrinePc(OpsReport opsReport)
		{
			if (opsReport.Fluid.BrinePc.Uom != null)
			{
				var obj = _mapper.Map<OpsReportBrinePc>(opsReport.Fluid.BrinePc);
				_db.OpsReportBrinePcs.Add(obj);
			}
		}
		private void Lime(OpsReport opsReport)
		{
			if (opsReport.Fluid.Lime.Uom != null)
			{
				var obj = _mapper.Map<OpsReportLime>(opsReport.Fluid.Lime);
				_db.OpsReportLimes.Add(obj);
			}
		}
		private void ElectStab(OpsReport opsReport)
		{
			if (opsReport.Fluid.ElectStab.Uom != null)
			{
				var obj = _mapper.Map<OpsReportElectStab>(opsReport.Fluid.ElectStab);
				_db.OpsReportElectStabs.Add(obj);
			}
		}
		private void CalciumChloride(OpsReport opsReport)
		{
			if (opsReport.Fluid.CalciumChloride.Uom != null)
			{
				var obj = _mapper.Map<OpsReportCalciumChloride>(opsReport.Fluid.CalciumChloride);
				_db.OpsReportCalciumChlorides.Add(obj);
			}
		}
		private void SolidsHiGravPc(OpsReport opsReport)
		{
			if (opsReport.Fluid.SolidsHiGravPc.Uom != null)
			{
				var obj = _mapper.Map<OpsReportSolidsHiGravPc>(opsReport.Fluid.SolidsHiGravPc);
				_db.OpsReportSolidsHiGravPcs.Add(obj);
			}
		}
		private void Polymer(OpsReport opsReport)
		{
			if (opsReport.Fluid.Polymer.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPolymer>(opsReport.Fluid.Polymer);
				_db.OpsReportPolymers.Add(obj);
			}
		}
		private void SolCorPc(OpsReport opsReport)
		{
			if (opsReport.Fluid.SolCorPc.Uom != null)
			{
				var obj = _mapper.Map<OpsReportSolCorPc>(opsReport.Fluid.SolCorPc);
				_db.OpsReportSolCorPcs.Add(obj);
			}
		}
		private void OilCtg(OpsReport opsReport)
		{
			if (opsReport.Fluid.OilCtg.Uom != null)
			{
				var obj = _mapper.Map<OpsReportOilCtg>(opsReport.Fluid.OilCtg);
				_db.OpsReportOilCtgs.Add(obj);
			}
		}
		private void HardnessCa(OpsReport opsReport)
		{
			if (opsReport.Fluid.HardnessCa.Uom != null)
			{
				var obj = _mapper.Map<OpsReportHardnessCa>(opsReport.Fluid.HardnessCa);
				_db.OpsReportHardnessCas.Add(obj);
			}
		}
		private void Sulfide(OpsReport opsReport)
		{
			if (opsReport.Fluid.Sulfide.Uom != null)
			{
				var obj = _mapper.Map<OpsReportSulfide>(opsReport.Fluid.Sulfide);
				_db.OpsReportSulfides.Add(obj);
			}
		}
		private void Fluid(OpsReport opsReport)
		{
			if (opsReport.Fluid.Uid != null)
			{
				var obj = _mapper.Map<OpsReportFluid>(opsReport.Fluid);
				_db.OpsReportFluids.Add(obj);
			}
		}
		private void Pump(OpsReport opsReport)
		{
			foreach (var item in opsReport.Scr)
			{
				if (item.Pump != null && item.Pump.UidRef!= null)
				{
					var obj = _mapper.Map<OpsReportPump>(item.Pump);
					_db.OpsReportPumps.Add(obj);
				}
			}
		}
		private void RateStroke(OpsReport opsReport)
		{
			foreach (var item in opsReport.Scr)
			{
				if (item.RateStroke != null && item.RateStroke.Uom != null)
				{
					var obj = _mapper.Map<OpsReportRateStroke>(item.RateStroke);
					_db.OpsReportRateStrokes.Add(obj);
				}
			}
		}
		private void PresRecorded(OpsReport opsReport)
		{
			foreach (var item in opsReport.Scr)
			{
				if (item.PresRecorded != null && item.PresRecorded.Uom != null)
				{
					var obj = _mapper.Map<OpsReportPresRecorded>(item.PresRecorded);
					_db.OpsReportPresRecordeds.Add(obj);
				}
			}
		}
		private void MdBit(OpsReport opsReport)
		{
			foreach (var item in opsReport.Scr)
			{
				if (item.MdBit != null && item.MdBit.Uom != null)
				{
					var obj = _mapper.Map<OpsReportMdBit>(item.MdBit);
					_db.OpsReportMdBits.Add(obj);
				}
			}
		}
		private void Scr(OpsReport opsReport)
		{
			foreach (var item in opsReport.Scr)
			{
				if (item != null && item.Uid != null)
				{
					var obj = _mapper.Map<OpsReportScr>(item);
					_db.OpsReportScrs.Add(obj);
				}
			}
		}
		private void Pit(OpsReport opsReport)
		{
			foreach (var item in opsReport.PitVolume)
			{
				if (item != null && item.Pit.UidRef != null)
				{
					var obj = _mapper.Map<OpsReportPit>(item.Pit);
					_db.OpsReportPits.Add(obj);
				}
			}
		}
		private void VolPit(OpsReport opsReport)
		{
			foreach (var item in opsReport.PitVolume)
			{
				if (item.VolPit != null && item.VolPit.Uom != null)
				{
					var obj = _mapper.Map<OpsReportVolPit>(item.VolPit);
					_db.OpsReportVolPits.Add(obj);
				}
			}
		}
		private void DensFluid(OpsReport opsReport)
		{
			foreach (var item in opsReport.PitVolume)
			{
				if (item.DensFluid != null && item.DensFluid.Uom != null)
				{
					var obj = _mapper.Map<OpsReportDensFluid>(item.DensFluid);
					_db.OpsReportDensFluids.Add(obj);
				}
			}
		}
		private void PitVolume(OpsReport opsReport)
		{
			foreach (var item in opsReport.PitVolume)
			{
				if (item != null && item.Uid != null)
				{
					var obj = _mapper.Map<OpsReportPitVolume>(item);
					_db.OpsReportPitVolumes.Add(obj);
				}
			}
		}

		private void VolTotMudStart(OpsReport opsReport)
		{
				if (opsReport.MudVolume.VolTotMudStart.Uom != null)
				{
					var obj = _mapper.Map<OpsReportVolTotMudStart>(opsReport.MudVolume.VolTotMudStart);
					_db.OpsReportVolTotMudStarts.Add(obj);
				}
		}
		private void VolMudDumped(OpsReport opsReport)
		{
			if (opsReport.MudVolume.VolMudDumped.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolMudDumped>(opsReport.MudVolume.VolMudDumped);
				_db.OpsReportVolMudDumpeds.Add(obj);
			}
		}
		private void VolMudReceived(OpsReport opsReport)
		{
			if (opsReport.MudVolume.VolMudReceived.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolMudReceived>(opsReport.MudVolume.VolMudReceived);
				_db.OpsReportVolMudReceiveds.Add(obj);
			}
		}
		private void VolMudReturned(OpsReport opsReport)
		{
			if (opsReport.MudVolume.VolMudReturned.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolMudReturned>(opsReport.MudVolume.VolMudReturned);
				_db.OpsReportVolMudReturneds.Add(obj);
			}
		}
		private void VolLostShakerSurf(OpsReport opsReport)
		{
			if (opsReport.MudVolume.MudLosses.VolLostShakerSurf.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolLostShakerSurf>(opsReport.MudVolume.MudLosses.VolLostShakerSurf);
				_db.OpsReportVolLostShakerSurfs.Add(obj);
			}
		}
		private void VolLostMudCleanerSurf(OpsReport opsReport)
		{
			if (opsReport.MudVolume.MudLosses.VolLostMudCleanerSurf.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolLostMudCleanerSurf>(opsReport.MudVolume.MudLosses.VolLostMudCleanerSurf);
				_db.OpsReportVolLostMudCleanerSurfs.Add(obj);
			}
		}
		private void VolLostPitsSurf(OpsReport opsReport)
		{
			if (opsReport.MudVolume.MudLosses.VolLostPitsSurf.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolLostPitsSurf>(opsReport.MudVolume.MudLosses.VolLostPitsSurf);
				_db.OpsReportVolLostPitsSurfs.Add(obj);
			}
		}
		private void VolLostTrippingSurf(OpsReport opsReport)
		{
			if (opsReport.MudVolume.MudLosses.VolLostTrippingSurf.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolLostTrippingSurf>(opsReport.MudVolume.MudLosses.VolLostTrippingSurf);
				_db.OpsReportVolLostTrippingSurfs.Add(obj);
			}
		}
		private void VolLostOtherSurf(OpsReport opsReport)
		{
			if (opsReport.MudVolume.MudLosses.VolLostOtherSurf.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolLostOtherSurf>(opsReport.MudVolume.MudLosses.VolLostOtherSurf);
				_db.OpsReportVolLostOtherSurfs.Add(obj);
			}
		}
		private void VolTotMudLostSurf(OpsReport opsReport)
		{
			if (opsReport.MudVolume.MudLosses.VolTotMudLostSurf.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolTotMudLostSurf>(opsReport.MudVolume.MudLosses.VolTotMudLostSurf);
				_db.OpsReportVolTotMudLostSurfs.Add(obj);
			}
		}
		private void VolLostCircHole(OpsReport opsReport)
		{
			if (opsReport.MudVolume.MudLosses.VolLostCircHole.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolLostCircHole>(opsReport.MudVolume.MudLosses.VolLostCircHole);
				_db.OpsReportVolLostCircHoles.Add(obj);
			}
		}
		private void VolLostCsgHole(OpsReport opsReport)
		{
			if (opsReport.MudVolume.MudLosses.VolLostCsgHole.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolLostCsgHole>(opsReport.MudVolume.MudLosses.VolLostCsgHole);
				_db.OpsReportVolLostCsgHoles.Add(obj);
			}
		}
		private void VolLostCmtHole(OpsReport opsReport)
		{
			if (opsReport.MudVolume.MudLosses.VolLostCmtHole.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolLostCmtHole>(opsReport.MudVolume.MudLosses.VolLostCmtHole);
				_db.OpsReportVolLostCmtHoles.Add(obj);
			}
		}
		private void VolLostBhdCsgHole(OpsReport opsReport)
		{
			if (opsReport.MudVolume.MudLosses.VolLostBhdCsgHole.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolLostBhdCsgHole>(opsReport.MudVolume.MudLosses.VolLostBhdCsgHole);
				_db.OpsReportVolLostBhdCsgHoles.Add(obj);
			}
		}
		private void VolLostAbandonHole(OpsReport opsReport)
		{
			if (opsReport.MudVolume.MudLosses.VolLostAbandonHole.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolLostAbandonHole>(opsReport.MudVolume.MudLosses.VolLostAbandonHole);
				_db.OpsReportVolLostAbandonHoles.Add(obj);
			}
		}
		private void VolLostOtherHole(OpsReport opsReport)
		{
			if (opsReport.MudVolume.MudLosses.VolLostOtherHole.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolLostOtherHole>(opsReport.MudVolume.MudLosses.VolLostOtherHole);
				_db.OpsReportVolLostOtherHoles.Add(obj);
			}
		}
		private void VolTotMudLostHole(OpsReport opsReport)
		{
			if (opsReport.MudVolume.MudLosses.VolTotMudLostHole.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolTotMudLostHole>(opsReport.MudVolume.MudLosses.VolTotMudLostHole);
				_db.OpsReportVolTotMudLostHoles.Add(obj);
			}
		}
		private void MudLosses(OpsReport opsReport)
		{
			if (opsReport.MudVolume.MudLosses != null)
			{
				var obj = _mapper.Map<OpsReportMudLosses>(opsReport.MudVolume.MudLosses);
				_db.OpsReportMudLossess.Add(obj);
			}
		}
		private void VolMudBuilt(OpsReport opsReport)
		{
			if (opsReport.MudVolume.VolMudBuilt.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolMudBuilt>(opsReport.MudVolume.VolMudBuilt);
				_db.OpsReportVolMudBuilts.Add(obj);
			}
		}
		private void VolMudString(OpsReport opsReport)
		{
			if (opsReport.MudVolume.VolMudString.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolMudString>(opsReport.MudVolume.VolMudString);
				_db.OpsReportVolMudStrings.Add(obj);
			}
		}
		private void VolMudCasing(OpsReport opsReport)
		{
			if (opsReport.MudVolume.VolMudCasing.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolMudCasing>(opsReport.MudVolume.VolMudCasing);
				_db.OpsReportVolMudCasings.Add(obj);
			}
		}
		private void VolMudHole(OpsReport opsReport)
		{
			if (opsReport.MudVolume.VolMudHole.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolMudHole>(opsReport.MudVolume.VolMudHole);
				_db.OpsReportVolMudHoles.Add(obj);
			}
		}
		private void VolMudRiser(OpsReport opsReport)
		{
			if (opsReport.MudVolume.VolMudRiser.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolMudRiser>(opsReport.MudVolume.VolMudRiser);
				_db.OpsReportVolMudRisers.Add(obj);
			}
		}
		private void VolTotMudEnd(OpsReport opsReport)
		{
			if (opsReport.MudVolume.VolTotMudEnd.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolTotMudEnd>(opsReport.MudVolume.VolTotMudEnd);
				_db.OpsReportVolTotMudEnds.Add(obj);
			}
		}

		private void MudVolume(OpsReport opsReport)
		{
			if (opsReport.MudVolume != null)
			{
				var obj = _mapper.Map<OpsReportMudVolume>(opsReport.MudVolume);
				_db.OpsReportMudVolumes.Add(obj);
			}
		}
		
		private void ItemWtPerUnit(OpsReport opsReport)
		{
			if (opsReport.MudInventory.ItemWtPerUnit.Uom != null)
			{
				var obj = _mapper.Map<OpsReportItemWtPerUnit>(opsReport.MudInventory.ItemWtPerUnit);
				_db.OpsReportItemWtPerUnits.Add(obj);
			}
		}
		private void PricePerUnit(OpsReport opsReport)
		{
			if (opsReport.MudInventory.PricePerUnit.PricePerUnitId != 0)
			{
				var obj = _mapper.Map<OpsReportPricePerUnit>(opsReport.MudInventory.PricePerUnit);
				_db.OpsReportPricePerUnits.Add(obj);
			}
		}
		private void CostItem(OpsReport opsReport)
		{
			if (opsReport.MudInventory.CostItem.CostItemId != 0)
			{
				var obj = _mapper.Map<OpsReportCostItem>(opsReport.MudInventory.CostItem);
				_db.OpsReportCostItems.Add(obj);
			}
		}
		private void MudInventory(OpsReport opsReport)
		{
			if (opsReport.MudInventory.Uid != null)
			{
				var obj = _mapper.Map<OpsReportMudInventory>(opsReport.MudInventory);
				_db.OpsReportMudInventorys.Add(obj);
			}
		}
		private void ItemVolPerUnit(OpsReport opsReport)
		{
			if (opsReport.Bulk.ItemVolPerUnit.Uom != null)
			{
				var obj = _mapper.Map<OpsReportItemVolPerUnit>(opsReport.Bulk.ItemVolPerUnit);
				_db.OpsReportItemVolPerUnits.Add(obj);
			}
		}
		private void Bulk(OpsReport opsReport)
		{
			if (opsReport.Bulk.Uid != null)
			{
				var obj = _mapper.Map<OpsReportBulk>(opsReport.Bulk);
				_db.OpsReportBulks.Add(obj);
			}
		}
		private void AnchorTension(OpsReport opsReport)
		{
			foreach (var item in opsReport.RigResponse.AnchorTension)
			{
				if (item != null && item.Uom != null)
				{
					var obj = _mapper.Map<OpsReportAnchorTension>(item);
					_db.OpsReportAnchorTensions.Add(obj);
				}
			}
		}
		private void AnchorAngle(OpsReport opsReport)
		{
			foreach (var item in opsReport.RigResponse.AnchorAngle)
			{
				if (item != null && item.Uom != null)
				{
					var obj = _mapper.Map<OpsReportAnchorAngle>(item);
					_db.OpsReportAnchorAngles.Add(obj);
				}
			}
		}
		private void RigHeading(OpsReport opsReport)
		{
				if (opsReport.RigResponse.RigHeading.Uom != null)
				{
					var obj = _mapper.Map<OpsReportRigHeading>(opsReport.RigResponse.RigHeading);
					_db.OpsReportRigHeadings.Add(obj);
				}
		}
		private void RigHeave(OpsReport opsReport)
		{
			if (opsReport.RigResponse.RigHeave.Uom != null)
			{
				var obj = _mapper.Map<OpsReportRigHeave>(opsReport.RigResponse.RigHeave);
				_db.OpsReportRigHeaves.Add(obj);
			}
		}
		private void RigPitchAngle(OpsReport opsReport)
		{
			if (opsReport.RigResponse.RigPitchAngle.Uom != null)
			{
				var obj = _mapper.Map<OpsReportRigPitchAngle>(opsReport.RigResponse.RigPitchAngle);
				_db.OpsReportRigPitchAngles.Add(obj);
			}
		}
		private void RigRollAngle(OpsReport opsReport)
		{
			if (opsReport.RigResponse.RigRollAngle.Uom != null)
			{
				var obj = _mapper.Map<OpsReportRigRollAngle>(opsReport.RigResponse.RigRollAngle);
				_db.OpsReportRigRollAngles.Add(obj);
			}
		}
		private void RiserAngle(OpsReport opsReport)
		{
			if (opsReport.RigResponse.RiserAngle.Uom != null)
			{
				var obj = _mapper.Map<OpsReportRiserAngle>(opsReport.RigResponse.RiserAngle);
				_db.OpsReportRiserAngles.Add(obj);
			}
		}
		private void RiserDirection(OpsReport opsReport)
		{
			if (opsReport.RigResponse.RiserDirection.Uom != null)
			{
				var obj = _mapper.Map<OpsReportRiserDirection>(opsReport.RigResponse.RiserDirection);
				_db.OpsReportRiserDirections.Add(obj);
			}
		}
		private void RiserTension(OpsReport opsReport)
		{
			if (opsReport.RigResponse.RiserTension.Uom != null)
			{
				var obj = _mapper.Map<OpsReportRiserTension>(opsReport.RigResponse.RiserTension);
				_db.OpsReportRiserTensions.Add(obj);
			}
		}
		private void VariableDeckLoad(OpsReport opsReport)
		{
			if (opsReport.RigResponse.VariableDeckLoad.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVariableDeckLoad>(opsReport.RigResponse.VariableDeckLoad);
				_db.OpsReportVariableDeckLoads.Add(obj);
			}
		}
		private void TotalDeckLoad(OpsReport opsReport)
		{
			if (opsReport.RigResponse.TotalDeckLoad.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTotalDeckLoad>(opsReport.RigResponse.TotalDeckLoad);
				_db.OpsReportTotalDeckLoads.Add(obj);
			}
		}
		private void GuideBaseAngle(OpsReport opsReport)
		{
			if (opsReport.RigResponse.GuideBaseAngle.Uom != null)
			{
				var obj = _mapper.Map<OpsReportGuideBaseAngle>(opsReport.RigResponse.GuideBaseAngle);
				_db.OpsReportGuideBaseAngles.Add(obj);
			}
		}
		private void BallJointAngle(OpsReport opsReport)
		{
			if (opsReport.RigResponse.BallJointAngle.Uom != null)
			{
				var obj = _mapper.Map<OpsReportBallJointAngle>(opsReport.RigResponse.BallJointAngle);
				_db.OpsReportBallJointAngles.Add(obj);
			}
		}
		private void BallJointDirection(OpsReport opsReport)
		{
			if (opsReport.RigResponse.BallJointDirection.Uom != null)
			{
				var obj = _mapper.Map<OpsReportBallJointDirection>(opsReport.RigResponse.BallJointDirection);
				_db.OpsReportBallJointDirections.Add(obj);
			}
		}
		private void OffsetRig(OpsReport opsReport)
		{
			if (opsReport.RigResponse.OffsetRig.Uom != null)
			{
				var obj = _mapper.Map<OpsReportOffsetRig>(opsReport.RigResponse.OffsetRig);
				_db.OpsReportOffsetRigs.Add(obj);
			}
		}
		private void LoadLeg1(OpsReport opsReport)
		{
			if (opsReport.RigResponse.LoadLeg1.Uom != null)
			{
				var obj = _mapper.Map<OpsReportLoadLeg1>(opsReport.RigResponse.LoadLeg1);
				_db.OpsReportLoadLeg1s.Add(obj);
			}
		}
		private void LoadLeg2(OpsReport opsReport)
		{
			if (opsReport.RigResponse.LoadLeg2.Uom != null)
			{
				var obj = _mapper.Map<OpsReportLoadLeg2>(opsReport.RigResponse.LoadLeg2);
				_db.OpsReportLoadLeg2s.Add(obj);
			}
		}
		private void LoadLeg3(OpsReport opsReport)
		{
			if (opsReport.RigResponse.LoadLeg3.Uom != null)
			{
				var obj = _mapper.Map<OpsReportLoadLeg3>(opsReport.RigResponse.LoadLeg3);
				_db.OpsReportLoadLeg3s.Add(obj);
			}
		}
		private void LoadLeg4(OpsReport opsReport)
		{
			if (opsReport.RigResponse.LoadLeg4.Uom != null)
			{
				var obj = _mapper.Map<OpsReportLoadLeg4>(opsReport.RigResponse.LoadLeg4);
				_db.OpsReportLoadLeg4s.Add(obj);
			}
		}
		private void PenetrationLeg1(OpsReport opsReport)
		{
			if (opsReport.RigResponse.PenetrationLeg1.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPenetrationLeg1>(opsReport.RigResponse.PenetrationLeg1);
				_db.OpsReportPenetrationLeg1s.Add(obj);
			}
		}
		private void PenetrationLeg2(OpsReport opsReport)
		{
			if (opsReport.RigResponse.PenetrationLeg2.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPenetrationLeg2>(opsReport.RigResponse.PenetrationLeg2);
				_db.OpsReportPenetrationLeg2s.Add(obj);
			}
		}
		private void PenetrationLeg3(OpsReport opsReport)
		{
			if (opsReport.RigResponse.PenetrationLeg3.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPenetrationLeg3>(opsReport.RigResponse.PenetrationLeg3);
				_db.OpsReportPenetrationLeg3s.Add(obj);
			}
		}
		private void PenetrationLeg4(OpsReport opsReport)
		{
			if (opsReport.RigResponse.PenetrationLeg4.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPenetrationLeg4>(opsReport.RigResponse.PenetrationLeg4);
				_db.OpsReportPenetrationLeg4s.Add(obj);
			}
		}
		private void DispRig(OpsReport opsReport)
		{
			if (opsReport.RigResponse.DispRig.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDispRig>(opsReport.RigResponse.DispRig);
				_db.OpsReportDispRigs.Add(obj);
			}
		}
		private void MeanDraft(OpsReport opsReport)
		{
			if (opsReport.RigResponse.MeanDraft.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMeanDraft>(opsReport.RigResponse.MeanDraft);
				_db.OpsReportMeanDrafts.Add(obj);
			}
		}
		private void RigResponse(OpsReport opsReport)
		{
			if (opsReport.RigResponse != null)
			{
				var obj = _mapper.Map<OpsReportRigResponse>(opsReport.RigResponse);
				_db.OpsReportRigResponses.Add(obj);
			}
		}
		private void IdLiner(OpsReport opsReport)
		{
			if (opsReport.PumpOp.IdLiner.Uom != null)
			{
				var obj = _mapper.Map<OpsReportIdLiner>(opsReport.PumpOp.IdLiner);
				_db.OpsReportIdLiners.Add(obj);
			}
		}
		private void LenStroke(OpsReport opsReport)
		{
			if (opsReport.PumpOp.LenStroke.Uom != null)
			{
				var obj = _mapper.Map<OpsReportLenStroke>(opsReport.PumpOp.LenStroke);
				_db.OpsReportLenStrokes.Add(obj);
			}
		}
		private void Pressure(OpsReport opsReport)
		{
			if (opsReport.PumpOp.Pressure.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPressure>(opsReport.PumpOp.Pressure);
				_db.OpsReportPressures.Add(obj);
			}
		}
		private void PcEfficiency(OpsReport opsReport)
		{
			if (opsReport.PumpOp.PcEfficiency.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPcEfficiency>(opsReport.PumpOp.PcEfficiency);
				_db.OpsReportPcEfficiencys.Add(obj);
			}
		}
		private void PumpOutput(OpsReport opsReport)
		{
			if (opsReport.PumpOp.PumpOutput.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPumpOutput>(opsReport.PumpOp.PumpOutput);
				_db.OpsReportPumpOutputs.Add(obj);
			}
		}
		private void PumpOp(OpsReport opsReport)
		{
			if (opsReport.PumpOp.Uid != null)
			{
				var obj = _mapper.Map<OpsReportPumpOp>(opsReport.PumpOp);
				_db.OpsReportPumpOps.Add(obj);
			}
		}
		private void Shaker(OpsReport opsReport)
		{
			if (opsReport.ShakerOp.Shaker.UidRef != null)
			{
				var obj = _mapper.Map<OpsReportShaker>(opsReport.ShakerOp.Shaker);
				_db.OpsReportShakers.Add(obj);
			}
		}
		private void MdHole(OpsReport opsReport)
		{
			if (opsReport.ShakerOp.MdHole.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMdHole>(opsReport.ShakerOp.MdHole);
				_db.OpsReportMdHoles.Add(obj);
			}
		}
		private void HoursRun(OpsReport opsReport)
		{
			if (opsReport.ShakerOp.HoursRun.Uom != null)
			{
				var obj = _mapper.Map<OpsReportHoursRun>(opsReport.ShakerOp.HoursRun);
				_db.OpsReportHoursRuns.Add(obj);
			}
		}
		private void PcScreenCovered(OpsReport opsReport)
		{
			if (opsReport.ShakerOp.PcScreenCovered.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPcScreenCovered>(opsReport.ShakerOp.PcScreenCovered);
				_db.OpsReportPcScreenCovereds.Add(obj);
			}
		}
		private void MeshX(OpsReport opsReport)
		{
			if (opsReport.ShakerOp.ShakerScreen.MeshX.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMeshX>(opsReport.ShakerOp.ShakerScreen.MeshX);
				_db.OpsReportMeshXs.Add(obj);
			}
		}
		private void MeshY(OpsReport opsReport)
		{
			if (opsReport.ShakerOp.ShakerScreen.MeshY.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMeshY>(opsReport.ShakerOp.ShakerScreen.MeshY);
				_db.OpsReportMeshYs.Add(obj);
			}
		}
		private void CutPoint(OpsReport opsReport)
		{
			if (opsReport.ShakerOp.ShakerScreen.CutPoint.Uom != null)
			{
				var obj = _mapper.Map<OpsReportCutPoint>(opsReport.ShakerOp.ShakerScreen.CutPoint);
				_db.OpsReportCutPoints.Add(obj);
			}
		}

		private void ShakerScreen(OpsReport opsReport)
		{
			if (opsReport.ShakerOp.ShakerScreen!= null)
			{
				var obj = _mapper.Map<OpsReportShakerScreen>(opsReport.ShakerOp.ShakerScreen);
				_db.OpsReportShakerScreens.Add(obj);
			}
		}
		private void ShakerOp(OpsReport opsReport)
		{
			if (opsReport.ShakerOp.Uid != null)
			{
				var obj = _mapper.Map<OpsReportShakerOp>(opsReport.ShakerOp);
				_db.OpsReportShakerOps.Add(obj);
			}
		}
		private void DaysIncFree(OpsReport opsReport)
		{
			if (opsReport.Hse.DaysIncFree.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDaysIncFree>(opsReport.Hse.DaysIncFree);
				_db.OpsReportDaysIncFrees.Add(obj);
			}
		}
		 private void ETimLostGross(OpsReport opsReport)
		{
			if (opsReport.Hse.Incident.ETimLostGross.Uom != null)
			{
				var obj = _mapper.Map<OpsReportETimLostGross>(opsReport.Hse.Incident.ETimLostGross);
				_db.OpsReportETimLostGrosss.Add(obj);
			}
		}
		private void CostLostGross(OpsReport opsReport)
		{
			if (opsReport.Hse.Incident.CostLostGross != null)
			{
				var obj = _mapper.Map<OpsReportCostLostGross>(opsReport.Hse.Incident.CostLostGross);
				_db.OpsReportCostLostGrosss.Add(obj);
			}
		}
		private void Incident(OpsReport opsReport)
		{
			if (opsReport.Hse.Incident.Uid != null)
			{
				var obj = _mapper.Map<OpsReportIncident>(opsReport.Hse.Incident);
				_db.OpsReportIncidents.Add(obj);
			}
		}
		private void PresLastCsg(OpsReport opsReport)
		{
			if (opsReport.Hse.PresLastCsg.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPresLastCsg>(opsReport.Hse.PresLastCsg);
				_db.OpsReportPresLastCsgs.Add(obj);
			}
		}
		private void PresStdPipe(OpsReport opsReport)
		{
			if (opsReport.Hse.PresStdPipe.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPresStdPipe>(opsReport.Hse.PresStdPipe);
				_db.OpsReportPresStdPipes.Add(obj);
			}
		}
		private void PresKellyHose(OpsReport opsReport)
		{
			if (opsReport.Hse.PresKellyHose.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPresKellyHose>(opsReport.Hse.PresKellyHose);
				_db.OpsReportPresKellyHoses.Add(obj);
			}
		}
		private void PresDiverter(OpsReport opsReport)
		{
			if (opsReport.Hse.PresDiverter.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPresDiverter>(opsReport.Hse.PresDiverter);
				_db.OpsReportPresDiverters.Add(obj);
			}
		}
		private void PresAnnular(OpsReport opsReport)
		{
			if (opsReport.Hse.PresAnnular.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPresAnnular>(opsReport.Hse.PresAnnular);
				_db.OpsReportPresAnnulars.Add(obj);
			}
		}
		private void PresRams(OpsReport opsReport)
		{
			if (opsReport.Hse.PresRams.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPresRams>(opsReport.Hse.PresRams);
				_db.OpsReportPresRamss.Add(obj);
			}
		}
		private void PresChokeLine(OpsReport opsReport)
		{
			if (opsReport.Hse.PresChokeLine.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPresChokeLine>(opsReport.Hse.PresChokeLine);
				_db.OpsReportPresChokeLines.Add(obj);
			}
		}
		private void PresChokeMan(OpsReport opsReport)
		{
			if (opsReport.Hse.PresChokeMan.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPresChokeMan>(opsReport.Hse.PresChokeMan);
				_db.OpsReportPresChokeMans.Add(obj);
			}
		}
		private void FluidDischarged(OpsReport opsReport)
		{
			if (opsReport.Hse.FluidDischarged.Uom != null)
			{
				var obj = _mapper.Map<OpsReportFluidDischarged>(opsReport.Hse.FluidDischarged);
				_db.OpsReportFluidDischargeds.Add(obj);
			}
		}
		private void VolCtgDischarged(OpsReport opsReport)
		{
			if (opsReport.Hse.VolCtgDischarged.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolCtgDischarged>(opsReport.Hse.VolCtgDischarged);
				_db.OpsReportVolCtgDischargeds.Add(obj);
			}
		}

		private void VolOilCtgDischarge(OpsReport opsReport)
		{
			if (opsReport.Hse.VolOilCtgDischarge.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolOilCtgDischarge>(opsReport.Hse.VolOilCtgDischarge);
				_db.OpsReportVolOilCtgDischarges.Add(obj);
			}
		}

		private void WasteDischarged(OpsReport opsReport)
		{
			if (opsReport.Hse.WasteDischarged.Uom != null)
			{
				var obj = _mapper.Map<OpsReportWasteDischarged>(opsReport.Hse.WasteDischarged);
				_db.OpsReportWasteDischargeds.Add(obj);
			}
		}
		private void Hse(OpsReport opsReport)
		{
			if (opsReport.Hse != null)
			{
				var obj = _mapper.Map<OpsReportHse>(opsReport.Hse);
				_db.OpsReportHses.Add(obj);
			}
		}
		private void TotalTime(OpsReport opsReport)
		{
			foreach (var item in opsReport.Personnel)
			{
				if (item.TotalTime != null && item.TotalTime.Uom != null)
				{
					var obj = _mapper.Map<OpsReportTotalTime>(item.TotalTime);
					_db.OpsReportTotalTimes.Add(obj);
				}
			}
		}
		private void Personnel(OpsReport opsReport)
		{
			foreach (var item in opsReport.Personnel)
			{
				if (item != null && item.Uid != null)
				{
					var obj = _mapper.Map<OpsReportPersonnel>(item);
					_db.OpsReportPersonnels.Add(obj);
				}
			}
		}
		private void SupportCraft(OpsReport opsReport)
		{
			if (opsReport.SupportCraft.Uid != null)
			{
				var obj = _mapper.Map<OpsReportSupportCraft>(opsReport.SupportCraft);
				_db.OpsReportSupportCrafts.Add(obj);
			}
		}
		private void BarometricPressure(OpsReport opsReport)
		{
			if (opsReport.Weather.BarometricPressure.Uom != null)
			{
				var obj = _mapper.Map<OpsReportBarometricPressure>(opsReport.Weather.BarometricPressure);
				_db.OpsReportBarometricPressures.Add(obj);
			}
		}
		private void TempSurfaceMn(OpsReport opsReport)
		{
			if (opsReport.Weather.TempSurfaceMn.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTempSurfaceMn>(opsReport.Weather.TempSurfaceMn);
				_db.OpsReportTempSurfaceMns.Add(obj);
			}
		}
		private void TempSurfaceMx(OpsReport opsReport)
		{
			if (opsReport.Weather.TempSurfaceMx.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTempSurfaceMx>(opsReport.Weather.TempSurfaceMx);
				_db.OpsReportTempSurfaceMxs.Add(obj);
			}
		}
		private void TempWindChill(OpsReport opsReport)
		{
			if (opsReport.Weather.TempWindChill.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTempWindChill>(opsReport.Weather.TempWindChill);
				_db.OpsReportTempWindChills.Add(obj);
			}
		}
		private void Tempsea(OpsReport opsReport)
		{
			if (opsReport.Weather.Tempsea.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTempsea>(opsReport.Weather.Tempsea);
				_db.OpsReportTempseas.Add(obj);
			}
		}
		private void Visibility(OpsReport opsReport)
		{
			if (opsReport.Weather.Visibility.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVisibility>(opsReport.Weather.Visibility);
				_db.OpsReportVisibilitys.Add(obj);
			}
		}
		private void AziWave(OpsReport opsReport)
		{
			if (opsReport.Weather.AziWave.Uom != null)
			{
				var obj = _mapper.Map<OpsReportAziWave>(opsReport.Weather.AziWave);
				_db.OpsReportAziWaves.Add(obj);
			}
		}
		private void HtWave(OpsReport opsReport)
		{
			if (opsReport.Weather.HtWave.Uom != null)
			{
				var obj = _mapper.Map<OpsReportHtWave>(opsReport.Weather.HtWave);
				_db.OpsReportHtWaves.Add(obj);
			}
		}
		private void PeriodWave(OpsReport opsReport)
		{
			if (opsReport.Weather.PeriodWave.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPeriodWave>(opsReport.Weather.PeriodWave);
				_db.OpsReportPeriodWaves.Add(obj);
			}
		}
		private void AziWind(OpsReport opsReport)
		{
			if (opsReport.Weather.AziWind.Uom != null)
			{
				var obj = _mapper.Map<OpsReportAziWind>(opsReport.Weather.AziWind);
				_db.OpsReportAziWinds.Add(obj);
			}
		}
		private void VelWind(OpsReport opsReport)
		{
			if (opsReport.Weather.VelWind.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVelWind>(opsReport.Weather.VelWind);
				_db.OpsReportVelWinds.Add(obj);
			}
		}
		private void AmtPrecip(OpsReport opsReport)
		{
			if (opsReport.Weather.AmtPrecip.Uom != null)
			{
				var obj = _mapper.Map<OpsReportAmtPrecip>(opsReport.Weather.AmtPrecip);
				_db.OpsReportAmtPrecips.Add(obj);
			}
		}
		private void CeilingCloud(OpsReport opsReport)
		{
			if (opsReport.Weather.CeilingCloud.Uom != null)
			{
				var obj = _mapper.Map<OpsReportCeilingCloud>(opsReport.Weather.CeilingCloud);
				_db.OpsReportCeilingClouds.Add(obj);
			}
		}
		private void CurrentSea(OpsReport opsReport)
		{
			if (opsReport.Weather.CurrentSea.Uom != null)
			{
				var obj = _mapper.Map<OpsReportCurrentSea>(opsReport.Weather.CurrentSea);
				_db.OpsReportCurrentSeas.Add(obj);
			}
		}
		private void AziCurrentSea(OpsReport opsReport)
		{
			if (opsReport.Weather.AziCurrentSea.Uom != null)
			{
				var obj = _mapper.Map<OpsReportAziCurrentSea>(opsReport.Weather.AziCurrentSea);
				_db.OpsReportAziCurrentSeas.Add(obj);
			}
		}
		private void Weather(OpsReport opsReport)
		{
			if (opsReport.Weather.Uid != null)
			{
				var obj = _mapper.Map<OpsReportWeather>(opsReport.Weather);
				_db.OpsReportWeathers.Add(obj);
			}
		}
		private void CostDay(OpsReport opsReport)
		{
			if (opsReport.CostDay != null)
			{
				var obj = _mapper.Map<OpsReportCostDay>(opsReport.CostDay);
				_db.OpsReportCostDays.Add(obj);
			}
		}
		private void CostDayMud(OpsReport opsReport)
		{
			if (opsReport.CostDayMud != null)
			{
				var obj = _mapper.Map<OpsReportCostDayMud>(opsReport.CostDayMud);
				_db.OpsReportCostDayMuds.Add(obj);
			}
		}
		private void DiaHole(OpsReport opsReport)
		{
			if (opsReport.DiaHole.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDiaHole>(opsReport.DiaHole);
				_db.OpsReportDiaHoles.Add(obj);
			}
		}
		private void DiaCsgLast(OpsReport opsReport)
		{
			if (opsReport.DiaCsgLast.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDiaCsgLast>(opsReport.DiaCsgLast);
				_db.OpsReportDiaCsgLasts.Add(obj);
			}
		}
		private void MdCsgLast(OpsReport opsReport)
		{
			if (opsReport.MdCsgLast.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMdCsgLast>(opsReport.MdCsgLast);
				_db.OpsReportMdCsgLasts.Add(obj);
			}
		}
		private void TvdCsgLast(OpsReport opsReport)
		{
			if (opsReport.TvdCsgLast.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTvdCsgLast>(opsReport.TvdCsgLast);
				_db.OpsReportTvdCsgLasts.Add(obj);
			}
		}
		private void TvdLot(OpsReport opsReport)
		{
			if (opsReport.TvdLot.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTvdLot>(opsReport.TvdLot);
				_db.OpsReportTvdLots.Add(obj);
			}
		}
		private void PresLotEmw(OpsReport opsReport)
		{
			if (opsReport.PresLotEmw.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPresLotEmw>(opsReport.PresLotEmw);
				_db.OpsReportPresLotEmws.Add(obj);
			}
		}
		private void PresKickTol(OpsReport opsReport)
		{
			if (opsReport.PresKickTol.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPresKickTol>(opsReport.PresKickTol);
				_db.OpsReportPresKickTols.Add(obj);
			}
		}
		private void VolKickTol(OpsReport opsReport)
		{
			if (opsReport.VolKickTol.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolKickTol>(opsReport.VolKickTol);
				_db.OpsReportVolKickTols.Add(obj);
			}
		}
		private void Maasp(OpsReport opsReport)
		{
			if (opsReport.Maasp.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMaasp>(opsReport.Maasp);
				_db.OpsReportMaasps.Add(obj);
			}
		}
		private void CommonData(OpsReport opsReport)
		{
			if (opsReport.CommonData != null)
			{
				var obj = _mapper.Map<OpsReportsCommonData>(opsReport.CommonData);
				_db.OpsReportsCommonDatas.Add(obj);
			}
		}
		#endregion Insert OpsReport


		#region Update OpsReport
		private void UpdateRig(OpsReport opsReport)
		{
			if (opsReport.Rig.UidRef != null)
			{
				var obj = _mapper.Map<OpsReportRig>(opsReport.Rig);
				_db.OpsReportRigs.Update(obj);
			}
		}
		private void UpdateETimStart(OpsReport opsReport)
		{
			if (opsReport.ETimStart.Uom != null)
			{
				var obj = _mapper.Map<OpsReportETimStart>(opsReport.ETimStart);
				_db.OpsReportETimStarts.Update(obj);
			}
		}
		private void UpdateETimSpud(OpsReport opsReport)
		{
			if (opsReport.ETimSpud.Uom != null)
			{
				var obj = _mapper.Map<OpsReportETimSpud>(opsReport.ETimSpud);
				_db.OpsReportETimSpuds.Update(obj);
			}
		}
		private void UpdateETimLoc(OpsReport opsReport)
		{
			if (opsReport.ETimLoc.Uom != null)
			{
				var obj = _mapper.Map<OpsReportETimLoc>(opsReport.ETimLoc);
				_db.OpsReportETimLocs.Update(obj);
			}
		}
		private void UpdateMdReport(OpsReport opsReport)
		{
			if (opsReport.MdReport.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMdReport>(opsReport.MdReport);
				_db.OpsReportMdReports.Update(obj);
			}
		}
		private void UpdateTvdReport(OpsReport opsReport)
		{
			if (opsReport.TvdReport.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTvdReport>(opsReport.TvdReport);
				_db.OpsReportTvdReports.Update(obj);
			}
		}
		private void UpdateDistDrill(OpsReport opsReport)
		{
			if (opsReport.DistDrill.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDistDrill>(opsReport.DistDrill);
				_db.OpsReportDistDrills.Update(obj);
			}
		}
		private void UpdateETimDrill(OpsReport opsReport)
		{
			if (opsReport.ETimDrill.Uom != null)
			{
				var obj = _mapper.Map<OpsReportETimDrill>(opsReport.ETimDrill);
				_db.OpsReportETimDrills.Update(obj);
			}
		}
		private void UpdateMdPlanned(OpsReport opsReport)
		{
			if (opsReport.MdPlanned.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMdPlanned>(opsReport.MdPlanned);
				_db.OpsReportMdPlanneds.Update(obj);
			}
		}
		private void UpdateRopAv(OpsReport opsReport)
		{
			if (opsReport.RopAv.Uom != null)
			{
				var obj = _mapper.Map<OpsReportRopAv>(opsReport.RopAv);
				_db.OpsReportRopAvs.Update(obj);
			}
		}
		private void UpdateRopCurrent(OpsReport opsReport)
		{
			if (opsReport.RopCurrent.Uom != null)
			{
				var obj = _mapper.Map<OpsReportRopCurrent>(opsReport.RopCurrent);
				_db.OpsReportRopCurrents.Update(obj);
			}
		}
		private void UpdateETimDrillRot(OpsReport opsReport)
		{
			if (opsReport.ETimDrillRot.Uom != null)
			{
				var obj = _mapper.Map<OpsReportETimDrillRot>(opsReport.ETimDrillRot);
				_db.OpsReportETimDrillRots.Update(obj);
			}
		}
		private void UpdateETimDrillSlid(OpsReport opsReport)
		{
			if (opsReport.ETimDrillSlid.Uom != null)
			{
				var obj = _mapper.Map<OpsReportETimDrillSlid>(opsReport.ETimDrillSlid);
				_db.OpsReportETimDrillSlids.Update(obj);
			}
		}
		private void UpdateETimCirc(OpsReport opsReport)
		{
			if (opsReport.ETimCirc.Uom != null)
			{
				var obj = _mapper.Map<OpsReportETimCirc>(opsReport.ETimCirc);
				_db.OpsReportETimCircs.Update(obj);
			}
		}
		private void UpdateETimReam(OpsReport opsReport)
		{
			if (opsReport.ETimReam.Uom != null)
			{
				var obj = _mapper.Map<OpsReportETimReam>(opsReport.ETimReam);
				_db.OpsReportETimReams.Update(obj);
			}
		}

		private void UpdateETimHold(OpsReport opsReport)
		{
			if (opsReport.ETimHold.Uom != null)
			{
				var obj = _mapper.Map<OpsReportETimHold>(opsReport.ETimHold);
				_db.OpsReportETimHolds.Update(obj);
			}
		}
		private void UpdateETimSteering(OpsReport opsReport)
		{
			if (opsReport.ETimSteering.Uom != null)
			{
				var obj = _mapper.Map<OpsReportETimSteering>(opsReport.ETimSteering);
				_db.OpsReportETimSteerings.Update(obj);
			}
		}
		private void UpdateDistDrillRot(OpsReport opsReport)
		{
			if (opsReport.DistDrillRot.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDistDrillRot>(opsReport.DistDrillRot);
				_db.OpsReportDistDrillRots.Update(obj);
			}
			if (opsReport.DrillingParams.DistDrillRot.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDistDrillRot>(opsReport.DrillingParams.DistDrillRot);
				_db.OpsReportDistDrillRots.Update(obj);
			}
		}
		private void UpdateDistDrillSlid(OpsReport opsReport)
		{
			if (opsReport.DistDrillSlid.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDistDrillSlid>(opsReport.DistDrillSlid);
				_db.OpsReportDistDrillSlids.Update(obj);
			}
			if (opsReport.DrillingParams.DistDrillSlid.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDistDrillSlid>(opsReport.DrillingParams.DistDrillSlid);
				_db.OpsReportDistDrillSlids.Update(obj);
			}
		}

		private void UpdateDistReam(OpsReport opsReport)
		{
			if (opsReport.DistReam.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDistReam>(opsReport.DistReam);
				_db.OpsReportDistReams.Update(obj);
			}
			if (opsReport.DrillingParams.DistReam.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDistReam>(opsReport.DrillingParams.DistReam);
				_db.OpsReportDistReams.Update(obj);
			}
		}
		private void UpdateDistHold(OpsReport opsReport)
		{
			if (opsReport.DistHold.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDistHold>(opsReport.DistHold);
				_db.OpsReportDistHolds.Update(obj);
			}
			if (opsReport.DrillingParams.DistHold.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDistHold>(opsReport.DrillingParams.DistHold);
				_db.OpsReportDistHolds.Update(obj);
			}
		}
		private void UpdateDistSteering(OpsReport opsReport)
		{
			if (opsReport.DistSteering.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDistSteering>(opsReport.DistSteering);
				_db.OpsReportDistSteerings.Update(obj);
			}
			if (opsReport.DrillingParams.DistSteering.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDistSteering>(opsReport.DrillingParams.DistSteering);
				_db.OpsReportDistSteerings.Update(obj);
			}
		}
		private void UpdateDuration(OpsReport opsReport)
		{
			if (opsReport.Activity.Duration.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDuration>(opsReport.Activity.Duration);
				_db.OpsReportDurations.Update(obj);
			}
		}
		private void UpdateMdHoleStart(OpsReport opsReport)
		{
			if (opsReport.Activity.MdHoleStart.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMdHoleStart>(opsReport.Activity.MdHoleStart);
				_db.OpsReportMdHoleStarts.Update(obj);
			}
			if (opsReport.DrillingParams.MdHoleStart.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMdHoleStart>(opsReport.DrillingParams.MdHoleStart);
				_db.OpsReportMdHoleStarts.Update(obj);
			}
		}
		private void UpdateTvdHoleStart(OpsReport opsReport)
		{
			if (opsReport.Activity.TvdHoleStart.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTvdHoleStart>(opsReport.Activity.TvdHoleStart);
				_db.OpsReportTvdHoleStarts.Update(obj);
			}
		}
		private void UpdateMdHoleEnd(OpsReport opsReport)
		{
			if (opsReport.Activity.MdHoleEnd.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMdHoleEnd>(opsReport.Activity.MdHoleEnd);
				_db.OpsReportMdHoleEnds.Update(obj);
			}
		}
		private void UpdateTvdHoleEnd(OpsReport opsReport)
		{
			if (opsReport.Activity.TvdHoleEnd.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTvdHoleEnd>(opsReport.Activity.TvdHoleEnd);
				_db.OpsReportTvdHoleEnds.Update(obj);
			}
		}
		private void UpdateMdBitStart(OpsReport opsReport)
		{
			if (opsReport.Activity.MdBitStart.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMdBitStart>(opsReport.Activity.MdBitStart);
				_db.OpsReportMdBitStarts.Update(obj);
			}
		}
		private void UpdateMdBitEnd(OpsReport opsReport)
		{
			if (opsReport.Activity.MdBitEnd.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMdBitEnd>(opsReport.Activity.MdBitEnd);
				_db.OpsReportMdBitEnds.Update(obj);
			}
		}
		private void UpdateActivity(OpsReport opsReport)
		{
			if (opsReport.Activity.Uid != null)
			{
				var obj = _mapper.Map<OpsReportActivity>(opsReport.Activity);
				_db.OpsReportActivitys.Update(obj);
			}
		}



		private void UpdateETimOpBit(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.ETimOpBit.Uom != null)
			{
				var obj = _mapper.Map<OpsReportETimOpBit>(opsReport.DrillingParams.ETimOpBit);
				_db.OpsReportETimOpBits.Update(obj);
			}
		}

		private void UpdateMdHoleStop(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.MdHoleStop.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMdHoleStop>(opsReport.DrillingParams.MdHoleStop);
				_db.OpsReportMdHoleStops.Update(obj);
			}
		}
		private void UpdateTubular(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.Tubular.UidRef != null)
			{
				var obj = _mapper.Map<OpsReportTubular>(opsReport.DrillingParams.Tubular);
				_db.OpsReportTubulars.Update(obj);
			}
		}

		private void UpdateHkldRot(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.HkldRot.Uom != null)
			{
				var obj = _mapper.Map<OpsReportHkldRot>(opsReport.DrillingParams.HkldRot);
				_db.OpsReportHkldRots.Update(obj);
			}
		}
		private void UpdateOverPull(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.OverPull.Uom != null)
			{
				var obj = _mapper.Map<OpsReportOverPull>(opsReport.DrillingParams.OverPull);
				_db.OpsReportOverPulls.Update(obj);
			}
		}

		private void UpdateSlackOff(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.SlackOff.Uom != null)
			{
				var obj = _mapper.Map<OpsReportSlackOff>(opsReport.DrillingParams.SlackOff);
				_db.OpsReportSlackOffs.Update(obj);
			}
		}
		private void UpdateHkldUp(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.HkldUp.Uom != null)
			{
				var obj = _mapper.Map<OpsReportHkldUp>(opsReport.DrillingParams.HkldUp);
				_db.OpsReportHkldUps.Update(obj);
			}
		}
		private void UpdateHkldDn(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.HkldDn.Uom != null)
			{
				var obj = _mapper.Map<OpsReportHkldDn>(opsReport.DrillingParams.HkldDn);
				_db.OpsReportHkldDns.Update(obj);
			}
		}
		private void UpdateTqOnBotAv(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.TqOnBotAv.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTqOnBotAv>(opsReport.DrillingParams.TqOnBotAv);
				_db.OpsReportTqOnBotAvs.Update(obj);
			}
		}
		private void UpdateTqOnBotMx(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.TqOnBotMx.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTqOnBotMx>(opsReport.DrillingParams.TqOnBotMx);
				_db.OpsReportTqOnBotMxs.Update(obj);
			}
		}
		private void UpdateTqOnBotMn(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.TqOnBotMn.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTqOnBotMn>(opsReport.DrillingParams.TqOnBotMn);
				_db.OpsReportTqOnBotMns.Update(obj);
			}
		}
		private void UpdateTqOffBotAv(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.TqOffBotAv.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTqOffBotAv>(opsReport.DrillingParams.TqOffBotAv);
				_db.OpsReportTqOffBotAvs.Update(obj);
			}
		}
		private void UpdateTqDhAv(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.TqDhAv.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTqDhAv>(opsReport.DrillingParams.TqDhAv);
				_db.OpsReportTqDhAvs.Update(obj);
			}
		}
		private void UpdateWtAboveJar(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.WtAboveJar.Uom != null)
			{
				var obj = _mapper.Map<OpsReportWtAboveJar>(opsReport.DrillingParams.WtAboveJar);
				_db.OpsReportWtAboveJars.Update(obj);
			}
		}
		private void UpdateWtBelowJar(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.WtBelowJar.Uom != null)
			{
				var obj = _mapper.Map<OpsReportWtBelowJar>(opsReport.DrillingParams.WtBelowJar);
				_db.OpsReportWtBelowJars.Update(obj);
			}
		}
		private void UpdateWtMud(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.WtMud.Uom != null)
			{
				var obj = _mapper.Map<OpsReportWtMud>(opsReport.DrillingParams.WtMud);
				_db.OpsReportWtMuds.Update(obj);
			}
		}
		private void UpdateFlowratePump(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.FlowratePump.Uom != null)
			{
				var obj = _mapper.Map<OpsReportFlowratePump>(opsReport.DrillingParams.FlowratePump);
				_db.OpsReportFlowratePumps.Update(obj);
			}
		}
		private void UpdatePowBit(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.PowBit.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPowBit>(opsReport.DrillingParams.PowBit);
				_db.OpsReportPowBits.Update(obj);
			}
		}
		private void UpdateVelNozzleAv(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.VelNozzleAv.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVelNozzleAv>(opsReport.DrillingParams.VelNozzleAv);
				_db.OpsReportVelNozzleAvs.Update(obj);
			}
		}
		private void UpdatePresDropBit(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.PresDropBit.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPresDropBit>(opsReport.DrillingParams.PresDropBit);
				_db.OpsReportPresDropBits.Update(obj);
			}
		}
		private void UpdateCTimHold(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.CTimHold.Uom != null)
			{
				var obj = _mapper.Map<OpsReportCTimHold>(opsReport.DrillingParams.CTimHold);
				_db.OpsReportCTimHolds.Update(obj);
			}
		}
		private void UpdateCTimSteering(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.CTimSteering.Uom != null)
			{
				var obj = _mapper.Map<OpsReportCTimSteering>(opsReport.DrillingParams.CTimSteering);
				_db.OpsReportCTimSteerings.Update(obj);
			}
		}
		private void UpdateCTimDrillRot(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.CTimDrillRot.Uom != null)
			{
				var obj = _mapper.Map<OpsReportCTimDrillRot>(opsReport.DrillingParams.CTimDrillRot);
				_db.OpsReportCTimDrillRots.Update(obj);
			}
		}
		private void UpdateCTimDrillSlid(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.CTimDrillSlid.Uom != null)
			{
				var obj = _mapper.Map<OpsReportCTimDrillSlid>(opsReport.DrillingParams.CTimDrillSlid);
				_db.OpsReportCTimDrillSlids.Update(obj);
			}
		}

		private void UpdateCTimCirc(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.CTimCirc.Uom != null)
			{
				var obj = _mapper.Map<OpsReportCTimCirc>(opsReport.DrillingParams.CTimCirc);
				_db.OpsReportCTimCircs.Update(obj);
			}
		}

		private void UpdateCTimReam(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.CTimReam.Uom != null)
			{
				var obj = _mapper.Map<OpsReportCTimReam>(opsReport.DrillingParams.CTimReam);
				_db.OpsReportCTimReams.Update(obj);
			}
		}
		private void UpdateRpmAv(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.RpmAv.Uom != null)
			{
				var obj = _mapper.Map<OpsReportRpmAv>(opsReport.DrillingParams.RpmAv);
				_db.OpsReportRpmAvs.Update(obj);
			}
		}
		private void UpdateRpmMx(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.RpmMx.Uom != null)
			{
				var obj = _mapper.Map<OpsReportRpmMx>(opsReport.DrillingParams.RpmMx);
				_db.OpsReportRpmMxs.Update(obj);
			}
		}
		private void UpdateRpmMn(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.RpmMn.Uom != null)
			{
				var obj = _mapper.Map<OpsReportRpmMn>(opsReport.DrillingParams.RpmMn);
				_db.OpsReportRpmMns.Update(obj);
			}
		}

		private void UpdateRpmAvDh(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.RpmAvDh.Uom != null)
			{
				var obj = _mapper.Map<OpsReportRpmAvDh>(opsReport.DrillingParams.RpmAvDh);
				_db.OpsReportRpmAvDhs.Update(obj);
			}
		}
		private void UpdateRopMx(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.RopMx.Uom != null)
			{
				var obj = _mapper.Map<OpsReportRopMx>(opsReport.DrillingParams.RopMx);
				_db.OpsReportRopMxs.Update(obj);
			}
		}
		private void UpdateRopMn(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.RopMn.Uom != null)
			{
				var obj = _mapper.Map<OpsReportRopMn>(opsReport.DrillingParams.RopMn);
				_db.OpsReportRopMns.Update(obj);
			}
		}
		private void UpdateWobAv(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.WobAv.Uom != null)
			{
				var obj = _mapper.Map<OpsReportWobAv>(opsReport.DrillingParams.WobAv);
				_db.OpsReportWobAvs.Update(obj);
			}
		}


		private void UpdateWobMx(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.WobMx.Uom != null)
			{
				var obj = _mapper.Map<OpsReportWobMx>(opsReport.DrillingParams.WobMx);
				_db.OpsReportWobMxs.Update(obj);
			}
		}
		private void UpdateWobMn(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.WobMn.Uom != null)
			{
				var obj = _mapper.Map<OpsReportWobMn>(opsReport.DrillingParams.WobMn);
				_db.OpsReportWobMns.Update(obj);
			}
		}
		private void UpdateWobAvDh(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.WobAvDh.Uom != null)
			{
				var obj = _mapper.Map<OpsReportWobAvDh>(opsReport.DrillingParams.WobAvDh);
				_db.OpsReportWobAvDhs.Update(obj);
			}
		}
		private void UpdateAziTop(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.AziTop.Uom != null)
			{
				var obj = _mapper.Map<OpsReportAziTop>(opsReport.DrillingParams.AziTop);
				_db.OpsReportAziTops.Update(obj);
			}
		}
		private void UpdateAziBottom(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.AziBottom.Uom != null)
			{
				var obj = _mapper.Map<OpsReportAziBottom>(opsReport.DrillingParams.AziBottom);
				_db.OpsReportAziBottoms.Update(obj);
			}
		}
		private void UpdateInclStart(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.InclStart.Uom != null)
			{
				var obj = _mapper.Map<OpsReportInclStart>(opsReport.DrillingParams.InclStart);
				_db.OpsReportInclStarts.Update(obj);
			}
		}
		private void UpdateInclMx(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.InclMx.Uom != null)
			{
				var obj = _mapper.Map<OpsReportInclMx>(opsReport.DrillingParams.InclMx);
				_db.OpsReportInclMxs.Update(obj);
			}
		}
		private void UpdateInclMn(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.InclMn.Uom != null)
			{
				var obj = _mapper.Map<OpsReportInclMn>(opsReport.DrillingParams.InclMn);
				_db.OpsReportInclMns.Update(obj);
			}
		}
		private void UpdateInclStop(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.InclStop.Uom != null)
			{
				var obj = _mapper.Map<OpsReportInclStop>(opsReport.DrillingParams.InclStop);
				_db.OpsReportInclStops.Update(obj);
			}
		}
		private void UpdateTempMudDhMx(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.TempMudDhMx.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTempMudDhMx>(opsReport.DrillingParams.TempMudDhMx);
				_db.OpsReportTempMudDhMxs.Update(obj);
			}
		}
		private void UpdatePresPumpAv(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.PresPumpAv.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPresPumpAv>(opsReport.DrillingParams.PresPumpAv);
				_db.OpsReportPresPumpAvs.Update(obj);
			}
		}
		private void UpdateFlowrateBit(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.FlowrateBit.Uom != null)
			{
				var obj = _mapper.Map<OpsReportFlowrateBit>(opsReport.DrillingParams.FlowrateBit);
				_db.OpsReportFlowrateBits.Update(obj);
			}
		}
		private void UpdateDrillingParams(OpsReport opsReport)
		{
			if (opsReport.DrillingParams.Uid != null)
			{
				var obj = _mapper.Map<OpsReportDrillingParam>(opsReport.DrillingParams);
				_db.OpsReportDrillingParams.Update(obj);
			}
		}
		private void UpdateCostPerItem(OpsReport opsReport)
		{
			foreach (var item in opsReport.DayCost)
			{

				if (item.CostPerItem != null &&  item.CostPerItem != null)
				{
					var obj = _mapper.Map<OpsReportCostPerItem>(item.CostPerItem);
					_db.OpsReportCostPerItems.Update(obj);
				}
			}
		}
		private void UpdateCostAmount(OpsReport opsReport)
		{
			foreach (var item in opsReport.DayCost)
			{

				if (item.CostAmount != null &&  item.CostAmount != null)
				{
					var obj = _mapper.Map<OpsReportCostAmount>(item.CostAmount);
					_db.OpsReportCostAmounts.Update(obj);
				}
			}
		}
		private void UpdateDayCost(OpsReport opsReport)
		{
			foreach (var item in opsReport.DayCost)
			{

				if (item != null)
				{
					var obj = _mapper.Map<OpsReportDayCost>(item);
					_db.OpsReportDayCosts.Update(obj);
				}
			}
		}

		private void UpdateMd(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.Md.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMd>(opsReport.TrajectoryStation.Md);
				_db.OpsReportMds.Update(obj);
			}
		}
		private void UpdateTvd(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.Tvd.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTvd>(opsReport.TrajectoryStation.Tvd);
				_db.OpsReportTvds.Update(obj);
			}
		}

		private void UpdateIncl(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.Incl.Uom != null)
			{
				var obj = _mapper.Map<OpsReportIncl>(opsReport.TrajectoryStation.Incl);
				_db.OpsReportIncls.Update(obj);
			}
		}

		private void UpdateAzi(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.Azi.Uom != null)
			{
				var obj = _mapper.Map<OpsReportAzi>(opsReport.TrajectoryStation.Azi);
				_db.OpsReportAzis.Update(obj);
			}
		}
		private void UpdateMtf(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.Mtf.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMtf>(opsReport.TrajectoryStation.Mtf);
				_db.OpsReportMtfs.Update(obj);
			}
		}
		private void UpdateGtf(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.Gtf.Uom != null)
			{
				var obj = _mapper.Map<OpsReportGtf>(opsReport.TrajectoryStation.Gtf);
				_db.OpsReportGtfs.Update(obj);
			}
		}
		private void UpdateDispNs(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.DispNs.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDispNs>(opsReport.TrajectoryStation.DispNs);
				_db.OpsReportDispNss.Update(obj);
			}
		}
		private void UpdateDispEw(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.DispEw.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDispEw>(opsReport.TrajectoryStation.DispEw);
				_db.OpsReportDispEws.Update(obj);
			}
		}
		private void UpdateVertSect(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.VertSect.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVertSect>(opsReport.TrajectoryStation.VertSect);
				_db.OpsReportVertSects.Update(obj);
			}
		}
		private void UpdateDls(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.Dls.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDls>(opsReport.TrajectoryStation.Dls);
				_db.OpsReportDlss.Update(obj);
			}
		}
		private void UpdateRateTurn(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.RateTurn.Uom != null)
			{
				var obj = _mapper.Map<OpsReportRateTurn>(opsReport.TrajectoryStation.RateTurn);
				_db.OpsReportRateTurns.Update(obj);
			}
		}
		private void UpdateRateBuild(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.RateBuild.Uom != null)
			{
				var obj = _mapper.Map<OpsReportRateBuild>(opsReport.TrajectoryStation.RateBuild);
				_db.OpsReportRateBuilds.Update(obj);
			}
		}
		private void UpdateMdDelta(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.MdDelta.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMdDelta>(opsReport.TrajectoryStation.MdDelta);
				_db.OpsReportMdDeltas.Update(obj);
			}
		}
		private void UpdateTvdDelta(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.TvdDelta.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTvdDelta>(opsReport.TrajectoryStation.TvdDelta);
				_db.OpsReportTvdDeltas.Update(obj);
			}
		}
		private void UpdateGravTotalUncert(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.GravTotalUncert.Uom != null)
			{
				var obj = _mapper.Map<OpsReportGravTotalUncert>(opsReport.TrajectoryStation.GravTotalUncert);
				_db.OpsReportGravTotalUncerts.Update(obj);
			}
		}
		private void UpdateDipAngleUncert(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.DipAngleUncert.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDipAngleUncert>(opsReport.TrajectoryStation.DipAngleUncert);
				_db.OpsReportDipAngleUncerts.Update(obj);
			}
		}
		private void UpdateMagTotalUncert(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.MagTotalUncert.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMagTotalUncert>(opsReport.TrajectoryStation.MagTotalUncert);
				_db.OpsReportMagTotalUncerts.Update(obj);
			}
		}
		private void UpdateGravAxialRaw(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.RawData.GravAxialRaw.Uom != null)
			{
				var obj = _mapper.Map<OpsReportGravAxialRaw>(opsReport.TrajectoryStation.RawData.GravAxialRaw);
				_db.OpsReportGravAxialRaws.Update(obj);
			}
		}
		private void UpdateGravTran1Raw(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.RawData.GravTran1Raw.Uom != null)
			{
				var obj = _mapper.Map<OpsReportGravTran1Raw>(opsReport.TrajectoryStation.RawData.GravTran1Raw);
				_db.OpsReportGravTran1Raws.Update(obj);
			}
		}
		private void UpdateGravTran2Raw(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.RawData.GravTran2Raw.Uom != null)
			{
				var obj = _mapper.Map<OpsReportGravTran2Raw>(opsReport.TrajectoryStation.RawData.GravTran2Raw);
				_db.OpsReportGravTran2Raws.Update(obj);
			}
		}

		private void UpdateGravAxialRaw(Trajectory trajectory)
		{
			if (trajectory.TrajectoryStation.RawData.GravAxialRaw.Uom != null)
			{
				var obj = _mapper.Map<OpsReportGravAxialRaw>(trajectory.TrajectoryStation.RawData.GravAxialRaw);
				_db.OpsReportGravAxialRaws.Update(obj);
			}
		}
		private void UpdateGravTran1Raw(Trajectory trajectory)
		{
			if (trajectory.TrajectoryStation.RawData.GravTran1Raw.Uom != null)
			{
				var obj = _mapper.Map<OpsReportGravTran1Raw>(trajectory.TrajectoryStation.RawData.GravTran1Raw);
				_db.OpsReportGravTran1Raws.Update(obj);
			}
		}
		private void UpdateGravTran2Raw(Trajectory trajectory)
		{
			if (trajectory.TrajectoryStation.RawData.GravTran2Raw.Uom != null)
			{
				var obj = _mapper.Map<OpsReportGravTran2Raw>(trajectory.TrajectoryStation.RawData.GravTran2Raw);
				_db.OpsReportGravTran2Raws.Update(obj);
			}
		}

		private void UpdateMagAxialRaw(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.RawData.MagAxialRaw.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMagAxialRaw>(opsReport.TrajectoryStation.RawData.MagAxialRaw);
				_db.OpsReportMagAxialRaws.Update(obj);
			}
		}
		private void UpdateMagTran1Raw(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.RawData.MagTran1Raw.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMagTran1Raw>(opsReport.TrajectoryStation.RawData.MagTran1Raw);
				_db.OpsReportMagTran1Raws.Update(obj);
			}
		}
		private void UpdateMagTran2Raw(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.RawData.MagTran2Raw.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMagTran2Raw>(opsReport.TrajectoryStation.RawData.MagTran2Raw);
				_db.OpsReportMagTran2Raws.Update(obj);
			}
		}

		private void UpdateRawData(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.RawData != null)
			{
				var obj = _mapper.Map<OpsReportRawData>(opsReport.TrajectoryStation.RawData);
				_db.OpsReportRawDatas.Update(obj);
			}
		}

		private void UpdateGravAxialAccelCor(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.CorUsed.GravAxialAccelCor.Uom != null)
			{
				var obj = _mapper.Map<OpsReportGravAxialAccelCor>(opsReport.TrajectoryStation.CorUsed.GravAxialAccelCor);
				_db.OpsReportGravAxialAccelCors.Update(obj);
			}
		}

		private void UpdateGravTran2AccelCor(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.CorUsed.GravTran2AccelCor.Uom != null)
			{
				var obj = _mapper.Map<OpsReportGravTran2AccelCor>(opsReport.TrajectoryStation.CorUsed.GravTran2AccelCor);
				_db.OpsReportGravTran2AccelCors.Update(obj);
			}
		}

		private void UpdateMagAxialDrlstrCor(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.CorUsed.MagAxialDrlstrCor.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMagAxialDrlstrCor>(opsReport.TrajectoryStation.CorUsed.MagAxialDrlstrCor);
				_db.OpsReportMagAxialDrlstrCors.Update(obj);
			}
		}

		private void UpdateMagTran1DrlstrCor(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.CorUsed.MagTran1DrlstrCor.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMagTran1DrlstrCor>(opsReport.TrajectoryStation.CorUsed.MagTran1DrlstrCor);
				_db.OpsReportMagTran1DrlstrCors.Update(obj);
			}
		}

		private void UpdateMagTran2DrlstrCor(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.CorUsed.MagTran2DrlstrCor.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMagTran2DrlstrCor>(opsReport.TrajectoryStation.CorUsed.MagTran2DrlstrCor);
				_db.OpsReportMagTran2DrlstrCors.Update(obj);
			}
		}

		private void UpdateSagIncCor(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.CorUsed.SagIncCor.Uom != null)
			{
				var obj = _mapper.Map<OpsReportSagIncCor>(opsReport.TrajectoryStation.CorUsed.SagIncCor);
				_db.OpsReportSagIncCors.Update(obj);
			}
		}

		private void UpdateSagAziCor(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.CorUsed.SagAziCor.Uom != null)
			{
				var obj = _mapper.Map<OpsReportSagAziCor>(opsReport.TrajectoryStation.CorUsed.SagAziCor);
				_db.OpsReportSagAziCors.Update(obj);
			}
		}
		private void UpdateStnMagDeclUsed(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.CorUsed.StnMagDeclUsed.Uom != null)
			{
				var obj = _mapper.Map<OpsReportStnMagDeclUsed>(opsReport.TrajectoryStation.CorUsed.StnMagDeclUsed);
				_db.OpsReportStnMagDeclUseds.Update(obj);
			}
		}
		private void UpdateStnGridCorUsed(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.CorUsed.StnGridCorUsed.Uom != null)
			{
				var obj = _mapper.Map<OpsReportStnGridCorUsed>(opsReport.TrajectoryStation.CorUsed.StnGridCorUsed);
				_db.OpsReportStnGridCorUseds.Update(obj);
			}
		}
		private void UpdateDirSensorOffset(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.CorUsed.DirSensorOffset.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDirSensorOffset>(opsReport.TrajectoryStation.CorUsed.DirSensorOffset);
				_db.OpsReportDirSensorOffsets.Update(obj);
			}
		}

		private void UpdateGravTran1AccelCor(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.CorUsed.GravTran1AccelCor.Uom != null)
			{
				var obj = _mapper.Map<OpsReportGravTran1AccelCor>(opsReport.TrajectoryStation.CorUsed.GravTran1AccelCor);
				_db.OpsReportGravTran1AccelCors.Update(obj);
			}
		}
		private void UpdateCorUsed(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.CorUsed != null)
			{
				var obj = _mapper.Map<OpsReportCorUsed>(opsReport.TrajectoryStation.CorUsed);
				_db.OpsReportCorUseds.Update(obj);
			}
		}
		private void UpdateMagTotalFieldCalc(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.Valid.MagTotalFieldCalc.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMagTotalFieldCalc>(opsReport.TrajectoryStation.Valid.MagTotalFieldCalc);
				_db.OpsReportMagTotalFieldCalcs.Update(obj);
			}
		}

		private void UpdateMagDipAngleCalc(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.Valid.MagDipAngleCalc.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMagDipAngleCalc>(opsReport.TrajectoryStation.Valid.MagDipAngleCalc);
				_db.OpsReportMagDipAngleCalcs.Update(obj);
			}
		}
		private void UpdateGravTotalFieldCalc(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.Valid.GravTotalFieldCalc.Uom != null)
			{
				var obj = _mapper.Map<OpsReportGravTotalFieldCalc>(opsReport.TrajectoryStation.Valid.GravTotalFieldCalc);
				_db.OpsReportGravTotalFieldCalcs.Update(obj);
			}
		}
		private void UpdateValid(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.Valid != null)
			{
				var obj = _mapper.Map<OpsReportValid>(opsReport.TrajectoryStation.Valid);
				_db.OpsReportValids.Update(obj);
			}
		}
		private void UpdateVarianceNN(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.MatrixCov.VarianceNN.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVarianceNN>(opsReport.TrajectoryStation.MatrixCov.VarianceNN);
				_db.OpsReportVarianceNNs.Update(obj);
			}
		}



		private void UpdateVarianceNE(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.MatrixCov.VarianceNE.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVarianceNE>(opsReport.TrajectoryStation.MatrixCov.VarianceNE);
				_db.OpsReportVarianceNEs.Update(obj);
			}
		}

		private void UpdateVarianceNVert(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.MatrixCov.VarianceNVert.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVarianceNVert>(opsReport.TrajectoryStation.MatrixCov.VarianceNVert);
				_db.OpsReportVarianceNVerts.Update(obj);
			}
		}
		private void UpdateVarianceEE(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.MatrixCov.VarianceEE.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVarianceEE>(opsReport.TrajectoryStation.MatrixCov.VarianceEE);
				_db.OpsReportVarianceEEs.Update(obj);
			}
		}
		private void UpdateVarianceEVert(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.MatrixCov.VarianceEVert.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVarianceEVert>(opsReport.TrajectoryStation.MatrixCov.VarianceEVert);
				_db.OpsReportVarianceEVerts.Update(obj);
			}
		}
		private void UpdateVarianceVertVert(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.MatrixCov.VarianceVertVert.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVarianceVertVert>(opsReport.TrajectoryStation.MatrixCov.VarianceVertVert);
				_db.OpsReportVarianceVertVerts.Update(obj);
			}
		}
		private void UpdateBiasN(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.MatrixCov.BiasN.Uom != null)
			{
				var obj = _mapper.Map<OpsReportBiasN>(opsReport.TrajectoryStation.MatrixCov.BiasN);
				_db.OpsReportBiasNs.Update(obj);
			}
		}
		private void UpdateBiasE(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.MatrixCov.BiasE.Uom != null)
			{
				var obj = _mapper.Map<OpsReportBiasE>(opsReport.TrajectoryStation.MatrixCov.BiasE);
				_db.OpsReportBiasEs.Update(obj);
			}
		}
		private void UpdateBiasVert(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.MatrixCov.BiasVert.Uom != null)
			{
				var obj = _mapper.Map<OpsReportBiasVert>(opsReport.TrajectoryStation.MatrixCov.BiasVert);
				_db.OpsReportBiasVerts.Update(obj);
			}
		}
		private void UpdateMatrixCov(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.MatrixCov != null)
			{
				var obj = _mapper.Map<OpsReportMatrixCov>(opsReport.TrajectoryStation.MatrixCov);
				_db.OpsReportMatrixCovs.Update(obj);
			}
		}
		private void UpdateWellCRS(OpsReport opsReport)
		{
			foreach (var item in opsReport.TrajectoryStation.Location)
			{
				if (item.WellCRS != null &&  item.WellCRS.UidRef != null)
				{
					var obj = _mapper.Map<OpsReportWellCRS>(item.WellCRS);
					_db.OpsReportWellCRSs.Update(obj);
				}
			}
		}
		private void UpdateLatitude(OpsReport opsReport)
		{
			foreach (var item in opsReport.TrajectoryStation.Location)
			{
				if (item.Latitude != null &&  item.Latitude.Uom != null)
				{
					var obj = _mapper.Map<OpsReportLatitude>(item.Latitude);
					_db.OpsReportLatitudes.Update(obj);
				}
			}
		}
		private void UpdateLongitude(OpsReport opsReport)
		{
			foreach (var item in opsReport.TrajectoryStation.Location)
			{
				if (item.Longitude != null && item.Longitude.Uom != null)
				{
					var obj = _mapper.Map<OpsReportLongitude>(item.Longitude);
					_db.OpsReportLongitudes.Update(obj);
				}
			}
		}
	   
		private void UpdateLocation(OpsReport opsReport)
		{
			foreach (var item in opsReport.TrajectoryStation.Location)
			{
				if (item != null && item.Uid != null)
				{
					var obj = _mapper.Map<OpsReportLocation>(item);
					_db.OpsReportLocations.Update(obj);
				}
			}
		}
		private void UpdateTrajectoryStation(OpsReport opsReport)
		{
			if (opsReport.TrajectoryStation.Uid != null)
			{
				var obj = _mapper.Map<OpsReportTrajectoryStation>(opsReport.TrajectoryStation);
				_db.OpsReportTrajectoryStations.Update(obj);
			}
		}

		private void UpdateProjectedX(OpsReport opsReport)
		{
			foreach (var item in opsReport.TrajectoryStation.Location)
			{
				if (item.ProjectedX != null && item.ProjectedX.Uom != null)
				{
					var obj = _mapper.Map<OpsReportProjectedX>(item.ProjectedX);
					_db.OpsReportProjectedXs.Update(obj);
				}
			}
		}

		private void UpdateProjectedY(OpsReport opsReport)
		{
			foreach (var item in opsReport.TrajectoryStation.Location)
			{
				if (item.ProjectedY != null && item.ProjectedY.Uom != null)
				{
					var obj = _mapper.Map<OpsReportProjectedY>(item.ProjectedY);
					_db.OpsReportProjectedYs.Update(obj);
				}
			}
		}

		private void UpdateDensity(OpsReport opsReport)
		{
			if (opsReport.Fluid.Density.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDensity>(opsReport.Fluid.Density);
				_db.OpsReportDensitys.Update(obj);
			}
		}

		private void UpdateVisFunnel(OpsReport opsReport)
		{
			if (opsReport.Fluid.VisFunnel.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVisFunnel>(opsReport.Fluid.VisFunnel);
				_db.OpsReportVisFunnels.Update(obj);
			}
		}

		private void UpdateTempVis(OpsReport opsReport)
		{
			if (opsReport.Fluid.TempVis.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTempVis>(opsReport.Fluid.TempVis);
				_db.OpsReportTempViss.Update(obj);
			}
		}

		private void UpdatePv(OpsReport opsReport)
		{
			if (opsReport.Fluid.Pv.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPv>(opsReport.Fluid.Pv);
				_db.OpsReportPvs.Update(obj);
			}
		}

		private void UpdateYp(OpsReport opsReport)
		{
			if (opsReport.Fluid.Yp.Uom != null)
			{
				var obj = _mapper.Map<OpsReportYp>(opsReport.Fluid.Yp);
				_db.OpsReportYps.Update(obj);
			}
		}

		private void UpdateGel10Sec(OpsReport opsReport)
		{
			if (opsReport.Fluid.Gel10Sec.Uom != null)
			{
				var obj = _mapper.Map<OpsReportGel10Sec>(opsReport.Fluid.Gel10Sec);
				_db.OpsReportGel10Secs.Update(obj);
			}
		}
		private void UpdateGel10Min(OpsReport opsReport)
		{
			if (opsReport.Fluid.Gel10Min.Uom != null)
			{
				var obj = _mapper.Map<OpsReportGel10Min>(opsReport.Fluid.Gel10Min);
				_db.OpsReportGel10Mins.Update(obj);
			}
		}
		private void UpdateGel30Min(OpsReport opsReport)
		{
			if (opsReport.Fluid.Gel30Min.Uom != null)
			{
				var obj = _mapper.Map<OpsReportGel30Min>(opsReport.Fluid.Gel30Min);
				_db.OpsReportGel30Mins.Update(obj);
			}
		}
		private void UpdateFilterCakeLtlp(OpsReport opsReport)
		{
			if (opsReport.Fluid.FilterCakeLtlp.Uom != null)
			{
				var obj = _mapper.Map<OpsReportFilterCakeLtlp>(opsReport.Fluid.FilterCakeLtlp);
				_db.OpsReportFilterCakeLtlps.Update(obj);
			}
		}
		private void UpdateFiltrateLtlp(OpsReport opsReport)
		{
			if (opsReport.Fluid.FiltrateLtlp.Uom != null)
			{
				var obj = _mapper.Map<OpsReportFiltrateLtlp>(opsReport.Fluid.FiltrateLtlp);
				_db.OpsReportFiltrateLtlps.Update(obj);
			}
		}
		private void UpdateTempHthp(OpsReport opsReport)
		{
			if (opsReport.Fluid.TempHthp.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTempHthp>(opsReport.Fluid.TempHthp);
				_db.OpsReportTempHthps.Update(obj);
			}
		}
		private void UpdatePresHthp(OpsReport opsReport)
		{
			if (opsReport.Fluid.PresHthp.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPresHthp>(opsReport.Fluid.PresHthp);
				_db.OpsReportPresHthps.Update(obj);
			}
		}
		private void UpdateFiltrateHthp(OpsReport opsReport)
		{
			if (opsReport.Fluid.FiltrateHthp.Uom != null)
			{
				var obj = _mapper.Map<OpsReportFiltrateHthp>(opsReport.Fluid.FiltrateHthp);
				_db.OpsReportFiltrateHthps.Update(obj);
			}
		}

		private void UpdateFilterCakeHthp(OpsReport opsReport)
		{
			if (opsReport.Fluid.FilterCakeHthp.Uom != null)
			{
				var obj = _mapper.Map<OpsReportFilterCakeHthp>(opsReport.Fluid.FilterCakeHthp);
				_db.OpsReportFilterCakeHthps.Update(obj);
			}
		}

		private void UpdateSolidsPc(OpsReport opsReport)
		{
			if (opsReport.Fluid.SolidsPc.Uom != null)
			{
				var obj = _mapper.Map<OpsReportSolidsPc>(opsReport.Fluid.SolidsPc);
				_db.OpsReportSolidsPcs.Update(obj);
			}
		}

		private void UpdateWaterPc(OpsReport opsReport)
		{
			if (opsReport.Fluid.WaterPc.Uom != null)
			{
				var obj = _mapper.Map<OpsReportWaterPc>(opsReport.Fluid.WaterPc);
				_db.OpsReportWaterPcs.Update(obj);
			}
		}

		private void UpdateOilPc(OpsReport opsReport)
		{
			if (opsReport.Fluid.OilPc.Uom != null)
			{
				var obj = _mapper.Map<OpsReportOilPc>(opsReport.Fluid.OilPc);
				_db.OpsReportOilPcs.Update(obj);
			}
		}

		private void UpdateSandPc(OpsReport opsReport)
		{
			if (opsReport.Fluid.SandPc.Uom != null)
			{
				var obj = _mapper.Map<OpsReportSandPc>(opsReport.Fluid.SandPc);
				_db.OpsReportSandPcs.Update(obj);
			}
		}

		private void UpdateSolidsLowGravPc(OpsReport opsReport)
		{
			if (opsReport.Fluid.SolidsLowGravPc.Uom != null)
			{
				var obj = _mapper.Map<OpsReportSolidsLowGravPc>(opsReport.Fluid.SolidsLowGravPc);
				_db.OpsReportSolidsLowGravPcs.Update(obj);
			}
		}
		private void UpdateSolidsCalcPc(OpsReport opsReport)
		{
			if (opsReport.Fluid.SolidsCalcPc.Uom != null)
			{
				var obj = _mapper.Map<OpsReportSolidsCalcPc>(opsReport.Fluid.SolidsCalcPc);
				_db.OpsReportSolidsCalcPcs.Update(obj);
			}
		}
		private void UpdateBaritePc(OpsReport opsReport)
		{
			if (opsReport.Fluid.BaritePc.Uom != null)
			{
				var obj = _mapper.Map<OpsReportBaritePc>(opsReport.Fluid.BaritePc);
				_db.OpsReportBaritePcs.Update(obj);
			}
		}
		private void UpdateLcm(OpsReport opsReport)
		{
			if (opsReport.Fluid.Lcm.Uom != null)
			{
				var obj = _mapper.Map<OpsReportLcm>(opsReport.Fluid.Lcm);
				_db.OpsReportLcms.Update(obj);
			}
		}
		private void UpdateMbt(OpsReport opsReport)
		{
			if (opsReport.Fluid.Mbt.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMbt>(opsReport.Fluid.Mbt);
				_db.OpsReportMbts.Update(obj);
			}
		}
		private void UpdateTempPh(OpsReport opsReport)
		{
			if (opsReport.Fluid.TempPh.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTempPh>(opsReport.Fluid.TempPh);
				_db.OpsReportTempPhs.Update(obj);
			}
		}
		private void UpdatePm(OpsReport opsReport)
		{
			if (opsReport.Fluid.Pm.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPm>(opsReport.Fluid.Pm);
				_db.OpsReportPms.Update(obj);
			}
		}
		private void UpdatePmFiltrate(OpsReport opsReport)
		{
			if (opsReport.Fluid.PmFiltrate.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPmFiltrate>(opsReport.Fluid.PmFiltrate);
				_db.OpsReportPmFiltrates.Update(obj);
			}
		}
		private void UpdateMf(OpsReport opsReport)
		{
			if (opsReport.Fluid.Mf.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMf>(opsReport.Fluid.Mf);
				_db.OpsReportMfs.Update(obj);
			}
		}
		private void UpdateAlkalinityP1(OpsReport opsReport)
		{
			if (opsReport.Fluid.AlkalinityP1.Uom != null)
			{
				var obj = _mapper.Map<OpsReportAlkalinityP1>(opsReport.Fluid.AlkalinityP1);
				_db.OpsReportAlkalinityP1s.Update(obj);
			}
		}
		private void UpdateAlkalinityP2(OpsReport opsReport)
		{
			if (opsReport.Fluid.AlkalinityP2.Uom != null)
			{
				var obj = _mapper.Map<OpsReportAlkalinityP2>(opsReport.Fluid.AlkalinityP2);
				_db.OpsReportAlkalinityP2s.Update(obj);
			}
		}
		private void UpdateChloride(OpsReport opsReport)
		{
			if (opsReport.Fluid.Chloride.Uom != null)
			{
				var obj = _mapper.Map<OpsReportChloride>(opsReport.Fluid.Chloride);
				_db.OpsReportChlorides.Update(obj);
			}
		}
		private void UpdateCalcium(OpsReport opsReport)
		{
			if (opsReport.Fluid.Calcium.Uom != null)
			{
				var obj = _mapper.Map<OpsReportCalcium>(opsReport.Fluid.Calcium);
				_db.OpsReportCalciums.Update(obj);
			}
		}
		private void UpdateMagnesium(OpsReport opsReport)
		{
			if (opsReport.Fluid.Magnesium.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMagnesium>(opsReport.Fluid.Magnesium);
				_db.OpsReportMagnesiums.Update(obj);
			}
		}

		private void UpdateTempRheom(OpsReport opsReport)
		{
			foreach (var item in opsReport.Fluid.Rheometer)
			{

				if (item.TempRheom != null && item.TempRheom.Uom != null)
				{
					var obj = _mapper.Map<OpsReportTempRheom>(item.TempRheom);
					_db.OpsReportTempRheoms.Update(obj);
				}
			}
		}
		private void UpdatePresRheom(OpsReport opsReport)
		{
			foreach (var item in opsReport.Fluid.Rheometer)
			{

				if (item.PresRheom != null && item.PresRheom.Uom != null)
				{
					var obj = _mapper.Map<OpsReportPresRheom>(item.PresRheom);
					_db.OpsReportPresRheoms.Update(obj);
				}
			}
		}
		private void UpdateRheometer(OpsReport opsReport)
		{
			foreach (var item in opsReport.Fluid.Rheometer)
			{

				if (item != null && item.Uid != null)
				{
					var obj = _mapper.Map<OpsReportRheometer>(item);
					_db.OpsReportRheometers.Update(obj);
				}
			}
		}
		private void UpdateBrinePc(OpsReport opsReport)
		{
			if (opsReport.Fluid.BrinePc.Uom != null)
			{
				var obj = _mapper.Map<OpsReportBrinePc>(opsReport.Fluid.BrinePc);
				_db.OpsReportBrinePcs.Update(obj);
			}
		}
		private void UpdateLime(OpsReport opsReport)
		{
			if (opsReport.Fluid.Lime.Uom != null)
			{
				var obj = _mapper.Map<OpsReportLime>(opsReport.Fluid.Lime);
				_db.OpsReportLimes.Update(obj);
			}
		}
		private void UpdateElectStab(OpsReport opsReport)
		{
			if (opsReport.Fluid.ElectStab.Uom != null)
			{
				var obj = _mapper.Map<OpsReportElectStab>(opsReport.Fluid.ElectStab);
				_db.OpsReportElectStabs.Update(obj);
			}
		}
		private void UpdateCalciumChloride(OpsReport opsReport)
		{
			if (opsReport.Fluid.CalciumChloride.Uom != null)
			{
				var obj = _mapper.Map<OpsReportCalciumChloride>(opsReport.Fluid.CalciumChloride);
				_db.OpsReportCalciumChlorides.Update(obj);
			}
		}
		private void UpdateSolidsHiGravPc(OpsReport opsReport)
		{
			if (opsReport.Fluid.SolidsHiGravPc.Uom != null)
			{
				var obj = _mapper.Map<OpsReportSolidsHiGravPc>(opsReport.Fluid.SolidsHiGravPc);
				_db.OpsReportSolidsHiGravPcs.Update(obj);
			}
		}
		private void UpdatePolymer(OpsReport opsReport)
		{
			if (opsReport.Fluid.Polymer.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPolymer>(opsReport.Fluid.Polymer);
				_db.OpsReportPolymers.Update(obj);
			}
		}
		private void UpdateSolCorPc(OpsReport opsReport)
		{
			if (opsReport.Fluid.SolCorPc.Uom != null)
			{
				var obj = _mapper.Map<OpsReportSolCorPc>(opsReport.Fluid.SolCorPc);
				_db.OpsReportSolCorPcs.Update(obj);
			}
		}
		private void UpdateOilCtg(OpsReport opsReport)
		{
			if (opsReport.Fluid.OilCtg.Uom != null)
			{
				var obj = _mapper.Map<OpsReportOilCtg>(opsReport.Fluid.OilCtg);
				_db.OpsReportOilCtgs.Update(obj);
			}
		}
		private void UpdateHardnessCa(OpsReport opsReport)
		{
			if (opsReport.Fluid.HardnessCa.Uom != null)
			{
				var obj = _mapper.Map<OpsReportHardnessCa>(opsReport.Fluid.HardnessCa);
				_db.OpsReportHardnessCas.Update(obj);
			}
		}
		private void UpdateSulfide(OpsReport opsReport)
		{
			if (opsReport.Fluid.Sulfide.Uom != null)
			{
				var obj = _mapper.Map<OpsReportSulfide>(opsReport.Fluid.Sulfide);
				_db.OpsReportSulfides.Update(obj);
			}
		}
		private void UpdateFluid(OpsReport opsReport)
		{
			if (opsReport.Fluid.Uid != null)
			{
				var obj = _mapper.Map<OpsReportFluid>(opsReport.Fluid);
				_db.OpsReportFluids.Update(obj);
			}
		}
		private void UpdatePump(OpsReport opsReport)
		{
			foreach (var item in opsReport.Scr)
			{
				if (item.Pump != null && item.Pump.UidRef != null)
				{
					var obj = _mapper.Map<OpsReportPump>(item.Pump);
					_db.OpsReportPumps.Update(obj);
				}
			}
		}
		private void UpdateRateStroke(OpsReport opsReport)
		{
			foreach (var item in opsReport.Scr)
			{
				if (item.RateStroke != null && item.RateStroke.Uom != null)
				{
					var obj = _mapper.Map<OpsReportRateStroke>(item.RateStroke);
					_db.OpsReportRateStrokes.Update(obj);
				}
			}
		}
		private void UpdatePresRecorded(OpsReport opsReport)
		{
			foreach (var item in opsReport.Scr)
			{
				if (item.PresRecorded != null && item.PresRecorded.Uom != null)
				{
					var obj = _mapper.Map<OpsReportPresRecorded>(item.PresRecorded);
					_db.OpsReportPresRecordeds.Update(obj);
				}
			}
		}
		private void UpdateMdBit(OpsReport opsReport)
		{
			foreach (var item in opsReport.Scr)
			{
				if (item.MdBit != null && item.MdBit.Uom != null)
				{
					var obj = _mapper.Map<OpsReportMdBit>(item.MdBit);
					_db.OpsReportMdBits.Update(obj);
				}
			}
		}
		private void UpdateScr(OpsReport opsReport)
		{
			foreach (var item in opsReport.Scr)
			{
				if (item != null && item.Uid != null)
				{
					var obj = _mapper.Map<OpsReportScr>(item);
					_db.OpsReportScrs.Update(obj);
				}
			}
		}
		private void UpdatePit(OpsReport opsReport)
		{
			foreach (var item in opsReport.PitVolume)
			{
				if (item.Pit != null && item.Pit.UidRef != null)
				{
					var obj = _mapper.Map<OpsReportPit>(item.Pit);
					_db.OpsReportPits.Update(obj);
				}
			}
		}
		private void UpdateVolPit(OpsReport opsReport)
		{
			foreach (var item in opsReport.PitVolume)
			{
				if (item.VolPit != null && item.VolPit.Uom != null)
				{
					var obj = _mapper.Map<OpsReportVolPit>(item.VolPit);
					_db.OpsReportVolPits.Update(obj);
				}
			}
		}
		private void UpdateDensFluid(OpsReport opsReport)
		{
			foreach (var item in opsReport.PitVolume)
			{
				if (item.DensFluid != null && item.DensFluid.Uom != null)
				{
					var obj = _mapper.Map<OpsReportDensFluid>(item.DensFluid);
					_db.OpsReportDensFluids.Update(obj);
				}
			}
		}
		private void UpdatePitVolume(OpsReport opsReport)
		{
			foreach (var item in opsReport.PitVolume)
			{
				if (item != null &&  item.Uid != null)
				{
					var obj = _mapper.Map<OpsReportPitVolume>(item);
					_db.OpsReportPitVolumes.Update(obj);
				}
			}
		}

		private void UpdateVolTotMudStart(OpsReport opsReport)
		{
			if (opsReport.MudVolume.VolTotMudStart.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolTotMudStart>(opsReport.MudVolume.VolTotMudStart);
				_db.OpsReportVolTotMudStarts.Update(obj);
			}
		}
		private void UpdateVolMudDumped(OpsReport opsReport)
		{
			if (opsReport.MudVolume.VolMudDumped.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolMudDumped>(opsReport.MudVolume.VolMudDumped);
				_db.OpsReportVolMudDumpeds.Update(obj);
			}
		}
		private void UpdateVolMudReceived(OpsReport opsReport)
		{
			if (opsReport.MudVolume.VolMudReceived.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolMudReceived>(opsReport.MudVolume.VolMudReceived);
				_db.OpsReportVolMudReceiveds.Update(obj);
			}
		}
		private void UpdateVolMudReturned(OpsReport opsReport)
		{
			if (opsReport.MudVolume.VolMudReturned.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolMudReturned>(opsReport.MudVolume.VolMudReturned);
				_db.OpsReportVolMudReturneds.Update(obj);
			}
		}
		private void UpdateVolLostShakerSurf(OpsReport opsReport)
		{
			if (opsReport.MudVolume.MudLosses.VolLostShakerSurf.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolLostShakerSurf>(opsReport.MudVolume.MudLosses.VolLostShakerSurf);
				_db.OpsReportVolLostShakerSurfs.Update(obj);
			}
		}
		private void UpdateVolLostMudCleanerSurf(OpsReport opsReport)
		{
			if (opsReport.MudVolume.MudLosses.VolLostMudCleanerSurf.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolLostMudCleanerSurf>(opsReport.MudVolume.MudLosses.VolLostMudCleanerSurf);
				_db.OpsReportVolLostMudCleanerSurfs.Update(obj);
			}
		}
		private void UpdateVolLostPitsSurf(OpsReport opsReport)
		{
			if (opsReport.MudVolume.MudLosses.VolLostPitsSurf.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolLostPitsSurf>(opsReport.MudVolume.MudLosses.VolLostPitsSurf);
				_db.OpsReportVolLostPitsSurfs.Update(obj);
			}
		}
		private void UpdateVolLostTrippingSurf(OpsReport opsReport)
		{
			if (opsReport.MudVolume.MudLosses.VolLostTrippingSurf.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolLostTrippingSurf>(opsReport.MudVolume.MudLosses.VolLostTrippingSurf);
				_db.OpsReportVolLostTrippingSurfs.Update(obj);
			}
		}
		private void UpdateVolLostOtherSurf(OpsReport opsReport)
		{
			if (opsReport.MudVolume.MudLosses.VolLostOtherSurf.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolLostOtherSurf>(opsReport.MudVolume.MudLosses.VolLostOtherSurf);
				_db.OpsReportVolLostOtherSurfs.Update(obj);
			}
		}
		private void UpdateVolTotMudLostSurf(OpsReport opsReport)
		{
			if (opsReport.MudVolume.MudLosses.VolTotMudLostSurf.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolTotMudLostSurf>(opsReport.MudVolume.MudLosses.VolTotMudLostSurf);
				_db.OpsReportVolTotMudLostSurfs.Update(obj);
			}
		}
		private void UpdateVolLostCircHole(OpsReport opsReport)
		{
			if (opsReport.MudVolume.MudLosses.VolLostCircHole.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolLostCircHole>(opsReport.MudVolume.MudLosses.VolLostCircHole);
				_db.OpsReportVolLostCircHoles.Update(obj);
			}
		}
		private void UpdateVolLostCsgHole(OpsReport opsReport)
		{
			if (opsReport.MudVolume.MudLosses.VolLostCsgHole.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolLostCsgHole>(opsReport.MudVolume.MudLosses.VolLostCsgHole);
				_db.OpsReportVolLostCsgHoles.Update(obj);
			}
		}
		private void UpdateVolLostCmtHole(OpsReport opsReport)
		{
			if (opsReport.MudVolume.MudLosses.VolLostCmtHole.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolLostCmtHole>(opsReport.MudVolume.MudLosses.VolLostCmtHole);
				_db.OpsReportVolLostCmtHoles.Update(obj);
			}
		}
		private void UpdateVolLostBhdCsgHole(OpsReport opsReport)
		{
			if (opsReport.MudVolume.MudLosses.VolLostBhdCsgHole.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolLostBhdCsgHole>(opsReport.MudVolume.MudLosses.VolLostBhdCsgHole);
				_db.OpsReportVolLostBhdCsgHoles.Update(obj);
			}
		}
		private void UpdateVolLostAbandonHole(OpsReport opsReport)
		{
			if (opsReport.MudVolume.MudLosses.VolLostAbandonHole.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolLostAbandonHole>(opsReport.MudVolume.MudLosses.VolLostAbandonHole);
				_db.OpsReportVolLostAbandonHoles.Update(obj);
			}
		}
		private void UpdateVolLostOtherHole(OpsReport opsReport)
		{
			if (opsReport.MudVolume.MudLosses.VolLostOtherHole.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolLostOtherHole>(opsReport.MudVolume.MudLosses.VolLostOtherHole);
				_db.OpsReportVolLostOtherHoles.Update(obj);
			}
		}
		private void UpdateVolTotMudLostHole(OpsReport opsReport)
		{
			if (opsReport.MudVolume.MudLosses.VolTotMudLostHole.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolTotMudLostHole>(opsReport.MudVolume.MudLosses.VolTotMudLostHole);
				_db.OpsReportVolTotMudLostHoles.Update(obj);
			}
		}
		private void UpdateMudLosses(OpsReport opsReport)
		{
			if (opsReport.MudVolume.MudLosses != null)
			{
				var obj = _mapper.Map<OpsReportMudLosses>(opsReport.MudVolume.MudLosses);
				_db.OpsReportMudLossess.Update(obj);
			}
		}
		private void UpdateVolMudBuilt(OpsReport opsReport)
		{
			if (opsReport.MudVolume.VolMudBuilt.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolMudBuilt>(opsReport.MudVolume.VolMudBuilt);
				_db.OpsReportVolMudBuilts.Update(obj);
			}
		}
		private void UpdateVolMudString(OpsReport opsReport)
		{
			if (opsReport.MudVolume.VolMudString.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolMudString>(opsReport.MudVolume.VolMudString);
				_db.OpsReportVolMudStrings.Update(obj);
			}
		}
		private void UpdateVolMudCasing(OpsReport opsReport)
		{
			if (opsReport.MudVolume.VolMudCasing.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolMudCasing>(opsReport.MudVolume.VolMudCasing);
				_db.OpsReportVolMudCasings.Update(obj);
			}
		}
		private void UpdateVolMudHole(OpsReport opsReport)
		{
			if (opsReport.MudVolume.VolMudHole.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolMudHole>(opsReport.MudVolume.VolMudHole);
				_db.OpsReportVolMudHoles.Update(obj);
			}
		}
		private void UpdateVolMudRiser(OpsReport opsReport)
		{
			if (opsReport.MudVolume.VolMudRiser.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolMudRiser>(opsReport.MudVolume.VolMudRiser);
				_db.OpsReportVolMudRisers.Update(obj);
			}
		}
		private void UpdateVolTotMudEnd(OpsReport opsReport)
		{
			if (opsReport.MudVolume.VolTotMudEnd.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolTotMudEnd>(opsReport.MudVolume.VolTotMudEnd);
				_db.OpsReportVolTotMudEnds.Update(obj);
			}
		}

		private void UpdateMudVolume(OpsReport opsReport)
		{
			if (opsReport.MudVolume != null)
			{
				var obj = _mapper.Map<OpsReportMudVolume>(opsReport.MudVolume);
				_db.OpsReportMudVolumes.Update(obj);
			}
		}

		private void UpdateItemWtPerUnit(OpsReport opsReport)
		{
			if (opsReport.MudInventory.ItemWtPerUnit.Uom != null)
			{
				var obj = _mapper.Map<OpsReportItemWtPerUnit>(opsReport.MudInventory.ItemWtPerUnit);
				_db.OpsReportItemWtPerUnits.Update(obj);
			}
		}
		private void UpdatePricePerUnit(OpsReport opsReport)
		{
			if (opsReport.MudInventory.PricePerUnit.PricePerUnitId != 0)
			{
				var obj = _mapper.Map<OpsReportPricePerUnit>(opsReport.MudInventory.PricePerUnit);
				_db.OpsReportPricePerUnits.Update(obj);
			}
		}
		private void UpdateCostItem(OpsReport opsReport)
		{
			if (opsReport.MudInventory.CostItem.CostItemId != 0)
			{
				var obj = _mapper.Map<OpsReportCostItem>(opsReport.MudInventory.CostItem);
				_db.OpsReportCostItems.Update(obj);
			}
		}
		private void UpdateMudInventory(OpsReport opsReport)
		{
			if (opsReport.MudInventory.Uid != null)
			{
				var obj = _mapper.Map<OpsReportMudInventory>(opsReport.MudInventory);
				_db.OpsReportMudInventorys.Update(obj);
			}
		}
		private void UpdateItemVolPerUnit(OpsReport opsReport)
		{
			if (opsReport.Bulk.ItemVolPerUnit.Uom != null)
			{
				var obj = _mapper.Map<OpsReportItemVolPerUnit>(opsReport.Bulk.ItemVolPerUnit);
				_db.OpsReportItemVolPerUnits.Update(obj);
			}
		}
		private void UpdateBulk(OpsReport opsReport)
		{
			if (opsReport.Bulk.Uid != null)
			{
				var obj = _mapper.Map<OpsReportBulk>(opsReport.Bulk);
				_db.OpsReportBulks.Update(obj);
			}
		}
		private void UpdateAnchorTension(OpsReport opsReport)
		{
			foreach (var item in opsReport.RigResponse.AnchorTension)
			{
				if (item != null && item.Uom != null)
				{
					var obj = _mapper.Map<OpsReportAnchorTension>(item);
					_db.OpsReportAnchorTensions.Update(obj);
				}
			}
		}
		private void UpdateAnchorAngle(OpsReport opsReport)
		{
			foreach (var item in opsReport.RigResponse.AnchorAngle)
			{
				if (item != null && item.Uom != null)
				{
					var obj = _mapper.Map<OpsReportAnchorAngle>(item);
					_db.OpsReportAnchorAngles.Update(obj);
				}
			}
		}
		private void UpdateRigHeading(OpsReport opsReport)
		{
			if (opsReport.RigResponse.RigHeading.Uom != null)
			{
				var obj = _mapper.Map<OpsReportRigHeading>(opsReport.RigResponse.RigHeading);
				_db.OpsReportRigHeadings.Update(obj);
			}
		}
		private void UpdateRigHeave(OpsReport opsReport)
		{
			if (opsReport.RigResponse.RigHeave.Uom != null)
			{
				var obj = _mapper.Map<OpsReportRigHeave>(opsReport.RigResponse.RigHeave);
				_db.OpsReportRigHeaves.Update(obj);
			}
		}
		private void UpdateRigPitchAngle(OpsReport opsReport)
		{
			if (opsReport.RigResponse.RigPitchAngle.Uom != null)
			{
				var obj = _mapper.Map<OpsReportRigPitchAngle>(opsReport.RigResponse.RigPitchAngle);
				_db.OpsReportRigPitchAngles.Update(obj);
			}
		}
		private void UpdateRigRollAngle(OpsReport opsReport)
		{
			if (opsReport.RigResponse.RigRollAngle.Uom != null)
			{
				var obj = _mapper.Map<OpsReportRigRollAngle>(opsReport.RigResponse.RigRollAngle);
				_db.OpsReportRigRollAngles.Update(obj);
			}
		}
		private void UpdateRiserAngle(OpsReport opsReport)
		{
			if (opsReport.RigResponse.RiserAngle.Uom != null)
			{
				var obj = _mapper.Map<OpsReportRiserAngle>(opsReport.RigResponse.RiserAngle);
				_db.OpsReportRiserAngles.Update(obj);
			}
		}
		private void UpdateRiserDirection(OpsReport opsReport)
		{
			if (opsReport.RigResponse.RiserDirection.Uom != null)
			{
				var obj = _mapper.Map<OpsReportRiserDirection>(opsReport.RigResponse.RiserDirection);
				_db.OpsReportRiserDirections.Update(obj);
			}
		}
		private void UpdateRiserTension(OpsReport opsReport)
		{
			if (opsReport.RigResponse.RiserTension.Uom != null)
			{
				var obj = _mapper.Map<OpsReportRiserTension>(opsReport.RigResponse.RiserTension);
				_db.OpsReportRiserTensions.Update(obj);
			}
		}
		private void UpdateVariableDeckLoad(OpsReport opsReport)
		{
			if (opsReport.RigResponse.VariableDeckLoad.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVariableDeckLoad>(opsReport.RigResponse.VariableDeckLoad);
				_db.OpsReportVariableDeckLoads.Update(obj);
			}
		}
		private void UpdateTotalDeckLoad(OpsReport opsReport)
		{
			if (opsReport.RigResponse.TotalDeckLoad.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTotalDeckLoad>(opsReport.RigResponse.TotalDeckLoad);
				_db.OpsReportTotalDeckLoads.Update(obj);
			}
		}
		private void UpdateGuideBaseAngle(OpsReport opsReport)
		{
			if (opsReport.RigResponse.GuideBaseAngle.Uom != null)
			{
				var obj = _mapper.Map<OpsReportGuideBaseAngle>(opsReport.RigResponse.GuideBaseAngle);
				_db.OpsReportGuideBaseAngles.Update(obj);
			}
		}
		private void UpdateBallJointAngle(OpsReport opsReport)
		{
			if (opsReport.RigResponse.BallJointAngle.Uom != null)
			{
				var obj = _mapper.Map<OpsReportBallJointAngle>(opsReport.RigResponse.BallJointAngle);
				_db.OpsReportBallJointAngles.Update(obj);
			}
		}
		private void UpdateBallJointDirection(OpsReport opsReport)
		{
			if (opsReport.RigResponse.BallJointDirection.Uom != null)
			{
				var obj = _mapper.Map<OpsReportBallJointDirection>(opsReport.RigResponse.BallJointDirection);
				_db.OpsReportBallJointDirections.Update(obj);
			}
		}
		private void UpdateOffsetRig(OpsReport opsReport)
		{
			if (opsReport.RigResponse.OffsetRig.Uom != null)
			{
				var obj = _mapper.Map<OpsReportOffsetRig>(opsReport.RigResponse.OffsetRig);
				_db.OpsReportOffsetRigs.Update(obj);
			}
		}
		private void UpdateLoadLeg1(OpsReport opsReport)
		{
			if (opsReport.RigResponse.LoadLeg1.Uom != null)
			{
				var obj = _mapper.Map<OpsReportLoadLeg1>(opsReport.RigResponse.LoadLeg1);
				_db.OpsReportLoadLeg1s.Update(obj);
			}
		}
		private void UpdateLoadLeg2(OpsReport opsReport)
		{
			if (opsReport.RigResponse.LoadLeg2.Uom != null)
			{
				var obj = _mapper.Map<OpsReportLoadLeg2>(opsReport.RigResponse.LoadLeg2);
				_db.OpsReportLoadLeg2s.Update(obj);
			}
		}
		private void UpdateLoadLeg3(OpsReport opsReport)
		{
			if (opsReport.RigResponse.LoadLeg3.Uom != null)
			{
				var obj = _mapper.Map<OpsReportLoadLeg3>(opsReport.RigResponse.LoadLeg3);
				_db.OpsReportLoadLeg3s.Update(obj);
			}
		}
		private void UpdateLoadLeg4(OpsReport opsReport)
		{
			if (opsReport.RigResponse.LoadLeg4.Uom != null)
			{
				var obj = _mapper.Map<OpsReportLoadLeg4>(opsReport.RigResponse.LoadLeg4);
				_db.OpsReportLoadLeg4s.Update(obj);
			}
		}
		private void UpdatePenetrationLeg1(OpsReport opsReport)
		{
			if (opsReport.RigResponse.PenetrationLeg1.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPenetrationLeg1>(opsReport.RigResponse.PenetrationLeg1);
				_db.OpsReportPenetrationLeg1s.Update(obj);
			}
		}
		private void UpdatePenetrationLeg2(OpsReport opsReport)
		{
			if (opsReport.RigResponse.PenetrationLeg2.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPenetrationLeg2>(opsReport.RigResponse.PenetrationLeg2);
				_db.OpsReportPenetrationLeg2s.Update(obj);
			}
		}
		private void UpdatePenetrationLeg3(OpsReport opsReport)
		{
			if (opsReport.RigResponse.PenetrationLeg3.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPenetrationLeg3>(opsReport.RigResponse.PenetrationLeg3);
				_db.OpsReportPenetrationLeg3s.Update(obj);
			}
		}
		private void UpdatePenetrationLeg4(OpsReport opsReport)
		{
			if (opsReport.RigResponse.PenetrationLeg4.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPenetrationLeg4>(opsReport.RigResponse.PenetrationLeg4);
				_db.OpsReportPenetrationLeg4s.Update(obj);
			}
		}
		private void UpdateDispRig(OpsReport opsReport)
		{
			if (opsReport.RigResponse.DispRig.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDispRig>(opsReport.RigResponse.DispRig);
				_db.OpsReportDispRigs.Update(obj);
			}
		}
		private void UpdateMeanDraft(OpsReport opsReport)
		{
			if (opsReport.RigResponse.MeanDraft.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMeanDraft>(opsReport.RigResponse.MeanDraft);
				_db.OpsReportMeanDrafts.Update(obj);
			}
		}
		private void UpdateRigResponse(OpsReport opsReport)
		{
			if (opsReport.RigResponse != null)
			{
				var obj = _mapper.Map<OpsReportRigResponse>(opsReport.RigResponse);
				_db.OpsReportRigResponses.Update(obj);
			}
		}
		private void UpdateIdLiner(OpsReport opsReport)
		{
			if (opsReport.PumpOp.IdLiner.Uom != null)
			{
				var obj = _mapper.Map<OpsReportIdLiner>(opsReport.PumpOp.IdLiner);
				_db.OpsReportIdLiners.Update(obj);
			}
		}
		private void UpdateLenStroke(OpsReport opsReport)
		{
			if (opsReport.PumpOp.LenStroke.Uom != null)
			{
				var obj = _mapper.Map<OpsReportLenStroke>(opsReport.PumpOp.LenStroke);
				_db.OpsReportLenStrokes.Update(obj);
			}
		}
		private void UpdatePressure(OpsReport opsReport)
		{
			if (opsReport.PumpOp.Pressure.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPressure>(opsReport.PumpOp.Pressure);
				_db.OpsReportPressures.Update(obj);
			}
		}
		private void UpdatePcEfficiency(OpsReport opsReport)
		{
			if (opsReport.PumpOp.PcEfficiency.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPcEfficiency>(opsReport.PumpOp.PcEfficiency);
				_db.OpsReportPcEfficiencys.Update(obj);
			}
		}
		private void UpdatePumpOutput(OpsReport opsReport)
		{
			if (opsReport.PumpOp.PumpOutput.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPumpOutput>(opsReport.PumpOp.PumpOutput);
				_db.OpsReportPumpOutputs.Update(obj);
			}
		}
		private void UpdatePumpOp(OpsReport opsReport)
		{
			if (opsReport.PumpOp.Uid != null)
			{
				var obj = _mapper.Map<OpsReportPumpOp>(opsReport.PumpOp);
				_db.OpsReportPumpOps.Update(obj);
			}
		}
		private void UpdateShaker(OpsReport opsReport)
		{
			if (opsReport.ShakerOp.Shaker.UidRef != null)
			{
				var obj = _mapper.Map<OpsReportShaker>(opsReport.ShakerOp.Shaker);
				_db.OpsReportShakers.Update(obj);
			}
		}
		private void UpdateMdHole(OpsReport opsReport)
		{
			if (opsReport.ShakerOp.MdHole.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMdHole>(opsReport.ShakerOp.MdHole);
				_db.OpsReportMdHoles.Update(obj);
			}
		}
		private void UpdateHoursRun(OpsReport opsReport)
		{
			if (opsReport.ShakerOp.HoursRun.Uom != null)
			{
				var obj = _mapper.Map<OpsReportHoursRun>(opsReport.ShakerOp.HoursRun);
				_db.OpsReportHoursRuns.Update(obj);
			}
		}
		private void UpdatePcScreenCovered(OpsReport opsReport)
		{
			if (opsReport.ShakerOp.PcScreenCovered.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPcScreenCovered>(opsReport.ShakerOp.PcScreenCovered);
				_db.OpsReportPcScreenCovereds.Update(obj);
			}
		}
		private void UpdateMeshX(OpsReport opsReport)
		{
			if (opsReport.ShakerOp.ShakerScreen.MeshX.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMeshX>(opsReport.ShakerOp.ShakerScreen.MeshX);
				_db.OpsReportMeshXs.Update(obj);
			}
		}
		private void UpdateMeshY(OpsReport opsReport)
		{
			if (opsReport.ShakerOp.ShakerScreen.MeshY.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMeshY>(opsReport.ShakerOp.ShakerScreen.MeshY);
				_db.OpsReportMeshYs.Update(obj);
			}
		}
		private void UpdateCutPoint(OpsReport opsReport)
		{
			if (opsReport.ShakerOp.ShakerScreen.CutPoint.Uom != null)
			{
				var obj = _mapper.Map<OpsReportCutPoint>(opsReport.ShakerOp.ShakerScreen.CutPoint);
				_db.OpsReportCutPoints.Update(obj);
			}
		}

		private void UpdateShakerScreen(OpsReport opsReport)
		{
			if (opsReport.ShakerOp.ShakerScreen != null)
			{
				var obj = _mapper.Map<OpsReportShakerScreen>(opsReport.ShakerOp.ShakerScreen);
				_db.OpsReportShakerScreens.Update(obj);
			}
		}
		private void UpdateShakerOp(OpsReport opsReport)
		{
			if (opsReport.ShakerOp.Uid != null)
			{
				var obj = _mapper.Map<OpsReportShakerOp>(opsReport.ShakerOp);
				_db.OpsReportShakerOps.Update(obj);
			}
		}
		private void UpdateDaysIncFree(OpsReport opsReport)
		{
			if (opsReport.Hse.DaysIncFree.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDaysIncFree>(opsReport.Hse.DaysIncFree);
				_db.OpsReportDaysIncFrees.Update(obj);
			}
		}
		private void UpdateETimLostGross(OpsReport opsReport)
		{
			if (opsReport.Hse.Incident.ETimLostGross.Uom != null)
			{
				var obj = _mapper.Map<OpsReportETimLostGross>(opsReport.Hse.Incident.ETimLostGross);
				_db.OpsReportETimLostGrosss.Update(obj);
			}
		}
		private void UpdateCostLostGross(OpsReport opsReport)
		{
			if (opsReport.Hse.Incident.CostLostGross != null)
			{
				var obj = _mapper.Map<OpsReportCostLostGross>(opsReport.Hse.Incident.CostLostGross);
				_db.OpsReportCostLostGrosss.Update(obj);
			}
		}
		private void UpdateIncident(OpsReport opsReport)
		{
			if (opsReport.Hse.Incident.Uid != null)
			{
				var obj = _mapper.Map<OpsReportIncident>(opsReport.Hse.Incident);
				_db.OpsReportIncidents.Update(obj);
			}
		}
		private void UpdatePresLastCsg(OpsReport opsReport)
		{
			if (opsReport.Hse.PresLastCsg.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPresLastCsg>(opsReport.Hse.PresLastCsg);
				_db.OpsReportPresLastCsgs.Update(obj);
			}
		}
		private void UpdatePresStdPipe(OpsReport opsReport)
		{
			if (opsReport.Hse.PresStdPipe.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPresStdPipe>(opsReport.Hse.PresStdPipe);
				_db.OpsReportPresStdPipes.Update(obj);
			}
		}
		private void UpdatePresKellyHose(OpsReport opsReport)
		{
			if (opsReport.Hse.PresKellyHose.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPresKellyHose>(opsReport.Hse.PresKellyHose);
				_db.OpsReportPresKellyHoses.Update(obj);
			}
		}
		private void UpdatePresDiverter(OpsReport opsReport)
		{
			if (opsReport.Hse.PresDiverter.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPresDiverter>(opsReport.Hse.PresDiverter);
				_db.OpsReportPresDiverters.Update(obj);
			}
		}
		private void UpdatePresAnnular(OpsReport opsReport)
		{
			if (opsReport.Hse.PresAnnular.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPresAnnular>(opsReport.Hse.PresAnnular);
				_db.OpsReportPresAnnulars.Update(obj);
			}
		}
		private void UpdatePresRams(OpsReport opsReport)
		{
			if (opsReport.Hse.PresRams.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPresRams>(opsReport.Hse.PresRams);
				_db.OpsReportPresRamss.Update(obj);
			}
		}
		private void UpdatePresChokeLine(OpsReport opsReport)
		{
			if (opsReport.Hse.PresChokeLine.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPresChokeLine>(opsReport.Hse.PresChokeLine);
				_db.OpsReportPresChokeLines.Update(obj);
			}
		}
		private void UpdatePresChokeMan(OpsReport opsReport)
		{
			if (opsReport.Hse.PresChokeMan.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPresChokeMan>(opsReport.Hse.PresChokeMan);
				_db.OpsReportPresChokeMans.Update(obj);
			}
		}
		private void UpdateFluidDischarged(OpsReport opsReport)
		{
			if (opsReport.Hse.FluidDischarged.Uom != null)
			{
				var obj = _mapper.Map<OpsReportFluidDischarged>(opsReport.Hse.FluidDischarged);
				_db.OpsReportFluidDischargeds.Update(obj);
			}
		}
		private void UpdateVolCtgDischarged(OpsReport opsReport)
		{
			if (opsReport.Hse.VolCtgDischarged.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolCtgDischarged>(opsReport.Hse.VolCtgDischarged);
				_db.OpsReportVolCtgDischargeds.Update(obj);
			}
		}

		private void UpdateVolOilCtgDischarge(OpsReport opsReport)
		{
			if (opsReport.Hse.VolOilCtgDischarge.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolOilCtgDischarge>(opsReport.Hse.VolOilCtgDischarge);
				_db.OpsReportVolOilCtgDischarges.Update(obj);
			}
		}

		private void UpdateWasteDischarged(OpsReport opsReport)
		{
			if (opsReport.Hse.WasteDischarged.Uom != null)
			{
				var obj = _mapper.Map<OpsReportWasteDischarged>(opsReport.Hse.WasteDischarged);
				_db.OpsReportWasteDischargeds.Update(obj);
			}
		}
		private void UpdateHse(OpsReport opsReport)
		{
			if (opsReport.Hse != null)
			{
				var obj = _mapper.Map<OpsReportHse>(opsReport.Hse);
				_db.OpsReportHses.Update(obj);
			}
		}
		private void UpdateTotalTime(OpsReport opsReport)
		{
			foreach (var item in opsReport.Personnel)
			{
				if (item.TotalTime != null && item.TotalTime.Uom != null)
				{
					var obj = _mapper.Map<OpsReportTotalTime>(item.TotalTime);
					_db.OpsReportTotalTimes.Update(obj);
				}
			}
		}
		private void UpdatePersonnel(OpsReport opsReport)
		{
			foreach (var item in opsReport.Personnel)
			{
				if (item != null &&  item.Uid != null)
				{
					var obj = _mapper.Map<OpsReportPersonnel>(item);
					_db.OpsReportPersonnels.Update(obj);
				}
			}
		}
		private void UpdateSupportCraft(OpsReport opsReport)
		{
			if (opsReport.SupportCraft.Uid != null)
			{
				var obj = _mapper.Map<OpsReportSupportCraft>(opsReport.SupportCraft);
				_db.OpsReportSupportCrafts.Update(obj);
			}
		}
		private void UpdateBarometricPressure(OpsReport opsReport)
		{
			if (opsReport.Weather.BarometricPressure.Uom != null)
			{
				var obj = _mapper.Map<OpsReportBarometricPressure>(opsReport.Weather.BarometricPressure);
				_db.OpsReportBarometricPressures.Update(obj);
			}
		}
		private void UpdateTempSurfaceMn(OpsReport opsReport)
		{
			if (opsReport.Weather.TempSurfaceMn.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTempSurfaceMn>(opsReport.Weather.TempSurfaceMn);
				_db.OpsReportTempSurfaceMns.Update(obj);
			}
		}
		private void UpdateTempSurfaceMx(OpsReport opsReport)
		{
			if (opsReport.Weather.TempSurfaceMx.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTempSurfaceMx>(opsReport.Weather.TempSurfaceMx);
				_db.OpsReportTempSurfaceMxs.Update(obj);
			}
		}
		private void UpdateTempWindChill(OpsReport opsReport)
		{
			if (opsReport.Weather.TempWindChill.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTempWindChill>(opsReport.Weather.TempWindChill);
				_db.OpsReportTempWindChills.Update(obj);
			}
		}
		private void UpdateTempsea(OpsReport opsReport)
		{
			if (opsReport.Weather.Tempsea.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTempsea>(opsReport.Weather.Tempsea);
				_db.OpsReportTempseas.Update(obj);
			}
		}
		private void UpdateVisibility(OpsReport opsReport)
		{
			if (opsReport.Weather.Visibility.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVisibility>(opsReport.Weather.Visibility);
				_db.OpsReportVisibilitys.Update(obj);
			}
		}
		private void UpdateAziWave(OpsReport opsReport)
		{
			if (opsReport.Weather.AziWave.Uom != null)
			{
				var obj = _mapper.Map<OpsReportAziWave>(opsReport.Weather.AziWave);
				_db.OpsReportAziWaves.Update(obj);
			}
		}
		private void UpdateHtWave(OpsReport opsReport)
		{
			if (opsReport.Weather.HtWave.Uom != null)
			{
				var obj = _mapper.Map<OpsReportHtWave>(opsReport.Weather.HtWave);
				_db.OpsReportHtWaves.Update(obj);
			}
		}
		private void UpdatePeriodWave(OpsReport opsReport)
		{
			if (opsReport.Weather.PeriodWave.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPeriodWave>(opsReport.Weather.PeriodWave);
				_db.OpsReportPeriodWaves.Update(obj);
			}
		}
		private void UpdateAziWind(OpsReport opsReport)
		{
			if (opsReport.Weather.AziWind.Uom != null)
			{
				var obj = _mapper.Map<OpsReportAziWind>(opsReport.Weather.AziWind);
				_db.OpsReportAziWinds.Update(obj);
			}
		}
		private void UpdateVelWind(OpsReport opsReport)
		{
			if (opsReport.Weather.VelWind.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVelWind>(opsReport.Weather.VelWind);
				_db.OpsReportVelWinds.Update(obj);
			}
		}
		private void UpdateAmtPrecip(OpsReport opsReport)
		{
			if (opsReport.Weather.AmtPrecip.Uom != null)
			{
				var obj = _mapper.Map<OpsReportAmtPrecip>(opsReport.Weather.AmtPrecip);
				_db.OpsReportAmtPrecips.Update(obj);
			}
		}
		private void UpdateCeilingCloud(OpsReport opsReport)
		{
			if (opsReport.Weather.CeilingCloud.Uom != null)
			{
				var obj = _mapper.Map<OpsReportCeilingCloud>(opsReport.Weather.CeilingCloud);
				_db.OpsReportCeilingClouds.Update(obj);
			}
		}
		private void UpdateCurrentSea(OpsReport opsReport)
		{
			if (opsReport.Weather.CurrentSea.Uom != null)
			{
				var obj = _mapper.Map<OpsReportCurrentSea>(opsReport.Weather.CurrentSea);
				_db.OpsReportCurrentSeas.Update(obj);
			}
		}
		private void UpdateAziCurrentSea(OpsReport opsReport)
		{
			if (opsReport.Weather.AziCurrentSea.Uom != null)
			{
				var obj = _mapper.Map<OpsReportAziCurrentSea>(opsReport.Weather.AziCurrentSea);
				_db.OpsReportAziCurrentSeas.Update(obj);
			}
		}
		private void UpdateWeather(OpsReport opsReport)
		{
			if (opsReport.Weather.Uid != null)
			{
				var obj = _mapper.Map<OpsReportWeather>(opsReport.Weather);
				_db.OpsReportWeathers.Update(obj);
			}
		}
		private void UpdateCostDay(OpsReport opsReport)
		{
			if (opsReport.CostDay != null)
			{
				var obj = _mapper.Map<OpsReportCostDay>(opsReport.CostDay);
				_db.OpsReportCostDays.Update(obj);
			}
		}
		private void UpdateCostDayMud(OpsReport opsReport)
		{
			if (opsReport.CostDayMud != null)
			{
				var obj = _mapper.Map<OpsReportCostDayMud>(opsReport.CostDayMud);
				_db.OpsReportCostDayMuds.Update(obj);
			}
		}
		private void UpdateDiaHole(OpsReport opsReport)
		{
			if (opsReport.DiaHole.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDiaHole>(opsReport.DiaHole);
				_db.OpsReportDiaHoles.Update(obj);
			}
		}
		private void UpdateDiaCsgLast(OpsReport opsReport)
		{
			if (opsReport.DiaCsgLast.Uom != null)
			{
				var obj = _mapper.Map<OpsReportDiaCsgLast>(opsReport.DiaCsgLast);
				_db.OpsReportDiaCsgLasts.Update(obj);
			}
		}
		private void UpdateMdCsgLast(OpsReport opsReport)
		{
			if (opsReport.MdCsgLast.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMdCsgLast>(opsReport.MdCsgLast);
				_db.OpsReportMdCsgLasts.Update(obj);
			}
		}
		private void UpdateTvdCsgLast(OpsReport opsReport)
		{
			if (opsReport.TvdCsgLast.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTvdCsgLast>(opsReport.TvdCsgLast);
				_db.OpsReportTvdCsgLasts.Update(obj);
			}
		}
		private void UpdateTvdLot(OpsReport opsReport)
		{
			if (opsReport.TvdLot.Uom != null)
			{
				var obj = _mapper.Map<OpsReportTvdLot>(opsReport.TvdLot);
				_db.OpsReportTvdLots.Update(obj);
			}
		}
		private void UpdatePresLotEmw(OpsReport opsReport)
		{
			if (opsReport.PresLotEmw.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPresLotEmw>(opsReport.PresLotEmw);
				_db.OpsReportPresLotEmws.Update(obj);
			}
		}
		private void UpdatePresKickTol(OpsReport opsReport)
		{
			if (opsReport.PresKickTol.Uom != null)
			{
				var obj = _mapper.Map<OpsReportPresKickTol>(opsReport.PresKickTol);
				_db.OpsReportPresKickTols.Update(obj);
			}
		}
		private void UpdateVolKickTol(OpsReport opsReport)
		{
			if (opsReport.VolKickTol.Uom != null)
			{
				var obj = _mapper.Map<OpsReportVolKickTol>(opsReport.VolKickTol);
				_db.OpsReportVolKickTols.Update(obj);
			}
		}
		private void UpdateMaasp(OpsReport opsReport)
		{
			if (opsReport.Maasp.Uom != null)
			{
				var obj = _mapper.Map<OpsReportMaasp>(opsReport.Maasp);
				_db.OpsReportMaasps.Update(obj);
			}
		}
		private void UpdateCommonData(OpsReport opsReport)
		{
			if (opsReport.CommonData != null)
			{
				var obj = _mapper.Map<OpsReportsCommonData>(opsReport.CommonData);
				_db.OpsReportsCommonDatas.Update(obj);
			}
		}
		#endregion Update OpsReport
	}
}
