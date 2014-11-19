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
	    public double medianSize = 0;
        public int height = 0;
        public int width = 0;
        //public Image<Gray, byte> detectionImage;
        
        #region Constructors
        DetectionData(List<Rectangle> boundingRectangles, Image<Gray,byte> detectionImage)
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
        DetectionData(DetectionData source)
        {
	        ColorBoundingRectangles = new List<List<Rectangle>>(source.ColorBoundingRectangles);
	        medianSize = source.medianSize;
            this.height = source.height;
            this.width = source.width;
        }
        #endregion
        #region Calculating Median
        double CalculateMedianRectSize(){
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
        void AddColor(List<Rectangle> boundingRectangles){
	        ColorBoundingRectangles.Add(boundingRectangles);
	        CalculateMedianRectSize();
        }
        void AddColor(DetectionData dd){
	        ColorBoundingRectangles.AddRange(dd.ColorBoundingRectangles);
	        CalculateMedianRectSize();
            if (this.height != dd.height)
                throw new ArgumentException("The added data have different image size!", "Height");
            if (this.width != dd.width)
                throw new ArgumentException("The added data have different image size!", "Width");
        }
        #endregion
        #region Board creation and all functions needed in that process
        Board CreateBoard(){

            List<PointF> columns = new List<PointF>();
            List<PointF> rows = new List<PointF>();
            var fullDetection = DrawDetection();
            //int width = fullDetection.cols;
            //int height = fullDetection.rows;
            int[] blackPixelCols = new int[width];
            int[] blackPixelRows = new int[height];

            for (int i = 0; i < width; i++)
                for (int j = 0; i < height; i++) {
                    if ((int)fullDetection[j, i].Intensity == 0)
                    {
                        blackPixelCols[i]++;
                        blackPixelRows[i]++;
                    }
            }

            List<Point> ColsRanges = ColorRanges(blackPixelCols, width);
            List<Point> RowsRanges = ColorRanges(blackPixelRows, height);

            int y = ColsRanges.Count();
            int x = RowsRanges.Count();

            Board board = new Board(x, y);
       
            return board;
        }

        #region Helping methods for finding the Board size 

	        List<Point> ColorRanges(int[] blacks, int size){
		        List<Point> ranges = new List<Point>();
		        Point detectedRange = FindRange(blacks, 0, size);
		        while (detectedRange.X != -1 && detectedRange.Y != -1){
			        ranges.Add(detectedRange);
			        detectedRange = FindRange(blacks, detectedRange.Y + 1, size);
		        }
		        if (detectedRange.X != -1){
			        detectedRange.Y = size - 1;
			        ranges.Add(detectedRange);
		        }
		        return ranges;
	        }

	        Point FindRange(int[] mat, int start, int size){
		        int a = FindColorIndex(mat, start, size);
		        int b = FindBlackIndex(mat, a + 1, size);
		        return new Point(a, b);
	        }

	        int FindColorIndex(int[] mat, int start, int size){
		        while (start < size){
			        if (mat[start] > 0)
				        return start;
			        else
				        start++;
		        }
		        return -1;
	        }
	        int FindBlackIndex(int[] mat, int start, int size){
		        while (start < size){
			        if (mat[start] == 0)
				        return start;
			        else
				        start++;
		        }
		        return -1;
	        }
	        #endregion
        
        #region Removing Noses
        int RemoveNoises(float minProcent){
		    if (medianSize <= 0)
			    return 0;
                
            int counter = 0;
            double minSize = minProcent * medianSize;
            ColorBoundingRectangles.ForEach(rectanglesList => rectanglesList.RemoveAll(rectangle => rectangle.Height < minSize || rectangle.Width < minSize));
            return counter;
	    }
        #endregion
        #endregion

        #region Drawing the detected rectangles
        Image<Gray,byte> DrawDetection(string debugWindow = ""){
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
