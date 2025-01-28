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
    public class TubularController : ControllerBase
    {
        private ITubularRepository _tbrRepository;
        private readonly IMapper _mapper;
        private readonly WebAIAdvisorContext _db;
        public TubularController(ITubularRepository tbrRepo, IMapper mapper, WebAIAdvisorContext db)
        {
            _tbrRepository = tbrRepo;
            _mapper = mapper;
            _db = db;
        }
        WellAIAdvisiorRegistrationRepository wellAIRepository = new WellAIAdvisiorRegistrationRepository();

         
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<TubularDto>))]
        [ProducesResponseType(400)]

        public IActionResult GetTubulars()
        {
            try
            {

                var objList = _tbrRepository.GetTubularDetails();
                var objDto = new List<TubularDto>();
                foreach (var obj in objList)
                {
                    objDto.Add(_mapper.Map<TubularDto>(obj));
                }
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Tubular GetTubulars", null);
                return Ok(ex.ToString());
            }
        }

        [HttpGet("{tbruid}", Name = "GetTubular")]
        [ProducesResponseType(200, Type = typeof(TubularDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetTubular(string tbruid)
        {
            try
            {

                var obj = _tbrRepository.GetTubularDetail(tbruid);

                if (obj == null)
                {
                    return NotFound();
                }
                var objDto = _mapper.Map<TubularDto>(obj);
                
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Tubular GetTubular", null);
                return Ok(ex.ToString());
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TubularDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateTubular([FromBody] TubularDto lgDtos)
        {
            try
            {
                if (lgDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_tbrRepository.TubularExists(lgDtos.Uid))
                {
                    ModelState.AddModelError("", "Tubular Already Exist");
                    return StatusCode(400, ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var lgObj = _mapper.Map<Tubular>(lgDtos);
                if (!_tbrRepository.CreateTubular(lgObj))
                {
                    ModelState.AddModelError("", $"Something went wrong when saving the record {lgObj.Name}");
                    return StatusCode(500, ModelState);

                }
                return CreatedAtRoute("GetTubular", new { tbruid = lgObj.Uid }, lgObj);

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Tubular CreateTubular", null);
                return Ok(ex.ToString());
            }
        }


        [HttpPatch("{uid}", Name = "UpdateTubular")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateTubular(string uid, [FromBody] TubularDto lgDtos)
        {
            try
            {
                if (lgDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_tbrRepository.TubularExists(lgDtos.Uid))
                {
                    ModelState.AddModelError("", "Tubular Already Exist");
                    return StatusCode(400, ModelState);
                }

                var cementJobDto = _mapper.Map<Tubular>(lgDtos);
                if (!_tbrRepository.UpdateTubular(cementJobDto))
                {
                    ModelState.AddModelError("", $"Something went wrong when updating the record {lgDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Tubular UpdateTubular", null);
                return Ok();
            }
        }

        [HttpDelete("{uid}", Name = "DeleteTubular")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteTubular(string uid)
        {
            try
            {
                if (!_tbrRepository.TubularExists(uid))
                {
                    return NotFound();
                }


                var lgDtos = _tbrRepository.GetTubularDetail(uid);
                if (!_tbrRepository.DeleteTubular(lgDtos))
                {
                    ModelState.AddModelError("", $"Something went wrong when Deleting the record {lgDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Tubular DeleteTubular", null);
                return Ok();
            }
        }
    }
}
