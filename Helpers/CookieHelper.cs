using NuGet.Packaging.Signing;

namespace e_course_web.Helpers
{
    public static class CookieHelper
    {
        public static void SetOrRemoveCookie(this IResponseCookies cookies, string key, string value, int expiresIn, bool isRemove = false)
        {
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(isRemove ? -1: expiresIn),
            };
            cookies.Append(isRemove ? "" : key, value);
        }

        public static string GetCookie(this IRequestCookieCollection cookieCollection, string key)
        {
            return cookieCollection[key];
        }
    }
}
