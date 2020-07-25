using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading;
using System.Threading.Tasks;
using API.DAL;
using API.Models;

namespace API.Services
{
    /// <summary>
    /// City service.
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

        /// <summary>
        /// Bies the state identifier.
        /// </summary>
        /// <returns>The state identifier.</returns>
        /// <param name="stateId">State identifier.</param>
        public async Task<object> ByStateId(int stateId)
        {
            City[] Results = await (from e in db.Cities where e.DeletedAt == null where e.StateID == stateId select e).ToArrayAsync();

            int Total = await (from e in db.Cities where e.DeletedAt == null select e).CountAsync();
            return new { Total, Results };
        }

        /// <summary>
        /// Adds the and edit.
        /// </summary>
        /// <returns>The and edit.</returns>
        /// <param name="city">City.</param>
        public async Task<object> AddAndEdit(City city)
        {
            db.Cities.Add(city);
            await db.SaveChangesAsync();

            // Save Log
            int userId = int.Parse(Thread.CurrentPrincipal.Identity.Name);
            int id = int.TryParse(city.ID.ToString(), out id) ? id : 0;
            string action = id == 0 ? "user.add.state" : "user.edit.state";

            Log log = new Log
            {
                UserID = userId,
                Action = action
            };
            await new LogService().Save(log);

            return city.ID;
        }

        /// <summary>
        /// Delete the specified id.
        /// </summary>
        /// <returns>The delete.</returns>
        /// <param name="id">Identifier.</param>
        public async Task<object> Delete(int id)
        {
            // Check if user id exists
            City[] city = await (from c in db.Cities
                                   where c.ID == id
                                   select c).Take(1).ToArrayAsync();

            if (!city.Any())
                return false;

            // Save Log
            int userId = int.Parse(Thread.CurrentPrincipal.Identity.Name);
            Log log = new Log
            {
                UserID = userId,
                Action = "user.delete.city"
            };
            await new LogService().Save(log);

            city.FirstOrDefault().DeletedAt = DateTime.Now;
            await db.SaveChangesAsync();
            return true;
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
