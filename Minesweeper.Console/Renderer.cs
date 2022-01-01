using Minesweeper.Lib;


internal class Renderer
{
    /// <summary>
    /// Show field with numbers atop and letters on the left.
    /// </summary>
    /// <param name="game"></param>
    internal static void WriteField(Game game)
    {
        Console.Clear();
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
                    DisplayStates.Hide => "▓▓", // 178 x 2
                    DisplayStates.ShowNumber => $"{c.Neighbours,2:D}",
                    DisplayStates.OpenBomb => "XX",
                    _ => "  "
                };
                Console.Write(s);
            }

            Console.WriteLine();
        }
    }
}
