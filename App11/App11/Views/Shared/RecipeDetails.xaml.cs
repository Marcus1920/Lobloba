using App11.Models.SellersModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App11.Views.Shared
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RecipeDetails : ContentPage
	{
        public RecipeDetails(Recipe product)
        {
            BindingContext = product ?? throw new ArgumentNullException();

            InitializeComponent();
        }
    }
}