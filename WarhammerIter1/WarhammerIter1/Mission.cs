using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public abstract class intMission
{
    private int []Score=new int[2];
    public intMission()
    {
    }
    public void Begin() { }
    public void EndTurn(int p, Player pl) { }
    public void End() { }
    public void ShowScore() { }
    public void ShowName() { }
    public void Death() { }
}

class Mission
{
}

public class EturnalWar1 : intMission
{
    public EturnalWar1() { }
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