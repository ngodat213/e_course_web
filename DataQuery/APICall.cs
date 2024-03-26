using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace e_course_web.DataQuery
{
    public static class APICall
    {
        private static HttpClient GetHttpClient(string url)
        {
            var client = new HttpClient { BaseAddress = new Uri(url) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
        // GET ALL and ID
        private static async Task<T> GetAsync<T>(string url, string urlParameters, string id = "")
        {
            try
            {
                using (var client = GetHttpClient(url))
                {
                    HttpResponseMessage response;
                    if (id != "")
                    {
                        response = await client.GetAsync(urlParameters + "/" + id);
                    }
                    else
                    {
                        response = await client.GetAsync(urlParameters);
                    }

                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        var data = JsonConvert.DeserializeObject<T>(result);
                        return data;
                    }
                    return default;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default;
            }
        }
        // POST
        private static async Task<Result> PostAsync<T, Result>(string url,string urlParameters, T obj)
        {
            try
            {
                using (var client = GetHttpClient(url))
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync<T>(urlParameters, obj);

                    if (response.IsSuccessStatusCode)
                    {
						string result = response.Content.ReadAsStringAsync().Result;
						var data = JsonConvert.DeserializeObject<Result>(result);
						return data;
					}
                    else
                    {
                        return default;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default;
            }
        }
        // PUT
        private static async Task<bool> PatchAsync<T>(string url, string urlParameters, string id, T obj)
        {
            try
            {
                using (var client = GetHttpClient(url))
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync<T>(urlParameters + '/' + id, obj);

                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        // DELETE
        private static async Task<bool> DeleteAsync<T>(string url, string urlParameters, string id)
        {

            try
            {
                using (var client = GetHttpClient(url))
                {
                    HttpResponseMessage response = await client.DeleteAsync(urlParameters + '/' + id);

                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // API CALL
        public static async Task<T> RunAsyncGetAll<T>(string url, string urlParameters, string id = "")
        {
            return await GetAsync<T>(url, urlParameters, id);
        }

        public static async Task<Result> RunAsyncCreate<T, Result>(string url, string urlParameters, T obj)
        {
            return await PostAsync<T, Result>(url, urlParameters, obj);
        }

        public static async Task<bool> RunAsyncDelete<T>(string url, string urlParameters, string id)
        {
            return await DeleteAsync<T>(url, urlParameters, id);
        }
        public static async Task<bool> RunAsyncPut<T>(string url, string urlParameters, string id)
        {
            return await RunAsyncPut<T>(url, urlParameters, id);
        }
    }
}