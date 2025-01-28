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
    public class ObjectGroupRepository : IObjectGroupRepository
    {
        private readonly WellAIAdvisiorContext _db;
        private readonly IMapper _mapper;
        private ITenantRepository _tenantRepository;
        private readonly WellAI.Advisor.DLL.Data.WebAIAdvisorContext _wdb;
        public ObjectGroupRepository(WellAIAdvisiorContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper, ITenantRepository tenantRepository, WellAI.Advisor.DLL.Data.WebAIAdvisorContext wdb)
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
        public bool ObjectGroupExists(string uid)
        {
            bool value = _db.ObjectGroups.Any(x => x.Uid.ToLower().Trim() == uid.ToLower().Trim());
            return value;
        }

        public bool CreateObjectGroup(ObjectGroup objectGroup)
        {
            try
            {
                Param(objectGroup);
                ObjectReference(objectGroup);
                Sequence1(objectGroup);
                Sequence2(objectGroup);
                Sequence3(objectGroup);
                RangeMin(objectGroup);
                RangeMax(objectGroup);
                ReferenceDepth(objectGroup);
                Value(objectGroup);
                Md(objectGroup);
                ExtensionNameValue(objectGroup);
                MemberObject(objectGroup);
                AcquisitionTimeZone(objectGroup);
                DefaultDatum(objectGroup);
                CommonData(objectGroup);
                _db.ObjectGroups.Add(objectGroup);
                return Save();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "ObjectGroupRepository CreateObjectGroup", null);
                return Save();
            }
        }

        

        public bool DeleteObjectGroup(ObjectGroup objectGroup)
        {
            _db.ObjectGroups.Remove(objectGroup);
            return Save();
        }

        public ObjectGroup GetObjectGroupDetail(string Uid)
        {
            return _db.ObjectGroups.FirstOrDefault(x=>x.Uid==Uid);
            
        }

        public ICollection<ObjectGroup> GetObjectGroupDetails()
        {
            return _db.ObjectGroups.OrderBy(x => x.NameWell).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateObjectGroup(ObjectGroup objectGroup)
        {
            _db.ObjectGroups.Update(objectGroup);
            return Save();
        }

        #region Insert ObjectGroup
        private void Param(ObjectGroup objectGroup)
        {
            if (objectGroup.Param.Uom != null)
            {
                var obj = _mapper.Map<ObjectGroupParam>(objectGroup.Param);
                _db.ObjectGroupParam.Add(obj);
            }
            if (objectGroup.MemberObject.Param.Uom != null)
            {
                var obj = _mapper.Map<ObjectGroupParam>(objectGroup.MemberObject.Param);
                _db.ObjectGroupParam.Add(obj);
            }
        }
        private void ObjectReference(ObjectGroup objectGroup)
        {
            if (objectGroup.MemberObject.ObjectReference.UidRef != null)
            {
                var obj = _mapper.Map<ObjectGroupObjectReference>(objectGroup.MemberObject.ObjectReference);
                _db.ObjectGroupObjectReference.Add(obj);
            }
        }
        private void Sequence1(ObjectGroup objectGroup)
        {
            if (objectGroup.MemberObject.Sequence1 != null)
            {
                var obj = _mapper.Map<ObjectGroupSequence1>(objectGroup.MemberObject.Sequence1);
                _db.ObjectGroupSequence1s.Add(obj);
            }
        }
        private void Sequence2(ObjectGroup objectGroup)
        {
            if (objectGroup.MemberObject.Sequence2 != null)
            {
                var obj = _mapper.Map<ObjectGroupSequence2>(objectGroup.MemberObject.Sequence2);
                _db.ObjectGroupSequence2s.Add(obj);
            }
        }
        private void Sequence3(ObjectGroup objectGroup)
        {
            if (objectGroup.MemberObject.Sequence3 != null)
            {
                var obj = _mapper.Map<ObjectGroupSequence3>(objectGroup.MemberObject.Sequence3);
                _db.ObjectGroupSequence3s.Add(obj);
            }
        }
        private void RangeMin(ObjectGroup objectGroup)
        {
            if (objectGroup.MemberObject.RangeMin.Uom != null)
            {
                var obj = _mapper.Map<ObjectGroupRangeMin>(objectGroup.MemberObject.RangeMin);
                _db.ObjectGroupRangeMins.Add(obj);
            }
        }

        private void RangeMax(ObjectGroup objectGroup)
        {
            if (objectGroup.MemberObject.RangeMax.Uom != null)
            {
                var obj = _mapper.Map<ObjectGroupRangeMax>(objectGroup.MemberObject.RangeMax);
                _db.ObjectGroupRangeMaxs.Add(obj);
            }
        }
        private void ReferenceDepth(ObjectGroup objectGroup)
        {
            if (objectGroup.MemberObject.ReferenceDepth.Uom != null)
            {
                var obj = _mapper.Map<ObjectGroupReferenceDepth>(objectGroup.MemberObject.ReferenceDepth);
                _db.ObjectGroupReferenceDepths.Add(obj);
            }
        }
        private void Value(ObjectGroup objectGroup)
        {
            if (objectGroup.MemberObject.ExtensionNameValue.Value.Uom != null)
            {
                var obj = _mapper.Map<ObjectGroupValue>(objectGroup.MemberObject.ExtensionNameValue.Value);
                _db.ObjectGroupValue.Add(obj);
            }
        }
        private void Md(ObjectGroup objectGroup)
        {
            if (objectGroup.MemberObject.ExtensionNameValue.Md.Uom != null)
            {
                var obj = _mapper.Map<ObjectGroupMd>(objectGroup.MemberObject.ExtensionNameValue.Md);
                _db.ObjectGroupMd.Add(obj);
            }
        }
        private void ExtensionNameValue(ObjectGroup objectGroup)
        {
            if (objectGroup.MemberObject.ExtensionNameValue.Uid != null)
            {
                var obj = _mapper.Map<ObjectGroupExtensionNameValue>(objectGroup.MemberObject.ExtensionNameValue);
                _db.ObjectGroupExtensionNameValues.Add(obj);
            }
            if (objectGroup.CommonData.ExtensionNameValue.Uid != null)
            {
                var obj = _mapper.Map<ObjectGroupExtensionNameValue>(objectGroup.CommonData.ExtensionNameValue);
                _db.ObjectGroupExtensionNameValues.Add(obj);
            }
        }
        private void MemberObject(ObjectGroup objectGroup)
        {
            if (objectGroup.MemberObject.Uid != null)
            {
                var obj = _mapper.Map<ObjectGroupMemberObject>(objectGroup.MemberObject);
                _db.ObjectGroupMemberObjects.Add(obj);
            }
        }
        private void AcquisitionTimeZone(ObjectGroup objectGroup)
        {
            if (objectGroup.CommonData.AcquisitionTimeZone.DTim != null)
            {
                var obj = _mapper.Map<ObjectGroupAcquisitionTimeZone>(objectGroup.CommonData.AcquisitionTimeZone);
                _db.ObjectGroupAcquisitionTimeZones.Add(obj);
            }
        }
        private void DefaultDatum(ObjectGroup objectGroup)
        {
            if (objectGroup.CommonData.DefaultDatum.UidRef != null)
            {
                var obj = _mapper.Map<ObjectGroupDefaultDatum>(objectGroup.CommonData.DefaultDatum);
                _db.ObjectGroupDefaultDatum.Add(obj);
            }
        }

        private void CommonData(ObjectGroup objectGroup)
        {
            if (objectGroup.CommonData.SourceName != null)
            {
                var obj = _mapper.Map<ObjectGroupCommonData>(objectGroup.CommonData);
                _db.ObjectGroupCommonDatas.Add(obj);
            }
        }

        #endregion Insert ObjectGroup

        #region Update ObjectGroup
        private void UpdateParam(ObjectGroup objectGroup)
        {
            if (objectGroup.Param.Uom != null)
            {
                var obj = _mapper.Map<ObjectGroupParam>(objectGroup.Param);
                _db.ObjectGroupParam.Update(obj);
            }
            if (objectGroup.MemberObject.Param.Uom != null)
            {
                var obj = _mapper.Map<ObjectGroupParam>(objectGroup.MemberObject.Param);
                _db.ObjectGroupParam.Update(obj);
            }
        }
        private void UpdateObjectReference(ObjectGroup objectGroup)
        {
            if (objectGroup.MemberObject.ObjectReference.UidRef != null)
            {
                var obj = _mapper.Map<ObjectGroupObjectReference>(objectGroup.MemberObject.ObjectReference);
                _db.ObjectGroupObjectReference.Update(obj);
            }
        }
        private void UpdateSequence1(ObjectGroup objectGroup)
        {
            if (objectGroup.MemberObject.Sequence1 != null)
            {
                var obj = _mapper.Map<ObjectGroupSequence1>(objectGroup.MemberObject.Sequence1);
                _db.ObjectGroupSequence1s.Update(obj);
            }
        }
        private void UpdateSequence2(ObjectGroup objectGroup)
        {
            if (objectGroup.MemberObject.Sequence2 != null)
            {
                var obj = _mapper.Map<ObjectGroupSequence2>(objectGroup.MemberObject.Sequence2);
                _db.ObjectGroupSequence2s.Update(obj);
            }
        }
        private void UpdateSequence3(ObjectGroup objectGroup)
        {
            if (objectGroup.MemberObject.Sequence3 != null)
            {
                var obj = _mapper.Map<ObjectGroupSequence3>(objectGroup.MemberObject.Sequence3);
                _db.ObjectGroupSequence3s.Update(obj);
            }
        }
        private void UpdateRangeMin(ObjectGroup objectGroup)
        {
            if (objectGroup.MemberObject.RangeMin.Uom != null)
            {
                var obj = _mapper.Map<ObjectGroupRangeMin>(objectGroup.MemberObject.RangeMin);
                _db.ObjectGroupRangeMins.Update(obj);
            }
        }

        private void UpdateRangeMax(ObjectGroup objectGroup)
        {
            if (objectGroup.MemberObject.RangeMax.Uom != null)
            {
                var obj = _mapper.Map<ObjectGroupRangeMax>(objectGroup.MemberObject.RangeMax);
                _db.ObjectGroupRangeMaxs.Update(obj);
            }
        }
        private void UpdateReferenceDepth(ObjectGroup objectGroup)
        {
            if (objectGroup.MemberObject.ReferenceDepth.Uom != null)
            {
                var obj = _mapper.Map<ObjectGroupReferenceDepth>(objectGroup.MemberObject.ReferenceDepth);
                _db.ObjectGroupReferenceDepths.Update(obj);
            }
        }
        private void UpdateValue(ObjectGroup objectGroup)
        {
            if (objectGroup.MemberObject.ExtensionNameValue.Value.Uom != null)
            {
                var obj = _mapper.Map<ObjectGroupValue>(objectGroup.MemberObject.ExtensionNameValue.Value);
                _db.ObjectGroupValue.Update(obj);
            }
        }
        private void UpdateMd(ObjectGroup objectGroup)
        {
            if (objectGroup.MemberObject.ExtensionNameValue.Md.Uom != null)
            {
                var obj = _mapper.Map<ObjectGroupMd>(objectGroup.MemberObject.ExtensionNameValue.Md);
                _db.ObjectGroupMd.Update(obj);
            }
        }
        private void UpdateExtensionNameValue(ObjectGroup objectGroup)
        {
            if (objectGroup.MemberObject.ExtensionNameValue.Uid != null)
            {
                var obj = _mapper.Map<ObjectGroupExtensionNameValue>(objectGroup.MemberObject.ExtensionNameValue);
                _db.ObjectGroupExtensionNameValues.Update(obj);
            }
            if (objectGroup.CommonData.ExtensionNameValue.Uid != null)
            {
                var obj = _mapper.Map<ObjectGroupExtensionNameValue>(objectGroup.CommonData.ExtensionNameValue);
                _db.ObjectGroupExtensionNameValues.Update(obj);
            }
        }
        private void UpdateMemberObject(ObjectGroup objectGroup)
        {
            if (objectGroup.MemberObject.Uid != null)
            {
                var obj = _mapper.Map<ObjectGroupMemberObject>(objectGroup.MemberObject);
                _db.ObjectGroupMemberObjects.Update(obj);
            }
        }
        private void UpdateAcquisitionTimeZone(ObjectGroup objectGroup)
        {
            if (objectGroup.CommonData.AcquisitionTimeZone.DTim != null)
            {
                var obj = _mapper.Map<ObjectGroupAcquisitionTimeZone>(objectGroup.CommonData.AcquisitionTimeZone);
                _db.ObjectGroupAcquisitionTimeZones.Update(obj);
            }
        }
        private void UpdateDefaultDatum(ObjectGroup objectGroup)
        {
            if (objectGroup.CommonData.DefaultDatum.UidRef != null)
            {
                var obj = _mapper.Map<ObjectGroupDefaultDatum>(objectGroup.CommonData.DefaultDatum);
                _db.ObjectGroupDefaultDatum.Update(obj);
            }
        }

        private void UpdateCommonData(ObjectGroup objectGroup)
        {
            if (objectGroup.CommonData.SourceName != null)
            {
                var obj = _mapper.Map<ObjectGroupCommonData>(objectGroup.CommonData);
                _db.ObjectGroupCommonDatas.Update(obj);
            }
        }
        #endregion Update ObjectGroup
    }
}
