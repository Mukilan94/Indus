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
    public class ToolErrorTermSetController : ControllerBase
    {
        private IToolErrorTermSetRepository _tetRepository;
        private readonly IMapper _mapper;
        private readonly WebAIAdvisorContext _db;
        public ToolErrorTermSetController(IToolErrorTermSetRepository tetRepo, IMapper mapper, WebAIAdvisorContext db)
        {
            _tetRepository = tetRepo;
            _mapper = mapper;
            _db = db;
        }
        WellAIAdvisiorRegistrationRepository wellAIRepository = new WellAIAdvisiorRegistrationRepository();

        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ToolErrorTermSetDto>))]
        [ProducesResponseType(400)]

        public IActionResult GetToolErrorTermSets()
        {
            try
            {

                var objList = _tetRepository.GetToolErrorTermSetDetails();
                var objDto = new List<ToolErrorTermSetDto>();
                foreach (var obj in objList)
                {
                    objDto.Add(_mapper.Map<ToolErrorTermSetDto>(obj));
                }
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "ToolErrorModel GetToolErrorTermSets", null);
                return Ok(ex.ToString());
            }
        }

        [HttpGet("{tetuid}", Name = "GetToolErrorTermSet")]
        [ProducesResponseType(200, Type = typeof(ToolErrorTermSetDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetToolErrorTermSet(string tetuid)
        {
            try
            {

                var obj = _tetRepository.GetToolErrorTermSetDetail(tetuid);

                if (obj == null)
                {
                    return NotFound();
                }
                var objDto = _mapper.Map<ToolErrorTermSetDto>(obj);
                
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "ToolErrorModel GetToolErrorTermSet", null);
                return Ok(ex.ToString());
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ToolErrorTermSetDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateToolErrorTermSet([FromBody] ToolErrorTermSetDto lgDtos)
        {
            try
            {
                if (lgDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_tetRepository.ToolErrorTermSetExists(lgDtos.Uid))
                {
                    ModelState.AddModelError("", "ToolErrorTermSet Already Exist");
                    return StatusCode(400, ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var lgObj = _mapper.Map<ToolErrorTermSet>(lgDtos);
                if (!_tetRepository.CreateToolErrorTermSet(lgObj))
                {
                    ModelState.AddModelError("", $"Something went wrong when saving the record {lgObj.Name}");
                    return StatusCode(500, ModelState);

                }
                return CreatedAtRoute("GetToolErrorTermSet", new { tetuid = lgObj.Uid }, lgObj);

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "ToolErrorModel CreateToolErrorTermSet", null);
                return Ok(ex.ToString());
            }
        }


        [HttpPatch("{uid}", Name = "UpdateToolErrorTermSet")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateToolErrorTermSet(string uid, [FromBody] ToolErrorTermSetDto lgDtos)
        {
            try
            {
                if (lgDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_tetRepository.ToolErrorTermSetExists(lgDtos.Uid))
                {
                    ModelState.AddModelError("", "ToolErrorTermSet Already Exist");
                    return StatusCode(400, ModelState);
                }

                var cementJobDto = _mapper.Map<ToolErrorTermSet>(lgDtos);
                if (!_tetRepository.UpdateToolErrorTermSet(cementJobDto))
                {
                    ModelState.AddModelError("", $"Something went wrong when updating the record {lgDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "ToolErrorModel UpdateToolErrorTermSet", null);
                return Ok();
            }
        }

        [HttpDelete("{uid}", Name = "DeleteToolErrorTermSet")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteToolErrorTermSet(string uid)
        {
            try
            {
                if (!_tetRepository.ToolErrorTermSetExists(uid))
                {
                    return NotFound();
                }


                var lgDtos = _tetRepository.GetToolErrorTermSetDetail(uid);
                if (!_tetRepository.DeleteToolErrorTermSet(lgDtos))
                {
                    ModelState.AddModelError("", $"Something went wrong when Deleting the record {lgDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "ToolErrorModel DeleteToolErrorTermSet", null);
                return Ok();
            }
        }
    }
}
