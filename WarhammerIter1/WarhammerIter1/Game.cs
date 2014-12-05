///////////////////////////////////////////////////////////
//  Game.cs
//  Implementation of the Class Game
//  Generated by Enterprise Architect
//  Created on:      04-���-2014 12:03:36
//  Original author: User
///////////////////////////////////////////////////////////

using System.Collections.Generic;
using System.Windows.Forms;
using System;

public interface Show
{
    void ShowMessage(string s);
    void ShowSoots(List<Wound> Lw);
    void ShowWound(List<Wound> Lw);
    void ShowSave(List<Wound> Lw);
}

public class ShowNofing : Show
{
    public void ShowMessage(string s){}
    public void ShowSoots(List<Wound> Lw){}
    public void ShowWound(List<Wound> Lw){}
    public void ShowSave(List<Wound> Lw) { }
}

public class ShowMessageBox : Show
{
    public void ShowMessage(string s)
    {
        MessageBox.Show(s);
    }
    public void ShowSoots(List<Wound> Lw)
    {
        Lw.Sort(delegate(Wound x, Wound y)
        {
            if (x.BalisticSkills > y.BalisticSkills)
                return 1;
            else if (x.BalisticSkills == y.BalisticSkills)
                if (x.Strenght > y.Strenght)
                    return 1;
                else if (x.Strenght == y.Strenght)
                    if (x.ap > y.ap)
                        return 1;
                    else if (x.ap == y.ap)
                        return 0;
                    else
                        return -1;
                else
                    return -1;
            else
                return -1;
        });
        int s=0, bs=0, ap=0;
        string Show = "";
        char p = ' ';
        foreach(Wound w in Lw)
        {
            if(s!=w.Strenght||bs!=w.BalisticSkills||ap!=w.ap)
            {
                if(Show!="")
                {
                    MessageBox.Show(Show,"bs "+ bs.ToString()+" s " + s.ToString() + " ap " + ap.ToString());
                }
                Show = "";
                Show += (char)('0' + w.dShoot); ;
                Show += p;
                s = w.Strenght; bs = w.BalisticSkills; ap = w.ap;
            }
            else
            {
                Show += (char)('0' + w.dShoot);
                Show += p;
            }
        }
        if (Show != "")
        {
            MessageBox.Show(Show, "bs " + bs.ToString() + " s " + s.ToString() + " ap " + ap.ToString());
        }
    }
    public void ShowWound(List<Wound> Lw)
    {
        Lw.Sort(delegate(Wound x, Wound y)
        {
            if (x.Strenght > y.Strenght)
                return 1;
            else if (x.Strenght == y.Strenght)
                if (x.ap > y.ap)
                    return 1;
                else if (x.ap == y.ap)
                    return 0;
                else
                    return -1;
            else
                return -1;
        });
        int s = 0, ap = 0;
        string Show = "";
        char p = ' ';
        foreach (Wound w in Lw)
        {
            if (s != w.Strenght || ap != w.ap)
            {
                if (Show != "")
                {
                    MessageBox.Show(Show,"s " + s.ToString() + " ap " + ap.ToString());
                }
                Show = "";
                Show += (char)('0' + w.dWound);
                Show += p;
                s = w.Strenght; ap = w.ap;
            }
            else
            {
                Show += (char)('0' + w.dWound);
                Show += p;
            }
        }
        if (Show != "")
        {
            MessageBox.Show(Show,"s " + s.ToString() + " ap " + ap.ToString());
        }
    }
    public void ShowSave(List<Wound> Lw)
    {
        int save = 0;
        string Show = "";
        char p = ' ';
        foreach (Wound w in Lw)
        {
            if (w.Save == 0)
                break;
            if (save != w.Save)
            {
                if (Show != "")
                {
                    MessageBox.Show(Show, "Save " + save.ToString());
                }
                Show = "";
                Show += (char)('0' + w.dSave);
                Show += p;
                save = w.Save;
            }
            else
            {
                Show += (char)('0' + w.dSave);
                Show += p;
            }
        }
        if (Show != "")
        {
            MessageBox.Show(Show, "Save " + save.ToString());
        }
    }
}

public interface PfaseSr
{
    void MousClick(int x, int y,Game _g);
    void ActButtonClick(Game _g);
    void IndependentCharecterButtonClick(Game _g);
    void EndPfaseButton(Game _g);
}

public class PfaseNofing:PfaseSr
{
    public void MousClick(int x, int y, Game _g) 
    {

    }
    public void ActButtonClick(Game _g)
    {
        
    }
    public void IndependentCharecterButtonClick(Game _g)
    {

    }
    public void EndPfaseButton(Game _g)
    {
        _g.NextPfase();
    }
}

public class PfaseJoin:PfaseSr
{
    public void MousClick(int x, int y, Game _g)
    {
        //Unit unit = _g.IsMap.FindUnit(x, y); //IsMap - current map
        //_g.cur_model = _g.IsMap.FindModel(x, y);
        _g.cur_unit = _g.IsMap.FindUnit(x, y);

    }
    public void ActButtonClick(Game _g)
    {
        if (_g.cur_unit == null)
        {
            _g.IsShow.ShowMessage("�������� �����.");
        }
        else if (_g.cur_unit.w_Player != _g.PlayerNow())
        {
            _g.IsShow.ShowMessage("���� ����� �� ����������� ���.");
        }
        else
        {
            _g.cur_unit.JoinIndepChar(_g.cur_model.w_Unit, _g);
            _g.NowPfaseStr = _g.ChosePf;
        }
    }
    public void IndependentCharecterButtonClick(Game _g)
    {

    }
    public void EndPfaseButton(Game _g)
    {
        _g.IsShow.ShowMessage("��������� ������������� ������������ ���������");
    }
}

public class PfaseShoot : PfaseSr
{
    public void MousClick(int x, int y, Game _g)
    {
        BasicModel un=_g.IsMap.FindModel(x, y);

        if(un !=null)
        {
            if (un.w_Unit.w_Player == _g.PlayerNow())
            { 
                _g.cur_model = un;
                _g.cur_unit = un.w_Unit;
            }
            else
            {
                _g.Target = un.w_Unit;
            }
        }
    }
    public void ActButtonClick(Game _g)
    {
        _g.Shooting();
    }
    public void IndependentCharecterButtonClick(Game _g)
    {

    }
    public void EndPfaseButton(Game _g)
    {
        _g.NextPfase();
    }
}

public class PfaseChose : PfaseSr
{
    public void MousClick(int x, int y, Game _g)
    {
        //Unit unit = _g.IsMap.FindUnit(x, y); //IsMap - current map
        _g.cur_model = _g.IsMap.FindModel(x, y);
        if (_g.cur_model == null)
            _g.cur_unit = null;
        else
            _g.cur_unit = _g.cur_model.w_Unit;

    }
    public void ActButtonClick(Game _g)
    {
        if (_g.cur_unit==null)
        {
            _g.IsShow.ShowMessage("�������� �����.");
        }        
        else if(_g.cur_unit.w_Player!=_g.PlayerNow())
        {
            _g.IsShow.ShowMessage("���� ����� �� ����������� ���.");
        }
        else if (_g.cur_unit.Moved != 0)
        {
            _g.IsShow.ShowMessage("�� ��� ���������� ���� �����.");
        }        
        else
            _g.NowPfaseStr = _g.MovePf;
    }
    public void IndependentCharecterButtonClick(Game _g)
    {
        if(_g.cur_unit==null)
        {
            return;
        }
        /*
        BasicModel Indep;
        List<BasicModel> Indeps=_g.cur_unit.SearchIndeps(_g);
        if(Indeps.Count==0)
        {
            return;
        }
        Indep = Indeps[0];
        _g.cur_unit.LeaveIndepChar(Indep, _g);*/
        int r = 0;
        foreach(EffectsModel EffMod in _g.cur_model.Effects)
        {
            r += EffMod.IsIndependetCharecter(_g);
        }
        if(r==0)
        {
            return;
        }
        if (_g.cur_unit.Models.Count > 1)
        {
            _g.cur_unit.LeaveIndepChar(_g.cur_model, _g);
        }
        else
        {
            _g.NowPfaseStr = _g.JoinPf;
        }
    }
    public void EndPfaseButton(Game _g)
    {
        _g.NextPfase();
    }
}

public class PfaseMove : PfaseSr
{
    public void MousClick(int x, int y, Game _g)
    {
        BasicModel model=_g.IsMap.FindModel(x, y);
        BasicModel model1 = _g.IsMap.ModelDistance(x, y);
        if (model != null || (model1 != null && model1 != _g.cur_model))
        {
            if (model != null)
            {
                if (model.w_Unit != _g.cur_unit)
                {
                    _g.IsShow.ShowMessage("�� �� ������ ���������� ������ ������.");
                }
                else
                {
                    _g.cur_model = model;
                }
            }
            else
            {
                _g.IsShow.ShowMessage("������ �� ����� ������������.");
            }
        }
        else
        {

            int en = 0;
            foreach (Unit unit in _g.Players[1-_g.NowPlayer].GetUnits())
            {
                foreach (BasicModel t_model in unit.Models)
                {
                    if (t_model.IsAlive() != 1 && _g.IsMap.squares(x, y, t_model.x, t_model.y, _g.enemy_distance) == true)
                    {
                        _g.IsShow.ShowMessage("������� ����� ��������� � ������.");
                        en = 1;
                        break;
                    }
                    foreach (BasicModel c_model in unit.Models)
                    {
                        if (c_model!=t_model)
                        {
                            if (_g.IsMap.squares(c_model.x, c_model.y, t_model.x, t_model.y, _g.enemy_distance) == true)
                            {
                                Point a = new Point(_g.cur_model.x, _g.cur_model.y);
                                Point b = new Point(x, y);
                                Point c = new Point(c_model.x, c_model.y);
                                Point d = new Point(t_model.x, t_model.y);
                                if (a.check_sections(a, b, c, d))
                                {
                                    en = 1;
                                    _g.IsShow.ShowMessage("�� �� ������ ������ ����� ��������� ������.");
                                    break;
                                }
                            }
                        }
                        if (en == 1)
                            break;
                    }
                    if (en == 1)
                        break;
                }
                if (en == 1)
                    break;
            }
            if (en == 0)
            {
                if (_g.IsMap.squares(x, y, _g.cur_model.start_x, _g.cur_model.start_y, _g.length) == true)
                {
                    _g.cur_model.x = x;
                    _g.cur_model.y = y;
                    _g.cur_model.Moved = 1;
                }
                else
                {
                    _g.IsShow.ShowMessage("���������� ����������� ������� ������.");
                }
            }
        }

    }
    public void ActButtonClick(Game _g)
    {
        if (_g.cur_unit.coherency(_g)==false)
        {
            _g.IsShow.ShowMessage("��������� ����� �������� �����������.");
        }
        else
        {
            foreach (BasicModel model in _g.cur_unit.Models)
            {
                model.start_x = model.x;
                model.start_y = model.y;
            }
            _g.cur_model.w_Unit.Moved = 1;
            _g.NowPfaseStr = _g.ChosePf;
        }
    }
    public void IndependentCharecterButtonClick(Game _g)
    {

    }

    public void EndPfaseButton(Game _g)
    {
        _g.IsShow.ShowMessage("��������� ������������ ������");
    }
}

public class PfaseChoseUnit : PfaseSr
{
    public void MousClick(int x, int y, Game _g)
    {
        if(_g.cur_unit==null)
        {
            _g.cur_unit = _g.IsMap.FindUnit(x, y);
        }
        else
        {
            _g.Target = _g.IsMap.FindUnit(x, y);
        }
    }
    public void ActButtonClick(Game _g)
    {
        if (_g.cur_unit == null)
        {
            _g.IsShow.ShowMessage("�������� ��������� �����.");
        }
        if (_g.Target == null)
        {
            _g.IsShow.ShowMessage("�������� ��������� �����.");
        }
        else if (_g.cur_unit.w_Player != _g.PlayerNow())
        {
            _g.IsShow.ShowMessage("��������� ��������� ����� �� ����������� ���.");
        }
        else if (_g.Target.w_Player == _g.PlayerNow())
        {
            _g.IsShow.ShowMessage("��������� ��������� ����� ����������� ���.");
        }
        else if (_g.cur_unit.Moved != 0)
        {
            _g.IsShow.ShowMessage("�� ��� ��������� ������ ������ �������.");
        }
        else
        {
            double min = 10000000;
            BasicModel model=null, en_model=null;
            foreach (BasicModel m in _g.cur_unit.Models)
            {
                if(m.IsAlive()==0)
                {
                    BasicModel em = _g.cur_unit.First(m, _g.Target, _g);
                    double d = _g.IsMap.distance(em.x, em.y, m.x, m.y);
                    if (d < min)
                    {
                        min = d;
                        model = m;
                        en_model = em;
                    }
                }
            }
            
            int en = 0;
            foreach (Unit unit in _g.Players[1 - _g.NowPlayer].GetUnits())
            {
                if (unit != _g.Target)
                {
                    foreach (BasicModel t_model in unit.Models)
                    {
                        foreach (BasicModel c_model in unit.Models)
                        {
                            if (c_model != t_model)
                            {
                                if (_g.IsMap.squares(c_model.x, c_model.y, t_model.x, t_model.y, _g.enemy_distance) == true)
                                {
                                    Point a = new Point(model.x, model.y);
                                    Point b = new Point(en_model.x, en_model.y);
                                    Point c = new Point(c_model.x, c_model.y);
                                    Point d = new Point(t_model.x, t_model.y);
                                    if (a.check_sections(a, b, c, d))
                                    {
                                        en = 1;
                                        break;
                                    }
                                }
                            }
                            if (en == 1)
                                break;
                        }
                        if (en == 1)
                            break;
                    }
                    if (en == 1)
                        break;
                }
            }
            if(en==1)
            {
                _g.IsShow.ShowMessage("��������� ��������� ����� �� �������� ���������.");
            }
            else
            {
                double length = (double)_g.cur_unit.ChargeRange(_g);
                Charge charge = new Charge(_g.cur_unit, _g.Target, length, _g);
                _g.AllCharge.Add(charge);
                _g.cur_unit = null;
                _g.Target = null;
            }
            

        }
    }
    public void IndependentCharecterButtonClick(Game _g)
    {

    }
    public void EndPfaseButton(Game _g)
    {
        _g.NowPfaseStr = _g.ChargePf;
    }
}

public class PfaseCharge : PfaseSr
{
    public void MousClick(int x, int y, Game _g)
    {

    }
    public void ActButtonClick(Game _g)
    {

    }
    public void IndependentCharecterButtonClick(Game _g)
    {

    }
    public void EndPfaseButton(Game _g)
    {
        _g.NextPfase();
    }
}

public enum Pfase
{
    Move,
    Shoot,
    Charge,
    End
}

public class Game 
{
    public Show IsShow { get; private set; }
    public PfaseSr NowPfaseStr { get; set; }
    public PfaseSr MovePf = new PfaseMove();
    public PfaseSr ChosePf = new PfaseChose();
    public PfaseSr NofingPf = new PfaseNofing();
    public PfaseSr ShootPf = new PfaseShoot();
    public PfaseSr JoinPf = new PfaseJoin();
    public PfaseSr ChargePf = new PfaseCharge();
    public PfaseSr ChoseUnitPf = new PfaseChoseUnit();
    public int NowPlayer { get; private set; }
    public Pfase NowPhase { get; private set; }
    public Player[] Players { get; private set; }
    public Map IsMap { get; private set; }
    public MapInterfeise IsMapInter = new MapInterfeise();
    public MiniMap IsMiniMap = new MiniMap();
    public List<Combat> AllCombat = new List<Combat> { };
	private int Turn;
    public Unit Target {get;set;}
    //public Unit Sourse {get;set;}
    public intMission NowMission { get; set; }
    public BasicModel cur_model { get; set; }
    public Unit cur_unit { get; set; }
    public BasicModel cur_en_model { get; set; }
    public int length = 600;
    public int distance = 200;
    public int enemy_distance = 100;
    public int friend_distance = 100;
    public DiceInt DiceGen { get; private set; }
    public List<Charge> AllCharge = new List<Charge> { };

    public  bool IsNowPfase(Pfase p)
    {
        if (p == NowPhase)
            return true;
        return false;
    }

    private void EndPfase()
    {
        foreach (Player p in Players)
        {
            foreach (Unit U in p.PlayersUnit)
            {
                U.EndPfase(this);
            }
        }
        switch(NowPhase)
        {
            case Pfase.Move:
                break;
            case Pfase.Shoot:
                Target = null;
                cur_unit = null;
                cur_model = null;
                cur_en_model = null;
                foreach (Unit unit in Players[NowPlayer].GetUnits())
                {
                    unit.Moved = 0;
                }
                break;
            case Pfase.Charge:
                foreach (Unit unit in Players[NowPlayer].GetUnits())
                {
                    foreach (BasicModel model in unit.Models)
                    {
                        model.Moved = 0;
                    }
                    unit.Moved = 0;
                }
                cur_unit = null;
                Target = null;
                break;
        }
    }

    private void BeginPfase()
    {
        foreach (Player p in Players)
        {
            foreach (Unit U in p.PlayersUnit)
            {
                U.EndPfase(this);
            }
        }
        switch (NowPhase)
        {
            case Pfase.Move:
                IsShow.ShowMessage("���� ��������");
                NowPfaseStr = ChosePf;
                break;
            case Pfase.Shoot:
                IsShow.ShowMessage("���� ��������");
                NowPfaseStr = ShootPf;
                break;
            case Pfase.Charge:
                IsShow.ShowMessage("���� �����");
                NowPfaseStr = ChoseUnitPf;
                break;
        }
    }

    public void NextPfase()
    {
        EndPfase();
        NowPhase++;
        if(NowPhase==Pfase.End)
        {
            NowPhase = Pfase.Move;
            NowPlayer++;
            if(NowPlayer==2)
            {
                NowPlayer = 0;

                if(Turn==5)
                {
                    if(DiceGen.D6()<=4)
                    {
                        IsShow.ShowMessage("����� ����");
                    }
                }
                if (Turn == 6)
                {
                    if (DiceGen.D6() <= 5)
                    {
                        IsShow.ShowMessage("����� ����");
                    }
                }
                if (Turn == 7)
                {
                    IsShow.ShowMessage("����� ����");
                } 
                Turn++;
                IsShow.ShowMessage("����� ���");
            }
            else
            {
                IsShow.ShowMessage("��������� �����");
            }
        }
        else
        {
            //IsShow.ShowMessage("����� ����");
        }
        BeginPfase();
    }

    public Player PlayerNow()
    {
        return Players[NowPlayer];
    }

    public void MouseClick(int x,int y)
    {
        NowPfaseStr.MousClick(x, y,this);
    }

    public void ClickActionButton()
    {
       /* switch(NowPhase)
        {
            case Pfase.Move:
                break;
            case Pfase.Shoot:
                Shooting(Target, 0, Sourse);
                break;
            case Pfase.Charge:
                break;
        }*/
        NowPfaseStr.ActButtonClick(this);
    }

    public void IndependentCharecterButtonClick()
    {
        NowPfaseStr.IndependentCharecterButtonClick(this);
    }

    public Game(Player P1, Player P2, DiceInt DiceG,Show ShowStr)
    {
        Players = new Player[2];
        Players[0] = P1;
        Players[1] = P2;
        DiceGen = DiceG;
        IsShow = ShowStr;
        NowMission = new EturnalWar1();
        NowPhase = Pfase.Move;
        Turn = 1;
        NowPlayer = 0;
        NowPfaseStr = ChosePf;
        List<Unit> LUnit = new List<Unit> { };
        foreach (Player p in Players)
        {
            LUnit.AddRange(p.GetUnits());
        }
        IsMap = new Map(LUnit);
    }

    public Game(DiceInt DiceG, Show ShowStr)
    {
        NowPfaseStr = ShootPf;
        IsShow = ShowStr;
        List<Unit> LUnit = new List<Unit> {};
        NowMission = new EturnalWar1();
        DiceGen = DiceG;
        Players = new Player[2];
        Players[0] = new Player();
        Players[1] = new Player();
        NowPlayer = 0;
        NowPhase = Pfase.Shoot;
        Turn = 1;
        cur_unit = Players[0].PlayersUnit[0];
        cur_unit.Models[0].x += 300;
        cur_unit.Models[1].x += 300;
        cur_unit.Models[2].x += 300;
        Target = Players[1].PlayersUnit[0];
        foreach(Player p in Players)
        {
            LUnit.AddRange(p.GetUnits());
        }
        IsMap = new Map(LUnit);
	}

	~Game()
    {

	}

    public int Shooting()
    {
        if(cur_unit==null||Target==null)
        {
            IsShow.ShowMessage("�� �������� ���� ��� �������");
            return 1;
        }
        int Cover = 7;
        List<Wound> L = new List<Wound> { };
        int Range = (int)IsMap.Range(cur_unit, Target);
        L = cur_unit.Shoot(Range,0, this);
        if (L == null || L.Count==0)
            return 0;
        L = Target.Wonding(cur_unit, L, this);
        if (L == null || L.Count == 0)
            return 0;
        Target.Save(Cover, L, this);
        return 0;
    }

    public void Overwatch()
    {

        int Cover = 7;
        List<Wound> L = new List<Wound> { };
        int Range = (int)IsMap.Range(cur_unit, Target);
        Target.Overvatch(Range, 0, this);
        if (L == null || L.Count == 0)
            return;
        L = Target.Wonding(cur_unit, L, this);
        if (L == null || L.Count == 0)
            return;
        Target.Save(Cover, L, this);
    }

	public void Wounding(Unit Target, Wound[] Shots, int HowManyShot)
    {

	}

    public void HeadToHead()
    {

    }

    public void NewCombat()
    {
        AllCombat.Add(new Combat(cur_unit,Target,this));
    }

}//end Game
