using Data.Contracts;
using Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ControlServices.GenericControlServices
{
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class, IEntity
    {
        private readonly IGenericRepo<TEntity> genericRepo;

        public GenericService(IGenericRepo<TEntity> genericRepo)
        {
            this.genericRepo = genericRepo;
        }

        public virtual async Task<TEntity> GetById(long Id, CancellationToken cancellationToken)
        {
            return await genericRepo.GetByIdAsync(cancellationToken, Id);
        }
        
        public virtual async Task<List<TEntity>> GetAll(CancellationToken cancellationToken)
        {
            return await genericRepo.TableNoTracking.ToListAsync(cancellationToken);
        }
        public virtual async Task<TEntity> Create(TEntity entity, CancellationToken cancellationToken)
        {
           return await genericRepo.AddAsync(entity, cancellationToken);
        }
        public virtual async Task<bool> Delete(long Id, CancellationToken cancellationToken)
        {
            await genericRepo.DeleteAsync(Id, cancellationToken);
            return true;
        }
        public virtual async Task<bool> Delete(TEntity entity, CancellationToken cancellationToken)
        {
            await genericRepo.DeleteAsync(entity, cancellationToken);
            return true;
        }
        public virtual async Task Update(TEntity entity, CancellationToken cancellationToken)
        {
            await genericRepo.UpdateAsync(entity, cancellationToken);
        }
    }
}
