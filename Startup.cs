public void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"))); // Conexión a PostgreSQL

    services.AddControllers();
}
