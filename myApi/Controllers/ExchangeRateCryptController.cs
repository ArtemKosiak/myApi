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

        public ExchangeRateCryptController(ILogger<ExchangeRateCryptController> logger, ExchangeClientCrypt exchangeClientCrypt, IDynamoDbClient dynamoDbClient)
        {
            _logger = logger;
            _exchangeClientCrypt = exchangeClientCrypt;
        }
        [HttpGet("/ExchangeRateCrypt/{coins}/{burse}")]
        public async Task<CryptExchangeBurse> GetExchangeCrypt([FromRoute] string coins, string burse)
        {
            var exchange = await _exchangeClientCrypt.GetExchangeCryptByBurse(coins, burse);
            if (exchange == null)
            {
                return null;
            }
            //var result = new market
            //{
            //    name = exchange.tickers.FirstOrDefault().market.name
            //};

            return exchange;
        }
       
    }
}
