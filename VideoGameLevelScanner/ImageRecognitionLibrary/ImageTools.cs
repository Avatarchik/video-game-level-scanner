using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Emgu.CV;
using Emgu.CV.UI;
using Emgu.CV.Structure;
namespace ImageRecognitionLibrary
{
    public static class ImageTools
    {
        #region Displaying Images
        public static void ShowInNamedWindow(Emgu.CV.IImage result, string debugWindowName)
        {
            if (!String.IsNullOrWhiteSpace(debugWindowName))
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
        public static Image<Gray, byte> FilterColor(Image<Hsv, Byte> src, IEnumerable<Tuple<Hsv, Hsv>> colors, string debugWindowName = "")
        {
            Image<Gray,Byte> result;
            
            Image<Gray,Byte>[] partialResults = new Image<Gray,byte>[colors.Count<Tuple<Hsv,Hsv>>()];
            int counter = 0;
            foreach (var colorPair in colors){
                partialResults[counter] = FilterColor(src, colorPair.Item1, colorPair.Item2);
                counter++;
            }

            result = CombineMapsBW(partialResults);

            ShowInNamedWindow(result, debugWindowName);
            return result;
        }
        #endregion
        #region Squares detection
        //public DetectionData DetectSquares(MCvMat src, Constants::ColorModelConstant imgType, string detectionWindow = "");
        //public DetectionData DetectSquares(MCvMat src, Constants::ColorModelConstant imgType, string detectionWindow = "");
        #endregion
        #region Combining images
        public static Image<Bgr, byte> CombineMaps(IEnumerable<Tuple<Image<Gray, byte>, Bgr>> maps)
        {
            if (maps.Count<Tuple<Image<Gray,byte>,Bgr>>()==0){
                throw new ArgumentException("Cannot combine empty collection of images.");
            }
            Image<Bgr,byte> result = new Image<Bgr,byte>(maps.First<Tuple<Image<Gray,byte>,Bgr>>().Item1.Size);
            foreach(var map in maps){
                result.SetValue(map.Item2,map.Item1);
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
    }
}
