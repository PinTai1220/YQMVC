using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YQMVC.Helper;
using YQMVC.Models;

namespace YQMVC.Controllers
{
    /// <summary>
    /// 订单表
    /// </summary>
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Show()
        {
            string n = DataTransfer.GetNonce().ToString();
            string chuo = DataTransfer.GetTimeStamp();            
            DataTransfer.GetMD5Staff(,chuo,n);
            string result = HttpClientHelper.SendRequest("get", "api/Order/Show",chuo,n,);
            return View();
        }
    }
}