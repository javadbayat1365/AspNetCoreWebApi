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
        private readonly IGenericRepo<User> genericRepo;

        public UserServices(IGenericRepo<User> genericRepo) : base(genericRepo)
        {
            this.genericRepo = genericRepo;
        }
        public override async Task Update(User user, CancellationToken cancellationToken)
        {
            var sel = await genericRepo.GetByIdAsync(cancellationToken, user.Id);
            sel.Age = user.Age;
            sel.FullName = user.FullName;
            base.Update(user, cancellationToken);
        }
    }
}
