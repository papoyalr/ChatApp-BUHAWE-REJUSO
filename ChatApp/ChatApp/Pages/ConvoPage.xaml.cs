using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConvoPage : ContentPage
    {
        public class CreatedAt
        {
            public int Seconds { get; set; }
            public int Nanoseconds { get; set; }
        }

        public class ConvoModel 
        {
            public string id { get; set; }
            public string message { get; set; }
            public string converseeID { get; set; }
            public CreatedAt created_at { get; set; }
        }

        public ConvoPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
        }

        public async void CloseConvo(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}