using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myApi.Constans;
using myApi.Model;
using Newtonsoft.Json;
using System.Net.Http;
namespace myApi.Clients
{
    public class ExchangeClientCrypt
    {
        HttpClient _client;
        private static string _address;
        public ExchangeClientCrypt()
        {
            _address = Constants.adress1;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_address);
        }
        public async Task<CryptExchangeBurse> GetExchangeCryptByBurse(string coins,string burse)
        {
            var responce = await _client.GetAsync($"/api/v3/coins/{coins}/tickers?exchange_ids={burse}");
            responce.EnsureSuccessStatusCode();
            var content = responce.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<CryptExchangeBurse>(content);
            return result;
        }     
    }
}
