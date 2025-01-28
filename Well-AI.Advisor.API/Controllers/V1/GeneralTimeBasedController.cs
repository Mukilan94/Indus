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
using WellAI.Advisor.API.Models.Dtos;
using WellAI.Advisor.DLL.Data;

namespace Well_AI.Advisor.API.Controllers
{
    [Authorize]
    //[Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class GeneralTimeBasedController : ControllerBase
    {
        private IGeneralTimeBasedRepository _gtRepository;
        private readonly WebAIAdvisorContext _db;
        private readonly IMapper _mapper;
        public GeneralTimeBasedController(IConfiguration configuration,  IMapper mapper, IGeneralTimeBasedRepository gtRepo, WebAIAdvisorContext db)
        {
            _gtRepository = gtRepo;
            _mapper = mapper;
            _db = db;
        }

        /// <summary>
        /// Insert GeneralTimeBased
        /// </summary>
        /// <returns></returns>



        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GeneralTimeBasedDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateGeneralTimeBased([FromBody] List<GeneralTimeBasedDto> generalTimeBasedDto)
        {
            try
            {
                foreach (var item in generalTimeBasedDto)
                {
                    if (generalTimeBasedDto == null)
                    {
                        return BadRequest(ModelState);
                    }
                    
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    var generalTimeBasedObj = _mapper.Map<GeneralTimeBased>(item);
                   
                    if (!_gtRepository.CreateGeneralTimeBased(generalTimeBasedObj))
                    {
                        ModelState.AddModelError("", $"Something went wrong while saving the record {item.WELLID}");
                        return StatusCode(500, ModelState);

                    }
                    else
                    {
                        if (!_gtRepository.SaveWellDepthData(generalTimeBasedObj))
                        {
                            ModelState.AddModelError("", $"Something went wrong while saving the record {item.WELLID}");
                            return StatusCode(500, ModelState);
                        }
                    }
                }
                return Ok();

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "GeneralTimeBased CreateGeneralTimeBased", null);
                ModelState.AddModelError("", $"Something went wrong while saving the record ");
                return StatusCode(500, ModelState);
            }
        }

        
    }
}