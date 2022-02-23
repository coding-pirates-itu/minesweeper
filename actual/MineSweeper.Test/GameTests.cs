using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MineSweeper.Test;


[TestClass]
public class GameTests
{
    [TestMethod]
    public void GameInit_Standard()
    {
        var sut = new Game(4, 4, 1);
        Assert.AreEqual(4, sut.Width);
        Assert.AreEqual(1, sut.Bombs);
    }


    [TestMethod]
    public void GameInit_Full()
    {
        var sut = new Game(4, 4, 16);
    }


    [TestMethod]
    public void GameInit_Overfloat()
    {
        Assert.ThrowsException<ArgumentException>(() =>
            new Game(4, 4, 17));
    }
}
