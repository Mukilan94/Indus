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
    public class SurveyProgramController : ControllerBase
    {
        private ISurveyProgramRepository _spRepository;
        private readonly IMapper _mapper;
        private readonly WebAIAdvisorContext _db;
        public SurveyProgramController(ISurveyProgramRepository spRepo, IMapper mapper, WebAIAdvisorContext db)
        {
            _spRepository = spRepo;
            _mapper = mapper;
            _db = db;
        }
        WellAIAdvisiorRegistrationRepository wellAIRepository = new WellAIAdvisiorRegistrationRepository();

        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<SurveyProgramDto>))]
        [ProducesResponseType(400)]

        public IActionResult GetSurveyPrograms()
        {
            try
            {

                var objList = _spRepository.GetSurveyProgramDetails();
                var objDto = new List<SurveyProgramDto>();
                foreach (var obj in objList)
                {
                    objDto.Add(_mapper.Map<SurveyProgramDto>(obj));
                }
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "SurveyProgram GetSurveyPrograms", null);
                return Ok(ex.ToString());
            }
        }

        [HttpGet("{spuid}", Name = "GetSurveyProgram")]
        [ProducesResponseType(200, Type = typeof(SurveyProgramDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetSurveyProgram(string spuid)
        {
            try
            {

                var obj = _spRepository.GetSurveyProgramDetail(spuid);

                if (obj == null)
                {
                    return NotFound();
                }
                var objDto = _mapper.Map<SurveyProgramDto>(obj);
                
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "SurveyProgram GetSurveyProgram", null);
                return Ok(ex.ToString());
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SurveyProgramDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateSurveyProgram([FromBody] SurveyProgramDto lgDtos)
        {
            try
            {
                if (lgDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_spRepository.SurveyProgramExists(lgDtos.Uid))
                {
                    ModelState.AddModelError("", "SurveyProgram Already Exist");
                    return StatusCode(400, ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var lgObj = _mapper.Map<SurveyProgram>(lgDtos);
                if (!_spRepository.CreateSurveyProgram(lgObj))
                {
                    ModelState.AddModelError("", $"Something went wrong when saving the record {lgObj.Name}");
                    return StatusCode(500, ModelState);

                }
                return CreatedAtRoute("GetSurveyProgram", new { spuid = lgObj.Uid }, lgObj);

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "SurveyProgram CreateSurveyProgram", null);
                return Ok(ex.ToString());
            }
        }


        [HttpPatch("{uid}", Name = "UpdateSurveyProgram")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateSurveyProgram(string uid, [FromBody] SurveyProgramDto lgDtos)
        {
            try
            {
                if (lgDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_spRepository.SurveyProgramExists(lgDtos.Uid))
                {
                    ModelState.AddModelError("", "SurveyProgram Already Exist");
                    return StatusCode(400, ModelState);
                }

                var cementJobDto = _mapper.Map<SurveyProgram>(lgDtos);
                if (!_spRepository.UpdateSurveyProgram(cementJobDto))
                {
                    ModelState.AddModelError("", $"Something went wrong when updating the record {lgDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "SurveyProgram UpdateSurveyProgram", null);
                return Ok();
            }
        }

        [HttpDelete("{uid}", Name = "DeleteSurveyProgram")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteSurveyProgram(string uid)
        {
            try
            {
                if (!_spRepository.SurveyProgramExists(uid))
                {
                    return NotFound();
                }


                var lgDtos = _spRepository.GetSurveyProgramDetail(uid);
                if (!_spRepository.DeleteSurveyProgram(lgDtos))
                {
                    ModelState.AddModelError("", $"Something went wrong when Deleting the record {lgDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "SurveyProgram DeleteSurveyProgram", null);
                return Ok();
            }
        }
    }
}
