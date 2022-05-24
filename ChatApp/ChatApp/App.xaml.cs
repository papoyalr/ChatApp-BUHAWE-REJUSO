using ChatApp.Helpers;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatApp
{
    public partial class App : Application
    {
        public static float screenWidth { get; set; }
        public static float screenHeight { get; set; }
        public static float appScale { get; set; }


        DataClass dataClass = DataClass.GetInstance;
        public App()
        {
            InitializeComponent();

            if (dataClass.isSignedIn)
            {
                MainPage = new NavigationPage(new Pages.MainPage());
            }
            else
            {
                MainPage = new NavigationPage(new Pages.LoginPage());
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
