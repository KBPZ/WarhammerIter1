///////////////////////////////////////////////////////////
//  Effect.cs
//  Implementation of the Class Effect
//  Generated by Enterprise Architect
//  Created on:      04-���-2014 12:01:06
//  Original author: Samurai
///////////////////////////////////////////////////////////

using System.Collections.Generic;
using System.Windows.Forms;
using System;


public abstract class Effect 
{
    public abstract string Name();
}

public abstract class EffectsUnit : Effect
{
    public virtual void Leader(Unit u,ref int res,ref int leadership,ref int ReRoll,Game _g)
    { 
    }
}
public abstract class EffectsWeapons : Effect
{
    public virtual void OnShoot(Wound w, List<Wound> l, ref int ReRoll, Game _g)
    {
    }
    public virtual void OnWound(Wound w, List<Wound> l, ref int ReRoll, Game _g)
    {
    }
}
public abstract class EffectsModel : Effect
{
    public virtual EffectsUnit SpreadToUnit()
    {
        return null;
    }
    public virtual String NameSpreadToUnit()
    {
        return "";
    }
    public virtual void EndTurn(BasicModel m)
    {

    }
    public virtual void EndEnemyTurn(BasicModel m)
    {

    }
    public virtual int IsIndependetCharecter(Game _g)
    {
        return 0;
    }
}
public class baldestorm : EffectsWeapons
{
    public override void OnWound(Wound w, List<Wound> l, ref int ReRoll, Game _g)
    {
        if(w.dWound==6)
        {
            w.ap = 2;
            w.win();
        }
    }
    public override string Name()
    {
        return "Bladestorm";
    }
}
public class FearlessUnit : EffectsUnit
{
    public override void Leader(Unit u, ref int res, ref int leadership, ref int ReRoll, Game _g)
    {
        res = 2;
        leadership = 13;
    }
    public override string Name()
    {
        return "Fearless";
    }
}
public class Fearless : EffectsModel
{
    public override EffectsUnit SpreadToUnit()
    {
        return new FearlessUnit();
    }

    public override String NameSpreadToUnit()
    {
        return "Fearless";
    }

    public override string Name()
    {
        return "Fearless";
    }
}
public class IndependetCharecter : EffectsModel
{
    public override string Name()
    {
        return "IndependetCharecter";
    }
    public override int IsIndependetCharecter(Game _g)
    {
        return 1;
    }
}