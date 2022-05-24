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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void SignUpNavigate(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }

        private async void ResetPassPageNavigate(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ResetPassPage());
        }

        void ToggleIndicator(bool show)
        {
            bg.IsVisible = show;
            actvty_indctr.IsEnabled = show;
            actvty_indctr.IsVisible = show;
            actvty_indctr.IsRunning = show;
        }

        public void ShowHidePass(object sender, EventArgs e)
        {
            Password.IsPassword = Password.IsPassword ? false : true;
            btn_ShowHide.Text = Password.IsPassword ? "Show" : "Hide";
        }

        public void ChangeColor(object sender, EventArgs e)
        {
            if (Email.IsFocused)
            {
                Frame1.BorderColor = Color.Black;
            }

            if (Password.IsFocused)
            {
                Frame2.BorderColor = Color.Black;
            }
        }

        private async void ButtonAction(object sender, EventArgs e)
        {
            ToggleIndicator(true);
            await Task.Delay(2500);
            ToggleIndicator(false);
            await DisplayAlert("", "No functionality.", "Okay");
        }

        private async void SignInAction(object sender, EventArgs e)
        {
            ToggleIndicator(true);
            await Task.Delay(2500);
            ToggleIndicator(false);
            App.Current.Properties["isLoggedIn"] = true;
            App.Current.MainPage = new MainPage();
        }

        public async void SignInProcess(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Email.Text) || string.IsNullOrEmpty(Password.Text))
            {
                if (string.IsNullOrEmpty(Email.Text))
                {
                    Frame1.BorderColor = Color.Red;
                }

                if (string.IsNullOrEmpty(Password.Text))
                {
                    Frame2.BorderColor = Color.Red;
                }

                bool retryBool = await DisplayAlert("Error", "Missing field/s. Retry?", "No", "Yes");
                if (retryBool)
                {
                    Email.Text = string.Empty;
                    Password.Text = string.Empty;
                    Email.Focus();
                }
            }
            else
            {
                ToggleIndicator(true);

                FirebaseAuthResponseModel res = new FirebaseAuthResponseModel() { };
                res = await DependencyService.Get<iFirebaseAuth>().LoginWithEmailPassword(Email.Text, Password.Text);

                if (res.Status == true)
                {
                    Application.Current.MainPage = new NavigationPage(new MainPage());
                }
                else
                {
                    bool retryBool = await DisplayAlert("Error", res.Response + " Retry?", "No", "Yes");
                    if (retryBool)
                    {
                        Email.Text = string.Empty;
                        Password.Text = string.Empty;
                        Email.Focus();
                    }
                }

                ToggleIndicator(false);
            }
        }
    }
}