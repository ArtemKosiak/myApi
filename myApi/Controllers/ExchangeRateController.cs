using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myApi.Clients;
using myApi.Model;
namespace myApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExchangeRateController : ControllerBase
    {     
       private readonly ILogger<ExchangeRateController> _logger;
        private readonly ExchangeClient _exchangeClient;
        public ExchangeRateController(ILogger<ExchangeRateController> logger, ExchangeClient exchangeClient)
        {
            _logger = logger;
            _exchangeClient = exchangeClient;
        }  
        [HttpGet("/ExchangeRate{Date}/{valcode}")]
        public async Task<List<ExchangeDate>> GetExchange([FromRoute] string Date, string valcode)
        {
            var exchange = await _exchangeClient.GetExchangeByDate(Date, valcode);
            if (exchange == null)
            {
                return null;
            }
            return exchange;
        }
    }
    
}
