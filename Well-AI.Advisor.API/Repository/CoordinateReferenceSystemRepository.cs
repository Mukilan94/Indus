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
    public class CoordinateReferenceSystemRepository : ICoordinateReferenceSystemRepository
    {
        private readonly WellAIAdvisiorContext _db;
        private readonly WebAIAdvisorContext _wdb;
        private readonly IMapper _mapper;
        private ITenantRepository _tenantRepository;
        public CoordinateReferenceSystemRepository(WellAIAdvisiorContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper, ITenantRepository tenantRepository, WebAIAdvisorContext wdb)
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
        public bool CoordinateReferenceSystemExists(string uid)
        {
            bool value = _db.CoordinateReferenceSystem.Any(x => x.Uid.ToLower().Trim() == uid.ToLower().Trim());
            return value;
        }

        public bool CreateCoordinateReferenceSystem(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            try
            {
                NameCRS(coordinateReferenceSystem);
                GmlGeodeticCRS(coordinateReferenceSystem);
                GeodeticCRS(coordinateReferenceSystem);
                ProjectedCRS(coordinateReferenceSystem);
                VerticalCRS(coordinateReferenceSystem);
                Identifier(coordinateReferenceSystem);
                Name(coordinateReferenceSystem);
                AxisDirection(coordinateReferenceSystem);
                CoordinateSystemAxis(coordinateReferenceSystem);
                UsesAxis(coordinateReferenceSystem);
                EllipsoidalCS(coordinateReferenceSystem);
                GreenwichLongitude(coordinateReferenceSystem);
                PrimeMeridian(coordinateReferenceSystem);
                UsesPrimeMeridian(coordinateReferenceSystem);
                SemiMajorAxis(coordinateReferenceSystem);
                InverseFlattening(coordinateReferenceSystem);
                SecondDefiningParameter(coordinateReferenceSystem);
                Ellipsoid(coordinateReferenceSystem);
                UsesEllipsoid(coordinateReferenceSystem);
                GeodeticDatum(coordinateReferenceSystem);
                UsesGeodeticDatum(coordinateReferenceSystem);
                GeodeticCRS(coordinateReferenceSystem);
                OperationParameter(coordinateReferenceSystem);
                UsesParameter(coordinateReferenceSystem);
                OperationMethod(coordinateReferenceSystem);
                UsesMethod(coordinateReferenceSystem);
                Value(coordinateReferenceSystem);
                ValueOfParameter(coordinateReferenceSystem);
                ParameterValue(coordinateReferenceSystem);
                UsesValue(coordinateReferenceSystem);
                Conversion(coordinateReferenceSystem);
                DefinedByConversion(coordinateReferenceSystem);
                UsesEllipsoidalCS(coordinateReferenceSystem);
                GeographicCRS(coordinateReferenceSystem);
                BaseGeographicCRS(coordinateReferenceSystem);
                CartesianCS(coordinateReferenceSystem);
                UsesCartesianCS(coordinateReferenceSystem);
                ProjectedCRS(coordinateReferenceSystem);
                _db.CoordinateReferenceSystem.Add(coordinateReferenceSystem);
                return Save();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "CoordinateReference CreateCoordinateReferenceSystem", null);
                return Save();
            }
        }

        
        public bool DeleteCoordinateReferenceSystem(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            _db.CoordinateReferenceSystem.Remove(coordinateReferenceSystem);
            return Save();
        }

        public CoordinateReferenceSystem GetCoordinateReferenceSystemDetail(string Uid)
        {
            return _db.CoordinateReferenceSystem.FirstOrDefault(x=>x.Uid==Uid);
            
        }

        public ICollection<CoordinateReferenceSystem> GetCoordinateReferenceSystemDetails()
        {
            return _db.CoordinateReferenceSystem.OrderBy(x => x.Name).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateCoordinateReferenceSystem(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            try
            {
                UpdateNameCRS(coordinateReferenceSystem);
                UpdateGmlGeodeticCRS(coordinateReferenceSystem);
                UpdateGeodeticCRS(coordinateReferenceSystem);
                UpdateProjectedCRS(coordinateReferenceSystem);
                UpdateVerticalCRS(coordinateReferenceSystem);
                UpdateIdentifier(coordinateReferenceSystem);
                UpdateName(coordinateReferenceSystem);
                UpdateAxisDirection(coordinateReferenceSystem);
                UpdateCoordinateSystemAxis(coordinateReferenceSystem);
                UpdateUsesAxis(coordinateReferenceSystem);
                UpdateEllipsoidalCS(coordinateReferenceSystem);
                UpdateGreenwichLongitude(coordinateReferenceSystem);
                UpdatePrimeMeridian(coordinateReferenceSystem);
                UpdateUsesPrimeMeridian(coordinateReferenceSystem);
                UpdateSemiMajorAxis(coordinateReferenceSystem);
                UpdateInverseFlattening(coordinateReferenceSystem);
                UpdateSecondDefiningParameter(coordinateReferenceSystem);
                UpdateEllipsoid(coordinateReferenceSystem);
                UpdateUsesEllipsoid(coordinateReferenceSystem);
                UpdateGeodeticDatum(coordinateReferenceSystem);
                UpdateUsesGeodeticDatum(coordinateReferenceSystem);
                UpdateGeodeticCRS(coordinateReferenceSystem);
                UpdateOperationParameter(coordinateReferenceSystem);
                UpdateUsesParameter(coordinateReferenceSystem);
                UpdateOperationMethod(coordinateReferenceSystem);
                UpdateUsesMethod(coordinateReferenceSystem);
                UpdateValue(coordinateReferenceSystem);
                UpdateValueOfParameter(coordinateReferenceSystem);
                UpdateParameterValue(coordinateReferenceSystem);
                UpdateUsesValue(coordinateReferenceSystem);
                UpdateConversion(coordinateReferenceSystem);
                UpdateDefinedByConversion(coordinateReferenceSystem);
                UpdateUsesEllipsoidalCS(coordinateReferenceSystem);
                UpdateGeographicCRS(coordinateReferenceSystem);
                UpdateBaseGeographicCRS(coordinateReferenceSystem);
                UpdateCartesianCS(coordinateReferenceSystem);
                UpdateUsesCartesianCS(coordinateReferenceSystem);
                UpdateProjectedCRS(coordinateReferenceSystem);
                _db.CoordinateReferenceSystem.Update(coordinateReferenceSystem);
                return Save();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "CoordinateReference UpdateCoordinateReference", null);
                return Save();
            }
        }

        #region Insert CoordinateReferenceSystem
        private void NameCRS(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            if (coordinateReferenceSystem.GeodeticCRS.NameCRS.Code != null)
            {
                var obj = _mapper.Map<NameCRS>(coordinateReferenceSystem.GeodeticCRS.NameCRS);
                _db.CoordinateReferenceSystemNameCRS.Add(obj);
            }
            if (coordinateReferenceSystem.GeodeticCRS.NameCRS.Code != null)
            {
                var obj = _mapper.Map<NameCRS>(coordinateReferenceSystem.GeodeticCRS.NameCRS);
                _db.CoordinateReferenceSystemNameCRS.Add(obj);
            }
            
        }

        private void GmlGeodeticCRS(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            if (coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.Id!= null)
            {
                var obj = _mapper.Map<GmlGeodeticCRS>(coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS);
                _db.CoordinateReferenceSystemGmlGeodeticCRS.Add(obj);
            }
           

        }

        private void GeodeticCRS(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            if (coordinateReferenceSystem.GeodeticCRS != null)
            {
                var obj = _mapper.Map<CoordinateReferenceSystemGeodeticCRS>(coordinateReferenceSystem.GeodeticCRS);
                _db.CoordinateReferenceSystemGeodeticCRS.Add(obj);
            }
        }
        private void ProjectedCRS(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            if (coordinateReferenceSystem.ProjectedCRS != null)
            {
                var obj = _mapper.Map<ProjectedCRS>(coordinateReferenceSystem.ProjectedCRS);
                _db.CoordinateReferenceSystemProjectedCRS.Add(obj);
            }
        }
       
        private void VerticalCRS(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            if (coordinateReferenceSystem.VerticalCRS != null)
            {
                var obj = _mapper.Map<VerticalCRS>(coordinateReferenceSystem.VerticalCRS);
                _db.CoordinateReferenceSystemVerticalCRS.Add(obj);
            }
        }

        private void Identifier(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            if (coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.Identifier.CodeSpace != null)
            {
                var obj = _mapper.Map<Identifier>(coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.Identifier);
                _db.CoordinateReferenceSystemIdentifier.Add(obj);
            }

            foreach (var item in coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.EllipsoidalCS.UsesAxis)
            {

                if (item.CoordinateSystemAxis.Identifier.CodeSpace != null)
                {
                    var obj = _mapper.Map<Identifier>(item.CoordinateSystemAxis.Identifier);
                    _db.CoordinateReferenceSystemIdentifier.Add(obj);
                }
            }

            if (coordinateReferenceSystem.ProjectedCRS.Identifier.CodeSpace != null)
            {
                var obj = _mapper.Map<Identifier>(coordinateReferenceSystem.ProjectedCRS.Identifier);
                _db.CoordinateReferenceSystemIdentifier.Add(obj);
            }
        }

        private void Name(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            if (coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.EllipsoidalCS.Name.CodeSpace != null)
            {
                var obj = _mapper.Map<Name>(coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.EllipsoidalCS.Name);
                _db.CoordinateReferenceSystemName.Add(obj);
            }
            foreach (var item in coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum.UsesEllipsoid.Ellipsoid.Name)
            {
                if (item.CodeSpace != null)
                {
                    var obj = _mapper.Map<Name>(item);
                    _db.CoordinateReferenceSystemName.Add(obj);
                }
            }
        }

        private void AxisDirection(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            foreach (var item in coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.EllipsoidalCS.UsesAxis)
            {

            if (item.CoordinateSystemAxis.AxisDirection.CodeSpace != null)
                {
                    var obj = _mapper.Map<AxisDirection>(item.CoordinateSystemAxis.AxisDirection);
                    _db.CoordinateReferenceSystemAxisDirection.Add(obj);
                }
            }
        }

        private void CoordinateSystemAxis(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            foreach (var item in coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.EllipsoidalCS.UsesAxis)
            {

                if (item.CoordinateSystemAxis.Uom != null)
                {
                    var obj = _mapper.Map<CoordinateSystemAxis>(item.CoordinateSystemAxis);
                    _db.CoordinateReferenceSystemCoordinateSystemAxis.Add(obj);
                }
            }
        }
        private void UsesAxis(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            foreach (var item in coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.EllipsoidalCS.UsesAxis)
            {

                if (item.CoordinateSystemAxis != null)
                {
                    var obj = _mapper.Map<UsesAxis>(item);
                    _db.CoordinateReferenceSystemUsesAxis.Add(obj);
                }
            }
        }
        private void EllipsoidalCS(CoordinateReferenceSystem coordinateReferenceSystem)
        {
           
                if (coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.EllipsoidalCS.Id != null)
                {
                    var obj = _mapper.Map<EllipsoidalCS>(coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.EllipsoidalCS);
                    _db.CoordinateReferenceSystemEllipsoidalCS.Add(obj);
                }

                 if (coordinateReferenceSystem.ProjectedCRS.BaseGeographicCRS.GeographicCRS.UsesEllipsoidalCS.EllipsoidalCS.Id != null)
                {
                    var obj = _mapper.Map<EllipsoidalCS>(coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.EllipsoidalCS);
                    _db.CoordinateReferenceSystemEllipsoidalCS.Add(obj);
                }
            
        }

        private void GreenwichLongitude(CoordinateReferenceSystem coordinateReferenceSystem)
        {

            if (coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum.UsesPrimeMeridian.PrimeMeridian.GreenwichLongitude.Uom != null)
            {
                var obj = _mapper.Map<GreenwichLongitude>(coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum.UsesPrimeMeridian.PrimeMeridian.GreenwichLongitude);
                _db.CoordinateReferenceSystemGreenwichLongitude.Add(obj);
            }

        }

        private void PrimeMeridian(CoordinateReferenceSystem coordinateReferenceSystem)
        {

            if (coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum.UsesPrimeMeridian.PrimeMeridian.Id != null)
            {
                var obj = _mapper.Map<PrimeMeridian>(coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum.UsesPrimeMeridian.PrimeMeridian);
                _db.CoordinateReferenceSystemPrimeMeridian.Add(obj);
            }

        }

        private void UsesPrimeMeridian(CoordinateReferenceSystem coordinateReferenceSystem)
        {

            if (coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum.UsesPrimeMeridian != null)
            {
                var obj = _mapper.Map<UsesPrimeMeridian>(coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum.UsesPrimeMeridian);
                _db.CoordinateReferenceSystemUsesPrimeMeridian.Add(obj);
            }

        }

        private void SemiMajorAxis(CoordinateReferenceSystem coordinateReferenceSystem)
        {

            if (coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum.UsesEllipsoid.Ellipsoid.SemiMajorAxis.Uom != null)
            {
                var obj = _mapper.Map<SemiMajorAxis>(coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum.UsesEllipsoid.Ellipsoid.SemiMajorAxis);
                _db.CoordinateReferenceSystemSemiMajorAxis.Add(obj);
            }

        }

        private void InverseFlattening(CoordinateReferenceSystem coordinateReferenceSystem)
        {

            if (coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum.UsesEllipsoid.Ellipsoid.SecondDefiningParameter.InverseFlattening.Uom != null)
            {
                var obj = _mapper.Map<InverseFlattening>(coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum.UsesEllipsoid.Ellipsoid.SecondDefiningParameter.InverseFlattening);
                _db.CoordinateReferenceSystemInverseFlattening.Add(obj);
            }

        }

        private void SecondDefiningParameter(CoordinateReferenceSystem coordinateReferenceSystem)
        {

            if (coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum.UsesEllipsoid.Ellipsoid.SecondDefiningParameter != null)
            {
                var obj = _mapper.Map<SecondDefiningParameter>(coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum.UsesEllipsoid.Ellipsoid.SecondDefiningParameter);
                _db.CoordinateReferenceSystemSecondDefiningParameter.Add(obj);
            }

        }

        private void Ellipsoid(CoordinateReferenceSystem coordinateReferenceSystem)
        {

            if (coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum.UsesEllipsoid.Ellipsoid.Id != null)
            {
                var obj = _mapper.Map<Ellipsoid>(coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum.UsesEllipsoid.Ellipsoid);
                _db.CoordinateReferenceSystemEllipsoid.Add(obj);
            }

        }


        private void UsesEllipsoid(CoordinateReferenceSystem coordinateReferenceSystem)
        {

            if (coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum.UsesEllipsoid.Id != null)
            {
                var obj = _mapper.Map<UsesEllipsoid>(coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum.UsesEllipsoid);
                _db.CoordinateReferenceSystemUsesEllipsoid.Add(obj);
            }

        }

        private void GeodeticDatum(CoordinateReferenceSystem coordinateReferenceSystem)
        {

            if (coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum.Id != null)
            {
                var obj = _mapper.Map<GeodeticDatum>(coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum);
                _db.CoordinateReferenceSystemGeodeticDatum.Add(obj);
            }

        }

        private void UsesGeodeticDatum(CoordinateReferenceSystem coordinateReferenceSystem)
        {

            if (coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum!= null)
            {
                var obj = _mapper.Map<UsesGeodeticDatum>(coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum);
                _db.CoordinateReferenceSystemUsesGeodeticDatum.Add(obj);
            }

        }
        private void OperationParameter(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            foreach (var item in coordinateReferenceSystem.ProjectedCRS.DefinedByConversion.Conversion.UsesMethod.OperationMethod.UsesParameter)
            {

                if (item.OperationParameter.Id!=null)
                {
                    var obj = _mapper.Map<OperationParameter>(item.OperationParameter);
                    _db.CoordinateReferenceSystemOperationParameter.Add(obj);
                }
            }
        }
        private void UsesParameter(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            foreach (var item in coordinateReferenceSystem.ProjectedCRS.DefinedByConversion.Conversion.UsesMethod.OperationMethod.UsesParameter)
            {

                if (item != null)
                {
                    var obj = _mapper.Map<UsesParameter>(item);
                    _db.CoordinateReferenceSystemUsesParameter.Add(obj);
                }
            }
        }
        private void OperationMethod(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            
                if (coordinateReferenceSystem.ProjectedCRS.DefinedByConversion.Conversion.UsesMethod.OperationMethod.Id != null)
                {
                    var obj = _mapper.Map<OperationMethod>(coordinateReferenceSystem.ProjectedCRS.DefinedByConversion.Conversion.UsesMethod.OperationMethod);
                    _db.CoordinateReferenceSystemOperationMethod.Add(obj);
                }
            }
        
        private void UsesMethod(CoordinateReferenceSystem coordinateReferenceSystem)
    {

        if (coordinateReferenceSystem.ProjectedCRS.DefinedByConversion.Conversion.UsesMethod != null)
        {
            var obj = _mapper.Map<UsesMethod>(coordinateReferenceSystem.ProjectedCRS.DefinedByConversion.Conversion.UsesMethod);
            _db.CoordinateReferenceSystemUsesMethod.Add(obj);
        }
    }
        private void Value(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            foreach (var item in coordinateReferenceSystem.ProjectedCRS.DefinedByConversion.Conversion.UsesValue)
            {

                if (item.ParameterValue.Value.Uom != null)
                {
                    var obj = _mapper.Map<CoordinateReferenceSystemValue>(item.ParameterValue.Value);
                    _db.CoordinateReferenceSystemValues.Add(obj);
                }
            }
        }
        private void ValueOfParameter(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            foreach (var item in coordinateReferenceSystem.ProjectedCRS.DefinedByConversion.Conversion.UsesValue)
            {

                if (item.ParameterValue.ValueOfParameter.Href != null)
                {
                    var obj = _mapper.Map<ValueOfParameter>(item.ParameterValue.ValueOfParameter);
                    _db.CoordinateReferenceSystemValueOfParameter.Add(obj);
                }
            }
        }

        private void ParameterValue(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            foreach (var item in coordinateReferenceSystem.ProjectedCRS.DefinedByConversion.Conversion.UsesValue)
            {

                if (item.ParameterValue != null)
                {
                    var obj = _mapper.Map<ParameterValue>(item.ParameterValue);
                    _db.CoordinateReferenceSystemParameterValue.Add(obj);
                }
            }
        }

        private void UsesValue(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            foreach (var item in coordinateReferenceSystem.ProjectedCRS.DefinedByConversion.Conversion.UsesValue)
            {

                if (item != null)
                {
                    var obj = _mapper.Map<UsesValue>(item);
                    _db.CoordinateReferenceSystemUsesValue.Add(obj);
                }
            }
        }
        private void Conversion(CoordinateReferenceSystem coordinateReferenceSystem)
        {
                if (coordinateReferenceSystem.ProjectedCRS.DefinedByConversion.Conversion.Id != null)
                {
                    var obj = _mapper.Map<Conversion>(coordinateReferenceSystem.ProjectedCRS.DefinedByConversion.Conversion);
                    _db.CoordinateReferenceSystemConversion.Add(obj);
                }
            } 
        private void DefinedByConversion(CoordinateReferenceSystem coordinateReferenceSystem)
    {
        if (coordinateReferenceSystem.ProjectedCRS.DefinedByConversion!= null)
        {
            var obj = _mapper.Map<DefinedByConversion>(coordinateReferenceSystem.ProjectedCRS.DefinedByConversion);
            _db.CoordinateReferenceSystemDefinedByConversion.Add(obj);
        }
    }
        private void UsesEllipsoidalCS(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            if (coordinateReferenceSystem.ProjectedCRS.BaseGeographicCRS.GeographicCRS.UsesEllipsoidalCS != null)
            {
                var obj = _mapper.Map<UsesEllipsoidalCS>(coordinateReferenceSystem.ProjectedCRS.BaseGeographicCRS.GeographicCRS.UsesEllipsoidalCS);
                _db.CoordinateReferenceSystemUsesEllipsoidalCS.Add(obj);
            }
        }
        private void GeographicCRS(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            if (coordinateReferenceSystem.ProjectedCRS.BaseGeographicCRS.GeographicCRS.Id != null)
            {
                var obj = _mapper.Map<GeographicCRS>(coordinateReferenceSystem.ProjectedCRS.BaseGeographicCRS.GeographicCRS);
                _db.CoordinateReferenceSystemGeographicCRS.Add(obj);
            }
        }
        private void BaseGeographicCRS(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            if (coordinateReferenceSystem.ProjectedCRS.BaseGeographicCRS != null)
            {
                var obj = _mapper.Map<BaseGeographicCRS>(coordinateReferenceSystem.ProjectedCRS.BaseGeographicCRS);
                _db.CoordinateReferenceSystemBaseGeographicCRS.Add(obj);
            }
        }
        private void CartesianCS(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            if (coordinateReferenceSystem.ProjectedCRS.UsesCartesianCS.CartesianCS.Id != null)
            {
                var obj = _mapper.Map<CartesianCS>(coordinateReferenceSystem.ProjectedCRS.UsesCartesianCS.CartesianCS);
                _db.CoordinateReferenceSystemCartesianCS.Add(obj);
            }
        }

        private void UsesCartesianCS(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            if (coordinateReferenceSystem.ProjectedCRS.UsesCartesianCS != null)
            {
                var obj = _mapper.Map<UsesCartesianCS>(coordinateReferenceSystem.ProjectedCRS.UsesCartesianCS);
                _db.CoordinateReferenceSystemUsesCartesianCS.Add(obj);
            }
        }
        #endregion Insert CoordinateReferenceSystem

        #region Update CoordinateReferenceSystem
        private void UpdateNameCRS(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            if (coordinateReferenceSystem.GeodeticCRS.NameCRS.Code != null)
            {
                var obj = _mapper.Map<NameCRS>(coordinateReferenceSystem.GeodeticCRS.NameCRS);
                _db.CoordinateReferenceSystemNameCRS.Update(obj);
            }
            if (coordinateReferenceSystem.GeodeticCRS.NameCRS.Code != null)
            {
                var obj = _mapper.Map<NameCRS>(coordinateReferenceSystem.GeodeticCRS.NameCRS);
                _db.CoordinateReferenceSystemNameCRS.Update(obj);
            }

        }

        private void UpdateGmlGeodeticCRS(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            if (coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.Id != null)
            {
                var obj = _mapper.Map<GmlGeodeticCRS>(coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS);
                _db.CoordinateReferenceSystemGmlGeodeticCRS.Update(obj);
            }


        }

        private void UpdateGeodeticCRS(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            if (coordinateReferenceSystem.GeodeticCRS != null)
            {
                var obj = _mapper.Map<CoordinateReferenceSystemGeodeticCRS>(coordinateReferenceSystem.GeodeticCRS);
                _db.CoordinateReferenceSystemGeodeticCRS.Update(obj);
            }
        }
        private void UpdateProjectedCRS(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            if (coordinateReferenceSystem.ProjectedCRS != null)
            {
                var obj = _mapper.Map<ProjectedCRS>(coordinateReferenceSystem.ProjectedCRS);
                _db.CoordinateReferenceSystemProjectedCRS.Update(obj);
            }
        }

        private void UpdateVerticalCRS(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            if (coordinateReferenceSystem.VerticalCRS != null)
            {
                var obj = _mapper.Map<VerticalCRS>(coordinateReferenceSystem.VerticalCRS);
                _db.CoordinateReferenceSystemVerticalCRS.Update(obj);
            }
        }

        private void UpdateIdentifier(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            if (coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.Identifier.CodeSpace != null)
            {
                var obj = _mapper.Map<Identifier>(coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.Identifier);
                _db.CoordinateReferenceSystemIdentifier.Update(obj);
            }

            foreach (var item in coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.EllipsoidalCS.UsesAxis)
            {

                if (item.CoordinateSystemAxis.Identifier.CodeSpace != null)
                {
                    var obj = _mapper.Map<Identifier>(item.CoordinateSystemAxis.Identifier);
                    _db.CoordinateReferenceSystemIdentifier.Update(obj);
                }
            }

            if (coordinateReferenceSystem.ProjectedCRS.Identifier.CodeSpace != null)
            {
                var obj = _mapper.Map<Identifier>(coordinateReferenceSystem.ProjectedCRS.Identifier);
                _db.CoordinateReferenceSystemIdentifier.Update(obj);
            }
        }

        private void UpdateName(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            if (coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.EllipsoidalCS.Name.CodeSpace != null)
            {
                var obj = _mapper.Map<Name>(coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.EllipsoidalCS.Name);
                _db.CoordinateReferenceSystemName.Update(obj);
            }
            foreach (var item in coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum.UsesEllipsoid.Ellipsoid.Name)
            {
                if (item.CodeSpace != null)
                {
                    var obj = _mapper.Map<Name>(item);
                    _db.CoordinateReferenceSystemName.Update(obj);
                }
            }
        }

        private void UpdateAxisDirection(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            foreach (var item in coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.EllipsoidalCS.UsesAxis)
            {

                if (item.CoordinateSystemAxis.AxisDirection.CodeSpace != null)
                {
                    var obj = _mapper.Map<AxisDirection>(item.CoordinateSystemAxis.AxisDirection);
                    _db.CoordinateReferenceSystemAxisDirection.Update(obj);
                }
            }
        }

        private void UpdateCoordinateSystemAxis(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            foreach (var item in coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.EllipsoidalCS.UsesAxis)
            {

                if (item.CoordinateSystemAxis.Uom != null)
                {
                    var obj = _mapper.Map<CoordinateSystemAxis>(item.CoordinateSystemAxis);
                    _db.CoordinateReferenceSystemCoordinateSystemAxis.Update(obj);
                }
            }
        }
        private void UpdateUsesAxis(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            foreach (var item in coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.EllipsoidalCS.UsesAxis)
            {

                if (item.CoordinateSystemAxis != null)
                {
                    var obj = _mapper.Map<UsesAxis>(item);
                    _db.CoordinateReferenceSystemUsesAxis.Update(obj);
                }
            }
        }
        private void UpdateEllipsoidalCS(CoordinateReferenceSystem coordinateReferenceSystem)
        {

            if (coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.EllipsoidalCS.Id != null)
            {
                var obj = _mapper.Map<EllipsoidalCS>(coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.EllipsoidalCS);
                _db.CoordinateReferenceSystemEllipsoidalCS.Update(obj);
            }

            if (coordinateReferenceSystem.ProjectedCRS.BaseGeographicCRS.GeographicCRS.UsesEllipsoidalCS.EllipsoidalCS.Id != null)
            {
                var obj = _mapper.Map<EllipsoidalCS>(coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.EllipsoidalCS);
                _db.CoordinateReferenceSystemEllipsoidalCS.Update(obj);
            }

        }

        private void UpdateGreenwichLongitude(CoordinateReferenceSystem coordinateReferenceSystem)
        {

            if (coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum.UsesPrimeMeridian.PrimeMeridian.GreenwichLongitude.Uom != null)
            {
                var obj = _mapper.Map<GreenwichLongitude>(coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum.UsesPrimeMeridian.PrimeMeridian.GreenwichLongitude);
                _db.CoordinateReferenceSystemGreenwichLongitude.Update(obj);
            }

        }

        private void UpdatePrimeMeridian(CoordinateReferenceSystem coordinateReferenceSystem)
        {

            if (coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum.UsesPrimeMeridian.PrimeMeridian.Id != null)
            {
                var obj = _mapper.Map<PrimeMeridian>(coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum.UsesPrimeMeridian.PrimeMeridian);
                _db.CoordinateReferenceSystemPrimeMeridian.Update(obj);
            }

        }

        private void UpdateUsesPrimeMeridian(CoordinateReferenceSystem coordinateReferenceSystem)
        {

            if (coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum.UsesPrimeMeridian != null)
            {
                var obj = _mapper.Map<UsesPrimeMeridian>(coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum.UsesPrimeMeridian);
                _db.CoordinateReferenceSystemUsesPrimeMeridian.Update(obj);
            }

        }

        private void UpdateSemiMajorAxis(CoordinateReferenceSystem coordinateReferenceSystem)
        {

            if (coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum.UsesEllipsoid.Ellipsoid.SemiMajorAxis.Uom != null)
            {
                var obj = _mapper.Map<SemiMajorAxis>(coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum.UsesEllipsoid.Ellipsoid.SemiMajorAxis);
                _db.CoordinateReferenceSystemSemiMajorAxis.Update(obj);
            }

        }

        private void UpdateInverseFlattening(CoordinateReferenceSystem coordinateReferenceSystem)
        {

            if (coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum.UsesEllipsoid.Ellipsoid.SecondDefiningParameter.InverseFlattening.Uom != null)
            {
                var obj = _mapper.Map<InverseFlattening>(coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum.UsesEllipsoid.Ellipsoid.SecondDefiningParameter.InverseFlattening);
                _db.CoordinateReferenceSystemInverseFlattening.Update(obj);
            }

        }

        private void UpdateSecondDefiningParameter(CoordinateReferenceSystem coordinateReferenceSystem)
        {

            if (coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum.UsesEllipsoid.Ellipsoid.SecondDefiningParameter != null)
            {
                var obj = _mapper.Map<SecondDefiningParameter>(coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum.UsesEllipsoid.Ellipsoid.SecondDefiningParameter);
                _db.CoordinateReferenceSystemSecondDefiningParameter.Update(obj);
            }

        }

        private void UpdateEllipsoid(CoordinateReferenceSystem coordinateReferenceSystem)
        {

            if (coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum.UsesEllipsoid.Ellipsoid.Id != null)
            {
                var obj = _mapper.Map<Ellipsoid>(coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum.UsesEllipsoid.Ellipsoid);
                _db.CoordinateReferenceSystemEllipsoid.Update(obj);
            }

        }


        private void UpdateUsesEllipsoid(CoordinateReferenceSystem coordinateReferenceSystem)
        {

            if (coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum.UsesEllipsoid.Id != null)
            {
                var obj = _mapper.Map<UsesEllipsoid>(coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum.UsesEllipsoid);
                _db.CoordinateReferenceSystemUsesEllipsoid.Update(obj);
            }

        }

        private void UpdateGeodeticDatum(CoordinateReferenceSystem coordinateReferenceSystem)
        {

            if (coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum.Id != null)
            {
                var obj = _mapper.Map<GeodeticDatum>(coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum.GeodeticDatum);
                _db.CoordinateReferenceSystemGeodeticDatum.Update(obj);
            }

        }

        private void UpdateUsesGeodeticDatum(CoordinateReferenceSystem coordinateReferenceSystem)
        {

            if (coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum != null)
            {
                var obj = _mapper.Map<UsesGeodeticDatum>(coordinateReferenceSystem.GeodeticCRS.GmlGeodeticCRS.UsesGeodeticDatum);
                _db.CoordinateReferenceSystemUsesGeodeticDatum.Update(obj);
            }

        }
        private void UpdateOperationParameter(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            foreach (var item in coordinateReferenceSystem.ProjectedCRS.DefinedByConversion.Conversion.UsesMethod.OperationMethod.UsesParameter)
            {

                if (item.OperationParameter.Id != null)
                {
                    var obj = _mapper.Map<OperationParameter>(item.OperationParameter);
                    _db.CoordinateReferenceSystemOperationParameter.Update(obj);
                }
            }
        }
        private void UpdateUsesParameter(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            foreach (var item in coordinateReferenceSystem.ProjectedCRS.DefinedByConversion.Conversion.UsesMethod.OperationMethod.UsesParameter)
            {

                if (item != null)
                {
                    var obj = _mapper.Map<UsesParameter>(item);
                    _db.CoordinateReferenceSystemUsesParameter.Update(obj);
                }
            }
        }
        private void UpdateOperationMethod(CoordinateReferenceSystem coordinateReferenceSystem)
        {

            if (coordinateReferenceSystem.ProjectedCRS.DefinedByConversion.Conversion.UsesMethod.OperationMethod.Id != null)
            {
                var obj = _mapper.Map<OperationMethod>(coordinateReferenceSystem.ProjectedCRS.DefinedByConversion.Conversion.UsesMethod.OperationMethod);
                _db.CoordinateReferenceSystemOperationMethod.Update(obj);
            }
        }

        private void UpdateUsesMethod(CoordinateReferenceSystem coordinateReferenceSystem)
        {

            if (coordinateReferenceSystem.ProjectedCRS.DefinedByConversion.Conversion.UsesMethod != null)
            {
                var obj = _mapper.Map<UsesMethod>(coordinateReferenceSystem.ProjectedCRS.DefinedByConversion.Conversion.UsesMethod);
                _db.CoordinateReferenceSystemUsesMethod.Update(obj);
            }
        }
        private void UpdateValue(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            foreach (var item in coordinateReferenceSystem.ProjectedCRS.DefinedByConversion.Conversion.UsesValue)
            {

                if (item.ParameterValue.Value.Uom != null)
                {
                    var obj = _mapper.Map<CoordinateReferenceSystemValue>(item.ParameterValue.Value);
                    _db.CoordinateReferenceSystemValues.Update(obj);
                }
            }
        }
        private void UpdateValueOfParameter(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            foreach (var item in coordinateReferenceSystem.ProjectedCRS.DefinedByConversion.Conversion.UsesValue)
            {

                if (item.ParameterValue.ValueOfParameter.Href != null)
                {
                    var obj = _mapper.Map<ValueOfParameter>(item.ParameterValue.ValueOfParameter);
                    _db.CoordinateReferenceSystemValueOfParameter.Update(obj);
                }
            }
        }

        private void UpdateParameterValue(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            foreach (var item in coordinateReferenceSystem.ProjectedCRS.DefinedByConversion.Conversion.UsesValue)
            {

                if (item.ParameterValue != null)
                {
                    var obj = _mapper.Map<ParameterValue>(item.ParameterValue);
                    _db.CoordinateReferenceSystemParameterValue.Update(obj);
                }
            }
        }

        private void UpdateUsesValue(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            foreach (var item in coordinateReferenceSystem.ProjectedCRS.DefinedByConversion.Conversion.UsesValue)
            {

                if (item != null)
                {
                    var obj = _mapper.Map<UsesValue>(item);
                    _db.CoordinateReferenceSystemUsesValue.Update(obj);
                }
            }
        }
        private void UpdateConversion(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            if (coordinateReferenceSystem.ProjectedCRS.DefinedByConversion.Conversion.Id != null)
            {
                var obj = _mapper.Map<Conversion>(coordinateReferenceSystem.ProjectedCRS.DefinedByConversion.Conversion);
                _db.CoordinateReferenceSystemConversion.Update(obj);
            }
        }
        private void UpdateDefinedByConversion(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            if (coordinateReferenceSystem.ProjectedCRS.DefinedByConversion != null)
            {
                var obj = _mapper.Map<DefinedByConversion>(coordinateReferenceSystem.ProjectedCRS.DefinedByConversion);
                _db.CoordinateReferenceSystemDefinedByConversion.Update(obj);
            }
        }
        private void UpdateUsesEllipsoidalCS(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            if (coordinateReferenceSystem.ProjectedCRS.BaseGeographicCRS.GeographicCRS.UsesEllipsoidalCS != null)
            {
                var obj = _mapper.Map<UsesEllipsoidalCS>(coordinateReferenceSystem.ProjectedCRS.BaseGeographicCRS.GeographicCRS.UsesEllipsoidalCS);
                _db.CoordinateReferenceSystemUsesEllipsoidalCS.Update(obj);
            }
        }
        private void UpdateGeographicCRS(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            if (coordinateReferenceSystem.ProjectedCRS.BaseGeographicCRS.GeographicCRS.Id != null)
            {
                var obj = _mapper.Map<GeographicCRS>(coordinateReferenceSystem.ProjectedCRS.BaseGeographicCRS.GeographicCRS);
                _db.CoordinateReferenceSystemGeographicCRS.Update(obj);
            }
        }
        private void UpdateBaseGeographicCRS(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            if (coordinateReferenceSystem.ProjectedCRS.BaseGeographicCRS != null)
            {
                var obj = _mapper.Map<BaseGeographicCRS>(coordinateReferenceSystem.ProjectedCRS.BaseGeographicCRS);
                _db.CoordinateReferenceSystemBaseGeographicCRS.Update(obj);
            }
        }
        private void UpdateCartesianCS(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            if (coordinateReferenceSystem.ProjectedCRS.UsesCartesianCS.CartesianCS.Id != null)
            {
                var obj = _mapper.Map<CartesianCS>(coordinateReferenceSystem.ProjectedCRS.UsesCartesianCS.CartesianCS);
                _db.CoordinateReferenceSystemCartesianCS.Update(obj);
            }
        }

        private void UpdateUsesCartesianCS(CoordinateReferenceSystem coordinateReferenceSystem)
        {
            if (coordinateReferenceSystem.ProjectedCRS.UsesCartesianCS != null)
            {
                var obj = _mapper.Map<UsesCartesianCS>(coordinateReferenceSystem.ProjectedCRS.UsesCartesianCS);
                _db.CoordinateReferenceSystemUsesCartesianCS.Update(obj);
            }
        }
        #endregion Update CoordinateReferenceSystem
    }
}
