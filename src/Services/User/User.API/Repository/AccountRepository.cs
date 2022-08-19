using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using User.API.Data;
using User.API.Entities;

namespace User.API.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IAccountContext _context;

        public AccountRepository(IAccountContext accountContext)
        {
            _context = accountContext ?? throw new ArgumentNullException(nameof(accountContext));
        }

        public async Task CreateAccount(Account account)
        {
            await _context.Accounts.InsertOneAsync(account);
        }

        public async Task<bool> DeleteAccount(string id)
        {
            FilterDefinition<Account> filter = Builders<Account>.Filter.Eq(a => a.Id, id);

            DeleteResult deleteResult = await _context.Accounts.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<Account> GetAccount(string id)
        {
            return await _context.Accounts.Find(a => a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Account>> GetAccounts()
        {
            return await _context.Accounts.Find(a => true).ToListAsync();
        }

        public async Task<IEnumerable<Account>> GetAccountsByName(string name)
        {
            FilterDefinition<Account> filter = Builders<Account>.Filter.Eq(a => a.UserName, name);

            return await _context.Accounts.Find(filter).ToListAsync();
        }

        public async Task<bool> UpdateAccount(Account account)
        {
            var updateResult = await _context
                .Accounts
                .ReplaceOneAsync(filter: a => a.Id == account.Id, replacement: account);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
