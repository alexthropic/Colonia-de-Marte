namespace ColoniaEmMarte.Modelos;

/// <summary>
/// Tipos de terreno de um espaço do mapa.
/// As posições de Base são, em termos de terreno, Planície
/// (por isso uma Base não atribuída pode ser ocupada como Planície).
/// </summary>
public enum Terreno
{
    Planicie, // 32 espaços (+ Bases não atribuídas)
    Gelo,     // 10 espaços
    Minerio,  // 12 espaços
    Cratera   // 6 espaços - não se pode construir nada
}

/// <summary>
/// Tipos de módulo que um jogador pode construir com CM.
/// No texto das instruções e no mapa são representados pelas letras S, X e H.
/// </summary>
public enum TipoModulo
{
    PainelSolar, // S
    Extrator,    // X
    Habitat      // H
}

/// <summary>
/// Recursos do jogo. Nas instruções são representados pelas letras E, A e M.
/// </summary>
public enum Recurso
{
    Energia, // E
    Agua,    // A
    Metal    // M
}
