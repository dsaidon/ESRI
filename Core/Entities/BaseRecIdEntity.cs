using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
   public class BaseRecIdEntity
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
        [Required()]
        [Key()]
        public int Id { get; set; }
    }
}
