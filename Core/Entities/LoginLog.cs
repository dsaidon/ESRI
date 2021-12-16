using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class LoginLog: BaseEntity
    {
        public LoginLog()
        {
        }
        public Nullable<System.DateTime> creationDate { get; set; }
        [MaxLength(200)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string sessionId { get; set; }
        public int? status { get; set; }
        [MaxLength(200)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ip { get; set; }
        [MaxLength(100)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string SrvManName { get; set; }
        public int SrvManNo { get; set; }
        [MaxLength(20)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string phoneNo { get; set; }
        [MaxLength(50)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string email { get; set; }
        [MaxLength(10)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string smsCode { get; set; }
        public Nullable<int> smsCodeCounter { get; set; }
        public System.DateTime lastUpdateDate { get; set; }
        public Nullable<int> smsCodeRetriesCounter { get; set; }
        public Nullable<System.DateTime> smsCreationDate { get; set; }
        public Boolean isSupplier { get; set; }

    }
}
