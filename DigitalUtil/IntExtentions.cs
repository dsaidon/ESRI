using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalUtil
{
    public static class IntExtentions
    {
        public static int DefaultOrValue(this int? param, int defaultValue = 0)
        {
            return param != null ? param.Value : defaultValue;
        }

    }
}
