using API.Errors;
using Core.Dtos.Login;
using Core.Interfaces.Auth;
using Infrastructure.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [EnableCors("CorsPolicy")] // Enable that policy to the current controller
    public class SrvManLoginController : BaseApiController
    {
        private readonly IConfiguration _configuration;
        private readonly JWTContainerModel _tokenService;
        private readonly SQLContext _DbContext;
        private readonly IAuthService _auth;
        private readonly ILoggerFactory _logger;
        private readonly ILoginSrv _loginSrv;
        public SrvManLoginController(SQLContext context, JWTContainerModel tokenService, IAuthService auth,ILoginSrv loginSrv, IConfiguration config, ILoggerFactory loggerFactory)
        {
            _configuration = config;
            _auth = auth;
            _loginSrv = loginSrv;
            _tokenService = tokenService;
            _DbContext = context;
            _logger = loggerFactory;
        }

        [HttpGet]
        public IActionResult AppIsRunning()
        {
            try
            {
                string UserInfo = "API V1.0.0.0 Is Up and running   ";
                return Ok(UserInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //[HttpGet("testAuth")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //public ActionResult<string> GetSecretText()
        //{
        //    return "secret stush";
        //}

        [HttpPost("LoginSrvMan")]
        public async Task<ActionResult<SrvManLoginDto>> LoginSrvMan([FromBody] SrvManLoginCDto data)
        {
            if (data == null)
            {
                return BadRequest(new ApiResponse(400,"Request body is incorrect (empty)."));
            }
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse(400, "Request body is not valis (empty)."));

                }
                Boolean retVal = false;
                IActionResult ret = null;
                SrvManLoginDto srvManLogin = new SrvManLoginDto();
                //mgr responsible for ValidateSrvMan and get SrvMan Claims
                //SrvManSecurityManager mgr = new SrvManSecurityManager(_DbContext, srvManLogin, _auth, _tokenService);
                ////cLogin responsible for the login process get last login info and send SMS
                
                //LoginSrv cLogin = new LoginSrv(_configuration, _DbContext);
                ////mgr --> Create JWT token form SrvMan
                srvManLogin = await (Task<SrvManLoginDto>)_loginSrv.ValidateSrvMan(data.UserId, srvManLogin, _auth, _tokenService);
                if (srvManLogin.IsAuthenticated)
                {
                    srvManLogin.IP = GetClientIP();
                    ////Write SrvMan Login Log and Define SrvMan Next Step
                    retVal = await _loginSrv.CheckAuthenticationDetails( srvManLogin);
                    ////return Ok(auth);
                   // ret = StatusCode(StatusCodes.Status200OK, srvManLogin);
                }
                else
                {
                    //ret = StatusCode(StatusCodes.Status404NotFound, "מספר עובד לא נמצא במערכת");
                    return NotFound(new ApiResponse(404, "מספר עובד לא נמצא במערכת"));
                }

                return await Task.FromResult(Ok(srvManLogin));

            }
            catch (Exception ex)
            {
                var logger = _logger.CreateLogger<SrvManLoginDto>();
                logger.LogError(ex.Message);
                //return StatusCode(StatusCodes.Status500InternalServerError, "השירות אינו זמין");
                return BadRequest(new ApiResponse(500, "השירות אינו זמין"));
            }

        }

        private string GetClientIP()
        {
            ////Old Code WCF
            //return !string.IsNullOrWhiteSpace(HttpContext.Current.Request.UserHostAddress) ? HttpContext.Current.Request.UserHostAddress :
            //       !string.IsNullOrWhiteSpace(Request.GetOwinContext().Request.RemoteIpAddress) ? Request.GetOwinContext().Request.RemoteIpAddress : "";
            //////////////////////////////////////////////////////////////
            //its better to get the ip address from the request header
            //RemoteIpAddress there is ipv4 and ipv6
            return HttpContext.Connection.RemoteIpAddress.ToString();
        }



    }
}
