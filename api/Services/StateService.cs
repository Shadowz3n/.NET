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
    /// User service.
    /// </summary>
    public class StateService : DbContext
    {
        private APIContext db = new APIContext();

        /// <summary>
        /// List the specified searchParams.
        /// </summary>
        /// <returns>The list.</returns>
        /// <param name="searchParams">Search parameters.</param>
        public async Task<object> List(SearchParams searchParams)
        {
            State[] Results = await (from e in db.States where e.DeletedAt == null select e)
                                .OrderBy(searchParams.LinqOrder)
                                .Skip(searchParams.OffSet)
                                .Take(searchParams.Limit).ToArrayAsync();

            int Total = await (from e in db.States where e.DeletedAt == null select e).CountAsync();
            return new { Total, Results };
        }

        /// <summary>
        /// Add the specified state.
        /// </summary>
        /// <returns>The add.</returns>
        /// <param name="state">State.</param>
        public async Task<object> AddAndEdit(State state)
        {
            db.States.Add(state);
            await db.SaveChangesAsync();

            // Save Log
            int userId = int.Parse(Thread.CurrentPrincipal.Identity.Name);

            int id = int.TryParse(state.ID.ToString(), out id) ? id : 0;
            string action = id == 0 ? "user.add.state" : "user.edit.state";

            Log log = new Log
            {
                UserID = userId,
                Action = action
            };
            await new LogService().Save(log);

            return state.ID;
        }

        public async Task<object> Delete(int id)
        {
            // Check if user id exists
            State[] state = await (from s in db.States
                                 where s.ID == id
                                 select s).Take(1).ToArrayAsync();

            if (!state.Any())
                return false;

            // Save Log
            int userId = int.Parse(Thread.CurrentPrincipal.Identity.Name);
            Log log = new Log
            {
                UserID = userId,
                Action = "user.delete.state"
            };
            await new LogService().Save(log);

            state.FirstOrDefault().DeletedAt = DateTime.Now;
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
