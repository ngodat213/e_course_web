using Newtonsoft.Json;

namespace e_course_web.Helpers
{
    public static class SessionHelper
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            //SerializeObject: chuyển object thành chuỗi json
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static void SetJson(this ISession session, string key, object value)
        {
            session.SetString(key, value);
        }
        public static T? GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            //DeserializeObject: chuyển chuỗi json thành object
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
