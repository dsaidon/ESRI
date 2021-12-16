using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models.Auth
{
    public class AuthCode
    {
        public AuthCode(long SrvManNo) : base()
        {
            //ErrID = false;
            srvManNo = SrvManNo;
            nextStep = 0;
            SmsCodeStatusType = 0;
            fleetCarNo = 0;
            isStartBreak = false;
            isShiftStarted = false;
            isSupplier = false;
            startShift = DateTime.Now;
            shiftBreak = DateTime.Now;
            Success = false;
            ErrDesc = "";

        }
        public long srvManNo { get; set; }
        public long fleetCarNo { get; set; }
        public int nextStep { get; set; }
        public int SmsCodeStatusType { get; set; }
        public bool isStartBreak { get; set; }
        public bool isShiftStarted{ get; set; }
        public DateTime startShift { get; set; }
        public DateTime shiftBreak { get; set; }
        public bool isSupplier { get; set; }

        public bool Success { get; set; }
        public String ErrDesc { get; set; }

        







    }
}
