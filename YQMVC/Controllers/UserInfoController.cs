using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YQMVC.Models;
using YQMVC.Helper;
using Newtonsoft.Json;

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
            //string json = HttpClientHelper.SendRequest("http://localhost:54830/api/UserInfos/Show", "get");
            //List<UserInfos> ulist = JsonConvert.DeserializeObject<List<UserInfos>>(json); ;
            //return View(ulist);
            return View();
        }
    }
}