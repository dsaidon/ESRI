using DigitalUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ShagApi.Enums
{
    public enum ProcessStepTypeAman
    {
        [IsInDigitalProcess(false)]
        [Description("None")]
        None = 0,

        //[IsInDigitalProcess(true)]
        //[Description("Open Digital Process")]
        //OpenDigitalProcess = 10,

        [IsInDigitalProcess(true)]
        [Description("Wizard Questions")]
        WizardQuestions = 1,

        [IsInDigitalProcess(true)]
        [Description("Location")]
        Location = 2,

        [IsInDigitalProcess(true)]
        [Description("Destination")]
        Destination = 3,

        [IsInDigitalProcess(true)]
        [Description("Time")]
        Time = 4,

        [IsInDigitalProcess(true)]
        [Description("Conditions Confirmation")]
        ConditionsConfirmation = 5,

        [IsInDigitalProcess(false)]
        [Description("Waiting For Provider")]
        WaitingForProvider = 6,

        [IsInDigitalProcess(false)]
        [Description("Open Phone Process")]
        OpenPhoneProcess = 100,

        [IsInDigitalProcess(false)]
        [Description("Display Call Info")]
        DisplayCallInfo = 200,

        [IsInDigitalProcess(false)]
        [Description("Feedback")]
        Feedback = 300,

        [IsInDigitalProcess(false)]
        [Description("Closed Call")]
        ClosedCall = 400,

        [IsInDigitalProcess(false)]
        [Description("Cancelled Call")]
        Cancelled = 600,

        [IsInDigitalProcess(false)]
        [Description("Has Previous Call")]
        HasPrevCall = 500
    }

}
