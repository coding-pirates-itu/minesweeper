namespace Minesweeper.Lib;


public class Game
{
    #region Fields

    private readonly bool[,] mField;

    private readonly DisplayedCell[,] mCells;

    #endregion


    #region Properties

    public int Width { get; }

    public int Height { get; }

    public bool IsWon { get; private set; }

    public bool IsLost { get; private set; }

    public bool IsEnded => IsWon || IsLost;

    #endregion


    #region Init and clean-up

    /// <summary>
    /// Initialize the game field and distribute the mines.
    /// </summary>
    public Game(int width, int height, int mines)
    {
        Width = width;
        Height = height;
        mField = new bool[width, height];
        mCells = new DisplayedCell[width, height];

        SetMines(mField, mines);
        ClearField(mCells);
    }

    #endregion


    #region API

    /// <summary>
    /// Use for rendering the field.
    /// </summary>
    public DisplayedCell Cell(int x, int y) => mCells[x, y];


    public void Execute(Command command)
    {
        if (command.Operation == Operations.Exit)
        {
            IsLost = true;
            return;
        }
    }

    #endregion


    #region Utility

    private void SetMines(bool[,] mField, int mines)
    {
        var rnd = new Random();
        
        while (mines > 0)
        {
            var x = rnd.Next(Width);
            var y = rnd.Next(Height);
            if (mField[x, y]) continue;

            mField[x, y] = true;
            mines--;
        }
    }


    private void ClearField(DisplayedCell[,] mCells)
    {
        for (var y = 0; y < Height; y++)
            for (var x = 0; x < Width; x++)
                mCells[x, y] = new DisplayedCell();
    }

    #endregion
}
