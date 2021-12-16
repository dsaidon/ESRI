using System.ComponentModel.DataAnnotations;
namespace Core.Entities
{
    public class ClosingReason : BaseRecIdEntity
    {
        [StringLength(50)]
        public string Dsc { get; set; }
    }
}
