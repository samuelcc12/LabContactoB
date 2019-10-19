namespace appContacto.ViewModels
{
    using appContacto.Sevices;
    using appContacto.Models;
    using System.Collections.ObjectModel;
    using System;
    using Xamarin.Forms;
    using System.Collections.Generic;
    using System.Linq;

    public class ContactViewModel:BaseViewModel
    {
        #region Attributes
        ApiService apiService;
        private ObservableCollection<Contact> Contacts
        {
            get;
            set;

        }
        #endregion
        #region Constructor
        public ContactViewModel()
        {
            this.apiService = new ApiService();
                this.LoadContacts();
        }
        #endregion
        #region Methods
        private async void LoadContacts()
        {
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                "Connection Error",
                connection.Message,
                "Accept" 
                );
                return;

            }
            var response = await this.apiService.GetList<Contact>(
                "http://localhost:49867/",
                "api/",
                "Contacts");
            if(!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                "GET Contact Error",
                connection.Message,
                "Accept"
                );
                return;


            }
            MainViewModel mainviewmodel = MainViewModel.GetInstance();
            mainviewmodel.ContactList = (List<Contact>)response.Result;
            this.Contacts = new ObservableCollection<Contact>(this.ToContactView());
            
        }

        private IEnumerable<Contact> ToContactView()
        {
            ObservableCollection<Contact> collection = new ObservableCollection<Contact>();
            MainViewModel main = MainViewModel.GetInstance();
            foreach (var lista in main.ContactList)
            {
                Contact contacto = new Contact();
                contacto.ContactID = lista.ContactID;
                contacto.Name = lista.Name;
                contacto.Type = lista.Type;
                contacto.ContactValue = lista.ContactValue;
            }
            return collection;
        }
        #endregion




    }
}
