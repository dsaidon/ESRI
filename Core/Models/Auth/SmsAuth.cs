using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models.Auth
{
    public class SmsAuth
    {

        public SmsAuth() : base()
        {
            smsCode = "";
            srvManNo = -1;
            isAuthenticated = false;
            sessionId = "";
            bearerToken = "";
            IsSupplier = false;
        }


        [Required(ErrorMessage = "Missing price")]
        public int srvManNo { get; set; }
        [Required()]
        public string smsCode { get; set; }

        public bool IsSupplier { get; set; }
        public bool isAuthenticated { get; set; }
        [Required()]
        public string sessionId { get; set; }

        [Required()]
        public string bearerToken { get; set; }
        
    }
}
