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

            Parallel.For(0, 4, i =>
            {
                filtered[i] = ImageTools.FilterColor(hsvImg, ranges[i]);
                dds[i] = ImageTools.DetectSquares(filtered[i]);
                dds[i].RemoveNoises();
            });

            dds[0].AddColor(dds[1]);
            dds[0].AddColor(dds[2]);
            dds[0].AddColor(dds[3]);
            board = dds[0].CreateBoard();
            board.DetectRooms();
            var img = ImageTools.DrawRooms(640, 480, board.Grid);
        }

        private static Tuple<Hsv, Hsv>[] blueRange = new Tuple<Hsv, Hsv>[]
        { 
            new Tuple<Hsv,Hsv>(new Hsv(90, 90, 90), new Hsv(120, 255, 255))
        };
        private static Tuple<Hsv, Hsv>[] redRange = new Tuple<Hsv, Hsv>[]
        { 
            new Tuple<Hsv,Hsv>(new Hsv(0,85,80), new Hsv(12,255,255)),
            new Tuple<Hsv,Hsv>(new Hsv(150,85,80), new Hsv(179,255,255))
        };
        private static Tuple<Hsv, Hsv>[] greenRange = new Tuple<Hsv, Hsv>[]
        { 
            new Tuple<Hsv,Hsv>(new Hsv(35, 70, 35), new Hsv(90, 255, 255))
        };
        private static Tuple<Hsv, Hsv>[] yellowRange = new Tuple<Hsv, Hsv>[]
        { 
            new Tuple<Hsv,Hsv>(new Hsv(10, 70, 127), new Hsv(35, 255, 255))
        };
        private List<Tuple<Hsv, Hsv>[]> ranges = new List<Tuple<Hsv, Hsv>[]>{ blueRange, redRange, greenRange, yellowRange };


    }
}
