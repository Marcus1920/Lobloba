using App11.Models.SellersModel;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App11.Services.SellersServices
{
    public class UserProfileService
    {
        private string _url;
        private readonly HttpClient _client = new HttpClient();
        private string ApiKey = (Application.Current as App).UserTokenKey;


        public async Task<Profile> GetUser()
        {
            _url = "http://system.foodforus.cloud/api/v1/myProfile?api_key=" + ApiKey;

            var json = await _client.GetStringAsync(_url);

            var user = JsonConvert.DeserializeObject<Profile>(json);

            return user;
        }
    }
}
