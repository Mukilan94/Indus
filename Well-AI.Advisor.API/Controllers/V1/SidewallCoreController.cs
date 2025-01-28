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
    public class SidewallCoreController : ControllerBase
    {
        private ISidewallCoreRepository _swRepository;
        private readonly IMapper _mapper;
        private readonly WebAIAdvisorContext _db;
        public SidewallCoreController(ISidewallCoreRepository swRepo, IMapper mapper, WebAIAdvisorContext db)
        {
            _swRepository = swRepo;
            _mapper = mapper;
            _db = db;
        }
        WellAIAdvisiorRegistrationRepository wellAIRepository = new WellAIAdvisiorRegistrationRepository();

        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<SidewallCoreDto>))]
        [ProducesResponseType(400)]

        public IActionResult GetSidewallCores()
        {
            try
            {

                var objList = _swRepository.GetSidewallCoreDetails();
                var objDto = new List<SidewallCoreDto>();
                foreach (var obj in objList)
                {
                    objDto.Add(_mapper.Map<SidewallCoreDto>(obj));
                }
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "SidewallCore GetSidewallCores", null);
                return Ok(ex.ToString());
            }
        }

        [HttpGet("{swuid}", Name = "GetSidewallCore")]
        [ProducesResponseType(200, Type = typeof(SidewallCoreDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetSidewallCore(string swuid)
        {
            try
            {

                var obj = _swRepository.GetSidewallCoreDetail(swuid);

                if (obj == null)
                {
                    return NotFound();
                }
                var objDto = _mapper.Map<SidewallCoreDto>(obj);
                
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "SidewallCore GetSidewallCore", null);
                return Ok(ex.ToString());
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SidewallCoreDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateSidewallCore([FromBody] SidewallCoreDto lgDtos)
        {
            try
            {
                if (lgDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_swRepository.SidewallCoreExists(lgDtos.Uid))
                {
                    ModelState.AddModelError("", "SidewallCore Already Exist");
                    return StatusCode(400, ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var lgObj = _mapper.Map<SidewallCore>(lgDtos);
                if (!_swRepository.CreateSidewallCore(lgObj))
                {
                    ModelState.AddModelError("", $"Something went wrong when saving the record {lgObj.Name}");
                    return StatusCode(500, ModelState);

                }
                return CreatedAtRoute("GetSidewallCore", new { swuid = lgObj.Uid }, lgObj);

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "SidewallCore CreateSidewallCore", null);
                return Ok(ex.ToString());
            }
        }


        [HttpPatch("{uid}", Name = "UpdateSidewallCore")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateSidewallCore(string uid, [FromBody] SidewallCoreDto lgDtos)
        {
            try
            {
                if (lgDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_swRepository.SidewallCoreExists(lgDtos.Uid))
                {
                    ModelState.AddModelError("", "SidewallCore Already Exist");
                    return StatusCode(400, ModelState);
                }

                var cementJobDto = _mapper.Map<SidewallCore>(lgDtos);
                if (!_swRepository.UpdateSidewallCore(cementJobDto))
                {
                    ModelState.AddModelError("", $"Something went wrong when updating the record {lgDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "SidewallCore UpdateSidewallCore", null);
                return Ok();
            }
        }

        [HttpDelete("{uid}", Name = "DeleteSidewallCore")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteSidewallCore(string uid)
        {
            try
            {
                if (!_swRepository.SidewallCoreExists(uid))
                {
                    return NotFound();
                }


                var lgDtos = _swRepository.GetSidewallCoreDetail(uid);
                if (!_swRepository.DeleteSidewallCore(lgDtos))
                {
                    ModelState.AddModelError("", $"Something went wrong when Deleting the record {lgDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "SidewallCore DeleteSidewallCore", null);
                return Ok();
            }
        }
    }
}
