namespace ColoniaEmMarte.Vistas;

public static class VistaTabuleiro
{

    /// Imprime a grelha 8x8 exatamente como pedido no enunciado.
    /// Cada linha começa com o número (1 a 8) e tem 8 células separadas por espaço.
  
    public static void DesenharGrelha(string[,] grelha)
    {
        for (int linha = 0; linha < 8; linha++)
        {
            // Número da linha de 1 a 8 seguido de espaço
            Console.Write((linha + 1) + " ");

            for (int col = 0; col < 8; col++)
            {
                Console.Write(grelha[linha, col]);

                // Adiciona espaço entre colunas, mas não no final da linha
                if (col < 7)
                {
                    Console.Write(" ");
                }
            }
            // Muda para a linha seguinte
            Console.WriteLine();
        }
    }


    /// Imprime a linha da ronda:
    /// "Ronda R - Vez: Nome" ou "Ronda R (tempestade) - Vez: Nome"

    public static void DesenharRondaETurno(int ronda, string nomeJogadorVez)
    {
        bool ehTempestade = (ronda % 4 == 0);

        if (ehTempestade)
        {
            Console.WriteLine($"Ronda {ronda} (tempestade) - Vez: {nomeJogadorVez}");
        }
        else
        {
            Console.WriteLine($"Ronda {ronda} - Vez: {nomeJogadorVez}");
        }
    }


    /// Imprime a linha de estado de um jogador:
    /// "Jn NomeJogador - E:e A:a M:m - Colonos: c"
    public static void DesenharInfoJogador(int numeroBase, string nome, int energia, int agua, int metal, int colonos)
    {
        Console.WriteLine($"J{numeroBase} {nome} - E:{energia} A:{agua} M:{metal} - Colonos: {colonos}");
    }
}