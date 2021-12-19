using API.Errors;
using Core.Dtos.Login;
using Core.Entities;
using Core.Interfaces.Auth;
using Core.Models.Auth;
using Infrastructure.Data;
using Infrastructure.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ShagApi.Enums;
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
        private readonly ILoginSrv _loginSrv;
        public SmsController(SQLContext context, ILoginSrv loginSrv, ILoggerFactory loggerFactory, IConfiguration config)
        {
            _configuration = config;
            _DbContext = context;
            _logger = loggerFactory;
            _loginSrv = loginSrv;

        }

        [HttpPost]
        [Route("SendSms")]
        
        public async Task<IActionResult> SendSms([FromBody] SrvManLoginCDto data)
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
                    //LoginSrv cLogin = new LoginSrv(_configuration, _DbContext);
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
                                currentLogin = await _loginSrv.CheckIfLoginExists(auth);
                                if (currentLogin != null)
                                {
                                    clsAuthCode = await _loginSrv.SendSms( currentLogin);

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


        [HttpPost]
        [Route("CheckSmsAuthenticationCode")]
        public async Task<IActionResult> CheckSmsAuthenticationCode([FromBody] SmsAuth data) //

        {
            //bool blnShiftmentExists = false;
            if (data == null)
            {
                return NotFound();
            }
            try
            {
                if (!ModelState.IsValid)
                {
                   // return BadRequest(ModelState);
                    return BadRequest(new ApiResponse(400, "Request body is incorrect (empty)."));
                }
               
               
                    SmsCodeStatusType status;
                    AuthCode clsAuthCode = new AuthCode(data.srvManNo);
                    if (data.isAuthenticated)
                    {
                        clsAuthCode = await _loginSrv.CheckSmsAuthenticationCode( data);
                    }

                    if (clsAuthCode.SmsCodeStatusType == (int)SmsCodeStatusType.SentNewCode)
                    {
                        status = SmsCodeStatusType.SentNewCode;
                        return Created("api/login/", clsAuthCode);
                    }
                    else
                    {

                        //if (clsAuthCode.SmsCodeStatusType == (int)SmsCodeStatusType.AuthenticationPassed)
                        //{
                        //    //Get SrvMan EmpAttn Info
                        //    //IsSupplier
                        //    if (data.IsSupplier)
                        //    {
                        //        ShiftSrv shiftSrv = new ShiftSrv(configuration);
                        //        int intNextStep = 0;
                        //        clsAuthCode.isSupplier = true;
                        //        intNextStep = shiftSrv.SetSupplierShiftment(strConn, data.sessionId, (int)ShiftmentTp.enmStartShiftment, data.srvManNo);
                        //        if (intNextStep > 0)
                        //        {
                        //            clsAuthCode.isShiftStarted = true;
                        //            clsAuthCode.fleetCarNo = data.srvManNo;
                        //            clsAuthCode.isSupplier = true;
                        //            clsAuthCode.startShift = DateTime.Now;
                        //            clsAuthCode.Success = true;
                        //            clsAuthCode.nextStep = intNextStep;
                        //            clsAuthCode.isStartBreak = false;
                        //        }
                        //        //else
                        //        //{
                        //        //    clsAuthCode.Success = false;
                        //        //}
                        //    }
                        //    else
                        //    {
                        //        blnShiftmentExists = clsLog.GetSrvManLastShiftment(strConn, ref clsAuthCode);
                        //        //if (blnShiftmentExists == false)
                        //        //{
                        //        //    clsAuthCode.Success = false;
                        //        //}
                        //    }
                        //}

                        return Created("api/login/", clsAuthCode);
                    }
               

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "השירות אינו זמין");
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
