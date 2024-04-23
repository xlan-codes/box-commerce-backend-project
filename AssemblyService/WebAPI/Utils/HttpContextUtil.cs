using System.Security.Claims;

namespace WebAPI.Utils
{
    public static class HttpContextUtil
    {
        public static string GetCurrentUsername(HttpContext httpContext)
        {
            var identity = httpContext.User.Identity as ClaimsIdentity;
            var username = identity?.Claims.Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")
                .Select(c => c.Value)
                .FirstOrDefault();
            if (username is null)
            {
                return "anonymous";
            }
            return username;
        }
    }
}
