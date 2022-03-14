using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal class GameGrid
    {
        private readonly int[,] grid;
        public int Rows { get; }
        public int Columns { get; }
        public int this[int r, int c]
        {
            get => grid[r, c];
            set => grid[r, c] = value;
        }

        public GameGrid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            grid = new int[rows,columns];
        }

        //Checks if the given coordinates are inside Playing area boundaries
        public bool IsInside(int r, int c)
        {
            return r >= 0 && r < Rows && c >= 0 && c < Columns;
        }

        //Checks if cell is empty
        public bool IsEmpty(int r, int c)
        {
            return IsInside(r, c) && grid[r, c] == 0;
        }

        //Checks if whole row is full
        public bool IsRowFull(int r)
        {
            bool isFull = true;
            for(int c = 0; c < Columns; c++)
            {
                if(grid[r, c] == 0)
                {
                    isFull = false;
                }
            }
            return isFull;
        }

        //Checks if ANY cell in row is occupied
        public bool IsRowEmpty(int r)
        {
            bool isFull = true;
            for (int c = 0; c < Columns; c++)
            {
                if (grid[r, c] != 0)
                {
                    isFull = false;
                }
            }
            return isFull;
        }

        //Deletes cells at given row index
        private void ClearRow(int r)
        {
            for(int c = 0; c < Columns; c++)
            {
                grid[r, c] = 0;
            }
        }

        //Moves rows downwards
        private void MoveRowDown(int r, int numRows)
        {
            for(int c = 0; c < Columns; c++)
            {
                grid[r + numRows, c] = grid[r,c];
                grid[r,c] = 0;
            }
        }

        //Clears rows that are clearable
        //Moves left rows down
        //Counts and returns points depending on how many rows were cleared
        public int ClearFullRows()
        {
            int cleared = 0;
            int points = 0;

            for(int r = Rows-1; r >= 0; r--)
            {
                if (IsRowFull(r))
                {
                    ClearRow(r);
                    cleared++;
                }
                else if(cleared > 0)
                {
                    MoveRowDown(r, cleared);
                }
            }
            if(cleared == 1) { points+=50; }
            else if(cleared >= 1) { points += (cleared * 100); }

            return points;
        }
    }
}
