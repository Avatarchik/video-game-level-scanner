using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryTestingProgram
{
    public class FilteringParameters
    {
        public static Hsv blueMin = new Hsv(90, 90, 90);
        public static Hsv blueMax = new Hsv(120, 255, 255);
        public static Hsv redMin = new Hsv(0, 85, 80);
        public static Hsv redMax = new Hsv(12, 225, 225);
        public static Hsv redMin2 = new Hsv(150, 85, 80);
        public static Hsv redMax2 = new Hsv(179, 255, 255);
        public static Hsv greenMin = new Hsv(35, 70, 35);
        public static Hsv greenMax = new Hsv(90, 225, 225);
        public static Hsv yellowMin = new Hsv(10, 70, 127);
        public static Hsv yellowMax = new Hsv(35, 225, 225);

        public static Hsv BlueMin { get { return blueMin; } set { blueMin = value; } }
        public static Hsv BlueMax { get { return blueMax; } set { blueMax = value; } }
        public static Hsv RedMin { get { return redMin; } set { redMin = value; } }
        public static Hsv RedMax { get { return redMax; } set { redMax = value; } }
        public static Hsv RedMin2 { get { return redMin2; } set { redMin2 = value; } }
        public static Hsv RedMax2 { get { return redMax2; } set { redMax2 = value; } }
        public static Hsv GreenMin { get { return greenMin; } set { greenMin = value; } }
        public static Hsv GreenMax { get { return greenMax; } set { greenMax = value; } }
        public static Hsv YellowMin { get { return yellowMin; } set { yellowMin = value; } }
        public static Hsv YellowMax { get { return yellowMax; } set { yellowMax = value; } }
    }
}
