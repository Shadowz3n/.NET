using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using API.Models;

namespace API.DAL
{
    public class APIContext : DbContext
    {
        public APIContext() : base("APIContext")
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}