using ColoniaEmMarte.Modelos;

namespace ColoniaEmMarte.Servicos;

/// <summary>
/// Regras de negócio das instruções CM (construir módulo) e DS (desistir).
///
/// Nesta primeira parte contém apenas as regras fixas de cada tipo de módulo,
/// tal como aparecem na tabela "Módulos" do enunciado:
///
///   Letra  Módulo        Terreno            Custo
///   S      Painel solar  Planície           2 metal
///   X      Extrator      Gelo ou Minério    2 metal + 1 energia
///   H      Habitat       Planície           3 metal + 1 energia
///
/// Ter estas regras num só sítio evita repetir números "mágicos" pelo código:
/// se o custo de um módulo mudasse, só era preciso alterar aqui.
/// </summary>
public static class ServicoAcoes
{
    // ------------------------------------------------------------------
    // Leitura da letra do módulo (usada no erro 4 do CM)
    // ------------------------------------------------------------------

    /// <summary>
    /// Converte a letra escrita na instrução CM ("S", "X" ou "H") no tipo de módulo.
    /// As letras distinguem maiúsculas de minúsculas: "s" não é válido.
    /// </summary>
    /// <param name="texto">Texto escrito pelo utilizador no lugar do tipo.</param>
    /// <param name="tipo">Tipo de módulo lido (só tem significado se o método devolver true).</param>
    /// <returns>true se o texto for S, X ou H; false caso contrário.</returns>
    public static bool TentarLerTipo(string texto, out TipoModulo tipo)
    {
        switch (texto)
        {
            case "S":
                tipo = TipoModulo.PainelSolar;
                return true;
            case "X":
                tipo = TipoModulo.Extrator;
                return true;
            case "H":
                tipo = TipoModulo.Habitat;
                return true;
            default:
                tipo = TipoModulo.PainelSolar; // valor qualquer, não é usado
                return false;
        }
    }

    /// <summary>
    /// Letra que representa o tipo de módulo (S, X ou H).
    /// Útil para desenhar o mapa no DJ (por exemplo "S1", "X2").
    /// </summary>
    public static char LetraDoTipo(TipoModulo tipo)
    {
        switch (tipo)
        {
            case TipoModulo.PainelSolar: return 'S';
            case TipoModulo.Extrator: return 'X';
            default: return 'H';
        }
    }

    // ------------------------------------------------------------------
    // Custos de construção (usados no erro 10 do CM e no pagamento)
    // ------------------------------------------------------------------

    /// <summary>Metal necessário para construir um módulo deste tipo.</summary>
    public static int CustoMetal(TipoModulo tipo)
    {
        switch (tipo)
        {
            case TipoModulo.PainelSolar: return 2;
            case TipoModulo.Extrator: return 2;
            default: return 3; // Habitat
        }
    }

    /// <summary>Energia necessária para construir um módulo deste tipo.</summary>
    public static int CustoEnergia(TipoModulo tipo)
    {
        switch (tipo)
        {
            case TipoModulo.PainelSolar: return 0;
            case TipoModulo.Extrator: return 1;
            default: return 1; // Habitat
        }
    }

    // ------------------------------------------------------------------
    // Terreno compatível (usado no erro 8 do CM)
    // ------------------------------------------------------------------

    /// <summary>
    /// Indica se um tipo de módulo pode ser construído num terreno.
    /// Painel solar e Habitat: só Planície.
    /// Extrator: Gelo ou Minério.
    /// Na Cratera não se pode construir nada.
    /// </summary>
    public static bool TerrenoPermite(TipoModulo tipo, Terreno terreno)
    {
        if (tipo == TipoModulo.Extrator)
        {
            return terreno == Terreno.Gelo || terreno == Terreno.Minerio;
        }

        // Painel solar ou Habitat
        return terreno == Terreno.Planicie;
    }
}
