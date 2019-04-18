using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YQMVC.Filter;

namespace YQMVC.Controllers
{
    /// <summary>
    /// 金额表
    /// </summary>
    public class MoneyController : Controller
    {
        // GET: Money
        public ActionResult Index()
        {
            return View();
        }
    }
}