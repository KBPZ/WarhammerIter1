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
            Game isGame = new Game(di, new ShowNofing());
            isGame.Shooting();
            isGame.NextPfase();
            Assert.AreEqual(1, isGame.Target.isFallBack());
        }
        [TestMethod]
        public void TestMethod2()
        {
            List<int> d = new List<int> { 1, 2, 2, 4, 5, 6, 4, 6, 4, 6, 5, 6, 6, 6 };
            DiceInt di = new TestDice(d, new List<int> { 2 });
            Weapon ShurikenCatapult = new Assault(12,4, 5, 2, new List<EffectsWeapons> { new baldestorm() });
            Weapon StormBolter = new Assault(24,4, 5, 2, new List<EffectsWeapons> { });
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
            Game IsGame = new Game(F, S, di, new ShowNofing());
            IsGame.cur_unit = IsGame.Players[0].PlayersUnit[0];
            IsGame.Target = IsGame.Players[1].PlayersUnit[0];
            BasicModel TargetFurst = IsGame.Target.Furst(IsGame.cur_unit);
            IsGame.NextPfase();
            IsGame.Shooting();
            IsGame.NextPfase();
            Assert.AreEqual(1, TargetFurst.IsAlive());
            Assert.AreEqual(0, IsGame.Target.isFallBack());
        }
        [TestMethod]
        public void IndependetCharJoinTest()
        {
            List<int> d = new List<int> { 1, 2, 3, 4, 5, 6, 1, 2, 3, 4, 5,
                4, 2, 4, 3, 6, 1, 6,
                4,3,2,3,6,6,6,6,6,6 };
            DiceInt di = new TestDice(d, new List<int> { 2 });
            List<Unit> FirstPlayerUnits, SecondPlayerUnits;
            List<BasicModel> DireAvengers = new List<BasicModel>
            {
                new Infantry(100,150,4,4,3,3,5,9,4,7,new List<Weapon>{new Assault(12,4,5,2,new List<EffectsWeapons>{new baldestorm()})},new List<EffectsModel>{}),
                new Infantry(100,200,4,4,3,3,5,9,4,7,new List<Weapon>{new Assault(12,4,5,2,new List<EffectsWeapons>{new baldestorm()})},new List<EffectsModel>{}),
                new Infantry(100,250,4,4,3,3,5,9,4,7,new List<Weapon>{new Assault(12,4,5,2,new List<EffectsWeapons>{new baldestorm()})},new List<EffectsModel>{}),
                new Infantry(100,300,4,4,3,3,5,9,4,7,new List<Weapon>{new Assault(12,4,5,2,new List<EffectsWeapons>{new baldestorm()})},new List<EffectsModel>{}),
                new Infantry(100,350,4,4,3,3,5,9,4,7,new List<Weapon>{new Assault(12,4,5,2,new List<EffectsWeapons>{new baldestorm()})},new List<EffectsModel>{}),
                new Infantry(100,400,5,5,3,3,5,9,4,7,new List<Weapon>{new Pistol(12,4,5,new List<EffectsWeapons>{new baldestorm()})},new List<EffectsModel>{})
            };
            List<BasicModel> Terminators = new List<BasicModel>
            {
                new Infantry(200,150,4,4,4,4,5,9,2,7,new List<Weapon>{new Assault(24,4,5,2,new List<EffectsWeapons>{})},new List<EffectsModel>{}),
                new Infantry(200,200,4,4,4,4,5,9,2,7,new List<Weapon>{new Assault(24,4,5,2,new List<EffectsWeapons>{})},new List<EffectsModel>{}),
                new Infantry(200,250,4,4,4,4,5,9,2,7,new List<Weapon>{new Assault(24,4,5,2,new List<EffectsWeapons>{})},new List<EffectsModel>{}),
                new Infantry(200,300,4,4,4,4,5,9,2,7,new List<Weapon>{new Assault(24,4,5,2,new List<EffectsWeapons>{})},new List<EffectsModel>{}),
                new Infantry(200,350,4,4,4,4,5,9,2,7,new List<Weapon>{new Assault(24,4,5,2,new List<EffectsWeapons>{})},new List<EffectsModel>{}),
            };
            List<BasicModel> TerminatorLeader = new List<BasicModel>
            {
               new Infantry(200,400,5,5,4,4,5,9,2,7,new List<Weapon>{new Pistol(12,4,5,new List<EffectsWeapons> { new baldestorm() }) }, new List<EffectsModel> { new Fearless(), new IndependetCharecter()})
            };
            FirstPlayerUnits = new List<Unit> { new Unit(DireAvengers, new List<EffectsUnit> { }) };
            SecondPlayerUnits = new List<Unit> { new Unit(Terminators, new List<EffectsUnit> { }), new Unit(TerminatorLeader, new List<EffectsUnit> { }) };
            Game IsGame = new Game(new Player(FirstPlayerUnits), new Player(SecondPlayerUnits), di, new ShowNofing());
            SecondPlayerUnits[0].JoinIndepChar(SecondPlayerUnits[1], IsGame);
            Assert.AreEqual(1, SecondPlayerUnits.Count);
            IsGame.cur_unit = FirstPlayerUnits[0];
            IsGame.Target = SecondPlayerUnits[0];
            IsGame.NextPfase();
            IsGame.Shooting();
            IsGame.NextPfase();
            Assert.AreEqual(0, IsGame.Target.isFallBack());
        }
        [TestMethod]
        public void IndependetCharLeaveTest()
        {
            List<int> d = new List<int> { 1, 2, 3, 4, 5, 6, 1, 2, 3, 4, 5,
                4, 2, 4, 3, 6, 1, 6,
                4,3,2,3,6,6,6 };
            DiceInt di = new TestDice(d, new List<int> { 2 });
            List<Unit> FirstPlayerUnits, SecondPlayerUnits;
            BasicModel TerminatorLeader =
               new Infantry(200, 400, 5, 5, 4, 4, 5, 9, 2, 7,
                   new List<Weapon> { new Pistol(12,4, 5, new List<EffectsWeapons> { new baldestorm() }) },
                   new List<EffectsModel> { new Fearless(), new IndependetCharecter() });
            List<BasicModel> DireAvengers = new List<BasicModel>
            {
                new Infantry(100,150,4,4,3,3,5,9,4,7,new List<Weapon>{new Assault(12,4,5,2,new List<EffectsWeapons>{new baldestorm()})},new List<EffectsModel>{}),
                new Infantry(100,200,4,4,3,3,5,9,4,7,new List<Weapon>{new Assault(12,4,5,2,new List<EffectsWeapons>{new baldestorm()})},new List<EffectsModel>{}),
                new Infantry(100,250,4,4,3,3,5,9,4,7,new List<Weapon>{new Assault(12,4,5,2,new List<EffectsWeapons>{new baldestorm()})},new List<EffectsModel>{}),
                new Infantry(100,300,4,4,3,3,5,9,4,7,new List<Weapon>{new Assault(12,4,5,2,new List<EffectsWeapons>{new baldestorm()})},new List<EffectsModel>{}),
                new Infantry(100,350,4,4,3,3,5,9,4,7,new List<Weapon>{new Assault(12,4,5,2,new List<EffectsWeapons>{new baldestorm()})},new List<EffectsModel>{}),
                new Infantry(100,400,5,5,3,3,5,9,4,7,new List<Weapon>{new Pistol(12,4,5,new List<EffectsWeapons>{new baldestorm()})},new List<EffectsModel>{})
            };
            List<BasicModel> Terminators = new List<BasicModel>
            {
                new Infantry(200,150,4,4,4,4,5,9,2,7,new List<Weapon>{new Assault(12,4,5,2,new List<EffectsWeapons>{})},new List<EffectsModel>{}),
                new Infantry(200,200,4,4,4,4,5,9,2,7,new List<Weapon>{new Assault(12,4,5,2,new List<EffectsWeapons>{})},new List<EffectsModel>{}),
                new Infantry(200,250,4,4,4,4,5,9,2,7,new List<Weapon>{new Assault(12,4,5,2,new List<EffectsWeapons>{})},new List<EffectsModel>{}),
                new Infantry(200,300,4,4,4,4,5,9,2,7,new List<Weapon>{new Assault(12,4,5,2,new List<EffectsWeapons>{})},new List<EffectsModel>{}),
                new Infantry(200,350,4,4,4,4,5,9,2,7,new List<Weapon>{new Assault(12,4,5,2,new List<EffectsWeapons>{})},new List<EffectsModel>{}),
                TerminatorLeader
            };

            FirstPlayerUnits = new List<Unit> { new Unit(DireAvengers, new List<EffectsUnit> { }) };
            SecondPlayerUnits = new List<Unit> { new Unit(Terminators, new List<EffectsUnit> { }) };
            Game IsGame = new Game(new Player(FirstPlayerUnits), new Player(SecondPlayerUnits), di, new ShowNofing());
            SecondPlayerUnits[0].LeaveIndepChar(TerminatorLeader, IsGame);
            IsGame.NextPfase();
            Assert.AreEqual(2, SecondPlayerUnits.Count);
            IsGame.cur_unit = FirstPlayerUnits[0];
            IsGame.Target = SecondPlayerUnits[0];
            IsGame.Shooting();
            IsGame.NextPfase();
            Assert.AreEqual(1, IsGame.Target.isFallBack());
        }
        [TestMethod]
        public void CogerTest()
        {
            List<BasicModel> LInCag = new List<BasicModel>
            {
                new Infantry(25,25,1,1,1,1,1,1,1,1,new List<Weapon>{},new List<EffectsModel>{}),
                new Infantry(25,60,1,1,1,1,1,1,1,1,new List<Weapon>{},new List<EffectsModel>{}),
                new Infantry(300,450,1,1,1,1,1,1,1,1,new List<Weapon>{},new List<EffectsModel>{}),
                new Infantry(300,300,1,1,1,1,1,1,1,1,new List<Weapon>{},new List<EffectsModel>{}),
                new Infantry(25,150,1,1,1,1,1,1,1,1,new List<Weapon>{},new List<EffectsModel>{}),
                new Infantry(25,350,1,1,1,1,1,1,1,1,new List<Weapon>{},new List<EffectsModel>{}),
                new Infantry(25,500,1,1,1,1,1,1,1,1,new List<Weapon>{},new List<EffectsModel>{}),
                new Infantry(300,120,1,1,1,1,1,1,1,1,new List<Weapon>{},new List<EffectsModel>{}),
                new Infantry(150,40,1,1,1,1,1,1,1,1,new List<Weapon>{},new List<EffectsModel>{}),
                new Infantry(200,300,1,1,1,1,1,1,1,1,new List<Weapon>{},new List<EffectsModel>{}),
            };
            Unit InCag = new Unit(LInCag, new List<EffectsUnit> { });
            Game IsGame = new Game(new DiceGenerator(), new ShowNofing());
            IsGame.cur_unit = InCag;
            Assert.AreEqual(true, IsGame.cur_unit.coherency(IsGame));
            List<BasicModel> LNotInCag = new List<BasicModel>
            {
                new Infantry(25,25,1,1,1,1,1,1,1,1,new List<Weapon>{},new List<EffectsModel>{}),
                new Infantry(25,60,1,1,1,1,1,1,1,1,new List<Weapon>{},new List<EffectsModel>{}),
                new Infantry(1000,450,1,1,1,1,1,1,1,1,new List<Weapon>{},new List<EffectsModel>{}),
                new Infantry(1000,300,1,1,1,1,1,1,1,1,new List<Weapon>{},new List<EffectsModel>{}),
                new Infantry(25,150,1,1,1,1,1,1,1,1,new List<Weapon>{},new List<EffectsModel>{}),
                new Infantry(25,350,1,1,1,1,1,1,1,1,new List<Weapon>{},new List<EffectsModel>{}),
                new Infantry(25,500,1,1,1,1,1,1,1,1,new List<Weapon>{},new List<EffectsModel>{}),
                new Infantry(300,120,1,1,1,1,1,1,1,1,new List<Weapon>{},new List<EffectsModel>{}),
                new Infantry(150,40,1,1,1,1,1,1,1,1,new List<Weapon>{},new List<EffectsModel>{}),
                new Infantry(200,300,1,1,1,1,1,1,1,1,new List<Weapon>{},new List<EffectsModel>{}),
            };
            Unit NotInCag = new Unit(LNotInCag, new List<EffectsUnit> { });
            IsGame.cur_unit = NotInCag;
            Assert.AreEqual(false, IsGame.cur_unit.coherency(IsGame));
            Unit Alone = new Unit(new List<BasicModel> { new Infantry(100, 100, 1, 1, 1, 1, 1, 1, 1, 1, new List<Weapon> { }, new List<EffectsModel> { }) }, new List<EffectsUnit> { });
            IsGame.cur_unit = Alone;
            Assert.AreEqual(true, Alone.coherency(IsGame));
        }
    }
}
