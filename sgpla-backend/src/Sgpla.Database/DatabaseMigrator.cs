using DbUp;
using DbUp.Engine;
using DbUp.Engine.Output;

namespace Sgpla.Database;

/// <summary>
/// Aplica los scripts SQL versionados de <c>Scripts/</c> (recursos embebidos) sobre la base de datos.
/// Lo usan la CLI de este proyecto y el fixture de las pruebas de integración.
/// </summary>
public static class DatabaseMigrator
{
    public const string EsquemaBitacora = "dbo";
    public const string TablaBitacora = "schema_versions";

    /// <param name="cadenaConexion">Cadena de conexión a SQL Server.</param>
    /// <param name="crearBaseSiNoExiste">Solo para desarrollo y pruebas: crea la base de datos si no existe.</param>
    /// <param name="log">Destino de la bitácora de ejecución; por defecto, la consola.</param>
    public static DatabaseUpgradeResult Migrar(string cadenaConexion, bool crearBaseSiNoExiste = false, IUpgradeLog? log = null)
    {
        if (crearBaseSiNoExiste)
        {
            EnsureDatabase.For.SqlDatabase(cadenaConexion, log ?? new ConsoleUpgradeLog());
        }

        var motor = DeployChanges.To
            .SqlDatabase(cadenaConexion)
            .JournalToSqlTable(EsquemaBitacora, TablaBitacora)
            .WithScriptsEmbeddedInAssembly(typeof(DatabaseMigrator).Assembly, nombre => nombre.EndsWith(".sql", StringComparison.OrdinalIgnoreCase))
            .WithTransactionPerScript()
            .LogTo(log ?? new ConsoleUpgradeLog())
            .Build();

        return motor.PerformUpgrade();
    }
}
