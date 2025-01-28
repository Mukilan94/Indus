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
    public class WbGeometryController : ControllerBase
    {
        private IWbGeometryRepository _wbrRepository;
        private readonly IMapper _mapper;
        private readonly WebAIAdvisorContext _db;
        public WbGeometryController(IWbGeometryRepository wbrRepo, IMapper mapper, WebAIAdvisorContext db)
        {
            _wbrRepository = wbrRepo;
            _mapper = mapper;
            _db = db;
        }
        WellAIAdvisiorRegistrationRepository wellAIRepository = new WellAIAdvisiorRegistrationRepository();

        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<WbGeometryDto>))]
        [ProducesResponseType(400)]

        public IActionResult GetWbGeometrys()
        {
            try
            {

                var objList = _wbrRepository.GetWbGeometryDetails();
                var objDto = new List<WbGeometryDto>();
                foreach (var obj in objList)
                {
                    objDto.Add(_mapper.Map<WbGeometryDto>(obj));
                }
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "WbGeometry GetWbGeometrys", null);
                return Ok(ex.ToString());
            }
        }

        [HttpGet("{wbruid}", Name = "GetWbGeometry")]
        [ProducesResponseType(200, Type = typeof(WbGeometryDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetWbGeometry(string wbruid)
        {
            try
            {

                var obj = _wbrRepository.GetWbGeometryDetail(wbruid);

                if (obj == null)
                {
                    return NotFound();
                }
                var objDto = _mapper.Map<WbGeometryDto>(obj);
                
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "WbGeometry GetWbGeometry", null);
                return Ok(ex.ToString());
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(WbGeometryDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateWbGeometry([FromBody] WbGeometryDto lgDtos)
        {
            try
            {
                if (lgDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_wbrRepository.WbGeometryExists(lgDtos.Uid))
                {
                    ModelState.AddModelError("", "WbGeometry Already Exist");
                    return StatusCode(400, ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var lgObj = _mapper.Map<WbGeometry>(lgDtos);
                if (!_wbrRepository.CreateWbGeometry(lgObj))
                {
                    ModelState.AddModelError("", $"Something went wrong when saving the record {lgObj.Name}");
                    return StatusCode(500, ModelState);

                }
                return CreatedAtRoute("GetWbGeometry", new { wbruid = lgObj.Uid }, lgObj);

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "WbGeometry CreateWbGeometry", null);
                return Ok(ex.ToString());
            }
        }


        [HttpPatch("{uid}", Name = "UpdateWbGeometry")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateWbGeometry(string uid, [FromBody] WbGeometryDto lgDtos)
        {
            try
            {
                if (lgDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_wbrRepository.WbGeometryExists(lgDtos.Uid))
                {
                    ModelState.AddModelError("", "WbGeometry Already Exist");
                    return StatusCode(400, ModelState);
                }

                var cementJobDto = _mapper.Map<WbGeometry>(lgDtos);
                if (!_wbrRepository.UpdateWbGeometry(cementJobDto))
                {
                    ModelState.AddModelError("", $"Something went wrong when updating the record {lgDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "WbGeometry UpdateWbGeometry", null);
                return Ok();
            }
        }

        [HttpDelete("{uid}", Name = "DeleteWbGeometry")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteWbGeometry(string uid)
        {
            try
            {
                if (!_wbrRepository.WbGeometryExists(uid))
                {
                    return NotFound();
                }


                var lgDtos = _wbrRepository.GetWbGeometryDetail(uid);
                if (!_wbrRepository.DeleteWbGeometry(lgDtos))
                {
                    ModelState.AddModelError("", $"Something went wrong when Deleting the record {lgDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "WbGeometry DeleteWbGeometry", null);
                return Ok();
            }
        }
    }
}
