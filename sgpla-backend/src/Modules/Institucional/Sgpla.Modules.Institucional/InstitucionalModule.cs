using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sgpla.BuildingBlocks.Infrastructure.Persistence;

namespace Sgpla.Modules.Institucional;

/// <summary>Punto de registro del módulo Institucional en el host.</summary>
public static class InstitucionalModule
{
    public const string Ruta = "/api/v1/institucional";

    public static IServiceCollection AddInstitucionalModule(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        services.AddPersistenciaModulo(typeof(InstitucionalModule).Assembly);
        return services;
    }

    public static IEndpointRouteBuilder MapInstitucionalEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGroup(Ruta).WithTags("Institucional");
        return endpoints;
    }
}
