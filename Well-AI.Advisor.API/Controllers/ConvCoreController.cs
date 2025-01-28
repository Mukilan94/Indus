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
    public class ConvCoreController : ControllerBase
    {
        private IConvCoreRepository _crRepository;
        private readonly IMapper _mapper;
        private readonly WebAIAdvisorContext _db;
        public ConvCoreController(IConvCoreRepository crRepo, IMapper mapper, WebAIAdvisorContext db)
        {
            _crRepository = crRepo;
            _mapper = mapper;
            _db = db;
        }
        WellAIAdvisiorRegistrationRepository wellAIRepository = new WellAIAdvisiorRegistrationRepository();

         
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ConvCoreDto>))]
        [ProducesResponseType(400)]

        public IActionResult GetConvCores()
        {
            try
            {

                var objList = _crRepository.GetConvCoreDetails();
                var objDto = new List<ConvCoreDto>();
                foreach (var obj in objList)
                {
                    objDto.Add(_mapper.Map<ConvCoreDto>(obj));
                }
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "ConvCore GetConvCores", null);
                return Ok(ex.ToString());
            }
        }

        [HttpGet("{convCoreuid}", Name = "GetConvCore")]
        [ProducesResponseType(200, Type = typeof(ConvCoreDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetConvCore(string convCoreuid)
        {
            try
            {

                var obj = _crRepository.GetConvCoreDetail(convCoreuid);

                if (obj == null)
                {
                    return NotFound();
                }
                var objDto = _mapper.Map<ConvCoreDto>(obj);
                 
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "ConvCore GetConvCore", null);
                return Ok(ex.ToString());
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ConvCoreDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateConvCore([FromBody] ConvCoreDto convCoreDtos)
        {
            try
            {
                if (convCoreDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_crRepository.ConvCoreExists(convCoreDtos.Uid))
                {
                    ModelState.AddModelError("", "ConvCore Already Exist");
                    return StatusCode(400, ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var convCoreObj = _mapper.Map<ConvCore>(convCoreDtos);
                if (!_crRepository.CreateConvCore(convCoreObj))
                {
                    ModelState.AddModelError("", $"Something went wrong when saving the record {convCoreObj.Name}");
                    return StatusCode(500, ModelState);

                }
                return CreatedAtRoute("GetConvCore", new { convCoreuid = convCoreObj.Uid }, convCoreObj);

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "ConvCore CreateConvCore", null);
                return Ok(ex.ToString());
            }
        }


        [HttpPatch("{uid}", Name = "UpdateConvCore")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateConvCore(string uid, [FromBody] ConvCoreDto convCoreDtos)
        {
            try
            {
                if (convCoreDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_crRepository.ConvCoreExists(convCoreDtos.Uid))
                {
                    ModelState.AddModelError("", "ConvCore Already Exist");
                    return StatusCode(400, ModelState);
                }

                var cementJobDto = _mapper.Map<ConvCore>(convCoreDtos);
                if (!_crRepository.UpdateConvCore(cementJobDto))
                {
                    ModelState.AddModelError("", $"Something went wrong when updating the record {convCoreDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "ConvCore UpdateConvCore", null);
                return Ok();
            }
        }

        [HttpDelete("{uid}", Name = "DeleteConvCore")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteConvCore(string uid)
        {
            try
            {
                if (!_crRepository.ConvCoreExists(uid))
                {
                    return NotFound();
                }


                var convCoreDtos = _crRepository.GetConvCoreDetail(uid);
                if (!_crRepository.DeleteConvCore(convCoreDtos))
                {
                    ModelState.AddModelError("", $"Something went wrong when Deleting the record {convCoreDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "ConvCore DeleteConvCore", null);
                return Ok();
            }
        }
    }
}
