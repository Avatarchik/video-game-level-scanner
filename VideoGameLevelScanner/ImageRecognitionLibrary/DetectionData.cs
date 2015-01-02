using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;

using RectangleExtension;

namespace ImageRecognitionLibrary
{
    public class DetectionData
    {
        public List<List<Rectangle>> ColorBoundingRectangles = new List<List<Rectangle>>();
        private int height = 0;
        private int width = 0;

        public int Height { get { return height; } }
        public int Width { get { return width; } }
        
        #region Constructors
        public DetectionData(Size size)
        {
            this.height = size.Height;
            this.width = size.Width;
        }        

        public DetectionData(List<Rectangle> boundingRectangles, Image<Gray,byte> detectionImage)
        {
	        ColorBoundingRectangles.Add(boundingRectangles);
            this.height = detectionImage.Height;
            this.width = detectionImage.Width;
        }
        //DetectionData(List<List<Rectangle> > colorRectangles)
        //{
        //    ColorBoundingRectangles.AddRange(colorRectangles);	        
        //    CalculateMedianRectSize();
        //}
        public DetectionData(DetectionData source)
        {
            ColorBoundingRectangles = new List<List<Rectangle>>(source.ColorBoundingRectangles);
            this.height = source.height;
            this.width = source.width;
        }
        #endregion
        
        #region Adding detection data for other colors
        public void AddColor(List<Rectangle> boundingRectangles)
        {
	        ColorBoundingRectangles.Add(boundingRectangles);
        }
        public void AddColor(DetectionData dd){
	        ColorBoundingRectangles.AddRange(dd.ColorBoundingRectangles);
            if (this.height != dd.height)
                throw new ArgumentException("The added data have different image size!", "Height");
            if (this.width != dd.width)
                throw new ArgumentException("The added data have different image size!", "Width");
        }
        #endregion
        #region Board creation and all functions needed in that process
        public Board CreateBoard(){

            var fullDetection = DrawDetection();

            var sums = ImageTools.CalculateSums(fullDetection);

            List<Point> ColsRanges = ImageTools.ColorRanges(sums.Key, width);
            List<Point> RowsRanges = ImageTools.ColorRanges(sums.Value, height);

            int sizeY = ColsRanges.Count();
            int sizeX = RowsRanges.Count();

            if (sizeX < 1 || sizeY < 1)
                return null;

            Board board = new Board(sizeX, sizeY);
       
            int colorCounter = 0;
            var colors = Enum.GetValues(typeof(ColorIndex)).Cast<int>().Reverse().ToList();
            foreach(var rectangleList in ColorBoundingRectangles)
            {   
                foreach (var rectangle in rectangleList)
                {
                    var position = FindPosition(rectangle, ColsRanges, RowsRanges);
                    if (position.X > -1 && position.Y > -1)
                        board[position.X, position.Y] = colors[colorCounter];
                }
                colorCounter++;
            }
            return board;
        }

        protected static Point FindPosition(Rectangle rectangle, IList<Point> colsRanges, IList<Point> rowsRanges)
        {
            int x = colsRanges.Count - 1;
            int y = rowsRanges.Count - 1;
            while (x>=0 && !IsInHorizontalRange(rectangle, colsRanges[x]))
                --x;
            while (y>=0 && !IsInVerticalRange(rectangle, rowsRanges[y]))
                --y;
            return new Point(y, x);
        }

        protected static bool IsInHorizontalRange(Rectangle rectangle, Point range)
        {
            var middle = (rectangle.Right + rectangle.Left)/2;
            return (range.X <= middle && middle <= range.Y);
        }

        protected static bool IsInVerticalRange(Rectangle rectangle, Point range)
        {
            var middle = (rectangle.Bottom + rectangle.Top)/2;
            return (range.X <= middle && middle <= range.Y);
        }
        
        
        public int RemoveNoises(float minProcent = 0.70f)
        {
            if (ColorBoundingRectangles.Count == 0 || !ColorBoundingRectangles.Any(rectangles=> rectangles.Count > 0))
                return 0;

            double minH = minProcent * ColorBoundingRectangles.Where(rectanglesList => rectanglesList.Count > 0).Max(rectangles => rectangles.Max(rectangle => rectangle.Height));
            double minW = minProcent * ColorBoundingRectangles.Where(rectanglesList => rectanglesList.Count > 0).Max(rectangles => rectangles.Max(rectangle => rectangle.Width));

            int counter = 0;

            ColorBoundingRectangles.ForEach(rectanglesList => rectanglesList.RemoveAll(rectangle => rectangle.Height < minH || rectangle.Width < minW));
            return counter;
	    }
        #endregion

        #region Drawing the detected rectangles
        public Image<Gray, byte> DrawDetection(string debugWindow = "")
        {
            Image<Gray,byte> img = new Image<Gray,byte>(width,height,new Gray(0));
            Gray color = new Gray(255);
	        foreach (var rectangleList in ColorBoundingRectangles){
		        foreach (var rectangle in rectangleList)
		        {
			        img.Draw(rectangle, color, 1);
		        }
	        }
            ImageTools.ShowInNamedWindow(img, debugWindow);
            return img;
        }
        #endregion 
    }
}
