using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public abstract class intMission
{
    private int[] Score = new int[2];
    public intMission()
    {
    }
    public virtual void Begin(Map m) { }
    public virtual void EndTurn(int p, Player pl) { }
    public virtual void End() { }
    public virtual void ShowScore() { }
    public virtual void ShowName() { }
    public virtual void Death() { }
}

class Mission
{
}

public class EturnalWar1 : intMission
{
    public EturnalWar1() { }
    public override void Begin(Map m) { }
    public override void EndTurn(int p, Player pl) { }
    public override void End() { }
    public override void ShowScore() { }
    public override void ShowName()
    {
        MessageBox.Show("EtWar 1");
    }
    public override void Death()
    { }
}
public class EturnalWar2 : intMission
{
    public override void Begin(Map m) { }
    public override void EndTurn(int p, Player pl) { }
    public override void End() { }
    public override void ShowScore() { }
    public override void ShowName()
    {
        MessageBox.Show("EtWar 2");
    }
    public override void Death()
    { }
}
public class EturnalWar3 : intMission
{
    public override void Begin(Map m) { }
    public override void EndTurn(int p, Player pl) { }
    public override void End() { }
    public override void ShowScore() { }
    public override void ShowName()
    {
        MessageBox.Show("EtWar 2");
    }
    public override void Death()
    { }
}