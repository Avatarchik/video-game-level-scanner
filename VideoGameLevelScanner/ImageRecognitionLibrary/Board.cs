using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ImageRecognitionLibrary
{
    public class Board
    {
        private int height;
        public int Height { get { return height; } }
        private int width;
        public int Width { get { return width; } }
        private int[] grid;

        #region Constructors and destructor
        public Board(Point dimentions)
        {
            if (dimentions.X < 1)
                throw new ArgumentException("Wrong size of the board. One of the dimentions is less than one.", "X=" + dimentions.X);
            if (dimentions.Y < 1)
                throw new ArgumentException("Wrong size of the board. One of the dimentions is less than one.", "Y=" + dimentions.Y);
            height = dimentions.X;
            width = dimentions.Y;
            grid = new int[height * width];
        }
        public Board(int x, int y)
        {
            if (x < 1)
                throw new ArgumentException("Wrong size of the board. One of the dimentions is less than one.", "X=" + x);
            if (y < 1)
                throw new ArgumentException("Wrong size of the board. One of the dimentions is less than one.", "X=" + y);
            height = x;
            width = y;
            grid = new int[height * width];
        }
        public Board(Board source)
        {
            height = source.height;
            width = source.width;

            int arraySize = height * width;
            grid = new int[arraySize];

            for (int i = 0; i < arraySize; i++)
            {
                grid[i] = source.grid[i];
            }

        }
        #endregion
        #region Grid access funtions
        public int GetCell(uint x, uint y)
        {
            return grid[height * (x - 1) + (y - 1)];
        }
        public void SetCell(uint x, uint y, int value)
        {
            grid[height * (x - 1) + (y - 1)] = value;
        }
        
        public int this[int x, int y]{
            get { return grid[height * (x - 1) + (y - 1)]; }
            set { grid[height * (x - 1) + (y - 1)] = value; }
        }
        #endregion
    }
}
