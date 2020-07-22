using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    /// <summary>
    /// Log.
    /// </summary>
    public class Log
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("Action")]
        [Required(ErrorMessage = "error.validation.invalid-action")]
        public string Action { get; set; }

        [Column("UserID")]
        public int? UserID { get; set; }

        [Column("IP")]
        [Required(ErrorMessage = "error.validation.invalid-ip")]
        public string Ip { get; set; }

        [Column("CreatedAt")]
        [DataType(DataType.DateTime, ErrorMessage = "error.validation.invalid-created-at")]
        public DateTime CreatedAt { get; set; }
    }
}
