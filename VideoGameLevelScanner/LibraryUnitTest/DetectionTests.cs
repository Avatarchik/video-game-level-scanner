using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV.Structure;
using Emgu.CV;
using ImageRecognitionLibrary;

namespace LibraryUnitTest
{
    [TestClass]
    public class DetectionTests
    {
        [TestMethod]
        public void ShouldDetect3Squares()
        {
            Image<Gray, byte> frame = new Image<Gray, byte>(Properties.Resources.ElevenSquares);
            var dd = ImageTools.DetectSquares(frame);
            Assert.AreEqual(3, dd.ColorBoundingRectangles.First().Count);
        }


    }
}
