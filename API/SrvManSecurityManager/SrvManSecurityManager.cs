using API.Token;
using Core.Dtos.Login;
using Core.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.SecurityManager
{
    public class SrvManSecurityManager
    {
        private const int ecsSrvMan = 888;
        private const int ecsSupplierSrvMan = 999;
        private IAuthContainerModel _settings = null;
        private SQLContext _DbContext = null;
        private SrvManLoginDto _auth = null;
        private readonly IAuthService _tokenService ;

        public SrvManSecurityManager(SQLContext context, SrvManLoginDto auth, IAuthService tokenService, IAuthContainerModel settings)
        {
            _DbContext = context;
            _settings = settings;
            _auth = auth;
            _tokenService = tokenService;
        }
        public SrvManLoginDto ValidateSrvMan(int SrvManId)
        {

            List<SrvMan> list = new List<SrvMan>();

            try
            {
                list = _DbContext.SrvMan.Where(u => u.Id == SrvManId).ToList();

                if (list.Count() > 0)
                {
                    _auth = BuildUserAuthObject(list[0]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Exception while trying to retrieve user.", ex);
            }

            return _auth;
        }


        protected SrvManLoginDto BuildUserAuthObject(SrvMan srvman)
        {
            Type _authType = _auth.GetType();

            // Set User Properties
            _auth.UserId = srvman.Id;
            _auth.UserName = srvman.FirstNm.ToString() + " " + srvman.LastNm.ToString();
            _auth.PhoneNo = srvman.PhoneNo;
            _auth.SrvManTp = srvman.SrvManTpId;
            _auth.IsAuthenticated = true;
            if (srvman.SrvManTpId == 3)
            {
                _auth.IsSupplier = true;
            }


            // Get all claims for this user
            _auth.Claims = GetSrvManClaims(srvman.Id, _auth.IsSupplier);

            // Create JWT Bearer Token
            //_auth.BearerToken = _tokenService.GenerateToken(_auth.Claims, _auth.UserName);
            //_auth.BearerToken = _tokenService.CreateSrvManToken(_auth.Claims, _auth.UserName);
            _auth.BearerToken = _tokenService.GenerateToken(_settings, _auth);

            return _auth;
        }


        protected List<SrvManClaim> GetSrvManClaims(int userId, bool IsSupplier)
        {
            int SrvManClaimId = -1;
            if (IsSupplier == true)
            {
                SrvManClaimId = ecsSupplierSrvMan;
            }
            else
            {
                SrvManClaimId = ecsSrvMan; //Shagrir SrvMan
            }

            List<SrvManClaim> list = new List<SrvManClaim>();

            try
            {
                list = _DbContext.SrvManClaims.Where(u => u.UserId == SrvManClaimId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Exception trying to retrieve user claims.", ex);
            }

            return list;
        }

    }
}
