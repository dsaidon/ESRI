using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class SiteParam 
    {
		[DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
		[Required()]
		[Key()]
		public int Id { get; set; }
		public string Name { get; set; }
		public string hebrewName { get; set; }
		[MaxLength(1000)]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string stringValue { get; set; }
		public int? intValue { get; set; }
		public double? decimalValue { get; set; }
		[Column(TypeName = "datetime2")]
		public DateTime? updateDate { get; set; }
	}
}
