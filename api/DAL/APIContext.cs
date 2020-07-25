using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using API.Models;

namespace API.DAL
{
    /// <summary>
    /// API Context.
    /// </summary>
    public class APIContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:API.DAL.APIContext"/> class.
        /// </summary>
        public APIContext() : base("APIContext")
        {
        }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>The users.</value>
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Log> Logs { get; set; }

        /// <summary>
        /// Ons the model creating.
        /// </summary>
        /// <param name="modelBuilder">Model builder.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}