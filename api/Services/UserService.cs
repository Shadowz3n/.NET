using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using API.DAL;
using API.Models;

namespace API.Services
{
    public class UserService : DbContext
    {
        private APIContext db = new APIContext();

        public async Task<object> List(SearchParams searchParams)
        {
            User[] Results = await (from e in db.Users where e.DeletedAt == null where e.Email.Contains(searchParams.Search) select e)
                                .OrderBy(searchParams.LinqOrder)
                                .Skip(searchParams.OffSet)
                                .Take(searchParams.Limit).ToArrayAsync();

            int Total = await (from e in db.Users where e.DeletedAt == null where e.Email.Contains(searchParams.Search) select e).CountAsync();
            return new { Total, Results };
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                this.db.Dispose();

            base.Dispose(disposing);
        }
    }
}
