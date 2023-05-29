using Data.Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UserNewRepo : GenericRepo<User>,IUserRepo
    {
        public UserNewRepo(AppDBContext appDBContext)
            : base(appDBContext)
        {
        }

        public Task<bool> ExistBeforByUserName(string UserName)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByUsernameAndPassword(string username, string password, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void GetNewUser()
        {

        }

        public Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSecuirtyStampAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
