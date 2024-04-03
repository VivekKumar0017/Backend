using Backend.Models;

namespace Backend.Repositories
{
    public interface IDataRepository<TEntity, in TPk> where TEntity : EntityBase
    {
        Task<CollectionRespons<TEntity>> GetAsync();
        Task<SingleObjectRespons<TEntity>> GetAsync(string Name);
        Task<SingleObjectRespons<TEntity>> CreateAsync(TEntity entity);
        Task<SingleObjectRespons<TEntity>> UpdateAsync(TPk id, TEntity entity);
        Task<SingleObjectRespons<TEntity>> DeleteAsync(TPk id);
        Task<SingleObjectRespons<TEntity>> ReviewStudentApplicationAsync(TPk CollegeId, TPk admissionId, ApprovalStatus status);

        Task<SingleObjectRespons<College>> GetCollegeByIdAsync(TPk id);
    }
}
