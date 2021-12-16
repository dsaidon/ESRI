using Core.Dtos.Login;
using Core.Interfaces.Auth;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Auth
{
    public class JWTService : IAuthService
    {

        public string SecretKey { get; set; }
        public JWTService(string secretKey)
        {
            SecretKey = secretKey;
        }
        public string GenerateToken(IAuthContainerModel model, SrvManLoginDto authEmp)
        {

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(model.SecretKey));
            // Create standard JWT claims
            List<Claim> jwtClaims = new List<Claim>();
            jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            // Add custom claims 
            //###/##/ use canAccessSupplier just like it define in angular dont use CanAccessSupplier
            //jwtClaims.Add(new Claim("isAuthenticated",authEmp.IsAuthenticated.ToString().ToLower()));
            foreach (var claim in authEmp.Claims)
            {
                jwtClaims.Add(new Claim(claim.ClaimType, claim.ClaimValue));
            }
            if (model == null)
                throw new ArgumentException("Arguments to create token are not valid.");

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(jwtClaims),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(model.ExpireMinutes)),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
                Issuer = model.Issuer,//  "ShagrirIdentityProvider",
                Audience = model.Audience,// "InventoryAPI",

            };

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            string token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return token;
        }

        public string GenerateUserToken(IAuthContainerModel model, SrvManLoginDto authUser)
        {


            // Create standard JWT claims
            List<Claim> jwtClaims = new List<Claim>();
            jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, authUser.UserName));
            jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            // Add custom claims 
            //###/##/ use canAccessSupplier just like it define in angular dont use CanAccessSupplier
            jwtClaims.Add(new Claim("isAuthenticated", authUser.IsAuthenticated.ToString().ToLower()));
            foreach (var claim in authUser.Claims)
            {
                jwtClaims.Add(new Claim(claim.ClaimType, claim.ClaimValue));
            }
            if (model == null)
                throw new ArgumentException("Arguments to create token are not valid.");

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(jwtClaims),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(model.ExpireMinutes)),
                SigningCredentials = new SigningCredentials(GetSymmetricSecurityKey(), model.SecurityAlgorithm),
                Issuer = model.Issuer,//  "ShagrirIdentityProvider",
                Audience = model.Audience,// "InventoryAPI",

            };

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            string token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return token;
        }

        public IEnumerable<Claim> GetTokenClaims(string token)
        {
            throw new NotImplementedException();
        }

        //check the token signature
        public bool IsTokenValid(string token, IAuthContainerModel settings)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Given token is null or empty.");

            TokenValidationParameters tokenValidationParameters = GetTokenValidationParameters(settings);
            JwtSecurityToken jwt = new JwtSecurityToken();

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            try
            {
                //The porpose is to check the token signature not the token data (subject)
                ClaimsPrincipal tokenValid = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public TokenValidationParameters GetTokenValidationParameters(IAuthContainerModel settings)
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = settings.Issuer, // "ShagrirIdentityProvider",
                ValidateAudience = true,
                ValidAudience = settings.Audience, //"InventoryAPI",
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.FromMinutes(settings.MinutesToExpiration),
                IssuerSigningKey = GetSymmetricSecurityKey()
            };
        }
        private SecurityKey GetSymmetricSecurityKey()
        {
            byte[] symmetricKey = Convert.FromBase64String(SecretKey);
            return new SymmetricSecurityKey(symmetricKey);
        }

    }
}
