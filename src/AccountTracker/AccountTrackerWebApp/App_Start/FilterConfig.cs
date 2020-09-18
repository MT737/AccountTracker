using System.Web.Mvc;

namespace AccountTrackerWebApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new RequireHttpsAttribute()); //Forces MVC to redirect all requests using HTTP to use HTTPS
            filters.Add(new AuthorizeAttribute()); //Sets authorized as the required status. AllowAnonymous attribute needs to be used to make actions accessible to non-authorized users.
            filters.Add(new HandleErrorAttribute());
        }
    }
}
