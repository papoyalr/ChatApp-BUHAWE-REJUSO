using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        public async void SearchContact(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ResultPage(Search.Text));
        }
        
        void ToggleIndicator(bool show)
        {
            bg.IsVisible = show;
            actvty_indctr.IsEnabled = show;
            actvty_indctr.IsVisible = show;
            actvty_indctr.IsRunning = show;
        }

        public void Clear(object sender, EventArgs e)
        {
            Search.Text = "";
        }

        public async void NavigateConvo(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ConvoPage());
        }

        private async void SignOut(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}