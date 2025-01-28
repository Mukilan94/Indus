using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Well_AI.Advisor.API.Dispatch.Models;
using Well_AI.Advisor.API.Dispatch.Repository.IRepository;
using WellAI.Advisor.API.Models.Dtos;
using WellAI.Advisor.DLL.Data;
using WellAI.Advisor.API.Dispatch.Controllers;

namespace Well_AI.Advisor.API.Dispatch.Controllers
{
     [Route("api/[controller]")]
  
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IWellSubscriptionRepository _subRepository;
        IHttpContextAccessor _httpContextAccessor;
        private readonly WebAIAdvisorContext _db;
        public AuthenticationController(IWellSubscriptionRepository subRepository,  IHttpContextAccessor httpContextAccessor, WebAIAdvisorContext db)
        {
            _subRepository = subRepository;
            _httpContextAccessor = httpContextAccessor;
            _db = db;
        }



        //[AllowAnonymous]
        //[HttpPost("DeviceRegistration")]

        //public IActionResult Register([FromBody] WellSubscriptionDto wellSubscriptionDto)
        //{
        //    try
        //    {
        //        string systemToken = _httpContextAccessor.HttpContext.Session.GetString("apiToken");
        //        if (systemToken == wellSubscriptionDto.RegistrationToken)
        //        {
        //            bool isCustomerUnique = _subRepository.IsUniqueWorkStation(wellSubscriptionDto.WorkstationIdentifier);
        //            if (!isCustomerUnique)
        //            {
        //                return BadRequest(new { message = "WorkstationId is already exists" });
        //            }
                   
        //            var subscription = _subRepository.Register(wellSubscriptionDto.CustomerAccountIdentifier, wellSubscriptionDto.WorkstationIdentifier);
        //            if (subscription == null)
        //            {
        //                return BadRequest(new { Message = "Error while registering" });
        //            }

        //            return Ok(subscription);
        //        }
        //        else
        //        {
        //            return BadRequest(new { Message = "Token is expire"});
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
        //        customErrorHandler.WriteError(ex, "WellSubscription Register", null);
        //        return BadRequest(new { Message = "Token is invalid" });
        //    }

        //}

        //[AllowAnonymous]
        //[HttpPost("authenticate")]
        //public IActionResult Authenticate([FromBody] WellSubscription model)
        //{

        //    try
        //    {
        //        var wellSubscription = _subRepository.Authenticate(model.CustomerAccountIdentifier, model.WorkstationIdentifier, model.ApiAccessKey, model.WorkstationToken);
        //        if (wellSubscription == null)
        //        {
        //            return Unauthorized();
        //        }

        //        return Ok(wellSubscription);
        //    }
        //    catch (Exception ex)
        //    {
        //        CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
        //        customErrorHandler.WriteError(ex, "WellSubscription Authenticate", null);
        //        return BadRequest(new { Message = "Error while Authenticate" });
        //    }
        //}


        [AllowAnonymous]
        [HttpPost("GenerateToken")]
        public IActionResult GenerateToken()
        {
            try
            {
                var ApiToken = _subRepository.GenerateToken();
            if (ApiToken == null)
            {
                return Unauthorized();
            }
            _httpContextAccessor.HttpContext.Session.SetString("apiToken", ApiToken["ApiToken"]);
            return Ok(ApiToken);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "WellSubscription GenerateToken", null);
                return BadRequest(new { Message = "Error while Authenticate" });
            }
        }
    }
}