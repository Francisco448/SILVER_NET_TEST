using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using REST_API.Models;
using SILVERNET_TEST.Controllers;

namespace SILVERNET_TEST.Permissions
{
    public class ValidateSessionAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var oUser = HttpContext.Current.Session["User"];
            if (oUser == null)
            {
                if (filterContext.Controller is accountController == false)
                {
                    filterContext.HttpContext.Response.Redirect("/account/Index");
                }
            }
            else
            {
                if (filterContext.Controller is ProfileController == false)
                {
                    filterContext.HttpContext.Response.Redirect("/Profile/Index");
                }
            }
        }
    }
}