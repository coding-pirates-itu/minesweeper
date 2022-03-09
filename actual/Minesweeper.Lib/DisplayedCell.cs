﻿namespace Minesweeper.Lib;


public class DisplayedCell
{
    public DisplayStates State { get; set; }

    public int Neighbours { get; set; }


    public override string ToString()
    {
        return State.ToString();
    }
}
