using Core.Models.Auth;
using DigitalUtil;
using Microsoft.Extensions.Configuration;
using ShagApi.Enums;
using ShagApi.Models.Auth;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Infrastructure.Services.Auth
{
    public class LoginSrv
    {
        private readonly IConfiguration _configuration;
             public LoginSrv( IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Boolean CheckAuthenticationDetails(string strConn, LoginDto data)
        {
            try
            {
                LoginLogs currentLogin;
                clsBLogin objLogin = new clsBLogin();
                currentLogin = GetCurrentLoginRecord(strConn, data);
                if (!string.IsNullOrWhiteSpace(data.IP) && !ValidateLoginAttemptsCount(strConn, data.IP))
                {
                    LockSrvMan(strConn, currentLogin);
                    data.Status = (int)AuthenticationStatusType.LockedUser;
                    objLogin.UpdateLoginTbl(strConn, (int)AuthenticationStatusType.LockedUser, data.SessionId);
                    return false;
                }
                //Check if user has validity cover id in Shagrir
                if (data.UserId <= 0)
                {
                    data.Status = (int)AuthenticationStatusType.UserNotSubsecribed;
                    objLogin.UpdateLoginTbl(strConn, (int)AuthenticationStatusType.UserNotSubsecribed, data.SessionId);
                    return false;
                }
                //Check if locked user
                if (IsLocked(strConn, data))
                {
                    data.Status = (int)AuthenticationStatusType.LockedUser;
                    objLogin.UpdateLoginTbl(strConn, (int)AuthenticationStatusType.LockedUser, data.SessionId);
                    return false;
                }

                return true;

            }
            catch (Exception ex)
            {

                return false;

            }
        }
        public AuthCode CheckAuthenticationCode(string strConn, SmsAuth data)
        {
            AuthCode clsAuthCode = new AuthCode(data.srvManNo);
            try
            {
                LoginLogs currentLogin;
                clsBLogin objLogin = new clsBLogin();
                clsUtil ObjUtil = new clsUtil();
                Boolean retVal = false;
                int iSmsExperationInterval = 0;
                DateTime dtExperationDate;
                clsAuthCode.nextStep = 0;
                if (string.IsNullOrEmpty(data.smsCode))
                {
                    clsAuthCode.SmsCodeStatusType = (int)SmsCodeStatusType.EmptySmsCode;
                    return clsAuthCode;
                }

                currentLogin = GetLoginRecord(strConn, data.sessionId);

                if (currentLogin.ErrID)
                {
                    clsAuthCode.SmsCodeStatusType = (int)SmsCodeStatusType.LoginNotFound;
                    return clsAuthCode;
                }
                else
                {

                    int.TryParse(ObjUtil.GetParameters(strConn, (int)SiteParams.SmsExperationInterval, true), out iSmsExperationInterval);
                    if (iSmsExperationInterval == 0) iSmsExperationInterval = 2;
                    dtExperationDate = DateTime.Now.AddMinutes(iSmsExperationInterval * -1);

                    if (currentLogin.smsCreationDate < dtExperationDate)
                    {
                        //logger.Debug(string.Format("CheckAuthenticationCode: CodeExpired {{ sessionId: {0}, authenticationCode: {1}, lastUpdateDate: {2} }}", sessionId, authenticationCode, loginLog.lastUpdateDate));
                        clsAuthCode.SmsCodeStatusType = (int)SmsCodeStatusType.CodeExpired;
                        return clsAuthCode;
                    }


                    currentLogin.smsCodeRetriesCounter = currentLogin.smsCodeRetriesCounter == 0 ? 1 : currentLogin.smsCodeRetriesCounter + 1;//increace smsCodeRetriesCounter
                    if (currentLogin.smsCode != data.smsCode)
                    {
                        objLogin.UpdateLoginTblSmsTry(strConn, currentLogin.smsCodeRetriesCounter, data.sessionId);
                        if (currentLogin.smsCodeRetriesCounter < 3)
                        {
                            clsAuthCode.SmsCodeStatusType = (int)SmsCodeStatusType.WrongCode;
                            return clsAuthCode;
                        }
                        else
                        {
                            
                            clsAuthCode = CreateNewAuthenticationCode(strConn, currentLogin);
                            clsAuthCode.SmsCodeStatusType = (int)SmsCodeStatusType.ReSendNewSmsCode;
                            return clsAuthCode;
                        }
                    }


                    //logger.Debug(string.Format("CheckAuthenticationCode: passed {{ sessionId: {0}, authenticationCode: {1} }}", sessionId, authenticationCode));
                    //objLogin.UpdateLoginTbl(strConn, (int)AuthenticationStatusType.AuthenticationPassed, data.SessionId);
                    retVal = objLogin.CheckSmsCode(strConn, data.sessionId, data.smsCode);

                    int.TryParse(ObjUtil.GetParameters(strConn, (int)SiteParams.SmsExperationInterval, true), out iSmsExperationInterval);
                    DateTime experationDate = DateTime.Now.AddMinutes(iSmsExperationInterval * -1);


                    objLogin.UpdateLoginTbl(strConn, (int)AuthenticationStatusType.AuthenticationPassed, data.sessionId);
                    clsAuthCode.SmsCodeStatusType = (int)SmsCodeStatusType.AuthenticationPassed;
                    clsAuthCode.nextStep = BindLoginToProcess(strConn, currentLogin, ProcessStepType.Login, 1);
                    return clsAuthCode;

                }

                //return SmsCodeStatusType.NotOnSmsCodeStatus;


            }

            catch (Exception ex)
            {
                clsAuthCode.SmsCodeStatusType = (int)SmsCodeStatusType.NotOnSmsCodeStatus;
                return clsAuthCode;
            }
        }
        //public LoginLog CheckIfLoginExists(string strConn, LoginDto data)
        public LoginLogs CheckIfLoginExists(string strConn, int intUserId)
        {
            LoginLogs currentLogin;
            clsUtil ObjUtil = new clsUtil();
            clsBLogin objLogin = new clsBLogin();
            int intParam = 0;
            int.TryParse(ObjUtil.GetParameters(strConn, (int)SiteParams.AuthenticationSessionInterval, true), out intParam);
            DateTime limitTime = DateTime.Now.AddMinutes(intParam * -1);
            //currentLogin = objLogin.GetCurrentLoginRecord(strConn, data.userId, limitTime, (int)AuthenticationStatusType.AuthenticationCreateLoginRow);
            currentLogin = objLogin.GetCurrentLoginRecord(strConn, intUserId, limitTime, (int)AuthenticationStatusType.AuthenticationCreateLoginRow);
            //retVal = currentLogin.ErrID;

            return currentLogin;
        }
        public LoginLogs GetLoginRecord(string strConn, string sessionId)
        {
            LoginLogs currentLogin;
            clsBLogin objLogin = new clsBLogin();


            currentLogin = objLogin.GetLoginRecord(strConn, sessionId);



            return currentLogin;
        }
        public AuthCode SendSms(string strConn, LoginLogs data)
        {



            return CreateNewAuthenticationCode(strConn, data);




            //return true;
        }
        public AuthCode CreateNewAuthenticationCode(string strConn, LoginLogs currentLogin)//, string licenseNumber)
        {
            try
            {
                //logger.Debug(string.Format("start CreateNewAuthenticationCode {{sessionId: {0} }}", sessionId));
                SmsCodeStatusType status;
                //LoginLog currentLogin;
                clsBLogin objLogin = new clsBLogin();
                //string code = string.Empty;
                Boolean retVal = false;
                int nextStep = -1;
                int iMaxRetries = 0;
                int iSmsAuthenticationCodeLength = 0;
                string sTestUserAuthCode = "";
                AuthCode clsAuthCode = new AuthCode(currentLogin.SrvManNo);


                //currentLogin = GetCurrentLoginRecord(strConn, data);

                clsAuthCode.nextStep = 0;

                if (currentLogin.ErrID || currentLogin.status != (int)AuthenticationStatusType.AuthenticationCreateLoginRow)
                {
                    //logger.Debug(string.Format("End CreateNewAuthenticationCode with SmsCodeStatusType.NotOnSmsCodeStatus {{sessionId: {0} }}", sessionId));
                    clsAuthCode.SmsCodeStatusType = (int)SmsCodeStatusType.NotOnSmsCodeStatus;
                    return clsAuthCode;
                }

                clsUtil ObjUtil = new clsUtil();
                int.TryParse(ObjUtil.GetParameters(strConn, (int)SiteParams.MaxRetries, true), out iMaxRetries);
                if (currentLogin.smsCodeCounter >= iMaxRetries)
                {

                    currentLogin.status = (int)AuthenticationStatusType.LockedUser;
                    LockSrvMan(strConn, currentLogin);
                    status = SmsCodeStatusType.OverMaxRetries;
                }

                else
                {
                    //Valid user => send SMS
                    string code = string.Empty;
                    bool isTestSrvMan = IsTestUser(strConn, currentLogin);

                    if (isTestSrvMan || 1 == 1)
                    {
                        //return constant SMS No = 1234
                        sTestUserAuthCode = ObjUtil.GetParameters(strConn, (int)SiteParams.TestUserAuthCode, false);
                        code = sTestUserAuthCode;
                    }
                    else
                    {

                        int.TryParse(ObjUtil.GetParameters(strConn, (int)SiteParams.SmsAuthenticationCodeLength, true), out iSmsAuthenticationCodeLength);
                        code = RandomExtentions.CreateString(iSmsAuthenticationCodeLength);
                    }

                    currentLogin.smsCode = code;
                    currentLogin.smsCodeCounter = currentLogin.smsCodeCounter == 0 ? 1 : (int)currentLogin.smsCodeCounter + 1;
                    currentLogin.smsCodeRetriesCounter = 0;
                    currentLogin.smsCreationDate = DateTime.Now;
                    string strSmsMessageParam = ObjUtil.GetParameters(strConn, (int)SiteParams.SmsAuthenticationMessage, false);
                    string strMessage = strSmsMessageParam != null ? strSmsMessageParam.Replace("@name@", currentLogin.SrvManName).Replace("@code@", code) : string.Format("שלום {0}\n לפניך קוד אימות עבור כניסה למערכת שגריר, אנא הזן את הקוד והמשך בתהליך \n קוד האימות: {1}", currentLogin.SrvManName, code);

                    if (isTestSrvMan || 1==1)
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

                retVal = objLogin.UpdateLoginTblWithSmsInfo(strConn, currentLogin);

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
        private bool IsTestUser(string strConn, LoginLogs loginLog)
        {
            clsUtil ObjUtil = new clsUtil();
            int iTestSrvManNumber = 0;
            string sTestUserPhone = "";
            string sTestSrvManName = "";
            //clsUtil ObjUtil = new clsUtil();
            //DataTable result = ObjUtil.GetSiteParams(strConn);
            //if (result != null)
            //{
            //    foreach (DataRow row in result.Rows)
            //    {
            //        iTestSrvManNumber= row["SbcVld"].ToString().IntParseDefaultOrValue();
            //        sTestUserPhone= row["SBCNAME"].ToString();
            //        sTestSrvManName= row["SBCNAME"].ToString();
            //    }
            //}

            //logger.Debug($"IsTestUser Validation: Paremeters send: licenseNum: {loginLog.licenseNum}, phone: {loginLog.phoneNum},name: {loginLog.SbcDetail.Sbc_name } ");

            bool res = true;
            int.TryParse(ObjUtil.GetParameters(strConn, (int)SiteParams.TestLicenseNumber, true), out iTestSrvManNumber);
            if (loginLog.SrvManNo != iTestSrvManNumber)
                return false;
            sTestUserPhone = ObjUtil.GetParameters(strConn, (int)SiteParams.TestUserPhone, false);
            if (loginLog.phoneNo != sTestUserPhone && loginLog.SrvManNo != iTestSrvManNumber)
                return false;


            return res;
        }
        public int BindLoginToProcess(string strConn, LoginLogs loginLog, ProcessStepType stepId, int procSts)
        {
            //logger.Debug(string.Format("BindLoginToProcess start {{ loginLog:{0}, sbcVld:{1} }}", loginLog.loginId, sbcVld));
            clsBLogin objLogin = new clsBLogin();
            //string code = string.Empty;
            Boolean retVal = false;
            long lngProccessId = 0;
            ProcessStepType intStep = ProcessStepType.Login;
            int intCurrentStep = -1;

            lngProccessId = objLogin.GetProccessId(strConn, loginLog.loginId);
            intCurrentStep = objLogin.GetProccessCurrentStep(strConn, loginLog.loginId);
            if (intCurrentStep == (int)ProcessStepType.Login || intCurrentStep == 0)
            {
                //(int)ProcessStepType.None
                intStep = ProcessStepType.Emp_attn;
                intCurrentStep = (int)intStep;
                retVal = objLogin.CreateProccessId(strConn, loginLog, intCurrentStep);
            }
            else
            {
                if (stepId != ProcessStepType.None && stepId != ProcessStepType.Login)
                {
                    intStep = stepId;
                }
                retVal = objLogin.UpdateProccessStepId(strConn, lngProccessId, (int)intStep, procSts);
                switch (stepId)
                {
                    case ProcessStepType.Login:
                        {
                            intStep = ProcessStepType.Emp_attn;
                            break;
                        }
                    case ProcessStepType.Emp_attn:
                        {
                            intStep = ProcessStepType.srvManCallsList;
                            break;
                        }
                    case ProcessStepType.Emp_attn_Start_Break:
                        {
                            intStep = ProcessStepType.Emp_attn_Start_Break;
                            break;
                        }
                    case ProcessStepType.srvManCallsList:
                        {
                            intStep = ProcessStepType.onMyWay;
                            break;
                        }
                    case ProcessStepType.onMyWay:
                        {
                            intStep = ProcessStepType.arrived;
                            break;
                        }
                    case ProcessStepType.arrived:
                        {
                            intStep = ProcessStepType.insertRoadCallData;
                            break;
                        }
                    case ProcessStepType.insertRoadCallData:
                        {
                            intStep = ProcessStepType.closing_Call;
                            break;
                        }
                    case ProcessStepType.closing_Call:
                        {
                            intStep = ProcessStepType.srvManCallsList;
                            break;
                        }
                    default:
                        {
                            string msg = string.Format("BindProcessStep -> not valid CallStatusBehaviorType for {{ processId: {0}, statusBehvior: {1} }} ", stepId, procSts);
                            throw new Exception(msg);
                        }
                }
            }

            return (int)intStep;

        }
        private int SendCodeSms(string phoneNumber, string message)
        {
            //install Microsoft.Extensions.Configuration.Binder
            //TODO: create a function to get config keys and set default values

            string InForUSmsUri = _configuration.GetValue<string>("AppSettings:InForU_Uri");//  System.Configuration.ConfigurationManager.AppSettings["InForU_Uri"];
            string InForUSmsUserName = _configuration.GetValue<string>("AppSettings:InForU_UserName"); //System.Configuration.ConfigurationManager.AppSettings["InForU_UserName"];
            string InForUSmsPassword = _configuration.GetValue<string>("AppSettings:InForU_Password"); //System.Configuration.ConfigurationManager.AppSettings["InForU_Password"];
            string InForUSmsSender = _configuration.GetValue<string>("AppSettings:InForU_Sender"); // System.Configuration.ConfigurationManager.AppSettings["InForU_Sender"];

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
        private LoginLogs GetCurrentLoginRecord(string strConn, LoginDto data)
        {
            LoginLogs currentLogin;
            clsUtil ObjUtil = new clsUtil();
            clsBLogin objLogin = new clsBLogin();
            Boolean retVal = false;
            int intParam = 0;
            // string strParam = "";

            int.TryParse(ObjUtil.GetParameters(strConn, (int)SiteParams.AuthenticationSessionInterval, true), out intParam);

            DateTime limitTime = DateTime.Now.AddMinutes(intParam * -1);

            currentLogin = objLogin.GetCurrentLoginRecord(strConn, data.UserId, limitTime, (int)AuthenticationStatusType.AuthenticationCreateLoginRow);


            if (currentLogin.ErrID) //if ErrId = true then No Login Exists
            {
                //Create a login row
                LoginLogs loginRecord = new LoginLogs();
                loginRecord.creationDate = DateTime.Now;
                loginRecord.sessionId = GenerateSessionGuid();
                data.SessionId = loginRecord.sessionId;
                //No AuthenticationQuestions in this app then aleways when we create a new log we will set the status to AuthenticationQuestions
                data.Status = (int)AuthenticationStatusType.AuthenticationCreateLoginRow;
                loginRecord.status = (int)AuthenticationStatusType.AuthenticationCreateLoginRow;
                loginRecord.ip = data.IP;
                loginRecord.SrvManName = data.UserName;
                loginRecord.SrvManNo = data.UserId;
                loginRecord.phoneNo = data.PhoneNo;
                loginRecord.email = "";
                loginRecord.smsCode = "";
                loginRecord.smsCodeCounter = 0;
                loginRecord.lastUpdateDate = DateTime.Now;
                loginRecord.sbcDetailsId = 0;
                loginRecord.smsCodeRetriesCounter = 0;
                loginRecord.isSupplier = data.IsSupplier;
                


                retVal = objLogin.CreateLoginInfo(strConn, loginRecord);

            }
            else
            {
                data.SessionId = currentLogin.sessionId;
                data.Status = currentLogin.status;
            }
            //currentLogin.sessionId = data.SessionId;
            //currentLogin.ip = data.ip;
            //currentLogin.customerName = autSrvMan.FirstName + " " + autSrvMan.LastName;
            //currentLogin.licenseNum = data.licenseNumber;
            //currentLogin.phoneNum = data.mobilePhoneNumber;
            //currentLogin.email = data.emailAddress;
            return currentLogin;
        }
        public string GenerateSessionGuid()
        {
            return string.Concat(System.Guid.NewGuid(), System.Guid.NewGuid());
        }
        private bool ValidateLoginAttemptsCount(string strConn, string ip)
        {
            clsUtil ObjUtil = new clsUtil();
            clsBLogin objLogin = new clsBLogin();
            int limitPeriod = 0;
            int LimitAttemptsCount = 0;
            try
            {


                int.TryParse(ObjUtil.GetParameters(strConn, (int)SiteParams.LoginLimitPeriod, true), out limitPeriod);
                DateTime limitTime = DateTime.Now.AddMinutes(-limitPeriod);

                var attemptsCountInLimitPeriod = objLogin.GetIpLoginCount(strConn, ip, limitTime);//    db.LoginLogs.Where(i => i.ip == ip && i.creationDate >= limitTime).Count();
                int.TryParse(ObjUtil.GetParameters(strConn, (int)SiteParams.LoginLimitAttemptsCount, true), out LimitAttemptsCount);
                if (attemptsCountInLimitPeriod > LimitAttemptsCount)
                    return false;
                else
                    return true;



            }
            catch (Exception ex)
            {

                return false;
            }
        }
        private void LockSrvMan(string strConn, LoginLogs currentLogin)
        {
            clsBLogin objLogin = new clsBLogin();
            bool retVal = false;
            retVal = objLogin.LockSrvMan(strConn, currentLogin.SrvManNo);


        }
        private bool IsLocked(string strConn, LoginDto data)
        {
            clsUtil ObjUtil = new clsUtil();
            clsBLogin objLogin = new clsBLogin();
            int iLockLicenseInterval = 0;


            int.TryParse(ObjUtil.GetParameters(strConn, (int)SiteParams.LockLicenseInterval, true), out iLockLicenseInterval);
            DateTime limitTime = DateTime.Now.AddMinutes(iLockLicenseInterval * -1);
            bool isLocked = false;


            //check if License is locked
            isLocked = objLogin.checkIfSrvManLocked(strConn, data.UserId, limitTime);
            if (!isLocked)
            {
                isLocked = objLogin.checkIfSrvManIpLocked(strConn, data.IP, limitTime);
                return isLocked;
            }
            else
            {
                return true;
            }


            //return false;
        }
        public LoginLogs GetUserLoginInfo(string strConn, string SessionId)
        {
            LoginLogs currentLogin;
            clsUtil ObjUtil = new clsUtil();
            clsBLogin objLogin = new clsBLogin();
            Boolean retVal = false;
            int intParam = 0;
            // string strParam = "";

            int.TryParse(ObjUtil.GetParameters(strConn, (int)SiteParams.AuthenticationSessionInterval, true), out intParam);

            //Insted of given the login live life to 15 min set to allowed emp shift 12 Hr = 720 Min
            intParam =  720;

            DateTime limitTime = DateTime.Now.AddMinutes(intParam * -1);
            //currentLogin = objLogin.GetSrvManLoginRecord(strConn, SessionId, limitTime, (int)AuthenticationStatusType.AuthenticationCreateLoginRow);
            currentLogin = objLogin.GetSrvManLoginRecord(strConn, SessionId, limitTime, (int)AuthenticationStatusType.AuthenticationPassed);

            return currentLogin;
        }
        public Boolean CheckUserLogin(string strConn, string SessionId)
        {
            LoginLogs  currentLogin;
            clsUtil ObjUtil = new clsUtil();
            clsBLogin objLogin = new clsBLogin();
            Boolean retVal = false;
            int intParam = 0;
            // string strParam = "";

            int.TryParse(ObjUtil.GetParameters(strConn, (int)SiteParams.AuthenticationSessionInterval, true), out intParam);

            DateTime limitTime = DateTime.Now.AddMinutes(intParam * -1);
            //currentLogin = objLogin.GetSrvManLoginRecord(strConn, SessionId, limitTime, (int)AuthenticationStatusType.AuthenticationCreateLoginRow);
            currentLogin = objLogin.GetSrvManLoginRecord(strConn, SessionId, limitTime, (int)AuthenticationStatusType.AuthenticationPassed);
            if (currentLogin.ErrID == false)
            {
                retVal = true;
            }
            else
            {
                retVal = false;
            }
            return retVal;
        }

        public bool GetSrvManLastShiftment(string strConn,ref AuthCode authCode)
        {
            bool retVal = false;
            clsSrvManLastShiftInfo srvManLastShiftInfo;
            clsBLogin objLogin = new clsBLogin();

            srvManLastShiftInfo = objLogin.GetSrvManLastShiftInfo(strConn, authCode.srvManNo);
            if (srvManLastShiftInfo.ErrID == false)
            {
                authCode.isShiftStarted = srvManLastShiftInfo.IsShiftStarted;
                authCode.isStartBreak = srvManLastShiftInfo.IsStartBreak;
                authCode.fleetCarNo = srvManLastShiftInfo.FleetCarNo;
                authCode.startShift = srvManLastShiftInfo.StartShift;
                authCode.shiftBreak = DateTime.Now;
                authCode.Success = true;
                authCode.ErrDesc = "";
                retVal = true;
            }
            else
            {
                retVal = false;
                authCode.Success = true;
                authCode.ErrDesc = srvManLastShiftInfo.ErrDesc;
            }
            return retVal;

        }

       


    }
}
