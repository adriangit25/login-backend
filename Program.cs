using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using LoginBackend.Data;  // Asegúrate de incluir este using para el DbContext

namespace LoginBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices((context, services) =>
                    {
                        // Configura el DbContext usando la cadena de conexión desde appsettings.json
                        services.AddDbContext<ApplicationDbContext>(options =>
                            options.UseNpgsql(context.Configuration.GetConnectionString("DefaultConnection"))); // Usando appsettings.json
                        services.AddControllers();
                    })
                    .Configure(app =>
                    {
                        app.UseRouting();

                        // Agrega los middlewares para autorización y controladores
                        app.UseAuthorization();
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllers();
                        });
                    });
                });
    }
}
