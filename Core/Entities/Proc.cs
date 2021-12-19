using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Proc:BaseEntity
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Proc()
        {
           
            //this.AuditProcesses = new HashSet<AuditProcess>();
           
        }
        public Nullable<long> lastLoginId { get; set; }
        public System.DateTime creationDate { get; set; }
        public int step { get; set; }
        public Nullable<int> status { get; set; }
        public System.DateTime lastUpdateDate { get; set; }
        public string SessionGuid { get; set; }
        //public Nullable<long> auditProcessId { get; set; }
        public Nullable<int> callNumber { get; set; }
        public Nullable<int> execNumber { get; set; }
        public Nullable<int> SrvManNo { get; set; }
        public Nullable<Boolean> IsSupplier { get; set; }
        public string SrvManNM { get; set; }
        public string SrvManPhoneNo { get; set; }
        public string SbcCarNumber { get; set; }
        public Nullable<bool> confirmation_isUserApproveCondiotions { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public  ICollection<AuditProcess> AuditProcesses { get; set; }
       // public  AuditProcess AuditProcess { get; set; }
        //public  LoginLog LoginLog { get; set; }
        //public  StepType StepType { get; set; }
    }
}
