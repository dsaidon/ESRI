using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class LockedSrvMan: BaseEntity
    {
        public long lockedSrvManNo { get; set; }
        public string SrvManNM { get; set; }
        public Boolean IsSupplier { get; set; }
        public System.DateTime lockedDate { get; set; }
    }
}
