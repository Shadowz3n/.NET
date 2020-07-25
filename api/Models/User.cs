using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API.Utils.Validation;

namespace API.Models
{
    /// <summary>
    /// User.
    /// </summary>
    public class User
    {
        [Key]
        public int ID { get; set; }

        [Column("Name")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "error.validation.invalid-name")]
        public string Name { get; set; }

        [Column("Lastname")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "error.validation.invalid-lastname")]
        public string Lastname { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("Email")]
        [EmailAddress(ErrorMessage = "error.validation.incorrect-email")]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("Password")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "error.validation.invalid-password")]
        public string Password { get; set; }

        [Column("CityID")]
        [Range(1, 9999999, ErrorMessage = "error.validation.invalid-city-id")]
        public int CityID { get; set; }

        [Column("StateID")]
        [Range(1, 9999999, ErrorMessage = "error.validation.invalid-state-id")]
        public int StateID { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("CPF")]
        [CpfValidation(ErrorMessage = "error.validation.invalid-cpf")]
        public string Cpf { get; set; }

        [Column("CNPJ")]
        [CnpjValidation(ErrorMessage = "error.validation.invalid-cnpj")]
        public string Cnpj { get; set; }

        [Required]
        [Column("RoleID")]
        [Range(1, 9999999, ErrorMessage = "error.validation.invalid-role-id")]
        public int RoleID { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "error.validation.invalid-jwt-token")]
        public string JwtToken { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "error.validation.invalid-token")]
        public string Token { get; set; }

        [Column("AcceptReleases")]
        public bool AcceptReleases { get; set; }

        [Column("CreatedAt")]
        [DataType(DataType.DateTime, ErrorMessage = "error.validation.invalid-created-at")]
        public DateTime CreatedAt { get; set; }

        [Column("UpdatedAt")]
        [DataType(DataType.DateTime, ErrorMessage = "error.validation.invalid-updated-at")]
        public DateTime? UpdatedAt { get; set; }

        [Column("DeletedAt")]
        [DataType(DataType.DateTime, ErrorMessage = "error.validation.invalid-deleted-at")]
        public DateTime? DeletedAt { get; set; }
    }

    public class UserRegister
    {
        [Column("Name")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "error.validation.invalid-name")]
        public string Name { get; set; }

        [Column("Lastname")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "error.validation.invalid-lastname")]
        public string Lastname { get; set; }

        [Column("Email")]
        [Required(ErrorMessage = "error.validation.invalid-email")]
        [EmailAddress(ErrorMessage = "error.validation.incorrect-email")]
        public string Email { get; set; }

        [Column("Password")]
        [Required(ErrorMessage = "error.validation.invalid-password")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "error.validation.invalid-password")]
        public string Password { get; set; }

        [NotMapped]
        [Compare("Password", ErrorMessage = "error.validation.password-compare")]
        [Required(ErrorMessage = "error.validation.password-compare")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "error.validation.password-compare")]
        public string CheckPassword { get; set; }

        [Column("CityID")]
        [Range(1, 9999999, ErrorMessage = "error.validation.invalid-city-id")]
        public int CityID { get; set; }

        [Column("StateID")]
        [Range(1, 9999999, ErrorMessage = "error.validation.invalid-state-id")]
        public int StateID { get; set; }

        [Column("CPF")]
        [CpfValidation(ErrorMessage = "error.validation.invalid-cpf")]
        public string Cpf { get; set; }

        [Column("CNPJ")]
        [CnpjValidation(ErrorMessage = "error.validation.invalid-cnpj")]
        public string Cnpj { get; set; }

        [Column("Phone")]
        [Required(ErrorMessage = "error.validation.invalid-phone")]
        public string Phone { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "error.validation.invalid-token")]
        public string Token { get; set; }

        [Column("RoleID")]
        public int RoleID { get; set; }

        [Column("AcceptReleases")]
        public bool AcceptReleases { get; set; }
    }

    /// <summary>
    /// User register response.
    /// </summary>
    public class UserRegisterResponse
    {
        public string TokenType { get; set; }
        public string AccessToken { get; set; }
        public bool ErrorEmail { get; set; }
        public bool ErrorCpf { get; set; }
    }

    /// <summary>
    /// User login.
    /// </summary>
    public class UserLogin
    {
        [Required]
        [EmailAddress(ErrorMessage = "error.validation.incorrect-email")]
        [MaxLength(255, ErrorMessage = "error.validation.incorrect-email")]
        public string Email { get; set; }

        [Required]
        [MaxLength(255, ErrorMessage = "error.validation.invalid-password")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "error.validation.invalid-password")]
        public string Password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "error.validation.invalid-token")]
        public string Token { get; set; }
    }
}
