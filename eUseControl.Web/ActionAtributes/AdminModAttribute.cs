using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Enums;
using eUseControl.Web.Extensions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace eUseControl.Web.ActionAtributes
{
    public class AdminModAttribute : ActionFilterAttribute
    {
        private readonly ISession _session;

        public AdminModAttribute()
        {
            var businessLogic = new BusinessLogic.BusinessLogic();
            _session = businessLogic.GetSessionBL();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var apiCookie = HttpContext.Current.Request.Cookies["X-KEY"];
            if (apiCookie != null)
            {
                var profile = _session.GetUserByCookie(apiCookie.Value);
                if (profile != null && profile.AccessLevel == URole.ADMINISTRATOR)
                {
                    HttpContext.Current.SetMySessionObject(profile);
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "Home",
                        action = "Index"
                    }));
                }
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "Index"
                }));
            }
        }
    }
}