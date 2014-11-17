using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface intMission
{
    //private int []Score=new int[2]; 
    void Begin(Map m);
    void EndTurn(int p, Player pl);
    void End();
    void ShowScore();
    void ShowName();
}

class Mission
{
}

public  class EturnalWar2 : intMission
{
    public void Begin(Map m)
    {

    }
    public void EndTurn(int p, Player pl)
    {

    }
    public void End()
    {

    }
    public void ShowName()
    {

    }
    public void ShowScore()
    {

    }
}
