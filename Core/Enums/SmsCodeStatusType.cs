using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ShagApi.Enums
{
    public enum SmsCodeStatusType
    {
        [Description("Over Max Retries, License Is Locked")]
        OverMaxRetries = 1,
        [Description("User Is Not On SMS Code Status")]
        NotOnSmsCodeStatus = 2,
        [Description("Sent New Code")]
        SentNewCode = 3,
        [Description("SMS Authentication Passed")]
        AuthenticationPassed = 4,
        [Description("Empty SMS Code")]
        EmptySmsCode = 5,
        [Description("ErrorOnSendingCodeSms")]
        ErrorOnSendingCodeSms = 6,
        [Description("Wrong Code")]
        WrongCode = 7,
        [Description("Code Expired")]
        CodeExpired = 8,
        [Description("Login not found")]
        LoginNotFound = 9,
        [Description("resend a New sms Code")]
        ReSendNewSmsCode = 10,
    }
}
