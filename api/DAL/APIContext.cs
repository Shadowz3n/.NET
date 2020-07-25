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

        /// <summary>
        /// Gets or sets the user roles.
        /// </summary>
        /// <value>The user roles.</value>
        public DbSet<UserRole> UserRoles { get; set; }

        /// <summary>
        /// Gets or sets the states.
        /// </summary>
        /// <value>The states.</value>
        public DbSet<State> States { get; set; }

        /// <summary>
        /// Gets or sets the cities.
        /// </summary>
        /// <value>The cities.</value>
        public DbSet<City> Cities { get; set; }

        /// <summary>
        /// Gets or sets the logs.
        /// </summary>
        /// <value>The logs.</value>
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