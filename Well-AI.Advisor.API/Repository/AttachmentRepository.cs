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
using WellAI.Advisor.API.Models;
using WellAI.Advisor.API.Repository.IRepository;
using WellAI.Advisor.DLL.Data;

namespace Well_AI.Advisor.API.Repository
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly WellAIAdvisiorContext _db;
        private readonly WebAIAdvisorContext _wdb;
        private readonly IMapper _mapper;
        private ITenantRepository _tenantRepository;
        public AttachmentRepository(WellAIAdvisiorContext db, IHttpContextAccessor httpContextAccessor ,IMapper mapper, ITenantRepository tenantRepository, WebAIAdvisorContext wdb)
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

        public bool AttachmentExists(string Uid)
        {
            bool value = _db.Attachments.Any(x => x.uid.ToLower().Trim() == Uid.ToLower().Trim());
            return value;
        }

        public bool DeleteAttachment(Attachment attachment)
        {
            _db.Attachments.Remove(attachment);
            return Save();
        }

        public Attachment GetAttachmentDetail(string Uid)
        {
            Attachment objAttachment = new Attachment();
            objAttachment = _db.Attachments.FirstOrDefault(x => x.uid == Uid);
            
            return objAttachment;
        }

        public ICollection<Attachment> GetAttachmentDetails()
        {
            return _db.Attachments.OrderBy(x => x.uid).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateAttachment(Attachment attachment)
        {
            try
            {
                UpdateAttachmentObjectReference(attachment);
                UpdateAttchmentCommonData(attachment);
                _db.Attachments.Update(attachment);
                return Save();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "AttachmentRepository UpdateAttachment", null);
                return Save();
            }
        }

        public bool UploadAttachment(Attachment attachment)
        {
            try
            {
                AttachmentObjectReference(attachment);
                AttchmentCommonData(attachment);
                _db.Attachments.Add(attachment);
                return Save();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "AttachmentRepository UploadAttachment", null);
                return Save();
            }
        }

        #region Upload
        private void AttachmentObjectReference(Attachment attachment)
        {
            if (attachment.objectReference.uidRef != null)
            {
                var obj = _mapper.Map<AttachmentObjectReference>(attachment.objectReference);
                _db.AttachmentObjectReferences.Add(obj);
            }
        }

        private void AttchmentCommonData(Attachment attachment)
        {
            if (attachment.commonData != null)
            {
                var obj = _mapper.Map<AttchmentCommonData>(attachment.commonData);
                _db.AttchmentCommonDatas.Add(obj);
            }
        }

        #endregion Upload


        #region Update Upload
        private void UpdateAttachmentObjectReference(Attachment attachment)
        {
            if (attachment.objectReference.uidRef != null)
            {
                var obj = _mapper.Map<AttachmentObjectReference>(attachment.objectReference);
                _db.AttachmentObjectReferences.Update(obj);
            }
        }

        private void UpdateAttchmentCommonData(Attachment attachment)
        {
            if (attachment.commonData != null)
            {
                var obj = _mapper.Map<AttchmentCommonData>(attachment.commonData);
                _db.AttchmentCommonDatas.Update(obj);
            }
        }

        #endregion Update Upload
    }
}
