using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Well_AI.Advisor.API.Models;
using Well_AI.Advisor.API.Repository;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Well_AI.Advisor.API.Repository.IRepository;
using Well_AI.Advisor.API.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WellAI.Advisor.API.Models;
using Well_AI.Advisor.API.Data;
using Microsoft.EntityFrameworkCore;
using WellAI.Advisor.API.Repository;
using WellAI.Advisor.API.Repository.IRepository;
using WellAI.Advisor.DLL.Data;

namespace Well_AI.Advisor.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class BharunController : ControllerBase
    {
        private IBharunRepository _brRepository;
        private readonly WebAIAdvisorContext _db;
        private readonly IMapper _mapper;
        public BharunController(IConfiguration configuration,  IMapper mapper, IBharunRepository brRepo, WebAIAdvisorContext db)
        {
            _brRepository = brRepo;
            _mapper = mapper;
            _db = db;
        }
        WellAIAdvisiorRegistrationRepository wellAIRepository = new WellAIAdvisiorRegistrationRepository();

        /// <summary>
        /// Get List of Bharun
        /// </summary>
        /// <returns></returns>
        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<BharunDto>))]
        [ProducesResponseType(400)]
        
        public IActionResult GetBharuns()
        {
            try
            {
                
                var objList = _brRepository.GetBharunDetails();
                var objDto = new List<BharunDto>();
                foreach (var obj in objList)
                {
                    objDto.Add(_mapper.Map<BharunDto>(obj));
                }
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "BhaRun GetBharuns", null);
                return Ok(ex.ToString());
            }
        }
        [Authorize]
        [HttpGet("{Bharunuid}", Name = "GetBharun")]
        [ProducesResponseType(200, Type = typeof(BharunDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetBharun(string Bharunuid)
        {
            try
            {

                var obj = _brRepository.GetBharunDetail(Bharunuid);

                if (obj == null)
                {
                    return NotFound();
                }
                var objDto = _mapper.Map<BharunDto>(obj);
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "BhaRun GetBharun", null);
                return Ok(ex.ToString());
            }
        }
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BharunDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateBharun([FromBody] BharunDto BharunDto)
        {
            try
            {
                if (BharunDto == null)
                {
                    return BadRequest(ModelState);
                }
                if (_brRepository.BharunExists(BharunDto.Uid))
                {
                    ModelState.AddModelError("", "Bharun Already Exist");
                    return StatusCode(400, ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var BharunObj = _mapper.Map<Bharun>(BharunDto);
               
                if (!_brRepository.CreateBharun(BharunObj))
                {
                    ModelState.AddModelError("", $"Something went wrong when saving the record {BharunObj.Name}");
                    return StatusCode(500, ModelState);

                }
                return CreatedAtRoute("GetBharun", new { Bharunuid = BharunObj.Uid }, BharunObj);

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "BhaRun CreateBharun", null);
                return Ok(ex.ToString());
            }
        }

        [Authorize]
        [HttpPatch("{uid}", Name = "UpdateBharun")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateBharun(string uid, [FromBody] BharunDto BharunDto)
        {
            try
            {
                if (BharunDto == null)
                {
                    return BadRequest(ModelState);
                }
                if (_brRepository.BharunExists(BharunDto.Uid))
                {
                    ModelState.AddModelError("", "Bharun Already Exist");
                    return StatusCode(400, ModelState);
                }

                var BharunObj = _mapper.Map<Bharun>(BharunDto);
                if (!_brRepository.UpdateBharun(BharunObj))
                {
                    ModelState.AddModelError("", $"Something went wrong when updating the record {BharunObj.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "BhaRun UpdateBharun", null);
                return Ok();
            }
        }
        [Authorize]
        [HttpDelete("{uid}", Name = "DeleteBharun")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteBharun(string uid)
        {
            try
            {
                if (!_brRepository.BharunExists(uid))
                {
                    return NotFound();
                }


                var BharunObj = _brRepository.GetBharunDetail(uid);
                if (!_brRepository.DeleteBharun(BharunObj))
                {
                    ModelState.AddModelError("", $"Something went wrong when Deleting the record {BharunObj.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "BhaRun DeleteBharun", null);
                return Ok();
            }
        }
    }
}