using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sgpla.BuildingBlocks.Infrastructure.Persistence;

namespace Sgpla.Modules.Publicacion;

/// <summary>Punto de registro del módulo Publicacion en el host.</summary>
public static class PublicacionModule
{
    public const string Ruta = "/api/v1/publicacion";

    public static IServiceCollection AddPublicacionModule(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        services.AddPersistenciaModulo(typeof(PublicacionModule).Assembly);
        return services;
    }

    public static IEndpointRouteBuilder MapPublicacionEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGroup(Ruta).WithTags("Publicacion");
        return endpoints;
    }
}
