using Microsoft.EntityFrameworkCore;

namespace DesafioBackend.Model.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Motorcycle> Motos { get; set; }
        public DbSet<DeliveryDriver> Entregadores { get; set; }
        public DbSet<Rental> Locacoes { get; set; }
        public DbSet<MotorcycleNotification> MotorcycleNotifications { get; set; }

    }
}
