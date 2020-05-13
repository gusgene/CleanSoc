// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Author: Evgeniy Gusev
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Persistence
{
    using Domain;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class DataContext : IdentityDbContext<AppUser>
    {
        #region Properties

        public DbSet<Value> Values { get; set; }

        public DbSet<Activity> Activities { get; set; }

        #endregion

        #region Constructors

        public DataContext(DbContextOptions options)
            : base(options)
        {
        }

        #endregion

        #region Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Value>().HasData(
                new Value
                {
                    Id = 1,
                    Name = "Value 101"
                },
                new Value
                {
                    Id = 2,
                    Name = "Value 102"
                },
                new Value
                {
                    Id = 3,
                    Name = "Value 103"
                },
                new Value
                {
                    Id = 4,
                    Name = "Value 104"
                },
                new Value
                {
                    Id = 5,
                    Name = "Value 105"
                });
        }

        #endregion
    }
}
