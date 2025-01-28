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
    public class StimJobController : ControllerBase
    {
        private IStimJobRepository _sjRepository;
        private readonly IMapper _mapper;
        private readonly WebAIAdvisorContext _db;
        public StimJobController(IStimJobRepository sjRepo, IMapper mapper, WebAIAdvisorContext db)
        {
            _sjRepository = sjRepo;
            _mapper = mapper;
            _db = db;
        }
        WellAIAdvisiorRegistrationRepository wellAIRepository = new WellAIAdvisiorRegistrationRepository();

        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<StimJobDto>))]
        [ProducesResponseType(400)]

        public IActionResult GetStimJobs()
        {
            try
            {

                var objList = _sjRepository.GetStimJobDetails();
                var objDto = new List<StimJobDto>();
                foreach (var obj in objList)
                {
                    objDto.Add(_mapper.Map<StimJobDto>(obj));
                }
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "StimJob GetStimJobs", null);
                return Ok(ex.ToString());
            }
        }

        [HttpGet("{sjuid}", Name = "GetStimJob")]
        [ProducesResponseType(200, Type = typeof(StimJobDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetStimJob(string sjuid)
        {
            try
            {

                var obj = _sjRepository.GetStimJobDetail(sjuid);

                if (obj == null)
                {
                    return NotFound();
                }
                var objDto = _mapper.Map<StimJobDto>(obj);
                
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "StimJob GetStimJob", null);
                return Ok(ex.ToString());
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(StimJobDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateStimJob([FromBody] StimJobDto lgDtos)
        {
            try
            {
                if (lgDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_sjRepository.StimJobExists(lgDtos.Uid))
                {
                    ModelState.AddModelError("", "StimJob Already Exist");
                    return StatusCode(400, ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var lgObj = _mapper.Map<StimJob>(lgDtos);
                if (!_sjRepository.CreateStimJob(lgObj))
                {
                    ModelState.AddModelError("", $"Something went wrong when saving the record {lgObj.Name}");
                    return StatusCode(500, ModelState);

                }
                return CreatedAtRoute("GetStimJob", new { sjuid = lgObj.Uid }, lgObj);

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "StimJob CreateStimJob", null);
                return Ok(ex.ToString());
            }
        }


        [HttpPatch("{uid}", Name = "UpdateStimJob")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateStimJob(string uid, [FromBody] StimJobDto lgDtos)
        {
            try
            {
                if (lgDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_sjRepository.StimJobExists(lgDtos.Uid))
                {
                    ModelState.AddModelError("", "StimJob Already Exist");
                    return StatusCode(400, ModelState);
                }

                var cementJobDto = _mapper.Map<StimJob>(lgDtos);
                if (!_sjRepository.UpdateStimJob(cementJobDto))
                {
                    ModelState.AddModelError("", $"Something went wrong when updating the record {lgDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "StimJob UpdateStimJob", null);
                return Ok();
            }
        }

        [HttpDelete("{uid}", Name = "DeleteStimJob")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteStimJob(string uid)
        {
            try
            {
                if (!_sjRepository.StimJobExists(uid))
                {
                    return NotFound();
                }


                var lgDtos = _sjRepository.GetStimJobDetail(uid);
                if (!_sjRepository.DeleteStimJob(lgDtos))
                {
                    ModelState.AddModelError("", $"Something went wrong when Deleting the record {lgDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "StimJob DeleteStimJob", null);
                return Ok();
            }
        }
    }
}
