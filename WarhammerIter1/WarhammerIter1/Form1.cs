using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Warhammer;
namespace WarhammerIter1
{
    public partial class Form1 : Form
    {
        Game IsGame; //= new Game(new DiceGenerator());
        private void Form1_Shown(object sender, EventArgs e)
        {
            BasicModel Autarh = new Infantry(40, 160, 5, 5, 3, 3,3, 5,3, 10, 2, 4,
                new List<Weapon>{new Heavy(24,6,5,3,new List<EffectsWeapons>{new baldestorm()}),new Pistol(12,4,5,new List<EffectsWeapons> { new baldestorm() })
                    },
                new List<EffectsModel>() { new IndependetCharecter() });
            Weapon ShurikenCatapult = new Assault(12, 4, 5, 2, new List<EffectsWeapons> { new baldestorm() });
            Weapon StormBolter = new Assault(24,4, 5, 2, new List<EffectsWeapons> { });
            List<Unit> DireAvengersUnits = new List<Unit>{
                new Unit(
                    new List<BasicModel>{new Infantry(100, 100, 4, 4, 3, 3,1, 5,1,9, 4, 7,new List<Weapon> { ShurikenCatapult }, new List<EffectsModel> { }),
                    new Infantry(160, 100, 4, 4, 3, 3,1, 5,1,9, 4,7, new List<Weapon> { ShurikenCatapult }, new List<EffectsModel> { }),
                    new Infantry(100, 160, 4, 4, 3, 3,1, 5,1,9, 4, 7,new List<Weapon> { ShurikenCatapult }, new List<EffectsModel> { }),
                    new Infantry(100, 220, 4, 4, 3, 3,1, 5,1,9, 4,7, new List<Weapon> { ShurikenCatapult }, new List<EffectsModel> { }),
                    new Infantry(160, 220, 4, 4, 3, 3,1, 5,1,9, 4,7, new List<Weapon> { /*new Heavy(24,6,5,3,new List<EffectsWeapons>{new baldestorm()})*/ShurikenCatapult }, new List<EffectsModel> { }),
                    Autarh},
                    new List<EffectsUnit>{}),
                    new Unit(
                    new List<BasicModel>{new Infantry(500, 100, 4, 4, 3, 3,1, 5,1,9, 4,7, new List<Weapon> { ShurikenCatapult }, new List<EffectsModel> { }),
                    new Infantry(560, 100, 4, 4, 3, 3,1, 5,1,9, 4,7, new List<Weapon> { ShurikenCatapult }, new List<EffectsModel> { }),
                    new Infantry(500, 160, 4, 4, 3, 3,1, 5,1,9, 4,7, new List<Weapon> { ShurikenCatapult }, new List<EffectsModel> { }),
                    new Infantry(500, 220, 4, 4, 3, 3,1, 5,1,9, 4,7, new List<Weapon> { ShurikenCatapult }, new List<EffectsModel> { }),
                    new Infantry(560, 220, 4, 4, 3, 3,1, 5,1,9, 4,7, new List<Weapon> { ShurikenCatapult }, new List<EffectsModel> { }),},new List<EffectsUnit>{})};
            Player F = new Player(DireAvengersUnits);
            List<Unit> TerminatorsUnits = new List<Unit>{
                new Unit(
                    new List<BasicModel>{new Infantry(300, 100, 4, 4, 4, 4, 1,4,2,9, 2,5, new List<Weapon> { StormBolter,new MuteCCW(2,2,new List<EffectsWeapons>{})}, new List<EffectsModel> { }),
                    new Infantry(360, 100, 4, 4, 4, 4, 1, 4,2,9, 2,5, new List<Weapon> { StormBolter,new MuteCCW(2,2,new List<EffectsWeapons>{}) }, new List<EffectsModel> { })
                    ,new Infantry(300, 160, 4, 4, 4, 4,1, 4,2,9, 2,5, new List<Weapon> { StormBolter,new MuteCCW(2,2,new List<EffectsWeapons>{}) }, new List<EffectsModel> { })
                    ,new Infantry(360, 160, 4, 4, 4, 4,1, 4,2,9, 2,5, new List<Weapon> { StormBolter,new MuteCCW(2,2,new List<EffectsWeapons>{}) }, new List<EffectsModel> { })
                    ,new Infantry(420, 160, 4, 4, 4, 4,1, 4,2,9, 2,5, new List<Weapon> { StormBolter,new MuteCCW(2,2,new List<EffectsWeapons>{}) }, new List<EffectsModel> { })},
                    new List<EffectsUnit>{})};
            Player S = new Player(TerminatorsUnits);
            IsGame = new Game(F, S, new DiceGenerator(),new ShowMessageBox());
            IsGame.IsForm = this;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
 
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            if (IsGame != null)
            {
                e.Graphics.Clear(Color.Green);
                IsGame.IsMap.Paint(e, IsGame);
            }
        }

        private void tableLayoutPanel7_Click(object sender, EventArgs e)
        {
            /*
            if(IsGame.IsNowPfase(Pfase.Shoot))
                IsGame.Shooting(IsGame.Target, 0, IsGame.Sourse);*/
            IsGame.ClickActionButton();
            tableLayoutPanel1.Invalidate();
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel3_MouseClick(object sender, MouseEventArgs e)
        {
            IsGame.NowPfaseStr.EndPfaseButton(IsGame);
            tableLayoutPanel1.Invalidate();
        }

        private void tableLayoutPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            IsGame.MouseClick(e.X,e.Y);
            tableLayoutPanel1.Invalidate();
        }

        private void IndependentCharecterButton_Click(object sender, EventArgs e)
        {
            IsGame.IndependentCharecterButtonClick();
            tableLayoutPanel1.Invalidate();
        }

        public void EndGame()
        {
            this.Close();
        }
    }
}
