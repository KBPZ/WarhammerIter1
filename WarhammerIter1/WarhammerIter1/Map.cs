///////////////////////////////////////////////////////////
//  Map.cs
//  Implementation of the Class Map
//  Generated by Enterprise Architect
//  Created on:      04-���-2014 12:01:15
//  Original author: Samurai
///////////////////////////////////////////////////////////

using System.Collections.Generic;
using System.Windows.Forms;
using System;

public class Map 
{

	public List<Unit> AllUnits;
	private Point Coord;
	private List<Object> MapObjects;

    public Unit FindUnit(int x, int y)
    {
        foreach(Unit unit in AllUnits)
        {
            foreach(BasicModel model in unit.Models)
            {
                if((x-model.x)*(x-model.x)+(y-model.y)*(y-model.y)<=25*25)
                {
                    return unit;
                }
            }
        }
        return null;
    }

    public BasicModel FindModel(int x, int y)
    {
        foreach (Unit unit in AllUnits)
        {
            foreach (BasicModel model in unit.Models)
            {
                if ((x - model.x) * (x - model.x) + (y - model.y) * (y - model.y) <= 25*25)
                {
                    return model;
                }
            }
        }
        return null;
    }

	public Map(List<Unit> Units)
    {
        AllUnits = Units;
	}

	~Map(){

	}

	public virtual void Dispose(){

	}

	/// 
	/// <param name="Click"></param>
	public BasicModel MouseClick(Point Click){

		return null;
	}

	public void Paint(PaintEventArgs e,Game _g)
    {
        foreach(Unit U in AllUnits)
        {
            U.Paint(e,_g);
        }
	}

}//end Map