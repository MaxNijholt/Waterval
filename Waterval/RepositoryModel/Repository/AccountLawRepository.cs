using DomainModel.Models;
using RepositoryModel.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.Repository {

    public class AccountLawRepository : IAccountLawRepository {

        private Project_WatervalEntities dbContext;

        public AccountLawRepository() {
            dbContext = new Project_WatervalEntities();
        }

        public List<AccountLaw> GetAll() {
            return dbContext.AccountLaw.ToList();
        }

        public AccountLaw Get(int accountLawId) {
            return dbContext.AccountLaw.Find(accountLawId);
        }


    }

}
