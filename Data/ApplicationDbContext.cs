using Microsoft.EntityFrameworkCore;
using LoginBackend.Models;

namespace LoginBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Definir un único DbSet para la tabla de usuarios y registros
        public DbSet<UsuarioRegistro> UsuariosRegistros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar las tablas
            modelBuilder.Entity<UsuarioRegistro>()
                .ToTable("tbl_usuarios")
                .HasKey(e => e.UsuId);  // Clave primaria de la tabla usuarios

            modelBuilder.Entity<UsuarioRegistro>()
                .ToTable("tbl_registro")
                .HasKey(e => e.RegId);  // Clave primaria de la tabla registro
        }
    }
}
