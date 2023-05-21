using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EvidentaStudentilor.Utilities
{
    public class Authorize : ActionFilterAttribute
    {
        private static FilterContext FilterContext { get; set; }
        private string[] role;
        public Authorize(params string[] role) 
        {
            this.role = role;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            FilterContext = filterContext;
            string userRole = filterContext.HttpContext.Session.GetString("UserRole");
            bool authorize = false;

            foreach (string item in role) 
            {
                if (item == userRole) 
                { 
                    authorize = true;
                }
            }

            if (!authorize)
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