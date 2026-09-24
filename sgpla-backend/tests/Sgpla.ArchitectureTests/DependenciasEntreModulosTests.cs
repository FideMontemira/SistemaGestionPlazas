using NetArchTest.Rules;

namespace Sgpla.ArchitectureTests;

public class DependenciasEntreModulosTests
{
    [Theory]
    [MemberData(nameof(Modulos.Nombres), MemberType = typeof(Modulos))]
    public void Modulo_SoloDependeDeModulosPermitidos(string modulo)
    {
        var prohibidos = Modulos.Ensamblados.Keys
            .Where(otro => otro != modulo && !Modulos.DependenciasPermitidas[modulo].Contains(otro))
            .Select(Modulos.Namespace)
            .ToArray();

        var resultado = Types.InAssembly(Modulos.Ensamblados[modulo])
            .ShouldNot()
            .HaveDependencyOnAny(prohibidos)
            .GetResult();

        resultado.IsSuccessful.ShouldBeTrue(
            $"{modulo} depende de módulos no permitidos: {string.Join(", ", resultado.FailingTypeNames ?? [])}");
    }

    [Fact]
    public void Grafo_NoTieneCiclos()
    {
        var visitados = new HashSet<string>();
        var enCurso = new HashSet<string>();

        bool TieneCiclo(string modulo)
        {
            if (enCurso.Contains(modulo))
            {
                return true;
            }

            if (!visitados.Add(modulo))
            {
                return false;
            }

            enCurso.Add(modulo);
            var ciclo = Modulos.DependenciasPermitidas[modulo].Any(TieneCiclo);
            enCurso.Remove(modulo);
            return ciclo;
        }

        Modulos.DependenciasPermitidas.Keys.Any(TieneCiclo).ShouldBeFalse();
    }
}
