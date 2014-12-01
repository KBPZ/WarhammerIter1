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
            List<int> d = new List<int> { 1, 2, 3, 4, 5, 6, 4, 4, 4, 6, 5, 3, 6, 6 };
            DiceInt di = new TestDice(d, new List<int> { 2 });
            Game isGame=new Game(di);
            isGame.Shooting();
            isGame.NextPfase();
            Assert.AreEqual(1, isGame.Target.isFallBack());
        }
        [TestMethod]
        public void TestMethod2()
        {
            List<int> d = new List<int> { 1, 2, 2, 4, 5, 6, 4, 6, 4, 6, 5, 6, 6, 6 };
            DiceInt di = new TestDice(d, new List<int> { 2 });
            Weapon ShurikenCatapult = new Assault(4,5,2, new List<EffectsWeapons> { new baldestorm() });
            Weapon StormBolter = new Assault(4,5,2,new List<EffectsWeapons> { });
            List<Unit> DireAvengersUnits = new List<Unit>{
                new Unit(
                    new List<BasicModel>{new Infantry(100, 100, 4, 4, 3, 3, 5,9, 4,7, new List<Weapon> { ShurikenCatapult }, new List<EffectsModel> { }),
                    new Infantry(160, 100, 4, 4, 3, 3, 5,9, 4,7, new List<Weapon> { ShurikenCatapult }, new List<EffectsModel> { })
                    ,new Infantry(100, 160, 4, 4, 3, 3, 5,9, 4,7, new List<Weapon> { ShurikenCatapult }, new List<EffectsModel> { })},
                    new List<EffectsUnit>{new FearlessUnit()})};
            Player F = new Player(DireAvengersUnits);
            List<Unit> TerminatorsUnits = new List<Unit>{
                new Unit(
                    new List<BasicModel>{new Infantry(200, 100, 4, 4, 4, 4, 5,9, 2,7, new List<Weapon> { StormBolter }, new List<EffectsModel> { }),
                    new Infantry(260, 100, 4, 4, 4, 4, 5,9, 2,7, new List<Weapon> { StormBolter }, new List<EffectsModel> { })
                    ,new Infantry(200, 160, 4, 4, 4, 4, 5,9, 2,7, new List<Weapon> { StormBolter }, new List<EffectsModel> { })},
                    new List<EffectsUnit>{new FearlessUnit()})};
            Player S = new Player(TerminatorsUnits);
            Game IsGame = new Game(F, S, di);
            IsGame.cur_unit = IsGame.Players[0].PlayersUnit[0];
            IsGame.Target = IsGame.Players[1].PlayersUnit[0];
            BasicModel TargetFurst = IsGame.Target.Furst(IsGame.cur_unit);
            IsGame.NextPfase();
            IsGame.Shooting();
            IsGame.NextPfase();
            Assert.AreEqual(1,TargetFurst.IsAlive());
            Assert.AreEqual(0, IsGame.Target.isFallBack());
        }
    }
}
