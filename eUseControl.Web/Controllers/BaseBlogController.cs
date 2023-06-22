using eUseControl.BusinessLogic.Services;
using eUseControl.Web.Extensions;
using System.Web.Mvc;

namespace eUseControl.Controllers
{
	public abstract class eUseBaseController : Controller
	{
		//public const string SESSION_COOKIE_NAME = "SessionToken";
		//
		//public HttpStatusCodeResult HttpNoPermission() => new HttpStatusCodeResult(403);
		//
		//protected override void OnActionExecuting(ActionExecutingContext filterContext)
		//{
		//	var sessionCookie = Request.Cookies[SESSION_COOKIE_NAME];
		//	if (sessionCookie == null)
		//		return;
		//
		//	var token = sessionCookie.Value;
		//	var sessionResp = SessionService.GetByToken(token);
		//	if (!sessionResp.Success)
		//		return;
		//
		//	var session = sessionResp.Entry;
		//	if (session == null)
		//		return;
		//
		//	var userResp = UserService.GetById(session.UserId);
        //    if (!userResp.Success)
        //        return;
		//
        //    var user = userResp.Entry;
        //    if (user == null)
		//		return;
		//
		//	Session.SetUser(user);
		//	ViewBag.SessionUser = user;
		//
		//	base.OnActionExecuting(filterContext);
		//}
	}
}