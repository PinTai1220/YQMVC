using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YQMVC.Models;
using YQMVC.Helper;
using Newtonsoft.Json;
using YQMVC.Filter;

namespace YQMVC.Controllers
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class UserInfoController : Controller
    {
        [LoginAuthorization]
        public ActionResult UserInfoIndex()
        {
            return View();
        }
        // GET: UserInfo
        [HttpGet]
        [LoginAuthorization]
        public dynamic UserInfoGet(int page, int limit)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("", "");

            string nonce = DataTransfer.GetNonce().ToString();
            string timestamp = DataTransfer.GetTimeStamp();
            string signature = DataTransfer.GetMD5Staff(dic, timestamp, nonce);

            string result = HttpClientHelper.SendRequest("api/UserInfos/Show", "get", timestamp, nonce, signature, "");
            List<UserInfos> userinfos = JsonConvert.DeserializeObject<List<UserInfos>>(result);
            var data = new
            {
                code = 0,
                msg = "",
                count = userinfos.Count(),
                data = from a in userinfos
                       select new
                       {
                           UserInfo_Name = a.UserInfo_Name,
                           Sex = a.UserInfo_Sex == 0 ? "男" : "女",
                           ID_Num = a.ID_Num,
                           Phone_Num = a.Phone_Num
                       }
            };
            return JsonConvert.SerializeObject(data);
        }
    }
}