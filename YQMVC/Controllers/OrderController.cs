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
    /// 订单表
    /// </summary>
    public class OrderController : Controller
    {
        // GET: Order
        [LoginAuthorization]
        public ActionResult Show()
        {
            return View();
        }
        [HttpGet]
        [LoginAuthorization]
        public dynamic Data(int page, int limit)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("", "");

            string nonce = DataTransfer.GetNonce().ToString();
            string timestamp = DataTransfer.GetTimeStamp();
            string signature = DataTransfer.GetMD5Staff(dic, timestamp, nonce);

            string result = HttpClientHelper.SendRequest("api/Orders/Show", "get", timestamp, nonce, signature, "");
            List<Orders> list = JsonConvert.DeserializeObject<List<Orders>>(result);
            var data = new
            {
                code = 0,
                msg = "",
                count = list.Count(),
                data = from a in list
                       select new
                       {
                            UserInfo_Name = a.UserInfo_Name,
                            Phone_Num = a.Phone_Num,
                            Checkln_Time=a.CheckIn_Time.Year+"-"+a.CheckIn_Time.Month+"-"+a.CheckIn_Time.Day,
                            Leave_Time="",
                            Money_Should=a.Money_Should,
                            Money_Favorable=a.Money_Favorable,
                            Money_Received=a.Money_Received,
                            Order_Time=a.Order_Time.Year+"-"+a.Order_Time.Month+"-"+a.Order_Time.Day,
                       }
            };
            return JsonConvert.SerializeObject(data);
        }
        [HttpPost]
        [LoginAuthorization]
        public string Upt(int uid, string name, string sex, string cid, int orderid)
        {
            UserInfos u = new UserInfos()
            {
                UserInfo_Id = uid,
                UserInfo_Name = name,
                UserInfo_Sex = sex == "男" ? 0 : 1,
                ID_Num = cid,
                Phone_Num = "",
                Address = "",
                HeadImg = "",
                orderid = orderid,
                state = 3
            };
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("UserInfo_Id", uid.ToString());
            dic.Add("UserInfo_Name", name);
            dic.Add("UserInfo_Sex", sex == "男" ? "0" : "1");
            dic.Add("ID_Num", cid);
            dic.Add("Phone_Num", "");
            dic.Add("Address", "");
            dic.Add("HeadImg", "");
            dic.Add("orderid", orderid.ToString());
            dic.Add("state", "3");



            string nonce = DataTransfer.GetNonce().ToString();
            string timestamp = DataTransfer.GetTimeStamp();
            string signature = DataTransfer.GetMD5Staff(dic, timestamp, nonce);
            string result = HttpClientHelper.SendRequest("api/userinfos/Update", "post", timestamp, nonce, signature, JsonConvert.SerializeObject(u));
            return result;
        }

        [RolesCheckIn]
        [LoginAuthorization]
        public ActionResult Money()
        {
            return View();
        }
    }

}