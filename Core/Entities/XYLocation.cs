using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class XYLocation :BaseRecIdEntity
    {
		
		public int CallNo { get; set; }
		public int ExecNo { get; set; }
		public int SrvManNo { get; set; }
		public int SrvCarNo { get; set; }
		public double LocX { get; set; }
		public double LocY { get; set; }
		[Column(TypeName = "datetime2")]
		public DateTime RepTm { get; set; }
		public int RecSts { get; set; }
	}
}
