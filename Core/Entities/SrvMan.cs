using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    //[DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
    //[Required()]
    //[Key()]
    //public int SrvManNo { get; set; }
    public class SrvMan:BaseRecIdEntity
    {
        [MaxLength(50)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string FirstNm { get; set; }

        [MaxLength(20)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string LastNm { get; set; }
        public SrvManTp SrvManTp { get; set; }
        public int SrvManTpId { get; set; }

        [MaxLength(15)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string PhoneNo { get; set; }
    }
}
