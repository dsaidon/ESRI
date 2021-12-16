using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Auth
{
    public interface IAuthContainerModel
    {


        //Define the data model that what we need for generating JWT
        //כשאתה בונה Token יש מספר דברים שחייבים להגיד
        //SecretKey , SecurityAlgorithm ו ExpireMinutes
        #region Members
        string SecretKey { get; set; }
        string SecurityAlgorithm { get; set; }
        int ExpireMinutes { get; set; }
        //איזה claims אתה נותן
        //ניקח את ה claims ומזה אני בונה subject 
        Claim[] Claims { get; set; }
        #endregion
        //PluralSight
        string Issuer { get; set; }
        string Audience { get; set; }
        int MinutesToExpiration { get; set; }
    }
}
