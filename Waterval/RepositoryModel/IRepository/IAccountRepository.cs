using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.IRepository {

    public interface IAccountRepository {

        List<Account>   GetAll();
        Account         Get(int accountId);
        Account         Create(Account account);
        Account         Update(Account account);
        void            Delete(Account account);

    }

}
