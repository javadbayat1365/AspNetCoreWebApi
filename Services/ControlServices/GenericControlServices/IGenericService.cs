using Entities.Common;

namespace Services.ControlServices.GenericControlServices
{
    public interface IGenericService<TEntity> where TEntity : class, IEntity
    {
        Task<TEntity> Create(TEntity user, CancellationToken cancellationToken);
        Task<bool> Delete(long Id, CancellationToken cancellationToken);
        Task<bool> Delete(TEntity user, CancellationToken cancellationToken);
        Task<List<TEntity>> GetAll(CancellationToken cancellationToken);
        Task<TEntity> GetById(long Id, CancellationToken cancellationToken);
        Task Update(TEntity user, CancellationToken cancellationToken);
    }
}