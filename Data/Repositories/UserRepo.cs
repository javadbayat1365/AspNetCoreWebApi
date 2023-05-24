using Common.Utilities;
using Data.Contracts;
using Entities.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UserRepo : GenericRepo<User>, IUserRepo
    {
        public UserRepo(AppDBContext appDBContext) 
            : base(appDBContext)
        {
        }

        public Task<bool> ExistBeforByUserName(string UserName)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetByUsernameAndPassword(string username, string password, CancellationToken cancellationToken)
        {
            var PasswordHash = SecurityHelper.GetSha256Hash(password);
           //var selByContext = await _dbContext.Users.Where(w =>
           //                  w.UserName == username
           //                  && w.PasswordHash == PasswordHash).FirstOrDefaultAsync(cancellationToken);

            //OR

            var selByEntities= await Entities.Where(w =>
                             w.UserName == username
                             && w.PasswordHash == PasswordHash).FirstOrDefaultAsync(cancellationToken);

            //OR
            //var selByTable = await TableNoTracking
            //      .Where(w =>
            //                 w.UserName == username
            //                 && w.PasswordHash == PasswordHash)
            //      .FirstOrDefaultAsync(cancellationToken);

            return selByEntities;
        }

        public Task UpdateSecuirtyStampAsync(User user, CancellationToken cancellationToken)
        {
            user.SecurityStamp = Guid.NewGuid();
            return UpdateAsync(user, cancellationToken);
        }
        public Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken)
        {
            user.LastLoginDate = DateTimeOffset.Now;
            return UpdateAsync(user, cancellationToken);
        }
    }
}
