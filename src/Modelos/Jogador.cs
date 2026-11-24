namespace ColoniaMarte.Modelos;

/// Jogador registado e o seu historial de jogos.
public class Jogador
{
    public string Nome { get; }
    public int Jogos { get; set; }
    public int Vitorias { get; set; }
    public int Empates { get; set; }
    public int Derrotas { get; set; }

    public Jogador(string nome)
    {
        Nome = nome;
        Jogos = 0;
        Vitorias = 0;
        Empates = 0;
        Derrotas = 0;
    }
}