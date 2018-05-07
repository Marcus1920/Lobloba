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
	public partial class DetailedResearch : ContentPage
	{
        public DetailedResearch(Researcher research)
        {

            BindingContext = research ?? throw new ArgumentNullException();
            InitializeComponent();
        }
    }
}