using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    /// <summary>
    /// State.
    /// </summary>
    public class State
    {
        [Key]
        public int ID { get; set; }

        [Column("Name")]
        [MaxLength(255)]
        [Required(ErrorMessage = "error.validation.invalid-name")]
        public string Name { get; set; }

        [Column("UF")]
        [MaxLength(10)]
        [Required(ErrorMessage = "error.validation.invalid-uf")]
        public string Uf { get; set; }
    }
}
