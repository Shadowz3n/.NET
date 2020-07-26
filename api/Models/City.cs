using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    /// <summary>
    /// State.
    /// </summary>
    public class City
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
        [Column("Name")]
        [MaxLength(255)]
        [Required(ErrorMessage = "error.validation.invalid-name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the state identifier.
        /// </summary>
        /// <value>The state identifier.</value>
        [Column("StateID")]
        [Range(1, 9999999, ErrorMessage = "error.validation.invalid-state-id")]
        [Required(ErrorMessage = "error.validation.invalid-state-id")]
        public int StateID { get; set; }

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
}
