using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;

namespace ChatApp.Droid
{
    [Activity(Label = "ChatApp", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var density = Resources.DisplayMetrics.Density;
            App.screenWidth = Resources.DisplayMetrics.WidthPixels / density;
            App.screenHeight = Resources.DisplayMetrics.HeightPixels / density;

            if (Xamarin.Forms.Device.Idiom == Xamarin.Forms.TargetIdiom.Phone)
                App.screenHeight = (16 * App.screenWidth) / 9;

            if (Xamarin.Forms.Device.Idiom == Xamarin.Forms.TargetIdiom.Tablet)
                App.screenWidth = (9 * App.screenHeight) / 16;

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}