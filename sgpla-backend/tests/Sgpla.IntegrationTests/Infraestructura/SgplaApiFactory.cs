using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Sgpla.IntegrationTests.Infraestructura;

/// <summary>Host de la API en memoria apuntando a la base de datos del contenedor.</summary>
public sealed class SgplaApiFactory(SqlServerFixture sqlServer) : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.UseSetting("ConnectionStrings:Sgpla", sqlServer.CadenaConexion);
    }
}
