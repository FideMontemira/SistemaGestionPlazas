using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sgpla.BuildingBlocks.Infrastructure.Persistence;

namespace Sgpla.Modules.Aspirantes;

/// <summary>Punto de registro del módulo Aspirantes en el host.</summary>
public static class AspirantesModule
{
    public const string Ruta = "/api/v1/aspirantes";

    public static IServiceCollection AddAspirantesModule(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        services.AddPersistenciaModulo(typeof(AspirantesModule).Assembly);
        return services;
    }

    public static IEndpointRouteBuilder MapAspirantesEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGroup(Ruta).WithTags("Aspirantes");
        return endpoints;
    }
}
