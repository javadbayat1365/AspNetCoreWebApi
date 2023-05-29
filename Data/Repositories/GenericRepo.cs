using Common.Utilities;
using Data.Contracts;
using Entities.Common;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class GenericRepo<TEntity> : IGenericRepo<TEntity> where TEntity : class, IEntity
    {
        protected AppDBContext _dbContext;
        protected internal DbSet<TEntity> Entities;
        public virtual IQueryable<TEntity> Table => Entities;
        //public virtual IQueryable<User> TableNoTracking => _dbContext.Users.AsNoTracking();
        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();
        public GenericRepo(AppDBContext appDBContext)
        {
            _dbContext = appDBContext;
            Entities = _dbContext.Set<TEntity>();
        }

        #region Async Method
        public virtual async Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] ids)
        {
            //_dbContext.Users.FindAsync(ids);

            return await Entities.FindAsync(ids, cancellationToken);
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
           var sel = await Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
            //_dbContext.AddAsync(entity, cancellationToken);
            if (saveNow)
                await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return sel.Entity;
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            await Entities.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
            if (saveNow)
                await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Update(entity);
            //_dbContext.Update(entity);
            if (saveNow)
                await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.UpdateRange(entities);
            if (saveNow)
                await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Remove(entity);
            //_dbContext.Remove(entity);
            if (saveNow)
                await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task DeleteAsync(object Id, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(Id, nameof(Id));
            var sel =await GetByIdAsync(cancellationToken, Id);
            Entities.Remove(sel);
            if (saveNow)
                await _dbContext.SaveChangesAsync(cancellationToken);
        }


        public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.RemoveRange(entities);
            if (saveNow)
                await _dbContext.SaveChangesAsync(cancellationToken);
        }
        #endregion

        #region Sync Methods
        public virtual TEntity GetById(params object[] ids)
        {
            return Entities.Find(ids);
        }

        public virtual void Add(TEntity entity, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Add(entity);
            if (saveNow)
                _dbContext.SaveChanges();
        }

        public virtual void AddRange(IEnumerable<TEntity> entities, bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.AddRange(entities);
            if (saveNow)
                _dbContext.SaveChanges();
        }

        public virtual void Update(TEntity entity, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Update(entity);
            _dbContext.SaveChanges();
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities, bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.UpdateRange(entities);
            if (saveNow)
                _dbContext.SaveChanges();
        }

        public virtual void Delete(TEntity entity, bool saveNow = true)
        {
            Assert.NotNull(entity, nameof(entity));
            Entities.Remove(entity);
            if (saveNow)
                _dbContext.SaveChanges();
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities, bool saveNow = true)
        {
            Assert.NotNull(entities, nameof(entities));
            Entities.RemoveRange(entities);
            if (saveNow)
                _dbContext.SaveChanges();
        }
        #endregion

        #region Attach & Detach
        public async Task Attach(TEntity entity)
        {
            Assert.NotNull(entity, nameof(entity));
            if (_dbContext.Entry(entity).State == EntityState.Detached)
                Entities.Attach(entity);
        }
        public async Task DeAttach(TEntity entity)
        {
            Assert.NotNull(entity, nameof(entity));
            var entry = _dbContext.Entry(entity);
            if (entity is not null)
                entry.State = EntityState.Detached;
        }

        #endregion

        #region Explicit Loading
        public virtual async Task LoadCollectionAsync<Property>(
            TEntity entity,
            Expression<Func<TEntity, IEnumerable<Property>>> collection,
            CancellationToken cancellationToken)
            where Property : class
        {
            await Attach(entity);
            var coll = _dbContext.Entry(entity).Collection(collection);
            if (!coll.IsLoaded)
            {
                await coll.LoadAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        public virtual async Task LoadRefrenceAsync<Refrence>(
            TEntity entity,
            Expression<Func<TEntity, Refrence>> refrence,
            CancellationToken cancellationToken
            )
            where Refrence : class
        {
            await Attach(entity);
            var refrenc = _dbContext.Entry(entity).Reference(refrence);
            if (!refrenc.IsLoaded)
            {
                await _dbContext.Entry(entity).ReloadAsync(cancellationToken).ConfigureAwait(false);
            }
        }
        #endregion
    }
}
