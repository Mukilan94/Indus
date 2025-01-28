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
    public class WellController : ControllerBase
    {
        private IWellRepository _wlRepository;
        private readonly IMapper _mapper;
        private readonly WebAIAdvisorContext _db;
        public WellController(IWellRepository wlRepo, IMapper mapper, WebAIAdvisorContext db)
        {
            _wlRepository = wlRepo;
            _mapper = mapper;
            _db = db;
        }
        WellAIAdvisiorRegistrationRepository wellAIRepository = new WellAIAdvisiorRegistrationRepository();

        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<WellDto>))]
        [ProducesResponseType(400)]

        public IActionResult GetWells()
        {
            try
            {

                var objList = _wlRepository.GetWellDetails();
                var objDto = new List<WellDto>();
                foreach (var obj in objList)
                {
                    objDto.Add(_mapper.Map<WellDto>(obj));
                }
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Well GetWells", null);
                return Ok(ex.ToString());
            }
        }

        [HttpGet("{wluid}", Name = "GetWell")]
        [ProducesResponseType(200, Type = typeof(WellDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetWell(string wluid)
        {
            try
            {

                var obj = _wlRepository.GetWellDetail(wluid);

                if (obj == null)
                {
                    return NotFound();
                }
                var objDto = _mapper.Map<WellDto>(obj);
                
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Well GetWell", null);
                return Ok(ex.ToString());
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(WellDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateWell([FromBody] WellDto lgDtos)
        {
            try
            {
                if (lgDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_wlRepository.WellExists(lgDtos.Uid))
                {
                    ModelState.AddModelError("", "Well Already Exist");
                    return StatusCode(400, ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var lgObj = _mapper.Map<Well>(lgDtos);
                if (!_wlRepository.CreateWell(lgObj))
                {
                    ModelState.AddModelError("", $"Something went wrong when saving the record {lgObj.Name}");
                    return StatusCode(500, ModelState);

                }
                return CreatedAtRoute("GetWell", new { wluid = lgObj.Uid }, lgObj);

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Well CreateWell", null);
                return Ok(ex.ToString());
            }
        }


        [HttpPatch("{uid}", Name = "UpdateWell")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateWell(string uid, [FromBody] WellDto lgDtos)
        {
            try
            {
                if (lgDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_wlRepository.WellExists(lgDtos.Uid))
                {
                    ModelState.AddModelError("", "Well Already Exist");
                    return StatusCode(400, ModelState);
                }

                var cementJobDto = _mapper.Map<Well>(lgDtos);
                if (!_wlRepository.UpdateWell(cementJobDto))
                {
                    ModelState.AddModelError("", $"Something went wrong when updating the record {lgDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Well UpdateWell", null);
                return Ok();
            }
        }

        [HttpDelete("{uid}", Name = "DeleteWell")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteWell(string uid)
        {
            try
            {
                if (!_wlRepository.WellExists(uid))
                {
                    return NotFound();
                }


                var lgDtos = _wlRepository.GetWellDetail(uid);
                if (!_wlRepository.DeleteWell(lgDtos))
                {
                    ModelState.AddModelError("", $"Something went wrong when Deleting the record {lgDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Well DeleteWell", null);
                return Ok();
            }
        }
    }
}
