using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Dtos.Login
{
    public class SrvManLoginCDto
    {

        public SrvManLoginCDto() : base()
        {
            IP = "";
            UserId = 0;
            UserName = "";
            FleetCarNo = "";
            IsSupplier = false;
            PhoneNo = "";
            BearerToken = "";
            IsAuthenticated = false;
            SessionId = "";
            SrvManTp = -1;
            Status = 0;
            Password = "";
            Email = "";
            PhoneNo = "";
        }


        [Required(ErrorMessage = "Missing Srv Man No")]
        [Key()]
        public int UserId { get; set; } //srvManNo
        public string UserName { get; set; } //srvManName
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string IP { get; set; }
        public string FleetCarNo { get; set; }
        public bool IsSupplier { get; set; }
        public bool IsAuthenticated { get; set; }
        public string SessionId { get; set; }
        public string BearerToken { get; set; }
        public int Status { get; set; }
        public int SrvManTp { get; set; }

    }
}
