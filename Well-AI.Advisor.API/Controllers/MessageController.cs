using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Well_AI.Advisor.API.Dtos;
using Well_AI.Advisor.API.Models;
using Well_AI.Advisor.API.Repository;
using Well_AI.Advisor.API.Repository.IRepository;
using WellAI.Advisor.DLL.Data;

namespace Well_AI.Advisor.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private IMessageRepository _mgRepository;
        private readonly IMapper _mapper;
        private readonly WebAIAdvisorContext _db;
        public MessageController(IMessageRepository mgRepo, IMapper mapper, WebAIAdvisorContext db)
        {
            _mgRepository = mgRepo;
            _mapper = mapper;
            _db = db;
        }
        WellAIAdvisiorRegistrationRepository wellAIRepository = new WellAIAdvisiorRegistrationRepository();

        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<MessageDto>))]
        [ProducesResponseType(400)]

        public IActionResult GetMessages()
        {
            try
            {

                var objList = _mgRepository.GetMessageDetails();
                var objDto = new List<MessageDto>();
                foreach (var obj in objList)
                {
                    objDto.Add(_mapper.Map<MessageDto>(obj));
                }
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Message GetMessages", null);
                return Ok(ex.ToString());
            }
        }

        [HttpGet("{mguid}", Name = "GetMessage")]
        [ProducesResponseType(200, Type = typeof(MessageDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetMessage(string mguid)
        {
            try
            {

                var obj = _mgRepository.GetMessageDetail(mguid);

                if (obj == null)
                {
                    return NotFound();
                }
                var objDto = _mapper.Map<MessageDto>(obj);
                 
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Message GetMessage", null);
                return Ok(ex.ToString());
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MessageDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateMessage([FromBody] MessageDto lgDtos)
        {
            try
            {
                if (lgDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_mgRepository.MessageExists(lgDtos.Uid))
                {
                    ModelState.AddModelError("", "Message Already Exist");
                    return StatusCode(400, ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var lgObj = _mapper.Map<Message>(lgDtos);
                if (!_mgRepository.CreateMessage(lgObj))
                {
                    ModelState.AddModelError("", $"Something went wrong when saving the record {lgObj.Name}");
                    return StatusCode(500, ModelState);

                }
                return CreatedAtRoute("GetMessage", new { mguid = lgObj.Uid }, lgObj);

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Message CreateMessage", null);
                return Ok(ex.ToString());
            }
        }


        [HttpPatch("{uid}", Name = "UpdateMessage")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateMessage(string uid, [FromBody] MessageDto lgDtos)
        {
            try
            {
                if (lgDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_mgRepository.MessageExists(lgDtos.Uid))
                {
                    ModelState.AddModelError("", "Message Already Exist");
                    return StatusCode(400, ModelState);
                }

                var cementJobDto = _mapper.Map<Message>(lgDtos);
                if (!_mgRepository.UpdateMessage(cementJobDto))
                {
                    ModelState.AddModelError("", $"Something went wrong when updating the record {lgDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Message UpdateMessage", null);
                return Ok();
            }
        }

        [HttpDelete("{uid}", Name = "DeleteMessage")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteMessage(string uid)
        {
            try
            {
                if (!_mgRepository.MessageExists(uid))
                {
                    return NotFound();
                }


                var lgDtos = _mgRepository.GetMessageDetail(uid);
                if (!_mgRepository.DeleteMessage(lgDtos))
                {
                    ModelState.AddModelError("", $"Something went wrong when Deleting the record {lgDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Message DeleteMessage", null);
                return Ok();
            }
        }
    }
}
