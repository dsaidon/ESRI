using System.ComponentModel.DataAnnotations;
namespace Core.Entities
{
    public class SrvManExecStsTp : BaseRecIdEntity
    {
        [StringLength(50)]
        public string Dsc { get; set; }
    }
}
