using System.ComponentModel.DataAnnotations;
namespace Core.Entities
{
    public class SrvManAttnTp : BaseRecIdEntity
    {
        [StringLength(50)]
        public string Dsc { get; set; }
    }
}
