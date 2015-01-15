using DomainModel.Models;
using RepositoryModel.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.Repository {

    public class AccountRepository : IAccountRepository {

        private Project_WatervalEntities dbContext;

        public AccountRepository() {
            dbContext = new Project_WatervalEntities();
        }

        public List<Account> GetAll() {
            return dbContext.Account.ToList();
        }

        public Account Get(int accountId) {
            return dbContext.Account.Find(accountId);
        }

        public Account Create(Account account) {
            if (account == null) return account;
            dbContext.Account.Add(account);
            dbContext.SaveChanges();
            return account;
        }

        public Account Update(Account account) {
            // Add to database
            throw new NotImplementedException();
        }

        public void Delete(Account account) {
            // Add to database
            throw new NotImplementedException();
        }
    }

}
