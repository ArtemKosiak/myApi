using myApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myApi.Clients
{
    public interface IDynamoDbClient
    {
        public Task<CryptExchangeDb> GetDataFromDb(long ID);
        public Task PostDataToDb(CryptExchangeDb data);
        public Task Delete(long ID);
    }
}
