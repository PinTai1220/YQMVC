using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YQMVC.Controllers
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class UserInfoController : Controller
    {
        // GET: UserInfo
        public ActionResult UserInfoIndex()
        {

            return View();
        }
    }
}