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
    public class OpsReportController : ControllerBase
    {
        private IOpsReportRepository _orRepository;
        private readonly IMapper _mapper;
        private readonly WebAIAdvisorContext _db;
        public OpsReportController(IOpsReportRepository orRepo, IMapper mapper, WebAIAdvisorContext db)
        {
            _orRepository = orRepo;
            _mapper = mapper;
            _db = db;
        }
        WellAIAdvisiorRegistrationRepository wellAIRepository = new WellAIAdvisiorRegistrationRepository();

        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<OpsReportDto>))]
        [ProducesResponseType(400)]

        public IActionResult GetOpsReports()
        {
            try
            {

                var objList = _orRepository.GetOpsReportDetails();
                var objDto = new List<OpsReportDto>();
                foreach (var obj in objList)
                {
                    objDto.Add(_mapper.Map<OpsReportDto>(obj));
                }
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "OpsReport GetOpsReports", null);
                return Ok(ex.ToString());
            }
        }

        [HttpGet("{oruid}", Name = "GetOpsReport")]
        [ProducesResponseType(200, Type = typeof(OpsReportDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetOpsReport(string oruid)
        {
            try
            {

                var obj = _orRepository.GetOpsReportDetail(oruid);

                if (obj == null)
                {
                    return NotFound();
                }
                var objDto = _mapper.Map<OpsReportDto>(obj);
                
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "OpsReport GetOpsReport", null);
                return Ok(ex.ToString());
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OpsReportDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateOpsReport([FromBody] OpsReportDto lgDtos)
        {
            try
            {
                if (lgDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_orRepository.OpsReportExists(lgDtos.Uid))
                {
                    ModelState.AddModelError("", "OpsReport Already Exist");
                    return StatusCode(400, ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var lgObj = _mapper.Map<OpsReport>(lgDtos);
                if (!_orRepository.CreateOpsReport(lgObj))
                {
                    ModelState.AddModelError("", $"Something went wrong when saving the record {lgObj.Name}");
                    return StatusCode(500, ModelState);

                }
                return CreatedAtRoute("GetOpsReport", new { oruid = lgObj.Uid }, lgObj);

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "OpsReport CreateOpsReport", null);
                return Ok(ex.ToString());
            }
        }


        [HttpPatch("{uid}", Name = "UpdateOpsReport")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateOpsReport(string uid, [FromBody] OpsReportDto lgDtos)
        {
            try
            {
                if (lgDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_orRepository.OpsReportExists(lgDtos.Uid))
                {
                    ModelState.AddModelError("", "OpsReport Already Exist");
                    return StatusCode(400, ModelState);
                }

                var cementJobDto = _mapper.Map<OpsReport>(lgDtos);
                if (!_orRepository.UpdateOpsReport(cementJobDto))
                {
                    ModelState.AddModelError("", $"Something went wrong when updating the record {lgDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "OpsReport UpdateOpsReport", null);
                return Ok();
            }
        }

        [HttpDelete("{uid}", Name = "DeleteOpsReport")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteOpsReport(string uid)
        {
            try
            {
                if (!_orRepository.OpsReportExists(uid))
                {
                    return NotFound();
                }


                var lgDtos = _orRepository.GetOpsReportDetail(uid);
                if (!_orRepository.DeleteOpsReport(lgDtos))
                {
                    ModelState.AddModelError("", $"Something went wrong when Deleting the record {lgDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "OpsReport DeleteOpsReport", null);
                return Ok();
            }
        }
    }
}
