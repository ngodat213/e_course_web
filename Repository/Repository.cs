
using e_course_web.DataQuery;
using e_course_web.Manager;
using e_course_web.Models;

namespace e_course_web.Repository
{
    public class Repository<T, Result> : IRepository<T, Result> where T  : class
    {
        public Repository() { }
        // GET METHOD
        public async Task<Result> GetAsync(String domain, String address)
        {
            var res = await APICall.RunAsyncGetAll<Result>(domain, address);
            return res;
        }
        // GET BY ID METHOD
        public async Task<T> GetAsync(string id, String domain, String address)
        {
            var res = await APICall.RunAsyncGetAll<T>(domain, address, id);
            return res;
        }
        // POST METHOD
        public async Task<Result> PostAsync(T entity, String domain, String address)
        {
            return await APICall.RunAsyncCreate<T, Result>(domain, address, entity);
        }
        // PUT METHOD
        public async Task<bool> PutAsync(string id, String domain, String address)
        {
            return await APICall.RunAsyncPut<T>(domain, address, id);
        }
        // DELETE METHOD
        public async Task<bool> DeleteAsync(string id, string domain, string address)
        {
            return await APICall.RunAsyncDelete<T>(domain, address, id);
        }
    }
}
