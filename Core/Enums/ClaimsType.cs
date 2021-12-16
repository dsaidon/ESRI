using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShagApi.Enums
{
    public static class ClaimsType
    {
        public static String supplier { get { return "CanAccessSupplier"; } }
        public static String srvMan { get { return "CanAccessSrvMan"; } }
        public static String admin { get { return "CanAccessAdmin"; } }
        public static String RegularUser { get { return "CanAccessUser"; } }

    }
}
