namespace appContacto.ViewModels
{
    using appContacto.Sevices;
    using appContacto.Models;
    using System.Collections.ObjectModel;
    using System;
    using Xamarin.Forms;
    using System.Collections.Generic;

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
                "http://localhost:50048/",
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


        }
        #endregion




    }
}
