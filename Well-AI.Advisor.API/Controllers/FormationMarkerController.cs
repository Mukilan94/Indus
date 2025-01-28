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
    public class FormationMarkerController : ControllerBase
    {
        private IFormationMarkerRepository _frRepository;
        private readonly IMapper _mapper;
        private readonly WebAIAdvisorContext _db;
        public FormationMarkerController(IFormationMarkerRepository frRepo, IMapper mapper, WebAIAdvisorContext db)
        {
            _frRepository = frRepo;
            _mapper = mapper;
            _db = db;
        }
        WellAIAdvisiorRegistrationRepository wellAIRepository = new WellAIAdvisiorRegistrationRepository();

        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<FormationMarkerDto>))]
        [ProducesResponseType(400)]

        public IActionResult GetFormationMarkers()
        {
            try
            {

                var objList = _frRepository.GetFormationMarkerDetails();
                var objDto = new List<FormationMarkerDto>();
                foreach (var obj in objList)
                {
                    objDto.Add(_mapper.Map<FormationMarkerDto>(obj));
                }
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "FormationMarker GetFormationMarkers", null);
                return Ok(ex.ToString());
            }
        }

        [HttpGet("{forrmMarkeruid}", Name = "GetFormationMarker")]
        [ProducesResponseType(200, Type = typeof(FormationMarkerDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetFormationMarker(string forrmMarkeruid)
        {
            try
            {

                var obj = _frRepository.GetFormationMarkerDetail(forrmMarkeruid);

                if (obj == null)
                {
                    return NotFound();
                }
                var objDto = _mapper.Map<FormationMarkerDto>(obj);
               
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "FormationMarker GetFormationMarker", null);
                return Ok(ex.ToString());
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(FormationMarkerDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateFormationMarker([FromBody] FormationMarkerDto forrmMarkerDtos)
        {
            try
            {
                if (forrmMarkerDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_frRepository.FormationMarkerExists(forrmMarkerDtos.Uid))
                {
                    ModelState.AddModelError("", "FormationMarker Already Exist");
                    return StatusCode(400, ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var forrmMarkerObj = _mapper.Map<FormationMarker>(forrmMarkerDtos);
                if (!_frRepository.CreateFormationMarker(forrmMarkerObj))
                {
                    ModelState.AddModelError("", $"Something went wrong when saving the record {forrmMarkerObj.Name}");
                    return StatusCode(500, ModelState);

                }
                return CreatedAtRoute("GetFormationMarker", new { forrmMarkeruid = forrmMarkerObj.Uid }, forrmMarkerObj);

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "FormationMarker CreateFormationMarker", null);
                return Ok(ex.ToString());
            }
        }


        [HttpPatch("{uid}", Name = "UpdateFormationMarker")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateFormationMarker(string uid, [FromBody] FormationMarkerDto forrmMarkerDtos)
        {
            try
            {
                if (forrmMarkerDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_frRepository.FormationMarkerExists(forrmMarkerDtos.Uid))
                {
                    ModelState.AddModelError("", "FormationMarker Already Exist");
                    return StatusCode(400, ModelState);
                }

                var cementJobDto = _mapper.Map<FormationMarker>(forrmMarkerDtos);
                if (!_frRepository.UpdateFormationMarker(cementJobDto))
                {
                    ModelState.AddModelError("", $"Something went wrong when updating the record {forrmMarkerDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "FormationMarker UpdateFormationMarker", null);
                return Ok();
            }
        }

        [HttpDelete("{uid}", Name = "DeleteFormationMarker")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteFormationMarker(string uid)
        {
            try
            {
                if (!_frRepository.FormationMarkerExists(uid))
                {
                    return NotFound();
                }


                var forrmMarkerDtos = _frRepository.GetFormationMarkerDetail(uid);
                if (!_frRepository.DeleteFormationMarker(forrmMarkerDtos))
                {
                    ModelState.AddModelError("", $"Something went wrong when Deleting the record {forrmMarkerDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "FormationMarker DeleteFormationMarker", null);
                return Ok();
            }
        }
    }
}
