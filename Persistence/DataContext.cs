// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Author: Evgeniy Gusev
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Persistence
{
    using Domain;

    using Microsoft.EntityFrameworkCore;

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            
        }
        
        public DbSet<Value> Values { get; set; }
    }
}
