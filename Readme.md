# Colónia em Marte — Projeto de Fundamentos da Programação

**Unidade Curricular:** Fundamentos da Programação (2026 / 2027)  
**Curso:** Licenciatura em Engenharia Informática  
**Docente:** Anastasiya Graça  
**Tecnologia:** C# (.NET 8)

---

## 1. Identificação do Grupo de Trabalho

| Número de Estudante | Nome Completo | E-mail Institucional |
| :--- | :--- | :--- |
| `[20241821]` | `[Fábio Alexandre Rómulo Teixeira]` | `[20241821.iade.pt                     ` |
| `[Número 2]` | `[Nome Completo do Aluno 2]` | `[email2]@ext.universidadeeuropeia.pt` |
| `[Número 3]` | `[Nome Completo do Aluno 3]` | `[email3]@ext.universidadeeuropeia.pt` |
| `[Número 4]` | `[Nome Completo do Aluno 4]` | `[email4]@ext.universidadeeuropeia.pt` |

---

## 2. Visão Geral da Solução

O projeto **Colónia em Marte** consiste num jogo de estratégia por turnos para a consola em C#, no qual 2 a 4 agências espaciais disputam a colonização de Marte num mapa simétrico de 8x8. 

A aplicação foi desenvolvida sob o paradigma da programação orientada a objetos com uma **arquitetura em camadas baseada no padrão MVC (Model-View-Controller) suportada por Serviços e Repositórios**. O comportamento é estritamente determinista, sem recurso a dependências externas ao .NET 8.

---

## 3. Arquitetura do Software e Estrutura de Ficheiros

Para assegurar uma elevada coesão e um baixo acoplamento, a solução no diretório `src/` está estruturada nas seguintes camadas:

```text
projeto/
│
├── README.md                                  <-- Relatório do projeto
│
└── src/
    ├── Program.cs                             <-- Ponto de entrada (ciclo principal de leitura de comandos)
    │
    ├── Comum/                                 <-- Elementos transversais e constantes
    │   ├── Mensagens.cs                       <-- Constantes com textos exatos de sucesso e insucesso
    │   └── Coordenada.cs                      <-- Conversão e validação de "A1"-"H8" e cálculo de adjacências
    │
    ├── Modelos/ (Models)                      <-- Entidades de dados
    │   ├── Enumerados.cs                      <-- Tipos de Terreno, Módulo e Recurso
    │   ├── Jogador.cs                         <-- Entidade persistente do jogador (histórico de jogos)
    │   ├── EstadoJogador.cs                   <-- Estado em jogo: recursos (E, A, M), base e colonos
    │   └── Celula.cs                          <-- Representação do espaço da grelha marciana
    │
    ├── Repositorios/ (Repositories)           <-- Camada de acesso e armazenamento de dados em memória
    │   ├── IRepositorioJogadores.cs           <-- Contrato de acesso ao catálogo de jogadores
    │   ├── RepositorioJogadores.cs            <-- Registo, pesquisa e ordenação de jogadores (LJ)
    │   └── RepositorioJogo.cs                 <-- Armazena o estado do tabuleiro e a sessão do jogo ativo
    │
    ├── Servicos/ (Services)                   <-- Camada com regras de negócio e algoritmos
    │   ├── ServicoJogo.cs                     <-- Ciclo do jogo: rondas, turnos, desistências e fim de jogo
    │   ├── ServicoConectividade.cs            <-- Algoritmo BFS que garante a colónia sempre ligada
    │   ├── ServicoAcoes.cs                    <-- Validação de regras de CM, DM e trocas TR
    │   └── ServicoProducao.cs                 <-- Recolha de recursos, tempestades e abastecimento de habitats (TT)
    │
    ├── Controladores/ (Controllers)           <-- Orquestração entre entrada, serviços e vistas
    │   ├── DespachanteComandos.cs             <-- Interpretação sintática da linha e encaminhamento
    │   ├── ControladorJogador.cs              <-- Trata as instruções RJ, LJ, IJ, TJ e DS
    │   └── ControladorAcoes.cs                <-- Trata as instruções CM, DM, TR, TT e DJ
    │
    └── Vistas/ (Views)                        <-- Apresentação e formatação das saídas
        ├── VistaTabuleiro.cs                  <-- Desenho da matriz 8x8 e resumo no formato do comando DJ
        └── VistaMensagens.cs                  <-- Formatação de mensagens de confirmação e resumo de produção
```

### Papel de Cada Camada:
1. **Modelos:** Armazenam o estado do domínio sem dependências externas.
2. **Repositórios:** Gerem o ciclo de vida dos dados em memória (por exemplo, guardar a lista de jogadores registados entre partidas).
3. **Serviços:** Contêm todas as regras do jogo e algoritmos complexos (ex: a pesquisa BFS para prevenir demolições que isolem módulos).
4. **Controladores:** Recebem os argumentos inseridos na consola, acionam as operações nos Serviços e solicitam à Vista a apresentação do resultado.
5. **Vistas:** Garantem que cada linha de texto emitida para a consola corresponde rigorosamente ao formato exigido no enunciado.

---

## 4. Distribuição Equilibrada de Tarefas (4 Elementos)

A divisão das responsabilidades foi atribuída de forma equilibrada entre as camadas do sistema:

### Aluno 1 (`[Nome do Aluno 1]`):
* **Foco:** Camada de Repositórios, Gestão de Jogadores e Controlador de Sessão.
* **Ficheiros:** `Modelos/Jogador.cs`, `Repositorios/*`, `Controladores/ControladorJogador.cs`, `Program.cs`.
* **Instruções:** `RJ` (Registar), `LJ` (Listar ordenado), `IJ` (Iniciar partida), `TJ` (Terminar jogo) e `DS` (Desistir).

### Aluno 2 (`[Nome do Aluno 2]`):
* **Foco:** Modelo Espacial e Algoritmos de Tabuleiro / Conectividade.
* **Ficheiros:** `Comum/Coordenada.cs`, `Modelos/Celula.cs`, `Servicos/ServicoConectividade.cs`.
* **Lógica:** Estrutura da matriz 8x8, cálculo de adjacências ortogonais e algoritmo de grafos (BFS) para garantir que a colónia permanece sempre ligada à Base após uma demolição (`DM`).

### Aluno 3 (`[Nome do Aluno 3]`):
* **Foco:** Serviços de Economia e Ações de Jogo.
* **Ficheiros:** `Modelos/EstadoJogador.cs`, `Servicos/ServicoAcoes.cs`, `Controladores/ControladorAcoes.cs` (ações `CM`, `DM`, `TR`).
* **Lógica:** Implementação e validação estrita da hierarquia de erros em `CM` (10 condições), devolução de metal em `DM` e trocas à taxa 3:1 em `TR`.

### Aluno 4 (`[Nome do Aluno 4]`):
* **Foco:** Serviço de Produção, Camada de Vistas e Bateria de Testes.
* **Ficheiros:** `Servicos/ServicoProducao.cs`, `Vistas/*`, `Comum/Mensagens.cs`, `Controladores/DespachanteComandos.cs`.
* **Lógica:** Execução sequencial da produção em `TT` (recolha com efeito de tempestades $\to$ abastecimento de habitats na ordem de leitura $\to$ verificação de vitória), renderização visual do `DJ` e criação dos testes automáticos.

---

## 5. Decisões de Implementação e Algoritmos

1. **Precedência na Validação de Erros:**  
   Em conformidade com o enunciado, quando ocorrem múltiplos erros em simultâneo, o sistema reporta unicamente o primeiro erro segundo a ordem numérica indicada. Esta regra foi implementada através de validações encadeadas nos Serviços.
2. **Garantia de Colónia Conexa (BFS):**  
   Ao executar o comando de demolição (`DM`), o `ServicoConectividade` simula a remoção do módulo e efetua uma pesquisa em largura (BFS) a partir da Base do jogador. Se o número de módulos alcançados for inferior ao total de módulos remanescentes, a operação é rejeitada com a mensagem: `A demolição deixaria módulos isolados.`
3. **Ordem de Leitura no Abastecimento de Habitats:**  
   Durante a produção (`TT`), os habitats ativos do jogador são ordenados de cima para baixo e da esquerda para a direita (`A1` a `H8`). Caso o jogador não disponha de 1 Energia e 1 Água para todos os seus habitats, apenas os primeiros na ordem de leitura são abastecidos e acolhem colonos; os restantes ficam inativos.
4. **Terminação Segura da Aplicação:**  
   O ciclo de entrada em `Program.cs` interrompe a execução de forma limpa assim que deteta uma linha nula, vazia ou composta exclusivamente por caracteres de espaçamento.

---

## 6. Como Compilar e Executar

### Pré-requisitos:
* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) instalado.

### Compilação:
```bash
dotnet build src/
```

### Execução:
```bash
dotnet run --project src/
```

### Validação com Ficheiro de Teste (Redirecionamento de Input):
```bash
dotnet run --project src/ < input.txt
```