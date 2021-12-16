using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class SrvManAttn :BaseRecIdEntity
    {
		public int SrvManNo { get; set; }
		public SrvManShiftTp SrvManShiftTp { get; set; }
		public int SrvManShiftTpId { get; set; }
		[Column(TypeName = "datetime2")]
		public DateTime ShiftDt { get; set; }
		public double? ShiftX { get; set; }
		public double? ShiftY { get; set; }
		public int SrvCarNo { get; set; }
		public Single? SrvCarKm { get; set; }
		public int RecSts { get; set; }
	}
}
