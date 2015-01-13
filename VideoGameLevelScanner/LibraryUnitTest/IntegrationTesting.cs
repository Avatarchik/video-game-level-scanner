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
            Board board = ImageTools.ReadFromFrame(frame);
            var img = ImageTools.DrawRooms(640, 480, board.Grid);
        }
    }
}
