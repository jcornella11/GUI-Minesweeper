using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_Minesweeper
{
    public partial class Form2 : Form
    {
        public double Difficulty;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true) 
            {
                Difficulty = 10;
            }

            if (radioButton2.Checked == true) 
            {
                Difficulty = 20;
            }

            if (radioButton3.Checked == true) 
            {
                Difficulty = 30;
            }


            
            Form f1 = new GUI_Minesweeper.Form1(Difficulty);
            this.Hide();
            f1.Show();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
