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

        Boolean CheckAuthenticationDetails(SrvManLoginDto data);
        LoginLog CheckIfLoginExists(SrvManLoginDto auth);
        AuthCode SendSms(LoginLog data);
        AuthCode CreateNewAuthenticationCode(LoginLog currentLogin);
        string GenerateSessionGuid();
    }
}
