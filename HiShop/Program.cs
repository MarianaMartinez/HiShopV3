using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using HiShop.Entity.Data;

namespace HiShop
{

    public class Program
    {


        /// <summary>
        /// En Program.cs , modifique el Mainmétodo para hacer lo siguiente en el inicio de la aplicación:+
        /// Obtenga una instancia de contexto de base de datos del contenedor de inyección de dependencia.
        /// Llame al método de la semilla, pasando a él el contexto.
        /// Deseche el contexto cuando el método de semilla esté terminado.
        /// AxelMolaro
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {

            var host = BuildWebHost(args);
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<HiShopContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
