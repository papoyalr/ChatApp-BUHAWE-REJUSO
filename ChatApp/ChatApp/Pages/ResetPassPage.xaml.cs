using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResetPassPage : ContentPage
    {
        public ResetPassPage()
        {
            InitializeComponent();
        }
        
        async void LoginPageNavigate(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        void ToggleIndicator(bool show)
        {
            bg.IsVisible = show;
            actvty_indctr.IsEnabled = show;
            actvty_indctr.IsVisible = show;
            actvty_indctr.IsRunning = show;
        }

        private async void SendEmailAction(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Email.Text))
            {
                Frame1.BorderColor = Color.Red;
                await DisplayAlert("Error", "Missing field.", "Okay");
            }
            else
            {
                ToggleIndicator(true);
                await Task.Delay(2500);
                await DisplayAlert("Success", "Email has been sent to your email address.", "Okay");
                ToggleIndicator(false);
                await Navigation.PopAsync();
            }
        }

        public void ChangeColor(object sender, EventArgs e)
        {
            if (Email.IsFocused)
            {
                Frame1.BorderColor = Color.Black;
            }
        }
    }
}