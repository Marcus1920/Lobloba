using App11.Models.SellersModel;
using App11.Services.SellersServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App11.Views.Sellers
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditProduct : ContentPage
	{
        private Product _product;
        private readonly SellersPostsService _service = new SellersPostsService();

        public EditProduct(Product product)
        {
            _product = product;
            BindingContext = _product ?? throw new ArgumentNullException();

            InitializeComponent();
        }

        public Label PType => ProductType;
        public Button TakePicture => TakePic;
        public Button BrowsePicture => BrowsePic;
        public Image FeatureImage => Image;
        public Slider PRating => ProductRating;
        public EntryCell PDescription => ProductDescription;
        public EntryCell PCountry => ProductCountry;
        public EntryCell PCity => ProductCity;
        public Label PPackaging => PackagingType;
        public EntryCell PQuantity => ProductQuantity;
        public EntryCell PPrice => ProductPrice;
        public Button SaveChanges => Save;
        public Button GetProductTypes => GetProductType;
        public Button GetPackagings => GetPackaging;
    }
}