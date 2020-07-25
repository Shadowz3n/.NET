using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using API.DAL;
using API.Models;

namespace API.Services
{
    /// <summary>
    /// User service.
    /// </summary>
    public class CityService : DbContext
    {
        private APIContext db = new APIContext();

        /// <summary>
        /// List the specified searchParams.
        /// </summary>
        /// <returns>The list.</returns>
        /// <param name="searchParams">Search parameters.</param>
        public async Task<object> List(SearchParams searchParams)
        {
            City[] Results = await (from e in db.Cities where e.DeletedAt == null select e)
                                .OrderBy(searchParams.LinqOrder)
                                .Skip(searchParams.OffSet)
                                .Take(searchParams.Limit).ToArrayAsync();

            int Total = await (from e in db.Cities where e.DeletedAt == null select e).CountAsync();
            return new { Total, Results };
        }

        public async Task<object> ByStateId(int stateId)
        {
            City[] Results = await (from e in db.Cities where e.DeletedAt == null where e.StateID == stateId select e).ToArrayAsync();

            int Total = await (from e in db.Cities where e.DeletedAt == null select e).CountAsync();
            return new { Total, Results };
        }

        /// <summary>
        /// Dispose the specified disposing.
        /// </summary>
        /// <param name="disposing">If set to <c>true</c> disposing.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}
