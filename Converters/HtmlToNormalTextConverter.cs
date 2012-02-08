using System;
using System.Globalization;
using System.Windows.Data;
using Reader.Workers;

namespace Reader.Converters
{
    public class HtmlToNormalTextConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var text = (string) value;

            return HtmlToTextWorker.ConvertHtmlToText(text);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
