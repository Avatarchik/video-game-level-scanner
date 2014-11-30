using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace LibraryTestingProgram
{
    class ColorsToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            byte theH,theS,theV;
            byte h,s,v;
            byte H, S, V;
            if (values.Count() >= 9
                && byte.TryParse(values[0].ToString(), out theH)
                && byte.TryParse(values[1].ToString(), out theS)
                && byte.TryParse(values[2].ToString(), out theV)
                && byte.TryParse(values[3].ToString(), out h)
                && byte.TryParse(values[4].ToString(), out s)
                && byte.TryParse(values[5].ToString(), out v)
                && byte.TryParse(values[6].ToString(), out H)
                && byte.TryParse(values[7].ToString(), out S)
                && byte.TryParse(values[8].ToString(), out V)
                && h <= theH && theH <= H
                && s <= theS && theS <= S
                && v <= theV && theV <= V)
                return Visibility.Visible;
            return Visibility.Visible;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
