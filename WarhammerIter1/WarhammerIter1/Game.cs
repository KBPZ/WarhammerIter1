///////////////////////////////////////////////////////////
//  Game.cs
//  Implementation of the Class Game
//  Generated by Enterprise Architect
//  Created on:      04-���-2014 12:03:36
//  Original author: User
///////////////////////////////////////////////////////////

using System.Collections.Generic;
using System.Windows.Forms;

public enum Pfase
{
    Move,
    Shoot,
    Charge,
    End
}

public interface IPfaseBehav
{
    void ClickMap(int x,int y);
    void ClickActiveButton();
}

public class PfaseMoveBehav : IPfaseBehav
{
    public void ClickMap(int x, int y)
    { }
    public void ClickActiveButton()
    { }
}

public class PfaseShootBehav : IPfaseBehav
{
    public void ClickMap(int x, int y)
    { }
    public void ClickActiveButton()
    { }
}

public class PfaseChargeBehav : IPfaseBehav
{
    public void ClickMap(int x, int y)
    { }
    public void ClickActiveButton()
    { }
}

public class Game 
{
    PfaseMoveBehav MoveBehav;
    PfaseShootBehav ShootBehav;
    PfaseChargeBehav ChargeBehav;
    IPfaseBehav PfaseNow;
	private int NowPlayer;
    private Pfase NowPhase;
	private Player[] Players;
    public Map IsMap;
    public MapInterfeise IsMapInter = new MapInterfeise();
    public MiniMap IsMiniMap = new MiniMap();
	private int Turn;
    public Unit Target;
    public Unit Sourse;
    private DiceGenerator DiceGen;

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
                U.EndPfase(NowPhase, Players[NowPlayer]);
            }
        }
        switch(NowPhase)
        {
            case Pfase.Move:
                break;
            case Pfase.Shoot:
                break;
            case Pfase.Charge:
                break;
        }
    }

    private void BeginPfase()
    {
        foreach (Player p in Players)
        {
            foreach (Unit U in p.PlayersUnit)
            {
                U.EndPfase(NowPhase, Players[NowPlayer]);
            }
        }
        switch (NowPhase)
        {
            case Pfase.Move:
                MessageBox.Show("���� ��������");
                break;
            case Pfase.Shoot:
                MessageBox.Show("���� ��������");
                break;
            case Pfase.Charge:
                MessageBox.Show("���� �����");
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
            Unit p=Target;
            Target = Sourse;
            Sourse = p;
            if(NowPlayer==2)
            {
                NowPlayer = 0;

                if(Turn==5)
                {
                    if(DiceGen.D6()<=4)
                    {
                        MessageBox.Show("����� ����");
                    }
                }
                if (Turn == 6)
                {
                    if (DiceGen.D6() <= 5)
                    {
                        MessageBox.Show("����� ����");
                    }
                }
                if (Turn == 7)
                {
                    MessageBox.Show("����� ����");
                } 
                Turn++;
                MessageBox.Show("����� ���");
            }
            else
            {
                MessageBox.Show("��������� �����");
            }
        }
        else
        {
            //MessageBox.Show("����� ����");
        }
        BeginPfase();
    }

    public Player PlayerNow()
    {
        return Players[NowPlayer];
    }

	public Game()
    {
        List<Unit> LUnit = new List<Unit> {};
        DiceGen = new DiceGenerator();
        Players = new Player[2];
        Players[0] = new Player();
        Players[1] = new Player();
        NowPlayer = 0;
        NowPhase = Pfase.Shoot;
        Turn = 1;
        Sourse = Players[0].PlayersUnit[0];
        Sourse.Models[0].x += 300;
        Sourse.Models[1].x += 300;
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

	/// 
	/// <param name="Target"></param>
	/// <param name="WeaponTyper"></param>
	/// <param name="Sourse"></param>
	public int Shooting(Unit Target, int WeaponTyper, Unit Sourse)
    {
        int Cover = 7;
        List<Wound> L = new List<Wound> { };
        L = Sourse.Shoot(Target,0,DiceGen);
        if (L == null)
            return 0;
        L = Target.Wonding(Sourse, L, DiceGen);
        if (L == null)
            return 0;
        Target.Save(Cover, Sourse,L, DiceGen);
		return 0;
	}

	/// 
	/// <param name="Target"></param>
	/// <param name="Shots"></param>
	/// <param name="HowManyShot"></param>
	public void Wounding(Unit Target, Wound[] Shots, int HowManyShot)
    {

	}

}//end Game