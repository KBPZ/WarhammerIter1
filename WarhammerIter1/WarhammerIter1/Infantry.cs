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

public class Infantry : BasicModel 
{

	private int ArmorSave = 7;
	private int _Leadership;
	private int Toughnes;

    public override int GetToughnes()
    {
        return Toughnes;
    }

    public override int Leadership()
    {
        return _Leadership;
    }

    public Infantry(int xin, int yin, int bs, int ws, int s, int t,int I, int L, int ArSv, int InvSv, List<Weapon> wea, List<EffectsModel> Ef)
    {
        x = xin;
        y = yin;
        start_x = x;
        start_y = y;
        InvulnerableSave = InvSv;
        _Leadership = L;
        Initiative = I;
        BalisticSkill = bs;
        WeaponSkill = ws;
        Strength = s;
        Toughnes = t;
        ArmorSave = ArSv;
        Weapons = wea;
        //m_Weapons = Weapons[0];
        Effects = Ef;
        foreach (Weapon w in Weapons)
        {
            w.w_BasicModel = this;
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
        Weapons = new List<Weapon> {new Weapon()};
        m_Weapons = Weapons[0];
        Effects = new List<EffectsModel> { };
        foreach(Weapon w in Weapons)
        {
            w.w_BasicModel = this;
        }
	}

    public override List<Wound> CombatAtack(int EnemyWs, int EnemyMajT)
    {
        
        return base.CombatAtack(EnemyWs, EnemyMajT);
    }

    public override int Save(Wound x, int dice,int Cover)
    {
        int ASave = ArmorSave;
        if (x.ap <= ASave)
            ASave = 7;
        ASave = Math.Min(ASave, Math.Min(Cover, InvulnerableSave));
        x.dSave=dice;
        x.Save = ASave;
        if(ASave>dice)
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
            e.Graphics.FillEllipse(B, x - 25, y - 25, 50, 50);
        if(true == IsIndepChar(_g))
        {
            B = new SolidBrush(Color.Navy);
            e.Graphics.FillEllipse(B, x - 10, y - 10, 20, 20);
        }
    }

}//end Infantry
