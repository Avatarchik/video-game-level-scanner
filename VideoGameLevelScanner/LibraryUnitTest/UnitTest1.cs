﻿using System;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using ImageRecognitionLibrary;

using Emgu.CV;
using Emgu.CV.Structure;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace LibraryUnitTest
{
    [TestClass]
    public class FiltersTests
    {
        [TestMethod]
        public void CombineMapsBW()
        {
            var result = ImageTools.CombineMapsBW(new Image<Gray,byte>[]{
                new Image<Gray,byte>(Properties.Resources.CombineBW1),
                new Image<Gray,byte>(Properties.Resources.CombineBW2)
            }).ToBitmap();

            var expected = Properties.Resources.CombineBWresult;

            var actual = result;
           
            Assert.AreEqual(true, CompareBitmaps(expected,actual));            
        }

        [TestMethod]
        public void CombineMaps()
        {
            var result = ImageTools.CombineMaps(new Tuple<Image<Gray, byte>, Bgr>[]{
                new Tuple<Image<Gray, byte>,Bgr>(new Image<Gray,byte>(Properties.Resources.Combine1),new Bgr(0,0,255)),
                new Tuple<Image<Gray, byte>,Bgr>(new Image<Gray,byte>(Properties.Resources.Combine2),new Bgr(0,255,0)),
                new Tuple<Image<Gray, byte>,Bgr>(new Image<Gray,byte>(Properties.Resources.Combine3),new Bgr(255,0,0)),
                new Tuple<Image<Gray, byte>,Bgr>(new Image<Gray,byte>(Properties.Resources.Combine4),new Bgr(0,255,255)),
            });

            var expected = Properties.Resources.Combineresult;
            var actual = result.ToBitmap();
            
            Assert.IsTrue(CompareBitmaps(expected, actual));
        }

        [TestMethod]
        public void FilterFromSingleRange()
        {
            var img = new Image<Hsv,byte>(Properties.Resources.Combineresult);
            var filtered = ImageTools.FilterColor(img, new Hsv(35, 70, 35), new Hsv(90, 255, 255));

            var expected = Properties.Resources.Combine2;
            var actual = filtered.ToBitmap();

            Assert.IsTrue(CompareBitmaps(expected, actual));
        }

        [TestMethod]
        public void FilterFromMultipleRanges()
        {
            var img = new Image<Hsv, byte>(Properties.Resources.Combineresult);
            var ranges = new List<Tuple<Hsv, Hsv>> { rangeYellow };
            ranges.AddRange(rangeRed);

            var filtered = ImageTools.FilterColor(img, ranges);

            var expected = Properties.Resources.FilterYellowandRed;
            var actual = filtered.ToBitmap();

            Assert.IsTrue(CompareBitmaps(expected, actual));
        }



        #region Helping functions and variables
        private bool CompareBitmaps(System.Drawing.Image left, System.Drawing.Image right)
        {
            if (object.Equals(left, right))
                return true;
            if (left == null || right == null)
                return false;
            if (!left.Size.Equals(right.Size) || !left.PixelFormat.Equals(right.PixelFormat))
                return false;

            Bitmap leftBitmap = left as Bitmap;
            Bitmap rightBitmap = right as Bitmap;
            if (leftBitmap == null || rightBitmap == null)
                return true;

            #region Code taking more time for comparison

            for (int col = 0; col < left.Width; col++)
            {
                for (int row = 0; row < left.Height; row++)
                {
                    if (!leftBitmap.GetPixel(col, row).Equals(rightBitmap.GetPixel(col, row)))
                        return false;
                }
            }

            #endregion

            return true;
        }
        private Bgr Red = new Bgr(0, 0, 255);
        private Bgr Green = new Bgr(0, 255, 0);
        private Bgr Blue = new Bgr(255, 0, 0);
        private Bgr Yellow = new Bgr(0, 255, 255);
        private Tuple<Hsv, Hsv>[] rangeRed = new Tuple<Hsv, Hsv>[]{
                    new Tuple<Hsv,Hsv>(new Hsv(0, 85, 80), new Hsv(12, 255, 255)),
                    new Tuple<Hsv,Hsv>(new Hsv(150,85,80), new Hsv(179,255,255))
                };
        private Tuple<Hsv, Hsv> rangeBlue = new Tuple<Hsv, Hsv>(new Hsv(90, 90, 50), new Hsv(120, 255, 255));
        private Tuple<Hsv, Hsv> rangeGreen = new Tuple<Hsv, Hsv>(new Hsv(35, 70, 35), new Hsv(90, 255, 255));
        private Tuple<Hsv, Hsv> rangeYellow = new Tuple<Hsv, Hsv>(new Hsv(10, 70, 127), new Hsv(35, 255, 255));
        #endregion
    }
}
