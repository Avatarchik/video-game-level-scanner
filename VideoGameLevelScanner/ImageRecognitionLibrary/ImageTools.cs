using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

using Emgu.CV;
using Emgu.CV.Structure;
namespace ImageRecognitionLibrary
{
    enum ColorIndex { Blue = -1, Red=-2, Green=-3, Yellow=-4};
    public static class ImageTools
    {
        #region Displaying Images
        public static void ShowInNamedWindow(Emgu.CV.IImage result, string debugWindowName)
        {
            if (!(debugWindowName.Equals ("") || debugWindowName == null))
            {
                CvInvoke.cvNamedWindow(debugWindowName);
                CvInvoke.cvShowImage(debugWindowName, result.Ptr);
            }
        }
        #endregion
        #region Filtering Colors
        //for one color range
        public static Image<Gray,byte> FilterColor(Image<Hsv,Byte> src, Hsv colorLow, Hsv colorHigh, string debugWindowName = "")
        {
            Image<Gray,byte> result = src.InRange(colorLow, colorHigh);
            ShowInNamedWindow(result, debugWindowName);
            return result;
        }

        //for many color ranges (needed in case of red)
        public static Image<Gray, byte> FilterColor(Image<Hsv, Byte> src, IEnumerable<KeyValuePair<Hsv, Hsv>> colors, string debugWindowName = "")
        {
            Image<Gray,Byte> result;
            
            Image<Gray,Byte>[] partialResults = new Image<Gray,byte>[colors.Count<KeyValuePair<Hsv,Hsv>>()];
            int counter = 0;
            foreach (var colorPair in colors){
                partialResults[counter] = FilterColor(src, colorPair.Key, colorPair.Value);
                counter++;
            }

            result = CombineMapsBW(partialResults);

            ShowInNamedWindow(result, debugWindowName);
            return result;
        }
        #endregion
        #region Squares detection
        public static DetectionData DetectSquares(Image<Gray, byte> src, string detectionWindow = "")
        {
            for (int i = 0; i < 1; i++)
            {
                src = src.PyrDown();
                src = src.PyrUp();
            }
            src = src.Erode(1);

            Gray cannyThreshold = new Gray(255);
            Gray cannyThresholdLinking = new Gray(1);

            Image<Gray, Byte> cannyEdges = src.Canny(cannyThreshold.Intensity,cannyThresholdLinking.Intensity,3);
            LineSegment2D[] lines = cannyEdges.HoughLinesBinary(
                    1, //Distance resolution in pixel-related units
                    Math.PI / 45.0, //Angle resolution measured in radians.
                    20, //threshold
                    30, //min Line width
                    10 //gap between lines
                    )[0]; //Get the lines from the first channel

            List<Rectangle> rectanglesList = new List<Rectangle>();

            using (var storage = new MemStorage())
            {
                for (Contour<Point> contours = cannyEdges.FindContours(); contours != null; contours = contours.HNext)
                {
                    Contour<Point> currentContour = contours.ApproxPoly(contours.Perimeter * 0.05, storage);

                    if (currentContour.BoundingRectangle.Height * currentContour.BoundingRectangle.Width > 50) //only consider contours with area greater than 250
                    {
                        if (currentContour.Total >= 4) //The contour has more than 4 vertices.
                        {
                            var boundingRectangle = currentContour.BoundingRectangle;
                            if (!rectanglesList.Exists(rect => rect.IntersectsWith(boundingRectangle))) 
                                rectanglesList.Add(boundingRectangle);
                        }
                    }
                }
            }
            ShowInNamedWindow(cannyEdges, detectionWindow);
            return new DetectionData(rectanglesList, src);         
        }
        #endregion
        #region Helping methods for finding the Board size

        public static KeyValuePair<int[], int[]> CalculateSums(Image<Gray, byte> image)
        {
            int width = image.Width;
            int height = image.Height;
            int[] blackPixelCols = new int[width];
            int[] blackPixelRows = new int[height];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if ((int)(image[i, j].Intensity) > 0)
                    {
                        blackPixelCols[j]++;
                        blackPixelRows[i]++;
                    }
                }
            }
            return new KeyValuePair<int[], int[]>(blackPixelCols, blackPixelRows);
        }

        public static List<Point> ColorRanges(int[] blacks, int size)
        {
            List<Point> ranges = new List<Point>();
            Point detectedRange = FindRange(blacks, 0, size);
            while (detectedRange.X != -1 && detectedRange.Y != -1)
            {
                ranges.Add(detectedRange);
                detectedRange = FindRange(blacks, detectedRange.Y + 1, size);
            }
            return ranges;
        }

        public static Point FindRange(int[] mat, int start, int size)
        {
            int a = FindColorIndex(mat, start, size);
            int b = FindBlackIndex(mat, a + 1, size) - 1;
            if (a >= 0)
                if (b >= 0)
                    return new Point(a, b);
                else
                    return new Point(a, size - 1);
            else
                return new Point(-1, -1);
        }

        public static int FindColorIndex(int[] mat, int start, int size)
        {
            while (start < size)
            {
                if (mat[start] > 0)
                    return start;
                else
                    start++;
            }
            return -1;
        }

        public static int FindBlackIndex(int[] mat, int start, int size)
        {
            while (start < size)
            {
                if (mat[start] == 0)
                    return start;
                else
                    start++;
            }
            return -1;
        }
        #endregion
        #region Drawing Images from int arrays
        public static Image<Bgr, byte> DrawDetectedColors(int maxWidth, int maxHeight, int[,] data)
        {
            return DrawLookupImage(maxWidth, maxHeight, data, Colors.ColorsArray);
        }

        public static Image<Bgr, byte> DrawRooms(int maxWidth, int maxHeight, int[,] data)
        {
            Bgr[] palette = GetRandomPalette(data.Cast<int>().Max()+1);
            return DrawLookupImage(maxWidth, maxHeight, data, palette);
        }

        public static Image<Bgr, byte> DrawRooms(int maxWidth, int maxHeight, int[,] data, Bgr[] palette)
        {
            return DrawLookupImage(maxWidth, maxHeight, data, palette);
        }

        private static Image<Bgr,byte> DrawLookupImage(int maxWidth, int maxHeight, int[,] data, Bgr[] palette)
        {
            int boardHeight = data.GetLength(0);
            int boardWidth = data.GetLength(1);
            int imgScale = Math.Min(maxWidth / boardWidth, maxHeight / boardHeight);
            Image<Bgr, byte> img = new Image<Bgr, byte>(boardWidth, boardHeight);
            for (int x = 0; x < boardHeight; ++x)
            {
                for (int y = 0; y < boardWidth; ++y)
                {
                    int colorIndex = (data[x, y] >=0) ? data[x,y] : 0;
                    img[x, y] = palette[colorIndex];
                }
            }
            return img.Resize(imgScale,Emgu.CV.CvEnum.INTER.CV_INTER_NN);
        }

        public static Bgr[] GetRandomPalette(int paletteSize)
        {
            Image<Hsv,byte> hsvPalette = new Image<Hsv,byte>(paletteSize,1);
            Image<Bgr,byte> bgrPalette;
            Bgr[] palette = new Bgr[paletteSize];
            for (int i = 0; i < paletteSize; ++i)
            {
                int x = i*25;
                hsvPalette[0,i] = new Hsv(x % 180, 255*(1-(x/900*0.25)) ,255);
            }
            bgrPalette = hsvPalette.Convert<Bgr, byte>();
            palette[0] = new Bgr(0, 0, 0);
            for (int i = 1; i < paletteSize; ++i)
            {
                palette[i] = bgrPalette[0,i];
            }
            return palette;
        }
        #endregion
        #region Combining images
        public static Image<Bgr, byte> CombineMaps(IEnumerable<KeyValuePair<Image<Gray, byte>, Bgr>> maps)
        {
            if (maps.Count<KeyValuePair<Image<Gray,byte>,Bgr>>()==0){
                throw new ArgumentException("Cannot combine empty collection of images.");
            }
            Image<Bgr,byte> result = new Image<Bgr,byte>(maps.First<KeyValuePair<Image<Gray,byte>,Bgr>>().Key.Size);
            foreach(var map in maps){
                result.SetValue(map.Value,map.Key);
            }
            return result;
        }
        public static Image<Gray, byte> CombineMapsBW(IEnumerable<Image<Gray, byte>> images)
        {
            if (images.Count<Image<Gray,byte>>()==0){
                throw new ArgumentException("Cannot combine empty collection of images.");
            }
            Image<Gray,byte> result = new Image<Gray,byte>(images.First<Image<Gray,byte>>().Size);
            foreach(var image in images){
                result = result | image;
            }
            return result;
        }
        #endregion
        #region Colors
        public static class Colors
        {
            public static Bgr Blue = new Bgr(255, 0, 0);
            public static Bgr Red = new Bgr(0, 0, 255);
            public static Bgr Green = new Bgr(0, 255, 0);
            public static Bgr Yellow = new Bgr(0, 255, 255);

            public static Bgr[] ColorsArray = new Bgr[] { Black, Blue, Red, Green, Yellow };
 
            public static Bgr White = new Bgr(255, 255, 255);
            public static Bgr Black = new Bgr(0, 0, 0);
        }
        #endregion

        #region The most importatnt fuction
        public static Board ReadFromFrame(Image<Bgr,byte> frame)
        {
            Board board = ReadFromFrame(frame, new FilteringParameters(null));
            return board;
        }

        public static Board ReadFromFrame(Image<Bgr, byte> frame, FilteringParameters filteringParameters)
        {
            Image<Gray, byte>[] filtered = new Image<Gray, byte>[4];
            DetectionData[] dds = new DetectionData[4];
            Board board;

            var hsvImg = frame.Convert<Hsv, byte>();

            for (int i = 0; i < 4; i++)
            {
                
                filtered[i] = ImageTools.FilterColor(hsvImg, filteringParameters.ColorsRanges[i]);
                dds[i] = ImageTools.DetectSquares(filtered[i]);
                dds[i].RemoveNoises();
            };

            dds[0].AddColor(dds[1]);
            dds[0].AddColor(dds[2]);
            dds[0].AddColor(dds[3]);
            board = dds[0].CreateBoard();
            if (board!=null)
                board.DetectRooms();
            return board;
        } 
        #endregion
    }
}
