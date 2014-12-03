using System;
using System.Collections.Generic;


public class Combat
{
    List<model_charge> warriors;
    public Combat(Unit A, Unit B, Game _g)
	{
        
	}
    public void FightSubPf()
    {
        int Initiative;
        List<Wound>[] InitiativeWound= new List<Wound>[2] { new List<Wound> { }, new List<Wound> { } };
        for(Initiative=10;Initiative>0;Initiative--)
        {
            foreach(model_charge ModChar in warriors)
            {

            }
        }
    }
}


public class model_charge
{
    public BasicModel model;
    public List<BasicModel> Enemies;

    public model_charge(BasicModel m)
    {
        model = m;
    }
}