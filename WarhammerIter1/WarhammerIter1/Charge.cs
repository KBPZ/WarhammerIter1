using System;
using System.Collections.Generic;


public class Combat
{
    List<model_charge> warriors;
    List<Unit> UnitInCombat;
    public Combat(Unit A, Unit B, Game _g)
    {
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
    public void FightSubPf(Game _g)
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
            {
                MajT[U.w_Player.PlayerN] = U.MajorityToughnes();
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
                    Unit TargetWound;
                    foreach(Unit EnU in UnitInCombat)
                    {
                        if(EnU.w_Player.PlayerN!=i)
                        {
                            TargetWound = EnU;
                            break;
                        }
                    }
                    //TargetWound.HtHSave(InitiativeWound[1-TargetWound.w_Player.PlayerN], _g);
                }
            }
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