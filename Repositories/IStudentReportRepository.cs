using Backend.Models;

namespace Backend.Repositories
{
    public interface IStudentReportRepository<TEntity,in TPk> where TEntity : EntityBase
    {
        
        Task<SingleObjectRespons<TEntity>> GetReportByIdAsync(TPk id);
        Task<SingleObjectRespons<TEntity>> CreateReportAsync(TEntity entity);
        Task<SingleObjectRespons<TEntity>> UpdateReportAsync(TPk id, TEntity entity);
        Task<SingleObjectRespons<TEntity>> DeleteAsync(TPk id);


    }
}
