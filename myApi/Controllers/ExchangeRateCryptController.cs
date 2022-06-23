using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myApi.Clients;
using myApi.Model;
using myApi.Extensions;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2;
using Newtonsoft.Json;
using System.Net;
using Amazon.DynamoDBv2.Model.Internal.MarshallTransformations;

namespace myApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ExchangeRateCryptController : ControllerBase
    {
       
        private readonly ILogger<ExchangeRateCryptController> _logger;
        private readonly ExchangeClientCrypt _exchangeClientCrypt;
        private readonly IDynamoDbClient _dynamoDbClient;

        public ExchangeRateCryptController(ILogger<ExchangeRateCryptController> logger, ExchangeClientCrypt exchangeClientCrypt, IDynamoDbClient dynamoDbClient)
        {
            _logger = logger;
            _exchangeClientCrypt = exchangeClientCrypt;
            _dynamoDbClient = dynamoDbClient;
        }
        [HttpGet("/ExchangeRateCrypt/{coins}/{burse}")]
        public async Task<CryptExchangeBurse> GetExchangeCrypt([FromRoute] string coins, string burse)
        {
            var exchange = await _exchangeClientCrypt.GetExchangeCryptByBurse(coins, burse);
            var result = new market
            {
                name = exchange.tickers.FirstOrDefault().market.name
            };

            return exchange;
        }
        [HttpGet]
        public async Task<fmarket> GetFavoriteExchange([FromQuery] string ID)//IActionResult
        {
            var result = await _dynamoDbClient.GetDataFromDb(ID);
            if (result == null)
            {
                return null;
            }
            var responce = new fmarket
            {
                Exchange = result.Exchange,
            };
            // var Data = JsonConvert.DeserializeObject<List<string>>(responce.name);
            // var Data = JsonConvert.DeserializeObject<List<string>>(responce.name);
            return responce;
        }
        [HttpPost]
        public async Task<IActionResult> AddFavorites([FromBody] CryptExchangeDb cryptExchangeDb)
        {
            await _dynamoDbClient.PostDataToDb(cryptExchangeDb);
              
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(string User_id)
        {
            //var json = webClient.DownloadString($"https://localhost:44368/ExchangeRateCrypt?ID={User_id}");
            //var result = JsonConvert.DeserializeObject<CryptExchangeBurse>(json);
            //string newExchange;
            //if (del != null && del != "all" && result.name.Contains(del) == true)
            //{
            //    newExchange = result.name.Replace(del, null);
            //    string[] exchanges = newExchange.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            //    if (exchanges.Length == 0)
            //    {
            //        await _dynamoDbClient.Delete(User_id);
            //        return Ok();
            //    }
            //    string resultExchange = null;
            //    foreach (string s in exchanges)
            //    {
            //        resultExchange += " " + s;
            //    }
            //    resultExchange = resultExchange.Remove(0, 1);

            //    var data = new CryptExchangeDb
            //    {
            //        ID = User_id.ToString(),
            //        Exchange = resultExchange

            //    };
            //    await _dynamoDbClient.PostDataToDb(data);
            //}
            //else 
            //{
            //}
            await _dynamoDbClient.Delete(User_id);
            return Ok();
        }
    }
}
