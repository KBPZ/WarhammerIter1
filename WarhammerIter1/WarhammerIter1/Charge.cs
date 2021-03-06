﻿using System;
using System.Collections.Generic;

using Warhammer;
namespace Warhammer
{
    public class Combat
    {
        List<ModelCharge> warriors;
        List<Unit> UnitInCombat;
        int round;
        public Combat(Unit A, Unit B, Game _g)
        {
            round = 0;
            warriors = new List<ModelCharge> { };
            UnitInCombat = new List<Unit> { A, B };
            foreach (BasicModel BM in A.Models)
            {
                warriors.Add(new ModelCharge(BM));
            }
            foreach (BasicModel BM in B.Models)
            {
                warriors.Add(new ModelCharge(BM));
            }
        }
        public void FightSubPh(Game _g)
        {
            int Initiative;
            List<Wound>[] InitiativeWound = new List<Wound>[2] { new List<Wound> { }, new List<Wound> { } };
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
                foreach (ModelCharge ModChar in warriors)
                {
                    if (ModChar.model.GetInitiative(_g) == Initiative)
                    {
                        InitiativeWound[ModChar.model.w_Unit.w_Player.PlayerN].AddRange(ModChar.model.CombatAtack(MajT[1 - ModChar.model.w_Unit.w_Player.PlayerN]
                            , MajWS[1 - ModChar.model.w_Unit.w_Player.PlayerN],0));
                    }
                }
                for (int i = 0; i < 2; i++)
                {
                    if (InitiativeWound[i].Count > 0)
                    {
                        ChargeAtackProcessing(InitiativeWound[i], _g);
                        Unit TargetWound = null;
                        foreach (Unit EnU in UnitInCombat)
                        {
                            if (EnU.w_Player.PlayerN != i)
                            {
                                TargetWound = EnU;
                                break;
                            }
                        }
                        if (TargetWound != null)
                            TargetWound.HtHSave(InitiativeWound[1 - TargetWound.w_Player.PlayerN], _g);
                    }
                }
            }
            EndFightSubPhas(_g);
        }
        public void ChargeAtackProcessing(List<Wound> Lwound, Game _g)
        {
            foreach (Wound W in Lwound)
            {
                int MinDice;
                W.dShoot = _g.DiceGen.D6();
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
                int rer = 0;
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
            if (Waste[0] > Waste[1])
            {
                looseTeam = 0;
            }
            else if (Waste[0] < Waste[1])
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
                        if (!u.LeadershipTest(_g, Waste[looseTeam] - Waste[1 - looseTeam]))
                        {
                            u.Destroy(_g);
                            Dead.Add(u);
                        }
                    }
                }
                foreach (Unit u in Dead)
                {
                    //UnitInCombat.Remove(u);
                }
            }
            if (UnitInCombat.Count == 1)
            {
                //_g.AllCombat.Remove(this);
            }
        }
    }

    public class Charge
    {

        public List<Unit> UnitInCombat;
        public List<ModelCharge> warriors = new List<ModelCharge> { };
        public double length;
        int round;

        public bool inter(double x, double y, Game _g)
        {
            BasicModel vac = _g.IsMap.FindModel(x, y);
            bool intersection = false;
            if (vac == null)
            {
                foreach (Unit unit in _g.IsMap.AllUnits)
                {
                    foreach (BasicModel en_model in unit.Models)
                    {
                        if (en_model.IsAlive() == 0 && _g.IsMap.distance(x, y, en_model.x, en_model.y) < 50)
                        {
                            intersection = true;
                            break;
                        }
                    }
                }
            }
            else
                intersection = true;
            return intersection;
        }

        public void FightSubPh(Game _g)
        {
            int Initiative;
            int bonus=0;
            List<Wound>[] InitiativeWound = new List<Wound>[2] { new List<Wound> { }, new List<Wound> { } };
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
                foreach (ModelCharge ModChar in warriors)
                {
                    if (ModChar.model.GetInitiative(_g) == Initiative)
                    {
                        if (_g.PlayerNow() == ModChar.model.w_Unit.w_Player && round == 1)
                            bonus = 1;
                        else
                            bonus = 0;
                        InitiativeWound[ModChar.model.w_Unit.w_Player.PlayerN].AddRange(ModChar.model.CombatAtack(MajT[1 - ModChar.model.w_Unit.w_Player.PlayerN]
                            , MajWS[1 - ModChar.model.w_Unit.w_Player.PlayerN], bonus));
                    }
                }
                for (int i = 0; i < 2; i++)
                {
                    if (InitiativeWound[i].Count > 0)
                    {
                        ChargeAtackProcessing(InitiativeWound[i], _g);
                        Unit TargetWound = null;
                        foreach (Unit EnU in UnitInCombat)
                        {
                            if (EnU.w_Player.PlayerN != i)
                            {
                                TargetWound = EnU;
                                break;
                            }
                        }
                        if (TargetWound != null)
                            TargetWound.HtHSave(InitiativeWound[1 - TargetWound.w_Player.PlayerN], _g);
                    }
                }
            }
            EndFightSubPhas(_g);
        }

        public void ChargeAtackProcessing(List<Wound> Lwound, Game _g)
        {
            foreach (Wound W in Lwound)
            {
                int MinDice;
                W.dShoot = _g.DiceGen.D6();
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
                int rer = 0;
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
            if (Waste[0] > Waste[1])
            {
                looseTeam = 0;
            }
            else if (Waste[0] < Waste[1])
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
                        if (!u.LeadershipTest(_g, Waste[looseTeam] - Waste[1 - looseTeam]))
                        {
                            u.Destroy(_g);
                            Dead.Add(u);
                        }
                    }
                }
                foreach (Unit u in Dead)
                {
                    UnitInCombat.Remove(u);
                }
            }
            if (UnitInCombat.Count == 1)
            {
                // _g.AllCombat.Remove(this);
            }
        }

        public bool check(BasicModel f_m, BasicModel f_em, Game _g, Unit B, List<BasicModel> base_contact)
        {
            double[,] m = new double[6, 2];
            int i = 0;
            foreach (BasicModel en_model in B.Models)
            {
                if (_g.IsMap.distance(en_model.x, en_model.y, f_em.x, f_em.y) == 50.0)
                {
                    m[i, 0] = en_model.x;
                    m[i, 1] = en_model.y;
                    i++;
                }
            }
            foreach (BasicModel en_model in _g.cur_unit.Models)
            {
                if (_g.IsMap.distance(en_model.x, en_model.y, f_em.x, f_em.y) == 50.0)
                {
                    m[i, 0] = en_model.x;
                    m[i, 1] = en_model.y;
                    i++;
                }
            }
            double x = -1, y = -1;
            bool intersection = true;
            if (i < 6)
            {
                if (i == 0)
                {
                    intersection = false;
                    double d = _g.IsMap.distance(f_m.x, f_m.y, f_em.x, f_em.y);
                    double sootn1, sootn2;
                    sootn1 = 50.0 / d;
                    sootn2 = 1 - 50.0 / d;
                    if (f_m.x < f_em.x)
                        m[0, 0] = (f_em.x - f_m.x) * sootn2 + f_m.x;
                    else
                        m[0, 0] = (f_m.x - f_em.x) * sootn1 + f_em.x;
                    if (f_m.y < f_em.y)
                        m[0, 1] = (f_em.y - f_m.y) * sootn2 + f_m.y;
                    else
                        m[0, 1] = (f_m.y - f_em.y) * sootn1 + f_em.y;

                    x = m[0, 0];
                    y = m[0, 1];
                    if (_g.IsMap.distance(f_m.x, f_m.y, x, y) <= length)
                    {
                        intersection = inter(x, y, _g);
                    }
                    else
                        intersection = true;
                    if (intersection == false)
                    {
                        f_m.x = x;
                        f_m.y = y;
                        f_m.Moved = 1;
                        ModelCharge f_m_ch = new ModelCharge(f_m);
                        f_m_ch.Enemies.Add(f_em);
                        warriors.Add(f_m_ch);
                        base_contact.Add(f_m);
                        ModelCharge f_em_ch = new ModelCharge(f_em);
                        warriors.Add(f_em_ch);
                        i = 0;
                    }
                    else
                        i = 1;
                }
                for (int j = 0; j < i; j++)
                {
                    double xx = m[j, 0];
                    double yy = m[j, 1];
                    for (int k = 0; k < 6; k++)
                    {
                        x = _g.IsMap.triangle_x(f_em.x, f_em.y, xx, yy);
                        y = _g.IsMap.triangle_y(f_em.x, f_em.y, xx, yy);
                        if (_g.IsMap.distance(f_m.x, f_m.y, x, y) <= length)
                        {
                            intersection = inter(x, y, _g);
                        }
                        else
                            intersection = true;
                        if (intersection == false)
                        {
                            f_m.x = x;
                            f_m.y = y;
                            f_m.Moved = 1;
                            ModelCharge f_m_ch = new ModelCharge(f_m);
                            f_m_ch.Enemies.Add(f_em);
                            warriors.Add(f_m_ch);
                            base_contact.Add(f_m);
                            ModelCharge f_em_ch = new ModelCharge(f_em);
                            warriors.Add(f_em_ch);
                            break;
                        }
                        xx = x;
                        yy = y;
                    }
                }
            }
            if (intersection == false)
                return true;
            return false;
        }

        public bool BaseToBase(BasicModel f_m, BasicModel f_em, List<BasicModel> base_contact, Game _g)
        {
            double[,] m = new double[6, 2];
            int i = 0;
            foreach (Unit unit in _g.IsMap.AllUnits)
            {
                foreach (BasicModel em in unit.Models)
                {
                    if (_g.IsMap.distance(em.x, em.y, f_em.x, f_em.y) == 50.0)
                    {
                        m[i, 0] = em.x;
                        m[i, 1] = em.y;
                        i++;
                    }
                }
            }
            double x = -1, y = -1;
            bool intersection = true;
            if (i < 6)
            {
                if (i == 0)
                {
                    intersection = false;
                    double d = _g.IsMap.distance(f_m.x, f_m.y, f_em.x, f_em.y);
                    double sootn1, sootn2;
                    sootn1 = 50.0 / d;
                    sootn2 = 1 - 50.0 / d;
                    if (f_m.x < f_em.x)
                        m[0, 0] = (f_em.x - f_m.x) * sootn2 + f_m.x;
                    else
                        m[0, 0] = (f_m.x - f_em.x) * sootn1 + f_em.x;
                    if (f_m.y < f_em.y)
                        m[0, 1] = (f_em.y - f_m.y) * sootn2 + f_m.y;
                    else
                        m[0, 1] = (f_m.y - f_em.y) * sootn1 + f_em.y;

                    x = m[0, 0];
                    y = m[0, 1];
                    if (_g.IsMap.distance(f_m.x, f_m.y, x, y) <= length)
                    {
                        intersection = inter(x, y, _g);
                    }
                    else
                        intersection = true;
                    if (intersection == false)
                    {
                        f_m.x = x;
                        f_m.y = y;
                        base_contact.Add(f_m);
                        i = 0;
                    }
                    else
                        i = 1;
                }
                for (int j = 0; j < i; j++)
                {
                    double xx = m[j, 0];
                    double yy = m[j, 1];
                    for (int k = 0; k < 6; k++)
                    {
                        x = _g.IsMap.triangle_x(f_em.x, f_em.y, xx, yy);
                        y = _g.IsMap.triangle_y(f_em.x, f_em.y, xx, yy);
                        if (_g.IsMap.distance(f_m.x, f_m.y, x, y) <= length)
                        {
                            intersection = inter(x, y, _g);
                        }
                        else
                            intersection = true;
                        if (intersection == false)
                        {
                            f_m.x = x;
                            f_m.y = y;
                            base_contact.Add(f_m);
                            break;
                        }
                        xx = x;
                        yy = y;
                    }
                }
            }
            if (intersection == false)
                return true;
            return false;
        }

        public Charge(Unit A, Unit B, double l, BasicModel stm, BasicModel stem, Game _g)
        {
            //Pile
            round = 1;
            UnitInCombat = new List<Unit> { A, B };
            length = l;
            A.HeadToHead = B.HeadToHead = 1;
            int i;
            bool found;
            List<BasicModel> base_contact = new List<BasicModel> { };
            foreach (Unit unit in _g.IsMap.AllUnits)
            {
                if (unit != _g.cur_unit)
                {
                    foreach (BasicModel m in unit.Models)
                    {
                        foreach (BasicModel em in _g.Target.Models)
                        {
                            if (m.IsAlive() == 0 && em.IsAlive() == 0 && _g.IsMap.distance(m.x, m.y, em.x, em.y) == 50)
                            {
                                base_contact.Add(m);
                            }
                        }
                    }
                }
            }
            BasicModel f_m = null, f_em = null;
            bool c = check(stm, stem, _g, B, base_contact);
            foreach (BasicModel model in A.Models)
            {
                i = 0;
                found = false;
                for (i = 0; i < warriors.Count; i++)
                {
                    if (warriors[i].model == model)
                    {
                        found = true;
                        break;
                    }
                }
                if (found == false && model.IsAlive() == 0)
                {
                    f_m = model;
                    f_em = _g.cur_unit.First(f_m, B, warriors, _g);
                    //f_em = _g.cur_unit.First(f_m, B, _g);
                    c = check(f_m, f_em, _g, B, base_contact);
                    List<BasicModel> bad_enemies = new List<BasicModel>();
                    while (c == false)
                    {
                        bad_enemies.Add(f_em);
                        f_em = _g.cur_unit.First(f_m, B, bad_enemies, warriors, _g);
                        if (f_em == null)
                        {
                            break;
                        }
                        c = check(f_m, f_em, _g, B, base_contact);
                    }
                    if (c == false)
                    {
                        f_em = _g.cur_unit.First(f_m, base_contact, _g, length);
                        if (f_em != null)
                        {
                            c = BaseToBase(f_m, f_em, base_contact, _g);
                        }
                        if (c == false)
                        {
                            f_em = _g.cur_unit.First(f_m, B, _g);
                            double d = _g.IsMap.distance(f_m.x, f_m.y, f_em.x, f_em.y);
                            double x, y;
                            double sootn1, sootn2;
                            sootn1 = 50.0 / d;
                            sootn2 = 1 - 50.0 / d;
                            if (f_m.x < f_em.x)
                                x = (f_em.x - f_m.x) * sootn2 + f_m.x;
                            else
                                x = (f_m.x - f_em.x) * sootn1 + f_em.x;
                            if (f_m.y < f_em.y)
                                y = (f_em.y - f_m.y) * sootn2 + f_m.y;
                            else
                                y = (f_m.y - f_em.y) * sootn1 + f_em.y;
                            bool intersection = true;
                            while (intersection == true)
                            {
                                if (_g.IsMap.distance(f_m.x, f_m.y, x, y) <= length)
                                {
                                    intersection = inter(x, y, _g);
                                }
                                else
                                    intersection = true;
                                if (intersection == true)
                                {
                                    if (f_m.x < f_em.x)
                                        x--;
                                    else
                                        x++;
                                    if (f_m.y < f_em.y)
                                        y--;
                                    else
                                        y++;
                                    if (x == f_m.x || y == f_m.y)
                                        break;
                                }
                            }
                            f_m.x = x;
                            f_m.y = y;
                        }
                    }
                }
            }
            foreach (ModelCharge mch in warriors)
            {
                foreach (BasicModel en in _g.Target.Models)
                {
                    if (mch.Enemies.Contains(en) == false && _g.IsMap.distance(mch.model.x, mch.model.y, en.x, en.y) == 50)
                    {
                        mch.Enemies.Add(en);
                    }
                }
            }
        }
    }

    public class ModelCharge
    {
        public BasicModel model;
        public List<BasicModel> Enemies;

        public ModelCharge(BasicModel m)
        {
            model = m;
            Enemies = new List<BasicModel> { };
        }
    }
}