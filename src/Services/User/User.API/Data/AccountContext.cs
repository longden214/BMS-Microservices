using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using User.API.Entities;

namespace User.API.Data
{
    public class AccountContext : IAccountContext
    {
        public AccountContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Accounts = database.GetCollection<Account>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            AccountContextSeed.SeedData(Accounts);
        }

        public IMongoCollection<Account> Accounts { get; }
    }
}
