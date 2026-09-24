using Microsoft.Data.SqlClient;
using Sgpla.Database;
using Testcontainers.MsSql;

[assembly: AssemblyFixture(typeof(Sgpla.IntegrationTests.Infraestructura.SqlServerFixture))]

namespace Sgpla.IntegrationTests.Infraestructura;

/// <summary>
/// Levanta un SQL Server en contenedor una sola vez por ejecución de pruebas
/// y aplica las migraciones de DbUp sobre la base <c>sgpla-bd</c>.
/// </summary>
public sealed class SqlServerFixture : IAsyncLifetime
{
    private const string NombreBaseDatos = "sgpla-bd";

    private readonly MsSqlContainer _contenedor = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-latest").Build();

    public string CadenaConexion { get; private set; } = string.Empty;

    public async ValueTask InitializeAsync()
    {
        await _contenedor.StartAsync();

        CadenaConexion = new SqlConnectionStringBuilder(_contenedor.GetConnectionString())
        {
            InitialCatalog = NombreBaseDatos,
        }.ConnectionString;

        var resultado = DatabaseMigrator.Migrar(CadenaConexion, crearBaseSiNoExiste: true);
        if (!resultado.Successful)
        {
            throw new InvalidOperationException("No se pudieron aplicar las migraciones.", resultado.Error);
        }
    }

    public ValueTask DisposeAsync() => _contenedor.DisposeAsync();
}
