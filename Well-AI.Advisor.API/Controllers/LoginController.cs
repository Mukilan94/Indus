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
using Well_AI.Advisor.API.Controllers;
using Microsoft.AspNetCore.Identity;
using WellAI.Advisor.Model.Identity;
using WellAI.Advisor.BLL.Business;

namespace WellAI.Advisor.API.Controllers
{
    [Authorize]
    ////[Route("api/[controller]")]
    [Route("api/[controller]")]
    [ApiController]    
    public class LoginController : ControllerBase
    {

        private ILoginRepository _loginRepository;
        private readonly IMapper _mapper;
        private readonly WebAIAdvisorContext _db;
        

        public LoginController(ILoginRepository lgRepo, IMapper mapper, WebAIAdvisorContext db)
        {
            _loginRepository = lgRepo;
            _mapper = mapper;
            _db = db;
        }
        WellAIAdvisiorRegistrationRepository wellAIRepository = new WellAIAdvisiorRegistrationRepository();

        //[MapToApiVersion("2.0")]
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(LoginReponse))]
        [ProducesResponseType(400)]

        public IActionResult Login(LoginRequest request)
        {
            try
            {
                var objResponse = _loginRepository.Login(request);                              

                var objResponseDto = new LoginResponseDto();
                objResponseDto = _mapper.Map<LoginResponseDto>(objResponse.Result);
                return Ok(objResponseDto);
            }
            catch (Exception ex)
            {
                CustomErrorHandlerForAdvisorAPI customErrorHandler = new CustomErrorHandlerForAdvisorAPI(_db);
                customErrorHandler.WriteError(ex, "Login API - Login Exception", null);
                return Ok(ex.ToString());
            }
        }
    }
}
