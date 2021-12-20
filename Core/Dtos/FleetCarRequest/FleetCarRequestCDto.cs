using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Dtos.FleetCarRequest
{
    public class FleetCarRequestCDto
    {
        public FleetCarRequestCDto() : base()
        {
            isSupplier = false;
            srvManNo = -1;
            fleetCarNo = 0;
        }
        public int srvManNo { get; set; }
        public bool isSupplier { get; set; }
        public int fleetCarNo { get; set; }
   
}
}
