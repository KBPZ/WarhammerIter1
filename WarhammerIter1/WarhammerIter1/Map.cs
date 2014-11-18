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

	private List<Unit> AllUnits;
	private Point Coord;
	private List<Object> MapObjects;

	public Map(List<Unit> Units)
    {
        AllUnits = Units;
	}

	~Map(){

	}


	/// 
	/// <param name="Click"></param>
	public BasicModel MouseClick(Point Click){

		return null;
	}

	public void Paint(PaintEventArgs e,Player now)
    {
        foreach(Unit U in AllUnits)
        {
            U.Paint(e,now);
        }
	}

}//end Map