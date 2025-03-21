using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    [Table("Greetings")]
    public class GreetingEntity
    {
        [Key]
        public Guid GreetingId { get; set; }

        [Required]
        public string? Message { get; set; }

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }

        public virtual UserEntity? User { get; set; }
    }
}
