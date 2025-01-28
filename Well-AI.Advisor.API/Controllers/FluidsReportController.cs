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
    public class FluidsReportController : ControllerBase
    {
        private IFluidsReportCoreRepository _frRepository;
        private readonly IMapper _mapper;
        private readonly WebAIAdvisorContext _db;
        public FluidsReportController(IFluidsReportCoreRepository frRepo, IMapper mapper, WebAIAdvisorContext db)
        {
            _frRepository = frRepo;
            _mapper = mapper;
            _db = db;
        }
        WellAIAdvisiorRegistrationRepository wellAIRepository = new WellAIAdvisiorRegistrationRepository();

         
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<FluidsReportDto>))]
        [ProducesResponseType(400)]

        public IActionResult GetFluidReportCore()
        {
            try
            {

                var objList = _frRepository.GetFluidsReportDetails();
                var objDto = new List<FluidsReportDto>();
                foreach (var obj in objList)
                {
                    objDto.Add(_mapper.Map<FluidsReportDto>(obj));
                }
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "FluidsReport GetFluidReportCore", null);
                return Ok(ex.ToString());
            }
        }

        [HttpGet("{fluidsReportuid}", Name = "GetFluidReportCore")]
        [ProducesResponseType(200, Type = typeof(FluidsReportDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetFluidReportCore(string fluidsReportuid)
        {
            try
            {

                var obj = _frRepository.GetFluidsReportDetail(fluidsReportuid);

                if (obj == null)
                {
                    return NotFound();
                }
                var objDto = _mapper.Map<FluidsReportDto>(obj);
                
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "FluidsReport GetFluidReportCore", null);
                return Ok(ex.ToString());
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(FluidsReportDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateFluidReportCore([FromBody] FluidsReportDto fluidsReportDtos)
        {
            try
            {
                if (fluidsReportDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_frRepository.FluidsReportExists(fluidsReportDtos.Uid))
                {
                    ModelState.AddModelError("", "FluidReportCore Already Exist");
                    return StatusCode(400, ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var fluidsReportObj = _mapper.Map<FluidsReport>(fluidsReportDtos);
                if (!_frRepository.CreateFluidsReport(fluidsReportObj))
                {
                    ModelState.AddModelError("", $"Something went wrong when saving the record {fluidsReportObj.Name}");
                    return StatusCode(500, ModelState);

                }
                return CreatedAtRoute("GetFluidReportCore", new { fluidsReportuid = fluidsReportObj.Uid }, fluidsReportObj);

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "FluidsReport CreateFluidReportCore", null);
                return Ok(ex.ToString());
            }
        }


        [HttpPatch("{uid}", Name = "UpdateFluidReportCore")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateFluidReportCore(string uid, [FromBody] FluidsReportDto fluidsReportDtos)
        {
            try
            {
                if (fluidsReportDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_frRepository.FluidsReportExists(fluidsReportDtos.Uid))
                {
                    ModelState.AddModelError("", "FluidReportCore Already Exist");
                    return StatusCode(400, ModelState);
                }

                var cementJobDto = _mapper.Map<FluidsReport>(fluidsReportDtos);
                if (!_frRepository.UpdateFluidsReport(cementJobDto))
                {
                    ModelState.AddModelError("", $"Something went wrong when updating the record {fluidsReportDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "FluidsReport UpdateFluidReportCore", null);
                return Ok();
            }
        }

        [HttpDelete("{uid}", Name = "DeleteFluidReportCore")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteFluidReportCore(string uid)
        {
            try
            {
                if (!_frRepository.FluidsReportExists(uid))
                {
                    return NotFound();
                }


                var fluidsReportDtos = _frRepository.GetFluidsReportDetail(uid);
                if (!_frRepository.DeleteFluidsReport(fluidsReportDtos))
                {
                    ModelState.AddModelError("", $"Something went wrong when Deleting the record {fluidsReportDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "FluidsReport DeleteFluidReportCore", null);
                return Ok();
            }
        }
    }
}
