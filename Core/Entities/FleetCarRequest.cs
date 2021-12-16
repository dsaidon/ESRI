using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class FleetCarRequest :BaseRecIdEntity
    {

		public int? CallNo { get; set; }
		public int? ExecNo { get; set; }
		public SrcSrv SrcSrv { get; set; }
		public int? SrcSrvId { get; set; }
		public SrvTp SrvTp { get; set; }
		public int? SrvTpId { get; set; }

		//[Required(AllowEmptyStrings = true)]
		[MaxLength(100)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string SrvTpDsc { get; set; }
		public int? SrvManNo { get; set; }
		public int? ExternalOprr { get; set; }
		[Column(TypeName = "datetime2")]
		public DateTime? ExecApxTm { get; set; }
		//[Required(AllowEmptyStrings = true)]
		[MaxLength(20)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string SbcCarNo { get; set; }
		public int SrvCarNo { get; set; }
		[MaxLength(250)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string SrcAddress { get; set; }
		public double? SrcX { get; set; }
		public double? SrcY { get; set; }
		//[Required(AllowEmptyStrings = true)]
		[MaxLength(250)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string DestAddress { get; set; }
		public double? DestX { get; set; }
		public double? DestY { get; set; }
		//[Required(AllowEmptyStrings = true)]
		[MaxLength(100)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string GarageNm { get; set; }
		[Column(TypeName = "datetime2")]
		public DateTime? CltGivenExecTm { get; set; }
		//[Required(AllowEmptyStrings = true)]
		[MaxLength(50)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string SbcCarTpDsc { get; set; }
		//[Required(AllowEmptyStrings = true)]
		[MaxLength(200)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string SbcCarInfo { get; set; }
		[MaxLength(50)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string FaultTpDsc { get; set; }
		[MaxLength(50)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string CallReceiverEmpNm { get; set; }
		[MaxLength(50)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string DispatcherEmpNm { get; set; }
		[MaxLength(200)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string SbcNm { get; set; }
		[MaxLength(100)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string SbcCmpNm { get; set; }
		public int? SbcIdCrtNo { get; set; }

		[MaxLength(45)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string WaitNm { get; set; }

		[MaxLength(15)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string SbcMobilePhoneNo { get; set; }

		[MaxLength(15)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string SbcContactPhone { get; set; }

		[MaxLength(200)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string SbcCarLocDsc { get; set; }

		[MaxLength(200)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string KeyLocDsc { get; set; }

		[MaxLength(1000)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string ExecReminderRem { get; set; }

		[MaxLength(1000)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string OfficeRem { get; set; }
		public double? ExecPrice { get; set; }
		public int? SubCar { get; set; }
		public int? SubContractNo { get; set; }
		[Column(TypeName = "datetime2")]
		public DateTime? SubAdmissionTm { get; set; }
		[Column(TypeName = "datetime2")]
		public DateTime? SubReturnTm { get; set; }
		[MaxLength(100)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string SubReturnDestAddrr { get; set; }
		public double? SubDailyRentPrice { get; set; }
		[MaxLength(50)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string SubDrvNm { get; set; }
		public int? SubDrvIdCrtNo { get; set; }
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime? SubDrvBirthDt { get; set; }
		public int? SubDrvLicenseNo { get; set; }
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime? DrvLicenseReceiptDt { get; set; }
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
		public DateTime? DrvLicenseVldEndDt { get; set; }
		public int? Sts { get; set; }
		public int? ExecSeq { get; set; }
		public int? DipatcherEmpNo { get; set; }
		public Area AreaSrc { get; set; }
		public int SbcCarAreaId { get; set; }
		[MaxLength(50)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string SbcCarAreaDsc { get; set; }
		public Area AreaDest { get; set; }
		public int? DestAreaId { get; set; }

		[MaxLength(50)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string DestAreaDsc { get; set; }
		public int? CallCustomer { get; set; }
		[Column(TypeName = "datetime2")]
		public DateTime? CreateTm { get; set; }
		public int? ShowCallInfo { get; set; }
		[MaxLength(1000)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string UpdateNotification { get; set; }
	}
}
