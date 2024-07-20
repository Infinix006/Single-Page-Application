using System.Web;
using System.Web.Mvc;

namespace MACHINE_TEST_PRACTICE
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
