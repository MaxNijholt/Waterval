using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.IRepository {

    public interface IAccountRoleRepository {

        List<AccountRole> GetAll();
        AccountRole Get(int roleId);
        AccountRole Create(AccountRole role);
        AccountRole Update(AccountRole role);
        void Delete(AccountRole role);

    }

}
