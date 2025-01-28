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
    public class RiskController : ControllerBase
    {
        private IRiskRepository _rkRepository;
        private readonly IMapper _mapper;
        private readonly WebAIAdvisorContext _db;
        public RiskController(IRiskRepository rkRepo, IMapper mapper, WebAIAdvisorContext db)
        {
            _rkRepository = rkRepo;
            _mapper = mapper;
            _db = db;
        }
        WellAIAdvisiorRegistrationRepository wellAIRepository = new WellAIAdvisiorRegistrationRepository();

        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<RiskDto>))]
        [ProducesResponseType(400)]

        public IActionResult GetRisks()
        {
            try
            {

                var objList = _rkRepository.GetRiskDetails();
                var objDto = new List<RiskDto>();
                foreach (var obj in objList)
                {
                    objDto.Add(_mapper.Map<RiskDto>(obj));
                }
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Risk GetRisks", null);
                return Ok(ex.ToString());
            }
        }

        [HttpGet("{_rkuid}", Name = "GetRisk")]
        [ProducesResponseType(200, Type = typeof(RiskDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetRisk(string _rkuid)
        {
            try
            {

                var obj = _rkRepository.GetRiskDetail(_rkuid);

                if (obj == null)
                {
                    return NotFound();
                }
                var objDto = _mapper.Map<RiskDto>(obj);
                 
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Risk GetRisk", null);
                return Ok(ex.ToString());
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RiskDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateRisk([FromBody] RiskDto lgDtos)
        {
            try
            {
                if (lgDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_rkRepository.RiskExists(lgDtos.Uid))
                {
                    ModelState.AddModelError("", "Risk Already Exist");
                    return StatusCode(400, ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var lgObj = _mapper.Map<Risk>(lgDtos);
                if (!_rkRepository.CreateRisk(lgObj))
                {
                    ModelState.AddModelError("", $"Something went wrong when saving the record {lgObj.Name}");
                    return StatusCode(500, ModelState);

                }
                return CreatedAtRoute("GetRisk", new { _rkuid = lgObj.Uid }, lgObj);

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Risk CreateRisk", null);
                return Ok(ex.ToString());
            }
        }


        [HttpPatch("{uid}", Name = "UpdateRisk")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateRisk(string uid, [FromBody] RiskDto lgDtos)
        {
            try
            {
                if (lgDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_rkRepository.RiskExists(lgDtos.Uid))
                {
                    ModelState.AddModelError("", "Risk Already Exist");
                    return StatusCode(400, ModelState);
                }

                var cementJobDto = _mapper.Map<Risk>(lgDtos);
                if (!_rkRepository.UpdateRisk(cementJobDto))
                {
                    ModelState.AddModelError("", $"Something went wrong when updating the record {lgDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Risk UpdateRisk", null);
                return Ok();
            }
        }

        [HttpDelete("{uid}", Name = "DeleteRisk")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteRisk(string uid)
        {
            try
            {
                if (!_rkRepository.RiskExists(uid))
                {
                    return NotFound();
                }


                var lgDtos = _rkRepository.GetRiskDetail(uid);
                if (!_rkRepository.DeleteRisk(lgDtos))
                {
                    ModelState.AddModelError("", $"Something went wrong when Deleting the record {lgDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Risk DeleteRisk", null);
                return Ok();
            }
        }
    }
}
