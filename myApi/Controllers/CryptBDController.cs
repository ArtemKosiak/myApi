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
        public class CryptBDController : ControllerBase
        {
            private readonly ILogger<CryptBDController> _logger;
            private readonly IDynamoDbClient _dynamoDbClient;

            public CryptBDController(ILogger<CryptBDController> logger, IDynamoDbClient dynamoDbClient)
            {
                _logger = logger;
                _dynamoDbClient = dynamoDbClient;
            }
            [HttpGet("/CryptBD{ID}")]
            public async Task<favmarket> GetFavoriteExchange([FromRoute] string ID)
            {
                var result = await _dynamoDbClient.GetDataFromDb(ID);
                if (result == null)
                {
                    return null;
                }
                var responce = new favmarket
                {
                    Exchange = result.Exchange,
                };
                return responce;
            }
            [HttpPost("/CryptBD")]
            public async Task<IActionResult> AddFavorites([FromBody] CryptExchangeDb cryptExchangeDb)
            {
                await _dynamoDbClient.PostDataToDb(cryptExchangeDb);
                return Ok();
            }
            [HttpDelete("/CryptBD{User_id}")]
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

