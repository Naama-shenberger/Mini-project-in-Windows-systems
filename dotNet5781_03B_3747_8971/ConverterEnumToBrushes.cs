using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using dotNet5781_01_3747_8971;
using System.Windows.Media;

namespace dotNet5781_03B_3747_8971
{
    class ConverterEnumToBrushes:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!(value is Bus.Situation))
            throw new ArgumentException("value not of type StateValue");
        Bus.Situation sv = (Bus.Situation)value;
        //sanity checks
        switch (sv)
        {
            case Bus.Situation.Ready_to_go:
                return Colors.Green;
            case Bus.Situation.refueling:
                return Colors.Orange;
            case Bus.Situation.Dangerous:
                return Colors.Red;
            case Bus.Situation.In_the_middle_of_A_ride:
                return Colors.Blue;
            default:
                return Colors.White;
        }
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
}
