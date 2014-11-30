using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LibraryTestingProgram
{
    /// <summary>
    /// Interaction logic for StaticImageFilteringWindow.xaml
    /// </summary>
    public partial class StaticImageFilteringWindow : Window
    {
        Image<Bgr, byte> sourceImg { get; set; }
        Image<Gray, byte> blueImg;
        Image<Gray, byte> redImg;
        Image<Gray, byte> greenImg;
        Image<Gray, byte> yellowImg;

        public Bitmap SourceImg { get { return sourceImg.Bitmap; } }
        public Bitmap BlueImg { get { return blueImg.Bitmap; } }
        public Bitmap RedImg { get { return redImg.Bitmap; } }
        public Bitmap GreenImg { get { return greenImg.Bitmap; } }
        public Bitmap YellowImg { get { return yellowImg.Bitmap; } }

        public StaticImageFilteringWindow()
        {
            InitializeComponent();
        }
    }
}
