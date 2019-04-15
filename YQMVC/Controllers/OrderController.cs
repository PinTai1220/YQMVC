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
            string result1 = HttpClientHelper.SendRequest("api/UserInfos/Show", "get", timestamp, nonce, signature, "");
            List<UserInfos> userinfos = JsonConvert.DeserializeObject<List<UserInfos>>(result1);
            string result2 = HttpClientHelper.SendRequest("api/Rooms/Show", "get", timestamp, nonce, signature, "");
            List<Rooms> rooms = JsonConvert.DeserializeObject<List<Rooms>>(result2);
            var lists = from s in list
                        join t in userinfos on s.Order_Id equals t.UserInfo_Id
                        join e in rooms on s.Order_Id equals e.Room_Id
                        select new
                        {
                            Order_Id = s.Order_Id,
                            Phone_Num=t.Phone_Num,
                            Room_Id=e.Room_Id,
                            Checkln_Time=s.Checkln_Time,
                            Leave_Time=s.Leave_Time,
                            Money_Received=s.Money_Received,
                            Money_Favorable=s.Money_Favorable,
                            Money_Should=s.Money_Should,
                            Order_Time=s.Order_Time
                        };
            return View(lists);
        }
    }
    public enum State
    {
        已预定=0,
        已入住=1,
        未入住=3       
    }
}