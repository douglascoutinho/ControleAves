using Dloc.Data.Repositories;
using Dloc.Domain.IRepositories;
using Microsoft.Extensions.DependencyInjection;

namespace Dloc.ControleAvesWeb.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IAveRepository, AveRepository>();
            services.AddScoped<IMutacaoRepository, MutacaoRepository>();
            services.AddScoped<IPortadorRepository, PortadorRepository>();
        }
    }
}
