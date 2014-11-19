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

            Image<Gray, byte> blue = ImageTools.FilterColor(img, new Hsv(90,90,50),new Hsv(120,255,255), "Blue Debug Window");
            Image<Gray, byte> green= ImageTools.FilterColor(img, new Hsv(35, 70, 35), new Hsv(90, 255, 255), "Green Debug Window");
            Image<Gray, byte> yellow = ImageTools.FilterColor(img, new Hsv(10,70,127), new Hsv(35,255,255), "Yellow Debug Window");
            Image<Gray, byte> red = ImageTools.FilterColor(
                img, 
                new Tuple<Hsv,Hsv>[]{
                    new Tuple<Hsv,Hsv>(new Hsv(0, 85, 80), new Hsv(12, 255, 255)),
                    new Tuple<Hsv,Hsv>(new Hsv(150,85,80), new Hsv(179,255,255))
                }, 
                "Red Debug Window"
                );

        }

        private void CameraButton_Click(object sender, RoutedEventArgs e)
        {
            capture = new Capture(); //create a camera captue
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 25);
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            CameraFrame = capture.QueryFrame();
            CvInvoke.cvShowImage("Camera Capture", CameraFrame);

        }

    }
}
