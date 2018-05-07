using App11.Models.ResercherModel;
using App11.Services.ResearchersServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App11.Views.Researchers.ResearchApi
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ResearchFeeds : ContentPage
	{
        public string image { get; set; }
        public string nature { get; set; }
        public string summary { get; set; }
        public string notes { get; set; }
        private ObservableCollection<Researcher> _research;
        private readonly ResearchPostsService _service = new ResearchPostsService();
        public ResearchFeeds()
        {
            InitializeComponent();
            LoadData();
        }

        public async void LoadData()
        {
            var Items = await _service.GetAllResearch();
            ResearchListView.ItemsSource = Items;
        }


        private void ToolbarItem_OnActivated(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddNewResearch());
        }

        public async Task GetResearch()
        {
            try
            {
                notFound.IsVisible = true;

                var researchList = await _service.GetAllResearch();

                _research = new ObservableCollection<Researcher>(researchList);
                ResearchListView.ItemsSource = _research;

                ResearchListView.IsVisible = _research.Any();
                notFound.IsVisible = !ResearchListView.IsVisible;
            }
            catch (Exception)
            {
                await DisplayAlert("Error!", "Could not retrieve the list of posts.", "OK");
            }
        }



    }

}
