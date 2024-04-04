using e_course_web.Models;
using Microsoft.EntityFrameworkCore;

namespace e_course_web.Repositorys
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> entities;
        public Repository(ApplicationDbContext context) {
            _context = context;
            entities = context.Set<T>();
        }

        public async Task<T> GetById(int? id)
        {
            return await entities.FindAsync(id);
        }

        public IEnumerable<T> GetAll()
        {
            return  entities.ToList();
        }

        public void Add(T entity)
        {
            entities.Add(entity);
            _context.SaveChanges();
        }

        public async void Delete(T entity)
        {
            entities.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async void Update(T entity)
        {
            entities.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
