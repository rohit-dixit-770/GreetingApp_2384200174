using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    [Table("Users")]
    public class UserEntity
    {
        [Key]
        public Guid? UserId { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public byte[]? Salt { get; set; }

        public ICollection<GreetingEntity>? Greetings { get; set; }
    }
}
