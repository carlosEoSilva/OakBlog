using DTO;
using System.Web.Mvc;

namespace UI.Areas.Admin.Models.Attributes
{
    //note-18
    public class LoginControl : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (UserStatic.UserID == 0)
                filterContext.HttpContext.Response.Redirect("/Admin/Login/Index");
        }
    }
}