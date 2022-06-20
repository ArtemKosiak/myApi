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

namespace myApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExchangeRateCryptController : ControllerBase
    {       
        private readonly ILogger<ExchangeRateCryptController> _logger;
        private readonly ExchangeClientCrypt _exchangeClientCrypt;
        private readonly IDynamoDbClient _dynamoDbClient;
        public ExchangeRateCryptController(ILogger<ExchangeRateCryptController> logger, ExchangeClientCrypt exchangeClientCrypt, IDynamoDbClient dynamoDbClient )
        {
            _logger = logger;
            _exchangeClientCrypt = exchangeClientCrypt;
            _dynamoDbClient = dynamoDbClient;
        }
        [HttpGet("/ExchangeRateCrypt/{coins}/{burse}")]
        public async Task<CryptExchangeBurse> GetExchangeCrypt([FromRoute] string coins, string burse)
        {
            var exchange = await _exchangeClientCrypt.GetExchangeCryptByBurse(coins,burse);
            var result = new market {
                name = exchange.tickers.FirstOrDefault().market.name
            };

            return exchange;
        }
        [HttpGet]
        public async Task<market> GetFavoriteExchange([FromQuery] long ID)
        {
            var result = await _dynamoDbClient.GetDataFromDb(ID);
            if (result == null)
            {
                return null;
            }
            var responce = new market
            {
                name = result.Exchange
            };

            return responce;
        }
    }
}
