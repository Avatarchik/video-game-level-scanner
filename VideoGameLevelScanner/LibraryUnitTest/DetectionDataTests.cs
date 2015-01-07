using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV.Structure;
using Emgu.CV;
using ImageRecognitionLibrary;
using System.Drawing;

namespace LibraryUnitTest
{
    [TestClass]
    public class DetectionDataTests
    {
        [TestMethod]
        public void ShouldDetect3Squares()
        {
            Image<Gray, byte> frame = new Image<Gray, byte>(Properties.Resources.ThreeSquares);
            var dd = ImageTools.DetectSquares(frame);
            Assert.AreEqual(3, dd.ColorBoundingRectangles.First().Count);
        }

        [TestMethod]
        public void RemoveNoises()
        {
            Image<Gray, byte> frame = new Image<Gray, byte>(Properties.Resources.FourSquaresWithTenNoises);
            var dd = ImageTools.DetectSquares(frame);
            dd.RemoveNoises();
            Assert.AreEqual(4, dd.ColorBoundingRectangles.First().Count);
        }

        [TestMethod]
        public void IsInRange()
        {
            Rectangle In = new Rectangle(10, 15, 10, 10);
            Rectangle Out = new Rectangle(50, 25, 10, 10);
            Point rangeHoriz = new Point(5,15);
            Point rangeVertical = new Point(10,20);
            Assert.IsTrue(DDStub.IsInHorizontalRangeStub(In,rangeHoriz));
            Assert.IsTrue(DDStub.IsInHorizontalRangeStub(In,rangeVertical));
            Assert.IsFalse(DDStub.IsInHorizontalRangeStub(Out,rangeHoriz));
            Assert.IsFalse(DDStub.IsInHorizontalRangeStub(Out,rangeVertical));

        }

        [TestMethod]
        public void FindPosition()
        {
            Rectangle rect_0_0 = new Rectangle(2,12,2,2);
            Rectangle rect_0_1 = new Rectangle(25,15,10,5);
            Rectangle rect_1_2 = new Rectangle(70,50,10,20);
            Rectangle rect_1_3 = new Rectangle(110,70,10,20);
            Rectangle rect_0_X = new Rectangle(160,20,20,20);
            Rectangle rect_X_2 = new Rectangle(60,100,10,20);
            Rectangle rect_X_X = new Rectangle(200,200,20,20);

            List<Point> rangesHoriz = new List<Point>() { new Point(0, 10), new Point(20, 30), new Point(60, 80), new Point(115, 145) };
            List<Point> rangeVerti= new List<Point>() { new Point(10, 40), new Point(60, 80) };

            var position_0_0 = DDStub.FindPositionStub(rect_0_0,rangesHoriz, rangeVerti);
            var position_0_1 = DDStub.FindPositionStub(rect_0_1,rangesHoriz, rangeVerti);
            var position_1_2 = DDStub.FindPositionStub(rect_1_2,rangesHoriz, rangeVerti);
            var position_1_3 = DDStub.FindPositionStub(rect_1_3,rangesHoriz, rangeVerti);
            var position_0_X = DDStub.FindPositionStub(rect_0_X,rangesHoriz, rangeVerti);
            var position_X_2 = DDStub.FindPositionStub(rect_X_2,rangesHoriz, rangeVerti);
            var position_X_X = DDStub.FindPositionStub(rect_X_X, rangesHoriz, rangeVerti);

            Assert.AreEqual(new Point(0, 0), position_0_0);
            Assert.AreEqual(new Point(0, 1), position_0_1);
            Assert.AreEqual(new Point(1, 2), position_1_2);
            Assert.AreEqual(new Point(1, 3), position_1_3);
            Assert.AreEqual(new Point(0, -1), position_0_X);
            Assert.AreEqual(new Point(-1, 2), position_X_2);
            Assert.AreEqual(new Point(-1, -1), position_X_X);

        }

        class DDStub : DetectionData
        {
            public DDStub() : base(new Size(100,100)){}
            public static Point FindPositionStub(Rectangle rectangle, IList<Point> colsRanges, IList<Point> rowsRanges)
            {
                return FindPosition(rectangle, colsRanges, rowsRanges);
            }

            public static bool IsInVerticalRangeStub(Rectangle rectangle, Point range)
            {
                return IsInVerticalRange(rectangle, range);
            }

            public static bool IsInHorizontalRangeStub(Rectangle rectangle, Point range)
            {
                return IsInHorizontalRange(rectangle, range);
            }
        }

    }
}
