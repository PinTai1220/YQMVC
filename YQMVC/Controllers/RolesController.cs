using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YQMVC.Filter;

namespace YQMVC.Controllers
{
    /// <summary>
    /// 权限表
    /// </summary>
    public class RolesController : Controller
    {
        // GET: Roles
        [LoginAuthorization]
        public ActionResult Index()
        {
            return View();
        }
    }
}