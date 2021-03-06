///////////////////////////////////////////////////////////
//  BasicModel.cs
//  Implementation of the Class BasicModel
//  Generated by Enterprise Architect
//  Created on:      04-���-2014 12:01:08
//  Original author: Samurai
///////////////////////////////////////////////////////////

using System.Collections.Generic;
using System.Windows.Forms;
using System;
using System.Drawing;
using Warhammer;
namespace Warhammer
{
    public abstract class BasicModel
    {

        protected int Alive = 0;
        protected int BalisticSkill;
        protected int Initiative;
        protected int InvulnerableSave = 7;
        protected int Move = 6;
        protected int Stilness = 0;
        protected int Strength;
        public int Atack { get; protected set; }
        public int Moved = 0;
        protected List<Weapon> Weapons;
        protected Weapon m_Weapons;
        public List<EffectsModel> Effects { get; protected set; }
        public Unit w_Unit;
        public int WeaponSkill { get; protected set; }
        public int Wound { get; protected set; }
        public double x, y;
        public Weapon ActiveRangeWeapon;
        public Weapon ActiveMeleWeapon;


        public abstract String Character();

        public int MoveRange()
        {
            return 6;
        }

        public double RadiusBase()
        {
            return 0.5;
        }

        public void Destroy(Game _g)
        {
            Alive = 1;
        }

        public virtual List<Wound> CombatAtack(int EnemyWs, int EnemyMajT,int bonus)
        {
            return new List<Wound> { };
        }

        public virtual int GetInitiative(Game _g)
        {
            return Initiative;
        }

        public virtual int Leadership()
        {
            return 13;
        }

        public double start_x, start_y;

        public virtual int DificltMoveRange(DiceInt d)
        {
            return Math.Max(d.D6(), d.D6());
        }

        public List<String> ListWeapon(Game _g)
        {
            List<String> r = new List<string> { };
            foreach(Weapon W in Weapons)
            {
                if(W==ActiveMeleWeapon||W==ActiveRangeWeapon)
                    r.Add("*" + W.WeaponChar(_g));
                else
                    r.Add(W.WeaponChar(_g));
            }
            return r;
        }

        public void ActiveWeapon(String a,Game _g)
        {
            foreach (Weapon weap in Weapons)
            {
                if (weap.WeaponChar(_g) == a)
                {
                    if (weap.IsHtHWeapon() == 0)
                        ActiveRangeWeapon = weap;
                    if (weap.IsSpecialHtHWeapon() != 0)
                        ActiveRangeWeapon = weap;
                } break;
            }
        }

        public void BeginPfase(Game _g)
        {
            if (Alive < 1)
                Alive = 1;
            switch (_g.NowPhase)
            {
                case Pfase.Move:
                    break;
                case Pfase.Shoot:
                    break;
                case Pfase.Charge:
                    break;
            }
        }

        public void EndPfase(Game _g)
        {
            if (Alive > 1)
                Alive = 1;
            switch (_g.NowPhase)
            {
                case Pfase.Move:
                    break;
                case Pfase.Shoot:
                    break;
                case Pfase.Charge:
                    break;
            }
        }

        virtual public int Save(Wound x, int dice, int Cover)
        {
            return 0;
        }

        virtual public int HtHSave(Wound x, int dice)
        {
            return 0;
        }

        public int IsAlive()
        {
            return Alive;
        }

        public virtual int GetToughnes()
        {
            return 4;
        }

        public BasicModel()
        {
            Wound = 1;
        }

        public bool IsIndepChar(Game _g)
        {
            int k = 0;
            foreach (EffectsModel Eff in Effects)
            {
                k += Eff.IsIndependetCharecter(_g);
            }
            if (k == 0)
                return false;
            return true;
        }

        public virtual List<Wound> Shoot(int Range, int t, Game _g)
        {
            if (Alive == 0)
            {
                List<Wound> L = new List<Wound> { };
                L.AddRange(Weapons[0].Shoot(Moved, BalisticSkill, Range));
                return L;
            }
            else
                return null;
        }

        public virtual List<Wound> Overwatch(int Range, int t, Game _g)
        {
            if (Alive == 0)
            {
                List<Wound> L = new List<Wound> { };
                L.AddRange(Weapons[0].SnapShoots(Range));
                return L;
            }
            else
                return null;
        }


        ~BasicModel()
        {

        }

        public abstract void Paint(PaintEventArgs e, Game _g);

    }//end BasicModel
}