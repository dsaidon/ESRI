using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Enums
{
    public enum WebSiteParams
    {
        [Description("מספר ניסיונות קוד סמס")]
        MaxRetries = 31,
        [Description("מספר דקות לנעילת מספר רישוי")]
        LockLicenseInterval = 4,
        [Description("מספר דקות לסשן")]
        LoginSessionInterval = 5,
        [Description("טווח דקות לבדיקת משתמש עם נסיונות כניסה שגויים")]
        LockIpRange = 6,
        [Description("מספר כניסות מקסימלי בטווח זמן לנעילת משתמש")]
        MaxWrongLogingCnt = 7,
        [Description("טקסט להצגה בהודעת אימות SMS")]
        SmsAuthenticationMessage = 8,
        [Description("מספר נסיונות לאימות SMS")]
        SmsMessageMaxRetries = 9,
        [Description("מספר דקות לתוקף אימות קוד SMS")]
        SmsCodeExperationInterval = 10,
        [Description("אורך קוד אימות SMS")]
        SmsAuthenticationCodeLength = 12,
        [Description("תווים המסמנים מעבר שורה בתבנית troubleshooting")]
        TroubleshootingSpliter = 13,
        [Description("מספר דקות לתהליך לוגין (איפוס ספירת שאלות אימות)")]
        AuthenticationSessionInterval = 16,
        [Description("תווים להפרדה בין הודעות סטטוס קריאה")]
        CallInfoNotificationsSpliter = 18,
        [Description("סוגי קבצים נתמכים בהעלאת צרופה")]
        AttachmentContentTypes = 21,
        [Description("גודל קובץ מקסימלי להעלאת צרופה ב- KB")]
        AttachmentMaxContentLength = 22,
        [Description("אבטחה - תווך זמן בדקות לבקרת העלאת קבצים")]
        FilesUploadLemitPeriod = 23,
        [Description("אבטחה - מספר קבצים המותר להעלאה בתווך זמן מוגדר לבקרה")]
        FilesUploadLemitFilesCount = 24,
        [Description("אבטחה - תווך זמן בדקות לבקרת הניסיונות לוגין")]
        LoginLimitPeriod = 25,
        [Description("אבטחה - מספר ניסיונות לוגין בתווך זמן מוגדר לבקרה")]
        LoginLimitAttemptsCount = 26,
        [Description("בדיקות - שם  מנוי")]
        TestUserName = 27,
        [Description("בדיקות - מספר רישוי")]
        TestLicenseNumber = 28,
        [Description("בדיקות - מספר טלפון")]
        TestUserPhone = 29,
        [Description("בדיקות - קוד אימות")]
        TestUserAuthCode = 30,
        //ToDo
        [Description("תאריך תפוגת סמס")]
        SmsExperationInterval = 32




    }
}
