namespace Backend.Models
{
    public class ResponsesObject<TEntity> where TEntity : class
    {
        public string? Message { get; set; }
        public int StatusCode { get; set; }
    }
    public class CollectionRespons<TEntity> : ResponsesObject<TEntity> where TEntity : class
    {
        public IEnumerable<TEntity>? Records { get; set; }
    }
    public class SingleObjectRespons<TEntity> : ResponsesObject<TEntity> where TEntity : class
    {
        public TEntity? Record { get; set; }
    }

}
