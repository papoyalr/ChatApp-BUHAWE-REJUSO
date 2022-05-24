using System;
using System.Globalization;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using ChatApp.Helpers;

namespace ChatApp.Converters
{
    public class IsOwnerConverter : IValueConverter
    {
        DataClass dataClass = DataClass.GetInstance;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool retVal = false;

            string[] players = value as string[];

            if (players[0].Equals(dataClass.loggedInUser.uid))
                retVal = true;

            return retVal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}