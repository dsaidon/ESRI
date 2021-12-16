using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ShagApi.Enums
{

    public enum ComboTypes
    {

        [Description("None")]
        None = 0,
        [Description("Shiftment Type")]
        shiftTp = 1,
        [Description("Srvice Type")]
        srvTp = 2,
        [Description("Fualt Type")]
        faultTp = 3,
        [Description("Closing Cuase Type")]
        closingCuase = 4,
    }
}