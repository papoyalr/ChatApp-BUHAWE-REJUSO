using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ChatApp.Models;
using ChatApp.Helpers;
using ChatApp.Converters;
using ChatApp.DependencyServices;
using Newtonsoft.Json;
using Plugin.CloudFirestore;



namespace ChatApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        DataClass dataClass = DataClass.GetInstance;

        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            lbl_Name.Text = dataClass.loggedInUser.name;
            lbl_Email.Text = dataClass.loggedInUser.email;
            GetContact();
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

        public async void SearchContact(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ResultPage(Search.Text));
        }

        public async void contactsList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (sender is ListView lv) lv.SelectedItem = null;

            Model item = e.Item as Model;

            await Navigation.PushAsync(new ConvoPage(item));
        }

        public void GetContact()
        {
            ObservableCollection<Model> contactList = new ObservableCollection<Model>();

            CrossCloudFirestore.Current
                .Instance
                .Collection("contacts")
                .WhereArrayContains("contactID", dataClass.loggedInUser.uid)
                .AddSnapshotListener((snapshot, error) =>
                {
                    ToggleIndicator(true);

                    if (snapshot != null)
                    {
                        foreach (var documentChange in snapshot.DocumentChanges)
                        {
                            var json = JsonConvert.SerializeObject(documentChange.Document.Data);
                            var obj = JsonConvert.DeserializeObject<Model>(json);
                            switch (documentChange.Type)
                            {
                                case DocumentChangeType.Added:
                                    contactList.Add(obj);
                                    break;
                                case DocumentChangeType.Modified:
                                    if (contactList.Where(c => c.id == obj.id).Any())
                                    {
                                        var item = contactList.Where(c => c.id == obj.id).FirstOrDefault();
                                        item = obj;
                                    }
                                    break;
                                case DocumentChangeType.Removed:
                                    if (contactList.Where(c => c.id == obj.id).Any())
                                    {
                                        var item = contactList.Where(c => c.id == obj.id).FirstOrDefault();
                                        contactList.Remove(item);
                                    }
                                    break;
                            }
                        }
                    }

                    lbl_NoContacts.IsVisible = contactList.Count == 0;
                    contactsList.IsVisible = !(contactList.Count == 0);
                    contactsList.ItemsSource = contactList;

                    ToggleIndicator(false);
                });
        }

        private async void SignOut(object sender, EventArgs e)
        {
            FirebaseAuthResponseModel res = new FirebaseAuthResponseModel() { };
            res = DependencyService.Get<iFirebaseAuth>().SignOut();

            if (res.Status == true)
            {
                App.Current.MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                await DisplayAlert("Error", res.Response, "Okay");
            }
        }
    }
}