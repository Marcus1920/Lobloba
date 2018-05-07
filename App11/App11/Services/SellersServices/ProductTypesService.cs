using App11.Models.SellersModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace App11.Services.SellersServices
{
    public class ProductsService
    {
        private string _url;
        private readonly HttpClient _client = new HttpClient();


        public async Task<List<ProductType>> GetProductTypes(string query = null)
        {
            _url = "http://system.foodforus.cloud/api/v1/producttype";


            var json = await _client.GetStringAsync(_url);

            var types = JsonConvert.DeserializeObject<List<ProductType>>(json);


            if (!String.IsNullOrWhiteSpace(query))
                types = types.Where(p => p.Name.ToUpper().Contains(query.ToUpper())).ToList();

            return types;
        }

        public async Task<List<PackagingType>> GetPackagingTypes()
        {
            _url = "http://system.foodforus.cloud/api/v1/packagingList";

            var json = await _client.GetStringAsync(_url);
            var types = JsonConvert.DeserializeObject<List<PackagingType>>(json);

            return types;
        }
    }
}
