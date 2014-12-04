using System;
using System.Collections.Generic;


public class Combat
{
    List<model_charge> warriors;
    List<Unit> UnitInCombat;
    int round;
    public Combat(Unit A, Unit B, Game _g)
    {
        round = 0;
        warriors = new List<model_charge> { };
        UnitInCombat = new List<Unit> { A, B };
        foreach(BasicModel BM in A.Models)
        {
            warriors.Add(new model_charge(BM));
        }
        foreach (BasicModel BM in B.Models)
        {
            warriors.Add(new model_charge(BM));
        }
    }
    public void FightSubPh(Game _g)
    {
        int Initiative;
        List<Wound>[] InitiativeWound= new List<Wound>[2] { new List<Wound> { }, new List<Wound> { } };
        for (Initiative = 10; Initiative > 0; Initiative--)
        {
            InitiativeWound[0].Clear();
            InitiativeWound[1].Clear();
            int[] MajT = new int[2] { 0, 0 };
            int[] MajWS = new int[2] { 0, 0 };
            foreach (Unit U in UnitInCombat)
            {   MajT[U.w_Player.PlayerN] = U.MajorityToughnes();
                MajWS[U.w_Player.PlayerN] = U.MajorityWeaponSkill();
            }
            foreach (model_charge ModChar in warriors)
            {
                if(ModChar.model.GetInitiative(_g)==Initiative)
                {
                    InitiativeWound[ModChar.model.w_Unit.w_Player.PlayerN].AddRange(ModChar.model.CombatAtack(MajT[1 - ModChar.model.w_Unit.w_Player.PlayerN]
                        , MajWS[1 - ModChar.model.w_Unit.w_Player.PlayerN]));
                }
            }
            for(int i=0;i<2;i++)
            {
                if (InitiativeWound[i].Count>0)
                {
                    ChargeAtackProcessing(InitiativeWound[i], _g);
                    Unit TargetWound=null;
                    foreach(Unit EnU in UnitInCombat)
                    {
                        if(EnU.w_Player.PlayerN!=i)
                        {
                            TargetWound = EnU;
                            break;
                        }
                    }
                    if (TargetWound!=null)
                        TargetWound.HtHSave(InitiativeWound[1-TargetWound.w_Player.PlayerN], _g);
                }
            }
        }
        EndFightSubPhas(_g);
    }
    public void ChargeAtackProcessing(List<Wound> Lwound,Game _g)
    {
        foreach(Wound W in Lwound)
        {
            int MinDice;
            W.dShoot=_g.DiceGen.D6();
            if (W.Skills > W.enSkills)
                MinDice = 3;
            else if (2 * W.Skills >= W.enSkills)
                MinDice = 4;
            else
                MinDice = 5;
            if (MinDice > W.dShoot)
                W.fail();
        }
        _g.IsShow.ShowSoots(Lwound);
        Lwound[0].deleteFail(Lwound);
        if (Lwound.Count != 0)
        {
            int rer=0;
            foreach (Wound W in Lwound)
            {
                W.dWound = _g.DiceGen.D6();
                if ((W.enMajT - W.Strenght + 4) > W.dWound)
                    W.fail();
                if ((W.enMajT - W.Strenght + 4) == 7 && W.dWound == 6)
                    W.win();
                if (W.dWound == 1)
                    W.fail();
                foreach (EffectsWeapons ew in W.Effects)
                {
                    ew.OnHtHWound(W, Lwound, ref rer, _g);
                }
            }
            _g.IsShow.ShowWound(Lwound);
            Lwound[0].deleteFail(Lwound);
        }
    }
    public void EndFightSubPhas(Game _g)
    {
         int[] Waste = new int[2] { 0, 0 };
         foreach (Unit U in UnitInCombat)
         {
             Waste[U.w_Player.PlayerN] += U.Waste();
         }
         int looseTeam;
        if(Waste[0]>Waste[1])
        {
            looseTeam = 0;
        }
        else if(Waste[0]<Waste[1])
        {
            looseTeam = 1;
        }
        else
        {
            looseTeam = 3;
        }
        if (looseTeam != 3)
        {
            List<Unit> Dead = new List<Unit> { };
            foreach (Unit u in UnitInCombat)
            {
                if (u.w_Player.PlayerN == looseTeam)
                {
                    if (!u.LeadershipTest(_g, Waste[looseTeam] - Waste[1-looseTeam]))
                    {
                        u.Destroy(_g);
                        Dead.Add(u);
                    }
                }
            }
            foreach(Unit u in Dead)
            {
                UnitInCombat.Remove(u);
            }
        }
        if(UnitInCombat.Count==1)
        {
            _g.AllCombat.Remove(this);
        }
    }
}


public class model_charge
{
    public BasicModel model;
    public List<BasicModel> Enemies;

    public model_charge(BasicModel m)
    {
        model = m;
    }
}