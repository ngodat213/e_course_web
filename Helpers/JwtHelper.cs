using e_course_web.Helpers;
using e_course_web.Manager;
using e_course_web.Models;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace e_course_web.Utils
{
    public static class JwtHelper
	{
		public static void SaveToken(this ISession session, string jwt)
		{
			SessionHelper.SetJson(session, ManagerKeyStorage.KEY_TOKEN, jwt);
		}
		public static User DecodeToken(string? jwt)
		{
			try
			{
				var hander = new JwtSecurityTokenHandler();
				var token = hander.ReadJwtToken(jwt);
				if (token != null)
				{
					var userJson = token.Payload.SerializeToJson();
					var user = JsonConvert.DeserializeObject<User>(userJson);
					return user;
				}
				Console.WriteLine("Invalid JWT Token.");
				return null;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}
		}
	}
}
