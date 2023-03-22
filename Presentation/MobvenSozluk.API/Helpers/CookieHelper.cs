namespace MobvenSozluk.API.Helpers
{
    public static class CookieHelper
    {
        
        public static void SetCookie(this HttpResponse response, string name, string value, DateTime? expires = null)
        {
            response.Cookies.Append(name, value, new CookieOptions
            {
                HttpOnly = true,
                Expires = expires 
            });
        }
    }
}
