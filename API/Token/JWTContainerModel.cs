using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace API.Token
{
    public class JWTContainerModel : IAuthContainerModel
    {
        //public string SecretKey { get; set; } = "TW9zaGVFcmV6UHJpdmF0ZUtleQ==";
        public string SecretKey { get; set; } = "This*Is&A!Long)Key(For%Creating@A$SymmetricKey";
        public string SecurityAlgorithm { get; set; } = SecurityAlgorithms.HmacSha256Signature;
        public int ExpireMinutes { get; set; } = 10000;
        public Claim[] Claims { get; set; }

        //PluralSight
        public string Issuer { get; set; } = "ShagrirIdentityProvider";
        public string Audience { get; set; } = "WebLetUsers";
        public int MinutesToExpiration { get; set; } = 10000;
    }
}
