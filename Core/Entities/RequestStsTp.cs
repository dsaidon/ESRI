using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Core.Entities
{
    public class RequestStsTp : BaseRecIdEntity
    {
        [StringLength(50)]
        public string Dsc { get; set; }
    }
}
