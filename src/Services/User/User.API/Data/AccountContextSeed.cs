using MongoDB.Driver;
using System.Collections.Generic;
using User.API.Entities;

namespace User.API.Data
{
    public class AccountContextSeed
    {

        public static void SeedData(IMongoCollection<Account> accounts)
        {
            bool existAccount = accounts.Find(a => true).Any();

            if (!existAccount)
            {
                accounts.InsertManyAsync(GetPreconfiguredAccounts());
            }
        }

        public static IEnumerable<Account> GetPreconfiguredAccounts()
        {
            return new List<Account>() {
                new Account()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    UserName = "longden",
                    FirstName = "Quang",
                    LastName = "Long",
                    Birthday = new System.DateTime(2021,04,21),
                    Address = "Nam Dinh",
                    Avatar = "avatar.jppg",
                    Email = "name@gmail.com",
                    Password = "12345678",
                    Phone = "0927328743",
                    Department = "Division"
                },
                new Account()
                {
                    Id = "602d2149e773f2a3990b47f6",
                    UserName = "datte",
                    FirstName = "Quang",
                    LastName = "Dat",
                    Birthday = new System.DateTime(2004,01,01),
                    Address = "Nam Dinh",
                    Avatar = "avatar.jppg",
                    Email = "name@gmail.com",
                    Password = "12345678",
                    Phone = "0927328743",
                    Department = "Marketing"
                }
            };
        }
    }
}
