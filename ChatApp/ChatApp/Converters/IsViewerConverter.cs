using System;
using System.Globalization;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using ChatApp.Helpers;
using ChatApp.Models;

namespace ChatApp.Converters
{
    public class IsViewerConverter : IValueConverter
    {
        DataClass dataClass = DataClass.GetInstance;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool retVal = false;

            if (value != null)
            {
                string id = value as string;

                if (id.Equals(dataClass.loggedInUser.uid))
                    retVal = true;
            }

            return retVal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}