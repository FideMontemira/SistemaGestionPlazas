using Sgpla.Database;

// Uso:
//   dotnet run --project src/Sgpla.Database -- --connection "<cadena>" [--ensure-database]
// La cadena también puede proporcionarse con la variable de entorno SGPLA_DB_CONNECTION.

var cadenaConexion = ObtenerArgumento(args, "--connection")
    ?? Environment.GetEnvironmentVariable("SGPLA_DB_CONNECTION");

if (string.IsNullOrWhiteSpace(cadenaConexion))
{
    Console.Error.WriteLine("Falta la cadena de conexión: use --connection \"...\" o la variable SGPLA_DB_CONNECTION.");
    return 2;
}

var crearBase = args.Contains("--ensure-database", StringComparer.OrdinalIgnoreCase);
var resultado = DatabaseMigrator.Migrar(cadenaConexion, crearBase);

if (!resultado.Successful)
{
    Console.Error.WriteLine($"La migración falló en '{resultado.ErrorScript?.Name}': {resultado.Error}");
    return 1;
}

Console.WriteLine("Migración completada.");
return 0;

static string? ObtenerArgumento(string[] args, string nombre)
{
    var indice = Array.FindIndex(args, a => string.Equals(a, nombre, StringComparison.OrdinalIgnoreCase));
    return indice >= 0 && indice + 1 < args.Length ? args[indice + 1] : null;
}
