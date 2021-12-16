using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class FaultTp : BaseRecIdEntity
    {
        [StringLength(50)]
        public string Dsc { get; set; }
    }
}
