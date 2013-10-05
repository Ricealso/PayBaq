using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.UserData;
using Windows.Phone.PersonalInformation;

namespace PayBaq
{
    public partial class MainPage : PhoneApplicationPage
    {
        ContactStore contacts;
        ListBox contactList;
        public MainPage()
        {
            InitializeComponent();
            CreateContactStore();
        }
        private async void CreateContactStore()
        {
            contacts = await ContactStore.CreateOrOpenAsync(ContactStoreSystemAccessMode.ReadWrite, ContactStoreApplicationAccessMode.ReadOnly);
            if (contacts.RevisionNumber <1)
            {
                Contacts cons = new Contacts();
                cons.SearchCompleted += new EventHandler<ContactsSearchEventArgs>(SearchCompleted);
                cons.SearchAsync(String.Empty, FilterKind.None, "Searching");
            }
        }
        async void SearchCompleted(object sender, ContactsSearchEventArgs e)
        {
            foreach (Contact c in e.Results)
            {

                StoredContact a = new StoredContact(contacts);
                var props = await a.GetPropertiesAsync();
                var extprops =await a.GetExtendedPropertiesAsync();
                props.Add(KnownContactProperties.DisplayName, c.DisplayName);
                props.Add(KnownContactProperties.Telephone, c.PhoneNumbers);
                extprops.Add("MoneyOwed", 0);
                extprops.Add("MoneyLent", 0);
                extprops.Add("DateAdded", 0);
                extprops.Add("Group", 0);
                await a.SaveAsync();
            }

            contactList.SelectionChanged += ListBox_SelectionChanged;
        }
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }

}