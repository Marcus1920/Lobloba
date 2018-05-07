using App11.Models.ResercherModel;
using App11.Services.ResearchersServices;
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
	public partial class AddNewResearch : ContentPage
	{
        private readonly ResearchPostsService _service = new ResearchPostsService();

        public AddNewResearch()
        {
            InitializeComponent();
        }

        private async void Save_OnClicked(object sender, EventArgs e)
        {

            if (String.IsNullOrWhiteSpace(NatureOfBusiness.Text) ||
                String.IsNullOrWhiteSpace(SummaryBox.Text) ||
                String.IsNullOrWhiteSpace(ResearchNotes.Text))
                await DisplayAlert(null, "Please complete all the fields.", "Ok");

            else
            {
                var research = new Researcher
                {
                    imageUrl = null,
                    natureOfBusiness = NatureOfBusiness.Text,
                    summaryBox = SummaryBox.Text,
                    researchNotes = ResearchNotes.Text

                };


                var response = await _service.AddNewResearch(research);
                if (response)
                {
                    await DisplayAlert("Success", "New post created.", "Ok");
                    await Navigation.PopAsync();
                }

                else
                {
                    await DisplayAlert("Failed", "Failed to create post.", "Ok");
                }
            }
        }

        private void Upload_OnClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

    }
}
