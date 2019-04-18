using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YQMVC.Filter;

namespace YQMVC.Controllers
{
    /// <summary>
    /// 房间类型表
    /// </summary>
    public class RoomTypeController : Controller
    {
        // GET: RoomType
        [LoginAuthorization]
        public ActionResult Index()
        {
            return View();
        }
    }
}