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
    public class MessageRepository : IMessageRepository
    {
        private readonly WellAIAdvisiorContext _db;
        private readonly WellAI.Advisor.DLL.Data.WebAIAdvisorContext _wdb;
        private readonly IMapper _mapper;
        private ITenantRepository _tenantRepository;

        public MessageRepository(WellAIAdvisiorContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper, ITenantRepository tenantRepository, WellAI.Advisor.DLL.Data.WebAIAdvisorContext wdb)
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
        public bool MessageExists(string uid)
        {
            bool value = _db.Messages.Any(x => x.Uid.ToLower().Trim() == uid.ToLower().Trim());
            return value;
        }

        public bool CreateMessage(Message message)
        {
            try
            {
                Md(message);
                MdBit(message);
                Param(message);
                CommonData(message);
                _db.Messages.Add(message);
                return Save();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "MessageRepository CreateMessage", null);
                return Save();
            }
        }

        public bool DeleteMessage(Message message)
        {
            _db.Messages.Remove(message);
            return Save();
        }

        public Message GetMessageDetail(string Uid)
        {
            return _db.Messages.FirstOrDefault(x=>x.Uid==Uid);
            
        }

        public ICollection<Message> GetMessageDetails()
        {
            return _db.Messages.OrderBy(x => x.NameWell).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateMessage(Message message)
        {
            try
            {
                UpdateMd(message);
                UpdateMdBit(message);
                UpdateParam(message);
                UpdateCommonData(message);
                _db.Messages.Update(message);
                return Save();
            }
            catch(Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_wdb);
                customErrorHandler.WriteError(ex, "MessageRepository UpdateMessage", null);
                return Save();
            }
        }

        #region Insert Message
        private void Md(Message message)
        {
            if (message.Md.Uom != null)
            {
                var obj = _mapper.Map<MessageMd>(message.Md);
                _db.MessageMd.Add(obj);
            }
        }
        private void MdBit(Message message)
        {
            if (message.MdBit.Uom != null)
            {
                var obj = _mapper.Map<MessageMdBit>(message.MdBit);
                _db.MessageMdBit.Add(obj);
            }
        }
        private void Param(Message message)
        {
            if (message.Param.Index != null)
            {
                var obj = _mapper.Map<MessageParam>(message.Param);
                _db.MessageParam.Add(obj);
            }
        }
        private void CommonData(Message message)
        {
            if (message.CommonData != null)
            {
                var obj = _mapper.Map<MessageCommonData>(message.CommonData);
                _db.MessageCommonDatas.Add(obj);
            }
        }
        #endregion Insert Message

        #region Update Message
        private void UpdateMd(Message message)
        {
            if (message.Md.Uom != null)
            {
                var obj = _mapper.Map<MessageMd>(message.Md);
                _db.MessageMd.Update(obj);
            }
        }
        private void UpdateMdBit(Message message)
        {
            if (message.MdBit.Uom != null)
            {
                var obj = _mapper.Map<MessageMdBit>(message.MdBit);
                _db.MessageMdBit.Update(obj);
            }
        }
        private void UpdateParam(Message message)
        {
            if (message.Param.Index != null)
            {
                var obj = _mapper.Map<MessageParam>(message.Param);
                _db.MessageParam.Update(obj);
            }
        }
        private void UpdateCommonData(Message message)
        {
            if (message.CommonData != null)
            {
                var obj = _mapper.Map<MessageCommonData>(message.CommonData);
                _db.MessageCommonDatas.Update(obj);
            }
        }
        #endregion Update Message
    }
}
