using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avoid
{
    public partial class Form1 : Form
    {
        Graphics g;
        Image bmp;
        InputController inputController;
        GameManager gm;
        public Form1()
        {
            InitializeComponent();
            inputController = new InputController();
            gm = new GameManager(new MapCollection().generateRandomMap(200), inputController, 100);
            
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Width = this.Width / 4;
            pictureBox1.Height = this.Height * 5 / 6;
            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(bmp);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            inputController.keyDown(e);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            inputController.keyUp(e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            gm.update(timer1.Interval);
            gm.draw(g, bmp.Width, bmp.Height);
        }
    }
}
