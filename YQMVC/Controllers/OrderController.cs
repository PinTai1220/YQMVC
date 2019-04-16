using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YQMVC.Helper;
using YQMVC.Models;
using Newtonsoft.Json;

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
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("", "");
            
            string nonce = DataTransfer.GetNonce().ToString();
            string timestamp = DataTransfer.GetTimeStamp();
            string signature = DataTransfer.GetMD5Staff(dic, timestamp, nonce);

            string result = HttpClientHelper.SendRequest("api/Order/Show", "get", timestamp, nonce, signature, "");
            List<Orders> list = JsonConvert.DeserializeObject<List<Orders>>(result);
            return View(list);
        }
    }
    public enum State
    {
        已预定=0,
        已入住=1,
        未入住=3       
    }
}