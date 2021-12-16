using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalUtil
{
    public static class DecimalExtentions
    {
        public static decimal DefaultOrValue(this decimal? param, decimal defaultValue = 0)
        {
            return param != null ? param.Value : defaultValue;
        }
    }
}
