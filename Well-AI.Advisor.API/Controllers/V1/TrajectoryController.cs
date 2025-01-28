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
    public class TrajectoryController : ControllerBase
    {
        private ITrajectoryRepository _trjRepository;
        private readonly IMapper _mapper;
        private readonly WebAIAdvisorContext _db;
        public TrajectoryController(ITrajectoryRepository trjRepo, IMapper mapper, WebAIAdvisorContext db)
        {
            _trjRepository = trjRepo;
            _mapper = mapper;
            _db = db;
        }
        WellAIAdvisiorRegistrationRepository wellAIRepository = new WellAIAdvisiorRegistrationRepository();

        
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<TrajectoryDto>))]
        [ProducesResponseType(400)]

        public IActionResult GetTrajectorys()
        {
            try
            {

                var objList = _trjRepository.GetTrajectoryDetails();
                var objDto = new List<TrajectoryDto>();
                foreach (var obj in objList)
                {
                    objDto.Add(_mapper.Map<TrajectoryDto>(obj));
                }
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Trajectory GetTrajectorys", null);
                return Ok(ex.ToString());
            }
        }

        [HttpGet("{trjuid}", Name = "GetTrajectory")]
        [ProducesResponseType(200, Type = typeof(TrajectoryDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]

        public IActionResult GetTrajectory(string trjuid)
        {
            try
            {

                var obj = _trjRepository.GetTrajectoryDetail(trjuid);

                if (obj == null)
                {
                    return NotFound();
                }
                var objDto = _mapper.Map<TrajectoryDto>(obj);
               
                return Ok(objDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Trajectory GetTrajectory", null);
                return Ok(ex.ToString());
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TrajectoryDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateTrajectory([FromBody] TrajectoryDto lgDtos)
        {
            try
            {
                if (lgDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_trjRepository.TrajectoryExists(lgDtos.Uid))
                {
                    ModelState.AddModelError("", "Trajectory Already Exist");
                    return StatusCode(400, ModelState);
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var lgObj = _mapper.Map<Trajectory>(lgDtos);
                if (!_trjRepository.CreateTrajectory(lgObj))
                {
                    ModelState.AddModelError("", $"Something went wrong when saving the record {lgObj.Name}");
                    return StatusCode(500, ModelState);

                }
                return CreatedAtRoute("GetTrajectory", new { trjuid = lgObj.Uid }, lgObj);

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Trajectory CreateTrajectory", null);
                return Ok(ex.ToString());
            }
        }


        [HttpPatch("{uid}", Name = "UpdateTrajectory")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult UpdateTrajectory(string uid, [FromBody] TrajectoryDto lgDtos)
        {
            try
            {
                if (lgDtos == null)
                {
                    return BadRequest(ModelState);
                }
                if (_trjRepository.TrajectoryExists(lgDtos.Uid))
                {
                    ModelState.AddModelError("", "Trajectory Already Exist");
                    return StatusCode(400, ModelState);
                }

                var cementJobDto = _mapper.Map<Trajectory>(lgDtos);
                if (!_trjRepository.UpdateTrajectory(cementJobDto))
                {
                    ModelState.AddModelError("", $"Something went wrong when updating the record {lgDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Trajectory UpdateTrajectory", null);
                return Ok();
            }
        }

        [HttpDelete("{uid}", Name = "DeleteTrajectory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult DeleteTrajectory(string uid)
        {
            try
            {
                if (!_trjRepository.TrajectoryExists(uid))
                {
                    return NotFound();
                }


                var lgDtos = _trjRepository.GetTrajectoryDetail(uid);
                if (!_trjRepository.DeleteTrajectory(lgDtos))
                {
                    ModelState.AddModelError("", $"Something went wrong when Deleting the record {lgDtos.Name}");
                    return StatusCode(500, ModelState);

                }
                return NoContent();
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Trajectory DeleteTrajectory", null);
                return Ok();
            }
        }
    }
}
