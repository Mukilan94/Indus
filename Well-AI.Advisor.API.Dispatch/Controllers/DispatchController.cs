using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.Http;
using WellAI.Advisor.API.Dispatch.Repository.IRepository;
using WellAI.Advisor.DLL.Data;
using Microsoft.AspNetCore.Authorization;
using WellAI.Advisor.API.Models.Dtos;
using WellAI.Advisor.API.Dispatch.Models;
using WellAI.Advisor.Model.ServiceCompany.Models;

namespace WellAI.Advisor.API.Dispatch.Controllers
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

        public DispatchController(IDispatchRepository lgRepo,  IMapper mapper, WebAIAdvisorContext db)
        {
            _lDispatchRepository = lgRepo;
            _mapper = mapper;
            _db = db;
        }
        ////[MapToApiVersion("2.0")]
        //[HttpPost("DispatchRoutes")]
        //[ProducesResponseType(200, Type = typeof(DispatchRoutesResponse))]
        //[ProducesResponseType(400)]
        //public IActionResult GetDispatchRoutes(DispatchRoutesRequest request)
        //{
        //    try
        //    {
        //        var objResponse = _lDispatchRepository.GetDispatchRoutes(request);

        //        var objResponseDto = new DispatchRoutesResponeDto();                
        //        objResponseDto = _mapper.Map<DispatchRoutesResponeDto>(objResponse.Result);                
        //        return Ok(objResponseDto);
        //    }
        //    catch (Exception ex)
        //    {
        //        CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
        //        customErrorHandler.WriteError(ex, "Dispatch API - GetDispatchRoutes Exception", null);
        //        return Ok(ex.ToString());
        //    }
        //}

        [HttpPost("RouteAcceptStatus")]
        [ProducesResponseType(200, Type = typeof(RouteAcceptedResponse))]
        [ProducesResponseType(400)]

        public IActionResult RouteAccepted(RouteAcceptedRequest request)
        {
            //RouteAcceptedResponse dispatchReponse = new RouteAcceptedResponse();
            try
            {
                var objResponse = _lDispatchRepository.RouteAccepted(request);

                var objResponseDto = new RouteAcceptedResponseDto();
                objResponseDto = _mapper.Map<RouteAcceptedResponseDto>(objResponse.Result);

                return Ok(objResponseDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "RouteAccepted API - RouteAccepted Exception", null);
                return Ok(ex.ToString());

            }
        }


        [HttpPost("RecordDestinationChanges")]
        [ProducesResponseType(200, Type = typeof(RecordDestinationChangesResponse))]
        [ProducesResponseType(400)]
        public IActionResult RecordDestinationChanges(RecordDestinationChangesRequest request)
        {
            //RouteAcceptedResponse dispatchReponse = new RouteAcceptedResponse();
            try
            {
                var objResponse = _lDispatchRepository.RecordDestinationChanges(request);

                var objResponseDto = new RecordDestinationChangesResponseDto();
                objResponseDto = _mapper.Map<RecordDestinationChangesResponseDto>(objResponse.Result);

                return Ok(objResponseDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "RecordDestinationChanges API - Exception", null);
                return Ok(ex.ToString());

            }
        }
    }
}
