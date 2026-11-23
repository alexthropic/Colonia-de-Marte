using ColoniaEmMarte.Servicos;
using static ColoniaEmMarte.Servicos.ServicoProducao;

namespace ColoniaEmMarte;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== A TESTAR O COMANDO TT (PRODUÇÃO) ===");

        // Cenário da Ana:
        // Recursos iniciais após construir 1 Painel e 1 Extrator de Minério:
        // Tinha (3 E, 2 A, 6 M). Gastou para os 2 módulos e ficou com:
        int anaEnergia = 2;
        int anaAgua = 2;
        int anaMetal = 2;
        int anaColonos = 0;

        var modulosAna = new List<ModuloInfo>
        {
            new ModuloInfo { Tipo = 'S', Terreno = "Planície", Linha = 1, Coluna = 2 }, // Painel em C2
            new ModuloInfo { Tipo = 'X', Terreno = "Minério", Linha = 1, Coluna = 0 }  // Extrator em A2
        };

        // Ronda 1: Ana termina o turno
        string saidaTT = ServicoProducao.ExecutarFimDeTurno(
            nomeJogador: "Ana",
            proximoJogador: "Rui",
            ronda: 1,
            ref anaEnergia,
            ref anaAgua,
            ref anaMetal,
            ref anaColonos,
            modulosAna
        );

        Console.WriteLine(saidaTT);
        Console.WriteLine($"Recursos da Ana agora: E:{anaEnergia} A:{anaAgua} M:{anaMetal} Colonos:{anaColonos}");
    }
}