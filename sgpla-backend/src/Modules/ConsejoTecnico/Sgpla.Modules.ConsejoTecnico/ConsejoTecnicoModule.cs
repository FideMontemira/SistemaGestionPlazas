using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sgpla.BuildingBlocks.Infrastructure.Persistence;

namespace Sgpla.Modules.ConsejoTecnico;

/// <summary>Punto de registro del módulo ConsejoTecnico en el host.</summary>
public static class ConsejoTecnicoModule
{
    public const string Ruta = "/api/v1/consejo-tecnico";

    public static IServiceCollection AddConsejoTecnicoModule(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        services.AddPersistenciaModulo(typeof(ConsejoTecnicoModule).Assembly);
        return services;
    }

    public static IEndpointRouteBuilder MapConsejoTecnicoEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGroup(Ruta).WithTags("ConsejoTecnico");
        return endpoints;
    }
}
