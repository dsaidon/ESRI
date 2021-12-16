using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class SrvManExecSts : BaseRecIdEntity
	{
		public int Call_No { get; set; }
		public int Exec_No { get; set; }
		public int Web_Let_Rec_Id { get; set; }
		public SrvManExecStsTp SrvManExecStsTp { get; set; }
		public int SrvManExecStsTpId { get; set; }
		[Column(TypeName = "datetime2")]
		public DateTime Rep_Tm { get; set; }
		public double? Rep_X { get; set; }
		public double? Rep_Y { get; set; }
		public int Srv_Man_No { get; set; }
		public int Srv_Car_No { get; set; }
		public int Rec_Sts { get; set; }
		public int External_Oprr { get; set; }
	}
}
