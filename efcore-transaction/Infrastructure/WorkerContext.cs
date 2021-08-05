using efcore_transaction.Entities;
using Microsoft.EntityFrameworkCore;

namespace efcore_transaction.Infrastructure
{
    public class WorkerContext : DbContext
    {
        public WorkerContext(DbContextOptions<WorkerContext> options)
         : base(options)
        { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
