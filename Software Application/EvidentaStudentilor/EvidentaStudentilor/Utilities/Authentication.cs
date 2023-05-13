using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EvidentaStudentilor.Utilities
{
    public class Authentication : ActionFilterAttribute
    {
        public static FilterContext FilterContext { get; set; }    
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            FilterContext = filterContext;
            if (filterContext.HttpContext.Session.GetString("UserName") == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary {
                                { "Controller", "Home" },
                                { "Action", "Index" }
                            });
            }

        }
    }
}