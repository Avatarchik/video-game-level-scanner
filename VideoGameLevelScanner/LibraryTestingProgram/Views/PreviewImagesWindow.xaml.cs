using Emgu.CV;
using Emgu.CV.Structure;
using ImageRecognitionLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace LibraryTestingProgram.Views
{
    /// <summary>
    /// Interaction logic for PreviewImagesWindow.xaml
    /// </summary>
    public partial class PreviewImagesWindow : Window
    {
        public List<List<Cell>> Values { get; set; }
        private Image<Bgr, byte> previewImage = new Image<Bgr, byte>(100, 100);
        public Bitmap PreviewImage { get { return this.previewImage.Bitmap; } }
        public PreviewImagesWindow()
        {
            InitializeComponent();
            InitializeGrid();
        }

        public class Cell
        {
            private int value = 0;
            public int Value 
            { 
                get { return value; } set { this.value = value; } }
        }

        public void UpdateImage()
        {
            previewImage = ImageTools.DrawRooms((int)this.Preview.ActualWidth, (int)this.Preview.ActualHeight, CellsToArray(Values));
            this.Preview.DataContext = PreviewImage;
        }

        private int[,] CellsToArray(List<List<Cell>> cells)
        {
            var result = new int[cells.Count(), cells.Max(cellsRow => cellsRow.Count())];
            for (int x = 0; x < cells.Count(); ++x)
            {
                for (int y = 0; y < cells[x].Count(); ++y)
                {
                    result[x, y] = cells[x][y].Value;
                }
            }
            return result;
        }

        private void InitializeGrid(){
             Values = new List<List<Cell>>();
            for (int i = 0; i < 5; ++i)
            {
                var row = new List<Cell>();
                for (int j = 0; j < 5; ++j)
                {
                    row.Add(new Cell());
                }
                Values.Add(row);
            }
            this.ValuesGrid.ItemsSource = Values;
            this.Preview.DataContext = PreviewImage;
        }

        private void ImageGenerationButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateImage();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var box = (TextBox)sender;
            box.SelectAll();
        }
    }
}
