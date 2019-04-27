using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using YQMVC.Models;

namespace YQMVC.Filter
{
    public class RolesCheckInAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //判断userName的值是否为空，如果为空表示用户未登录。
            //如果未登录，则转到Login控制器的Index方法。
            dynamic result = System.Web.HttpContext.Current.Session["Login"];
            Admin admin = result;
            // 判断是否是 经理
            if (admin.Role_Id != "1")
            {
                filterContext.Result = new RedirectResult("/Admin/Error");
            }
        }
    }
}