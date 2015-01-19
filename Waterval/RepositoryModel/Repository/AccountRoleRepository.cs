using DomainModel.Models;
using RepositoryModel.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.Repository {

    public class AccountRoleRepository : IAccountRoleRepository {

        private Project_WatervalEntities dbContext;
        List<AccountLaw> laws;


        public AccountRoleRepository() {
            dbContext = new Project_WatervalEntities();
            laws = new List<AccountLaw>();
        }

        public List<AccountRole> GetAll() {
            return dbContext.AccountRole.ToList();
        }

        public AccountRole Get(int accountRoleId) {
            return dbContext.AccountRole.Find(accountRoleId);
        }

        public AccountRole Create(AccountRole accountRole) {
            if (accountRole == null) return accountRole;

            addLinks(accountRole);
            accountRole.AccountLaw = laws;

            dbContext.AccountRole.Add(accountRole);
            dbContext.SaveChanges();
            return accountRole;
        }


        private void addLinks(AccountRole role)
        {
            laws.Clear();

            for (int index = 0; index < role.AccountLaw.Count; index++)
            {
                laws.Add(dbContext.AccountLaw.Find(role.AccountLaw.ElementAt(index).Law_ID));
            }
        }


        public DomainModel.Models.AccountRole Update(DomainModel.Models.AccountRole update)
        {

            AccountRole accountRole = dbContext.AccountRole.SingleOrDefault(b => b.Role_ID == update.Role_ID);
            if (accountRole == null) return null;

            accountRole.RoleName = update.RoleName;
            accountRole.Description = update.Description;

            addLinks(accountRole);
            accountRole.AccountLaw.Clear();
            accountRole.AccountLaw = laws;

            dbContext.SaveChanges();
            return accountRole;
        }

        public void Delete(int id) {
			AccountRole accrole = dbContext.AccountRole.FirstOrDefault( b => b.Role_ID == id );
			dbContext.AccountRole.Remove( accrole );
            dbContext.SaveChanges();
        }
    }

}
