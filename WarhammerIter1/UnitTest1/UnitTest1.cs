using System;
using WarhammerIter1;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<int> d = new List<int> {1,2,3,4,5,6,4,4,4,6,5,3,6,6};
            DiceInt di = new TestDice(d, new List<int> { 2 });
            Game isGame=new Game(di);
            isGame.Shooting();
            isGame.NextPfase();
            Assert.AreEqual(1, isGame.Target.isFallBack());
        }
    }
}
