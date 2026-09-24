using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sgpla.BuildingBlocks.Infrastructure.Persistence;

namespace Sgpla.Modules.OfertaEducativa;

/// <summary>Punto de registro del módulo OfertaEducativa en el host.</summary>
public static class OfertaEducativaModule
{
    public const string Ruta = "/api/v1/oferta-educativa";

    public static IServiceCollection AddOfertaEducativaModule(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        services.AddPersistenciaModulo(typeof(OfertaEducativaModule).Assembly);
        return services;
    }

    public static IEndpointRouteBuilder MapOfertaEducativaEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGroup(Ruta).WithTags("OfertaEducativa");
        return endpoints;
    }
}
