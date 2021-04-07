using MapForms.Driver.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MapForms.Driver.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}