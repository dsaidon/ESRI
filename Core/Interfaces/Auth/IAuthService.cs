using Core.Dtos.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Auth
{
    public interface IAuthService
    {
        string GenerateToken(IAuthContainerModel model, SrvManLoginDto authEmp);
        string GenerateUserToken(IAuthContainerModel model, SrvManLoginDto authUser);
        bool IsTokenValid(string token, IAuthContainerModel settings);
        IEnumerable<Claim> GetTokenClaims(string token);


    }
}
