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
    public enum OperationType { Filtering, Detection, Colors, Rooms}
    public class ImagesSetViewModel
    {
        Image<Bgr, byte> sourceImg;
        Image<Gray, byte> blueImg;
        Image<Gray, byte> redImg;
        Image<Gray, byte> greenImg;
        Image<Gray, byte> yellowImg;
        Image<Bgr,byte> composedImg;
        string imageFilePath;
        OperationType operation;
        Board board;

        public Bitmap SourceImg { get { return sourceImg.Bitmap; } }
        public Bitmap BlueImg { get { return blueImg.Bitmap; } }
        public Bitmap RedImg { get { return redImg.Bitmap; } }
        public Bitmap GreenImg { get { return greenImg.Bitmap; } }
        public Bitmap YellowImg { get { return yellowImg.Bitmap; } }

        public int BoardX
        {
            get
            {
                if (board != null)
                    return board.Height;
                else
                    return 0;
            }
        }
        public int BoardY
        {
            get
            {
                if (board != null)
                    return board.Width;
                else
                    return 0;
            }
        }

        
        public OperationType Operation 
        { 
            get { return operation; } 
            set 
            { 
                operation = value; 
                UpdateImages(imageFilePath); 
            } 
        }
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

        public ImagesSetViewModel(OperationType operation, string path)
        {
            imageFilePath = path;
            this.Operation = operation;
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
            composedImg = ImageTools.CombineMaps(new List<Tuple<Image<Gray, byte>, Bgr>>
            {
                new Tuple<Image<Gray, byte>, Bgr>(blueImg, ImageTools.Colors.Blue),
                new Tuple<Image<Gray, byte>, Bgr>(redImg, ImageTools.Colors.Red),
                new Tuple<Image<Gray, byte>, Bgr>(greenImg, ImageTools.Colors.Green),
                new Tuple<Image<Gray, byte>, Bgr>(yellowImg, ImageTools.Colors.Yellow),

            });
            
            if (operation == OperationType.Detection) 
            {
                DetectionData ddb = ImageTools.DetectSquares(blueImg);
                DetectionData ddr = ImageTools.DetectSquares(redImg);
                DetectionData ddg = ImageTools.DetectSquares(greenImg);
                DetectionData ddy = ImageTools.DetectSquares(yellowImg);

                ddb.RemoveNoises();
                ddr.RemoveNoises();
                ddg.RemoveNoises();
                ddy.RemoveNoises();

                DetectionData common = new DetectionData(ddb);
                common.AddColor(ddr);
                common.AddColor(ddg);
                common.AddColor(ddy);

                composedImg = common.DrawDetection().Convert<Bgr,byte>();
                blueImg = ddb.DrawDetection();
                redImg = ddr.DrawDetection();
                greenImg = ddg.DrawDetection();
                yellowImg = ddy.DrawDetection();

                this.board = common.CreateBoard();
            }


        }

    }
}
