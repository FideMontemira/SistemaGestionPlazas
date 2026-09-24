using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sgpla.BuildingBlocks.Infrastructure.Persistence;

namespace Sgpla.Modules.Docentes;

/// <summary>Punto de registro del módulo Docentes en el host.</summary>
public static class DocentesModule
{
    public const string Ruta = "/api/v1/docentes";

    public static IServiceCollection AddDocentesModule(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        services.AddPersistenciaModulo(typeof(DocentesModule).Assembly);
        return services;
    }

    public static IEndpointRouteBuilder MapDocentesEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGroup(Ruta).WithTags("Docentes");
        return endpoints;
    }
}
