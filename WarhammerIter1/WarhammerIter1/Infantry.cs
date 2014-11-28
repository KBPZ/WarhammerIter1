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

    public override int GetToughnes(Unit Surce)
    {
        return Toughnes;
    }


    public override int Leadership()
    {
        return _Leadership;
    }

    public Infantry(int xin, int yin, int bs, int ws, int s, int t, int l, int ArSv, List<Weapon> wea, List<EffectsModel> Ef)
    {
        x = xin;
        y = yin;
        _Leadership = l;
        BalisticSkill = bs;
        WeaponSkill = ws;
        Strength = s;
        Toughnes = t;
        ArmorSave = ArSv;
        Weapons = wea;
        m_Weapons = Weapons[0];
        Effects = Ef;
        foreach (Weapon w in Weapons)
        {
            w.w_BasicModel = this;
        }
    }

	public Infantry()
    {
        x = y = 100;
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

    public override int Save(Wound x, int dice,int Cover)
    {
        int ASave = ArmorSave;
        if (x.ap <= ASave)
            ASave = 7;
        ASave = Math.Min(ASave, Math.Min(Cover, InvulnerableSave));
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

	public override void Paint(PaintEventArgs e,Player now)
    {
        SolidBrush B;
        if (w_Unit.w_Player == now)
            B = new SolidBrush(Color.Snow);
        else
            B = new SolidBrush(Color.DarkRed);
        if(Alive==0)
            e.Graphics.FillEllipse(B, x-50, y-50, 50, 50);
	}

}//end Infantry