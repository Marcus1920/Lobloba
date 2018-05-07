using App11.Models.ResercherModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App11.Views.Researchers.ResearchApi
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ResearchDetails : ContentPage
	{
        public ResearchDetails(Researcher research)
        {
            BindingContext = research ?? throw new ArgumentNullException();

            InitializeComponent();
        }

        private async Task Button_OnClicked(object sender, EventArgs e)
        {
            var respone = await DisplayActionSheet("ACTIONS", "Cancel", null, "Edit", "Save", "Delete");

            if (!respone.Equals("Cancel"))
            {
                if (respone.Equals("Delete"))
                {
                    var delete = await DisplayAlert(respone + "?", "Are you want to remove this post?", "Yes", "No");
                    if (delete)
                        await DisplayAlert(null, respone + " functionality coming soon!", "Ok");
                }

                else
                    await DisplayAlert(null, respone + " functionality coming soon!", "Ok");
            }
        }
    }

}
