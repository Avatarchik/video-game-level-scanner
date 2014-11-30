using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace LibraryTestingProgram
{
    public class RGBColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            byte r,g,b;
            if (values.Count() >= 3 
                && byte.TryParse(values[0].ToString() ,out r)
                && byte.TryParse(values[1].ToString(), out g)
                && byte.TryParse(values[2].ToString(), out b))
                return new SolidColorBrush(Color.FromRgb(r,g,b));
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            string[] splitValues = ((string)value).Split(' ');
            return splitValues;
        }
    }
}
