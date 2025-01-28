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
    public class CoordinateReferenceSystemController : ControllerBase
    {
        private ICoordinateReferenceSystemRepository _cnrRepository;
        private readonly IMapper _mapper;
        private readonly WebAIAdvisorContext _db;
        public CoordinateReferenceSystemController(ICoordinateReferenceSystemRepository crRepo, IMapper mapper, WebAIAdvisorContext db)
        {
            _cnrRepository = crRepo;
            _mapper = mapper;
            _db = db;
        }
        WellAIAdvisiorRegistrationRepository wellAIRepository = new WellAIAdvisiorRegistrationRepository();

        [MapToApiVersion("1.0")]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<CoordinateReferenceSystemDto>))]
        [ProducesResponseType(400)]

        public IActionResult GetCoordinateReferenceSystems()
        {
            try
            {

                var objList = _cnrRepository.GetCoordinateReferenceSystemDetails();
                var objDto = new List<CoordinateReferenceSystemDto>();
                foreach (var obj in objList)
                {
                    objDto.Add(_mapper.Map<CoordinateReferenceSystemDto>(obj));
                }
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "CoordinateReference GetCoordinateReferenceSystems", null);
                return Ok(ex.ToString());
            }
        }

        [MapToApiVersion("1.0")]
        [HttpGet("{coordinateRefuid}", Name = "GetCoordinateReferenceSystem")]
        [ProducesResponseType(200, Type = typeof(CoordinateReferenceSystemDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetCoordinateReferenceSystem(string coordinateRefuid)
        {
            try
            {

                var obj = _cnrRepository.GetCoordinateReferenceSystemDetail(coordinateRefuid);

                if (obj == null)
                {
                    return NotFound();
                }
                var objDto = _mapper.Map<CoordinateReferenceSystemDto>(obj);
                
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "CoordinateReference GetCoordinateReferenceSystem", null);
                return Ok(ex.ToString());
            }
        }

        [MapToApiVersion("1.0")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CoordinateReferenceSystemDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateCoordinateReferenceSystem([FromBody] CoordinateReferenceSystemDto coordinateRefDtos)
        {
            try
            {
                if (coordinateRefDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_cnrRepository.CoordinateReferenceSystemExists(coordinateRefDtos.Uid))
                {
                    ModelState.AddModelError("", "CoordinateReferenceSystem Already Exist");
                    return StatusCode(400, ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var coordinateRefObj = _mapper.Map<CoordinateReferenceSystem>(coordinateRefDtos);
                if (!_cnrRepository.CreateCoordinateReferenceSystem(coordinateRefObj))
                {
                    ModelState.AddModelError("", $"Something went wrong when saving the record {coordinateRefObj.Name}");
                    return StatusCode(500, ModelState);

                }
                return CreatedAtRoute("GetCoordinateReferenceSystem", new { coordinateRefuid = coordinateRefObj.Uid }, coordinateRefObj);

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "CoordinateReference CreateCoordinateReferenceSystem", null);
                return Ok(ex.ToString());
            }
        }

        [MapToApiVersion("1.0")]
        [HttpPatch("{uid}", Name = "UpdateCoordinateReferenceSystem")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateCoordinateReferenceSystem(string uid, [FromBody] CoordinateReferenceSystemDto coordinateRefDtos)
        {
            try
            {
                if (coordinateRefDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_cnrRepository.CoordinateReferenceSystemExists(coordinateRefDtos.Uid))
                {
                    ModelState.AddModelError("", "CoordinateReferenceSystem Already Exist");
                    return StatusCode(400, ModelState);
                }

                var cementJobDto = _mapper.Map<CoordinateReferenceSystem>(coordinateRefDtos);
                if (!_cnrRepository.UpdateCoordinateReferenceSystem(cementJobDto))
                {
                    ModelState.AddModelError("", $"Something went wrong when updating the record {coordinateRefDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "CoordinateReference UpdateCoordinateReferenceSystem", null);
                return Ok();
            }
        }

        [MapToApiVersion("1.0")]
        [HttpDelete("{uid}", Name = "DeleteCoordinateReferenceSystem")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteCoordinateReferenceSystem(string uid)
        {
            try
            {
                if (!_cnrRepository.CoordinateReferenceSystemExists(uid))
                {
                    return NotFound();
                }


                var coordinateRefDtos = _cnrRepository.GetCoordinateReferenceSystemDetail(uid);
                if (!_cnrRepository.DeleteCoordinateReferenceSystem(coordinateRefDtos))
                {
                    ModelState.AddModelError("", $"Something went wrong when Deleting the record {coordinateRefDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "CoordinateReference DeleteCoordinateReferenceSystem", null);
                return Ok();
            }
        }
    }
}
