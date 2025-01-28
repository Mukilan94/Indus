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

namespace Well_AI.Advisor.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private ILogRepository _lgRepository;
        private readonly IMapper _mapper;

        public LogController(ILogRepository lgRepo, IMapper mapper)
        {
            _lgRepository = lgRepo;
            _mapper = mapper;
        }
        WellAIAdvisiorRegistrationRepository wellAIRepository = new WellAIAdvisiorRegistrationRepository();

       
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<LogDto>))]
        [ProducesResponseType(400)]

        public IActionResult GetLogs()
        {
            try
            {

                var objList = _lgRepository.GetLogDetails();
                var objDto = new List<LogDto>();
                foreach (var obj in objList)
                {
                    objDto.Add(_mapper.Map<LogDto>(obj));
                }
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                return Ok(ex.ToString());
            }
        }

        [HttpGet("{lguid}", Name = "GetLog")]
        [ProducesResponseType(200, Type = typeof(LogDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetLog(string lguid)
        {
            try
            {

                var obj = _lgRepository.GetLogDetail(lguid);

                if (obj == null)
                {
                    return NotFound();
                }
                var objDto = _mapper.Map<LogDto>(obj);
                
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                return Ok(ex.ToString());
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(LogDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateLog([FromBody] LogDto lgDtos)
        {
            try
            {
                if (lgDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_lgRepository.LogExists(lgDtos.Uid))
                {
                    ModelState.AddModelError("", "Log Already Exist");
                    return StatusCode(400, ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var lgObj = _mapper.Map<Log>(lgDtos);
                if (!_lgRepository.CreateLog(lgObj))
                {
                    ModelState.AddModelError("", $"Something went wrong when saving the record {lgObj.Name}");
                    return StatusCode(500, ModelState);

                }
                return CreatedAtRoute("GetLog", new { lguid = lgObj.Uid }, lgObj);

            }
            catch (Exception ex)
            {
                return Ok(ex.ToString());
            }
        }


        [HttpPatch("{uid}", Name = "UpdateLog")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateLog(string uid, [FromBody] LogDto lgDtos)
        {
            try
            {
                if (lgDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_lgRepository.LogExists(lgDtos.Uid))
                {
                    ModelState.AddModelError("", "Log Already Exist");
                    return StatusCode(400, ModelState);
                }

                var cementJobDto = _mapper.Map<Log>(lgDtos);
                if (!_lgRepository.UpdateLog(cementJobDto))
                {
                    ModelState.AddModelError("", $"Something went wrong when updating the record {lgDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return Ok(ex.ToString());
            }
        }

        [HttpDelete("{uid}", Name = "DeleteLog")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteLog(string uid)
        {
            try
            {
                if (!_lgRepository.LogExists(uid))
                {
                    return NotFound();
                }


                var lgDtos = _lgRepository.GetLogDetail(uid);
                if (!_lgRepository.DeleteLog(lgDtos))
                {
                    ModelState.AddModelError("", $"Something went wrong when Deleting the record {lgDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return Ok(ex.ToString());
            }
        }
    }
}
