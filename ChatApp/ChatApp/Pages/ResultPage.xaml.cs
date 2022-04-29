using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultPage : ContentPage
    {
 
        public class CreatedAt
        {
            public int Seconds { get; set; }
            public int Nanoseconds { get; set; }
        }

        public class Model
        {
            public string uid { get; set; }
            public string email { get; set; }
            public string name { get; set; }
            public List<string> contacts { get; set; }
            public int userType { get; set; }
            public CreatedAt created_at { get; set; }
        }

        public ResultPage(string Email)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            SearchContact(Email);
        }

        public async void SearchContact(String Email)
        {
            if(Email != "sample@email.com")
            {
                await DisplayAlert("", "User not found.", "Okay");
                await Navigation.PopAsync();
            }
        }

        public async void DisplayOverlay1(object sender, EventArgs e)
        {
            
            bool ans = await DisplayAlert("Add contact", "Would you like to add this contact?", "Yes", "No");

            if (ans)
            {
                if (ItemFrame1.Text=="Chiz Beloy")
                {
                    await DisplayAlert("Failed", "You both already have a connection.", "Okay");
                }
   
                /* await DisplayAlert("Success", "Contact added!", "Okay"); */

                await Navigation.PopAsync();
            }
        }

        public async void DisplayOverlay2(object sender, EventArgs e)
        {

            bool ans = await DisplayAlert("Add contact", "Would you like to add this contact?", "Yes", "No");

            if (ans)
            {
                if (ItemFrame2.Text == "Arnold")
                {
                    await DisplayAlert("Error", "You are not allowed to add your own self.", "Okay");
                }

                /* await DisplayAlert("Success", "Contact added!", "Okay"); */

                await Navigation.PopAsync();
            }
        }

        public async void ClosePage(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}