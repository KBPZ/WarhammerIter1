///////////////////////////////////////////////////////////
//  Infantry.cs
//  Implementation of the Class Infantry
//  Generated by Enterprise Architect
//  Created on:      04-���-2014 12:01:10
//  Original author: Samurai
///////////////////////////////////////////////////////////
using System.Collections.Generic;
using System.Windows.Forms;
using System;
using System.Drawing;
using Warhammer;
namespace Warhammer
{
    public class Infantry : BasicModel
    {

        private int ArmorSave = 7;
        private int _Leadership;
        private int Toughnes;

        public override string Character()
        {
            String r = "WS BS S T W I A LD SV INSV\n";
            r = r + WeaponSkill.ToString() + "    " + BalisticSkill.ToString() + "    " + Strength.ToString() + " " + Toughnes.ToString() + "  " + Wound.ToString() 
                + " " + Initiative.ToString() + " "
                + Atack.ToString() + " " + _Leadership.ToString() + "   " + ArmorSave.ToString() + "     " + InvulnerableSave.ToString() + "\n";
           return r;
        }

        public override int GetToughnes()
        {
            return Toughnes;
        }

        public override int Leadership()
        {
            return _Leadership;
        }

        public Infantry(int xin, int yin, int bs, int ws, int s, int t, int w, int I, int a, int L, int ArSv, int InvSv, List<Weapon> wea, List<EffectsModel> Ef)
        {
            x = xin;
            y = yin;
            Atack = a;
            start_x = x;
            start_y = y;
            InvulnerableSave = InvSv;
            _Leadership = L;
            Initiative = I;
            Wound = w;
            BalisticSkill = bs;
            WeaponSkill = ws;
            Strength = s;
            Toughnes = t;
            ArmorSave = ArSv;
            Weapons = wea;
            //m_Weapons = Weapons[0];
            Effects = Ef;
            foreach (Weapon weap in Weapons)
            {
                weap.w_BasicModel = this;
            }
            foreach (Weapon weap in Weapons)
            {
                if (weap.IsHtHWeapon() == 0)
                {
                    ActiveRangeWeapon = weap;
                    break;
                }
            }

            foreach (Weapon weap in Weapons)
            {
                if (weap.IsSpecialHtHWeapon() != 0)
                {
                    ActiveMeleWeapon = weap;
                    break;
                }
            }
        }

        public Infantry()
        {
            x = y = 100;
            start_x = start_y = 100;
            _Leadership = 8;
            BalisticSkill = 3;
            WeaponSkill = 3;
            Strength = 3;
            Toughnes = 4;
            ArmorSave = 5;
            Weapons = new List<Weapon> { new Weapon() };
            m_Weapons = Weapons[0];
            Effects = new List<EffectsModel> { };
            foreach (Weapon w in Weapons)
            {
                w.w_BasicModel = this;
            }
        }

        public override List<Wound> CombatAtack(int EnemyWs, int EnemyMajT,int bonus)
        {
            if (Alive == 0)
            {
                List<Weapon> CCW = new List<Weapon> { };
                List<Weapon> SCCW = new List<Weapon> { };
                List<Wound> L; //= new List<Wound> { };
                foreach (Weapon W in Weapons)
                {
                    if (W.IsHtHWeapon() == 1)
                    {
                        CCW.Add(W);
                    }
                    if (W.IsSpecialHtHWeapon() == 1)
                        SCCW.Add(W);
                }
                if (CCW.Count > 1)
                    bonus++;
                if (SCCW.Count > 0)
                    L = SCCW[0].HeadToHead(Atack + bonus, Strength, EnemyMajT, WeaponSkill, EnemyWs);
                else if (SCCW.Count > 0)
                    L = CCW[0].HeadToHead(Atack + bonus, Strength, EnemyMajT, WeaponSkill, EnemyWs);
                else
                    L = Weapons[0].HeadToHead(Atack + bonus, Strength, EnemyMajT, WeaponSkill, EnemyWs);
                return L;
            }
            return new List<Wound> { };
        }

        public override int Save(Wound x, int dice, int Cover)
        {
            int ASave = ArmorSave;
            if (x.ap <= ASave)
                ASave = 7;
            ASave = Math.Min(ASave, Math.Min(Cover, InvulnerableSave));
            x.dSave = dice;
            x.Save = ASave;
            if (ASave > dice)
            {
                Wound--;
                if (x.Strenght >= Toughnes * 2)
                    Wound = 0;
            }
            if (Wound <= 0)
            {
                Alive = 2;
                return 2;
            }
            return 0;
        }

        public override int HtHSave(Wound x, int dice)
        {
            int ASave = ArmorSave;
            if (x.ap <= ASave)
                ASave = 7;
            ASave = Math.Min(ASave, InvulnerableSave);
            x.dSave = dice;
            x.Save = ASave;
            if (ASave > dice)
            {
                Wound--;
            }
            if (Wound <= 0)
            {
                Alive = 2;
                return 2;
            }
            return 0;
        }

        ~Infantry()
        {

        }

        public override void Paint(PaintEventArgs e, Game _g)
        {
            SolidBrush B;
            if (w_Unit.w_Player == _g.PlayerNow())
            {
                if (w_Unit == _g.cur_unit)
                    B = new SolidBrush(Color.BlueViolet);
                else
                    B = new SolidBrush(Color.Snow);
                if (this == _g.cur_model)
                    B = new SolidBrush(Color.Blue);
            }
            else
                if (w_Unit == _g.Target)
                    B = new SolidBrush(Color.DarkRed);
                else
                    B = new SolidBrush(Color.Red);
            if (Alive == 0)
            {
                e.Graphics.FillEllipse(B, (int)x - 25, (int)y - 25, 50, 50);
                if (true == IsIndepChar(_g))
                {
                    B = new SolidBrush(Color.Navy);
                    e.Graphics.FillEllipse(B, (int)x - 10, (int)y - 10, 20, 20);
                }
            }
        }

    }//end Infantry
}