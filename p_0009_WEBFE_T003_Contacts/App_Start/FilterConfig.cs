using System.Web;
using System.Web.Mvc;

namespace p_0009_WEBFE_T003_Contacts
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
