using Minesweeper.Lib;


internal class Renderer
{
    /// <summary>
    /// Show field with numbers atop and letters on the left.
    /// </summary>
    /// <remarks>See https://theasciicode.com.ar/ </remarks>
    internal static void WriteField(Game game)
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Mines left: {game.MinesLeft}");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();

        Console.Write("  ");
        var nums = Enumerable.Range(0, game.Width).Select(n => $"{(n + 1),2:D}");
        Console.WriteLine(string.Join("", nums));

        for (var y = 0; y < game.Height; y++)
        {
            Console.Write($"{char.ConvertFromUtf32(y + 'A')}│"); // 179

            for (var x = 0; x < game.Width; x++)
            {
                var c = game.Cell(x, y);
                var s = c.State switch
                {
                    DisplayStates.Hide => "▒▒", // 177 x 2
                    DisplayStates.ShowUnarmed => c.Neighbours == 0 ? "  " : $"{c.Neighbours,2:D}",
                    DisplayStates.MarkedBomb => "██", // 219 x 2
                    DisplayStates.OpenBomb => " ╬", // 206
                    _ => "  "
                };
                Console.Write(s);
            }

            Console.WriteLine();
        }

        Console.WriteLine();

        if (game.IsWon)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("You won!");
            Console.ForegroundColor = ConsoleColor.White;
        }
        else if (game.IsLost)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You lost :-(");
            Console.ForegroundColor = ConsoleColor.White;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Enter cell coordinates to step on it.");
            Console.WriteLine("Enter '!' cell coordinates to mark or unmark it.");
            Console.WriteLine("Enter 'quit' to exit the game.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Command > ");
        }
    }
}
