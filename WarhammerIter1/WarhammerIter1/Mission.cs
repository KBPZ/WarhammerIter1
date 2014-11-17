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
    void Death();
}

class Mission
{
}

public class EturnalWar1 : intMission
{
    public void Begin(Map m) { }
    public void EndTurn(int p, Player pl) { }
    public void End() { }
    public void ShowScore() { }
    public void ShowName()
    {
        MessageBox.Show("EtWar 1");
    }
    public void Death()
    { }
}
public class EturnalWar2 : intMission
{
    public void Begin(Map m) { }
    public void EndTurn(int p, Player pl) { }
    public void End() { }
    public void ShowScore() { }
    public void ShowName()
    {
        MessageBox.Show("EtWar 2");
    }
    public void Death()
    { }
}
public class EturnalWar3 : intMission
{
    public void Begin(Map m) { }
    public void EndTurn(int p, Player pl) { }
    public void End() { }
    public void ShowScore() { }
    public void ShowName()
    {
        MessageBox.Show("EtWar 2");
    }
    public void Death()
    { }
}