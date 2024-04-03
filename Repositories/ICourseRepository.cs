using Backend.Models;

namespace Backend.Repositories
{
    public interface ICourseRepository<TEntity, in TPk> where TEntity : EntityBase
    {
        Task<CollectionRespons<TEntity>> GetcourseAsync();
        Task<SingleObjectRespons<TEntity>> GetcourseAsync(string Name);
        Task<SingleObjectRespons<TEntity>> CreatecourseAsync(TEntity entity);
        Task<SingleObjectRespons<TEntity>> UpdatecourseAsync(TPk id, TEntity entity);
        Task<SingleObjectRespons<TEntity>> DeletecourseAsync(TPk id);
        Task<CollectionRespons<TEntity>> GetcourseByCollegeIdAsync(TPk id);

        Task<SingleObjectRespons<TEntity>> GetcourseByIdAsync(TPk id); 
    }
}
