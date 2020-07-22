using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    /// <summary>
    /// Search parameters.
    /// </summary>
    public class SearchParams
    {
        [Range(1, 99999, ErrorMessage = "error.validation.invalid-page")]
        public int Page { get; set; } = 1;

        [Range(1, 99999, ErrorMessage = "error.validation.invalid-limit")]
        public int Limit { get; set; } = 20;

        [MaxLength(255, ErrorMessage = "error.validation.invalid-search")]
        public string Search { get; set; } = "";

        [NotMapped]
        public int OffSet
        {
            get
            {
                return (Page - 1) * Limit;
            }
        }

        public string OrderBy { get; set; } = "ID";
        public string Order { get; set; }

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
