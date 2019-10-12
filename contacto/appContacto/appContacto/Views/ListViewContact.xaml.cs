using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace appContacto.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewContact : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }

        public ListViewContact()
        {
            InitializeComponent();

        }

    }
}
