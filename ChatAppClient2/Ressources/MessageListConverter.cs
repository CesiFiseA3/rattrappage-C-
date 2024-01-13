using ChatAppClient.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ChatAppClient.Ressources
{
    public class MessageListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<MessageDataModel> messages)
            {
                return string.Join(Environment.NewLine, messages.Select(m => m.content));
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public string ManualConvert(object value) {
            if (value is IEnumerable<MessageDataModel> messages)
            {
                return string.Join(Environment.NewLine, messages.Select(m => m.content));
            }
            return string.Empty;
        }
    }

}
