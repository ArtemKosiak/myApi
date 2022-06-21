using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myApi.Constans;
using myApi.Model;
using myApi.Extensions;

namespace myApi.Clients
{
    public class DynamoDbClient : IDynamoDbClient
    {
        public string _tableName;
        private readonly IAmazonDynamoDB _dynamoDb;
        public DynamoDbClient(IAmazonDynamoDB dynamoDb)
        {
            _dynamoDb = dynamoDb;
            _tableName = Constants.TableName;
        }
        public async Task Delete(long ID)
        {
            var item = new DeleteItemRequest
            {
                TableName = _tableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    {"ID", new AttributeValue{N = ID.ToString()}}
                }
            };
            var response = await _dynamoDb.DeleteItemAsync(item);           
        }

        public async Task<CryptExchangeDb> GetDataFromDb(long ID)
        {
            var item = new GetItemRequest
            {
                TableName = _tableName,
                Key = new Dictionary<string,AttributeValue>
                {
                    {"ID", new AttributeValue{N = ID.ToString()}}
                }
            };
            var response = await _dynamoDb.GetItemAsync(item);
            if(response.Item == null || !response.IsItemSet)
            {
                return null;
            }
            var result = response.Item.ToClass<CryptExchangeDb>();
            return result;
        }

        public async Task PostDataToDb(CryptExchangeDb data)
        {
            var request = new PutItemRequest
            {
                TableName = _tableName,
                Item = new Dictionary<string, AttributeValue>
                {
                    {"ID", new AttributeValue{N = data.ID.ToString()}},
                     {"Exchange", new AttributeValue{S=data.Exchange} }
                }
            };
            var responce = await _dynamoDb.PutItemAsync(request);
        }
    }
}
