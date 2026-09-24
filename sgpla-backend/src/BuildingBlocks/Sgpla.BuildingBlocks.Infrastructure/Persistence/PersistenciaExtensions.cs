using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sgpla.BuildingBlocks.Infrastructure.Persistence;

/// <summary>Ensamblado de un módulo cuyas configuraciones de EF Core se aplican a <see cref="SgplaDbContext"/>.</summary>
public sealed record EnsambladoModulo(Assembly Ensamblado);

public static class PersistenciaExtensions
{
    public const string NombreCadenaConexion = "Sgpla";

    public static IServiceCollection AddPersistenciaSgpla(this IServiceCollection services, IConfiguration configuration)
    {
        var cadenaConexion = configuration.GetConnectionString(NombreCadenaConexion);
        if (string.IsNullOrWhiteSpace(cadenaConexion))
        {
            throw new InvalidOperationException($"Falta la cadena de conexión '{NombreCadenaConexion}'.");
        }

        services.AddDbContext<SgplaDbContext>(options => options
            .UseSqlServer(cadenaConexion)
            .UseSnakeCaseNamingConvention());

        return services;
    }

    /// <summary>Registra el ensamblado de un módulo para que <see cref="SgplaDbContext"/> aplique sus configuraciones.</summary>
    public static IServiceCollection AddPersistenciaModulo(this IServiceCollection services, Assembly ensamblado)
    {
        services.AddSingleton(new EnsambladoModulo(ensamblado));
        return services;
    }
}
