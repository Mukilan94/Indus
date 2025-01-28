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
    public class DrillReportsController : ControllerBase
    {
        private IDrillReportsRepository _drRepository;
        private readonly IMapper _mapper;
        private readonly WebAIAdvisorContext _db;
        public DrillReportsController(IDrillReportsRepository drRepo, IMapper mapper, WebAIAdvisorContext db)
        {
            _drRepository = drRepo;
            _mapper = mapper;
            _db = db;
        }
        WellAIAdvisiorRegistrationRepository wellAIRepository = new WellAIAdvisiorRegistrationRepository();

        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<DrillReportDto>))]
        [ProducesResponseType(400)]

        public IActionResult GetDrillReportss()
        {
            try
            {

                var objList = _drRepository.GetDrillReportDetails();
                var objDto = new List<DrillReportDto>();
                foreach (var obj in objList)
                {
                    objDto.Add(_mapper.Map<DrillReportDto>(obj));
                }
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "DrillingReports GetDrillReportss", null);
                return Ok(ex.ToString());
            }
        }

        [HttpGet("{drillReportuid}", Name = "GetDrillReports")]
        [ProducesResponseType(200, Type = typeof(DrillReportDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetDrillReports(string drillReportuid)
        {
            try
            {

                var obj = _drRepository.GetDrillReportDetail(drillReportuid);

                if (obj == null)
                {
                    return NotFound();
                }
                var objDto = _mapper.Map<DrillReportDto>(obj);
               
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "DrillingReports GetDrillReports", null);
                return Ok(ex.ToString());
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DrillReportDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateDrillReports([FromBody] DrillReportDto drillReportDtos)
        {
            try
            {
                if (drillReportDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_drRepository.DrillReportExists(drillReportDtos.Uid))
                {
                    ModelState.AddModelError("", "DrillReports Already Exist");
                    return StatusCode(400, ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var drillReportObj = _mapper.Map<DrillReport>(drillReportDtos);
                if (!_drRepository.CreateDrillReport(drillReportObj))
                {
                    ModelState.AddModelError("", $"Something went wrong when saving the record {drillReportObj.Name}");
                    return StatusCode(500, ModelState);

                }
                return CreatedAtRoute("GetDrillReports", new { drillReportuid = drillReportObj.Uid }, drillReportObj);

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "DrillingReports CreateDrillReports", null);
                return Ok(ex.ToString());
            }
        }


        [HttpPatch("{uid}", Name = "UpdateDrillReports")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateDrillReports(string uid, [FromBody] DrillReportDto drillReportDtos)
        {
            try
            {
                if (drillReportDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_drRepository.DrillReportExists(drillReportDtos.Uid))
                {
                    ModelState.AddModelError("", "DrillReports Already Exist");
                    return StatusCode(400, ModelState);
                }

                var cementJobDto = _mapper.Map<DrillReport>(drillReportDtos);
                if (!_drRepository.UpdateDrillReport(cementJobDto))
                {
                    ModelState.AddModelError("", $"Something went wrong when updating the record {drillReportDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "DrillingReports UpdateDrillReports", null);
                return Ok();
            }
        }

        [HttpDelete("{uid}", Name = "DeleteDrillReports")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteDrillReports(string uid)
        {
            try
            {
                if (!_drRepository.DrillReportExists(uid))
                {
                    return NotFound();
                }


                var drillReportDtos = _drRepository.GetDrillReportDetail(uid);
                if (!_drRepository.DeleteDrillReport(drillReportDtos))
                {
                    ModelState.AddModelError("", $"Something went wrong when Deleting the record {drillReportDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "DrillingReports DeleteDrillReports", null);
                return Ok();
            }
        }
    }
}
