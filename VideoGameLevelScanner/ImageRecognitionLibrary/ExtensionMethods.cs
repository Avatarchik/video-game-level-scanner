using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;

namespace RectangleExtension
{
    public static class ExtensionMethods
    {
        public static int[] Column(this Image<Gray, byte> img, int index)
        {
            var line = img.Sample(new LineSegment2D(new Point(index, 0), new Point(index, img.Rows - 1)));

            int length = img.Rows;
            int[] column = new int[length];
            byte[, ,] data = img.Data;
            for (int i = 0; i < length - 1; i++)
            {
                column[i] = (int)img[i,index].Intensity;
            }
            return column;
        }

        public static int[] Row(this Image<Gray, byte> img, int index)
        {
            var line = img.Sample(new LineSegment2D(new Point(0, index), new Point(img.Cols - 1, index)));

            int length = img.Cols;
            int[] row = new int[length];
            byte[, ,] data = img.Data;
            for (int i = 0; i < length - 1; i++)
            {
                row[i] = (int)img[index,i].Intensity;
            }
            return row;
        }
    }
}
