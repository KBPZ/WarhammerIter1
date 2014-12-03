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
    public int Moved=0;

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
                    {
                        d++;
                        DelEffectsModelInUnit(b);
                    }
                    if (b.IsAlive() == 0)
                        a++;
                }
                if(0.25<d/(a+d))
                {
                    if(!LeadershipTest(_g))
                    {
                        FallBack = 1;
                        _g.IsShow.ShowMessage("FallBack!");
                    }
                }
                break;
            case Pfase.Charge:
                IsShoot = 0;
                foreach (BasicModel b in Models)
                {
                    b.EndPfase(_g);
                }
                break;
        }
        foreach (BasicModel b in Models)
        {
            if(b.IsAlive()!=1)
                b.EndPfase(_g);
        }

    }

    public Unit(List<BasicModel> ML,List<EffectsUnit> ef)
    {
        Models = ML;
        Effects = ef;
        foreach (BasicModel bm in ML)
        {
            bm.w_Unit = this;
        }
        SpreadEffects();
    }

    private void SpreadEffects()
    {
        String EffUfromModel = null;
        foreach (BasicModel bm in Models)
        {
            foreach (EffectsModel EfM in bm.Effects)
            {
                EffUfromModel = EfM.NameSpreadToUnit();
                if (EffUfromModel != "")
                {
                    int i = 0;
                    for (i = 0; i < Effects.Count; i++)
                    {
                        if (Effects[i].Name() == EffUfromModel)
                            break;
                    }
                    if (i >=Effects.Count)
                        Effects.Add(EfM.SpreadToUnit());
                }
            }
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

    public void Paint(PaintEventArgs e,Game _g)
    {
        foreach(BasicModel B in Models)
        {
            B.Paint(e,_g);
        }
    }

    public List<Wound> Shoot(int type, Game _g)
    {
        List<Wound> L=new List<Wound>{},Lp;
        if (IsShoot == 0)
        {
            IsShoot = 1;
            foreach (BasicModel ShootModel in Models)
            {
                Lp = ShootModel.Shoot(type, _g);
                if(Lp!=null)
                    L.AddRange(Lp);
            }
            int n = L.Count;
            List<int> dice = _g.DiceGen.manyD6(n);
            char a = ' ';
            string dices = new string(a, 1);
            foreach (int di in dice)
            {
                char c = (char)('0' + di);
                dices += c;
                dices += " ";
            }
            //TextBox Box = new TextBox();
            //_g.IsShow.ShowMessage(dices);
            for (int i = 0; i < n; i++)
            {  
                L[i].dShoot = dice[i];
                if (dice[i] < 7 - L[i].BalisticSkills)
                {

                    //L.Remove(L[r.Next() % L.Count]);
                    L[i].fail();
                }
            }
            _g.IsShow.ShowSoots(L);
            if (L.Count != 0)
                L[0].deleteFail(L);
        }
        else
        {
            _g.IsShow.ShowMessage("��� �������");
            return null;
        }
        return L;
    }

    public List<Wound> Wonding(Unit Sourse, List<Wound> Wounds, Game _g)
    {
        int n = Wounds.Count;
        int t=0,Majority=0;
        List<int> dices = _g.DiceGen.manyD6(n);
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
        //_g.IsShow.ShowMessage(TextDices);
        int rer = 0;
        Majority = Majority / t;
        for (int i = 0; i < n;i++)
        {
            Wounds[i].dWound = dices[i];
            if(( Majority-Wounds[i].Strenght + 4)>dices[i])
                Wounds[i].fail();
            if((Majority- Wounds[i].Strenght +4 ) == 7 && dices[i]==6)
                Wounds[i].win();
            if (dices[i] == 1)
                Wounds[i].fail();
            foreach(EffectsWeapons ew in Wounds[i].Effects)
            {
                ew.OnWound(Wounds[i], Wounds, ref rer,_g);
            }
        }
        _g.IsShow.ShowWound(Wounds);
        if (Wounds.Count != 0)
            Wounds[0].deleteFail(Wounds);
        return Wounds;
    }

    public bool LeadershipTest(Game _g)
    {
        int leader = 0,DiceLeader=_g.DiceGen.D6plD6(),rer=0;
        String s = "LeaderTest ";
        s += DiceLeader.ToString();
        _g.IsShow.ShowMessage(s);
        foreach(BasicModel bm in Models)
        {
            leader=Math.Max(bm.Leadership(),leader);
        }
        foreach (EffectsUnit EfU in Effects)
        {
            EfU.Leader(this,ref DiceLeader,ref leader,ref rer,_g);
        }
        if(leader==13||leader>=DiceLeader)
            return true;
        return false;
    }

    public BasicModel First(Unit Sourse)
    {
        foreach (BasicModel m in Models)
        {
            if (m.IsAlive() == 0)
                return m;
        }
        return null;
    }

    public void Save(int Cover,List<Wound> Wounds,Game _g)
    {
        int n = Wounds.Count;
        List<int> dices = _g.DiceGen.manyD6(n);
        char a = ' ';
        string TextDices = new string(a, 1);
        foreach (int d in dices)
        {
            char c = (char)('0' + d);
            TextDices += c;
            TextDices += " ";
        }
        _g.IsShow.ShowMessage(TextDices);


        for (int i = 0; i < n; i++)
        {
            BasicModel m = First(_g.cur_unit);
            if (m == null)
            {
                _g.IsShow.ShowMessage("All dead");
                break;
            }

            m.Save(Wounds[i], dices[i], Cover);
        }
    }

    private void DelEffectsModelInUnit(BasicModel DelModel)
    {
        List<EffectsUnit> ToDel = new List<EffectsUnit> { };
        foreach (EffectsModel EfInd in DelModel.Effects)
        {
            foreach (EffectsUnit EfUni in Effects)
            {
                if (EfInd.NameSpreadToUnit() == EfUni.Name())
                {
                    ToDel.Add(EfUni);
                }
            }
        }
        foreach (EffectsUnit EfToDel in ToDel)
        {
            Effects.Remove(EfToDel);
        }
    }

    public void LeaveIndepChar(BasicModel IndepChar,Game _g)
    {
        int k=0;
        if(this!=IndepChar.w_Unit)
        {
            return;
        }
        foreach(EffectsModel EfUn in IndepChar.Effects)
        {
            k += EfUn.IsIndependetCharecter(_g);
        }
        if(k==0)
        {
            return;
        }
        Models.Remove(IndepChar);
        DelEffectsModelInUnit(IndepChar);
        Unit Indep = new Unit(new List<BasicModel> { IndepChar }, new List<EffectsUnit> { });
        Indep.w_Player = w_Player;
        IndepChar.w_Unit = Indep;
        IndepChar.w_Unit.w_Player.PlayersUnit.Add(Indep);
        _g.IsMap.AllUnits.Add(Indep);
    }

    public void JoinIndepChar(Unit IndepUnit,Game _g)
    {
        if(IndepUnit.Models.Count!=1)
            return;
        int k = 0;
        foreach(EffectsModel EfInd in IndepUnit.Models[0].Effects)
        {
            k += EfInd.IsIndependetCharecter(_g);
        }
        if (k == 0)
            return;
        w_Player.PlayersUnit.Remove(IndepUnit);
        _g.IsMap.AllUnits.Remove(IndepUnit);

        Models.Add(IndepUnit.Models[0]);
        IndepUnit.Models[0].w_Unit = this;
        SpreadEffects();
    }

    public List<BasicModel> SearchIndeps(Game _g)
    {
        List<BasicModel> Indeps = new List<BasicModel> { };
        foreach(BasicModel BasMod in Models)
        {
            foreach(EffectsModel EffMod in BasMod.Effects)
            {
                if(1==EffMod.IsIndependetCharecter(_g))
                {
                    Indeps.Add(BasMod);
                    break;
                }
            }
        }
        return Indeps;
    }

    public bool coherency(Game _g)
    {
        int i, j, k;
        int [,]m= new int[_g.cur_unit.Models.Count, _g.cur_unit.Models.Count];
        for (i = 0; i < _g.cur_unit.Models.Count; i++)
        {
            for (j = 0; j < _g.cur_unit.Models.Count; j++)
            {
                m[i, j] = 0;
            }
        }
        for (i = 0; i < _g.cur_unit.Models.Count; i++)
        {
            m[i, i] = 1;            
        }
        i=0;
        foreach (BasicModel model in _g.cur_unit.Models)
        {
            j=0;
            foreach (BasicModel temp_m in _g.cur_unit.Models)
            {
                if (temp_m != model)
                {
                    if (temp_m.IsAlive() != 0 || (model.x - temp_m.x) * (model.x - temp_m.x) + (model.y - temp_m.y) * (model.y - temp_m.y) <= _g.distance * _g.distance)
                    {
                        m[i, j] = 1;
                        m[j, i] = 1;
                        for(k=0; k<_g.cur_unit.Models.Count; k++)
                        {
                            if (m[j, k] == 1)
                                m[i, k] = 1;
                            if (m[i, k] == 1)
                                m[j, k] = 1;
                        }
                    }
                }                
                j++;
            }
            i++;
        }
        int t;
        bool cor = false;
        for (i = 0; i < _g.cur_unit.Models.Count; i++)
        {
            t = 1;
            for (j = 0; j < _g.cur_unit.Models.Count; j++)
            {
                if(m[i, j]==0)
                {
                    t = 0;
                }
            }
            if(t==1)
            {
                cor = true;
                break;
            }
        }
        return cor;
    }

    public int ChargeDistance (Game _g)
    {
        return 600;
    }
}//end Unit
