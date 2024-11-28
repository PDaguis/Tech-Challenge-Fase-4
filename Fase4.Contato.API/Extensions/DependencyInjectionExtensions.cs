using Fase4.Contato.Core.Interfaces;
using Fase4.Contato.Infra.Repositories;

namespace Fase4.Contato.API.Extensions;

public static class DependencyInjectionExtensions
{
    public static void AddDependencies(this IServiceCollection services)
    {
        services.AddScoped<IContatoRepository, ContatoRepository>();
    }
}