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
    List<ModelCharge> warriors;
    public Charge(Unit A, Unit B, Game _g)
    {
        //Pile
        double min = 1000000;
        BasicModel f_m, f_em;
        foreach (BasicModel model in A.Models)
        {
            foreach (BasicModel en_model in B.Models)
            {
                double d = _g.IsMap.distance(en_model.x, en_model.y, model.x, model.y);
                if (d < min)
                {
                    min = d;
                    f_m = model;
                    f_em = en_model;
                }
            }
        }
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
        double x = -1, y = -1;
        bool intersection;
        if (i < 6)
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
                }
                if (intersection == false)
                {
                    f_m.x = x;
                    f_m.y = y;
                    f_m.Moved = 1;
                    ModelCharge f_m_ch = new ModelCharge(f_m);
                    f_m_ch.Enemies.add(f_em);
                    warriors.add(f_m_ch);
                    break;
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
    }
}