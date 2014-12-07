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
	    private double medianSize = 0;
        private int height = 0;
        private int width = 0;

        public double MedianSize { get { return medianSize; } }
        public int Height { get { return height; } }
        public int Width { get { return width; } }
        
        #region Constructors
        public DetectionData(List<Rectangle> boundingRectangles, Image<Gray,byte> detectionImage)
        {
	        ColorBoundingRectangles.Add(boundingRectangles);
	        CalculateMedianRectSize();
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
	        medianSize = source.medianSize;
            this.height = source.height;
            this.width = source.width;
        }
        #endregion
        #region Calculating Median
        private double CalculateMedianRectSize(){
	        List<double> sizes = new List<double>();
	        double avg;
	        foreach (var colorList in ColorBoundingRectangles){
		        foreach (var rectangle in colorList)
		        {
                    avg = ((rectangle.Height + rectangle.Width) / 2);
			        sizes.Add(avg);
		        }
	        }
	        long l = sizes.Count();
	        if (sizes.Count() == 0)
            {
		        medianSize = 0;
                return medianSize;
	        }
            else if (l % 2 == 0)
            {
                var sorted = sizes.OrderBy(size => size).ToList();
                medianSize = (sorted.ElementAt((int)(l / 2)) + sorted.ElementAt((int)((l - 1) / 2))) / 2;
            }
            else{
		        medianSize = sizes.ElementAt((int)(l / 2));
            }
            sizes.OrderBy(size => size);
	        return medianSize;
        }
        #endregion
        #region Adding detection data for other colors
        public void AddColor(List<Rectangle> boundingRectangles)
        {
	        ColorBoundingRectangles.Add(boundingRectangles);
	        CalculateMedianRectSize();
        }
        public void AddColor(DetectionData dd){
	        ColorBoundingRectangles.AddRange(dd.ColorBoundingRectangles);
	        CalculateMedianRectSize();
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

            List<Point> ColsRanges = ImageTools.ColorRanges(sums.Item1, width);
            List<Point> RowsRanges = ImageTools.ColorRanges(sums.Item2, height);

            int y = ColsRanges.Count();
            int x = RowsRanges.Count();

            if (x < 1 || y < 1)
                return null;

            Board board = new Board(x, y);
       
            return board;
        }

        
        
        #region Removing Noses
        public int RemoveNoises(float minProcent = 0.80f)
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
