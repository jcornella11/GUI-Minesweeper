using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_Minesweeper
{
    public partial class Form1 : Form
    {
        //Board
        static public board myBoard = new board(10, 1);
        static public Button[,] btnGrid = new Button[myBoard.size, myBoard.size];

        //Stopwatch
        static bool endgame = false;
        static Stopwatch stopwatch = new Stopwatch();

        //Player Scores and Stats
        static List<Playerstats> highscores = new List<Playerstats>();
        static string filePath = @"highscores.txt";
        static List<string> lines = File.ReadAllLines(filePath).ToList();
        static List<string> outputLines = new List<string>();
        static string PlayerName = "";
        static string gamedifficulty = "";

        public Form1(int difficulty, string playerName, string gameDifficulty)
        {
            InitializeComponent();
            PlayerName = playerName;
            gamedifficulty = gameDifficulty;
            populateButtonGrid();
            stopwatch.Start();
        }

        public void populateButtonGrid() 
        {
            //function to fill the panel control with buttons
            int buttonSize = panel1.Width / myBoard.size; //Calculate the width of each grid button
            panel1.Height = panel1.Width;

            //Nested Loop to Create buttons and place them in the panel
            for (int x = 0; x < myBoard.size; x++) 
            {
                for (int y = 0; y < myBoard.size; y++) 
                {
                    //Create the Button
                    btnGrid[x, y] = new Button();

                    //Make each button square
                    btnGrid[x, y].Width = buttonSize;
                    btnGrid[x, y].Height = buttonSize;

                    btnGrid[x, y].MouseDown += Grid_Button_Click; //Click event for each button
                    panel1.Controls.Add(btnGrid[x, y]); //Place Button on the Panel
                    btnGrid[x, y].Location = new Point(buttonSize * x, buttonSize * y); //Position it with x, y cordinates

                    //Tag attribute will hold row and column number in a string
                    btnGrid[x, y].Tag = x.ToString() + "|" + y.ToString();
                }
            }
        }

        private void Grid_Button_Click(object? sender, MouseEventArgs e)
        {
            //Get the Row and Column Number of the Button that was just Clicked
            string[] strArr = (sender as Button).Tag.ToString().Split("|");
            int x = int.Parse(strArr[0]);
            int y = int.Parse(strArr[1]);

            if (e.Button == MouseButtons.Left)
            {

                //If the Square is a bomb Display the Bomb Symbol
                if (myBoard.grid[x, y].liveNeighbors == 9)
                {
                    btnGrid[x, y].Text = "💣";
                }
                else
                {
                    //Display the Live Neighbors for the clicked square
                    btnGrid[x, y].Text = myBoard.grid[x, y].liveNeighbors.ToString();
                }


                //Set the Visited Property to true on the Selected Square
                myBoard.grid[x, y].visited = true;

                //If the grid contains a bomb at the chosen cell (row, column), set endgame to true
                if (myBoard.grid[x, y].liveNeighbors == 9)
                {
                    //Display Message to the User that the Game is Over
                    endgame = true;
                    stopwatch.Stop();
                    TimeSpan ts = stopwatch.Elapsed;
                    string elapsedTime3 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",ts.Hours, ts.Minutes, ts.Seconds,ts.Milliseconds / 10);
                    MessageBox.Show("You have hit a mine. GAME OVER. Total time Played: " + elapsedTime3);

                    //Reveal the Entire Board
                    for (int j = 0; j < myBoard.size; j++)
                    {
                        for (int k = 0; k < myBoard.size; k++)
                        {
                            if (myBoard.grid[j, k].liveNeighbors == 9)
                            {
                                btnGrid[j, k].Text = "💣";
                            }
                            else
                            {
                                btnGrid[j, k].Text = myBoard.grid[j, k].liveNeighbors.ToString();
                            }
                        }
                    }

                }

                //Call recursive flood fill on the clicked square
                myBoard.floodFill(x, y);

                //Loop through all of the Squares on the Grid
                //If the square has been visited reveal the contents

                for (int j = 0; j < myBoard.size; j++)
                {
                    for (int k = 0; k < myBoard.size; k++)
                    {
                        if (myBoard.grid[j, k].visited == true)
                        {
                            btnGrid[j, k].Text = myBoard.grid[j, k].liveNeighbors.ToString();
                        }
                    }
                }

                //Check the number of non bomb cells. 
                int nonBombCells = 0;

                for (int j = 0; j < myBoard.size; j++)
                {
                    for (int k = 0; k < myBoard.size; k++)
                    {
                        if (myBoard.grid[j, k].liveNeighbors != 9 && myBoard.grid[j, k].visited == false)
                        {
                            nonBombCells = nonBombCells + 1;
                        }
                    }
                }

                //If the number of non bomb cells is zero 

                if (nonBombCells == 0)
                {
                    stopwatch.Stop();
                    TimeSpan ts = stopwatch.Elapsed;
                    string elapsedTime2 = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",ts.Hours, ts.Minutes, ts.Seconds,ts.Milliseconds / 10);
                    MessageBox.Show("Congrats you have won the game. It took you: " + elapsedTime2);

                    //Reveal the Entire Board
                    for (int j = 0; j < myBoard.size; j++)
                    {
                        for (int k = 0; k < myBoard.size; k++)
                        {
                            if (myBoard.grid[j, k].liveNeighbors == 9)
                            {
                                btnGrid[j, k].Text = "🏴";
                            }
                            else
                            {
                                btnGrid[j, k].Text = myBoard.grid[j, k].liveNeighbors.ToString();
                            }
                        }
                    }
                    endgame = true;

                    //Open the High Scores Text File & Load Existing Data

                    
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
                    
                    //Add the Player Stats
                    Playerstats currentScore = new Playerstats();
                    currentScore.playerName = PlayerName;
                    currentScore.gameTime = elapsedTime2;
                    double totalSeconds = Math.Ceiling(ts.TotalSeconds);
                    currentScore.playerScore = (int)(totalSeconds);
                    currentScore.gameDifficulty = gamedifficulty;

                    highscores.Add(currentScore);

                    //Write the contents to the File and Overwrite the File
                    foreach (Playerstats allScores in highscores) 
                    {
                        outputLines.Add(allScores.playerName + ", " + allScores.gameTime + ", " + allScores.playerScore.ToString() + ", " + allScores.gameDifficulty +  "\n");
                    }

                    File.WriteAllLines(filePath, outputLines);
                }
                else 
                {
                    label2.Text = nonBombCells.ToString();
                }

            }


            //If the user right clicks set the Flag
            if (e.Button == MouseButtons.Right) 
            {
                btnGrid[x, y].Text = "🚩";
            
            }

                //reset the background color of all buttons to the default (original) color. 
                for (int i = 0; i < myBoard.size; i++)
            {
                for (int j = 0; j < myBoard.size; j++)
                {
                    btnGrid[i, j].BackColor = default(Color);
                }
            }

            //set the background of the clicked button to something different
            (sender as Button).BackColor = Color.Cornsilk;

            TimeSpan t1 = stopwatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            t1.Hours, t1.Minutes, t1.Seconds,
            t1.Milliseconds / 10);
            label4.Text = elapsedTime;

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form f2 = new GUI_Minesweeper.Form2();
            this.Hide();
            f2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form f3 = new GUI_Minesweeper.Form3();
            this.Hide();
            f3.Show();

        }
    }
}
