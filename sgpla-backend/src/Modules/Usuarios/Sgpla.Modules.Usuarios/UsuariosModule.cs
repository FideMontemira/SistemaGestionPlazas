using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sgpla.BuildingBlocks.Infrastructure.Persistence;

namespace Sgpla.Modules.Usuarios;

/// <summary>Punto de registro del módulo Usuarios en el host.</summary>
public static class UsuariosModule
{
    public const string Ruta = "/api/v1/usuarios";

    public static IServiceCollection AddUsuariosModule(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        services.AddPersistenciaModulo(typeof(UsuariosModule).Assembly);
        return services;
    }

    public static IEndpointRouteBuilder MapUsuariosEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGroup(Ruta).WithTags("Usuarios");
        return endpoints;
    }
}
