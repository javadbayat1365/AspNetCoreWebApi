using Common.Exceptions;
using Common.Utilities;
using Data.Contracts;
using Entities.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ControlServices
{
    public class UserServices : GenericControlServices.GenericService<User>, IUserServices
    {
        private readonly IUserRepo UserRepo;

        public UserServices(IUserRepo userRepo) : base(userRepo)
        {
            this.UserRepo = userRepo;
        }

        public async Task<User> AddUserAsync(User user,CancellationToken cancellationToken)
        {
            if(ExistBeforByUserName(user.FullName).Result)
            {
                throw new AppException("نام کاربری تکراری است", Common.Enums.ApiResultStatusCode.AppError);
            }
           return base.Create(user, cancellationToken).Result;

            return null;
        }

        public override async Task Update(User user, CancellationToken cancellationToken)
        {
            var sel = await UserRepo.GetByIdAsync(cancellationToken, user.Id);
            sel.Age = user.Age;
            sel.FullName = user.FullName;
            base.Update(user, cancellationToken);
        }

        public async Task<bool> ExistBeforByUserName(string UserName)
        {
          return await  UserRepo.ExistBeforByUserName(UserName);
        }
    }
}
