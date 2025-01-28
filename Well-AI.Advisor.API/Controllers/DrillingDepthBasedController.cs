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
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class DrillingDepthBasedController : ControllerBase
    {
        private IDrillingDepthBasedRepository _ddRepository;
        private readonly WebAIAdvisorContext _db;
        private readonly IMapper _mapper;
        public DrillingDepthBasedController(IConfiguration configuration,  IMapper mapper, IDrillingDepthBasedRepository ddRepo, WebAIAdvisorContext db)
        {
            _ddRepository = ddRepo;
            _mapper = mapper;
            _db = db;
        }

        /// <summary>
        /// Insert DrillingDepthBased
        /// </summary>
        /// <returns></returns>


        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DRILLINGDEPTHBASEDDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateDrillingDepthBased([FromBody] List<DRILLINGDEPTHBASEDDto> drillingDepthBasedDto)
        {
            try
            {
                foreach (var item in drillingDepthBasedDto)
                {
                    if (drillingDepthBasedDto == null)
                    {
                        return BadRequest(ModelState);
                    }
                    
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    var drillingDepthBasedObj = _mapper.Map<DRILLINGDEPTHBASED>(item);
                   
                    if (!_ddRepository.CreateDrillingDepthBased(drillingDepthBasedObj))
                    {
                        ModelState.AddModelError("", $"Something went wrong while saving the record {item.WELLID}");
                        return StatusCode(500, ModelState);

                    }
                    else
                    {
                         if (!_ddRepository.SaveWellDepthData(drillingDepthBasedObj))
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
                customErrorHandler.WriteError(ex, "DrillingDepthBased CreateDrillingDepthBased", null);
                ModelState.AddModelError("", $"Something went wrong while saving the record");
                return StatusCode(500, ModelState);
            }
        }

      
    }
}