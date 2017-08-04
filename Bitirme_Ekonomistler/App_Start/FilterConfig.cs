using System.Web;
using System.Web.Mvc;

namespace Bitirme_Ekonomistler
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
