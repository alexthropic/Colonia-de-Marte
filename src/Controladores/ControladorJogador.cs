using ColoniaMarte.Modelos;

namespace ColoniaMarte.Controladores;

/// <summary>Instruções dos jogadores: RJ, LJ, IJ e TJ.</summary>
public class ControladorJogador
{
    public static List<Jogador> jogadores = new();
    public static List<Jogador> jogadoresEmJogo = new(); // pela ordem de jogo
    public static bool JogoEmCurso = false;
    public static Dictionary<Jogador, int> colonos = new(); // atualizado pelo TT
    public static List<Jogador> desistentes = new();        // preenchido pelo DS

    /// <summary>RJ — Registar jogador.</summary>
    public static string RegistarJogador(string nome)
    {
        if (jogadores.Exists(j => j.Nome == nome))
            return "Jogador existente.";

        jogadores.Add(new Jogador(nome));
        return "Jogador registado com sucesso.";
    }

    /// <summary>LJ — Listar jogadores (por vitórias e depois por nome).</summary>
    public static string ListarJogadores()
    {
        if (!jogadores.Any())
            return "Sem jogadores registados.";

        var ordenados = jogadores
            .OrderByDescending(j => j.Vitorias)
            .ThenBy(j => j.Nome, StringComparer.Ordinal)
            .ToList();

        string resultado = "";
        foreach (var j in ordenados)
            resultado += $"{j.Nome} {j.Jogos} {j.Vitorias} {j.Empates} {j.Derrotas}\n";

        return resultado.TrimEnd();
    }

    /// <summary>IJ — Iniciar jogo (2 a 4 jogadores).</summary>
    public static string IniciarJogo(string[] nomes)
    {
        if (JogoEmCurso)
            return "Existe um jogo em curso.";

        foreach (string nome in nomes)
            if (jogadores.Find(j => j.Nome == nome) == null)
                return "Jogador inexistente.";

        if (nomes.Distinct().Count() != nomes.Length)
            return "Jogador repetido.";

        jogadoresEmJogo.Clear();
        foreach (string nome in nomes)
            jogadoresEmJogo.Add(jogadores.Find(j => j.Nome == nome)!);

        JogoEmCurso = true;
        return "Jogo iniciado com sucesso.";
    }

    /// <summary>TJ — Terminar jogo (vence o jogador ativo com mais colonos).</summary>
    public static string TerminarJogo()
    {
        if (!JogoEmCurso)
            return "Não existe jogo em curso.";

        List<Jogador> ativos = jogadoresEmJogo.Where(j => !desistentes.Contains(j)).ToList();
        int maxColonos = ativos.Max(j => colonos.GetValueOrDefault(j, 0));
        List<Jogador> melhores = ativos.Where(j => colonos.GetValueOrDefault(j, 0) == maxColonos).ToList();

        FinalizarJogo(melhores);

        if (melhores.Count == 1)
            return $"Jogo terminado. Vencedor: {melhores[0].Nome}.";

        return $"Jogo terminado. Empate: {string.Join(", ", melhores.Select(j => j.Nome))}.";
    }

    /// <summary>Atualiza o historial e termina o jogo. Também usado pelo TT e pelo DS.</summary>
    public static void FinalizarJogo(List<Jogador> vencedores)
    {
        bool empate = vencedores.Count > 1;

        foreach (Jogador jogador in jogadoresEmJogo) // inclui desistentes
        {
            jogador.Jogos++;

            if (!vencedores.Contains(jogador))
                jogador.Derrotas++;
            else if (empate)
                jogador.Empates++;
            else
                jogador.Vitorias++;
        }

        colonos.Clear();
        desistentes.Clear();
        JogoEmCurso = false;
    }
}
