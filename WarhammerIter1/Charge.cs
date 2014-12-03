using System;
using System.Collections.Generic;


public class Charge
{
    List<model_charge> warriors;
	public Charge(Unit A, Unit B, Game _g)
	{
        
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