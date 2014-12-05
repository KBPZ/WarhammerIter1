using System;
using System.Collections.Generic;


public class Combat
{
    List<ModelCharge> warriors;
    public Combat(Unit A, Unit B, Game _g)
	{
        
	}
    public void FightSubPf()
    {
        int Initiative;
        List<Wound>[] InitiativeWound= new List<Wound>[2] { new List<Wound> { }, new List<Wound> { } };
        for(Initiative=10;Initiative>0;Initiative--)
        {
            foreach(ModelCharge ModChar in warriors)
            {

            }
        }
    }
}

public class Charge
{
    public List<ModelCharge> warriors = new List<ModelCharge> { };
    public double length;

    public bool check(BasicModel f_m, BasicModel f_em, Game _g, Unit B)
    {
        double[,] m = new double[6, 2];
        int i = 0;
        foreach (BasicModel en_model in B.Models)
        {
            if (_g.IsMap.distance(en_model.x, en_model.y, f_em.x, f_em.y) == 50.0)
            {
                m[i, 0] = en_model.x;
                m[i, 1] = en_model.y;
                i++;
            }
        }
        foreach (BasicModel en_model in _g.cur_unit.Models)
        {
            if (_g.IsMap.distance(en_model.x, en_model.y, f_em.x, f_em.y) == 50.0)
            {
                m[i, 0] = en_model.x;
                m[i, 1] = en_model.y;
                i++;
            }
        }
        double x = -1, y = -1;
        bool intersection = true;
        if (i < 6)
        {
            if (i == 0)
            {
                intersection = false;
                double d = _g.IsMap.distance(f_m.x, f_m.y, f_em.x, f_em.y);
                double sootn = 1 - 50.0 / d;
                if (f_m.x < f_em.x)
                    f_m.x = (f_em.x - f_m.x) * sootn + f_m.x;
                else
                    f_m.x = (f_m.x - f_em.x) * sootn + f_em.x;
                if (f_m.y < f_em.y)
                    f_m.y = (f_em.y - f_m.y) * sootn + f_m.y;
                else
                    f_m.y = (f_m.y - f_em.y) * sootn + f_em.y;
                f_m.Moved = 1;
                ModelCharge f_m_ch = new ModelCharge(f_m);
                f_m_ch.Enemies.Add(f_em);
                warriors.Add(f_m_ch);                
            }
            else
            {
                for (int j = 0; j < i; j++)
                {
                    x = _g.IsMap.triangle_x(f_em.x, f_em.y, m[j, 0], m[j, 1]);
                    y = _g.IsMap.triangle_y(f_em.x, f_em.y, m[j, 0], m[j, 1]);
                    BasicModel vac = _g.IsMap.FindModel(x, y);
                    intersection = false;
                    if (vac == null)
                    {
                        foreach (BasicModel en_model in B.Models)
                        {
                            if (_g.IsMap.squares(x, y, en_model.x, en_model.y, 50) == true)
                            {
                                intersection = true;
                                break;
                            }
                        }
                        foreach (BasicModel en_model in _g.cur_unit.Models)
                        {
                            if (_g.IsMap.squares(x, y, en_model.x, en_model.y, 50) == true)
                            {
                                intersection = true;
                                break;
                            }
                        }
                    }
                    if (intersection == false)
                    {
                        f_m.x = x;
                        f_m.y = y;
                        f_m.Moved = 1;
                        ModelCharge f_m_ch = new ModelCharge(f_m);
                        f_m_ch.Enemies.Add(f_em);
                        warriors.Add(f_m_ch);
                        break;
                    }
                }
            }
        }
        if (intersection == false)
            return true;
        return false;
    }

    public Charge(Unit A, Unit B, double l, Game _g)
    {
        //Pile
        length = l;
        double min = 1000000;
        int i;
        bool found;
        BasicModel f_m=null, f_em=null;
        foreach (BasicModel model in A.Models)
        {
            i = 0;
            found = false;
            for (i = 0; i < warriors.Count; i++)
            {
                if(warriors[i].model==model)
                {
                    found = true;
                    break;
                }
            }
            if (found == false && model.IsAlive() == 0)
            {
                BasicModel en_model = _g.cur_unit.First(model, B, _g);
                double d = _g.IsMap.distance(en_model.x, en_model.y, model.x, model.y);
                if (d < min)
                {
                    min = d;
                    f_m = model;
                    f_em = en_model;
                }
            }
        }
        bool c=check(f_m, f_em, _g, B);
        foreach (BasicModel model in A.Models)
        {
            i = 0;
            found = false;
            for (i = 0; i < warriors.Count; i++)
            {
                if (warriors[i].model == model)
                {
                    found = true;
                    break;
                }
            }
            if (found == false && model.IsAlive() == 0)
            {
                f_m = model;
                f_em = _g.cur_unit.First(f_m, B, _g);
                c = check(f_m, f_em, _g, B);
                List<BasicModel> bad_enemies = new List<BasicModel>();
                while (c == false)
                {                    
                    bad_enemies.Add(f_em);
                    f_em = _g.cur_unit.First(f_m, B, bad_enemies, _g);
                    if (f_em == null)
                    {
                        break;
                    }
                    c = check(f_m, f_em, _g, B);
                }
                if (c == false)
                {
                    f_em = _g.cur_unit.First(f_m, B, _g);
                    double d = _g.IsMap.distance(f_m.x, f_m.y, f_em.x, f_em.y);
                    double sootn = d / length;
                    double x, y;
                    if (f_m.x < f_em.x)
                        x = (f_em.x - f_m.x) * sootn + f_m.x;
                    else
                        x = (f_m.x - f_em.x) * sootn + f_em.x;
                    if (f_m.y < f_em.y)
                        y = (f_em.y - f_m.y) * sootn + f_m.y;
                    else
                        y = (f_m.y - f_em.y) * sootn + f_em.y;
                    bool intersection = true;
                    while (intersection == true)
                    {
                        foreach (BasicModel en_model in B.Models)
                        {
                            if (_g.IsMap.squares(x, y, en_model.x, en_model.y, 50) == true)
                            {
                                intersection = true;
                                break;
                            }
                        }
                        foreach (BasicModel en_model in _g.cur_unit.Models)
                        {
                            if (_g.IsMap.squares(x, y, en_model.x, en_model.y, 50) == true)
                            {
                                intersection = true;
                                break;
                            }
                        }
                        if (intersection == true)
                        {
                            if (f_m.x < f_em.x)
                                x--;
                            else
                                x++;
                            if (f_m.y < f_em.y)
                                y--;
                            else
                                y++;
                            if (x == f_m.x || y == f_m.y)
                                break;
                        }
                    }
                    f_m.x=x;
                    f_m.y=y;                     
                }
            }
        }
    }
}

public class ModelCharge
{
    public BasicModel model;
    public List<BasicModel> Enemies;

    public ModelCharge(BasicModel m)
    {
        model = m;
        Enemies = new List<BasicModel> { };
    }
}