using GameHub.NavalBattle.Model;

namespace GameHub.NavalBattle.View
{
    public class NavalBattleView
    {
        private readonly string _player = @"
                 ██  ██████   ██████   █████  ██████   ██████  ██████  
                 ██ ██    ██ ██       ██   ██ ██   ██ ██    ██ ██   ██ 
                 ██ ██    ██ ██   ███ ███████ ██   ██ ██    ██ ██████  
            ██   ██ ██    ██ ██    ██ ██   ██ ██   ██ ██    ██ ██   ██ 
             █████   ██████   ██████  ██   ██ ██████   ██████  ██   ██ 
        ";
        private readonly string _1 = @"
             ██ 
            ███ 
             ██ 
             ██ 
             ██ 
        ";
        private readonly string _2 = @"
            ██████  
                 ██ 
             █████  
            ██      
            ███████ 
        ";

        public void ShowBoard(BoardNavalBattle board, bool[,] possibleShots)
        {
            Console.Clear();
            ConsoleColor auxBackground = Console.BackgroundColor;
            ConsoleColor auxForeground = Console.ForegroundColor;
            Random random = new Random();

            Console.WriteLine("     a  b  c  d  e  f  g  h  i  j");
            Console.WriteLine("   ┌──────────────────────────────┐");

            for (int i = 0; i < board.Size; i++)
            {
                if (i <= 8)
                    Console.Write($"  {i + 1}");
                else
                    Console.Write($" {i + 1}");

                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("│");
                Console.ForegroundColor = auxForeground;

                for (int j = 0; j < board.Size; j++)
                {
                    int wave = random.Next(1, 11);
                    string aux = "   ";
                    Console.BackgroundColor = ConsoleColor.DarkBlue;

                    if (board.ShipsArray[i, j] != null)
                    {
                        if (board.ShipsArray[i, j].Destroyed)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            wave = 5;
                        }
                    }
                    if (possibleShots[i, j])
                    {
                        wave = 5;
                        aux = " X ";
                    }

                    if (wave <= 2)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write(" ~ ");
                    }
                    else
                        Console.Write(aux);

                    Console.ForegroundColor = auxForeground;
                }

                Console.BackgroundColor = auxBackground;
                Console.Write("│");

                Console.WriteLine();
            }
            Console.WriteLine("   └──────────────────────────────┘");
            Console.ForegroundColor = auxForeground;

            if (board.QuantityOfShips > 0)
                Console.WriteLine($"\n  {board.QuantityOfShips} Navios Restantes");
        }

        public void ShowBoard(BoardNavalBattle board, Position position)
        {
            Console.Clear();
            ConsoleColor bgAux = Console.BackgroundColor;
            ConsoleColor fgAux = Console.ForegroundColor;

            int linha = position.Line;
            int coluna = position.Column;

            Console.WriteLine("     a  b  c  d  e  f  g  h  i  j");
            Console.WriteLine("   ┌──────────────────────────────┐");

            for (int i = 0; i < board.Size; i++)
            {
                if (i <= 8)
                    Console.Write($"  {i + 1}");
                else
                    Console.Write($" {i + 1}");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("│");
                Console.ForegroundColor = fgAux;

                for (int j = 0; j < board.Size; j++)
                {
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    int ondinha = new Random().Next(1, 11);
                    if (board.ShipsArray[i, j] != null)
                    {
                        if (board.ShipsArray[i, j].Destroyed)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            ondinha = 5;
                        }
                    }

                    if (i == linha && j == coluna)
                        Console.Write("-+-");
                    else if (i == linha)
                        Console.Write("---");
                    else if (j == coluna)
                        Console.Write(" | ");
                    else
                    {
                        if (ondinha <= 2)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write(" ~ ");
                            Console.ForegroundColor = fgAux;
                        }
                        else
                            Console.Write("   ");
                    }
                }
                Console.BackgroundColor = bgAux;
                Console.Write("│");

                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("   └──────────────────────────────┘");
            Console.ForegroundColor = fgAux;

            if (board.QuantityOfShips > 0)
                Console.WriteLine($"\n  {board.QuantityOfShips} Navios Restantes");
        }

        public void ShowBoard(BoardNavalBattle board)
        {
            ConsoleColor bgAux = Console.BackgroundColor;
            ConsoleColor fgAux = Console.ForegroundColor;
            ConsoleColor blue = ConsoleColor.DarkBlue;

            Console.WriteLine("  ┌────────────────────┐");

            for (int i = 0; i < board.Size; i++)
            {
                Console.Write("  │");

                for (int j = 0; j < board.Size; j++)
                {
                    Console.BackgroundColor = blue;

                    string aux = board.BoardArray[i, j];
                    if (aux.Trim() == "x")
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                    else if (aux.Trim() == "i")
                        Console.BackgroundColor = ConsoleColor.Black;

                    Console.Write("  ");
                    Console.BackgroundColor = bgAux;
                }

                Console.WriteLine("│");
            }

            Console.WriteLine("  └────────────────────┘");
            Console.ForegroundColor = fgAux;
        }

        public void ShowPlayerTurn(int player)
        {
            Console.Clear();
            Console.WriteLine(Utilities.Utilities.Line);
            Console.WriteLine(_player);

            if (player == 1)
                Console.WriteLine(_1);
            else
                Console.WriteLine(_2);

            Console.WriteLine(Utilities.Utilities.Line);
            Thread.Sleep(500);
        }
    }
}
