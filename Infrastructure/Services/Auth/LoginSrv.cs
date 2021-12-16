using Core.Dtos.Login;
using Core.Entities;
using Core.Enums;
using Core.Models.Auth;
using Infrastructure.Data;
using Infrastructure.Utils;
using Microsoft.Extensions.Configuration;
using ShagApi.Enums;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace Infrastructure.Services.Auth
{
    public class LoginSrv
    {
        private readonly IConfiguration _configuration;
        private readonly SQLContext _DbContext;
        public LoginSrv(IConfiguration configuration, SQLContext context)
        {
            _DbContext = context;
            _configuration = configuration;
        }
        public Boolean CheckAuthenticationDetails(SrvManLoginDto data)
        {
            try
            {
                //using (var db = new SQLContext())
                //{
                    LoginLog currentLogin;
                    //clsBLogin objLogin = new clsBLogin();
                    currentLogin = GetCurrentLoginRecord(data);
                    if (!string.IsNullOrWhiteSpace(data.IP) && !ValidateLoginAttemptsCount(data.IP))
                    {
                        LockSrvMan(currentLogin);
                        currentLogin.lastUpdateDate = DateTime.Now;
                        data.Status = (int)AuthenticationStatusType.LockedUser;
                    //objLogin.UpdateLoginTbl(strConn, (int)AuthenticationStatusType.LockedUser, data.SessionId);
                    _DbContext.SaveChanges();
                        
                        return false;
                    }
                    //Check if user has validity cover id in Shagrir
                    if (data.UserId <= 0)
                    {
                        currentLogin.lastUpdateDate = DateTime.Now;
                        data.Status = (int)AuthenticationStatusType.UserNotSubsecribed;
                    //objLogin.UpdateLoginTbl(strConn, (int)AuthenticationStatusType.UserNotSubsecribed, data.SessionId);
                    _DbContext.SaveChanges();
                        return false;
                    }
                    //Check if locked user
                    if (IsLocked( data))
                    {
                        currentLogin.lastUpdateDate = DateTime.Now;
                        data.Status = (int)AuthenticationStatusType.LockedUser;
                    _DbContext.SaveChanges();
                        //objLogin.UpdateLoginTbl(strConn, (int)AuthenticationStatusType.LockedUser, data.SessionId);
                        return false;
                    }

                    return true;
                //}
            }
            catch (Exception ex)
            {

                return false;

            }
        }
        //public AuthCode CheckAuthenticationCode( SmsAuth data)
        //{
        //    AuthCode clsAuthCode = new AuthCode(data.srvManNo);
        //    try
        //    {
        //        //LoginLogs currentLogin;
        //        //clsBLogin objLogin = new clsBLogin();
        //        //clsUtil ObjUtil = new clsUtil();
        //        Boolean retVal = false;
        //        int iSmsExperationInterval = 0;
        //        DateTime dtExperationDate;
        //        clsAuthCode.nextStep = 0;
        //        if (string.IsNullOrEmpty(data.smsCode))
        //        {
        //            clsAuthCode.SmsCodeStatusType = (int)SmsCodeStatusType.EmptySmsCode;
        //            return clsAuthCode;
        //        }
        //        SiteParams wSiteParams = new SiteParams(_DbContext);

                
        //        currentLogin = GetLoginRecord(strConn, data.sessionId);

        //        if (currentLogin.ErrID)
        //        {
        //            clsAuthCode.SmsCodeStatusType = (int)SmsCodeStatusType.LoginNotFound;
        //            return clsAuthCode;
        //        }
        //        else
        //        {
        //            iSmsExperationInterval = wSiteParams.GetSiteParamInt((int)WebSiteParams.SmsExperationInterval, 360);
        //            if (iSmsExperationInterval == 0) iSmsExperationInterval = 2;
        //            dtExperationDate = DateTime.Now.AddMinutes(iSmsExperationInterval * -1);

        //            if (currentLogin.smsCreationDate < dtExperationDate)
        //            {
        //                //logger.Debug(string.Format("CheckAuthenticationCode: CodeExpired {{ sessionId: {0}, authenticationCode: {1}, lastUpdateDate: {2} }}", sessionId, authenticationCode, loginLog.lastUpdateDate));
        //                clsAuthCode.SmsCodeStatusType = (int)SmsCodeStatusType.CodeExpired;
        //                return clsAuthCode;
        //            }


        //            currentLogin.smsCodeRetriesCounter = currentLogin.smsCodeRetriesCounter == 0 ? 1 : currentLogin.smsCodeRetriesCounter + 1;//increace smsCodeRetriesCounter
        //            if (currentLogin.smsCode != data.smsCode)
        //            {
        //                objLogin.UpdateLoginTblSmsTry(strConn, currentLogin.smsCodeRetriesCounter, data.sessionId);
        //                if (currentLogin.smsCodeRetriesCounter < 3)
        //                {
        //                    clsAuthCode.SmsCodeStatusType = (int)SmsCodeStatusType.WrongCode;
        //                    return clsAuthCode;
        //                }
        //                else
        //                {

        //                    clsAuthCode = CreateNewAuthenticationCode(strConn, currentLogin);
        //                    clsAuthCode.SmsCodeStatusType = (int)SmsCodeStatusType.ReSendNewSmsCode;
        //                    return clsAuthCode;
        //                }
        //            }


        //            //logger.Debug(string.Format("CheckAuthenticationCode: passed {{ sessionId: {0}, authenticationCode: {1} }}", sessionId, authenticationCode));
        //            //objLogin.UpdateLoginTbl(strConn, (int)AuthenticationStatusType.AuthenticationPassed, data.SessionId);
        //            retVal = objLogin.CheckSmsCode(strConn, data.sessionId, data.smsCode);


        //            iSmsExperationInterval = wSiteParams.GetSiteParamInt((int)WebSiteParams.SmsExperationInterval, 360);
        //            DateTime experationDate = DateTime.Now.AddMinutes(iSmsExperationInterval * -1);


        //            objLogin.UpdateLoginTbl(strConn, (int)AuthenticationStatusType.AuthenticationPassed, data.sessionId);
        //            clsAuthCode.SmsCodeStatusType = (int)SmsCodeStatusType.AuthenticationPassed;
        //            clsAuthCode.nextStep = BindLoginToProcess(strConn, currentLogin, ProcessStepType.Login, 1);
        //            return clsAuthCode;

        //        }

        //        //return SmsCodeStatusType.NotOnSmsCodeStatus;


        //    }

        //    catch (Exception ex)
        //    {
        //        clsAuthCode.SmsCodeStatusType = (int)SmsCodeStatusType.NotOnSmsCodeStatus;
        //        return clsAuthCode;
        //    }
        //}
        
        public LoginLog CheckIfLoginExists(SrvManLoginDto auth )

        {
            LoginLog currentLogin;
            currentLogin = GetCurrentLoginRecord(auth);
            return currentLogin;
        }

        //public LoginLogs GetLoginRecord( string sessionId)
        //{
        //    LoginLogs currentLogin;
        //    clsBLogin objLogin = new clsBLogin();


        //    currentLogin = objLogin.GetLoginRecord(strConn, sessionId);



        //    return currentLogin;
        //}
        public AuthCode SendSms( LoginLog data)
        {



            return CreateNewAuthenticationCode( data);




            //return true;
        }
        public AuthCode CreateNewAuthenticationCode( LoginLog currentLogin)//, string licenseNumber)
        {
            try
            {
                //logger.Debug(string.Format("start CreateNewAuthenticationCode {{sessionId: {0} }}", sessionId));
                SmsCodeStatusType status;
                //LoginLog currentLogin;
                //clsBLogin objLogin = new clsBLogin();
                //string code = string.Empty;
                Boolean retVal = false;
                int nextStep = -1;
                int iMaxRetries = 0;
                int iSmsAuthenticationCodeLength = 0;
                string sTestUserAuthCode = "";
                AuthCode clsAuthCode = new AuthCode(currentLogin.SrvManNo);
                clsAuthCode.nextStep = 0;

                if ( currentLogin.status != (int)AuthenticationStatusType.AuthenticationCreateLoginRow)
                {
                    //logger.Debug(string.Format("End CreateNewAuthenticationCode with SmsCodeStatusType.NotOnSmsCodeStatus {{sessionId: {0} }}", sessionId));
                    clsAuthCode.SmsCodeStatusType = (int)SmsCodeStatusType.NotOnSmsCodeStatus;
                    return clsAuthCode;
                }
                SiteParams wSiteParams = new SiteParams(_DbContext);

                 iMaxRetries = wSiteParams.GetSiteParamInt((int)WebSiteParams.MaxRetries, 5);
                if (currentLogin.smsCodeCounter >= iMaxRetries)
                {

                    currentLogin.status = (int)AuthenticationStatusType.LockedUser;
                    LockSrvMan(currentLogin);
                    status = SmsCodeStatusType.OverMaxRetries;
                }

                else
                {
                    //Valid user => send SMS
                    string code = string.Empty;
                    bool isTestSrvMan = IsTestUser( currentLogin);

                    if (isTestSrvMan || 1 == 1)
                    {
                        //return constant SMS No = 1234
                        sTestUserAuthCode = wSiteParams.GetSiteParamString((int)WebSiteParams.TestUserAuthCode, "1234");
                        code = sTestUserAuthCode;
                    }
                    else
                    {
                        iSmsAuthenticationCodeLength = wSiteParams.GetSiteParamInt((int)WebSiteParams.SmsAuthenticationCodeLength, 4);
                        code = RandomExtentions.CreateString(iSmsAuthenticationCodeLength);
                    }

                    currentLogin.smsCode = code;
                    currentLogin.smsCodeCounter = currentLogin.smsCodeCounter == 0 ? 1 : (int)currentLogin.smsCodeCounter + 1;
                    currentLogin.smsCodeRetriesCounter = 0;
                    currentLogin.smsCreationDate = DateTime.Now;
                    string strSmsMessageParam = wSiteParams.GetSiteParamString((int)WebSiteParams.SmsAuthenticationMessage, "");
                    string strMessage = strSmsMessageParam != null ? strSmsMessageParam.Replace("@name@", currentLogin.SrvManName).Replace("@code@", code) : string.Format("שלום {0}\n לפניך קוד אימות עבור כניסה למערכת שגריר, אנא הזן את הקוד והמשך בתהליך \n קוד האימות: {1}", currentLogin.SrvManName, code);

                    if (isTestSrvMan || 1 == 1)
                    {
                        status = SmsCodeStatusType.SentNewCode;
                    }
                    else
                    {
                        currentLogin.phoneNo = "0528320360";
                        if (SendCodeSms(currentLogin.phoneNo, strMessage) == 1)
                            status = SmsCodeStatusType.SentNewCode;
                        else
                            status = SmsCodeStatusType.ErrorOnSendingCodeSms;
                    }

                }
                currentLogin.lastUpdateDate = DateTime.Now;
                _DbContext.SaveChanges();
                //retVal = objLogin.UpdateLoginTblWithSmsInfo(strConn, currentLogin);

                // nextStep = BindLoginToProcess(strConn, currentLogin, (int)ProcessStepType.None, 1);
                nextStep = -1; //Login no need to create process row at first login only after Sms Code
                clsAuthCode.SmsCodeStatusType = (int)status;
                clsAuthCode.nextStep = nextStep;


                return clsAuthCode;
            }
            catch (Exception ex)
            {
                //logger.Error(string.Format("An error occurred on CreateNewAuthenticationCode {0}", JsonConvert.SerializeObject(ex)));
                throw ex;
            }


        }
        //Check if we deal with testing app data
        private bool IsTestUser( LoginLog loginLog)
        {
            
            int iTestSrvManNumber = 0;
            string sTestUserPhone = "";
            //logger.Debug($"IsTestUser Validation: Paremeters send: licenseNum: {loginLog.licenseNum}, phone: {loginLog.phoneNum},name: {loginLog.SbcDetail.Sbc_name } ");
            bool res = true;
            SiteParams wSiteParams = new SiteParams(_DbContext);
            iTestSrvManNumber = wSiteParams.GetSiteParamInt((int)WebSiteParams.TestLicenseNumber, 18);
            if (loginLog.SrvManNo != iTestSrvManNumber)
                return false;
            sTestUserPhone = wSiteParams.GetSiteParamString((int)WebSiteParams.TestUserPhone, "0528320360");
            if (loginLog.phoneNo != sTestUserPhone && loginLog.SrvManNo != iTestSrvManNumber)
                return false;
            return res;
        }
        //public int BindLoginToProcess(string strConn, LoginLogs loginLog, ProcessStepType stepId, int procSts)
        //{
        //    //logger.Debug(string.Format("BindLoginToProcess start {{ loginLog:{0}, sbcVld:{1} }}", loginLog.loginId, sbcVld));
        //    clsBLogin objLogin = new clsBLogin();
        //    //string code = string.Empty;
        //    Boolean retVal = false;
        //    long lngProccessId = 0;
        //    ProcessStepType intStep = ProcessStepType.Login;
        //    int intCurrentStep = -1;

        //    lngProccessId = objLogin.GetProccessId(strConn, loginLog.loginId);
        //    intCurrentStep = objLogin.GetProccessCurrentStep(strConn, loginLog.loginId);
        //    if (intCurrentStep == (int)ProcessStepType.Login || intCurrentStep == 0)
        //    {
        //        //(int)ProcessStepType.None
        //        intStep = ProcessStepType.Emp_attn;
        //        intCurrentStep = (int)intStep;
        //        retVal = objLogin.CreateProccessId(strConn, loginLog, intCurrentStep);
        //    }
        //    else
        //    {
        //        if (stepId != ProcessStepType.None && stepId != ProcessStepType.Login)
        //        {
        //            intStep = stepId;
        //        }
        //        retVal = objLogin.UpdateProccessStepId(strConn, lngProccessId, (int)intStep, procSts);
        //        switch (stepId)
        //        {
        //            case ProcessStepType.Login:
        //                {
        //                    intStep = ProcessStepType.Emp_attn;
        //                    break;
        //                }
        //            case ProcessStepType.Emp_attn:
        //                {
        //                    intStep = ProcessStepType.srvManCallsList;
        //                    break;
        //                }
        //            case ProcessStepType.Emp_attn_Start_Break:
        //                {
        //                    intStep = ProcessStepType.Emp_attn_Start_Break;
        //                    break;
        //                }
        //            case ProcessStepType.srvManCallsList:
        //                {
        //                    intStep = ProcessStepType.onMyWay;
        //                    break;
        //                }
        //            case ProcessStepType.onMyWay:
        //                {
        //                    intStep = ProcessStepType.arrived;
        //                    break;
        //                }
        //            case ProcessStepType.arrived:
        //                {
        //                    intStep = ProcessStepType.insertRoadCallData;
        //                    break;
        //                }
        //            case ProcessStepType.insertRoadCallData:
        //                {
        //                    intStep = ProcessStepType.closing_Call;
        //                    break;
        //                }
        //            case ProcessStepType.closing_Call:
        //                {
        //                    intStep = ProcessStepType.srvManCallsList;
        //                    break;
        //                }
        //            default:
        //                {
        //                    string msg = string.Format("BindProcessStep -> not valid CallStatusBehaviorType for {{ processId: {0}, statusBehvior: {1} }} ", stepId, procSts);
        //                    throw new Exception(msg);
        //                }
        //        }
        //    }

        //    return (int)intStep;

        //}
        private int SendCodeSms(string phoneNumber, string message)
        {
            //install Microsoft.Extensions.Configuration.Binder
            //TODO: create a function to get config keys and set default values

            string InForUSmsUri = _configuration["AppSettings:InForU_Uri"];//  System.Configuration.ConfigurationManager.AppSettings["InForU_Uri"];
            string InForUSmsUserName = _configuration["AppSettings:InForU_UserName"]; //System.Configuration.ConfigurationManager.AppSettings["InForU_UserName"];
            string InForUSmsPassword = _configuration["AppSettings:InForU_Password"]; //System.Configuration.ConfigurationManager.AppSettings["InForU_Password"];
            string InForUSmsSender = _configuration["AppSettings:InForU_Sender"]; // System.Configuration.ConfigurationManager.AppSettings["InForU_Sender"];

            string messageText = System.Security.SecurityElement.Escape(message);

            // create XML
            StringBuilder sbXml = new StringBuilder();
            sbXml.Append("<Inforu>");
            sbXml.Append("<User>");
            sbXml.Append("<Username>" + InForUSmsUserName + "</Username>");
            sbXml.Append("<Password>" + InForUSmsPassword + "</Password>");
            sbXml.Append("</User>");
            sbXml.Append("<Content Type=\"sms\">");
            sbXml.Append("<Message>" + messageText + "</Message>");
            sbXml.Append("</Content>");
            sbXml.Append("<Recipients>");
            sbXml.Append("<PhoneNumber>" + phoneNumber + "</PhoneNumber>");
            sbXml.Append("</Recipients>"); sbXml.Append("<Settings>");
            sbXml.Append("<Sender>" + InForUSmsSender + "</Sender>");
            sbXml.Append("</Settings>"); sbXml.Append("</Inforu >");
            string strXML = System.Web.HttpUtility.UrlEncode(sbXml.ToString(), System.Text.Encoding.UTF8);
            string result = PostDataToURL(InForUSmsUri, "InforuXML=" + strXML);

            XElement resultObj = XElement.Parse(result);
            int intResult;
            int.TryParse(resultObj.Element("Status").Value, out intResult);
            return intResult;
        }

        private static string PostDataToURL(string szUrl, string szData)
        {
            //Setup the web request
            string szResult = string.Empty;
            WebRequest Request = WebRequest.Create(szUrl);
            Request.Timeout = 30000;
            Request.Method = "POST";
            Request.ContentType = "application/x-www-form-urlencoded";

            //Set the POST data in a buffer
            byte[] PostBuffer;
            try
            {         // replacing " " with "+" according to Http post RPC
                szData = szData.Replace(" ", "+");

                //Specify the length of the buffer
                PostBuffer = Encoding.UTF8.GetBytes(szData);
                Request.ContentLength = PostBuffer.Length;

                //Open up a request stream
                Stream RequestStream = Request.GetRequestStream();

                //Write the POST data
                RequestStream.Write(PostBuffer, 0, PostBuffer.Length);

                //Close the stream
                RequestStream.Close();
                //Create the Response object
                WebResponse Response;
                Response = Request.GetResponse();

                //Create the reader for the response
                StreamReader sr = new StreamReader(Response.GetResponseStream(), Encoding.UTF8);

                //Read the response
                szResult = sr.ReadToEnd();

                //Close the reader, and response
                sr.Close();
                Response.Close();

                return szResult;
            }
            catch
            { return szResult; }
        }
        //GetCurrentLoginRecord need to be used only at login action
        //after login we will use the Token Auth
        //Login Count should start After SMS Identification attempt
        private LoginLog GetCurrentLoginRecord(SrvManLoginDto data )
        {
            //LoginLog currentLogin;
            int intParam = 0;
            SiteParams wSiteParams = new SiteParams(_DbContext);
            intParam = wSiteParams.GetSiteParamInt((int)WebSiteParams.AuthenticationSessionInterval, 15);
            DateTime limitTime = DateTime.Now.AddMinutes(intParam * -1);
                LoginLog currentLogin = _DbContext.LoginLogs.FirstOrDefault(s => s.SrvManNo == data.UserId
                                                            && s.lastUpdateDate >= limitTime
                                                            && s.status == (int)AuthenticationStatusType.AuthenticationCreateLoginRow
                                                            && s.sessionId == data.SessionId);
                if (currentLogin == null)
                {
                    currentLogin = new LoginLog();
                    _DbContext.LoginLogs.Add(currentLogin);
                }



            // currentLogin = db.LoginLogs.Add(currentLogin);

                currentLogin.creationDate = DateTime.Now;
                currentLogin.lastUpdateDate = DateTime.Now;


            currentLogin.sessionId = GenerateSessionGuid();
            data.SessionId = currentLogin.sessionId;
            //No AuthenticationQuestions in this app then aleways when we create a new log we will set the status to AuthenticationQuestions
            data.Status = (int)AuthenticationStatusType.AuthenticationCreateLoginRow;
            currentLogin.status = (int)AuthenticationStatusType.AuthenticationCreateLoginRow;
            currentLogin.ip = data.IP;
            currentLogin.SrvManName = data.UserName;
            currentLogin.SrvManNo = data.UserId;
            currentLogin.phoneNo = data.PhoneNo;
            currentLogin.email = "";
            currentLogin.smsCode = "";
            currentLogin.smsCodeCounter = 0;
            currentLogin.lastUpdateDate = DateTime.Now;
            currentLogin.smsCodeRetriesCounter = 0;
            currentLogin.isSupplier = data.IsSupplier;
            _DbContext.SaveChanges();

            return currentLogin;
        }
        public string GenerateSessionGuid()
        {
            return string.Concat(System.Guid.NewGuid(), System.Guid.NewGuid());
        }

        private bool ValidateLoginAttemptsCount(string ip)
        {
            try
            {
                //using (_DbContext)
                //{ 

                    SiteParams wSiteParams = new SiteParams(_DbContext);
                    int limitPeriod = wSiteParams.GetSiteParamInt((int)WebSiteParams.LoginLimitPeriod, 1);
                    int LimitAttemptsCount = 0;
                    DateTime limitTime = DateTime.Now.AddMinutes(-limitPeriod);

                    var attemptsCountInLimitPeriod = _DbContext.LoginLogs.Where(i => i.ip == ip && i.creationDate >= limitTime).Count();
                    LimitAttemptsCount= wSiteParams.GetSiteParamInt((int)WebSiteParams.LoginLimitAttemptsCount, 20);
                    if (attemptsCountInLimitPeriod > LimitAttemptsCount)
                        return false;
                    else
                        return true;
                //}


            }
            catch (Exception ex)
            {
                //logger.Error($"ValidateLoginAttemptsCount - Failed to validate login attepts count limit for IP: {ip}", ex);

                return false;
            }
        }
             private void LockSrvMan(LoginLog currentLogin)
        {
                //logger.Debug(string.Format("start lockLicense for license number: {0}", licenseNum));
                _DbContext.LockedSrvMans.Add(new LockedSrvMan() { lockedSrvManNo = currentLogin.SrvManNo, SrvManNM= currentLogin.SrvManName, IsSupplier = currentLogin.isSupplier, lockedDate = DateTime.Now });
                _DbContext.SaveChanges();
        }

           private bool IsLocked(SrvManLoginDto data)
        {
            //logger.Debug(string.Format("start isLocked for licenseNumber: {0}, ip: {1}", data.licenseNumber, data.ip));
            SiteParams wSiteParams = new SiteParams(_DbContext);

            int lockLicenseInterval = wSiteParams.GetSiteParamInt((int)WebSiteParams.LockLicenseInterval, 10);
            DateTime limitTime = DateTime.Now.AddMinutes(lockLicenseInterval * -1);
            //bool isLocked = false;

                //check if License is locked
                if (_DbContext.LockedSrvMans.Where(p => p.lockedSrvManNo == data.UserId && p.lockedDate > limitTime).Any())
                {
                    //logger.Debug(string.Format("isLocked for licenseNumber: {0}, ip: {1}, isLicenseLocked: {2}", data.licenseNumber, data.ip, isLocked));
                    return true;
                }

                //check if ip is locked
                if (_DbContext.LockedIps.Where(p => p.ip == data.IP && p.lockedDate > limitTime).Any())
                {
                   // logger.Debug(string.Format("isLocked for licenseNumber: {0}, ip: {1}, isIpLocked: {2}", data.licenseNumber, data.ip, isLocked));
                    return true;
                }
        

            return false;
        }


        //public LoginLogs GetUserLoginInfo( string SessionId)
        //{
        //    LoginLogs currentLogin;
        //    clsUtil ObjUtil = new clsUtil();
        //    clsBLogin objLogin = new clsBLogin();
        //    Boolean retVal = false;
        //    int intParam = 0;
        //    // string strParam = "";

        //    int.TryParse(ObjUtil.GetParameters(strConn, (int)SiteParams.AuthenticationSessionInterval, true), out intParam);

        //    //Insted of given the login live life to 15 min set to allowed emp shift 12 Hr = 720 Min
        //    intParam = 720;

        //    DateTime limitTime = DateTime.Now.AddMinutes(intParam * -1);
        //    //currentLogin = objLogin.GetSrvManLoginRecord(strConn, SessionId, limitTime, (int)AuthenticationStatusType.AuthenticationCreateLoginRow);
        //    currentLogin = objLogin.GetSrvManLoginRecord(strConn, SessionId, limitTime, (int)AuthenticationStatusType.AuthenticationPassed);

        //    return currentLogin;
        //}
        //public Boolean CheckUserLogin(string strConn, string SessionId)
        //{
        //    LoginLogs currentLogin;
        //    clsUtil ObjUtil = new clsUtil();
        //    clsBLogin objLogin = new clsBLogin();
        //    Boolean retVal = false;
        //    int intParam = 0;
        //    // string strParam = "";

        //    int.TryParse(ObjUtil.GetParameters(strConn, (int)SiteParams.AuthenticationSessionInterval, true), out intParam);

        //    DateTime limitTime = DateTime.Now.AddMinutes(intParam * -1);
        //    //currentLogin = objLogin.GetSrvManLoginRecord(strConn, SessionId, limitTime, (int)AuthenticationStatusType.AuthenticationCreateLoginRow);
        //    currentLogin = objLogin.GetSrvManLoginRecord(strConn, SessionId, limitTime, (int)AuthenticationStatusType.AuthenticationPassed);
        //    if (currentLogin.ErrID == false)
        //    {
        //        retVal = true;
        //    }
        //    else
        //    {
        //        retVal = false;
        //    }
        //    return retVal;
        //}

        //public bool GetSrvManLastShiftment(string strConn, ref AuthCode authCode)
        //{
        //    bool retVal = false;
        //    clsSrvManLastShiftInfo srvManLastShiftInfo;
        //    clsBLogin objLogin = new clsBLogin();

        //    srvManLastShiftInfo = objLogin.GetSrvManLastShiftInfo(strConn, authCode.srvManNo);
        //    if (srvManLastShiftInfo.ErrID == false)
        //    {
        //        authCode.isShiftStarted = srvManLastShiftInfo.IsShiftStarted;
        //        authCode.isStartBreak = srvManLastShiftInfo.IsStartBreak;
        //        authCode.fleetCarNo = srvManLastShiftInfo.FleetCarNo;
        //        authCode.startShift = srvManLastShiftInfo.StartShift;
        //        authCode.shiftBreak = DateTime.Now;
        //        authCode.Success = true;
        //        authCode.ErrDesc = "";
        //        retVal = true;
        //    }
        //    else
        //    {
        //        retVal = false;
        //        authCode.Success = true;
        //        authCode.ErrDesc = srvManLastShiftInfo.ErrDesc;
        //    }
        //    return retVal;

        //}


    }
}
