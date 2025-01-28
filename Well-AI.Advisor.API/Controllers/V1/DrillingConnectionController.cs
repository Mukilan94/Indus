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
    public class DrillingConnectionController : ControllerBase
    {
        private IDrillingConnectionRepository _drRepository;
        private readonly WebAIAdvisorContext _db;
        private readonly IMapper _mapper;
        public DrillingConnectionController(IConfiguration configuration,  IMapper mapper, IDrillingConnectionRepository drRepo, WebAIAdvisorContext db)
        {
            _drRepository = drRepo;
            _mapper = mapper;
            _db = db;
        }

        /// <summary>
        /// Insert DrillingConnection
        /// </summary>
        /// <returns></returns>


        [Authorize]
        [MapToApiVersion("1.0")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DrillingConnectionDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IActionResult CreateDrillingConnection([FromBody] List<DrillingConnectionDto> drillingConnectionDto)
        {
           
           
            try
            {
                foreach (var item in drillingConnectionDto)
                {

                    if (item == null)
                    {
                        return BadRequest(ModelState);
                    }
                   
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    var drillingConnectionObj = _mapper.Map<DrillingConnection>(item);
                   
                    if (!_drRepository.CreateDrillingConnection(drillingConnectionObj))
                    {
                        ModelState.AddModelError("", $"Something went wrong while saving the record {item.WELLID}");
                        return StatusCode(500, ModelState);

                    }
                }
                return Ok();

            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "DrillingConnection CreateDrillingConnection", null);
                ModelState.AddModelError("", $"Something went wrong while saving the record ");
                return StatusCode(500, ModelState);
            }
        }

       
    }
}