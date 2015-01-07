using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Emgu.CV.Structure;
using Emgu.CV;
using Emgu.CV.UI;

using ImageRecognitionLibrary;
using System.Windows.Threading;
using LibraryTestingProgram.Views;

namespace LibraryTestingProgram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Capture capture;
        DispatcherTimer timer;
        Image<Bgr, Byte> CameraFrame;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void StaticImageButton_Click(object sender, RoutedEventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();
            Image<Hsv,byte> img = new Image<Hsv,byte>(args[1]);

            var dd = img.Convert<Bgr,byte>();
            var di = Filter(dd,true);
            ImageTools.ShowInNamedWindow(img.Convert<Bgr,byte>(), "Original");
        }

        private void CameraButton_Click(object sender, RoutedEventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();
            Image<Hsv, byte> img = new Image<Hsv, byte>(args[1]);

            var dd = img.Convert<Bgr, byte>();
            var di = DetectionImage(dd,true);
            ImageTools.ShowInNamedWindow(di, "Detection");
            ImageTools.ShowInNamedWindow(img.Convert<Bgr, byte>(), "Original");
            //capture = new Capture(0); //create a camera captue
            //timer = new DispatcherTimer();
            //timer.Tick += new EventHandler(timer_Tick);
            //timer.Interval = new TimeSpan(0, 0, 0, 0, 45);
            //timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            CameraFrame = capture.QueryFrame();
            if (CameraFrame != null)
            {
                var img = DetectionImage(CameraFrame);

                CvInvoke.cvShowImage("Detection", img);
                CvInvoke.cvShowImage("Camera Capture", CameraFrame);
            }
        }

        private Image<Bgr,byte> Filter(Image<Bgr, byte> sourceImg, bool debugMode = false)
        {
            Image<Hsv, byte> img = sourceImg.Convert<Hsv,byte>();

            Image<Gray, byte> blue = ImageTools.FilterColor(img, new Hsv(90, 90, 50), new Hsv(120, 255, 255), debugMode ? "Blue Debug Window" : "");
            Image<Gray, byte> green = ImageTools.FilterColor(img, new Hsv(35, 70, 35), new Hsv(90, 255, 255), debugMode ? "Green Debug Window" : "");
            Image<Gray, byte> yellow = ImageTools.FilterColor(img, new Hsv(10, 70, 127), new Hsv(35, 255, 255), debugMode ? "Yellow Debug Window" : "");
            Image<Gray, byte> red = ImageTools.FilterColor(
                img,
                new KeyValuePair<Hsv, Hsv>[]{
                    new KeyValuePair<Hsv,Hsv>(new Hsv(0, 85, 80), new Hsv(12, 255, 255)),
                    new KeyValuePair<Hsv,Hsv>(new Hsv(150,85,80), new Hsv(179,255,255))
                },
                debugMode ? "Red Debug Window" : ""
            );
            var colorDetection = ImageTools.CombineMaps(new List<KeyValuePair<Image<Gray, byte>, Bgr>> {
                new KeyValuePair<Image<Gray,byte>,Bgr>(blue, ImageTools.Colors.Blue),
                new KeyValuePair<Image<Gray,byte>,Bgr>(red, ImageTools.Colors.Red),
                new KeyValuePair<Image<Gray,byte>,Bgr>(green, ImageTools.Colors.Green),
                new KeyValuePair<Image<Gray,byte>,Bgr>(yellow, ImageTools.Colors.Yellow),
            });
            return colorDetection;
        }

        private Image<Gray, byte> DetectionImage(Image<Bgr, byte> sourceImg, bool debugMode = false)
        {
            Image<Hsv, byte> img = sourceImg.Convert<Hsv, byte>();

            Image<Gray, byte> blue = ImageTools.FilterColor(img, new Hsv(90, 90, 50), new Hsv(120, 255, 255));
            Image<Gray, byte> green = ImageTools.FilterColor(img, new Hsv(35, 70, 35), new Hsv(90, 255, 255));
            Image<Gray, byte> yellow = ImageTools.FilterColor(img, new Hsv(10, 70, 127), new Hsv(35, 255, 255));
            Image<Gray, byte> red = ImageTools.FilterColor(
                img,
                new KeyValuePair<Hsv, Hsv>[]{
                    new KeyValuePair<Hsv,Hsv>(new Hsv(0, 85, 80), new Hsv(12, 255, 255)),
                    new KeyValuePair<Hsv,Hsv>(new Hsv(150,85,80), new Hsv(179,255,255))
                }                
            );

            DetectionData ddb = ImageTools.DetectSquares(blue, debugMode ? "Blue Debug Window" : "");
            DetectionData ddr = ImageTools.DetectSquares(red, debugMode ? "Red Debug Window" : "");
            DetectionData ddg = ImageTools.DetectSquares(green,  debugMode ? "Green Debug Window" : "");
            DetectionData ddy = ImageTools.DetectSquares(yellow, debugMode ? "Yellow Debug Window" : "");
            ddb.RemoveNoises();
            ddr.RemoveNoises();
            ddg.RemoveNoises();
            ddy.RemoveNoises();
            ddb.AddColor(ddr);
            ddb.AddColor(ddg);
            ddb.AddColor(ddy);
            
            return ddb.DrawDetection().Convert<Gray,byte>();
        }

        private void FilteringButton_Click(object sender, RoutedEventArgs e)
        {
            new FilteringWindow().Show();
        }

        private void StaticImageFilteringButton_Click(object sender, RoutedEventArgs e)
        {
            new StaticImageFilteringWindow().Show();
        }

        private void BoardButton_Click(object sender, RoutedEventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();
            Image<Hsv, byte> img = new Image<Hsv, byte>(args[1]);

            Image<Gray, byte> blue = ImageTools.FilterColor(img, new Hsv(90, 90, 50), new Hsv(120, 255, 255));
            Image<Gray, byte> green = ImageTools.FilterColor(img, new Hsv(35, 70, 35), new Hsv(90, 255, 255));
            Image<Gray, byte> yellow = ImageTools.FilterColor(img, new Hsv(10, 70, 127), new Hsv(35, 255, 255));
            Image<Gray, byte> red = ImageTools.FilterColor(
                img,
                new KeyValuePair<Hsv, Hsv>[]{
                    new KeyValuePair<Hsv,Hsv>(new Hsv(0, 85, 80), new Hsv(12, 255, 255)),
                    new KeyValuePair<Hsv,Hsv>(new Hsv(150,85,80), new Hsv(179,255,255))
                }
            );

            DetectionData ddb = ImageTools.DetectSquares(blue);
            DetectionData ddr = ImageTools.DetectSquares(red);
            DetectionData ddg = ImageTools.DetectSquares(green);
            DetectionData ddy = ImageTools.DetectSquares(yellow);
            ddb.RemoveNoises();
            ddr.RemoveNoises();
            ddg.RemoveNoises();
            ddy.RemoveNoises();
            ddb.AddColor(ddr);
            ddb.AddColor(ddg);
            ddb.AddColor(ddy);

            var board = ddb.CreateBoard();
            var di = ddb.DrawDetection().Bitmap;
            MessageBox.Show("Detected board: " + board.Height + "x" + board.Width);

            ImageTools.ShowInNamedWindow(img.Convert<Bgr, byte>(), "Original");
        }

        private void PreviewImagesButton_Click(object sender, RoutedEventArgs e)
        {
            new PreviewImagesWindow().Show();
        }
        
        
    }
}
