using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace LibraryTestingProgram
{
    public class SolidColorBrushToHsvConverter : IValueConverter
    {
        private static Image<Bgr,byte> buffer = new Image<Bgr,byte>(new System.Drawing.Size(1,1));
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!String.IsNullOrWhiteSpace(parameter.ToString()))
            {
                var brush = (SolidColorBrush)value;
                var color = new Bgr(brush.Color.B,brush.Color.G,brush.Color.R);
                buffer.SetValue(color);
                var data = buffer.Convert<Hsv,byte>().Data;
                string param = parameter.ToString();
                switch(param){
                    case "h":
                    case "H":
                        return data[0, 0, 0];
                    case "s":
                    case "S":
                        return data[0, 0, 1];
                    case "v":
                    case "V":
                        return data[0, 0, 2];
                    default:
                        break;
                }
            }
            throw new ArgumentException("Wrong Parameter. It can be only one from H,h,S,s,V,v.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
