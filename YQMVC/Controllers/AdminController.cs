using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using YQMVC.Helper;

namespace YQMVC.Controllers
{
    /// <summary>
    /// 管理员表
    /// </summary>
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            Dictionary<string, string> datas = new Dictionary<string, string>();
            string signature = DataTransfer.DataTransfer.GetMD5Staff(datas);       // 获得公钥
            int nonce = DataTransfer.DataTransfer.GetNonce();            // 获取随机数
            string timestamp = DataTransfer.DataTransfer.GetTimeStamp();        // 获取时间戳
            HttpClientHelper.SendRequest("", "",  timestamp, nonce.ToString(), signature,"");

            return View();
        }
    }
}