using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class SrvTp : BaseRecIdEntity
    {
        public string Dsc { get; set; }
        public int SrcSrvCd { get; set; }
        public int ActivityTp { get; set; }
    }
}
