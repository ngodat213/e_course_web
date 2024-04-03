namespace e_course_web.Repositorys
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetById(int? id);
        IEnumerable<T> GetAll();

        void Add(T value); 
        void Update(T value);
        void Delete(T entity);
    }
}
