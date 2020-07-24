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
        public string Action { get; set; }

        [Column("UserID")]
        public int? UserID { get; set; }

        [Column("IP")]
        public string Ip { get; set; }

        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }
    }
}
