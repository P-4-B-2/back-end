namespace backend.DAL.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetByID(int id);
        Task Insert(T obj);
        Task Delete(int id);
        Task Update(T obj);
        Task Save();
    }
}
