using System.Web;
using System.Web.Mvc;

namespace SILVERNET_TEST
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new Permissions.ValidateSessionAttribute());
        }
    }
}
