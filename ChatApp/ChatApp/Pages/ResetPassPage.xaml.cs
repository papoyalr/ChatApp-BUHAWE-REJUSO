using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ChatApp.Models;
using ChatApp.DependencyServices;

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

        public void ChangeColor(object sender, EventArgs e)
        {
            if (Email.IsFocused)
            {
                Frame1.BorderColor = Color.Black;
            }
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

                FirebaseAuthResponseModel res = new FirebaseAuthResponseModel() { };
                res = await DependencyService.Get<iFirebaseAuth>().ResetPassword(Email.Text);

                if (res.Status == true)
                {
                    await DisplayAlert("Success", res.Response, "Okay");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", res.Response, "Okay");
                }

                ToggleIndicator(false);
            }
        }
    }
}