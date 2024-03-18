
using e_course_web.DataQuery;
using e_course_web.Manager;
using e_course_web.Models;

namespace e_course_web.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public Repository() { }
        
        public async Task<T> GetAsync(String domain, String address)
        {
            var res = await APICall.RunAsyncGetAll<T>(domain, address);
            return res;
        }

        public async Task<T> GetAsync(string id, String domain, String address)
        {
            var res = await APICall.RunAsyncGetAll<T>(domain, address, id);
            return res;
        }

        public async Task<bool> PostAsync(T entity, String domain, String address)
        {
            return await APICall.RunAsyncCreate<T>(domain, address, entity);
        }

        public async Task<bool> PutAsync(string id, String domain, String address)
        {
            return await APICall.RunAsyncPut<T>(domain, address, id);
        }

        public async Task<bool> DeleteAsync(string id, string domain, string address)
        {
            return await APICall.RunAsyncDelete<T>(domain, address, id);
        }
    }
}
