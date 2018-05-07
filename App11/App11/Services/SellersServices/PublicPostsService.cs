using App11.Models.SellersModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App11.Services.SellersServices
{
    public class PublicPostsService
    {
        private string _url;
        private readonly HttpClient _client = new HttpClient();
        private HttpResponseMessage _response = new HttpResponseMessage();
        private string ApiKey = (Application.Current as App).UserTokenKey;


        public async Task<List<Recipe>> GetAllPosts()
        {
            _url = "http://system.foodforus.cloud/api/v1/getRecipes";

            var json = await _client.GetStringAsync(_url);

            var poststList = JsonConvert.DeserializeObject<List<Recipe>>(json);

            foreach (var post in poststList)
            {
                if (post.Imgurl == null)
                    post.Imgurl = "recipe.jpg";
            }

            return poststList;
        }

        public async Task<bool> AddNewPost(Recipe recipe)
        {
            _url = "http://system.foodforus.cloud/api/v1/createRecipe";

            var form = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("name", recipe.Name),
                new KeyValuePair<string, string>("description", recipe.Description),
                new KeyValuePair<string, string>("ingredients", recipe.Ingredients),
                new KeyValuePair<string, string>("methods", recipe.Methods)
            });

            _response = await _client.PostAsync(_url, form);

            return _response.IsSuccessStatusCode;
        }

        public async Task<bool> EditPost(int id, Recipe recipe)
        {
            _url = "http://system.foodforus.cloud/api/v1/all?api_key=123";

            var form = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("name", recipe.Name),
                new KeyValuePair<string, string>("description", recipe.Description),
                new KeyValuePair<string, string>("ingredients", recipe.Ingredients),
                new KeyValuePair<string, string>("methods", recipe.Methods)
            });

            _response = await _client.PostAsync(_url, form);

            return _response.IsSuccessStatusCode;
        }

        public async Task<bool> DeletePost(int id, Recipe recipe)
        {
            _url = "http://system.foodforus.cloud/api/v1/all?api_key=123";

            _response = await _client.DeleteAsync(_url + id);

            return _response.IsSuccessStatusCode;
        }
    }
}
