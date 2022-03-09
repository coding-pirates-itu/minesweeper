using Minesweeper.Lib;

internal class Renderer
{
    internal static void WriteField(Game game, int cx, int cy)
    {
        Console.SetCursorPosition(0, 0);

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"Bombs left: {game.Bombs}  ");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;

        for (int y = 0; y < game.Height; y++)
        {
            for (int x = 0; x < game.Width; x++)
            {
                var cell = game.GetCell(x, y);

                if (x == cx && y == cy)
                    Console.BackgroundColor = ConsoleColor.DarkCyan;

                var cellStr = cell.State switch
                {
                    DisplayStates.Hide => "▒▒",
                    DisplayStates.ShowUnarmed =>
                        cell.Neighbours == 0 ? "  " : $" {cell.Neighbours}",
                    DisplayStates.MarkedBomb => "██",
                    DisplayStates.OpenBomb => " ╬",
                    _ => throw new NotImplementedException()
                };
                Console.Write(cellStr);

                if (x == cx && y == cy)
                    Console.BackgroundColor = ConsoleColor.Black;
            }

            Console.WriteLine();
        }

        if (game.IsEnded)
        {
            Console.WriteLine();

            switch (game.State)
            {
                case GameStates.Win:
                    Console.WriteLine("You won!");
                    break;
                case GameStates.Loose:
                    Console.WriteLine("You lost :-(");
                    break;
                case GameStates.Aborted:
                    Console.WriteLine("See you later.");
                    break;
                default:
                    throw new ArgumentException("Unknown state");
            }
        }
    }
}