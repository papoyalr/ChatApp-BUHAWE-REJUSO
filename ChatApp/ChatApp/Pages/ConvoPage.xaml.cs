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
    public partial class ConvoPage : ContentPage
    {
        DataClass dataClass = DataClass.GetInstance;
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


        public ConvoPage(Model data)
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
            conversee.BindingContext = data;
            send.BindingContext = data;
            GetConvo(data);
        }

        public async void CloseConvo(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        public void GetConvo(Model data)
        {
            Model contact = data;

            ObservableCollection<ConvoModel> conversationList = new ObservableCollection<ConvoModel>();

            CrossCloudFirestore.Current
                .Instance
                .Collection("contacts")
                .Document(contact.id)
                .Collection("conversations")
                .OrderBy("created_at", false)
                .AddSnapshotListener((snapshot, error) =>
                {
                    conversationListView.ItemsSource = conversationList;
                    if (snapshot != null)
                    {
                        foreach (var documentChange in snapshot.DocumentChanges)
                        {
                            var json = JsonConvert.SerializeObject(documentChange.Document.Data);
                            var obj = JsonConvert.DeserializeObject<ConvoModel>(json);
                            switch (documentChange.Type)
                            {
                                case DocumentChangeType.Added:
                                    conversationList.Add(obj);
                                    break;
                                case DocumentChangeType.Modified:
                                    if (conversationList.Where(c => c.id == obj.id).Any())
                                    {
                                        var item = conversationList.Where(c => c.id == obj.id).FirstOrDefault();
                                        item = obj;
                                    }
                                    break;
                                case DocumentChangeType.Removed:
                                    if (conversationList.Where(c => c.id == obj.id).Any())
                                    {
                                        var item = conversationList.Where(c => c.id == obj.id).FirstOrDefault();
                                        conversationList.Remove(item);
                                    }
                                    break;
                            }
                            var conv = conversationListView.ItemsSource.Cast<object>().LastOrDefault();
                            conversationListView.ScrollTo(conv, ScrollToPosition.End, false);
                        }
                    }
                    emptyListLabel.IsVisible = conversationList.Count == 0;
                    conversationListView.IsVisible = !(conversationList.Count == 0);
                });
        }

        public async void SendMsg(object sender, EventArgs e)
        {
            Model data = ((Button)sender).BindingContext as Model;
            string ID = IDGenerator.generateID();
            ConversationModel conversation = new ConversationModel
            {
                id = ID,
                converseeID = dataClass.loggedInUser.uid,
                message = editor.Text,
                created_at = DateTime.UtcNow
            };

            await CrossCloudFirestore.Current
                                     .Instance
                                     .Collection("contacts")
                                     .Document(data.id)
                                     .Collection("conversations")
                                     .Document(ID)
                                     .SetAsync(conversation);

            editor.Text = string.Empty;
        }
    }
}