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
        // GET: UserInfo
        public ActionResult UserInfoIndex()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("", "");

            string nonce = DataTransfer.GetNonce().ToString();
            string timestamp = DataTransfer.GetTimeStamp();
            string signature = DataTransfer.GetMD5Staff(dic, timestamp, nonce);

            string result = HttpClientHelper.SendRequest("api/UserInfos/Show", "get", timestamp, nonce, signature, "");
            List<UserInfos> userinfos = JsonConvert.DeserializeObject<List<UserInfos>>(result);
            return View(userinfos);
        }
    }
}