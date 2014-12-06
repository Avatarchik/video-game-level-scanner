using Emgu.CV;
using Emgu.CV.Structure;
using ImageRecognitionLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace LibraryTestingProgram.ViewModels
{
    enum OperationType { Filtering, Detection, Colors, Rooms}
    public class ImagesSetViewModel
    {
        Image<Bgr, byte> sourceImg;
        Image<Gray, byte> blueImg;
        Image<Gray, byte> redImg;
        Image<Gray, byte> greenImg;
        Image<Gray, byte> yellowImg;
        string imageFilePath;
        OperationType operation;

        public Bitmap SourceImg { get { return sourceImg.Bitmap; } }
        public Bitmap BlueImg { get { return blueImg.Bitmap; } }
        public Bitmap RedImg { get { return redImg.Bitmap; } }
        public Bitmap GreenImg { get { return greenImg.Bitmap; } }
        public Bitmap YellowImg { get { return yellowImg.Bitmap; } }
        public string ImageFilePath 
        { 
            get { return imageFilePath; }
            set
            {
                imageFilePath = value;
                UpdateImages(imageFilePath);
            }
        }

        public ImagesSetViewModel()
        {
            ImageFilePath = "";
            operation = OperationType.Filtering;
        }

        private void UpdateImages(string path)
        {
            if (String.IsNullOrWhiteSpace(path))
                sourceImg = new Image<Bgr, byte>(100, 100);
            else
                sourceImg = new Image<Bgr, byte>(imageFilePath);
            var hsvImg = sourceImg.Convert<Hsv, byte>();
            
            blueImg = ImageTools.FilterColor(hsvImg, UserSettings.instance.blueMin, UserSettings.instance.blueMax);
            redImg = ImageTools.FilterColor(hsvImg, UserSettings.instance.RedRange);
            greenImg = ImageTools.FilterColor(hsvImg, UserSettings.instance.greenMin, UserSettings.instance.greenMax);
            yellowImg = ImageTools.FilterColor(hsvImg, UserSettings.instance.yellowMin, UserSettings.instance.yellowMax);

        }

    }
}
