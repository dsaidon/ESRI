using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DigitalUtil
{
    public static class DataRowExtentions
    {
        public static string Column(this DataRow row, string columnName)
        {
            return row[columnName].ToString();
        }
    }
}
