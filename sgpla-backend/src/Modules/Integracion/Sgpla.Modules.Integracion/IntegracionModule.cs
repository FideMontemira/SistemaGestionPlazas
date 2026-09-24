using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sgpla.BuildingBlocks.Infrastructure.Persistence;

namespace Sgpla.Modules.Integracion;

/// <summary>Punto de registro del módulo Integracion en el host.</summary>
public static class IntegracionModule
{
    public const string Ruta = "/api/v1/integracion";

    public static IServiceCollection AddIntegracionModule(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        services.AddPersistenciaModulo(typeof(IntegracionModule).Assembly);
        return services;
    }

    public static IEndpointRouteBuilder MapIntegracionEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGroup(Ruta).WithTags("Integracion");
        return endpoints;
    }
}
