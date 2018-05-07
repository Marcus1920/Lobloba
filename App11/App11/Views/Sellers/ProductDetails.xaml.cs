using App11.Models.SellersModel;
using App11.Services.SellersServices;
using App11.ViewModels.Logic;
using Plugin.Media.Abstractions;
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
	public partial class ProductDetails : ContentPage
	{
        private readonly Product _product;
        private readonly SellersPostsService _service = new SellersPostsService();
        private readonly AppFunctions _appFunctions = new AppFunctions();
        private MediaFile _mediafile;

        public ProductDetails(Product product)
        {
            _product = product;
            BindingContext = _product ?? throw new ArgumentNullException();

            InitializeComponent();
        }

        private async Task Button_OnClicked(object sender, EventArgs e)
        {
            var respone = await DisplayActionSheet("ACTIONS", "Cancel", null/*, "Edit"*/, "Delete");

            if (!respone.Equals("Cancel"))
            {
                if (respone.Equals("Delete"))
                {
                    var delete = await DisplayAlert(respone + "?", "Are you want to remove this post?", "Yes", "No");
                    if (delete)
                    {
                        var response = await _service.DeletePost(_product);
                        if (response)
                        {
                            await DisplayAlert("Success",
                                "Post deleted successfully", "Ok");
                            await Navigation.PopAsync();
                        }

                        else
                        {
                            await DisplayAlert("Failed",
                                "Failed to delete post. The item might be involved in other transactions.",
                                "Ok");
                        }
                    }
                }

                else
                {
                    var page = new EditProduct(_product);
                    page.TakePicture.Clicked += async (source, args) =>
                    {
                        _mediafile = await _appFunctions.TakePicture();

                        if (_mediafile == null)
                            await DisplayAlert("Error!", "Could not take picture.", "OK");

                        else
                            page.FeatureImage.Source = _mediafile.Path;
                    };

                    page.BrowsePicture.Clicked += async (source, args) =>
                    {
                        _mediafile = await _appFunctions.BrowsePicture();

                        if (_mediafile == null)
                            await DisplayAlert("Error!", "Could not get picture.", "OK");

                        else
                            page.FeatureImage.Source = _mediafile.Path;
                    };

                    page.GetProductTypes.Clicked += async (source, args) =>
                    {
                        var typesPage = new ProductTypes();
                        typesPage.ProductTypesListView.ItemSelected += async (source2, args2) =>
                        {
                            var type = args2.SelectedItem as ProductType;
                            if (type != null) page.PType.Text = type.Name.ToString();
                            await Navigation.PopAsync();
                        };

                        await Navigation.PushAsync(typesPage);
                    };

                    page.GetPackagings.Clicked += async (source, args) =>
                    {
                        var typesPage = new PackagingTypes();
                        typesPage.PackagingTypesListView.ItemSelected += async (source2, args2) =>
                        {
                            var type = args2.SelectedItem as PackagingType;
                            if (type != null) page.PPackaging.Text = type.Name.ToString();
                            await Navigation.PopAsync();
                        };

                        await Navigation.PushAsync(typesPage);
                    };

                    page.SaveChanges.Clicked += async (source, args) =>
                    {
                        if (String.IsNullOrWhiteSpace(page.PDescription.Text) ||
                            String.IsNullOrWhiteSpace(page.PCountry.Text) ||
                            String.IsNullOrWhiteSpace(page.PCity.Text) ||
                            Convert.ToDouble(page.PQuantity.Text) <= 0 ||
                            _mediafile == null)
                            await DisplayAlert(null, "Please complete all the fields and set a new product picture.", "Ok");

                        else
                        {
                            _product.ProductType = page.PType.Text;
                            _product.TransactionRating = page.PRating.Value;
                            _product.Description = page.PDescription.Text;
                            _product.Country = page.PCountry.Text;
                            _product.City = page.PCity.Text;
                            _product.Quantity = Convert.ToDouble(page.PQuantity.Text);
                            _product.CostPerKg = Convert.ToDouble(page.PPrice.Text);
                            _product.Packaging = page.PPackaging.Text;
                            _product.ProductPicture = _mediafile.Path;
                            _product.File = _mediafile;


                            var response = await _service.AddNewPost(_product);
                            if (response)
                            {
                                await DisplayAlert("Success",
                                    "Post edited successfully", "Ok");
                                await Navigation.PopAsync();
                            }

                            else
                            {
                                await DisplayAlert("Failed", "Failed to edit post.", "Ok");
                            }

                            await Navigation.PopAsync();
                        }
                    };

                    await Navigation.PushAsync(page);
                }
            }
        }
    }
}