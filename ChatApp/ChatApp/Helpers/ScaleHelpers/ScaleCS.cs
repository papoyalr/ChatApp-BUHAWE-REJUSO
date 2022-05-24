using Newtonsoft.Json;
using Xamarin.Forms;

namespace ChatApp.Helpers.ScaleHelper
{
    public static class ScaleCS
    {
        public static float ScaleHeight(this int number, int? iOS = null)
        {
            if (iOS.HasValue && Device.RuntimePlatform == Device.iOS)
                number = iOS.Value;

            return (float)(number * (App.screenHeight / 568.0));
        }

        public static float ScaleHeight(this double number, double? iOS = null)
        {
            if (iOS.HasValue && Device.RuntimePlatform == Device.iOS)
                number = iOS.Value;

            return (float)(number * (App.screenHeight / 568.0));
        }

        public static float ScaleHeight(this float number, float? iOS = null)
        {
            if (iOS.HasValue && Device.RuntimePlatform == Device.iOS)
                number = iOS.Value;

            return (float)(number * (App.screenHeight / 568.0));
        }


        public static float ScaleWidth(this int number, int? iOS = null)
        {
            if (iOS.HasValue && Device.RuntimePlatform == Device.iOS)
                number = iOS.Value;

            return (float)(number * (App.screenWidth / 320.0));
        }

        public static float ScaleWidth(this double number, double? iOS = null)
        {
            if (iOS.HasValue && Device.RuntimePlatform == Device.iOS)
                number = iOS.Value;

            return (float)(number * (App.screenWidth / 320.0));
        }

        public static float ScaleWidth(this float number, float? iOS = null)
        {
            if (iOS.HasValue && Device.RuntimePlatform == Device.iOS)
                number = iOS.Value;

            return (float)(number * (App.screenWidth / 320.0));
        }


        public static double ScaleFont(this int number, int? iOS = null)
        {
            if (iOS.HasValue && Device.RuntimePlatform == Device.iOS)
                number = iOS.Value;

            return (number * (App.screenHeight / 568.0) - (Device.RuntimePlatform == Device.iOS ? 0.5 : 0));
        }

        public static double ScaleFont(this double number, double? iOS = null)
        {
            if (iOS.HasValue && Device.RuntimePlatform == Device.iOS)
                number = iOS.Value;

            return (number * (App.screenHeight / 568.0) - (Device.RuntimePlatform == Device.iOS ? 0.5 : 0));
        }
    }
}
