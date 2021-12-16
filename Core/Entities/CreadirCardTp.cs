using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class CreadirCardTp:BaseRecIdEntity
		{

		[StringLength(50)]
		public String Dsc {get;set;}
		
		[StringLength(10)]
		//[Required(AllowEmptyStrings = true)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public String ShortDsc {get;set;}
		
		//[Required(AllowEmptyStrings = true)]
		[StringLength(10)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public String CreditCmpNote {get; set;}

		public int? CardIdentifire {get; set;}

		public int? VisaNoLength {get; set;}
	}
}
