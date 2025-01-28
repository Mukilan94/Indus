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
    public class WellBoreController : ControllerBase
    {
        private IWellBoreRepository _wlbRepository;
        private readonly IMapper _mapper;
        private readonly WebAIAdvisorContext _db;
        public WellBoreController(IWellBoreRepository wlbRepo, IMapper mapper, WebAIAdvisorContext db)
        {
            _wlbRepository = wlbRepo;
            _mapper = mapper;
            _db = db;
        }
        WellAIAdvisiorRegistrationRepository wellAIRepository = new WellAIAdvisiorRegistrationRepository();

        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<WellBoreDto>))]
        [ProducesResponseType(400)]

        public IActionResult GetWellBores()
        {
            try
            {

                var objList = _wlbRepository.GetWellBoreDetails();
                var objDto = new List<WellBoreDto>();
                foreach (var obj in objList)
                {
                    objDto.Add(_mapper.Map<WellBoreDto>(obj));
                }
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "WellBore GetWellBores", null);
                return Ok(ex.ToString());
            }
        }

        [HttpGet("{wlbuid}", Name = "GetWellBore")]
        [ProducesResponseType(200, Type = typeof(WellBoreDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetWellBore(string wlbuid)
        {
            try
            {

                var obj = _wlbRepository.GetWellBoreDetail(wlbuid);

                if (obj == null)
                {
                    return NotFound();
                }
                var objDto = _mapper.Map<WellBoreDto>(obj);
                
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "WellBore GetWellBore", null);
                return Ok(ex.ToString());
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(WellBoreDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateWellBore([FromBody] WellBoreDto lgDtos)
        {
            try
            {
                if (lgDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_wlbRepository.WellBoreExists(lgDtos.Uid))
                {
                    ModelState.AddModelError("", "WellBore Already Exist");
                    return StatusCode(400, ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var lgObj = _mapper.Map<WellBore>(lgDtos);
                if (!_wlbRepository.CreateWellBore(lgObj))
                {
                    ModelState.AddModelError("", $"Something went wrong when saving the record {lgObj.Name}");
                    return StatusCode(500, ModelState);

                }
                return CreatedAtRoute("GetWellBore", new { wlbuid = lgObj.Uid }, lgObj);

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "WellBore CreateWellBore", null);
                return Ok(ex.ToString());
            }
        }


        [HttpPatch("{uid}", Name = "UpdateWellBore")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateWellBore(string uid, [FromBody] WellBoreDto lgDtos)
        {
            try
            {
                if (lgDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_wlbRepository.WellBoreExists(lgDtos.Uid))
                {
                    ModelState.AddModelError("", "WellBore Already Exist");
                    return StatusCode(400, ModelState);
                }

                var cementJobDto = _mapper.Map<WellBore>(lgDtos);
                if (!_wlbRepository.UpdateWellBore(cementJobDto))
                {
                    ModelState.AddModelError("", $"Something went wrong when updating the record {lgDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "WellBore UpdateWellBore", null);
                return Ok();
            }
        }

        [HttpDelete("{uid}", Name = "DeleteWellBore")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteWellBore(string uid)
        {
            try
            {
                if (!_wlbRepository.WellBoreExists(uid))
                {
                    return NotFound();
                }


                var lgDtos = _wlbRepository.GetWellBoreDetail(uid);
                if (!_wlbRepository.DeleteWellBore(lgDtos))
                {
                    ModelState.AddModelError("", $"Something went wrong when Deleting the record {lgDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "WellBore DeleteWellBore", null);
                return Ok();
            }
        }
    }
}
