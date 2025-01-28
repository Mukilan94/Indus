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
    public class MudLogController : ControllerBase
    {
        private IMudLogRepository _mdRepository;
        private readonly IMapper _mapper;
        private readonly WebAIAdvisorContext _db;
        public MudLogController(IMudLogRepository mdRepo, IMapper mapper, WebAIAdvisorContext db)
        {
            _mdRepository = mdRepo;
            _mapper = mapper;
            _db = db;
        }
        WellAIAdvisiorRegistrationRepository wellAIRepository = new WellAIAdvisiorRegistrationRepository();

        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<MudLogDto>))]
        [ProducesResponseType(400)]

        public IActionResult GetMudLogs()
        {
            try
            {

                var objList = _mdRepository.GetMudLogDetails();
                var objDto = new List<MudLogDto>();
                foreach (var obj in objList)
                {
                    objDto.Add(_mapper.Map<MudLogDto>(obj));
                }
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "MudLog GetMudLogs", null);
                return Ok(ex.ToString());
            }
        }

        [HttpGet("{mduid}", Name = "GetMudLog")]
        [ProducesResponseType(200, Type = typeof(MudLogDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetMudLog(string mduid)
        {
            try
            {

                var obj = _mdRepository.GetMudLogDetail(mduid);

                if (obj == null)
                {
                    return NotFound();
                }
                var objDto = _mapper.Map<MudLogDto>(obj);
                
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "MudLog GetMudLog", null);
                return Ok(ex.ToString());
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MudLogDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateMudLog([FromBody] MudLogDto lgDtos)
        {
            try
            {
                if (lgDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_mdRepository.MudLogExists(lgDtos.Uid))
                {
                    ModelState.AddModelError("", "MudLog Already Exist");
                    return StatusCode(400, ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var lgObj = _mapper.Map<MudLog>(lgDtos);
                if (!_mdRepository.CreateMudLog(lgObj))
                {
                    ModelState.AddModelError("", $"Something went wrong when saving the record {lgObj.Name}");
                    return StatusCode(500, ModelState);

                }
                return CreatedAtRoute("GetMudLog", new { mduid = lgObj.Uid }, lgObj);

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "MudLog CreateMudLog", null);
                return Ok(ex.ToString());
            }
        }


        [HttpPatch("{uid}", Name = "UpdateMudLog")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateMudLog(string uid, [FromBody] MudLogDto lgDtos)
        {
            try
            {
                if (lgDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_mdRepository.MudLogExists(lgDtos.Uid))
                {
                    ModelState.AddModelError("", "MudLog Already Exist");
                    return StatusCode(400, ModelState);
                }

                var cementJobDto = _mapper.Map<MudLog>(lgDtos);
                if (!_mdRepository.UpdateMudLog(cementJobDto))
                {
                    ModelState.AddModelError("", $"Something went wrong when updating the record {lgDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "MudLog UpdateMudLog", null);
                return Ok();
            }
        }

        [HttpDelete("{uid}", Name = "DeleteMudLog")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteMudLog(string uid)
        {
            try
            {
                if (!_mdRepository.MudLogExists(uid))
                {
                    return NotFound();
                }


                var lgDtos = _mdRepository.GetMudLogDetail(uid);
                if (!_mdRepository.DeleteMudLog(lgDtos))
                {
                    ModelState.AddModelError("", $"Something went wrong when Deleting the record {lgDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "MudLog DeleteMudLog", null);
                return Ok();
            }
        }
    }
}
