using eUseControl.Web.Extensions;
using System.Web.Mvc;

namespace eUseControl.Web.Filters
{
	public class RequireUserLoginAttribute : ActionFilterAttribute
	{
		//public override void OnActionExecuting(ActionExecutingContext filterContext)
		//{
		//	var session = filterContext.HttpContext.Session;
		//	if (!session.IsUserLoggedIn())
		//	{
		//		// No permission - show 403 page.
		//		filterContext.Result = new HttpStatusCodeResult(403);
		//		return;
		//	}
		//}
	}
}