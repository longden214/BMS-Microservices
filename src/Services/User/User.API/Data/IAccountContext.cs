using MongoDB.Driver;
using User.API.Entities;

namespace User.API.Data
{
    public interface IAccountContext
    {
        IMongoCollection<Account> Accounts { get; }
    }
}
