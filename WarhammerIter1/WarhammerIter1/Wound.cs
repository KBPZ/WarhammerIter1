///////////////////////////////////////////////////////////
//  Wound.cs
//  Implementation of the Class Wound
//  Generated by Enterprise Architect
//  Created on:      04-���-2014 12:01:14
//  Original author: Samurai
///////////////////////////////////////////////////////////


using System.Collections.Generic;

public class Wound 
{
    public int dShoot { get;set; }
    public int dWound { get; set; }
	public int ap 
    {
        get;
        set;
    }
	public Effect[] Effects
    {
        get;
        private set;
    }
    public Effect m_Effect
    {
        get;
        private set;
    }
	public int Strenght
    {
        get;
        private set;
    }
    private int faled=0;
    public BasicModel Sourse
    {
        get;
        private set;
    }
    public int BalisticSkills
    {
        get;
        private set;
    }

    public void deleteFail(List<Wound> L)
    {
        List<Wound> Fail = new List<Wound> { };
        foreach(Wound l in L)
        {
            if(l.IsFaled()==1)
            {
                Fail.Add(l);
            }
        }
        foreach(Wound f in Fail)
        {
            L.Remove(f);
        }
    }

    public int IsFaled()
    {
        return faled;
    }

    public void fail()
    {
        faled = 1;
    }

    public void win()
    {
        faled = 0;
    }
    /*
    public int GetStrenght()
    {
        return Strenght;
    }

    public int GetAP()
    {
        return ap;
    }

    /*public int GetBallisticSkills()
    {
        return BalisticSkills;
    }

    public Effect[] GetEffects()
    {
        return Effects;
    }

    public Effect Getm_Effect()
    {
        return m_Effect;
    }*/

	public Wound(int S,int AP,Effect[] Ef,int bs,BasicModel Sor)
    {
        ap = AP;
        Strenght=S;
        Effects=Ef;
        BalisticSkills=bs;
        Sourse=Sor;
	}

	~Wound(){

	}
}//end Wound