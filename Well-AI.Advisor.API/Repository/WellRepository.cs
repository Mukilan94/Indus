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
    public class WellRepository : IWellRepository
    {
        private readonly WellAIAdvisiorContext _db;
        private readonly IMapper _mapper;
        private ITenantRepository _tenantRepository;
        private readonly WellAI.Advisor.DLL.Data.WebAIAdvisorContext _wdb;
        public WellRepository(WellAIAdvisiorContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper, ITenantRepository tenantRepository, WellAI.Advisor.DLL.Data.WebAIAdvisorContext wdb)
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
        public bool WellExists(string uid)
        {
            bool value = _db.Wells.Any(x => x.Uid.ToLower().Trim() == uid.ToLower().Trim());
            return value;
        }

        public bool CreateWell(Well well)
        {
            try
            {
                PcInterest(well);
                WellheadElevation(well);
                Elevation(well);
                WellDatum(well);
                DatumName(well);
                GroundElevation(well);
                WaterDepth(well);
                WellCRS(well);
                GeodeticCRS(well);
                MapProjectionCRS(well);
                LocalCRS(well);
                Easting(well);
                Northing(well);
                WellLocation(well);
                Location(well);
                LocalX(well);
                LocalY(well);
                Latitude(well);
                Longitude(well);
                Longitude(well);
                ReferencePoint(well);
                MeasuredDepth(well);
                YAxisAzimuth(well);
                CommonData(well);
                DefaultDatum(well);
                _db.Wells.Add(well);
                return Save();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "WellRepository CreateWell", null);
                return Save();
            }
        }

        public bool DeleteWell(Well well)
        {
            _db.Wells.Remove(well);
            return Save();
        }

        public Well GetWellDetail(string Uid)
        {
            return _db.Wells.FirstOrDefault(x=>x.Uid==Uid);
            
        }

        public ICollection<Well> GetWellDetails()
        {
            return _db.Wells.OrderBy(x => x.Name).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateWell(Well well)
        {
            try
            {
                UpdatePcInterest(well);
                UpdateWellheadElevation(well);
                UpdateElevation(well);
                UpdateWellDatum(well);
                UpdateDatumName(well);
                UpdateGroundElevation(well);
                UpdateWaterDepth(well);
                UpdateWellCRS(well);
                UpdateGeodeticCRS(well);
                UpdateMapProjectionCRS(well);
                UpdateLocalCRS(well);
                UpdateEasting(well);
                UpdateNorthing(well);
                UpdateWellLocation(well);
                UpdateLocation(well);
                UpdateLocalX(well);
                UpdateLocalY(well);
                UpdateLatitude(well);
                UpdateLongitude(well);
                UpdateLongitude(well);
                UpdateReferencePoint(well);
                UpdateMeasuredDepth(well);
                UpdateYAxisAzimuth(well);
                UpdateCommonData(well);
                UpdateDefaultDatum(well);
                _db.Wells.Update(well);
                return Save();
            }
            catch
            {
                return Save();
            }
        }

        #region Insert Well Data
        private void PcInterest(Well well)
        {
            if (well.PcInterest.Uom != null)
            {
                var obj = _mapper.Map<WellPcInterest>(well.PcInterest);
                _db.WellPcInterests.Add(obj);
            }
        }
        private void WellheadElevation(Well well)
        {
            if (well.PcInterest.Uom != null)
            {
                var obj = _mapper.Map<WellheadElevation>(well.WellheadElevation);
                _db.WellheadElevations.Add(obj);
            }
        }
        private void Elevation(Well well)
        {
            foreach (var item in well.WellDatum)
            {
                if (item.Elevation !=null && item.Elevation.Uom != null)
                {
                    var obj = _mapper.Map<WellElevation>(item.Elevation);
                    _db.WellElevations.Add(obj);
                }
            }
        }
        private void DatumName(Well well)
        {
            foreach (var item in well.WellDatum)
            {
                if (item.DatumName != null && item.DatumName.Code != null)
                {
                    var obj = _mapper.Map<WellDatumName>(item.DatumName);
                    _db.WellDatumNames.Add(obj);
                }
            }
        }
        private void WellDatum(Well well)
        {
            foreach (var item in well.WellDatum)
            {
                if (item!=null && item.Uid != null)
                {
                    var obj = _mapper.Map<WellDatum>(item);
                    _db.WellDatums.Add(obj);
                }
            }
        }
        private void WaterDepth(Well well)
        {
            if (well.WaterDepth.Uom != null)
            {
                var obj = _mapper.Map<WellWaterDepth>(well.WaterDepth);
                _db.WellWaterDepths.Add(obj);
            }
        }
        private void GeodeticCRS(Well well)
        {
            foreach (var item in well.WellCRS)
            {
                if (item.GeodeticCRS!=null && item.GeodeticCRS.UidRef != null)
                {
                    var obj = _mapper.Map<WellGeodeticCRS>(item.GeodeticCRS);
                    _db.WellGeodeticCRSs.Add(obj);
                }
            }
        }

        private void MapProjectionCRS(Well well)
        {
            foreach (var item in well.WellCRS)
            {
                if (item.MapProjectionCRS !=null && item.MapProjectionCRS.UidRef != null)
                {
                    var obj = _mapper.Map<WellMapProjectionCRS>(item.MapProjectionCRS);
                    _db.WellMapProjectionCRSs.Add(obj);
                }
            }
        }
        private void LocalCRS(Well well)
        {
            foreach (var item in well.WellCRS)
            {
                if (item.LocalCRS!=null && item.LocalCRS.UsesWellAsOrigin != null)
                {
                    var obj = _mapper.Map<WellLocalCRS>(item.LocalCRS);
                    _db.WellLocalCRSs.Add(obj);
                }
            }
        }
        private void WellCRS(Well well)
        {
            foreach (var item in well.WellCRS)
            {
                if (item!=null && item.Uid != null)
                {
                    var obj = _mapper.Map<WellCRS>(item);
                    _db.WellCRSs.Add(obj);
                }
            }

            if (well.WellLocation.WellCRS.UidRef != null)
            {
                var obj = _mapper.Map<WellCRS>(well.WellLocation.WellCRS);
                _db.WellCRSs.Add(obj);
            }
            foreach (var item in well.ReferencePoint)
            {
                foreach (var subItem in item.Location)
                {
                    if (subItem.WellCRS!=null && subItem.WellCRS.UidRef != null)
                    {
                        var obj = _mapper.Map<WellCRS>(subItem.WellCRS);
                        _db.WellCRSs.Add(obj);
                    }
                }
            }
        }
        private void Easting(Well well)
        {
            if (well.WellLocation.Easting.Uom != null)
            {
                var obj = _mapper.Map<WellEasting>(well.WellLocation.Easting);
                _db.WellEastings.Add(obj);
            }

            foreach (var item in well.ReferencePoint)
            {
                foreach (var subItem in item.Location)
                {
                    if (subItem.Easting != null && subItem.Easting.Uom != null)
                    {
                        var obj = _mapper.Map<WellEasting>(subItem.Easting);
                        _db.WellEastings.Add(obj);
                    }
                }
            }

        }

        private void Northing(Well well)
        {
            if (well.WellLocation.Northing.Uom != null)
            {
                var obj = _mapper.Map<WellNorthing>(well.WellLocation.Northing);
                _db.WellNorthings.Add(obj);
            }

            foreach (var item in well.ReferencePoint)
            {
                foreach (var subItem in item.Location)
                {
                    if (subItem.Northing != null && subItem.Northing.Uom != null)
                    {
                        var obj = _mapper.Map<WellNorthing>(subItem.Northing);
                        _db.WellNorthings.Add(obj);
                    }
                }
            }

        }
        private void WellLocation(Well well)
        {
            if (well.WellLocation.Uid != null)
            {
                var obj = _mapper.Map<WellLocation>(well.WellLocation);
                _db.WellLocations.Add(obj);
            }

        }

        private void LocalX(Well well)
        {
            foreach (var item in well.ReferencePoint)
            {
                foreach (var subItem in item.Location)
                {
                    if (subItem.LocalX != null && subItem.LocalX.Uom != null)
                    {
                        var obj = _mapper.Map<WellLocalX>(subItem.LocalX);
                        _db.WellLocalXs.Add(obj);
                    }
                }
            }

        }

        private void LocalY(Well well)
        {
            foreach (var item in well.ReferencePoint)
            {
                foreach (var subItem in item.Location)
                {
                    if (subItem.LocalY != null &&  subItem.LocalY.Uom != null)
                    {
                        var obj = _mapper.Map<WellLocalY>(subItem.LocalY);
                        _db.WellLocalYs.Add(obj);
                    }
                }
            }

        }

        private void Latitude(Well well)
        {
            foreach (var item in well.ReferencePoint)
            {
                foreach (var subItem in item.Location)
                {
                    if (subItem.Latitude != null &&  subItem.Latitude.Uom != null)
                    {
                        var obj = _mapper.Map<WellLatitude>(subItem.Latitude);
                        _db.WellLatitudes.Add(obj);
                    }
                }
            }

        }

        private void Longitude(Well well)
        {
            foreach (var item in well.ReferencePoint)
            {
                foreach (var subItem in item.Location)
                {
                    if (subItem.Longitude != null &&  subItem.Longitude.Uom != null)
                    {
                        var obj = _mapper.Map<WellLongitude>(subItem.Longitude);
                        _db.WellLongitudes.Add(obj);
                    }
                }
            }

        }

        private void Location(Well well)
        {
            foreach (var item in well.ReferencePoint)
            {
                foreach (var subItem in item.Location)
                {
                    if (subItem != null &&  subItem.Uid != null)
                    {
                        var obj = _mapper.Map<WellLocation>(subItem);
                        _db.WellLocations.Add(obj);
                    }
                }
            }

        }

        private void MeasuredDepth(Well well)
        {
            foreach (var item in well.ReferencePoint)
            {
                if (item.MeasuredDepth !=null && item.MeasuredDepth.Uom != null)
                {
                    var obj = _mapper.Map<WellMeasuredDepth>(item.MeasuredDepth);
                    _db.WellMeasuredDepths.Add(obj);
                }
            }
        }
        private void ReferencePoint(Well well)
        {
            foreach (var item in well.ReferencePoint)
            {
                if (item != null && item.Uid != null)
                {
                    var obj = _mapper.Map<WellReferencePoint>(item);
                    _db.WellReferencePoints.Add(obj);
                }
            }
        }
        private void YAxisAzimuth(Well well)
        {
            foreach (var item in well.WellCRS)
            {
                if (item.LocalCRS!=null && item.LocalCRS.YAxisAzimuth!=null && item.LocalCRS.YAxisAzimuth.Uom != null)
                {
                    var obj = _mapper.Map<WellYAxisAzimuth>(item.LocalCRS.YAxisAzimuth);
                    _db.WellYAxisAzimuths.Add(obj);
                }
            }
        }
        private void DefaultDatum(Well well)
        {
            if (well.CommonData!=null && well.CommonData.DefaultDatum!=null && well.CommonData.DefaultDatum.UidRef != null)
            {
                var obj = _mapper.Map<WellDefaultDatum>(well.CommonData.DefaultDatum);
                _db.WellDefaultDatums.Add(obj);
            }

        }
        private void CommonData(Well well)
        {
            if (well.CommonData !=null && well.CommonData.ItemState != null)
            {
                var obj = _mapper.Map<WellCommonData>(well.CommonData);
                _db.WellCommonDatas.Add(obj);
            }

        }
        private void GroundElevation(Well well)
        {
            if (well.GroundElevation!=null && well.GroundElevation.Uom != null)
            {
                var obj = _mapper.Map<WellGroundElevation>(well.GroundElevation);
                _db.WellGroundElevations.Add(obj);
            }

        }

        #endregion Insert Well Data


        #region Update Well Data
        private void UpdatePcInterest(Well well)
        {
            if (well.PcInterest.Uom != null)
            {
                var obj = _mapper.Map<WellPcInterest>(well.PcInterest);
                _db.WellPcInterests.Update(obj);
            }
        }
        private void UpdateWellheadElevation(Well well)
        {
            if (well.PcInterest.Uom != null)
            {
                var obj = _mapper.Map<WellheadElevation>(well.WellheadElevation);
                _db.WellheadElevations.Update(obj);
            }
        }
        private void UpdateElevation(Well well)
        {
            foreach (var item in well.WellDatum)
            {
                if (item.Elevation != null && item.Elevation.Uom != null)
                {
                    var obj = _mapper.Map<WellElevation>(item.Elevation);
                    _db.WellElevations.Update(obj);
                }
            }
        }
        private void UpdateDatumName(Well well)
        {
            foreach (var item in well.WellDatum)
            {
                if (item.DatumName != null && item.DatumName.Code != null)
                {
                    var obj = _mapper.Map<WellDatumName>(item.DatumName);
                    _db.WellDatumNames.Update(obj);
                }
            }
        }
        private void UpdateWellDatum(Well well)
        {
            foreach (var item in well.WellDatum)
            {
                if (item != null && item.Uid != null)
                {
                    var obj = _mapper.Map<WellDatum>(item);
                    _db.WellDatums.Update(obj);
                }
            }
        }
        private void UpdateWaterDepth(Well well)
        {
            if (well.WaterDepth.Uom != null)
            {
                var obj = _mapper.Map<WellWaterDepth>(well.WaterDepth);
                _db.WellWaterDepths.Update(obj);
            }
        }
        private void UpdateGeodeticCRS(Well well)
        {
            foreach (var item in well.WellCRS)
            {
                if (item.GeodeticCRS != null && item.GeodeticCRS.UidRef != null)
                {
                    var obj = _mapper.Map<WellGeodeticCRS>(item.GeodeticCRS);
                    _db.WellGeodeticCRSs.Update(obj);
                }
            }
        }

        private void UpdateMapProjectionCRS(Well well)
        {
            foreach (var item in well.WellCRS)
            {
                if (item.MapProjectionCRS != null && item.MapProjectionCRS.UidRef != null)
                {
                    var obj = _mapper.Map<WellMapProjectionCRS>(item.MapProjectionCRS);
                    _db.WellMapProjectionCRSs.Update(obj);
                }
            }
        }
        private void UpdateLocalCRS(Well well)
        {
            foreach (var item in well.WellCRS)
            {
                if (item.LocalCRS != null && item.LocalCRS.UsesWellAsOrigin != null)
                {
                    var obj = _mapper.Map<WellLocalCRS>(item.LocalCRS);
                    _db.WellLocalCRSs.Update(obj);
                }
            }
        }
        private void UpdateWellCRS(Well well)
        {
            foreach (var item in well.WellCRS)
            {
                if (item != null && item.Uid != null)
                {
                    var obj = _mapper.Map<WellCRS>(item);
                    _db.WellCRSs.Update(obj);
                }
            }

            if (well.WellLocation.WellCRS.UidRef != null)
            {
                var obj = _mapper.Map<WellCRS>(well.WellLocation.WellCRS);
                _db.WellCRSs.Update(obj);
            }
            foreach (var item in well.ReferencePoint)
            {
                foreach (var subItem in item.Location)
                {
                    if (subItem.WellCRS != null && subItem.WellCRS.UidRef != null)
                    {
                        var obj = _mapper.Map<WellCRS>(subItem.WellCRS);
                        _db.WellCRSs.Update(obj);
                    }
                }
            }
        }
        private void UpdateEasting(Well well)
        {
            if (well.WellLocation.Easting.Uom != null)
            {
                var obj = _mapper.Map<WellEasting>(well.WellLocation.Easting);
                _db.WellEastings.Update(obj);
            }

            foreach (var item in well.ReferencePoint)
            {
                foreach (var subItem in item.Location)
                {
                    if (subItem.Easting != null && subItem.Easting.Uom != null)
                    {
                        var obj = _mapper.Map<WellEasting>(subItem.Easting);
                        _db.WellEastings.Update(obj);
                    }
                }
            }

        }

        private void UpdateNorthing(Well well)
        {
            if (well.WellLocation.Northing.Uom != null)
            {
                var obj = _mapper.Map<WellNorthing>(well.WellLocation.Northing);
                _db.WellNorthings.Update(obj);
            }

            foreach (var item in well.ReferencePoint)
            {
                foreach (var subItem in item.Location)
                {
                    if (subItem.Northing != null && subItem.Northing.Uom != null)
                    {
                        var obj = _mapper.Map<WellNorthing>(subItem.Northing);
                        _db.WellNorthings.Update(obj);
                    }
                }
            }

        }
        private void UpdateWellLocation(Well well)
        {
            if (well.WellLocation.Uid != null)
            {
                var obj = _mapper.Map<WellLocation>(well.WellLocation);
                _db.WellLocations.Update(obj);
            }

        }

        private void UpdateLocalX(Well well)
        {
            foreach (var item in well.ReferencePoint)
            {
                foreach (var subItem in item.Location)
                {
                    if (subItem.LocalX != null && subItem.LocalX.Uom != null)
                    {
                        var obj = _mapper.Map<WellLocalX>(subItem.LocalX);
                        _db.WellLocalXs.Update(obj);
                    }
                }
            }

        }

        private void UpdateLocalY(Well well)
        {
            foreach (var item in well.ReferencePoint)
            {
                foreach (var subItem in item.Location)
                {
                    if (subItem.LocalY != null && subItem.LocalY.Uom != null)
                    {
                        var obj = _mapper.Map<WellLocalY>(subItem.LocalY);
                        _db.WellLocalYs.Update(obj);
                    }
                }
            }

        }

        private void UpdateLatitude(Well well)
        {
            foreach (var item in well.ReferencePoint)
            {
                foreach (var subItem in item.Location)
                {
                    if (subItem.Latitude != null && subItem.Latitude.Uom != null)
                    {
                        var obj = _mapper.Map<WellLatitude>(subItem.Latitude);
                        _db.WellLatitudes.Update(obj);
                    }
                }
            }

        }

        private void UpdateLongitude(Well well)
        {
            foreach (var item in well.ReferencePoint)
            {
                foreach (var subItem in item.Location)
                {
                    if (subItem.Longitude != null && subItem.Longitude.Uom != null)
                    {
                        var obj = _mapper.Map<WellLongitude>(subItem.Longitude);
                        _db.WellLongitudes.Update(obj);
                    }
                }
            }

        }

        private void UpdateLocation(Well well)
        {
            foreach (var item in well.ReferencePoint)
            {
                foreach (var subItem in item.Location)
                {
                    if (subItem != null && subItem.Uid != null)
                    {
                        var obj = _mapper.Map<WellLocation>(subItem);
                        _db.WellLocations.Update(obj);
                    }
                }
            }

        }

        private void UpdateMeasuredDepth(Well well)
        {
            foreach (var item in well.ReferencePoint)
            {
                if (item.MeasuredDepth != null && item.MeasuredDepth.Uom != null)
                {
                    var obj = _mapper.Map<WellMeasuredDepth>(item);
                    _db.WellMeasuredDepths.Update(obj);
                }
            }
        }
        private void UpdateReferencePoint(Well well)
        {
            foreach (var item in well.ReferencePoint)
            {
                if (item != null && item.Uid != null)
                {
                    var obj = _mapper.Map<WellReferencePoint>(item);
                    _db.WellReferencePoints.Update(obj);
                }
            }
        }
        private void UpdateYAxisAzimuth(Well well)
        {
            foreach (var item in well.WellCRS)
            {
                if (item.LocalCRS != null && item.LocalCRS.YAxisAzimuth != null && item.LocalCRS.YAxisAzimuth.Uom != null)
                {
                    var obj = _mapper.Map<WellYAxisAzimuth>(item.LocalCRS.YAxisAzimuth);
                    _db.WellYAxisAzimuths.Update(obj);
                }
            }
        }
        private void UpdateDefaultDatum(Well well)
        {
            if (well.CommonData != null && well.CommonData.DefaultDatum != null && well.CommonData.DefaultDatum.UidRef != null)
            {
                var obj = _mapper.Map<WellDefaultDatum>(well.CommonData.DefaultDatum);
                _db.WellDefaultDatums.Update(obj);
            }

        }
        private void UpdateCommonData(Well well)
        {
            if (well.CommonData != null && well.CommonData.ItemState != null)
            {
                var obj = _mapper.Map<WellCommonData>(well.CommonData);
                _db.WellCommonDatas.Update(obj);
            }

        }
        private void UpdateGroundElevation(Well well)
        {
            if (well.GroundElevation != null && well.GroundElevation.Uom != null)
            {
                var obj = _mapper.Map<WellGroundElevation>(well.GroundElevation);
                _db.WellGroundElevations.Update(obj);
            }

        }

        #endregion Update Well Data

    }
}
