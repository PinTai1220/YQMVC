using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YQMVC.Helper;
using YQMVC.Models;
using Newtonsoft.Json;
using YQMVC.Filter;

namespace YQMVC.Controllers
{
    public class RomeController : Controller
    {
        public ActionResult Show()
        {
            return View();
        }
        [HttpGet]
        public dynamic Get(string sstate,int page,int limit)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("", "");

            string nonce = DataTransfer.GetNonce().ToString();
            string timestamp = DataTransfer.GetTimeStamp();
            string signature = DataTransfer.GetMD5Staff(dic, timestamp, nonce);

            string result = HttpClientHelper.SendRequest("api/rooms/Show", "get", timestamp, nonce, signature, "");
            List<Rooms> list = JsonConvert.DeserializeObject<List<Rooms>>(result);
            var da = (from a in list
                     where a.RoomType_Name == sstate
                     select a).Skip(page*limit).Take(limit).ToList();

            var data = new
            {
                code = 0,
                msg = "",
                count = list.Count(),
                data = list
            };
            return JsonConvert.SerializeObject(data);
        }
    }
}