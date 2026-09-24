using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sgpla.BuildingBlocks.Infrastructure.Persistence;

namespace Sgpla.Modules.SolicitudesApertura;

/// <summary>Punto de registro del módulo SolicitudesApertura en el host.</summary>
public static class SolicitudesAperturaModule
{
    public const string Ruta = "/api/v1/solicitudes-apertura";

    public static IServiceCollection AddSolicitudesAperturaModule(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        services.AddPersistenciaModulo(typeof(SolicitudesAperturaModule).Assembly);
        return services;
    }

    public static IEndpointRouteBuilder MapSolicitudesAperturaEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGroup(Ruta).WithTags("SolicitudesApertura");
        return endpoints;
    }
}
