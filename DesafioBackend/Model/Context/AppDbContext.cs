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
        
        public DbSet<Moto> Motos { get; set; }
    }
}
