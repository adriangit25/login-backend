using Microsoft.EntityFrameworkCore;
using LoginBackend.Models; // Asegúrate de importar el namespace correcto

namespace LoginBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } // Solo Users es necesario ahora
    }
}
