using App11.Models.ResercherModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App11.Services.ResearchersServices
{
    public class ResearchPostsService
    {


        private string _url;
        private readonly HttpClient _client = new HttpClient();
        private HttpResponseMessage _response = new HttpResponseMessage();
        private readonly string apiKey = (Application.Current as App).UserTokenKey;

        public async Task<List<Researcher>> GetAllResearch()
        {
            _url = " http://system.foodforus.cloud/api/v1/allResearchs";

            var json = await _client.GetStringAsync(_url);
            var researchList = JsonConvert.DeserializeObject<List<Researcher>>(json);

            foreach (var post in researchList)
            {
                if (post.imageUrl == null)
                    post.imageUrl = "image_not_found.jpg";
            }

            return researchList;
        }

        public async Task<List<Models.ResercherModel.Researcher>> GetById()
        {
            _url = " http://154.0.164.72:8080/Foods/api/v1/myResearchs?api_key=" + apiKey;

            var json = await _client.GetStringAsync(_url);
            var researchList = JsonConvert.DeserializeObject<List<Researcher>>(json);

            foreach (var post in researchList)
            {
                if (post.imageUrl == null)
                    post.imageUrl = "image_not_found.jpg";
            }

            return researchList;
        }

        public async Task<bool> AddNewResearch(Researcher research)
        {
            _url = "http://154.0.164.72:8080/Foods/api/v1/createResearch?api_key=" + apiKey;

            var form = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("imageUrl", research.imageUrl),
                new KeyValuePair<string, string>("natureOfBusiness", research.natureOfBusiness),
                 new KeyValuePair<string, string>("summaryBox", research.summaryBox),
                  new KeyValuePair<string, string>("researchNotes", research.researchNotes),

            });

            _response = await _client.PostAsync(_url, form);

            return _response.IsSuccessStatusCode;
        }

        //public async Task<bool> EditResearch(int id, Research research)
        //{
        //    _url = "";C:\Users\Marcus -TM\Documents\Visual Studio 2017\Food  For  us (3)\Food  For  us\Food  For  us\Food__For__us\Views\Buyers\BuyersHomeMaster.xaml

        //    var form = new FormUrlEncodedContent(new[]
        //    {
        //        new KeyValuePair<string, string>("imageUrl", research.imageUrl),
        //        new KeyValuePair<string, string>("natureOfBusiness", research.natureOfBusiness),
        //        new KeyValuePair<string, string>("summaryBox", research.summaryBox),
        //        new KeyValuePair<string, string>("researchNotes", research.researchNotes),
        //    });

        //    _response = await _client.PostAsync(_url, form);

        //    return _response.IsSuccessStatusCode;
        //}

        //public async Task<bool> DeletePost(int id, Research research)
        //{
        //    _url = "";

        //    _response = await _client.DeleteAsync(_url + id);

        //    return _response.IsSuccessStatusCode;
        //}

    }
}
