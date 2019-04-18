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
            return View();
        }
        [HttpGet]
        public dynamic Data(int page,int limit)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("", "");
            
            string nonce = DataTransfer.GetNonce().ToString();
            string timestamp = DataTransfer.GetTimeStamp();
            string signature = DataTransfer.GetMD5Staff(dic, timestamp, nonce);

            string result = HttpClientHelper.SendRequest("api/Orders/Show", "get", timestamp, nonce, signature, "");
            List<Orders> list = JsonConvert.DeserializeObject<List<Orders>>(result);
            var data= new {
                code=0,
                msg="",
                count=list.Count(),
                data=list
            };
            return JsonConvert.SerializeObject(data);
        }
        [HttpPost]
        public string Upt(int uid,string name,string sex,string cid)
        {
            UserInfos u = new UserInfos()
            {
                UserInfo_Id = uid,
                UserInfo_Name = name,
                UserInfo_Sex = sex == "男" ? 0 : 1,
                ID_Num = cid
            };
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("UserInfo_Id", uid.ToString());
            dic.Add("UserInfo_Name", name);
            dic.Add("UserInfo_Sex", sex == "男" ? "0" : "1");
            dic.Add("ID_Num", cid);
            dic.Add("Phone_Num", "");
            dic.Add("Address", "");
            dic.Add("HeadImg", "");


            string nonce = DataTransfer.GetNonce().ToString();
            string timestamp = DataTransfer.GetTimeStamp();
            string signature = DataTransfer.GetMD5Staff(dic, timestamp, nonce);
            string result = HttpClientHelper.SendRequest("api/Orders/Show", "post", timestamp, nonce, signature, JsonConvert.SerializeObject(u));
            return result;
        }
    }
}