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

        public Account Get(string name) {
            return dbContext.Account.SingleOrDefault(a => a.Username == name);
        }

        public Account Create(Account account) {
            if (account == null) return account;
            dbContext.Account.Add(account);
            dbContext.SaveChanges();
            return account;
        }

        public Account Update(Account account) {
            Account a = dbContext.Account.SingleOrDefault(b => b.Account_ID == account.Account_ID);
            if (a == null) return null;
            a.Username = account.Username;
            a.isActive = account.isActive;
            a.Role_ID = account.AccountRole.Role_ID;

            dbContext.SaveChanges();

            return account;
        }

        public void Delete(Account account) {
            Account a = dbContext.Account.SingleOrDefault(b => b.Account_ID == account.Account_ID);

            a.isActive = false;

            dbContext.SaveChanges();
        }

        public List<AccountRole> GetAllAccountRoles()
        {
            return dbContext.AccountRole.ToList();
        }

        public AccountRole GetAcountRoles(string name)
        {
            return dbContext.AccountRole.SingleOrDefault(a => a.RoleName == name);
        }
        public List<AccountLaw> GetAllLawsThatBelongToThatAccount(string name)
        {
            Account acc = dbContext.Account.SingleOrDefault(a => a.Username == name);
            return acc.AccountRole.AccountLaw.ToList();
        }


        public Account GetById(int id)
        {
            return dbContext.Account.SingleOrDefault(a => a.Account_ID == id);
        }


        public AccountRole GetAccountRoleById(int id)
        {
            return dbContext.AccountRole.SingleOrDefault(a => a.Role_ID == id);
        }
    }

}
