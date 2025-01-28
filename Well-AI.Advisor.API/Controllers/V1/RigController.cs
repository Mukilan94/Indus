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
    //[Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RigController : ControllerBase
    {
        private IRigRepository _rgRepository;
        private readonly IMapper _mapper;
        private readonly WebAIAdvisorContext _db;
        public RigController(IRigRepository rgRepo, IMapper mapper, WebAIAdvisorContext db)
        {
            _rgRepository = rgRepo;
            _mapper = mapper;
            _db = db;
        }
        WellAIAdvisiorRegistrationRepository wellAIRepository = new WellAIAdvisiorRegistrationRepository();

        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<RigDto>))]
        [ProducesResponseType(400)]

        public IActionResult GetRigs()
        {
            try
            {

                var objList = _rgRepository.GetRigDetails();
                var objDto = new List<RigDto>();
                foreach (var obj in objList)
                {
                    objDto.Add(_mapper.Map<RigDto>(obj));
                }
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Rig GetRigs", null);
                return Ok(ex.ToString());
            }
        }

        [HttpGet("{rguid}", Name = "GetRig")]
        [ProducesResponseType(200, Type = typeof(RigDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetRig(string rguid)
        {
            try
            {

                var obj = _rgRepository.GetRigDetail(rguid);

                if (obj == null)
                {
                    return NotFound();
                }
                var objDto = _mapper.Map<RigDto>(obj);
                
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Rig GetRig", null);
                return Ok(ex.ToString());
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RigDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateRig([FromBody] RigDto lgDtos)
        {
            try
            {
                if (lgDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_rgRepository.RigExists(lgDtos.Uid))
                {
                    ModelState.AddModelError("", "Rig Already Exist");
                    return StatusCode(400, ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var lgObj = _mapper.Map<Rig>(lgDtos);
                if (!_rgRepository.CreateRig(lgObj))
                {
                    ModelState.AddModelError("", $"Something went wrong when saving the record {lgObj.Name}");
                    return StatusCode(500, ModelState);

                }
                return CreatedAtRoute("GetRig", new { rguid = lgObj.Uid }, lgObj);

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Rig CreateRig", null);
                return Ok(ex.ToString());
            }
        }


        [HttpPatch("{uid}", Name = "UpdateRig")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateRig(string uid, [FromBody] RigDto lgDtos)
        {
            try
            {
                if (lgDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_rgRepository.RigExists(lgDtos.Uid))
                {
                    ModelState.AddModelError("", "Rig Already Exist");
                    return StatusCode(400, ModelState);
                }

                var cementJobDto = _mapper.Map<Rig>(lgDtos);
                if (!_rgRepository.UpdateRig(cementJobDto))
                {
                    ModelState.AddModelError("", $"Something went wrong when updating the record {lgDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Rig UpdateRig", null);
                return Ok();
            }
        }

        [HttpDelete("{uid}", Name = "DeleteRig")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteRig(string uid)
        {
            try
            {
                if (!_rgRepository.RigExists(uid))
                {
                    return NotFound();
                }


                var lgDtos = _rgRepository.GetRigDetail(uid);
                if (!_rgRepository.DeleteRig(lgDtos))
                {
                    ModelState.AddModelError("", $"Something went wrong when Deleting the record {lgDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Rig DeleteRig", null);
                return Ok();
            }
        }
    }
}
