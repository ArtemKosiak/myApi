using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using myApi.Constans;
using myApi.Model;
using Newtonsoft.Json;

namespace myApi.Clients
{
    public class ExchangeClient
    {
        HttpClient _client;
        private string _address;
        public ExchangeClient()
        {
            _address = Constants.adress;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_address);
        }
        public async Task<List<ExchangeDate>> GetExchangeByDate(string Date, string valcode) 
        {
            var responce = await _client.GetAsync($"NBUStatService/v1/statdirectory/exchange?valcode={valcode}&date={Date}&json");
            responce.EnsureSuccessStatusCode();
            var content = responce.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<List<ExchangeDate>>(content);
            return result;
        }

    }
    
}
