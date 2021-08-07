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
        static public int Difficulty;
        static public string gameDifficulty = "";
        static public string playerName ="";
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
                gameDifficulty = "Easy";
                Difficulty = 10;
            }

            if (radioButton2.Checked == true) 
            {
                gameDifficulty = "Medium";
                Difficulty = 15;
            }

            if (radioButton3.Checked == true) 
            {
                gameDifficulty = "Hard";
                Difficulty = 20;
            }

            playerName = textBox1.Text;

            Form f1 = new GUI_Minesweeper.Form1(Difficulty, playerName, gameDifficulty);
            this.Hide();
            f1.Show();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
