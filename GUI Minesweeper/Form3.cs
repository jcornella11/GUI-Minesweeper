using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_Minesweeper
{
    public partial class Form3 : Form
    {
        static string filePath = @"highscores.txt";
        static List<Playerstats> highscores = new List<Playerstats>();
        static List<string> lines = File.ReadAllLines(filePath).ToList();

        public Form3()
        {
            InitializeComponent();
            createListView();
            updateListViewScores();
        }

        private void createListView()
        {
            listView1.View = View.Details;
            listView1.Columns.Add("Player Name", 200, HorizontalAlignment.Left);
            listView1.Columns.Add("Game Time", 200, HorizontalAlignment.Left);
            listView1.Columns.Add("Player Score", 200, HorizontalAlignment.Left);
            listView1.Columns.Add("Game Difficulty", 200, HorizontalAlignment.Left);
            listView1.GridLines = true;

            //Load Existing Score from File
            foreach (string line in lines) 
            {
                string[] entries = line.Split(',');

                if (entries.Length == 4)
                {
                    Playerstats score = new Playerstats();
                    score.playerName = entries[0];
                    score.gameTime = entries[1];
                    score.playerScore = int.Parse(entries[2]);
                    score.gameDifficulty = entries[3];
                    highscores.Add(score);
                }   
            }
        }

        private void updateListViewScores() 
        {
            //Use the Comparable interface to sort by Score & Difficulty
            highscores.Sort();

            //Use a Linq Statment to make sure only the top 5 are displayed
            var theTopScores =
                (from score in highscores
                orderby score.playerScore ascending
                select score).Take(5);


            foreach (Playerstats score in theTopScores) 
            {
                string[] row = {score.playerName, score.gameTime.ToString(), score.playerScore.ToString(), score.gameDifficulty};
                listView1.Items.Add(new ListViewItem(row));
            }
            
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form f2 = new GUI_Minesweeper.Form2();
            this.Hide();
            f2.Show();
        }
    }
}
