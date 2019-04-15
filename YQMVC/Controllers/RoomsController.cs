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
    /// 房间表
    /// </summary>
    public class RoomsController : Controller
    {
        // GET: Rooms
        public ActionResult RoomsIndex()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("", "");

            string nonce = DataTransfer.GetNonce().ToString();
            string timestamp = DataTransfer.GetTimeStamp();
            string signature = DataTransfer.GetMD5Staff(dic, timestamp, nonce);

            string result = HttpClientHelper.SendRequest("api/Rooms/Show", "get", timestamp, nonce, signature, "");
            List<Rooms> rooms = JsonConvert.DeserializeObject<List<Rooms>>(result);
            return View(rooms);
        }
    }
}