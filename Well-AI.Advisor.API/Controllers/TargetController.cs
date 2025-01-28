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
    public class TargetController : ControllerBase
    {
        private ITargetRepository _tgRepository;
        private readonly IMapper _mapper;
        private readonly WebAIAdvisorContext _db;
        public TargetController(ITargetRepository tgRepo, IMapper mapper, WebAIAdvisorContext db)
        {
            _tgRepository = tgRepo;
            _mapper = mapper;
            _db = db;
        }
        WellAIAdvisiorRegistrationRepository wellAIRepository = new WellAIAdvisiorRegistrationRepository();

       
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<TargetDto>))]
        [ProducesResponseType(400)]

        public IActionResult GetTargets()
        {
            try
            {

                var objList = _tgRepository.GetTargetDetails();
                var objDto = new List<TargetDto>();
                foreach (var obj in objList)
                {
                    objDto.Add(_mapper.Map<TargetDto>(obj));
                }
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Target GetTargets", null);
                return Ok(ex.ToString());
            }
        }

        [HttpGet("{tguid}", Name = "GetTarget")]
        [ProducesResponseType(200, Type = typeof(TargetDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetTarget(string tguid)
        {
            try
            {

                var obj = _tgRepository.GetTargetDetail(tguid);

                if (obj == null)
                {
                    return NotFound();
                }
                var objDto = _mapper.Map<TargetDto>(obj);
                
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Target GetTarget", null);
                return Ok(ex.ToString());
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TargetDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateTarget([FromBody] TargetDto lgDtos)
        {
            try
            {
                if (lgDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_tgRepository.TargetExists(lgDtos.Uid))
                {
                    ModelState.AddModelError("", "Target Already Exist");
                    return StatusCode(400, ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var lgObj = _mapper.Map<Target>(lgDtos);
                if (!_tgRepository.CreateTarget(lgObj))
                {
                    ModelState.AddModelError("", $"Something went wrong when saving the record {lgObj.Name}");
                    return StatusCode(500, ModelState);

                }
                return CreatedAtRoute("GetTarget", new { tguid = lgObj.Uid }, lgObj);

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Target CreateTarget", null);
                return Ok(ex.ToString());
            }
        }


        [HttpPatch("{uid}", Name = "UpdateTarget")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateTarget(string uid, [FromBody] TargetDto lgDtos)
        {
            try
            {
                if (lgDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_tgRepository.TargetExists(lgDtos.Uid))
                {
                    ModelState.AddModelError("", "Target Already Exist");
                    return StatusCode(400, ModelState);
                }

                var cementJobDto = _mapper.Map<Target>(lgDtos);
                if (!_tgRepository.UpdateTarget(cementJobDto))
                {
                    ModelState.AddModelError("", $"Something went wrong when updating the record {lgDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Target UpdateTarget", null);
                return Ok();
            }
        }

        [HttpDelete("{uid}", Name = "DeleteTarget")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteTarget(string uid)
        {
            try
            {
                if (!_tgRepository.TargetExists(uid))
                {
                    return NotFound();
                }


                var lgDtos = _tgRepository.GetTargetDetail(uid);
                if (!_tgRepository.DeleteTarget(lgDtos))
                {
                    ModelState.AddModelError("", $"Something went wrong when Deleting the record {lgDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Target DeleteTarget", null);
                return Ok();
            }
        }
    }
}
