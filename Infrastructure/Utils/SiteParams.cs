using Core.Entities;
using Infrastructure.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Utils
{
    public  class SiteParams
    {
        private readonly SQLContext db;
        public SiteParams(SQLContext DbContext)
        {
            db = DbContext;
        }

        public async Task<int> GetSiteParamInt(int paramId, int defaultValue)
        {
            //using (db)
            //{
                SiteParam param = db.SiteParams.Where(p => p.Id == paramId).FirstOrDefault();
                if (param == null)
                    return await Task.FromResult(defaultValue);
            return await Task.FromResult(param.intValue == null ? defaultValue : (int)param.intValue);
            //}
        }

        public async Task<string> GetSiteParamString(int paramId, string defaultValue)
        {
            //using (db)
            //{
                SiteParam param = db.SiteParams.Where(p => p.Id == paramId).FirstOrDefault();
                if (param == null)
                return await Task.FromResult(defaultValue);
            return await Task.FromResult(string.IsNullOrEmpty(param.stringValue) ? defaultValue : param.stringValue);
            //}
        }

        public async Task<decimal> GetSiteParamDecimal(int paramId, decimal defaultValue)
        {
            //using (db)
            //{
                SiteParam param = db.SiteParams.Where(p => p.Id == paramId).FirstOrDefault();
                if (param == null)
                return await Task.FromResult(defaultValue);
            return await Task.FromResult(param.decimalValue == null ? defaultValue : (decimal)param.decimalValue);
            //}
        }

        //public readonly static int SmsExperationInterval = GetSiteParam("SmsCodeExperationInterval", 2);
        //public readonly static int MaxRetries = GetSiteParam("SmsMessageMaxRetries", 3);
        //public readonly static int SessionInterval = GetSiteParam("LoginSessionInterval", 30);
        //public readonly  int AuthenticationSessionInterval = GetSiteParam("AuthenticationSessionInterval", 15);
        //public readonly  int LockLicenseInterval = GetSiteParam("LockLicenseInterval", 120);
        //public readonly  int LockIpInterval = GetSiteParam("LockIpRange", 120);
        //public readonly static int MaxWrongLogingCnt = GetSiteParam("MaxWrongLogingCnt", 10);
        //public readonly static int SmsAuthenticationCodeLength = GetSiteParam("smsAuthenticationCodeLength", 6);
        //public readonly static string TroubleshootingSpliter = GetSiteParam("TroubleshootingSpliter", "@#@");
        //public readonly static string CallInfoNotificationsSpliter = GetSiteParam("CallInfoNotificationsSpliter", "@#@");
        //public readonly static string[] AttachmentContentTypes = GetSiteParam("AttachmentContentTypes", "").Split(',').ToArray();
        //public readonly static int AttachmentMaxContentLength = GetSiteParam("AttachmentMaxContentLength", 10);
        //public readonly static int FilesUploadLemitPeriod = GetSiteParam("FilesUploadLimitPeriod", 5);
        //public readonly static int FilesUploadLemitFilesCount = GetSiteParam("FilesUploadLimitFilesCount", 5);
        //public readonly  int LoginLimitPeriod = GetSiteParam("LoginLimitPeriod", 1);
        //public readonly  int LoginLimitAttemptsCount = GetSiteParam("LoginLimitAttemptsCount", 50);
        //public readonly static string TestLicenseNumber = GetSiteParam("TestLicenseNumber", "00");
        //public readonly static string TestUserPhone = GetSiteParam("TestUserPhone", "00");
        //public readonly static string TestUserName = GetSiteParam("TestUserName", "00");
        //public readonly static string TestUserAuthCode = GetSiteParam("TestUserAuthCode", "00");

    }
}