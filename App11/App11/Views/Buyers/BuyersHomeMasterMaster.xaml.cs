using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App11.Views.Buyers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BuyersHomeMasterMaster : ContentPage
    {
        public ListView ListView;

        public BuyersHomeMasterMaster()
        {
            InitializeComponent();

            BindingContext = new BuyersHomeMasterMasterViewModel();
            ListView = MenuItemsListView;
        }

        class BuyersHomeMasterMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<BuyersHomeMasterMenuItem> MenuItems { get; set; }
            
            public BuyersHomeMasterMasterViewModel()
            {
                MenuItems = new ObservableCollection<BuyersHomeMasterMenuItem>(new[]
                {
                    new BuyersHomeMasterMenuItem { Id = 0, Title = "Page 1" },
                    new BuyersHomeMasterMenuItem { Id = 1, Title = "Page 2" },
                    new BuyersHomeMasterMenuItem { Id = 2, Title = "Page 3" },
                    new BuyersHomeMasterMenuItem { Id = 3, Title = "Page 4" },
                    new BuyersHomeMasterMenuItem { Id = 4, Title = "Page 5" },
                });
            }
            
            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}