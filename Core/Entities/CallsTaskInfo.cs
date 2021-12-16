using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class CallsTaskInfo:BaseRecIdEntity
    {

		public int? WebLetRecId { get; set; }
		public int CallNo { get; set; }
		public int ExecNo { get; set; }
		public int SrvManNo { get; set; }
		public int SrvCarNo { get; set; }
		[Column(TypeName = "datetime2")]
		public DateTime ClosingTm { get; set; }
		public int? ClosingFualtTp { get; set; }
		public int? ClosingReasonCd { get; set; }
		
		//[Required(AllowEmptyStrings = true)]
		[MaxLength(1000)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string ClosingRem { get; set; }
		public int SrvTp { get; set; }
		public int? ExtraPriceExists { get; set; }
		public double? ExecExtraPrice { get; set; }
		public int? BatterySold { get; set; }
		public double? BatteryPrice { get; set; }
		
		//[Required(AllowEmptyStrings = true)]
		[MaxLength(50)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string BatteryPicPath { get; set; }
		public int? OfficeRemExists { get; set; }
		//[Required(AllowEmptyStrings = true)]
		[MaxLength(1000)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string OfficeRem { get; set; }
		public int? TowingAccident { get; set; }

		//[Required(AllowEmptyStrings = true)]
		[MaxLength(1000)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string SbcCarLocation { get; set; }
		
		//[Required(AllowEmptyStrings = true)]
		[MaxLength(1000)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string SbcCarCrossroadLoc { get; set; }
		public int? CarInvolved { get; set; }
		
		//[Required(AllowEmptyStrings = true)]
		[MaxLength(20)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string InvolvedCarNo { get; set; }
		public int? CarLocOffRoad { get; set; }
		
		//[Required(AllowEmptyStrings = true)]
		[MaxLength(1000)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string OnMyWayRem { get; set; }
		public int? SubCarKm { get; set; }
		public int? SubCarFuelSts { get; set; }

		//[Required(AllowEmptyStrings = true)]
		[MaxLength(50)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string SubDrvNm1 { get; set; }
		public int? SubDrvIdCrtNo1 { get; set; }
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime? SubDrvBirthDt1 { get; set; }
		public int? SubDrvLicenseNo1 { get; set; }
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime? DrvLicenseVldEndDt1 { get; set; }

		//[Required(AllowEmptyStrings = true)]
		[MaxLength(50)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string SubDrvNm2 { get; set; }
		public int? SubDrvIdCrtNo2 { get; set; }
		public DateTime? SubDrvBirthDt2 { get; set; }
		public int? SubDrvLicenseNo2 { get; set; }
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime? DrvLicenseVldEndDt2 { get; set; }
		public int? InvoiceNo { get; set; }
		public int? CreditCardTp { get; set; }
		public int? CardHolderTz { get; set; }
		
		//[Required(AllowEmptyStrings = true)]
		[MaxLength(50)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string CardHolderNm { get; set; }
		public int? CardCvv { get; set; }
		
		//[Required(AllowEmptyStrings = true)]
		[MaxLength(50)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string SignerName { get; set; }
		public int? SignerTz { get; set; }
		public double? ArrivalX { get; set; }
		public double? ArrivalY { get; set; }
		//[Required(AllowEmptyStrings = true)]
		[MaxLength(100)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Pic1 { get; set; }
		[MaxLength(100)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Pic2 { get; set; }
		[MaxLength(100)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Pic3 { get; set; }
		[MaxLength(100)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Pic4 { get; set; }
		[MaxLength(100)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Pic5 { get; set; }
		[MaxLength(100)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Pic6 { get; set; }
		[MaxLength(100)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Pic7 { get; set; }
		[MaxLength(100)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Pic8 { get; set; }
		[MaxLength(100)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Pic9 { get; set; }
		[MaxLength(100)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Pic10 { get; set; }
        [MaxLength(100)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Pic11 { get; set; }
		[MaxLength(100)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Pic12 { get; set; }
		[MaxLength(100)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Pic13 { get; set; }
		[MaxLength(100)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Pic14 { get; set; }
		
		public int? RecSts { get; set; }
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime? CardVldDate { get; set; }
	}
}
