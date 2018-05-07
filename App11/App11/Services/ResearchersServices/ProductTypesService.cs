using App11.Models.ResercherModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App11.Services.ResearchersServices
{
    public class ProductTypesService
    {
        private string _url;
        private readonly HttpClient _client = new HttpClient();


        public async Task<List<ProductTypes>> GetProductTypes(string query = null)
        {
            _url = "http://system.foodforus.cloud/api/v1/producttype";

            var json = await _client.GetStringAsync(_url);
            var types = JsonConvert.DeserializeObject<List<ProductTypes>>(json);


            if (!String.IsNullOrWhiteSpace(query))
                types = types.Where(p => p.Name.ToUpper().Contains(query.ToUpper())).ToList();


            return types;
        }

    }
}
