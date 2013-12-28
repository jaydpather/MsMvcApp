using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManager.Controllers
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class MultiButtonAttribute : ActionNameSelectorAttribute
    {
        public string MatchFormKey { get; set; }
        public string MatchFormValue { get; set; }

        public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
        {
            return controllerContext.HttpContext.Request[MatchFormKey] != null &&
                controllerContext.HttpContext.Request[MatchFormKey] == MatchFormValue;
        }
    }

    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginReadOnly()
        {
            return OnLogin(true);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginReadWrite()
        {
           return OnLogin(false);
        }

        private ActionResult OnLogin(bool isReadOnly)
        {
            Models.User u = new Models.User();
            u.ReadOnly = isReadOnly;
            GlobalData.CurrentUser = u;
            return new RedirectResult("~/Employee");
        }
	}
}