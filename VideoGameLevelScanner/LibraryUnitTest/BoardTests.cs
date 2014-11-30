using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageRecognitionLibrary;
using System.Drawing;
using System.Collections.Generic;
using Emgu.CV;
using Emgu.CV.Structure;

namespace LibraryUnitTest
{
    [TestClass]
    public class BoardTests
    {
        [TestMethod]
        public void ShouldReturn3x5()
        {

        }

        [TestMethod]
        public void CalculatingSums()
        {
            var img = new Image<Gray,byte>(Properties.Resources.ColumnRowSums);
            var sums = ImageTools.CalculateSums(img);
            int[] expectedColumns = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int[] expectedRows = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 ,10 };
            CollectionAssert.AreEqual(expectedColumns, sums.Item1);
            CollectionAssert.AreEqual(expectedRows, sums.Item2);
        }

        [TestMethod]
        public void ColorRangeTests()
        {
            int[] array = { 2, 4, 4, 0, 0, 0, 5, 4, 0, 0 };
            List<Point> ranges = ImageTools.ColorRanges(array, array.Length);
            List<Point> expectedRanges = new List<Point>(){
                new Point(0,2),
                new Point(6,7),
            };
            CollectionAssert.AreEqual(expectedRanges,ranges);
        }

        [TestMethod]
        public void FindingRangeTest()
        {
            int[] array = { 0, 0, 0, 1, 23, 1, 2, 1, 0, 0, 0, 12, 2, 4, 50, 10, 0, 0, 1 };
            
            var From3To7 = ImageTools.FindRange(array, 0, array.Length);
            var From3To7v2 = ImageTools.FindRange(array, 1, array.Length);
            var From3To7v3 = ImageTools.FindRange(array, 2, array.Length);
            var From3To7v4 = ImageTools.FindRange(array, 3, array.Length);
            
            var From4To7 = ImageTools.FindRange(array, 4, array.Length);
            var From5To7 = ImageTools.FindRange(array, 5, array.Length);
            var From6To7 = ImageTools.FindRange(array, 6, array.Length);
            var From7To7 = ImageTools.FindRange(array, 7, array.Length);

            var From11To15 = ImageTools.FindRange(array, 8, array.Length);
            var From11To15v2 = ImageTools.FindRange(array, 9, array.Length);
            var From11To15v3 = ImageTools.FindRange(array, 10, array.Length);
            var From11To15v4 = ImageTools.FindRange(array, 11, array.Length);

            var From12To15 = ImageTools.FindRange(array, 12, array.Length);
            var From13To15 = ImageTools.FindRange(array, 13, array.Length);
            var From14To15 = ImageTools.FindRange(array, 14, array.Length);
            var From15To15 = ImageTools.FindRange(array, 15, array.Length);

            var From18To18 = ImageTools.FindRange(array, 16, array.Length);
            var From18To18v2 = ImageTools.FindRange(array, 17, array.Length);
            var From18To18v3 = ImageTools.FindRange(array, 18, array.Length);

            Assert.AreEqual(new Point(3, 7), From3To7);
            Assert.AreEqual(new Point(3, 7), From3To7v2);
            Assert.AreEqual(new Point(3, 7), From3To7v3);
            Assert.AreEqual(new Point(3, 7), From3To7v4);

            Assert.AreEqual(new Point(4, 7), From4To7);
            Assert.AreEqual(new Point(5, 7), From5To7);
            Assert.AreEqual(new Point(6, 7), From6To7);
            Assert.AreEqual(new Point(7, 7), From7To7);

            Assert.AreEqual(new Point(11, 15), From11To15);
            Assert.AreEqual(new Point(11, 15), From11To15v2);
            Assert.AreEqual(new Point(11, 15), From11To15v3);
            Assert.AreEqual(new Point(11, 15), From11To15v4);

            Assert.AreEqual(new Point(12, 15), From12To15);
            Assert.AreEqual(new Point(13, 15), From13To15);
            Assert.AreEqual(new Point(14, 15), From14To15);
            Assert.AreEqual(new Point(15, 15), From15To15);

            Assert.AreEqual(new Point(18, 18), From18To18);
            Assert.AreEqual(new Point(18, 18), From18To18v2);
            Assert.AreEqual(new Point(18, 18), From18To18v3);
        }

        [TestMethod]
        public void FindingColorIndex()
        {
            int[] array = { 0, 0, 0, 10, 0, 1, 0 };
            int shouldBeThree = ImageTools.FindColorIndex(array, 0, array.Length);
            int shouldBeThree2 = ImageTools.FindColorIndex(array, 1, array.Length);
            int shouldBeThree3 = ImageTools.FindColorIndex(array, 2, array.Length);
            int shouldBeThree4 = ImageTools.FindColorIndex(array, 3, array.Length);
            int shouldBeFive = ImageTools.FindColorIndex(array, 4, array.Length);
            int shouldBeFive2 = ImageTools.FindColorIndex(array, 5, array.Length);
            int shouldBeMinusOne = ImageTools.FindColorIndex(array, 6, array.Length);
            Assert.AreEqual(3, shouldBeThree);
            Assert.AreEqual(3, shouldBeThree2);
            Assert.AreEqual(3, shouldBeThree3);
            Assert.AreEqual(3, shouldBeThree4);
            Assert.AreEqual(5, shouldBeFive);
            Assert.AreEqual(5, shouldBeFive);
            Assert.AreEqual(-1, shouldBeMinusOne);
        }

        [TestMethod]
        public void FindingBlackIndex()
        {
            int[] array = { 0, 0, 0, 10, 0, 1, 0, 1 };
            int shouldBeZero = ImageTools.FindBlackIndex(array, 0, array.Length);
            int shouldBeOne = ImageTools.FindBlackIndex(array, 1, array.Length);
            int shouldBeTwo = ImageTools.FindBlackIndex(array, 2, array.Length);
            int shouldBeFour = ImageTools.FindBlackIndex(array, 3, array.Length);
            int shouldBeFour2 = ImageTools.FindBlackIndex(array, 4, array.Length);
            int shouldBeSix = ImageTools.FindBlackIndex(array, 5, array.Length);
            int shouldBeSix2 = ImageTools.FindBlackIndex(array, 6, array.Length);
            int shouldBeMinusOne = ImageTools.FindBlackIndex(array, 7, array.Length);
            Assert.AreEqual(0, shouldBeZero);
            Assert.AreEqual(1, shouldBeOne);
            Assert.AreEqual(2, shouldBeTwo);
            Assert.AreEqual(4, shouldBeFour);
            Assert.AreEqual(4, shouldBeFour2);
            Assert.AreEqual(6, shouldBeSix);
            Assert.AreEqual(6, shouldBeSix);
            Assert.AreEqual(-1, shouldBeMinusOne);
        }


    }
}
