using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    /// <summary>
    /// User roles.
    /// </summary>
    public class UserRole
    {
        public int ID { get; set; }

        [Required]
        [Column("Role")]
        [MaxLength(255)]
        public string Role { get; set; }
    }
}
