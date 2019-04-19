using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YQMVC.Filter
{
    public class LoginAuthorization: AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //判断userName的值是否为空，如果为空表示用户未登录。
            //如果未登录，则转到Login控制器的Index方法。
            if (System.Web.HttpContext.Current.Session["Admin"] == null)
            {
                filterContext.Result = new RedirectResult("/Admin/Login");
            }
        }

    }
}