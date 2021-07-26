using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_Minesweeper
{
    public class board
    {
        public int size { get; set; } //Square so Length & Width are Same Dimensions
        public Cell[,] grid; //2D Array of Cell Objects
        public double Difficulty = 10; //Set Dificulty to 10% mines

        public board(int Size)
        {
            size = Size;
            grid = new Cell[Size, Size];
            for (int j = 0; j < grid.GetLength(0); j++)
            {
                for (int k = 0; k < grid.GetLength(1); k++)
                {
                    grid[j, k] = new Cell();
                    grid[j, k].row = j;
                    grid[j, k].column = k;
                }
            }
            setupLiveNeighbors();
            calculateLiveNeighbords();
        }

        public void setupLiveNeighbors()
        {

            for (int j = 0; j < grid.GetLength(0); j++)
            {

                for (int k = 0; k < grid.GetLength(1); k++)
                {
                    Random random = new Random();
                    int percent = random.Next(0, 100);

                    if (percent <= Difficulty)
                    {
                        grid[j, k].Live = true;
                        grid[j, k].liveNeighbors = 9;
                    }
                    else
                    {
                        grid[j, k].Live = false;
                    }
                }
            }
        }

        public void calculateLiveNeighbords()
        {
            for (int j = 0; j < grid.GetLength(0); j++)
            {
                for (int k = 0; k < grid.GetLength(1); k++)
                {
                    int liveCount = 0;

                    if (grid[j, k].Live == true)
                    {
                        liveCount = 9;
                        continue;
                    }

                    if (j > 0 && k > 0)
                    {
                        if (grid[j - 1, k - 1].Live == true)
                        {
                            liveCount = liveCount + 1;
                        }
                    }

                    if (k > 0)
                    {
                        if (grid[j, k - 1].Live == true)
                        {
                            liveCount = liveCount + 1;
                        }

                    }

                    if (j < grid.GetLength(0) - 1 && k > 0)
                    {
                        if (grid[j + 1, k - 1].Live == true)
                        {
                            liveCount = liveCount + 1;
                        }
                    }

                    if (j > 0)
                    {
                        if (grid[j - 1, k].Live == true)
                        {
                            liveCount = liveCount + 1;
                        }
                    }

                    if (j < grid.GetLength(0) - 1)
                    {
                        if (grid[j + 1, k].Live == true)
                        {
                            liveCount = liveCount + 1;
                        }
                    }

                    if (j > 0 && k < grid.GetLength(1) - 1)
                    {
                        if (grid[j - 1, k + 1].Live == true)
                        {
                            liveCount = liveCount + 1;
                        }
                    }

                    if (k < grid.GetLength(1) - 1)
                    {
                        if (grid[j, k + 1].Live == true)
                        {
                            liveCount = liveCount + 1;
                        }
                    }

                    if (j < grid.GetLength(0) - 1 && k < grid.GetLength(1) - 1)
                    {
                        if (grid[j + 1, k + 1].Live == true)
                        {
                            liveCount = liveCount + 1;
                        }
                    }

                    grid[j, k].liveNeighbors = liveCount;
                }
            }
        }

        public void floodFill(int j, int k)
        {
            //8 Direction Flood Fill
            grid[j, k].visited = true;

            //South
            if (k < grid.GetLength(1) - 1)
            {
                if (grid[j, k + 1].liveNeighbors == 0 && grid[j, k + 1].visited == false)
                {
                    floodFill(j, k + 1);
                }
            }

            //West
            if (j > 0)
            {
                if (grid[j - 1, k].liveNeighbors == 0 && grid[j - 1, k].visited == false)
                {
                    floodFill(j - 1, k);
                }
            }

            //North
            if (k > 1)
            {
                if (grid[j, k - 1].liveNeighbors == 0 && grid[j, k - 1].visited == false)
                {
                    floodFill(j, k - 1);
                }
            }

            //East
            if (j < grid.GetLength(0) - 1)
            {
                if (grid[j + 1, k].liveNeighbors == 0 && grid[j + 1, k].visited == false)
                {
                    floodFill(j + 1, k);
                }
            }

            //South East
            if (j < grid.GetLength(0) - 1 && k < grid.GetLength(1) - 1)
            {

                if (grid[j + 1, k + 1].liveNeighbors == 0 && grid[j + 1, k + 1].visited == false)
                {
                    floodFill(j + 1, k + 1);
                }
            }

            //South West
            if (j > 0 && k < grid.GetLength(1) - 1)
            {
                if (grid[j - 1, k + 1].liveNeighbors == 0 && grid[j - 1, k + 1].visited == false)
                {
                    floodFill(j - 1, k + 1);
                }
            }

            //North East
            if (j < grid.GetLength(0) - 1 && k > 0)
            {
                if (grid[j + 1, k - 1].liveNeighbors == 0 && grid[j + 1, k - 1].visited == false)
                {
                    floodFill(j + 1, k - 1);
                }
            }

            //North West
            if (j > 0 && k > 0)
            {
                if (grid[j - 1, k - 1].liveNeighbors == 0 && grid[j - 1, k - 1].visited == false)
                {
                    floodFill(j - 1, k - 1);
                }
            }

        }
    }
}

