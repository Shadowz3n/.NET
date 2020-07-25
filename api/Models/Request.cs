using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    /// <summary>
    /// Search parameters.
    /// </summary>
    public class SearchParams
    {
        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>The page.</value>
        [Range(1, 99999, ErrorMessage = "error.validation.invalid-page")]
        public int Page { get; set; } = 1;

        /// <summary>
        /// Gets or sets the limit.
        /// </summary>
        /// <value>The limit.</value>
        [Range(1, 99999, ErrorMessage = "error.validation.invalid-limit")]
        public int Limit { get; set; } = 20;

        /// <summary>
        /// Gets or sets the search.
        /// </summary>
        /// <value>The search.</value>
        [MaxLength(255, ErrorMessage = "error.validation.invalid-search")]
        public string Search { get; set; } = "";

        /// <summary>
        /// Gets the off set.
        /// </summary>
        /// <value>The off set.</value>
        [NotMapped]
        public int OffSet
        {
            get
            {
                return (Page - 1) * Limit;
            }
        }

        /// <summary>
        /// Gets or sets the order by.
        /// </summary>
        /// <value>The order by.</value>
        public string OrderBy { get; set; } = "ID";

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        /// <value>The order.</value>
        public string Order { get; set; }

        /// <summary>
        /// Gets the linq order.
        /// </summary>
        /// <value>The linq order.</value>
        [NotMapped]
        public string LinqOrder
        {
            get
            {
                return OrderBy + " " + Order;
            }
        }
    }
}
