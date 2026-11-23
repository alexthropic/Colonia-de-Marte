using ColoniaEmMarte.Vistas;

namespace ColoniaEmMarte;

public class Program
{
    public static void Main(string[] args)
    {
        // 1. Criamos uma grelha 8x8 de teste com a sessão da página 14 do enunciado:
        string[,] mapaTeste = new string[8, 8]
        {
            { "--", "~~", "--", "^^", "^^", "--", "~~", "--" },
            { "X1", "B1", "H1", "--", "##", "--", "--", "^^" },
            { "--", "--", "~~", "--", "--", "~~", "--", "--" },
            { "##", "--", "^^", "--", "~~", "^^", "--", "##" },
            { "##", "--", "^^", "~~", "--", "^^", "--", "##" },
            { "--", "--", "~~", "--", "--", "~~", "H2", "--" },
            { "^^", "--", "--", "##", "--", "--", "B2", "^^" },
            { "--", "~~", "--", "^^", "^^", "--", "X2", "--" }
        };

        Console.WriteLine("=== A TESTAR O COMANDO DJ ===");
        
        // Desenha as 8 linhas do mapa
        VistaTabuleiro.DesenharGrelha(mapaTeste);

        // Desenha a ronda 2 (Vez do Rui)
        VistaTabuleiro.DesenharRondaETurno(2, "Rui");

        // Desenha o estado dos 2 jogadores
        VistaTabuleiro.DesenharInfoJogador(1, "Ana", 4, 4, 3, 1);
        VistaTabuleiro.DesenharInfoJogador(2, "Rui", 1, 4, 2, 1);
    }
}