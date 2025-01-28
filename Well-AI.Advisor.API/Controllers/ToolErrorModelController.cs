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
    public class ToolErrorModelController : ControllerBase
    {
        private IToolErrorModelRepository _teRepository;
        private readonly IMapper _mapper;
        private readonly WebAIAdvisorContext _db;
        public ToolErrorModelController(IToolErrorModelRepository teRepo, IMapper mapper, WebAIAdvisorContext db)
        {
            _teRepository = teRepo;
            _mapper = mapper;
            _db = db;
        }
        WellAIAdvisiorRegistrationRepository wellAIRepository = new WellAIAdvisiorRegistrationRepository();

       
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ToolErrorModelDto>))]
        [ProducesResponseType(400)]

        public IActionResult GetToolErrorModels()
        {
            try
            {

                var objList = _teRepository.GetToolErrorModelDetails();
                var objDto = new List<ToolErrorModelDto>();
                foreach (var obj in objList)
                {
                    objDto.Add(_mapper.Map<ToolErrorModelDto>(obj));
                }
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "ToolErrorModel GetToolErrorModels", null);
                return Ok(ex.ToString());
            }
        }

        [HttpGet("{teuid}", Name = "GetToolErrorModel")]
        [ProducesResponseType(200, Type = typeof(ToolErrorModelDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetToolErrorModel(string teuid)
        {
            try
            {

                var obj = _teRepository.GetToolErrorModelDetail(teuid);

                if (obj == null)
                {
                    return NotFound();
                }
                var objDto = _mapper.Map<ToolErrorModelDto>(obj);
               
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "ToolErrorModel GetToolErrorModel", null);
                return Ok(ex.ToString());
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ToolErrorModelDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateToolErrorModel([FromBody] List<ToolErrorModelDto> lgDtos)
        {
            try
            {
                foreach (ToolErrorModelDto item in lgDtos)
                {

                    if (item == null)
                    {
                        return BadRequest(ModelState);
                    }
                    if (_teRepository.ToolErrorModelExists(item.Uid))
                    {
                        ModelState.AddModelError("", "ToolErrorModel Already Exist");
                        return StatusCode(400, ModelState);
                    }
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    var lgObj = _mapper.Map<ToolErrorModel>(item);
                    if (!_teRepository.CreateToolErrorModel(lgObj))
                    {
                        ModelState.AddModelError("", $"Something went wrong when saving the record {lgObj.Name}");
                        return StatusCode(500, ModelState);

                    }
                }
                return Ok();
                
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "ToolErrorModel CreateToolErrorModel", null);
                return Ok(ex.ToString());
            }
        }


        [HttpPatch("{uid}", Name = "UpdateToolErrorModel")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateToolErrorModel(string uid, [FromBody] ToolErrorModelDto lgDtos)
        {
            try
            {
                if (lgDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_teRepository.ToolErrorModelExists(lgDtos.Uid))
                {
                    ModelState.AddModelError("", "ToolErrorModel Already Exist");
                    return StatusCode(400, ModelState);
                }

                var cementJobDto = _mapper.Map<ToolErrorModel>(lgDtos);
                if (!_teRepository.UpdateToolErrorModel(cementJobDto))
                {
                    ModelState.AddModelError("", $"Something went wrong when updating the record {lgDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "ToolErrorModel UpdateToolErrorModel", null);
                return Ok();
            }
        }

        [HttpDelete("{uid}", Name = "DeleteToolErrorModel")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteToolErrorModel(string uid)
        {
            try
            {
                if (!_teRepository.ToolErrorModelExists(uid))
                {
                    return NotFound();
                }


                var lgDtos = _teRepository.GetToolErrorModelDetail(uid);
                if (!_teRepository.DeleteToolErrorModel(lgDtos))
                {
                    ModelState.AddModelError("", $"Something went wrong when Deleting the record {lgDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "ToolErrorModel DeleteToolErrorModel", null);
                return Ok();
            }
        }
    }
}
