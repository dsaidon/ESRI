using Core.Dtos.Login;
using Core.Entities;
using Core.Models.Auth;
using Infrastructure.Data;
using Infrastructure.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    //check if the token is valid at startup.cs
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SmsController :  BaseApiController
    {
        private readonly IConfiguration _configuration;
        private readonly SQLContext _DbContext;
        private readonly ILoggerFactory _logger;
        public SmsController(SQLContext context, ILoggerFactory loggerFactory, IConfiguration config)
        {
            _configuration = config;
            _DbContext = context;
            _logger = loggerFactory;
        }

        [HttpPost]
        [Route("SendSms")]
        
        public IActionResult SendSms([FromBody] SrvManLoginCDto data)
        {
            if (data == null)
            {
                return NotFound();
            }
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                    LoginSrv cLogin = new LoginSrv(_configuration, _DbContext);
                    SrvManLoginDto auth = new SrvManLoginDto();
                    LoginLog currentLogin;
                    if (data.IsAuthenticated)
                    {
                        auth.IP = GetClientIP();
                        auth.SessionId = data.SessionId;
                        auth.PhoneNo = data.PhoneNo;
                        auth.UserId = data.UserId;
                        auth.IsSupplier = data.IsSupplier;
                        AuthCode clsAuthCode = new AuthCode(data.UserId);
                        if (!String.IsNullOrEmpty(data.SessionId) && !String.IsNullOrEmpty(data.PhoneNo))
                            {
                                currentLogin = cLogin.CheckIfLoginExists( auth);
                                if (currentLogin != null)
                                {
                                    clsAuthCode = cLogin.SendSms( currentLogin);

                                    return Created("api/sms/", clsAuthCode);
                                }
                                else
                                {
                                    return StatusCode(StatusCodes.Status404NotFound, "Sms Error");
                                }
                            }
                            else
                            {
                                return StatusCode(StatusCodes.Status404NotFound, "Invalid User Name/Password.");
                            }
                        }
                        else
                        {
                            return StatusCode(StatusCodes.Status404NotFound, "Invalid User Name/Password.");
                        }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        private string GetClientIP()
        {
            //return !string.IsNullOrWhiteSpace(HttpContext.Current.Request.UserHostAddress) ? HttpContext.Current.Request.UserHostAddress :
            //       !string.IsNullOrWhiteSpace(Request.GetOwinContext().Request.RemoteIpAddress) ? Request.GetOwinContext().Request.RemoteIpAddress : "";
            return HttpContext.Connection.RemoteIpAddress.ToString();
        }

    }
}
