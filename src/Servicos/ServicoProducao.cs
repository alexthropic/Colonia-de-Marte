namespace ColoniaEmMarte.Servicos;

public class ServicoProducao
{
    /// <summary>
    /// Guarda os dados de cada módulo no tabuleiro que pertencem ao jogador.
    /// </summary>
    public class ModuloInfo
    {
        public char Tipo { get; set; }        // 'S', 'X', 'H' ou 'B' (Base)
        public string Terreno { get; set; }   // "Gelo", "Minério", "Planície"
        public int Linha { get; set; }        // 0 a 7
        public int Coluna { get; set; }       // 0 a 7
    }

    /// <summary>
    /// Executa a produção completa do turno de um jogador.
    /// </summary>
    public static string ExecutarFimDeTurno(
        string nomeJogador,
        string proximoJogador,
        int ronda,
        ref int energia,
        ref int agua,
        ref int metal,
        ref int colonos,
        List<ModuloInfo> modulosDoJogador)
    {
        // -------------------------------------------------------------
        // PASSO 1: RECOLHA
        // -------------------------------------------------------------
        int energiaRecolhida = 0;
        int aguaRecolhida = 0;
        int metalRecolhido = 0;

        // A Base produz sempre 1 de cada
        energiaRecolhida += 1;
        aguaRecolhida += 1;
        metalRecolhido += 1;

        // Verificamos se é ronda de tempestade (múltiplos de 4)
        bool ehTempestade = (ronda % 4 == 0);

        foreach (var mod in modulosDoJogador)
        {
            if (mod.Tipo == 'S') // Painel Solar
            {
                // Dá 1 na tempestade, ou 2 fora dela
                energiaRecolhida += ehTempestade ? 1 : 2;
            }
            else if (mod.Tipo == 'X') // Extrator
            {
                if (mod.Terreno == "Gelo")
                {
                    aguaRecolhida += 2;
                }
                else if (mod.Terreno == "Minério")
                {
                    metalRecolhido += 2;
                }
            }
        }

        // Somamos a recolha às reservas do jogador
        energia += energiaRecolhida;
        agua += aguaRecolhida;
        metal += metalRecolhido;

        // -------------------------------------------------------------
        // PASSO 2: ABASTECIMENTO DOS HABITATS
        // -------------------------------------------------------------
        // Filtramos apenas os habitats
        var habitats = modulosDoJogador
            .Where(m => m.Tipo == 'H')
            // REGRA: Ordenar na ordem de leitura do mapa (linha a linha, depois coluna)
            .OrderBy(m => m.Linha)
            .ThenBy(m => m.Coluna)
            .ToList();

        int totalHabitats = habitats.Count;
        int habitatsAtivos = 0;

        foreach (var habitat in habitats)
        {
            // O habitat só consome se houver pelo menos 1 energia E 1 água
            if (energia >= 1 && agua >= 1)
            {
                energia -= 1;
                agua -= 1;
                habitatsAtivos += 1;
                colonos += 1; // Cada habitat ativo acolhe 1 colono
            }
            // Se faltar algum recurso, o habitat não faz nada neste turno
        }

        // -------------------------------------------------------------
        // PASSO 3: MENSAGEM FINAL E VITÓRIA
        // -------------------------------------------------------------
        string cabecalho = $"Turno terminado. Produção: E+{energiaRecolhida} A+{aguaRecolhida} M+{metalRecolhido}. " +
                           $"Habitats ativos: {habitatsAtivos} de {totalHabitats}.";

        // Se o jogador chegou aos 15 colonos, ganha o jogo imediatamente!
        if (colonos >= 15)
        {
            return $"{cabecalho} Jogo terminado. Vencedor: {nomeJogador}.";
        }

        // Caso contrário, passa ao próximo jogador
        return $"{cabecalho} Novo turno do jogador {proximoJogador}.";
    }
}