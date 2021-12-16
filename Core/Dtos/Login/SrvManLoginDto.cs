using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Dtos.Login
{
    public class SrvManLoginDto
    {
        public SrvManLoginDto() : base()
        {
            IP = "";
            UserId = -1;
            SrvManTp = -1;
            IsSupplier = false;
            UserName = "Not authorized";
            Password = "";
            Email = "";
            PhoneNo = "";
            SessionId = "";
            BearerToken = string.Empty;
            IsAuthenticated = false;
            Status = 0;
            //Init Claims list to empty list
            List<SrvManClaim> Claims = new List<SrvManClaim>();
        }

        public int UserId { get; set; } //srvManNo
        public string UserName { get; set; } //srvManName
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string IP { get; set; }
        public int Status { get; set; }
        public int SrvManTp { get; set; }
        public bool IsSupplier { get; set; }
        public string BearerToken { get; set; }
        public bool IsAuthenticated { get; set; }
        public List<SrvManClaim> Claims { get; set; }
        public string SessionId { get; set; }

    }
}
