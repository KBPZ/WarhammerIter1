///////////////////////////////////////////////////////////
//  Unit.cs
//  Implementation of the Class Unit
//  Generated by Enterprise Architect
//  Created on:      04-���-2014 12:01:12
//  Original author: Samurai
///////////////////////////////////////////////////////////

using System.Collections.Generic;
using System.Windows.Forms;
using System;

public class Unit 
{
	private int Alive = 0;
	private List<EffectsUnit> Effects;
	public List<BasicModel> Models;
    private int IsShoot=0;
    public int FallBack=0;
	public BasicModel m_BasicModel;
	public Effect m_Effect;
    public Player w_Player;

    public int isFallBack()
    {
        return FallBack;
    }

    public void BeginPfase(Game _g)
    {
        if (Alive < 1)
            Alive = 1;
        foreach (BasicModel b in Models)
        {
            b.BeginPfase(_g);
        }
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

        switch (_g.NowPhase)
        {
            case Pfase.Move:
                break;
            case Pfase.Shoot:
                double a=0,d=0;
                foreach (BasicModel b in Models)
                {
                    if (b.IsAlive() == 2)
                        d++;
                    if (b.IsAlive() == 0)
                        a++;
                }
                if(0.25<d/(a+d))
                {
                    if(!LeadershipTest(_g))
                    {
                        FallBack = 1;
                        MessageBox.Show("FallBack!");
                    }
                }
                break;
            case Pfase.Charge:
                IsShoot = 0;
                break;
                foreach (BasicModel b in Models)
                {
                    b.EndPfase(_g);
                }
        }
        foreach (BasicModel b in Models)
        {
            if(b.IsAlive()!=1)
                b.EndPfase(_g);
        }

    }

	public Unit()
    {
        Models = new List<BasicModel> { };
        Effects = new List<EffectsUnit> { };
        Models.Add(new Infantry());
        Models.Add(new Infantry());
        Models.Add(new Infantry());
        Models[0].x = 200;
        Models[0].y = 200;
        Models[1].x = 100;
        Models[1].y = 100;
        Models[1].x = 300;
        Models[1].y = 300;
        m_BasicModel = Models[1];
        foreach(BasicModel w in Models)
        {
            w.w_Unit=this;
        }
	}

    public void Paint(PaintEventArgs e,Player now)
    {
        foreach(BasicModel B in Models)
        {
            B.Paint(e,now);
        }
    }

    public List<Wound> Shoot(Unit Target, int type, DiceInt d)
    {
        List<Wound> L=new List<Wound>{},Lp;
        if (IsShoot == 0)
        {
            IsShoot = 1;
            foreach (BasicModel ShootModel in Models)
            {
                Lp = ShootModel.Shoot(type, d);
                if(Lp!=null)
                    L.AddRange(Lp);
            }
            int n = L.Count;
            List<int> dice = d.manyD6(n);
            char a = ' ';
            string dices = new string(a, 1);
            foreach (int di in dice)
            {
                char c = (char)('0' + di);
                dices += c;
                dices += " ";
            }
            //TextBox Box = new TextBox();
            MessageBox.Show(dices);
            for (int i = 0; i < n; i++)
            {
                if (dice[i] < 7 - L[i].BalisticSkills)
                {
                    //L.Remove(L[r.Next() % L.Count]);
                    L[i].fail();
                }
            }
            if (L.Count != 0)
                L[0].deleteFail(L);
        }
        else
        {
            MessageBox.Show("��� �������");
            return null;
        }
        return L;
    }

    public List<Wound> Wonding(Unit Sourse, List<Wound> Wounds, DiceInt DiceGen)
    {
        int n = Wounds.Count;
        int t=0,Majority=0;
        List<int> dices = DiceGen.manyD6(n);
        foreach(BasicModel m in Models)
        {
            t++;
            Majority += m.GetToughnes(Sourse);
        }

        char a = ' ';
        string TextDices = new string(a, 1);
        foreach (int d in dices)
        {
            char c = (char)('0' + d);
            TextDices += c;
            TextDices += " ";
        }
        MessageBox.Show(TextDices);

        Majority = Majority / t;
        for (int i = 0; i < n;i++)
        {
            if((Wounds[i].Strenght - Majority + 4)>dices[i])
                Wounds[i].fail();
            if((Wounds[i].Strenght- Majority +4 ) == 7 && dices[i]==6)
                Wounds[i].win();
            if (dices[i] == 1)
                Wounds[i].fail();
        }
        if (Wounds.Count != 0)
            Wounds[0].deleteFail(Wounds);
        return Wounds;
    }

    public bool LeadershipTest(Game _g)
    {
        int leader = 0,DiceLeader=_g.DiceGen.D6plD6();
        String s = "LeaderTest ";
        s += DiceLeader.ToString();
        MessageBox.Show(s);
        foreach(BasicModel bm in Models)
        {
            leader=Math.Max(bm.Leadership(),leader);
        }
        foreach (EffectsUnit EfU in Effects)
        {
            EfU.Leader(this,DiceLeader,leader,_g);
        }
        if(leader==13||leader>=DiceLeader)
            return true;
        return false;
    }

    public BasicModel Furst(Unit Sourse)
    {
        foreach (BasicModel m in Models)
        {
            if (m.IsAlive() == 0)
                return m;
        }
        return null;
    }

    public void Save(int Cover, Unit Sourse, List<Wound> Wounds, DiceInt DiceGen)
    {
        int n = Wounds.Count;
        List<int> dices = DiceGen.manyD6(n);
        char a = ' ';
        string TextDices = new string(a, 1);
        foreach (int d in dices)
        {
            char c = (char)('0' + d);
            TextDices += c;
            TextDices += " ";
        }
        MessageBox.Show(TextDices);


        for (int i = 0; i < n; i++)
        {
            BasicModel m = Furst(Sourse);
            if (m == null)
            {
                MessageBox.Show("All dead");
                break;
            }

            m.Save(Wounds[i], dices[i], Cover);
        }
    }

	~Unit()
    {

	}

	public virtual void Dispose(){

	}

}//end Unit