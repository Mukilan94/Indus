using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.Http;
using Well_AI.Advisor.API.Repository;
using WellAI.Advisor.API.Repository.IRepository;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.API.Models;
using Microsoft.AspNetCore.Authorization;
using Well_AI.Advisor.API.Controllers;
using WellAI.Advisor.API.Models.Dtos;


namespace WellAI.Advisor.API.Controllers
{
    [Authorize]
    ////[Route("api/[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    public class DispatchController : ControllerBase
    {
        private IDispatchRepository _lDispatchRepository;
        private readonly IMapper _mapper;
        private readonly WebAIAdvisorContext _db;

        public DispatchController(IDispatchRepository lgRepo, IMapper mapper, WebAIAdvisorContext db)
        {
            _lDispatchRepository = lgRepo;
            _mapper = mapper;
            _db = db;
        }
        //[MapToApiVersion("2.0")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(DispatchRoutesResponse))]
        [ProducesResponseType(400)]

        public IActionResult GetDispatchRoutes(DispatchRoutesRequest request)
        {
            try
            {
                var objResponse = _lDispatchRepository.GetDispatchRoutes(request);

                var objResponseDto = new DispatchRoutesResponeDto();
                objResponseDto = _mapper.Map<DispatchRoutesResponeDto>(objResponse.Result);
                return Ok(objResponseDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Dispatch API - GetDispatchRoutes Exception", null);
                return Ok(ex.ToString());
            }
        }
    }
}
