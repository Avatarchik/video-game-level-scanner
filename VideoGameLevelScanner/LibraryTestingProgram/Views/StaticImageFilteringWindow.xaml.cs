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
            this.ViewModel = new ImagesSetViewModel();
            InitializeComponent();
            this.DataContext = ViewModel;
        }

        private void LoadImageButton_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new Microsoft.Win32.OpenFileDialog(){Filter = "BMP files (*.bmp)|*.bmp|JPEG files|*.jpg"};
            bool? result = fileDialog.ShowDialog();

            if (result == false)
                return;

            string filePath = fileDialog.FileName;

            UpdateViewModel(ViewModel.Operation, filePath);
        }

        private void FilteringRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            UpdateViewModel(OperationType.Filtering, ViewModel.ImageFilePath);
        }

        private void DetectionRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            UpdateViewModel(OperationType.Detection, ViewModel.ImageFilePath);
        }

        private void UpdateViewModel(OperationType operation, string path)
        {
            var vm = new ImagesSetViewModel(operation, path);
            this.ViewModel = vm;
            this.DataContext = ViewModel;
        }
        

    }
}
