using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ShagApi.Enums
{
    public enum AuthenticationStatusTypeAman
    {
        [Description("Locked User")]
        LockedUser = 1,
        [Description("Authentication Questions")]
        AuthenticationQuestions = 2,
        [Description("Authentication SMS")]
        AuthenticationSMS = 3,
        [Description("Authentication Passed")]
        AuthenticationPassed = 4,
        [Description("Not Subscribed")]
        UserNotSubsecribed = 5,
        [Description("Short Authentication Passed")]
        ShortAuthenticationPassed = 6
    }
}
