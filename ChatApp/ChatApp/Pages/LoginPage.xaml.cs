using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;



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

        private async void SignInNavigate(object sender, EventArgs e)
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

                await DisplayAlert("Error", "Missing fields.", "Okay");
                Email.Text = string.Empty;
                Password.Text = string.Empty;
            }
            else
            {
                ToggleIndicator(true);
                await Navigation.PushAsync(new MainPage());
            }
            
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
    }
}