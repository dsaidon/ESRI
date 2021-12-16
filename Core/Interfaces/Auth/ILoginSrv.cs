using Core.Dtos.Login;
using Core.Entities;
using Core.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Auth
{
   public interface ILoginSrv
    {

        Task<Boolean> CheckAuthenticationDetails(SrvManLoginDto data);
        Task<LoginLog> CheckIfLoginExists(SrvManLoginDto auth);
        Task<AuthCode> SendSms(LoginLog data);
        Task<AuthCode> CreateNewAuthenticationCode(LoginLog currentLogin);
        string GenerateSessionGuid();

        Task<SrvManLoginDto> ValidateSrvMan(int SrvManId, SrvManLoginDto srvManLoginDto, IAuthService _tokenService, JWTContainerModel JWTSetting);
        //SrvManLoginDto BuildUserAuthObject(SrvMan srvman);
       // List<SrvManClaim> GetSrvManClaims(int userId, bool IsSupplier);
    }
}
