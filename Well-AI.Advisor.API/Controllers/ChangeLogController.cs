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
    public class ChangeLogController : ControllerBase
    {
        private IChangeLogRepository _clRepository;
        private readonly IMapper _mapper;
        private readonly WebAIAdvisorContext _db;
        public ChangeLogController(IChangeLogRepository cjRepo, IMapper mapper, WebAIAdvisorContext db)
        {
            _clRepository = cjRepo;
            _mapper = mapper;
            _db = db;
        }
        WellAIAdvisiorRegistrationRepository wellAIRepository = new WellAIAdvisiorRegistrationRepository();

        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ChangeLogDto>))]
        [ProducesResponseType(400)]

        public IActionResult GetChangeLogs()
        {
            try
            {

                var objList = _clRepository.GetChangeLogDetails();
                var objDto = new List<ChangeLogDto>();
                foreach (var obj in objList)
                {
                    objDto.Add(_mapper.Map<ChangeLogDto>(obj));
                }
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "ChangeLog GetChangeLogs", null);
                return Ok(ex.ToString());
            }
        }

        [HttpGet("{changeLoguid}", Name = "GetChangeLog")]
        [ProducesResponseType(200, Type = typeof(ChangeLogDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetChangeLog(string changeLoguid)
        {
            try
            {

                var obj = _clRepository.GetChangeLogDetail(changeLoguid);

                if (obj == null)
                {
                    return NotFound();
                }
                var objDto = _mapper.Map<ChangeLogDto>(obj);
                
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "ChangeLog GetChangeLog", null);
                return Ok(ex.ToString());
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ChangeLogDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateChangeLog([FromBody] ChangeLogDto changeLogDtos)
        {
            try
            {
                if (changeLogDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_clRepository.ChangeLogExists(changeLogDtos.Uid))
                {
                    ModelState.AddModelError("", "ChangeLog Already Exist");
                    return StatusCode(400, ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var changeLogObj = _mapper.Map<ChangeLog>(changeLogDtos);
                if (!_clRepository.CreateChangeLog(changeLogObj))
                {
                    ModelState.AddModelError("", $"Something went wrong when saving the record {changeLogObj.NameObject}");
                    return StatusCode(500, ModelState);

                }
                return CreatedAtRoute("GetChangeLog", new { changeLoguid = changeLogObj.Uid }, changeLogObj);

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "ChangeLog CreateChangeLog", null);
                return Ok(ex.ToString());
            }
        }


        [HttpPatch("{uid}", Name = "UpdateChangeLog")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateChangeLog(string uid, [FromBody] ChangeLogDto changeLogDtos)
        {
            try
            {
                if (changeLogDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_clRepository.ChangeLogExists(changeLogDtos.Uid))
                {
                    ModelState.AddModelError("", "ChangeLog Already Exist");
                    return StatusCode(400, ModelState);
                }

                var cementJobDto = _mapper.Map<ChangeLog>(changeLogDtos);
                if (!_clRepository.UpdateChangeLog(cementJobDto))
                {
                    ModelState.AddModelError("", $"Something went wrong when updating the record {changeLogDtos.NameObject}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "ChangeLog UpdateChangeLog", null);
                return Ok();
            }
        }

        [HttpDelete("{uid}", Name = "DeleteChangeLog")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteChangeLog(string uid)
        {
            try
            {
                if (!_clRepository.ChangeLogExists(uid))
                {
                    return NotFound();
                }


                var changeLogDtos = _clRepository.GetChangeLogDetail(uid);
                if (!_clRepository.DeleteChangeLog(changeLogDtos))
                {
                    ModelState.AddModelError("", $"Something went wrong when Deleting the record {changeLogDtos.NameObject}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "ChangeLog DeleteChangeLog", null);
                return Ok();
            }
        }
    }
}
