using Backend.Models;

namespace Backend.Repositories
{
    public interface IStudentRepository<TEntity, TPk> where TEntity : EntityBase
    {
        Task<CollectionRespons<TEntity>> GetAllAsync();
        Task<SingleObjectRespons<TEntity>> GetByIdAsync(TPk id);
        Task<SingleObjectRespons<TEntity>> CreateAsync(TEntity entity);
        Task<SingleObjectRespons<TEntity>> UpdateAsync(TPk id, TEntity entity);
        Task<SingleObjectRespons<TEntity>> DeleteAsync(TPk id);
        Task<CollectionRespons<TEntity>> GetPendingStudentsAsync(TPk collegeId);
    }
}