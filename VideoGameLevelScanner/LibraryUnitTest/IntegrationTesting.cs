using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Emgu.CV;
using Emgu.CV.Structure;
using ImageRecognitionLibrary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryUnitTest
{
    [TestClass]
    public class IntegrationTesting
    {
        [TestMethod]
        public void TimePerFrameTest()
        {
            Image<Bgr, byte> frame = new Image<Bgr, byte>(Properties.Resources.BoardSample);
            Image<Gray, byte>[] filtered = new Image<Gray, byte>[4];
            DetectionData[] dds = new DetectionData[4];
            Board board;
            
            var hsvImg = frame.Convert<Hsv, byte>();

            for(int i =0; i <4 ; ++i)
            {
                filtered[i] = ImageTools.FilterColor(hsvImg, ranges[i]);
                dds[i] = ImageTools.DetectSquares(filtered[i]);
                dds[i].RemoveNoises();
            };

            dds[0].AddColor(dds[1]);
            dds[0].AddColor(dds[2]);
            dds[0].AddColor(dds[3]);
            board = dds[0].CreateBoard();
            board.DetectRooms();
            var img = ImageTools.DrawRooms(640, 480, board.Grid);
        }

        private static KeyValuePair<Hsv, Hsv>[] blueRange = new KeyValuePair<Hsv, Hsv>[]
        { 
            new KeyValuePair<Hsv,Hsv>(new Hsv(90, 90, 90), new Hsv(120, 255, 255))
        };
        private static KeyValuePair<Hsv, Hsv>[] redRange = new KeyValuePair<Hsv, Hsv>[]
        { 
            new KeyValuePair<Hsv,Hsv>(new Hsv(0,85,80), new Hsv(12,255,255)),
            new KeyValuePair<Hsv,Hsv>(new Hsv(150,85,80), new Hsv(179,255,255))
        };
        private static KeyValuePair<Hsv, Hsv>[] greenRange = new KeyValuePair<Hsv, Hsv>[]
        { 
            new KeyValuePair<Hsv,Hsv>(new Hsv(35, 70, 35), new Hsv(90, 255, 255))
        };
        private static KeyValuePair<Hsv, Hsv>[] yellowRange = new KeyValuePair<Hsv, Hsv>[]
        { 
            new KeyValuePair<Hsv,Hsv>(new Hsv(10, 70, 127), new Hsv(35, 255, 255))
        };
        private List<KeyValuePair<Hsv, Hsv>[]> ranges = new List<KeyValuePair<Hsv, Hsv>[]>{ blueRange, redRange, greenRange, yellowRange };


    }
}
