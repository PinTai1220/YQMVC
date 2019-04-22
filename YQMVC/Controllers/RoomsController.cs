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
    /// <summary>
    /// 房间表
    /// </summary>
    public class RoomsController : Controller
    {
        // GET: Rooms
        [LoginAuthorization]
        public ActionResult RoomsIndex()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("", "");

            string nonce = DataTransfer.GetNonce().ToString();
            string timestamp = DataTransfer.GetTimeStamp();
            string signature = DataTransfer.GetMD5Staff(dic, timestamp, nonce);

            string result = HttpClientHelper.SendRequest("api/Rooms/Show", "get", timestamp, nonce, signature, "");
            string roomType = HttpClientHelper.SendRequest("api/RoomType/Show", "get", timestamp, nonce, signature, "");
            List<Rooms> rooms = JsonConvert.DeserializeObject<List<Rooms>>(result);
            List<RoomType> types = JsonConvert.DeserializeObject<List<RoomType>>(roomType);
            var roomResult = from c in rooms
                             join s in types
                             on c.RoomType_Id equals s.RoomType_Id
                             select new
                             {
                                 c.Room_Num,
                                 s.RoomType_Name,
                                 c.Room_State,
                                 s.RoomType_Price
                             };
            return View(roomResult);
        }
        [LoginAuthorization]
        public ActionResult Show()
        {
            return View();
        }
        [HttpGet]
        [LoginAuthorization]
        public dynamic Get(string state, int page, int limit)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("", "");


            string nonce = DataTransfer.GetNonce().ToString();
            string timestamp = DataTransfer.GetTimeStamp();
            string signature = DataTransfer.GetMD5Staff(dic, timestamp, nonce);


            string result = HttpClientHelper.SendRequest("api/rooms/Show", "get", timestamp, nonce, signature, "");
            List<Rooms> list = JsonConvert.DeserializeObject<List<Rooms>>(result);
            var da = (from a in list
                      where a.RoomType_Name == state
                      select a).ToList();
            var ta = da.Skip((page-1) * limit).Take(limit).ToList();

            var data = new
            {
                code = 0,
                msg = "",
                count = da.Count(),
                data = ta
            };
            return JsonConvert.SerializeObject(data);
        }
    }
}