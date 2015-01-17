using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryModel.IRepository {

    public interface IAccountLawRepository {

        List<AccountLaw> GetAll();
        AccountLaw Get(int lawId);

    }

}
