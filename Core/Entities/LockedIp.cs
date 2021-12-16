using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class LockedIp: BaseEntity
    {
        public string ip { get; set; }
        public System.DateTime lockedDate { get; set; }
    }
}
