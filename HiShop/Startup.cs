using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using HiShop.Entity.Data;
using Microsoft.EntityFrameworkCore;
using System;
using HiShop.Facebook;
using Microsoft.AspNetCore.Identity;

namespace HiShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //Coneccion a las base de datos , axel molaro
            services.AddDbContext<HiShopContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            services.AddMvc();



            // Sessiones
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
            });
            //face
           // services.AddIdentity<HiShop.Program, IdentityRole>()
           // .AddEntityFrameworkStores<HiShopContext>()
            //.AddDefaultTokenProviders();

            //services.AddAuthentication().AddFacebook(facebookOptions =>
            //{
            //    facebookOptions.AppId = Configuration["Authentication:Facebook:1610232299056898"];
           //     facebookOptions.AppSecret = Configuration["Authentication:Facebook:4b30c2c270d7f1c7441994e6955d1893"];
           // });


        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            //Usar sessiones
            app.UseSession();

            /*
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error/ErroresEnProcesoDeSolucion");
            }
            */
            //Esto es para publicar en un servidos iis
            if (env.IsProduction())
           {
               app.UseDeveloperExceptionPage();
               app.UseBrowserLink();
           }
           else
           {
               app.UseExceptionHandler("/Error/ErroresEnProcesoDeSolucion");
           }

            //Usar archivos estaticos css y eso 
            app.UseStaticFiles();




            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Inicio}/{action=Inicio}/{id?}");
            });

           
        }
    }
}
