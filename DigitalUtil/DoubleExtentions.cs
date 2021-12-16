using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalUtil
{
    public static class DoubleExtentions
    {
        public static double DefaultOrValue(this double? param, double defaultValue = 0)
        {
            return param != null ? param.Value : defaultValue;
        }
    }
}
