using Emgu.CV;
using Emgu.CV.Structure;
using ImageRecognitionLibrary;
using LibraryTestingProgram.ViewModels;
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

namespace LibraryTestingProgram.Views
{
    /// <summary>
    /// Interaction logic for StaticImageFilteringWindow.xaml
    /// </summary>
    public partial class StaticImageFilteringWindow : Window
    {
        private ImagesSetViewModel ViewModel { get; set; }

        public StaticImageFilteringWindow()
        {
            InitializeComponent();
            this.ViewModel = new ImagesSetViewModel();
            this.DataContext = ViewModel;
        }

        private void LoadImageButton_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new Microsoft.Win32.OpenFileDialog(){Filter = "BMP files (*.bmp)|*.bmp"};
            bool? result = fileDialog.ShowDialog();

            if (result == false)
                return;

            string filePath = fileDialog.FileName;

            var vm = new ImagesSetViewModel();
            vm.ImageFilePath = filePath;
            this.ViewModel = vm;
            
            this.DataContext = ViewModel;
        }


    }
}
