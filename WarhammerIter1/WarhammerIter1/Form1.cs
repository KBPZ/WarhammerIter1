using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WarhammerIter1
{
    public partial class Form1 : Form
    {
        Game IsGame = new Game();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Green);
            IsGame.IsMap.Paint(e,IsGame.PlayerNow());
        }

        private void tableLayoutPanel7_Click(object sender, EventArgs e)
        {
            if(IsGame.IsNowPfase(Pfase.Shoot))
                IsGame.Shooting(IsGame.Target, 0, IsGame.Sourse);
            tableLayoutPanel1.Invalidate();
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel3_MouseClick(object sender, MouseEventArgs e)
        {
            IsGame.NextPfase();
            tableLayoutPanel1.Invalidate();
        }
    }
}
