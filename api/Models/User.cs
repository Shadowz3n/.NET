using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        [Column("Name")]
        [MaxLength(255)]
        [Required(ErrorMessage = "error.validation.invalid-name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "error.validation.incorrect-email")]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("Password")]
        public string Password { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("CPF")]
        public string Cpf { get; set; }

        [Required]
        [Column("RoleID")]
        [Range(1, 9999, ErrorMessage = "error.validation.invalid-role-id")]
        public int RoleID { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "error.validation.invalid-jwt-token")]
        public string JwtToken { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "error.validation.invalid-token")]
        public string Token { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "error.validation.invalid-created-at")]
        public DateTime CreatedAt { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "error.validation.invalid-updated-at")]
        public DateTime UpdatedAt { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "error.validation.invalid-deleted-at")]
        public DateTime? DeletedAt { get; set; }
    }

    public class UserLogin
    {
        [Required]
        [MaxLength(255, ErrorMessage = "error.validation.incorrect-email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "error.validation.incorrect-email")]
        public string Email { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "error.validation.invalid-password")]
        public string Password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "error.validation.invalid-token")]
        public string UToken { get; set; }
    }

    public class UserRoles
    {
        public int ID { get; set; }

        [Required]
        [Column("Role")]
        [MaxLength(255)]
        public string Role { get; set; }
    }
}
