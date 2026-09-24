using System.Net;
using Microsoft.Data.SqlClient;
using Sgpla.IntegrationTests.Infraestructura;

namespace Sgpla.IntegrationTests;

public sealed class EsqueletoTests(SqlServerFixture sqlServer) : IAsyncDisposable
{
    private readonly SgplaApiFactory _api = new(sqlServer);

    [Fact]
    public async Task Health_ConBaseDeDatosDisponible_Responde200()
    {
        using var cliente = _api.CreateClient();

        using var respuesta = await cliente.GetAsync(new Uri("/health", UriKind.Relative), TestContext.Current.CancellationToken);

        respuesta.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Migraciones_CreanLosCuatroEsquemas()
    {
        await using var conexion = new SqlConnection(sqlServer.CadenaConexion);
        await conexion.OpenAsync(TestContext.Current.CancellationToken);
        await using var comando = new SqlCommand(
            "SELECT name FROM sys.schemas WHERE name IN (N'academico', N'plazas', N'usuarios', N'integracion')",
            conexion);

        var esquemas = new List<string>();
        await using var lector = await comando.ExecuteReaderAsync(TestContext.Current.CancellationToken);
        while (await lector.ReadAsync(TestContext.Current.CancellationToken))
        {
            esquemas.Add(lector.GetString(0));
        }

        esquemas.ShouldBe(["academico", "integracion", "plazas", "usuarios"], ignoreOrder: true);
    }

    public ValueTask DisposeAsync() => _api.DisposeAsync();
}
