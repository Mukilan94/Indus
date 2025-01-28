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
    public class TargetRepository : ITargetRepository
    {
        private readonly WellAIAdvisiorContext _db;
        private readonly WebAIAdvisorContext _wdb;
        private readonly IMapper _mapper;
        private ITenantRepository _tenantRepository;
        public TargetRepository(WellAIAdvisiorContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper, ITenantRepository tenantRepository, WebAIAdvisorContext wdb)
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
        public bool TargetExists(string uid)
        {
            bool value = _db.Targets.Any(x => x.Uid.ToLower().Trim() == uid.ToLower().Trim());
            return value;
        }

        public bool CreateTarget(Target target)
        {
            try
            {
                DispNsCenter(target);
                DispEwCenter(target);
                Tvd(target);
                DispNsOffset(target);
                DispEwOffset(target);
                ThickAbove(target);
                TargetSectionThickAbove(target);
                TargetSectionThickBelow(target);
                TargetSectionLocationWellCRS(target);
                TargetSectionLocationLatitude(target);
                TargetSectionLocationLongitude(target);
                ThickBelow(target);
                Dip(target);
                Strike(target);
                Rotation(target);
                LenMajorAxis(target);
                WidMinorAxis(target);
                DispNsSectOrig(target);
                DispEwSectOrig(target);
                WellCRS(target);
                Latitude(target);
                Longitude(target);
                Location(target);
                ProjectedX(target);
                ProjectedY(target);
                LenRadius(target);
                AngleArc(target);
                TargetSection(target);
                TargetCommonData(target);
                _db.Targets.Add(target);
                return Save();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "TargetRepository CreateTarget", null);
                return Save();
            }
        }

      

        public bool DeleteTarget(Target target)
        {
            _db.Targets.Remove(target);
            return Save();
        }

        public Target GetTargetDetail(string Uid)
        {
            return _db.Targets.FirstOrDefault(x=>x.Uid==Uid);
            
        }

        public ICollection<Target> GetTargetDetails()
        {
            return _db.Targets.OrderBy(x => x.NameWell).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateTarget(Target target)
        {
            try
            {
                UpdateDispNsCenter(target);
                UpdateDispEwCenter(target);
                UpdateTvd(target);
                UpdateDispNsOffset(target);
                UpdateDispEwOffset(target);
                UpdateThickAbove(target);
                UpdateTargetSectionThickAbove(target);
                UpdateTargetSectionThickBelow(target);
                UpdateTargetSectionLocationWellCRS(target);
                UpdateTargetSectionLocationLatitude(target);
                UpdateTargetSectionLocationLongitude(target);
                UpdateThickBelow(target);
                UpdateDip(target);
                UpdateStrike(target);
                UpdateRotation(target);
                UpdateLenMajorAxis(target);
                UpdateWidMinorAxis(target);
                UpdateDispNsSectOrig(target);
                UpdateDispEwSectOrig(target);
                UpdateWellCRS(target);
                UpdateLatitude(target);
                UpdateLongitude(target);
                UpdateLocation(target);
                UpdateProjectedX(target);
                UpdateProjectedY(target);
                UpdateLenRadius(target);
                UpdateAngleArc(target);
                UpdateTargetSection(target);
                UpdateTargetCommonData(target);
                _db.Targets.Update(target);
                return Save();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "TargetRepository UpdateTarget", null);
                return Save();
            }
        }

        #region Insert Target
        private void DispNsCenter(Target target)
        {
            if (target.DispNsCenter.Uom != null)
            {
                var obj = _mapper.Map<DispNsCenter>(target.DispNsCenter);
                _db.TargetDispNsCenters.Add(obj);
            }
        }

        private void DispEwCenter(Target target)
        {
            if (target.DispEwCenter.Uom != null)
            {
                var obj = _mapper.Map<DispEwCenter>(target.DispEwCenter);
                _db.TargetDispEwCenters.Add(obj);
            }
        }

        private void Tvd(Target target)
        {
            if (target.Tvd.Uom != null)
            {
                var obj = _mapper.Map<TargetTvd>(target.Tvd);
                _db.TargetTvds.Add(obj);
            }
        }

        private void DispNsOffset(Target target)
        {
            if (target.DispNsOffset.Uom != null)
            {
                var obj = _mapper.Map<DispNsOffset>(target.DispNsOffset);
                _db.TargetDispNsOffsets.Add(obj);
            }
        }

        private void DispEwOffset(Target target)
        {
            if (target.DispEwOffset.Uom != null)
            {
                var obj = _mapper.Map<DispEwOffset>(target.DispEwOffset);
                _db.TargetDispEwOffsets.Add(obj);
            }
        }

        private void ThickAbove(Target target)
        {
            if (target.ThickAbove.Uom != null)
            {
                var obj = _mapper.Map<ThickAbove>(target.ThickAbove);
                _db.TargetThickAboves.Add(obj);
            }
        }
        private void ThickBelow(Target target)
        {
            if (target.ThickBelow.Uom != null)
            {
                var obj = _mapper.Map<ThickBelow>(target.ThickBelow);
                _db.TargetThickBelows.Add(obj);
            }
        }
        private void Dip(Target target)
        {
            if (target.Dip.Uom != null)
            {
                var obj = _mapper.Map<TargetDip>(target.Dip);
                _db.TargetDips.Add(obj);
            }
        }

        private void Strike(Target target)
        {
            if (target.Strike.Uom != null)
            {
                var obj = _mapper.Map<Strike>(target.Strike);
                _db.TargetStrikes.Add(obj);
            }
        }

        private void Rotation(Target target)
        {
            if (target.Rotation.Uom != null)
            {
                var obj = _mapper.Map<Rotation>(target.Rotation);
                _db.TargetRotations.Add(obj);
            }
        }

        private void LenMajorAxis(Target target)
        {
            if (target.LenMajorAxis.Uom != null)
            {
                var obj = _mapper.Map<LenMajorAxis>(target.LenMajorAxis);
                _db.TargetLenMajorAxiss.Add(obj);
            }
        }


        private void WidMinorAxis(Target target)
        {
            if (target.WidMinorAxis.Uom != null)
            {
                var obj = _mapper.Map<WidMinorAxis>(target.WidMinorAxis);
                _db.TargetWidMinorAxiss.Add(obj);
            }
        }
        private void DispNsSectOrig(Target target)
        {
            if (target.DispNsSectOrig.Uom != null)
            {
                var obj = _mapper.Map<DispNsSectOrig>(target.DispNsSectOrig);
                _db.TargetDispNsSectOrigs.Add(obj);
            }
        }
        private void DispEwSectOrig(Target target)
        {
            if (target.DispEwSectOrig.Uom != null)
            {
                var obj = _mapper.Map<DispEwSectOrig>(target.DispEwSectOrig);
                _db.TargetDispEwSectOrigs.Add(obj);
            }
        }

        private void WellCRS(Target target)
        {
            foreach (var item in target.Location)
            {
                if (item.WellCRS!=null && item.WellCRS.UidRef != null)
                {
                    var obj = _mapper.Map<TargetWellCRS>(item.WellCRS);
                    _db.TargetWellCRSs.Add(obj);
                }
            }
        }

        private void Latitude(Target target)
        {
            foreach (var item in target.Location)
            {
                if (item.Latitude!=null && item.Latitude.Uom != null)
                {
                    var obj = _mapper.Map<TargetLatitude>(item.Latitude);
                    _db.TargetLatitudes.Add(obj);
                }
            }
        }
        private void Longitude(Target target)
        {
            foreach (var item in target.Location)
            {
                if (item.Longitude!=null && item.Longitude.Uom != null)
                {
                    var obj = _mapper.Map<TargetLongitude>(item.Longitude);
                    _db.TargetLongitudes.Add(obj);
                }
            }
        }

        private void ProjectedX(Target target)
        {
            foreach (var item in target.Location)
            {
                if (item.ProjectedX!=null && item.ProjectedX.Uom != null)
                {
                    var obj = _mapper.Map<TargetProjectedX>(item.ProjectedX);
                    _db.TargetProjectedXs.Add(obj);
                }
            }
        }

        private void ProjectedY(Target target)
        {
            foreach (var item in target.Location)
            {
                if (item.ProjectedY!=null && item.ProjectedY.Uom != null)
                {
                    var obj = _mapper.Map<TargetProjectedY>(item.ProjectedY);
                    _db.TargetProjectedYs.Add(obj);
                }
            }
        }
       
        private void LenRadius(Target target)
        {
            foreach (var item in target.TargetSection)
            {
                if (item.LenRadius!=null && item.LenRadius.Uom != null)
                {
                    var obj = _mapper.Map<LenRadius>(item.LenRadius);
                    _db.TargetLenRadiuss.Add(obj);
                }
            }
        }
        private void AngleArc(Target target)
        {
            foreach (var item in target.TargetSection)
            {
                if (item.AngleArc!=null && item.AngleArc.Uom != null)
                {
                    var obj = _mapper.Map<AngleArc>(item.AngleArc);
                    _db.TargetAngleArcs.Add(obj);
                }
            }
        }
        private void TargetSectionThickAbove(Target target)
        {
            foreach (var item in target.TargetSection)
            {
                if (item.ThickAbove!=null && item.ThickAbove.Uom != null)
                {
                    var obj = _mapper.Map<ThickAbove>(item.ThickAbove);
                    _db.TargetThickAboves.Add(obj);
                }
            }
        }

        private void TargetSectionThickBelow(Target target)
        {
            foreach (var item in target.TargetSection)
            {
                if (item.ThickBelow!=null && item.ThickBelow.Uom != null)
                {
                    var obj = _mapper.Map<ThickBelow>(item.ThickBelow);
                    _db.TargetThickBelows.Add(obj);
                }
            }
        }
        private void TargetSectionLocationWellCRS(Target target)
        {
            foreach (var item in target.TargetSection)
            {
                foreach (var subItem in item.Location)
                {
                    if (subItem.WellCRS!=null && subItem.WellCRS.UidRef != null)
                    {
                        var obj = _mapper.Map<TargetWellCRS>(subItem.WellCRS);
                        _db.TargetWellCRSs.Add(obj);
                    }
                }
            }
        }

        private void TargetSectionLocationLatitude(Target target)
        {
            foreach (var item in target.TargetSection)
            {
                foreach (var subItem in item.Location)
                {
                    if (subItem.Latitude!=null && subItem.Latitude.Uom != null)
                    {
                        var obj = _mapper.Map<TargetLatitude>(subItem.Latitude);
                        _db.TargetLatitudes.Add(obj);
                    }
                }
            }
        }
        private void TargetSectionLocationLongitude(Target target)
        {
            foreach (var item in target.TargetSection)
            {
                foreach (var subItem in item.Location)
                {
                    if (subItem.Longitude!=null && subItem.Longitude.Uom != null)
                    {
                        var obj = _mapper.Map<TargetLongitude>(subItem.Longitude);
                        _db.TargetLongitudes.Add(obj);
                    }
                }
            }
        }

        private void TargetSection(Target target)
        {
            foreach (var item in target.TargetSection)
            {
                    if (item.Uid != null)
                    {
                        var obj = _mapper.Map<TargetSection>(item);
                        _db.TargetSections.Add(obj);
                    }
            }
        }
        private void Location(Target target)
        {
            
                foreach (var item in target.Location)
                {
                    if (item.Uid != null)
                    {
                        var obj = _mapper.Map<TargetLocation>(item);
                        _db.TargetLocations.Add(obj);
                    }
                }
        }

        private void TargetCommonData(Target target)
        {
                if (target.CommonData!= null)
                {
                    var obj = _mapper.Map<TargetCommonData>(target.CommonData);
                    _db.TargetCommonDatas.Add(obj);
                }
            }

        #endregion Insert Target

        #region Update Target
        private void UpdateDispNsCenter(Target target)
        {
            if (target.DispNsCenter.Uom != null)
            {
                var obj = _mapper.Map<DispNsCenter>(target.DispNsCenter);
                _db.TargetDispNsCenters.Update(obj);
            }
        }

        private void UpdateDispEwCenter(Target target)
        {
            if (target.DispEwCenter.Uom != null)
            {
                var obj = _mapper.Map<DispEwCenter>(target.DispEwCenter);
                _db.TargetDispEwCenters.Update(obj);
            }
        }

        private void UpdateTvd(Target target)
        {
            if (target.Tvd.Uom != null)
            {
                var obj = _mapper.Map<TargetTvd>(target.Tvd);
                _db.TargetTvds.Update(obj);
            }
        }

        private void UpdateDispNsOffset(Target target)
        {
            if (target.DispNsOffset.Uom != null)
            {
                var obj = _mapper.Map<DispNsOffset>(target.DispNsOffset);
                _db.TargetDispNsOffsets.Update(obj);
            }
        }

        private void UpdateDispEwOffset(Target target)
        {
            if (target.DispEwOffset.Uom != null)
            {
                var obj = _mapper.Map<DispEwOffset>(target.DispEwOffset);
                _db.TargetDispEwOffsets.Update(obj);
            }
        }

        private void UpdateThickAbove(Target target)
        {
            if (target.ThickAbove.Uom != null)
            {
                var obj = _mapper.Map<ThickAbove>(target.ThickAbove);
                _db.TargetThickAboves.Update(obj);
            }
        }
        private void UpdateThickBelow(Target target)
        {
            if (target.ThickBelow.Uom != null)
            {
                var obj = _mapper.Map<ThickBelow>(target.ThickBelow);
                _db.TargetThickBelows.Update(obj);
            }
        }
        private void UpdateDip(Target target)
        {
            if (target.Dip.Uom != null)
            {
                var obj = _mapper.Map<TargetDip>(target.Dip);
                _db.TargetDips.Update(obj);
            }
        }

        private void UpdateStrike(Target target)
        {
            if (target.Strike.Uom != null)
            {
                var obj = _mapper.Map<Strike>(target.Strike);
                _db.TargetStrikes.Update(obj);
            }
        }

        private void UpdateRotation(Target target)
        {
            if (target.Rotation.Uom != null)
            {
                var obj = _mapper.Map<Rotation>(target.Rotation);
                _db.TargetRotations.Update(obj);
            }
        }

        private void UpdateLenMajorAxis(Target target)
        {
            if (target.LenMajorAxis.Uom != null)
            {
                var obj = _mapper.Map<LenMajorAxis>(target.LenMajorAxis);
                _db.TargetLenMajorAxiss.Update(obj);
            }
        }


        private void UpdateWidMinorAxis(Target target)
        {
            if (target.WidMinorAxis.Uom != null)
            {
                var obj = _mapper.Map<WidMinorAxis>(target.WidMinorAxis);
                _db.TargetWidMinorAxiss.Update(obj);
            }
        }
        private void UpdateDispNsSectOrig(Target target)
        {
            if (target.DispNsSectOrig.Uom != null)
            {
                var obj = _mapper.Map<DispNsSectOrig>(target.DispNsSectOrig);
                _db.TargetDispNsSectOrigs.Update(obj);
            }
        }
        private void UpdateDispEwSectOrig(Target target)
        {
            if (target.DispEwSectOrig.Uom != null)
            {
                var obj = _mapper.Map<DispEwSectOrig>(target.DispEwSectOrig);
                _db.TargetDispEwSectOrigs.Update(obj);
            }
        }

        private void UpdateWellCRS(Target target)
        {
            foreach (var item in target.Location)
            {
                if (item.WellCRS!=null && item.WellCRS.UidRef != null)
                {
                    var obj = _mapper.Map<TargetWellCRS>(item.WellCRS);
                    _db.TargetWellCRSs.Update(obj);
                }
            }
        }

        private void UpdateLatitude(Target target)
        {
            foreach (var item in target.Location)
            {
                if (item.Latitude!=null && item.Latitude.Uom != null)
                {
                    var obj = _mapper.Map<TargetLatitude>(item.Latitude);
                    _db.TargetLatitudes.Update(obj);
                }
            }
        }
        private void UpdateLongitude(Target target)
        {
            foreach (var item in target.Location)
            {
                if (item.Longitude!=null && item.Longitude.Uom != null)
                {
                    var obj = _mapper.Map<TargetLongitude>(item.Longitude);
                    _db.TargetLongitudes.Update(obj);
                }
            }
        }

        private void UpdateProjectedX(Target target)
        {
            foreach (var item in target.Location)
            {
                if (item.ProjectedX!=null && item.ProjectedX.Uom != null)
                {
                    var obj = _mapper.Map<TargetProjectedX>(item.ProjectedX);
                    _db.TargetProjectedXs.Update(obj);
                }
            }
        }

        private void UpdateProjectedY(Target target)
        {
            foreach (var item in target.Location)
            {
                if (item.ProjectedY!=null && item.ProjectedY.Uom != null)
                {
                    var obj = _mapper.Map<TargetProjectedY>(item.ProjectedY);
                    _db.TargetProjectedYs.Update(obj);
                }
            }
        }

        private void UpdateLenRadius(Target target)
        {
            foreach (var item in target.TargetSection)
            {
                if (item.LenRadius!=null && item.LenRadius.Uom != null)
                {
                    var obj = _mapper.Map<LenRadius>(item.LenRadius);
                    _db.TargetLenRadiuss.Update(obj);
                }
            }
        }
        private void UpdateAngleArc(Target target)
        {
            foreach (var item in target.TargetSection)
            {
                if (item.AngleArc!=null && item.AngleArc.Uom != null)
                {
                    var obj = _mapper.Map<AngleArc>(item.AngleArc);
                    _db.TargetAngleArcs.Update(obj);
                }
            }
        }
        private void UpdateTargetSectionThickAbove(Target target)
        {
            foreach (var item in target.TargetSection)
            {
                if (item.ThickAbove!=null && item.ThickAbove.Uom != null)
                {
                    var obj = _mapper.Map<ThickAbove>(item.ThickAbove);
                    _db.TargetThickAboves.Update(obj);
                }
            }
        }

        private void UpdateTargetSectionThickBelow(Target target)
        {
            foreach (var item in target.TargetSection)
            {
                if (item.ThickBelow!=null && item.ThickBelow.Uom != null)
                {
                    var obj = _mapper.Map<ThickBelow>(item.ThickBelow);
                    _db.TargetThickBelows.Update(obj);
                }
            }
        }
        private void UpdateTargetSectionLocationWellCRS(Target target)
        {
            foreach (var item in target.TargetSection)
            {
                foreach (var subItem in item.Location)
                {
                    if (subItem.WellCRS!=null && subItem.WellCRS.UidRef != null)
                    {
                        var obj = _mapper.Map<TargetWellCRS>(subItem.WellCRS);
                        _db.TargetWellCRSs.Update(obj);
                    }
                }
            }
        }

        private void UpdateTargetSectionLocationLatitude(Target target)
        {
            foreach (var item in target.TargetSection)
            {
                foreach (var subItem in item.Location)
                {
                    if (subItem.Latitude!=null && subItem.Latitude.Uom != null)
                    {
                        var obj = _mapper.Map<TargetLatitude>(subItem.Latitude);
                        _db.TargetLatitudes.Update(obj);
                    }
                }
            }
        }
        private void UpdateTargetSectionLocationLongitude(Target target)
        {
            foreach (var item in target.TargetSection)
            {
                foreach (var subItem in item.Location)
                {
                    if (subItem.Longitude!=null && subItem.Longitude.Uom != null)
                    {
                        var obj = _mapper.Map<TargetLongitude>(subItem.Longitude);
                        _db.TargetLongitudes.Update(obj);
                    }
                }
            }
        }

        private void UpdateTargetSection(Target target)
        {
            foreach (var item in target.TargetSection)
            {
                if (item!=null && item.Uid != null)
                {
                    var obj = _mapper.Map<TargetSection>(item);
                    _db.TargetSections.Update(obj);
                }
            }
        }
        private void UpdateLocation(Target target)
        {

            foreach (var item in target.Location)
            {
                if (item != null && item.Uid != null)
                {
                    var obj = _mapper.Map<TargetLocation>(item);
                    _db.TargetLocations.Update(obj);
                }
            }
        }

        private void UpdateTargetCommonData(Target target)
        {
            if (target.CommonData != null)
            {
                var obj = _mapper.Map<TargetCommonData>(target.CommonData);
                _db.TargetCommonDatas.Update(obj);
            }
        }

        #endregion Update Target
    }
}
