using Dloc.ControleAvesWeb.IoC;
using Dloc.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Dloc.ControleAvesWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Configura��o para o EntityFramework

            //mapeando inje��o de depend�ncia para a classe DataContext
             services.AddTransient<DataContext>();

            //mapeando a string de conex�o que ser� enviada para a classe DataContext
            services.AddDbContext<DataContext>(
                    options => options.UseSqlServer(Configuration.GetConnectionString("webContext"))
                );

            #endregion

            #region Configura��o para Inje��o de Depend�ncia

             NativeInjectorBootStrapper.RegisterServices(services);

           


            #endregion

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
