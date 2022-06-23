using myApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myApi.Clients
{
    public interface IDynamoDbClient
    {
        public Task<CryptExchangeDb> GetDataFromDb(string ID);
        public Task PostDataToDb(CryptExchangeDb data);
        public Task Delete(string ID);

    }
}
