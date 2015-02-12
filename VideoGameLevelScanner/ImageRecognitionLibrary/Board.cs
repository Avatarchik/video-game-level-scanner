using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace ImageRecognitionLibrary
{
    public class Board
    {
        private int height;
        public int Height { get { return height; } }
        private int width;
        public int Width { get { return width; } }
        public int NoRooms = 0;
        private int[] grid;


        #region Constructors and destructor
        public Board(Size size)
        {
            if (size.Height < 1)
                throw new ArgumentException("Wrong size of the board. One of the dimentions is less than one.", "X=" + size.Height);
            if (size.Width < 1)
                throw new ArgumentException("Wrong size of the board. One of the dimentions is less than one.", "Y=" + size.Width);
            height = size.Height;
            width = size.Width;
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
        public int[,] Grid { 
            get 
            {
                var tab = new int[height,width];
                for (int x= 0; x<height; ++x)
                    for(int y = 0; y<width; ++y)
                        tab[x,y] = this[x,y];
                return tab;
            } 
        }
        //public int GetCell(uint x, uint y)
        //{
        //    return grid[width * (x - 1) + (y - 1)];
        //}
        //public void SetCell(uint x, uint y, int value)
        //{
        //    grid[width * (x - 1) + (y - 1)] = value;
        //}
        
        public int this[int x, int y]{
            get { return grid[width * x + y]; }
            set { grid[width * x + y] = value; }
        }
        public int this[Point point]
        {
            get { return grid[width * point.X + point.Y]; }
            set { grid[width * point.X + point.Y] = value; }
        }
        #endregion
        public void DetectRooms()
        {
            int roomCounter = 1;
            LinkedList<Point> otherRoomsQueue = new LinkedList<Point>();
            var firstColored = FirstColored();
            if (firstColored > height)
                return;
            otherRoomsQueue.AddLast(new Point(FirstColored(),0));
            while(otherRoomsQueue.Count != 0)
            {
                LinkedList<Point> roomQueue = new LinkedList<Point>();
                LinkedList<Point> roomCoordinates =new LinkedList<Point>();
                Point point = otherRoomsQueue.First();
                otherRoomsQueue.RemoveFirst();
                roomQueue.AddLast(point);
                while(roomQueue.Count != 0)
                {
                    point = roomQueue.First();
                    roomQueue.RemoveFirst();
                    roomCoordinates.AddLast(point);
                    foreach (var direction in Directions)
                    {
                        Point neighbour  = new Point(point.X+direction.X,point.Y+direction.Y);
                    
                        if (IsInBoundaries(neighbour))
                        {
                            if ((ColorIndex)this[point] == (ColorIndex)this[neighbour])
                            {
                                if(!roomCoordinates.Any(p => p.Equals(neighbour)))
                                {
                                    roomQueue.AddLast(neighbour);
                                }
                            }
                            else if ((ColorIndex)this[neighbour] != 0)
                                otherRoomsQueue.AddLast(neighbour);
                        }
                    }
                }
                foreach (var roomPoint in roomCoordinates)
                {
                    this[roomPoint] = roomCounter;
                }
                ++roomCounter;
                otherRoomsQueue = new LinkedList<Point>(otherRoomsQueue.Except(roomCoordinates).Where(p => this[p] < 0).ToList());
                roomCoordinates.Clear();
            }
            for (int x = 0; x < height; ++x)
                for (int y = 0; y < width; ++y)
                    if (this[x, y] < 0)
                        this[x, y] = 0;
            this.NoRooms = roomCounter-1;
        }
        protected bool IsInBoundaries(Point point)
        {
            return (point.X < height && point.Y < width && point.X >= 0 && point.Y >= 0);
        }
        protected int FirstColored()
        {
            int x = 0;
            while (x < height && this[x,0] >= 0)
                ++x;
            return x;
        }
        private static Point[] Directions = new Point[4] { new Point(-1, 0), new Point(0, 1), new Point(1, 0), new Point(0, -1) };
    }
}
