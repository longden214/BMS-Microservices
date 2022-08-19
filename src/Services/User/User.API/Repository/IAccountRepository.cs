using System.Collections.Generic;
using System.Threading.Tasks;
using User.API.Entities;

namespace User.API.Repository
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAccounts();
        Task<Account> GetAccount(string id);
        Task<IEnumerable<Account>> GetAccountsByName(string name);
        Task CreateAccount(Account account);
        Task<bool> UpdateAccount(Account account);
        Task<bool> DeleteAccount(string id);
    }
}
