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
using Warhammer;
namespace Warhammer
{
public class Map 
{

	public List<Unit> AllUnits;
	private Point Coord;
	private List<Object> MapObjects;

    public double Range(Unit A, Unit B)
    {
        //int Dis=_g.cur_unit.ChargeRange(_g);
        double min = 1000000;
        foreach (BasicModel model in A.Models)
        {
            foreach (BasicModel en_model in B.Models)
            {
                double d=distance(en_model.x, en_model.y, model.x, model.y);
                if (en_model.IsAlive() == 0 && d < min)
                {
                    min = d;
                }
            }
        }        
        return min;
    }

    public Unit FindUnit(int x, int y)
    {
            foreach (Unit unit in AllUnits)
        {
                foreach (BasicModel model in unit.Models)
            {
                if(model.IsAlive() == 0 && (x-model.x)*(x-model.x)+(y-model.y)*(y-model.y)<=625)
                {
                    return unit;
                }
            }
        }
        return null;
    }

    public bool squares(double a, double b, double c, double d, double e)
    {
        return (a - c) * (a - c) + (b - d) * (b - d) <= e * e;
    }

    public double distance(double a, double b, double c, double d)
    {
        return Math.Sqrt((a - c) * (a - c) + (b - d) * (b - d));
    }

    public double triangle_x(double x1, double y1, double x2, double y2)
    {
        return (x2 - x1) * Math.Cos(60 * Math.PI / 180) - (y2 - y1) * Math.Sin(60 * Math.PI / 180) + x1;
    }

    public double triangle_y(double x1, double y1, double x2, double y2)
    {
        return (x2 - x1) * Math.Sin(60 * Math.PI / 180) + (y2 - y1) * Math.Cos(60 * Math.PI / 180) + y1;
    }

    public BasicModel FindModel(double x, double y)
    {
        foreach (Unit unit in AllUnits)
        {
            foreach (BasicModel model in unit.Models)
            {
                if (model.IsAlive() == 0 && squares(x, y, model.x, model.y, 25) == true)
                {
                    return model;
                }
            }
        }
        return null;
    }

    public BasicModel ModelDistance(double x, double y)
    {
        foreach (Unit unit in AllUnits)
        {
            foreach (BasicModel model in unit.Models)
            {
                if (model.IsAlive() == 0 && squares(x, y, model.x, model.y, 50) == true)
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

        ~Map()
        {

	}

        public virtual void Dispose()
        {

	}

	/// 
	/// <param name="Click"></param>
        public BasicModel MouseClick(Point Click)
        {

		return null;
	}

        public void Paint(PaintEventArgs e, Game _g)
    {
            foreach (Unit U in AllUnits)
        {
                U.Paint(e, _g);
        }
	}

}//end Map
}