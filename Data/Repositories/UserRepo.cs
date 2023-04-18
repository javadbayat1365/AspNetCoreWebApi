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

        public async Task<User?> GetByUsernameAndPassword(string username, int age, CancellationToken cancellationToken)
        {
           var selByContext = await _dbContext.Users.Where(w =>
                             w.FullName == username
                             && w.Age == age).FirstOrDefaultAsync(cancellationToken);

            //OR

            var selByEntities= await Entities.Where(w =>
                             w.FullName == username
                             && w.Age == age).FirstOrDefaultAsync(cancellationToken);

            //OR
            var selByTable = await TableNoTracking
                  .Where(w =>
                             w.FullName == username
                             && w.Age == age)
                  .FirstOrDefaultAsync(cancellationToken);

            return selByTable;
        }
    }
}
