using App11;
using App11.Models.SellersModel;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App11.Models.SellersModel

{
    public class Profile
    {
        public int Id { get; set; }
        public string IdNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Cellphone { get; set; }
        public string Email { get; set; }
        public string Interest { get; set; }
        public string profilePicture { get; set; }
        public MediaFile File { get; set; }
        public string Location { get; set; }
        public string DescriptionOfAcces { get; set; }

    }
}

public class Updateprofile


{


    private string _url;
    private readonly HttpClient _client = new HttpClient();
    private HttpResponseMessage _response = new HttpResponseMessage();
    private readonly string _apiKey = (Application.Current as App).UserTokenKey;



    public async Task<string> Update(Profile profile)
    {
        _url = "http://system.foodforus.cloud/api/v1/updateProfile";

        var content = new MultipartFormDataContent();

        var values = new[]
        {
                new KeyValuePair<string, string>("api_key",_apiKey),
                new KeyValuePair<string, string>("email", profile.Cellphone),
                new KeyValuePair<string, string>("cellphone", profile.Email),

            };

        foreach (var keyValuePair in values)
        {
            content.Add(new StringContent(keyValuePair.Value), keyValuePair.Key);
        }

        if (profile.File != null)
            content.Add(new StreamContent(profile.File.GetStream()), "\"file\"", $"\"{profile.profilePicture}\"");

        _response = await _client.PostAsync(_url, content);

        return _response.ToString();
    }


}

