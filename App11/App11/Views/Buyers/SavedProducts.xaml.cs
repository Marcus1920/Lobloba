using App11.Models.BuyersModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App11.Views.Buyers
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SavedProducts : ContentPage
	{
        private ObservableCollection<Product> productsList;
        Data data = new Data();

        public SavedProducts()
        {
            InitializeComponent();
            //BindingContext = new ContentPageViewModel();
        }


        private void ToolbarItem_OnActivatedChat(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChatList());
        }

        private void ToolbarItem_OnActivated(object sender, EventArgs e)
        {
            Navigation.PushAsync(new UserProductCart());
        }

        protected override async void OnAppearing()
        {
            var allProducts = await data.GetProducts();
            productsList = new ObservableCollection<Product>(allProducts);
            products.ItemsSource = productsList;
            base.OnAppearing();
        }
    }

    class SavedProductsViewModel : INotifyPropertyChanged
    {

        public SavedProductsViewModel()
        {
            IncreaseCountCommand = new Command(IncreaseCount);
        }

        int count;

        string countDisplay = "You clicked 0 times.";
        public string CountDisplay
        {
            get { return countDisplay; }
            set { countDisplay = value; OnPropertyChanged(); }
        }

        public ICommand IncreaseCountCommand { get; }

        void IncreaseCount() =>
            CountDisplay = $"You clicked {++count} times";


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}