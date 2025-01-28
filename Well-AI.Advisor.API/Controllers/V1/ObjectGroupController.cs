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
    public class ObjectGroupController : ControllerBase
    {
        private IObjectGroupRepository _ogRepository;
        private readonly IMapper _mapper;
        private readonly WebAIAdvisorContext _db;
        public ObjectGroupController(IObjectGroupRepository ogRepo, IMapper mapper, WebAIAdvisorContext db)
        {
            _ogRepository = ogRepo;
            _mapper = mapper;
            _db = db;
        }
        WellAIAdvisiorRegistrationRepository wellAIRepository = new WellAIAdvisiorRegistrationRepository();

        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ObjectGroupDto>))]
        [ProducesResponseType(400)]

        public IActionResult GetObjectGroups()
        {
            try
            {

                var objList = _ogRepository.GetObjectGroupDetails();
                var objDto = new List<ObjectGroupDto>();
                foreach (var obj in objList)
                {
                    objDto.Add(_mapper.Map<ObjectGroupDto>(obj));
                }
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "ObjectGroup GetObjectGroups", null);
                return Ok(ex.ToString());
            }
        }

        [HttpGet("{oguid}", Name = "GetObjectGroup")]
        [ProducesResponseType(200, Type = typeof(ObjectGroupDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetObjectGroup(string oguid)
        {
            try
            {

                var obj = _ogRepository.GetObjectGroupDetail(oguid);

                if (obj == null)
                {
                    return NotFound();
                }
                var objDto = _mapper.Map<ObjectGroupDto>(obj);
                
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "ObjectGroup GetObjectGroup", null);
                return Ok(ex.ToString());
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ObjectGroupDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateObjectGroup([FromBody] ObjectGroupDto lgDtos)
        {
            try
            {
                if (lgDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_ogRepository.ObjectGroupExists(lgDtos.Uid))
                {
                    ModelState.AddModelError("", "ObjectGroup Already Exist");
                    return StatusCode(400, ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var lgObj = _mapper.Map<ObjectGroup>(lgDtos);
                if (!_ogRepository.CreateObjectGroup(lgObj))
                {
                    ModelState.AddModelError("", $"Something went wrong when saving the record {lgObj.Name}");
                    return StatusCode(500, ModelState);

                }
                return CreatedAtRoute("GetObjectGroup", new { oguid = lgObj.Uid }, lgObj);

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "ObjectGroup CreateObjectGroup", null);
                return Ok(ex.ToString());
            }
        }


        [HttpPatch("{uid}", Name = "UpdateObjectGroup")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateObjectGroup(string uid, [FromBody] ObjectGroupDto lgDtos)
        {
            try
            {
                if (lgDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_ogRepository.ObjectGroupExists(lgDtos.Uid))
                {
                    ModelState.AddModelError("", "ObjectGroup Already Exist");
                    return StatusCode(400, ModelState);
                }

                var cementJobDto = _mapper.Map<ObjectGroup>(lgDtos);
                if (!_ogRepository.UpdateObjectGroup(cementJobDto))
                {
                    ModelState.AddModelError("", $"Something went wrong when updating the record {lgDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "ObjectGroup UpdateObjectGroup", null);
                return Ok();
            }
        }

        [HttpDelete("{uid}", Name = "DeleteObjectGroup")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteObjectGroup(string uid)
        {
            try
            {
                if (!_ogRepository.ObjectGroupExists(uid))
                {
                    return NotFound();
                }


                var lgDtos = _ogRepository.GetObjectGroupDetail(uid);
                if (!_ogRepository.DeleteObjectGroup(lgDtos))
                {
                    ModelState.AddModelError("", $"Something went wrong when Deleting the record {lgDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "ObjectGroup DeleteObjectGroup", null);
                return Ok();
            }
        }
    }
}
