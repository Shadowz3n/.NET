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
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [Column("FirstName")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "error.validation.invalid-name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the lastname.
        /// </summary>
        /// <value>The lastname.</value>
        [Column("LastName")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "error.validation.invalid-lastname")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [Required]
        [MaxLength(255)]
        [Column("Email")]
        [EmailAddress(ErrorMessage = "error.validation.incorrect-email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        [Required]
        [MaxLength(255)]
        [Column("Password")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "error.validation.invalid-password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the city identifier.
        /// </summary>
        /// <value>The city identifier.</value>
        [Column("CityID")]
        [Range(1, 9999999, ErrorMessage = "error.validation.invalid-city-id")]
        public int CityID { get; set; }

        /// <summary>
        /// Gets or sets the state identifier.
        /// </summary>
        /// <value>The state identifier.</value>
        [Column("StateID")]
        [Range(1, 9999999, ErrorMessage = "error.validation.invalid-state-id")]
        public int StateID { get; set; }

        /// <summary>
        /// Gets or sets the cpf.
        /// </summary>
        /// <value>The cpf.</value>
        [Required]
        [MaxLength(20)]
        [Column("CPF")]
        [CpfValidation(ErrorMessage = "error.validation.invalid-cpf")]
        public string Cpf { get; set; }

        /// <summary>
        /// Gets or sets the cnpj.
        /// </summary>
        /// <value>The cnpj.</value>
        [Column("CNPJ")]
        [CnpjValidation(ErrorMessage = "error.validation.invalid-cnpj")]
        public string Cnpj { get; set; }

        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        /// <value>The role identifier.</value>
        [Required]
        [Column("RoleID")]
        [Range(1, 9999999, ErrorMessage = "error.validation.invalid-role-id")]
        public int RoleID { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>The role.</value>
        [NotMapped]
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets the jwt token.
        /// </summary>
        /// <value>The jwt token.</value>
        [NotMapped]
        [Required(ErrorMessage = "error.validation.invalid-jwt-token")]
        public string JwtToken { get; set; }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>The token.</value>
        [NotMapped]
        [Required(ErrorMessage = "error.validation.invalid-token")]
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the accept releases.
        /// </summary>
        /// <value>The accept releases.</value>
        [Column("AcceptReleases")]
        public int? AcceptReleases { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>The created at.</value>
        [Column("CreatedAt")]
        [DataType(DataType.DateTime, ErrorMessage = "error.validation.invalid-created-at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the updated at.
        /// </summary>
        /// <value>The updated at.</value>
        [Column("UpdatedAt")]
        [DataType(DataType.DateTime, ErrorMessage = "error.validation.invalid-updated-at")]
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets the deleted at.
        /// </summary>
        /// <value>The deleted at.</value>
        [Column("DeletedAt")]
        [DataType(DataType.DateTime, ErrorMessage = "error.validation.invalid-deleted-at")]
        public DateTime? DeletedAt { get; set; }
    }

    /// <summary>
    /// User register.
    /// </summary>
    public class UserRegister
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [Column("Name")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "error.validation.invalid-name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the lastname.
        /// </summary>
        /// <value>The lastname.</value>
        [Column("Lastname")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "error.validation.invalid-lastname")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [Column("Email")]
        [Required(ErrorMessage = "error.validation.invalid-email")]
        [EmailAddress(ErrorMessage = "error.validation.incorrect-email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        [Column("Password")]
        [Required(ErrorMessage = "error.validation.invalid-password")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "error.validation.invalid-password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the check password.
        /// </summary>
        /// <value>The check password.</value>
        [NotMapped]
        [Compare("Password", ErrorMessage = "error.validation.password-compare")]
        [Required(ErrorMessage = "error.validation.password-compare")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "error.validation.password-compare")]
        public string CheckPassword { get; set; }

        /// <summary>
        /// Gets or sets the city identifier.
        /// </summary>
        /// <value>The city identifier.</value>
        [Column("CityID")]
        [Range(1, 9999999, ErrorMessage = "error.validation.invalid-city-id")]
        public int CityID { get; set; }

        /// <summary>
        /// Gets or sets the state identifier.
        /// </summary>
        /// <value>The state identifier.</value>
        [Column("StateID")]
        [Range(1, 9999999, ErrorMessage = "error.validation.invalid-state-id")]
        public int StateID { get; set; }

        /// <summary>
        /// Gets or sets the cpf.
        /// </summary>
        /// <value>The cpf.</value>
        [Column("CPF")]
        [CpfValidation(ErrorMessage = "error.validation.invalid-cpf")]
        public string Cpf { get; set; }

        /// <summary>
        /// Gets or sets the cnpj.
        /// </summary>
        /// <value>The cnpj.</value>
        [Column("CNPJ")]
        [CnpjValidation(ErrorMessage = "error.validation.invalid-cnpj")]
        public string Cnpj { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>The phone.</value>
        [Column("Phone")]
        [Required(ErrorMessage = "error.validation.invalid-phone")]
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>The token.</value>
        [NotMapped]
        [Required(ErrorMessage = "error.validation.invalid-token")]
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        /// <value>The role identifier.</value>
        [Column("RoleID")]
        [Range(1, 9999999, ErrorMessage = "error.validation.invalid-role-id")]
        public int RoleID { get; set; }

        /// <summary>
        /// Gets or sets the accept releases.
        /// </summary>
        /// <value>The accept releases.</value>
        [Column("AcceptReleases")]
        public int? AcceptReleases { get; set; }
    }

    /// <summary>
    /// User register response.
    /// </summary>
    public class UserRegisterResponse
    {
        /// <summary>
        /// Gets or sets the type of the token.
        /// </summary>
        /// <value>The type of the token.</value>
        public string TokenType { get; set; }

        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        /// <value>The access token.</value>
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:API.Models.UserRegisterResponse"/> error email.
        /// </summary>
        /// <value><c>true</c> if error email; otherwise, <c>false</c>.</value>
        public bool ErrorEmail { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:API.Models.UserRegisterResponse"/> error cpf.
        /// </summary>
        /// <value><c>true</c> if error cpf; otherwise, <c>false</c>.</value>
        public bool ErrorCpf { get; set; }
    }

    /// <summary>
    /// User add response.
    /// </summary>
    public class UserAddResponse
    {
        /// <summary>
        /// Gets or sets the type of the token.
        /// </summary>
        /// <value>The type of the token.</value>
        public string TokenType { get; set; }

        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        /// <value>The access token.</value>
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:API.Models.UserAddResponse"/> error email.
        /// </summary>
        /// <value><c>true</c> if error email; otherwise, <c>false</c>.</value>
        public bool ErrorEmail { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:API.Models.UserAddResponse"/> error cpf.
        /// </summary>
        /// <value><c>true</c> if error cpf; otherwise, <c>false</c>.</value>
        public bool ErrorCpf { get; set; }
    }

    /// <summary>
    /// User edit response.
    /// </summary>
    public class UserEditResponse
    {

        /// <summary>
        /// Gets or sets the type of the token.
        /// </summary>
        /// <value>The type of the token.</value>
        public string TokenType { get; set; }

        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        /// <value>The access token.</value>
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:API.Models.UserEditResponse"/> error identifier.
        /// </summary>
        /// <value><c>true</c> if error identifier; otherwise, <c>false</c>.</value>
        public bool ErrorId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:API.Models.UserEditResponse"/> error email.
        /// </summary>
        /// <value><c>true</c> if error email; otherwise, <c>false</c>.</value>
        public bool ErrorEmail { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:API.Models.UserEditResponse"/> error cpf.
        /// </summary>
        /// <value><c>true</c> if error cpf; otherwise, <c>false</c>.</value>
        public bool ErrorCpf { get; set; }
    }

    /// <summary>
    /// User login.
    /// </summary>
    public class UserLogin
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        [Required]
        [EmailAddress(ErrorMessage = "error.validation.incorrect-email")]
        [MaxLength(255, ErrorMessage = "error.validation.incorrect-email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        [Required]
        [MaxLength(255, ErrorMessage = "error.validation.invalid-password")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "error.validation.invalid-password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>The token.</value>
        [NotMapped]
        [Required(ErrorMessage = "error.validation.invalid-token")]
        public string Token { get; set; }
    }
}
