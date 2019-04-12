using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YQMVC.Controllers
{
    /// <summary>
    /// 管理员表
    /// </summary>
    public class AdminController : Controller
    {
        //登录
        // GET: Admin
        public ActionResult Login()
        {
            return View();
        }
    }
}