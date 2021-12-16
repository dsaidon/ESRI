using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ShagApi.Enums
{
    public enum ProcessStepType
    {
        [Description("Login")]
        Login = -1,
        [Description("None")]
        None = 0,
        [Description("Emp_attn")]
        Emp_attn = 1,
        [Description("Emp_attn")]
        srvManCallsList = 2,
        [Description("Exec")]
        onMyWay = 3,
        [Description("srvMan_OntheWay")]
        arrived = 4,
        [Description("srvMan_Arrived")]
        insertRoadCallData = 5,
        [Description("closing_Call")]
        closing_Call = 6,
        [Description("Emp_attn_Start_Break")]
        Emp_attn_Start_Break = 7,
        [Description("Emp_attn_End_Break")]
        Emp_attn_End_Break = 8,
        [Description("Emp_attn_Exit")]
        Emp_attn_Exit = 9,
        [Description("CallsListMenu")]
        CallsListMenu = 10
    }

    
    public  enum  CallActionType
    {
        
        [Description("None")]
        None = 0,
        [Description("wait")]
        wait = 1,
        [Description("receivePassive")]
        receivePassive = 2,
        [Description("ApproveActive")]
        ApproveActive = 3,
        [Description("rejectActive")]
        rejectActive = 4,
        [Description("onMyWay")]
        onMyWay = 5,
        [Description("arrivedToSbc")]
        arrivedToSbc = 6,
        [Description("onMyWayToDest")]
        onMyWayToDest = 7,
        [Description("Finished")]
        Finished = 8,
        

    }


}
