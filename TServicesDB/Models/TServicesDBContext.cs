using Microsoft.EntityFrameworkCore;

namespace TServicesDB.Models
{
    public class TServicesDBContext : DbContext
    {
        public TServicesDBContext(DbContextOptions<TServicesDBContext> options) : base(options)
        {
        }
        public DbSet<TSRoute> TSRoutes { get; set; }
        public DbSet<TSDriver> TSDrivers { get; set;}
        public DbSet<TSTicket> TSTickets { get; set; }
        public DbSet<TSBus> TSBus { get; set; }
        public DbSet<TSStopover> TSStopovers { get; set; }
    }
}
