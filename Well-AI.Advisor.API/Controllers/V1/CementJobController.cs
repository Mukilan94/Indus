using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Well_AI.Advisor.API.Dtos;
using Well_AI.Advisor.API.Mapper;
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
    public class CementJobController : ControllerBase
    {
        private ICementJobRepository _cjRepository;
        private readonly IMapper _mapper;
        private readonly WebAIAdvisorContext _db;
        public CementJobController(ICementJobRepository cjRepo, IMapper mapper, WebAIAdvisorContext db)
        {
            _cjRepository = cjRepo;
            _mapper = mapper;
            _db = db;
        }
        WellAIAdvisiorRegistrationRepository wellAIRepository = new WellAIAdvisiorRegistrationRepository();

        [MapToApiVersion("1.0")]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<CementJobDto>))]
        [ProducesResponseType(400)]

        public IActionResult GetCementJobs()
        {
            try
            {

                var objList = _cjRepository.GetCementJobDetails();
                var objDto = new List<CementJobDto>();
                foreach (var obj in objList)
                {
                    objDto.Add(_mapper.Map<CementJobDto>(obj));
                }
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "CementJob GetCementJobs", null);
                return Ok(ex.ToString());
            }
        }

        [MapToApiVersion("1.0")]
        [HttpGet("{CementJobuid}", Name = "GetCementJob")]
        [ProducesResponseType(200, Type = typeof(CementJobDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetCementJob(string CementJobuid)
        {
            try
            {

                var obj = _cjRepository.GetCementJobDetail(CementJobuid);

                if (obj == null)
                {
                    return NotFound();
                }
                  var objDto = _mapper.Map<CementJobDto>(obj);
                
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "CementJob GetCementJob", null);
                return Ok(ex.ToString());
            }
        }

        [MapToApiVersion("1.0")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CementJobDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateCementJob([FromBody] CementJobDto cementJobDtos)
        {
            try
            {
                if (cementJobDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_cjRepository.CementJobExists(cementJobDtos.Uid))
                {
                    ModelState.AddModelError("", "CementJob Already Exist");
                    return StatusCode(400, ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var CementJobObj = _mapper.Map<CementJob>(cementJobDtos);
                if (!_cjRepository.CreateCementJob(CementJobObj))
                {
                    ModelState.AddModelError("", $"Something went wrong when saving the record {CementJobObj.Name}");
                    return StatusCode(500, ModelState);

                }
                return CreatedAtRoute("GetCementJob", new { CementJobuid = CementJobObj.Uid }, CementJobObj);

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "CementJob CreateCementJob", null);
                return Ok(ex.ToString());
            }
        }

        [MapToApiVersion("1.0")]
        [HttpPatch("{uid}", Name = "UpdateCementJob")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateCementJob(string uid, [FromBody] CementJobDto cementJobDtos)
        {
            try
            {
                if (cementJobDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_cjRepository.CementJobExists(cementJobDtos.Uid))
                {
                    ModelState.AddModelError("", "CementJob Already Exist");
                    return StatusCode(400, ModelState);
                }

                var cementJobDto = _mapper.Map<CementJob>(cementJobDtos);
                if (!_cjRepository.UpdateCementJob(cementJobDto))
                {
                    ModelState.AddModelError("", $"Something went wrong when updating the record {cementJobDto.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "CementJob UpdateCementJob", null);
                return Ok();
            }
        }

        [MapToApiVersion("1.0")]
        [HttpDelete("{uid}", Name = "DeleteCementJob")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteCementJob(string uid)
        {
            try
            {
                if (!_cjRepository.CementJobExists(uid))
                {
                    return NotFound();
                }


                var cementJobDto = _cjRepository.GetCementJobDetail(uid);
                if (!_cjRepository.DeleteCementJob(cementJobDto))
                {
                    ModelState.AddModelError("", $"Something went wrong when Deleting the record {cementJobDto.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "CementJob DeleteCementJob", null);
                return Ok();
            }
        }
    }
}