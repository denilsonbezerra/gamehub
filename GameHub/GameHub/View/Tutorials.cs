namespace GameHub.View
{
    internal static class Tutorials
    {
        public readonly static string[] ChessTutorial =
        {
            "\nOlá, seja bem-vindo ao Xadrez do Hub de Jogos!\n" +
            "Para você aprender a jogar vamos ver as regras:\n\n" +
            "Assim como no xadrez tradicional, as peças brancas\n" +
            "começam e os turnos se alternam entre brancas e pretas.\n" +
            "Porém aqui as peças são representadas por letras:\n" +
            "'P' = Peão  | 'R' = Torre | 'N' = Cavalo |\n" +
            "'B' = Bispo | 'Q'= Rainha | 'K'= Rei |\n\n" +
            "Podemos identificar de quem são as peças através\n" +
            "das cores indicadas nas letras, as letras brancas são\n" +
            "peças brancas e as letras pretas são peças pretas." +
            "A seguir você verá como é o tabuleiro",

            "\nNa parte de cima e ao lado do tabuleiro\n" +
            "estão as marcações de linhas e colunas padrão do\n" +
            "xadrez (as colunas vão de A até H, e as linhas vão de\n" +
            "1 a 8). Para executar uma jogada escreva a\n" +
            "posição da coluna com a posição da linha da peça\n" +
            "que deseja mexer. Exemplos: 'A1', 'H5', 'B4'.\n\n" +
            "Depois de selecionar uma peça o tabuleiro muda visualmente\n" +
            "aparecendo casas destacadas, estas casas são as jogadas possíveis\n" +
            "para a peça selecionada. Uma escolha inválida de casa\n" +
            "faz com que a peça seja desselecionada, logo precisará\n" +
            "selecionar ela novamente ou poderá seelecionar outra.\n\n" +
            "A seguir temos um exemplo de como uma jogada ocorre!",

            "\nExitem duas jogadas que não são para mover peças.\n\n" +
            "Como no xadrez existem casos em que a vítória não é mais possível,\n" +
            "no xadrez do Hub é possível se render. Basta digitar 'render'\n" +
            "na sua jogada. E também é possível propor um empate ao digitar\n" +
            "'empate' na jogada. O oponente pode concordar ao também digitar\n" +
            "'empate' e então o jogo se encerra. Caso o oponente não concorde\n" +
            "com o empate, o jogo continua de onde parou: na vez do jogador\n" +
            "que propôs o empate. Cada jogador só pode propor empate 1 vez.\n" +
            "Vamos ver um exemplo de 'render' a seguir.",

            "\nO xadrez do Hub de Jogos implementa todas as jogadas\n" +
            "especiais do xadrez: En Passant, Roque e Promoção do Peão,\n\n" +
            "além de todas as partidas serem salvas em arquivo .pgn!\n" +
            "Elas ficam na pasta: 'GameHub > Chess > Repository > MatchesPGN'\n\n" +
            "Agora joguem o xadrez e bom jogo!)"
        }; 
        
        public readonly static string[] NavalBattleTutorial =
        {
              "\nBem-vindo a Batalha Naval do Hub de Jogos!\n" +
              "Vamos aprender as regras do jogo:\n\n" +
              "Para cada jogador tem um tabuleiro de 10 x 10 onde as\n" +
              "linhas são marcadas por números e as colunas por letras.\n" +
              "Neste tabuleiro existem 7 navios escondidos. Eles ficam na\n" +
              "horizontal ou na vertical. E tem 3 tamanhos:" +
              "3 de tamanho 2\n" +
              "2 de tamanho 3\n" +
              "2 de tamanho 4\n\n" +
              "As posições dos navios são aleatórios. O objetivo do jogo\n" +
              "é atirar em cada um destes navios escondidos e afundá-los antes\n" +
              "que seu oponente.\n\n" +
              "Veja o tabuleiro:",

              "\nPara escolher onde atirar, primeiro selecione a\n" +
              "coluna e depois a linha da posição.\n" +
              "Exemplos: 'A3', 'B2', 'E8', 'C4'\n\n" +
              "Veja como funciona ao realizar uma jogada:",

              "\nO jogo segue desta maneira, quando um jogador acerta ele pode\n" +
              "continuar atirando, e, quando ele erra, o turno passa para seu oponente.\n\n" +
              "Se tiver dúvidas repita veja o tutorial novamente. Tenha um bom jogo!"
        };

        public readonly static string[] TicTacToeTutorial =
        {
            "\nOlá, bem-vindo ao Jogo da Velha do Hub de Jogos!\n" +
            "Para jogar é muito simples! Vamos aos detalhes:\n\n" +
            "Antes de começar a partida podemos escolher um\n" +
            "tabuleiro de 3x3 até 10x10. Este tutorial usará\n" +
            "o tabuleiro 3x3 como base para os exemplos.\n" +
            "Agora, vamos ver como o tabuleiro do Hub parece! ",

            "\nAssim como no Jogo Da Velha tradicional os símbolos usados\n" +
            "para marcar o tabuleiro são 'X' e 'O'. Mas, para marcar\n" +
            "o tabuleiro aqui é um pouco diferente, temos que informar\n" +
            "a posição que queremos, no caso deste tabuleiro as posições\n" +
            "vão de 1 até 9.\n\n" +
            "Vamos fazer uma jogada a seguir!",

            "\nO jogo continua até um dos jogadores fazer uma combinação\n" +
            "de 3 caracteres ('X' ou 'O') seguidos ou até não existir\n" +
            "mais jogadas possíveis, resultando assim em um empate ou 'Velha'.\n" +
            "A combinação dos caracteres pode ser nas linhas, nas colunas ou\n" +
            "nas diagonais.\n" +
            "A seguir um exemplo onde o 'X' ganha.",

            "\nComo pôde ver, é um jogo bem simples!\n" +
            "Espero que aproveite e se divirta com ele! :)"
        };
    }
}
