using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ChatApp.Models;
using ChatApp.Helpers;
using Newtonsoft.Json;
using Plugin.CloudFirestore;

namespace ChatApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultPage : ContentPage
    {
        DataClass dataClass = DataClass.GetInstance;

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
            ObservableCollection<Model> result = new ObservableCollection<Model>();

            var documents = await CrossCloudFirestore.Current
                                                     .Instance
                                                     .Collection("users")
                                                     .WhereEqualsTo("email", Email)
                                                     .GetAsync(); 
            foreach (var documentChange in documents.DocumentChanges)
            {
                var json = JsonConvert.SerializeObject(documentChange.Document.Data);
                var obj = JsonConvert.DeserializeObject<Model>(json);

                result.Add(obj);
            }

            resultList.ItemsSource = result;

            if (result.Count == 0)
            {
                await DisplayAlert("", "User not found.", "Okay");
                await Navigation.PopAsync();
            }
        }

        public async void ClosePage(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        public async void resultList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            bool dupe = false;

            Model item = e.Item as Model;

            if (sender is ListView lv) lv.SelectedItem = null;

            bool ans = await DisplayAlert("Add contact", "Would you like to add " + item.name + "?", "Yes", "No");

            if (ans)
            {
                foreach (var data in dataClass.loggedInUser.contacts)
                {
                    dupe = data.Equals(item.uid);
                }

                if (dupe)
                {
                    await DisplayAlert("Failed", "You both already have a connection.", "Okay");
                }
                else if (dataClass.loggedInUser.email.Equals(item.email))
                {
                    await DisplayAlert("Error", "You are not allowed to add your own self.", "Okay");
                }
                else
                {
                    ContactModel contact = new ContactModel()
                    {
                        id = IDGenerator.generateID(),
                        contactID = new string[] { DataClass.GetInstance.loggedInUser.uid, item.uid },
                        contactEmail = new string[] { DataClass.GetInstance.loggedInUser.email, item.email },
                        contactName = new string[] { DataClass.GetInstance.loggedInUser.name, item.name },
                        created_at = DateTime.UtcNow
                    };

                    await CrossCloudFirestore.Current
                                             .Instance
                                             .Collection("contacts")
                                             .Document(contact.id)
                                             .SetAsync(contact);

                    if (dataClass.loggedInUser.contacts == null)
                        dataClass.loggedInUser.contacts = new List<string>();
                    dataClass.loggedInUser.contacts.Add(item.uid);
                    await CrossCloudFirestore.Current
                                             .Instance
                                             .Collection("users")
                                             .Document(dataClass.loggedInUser.uid)
                                             .UpdateAsync(new { contacts = dataClass.loggedInUser.contacts });

                    if (item.contacts == null)
                        item.contacts = new List<string>();
                    item.contacts.Add(dataClass.loggedInUser.uid);
                    await CrossCloudFirestore.Current
                                             .Instance
                                             .Collection("users")
                                             .Document(item.uid)
                                             .UpdateAsync(new { contacts = item.contacts });

                    await DisplayAlert("Success", "Contact added!", "Okay");
                }

                await Navigation.PopAsync();
            }
        }
    }
}