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
    public partial class Form1 : Form
    {
        static public board myBoard = new board(10);
        public Button[,] btnGrid = new Button[myBoard.size, myBoard.size];
        bool endgame = false;
        
        public Form1()
        {
            InitializeComponent();
            populateButtonGrid();
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
                    MessageBox.Show("You have hit a mine. GAME OVER");
                    endgame = true;

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
                    MessageBox.Show("Congrats you have one the game");

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

            //

           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
