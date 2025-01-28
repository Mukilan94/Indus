using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Well_AI.Advisor.API.Models;
using Well_AI.Advisor.API.Repository;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Well_AI.Advisor.API.Repository.IRepository;
using Well_AI.Advisor.API.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WellAI.Advisor.API.Models;
using Well_AI.Advisor.API.Data;
using Microsoft.EntityFrameworkCore;
using WellAI.Advisor.API.Repository;
using WellAI.Advisor.API.Repository.IRepository;
using WellAI.Advisor.API.Models.Dtos;
using WellAI.Advisor.DLL.Data;

namespace Well_AI.Advisor.API.Controllers.V1
{
    [Authorize]
    ////[Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    ////[Route("api/[controller]")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class AttachmentController : ControllerBase
    {
        private IAttachmentRepository _amRepository;
        private readonly WebAIAdvisorContext _db;
        private readonly IMapper _mapper;
        public AttachmentController(IConfiguration configuration,  IMapper mapper, IAttachmentRepository amRepo, WebAIAdvisorContext db)
        {
            _amRepository = amRepo;
            _mapper = mapper;
            _db = db;
        }

        /// <summary>
        /// Get List of Attachment
        /// </summary>
        /// <returns></returns>
        [MapToApiVersion("1.0")]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<AttachmentDto>))]
        [ProducesResponseType(400)]
        
        public IActionResult GetAttchment()
        {
            try
            {
                
                var objList = _amRepository.GetAttachmentDetails();
                var objDto = new List<AttachmentDto>();
                foreach (var obj in objList)
                {
                    objDto.Add(_mapper.Map<AttachmentDto>(obj));
                }
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Attachment GetAttchment", null);
                return Ok(ex.ToString());
            }
        }
        [Authorize]
        [MapToApiVersion("1.0")]
        [HttpGet,Route("{attachmentuid}", Name = "GetAttachment")]
        [ProducesResponseType(200, Type = typeof(AttachmentDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetAttachmentDetail(string attachmentuid)
        {
            try
            {

                var obj = _amRepository.GetAttachmentDetail(attachmentuid);

                if (obj == null)
                {
                    return NotFound();
                }
                var objDto = _mapper.Map<AttachmentDto>(obj);
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Attachment GetAttachmentDetail", null);
                return Ok(ex.ToString());
            }
        }
        [Authorize]
        [MapToApiVersion("1.0")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AttachmentDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateAttachment([FromBody] AttachmentDto attachmentDto)
        {
            try
            {
                if (attachmentDto == null)
                {
                    return BadRequest(ModelState);
                }
                if (_amRepository.AttachmentExists(attachmentDto.uid))
                {
                    ModelState.AddModelError("", "Attachment Already Exist");
                    return StatusCode(400, ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var attachmentObj = _mapper.Map<Attachment>(attachmentDto);
               
                if (!_amRepository.UploadAttachment(attachmentObj))
                {
                    ModelState.AddModelError("", $"Something went wrong when saving the record {attachmentObj.uid}");
                    return StatusCode(500, ModelState);

                }
                return CreatedAtRoute("GetAttachment", new { attachmentuid = attachmentObj.uid }, attachmentObj);

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Attachment CreateAttachment", null);
                return Ok(ex.ToString());
            }
        }

        [Authorize]
        [MapToApiVersion("1.0")]
        [HttpPatch, Route("{uid}", Name = "UpdateAttachment")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateAttachment(string uid, [FromBody] AttachmentDto attachmentDto)
        {
            try
            {
                if (attachmentDto == null)
                {
                    return BadRequest(ModelState);
                }
                if (_amRepository.AttachmentExists(attachmentDto.uid))
                {
                    ModelState.AddModelError("", "Attachment Already Exist");
                    return StatusCode(400, ModelState);
                }

                var attachmentObj = _mapper.Map<Attachment>(attachmentDto);
                if (!_amRepository.UpdateAttachment(attachmentObj))
                {
                    ModelState.AddModelError("", $"Something went wrong when updating the record {attachmentObj.uid}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Attachment UpdateAttachment", null);
                return Ok();
            }
        }
        [Authorize]
        [MapToApiVersion("1.0")]
        [HttpDelete, Route("{uid}", Name = "DeleteAttachment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteAttachment(string uid)
        {
            try
            {
                if (!_amRepository.AttachmentExists(uid))
                {
                    return NotFound();
                }


                var attachmentObj = _amRepository.GetAttachmentDetail(uid);
                if (!_amRepository.DeleteAttachment(attachmentObj))
                {
                    ModelState.AddModelError("", $"Something went wrong when Deleting the record {attachmentObj.uid}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Attachment DeleteAttachment", null);
                return Ok();
            }
        }
    }
}