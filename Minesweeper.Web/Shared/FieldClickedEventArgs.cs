namespace Minesweeper.Web.Shared
{
    public class FieldClickedEventArgs : EventArgs
    {
        public int X { get; set; }

        public int Y { get; set; }
    }
}
