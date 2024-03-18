﻿using e_course_web.DataQuery;
using e_course_web.Models;

namespace e_course_web.Repository
{
    public interface IRepository<T>
    {
        Task<T> GetAsync(String domain, String address);
        Task<T> GetAsync(string id, String domain, String address);
        Task<bool> PostAsync(T entity, String domain, String address);
        Task<bool> DeleteAsync(string id, String domain, String address);
        Task<bool> PutAsync(string id, String domain, String address);
    }
}