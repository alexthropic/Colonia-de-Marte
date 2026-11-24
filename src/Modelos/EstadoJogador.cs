namespace ColoniaEmMarte.Modelos;


public class EstadoJogador
{

    public const int EnergiaInicial = 3;
    public const int AguaInicial = 2;
    public const int MetalInicial = 6;

    public const int MaximoConstrucoesPorTurno = 2;

   
    public string Nome { get; }

    public int NumeroBase { get; }

    public int Energia { get; private set; }
    public int Agua { get; private set; }
    public int Metal { get; private set; }
    public int Colonos { get; private set; }

    public int ConstrucoesNoTurno { get; private set; }

    public bool Ativo { get; private set; }

  
    public EstadoJogador(string nome, int numeroBase)
    {
        Nome = nome;
        NumeroBase = numeroBase;
        Energia = EnergiaInicial;
        Agua = AguaInicial;
        Metal = MetalInicial;
        Colonos = 0;
        ConstrucoesNoTurno = 0;
        Ativo = true;
    }

    
    public bool PodeConstruirMais()
    {
        return ConstrucoesNoTurno < MaximoConstrucoesPorTurno;
    }

    public bool TemRecursosPara(int custoEnergia, int custoMetal)
    {
        return Energia >= custoEnergia && Metal >= custoMetal;
    }

    
    public void RegistarConstrucao(int custoEnergia, int custoMetal)
    {
        Energia -= custoEnergia;
        Metal -= custoMetal;
        ConstrucoesNoTurno++;
    }

    public void ReiniciarConstrucoes()
    {
        ConstrucoesNoTurno = 0;
    }

       public void AlterarRecursos(int energia, int agua, int metal)
    {
        Energia += energia;
        Agua += agua;
        Metal += metal;
    }

    public void AdicionarColono()
    {
        Colonos++;
    }

     public void Desistir()
    {
        Ativo = false;
    }
}
