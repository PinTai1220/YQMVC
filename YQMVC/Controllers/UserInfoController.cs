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
            int count = userinfos.Count();
            var dt= userinfos.Skip((page - 1) * limit).Take(limit).ToList();
            var data = new
            {
                code = 0,
                msg = "",
                count,
                data = dt
            };
            return JsonConvert.SerializeObject(data);
        }
    }
}